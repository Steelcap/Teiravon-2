using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class HoodedRobe : BaseOuterTorso
	{
	
		public override void OnDoubleClick( Mobile from )
		{
			if ( ItemID == 0x25EC )
				{
					ItemID = 0x2683;
				}
			else
				{
					ItemID = 0x25EC;
				}
			
			//foreach ( Mobile m in GetMobilesInRange( 10 ) )
			//	from.Send( new Network.MobileIncoming( (Mobile)this.Parent, m ) );

			if (from.Backpack != null)
			{
				Container m_Backpack = from.Backpack;
				m_Backpack.DropItem( this );

				from.EquipItem( (Item)this );
			}
		}

		[Constructable]
		public HoodedRobe() : this( 0 )
		{
		}

		[Constructable]
		public HoodedRobe( int hue ) : base( 0x25EC, hue )
		{
			Name = "Hooded Robe";
		}

		public HoodedRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) ItemID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			ItemID = reader.ReadInt();
		}
	}
}
