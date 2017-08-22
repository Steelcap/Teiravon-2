using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Spells;
using Server.Scripts.Commands;
using Server.Mobiles;
using Server.Items;

namespace Server.Teiravon.War
{
	public class WarGuardPointMap : WorldMap
	{
		[Constructable]
		public WarGuardPointMap()
		{
			Name = "Guard Point Map";

			LootType = LootType.Blessed;
		}

		public WarGuardPointMap( Serial serial ) : base( serial )
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

	public class WarGuardPoint : Item
	{
		private WarStone m_WarStone = new WarStone();

		[CommandProperty( AccessLevel.GameMaster )]
		public WarStone WarStone { get { return m_WarStone; } set { m_WarStone = value; } }

		public override bool Decays { get { return false; } }

		public override void OnDoubleClick( Mobile from )
		{
			base.OnDoubleClick( from );

			if ( Parent != null )
			{
				from.SendMessage( "The guard point must be on the ground to use." );
				return;
			}

			if ( from == m_WarStone.Leader || from == m_WarStone.Commander )
			{
				WarGuardPointMap pointMap = new WarGuardPointMap();

				pointMap.Bounds = new Rectangle2D( from.X - 50, from.Y - 50, 100, 100 );
				pointMap.AddWorldPin( from.X, from.Y );
				pointMap.Protected = true;

				pointMap.Name = String.Format( "Guard Point Map ( {0}, {1}, {2} )", from.X, from.Y, from.Z );

				from.Backpack.DropItem( pointMap );

				Visible = true;
				Movable = false;
			} else {
				m_WarStone.Gold += (ulong)10;
				Delete();
			}
		}

		[Constructable]
		public WarGuardPoint() : base( 7107 )
		{
			Name = "Guard Point";

			LootType = LootType.Blessed;
		}

		public WarGuardPoint( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_WarStone );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_WarStone = (WarStone)reader.ReadItem();
		}
	}
}