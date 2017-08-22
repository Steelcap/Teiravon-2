using System;

namespace Server.Items
{
	public enum CaveWallTypes
	{
		EastWall1,
		SouthWall1,
		EastWall2,
		SouthWall2,
		EastWall3,
		SouthWall3,
		NWestCorner1,
		NWestCorner2,
		SEastCorner1,
		SouthWall4,
		EastWall4,
		EastWall5,
		NWestCorner3,
		SouthWall5,
		EastWall6,
		SEastCorner2,
		SouthWallShort1,
		EastWallShort1,
		EastWallShort2,
		MidNorthEdge,
		NWestCornerEdge,
		WNorthEdge,
		ENorthEdge,
		MidWestEdge,
		SWestEdge,
		NWestEdge,
		Null1, //These are Ruined Walls, located in RuinedWall.cs
		Null2,
		Null3,
		Null4,
		Null5,
		Null6,
		EastWallShort3,
		SouthWallShort2,
		EastWallShort4,
		SouthWallShort3
	}

	public class CaveWall : BaseWall
	{
		[Constructable]
		public CaveWall( CaveWallTypes type ) : base( 0x025C + (int)type )
		{
		}

		public CaveWall( Serial serial ) : base( serial )
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