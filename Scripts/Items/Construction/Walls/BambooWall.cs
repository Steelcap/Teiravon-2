using System;

namespace Server.Items
{
	public enum BambooWallTypes
	{
		SEastCorner,
		SE_NWWall,
		NorthCorner,
		WNorthCorner,
		ENorthCorner,
		ESouthCorner,
		SouthCorner,
		WestCorner,
		NWestCorner,
		SWestCorner,
		EastCorner,
		NE_SWWall
	}

	public class BambooWall : BaseWall
	{
		[Constructable]
		public BambooWall( BambooWallTypes type ) : base( 0x020F + (int)type )
		{
		}

		public BambooWall( Serial serial ) : base( serial )
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