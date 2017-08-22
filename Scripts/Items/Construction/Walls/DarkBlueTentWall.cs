using System;

namespace Server.Items
{
	public enum DarkBlueTentWallTypes
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

	public class DarkBlueTentWall : BaseWall
	{
		[Constructable]
		public DarkBlueTentWall( DarkBlueTentWallTypes type ) : base( 0x01F0 + (int)type )
		{
		}

		public DarkBlueTentWall( Serial serial ) : base( serial )
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