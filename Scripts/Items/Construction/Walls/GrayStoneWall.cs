using System;

namespace Server.Items
{
	public enum GrayStoneWallTypes
	{
		Corner,
		SouthWall,
		EastWall,
		CornerPost,
		SouthWindow,
		EastWindow,
		CornerArch1,
		WestArch1,
		EastArch1,
		NorthArch1,
		SouthArch1,
		CornerArchPost,
		CornerArch2,
		WestArch2,
		EastArch2,
		NorthArch2,
		SouthArch2,
		EastArchTop,
		SouthArchTop,
		WestArchTop,
		NorthArchTop,
		SouthArchTop2,
		SouthSlant,
		EastSlant,
		CornerShort,
		SouthWallShort,
		EastWallShort,
		CornerPostShort
	}

	public class GrayStoneWall : BaseWall
	{
		[Constructable]
		public GrayStoneWall( GrayStoneWallTypes type ) : base( 0x01CF + (int)type )
		{
		}

		public GrayStoneWall( Serial serial ) : base( serial )
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