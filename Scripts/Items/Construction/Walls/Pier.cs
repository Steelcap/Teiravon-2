using System;

namespace Server.Items
{
	public enum PierTypes
	{
		Pier,
		SouthRope1,
		EastRope1,
		SouthRope2,
		EastRope2,
		SERopePier,
		SWRopePier,
		NERopePier,
		NWRopePier,
		WaterPier1,
		WaterPier2,
		WaterPier3
	}

	public class Pier : BaseWall
	{
		[Constructable]
		public Pier( PierTypes type ) : base( 0x03A5 + (int)type )
		{
		}

		public Pier( Serial serial ) : base( serial )
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