using System;

namespace Server.Items
{
	public enum OldPlasterWallTypes
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
		Corner5,
		SouthWall8,
		EastWall8,
		SouthWall9,
		EastWall9,
		CornerArch1,
		SouthArch,
		WestArch,
		EastArch,
		NorthArch,
		CornerVShort,
		SouthVShort,
		EastVShort,
		CornerPostVShort,
		CornerArch2
	}

	public class OldPlasterWall : BaseWall
	{
		[Constructable]
		public OldPlasterWall( OldPlasterWallTypes type ) : base( 0x037F + (int)type )
		{
		}

		public OldPlasterWall( Serial serial ) : base( serial )
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