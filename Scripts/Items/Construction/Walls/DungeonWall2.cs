using System;

namespace Server.Items
{
	public enum DungeonWall2Types
	{
		Corner,
		SouthWall1,
		EastWall1,
		CornerPost,
		CornerShort,
		SouthWallShort,
		EastWallShort,
		CornerPostShort
	}

	public class DungeonWall2 : BaseWall
	{
		[Constructable]
		public DungeonWall2( DungeonWall2Types type ) : base( 0x02F9 + (int)type )
		{
		}

		public DungeonWall2( Serial serial ) : base( serial )
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