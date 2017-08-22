using System;

namespace Server.Items
{
	public enum BattlementWallTypes
	{
		WestSlant,
		NorthSlant,
		SECorner,
		SouthSlant,
		SWCorner,
		EastSlant,
		NECorner
	}

	public class BattlementWall : BaseWall
	{
		[Constructable]
		public BattlementWall( BattlementWallTypes type ) : base( 0x02C7 + (int)type )
		{
		}

		public BattlementWall( Serial serial ) : base( serial )
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
}