using System;

namespace Server.Items
{
	public enum ThickLogWallTypes
	{
		Corner,
		EastWall,
		SouthWall,
		CornerPost,
		NorthDoorFrame,
		WestDoorFrame,
		SouthDoorFrame,
		EastDoorFrame,
		SouthWindow,
		EastWindow,
		CornerMedium,
		EastWallMedium,
		SouthWallMedium,
		CornerPostMedium,
		WestDoorFrameMedium,
		NorthDoorFrameMedium,
		SouthLogDoorFrameMedium,
		EastLogDoorFrameMedium,
		SouthWallShort,
		EastWallShort,
		SouthDoorFrameShort,
		EastDoorFrameShort
	}

	public class ThickLogWall : BaseWall
	{
		[Constructable]
		public ThickLogWall( ThickLogWallTypes type ) : base( 0x0090 + (int)type )
		{
		}

		public ThickLogWall( Serial serial ) : base( serial )
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