using System;

namespace Server.Items
{

	public abstract class BaseShirt : BaseClothing
	{
		public BaseShirt( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseShirt( int itemID, int hue ) : base( itemID, Layer.Shirt, hue )
		{
		}

		public BaseShirt( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1efd, 0x1efe )]
	public class FancyShirt : BaseShirt
	{
		[Constructable]
		public FancyShirt() : this( 0 )
		{
		}

		[Constructable]
		public FancyShirt( int hue ) : base( 0x1EFD, hue )
		{
			Weight = 2.0;
		}

		public FancyShirt( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1517, 0x1518 )]
	public class Shirt : BaseShirt
	{
		[Constructable]
		public Shirt() : this( 0 )
		{
		}

		[Constructable]
		public Shirt( int hue ) : base( 0x1517, hue )
		{
			Weight = 1.0;
		}

		public Shirt( Serial serial ) : base( serial )
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}

    // Teiravon custom

    public class RuffledShirt : BaseShirt
    {
        [Constructable]
        public RuffledShirt() : this(0) { }

        [Constructable]
        public RuffledShirt(int hue) : base(0x3cad, hue)
        {
            Weight = 1.0;
            Name = "Ruffled Shirt";
        }

        public RuffledShirt(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }



    public class Corset : BaseShirt
    {
        [Constructable]
        public Corset() : this(0) { }

        [Constructable]
        public Corset(int hue)
            : base(0x3c8a, hue)
        {
            Weight = 1.0;
            Name = "Corset";
        }

        public Corset(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class VestedShirt : BaseShirt
    {
        [Constructable]
        public VestedShirt() : this(0) { }

        [Constructable]
        public VestedShirt(int hue)
            : base(0x3c83, hue)
        {
            Weight = 1.0;
            Name = "Vested Shirt";
        }

        public VestedShirt(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class ElvenShirt : BaseShirt
    {
        [Constructable]
        public ElvenShirt() : this(0) { }

        [Constructable]
        public ElvenShirt(int hue)
            : base(0x3178, hue)
        {
            Weight = 1.0;
            Name = "Elven Shirt";
        }

        public ElvenShirt(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}