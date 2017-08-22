using System;

namespace Server.Items
{
	public enum GreenTentWallTypes
	{
		Corner,
		SouthWall,
		EastWall,
		CornerPost,
		SWCorner,
		NECorner,
		WestDoorFrame,
		NorthDoorFrame,
		SouthDoorFrame,
		EastDoorFrame
	}

	public class GreenTentWall : BaseWall
	{
		[Constructable]
		public GreenTentWall( GreenTentWallTypes type ) : base( 0x0230 + (int)type )
		{
		}

		public GreenTentWall( Serial serial ) : base( serial )
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