using System;

namespace Server.Items
{
	public enum GrayTentWallTypes
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
		EastPatch1,
		EastPatch2,
		EastPatch3,
		SouthPatch1,
		SouthPatch2,
		SouthPatch3,
		SouthDagges1,
		EastDagges1,
		SouthDagges2,
		EastDagges2,
		SouthDagges3,
		EastDagges3
	}

	public class GrayTentWall : BaseWall
	{
		[Constructable]
		public GrayTentWall( GrayTentWallTypes type ) : base( 0x0368 + (int)type )
		{
		}

		public GrayTentWall( Serial serial ) : base( serial )
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