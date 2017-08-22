using System;

namespace Server.Items
{
	public enum LightGrayStoneWallTypes
	{
		NWCornerMedium,
		SECornerMedium,
		SouthWallMedium1,
		SWCornerMedium,
		SouthWallMedium2,
		EastWallMedium1,
		NECornerMedium,
		EastWallMedium2,
		SouthWallShort1,
		SouthWallShort2,
		EastWallShort1,
		EastWallShort2
	}

	public class LightGrayStoneWall : BaseWall
	{
		[Constructable]
		public LightGrayStoneWall( LightGrayStoneWallTypes type ) : base( 0x02CE + (int)type )
		{
		}

		public LightGrayStoneWall( Serial serial ) : base( serial )
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