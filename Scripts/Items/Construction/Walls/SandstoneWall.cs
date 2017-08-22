using System;

namespace Server.Items
{
	public enum SandstoneWallTypes
	{
		Corner1,
		SouthWall1,
		EastWall1,
		CornerPost1,
		SouthWindow1,
		EastWindow1,
		Corner2,
		EastWall2,
		SouthWall2,
		CornerPost2,
		EastWindow2,
		SouthWindow2,
		CornerMedium1,
		SouthWallMedium1,
		EastWallMedium1,
		CornerPostMedium1,
		SECornerMedium,
		EastWallMedium2,
		SouthWallMedium2,
		CornerPostMedium2,
		CornerArch,
		EastArch,
		SouthArch,
		NorthArch1,
		WestArch,
		EastArchTop,
		SouthArchTop,
		NorthSlant,
		NECornerMedium,
		ThickWall1,
		ThickEastWall1,
		ThickSouthWall2,
		ThickWall2,
		EastSlant,
		SouthSlant,
		SESlant,
		SWSlant,
		NESlant,
		SouthSlantTop1,
		EastSlantTop1,
		NorthSlantTop,
		WestSlantTop,
		SWCornerMedium,
		NWCornerMedium,
		SouthWallMedium3,
		EastWallMedium3,
		SouthSlantTop2,
		EastSlantTop2,
		CornerSlantTop,
		CornerPostSlantTop,
		NorthArch2,
		SouthArch2,
		WestArch2,
		EastArch2,
		WestArch3,
		NorthArch3,
		SouthArch3,
		NorthArch4,
		EastArch3,
		WestArch4,
		CornerColumn,
		EastColumn,
		SouthColumn,
		SouthWallVShort1,
		EastWallVShort1,
		SouthWallShort1,
		EastWallShort1,
		Null1, //These are Wood Posts, located in WoodPost.cs
		Null2,
		Null3,
		Null4,
		Null5,
		Null6,
		Null7,
		Null8,
		Null9,
		WestSlant,
		Null11, //These are Ratten Walls, located in RattenWall.cs
		Null12,
		Null13,
		Null14,
		Null15,
		Null16,
		Null17,
		Null18,
		Null19,
		Null110,
		Null111,
		Null112,
		SouthWallShort2,
		EastWallShort2,
		SouthWallVShort2,
		EastWallVShort2
	}

	public class SandstoneWall : BaseWall
	{
		[Constructable]
		public SandstoneWall( SandstoneWallTypes type ) : base( 0x0158 + (int)type )
		{
		}

		public SandstoneWall( Serial serial ) : base( serial )
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