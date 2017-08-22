using System;

namespace Server.Items
{
	public enum FancySandstoneWallTypes
	{
		Corner1,
		SouthWall1,
		EastWall1,
		Corner2,
		SouthWall2,
		EastWall2,
		Corner3,
		SouthWall3,
		EastWall3,
		Corner4,
		SouthWall4,
		EastWall4,
		CornerPost2,
		CornerPost4,
		CornerPost3,
		CornerPost1
	}

	public class FancySandstoneWall : BaseWall
	{
		[Constructable]
		public FancySandstoneWall( FancySandstoneWallTypes type ) : base( 0x024C + (int)type )
		{
		}

		public FancySandstoneWall( Serial serial ) : base( serial )
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