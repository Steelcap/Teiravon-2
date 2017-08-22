using System; 
using Server; 
using Server.Items;

namespace Server.Items
{

    public class SargBag : Bag
    {
        public int goldAmount;

        [Constructable]
        public SargBag()
        {
            goldAmount = Utility.RandomMinMax(120, 480);
            DropItem(new Gold(goldAmount));
            DropItem(new Shirt());
            DropItem(new LongPants());
            DropItem(new Boots());
            DropItem(new Lantern());
            DropItem(new BrownBook());

            DropItem(new Scimitar());
            DropItem(new LeatherArms());
            DropItem(new LeatherCap());
            DropItem(new LeatherChest());
            DropItem(new LeatherGloves());
            DropItem(new LeatherGorget());
            DropItem(new LeatherLegs());
            DropItem(new WoodenShield());
        }

        public SargBag(Serial serial)
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

	public class FighterBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public FighterBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Longsword() );
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
			DropItem( new WoodenShield() );
		}

		public FighterBag( Serial serial ) : base( serial ) 
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

	public class KensaiBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public KensaiBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Katana() );
		}

		public KensaiBag( Serial serial ) : base( serial ) 
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

    public class MonkBag : Bag
    {
        public int goldAmount;

        [Constructable]
        public MonkBag()
        {
            goldAmount = Utility.RandomMinMax(120, 480);
            DropItem(new Gold(goldAmount));
            DropItem(new Robe(1125));
            DropItem(new LongPants());
            DropItem(new Sandals());
            DropItem(new Bandana());
            DropItem(new Lantern());
            DropItem(new BrownBook());

        }

        public MonkBag(Serial serial)
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

	public class BerserkerBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public BerserkerBag()
		{
			goldAmount = Utility.RandomMinMax( 80, 320 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new ShortPants() );
			DropItem( new Sandals() );
			DropItem( new Torch() );
			DropItem( new BrownBook() );

			DropItem( new Longsword() );
		}

		public BerserkerBag( Serial serial ) : base( serial ) 
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

	public class DragoonBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public DragoonBag()
		{
			goldAmount = Utility.RandomMinMax( 80, 320 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new ShortSpear() );
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
		}

		public DragoonBag( Serial serial ) : base( serial ) 
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

	public class ThiefBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public ThiefBag()
		{
			goldAmount = Utility.RandomMinMax( 100, 400 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new ThrowingKnife() );
            DropItem( new ThrowingKnife());
            DropItem( new Kryss());
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
		}

		public ThiefBag( Serial serial ) : base( serial ) 
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

	public class AssassinBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public AssassinBag()
		{
			goldAmount = Utility.RandomMinMax( 100, 400 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Kryss() );
			DropItem( new HandCrossbow() );
			DropItem( new Bolt( 50 ) );
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
		}

		public AssassinBag( Serial serial ) : base( serial ) 
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

    public class BardBag : Bag
    {
        public int goldAmount;

        [Constructable]
        public BardBag()
        {
            goldAmount = Utility.RandomMinMax(100, 400);
            DropItem(new Gold(goldAmount));
            DropItem(new Shirt());
            DropItem(new LongPants());
            DropItem(new Boots());
            DropItem(new Lantern());
            DropItem(new BrownBook());

            DropItem(new Kryss());
            DropItem(new Longsword());
            DropItem(new Drums());
            DropItem(new RuneLute());
            DropItem(new LeatherArms());
            DropItem(new LeatherCap());
            DropItem(new LeatherChest());
            DropItem(new LeatherGloves());
            DropItem(new LeatherGorget());
            DropItem(new LeatherLegs());
        }

        public BardBag(Serial serial)
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

	public class RangerBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public RangerBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Longsword() );
			DropItem( new Bow() );
			DropItem( new Arrow( 50 ) );
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
		}

		public RangerBag( Serial serial ) : base( serial ) 
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

	public class ArcherBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public ArcherBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Bow() );
			DropItem( new Arrow( 100 ) );
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
		}

		public ArcherBag( Serial serial ) : base( serial ) 
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

	public class ClericBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public ClericBag()
		{
			goldAmount = Utility.RandomMinMax( 100, 400 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Mace() );
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
			DropItem( new Bandage( 10 ) );
			DropItem( new Spellbook() );
			DropItem( new HarmScroll() );
			DropItem( new HealScroll() );
			DropItem( new NightSightScroll() );
		}

		public ClericBag( Serial serial ) : base( serial ) 
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

	public class DarkClericBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public DarkClericBag()
		{
			goldAmount = Utility.RandomMinMax( 100, 400 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Mace() );
			DropItem( new LeatherArms() );
			DropItem( new LeatherCap() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherLegs() );
			DropItem( new Bandage( 10 ) );
			DropItem( new Spellbook() );
			DropItem( new HarmScroll() );
			DropItem( new HealScroll() );
			DropItem( new NightSightScroll() );
		}

		public DarkClericBag( Serial serial ) : base( serial ) 
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

	public class ForesterBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public ForesterBag()
		{
			goldAmount = Utility.RandomMinMax( 40, 160 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new Robe() );
			DropItem( new ShortPants() );
			DropItem( new Sandals() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Club() );
			DropItem( new QuarterStaff() );
			DropItem( new Spellbook() );
			DropItem( new NightSightScroll() );
			DropItem( new MagicArrowScroll() );
			DropItem( new HarmScroll() );
		}

		public ForesterBag( Serial serial ) : base( serial ) 
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

	public class ShapeshifterBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public ShapeshifterBag()
		{
			goldAmount = Utility.RandomMinMax( 40, 160 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new ShortPants() );
			DropItem( new Sandals() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Club() );
		}

		public ShapeshifterBag( Serial serial ) : base( serial ) 
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

	public class AlchemistBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public AlchemistBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new FullApron() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new MortarPestle() );
			DropItem( new Bottle( 20 ) );
			DropItem( new BagOfReagents( 25 ) );
			DropItem( new AlchemyTome() );
			DropItem( new AgilityFormula() );
			DropItem( new NightSightFormula() );
			DropItem( new LesserHealFormula() );
			DropItem( new StrengthFormula() );
			DropItem( new LesserPoisonFormula() );
			DropItem( new LesserCureFormula() );
			DropItem( new LesserExplosionFormula() );
			DropItem( new RefreshFormula() );
		}

		public AlchemistBag( Serial serial ) : base( serial ) 
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

	public class BlacksmithBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public BlacksmithBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new FullApron() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new SmithHammer() );
            DropItem(new TinkerTools()) ;
			DropItem( new Pickaxe() );
			DropItem( new IronIngot( 50 ) );
		}

		public BlacksmithBag( Serial serial ) : base( serial ) 
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

	public class BowyerBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public BowyerBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new HalfApron() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new FletcherTools() );
			DropItem( new Hatchet() );
			DropItem( new Log( 75 ) );
		}

		public BowyerBag( Serial serial ) : base( serial ) 
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

	public class WoodworkerBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public WoodworkerBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new HalfApron() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Saw() );
            DropItem(new FletcherTools());
			DropItem( new Hatchet() );
            DropItem(new Feather(50));
			DropItem( new Log( 75 ) );
		}

		public WoodworkerBag( Serial serial ) : base( serial ) 
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

    public class StoneworkerBag : Bag
    {
        public int goldAmount;

        [Constructable]
        public StoneworkerBag()
        {
            goldAmount = Utility.RandomMinMax(120, 480);
            DropItem(new Gold(goldAmount));
            DropItem(new Shirt());
            DropItem(new LongPants());
            DropItem(new HalfApron());
            DropItem(new Boots());
            DropItem(new Lantern());
            DropItem(new BrownBook());

            DropItem(new MalletAndChisel());
            DropItem(new Scorp());
            DropItem(new FletcherTools());
            DropItem(new Hatchet());
            DropItem(new Feather(50));
            DropItem(new Log(75));
            DropItem(new Granite());
        }

        public StoneworkerBag(Serial serial)
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
	public class SewingBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public SewingBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new HalfApron() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new SewingKit() );
			DropItem( new Scissors() );
			DropItem( new BoltOfCloth() );
		}

		public SewingBag( Serial serial ) : base( serial ) 
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

	public class TinkerBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public TinkerBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new FullApron() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new TinkerTools() );
			DropItem( new Pickaxe() );
			DropItem( new IronIngot( 75 ) );
		}

		public TinkerBag( Serial serial ) : base( serial ) 
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

	public class CookBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public CookBag()
		{
			goldAmount = Utility.RandomMinMax( 120, 480 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new FullApron() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new Skillet() );
			DropItem( new RawBird( 75 ) );
		}

		public CookBag( Serial serial ) : base( serial ) 
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

    public class MerchantBag : Bag
    {
        public int goldAmount;

        [Constructable]
        public MerchantBag()
        {
            goldAmount = Utility.RandomMinMax(120, 480);
            DropItem(new Gold(goldAmount));
            DropItem(new Shirt());
            DropItem(new LongPants());
            DropItem(new FullApron());
            DropItem(new Boots());
            DropItem(new Lantern());
            DropItem(new BrownBook());

            DropItem(new Skillet());
            DropItem(new RawBird(75));
            DropItem(new Pickaxe());
            DropItem(new Saw());
            DropItem(new SmithHammer());
            DropItem(new TinkerTools());
            DropItem(new FletcherTools());
            DropItem(new Hatchet());
            DropItem(new SewingKit());
            
            DropItem(new IronIngot(30));
            DropItem(new Cloth(20));
        }

        public MerchantBag(Serial serial)
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

	public class MageBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public MageBag()
		{
			goldAmount = Utility.RandomMinMax( 60, 240 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new WizardsHat() );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Robe() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new QuarterStaff() );
			DropItem( new Spellbook() );
			DropItem( new ClumsyScroll() );
			DropItem( new FeeblemindScroll() );
			DropItem( new MagicArrowScroll() );
			DropItem( new NightSightScroll() );
			DropItem( new ReactiveArmorScroll() );
			DropItem( new WeakenScroll() );
		}

		public MageBag( Serial serial ) : base( serial ) 
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

	public class NecroBag : Bag 
	{ 
		public int goldAmount;

		[Constructable]
		public NecroBag()
		{
			goldAmount = Utility.RandomMinMax( 60, 240 );
			DropItem( new Gold( goldAmount ) );
			DropItem( new WizardsHat() );
			DropItem( new Shirt() );
			DropItem( new LongPants() );
			DropItem( new Robe() );
			DropItem( new Boots() );
			DropItem( new Lantern() );
			DropItem( new BrownBook() );

			DropItem( new QuarterStaff() );
			DropItem( new Spellbook() );
			DropItem( new ClumsyScroll() );
			DropItem( new FeeblemindScroll() );
			DropItem( new MagicArrowScroll() );
			DropItem( new NightSightScroll() );
			DropItem( new ReactiveArmorScroll() );
			DropItem( new WeakenScroll() );
			DropItem( new NecromancerSpellbook() );
			DropItem( new CurseWeaponScroll() );
		}

		public NecroBag( Serial serial ) : base( serial ) 
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
