using System;
using System.Collections;
using Server;
using Server.Spells;
using Server.Items;
using Server.Mobiles;

namespace Server.Teiravon
{
	public class NPC
	{
		/* Readjusted NPCs
			"ScaledSwampDragon",
			"SkeletalMount",
			"SwampDragon",
			"BoneMagi",
			"MeerCaptain",
			"MeerWarrior",
			"FleshGolem",
			"EnergyVortex",

			"BladeSpirits",
		*/

		public static String[] ForestLevel1 = new String[] {
			"Horse",
			"RidableLlama",
			"Chicken",
			"Goat",
			"Pig",
			"Sheep",
			"JackRabbit",
			"Rabbit",
			"SewerRat",
			"Bird",
			"Cat",
			"Rat",
			"Mongbat",
			"StrongMongbat",
			"Eagle",
			"Cow",
			"Boar",
			"BullFrog",
			"Llama",
			"Snake",
			"Dog",
			"RestlessSoul",
			"Slime",
			"Hind",
			"MountainGoat",
			"SkitteringHopper",
			"ShadowWisp",
			"GreatHart",
			"GiantRat",
			"Zombie"
		};

		public static String[] ForestLevel2 = new String[] {
			"GreyWolf",
			"TimberWolf",
			"Cougar",
			"Panther",
			"Gorilla",
			"Skeleton",
			"BlackBear",
			"BrownBear",
			"DireWolf",
			"Bull",
			"GiantToad",
			"Alligator",
			"PatchworkSkeleton",
			"VampireBat",
			"GiantBlackWidow",
			"GiantSpider",
			"Brigand",
			"Ghoul",
			"Guardian",
			"Orc",
			"Ratman",
			"Savage",
			"Bogling",
			"Quagmire",
			"SwampTentacle",
			"Harpy",
			"Lizardman",
			"GrizzlyBear",
			"WailingBanshee",
			"EarthElemental",
			"Bogle",
			"EvilMage",
			"EvilMageLord",
			"SavageShaman",
			"Shade",
			"SkeletalMage",
			"Spectre",
			"Wraith",
			"Ettin",
			"RatmanArcher",
			"SavageRider",
			"Corpser",
			"GiantSerpent",
			"AirElemental",
			"WaterElemental",
			"GolemController",
			"OrcishMage",
			"RatmanMage",
			"OrcishLord",
			"Troll",
		};

		public static String[] ForestLevel3 = new String[] {
			"Kirin",
			"SilverSteed",
			"Unicorn",
			"DreadSpider",
			"Lich",
			"BoneKnight",
			"SkeletalKnight",
			"MeerMage",
			"Wisp",
			"AgapiteElemental",
			"BronzeElemental",
			"CopperElemental",
			"DullCopperElemental",
			"GoldenElemental",
			"ShadowIronElemental",
			"ValoriteElemental",
			"VeriteElemental",
			"Reaper",
			"Centaur",
			"PlagueSpawn",
			"WhippingVine",
			"StoneHarpy",
			"SilverSerpent",
			"AncientLich",
			"Cyclops",
			"KhaldunSummoner",
			"KhaldunZealot",
			"Mummy",
			"RottingCorpse",
			"JukaMage",
			"JukaWarrior",
			"Golem",
			"OrcCaptain",
			"Efreet",
			"ToxicElemental",
			"Executioner",
			"Drake"
		};

		public static String[] ForestLevel4 = new String[] {
			"ForestOstard",
			"FrenziedOstard",
			"Wyvern",
			"MoundOfMaggots",
			"Treefellow",
			"Pixie",
			"JukaLord",
			"PlagueBeast",
			"PoisonElemental",
			"Betrayer",
			"LichLord",
			"Titan",
			"MeerEternal",
			"Ogre",
			"CrystalElemental",
			"SerpentineDragon"
		};

		public static String[] ForestLevel5 = new String[] {
			"EtherealWarrior",
			"WhiteWyrm",
			"OgreLord",
			"OrcBrute",
			"OrcBomber",
			"BogThing",
			"Dragon",
			"AncientWyrm",
			"ShadowWyrm",
			"SkeletalDragon"
		};

		public static String[] SnowLevel1 = new String[] {
			"RidableLlama",
			"SilverSteed",
			"JackRabbit",
			"Rabbit",
			"SewerRat",
			"Bird",
			"Rat",
			"Eagle",
			"Llama",
			"Walrus",
			"Snake",
			"RestlessSoul",
			"FrostOoze",
			"Hind",
			"MountainGoat",
			"IceSnake",
			"GreatHart",
			"GiantRat"
		};

		public static String[] SnowLevel2 = new String[] {
			"GreyWolf",
			"TimberWolf",
			"WhiteWolf",
			"SnowLeopard",
			"PolarBear",
			"FrostSpider",
			"GiantBlackWidow",
			"GiantSpider",
			"Brigand",
			"GiantIceWorm",
			"IceSerpent",
			"AirElemental",
			"IceElemental"
		};

		public static String[] SnowLevel3 = new String[] {
			"FrostTroll",
			"Wisp",
			"AgapiteElemental",
			"BronzeElemental",
			"CopperElemental",
			"DullCopperElemental",
			"GoldenElemental",
			"ShadowIronElemental",
			"ValoriteElemental",
			"VeriteElemental",
			"SilverSerpent",
			"SnowElemental",
			"Executioner"
		};

		public static String[] SnowLevel4 = new String[] {
			"IceFiend",
			"CrystalElemental"
		};

		public static String[] SnowLevel5 = new String[] {
			"WhiteWyrm",
			"ArcticOgreLord"
		};

		public static String[] DesertLevel1 = new String[] {
			"Beetle",
			"DesertOstard",
			"FireSteed",
			"FrenziedOstard",
			"Ridgeback",
			"SavageRidgeback",
			"SewerRat",
			"Bird",
			"Rat",
			"Eagle",
			"Cow",
			"Snake",
			"RestlessSoul",
			"LavaSnake",
			"TerathanDrone",
			"GiantRat",
			"Zombie"
		};

		public static String[] DesertLevel2 = new String[] {
			"Skeleton",
			"HellCat",
			"BlackSolenWorker",
			"RedSolenWorker",
			"PatchworkSkeleton",
			"GiantBlackWidow",
			"GiantSpider",
			"Brigand",
			"Ghoul",
			"Guardian",
			"Orc",
			"OrcCaptain",
			"Ratman",
			"Savage",
			"MeerCaptain",
			"MeerWarrior",
			"SandVortex",
			"Harpy",
			"Lizardman",
			"Scorpion",
			"PredatorHellCat",
			"LavaLizard",
			"WailingBanshee",
			"EarthElemental",
			"Bogle",
			"BoneMagi",
			"EvilMage",
			"EvilMageLord",
			"SavageShaman",
			"Shade",
			"SkeletalMage",
			"Spectre",
			"Wraith",
			"RatmanArcher",
			"SavageRider",
			"HellHound",
			"BladeSpirits",
			"GiantSerpent",
			"BlackSolenInfiltratorWarrior",
			"BlackSolenWarrior",
			"RedSolenInfiltratorWarrior",
			"RedSolenWarrior",
			"CrystalElemental",
			"MoundOfMaggots",
			"TerathanWarrior",
			"AirElemental",
			"FireElemental",
			"OrcishMage",
			"RatmanMage",
			"Troll"
		};

		public static String[] DesertLevel3 = new String[] {
			"DreadSpider",
			"Lich",
			"BoneKnight",
			"SkeletalKnight",
			"AgapiteElemental",
			"BronzeElemental",
			"CopperElemental",
			"DullCopperElemental",
			"GoldenElemental",
			"ShadowIronElemental",
			"ValoriteElemental",
			"VeriteElemental",
			"OphidianMage",
			"AntLion",
			"BlackSolenQueen",
			"RedSolenQueen",
			"PlagueSpawn",
			"OphidianWarrior",
			"SilverSerpent",
			"BlackSolenInfiltratorQueen",
			"RedSolenInfiltratorQueen",
			"AncientLich",
			"Cyclops",
			"Mummy",
			"RottingCorpse",
			"OphidianArchmage",
			"LavaSerpent",
			"Efreet",
			"Executioner"
		};

		public static String[] DesertLevel4 = new String[] {
			"TerathanMatriarch",
			"FireGargoyle",
			"IceFiend",
			"PlagueBeast",
			"OphidianKnight",
			"PoisonElemental",
			"Betrayer",
			"LichLord",
			"Titan",
			"OphidianMatriarch",
			"TerathanAvenger",
			"OrcBomber",
			"OrcishLord"
		};

		public static String[] DesertLevel5 = new String[] {
			"Phoenix",
			"OgreLord",
			"OrcBrute"
		};


		public static String[] DungeonLevel1 = new String[] {
			"Beetle",
			"FireSteed",
			"HellSteed",
			"Nightmare",
			"ScaledSwampDragon",
			"SkeletalMount",
			"SwampDragon",
			"SewerRat",
			"Rat",
			"Mongbat",
			"StrongMongbat",
			"Snake",
			"HordeMinion",
			"RestlessSoul",
			"TerathanDrone",
			"HeadlessOne",
			"Slime",
			"GiantRat",
			"Zombie"
		};

		public static String[] DungeonLevel2 = new String[] {
			"Skeleton",
			"HellCat",
			"BlackSolenWorker",
			"RedSolenWorker",
			"PatchworkSkeleton",
			"VampireBat",
			"GiantBlackWidow",
			"GiantSpider",
			"ChaosDaemon",
			"GazerLarva",
			"Ghoul",
			"Guardian",
			"Orc",
			"OrcCaptain",
			"Ratman",
			"Bogling",
			"Quagmire",
			"SwampTentacle",
			"Harpy",
			"Lizardman",
			"PredatorHellCat",
			"LavaLizard",
			"Gibberling",
			"GoreFiend",
			"Impaler",
			"WailingBanshee",
			"EarthElemental",
			"Bogle",
			"BoneMagi",
			"EvilMage",
			"EvilMageLord",
			"Gazer",
			"Imp",
			"Shade",
			"SkeletalMage",
			"Spectre",
			"Wraith",
			"Ettin",
			"Ogre",
			"RatmanArcher",
			"HellHound",
			"BladeSpirits",
			"Corpser",
			"SerpentineDragon",
			"GiantSerpent",
			"BlackSolenInfiltratorWarrior",
			"BlackSolenWarrior",
			"RedSolenInfiltratorWarrior",
			"RedSolenWarrior",
			"CrystalElemental",
			"FleshGolem",
			"MoundOfMaggots",
			"TerathanWarrior",
			"AirElemental",
			"FireElemental",
			"WaterElemental",
			"Gargoyle",
			"OrcishMage",
			"RatmanMage",
			"OrcBomber",
			"OrcishLord",
			"Troll",
			"EnergyVortex",
			"Wyvern"
		};

		public static String[] DungeonLevel3 = new String[] {
			"Ravager",
			"WandererOfTheVoid",
			"DreadSpider",
			"Lich",
			"BoneKnight",
			"SkeletalKnight",
			"StoneGargoyle",
			"AgapiteElemental",
			"BronzeElemental",
			"CopperElemental",
			"DullCopperElemental",
			"GoldenElemental",
			"ShadowIronElemental",
			"ValoriteElemental",
			"VeriteElemental",
			"Reaper",
			"OphidianMage",
			"AntLion",
			"BlackSolenQueen",
			"RedSolenQueen",
			"ShadowKnight",
			"SpectralArmour",
			"Centaur",
			"PlagueSpawn",
			"WhippingVine",
			"OphidianWarrior",
			"StoneHarpy",
			"SilverSerpent",
			"BlackSolenInfiltratorQueen",
			"RedSolenInfiltratorQueen",
			"DarknightCreeper",
			"AncientLich",
			"ElderGazer",
			"Cyclops",
			"Mummy",
			"RottingCorpse",
			"OphidianArchmage",
			"LavaSerpent",
			"Efreet",
			"ToxicElemental",
			"Drake"
		};

		public static String[] DungeonLevel4 = new String[] {
			"AbysmalHorror",
			"FleshRenderer",
			"TerathanMatriarch",
			"FireGargoyle",
			"PlagueBeast",
			"OphidianKnight",
			"DemonKnight",
			"PoisonElemental",
			"Daemon",
			"LichLord",
			"Titan",
			"OphidianMatriarch",
			"TerathanAvenger",
			"BloodElemental",
			"Succubus"
		};

		public static String[] DungeonLevel5 = new String[] {
			"Phoenix",
			"WhiteWyrm",
			"OgreLord",
			"OrcBrute",
			"BogThing",
			"Dragon",
			"BoneDemon",
			"Devourer",
			"Balron",
			"AncientWyrm",
			"ShadowWyrm",
			"SkeletalDragon"
		};
	}

	public class AbilityCoolDown
	{
        public static TimeSpan FuriousAssault                   = new TimeSpan( 0, 0, 5);
		public static TimeSpan Disarm							= new TimeSpan( 0, 2, 0);
		public static TimeSpan BerserkerRage					= new TimeSpan( 0, 5, 0);
		public static TimeSpan Critical							= new TimeSpan( 0, 0, 30);
		public static TimeSpan ArcaneTransfer					= new TimeSpan( 0, 5, 0);
		public static TimeSpan Bite								= new TimeSpan( 0, 2, 0);
		public static TimeSpan CriticalStrike					= new TimeSpan( 0, 0, 30);
		public static TimeSpan KaiShot							= new TimeSpan( 0, 2, 0);
		public static TimeSpan Bluudlust						= new TimeSpan( 0, 5, 0);
		public static TimeSpan CripplingBlow					= new TimeSpan( 0, 2, 0);
		public static TimeSpan GlobeOfDarkness					= new TimeSpan( 0, 5, 0);
		public static TimeSpan LayOnHands						= new TimeSpan( 0, 5, 0);
		public static TimeSpan Foraging							= new TimeSpan( 0, 2, 0);
		public static TimeSpan ShieldBash						= new TimeSpan( 0, 0, 30);
		public static TimeSpan Backstab							= new TimeSpan( 0, 0, 30);
		public static TimeSpan CalledShot						= new TimeSpan( 0, 2, 0);
		public static TimeSpan ChargedMissile					= new TimeSpan( 0, 2, 0);
		public static TimeSpan DragonRoar						= new TimeSpan( 0, 5, 0);
		public static TimeSpan BarbarianInstinct				= new TimeSpan( 0, 2, 0);
		public static TimeSpan AnimalCompanion					= new TimeSpan( 0, 10, 0);
		public static TimeSpan PowerLunge						= new TimeSpan( 0, 0, 30);
        public static TimeSpan ImprovedFamiliar                 = new TimeSpan(0, 0, 5);
        public static TimeSpan Flurry                           = new TimeSpan(0, 0, 30);
        public static TimeSpan EnchantingMelody                 = new TimeSpan(0, 5, 0);
        public static TimeSpan Pounce                           = new TimeSpan(0, 1, 30);
        public static TimeSpan DarkRebirth                      = new TimeSpan(0, 20, 0);
        public static TimeSpan Feast                            = new TimeSpan(0, 0, 20);
        public static TimeSpan SinisterForm                     = new TimeSpan(0, 0, 15);
	    public static TimeSpan LegIt                        	= new TimeSpan(0, 0, 10);
        public static TimeSpan AtWill                           = new TimeSpan(0, 0, 30);
        public static TimeSpan Encounter                        = new TimeSpan(0, 2, 0);
        public static TimeSpan Daily                            = new TimeSpan(0, 30, 0);
	}

	public class Colors
	{
		public const int BlackOakColor							= 0x455;
		public const int YewColor								= 0x715;
		public const int SilverBirchColor							= 0x8B5;
		public const int AshwoodColor							= 0x9F9;
		public const int WhitePineColor							= 0xB9B;
		public const int RedwoodColor							= 0x750;
		public const int PineColor								= 0xFA;
		public const int ElvenGreen								= 0x9F2;
		public const int FeatMessageColor						= 0xADB;
	}

	public class Spells
	{
		public const ulong	AgilitySpell							=	0x0000000000000001;
		public const ulong	AirElementalSpell					=	0x0000000000000002;
		public const ulong	ArchCureSpell						=	0x0000000000000004;
		public const ulong	ArchProtectionSpell					= 	0x0000000000000008;
		public const ulong	BlessSpell							=	0x0000000000000010;
		public const ulong	ChainLightningSpell					=	0x0000000000000020;
		public const ulong	ClumsySpell						=	0x0000000000000040;
		public const ulong	CreateFoodSpell						=	0x0000000000000080;
		public const ulong	CunningSpell						=	0x0000000000000100;
		public const ulong	CureSpell							=	0x0000000000000200;
		public const ulong	CurseSpell							=	0x0000000000000400;
		public const ulong	DispelFieldSpell						=	0x0000000000000800;
		public const ulong	DispelSpell							=	0x0000000000001000;
		public const ulong	EarthElementalSpell					=	0x0000000000002000;
		public const ulong	EarthquakeSpell						=	0x0000000000004000;
		public const ulong	EnergyBoltSpell						=	0x0000000000008000;
		public const ulong	EnergyFieldSpell						=	0x0000000000010000;
		public const ulong	EnergyVortexSpell					=	0x0000000000020000;
		public const ulong	ExplosionSpell						=	0x0000000000040000;
		public const ulong	FeeblemindSpell						=	0x0000000000080000;
		public const ulong	FireballSpell						=	0x0000000000100000;
		public const ulong	FireElementalSpell					=	0x0000000000200000;
		public const ulong	FireFieldSpell						=	0x0000000000400000;
		public const ulong	FlamestrikeSpell						=	0x0000000000800000;
		public const ulong	GateTravelSpell						=	0x0000000001000000;
		public const ulong	GreaterHealSpell					=	0x0000000002000000;
		public const ulong	HarmSpell							=	0x0000000004000000;
		public const ulong	HealSpell							=	0x0000000008000000;
		public const ulong	IncognitoSpell						=	0x0000000010000000;
		public const ulong	InvisibilitySpell						=	0x0000000020000000;
		public const ulong	LightningSpell						=	0x0000000040000000;
		public const ulong	MagicArrowSpell						=	0x0000000080000000;
		public const ulong	MagicLockSpell						=	0x0000000100000000;
		public const ulong	MagicReflectSpell					=	0x0000000200000000;
		public const ulong	MagicTrapSpell						=	0x0000000400000000;
		public const ulong	ManaDrainSpell						=	0x0000000800000000;
		public const ulong	ManaVampireSpell					=	0x0000001000000000;
		public const ulong	MarkSpell							=	0x0000002000000000;
		public const ulong	MassCurseSpell						=	0x0000004000000000;
		public const ulong	MassDispelSpell						=	0x0000008000000000;
		public const ulong	MeteorSwarmSpell					=	0x0000010000000000;
		public const ulong	MindBlastSpell						=	0x0000020000000000;
		public const ulong	NightSightSpell						=	0x0000040000000000;
		public const ulong	ParalyzeFieldSpell					=	0x0000080000000000;
		public const ulong	ParalyzeSpell						=	0x0000100000000000;
		public const ulong	PoisonFieldSpell						=	0x0000200000000000;
		public const ulong	PoisonSpell						=	0x0000400000000000;
		public const ulong	PolymorphSpell						=	0x0000800000000000;
		public const ulong	ProtectionSpell						=	0x0001000000000000;
		public const ulong	ReactiveArmorSpell					=	0x0002000000000000;
		public const ulong	RecallSpell							=	0x0004000000000000;
		public const ulong	RemoveTrapSpell					=	0x0008000000000000;
		public const ulong	ResurrectionSpell					=	0x0010000000000000;
		public const ulong	RevealSpell						=	0x0020000000000000;
		public const ulong	SpiritSpeakSpell						=	0x0040000000000000;
		public const ulong	StrengthSpell						=	0x0080000000000000;
		public const ulong	SummonCreatureSpell				= 	0x0100000000000000;
		public const ulong	SummonDaemonSpell					=	0x0200000000000000;
		public const ulong	TelekinesisSpell						=	0x0400000000000000;
		public const ulong	TeleportSpell						=	0x0800000000000000;
		public const ulong	UnlockSpell						=	0x1000000000000000;
		public const ulong	WallOfStoneSpell					=	0x2000000000000000;
		public const ulong	WaterElementalSpell					=	0x4000000000000000;
		public const ulong	WeakenSpell						=	0x8000000000000000;
	}

	public class Mobiles
	{
		public const int Man									= 400;
		public const int Woman								= 401;

		public const int GhostMan								= 402;
		public const int GhostWoman							= 403;

		public const int SavageMan								= 183;
		public const int SavageFemale							= 184;

		public const int Ridgeback								= 187;

		public const int AntLion								= 787;

		public const int SmallBlackWidow							= 157;
		public const int LargeBlackWidow							= 173;
		public const int DreadSpider							= 11;
		public const int FrostSpider								= 20;
		public const int GiantSpider								= 28;

		public const int Bogling								= 779;
		public const int BogThing								= 780;

		public const int BrownBull								= 232;
		public const int SpottedBull								= 233;
		public const int BrownCow								= 216;
		public const int SpottedCow								= 231;

		public const int BlackBear								= 211;
		public const int BrownBear								= 167;
		public const int GrizzlyBear								= 212;
		public const int PolarBear								= 213;

		public const int Dog									= 217;
		public const int HellHound								= 97;
		public const int TimberWolf								= 225;
		public const int DarkWolf								= 99;
		public const int DireWolf								= 23;
		public const int GreyWolf								= 25;
		public const int SilverWolf								= 100;
		public const int WhiteWolf								= 34;

		public const int Centaur								= 101;

		public const int ChaosDemon							= 792;

		public const int Chariot								= 786;

		public const int Cyclops								= 75;

		public const int Daemon								= 9;
		public const int BlackGateDaemon						= 38;
		public const int ElderDaemon							= 40;
		public const int ExodusDaemon							= 102;
		public const int IceFiend								= 43;

		public const int Doe									= 237;
		public const int Stag									= 234;

		public const int Dolphin								= 151;

		public const int AsianDragon							= 103;
		public const int GreyDragon								= 12;
		public const int GreyDrake								= 60;
		public const int HugeRedDragon							= 172;
		public const int RedDragon								= 59;
		public const int RedDrake								= 61;
		public const int WhiteWyrm								= 49;
		public const int AncientWyrm							= 105;
		public const int RustDragon								= 46;
		public const int ShadowWyrm							= 106;
		public const int SkeletalDragon							= 104;
		public const int Wyvern								= 62;

		public const int EarthElemental							= 14;
		public const int AgapiteElemental							= 107;
		public const int BronzeElemental							= 108;
		public const int CopperElemental							= 109;
		public const int DullCopperElemental						= 110;
		public const int GoldElemental							= 166;
		public const int IronElemental							= 111;
		public const int ValoriteElemental							= 112;
		public const int VeriteElemental							= 113;
		public const int AcidElemental							= 158;
		public const int AirElemental							= 13;
		public const int BloodElemental							= 159;
		public const int FireElemental							= 15;
		public const int IceElemental							= 161;
		public const int PoisonElemental							= 162;
		public const int SnowElemental							= 163;
		public const int WaterElemental							= 16;

		public const int EnergyVortex							= 164;

		public const int ShadowWisp							= 165;
		public const int Wisp									= 58;

		public const int BrownHorse								= 200;
		public const int FireSteed								= 190;
		public const int BrownPackHorse							= 291;
		public const int GreyHorse								= 226;
		public const int DarkBrownHorse							= 204;
		public const int TanHorse								= 228;
		public const int DarkSteed								= 114;
		public const int Nightmare								= 116;
		public const int SilverSteed								= 117;
		public const int BrittanianWarHorse						= 118;
		public const int MageCouncilWarHorse						= 119;
		public const int MinaxWarHorse							= 120;
		public const int ShadowlordWarHorse						= 121;
		public const int Unicorn								= 122;

		public const int SmallEtherealWarrior						= 123;
		public const int HugeEtherealWarrior						= 175;

		public const int Ettin									= 2;

		public const int Cougar								= 63;
		public const int SnowLeopard							= 64;
		public const int Panther								= 214;
		public const int PredatorHellcat							= 127;

		public const int SmallFey								= 128;
		public const int HugeFey								= 176;

		public const int FireAntWorker							= 781;
		public const int FireAntWarrior							= 782;
		public const int FireAntQueen							= 783;
		public const int FireAntMatriarch							= 804;
		public const int BlackAntWorker							= 805;
		public const int BlackAntWarrior							= 806;
		public const int BlackAntQueen							= 807;
		public const int BlackAntMatriarch						= 808;

		public const int Corpser								= 8;
		public const int Reaper								= 47;
		public const int SwampTentacles							= 66;

		public const int GiantFrog								= 80;

		public const int Gargoyle								= 4;
		public const int FireGargoyle							= 130;
		public const int StoneGargoyle							= 67;
		public const int SlaveGargoyle							= 753;
		public const int EnforcerGargoyle							= 754;
		public const int GuardGargoyle							= 755;
		public const int ShopkeeperGargoyle						= 758;

		public const int Gazer									= 22;
		public const int ElderGazer								= 68;

		public const int Efreet									= 131;

		public const int GiantBeetle								= 791;

		public const int Golem									= 752;

		public const int Gorilla									= 29;

		public const int Harpy									= 30;
		public const int StoneHarpy								= 73;

		public const int Headless								= 31;

		public const int SmallHordeDemon						= 776;
		public const int LargeHordeDemon						= 795;
		public const int HugeHordeDemon						= 796;

		public const int Imp									= 74;

		public const int Kirin									= 132;

		public const int Kraken								= 77;

		public const int Lich									= 24;
		public const int LichLord								= 78;

		public const int Lizardman								= 33;

		public const int Alligator								= 202;
		public const int SmallAlligator							= 133;
		public const int KomodoDragon							= 134;
		public const int LavaLizard								= 206;

		public const int Llama									= 220;
		public const int PackLlama								= 292;

		public const int EvilMage								= 124;
		public const int EvilMageMaster							= 125;

		public const int HugeFireDragon							= 797;
		public const int HugeRustDragon							= 798;

		public const int Mongbat								= 39;

		public const int Ogre									= 1;
		public const int OgreLord								= 83;
		public const int ArcticOgreLord							= 135;

		public const int ArchMageOphidian						= 136;
		public const int KnightOphidian							= 137;
		public const int MageOphidian							= 85;
		public const int QueenOphidian							= 87;
		public const int WarriorOphidian							= 86;

		public const int Orc									= 17;
		public const int OrcCaptain								= 7;
		public const int OrcLord								= 138;
		public const int OrcShaman								= 140;
		public const int OrcScout								= 181;
		public const int OrcBomber								= 182;
		public const int HugeOrcLord							= 189;
	}

	public class Items
	{
		public const int StarSapphire							= 0xF21;
	}

	public class Messages
	{
		public const string TooSoon								= "It's too soon to do this again.";
		public const string NoAbility								= "You can't do this!";
		public const string NoEquip								= "You can't equip items in this form!";
		public const string CantAdvanceSkill						= "You can't advance this skill any further.";
		public const string NoSkillPoints							= "You don't have enough skill points.";
		public const string NoReqs								= "You don't meet the requirements for this Feat.";
		public const string NoFeatSlots							= "You don't have enough Feat slots open.";
		public const string AddFeat								= "You have gained the Feat.";
		public const string AddFeatError							= "Error adding Feat! Post a bug report.";
		public const string NoTargetSelf							= "You can't target yourself.";
		public const string NoTarget								= "You can't target that.";
	}

	public class CreatureAbilities
	{
		public const ulong CanSnare							= 0x0000000000000001;
		public const ulong CanDevour							= 0x0000000000000002;
		public const ulong CanEntangle							= 0x0000000000000004;
		public const ulong CanRockThrow						= 0x0000000000000008;
		public const ulong CanSpitPoison							= 0x0000000000000010;
		public const ulong CanJump								= 0x0000000000000020;
	}
}
