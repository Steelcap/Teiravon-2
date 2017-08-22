using System;

namespace Server.Items
{
	public enum HideWallTypes
	{
		Corner,
		EastWall1,
		SouthWall1,
		CornerPost,
		EastWall2,
		SouthWall2,
		MidSouthWall1,
		MidEastWall1,
		MidSouthWall2,
		MidEastWall2,
		SouthWall3,
		EastWall3,
		MidSouthWall3,
		MidEastWall3,
		SouthWindow,
		EastWindow,
		NorthWindow,
		WestWindow,
		SouthWallShort,
		EastWallShort
	}

	public class HideWall : BaseWall
	{
		[Constructable]
		public HideWall( HideWallTypes type ) : base( 0x01B6 + (int)type )
		{
		}

		public HideWall( Serial serial ) : base( serial )
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