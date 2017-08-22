using System;
using Server;

namespace Server.Items
{
	public class LargeBedSouthAddon : BaseAddon, IDyable
	{
		public override BaseAddonDeed Deed{ get{ return new LargeBedSouthDeed(); } }

		[Constructable]
		public LargeBedSouthAddon( int hue )
		{
			AddComponent( new AddonComponent( 0xA83 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0xA7F ), 0, 1, 0 );
			AddComponent( new AddonComponent( 0xA82 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 0xA7E ), 1, 1, 0 );
            Hue = hue;
		}

		public LargeBedSouthAddon( Serial serial ) : base( serial )
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

    public class LargeBedSouthDeed : BaseAddonDeed, IDyable
	{
		public override BaseAddon Addon{ get{ return new LargeBedSouthAddon( this.Hue ); } }
		public override int LabelNumber{ get{ return 1044323; } } // large bed (south)

		[Constructable]
		public LargeBedSouthDeed()
		{
		}

		public LargeBedSouthDeed( Serial serial ) : base( serial )
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