using System;

namespace Server.Items
{
	public enum MarbleWallTypes
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
		SouthWindow3,
		EastWindow3,
		CornerMedium,
		SouthWallMedium,
		EastWallMedium,
		CornerPostMedium,
		CornerShort1,
		SouthWallShort1,
		EastWallShort1,
		CornerPostShort1,
		CornerArch1,
		EastArch1,
		WestArch1,
		SouthArch1,
		NorthArch1,
		CornerShort2,
		SouthWallShort2,
		EastWallShort2,
		CornerPostShort2,
		CornerColumn1,
		EastColumn1,
		SouthColumn1,
		CornerColumn2,
		EastColumn2,
		SouthColumn2,
		SouthArch2,
		NorthArch2,
		EastArch2,
		WestArch2,
		SouthWallShort3,
		EastWallShort3
	}

	public class MarbleWall : BaseWall
	{
		[Constructable]
		public MarbleWall( MarbleWallTypes type ) : base( 0x00F8 + (int)type )
		{
		}

		public MarbleWall( Serial serial ) : base( serial )
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