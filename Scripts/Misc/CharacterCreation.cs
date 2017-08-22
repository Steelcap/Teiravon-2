using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Accounting;

namespace Server.Misc
{
	public class CharacterCreation
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.CharacterCreated += new CharacterCreatedEventHandler( EventSink_CharacterCreated );
		}

		private static void AddBackpack( Mobile m )
		{
			Container pack = m.Backpack;

			if ( pack == null )
			{
				pack = new Backpack();
				pack.Movable = false;

				m.AddItem( pack );
			}

			EquipItem( new ShortPants(), true );
			EquipItem( new Shirt(), true );
			PackItem( new Dagger() );
		}

		private static Item MakeNewbie( Item item )
		{
			if ( !Core.AOS )
				item.LootType = LootType.Newbied;

			return item;
		}

		private static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}

		private static Item MakePotionKeg( PotionEffect type, int hue )
		{
			PotionKeg keg = new PotionKeg();

			keg.Held = 100;
			keg.Type = type;
			keg.Hue = hue;

			return MakeNewbie( keg );
		}

		private static void FillBankAOS( Mobile m )
		{
			BankBox bank = m.BankBox;

			if ( bank == null )
				return;

			// The new AOS bankboxes don't have powerscrolls, they are automatically 'applied':

			for ( int i = 0; i < PowerScroll.Skills.Length; ++i )
				m.Skills[PowerScroll.Skills[ i ]].Cap = 120.0;

			m.StatCap = 250;


			Container cont;


			// Begin box of money
			cont = new WoodenBox();
			cont.ItemID = 0xE7D;
			cont.Hue = 0x489;

			PlaceItemIn( cont, 16, 51, new BankCheck( 500000 ) );
			PlaceItemIn( cont, 28, 51, new BankCheck( 250000 ) );
			PlaceItemIn( cont, 40, 51, new BankCheck( 100000 ) );
			PlaceItemIn( cont, 52, 51, new BankCheck( 100000 ) );
			PlaceItemIn( cont, 64, 51, new BankCheck(  50000 ) );

			PlaceItemIn( cont, 16, 115, new Factions.Silver( 9000 ) );
			PlaceItemIn( cont, 34, 115, new Gold( 60000 ) );

			PlaceItemIn( bank, 18, 169, cont );
			// End box of money


			// Begin bag of potion kegs
			cont = new Backpack();
			cont.Name = "Various Potion Kegs";

			PlaceItemIn( cont,  45, 149, MakePotionKeg( PotionEffect.CureGreater, 0x2D ) );
			PlaceItemIn( cont,  69, 149, MakePotionKeg( PotionEffect.HealGreater, 0x499 ) );
			PlaceItemIn( cont,  93, 149, MakePotionKeg( PotionEffect.PoisonDeadly, 0x46 ) );
			PlaceItemIn( cont, 117, 149, MakePotionKeg( PotionEffect.RefreshTotal, 0x21 ) );
			PlaceItemIn( cont, 141, 149, MakePotionKeg( PotionEffect.ExplosionGreater, 0x74 ) );

			PlaceItemIn( cont, 93, 82, new Bottle( 1000 ) );

			PlaceItemIn( bank, 53, 169, cont );
			// End bag of potion kegs


			// Begin bag of tools
			cont = new Bag();
			cont.Name = "Tool Bag";

			PlaceItemIn( cont, 30,  35, new TinkerTools( 1000 ) );
			PlaceItemIn( cont, 60,  35, new HousePlacementTool() );
			PlaceItemIn( cont, 90,  35, new DovetailSaw( 1000 ) );
			PlaceItemIn( cont, 30,  68, new Scissors() );
			PlaceItemIn( cont, 45,  68, new MortarPestle( 1000 ) );
			PlaceItemIn( cont, 75,  68, new ScribesPen( 1000 ) );
			PlaceItemIn( cont, 90,  68, new SmithHammer( 1000 ) );
			PlaceItemIn( cont, 30, 118, new TwoHandedAxe() );
			PlaceItemIn( cont, 60, 118, new FletcherTools( 1000 ) );
			PlaceItemIn( cont, 90, 118, new SewingKit( 1000 ) );

			PlaceItemIn( cont, 36, 51, new RunicHammer( CraftResource.DullCopper, 50 ) );
			PlaceItemIn( cont, 42, 51, new RunicHammer( CraftResource.ShadowIron, 45 ) );
			PlaceItemIn( cont, 48, 51, new RunicHammer( CraftResource.Copper, 40 ) );
			PlaceItemIn( cont, 54, 51, new RunicHammer( CraftResource.Bronze, 35 ) );
			PlaceItemIn( cont, 61, 51, new RunicHammer( CraftResource.Gold, 30 ) );
			PlaceItemIn( cont, 67, 51, new RunicHammer( CraftResource.Agapite, 25 ) );
			PlaceItemIn( cont, 73, 51, new RunicHammer( CraftResource.Verite, 20 ) );
			PlaceItemIn( cont, 79, 51, new RunicHammer( CraftResource.Valorite, 15 ) );

			PlaceItemIn( cont, 36, 55, new RunicSewingKit( CraftResource.SpinedLeather, 45 ) );
			PlaceItemIn( cont, 42, 55, new RunicSewingKit( CraftResource.HornedLeather, 30 ) );
			PlaceItemIn( cont, 48, 55, new RunicSewingKit( CraftResource.BarbedLeather, 15 ) );

			PlaceItemIn( bank, 118, 169, cont );
			// End bag of tools


			// Begin bag of archery ammo
			cont = new Bag();
			cont.Name = "Bag Of Archery Ammo";

			PlaceItemIn( cont, 48, 76, new Arrow( 5000 ) );
			PlaceItemIn( cont, 72, 76, new Bolt( 5000 ) );

			PlaceItemIn( bank, 118, 124, cont );
			// End bag of archery ammo


			// Begin bag of treasure maps
			cont = new Bag();
			cont.Name = "Bag Of Treasure Maps";

			PlaceItemIn( cont, 30, 35, new TreasureMap( 1, Map.Trammel ) );
			PlaceItemIn( cont, 45, 35, new TreasureMap( 2, Map.Trammel ) );
			PlaceItemIn( cont, 60, 35, new TreasureMap( 3, Map.Trammel ) );
			PlaceItemIn( cont, 75, 35, new TreasureMap( 4, Map.Trammel ) );
			PlaceItemIn( cont, 90, 35, new TreasureMap( 5, Map.Trammel ) );
			PlaceItemIn( cont, 90, 35, new TreasureMap( 6, Map.Trammel ) );

			PlaceItemIn( cont, 30, 50, new TreasureMap( 1, Map.Trammel ) );
			PlaceItemIn( cont, 45, 50, new TreasureMap( 2, Map.Trammel ) );
			PlaceItemIn( cont, 60, 50, new TreasureMap( 3, Map.Trammel ) );
			PlaceItemIn( cont, 75, 50, new TreasureMap( 4, Map.Trammel ) );
			PlaceItemIn( cont, 90, 50, new TreasureMap( 5, Map.Trammel ) );
			PlaceItemIn( cont, 90, 50, new TreasureMap( 6, Map.Trammel ) );

			PlaceItemIn( cont, 55, 100, new Lockpick( 30 ) );
			PlaceItemIn( cont, 60, 100, new Pickaxe() );

			PlaceItemIn( bank, 98, 124, cont );
			// End bag of treasure maps


			// Begin bag of raw materials
			cont = new Bag();
			cont.Hue = 0x835;
			cont.Name = "Raw Materials Bag";

			PlaceItemIn( cont, 92, 60, new BarbedLeather( 5000 ) );
			PlaceItemIn( cont, 92, 68, new HornedLeather( 5000 ) );
			PlaceItemIn( cont, 92, 76, new SpinedLeather( 5000 ) );
			PlaceItemIn( cont, 92, 84, new Leather( 5000 ) );

			PlaceItemIn( cont, 30, 118, new Cloth( 5000 ) );
			PlaceItemIn( cont, 30,  84, new Board( 5000 ) );
			PlaceItemIn( cont, 57,  80, new BlankScroll( 500 ) );

			PlaceItemIn( cont, 30,  35, new DullCopperIngot( 5000 ) );
			PlaceItemIn( cont, 37,  35, new ShadowIronIngot( 5000 ) );
			PlaceItemIn( cont, 44,  35, new CopperIngot( 5000 ) );
			PlaceItemIn( cont, 51,  35, new BronzeIngot( 5000 ) );
			PlaceItemIn( cont, 58,  35, new GoldIngot( 5000 ) );
			PlaceItemIn( cont, 65,  35, new AgapiteIngot( 5000 ) );
			PlaceItemIn( cont, 72,  35, new VeriteIngot( 5000 ) );
			PlaceItemIn( cont, 79,  35, new ValoriteIngot( 5000 ) );
			PlaceItemIn( cont, 86,  35, new IronIngot( 5000 ) );

			PlaceItemIn( cont, 30,  59, new RedScales( 5000 ) );
			PlaceItemIn( cont, 36,  59, new YellowScales( 5000 ) );
			PlaceItemIn( cont, 42,  59, new BlackScales( 5000 ) );
			PlaceItemIn( cont, 48,  59, new GreenScales( 5000 ) );
			PlaceItemIn( cont, 54,  59, new WhiteScales( 5000 ) );
			PlaceItemIn( cont, 60,  59, new BlueScales( 5000 ) );

			PlaceItemIn( bank, 98, 169, cont );
			// End bag of raw materials


			// Begin bag of spell casting stuff
			cont = new Backpack();
			cont.Hue = 0x480;
			cont.Name = "Spell Casting Stuff";

			PlaceItemIn( cont, 45, 105, new Spellbook( UInt64.MaxValue ) );
			PlaceItemIn( cont, 65, 105, new NecromancerSpellbook( (UInt64)0xFFFF ) );
			PlaceItemIn( cont, 85, 105, new BookOfChivalry( (UInt64)0x3FF ) );
			PlaceItemIn( cont, 105, 105, new BookOfBushido() );	//Default ctor = full
			PlaceItemIn( cont, 125, 105, new BookOfNinjitsu() ); //Default ctor = full

			Runebook runebook = new Runebook( 10 );
			runebook.CurCharges = runebook.MaxCharges;
			PlaceItemIn( cont, 105, 105, runebook );

			Item toHue = new BagOfReagents( 150 );
			toHue.Hue = 0x2D;
			PlaceItemIn( cont, 45, 150, toHue );

			toHue = new BagOfNecroReagents( 150 );
			toHue.Hue = 0x488;
			PlaceItemIn( cont, 65, 150, toHue );

			PlaceItemIn( cont, 140, 150, new BagOfAllReagents( 500 ) );

			for ( int i = 0; i < 9; ++i )
				PlaceItemIn( cont, 45 + (i * 10), 75, new RecallRune() );

			PlaceItemIn( cont, 141, 74, new FireHorn() );

			PlaceItemIn( bank, 78, 169, cont );
			// End bag of spell casting stuff


			// Begin bag of ethereals
			cont = new Backpack();
			cont.Hue = 0x490;
			cont.Name = "Bag Of Ethy's!";

			PlaceItemIn( cont, 45, 66, new EtherealHorse() );
			PlaceItemIn( cont, 69, 82, new EtherealOstard() );
			PlaceItemIn( cont, 93, 99, new EtherealLlama() );
			PlaceItemIn( cont, 117, 115, new EtherealKirin() );
			PlaceItemIn( cont, 45, 132, new EtherealUnicorn() );
			PlaceItemIn( cont, 69, 66, new EtherealRidgeback() );
			PlaceItemIn( cont, 93, 82, new EtherealSwampDragon() );
			PlaceItemIn( cont, 117, 99, new EtherealBeetle() );

			PlaceItemIn( bank, 38, 124, cont );
			// End bag of ethereals


			// Begin first bag of artifacts
			cont = new Backpack();
			cont.Hue = 0x48F;
			cont.Name = "Bag of Artifacts";

			PlaceItemIn( cont, 45, 66, new TitansHammer() );
			PlaceItemIn( cont, 69, 82, new InquisitorsResolution() );
			PlaceItemIn( cont, 93, 99, new BladeOfTheRighteous() );
			PlaceItemIn( cont, 117, 115, new ZyronicClaw() );

			PlaceItemIn( bank, 58, 124, cont );
			// End first bag of artifacts


			// Begin second bag of artifacts
			cont = new Backpack();
			cont.Hue = 0x48F;
			cont.Name = "Bag of Artifacts";

			PlaceItemIn( cont, 45, 66, new GauntletsOfNobility() );
			PlaceItemIn( cont, 69, 82, new MidnightBracers() );
			PlaceItemIn( cont, 93, 99, new VoiceOfTheFallenKing() );
			PlaceItemIn( cont, 117, 115, new OrnateCrownOfTheHarrower() );
			PlaceItemIn( cont, 45, 132, new HelmOfInsight() );
			PlaceItemIn( cont, 69, 66, new HolyKnightsBreastplate() );
			PlaceItemIn( cont, 93, 82, new ArmorOfFortune() );
			PlaceItemIn( cont, 117, 99, new TunicOfFire() );
			PlaceItemIn( cont, 45, 115, new LeggingsOfBane() );
			PlaceItemIn( cont, 69, 132, new ArcaneShield() );
			PlaceItemIn( cont, 93, 66, new Aegis() );
			PlaceItemIn( cont, 117, 82, new RingOfTheVile() );
			PlaceItemIn( cont, 45, 99, new BraceletOfHealth() );
			PlaceItemIn( cont, 69, 115, new RingOfTheElements() );
			PlaceItemIn( cont, 93, 132, new OrnamentOfTheMagician() );
			PlaceItemIn( cont, 117, 66, new DivineCountenance() );
			PlaceItemIn( cont, 45, 82, new JackalsCollar() );
			PlaceItemIn( cont, 69, 99, new HuntersHeaddress() );
			PlaceItemIn( cont, 93, 115, new HatOfTheMagi() );
			PlaceItemIn( cont, 117, 132, new ShadowDancerLeggings() );
			PlaceItemIn( cont, 45, 66, new SpiritOfTheTotem() );
			PlaceItemIn( cont, 69, 82, new BladeOfInsanity() );
			PlaceItemIn( cont, 93, 99, new AxeOfTheHeavens() );
			PlaceItemIn( cont, 117, 115, new TheBeserkersMaul() );
			PlaceItemIn( cont, 45, 132, new Frostbringer() );
			PlaceItemIn( cont, 69, 66, new BreathOfTheDead() );
			PlaceItemIn( cont, 93, 82, new TheDragonSlayer() );
			PlaceItemIn( cont, 117, 99, new BoneCrusher() );
			PlaceItemIn( cont, 45, 115, new StaffOfTheMagi() );
			PlaceItemIn( cont, 69, 132, new SerpentsFang() );
			PlaceItemIn( cont, 93, 66, new LegacyOfTheDreadLord() );
			PlaceItemIn( cont, 117, 82, new TheTaskmaster() );
			PlaceItemIn( cont, 45, 99, new TheDryadBow() );

			PlaceItemIn( bank, 78, 124, cont );
			// End second bag of artifacts

			// Begin bag of minor artifacts
			cont = new Backpack();
			cont.Hue = 0x48F;
			cont.Name = "Bag of Minor Artifacts";


			PlaceItemIn( cont, 45, 66, new LunaLance() );
			PlaceItemIn( cont, 69, 82, new VioletCourage() );
			PlaceItemIn( cont, 93, 99, new CavortingClub() );
			PlaceItemIn( cont, 117, 115, new CaptainQuacklebushsCutlass() );
			PlaceItemIn( cont, 45, 132, new NightsKiss() );
			PlaceItemIn( cont, 69, 66, new ShipModelOfTheHMSCape() );
			PlaceItemIn( cont, 93, 82, new AdmiralsHeartyRum() );
			PlaceItemIn( cont, 117, 99, new CandelabraOfSouls() );
			PlaceItemIn( cont, 45, 115, new IolosLute() );
			PlaceItemIn( cont, 69, 132, new GwennosHarp() );
			PlaceItemIn( cont, 93, 66, new ArcticDeathDealer() );
			PlaceItemIn( cont, 117, 82, new EnchantedTitanLegBone() );
			PlaceItemIn( cont, 45, 99, new NoxRangersHeavyCrossbow() );
			PlaceItemIn( cont, 69, 115, new BlazeOfDeath() );
			PlaceItemIn( cont, 93, 132, new DreadPirateHat() );
			PlaceItemIn( cont, 117, 66, new BurglarsBandana() );
			PlaceItemIn( cont, 45, 82, new GoldBricks() );
			PlaceItemIn( cont, 69, 99, new AlchemistsBauble() );
			PlaceItemIn( cont, 93, 115, new PhillipsWoodenSteed() );
			PlaceItemIn( cont, 117, 132, new PolarBearMask() );
			PlaceItemIn( cont, 45, 66, new BowOfTheJukaKing() );
			PlaceItemIn( cont, 69, 82, new GlovesOfThePugilist() );
			PlaceItemIn( cont, 93, 99, new OrcishVisage() );
			PlaceItemIn( cont, 117, 115, new StaffOfPower() );
			PlaceItemIn( cont, 45, 132, new ShieldOfInvulnerability() );

			PlaceItemIn( cont, 69, 66, new HeartOfTheLion() );
			PlaceItemIn( cont, 93, 82, new ColdBlood() );
			PlaceItemIn( cont, 117, 99, new GhostShipAnchor() );
			PlaceItemIn( cont, 45, 115, new SeahorseStatuette() );
			PlaceItemIn( cont, 69, 132, new WrathOfTheDryad() );
			PlaceItemIn( cont, 93, 66, new PixieSwatter() );

			/*	TODO: Make MiBs that have 100% chance of a Ancient SoS
			for( int i = 0; i < 10; i++ )
                PlaceItemIn( cont, 117, 128, new MessageInABottle() );
			*/

			PlaceItemIn( bank, 18, 124, cont );
		}

		private static void FillBankbox( Mobile m )
		{
			if ( Core.AOS )
			{
				FillBankAOS( m );
				return;
			}

			BankBox bank = m.BankBox;

			if ( bank != null )
			{
				bank.DropItem( new BankCheck( 1000000 ) );

				// Full spellbook
				Spellbook book = new Spellbook();

				book.Content = ulong.MaxValue;

				bank.DropItem( book );

				Bag bag = new Bag();

				for ( int i = 0; i < 5; ++i )
					bag.DropItem( new Moonstone( MoonstoneType.Felucca ) );

				// Felucca moonstones
				bank.DropItem( bag );

				bag = new Bag();

				for ( int i = 0; i < 5; ++i )
					bag.DropItem( new Moonstone( MoonstoneType.Trammel ) );

				// Trammel moonstones
				bank.DropItem( bag );

				// Treasure maps
				bank.DropItem( new TreasureMap( 1, Map.Trammel ) );
				bank.DropItem( new TreasureMap( 2, Map.Trammel ) );
				bank.DropItem( new TreasureMap( 3, Map.Trammel ) );
				bank.DropItem( new TreasureMap( 4, Map.Trammel ) );
				bank.DropItem( new TreasureMap( 5, Map.Trammel ) );

				// Bag containing 50 of each reagent
				bank.DropItem( new BagOfReagents( 50 ) );

				// Craft tools
				bank.DropItem( MakeNewbie( new Scissors() ) );
				bank.DropItem( MakeNewbie( new SewingKit( 1000 ) ) );
				bank.DropItem( MakeNewbie( new SmithHammer( 1000 ) ) );
				bank.DropItem( MakeNewbie( new FletcherTools( 1000 ) ) );
				bank.DropItem( MakeNewbie( new DovetailSaw( 1000 ) ) );
				bank.DropItem( MakeNewbie( new MortarPestle( 1000 ) ) );
				bank.DropItem( MakeNewbie( new ScribesPen( 1000 ) ) );
				bank.DropItem( MakeNewbie( new TinkerTools( 1000 ) ) );

				// A few dye tubs
				bank.DropItem( new Dyes() );
				bank.DropItem( new DyeTub() );
				bank.DropItem( new DyeTub() );
				bank.DropItem( new BlackDyeTub() );

				DyeTub darkRedTub = new DyeTub();

				darkRedTub.DyedHue = 0x485;
				darkRedTub.Redyable = false;

				bank.DropItem( darkRedTub );

				// Some food
				bank.DropItem( MakeNewbie( new Apple( 1000 ) ) );

				// Resources
				bank.DropItem( MakeNewbie( new Feather( 1000 ) ) );
				bank.DropItem( MakeNewbie( new BoltOfCloth( 1000 ) ) );
				bank.DropItem( MakeNewbie( new BlankScroll( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Hides( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Bandage( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Bottle( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Log( 1000 ) ) );

				bank.DropItem( MakeNewbie( new IronIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new DullCopperIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new ShadowIronIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new CopperIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new BronzeIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new GoldIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new AgapiteIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new VeriteIngot( 5000 ) ) );
				bank.DropItem( MakeNewbie( new ValoriteIngot( 5000 ) ) );

				// Reagents
				bank.DropItem( MakeNewbie( new BlackPearl( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Bloodmoss( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Garlic( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Ginseng( 1000 ) ) );
				bank.DropItem( MakeNewbie( new MandrakeRoot( 1000 ) ) );
				bank.DropItem( MakeNewbie( new Nightshade( 1000 ) ) );
				bank.DropItem( MakeNewbie( new SulfurousAsh( 1000 ) ) );
				bank.DropItem( MakeNewbie( new SpidersSilk( 1000 ) ) );

				// Some extra starting gold
				bank.DropItem( MakeNewbie( new Gold( 9000 ) ) );

				// 5 blank recall runes
				for ( int i = 0; i < 5; ++i )
					bank.DropItem( MakeNewbie( new RecallRune() ) );

				AddPowerScrolls( bank );
			}
		}

		private static void AddPowerScrolls( BankBox bank )
		{
			Bag bag = new Bag();

			for ( int i = 0; i < PowerScroll.Skills.Length; ++i )
				bag.DropItem( new PowerScroll( PowerScroll.Skills[i], 120.0 ) );

			bag.DropItem( new StatCapScroll( 250 ) );

			bank.DropItem( bag );
		}

		private static void AddShirt( Mobile m, int shirtHue )
		{
			int hue = Utility.ClipDyedHue( shirtHue & 0x3FFF );

			switch ( Utility.Random( 3 ) )
			{
				case 0: EquipItem( new Shirt( hue ), true ); break;
				case 1: EquipItem( new FancyShirt( hue ), true ); break;
				case 2: EquipItem( new Doublet( hue ), true ); break;
			}
		}

		private static void AddPants( Mobile m, int pantsHue )
		{
			int hue = Utility.ClipDyedHue( pantsHue & 0x3FFF );

			if ( m.Female )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: EquipItem( new Skirt( hue ), true ); break;
					case 1: EquipItem( new Kilt( hue ), true ); break;
				}
			}
			else
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: EquipItem( new LongPants( hue ), true ); break;
					case 1: EquipItem( new ShortPants( hue ), true ); break;
				}
			}
		}

		private static void AddShoes( Mobile m )
		{
			EquipItem( new Shoes( Utility.RandomYellowHue() ), true );
		}

		private static void AddHair( Mobile m, int itemID, int hue )
		{
			Item item;

			switch ( itemID & 0x3FFF )
			{
				case 0x2044: item = new Mohawk( hue ); break;
				case 0x2045: item = new PageboyHair( hue ); break;
				case 0x2046: item = new BunsHair( hue ); break;
				case 0x2047: item = new Afro( hue ); break;
				case 0x2048: item = new ReceedingHair( hue ); break;
				case 0x2049: item = new TwoPigTails( hue ); break;
				case 0x204A: item = new KrisnaHair( hue ); break;
				case 0x203B: item = new ShortHair( hue ); break;
				case 0x203C: item = new LongHair( hue ); break;
				case 0x203D: item = new PonyTail( hue ); break;
				default: return;
			}

			m.AddItem( item );
		}

		private static void AddBeard( Mobile m, int itemID, int hue )
		{
			if ( m.Female )
				return;

			Item item;

			switch ( itemID & 0x3FFF )
			{
				case 0x203E: item = new LongBeard( hue ); break;
				case 0x203F: item = new ShortBeard( hue ); break;
				case 0x2040: item = new Goatee( hue ); break;
				case 0x2041: item = new Mustache( hue ); break;
				case 0x204B: item = new MediumShortBeard( hue ); break;
				case 0x204C: item = new MediumLongBeard( hue ); break;
				case 0x204D: item = new Vandyke( hue ); break;
				default: return;
			}

			m.AddItem( item );
		}

		private static Mobile CreateMobile( Account a )
		{
			//if ( a.Count >= a.Limit )
			//	return null;

			if ( a.AccessLevel == AccessLevel.Player )
			{
                int x = 3;

                if (a.GetTag("4Slot") != null)
                    x = 4;
                if (a.GetTag("5Slot") != null)
                    x = 5;

				for ( int i = 0; i < x; ++i )
				{
					if ( a[i] == null )
						return (a[i] = new TeiravonMobile());
				}
			}
			else
			{
				for ( int i = 0; i < 7; ++i )
				{
					if ( a[i] == null )
						return (a[i] = new TeiravonMobile());
				}
			}

			return null;
		}

		private static void EventSink_CharacterCreated( CharacterCreatedEventArgs args )
		{
			if ( !VerifyProfession( args.Profession ) )
				args.Profession = 0;

			Mobile newChar = CreateMobile( args.Account as Account );

			if ( newChar == null )
			{
				Console.WriteLine( "Login: {0}: Character creation failed, account full", args.State );
				return;
			}

			args.Mobile = newChar;
			m_Mobile = newChar;

			newChar.Player = true;
			newChar.AccessLevel = ((Account)args.Account).AccessLevel;
			newChar.Female = args.Female;
			newChar.Body = newChar.Female ? 0x191 : 0x190;
			newChar.Hue = Utility.ClipSkinHue( args.Hue & 0x3FFF ) | 0x8000;
			newChar.Hunger = 20;

			bool young = false;

			/*
			if ( newChar is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile) newChar;

				pm.Profession = args.Profession;

				if ( pm.AccessLevel == AccessLevel.Player && ((Account)pm.Account).Young )
					young = pm.Young = true;
			}
			*/
			if ( newChar is TeiravonMobile )
			{
				PlayerMobile pm = (PlayerMobile) newChar;

				if ( pm.AccessLevel == AccessLevel.Player && ((Account)pm.Account).Young )
					young = pm.Young = true;

				((TeiravonMobile)newChar).Profession = args.Profession;

				((TeiravonMobile)newChar).MaxHits = 20;
				((TeiravonMobile)newChar).Hits = 20;
				((TeiravonMobile)newChar).MaxStam = 20;
				((TeiravonMobile)newChar).Stam = 20;
				((TeiravonMobile)newChar).MaxMana = 20;
				((TeiravonMobile)newChar).Mana = 20;
			}

			SetName( newChar, args.Name );

			AddBackpack( newChar );

			SetStats( newChar, args.Str, args.Dex, args.Int );
			//SetSkills( newChar, args.Skills, args.Profession );

			AddHair( newChar, args.HairID, Utility.ClipHairHue( args.HairHue & 0x3FFF ) );
			AddBeard( newChar, args.BeardID, Utility.ClipHairHue( args.BeardHue & 0x3FFF ) );

			/*
			if ( args.Profession <= 3 )
			{
				AddShirt( newChar, args.ShirtHue );
				AddPants( newChar, args.PantsHue );
				AddShoes( newChar );
			}


			if( TestCenter.Enabled )
				FillBankbox( newChar );
			*/

			if ( young && newChar.BankBox != null )
			{
				NewPlayerTicket ticket = new NewPlayerTicket();
				ticket.Owner = newChar;
				newChar.BankBox.DropItem( ticket );
			}

			//CityInfo city = GetStartLocation( args, young );
			//CityInfo city = new CityInfo( "Britain", "Sweet Dreams Inn", 1496, 1628, 10, Map.Felucca );

			Point3D loc = new Point3D( 989, 520, -50 );
			newChar.MoveToWorld( loc, Map.Malas );

			Console.WriteLine( "Login: {0}: New character being created (account={1})", args.State, ((Account)args.Account).Username );
			Console.WriteLine( " - Character: {0} (serial={1})", newChar.Name, newChar.Serial );
			//Console.WriteLine( " - Started: {0} {1} in {2}", city.City, city.Location, city.Map.ToString() );

			new WelcomeTimer( newChar ).Start();
		}

		public static bool VerifyProfession( int profession )
		{
			if ( profession < 0 )
				return false;
			else if ( profession < 4 )
				return true;
			else if ( Core.AOS && profession < 6 )
				return true;
			else if ( Core.SE && profession < 8 )
				return true;
			else
				return false;
		}

		private static CityInfo GetStartLocation( CharacterCreatedEventArgs args, bool isYoung )
		{
			switch ( args.Profession )
			{
				case 4: //Necro
				{
					return new CityInfo( "Umbra", "Mardoth's Tower", 2114, 1301, -50, Map.Malas );
				}
				case 5:	//Paladin
				{
					return new CityInfo( "Haven", "Uzeraan's Mansion", 3578, 2589, 0, Map.Trammel );
				}
				case 6:	//Samurai
				{
					return new CityInfo( "Samurai DE", "Haoti's Grounds", 368, 780, -1, Map.Malas );
				}
				case 7:	//Ninja
				{
					return new CityInfo( "Ninja DE", "Enimo's Residence", 414,	823, -1, Map.Malas );
				}
				default:
				{
					if( isYoung )
						return new CityInfo( "Haven", "Uzeraan's Mansion", 3582, 2587, 0, Map.Trammel );
					else
						return args.City;
				}
			}
		}

		private static void FixStats( ref int str, ref int dex, ref int intel )
		{
			int vStr = str - 10;
			int vDex = dex - 10;
			int vInt = intel - 10;

			if ( vStr < 0 )
				vStr = 0;

			if ( vDex < 0 )
				vDex = 0;

			if ( vInt < 0 )
				vInt = 0;

			int total = vStr + vDex + vInt;

			if ( total == 0 || total == 50 )
				return;

			double scalar = 50 / (double)total;

			vStr = (int)(vStr * scalar);
			vDex = (int)(vDex * scalar);
			vInt = (int)(vInt * scalar);

			FixStat( ref vStr, (vStr + vDex + vInt) - 50 );
			FixStat( ref vDex, (vStr + vDex + vInt) - 50 );
			FixStat( ref vInt, (vStr + vDex + vInt) - 50 );

			str = vStr + 10;
			dex = vDex + 10;
			intel = vInt + 10;
		}

		private static void FixStat( ref int stat, int diff )
		{
			stat += diff;

			if ( stat < 0 )
				stat = 0;
			else if ( stat > 50 )
				stat = 50;
		}

		private static void SetStats( Mobile m, int str, int dex, int intel )
		{
			FixStats( ref str, ref dex, ref intel );

			if ( str < 10 || str > 60 || dex < 10 || dex > 60 || intel < 10 || intel > 60 || (str + dex + intel) != 80 )
			{
				str = 10;
				dex = 10;
				intel = 10;
			}

			m.InitStats( str, dex, intel );
		}

		private static void SetName( Mobile m, string name )
		{
			name = name.Trim();

			if ( !NameVerification.Validate( name, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote ) )
				name = "Generic Player";

			m.Name = name;
		}

		private static bool ValidSkills( SkillNameValue[] skills )
		{
			int total = 0;

			for ( int i = 0; i < skills.Length; ++i )
			{
				if ( skills[i].Value < 0 || skills[i].Value > 50 )
					return false;

				total += skills[i].Value;

				for ( int j = i + 1; j < skills.Length; ++j )
				{
					if ( skills[j].Value > 0 && skills[j].Name == skills[i].Name )
						return false;
				}
			}

			return ( total == 100 );
		}

		private static Mobile m_Mobile;

		private static void SetSkills( Mobile m, SkillNameValue[] skills, int prof )
		{
			switch ( prof )
			{
				case 1: // Warrior
				{
					skills = new SkillNameValue[]
						{
							new SkillNameValue( SkillName.Anatomy, 30 ),
							new SkillNameValue( SkillName.Healing, 45 ),
							new SkillNameValue( SkillName.Swords, 35 ),
							new SkillNameValue( SkillName.Tactics, 50 )
						};

					break;
				}
				case 2: // Magician
				{
					skills = new SkillNameValue[]
						{
							new SkillNameValue( SkillName.EvalInt, 30 ),
							new SkillNameValue( SkillName.Wrestling, 30 ),
							new SkillNameValue( SkillName.Magery, 50 ),
							new SkillNameValue( SkillName.Meditation, 50 )
						};

					break;
				}
				case 3: // Blacksmith
				{
					skills = new SkillNameValue[]
						{
							new SkillNameValue( SkillName.Mining, 30 ),
							new SkillNameValue( SkillName.ArmsLore, 30 ),
							new SkillNameValue( SkillName.Blacksmith, 50 ),
							new SkillNameValue( SkillName.Tinkering, 50 )
						};

					break;
				}
				case 4: // Necromancer
				{
					skills = new SkillNameValue[]
						{
							new SkillNameValue( SkillName.Necromancy, 50 ),
							new SkillNameValue( SkillName.Focus, 30 ),
							new SkillNameValue( SkillName.SpiritSpeak, 30 ),
							new SkillNameValue( SkillName.Swords, 30 ),
							new SkillNameValue( SkillName.Tactics, 20 )
						};

					break;
				}
				case 5: // Paladin
				{
					skills = new SkillNameValue[]
						{
							new SkillNameValue( SkillName.Chivalry, 51 ),
							new SkillNameValue( SkillName.Swords, 49 ),
							new SkillNameValue( SkillName.Focus, 30 ),
							new SkillNameValue( SkillName.Tactics, 30 )
						};

					break;
				}
				case 6:	//Samurai
				{
					skills = new SkillNameValue[]
						{
							new SkillNameValue( SkillName.Bushido, 50 ),
							new SkillNameValue( SkillName.Swords, 50 ),
							new SkillNameValue( SkillName.Anatomy, 30 ),
							new SkillNameValue( SkillName.Healing, 30 ),
					};
					break;
				}
				case 7:	//Ninja
				{
					skills = new SkillNameValue[]
						{
							new SkillNameValue( SkillName.Ninjitsu, 50 ),
							new SkillNameValue( SkillName.Hiding, 50 ),
							new SkillNameValue( SkillName.Fencing, 30 ),
							new SkillNameValue( SkillName.Stealth, 30 ),
						};
					break;
				}
				default:
				{
					if ( !ValidSkills( skills ) )
						return;

					break;
				}
			}

			bool addSkillItems = true;

			switch ( prof )
			{
				case 1: // Warrior
				{
					EquipItem( new LeatherChest() );
					break;
				}
				case 4: // Necromancer
				{
					Container regs = new BagOfNecroReagents( 50 );

					if ( !Core.AOS )
					{
						foreach ( Item item in regs.Items )
							item.LootType = LootType.Newbied;
					}

					PackItem( regs );

					regs.LootType = LootType.Regular;

					EquipItem( new BoneHarvester() );
					EquipItem( new BoneHelm() );

					EquipItem( NecroHue( new LeatherChest() ) );
					EquipItem( NecroHue( new LeatherArms() ) );
					EquipItem( NecroHue( new LeatherGloves() ) );
					EquipItem( NecroHue( new LeatherGorget() ) );
					EquipItem( NecroHue( new LeatherLegs() ) );
					EquipItem( NecroHue( new Skirt() ) );
					EquipItem( new Sandals( 0x8FD ) );

					Spellbook book = new NecromancerSpellbook( (ulong)0x8981 ); // animate dead, evil omen, pain spike, summon familiar, wraith form

					PackItem( book );

					book.LootType = LootType.Blessed;

					addSkillItems = false;

					break;
				}
				case 5: // Paladin
				{
					EquipItem( new Broadsword() );
					EquipItem( new Helmet() );
					EquipItem( new PlateGorget() );
					EquipItem( new RingmailArms() );
					EquipItem( new RingmailChest() );
					EquipItem( new RingmailLegs() );
					EquipItem( new ThighBoots( 0x748 ) );
					EquipItem( new Cloak( 0xCF ) );
					EquipItem( new BodySash( 0xCF ) );

					Spellbook book = new BookOfChivalry( (ulong)0x3FF );

					PackItem( book );

					book.LootType = LootType.Blessed;

					break;
				}

				case 6: // Samurai
				{
					addSkillItems = false;
					EquipItem( new HakamaShita( 0x2C3 ) );
					EquipItem( new Hakama( 0x2C3 ) );
					EquipItem( new SamuraiTabi( 0x2C3 ) );
					EquipItem( new TattsukeHakama( 0x22D ) );
					EquipItem( new Bokuto() );
					EquipItem( new LeatherJingasa() );
					EquipItem( new Scissors() );
					EquipItem( new Bandage( 50 ) );

					Spellbook book = new BookOfBushido();
					PackItem( book );

					break;
				}
				case 7: // Ninja
				{
					addSkillItems = false;
					EquipItem( new Kasa() );

					int[] hues = new int[] { 0x1A8, 0xEC, 0x99, 0x90, 0xB5, 0x336, 0x89	};
					//TODO: Verify that's ALL the hues for that above.

					EquipItem( new TattsukeHakama( hues[Utility.Random(hues.Length)] ) );

					EquipItem( new HakamaShita( 0x2C3 ) );
					EquipItem( new NinjaTabi( 0x2C3 ) );
					EquipItem( new Tekagi() );
					//EquipItem( new Smokebomb() );

					Spellbook book = new BookOfNinjitsu( );
					PackItem( book );

					break;
				}
			}

			for ( int i = 0; i < skills.Length; ++i )
			{
				SkillNameValue snv = skills[i];

				if ( snv.Value > 0 && snv.Name != SkillName.Stealth && snv.Name != SkillName.RemoveTrap )
				{
					Skill skill = m.Skills[snv.Name];

					if ( skill != null )
					{
						skill.BaseFixedPoint = snv.Value * 10;

						if ( addSkillItems )
							AddSkillItems( snv.Name );
					}
				}
			}
		}

		private static void EquipItem( Item item )
		{
			EquipItem( item, false );
		}

		private static void EquipItem( Item item, bool mustEquip )
		{
			if ( !Core.AOS )
				item.LootType = LootType.Newbied;

			if ( m_Mobile != null && m_Mobile.EquipItem( item ) )
				return;

			Container pack = m_Mobile.Backpack;

			if ( !mustEquip && pack != null )
				pack.DropItem( item );
			else
				item.Delete();
		}

		private static void PackItem( Item item )
		{
			if ( !Core.AOS )
				item.LootType = LootType.Newbied;

			Container pack = m_Mobile.Backpack;

			if ( pack != null )
				pack.DropItem( item );
			else
				item.Delete();
		}

		private static void PackInstrument()
		{
			switch ( Utility.Random( 6 ) )
			{
				case 0: PackItem( new Drums() ); break;
				case 1: PackItem( new Harp() ); break;
				case 2: PackItem( new LapHarp() ); break;
				case 3: PackItem( new Lute() ); break;
				case 4: PackItem( new Tambourine() ); break;
				case 5: PackItem( new TambourineTassel() ); break;
			}
		}

		private static void PackScroll( int circle )
		{
			switch ( Utility.Random( 8 ) * (circle * 8) )
			{
				case  0: PackItem( new ClumsyScroll() ); break;
				case  1: PackItem( new CreateFoodScroll() ); break;
				case  2: PackItem( new FeeblemindScroll() ); break;
				case  3: PackItem( new HealScroll() ); break;
				case  4: PackItem( new MagicArrowScroll() ); break;
				case  5: PackItem( new NightSightScroll() ); break;
				case  6: PackItem( new ReactiveArmorScroll() ); break;
				case  7: PackItem( new WeakenScroll() ); break;
				case  8: PackItem( new AgilityScroll() ); break;
				case  9: PackItem( new CunningScroll() ); break;
				case 10: PackItem( new CureScroll() ); break;
				case 11: PackItem( new HarmScroll() ); break;
				case 12: PackItem( new MagicTrapScroll() ); break;
				case 13: PackItem( new MagicUnTrapScroll() ); break;
				case 14: PackItem( new ProtectionScroll() ); break;
				case 15: PackItem( new StrengthScroll() ); break;
				case 16: PackItem( new BlessScroll() ); break;
				case 17: PackItem( new FireballScroll() ); break;
				case 18: PackItem( new MagicLockScroll() ); break;
				case 19: PackItem( new PoisonScroll() ); break;
				case 20: PackItem( new TelekinisisScroll() ); break;
				case 21: PackItem( new TeleportScroll() ); break;
				case 22: PackItem( new UnlockScroll() ); break;
				case 23: PackItem( new WallOfStoneScroll() ); break;
			}
		}

		private static Item NecroHue( Item item )
		{
			item.Hue = 0x2C3;

			return item;
		}

		private static void AddSkillItems( SkillName skill )
		{
			switch ( skill )
			{
				case SkillName.Alchemy:
				{
					PackItem( new Bottle( 4 ) );
					PackItem( new MortarPestle() );
					EquipItem( new Robe( Utility.RandomPinkHue() ) );
					break;
				}
				case SkillName.Anatomy:
				{
					PackItem( new Bandage( 3 ) );
					EquipItem( new Robe( Utility.RandomYellowHue() ) );
					break;
				}
				case SkillName.AnimalLore:
				{
					EquipItem( new ShepherdsCrook() );
					EquipItem( new Robe( Utility.RandomBlueHue() ) );
					break;
				}
				case SkillName.Archery:
				{
					PackItem( new Arrow( 25 ) );
					EquipItem( new Bow() );
					break;
				}
				case SkillName.ArmsLore:
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: EquipItem( new Kryss() ); break;
						case 1: EquipItem( new Katana() ); break;
						case 2: EquipItem( new Club() ); break;
					}

					break;
				}
				case SkillName.Begging:
				{
					EquipItem( new GnarledStaff() );
					break;
				}
				case SkillName.Blacksmith:
				{
					PackItem( new Tongs() );
					PackItem( new Pickaxe() );
					PackItem( new Pickaxe() );
					PackItem( new IronIngot( 50 ) );
					EquipItem( new HalfApron( Utility.RandomYellowHue() ) );
					break;
				}
				case SkillName.Fletching:
				{
					PackItem( new Board( 14 ) );
					PackItem( new Feather( 5 ) );
					PackItem( new Shaft( 5 ) );
					break;
				}
				case SkillName.Camping:
				{
					PackItem( new Bedroll() );
					PackItem( new Kindling( 5 ) );
					break;
				}
				case SkillName.Carpentry:
				{
					PackItem( new Board( 10 ) );
					PackItem( new Saw() );
					EquipItem( new HalfApron( Utility.RandomYellowHue() ) );
					break;
				}
				case SkillName.Cartography:
				{
					PackItem( new BlankMap() );
					PackItem( new BlankMap() );
					PackItem( new BlankMap() );
					PackItem( new BlankMap() );
					PackItem( new Sextant() );
					break;
				}
				case SkillName.Cooking:
				{
					PackItem( new Kindling( 2 ) );
					PackItem( new RawLambLeg() );
					PackItem( new RawChickenLeg() );
					PackItem( new RawFishSteak() );
					PackItem( new SackFlour() );
					PackItem( new Pitcher( BeverageType.Water ) );
					break;
				}
				case SkillName.DetectHidden:
				{
					EquipItem( new Cloak( 0x455 ) );
					break;
				}
				case SkillName.Discordance:
				{
					PackInstrument();
					break;
				}
				case SkillName.Fencing:
				{
					EquipItem( new Kryss() );
					break;
				}
				case SkillName.Fishing:
				{
					EquipItem( new FishingPole() );
					EquipItem( new FloppyHat( Utility.RandomYellowHue() ) );
					break;
				}
				case SkillName.Healing:
				{
					PackItem( new Bandage( 50 ) );
					PackItem( new Scissors() );
					break;
				}
				case SkillName.Herding:
				{
					EquipItem( new ShepherdsCrook() );
					break;
				}
				case SkillName.Hiding:
				{
					EquipItem( new Cloak( 0x455 ) );
					break;
				}
				case SkillName.Inscribe:
				{
					PackItem( new BlankScroll( 2 ) );
					PackItem( new BlueBook() );
					break;
				}
				case SkillName.ItemID:
				{
					EquipItem( new GnarledStaff() );
					break;
				}
				case SkillName.Lockpicking:
				{
					PackItem( new Lockpick( 20 ) );
					break;
				}
				case SkillName.Lumberjacking:
				{
					EquipItem( new Hatchet() );
					break;
				}
				case SkillName.Macing:
				{
					EquipItem( new Club() );
					break;
				}
				case SkillName.Magery:
				{
					BagOfReagents regs = new BagOfReagents( 30 );

					if ( !Core.AOS )
					{
						foreach ( Item item in regs.Items )
							item.LootType = LootType.Newbied;
					}

					PackItem( regs );

					regs.LootType = LootType.Regular;

					PackScroll( 0 );
					PackScroll( 1 );
					PackScroll( 2 );

					Spellbook book = new Spellbook( (ulong)0x382A8C38 );

					EquipItem( book );

					book.LootType = LootType.Blessed;

					EquipItem( new Robe( Utility.RandomBlueHue() ) );
					EquipItem( new WizardsHat() );

					break;
				}
				case SkillName.Mining:
				{
					PackItem( new Pickaxe() );
					break;
				}
				case SkillName.Musicianship:
				{
					PackInstrument();
					break;
				}
				case SkillName.Parry:
				{
					EquipItem( new WoodenShield() );
					break;
				}
				case SkillName.Peacemaking:
				{
					PackInstrument();
					break;
				}
				case SkillName.Poisoning:
				{
					PackItem( new LesserPoisonPotion() );
					PackItem( new LesserPoisonPotion() );
					break;
				}
				case SkillName.Provocation:
				{
					PackInstrument();
					break;
				}
				case SkillName.Snooping:
				{
					PackItem( new Lockpick( 20 ) );
					break;
				}
				case SkillName.SpiritSpeak:
				{
					EquipItem( new Cloak( 0x455 ) );
					break;
				}
				case SkillName.Stealing:
				{
					PackItem( new Lockpick( 20 ) );
					break;
				}
				case SkillName.Swords:
				{
					EquipItem( new Katana() );
					break;
				}
				case SkillName.Tactics:
				{
					EquipItem( new Katana() );
					break;
				}
				case SkillName.Tailoring:
				{
					PackItem( new BoltOfCloth() );
					PackItem( new SewingKit() );
					break;
				}
				case SkillName.Tracking:
				{
					if ( m_Mobile != null )
					{
						Item shoes = m_Mobile.FindItemOnLayer( Layer.Shoes );

						if ( shoes != null )
							shoes.Delete();
					}

					EquipItem( new Boots( Utility.RandomYellowHue() ) );
					EquipItem( new SkinningKnife() );
					break;
				}
				case SkillName.Veterinary:
				{
					PackItem( new Bandage( 5 ) );
					PackItem( new Scissors() );
					break;
				}
				case SkillName.Wrestling:
				{
					EquipItem( new LeatherGloves() );
					break;
				}
			}
		}
	}
}