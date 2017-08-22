using System;

namespace Server.Items
{
	public enum LightWoodWallTypes
	{
		Corner,
		EastWall,
		SouthWall,
		CornerPost,
		SouthDoorFrame,
		EastDoorFrame,
		NorthDoorFrame,
		WestDoorFrame,
		Corner2,
		SouthWall2,
		EastWall2,
		CornerPost2,
		EastDoor2,
		SouthDoor2,
		NorthDoor2,
		WestDoor2,
		CornerMedium,
		SouthWallMedium,
		EastWallMedium,
		EastWindow,
		SouthWindow,
		EastWindow2,
		SouthWindow2,
		CornerShort,
		EastWallShort,
		SouthWallShort,
		CornerPostShort,
		SouthWallVShort,
		EastWallVShort
	}

	public class LightWoodWall : BaseWall
	{
		[Constructable]
		public LightWoodWall( LightWoodWallTypes type ) : base( 0x00A6 + (int)type )
		{
		}

		public LightWoodWall( Serial serial ) : base( serial )
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