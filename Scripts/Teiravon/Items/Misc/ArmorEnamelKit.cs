using System;
using Server.Items;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.HuePickers;



namespace Server.Items
{

	public class ArmorEnamelKit : Item
	{
		
		private int DyeHue;
		public CustomHuePicker CustomHuePicker{ get{ return CustomHuePicker.ArmorEnamelKit; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Color
		{
			get { return DyeHue; }
			set { DyeHue = value; InvalidateProperties(); }
		}

	
		[Constructable]
		public ArmorEnamelKit() : base( 0xFAB )
		{
			Name = "Armor Coloring Kit";
			Color = 0;
			LootType = LootType.Blessed;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_player = (TeiravonMobile)from;

				if ((m_player.IsBlacksmith() &&  m_player.HasFeat( TeiravonMobile.Feats.ArmorEnameling ) || (m_player.HasFeat( TeiravonMobile.Feats.BlacksmithTraining ) && m_player.IsMerchant() ) ) ) 
				{
					m_player.Target = new EnamelTarget(this.Color, this);
				}
				else
				{
					m_player.SendMessage ("Only Trained Blacksmiths can use this!");
				}
			}
		}
		
		public ArmorEnamelKit( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( DyeHue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version >= 1 )
				DyeHue = reader.ReadInt();
		}
		
		
	}

	public class EnamelTarget : Target
	{
		private int dhue;
		private ArmorEnamelKit d_dkit;
		
		public EnamelTarget(int d_hue, ArmorEnamelKit dkit) : base( -1, false, TargetFlags.None )
		{
			CheckLOS = true;
			dhue = d_hue;
			d_dkit = dkit;
		}
		
		protected override void OnTarget( Mobile from, object o )
		{
			int enamhue = dhue;
			ArmorEnamelKit m_dkit = d_dkit;
			
			if (!(o is Item))
				return;
			
			if ( ((Item)o).IsChildOf( from.Backpack ) )
			{
				BaseArmor armor;
				
				if (o is ArmorEnamelKit)
				{
					ArmorEnamelKit enamkit = (ArmorEnamelKit)o;
					from.SendGump( new CustomHuePickerGump( from, m_dkit.CustomHuePicker, new CustomHuePickerCallback( SetKitHue ), m_dkit ) );
					return;
				}
				
				if (o is BaseArmor)
				{
					armor = (BaseArmor) o;
				}
				else
				{
					from.SendMessage ("Only Armor can be colored with this kit");
					return;
				}
				
				if ( armor.MaterialType == ArmorMaterialType.Plate || armor.MaterialType == ArmorMaterialType.Ringmail || armor.MaterialType == ArmorMaterialType.Chainmail || (armor is BaseShield && !(armor is WoodenShield)))
				{
					armor.Hue = enamhue;
				}
				else
				{
					from.SendMessage ("Only metal armor and shields can be colored with this kit");
					return;
				}
			}
			else
			{
				from.SendMessage ("Items must be in your backpack to color");
				return;
			}
		
		}

		private static void SetKitHue( Mobile from, object state, int hue )
		{
			((ArmorEnamelKit)state).Color = hue;
			((ArmorEnamelKit)state).Hue = hue;
		}
		
	}
	
}
