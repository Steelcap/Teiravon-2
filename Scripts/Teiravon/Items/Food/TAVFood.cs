using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Stew : Food
	{
		[Constructable]
		public Stew() : this( 1 )
		{
		}

		[Constructable]
		public Stew( int amount ) : base( amount, 0x1604 )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
			this.Name = "Stew";
			this.Stackable = false;
		}

		public Stew( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Stew(), amount );
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

	public class GrapeJelly : Food
	{
		[Constructable]
		public GrapeJelly() : this( 1 )
		{
		}

		[Constructable]
		public GrapeJelly( int amount ) : base( amount, 0x9EC )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Hue = 113;
			this.Name = "Grape Jelly";
			this.Stackable = true;
		}

		public GrapeJelly( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new GrapeJelly(), amount );
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

	public class PeachJam : Food
	{
		[Constructable]
		public PeachJam() : this( 1 )
		{
		}

		[Constructable]
		public PeachJam( int amount ) : base( amount, 0x9EC )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Hue = 141;
			this.Name = "Peach Jam";
			this.Stackable = true;
		}

		public PeachJam( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new PeachJam(), amount );
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
	
	public class Marshmallow : Food
	{
		[Constructable]
		public Marshmallow() : this( 1 )
		{
		}

		[Constructable]
		public Marshmallow( int amount ) : base( amount, 0x9B4 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
			//this.Hue = 2103;
			this.Name = "Marshmallow";
			this.Stackable = false;
		}

		public Marshmallow( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Marshmallow(), amount );
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

	public class Tarte : Food
	{
		[Constructable]
		public Tarte() : this( 1 )
		{
		}

		[Constructable]
		public Tarte( int amount ) : base( amount, 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
			this.Name = "Tarte";
		}

		public Tarte( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Tarte(), amount );
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
	
	public class SweetMutton : Food
	{
		[Constructable]
		public SweetMutton() : this( 1 )
		{
		}

		[Constructable]
		public SweetMutton( int amount ) : base( amount, 0x9C9 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
			this.Name = "Sweet Mutton";
			this.Stackable = true;
		}

		public SweetMutton( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new SweetMutton(), amount );
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

    public class CreamLiqueur : BeverageBottle
    {
        [Constructable]
        public CreamLiqueur()
            : base(BeverageType.Liquor)
        {
            Weight = 1;
            Name = "Cream Liqueur";
            Hue = 2375;
        }

        public CreamLiqueur(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
