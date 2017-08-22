using System;

namespace Server.Items
{
	public abstract class BaseMiddleTorso : BaseClothing
	{
		public BaseMiddleTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseMiddleTorso( int itemID, int hue ) : base( itemID, Layer.MiddleTorso, hue )
		{
		}

		public BaseMiddleTorso( Serial serial ) : base( serial )
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

	[Flipable( 0x1541, 0x1542 )]
	public class BodySash : BaseMiddleTorso
	{
		[Constructable]
		public BodySash() : this( 0 )
		{
		}

		[Constructable]
		public BodySash( int hue ) : base( 0x1541, hue )
		{
			Weight = 1.0;
		}

		public BodySash( Serial serial ) : base( serial )
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

	[Flipable( 0x153d, 0x153e )]
	public class FullApron : BaseMiddleTorso
	{
		[Constructable]
		public FullApron() : this( 0 )
		{
		}

		[Constructable]
		public FullApron( int hue ) : base( 0x153d, hue )
		{
			Weight = 4.0;
		}

		public FullApron( Serial serial ) : base( serial )
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

	[Flipable( 0x1f7b, 0x1f7c )]
	public class Doublet : BaseMiddleTorso
	{
		[Constructable]
		public Doublet() : this( 0 )
		{
		}

		[Constructable]
		public Doublet( int hue ) : base( 0x1F7B, hue )
		{
			Weight = 2.0;
		}

		public Doublet( Serial serial ) : base( serial )
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

	[Flipable( 0x1ffd, 0x1ffe )]
	public class Surcoat : BaseMiddleTorso
	{
		[Constructable]
		public Surcoat() : this( 0 )
		{
		}

		[Constructable]
		public Surcoat( int hue ) : base( 0x1FFD, hue )
		{
			Weight = 6.0;
		}

		public Surcoat( Serial serial ) : base( serial )
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

			if ( Weight == 3.0 )
				Weight = 6.0;
		}
	}

	[Flipable( 0x1fa1, 0x1fa2 )]
	public class Tunic : BaseMiddleTorso
	{
		[Constructable]
		public Tunic() : this( 0 )
		{
		}

		[Constructable]
		public Tunic( int hue ) : base( 0x1FA1, hue )
		{
			Weight = 5.0;
		}

		public Tunic( Serial serial ) : base( serial )
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

	[Flipable( 0x2310, 0x230F )]
	public class FormalShirt : BaseMiddleTorso
	{
		[Constructable]
		public FormalShirt() : this( 0 )
		{
		}

		[Constructable]
		public FormalShirt( int hue ) : base( 0x2310, hue )
		{
			Weight = 1.0;
		}

		public FormalShirt( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			if ( Weight == 2.0 )
				Weight = 1.0;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[Flipable( 0x1f9f, 0x1fa0 )]
	public class JesterSuit : BaseMiddleTorso
	{
		[Constructable]
		public JesterSuit() : this( 0 )
		{
		}

		[Constructable]
		public JesterSuit( int hue ) : base( 0x1F9F, hue )
		{
			Weight = 4.0;
		}

		public JesterSuit( Serial serial ) : base( serial )
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

    // Teiravon custom

    public class FittedSurcoat : BaseMiddleTorso
    {
        [Constructable]
        public FittedSurcoat() : this(0) { }

        [Constructable]
        public FittedSurcoat(int hue)
            : base(0x3d97, hue)
        {
            Weight = 1.0;
            Name = "Fitted Surcoat";
        }

        public FittedSurcoat(Serial serial) : base(serial) { }

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

    public class Tabbard : BaseMiddleTorso
    {
        [Constructable]
        public Tabbard() : this(0) { }

        [Constructable]
        public Tabbard(int hue)
            : base(0x3d96, hue)
        {
            Weight = 1.0;
            Name = "Tabbard";
        }

        public Tabbard(Serial serial) : base(serial) { }

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

    public class Vest : BaseMiddleTorso
    {
        [Constructable]
        public Vest() : this(0) { }

        [Constructable]
        public Vest(int hue)
            : base(0x3caf, hue)
        {
            Weight = 1.0;
            Name = "Vest";
        }

        public Vest(Serial serial) : base(serial) { }

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

    public class PirateShirt : BaseMiddleTorso
    {
        [Constructable]
        public PirateShirt() : this(0) { }

        [Constructable]
        public PirateShirt(int hue)
            : base(0x3cac, hue)
        {
            Weight = 1.0;
            Name = "Pirate Shirt";
        }

        public PirateShirt(Serial serial) : base(serial) { }

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

    public class BeltedVest : BaseMiddleTorso
    {
        [Constructable]
        public BeltedVest() : this(0) { }

        [Constructable]
        public BeltedVest(int hue)
            : base(0x3cab, hue)
        {
            Weight = 1.0;
            Name = "Belted Vest";
        }

        public BeltedVest(Serial serial) : base(serial) { }

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

    public class BuckledVest : BaseMiddleTorso
    {
        [Constructable]
        public BuckledVest() : this(0) { }

        [Constructable]
        public BuckledVest(int hue)
            : base(0x3ca6, hue)
        {
            Weight = 1.0;
            Name = "Buckled Vest";
        }

        public BuckledVest(Serial serial) : base(serial) { }

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

    public class NobleTabbard : BaseMiddleTorso
    {
        [Constructable]
        public NobleTabbard() : this(0) { }

        [Constructable]
        public NobleTabbard(int hue)
            : base(0x3c89, hue)
        {
            Weight = 1.0;
            Name = "Noble Tabbard";
        }

        public NobleTabbard(Serial serial) : base(serial) { }

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

    public class AscotJacket : BaseMiddleTorso
    {
        [Constructable]
        public AscotJacket() : this(0) { }

        [Constructable]
        public AscotJacket(int hue)
            : base(0x3c87, hue)
        {
            Weight = 1.0;
            Name = "Ascot Jacket";
        }

        public AscotJacket(Serial serial) : base(serial) { }

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

    public class TunicShirt : BaseMiddleTorso
    {
        [Constructable]
        public TunicShirt() : this(0) { }

        [Constructable]
        public TunicShirt(int hue)
            : base(0x3c86, hue)
        {
            Weight = 1.0;
            Name = "Tunic Shirt";
        }

        public TunicShirt(Serial serial) : base(serial) { }

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

    public class HalfDress : BaseMiddleTorso
    {
        [Constructable]
        public HalfDress() : this(0) { }

        [Constructable]
        public HalfDress(int hue)
            : base(0x0309, hue)
        {
            Weight = 1.0;
            Name = "Half dress";
        }

        public HalfDress(Serial serial) : base(serial) { }

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

    public class VestJacket : BaseMiddleTorso
    {
        [Constructable]
        public VestJacket() : this(0) { }

        [Constructable]
        public VestJacket(int hue)
            : base(0x3c88, hue)
        {
            Weight = 1.0;
            Name = "Vest Jacket";
        }

        public VestJacket(Serial serial) : base(serial) { }

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
	
	    public class NobleJacket : BaseMiddleTorso
    {
        [Constructable]
        public NobleJacket() : this(0) { }

        [Constructable]
        public NobleJacket(int hue)
            : base(0x3c84, hue)
        {
            Weight = 1.0;
            Name = "Noble Jacket";
        }

        public NobleJacket(Serial serial) : base(serial) { }

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
	
	    public class LayeredShirt : BaseMiddleTorso
    {
        [Constructable]
        public LayeredShirt() : this(0) { }

        [Constructable]
        public LayeredShirt(int hue) : base(0x3ca9, hue)
        {
            Weight = 1.0;
            Name = "Layered Shirt";
        }

        public LayeredShirt(Serial serial) : base(serial) { }

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
	
	    public class SleevelessTop : BaseMiddleTorso
    {
        [Constructable]
        public SleevelessTop() : this(0) { }

        [Constructable]
        public SleevelessTop(int hue) : base(0x3c92, hue)
        {
            Weight = 1.0;
            Name = "Sleeveless Top";
        }

        public SleevelessTop(Serial serial) : base(serial) { }

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
	
	    public class LayeredTop : BaseMiddleTorso
    {
        [Constructable]
        public LayeredTop() : this(0) { }

        [Constructable]
        public LayeredTop(int hue)
            : base(0x3c91, hue)
        {
            Weight = 1.0;
            Name = "Layered Top";
        }

        public LayeredTop(Serial serial) : base(serial) { }

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

    public class BandedTop : BaseMiddleTorso
    {
        [Constructable]
        public BandedTop() : this(0) { }

        [Constructable]
        public BandedTop(int hue)
            : base(0x3c8e, hue)
        {
            Weight = 1.0;
            Name = "Banded Top";
        }

        public BandedTop(Serial serial) : base(serial) { }

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

    public class DarkCorset : BaseMiddleTorso
    {
        [Constructable]
        public DarkCorset() : this(0) { }

        [Constructable]
        public DarkCorset(int hue)
            : base(0x3c8d, hue)
        {
            Weight = 1.0;
            Name = "Dark Corset";
        }

        public DarkCorset(Serial serial) : base(serial) { }

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

    public class GirdleTop : BaseMiddleTorso
    {
        [Constructable]
        public GirdleTop() : this(0) { }

        [Constructable]
        public GirdleTop(int hue)
            : base(0x3c8c, hue)
        {
            Weight = 1.0;
            Name = "Girdle Top";
        }

        public GirdleTop(Serial serial) : base(serial) { }

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

    public class CorsetTop : BaseMiddleTorso
    {
        [Constructable]
        public CorsetTop() : this(0) { }

        [Constructable]
        public CorsetTop(int hue)
            : base(0x3c8b, hue)
        {
            Weight = 1.0;
            Name = "Corset Top";
        }

        public CorsetTop(Serial serial) : base(serial) { }

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
	
	    public class CheckeredShirt : BaseMiddleTorso
    {
        [Constructable]
        public CheckeredShirt() : this(0) { }

        [Constructable]
        public CheckeredShirt(int hue)
            : base(0x25eb, hue)
        {
            Weight = 1.0;
            Name = "Checkered Shirt";
        }

        public CheckeredShirt(Serial serial) : base(serial) { }

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
	
	    public class ButtonedShirt : BaseMiddleTorso
    {
        [Constructable]
        public ButtonedShirt() : this(0) { }

        [Constructable]
        public ButtonedShirt(int hue) : base(0x3caa, hue)
        {
            Weight = 1.0;
            Name = "Buttoned Shirt";
        }

        public ButtonedShirt(Serial serial) : base(serial) { }

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
	
	    public class PatternedShirt : BaseMiddleTorso
    {
        [Constructable]
        public PatternedShirt() : this(0) { }

        [Constructable]
        public PatternedShirt(int hue)
            : base(0x3177, hue)
        {
            Weight = 1.0;
            Name = "Patterned Shirt";
        }

        public PatternedShirt(Serial serial) : base(serial) { }

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