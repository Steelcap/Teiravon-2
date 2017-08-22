using System;
using Server;

namespace Server.Items
{
	public class SmallBedEastAddon : BaseAddon, IDyable
	{
		public override BaseAddonDeed Deed{ get{ return new SmallBedEastDeed(); } }

		[Constructable]
		public SmallBedEastAddon( int hue )
		{
			AddComponent( new AddonComponent( 0xA5D ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0xA62 ), 1, 0, 0 );
            Hue = hue;
		}

		public SmallBedEastAddon( Serial serial ) : base( serial )
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

	public class SmallBedEastDeed : BaseAddonDeed, IDyable
	{
		public override BaseAddon Addon{ get{ return new SmallBedEastAddon( this.Hue ); } }
		public override int LabelNumber{ get{ return 1044322; } } // small bed (east)

		[Constructable]
		public SmallBedEastDeed()
		{
		}

		public SmallBedEastDeed( Serial serial ) : base( serial )
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