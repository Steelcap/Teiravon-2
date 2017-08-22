using System;

namespace Server.Items
{
	public enum PalisadeWallTypes
	{
		SouthSlant1,
		EastSlant1,
		SouthSlant2,
		EastSlant2,
		EastWallPiked,
		SouthWallPiked,
		CornerPiked,
		Corner,
		EastWall,
		SouthWall,
		CornerPost,
		SouthWallShort,
		EastWallShort,
		WestDoorFrame,
		EastDoorFrame,
		SouthDoorFrame,
		NorthDoorFrame
	}

	public class PalisadeWall : BaseWall
	{
		[Constructable]
		public PalisadeWall( PalisadeWallTypes type ) : base( 0x021D + (int)type )
		{
		}

		public PalisadeWall( Serial serial ) : base( serial )
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