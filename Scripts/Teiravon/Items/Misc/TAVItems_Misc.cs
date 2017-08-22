using System;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Items
{
	public class EttinRock : Item
	{
		[Constructable]
		public EttinRock() : base( 0x1363 )
		{
			Movable = true;

			ItemID = 4963 + Utility.RandomMinMax( 0, 3 );

			if ( ItemID == 4963 )
				Weight = 30.0;
			else if ( ItemID == 4964 || ItemID == 4965 )
				Weight = 20.0;
			else if ( ItemID == 4966 )
				Weight = 10.0;
		}

		public EttinRock( Serial serial ) : base( serial )
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