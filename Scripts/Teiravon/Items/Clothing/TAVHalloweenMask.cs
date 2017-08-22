using System;
using Server;
using Server.Items;
using Server.Mobiles;


namespace Server.Items
{

	public class HalloweenMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }
		public int m_charges;
		public int m_origbody;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaskCharges
		{
			get{ return m_charges; }
			set{ m_charges = value; }
		}

		[Constructable]
		public HalloweenMask() : this( 0 )
		{
		}

		[Constructable]
		public HalloweenMask( int hue ) : base( 0x154B, hue )
		{
			Weight = 1.0;
			Name = "Halloween Mask";
			MaskCharges = 5;
		}
		
		public override void OnAdded( object parent )
		{
			int RndBody = Utility.RandomList(29, 51, 776, 304, 307, 319, 309, 301, 72, 153, 148, 4, 74, 24, 26, 777, 18, 31, 39, 154, 42, 50, 3, 128, 101, 779, 87, 30, 35);

			Mobile mob = parent as Mobile;
	
			if (mob != null)
			{
				if (mob is TeiravonMobile)
				{
					TeiravonMobile tm = (TeiravonMobile)mob;
					m_origbody = tm.OBody;
					tm.OBody = RndBody;
				}
				else 
				{
					m_origbody = mob.Body;
					mob.Body = RndBody;
				}
			}
			
			base.OnAdded( parent );
		}

		public override void OnRemoved( object parent )
		{
			Mobile mob = parent as Mobile;

			if (mob != null)
			{
				if (mob is TeiravonMobile)
				{
					TeiravonMobile tm = (TeiravonMobile)mob;
					tm.OBody = m_origbody;
				}
				else
				{
					mob.Body = m_origbody;
				}
				MaskCharges -= 1;
				if (MaskCharges < 1)
					this.Delete();
			}

			base.OnRemoved( parent );
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060741, "{0}", m_charges ); // charges left
		}
		
		public HalloweenMask( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( m_charges );
			writer.Write( m_origbody );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_charges = reader.ReadInt();
			m_origbody = reader.ReadInt();
		}
	}
}
