using System;

namespace Server.Items
{
	public enum PlasterWallTypes
	{
		Corner1,
		SouthWall1,
		EastWall1,
		CornerPost,
		Corner2,
		SouthWall2,
		EastWall2,
		SouthWall3,
		EastWall3,
		SouthWall4,
		EastWall4,
		Corner3,
		SouthWall5,
		EastWall5,
		Corner4,
		SouthWall6,
		EastWall6,
		SouthWall7,
		EastWall7,
		SouthWindow1,
		EastWindow1,
		CornerArch1,
		SouthArch1,
		WestArch1,
		EastArch1,
		NorthArch1,
		CornerVShort,
		SouthWallVShort,
		EastWallVShort,
		CornerPostVShort,
		SouthLogPost,
		EastLogPost,
		SouthLogPostShort,
		EastLogPostShort,
		CornerArch2,
		SouthWallShort,
		EastWallShort,
		Corner5,
		SouthWall8,
		EastWall8,
		Corner6,
		SouthWall9,
		EastWall9,
		SouthWall10,
		EastWall10,
		SouthWindow2,
		EastWindow2,
		SouthWindow3,
		EastWindow3
	}

	public class PlasterWall : BaseWall
	{
		[Constructable]
		public PlasterWall( PlasterWallTypes type ) : base( 0x0127 + (int)type )
		{
		}

		public PlasterWall( Serial serial ) : base( serial )
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