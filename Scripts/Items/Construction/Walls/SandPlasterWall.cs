using System;

namespace Server.Items
{
	public enum SandPlasterWallTypes
	{
		Corner,
		SouthWall1,
		EastWall1,
		CornerPost,
		SouthWall2,
		EastWall2,
		SouthWall3,
		EastWall3,
		SouthWindow1,
		EastWindow1,
		SouthWindow2,
		EastWindow2,
		SouthWallShort,
		EastWallShort
	}

	public class SandPlasterWall : BaseWall
	{
		[Constructable]
		public SandPlasterWall( SandPlasterWallTypes type ) : base( 0x01FF + (int)type )
		{
		}

		public SandPlasterWall( Serial serial ) : base( serial )
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