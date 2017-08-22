using System;

namespace Server.Items
{
	public enum RuinedWallTypes
	{
		SouthWallMedium,
		EastDoorFrame,
		SouthDoorFrame,
		NorthDoorFrame,
		WestDoorFrame,
		EastWallMedium,
		Null1, // These are Cave Walls, located in CaveWall.cs
		Null2,
		Null3,
		Null4,
		SouthWall,
		EastWall
	}

	public class RuinedWall : BaseWall
	{
		[Constructable]
		public RuinedWall( RuinedWallTypes type ) : base( 0x0277 + (int)type )
		{
		}

		public RuinedWall( Serial serial ) : base( serial )
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