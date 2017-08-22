using System;

namespace Server.Items
{
	public enum WhiteMarbleWallTypes
	{
		Corner1,
		SouthWall1,
		EastWall1,
		CornerPost1,
		SouthWindow1,
		EastWindow1,
		Corner2,
		SouthWall2,
		EastWall2,
		CornerPost2,
		SouthWindow2,
		EastWindow2,
		Corner3,
		SouthWall3,
		EastWall3,
		CornerPost3,
		CornerMedium,
		SouthWallMedium,
		EastWallMedium,
		CornerPostMedium,
		SouthWallShort1,
		EastWallShort1,
		CornerColumn1,
		EastColumn1,
		SouthColumn1,
		CornerColumn2,
		EastColumn2,
		SouthColumn2,
		EastWindow3,
		SouthWindow3,
		CornerArch,
		EastArch1,
		WestArch1,
		SouthArch1,
		NorthArch1,
		CornerShort1,
		SouthWallShort2,
		EastWallShort2,
		CornerPostShort1,
		CornerShort2,
		SouthWallShort3,
		EastWallShort3,
		CornerPostShort2,
		SouthArch2,
		NorthArch2,
		EastArch2,
		WestArch2
	}

	public class WhiteMarbleWall : BaseWall
	{
		[Constructable]
		public WhiteMarbleWall( WhiteMarbleWallTypes type ) : base( 0x0291 + (int)type )
		{
		}

		public WhiteMarbleWall( Serial serial ) : base( serial )
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