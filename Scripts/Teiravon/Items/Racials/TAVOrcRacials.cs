using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Engines.Craft;
using Server.Items;

namespace Server.Items
{

	public class OrcMace : BaseBashing
	{
		public override int AosStrengthReq{ get{ return 60; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 19; } }
		public override int AosSpeed{ get{ return 28; } }

		public override int OldStrengthReq{ get{ return 80; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 32; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 51; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public OrcMace() : base( 0x27A6 )
		{
			Name = "Dire Mace";
			Weight = 15.0;
			Layer = Layer.TwoHanded;
		}

		public OrcMace( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if( version < 1)
            {
                MinDamage = 18;
                MaxDamage = 19;
                Speed = 28;
            }
		}
	}

	[FlipableAttribute( 11559, 11571 )]
	public class OrcScimitar : BaseSword
	{

		public override int AosStrengthReq{ get{ return 60; } }
		public override int AosMinDamage{ get{ return 19; } }
		public override int AosMaxDamage{ get{ return 20; } }
		public override int AosSpeed{ get{ return 23; } }

		public override int OldStrengthReq{ get{ return 80; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 32; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 51; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public OrcScimitar() : base( 11559 )
		{
			Name = "Claymore";
			Weight = 15.0;
			Layer = Layer.TwoHanded;
		}

		public OrcScimitar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if (version < 1){
                MinDamage = 19;
                MaxDamage = 20;
                Speed = 23;
            }
		}
	}

	[FlipableAttribute( 0x13eb, 0x13f2 )]
	public class OrcGloves : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 35; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 22; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Studded; } }

		[Constructable]
		public OrcGloves() : base( 0x13EB )
		{
			Name = "Spiked Gloves";
			Weight = 2.0;
			Attributes.WeaponDamage = 10;
		}

		public OrcGloves( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}

	public class OrcSpikedHelm : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 30; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 20; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public OrcSpikedHelm() : base( 0x1F0B )
		{
			Name = "Spiked Orc Helm";
			Weight = 3;
			Attributes.WeaponDamage = 10;
		}

		public OrcSpikedHelm( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}


	[FlipableAttribute( 0x170b, 0x170c )]
	public class OrcBoots : BaseShoes
	{
		[Constructable]
		public OrcBoots() : this( 0 )
		{
		}

		[Constructable]
		public OrcBoots( int hue ) : base( 0x170B, hue )
		{
			Name = "Spiked Boots";
			Weight = 5.0;
			Attributes.WeaponDamage = 10;
		}

		public OrcBoots( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcShamanMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		[Constructable]
		public OrcShamanMask() : this( 0 )
		{
		}

		[Constructable]
		public OrcShamanMask( int hue ) : base( 0x1545, hue )
		{
			Name = "Orc Shaman Mask";
			Weight = 1.0;
			Attributes.RegenMana = 1;
			Attributes.LowerManaCost = 5;
		}

		public OrcShamanMask( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcWhetstone : Item
	{
		private int m_uses;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_uses; }
			set{ m_uses = value; InvalidateProperties(); }
		}

		[Constructable]
		public OrcWhetstone() : base(0x1BF2)
		{
			Name = "Whetstone";
			Weight = 1.0;
			Hue = 448;
			Stackable = false;
			m_uses = 10;
		}

		public OrcWhetstone( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Target the weapon to sharpen");
			from.Target = new WhetstoneTarget( from, this );
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060584, "{0}", m_uses );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060584, String.Format( "{0}", m_uses ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_uses );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_uses = reader.ReadInt();
		}

	}

	public class OrcSplint : Item
	{

		[Constructable]
		public OrcSplint() : base(0xDF6)
		{
			Name = "Splint";
			Weight = 2.0;
		}

		public OrcSplint( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Target the person to splint");
			from.Target = new SplintTarget( from, this );
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcMrog : BeverageBottle
	{

		[Constructable]
		public OrcMrog(): base( BeverageType.Liquor)
		{
			Weight = 1;
			Name = "Mrog";
			Hue = 337;
		}

		public OrcMrog( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class OrcAle : BeverageBottle
	{
		[Constructable]
		public OrcAle(): base( BeverageType.Ale)
		{
			Weight = 1;
			Name = "Blood Beer";
			Hue = 38;
		}

		public OrcAle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x13B2, 0x13B1 )]
	public class OrcHorngothBow : BaseRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 8; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 37; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 15; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public OrcHorngothBow() : base( 0x13B2 )
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			Name = "Orchish Horngoth Bow";
		}

		public OrcHorngothBow( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcSlingBullet : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Ballista bolt" : "{0} Ballista bolts", Amount );
			}
		}

		[Constructable]
		public OrcSlingBullet() : this( 1 )
		{
		}

		[Constructable]
		public OrcSlingBullet( int amount ) : base( 0xF21 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Name = "Sling Bullet";
			Hue = 958;
		}

		public OrcSlingBullet( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new OrcSlingBullet( amount ), amount );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x27AA, 0x27F5 )]
	public class OrcSling : BaseRanged
	{
        public override int EffectID { get { return 0x0F8B; } }
		public override Type AmmoType{ get{ return typeof( OrcSlingBullet ); } }
		public override Item Ammo{ get{ return new OrcSlingBullet(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 8; } }
		public override int AosMaxDamage{ get{ return 12; } }
		public override int AosSpeed{ get{ return 45; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 15; } }

		public override int InitMinHits{ get{ return 11; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Bash1H; } }

		[Constructable]
		public OrcSling() : base( 0x27AA )
		{
			Weight = 1.0;
			Layer = Layer.TwoHanded;
			Name = "Sling";
		}

		public OrcSling( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class OrcPalisadeSouthAddon : BaseAddon
	{
		bool decay;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PalisadeDecay
		{
			get { return decay; }
			set { decay = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan DecaysIn
		{
			get { return m_DecayTime - DateTime.Now; }
		}

		[Constructable]
		public OrcPalisadeSouthAddon()
		{
			AddComponent( new AddonComponent( 0x21F ), 0, 0, 0 );
            Name = "Orcish Palisade Deed";
			decay = true;
			m_DecayTime = DateTime.Now + TimeSpan.FromHours( 4.0 );
			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		private class InternalTimer : Timer
		{
			private OrcPalisadeSouthAddon m_Item;

			public InternalTimer( OrcPalisadeSouthAddon item, DateTime end ) : base( end - DateTime.Now )
			{
				m_Item = item;
			}

			protected override void OnTick()
			{
				if (m_Item != null)
				{
					if ( m_Item.PalisadeDecay )
					{
						m_Item.PublicOverheadMessage( Network.MessageType.Regular, 438, false, "The palisade collapses." );
						m_Item.Delete();
					}
				}
			}
		}

		public OrcPalisadeSouthAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcPalisadeSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new OrcPalisadeSouthAddon(); } }

		[Constructable]
		public OrcPalisadeSouthDeed()
		{
		}

		public OrcPalisadeSouthDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcPalisadeEastAddon : BaseAddon
	{
		bool decay;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PalisadeDecay
		{
			get { return decay; }
			set { decay = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan DecaysIn
		{
			get { return m_DecayTime - DateTime.Now; }
		}

		[Constructable]
		public OrcPalisadeEastAddon()
		{
			AddComponent( new AddonComponent( 0x220 ), 0, 0, 0 );
			decay = true;
			m_DecayTime = DateTime.Now + TimeSpan.FromHours( 4.0 );
			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		public OrcPalisadeEastAddon( Serial serial ) : base( serial )
		{
		}

		private class InternalTimer : Timer
		{
			private OrcPalisadeEastAddon m_Item;

			public InternalTimer( OrcPalisadeEastAddon item, DateTime end ) : base( end - DateTime.Now )
			{
				m_Item = item;
			}

			protected override void OnTick()
			{
				if (m_Item != null)
				{
					if ( m_Item.PalisadeDecay )
					{
						m_Item.PublicOverheadMessage( Network.MessageType.Regular, 438, false, "The palisade collapses." );
						m_Item.Delete();
					}
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcPalisadeEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new OrcPalisadeEastAddon(); } }

		[Constructable]
		public OrcPalisadeEastDeed()
		{
            Name = "Orcish Palisade Deed";
		}

		public OrcPalisadeEastDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcCombatBridgeAddon : BaseAddon
	{
		bool decay;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool BridgeDecay
		{
			get { return decay; }
			set { decay = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan DecaysIn
		{
			get { return m_DecayTime - DateTime.Now; }
		}

		[Constructable]
		public OrcCombatBridgeAddon()
		{
			AddComponent( new AddonComponent( 0x50B ), 0, 0, 0 );
			decay = true;
			m_DecayTime = DateTime.Now + TimeSpan.FromHours( 4.0 );
			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		public OrcCombatBridgeAddon( Serial serial ) : base( serial )
		{
		}

		private class InternalTimer : Timer
		{
			private OrcCombatBridgeAddon m_Item;

			public InternalTimer( OrcCombatBridgeAddon item, DateTime end ) : base( end - DateTime.Now )
			{
				m_Item = item;
			}

			protected override void OnTick()
			{
				if (m_Item != null)
				{
					if ( m_Item.BridgeDecay )
					{
						m_Item.PublicOverheadMessage( Network.MessageType.Regular, 438, false, "The bridge collapses." );
						m_Item.Delete();
					}
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcCombatBridgeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new OrcCombatBridgeAddon(); } }

		[Constructable]
		public OrcCombatBridgeDeed()
		{
            Name = "Orcish Combat Bridge Deed";
		}

		public OrcCombatBridgeDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class OrcGrowthPotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.OrcGrowth; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 10 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public OrcGrowthPotion() : base( PotionEffect.OrcGrowth )
		{
			Name = "Potion of Growth";
			Hue = 608;
		}

		public OrcGrowthPotion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Drink( Mobile from )
		{
			TeiravonMobile m_Player = (TeiravonMobile) from;

			if ( m_Player.CanDrink( PEffect ) )
			{
				StatMod OrcGrowthSTRMod = new StatMod( StatType.Str, (int)m_Player.Serial + " Growth Str", 50, TimeSpan.FromMinutes( 10 ) );
				m_Player.AddStatMod( OrcGrowthSTRMod );
				
				if (m_Player.IsOrc())
					m_Player.OBody = 0xBD; // orc brute
				else if (m_Player.IsDwarf())
					m_Player.OBody = 0x4C; // Titan
				else if (m_Player.IsDrow())
					m_Player.OBody = 0xAD; //Mephitis
				else if (m_Player.IsElf())
					m_Player.OBody = 0x12D; // tree fellow
				else if (m_Player.IsHuman())
					m_Player.OBody = 0x137; // shadow knight
					
			}

			base.Drink( from );
		}
	}


}

namespace Server.Targets
{

	public class WhetstoneTarget : Target
	{
		private TeiravonMobile m_Player;
		private BaseWeapon wep;
		private OrcWhetstone stone;
		
		public WhetstoneTarget( Mobile from, OrcWhetstone wstone) : base( -1, false, TargetFlags.None )
		{
			m_Player = (TeiravonMobile)from;
			stone = wstone;
		}

		protected override void OnTarget( Mobile from, object targ )
		{
			if (!(targ is BaseWeapon))
			{
				from.SendMessage( "That is not a weapon!");
			}
			else
			{
				wep = (BaseWeapon)targ;
				if (wep is BaseSword || wep is BaseSpear || wep is BasePoleArm || wep is BaseKnife || wep is BaseAxe)
				{
					if (wep.MaxHits < 3)
					{
						wep.Delete();
						from.SendMessage("You destroyed the weapon");
						stone.Uses -= 1;
						if (stone.Uses < 1)
						{
							stone.Delete();
							from.SendMessage("Your whetstone has worn out");
						}
					}
					else
					{
						wep.MaxHits -= 2;
						wep.Hits = wep.MaxHits;
						stone.Uses -= 1;
						if (stone.Uses < 1)
						{
							stone.Delete();
							from.SendMessage("Your whetstone has worn out");
						}
					}
				}
				else
				{
					from.SendMessage("You cannot sharpen that weapon");
				}
			}
		}
	}

	public class SplintTarget : Target
	{
		private TeiravonMobile m_player;
		private TeiravonMobile m_targ;
		private OrcSplint splint;
		private Timer m_timer;
		
		public SplintTarget( Mobile from, OrcSplint splnt) : base( -1, false, TargetFlags.None )
		{
			m_player = (TeiravonMobile)from;
			splint = splnt;
		}

		protected override void OnTarget( Mobile from, object targ )
		{
			if (!(targ is TeiravonMobile))
			{
				from.SendMessage( "You can only splint that!");
			}
			else
			{
				m_player = (TeiravonMobile)from;
				m_targ = (TeiravonMobile)targ;
				if (m_targ == m_player)
					m_player.SendMessage("You cannot splint yourself!");
				else if (!m_targ.Alive)
					m_player.SendMessage("That person is not alive!");
				else if (!(m_player.InRange( m_targ.Location, 2 )))
				{
					m_player.SendMessage("You are too far away");
				}
				else
				{
					Container pack = m_player.Backpack;
					if (pack != null)
					{
						BaseWeapon wpn = (BaseWeapon)m_player.Weapon;
						pack.DropItem(wpn);
					}
					BaseWeapon.BlockEquip( m_player, TimeSpan.FromSeconds( 6.0 ) );

					int healmin = (int)((m_player.Skills.Healing.Value + m_player.Skills.Anatomy.Value)/3);
					int healmax = (int)((m_player.Skills.Healing.Value + m_player.Skills.Anatomy.Value)/2);
					int healamt = Utility.RandomMinMax(healmin, healmax);
					
					m_player.Emote("Begins splinting {0}", m_targ.Name);
				
					m_timer = new InternalTimer( m_player, m_targ, healamt, splint );
					m_timer.Start();
				}
			}
		}
		
		private class InternalTimer : Timer
		{
			private TeiravonMobile player;
			private TeiravonMobile target;
			private int healpts;
			private OrcSplint splint;

			public InternalTimer( TeiravonMobile play, TeiravonMobile targ, int heal, OrcSplint splnt ) : base( TimeSpan.FromSeconds( 6.0 ) )
			{
				player = play;
				target = targ;
				healpts = heal;
				splint = splnt;
			}

			protected override void OnTick()
			{
				if (!(target.Alive))
					player.SendMessage("Your patient died before you could finish");
				else if (!(player.Alive))
					player.SendMessage("You died before you could heal your patient");
				else if (!(player.InRange( target.Location, 2 )))
					player.SendMessage("You moved too far from your patient");
				else
				{
					if (target.Hits + healpts > target.MaxHits)
						target.Hits = target.MaxHits;
					else
						target.Hits += healpts;
					
					player.SendMessage("You finish splinting your patient");
					splint.Delete();
				}
			}
		}
		
	}
	
}
