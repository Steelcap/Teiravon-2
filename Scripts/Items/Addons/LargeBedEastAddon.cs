using System;
using Server;

namespace Server.Items
{
	public class LargeBedEastAddon : BaseAddon, IDyable
	{
		public override BaseAddonDeed Deed{ get{ return new LargeBedEastDeed(); } }

		[Constructable]
		public LargeBedEastAddon( int hue )
		{
			AddComponent( new AddonComponent( 0xA7D ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0xA7C ), 0, 1, 0 );
			AddComponent( new AddonComponent( 0xA79 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 0xA78 ), 1, 1, 0 );
            Hue = hue;
		}

		public LargeBedEastAddon( Serial serial ) : base( serial )
		{
		}

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;
            Hue = sender.DyedHue;
            return true;
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

	public class LargeBedEastDeed : BaseAddonDeed, IDyable
	{
		public override BaseAddon Addon{ get{ return new LargeBedEastAddon( this.Hue ); } }
		public override int LabelNumber{ get{ return 1044324; } } // large bed (east)

		[Constructable]
		public LargeBedEastDeed()
		{
		}

		public LargeBedEastDeed( Serial serial ) : base( serial )
		{
		}

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;
            Hue = sender.DyedHue;
            return true;
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