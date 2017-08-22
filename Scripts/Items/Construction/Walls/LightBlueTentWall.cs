using System;

namespace Server.Items
{
	public enum LightBlueTentWallTypes
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
		EastDoorFrame,
		CornerFrayed,
		SouthWallFrayed,
		EastWallFrayed,
		SWCornerFrayed,
		NECornerFrayed,
		EastPatch1,
		EastPatch2,
		EastPatch3,
		SouthPatch1,
		SouthPatch2,
		SouthPatch3
	}

	public class LightBlueTentWall : BaseWall
	{
		[Constructable]
		public LightBlueTentWall( LightBlueTentWallTypes type ) : base( 0x02DE + (int)type )
		{
		}

		public LightBlueTentWall( Serial serial ) : base( serial )
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