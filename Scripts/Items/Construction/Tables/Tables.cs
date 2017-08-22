using System;

namespace Server.Items
{

	[Furniture]
	public class ElegantLowTable : Item
	{
		[Constructable]
		public ElegantLowTable() : base(0x2819)
		{
			Weight = 1.0;
			Name = "Small Elegant table";
		}

		public ElegantLowTable(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	[Furniture]
	public class PlainLowTable : Item
	{
		[Constructable]
		public PlainLowTable() : base(0x281A)
		{
			Weight = 1.0;
			Name = "Plain Table";
		}

		public PlainLowTable(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

	[Furniture]
	[Flipable(0xB90,0xB7D)]
	public class LargeTable : Item
	{
		[Constructable]
		public LargeTable() : base(0xB90)
		{
			Weight = 1.0;
			Name = "Dining Table";
		}

		public LargeTable(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}

    [Furniture]
    [Flipable(0xB3F)]
    public class Counter : Item
    {
        [Constructable]
        public Counter()
            : base(0xB3F)
        {
            Weight = 1.0;
            Name = "counter";
        }

        public Counter(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 4.0)
                Weight = 1.0;
        }
    }

	[Furniture]
	[Flipable(0xB35,0xB34)]
	public class Nightstand : Item
	{
		[Constructable]
		public Nightstand() : base(0xB35)
		{
			Weight = 1.0;
			Name = "Nightstand";
		}

		public Nightstand(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}

	[Furniture]
	[Flipable(0xB8F,0xB7C)]
	public class YewWoodTable : Item
	{
		[Constructable]
		public YewWoodTable() : base(0xB8F)
		{
			Weight = 1.0;
			Name = "Elegant Table";
		}

		public YewWoodTable(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
}