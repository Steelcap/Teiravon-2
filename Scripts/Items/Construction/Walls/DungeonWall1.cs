using System;

namespace Server.Items
{
	public enum DungeonWall1Types
	{
		SouthWall,
		EastWall,
		Corner,
		CornerPost,
		WestArch,
		NorthArch,
		SouthArchTop,
		EastArchTop,
		EastArch,
		SouthArch
	}

	public class DungeonWall1 : BaseWall
	{
		[Constructable]
		public DungeonWall1( DungeonWall1Types type ) : base( 0x0241 + (int)type )
		{
		}

		public DungeonWall1( Serial serial ) : base( serial )
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