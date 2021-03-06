using System;
using Server;

namespace Server.Items
{
	public class SmallBedSouthAddon : BaseAddon, IDyable
	{
		public override BaseAddonDeed Deed{ get{ return new SmallBedSouthDeed(); } }

		[Constructable]
		public SmallBedSouthAddon( int hue )
		{
			AddComponent( new AddonComponent( 0xA63 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0xA5C ), 0, 1, 0 );
            Hue = hue;
		}

		public SmallBedSouthAddon( Serial serial ) : base( serial )
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

	public class SmallBedSouthDeed : BaseAddonDeed, IDyable
	{
		public override BaseAddon Addon{ get{ return new SmallBedSouthAddon( this.Hue ); } }
		public override int LabelNumber{ get{ return 1044321; } } // small bed (south)

		[Constructable]
		public SmallBedSouthDeed()
		{
		}

		public SmallBedSouthDeed( Serial serial ) : base( serial )
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