using System;

namespace Server.Items
{
	public enum DarkGrayStoneWallTypes
	{
		ThickWall,
		CornerArch1,
		Corner,
		SouthWall1,
		EastWall1,
		SouthWindow,
		EastWindow,
		CornerPost,
		CornerArch2,
		SouthArch1,
		EastArch1,
		NorthArch1,
		WestArch1,
		EastSlant,
		SouthSlant,
		CornerArch3,
		EastTopArch2,
		SouthTopArch2,
		SouthArch2,
		EastArch2,
		NorthArch2,
		WestArch2,
		Pillar,
		CornerShort,
		EastWallShort,
		SouthWallShort,
		CornerPostShort,
		EastTopArch,
		WestTopArch,
		NorthTopArch,
		SouthTopArch,
		SouthArchPost,
		EastArchPost,
		SouthWallVShort,
		EastWallVShort,
		SouthWall2,
		EastWall2,
		SouthWall3,
		EastWall3,
		SouthWall4,
		EastWall4,
		SouthWall5,
		EastWall5,
		EastWall6,
		SouthWall6,
		EastWall7,
		SouthWall7,
		EastWall8,
		SouthWall8,
		EastWall9,
		SouthWall9
	}

	public class DarkGrayStoneWall : BaseWall
	{
		[Constructable]
		public DarkGrayStoneWall( DarkGrayStoneWallTypes type) : base( 0x00C5 + (int)type )
		{
		}

		public DarkGrayStoneWall( Serial serial ) : base( serial )
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