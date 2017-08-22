using System;

namespace Server.Items
{
	public enum RattanWallTypes
	{
		Corner,
		MidSouthWall,
		ESouthWall,
		CornerPost,
		MidEastWall,
		SEastWall,
		WSouthWall,
		NEastWall,
		SouthWindow,
		NorthWindow,
		EastWindow,
		WestWindow
	}

	public class RattanWall : BaseWall
	{
		[Constructable]
		public RattanWall( RattanWallTypes type ) : base( 0x01A5 + (int)type )
		{
		}

		public RattanWall( Serial serial ) : base( serial )
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