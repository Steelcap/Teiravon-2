using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using Server;
using Server.Network;
using Server.Spells;
using Server.Items;
using Server.Teiravon;
using Server.Gumps;
using Server.Regions;
using Server.Mobiles;
using Server.Spells.Necromancy;
using Server.Misc;
using Server.Teiravon.War;
using Server.Accounting;
using Teiravon.Magecraft;
//using Teiravon.Encounters;
using Server.Engines.PartySystem;
using Server.Engines.XmlSpawner2;
using Server.Scripts.Commands;
using Knives.Utils;

//using Server.Spells.Runecasting;

namespace Server.Mobiles
{
	public class TeiravonMobile : PlayerMobile
	{
        private Item m_LastCrafted;
        private bool m_NeedPolish = false;
        private DateTime m_LastCraftTime = DateTime.MinValue;

        public bool NeedPolish { get { return m_NeedPolish; } set { m_NeedPolish = value; } }
        public Item LastCrafted { get { return m_LastCrafted; } set { m_LastCrafted = value; } }
        public DateTime LastCraftTime { get { return m_LastCraftTime; } set { m_LastCraftTime = value; } }

        private static TimeSpan Nod_Duration = TimeSpan.FromDays(3);
        public static Hashtable DiedOnce = new Hashtable();
		public static ArrayList HearAllEntries = new ArrayList();
		public static ArrayList HearAllPartyChat = new ArrayList();
        public static ArrayList HearAllNods = new ArrayList();

		//public DateTime[] AntiMacroCheckTime = new DateTime[52];
		//public int[] AntiMacroCheckCount = new int[52];
		private bool m_HearAll = false;
		public bool HearAll { get { return m_HearAll; } set { m_HearAll = value; if ( HearAll ) HearAllEntries.Add( this ); else HearAllEntries.Remove( this ); } }

        private bool m_HearNods = false;
        public bool HearNods { get { return m_HearNods; } set { m_HearNods = value; if (HearNods) HearAllNods.Add(this); else HearAllNods.Remove(this); } }

		private bool m_CanParty = true;
        public override bool ShowFameTitle { get { return !Shapeshifted && Fame >= 10000; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanParty { get { return m_CanParty; } set { m_CanParty = value; } }

		#region Enums
        public enum Deity
        {
            None,

            Adalia = 1,
            Talathas = 2,
            Kamalini = 3,
            Kinarugi = 4,
            Narindun = 5,
            Lloth = 6,
            Gruumsh = 7,
            Saerin = 8,
            Valar = 9,
            Cultist =10,
            Jareth = 11,
            Occido = 12
        }

		public enum Race
		{
			None,

			// Standard Races
			Human = 1,
			Elf = 2,
			Drow = 3,
			Dwarf = 4,
			Duergar = 5,
			Orc = 6,

			// Advanced Races
			Kresh,
			Undead,
			Vampire,
			Giant,
			HalfElf,
			Kender,
			Gnome,
			Goblin,
			Teifling,
            Frostling,
            HalfOrc
		}

		public enum Class
		{
			None,

			// Standard Classes
			// Fighters
			Fighter,
			Kensai,
			Cavalier,

			// Clerics
			Cleric,
			DarkCleric,

			// Arcane
			Necromancer,
			Aeromancer,
			Aquamancer,
			Geomancer,
			Pyromancer,

			// Paladins
			Paladin,
			UndeadHunter,

			// Rangers
			Ranger,
			Archer,

			// Druids
			Forester,
			Shapeshifter,

			// Barbarians
			Berserker,
			Dragoon,

			// Rouges
			Thief,
			Assassin,
			MageSlayer,

			// Crafters
			Blacksmith,
			Tailor,
			Woodworker,
			Bowyer,
			Alchemist,
			Tinker,
			Cook,

			// Advanced Classes
			Bard,
			Bladesinger,
			Savage,
			Monk,
			DrunkenMaster,
			Pirate,
			Scoundrel,
			DwarvenDefender,
			Knight,
			Templar,
			Sage,
			Marksman,
			DeathKnight,
            Ravager,
			MasterThief,
			Shadowdancer,
            Strider,
            Merchant
		}

		public enum Alignment
		{
			None,

			LawfulGood,
			NeutralGood,
			ChaoticGood,
			LawfulNeutral,
			TrueNeutral,
			ChaoticNeutral,
			LawfulEvil,
			NeutralEvil,
			ChaoticEvil
		}

		public enum Feats
		{
			None,

			WeaponSpecialization,
			WealthyLineage,
			Apprenticeship,
			WarfareTraining,
			Leadership,
			MagicResistance,
			Disguise,
			Blademaster,
			GlobeOfDarkness,
			CloakOfDarkness,
			CripplingBlow,
			ResistPoison,
			ExpertMining,
			ResistPsionics,
			Bluudlust,
			CriticalStrike,
			BuumBuum,
			Tuffness,
			TacticalAssessment,
			WarMount,
			AdvancedHealing,
			HolyAura,
			WeaponMastery,
			WeaponMasteryII,
			ShieldBash,
			ArmorProficiency,
			KaiShot,
			AcrobaticCombat,
			ExoticWeapons,
			ResistPoisons,
			MountedCombat,
			DetectEvil,
			LayOnHands,
			TurnUndead,
			ResistCurses,
			DivineAura,
			ElementalResistance,
			DragonRoar,
			BerserkerRage,
			BarbarianInstinct,
			PowerLunge,
			ChargedStrike,
			AlchemyScience,
			BestowBlessing,
			BestowDarkBlessing,
			DivineMight,
			UnholyMight,
			NetherAura,
			DivineHealing,
			RebukeUndead,
			UnholyAura,
			DarkAura,
			BestowCurse,
			UnholyRestoration,
			RacialEnemy,
			ExtraRacialEnemy,
			Orientation,
			AnimalCompanion,
			FlushCreatures,
			AdvancedTracking,
			ExtraFavoredEnemy,
			AnimalHusbandry,
			BeastLore,
			WildernessLore,
			WildShape,
			Bite,
			Forensics,
			CalledShot,
			ChargedMissile,
			Marksmanship,
			BowSpecialization,
			TrapLore,
			Backstab,
			UseMagicDevice,
			NimbleFingers,
			Apothecary,
			ExoticPoisons,
			AdvancedStealth,
			ContractKilling,
			DispelMagic,
			ImprovedFamiliar,
			ArcaneEnchantment,
			Inscription,
			Bookworm,
			ArcaneTransfer,
			RigorousTraining,
			Revenant,
			ImbuePoison,
			CreateInfusion,
			NaturesEnchantment,
			ResistEnergy,
			AnimalLore,
			Telepathy,
			PhysicalResistance,
			MindBlast,
			Disarm,
			Locksmithing,
			SummonUndead,
			NecroSpellbook,
			UndeadBond,
			AdvancedDying,
			LeatherDying,
			FurnitureStaining,
			ArmorEnameling,
			Research,
			WeaponMaster,
			AxeFighter,
			RacialCrafting,
			SkilledGatherer,
			MeditativeConcentration,
            Flurry,
            MindBodySoul,
            WholenessOfSelf,
            KiStrike,
            BodyOfIron,
            LeapOfClouds,
            EnchantingMelody,
            CuttingWords,
            JackOfAll,
            StillMind,
            Pounce,
            Riposte,
            EnchantedQuiver,
            Espionage,
            Challenge,
            HealersOath,
            GreivousWounds,
            FeastoftheDamned,
            BodyoftheGrave,
            SinisterForm,
            DarkRebirth,
            StickyFingas,
            LegIt,
            CharmedLife,
            ShoddyCrafts,
            Meticulous,
            Banish,
            DisruptingPresence,
            WyrdSense,
            NaturesDefender,
            TempestsWrath,
            ShieldMastery,
            NorthernResilience,
            FrostlingHibernation,
            ChilblainTouch,
            DefensiveStance,
            UncannyDefense,
            Dodge,
            Scavenger,
            FuriousAssault,
            Camouflage,
            StalkingPrey,
            BigGameHunter,
            BlacksmithTraining,
            CarpenterTraining,
            TinkerTraining,
            TailorTraining,
            FletcherTraining,
            CookTraining,
            CombatTraining,
            MasterCraftsman,
            BattleHardened,
            RottenLuck,
            DirtyTricks,
            CunningFlourish,
            UnderworldConnections
		}

		public enum RacialEnemies
		{
			None,
			Animals,
			Serpents,
			Spiders,
			Dragons,
			Giants,
			Elementals,
			Undead,
			Daemons,
			Orcs
		}

		private string m_RacialEnemy = null;
		private string m_ExtraRacialEnemy = null;

		private RacialEnemies m_RaceEnemy;
		private RacialEnemies m_ExtraRaceEnemy;
        
        [CommandProperty(AccessLevel.GameMaster)]
		public RacialEnemies RacialEnemy { get { return m_RaceEnemy; } set { m_RaceEnemy = value; } }
        
        [CommandProperty(AccessLevel.GameMaster)]
		public RacialEnemies ExtraRacialEnemy { get { return m_ExtraRaceEnemy; } set { m_ExtraRaceEnemy = value; } }

		public enum MasterWeapons
		{
			None,
			Broadsword,
			Longsword,
			Cutlass,
			Scimitar,
			VikingSword,
			Bardiche,
			Halberd,
			BattleAxe,
			LargeBattleAxe,
			ExecutionersAxe,
			WarAxe,
			Kryss,
			Dagger,
			Spear,
			ShortSpear,
			Warfork,
			Mace,
			Maul,
			WarMace,
			WarHammer,
			HammerPick,
			QuarterStaff,
            
            Axe = 100,
            Slashing,
            Staff,
            Bashing,
            Piercing,
            Polearm,
            Ranged,
            Thrown

		}


		private MasterWeapons m_MasterWep;
        
        [CommandProperty(AccessLevel.GameMaster)]
		public MasterWeapons MasterWeapon { get { return m_MasterWep; } set { m_MasterWep = value; } }

		public enum EncounterModes
		{
			AlwaysEncounter,
			Prompt,
			AlwaysAvoid
		}

		public enum PyromancerSpells
		{
			BurningHands = 0x1,
			ContinualFlame = 0x2,
			FlamingBlade = 0x4,

			Fireball = 0x8,
			SearingLight = 0x10,
			FireTrap = 0x20,

			WallofFire = 0x40,
			FlameStrike = 0x80,
			Sunbeam = 0x100,

			FireStorm = 0x200,
			Sunburst = 0x400,
			MeteorSwarm = 0x800
		}
		#endregion

		#region Variables
        //private DateTime m_LastDeath;
        private int m_HitsMod;
        private Deity m_Diety;
        private int m_Feast;
		private MessageType m_SpeechType = MessageType.Regular;
		private DateTime m_NextFletcherBulkOrder;
		private DateTime m_NextEXPLoss;
		private Race m_Race;
		private Class m_Class;
		private Alignment m_Alignment;
		private Feats[] m_Feats = new Feats[15];
		public Feats[] m_ActiveFeats = new Feats[15];
		private PotionEffect[] m_PotionEffect = new PotionEffect[10];
		private WarStone m_WarStone = null;
		private string m_ApprenticeSkill = "";
		private ulong m_PlayerSpells = 0;
		private int m_PyromancerSpells = 0;
		private bool m_Magic = false;
		private bool m_GlobeOfDarkness = false;
        private bool m_DisarmShotReady = false; //using to show light aura active
		private bool m_StunShotReady = false;  //using to show dark aura active
		private bool m_PoisonShotReady = false; //using to show wearing drow equipment
		private bool m_ShadowShotReady = false; //using to show current target of mgspb
		private bool m_CripplingShotReady = false;//using to show ambush active
		private bool m_FatalShotReady = false;  //using to show active called shot
		private bool m_ChargedMissileReady = false;
        private bool m_Ambush = false;
        private bool m_Feinted = false;
		private bool m_ShowStatus = true;
		private bool m_CelestialSkill = false;
		private bool m_BurningHands = false;  //using to show active flurry
        private bool m_Obscured;
		private int m_OBody = 0;
		private int m_Level = 1;
		private int m_Exp = 0;
		private int m_LanguagesKnown = 1;
		private int m_CurrentLanguage = 1;
		private int m_LanguageFont = 3;
		private int m_LanguageCase = 0;
		private int m_MaxHits = 0;
		private int m_MaxStam = 0;
		private int m_MaxMana = 0;
		private int m_FeatsRemaining = 0;
		private int m_WarMountDeaths = 0;
		private double m_ElvenSkill = 0;
		private double m_DrowSkill = 0;
		private double m_LupineSkill = 0;
		private double m_OrcSkill = 0;
		private double m_DwarvenSkill = 0;
		private double m_CommonSkill = 0;
		private double m_HumanReputation = 0.0;
		private double m_ElfReputation = 0.0;
		private double m_OrcReputation = 0.0;
		private double m_DwarfReputation = 0.0;
		private double m_DrowReputation = 0.0;
		private double m_DragonReputation = 0.0;
		private double m_AnimalReputation = 0.0;
		private double m_DuergarReputation = 0.0;

		public const int LCommon = 0x01;
		public const int LElven = 0x02;
		public const int LDrow = 0x04;
		public const int LOrc = 0x08;
		public const int LLupine = 0x10;
		public const int LDwarven = 0x20;
		public const int LCelestial = 0x40;

		public bool m_CloakOfDarkness = false;
        public DateTime m_Revealed = DateTime.Now;

		//Shapeshifting
		private int m_DruidForm = 0;
        private int m_DruidFormGroup = -1;
		private DateTime m_ShapeshiftSlotDelete;
		private DateTime m_ShapeshiftNext;
		private string[] m_ShapeshiftSlotName = new string[10];
		private int[] m_ShapeshiftSlot = new int[10];
		private int[] m_ShapeshiftSlotType = new int[10];
		private int[] m_ShapeshiftSlotHue = new int[10];
		private int m_ShapeshiftHue;
		private bool m_ShapeshiftSpecial;
		private bool m_ShapeshiftTransformed;
        private TAVShiftUtilities.ShiftInfo m_ShiftInfo;

		//Perks
		private int m_perkpoints = 1;
		private int m_ridingskill = 0;
		private int m_farmingskill = 0;
		private int m_teachingskill = 0;
		private DateTime m_nextteach = DateTime.Now;
		private DateTime m_nextlearn = DateTime.Now;
		private int m_farmcrops = 0;
		private int m_tavteachskill = 999;
		private EncounterModes m_EncounterMode = EncounterModes.AlwaysAvoid;

        //Nods
        private bool m_CantNod;
        private Hashtable Nods = new Hashtable();
        private DateTime Last_Nod_Paid = DateTime.Now;
        private int m_FromNods = 0;



        [CommandProperty(AccessLevel.GameMaster)]
        public bool CantNod
        {
            get { return m_CantNod; }
            set { m_CantNod = value; }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int ExpFromNods
        {
            get { return m_FromNods; }
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public EncounterModes EncounterMode
		{
			get { return m_EncounterMode; }
			set { m_EncounterMode = value; }
		}

		//Misc
		#endregion

		#region Properties
		public override bool ClickTitle { get { return false; } }

		private DateTime m_NextHeal = DateTime.Now;

        private int m_ExpertMining;

        [CommandProperty(AccessLevel.GameMaster)]
        public string GuildName { get { return Guild != null ? Guild.Name : "none"; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ExpertMining { get { return m_ExpertMining; } set { m_ExpertMining = value; } }

        private int m_ExpertSkinning;

        [CommandProperty(AccessLevel.GameMaster)]
        public int ExpertSkinning { get { return m_ExpertSkinning; } set { m_ExpertSkinning = value; } }

        private int m_ExpertWoodsman;

        [CommandProperty(AccessLevel.GameMaster)]
        public int ExpertWoodsman { get { return m_ExpertWoodsman; } set { m_ExpertWoodsman = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HitsMod { get { return m_HitsMod; } set { m_HitsMod = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Feast { get { return m_Feast; } set { m_Feast = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Deity Faith { get { return m_Diety; } set { m_Diety = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AdvancedStealth { get { return HasFeat(Feats.AdvancedStealth); } }

		public DateTime NextEXPLoss { get { return m_NextEXPLoss; } set { m_NextEXPLoss = value; } }
		public DateTime NextHeal { get { return m_NextHeal; } set { m_NextHeal = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int OBody { get { return m_OBody; } set { m_OBody = value; this.BodyMod = m_OBody; } }
        
		public int DruidForm { get { return m_DruidForm; } set { m_DruidForm = value; } }
        public int DruidFormGroup { get { return m_DruidFormGroup; } set { m_DruidFormGroup = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PerkPoints { get { return m_perkpoints; } set { m_perkpoints = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int RidingSkill { get { return m_ridingskill; } set { m_ridingskill = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int FarmingSkill { get { return m_farmingskill; } set { m_farmingskill = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int TeachingSkill { get { return m_teachingskill; } set { m_teachingskill = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextTeach { get { return m_nextteach; } set { m_nextteach = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextLearn { get { return m_nextlearn; } set { m_nextlearn = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int FarmCrops { get { return m_farmcrops; } set { m_farmcrops = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int TeachSkill { get { return m_tavteachskill; } set { m_tavteachskill = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Obscured { get { return m_Obscured; } set { m_Obscured = value; } }

		public PotionEffect[] PotionEffect { get { return m_PotionEffect; } set { m_PotionEffect = value; } }
		public bool GlobeOfDarkness { get { return m_GlobeOfDarkness; } set { m_GlobeOfDarkness = value; } }
		public bool ChargedMissileReady { get { return m_FatalShotReady; } set { m_FatalShotReady = value; } }
		public bool ShowPermaDeathGump = false;
		public bool BurningHands { get { return m_BurningHands; } set { m_BurningHands = value; } }

		public string ApprenticeSkill { get { return m_ApprenticeSkill; } set { m_ApprenticeSkill = value; } }

		// Called Shot
		public bool DisarmShotReady { get { return m_DisarmShotReady; } set { m_DisarmShotReady = value; } }
		public bool StunShotReady { get { return m_StunShotReady; } set { m_StunShotReady = value; } }
		public bool PoisonShotReady { get { return m_PoisonShotReady; } set { m_PoisonShotReady = value; } }
		public bool ShadowShotReady { get { return m_ShadowShotReady; } set { m_ShadowShotReady = value; } }
		public bool CripplingShotReady { get { return m_CripplingShotReady; } set { m_CripplingShotReady = value; } }
		public bool FatalShotReady { get { return m_FatalShotReady; } set { m_FatalShotReady = value; } }

        // Ambush
        public bool Ambush { get { return m_Ambush; } set { m_Ambush = value; } }
        public bool Feinted { get { return m_Feinted; } set { m_Feinted = value; } }

		// Languages
		public int Languages { get { return m_LanguagesKnown; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int CurrentLanguage { get { return m_CurrentLanguage; } set { m_CurrentLanguage = value; } }

		public double ElvenSkill { get { return m_ElvenSkill; } set { m_ElvenSkill = value; } }
		public double DrowSkill { get { return m_DrowSkill; } set { m_DrowSkill = value; } }
		public double LupineSkill { get { return m_LupineSkill; } set { m_LupineSkill = value; } }
		public double OrcSkill { get { return m_OrcSkill; } set { m_OrcSkill = value; } }
		public double DwarvenSkill { get { return m_DwarvenSkill; } set { m_DwarvenSkill = value; } }
		public double CommonSkill { get { return m_CommonSkill; } set { m_CommonSkill = value; } }
		public bool CelestialSkill { get { return m_CelestialSkill; } set { m_CelestialSkill = value; } }

		// Reputation ( scales from 1.0 to -1.0 )
		public double ElfReputation { get { return m_ElfReputation; } set { m_ElfReputation = value; } }
		public double OrcReputation { get { return m_OrcReputation; } set { m_OrcReputation = value; } }
		public double DwarfReputation { get { return m_DwarfReputation; } set { m_DwarfReputation = value; } }
		public double DrowReputation { get { return m_DrowReputation; } set { m_DrowReputation = value; } }
		public double DragonReputation { get { return m_DragonReputation; } set { m_DragonReputation = value; } }
		public double DuergarReputation { get { return m_DuergarReputation; } set { m_DuergarReputation = value; } }
		public double AnimalReputation { get { return m_AnimalReputation; } set { m_AnimalReputation = value; } }


		//Shapeshifting
		public DateTime ShapeshiftSlotDelete { get { return m_ShapeshiftSlotDelete; } set { m_ShapeshiftSlotDelete = value; } }
		public DateTime ShapeshiftNext { get { return m_ShapeshiftNext; } set { m_ShapeshiftNext = value; } }
		public bool ShapeshiftSpecial { get { return m_ShapeshiftSpecial; } set { m_ShapeshiftSpecial = value; } }
		public bool Shapeshifted { get { return m_ShapeshiftTransformed; } set { m_ShapeshiftTransformed = value; } }
		public int ShapeshiftHue { get { return m_ShapeshiftHue; } set { m_ShapeshiftHue = value; } }
		public int[] ShapeshiftSlot { get { return m_ShapeshiftSlot; } set { m_ShapeshiftSlot = value; } }
		public int[] ShapeshiftSlotLevel { get { return m_ShapeshiftSlotType; } set { m_ShapeshiftSlotType = value; } }
		public int[] ShapeshiftSlotHue { get { return m_ShapeshiftSlotHue; } set { m_ShapeshiftSlotHue = value; } }
		public string[] ShapeshiftSlotName { get { return m_ShapeshiftSlotName; } set { m_ShapeshiftSlotName = value; } }

		#endregion

		#region Classes, Races, and Alignment
		// Standard Classes
		public bool IsFighter() { if ( m_Class == Class.Fighter ) return true; return false; }
		public bool IsMageSlayer() { if ( m_Class == Class.MageSlayer ) return true; return false; }
		public bool IsKensai() { if ( m_Class == Class.Kensai ) return true; return false; }
		public bool IsCleric() { if ( m_Class == Class.Cleric ) return true; return false; }
		public bool IsDarkCleric() { if ( m_Class == Class.DarkCleric ) return true; return false; }
		public bool IsMage() { if ( IsAeromancer() || IsAquamancer() || IsGeomancer() || IsPyromancer() || IsNecromancer() ) return true; return false; }
		public bool IsAeromancer() { if ( m_Class == Class.Aeromancer ) return true; return false; }
		public bool IsAquamancer() { if ( m_Class == Class.Aquamancer ) return true; return false; }
		public bool IsGeomancer() { if ( m_Class == Class.Geomancer ) return true; return false; }
		public bool IsPyromancer() { if ( m_Class == Class.Pyromancer ) return true; return false; }
		public bool IsNecromancer() { if ( m_Class == Class.Necromancer ) return true; return false; }
		public bool IsPaladin() { if ( m_Class == Class.Paladin ) return true; return false; }
		public bool IsCavalier() { if ( m_Class == Class.Cavalier ) return true; return false; }
		public bool IsUndeadHunter() { if ( m_Class == Class.UndeadHunter ) return true; return false; }
		public bool IsRanger() { if ( m_Class == Class.Ranger ) return true; return false; }
		public bool IsArcher() { if ( m_Class == Class.Archer ) return true; return false; }
		public bool IsShapeshifter() { if ( m_Class == Class.Shapeshifter ) return true; return false; }
		public bool IsForester() { if ( m_Class == Class.Forester ) return true; return false; }
		public bool IsBerserker() { if ( m_Class == Class.Berserker ) return true; return false; }
		public bool IsDragoon() { if ( m_Class == Class.Dragoon ) return true; return false; }
		public bool IsThief() { if ( m_Class == Class.Thief ) return true; return false; }
		public bool IsAssassin() { if ( m_Class == Class.Assassin ) return true; return false; }
		public bool IsBlacksmith() { if (( m_Class == Class.Blacksmith ) || m_Class == Class.Tinker) return true; return false; }
		public bool IsTailor() { if ( m_Class == Class.Tailor ) return true; return false; }
		public bool IsWoodworker() { if ( m_Class == Class.Woodworker || m_Class == Class.Bowyer ) return true; return false; }
	    public bool IsBowyer() { if ( m_Class == Class.Bowyer ) return true; return false; }
		public bool IsAlchemist() { if ( m_Class == Class.Alchemist ) return true; return false; }
		public bool IsCook() { if ( m_Class == Class.Cook ) return true; return false; }
		public bool IsTinker() { if (( m_Class == Class.Tinker )|| m_Class == Class.Blacksmith) return true; return false; }
		public bool IsCrafter() { if ( ( m_Class == Class.Blacksmith ) || ( m_Class == Class.Tailor ) || ( m_Class == Class.Woodworker ) || ( m_Class == Class.Bowyer ) || ( m_Class == Class.Alchemist ) || ( m_Class == Class.Cook ) || ( m_Class == Class.Tinker ) || (m_Class == Class.Merchant) ) return true; return false; }
        public bool IsMerchant() { if (m_Class == Class.Merchant) return true; return false; }
		// Advanced Classes
		public bool IsBard() { if ( m_Class == Class.Bard ) return true; return false; }
		public bool IsBladesinger() { if ( m_Class == Class.Bladesinger ) return true; return false; }
		public bool IsSavage() { if ( m_Class == Class.Savage ) return true; return false; }
		public bool IsMonk() { if ( m_Class == Class.Monk ) return true; return false; }
		public bool IsDrunkenMaster() { if ( m_Class == Class.DrunkenMaster ) return true; return false; }
		public bool IsPirate() { if ( m_Class == Class.Pirate ) return true; return false; }
        public bool IsScoundrel() { if (m_Class == Class.Scoundrel) return true; return false; }
		public bool IsDwarvenDefender() { if ( m_Class == Class.DwarvenDefender ) return true; return false; }
		public bool IsKnight() { if ( m_Class == Class.Knight ) return true; return false; }
		public bool IsTemplar() { if ( m_Class == Class.Templar ) return true; return false; }
		public bool IsSage() { if ( m_Class == Class.Sage ) return true; return false; }
		public bool IsMarksman() { if ( m_Class == Class.Marksman ) return true; return false; }
		public bool IsDeathKnight() { if ( m_Class == Class.DeathKnight ) return true; return false; }
		public bool IsRavager() { if ( m_Class == Class.Ravager ) return true; return false; }
        public bool IsStrider() { if (m_Class == Class.Strider) return true; return false; }

		// Standard Races
		public bool IsElf() { if ( this.PlayerRace == Race.Elf ) return true; return false; }
		public bool IsDrow() { if ( this.PlayerRace == Race.Drow ) return true; return false; }
		public bool IsOrc() { if ( this.PlayerRace == Race.Orc ) return true; return false; }
		public bool IsDwarf() { if ( this.PlayerRace == Race.Dwarf ) return true; return false; }
		public bool IsHuman() { if ( this.PlayerRace == Race.Human ) return true; return false; }
		public bool IsDuergar() { if ( this.PlayerRace == Race.Duergar ) return true; return false; }

		// Advanced Races
		public bool IsKresh() { if ( this.PlayerRace == Race.Kresh ) return true; return false; }
		public bool IsHalfElf() { if ( this.PlayerRace == Race.HalfElf ) return true; return false; }
		public bool IsUndead() { if ( this.PlayerRace == Race.Undead ) return true; return false; }
		public bool IsGiant() { if ( this.PlayerRace == Race.Giant ) return true; return false; }
		public bool IsKender() { if ( this.PlayerRace == Race.Kender ) return true; return false; }
		public bool IsVampire() { if ( this.PlayerRace == Race.Vampire ) return true; return false; }
        public bool IsGoblin() { if (this.PlayerRace == Race.Goblin) return true; return false; }
        public bool IsGnome() { if (this.PlayerRace == Race.Gnome) return true; return false; }
        public bool IsFrostling() { if (this.PlayerRace == Race.Frostling) return true; return false; }
        public bool IsHalfOrc() { if (this.PlayerRace == Race.HalfOrc) return true; return false; }

		// Alignment
		public bool IsEvil() { if ( this.PlayerAlignment == Alignment.LawfulEvil || this.PlayerAlignment == Alignment.NeutralEvil || this.PlayerAlignment == Alignment.ChaoticEvil ) return true; return false; }
		public bool IsGood() { if ( this.PlayerAlignment == Alignment.LawfulGood || this.PlayerAlignment == Alignment.NeutralGood || this.PlayerAlignment == Alignment.ChaoticGood ) return true; return false; }
		public bool IsNeutral() { if ( this.PlayerAlignment == Alignment.LawfulNeutral || this.PlayerAlignment == Alignment.TrueNeutral || this.PlayerAlignment == Alignment.ChaoticNeutral ) return true; return false; }
		public bool IsLawfulGood() { if ( this.PlayerAlignment == Alignment.LawfulGood ) return true; return false; }
		public bool IsNeutralGood() { if ( this.PlayerAlignment == Alignment.NeutralGood ) return true; return false; }
		public bool IsChaoticGood() { if ( this.PlayerAlignment == Alignment.ChaoticGood ) return true; return false; }
		public bool IsLawfulNeutral() { if ( this.PlayerAlignment == Alignment.LawfulNeutral ) return true; return false; }
		public bool IsTrueNeutral() { if ( this.PlayerAlignment == Alignment.TrueNeutral ) return true; return false; }
		public bool IsChaoticNeutral() { if ( this.PlayerAlignment == Alignment.ChaoticNeutral ) return true; return false; }
		public bool IsLawfulEvil() { if ( this.PlayerAlignment == Alignment.LawfulEvil ) return true; return false; }
		public bool IsNeutralEvil() { if ( this.PlayerAlignment == Alignment.NeutralEvil ) return true; return false; }
		public bool IsChaoticEvil() { if ( this.PlayerAlignment == Alignment.ChaoticEvil ) return true; return false; }
		#endregion

		#region Feats, Abilities and Misc
		public Feats[] GetFeats() { return m_Feats; }
		public void SetFeats( Feats[] tempFeats ) { m_Feats = tempFeats; }
		public bool HasFeat( Feats hasfeat ) { for ( int i = 0; i < m_Feats.Length; i++ ) { if ( m_Feats[i] == hasfeat ) return true; } return false; }
		
        public void AddFeat( Feats feat ) 
        {
            if (HasFeat(feat))
                return;
            
            for ( int i = 0; i < m_Feats.Length; i++ ) 
            { 
                if ( m_Feats[i] == Feats.None ) 
                { 
                    m_Feats[i] = feat; SendMessage( Teiravon.Messages.AddFeat ); return; 
                } 
            } 
            SendMessage( Teiravon.Messages.AddFeatError ); 
        }

        public void RemoveFeat(Feats feat) { for (int i = 0; i < m_Feats.Length; i++) { if (m_Feats[i] == feat) { m_Feats[i] = Feats.None; return; } } }

		public bool GetActiveFeats( Feats feat ) { for ( int i = 0; i < m_ActiveFeats.Length; i++ ) { if ( m_ActiveFeats[i] == feat ) return true; } return false; }
		public void SetActiveFeats( Feats feat, bool toogle )
		{
			for ( int i = 0; i < m_ActiveFeats.Length; i++ )
			{
				if ( toogle && m_ActiveFeats[i] == Feats.None )
				{
					m_ActiveFeats[i] = feat;
					return;
				}
				else if ( !toogle && m_ActiveFeats[i] == feat )
					m_ActiveFeats[i] = Feats.None;
			}
		}

		#endregion

        //Nods
        public void GetNod(TeiravonMobile from)
        {
            if (from == null)
                return;
            if (from == this)
            {
                SendMessage("In a desperate attempt to nod at yourself you manage only a stiff neck.");
                return;
            }
            if (this.Nods.Contains(from.Account.ToString()))
                from.SendMessage("You have already acknowledged them.");
            else
            {
                from.SendMessage("You nod in recognition of {0}'s actions.",this.Name);
                SendMessage("{0} nods in acknowledgement of your actions.",from.Name);
                this.Nods.Add(from.Account.ToString(), from.Account.ToString());
                CommandLogging.WriteLine(from, "{0}:{1} Nods {2}:{3}", from.Account,from.Name,this.Account,this.Name);

                foreach (Mobile m in HearAllNods)
                {
                    if (m != null)
                        m.SendMessage("{0}:{1} Nods {2}:{3}", from.Account, from.Name, this.Account, this.Name);
                }
            }
        }


		public void BlockAction( object toLock, TimeSpan duration )
		{
			if ( BeginAction( toLock ) )
				Timer.DelayCall( duration, new TimerStateCallback( EndAction ), toLock );
		}

		public bool GetActivePotions( Server.Items.PotionEffect potion )
		{
			for ( int i = 0; i < m_PotionEffect.Length; i++ )
			{
				if ( m_PotionEffect[i] == potion )
					return true;
			}

			return false;
		}

		public void SetActivePotions( Server.Items.PotionEffect potion, bool toogle )
		{
			for ( int i = 0; i < m_PotionEffect.Length; i++ )
			{
				if ( toogle && m_PotionEffect[i] == Server.Items.PotionEffect.None )
					m_PotionEffect[i] = potion;
				else if ( !toogle && m_PotionEffect[i] == potion )
					m_PotionEffect[i] = Server.Items.PotionEffect.None;
			}
		}

		public bool CanDrink( Server.Items.PotionEffect potion )
		{
			for ( int i = 0; i < m_PotionEffect.Length; i++ )
			{
				if ( m_PotionEffect[i] == potion || m_PotionEffect[m_PotionEffect.Length - 1] != Server.Items.PotionEffect.None )
					return false;
			}

			return true;
		}

		public void SetSpell( ulong m_Spell, bool toggle )
		{
			if ( toggle )
			{
				m_PlayerSpells |= m_Spell;
			}
			else
			{
				m_PlayerSpells &= ~m_Spell;
			}
		}

		public void SetSpell( int m_Spell, bool toggle, Class theclass )
		{
			switch ( theclass )
			{
				case Class.Pyromancer:
					if ( toggle )
						m_PyromancerSpells |= m_Spell;
					else
						m_PyromancerSpells &= ~m_Spell;

					break;
			}
		}

		public bool HasSpell( int m_Spell, Class theclass )
		{
			switch ( theclass )
			{
				case Class.Pyromancer:
					return ( ( m_PyromancerSpells & m_Spell ) != 0 );
			}

			return false;
		}

		public void SetAllSpells( bool toggle )
		{
			if ( toggle )
			{
				m_PlayerSpells = 0xfffffffffffffff;
			}
			else
			{
				m_PlayerSpells = 0;
			}
		}

		public bool CanCast( string spell )
		{

			ulong spells = 0;

			if ( AccessLevel > AccessLevel.Player )
				return true;

			if ( spell == "Agility" )
				spells = Teiravon.Spells.AgilitySpell;

			if ( spell == "Air Elemental" )
				spells = Teiravon.Spells.AirElementalSpell;

			if ( spell == "Arch Cure" )
				spells = Teiravon.Spells.ArchCureSpell;

			if ( spell == "Arch Protection" )
				spells = Teiravon.Spells.ArchProtectionSpell;

			if ( spell == "Blade Spirits" )
				return false;

			if ( spell == "Bless" )
				spells = Teiravon.Spells.BlessSpell;

			if ( spell == "Chain Lightning" )
				spells = Teiravon.Spells.ChainLightningSpell;

			if ( spell == "Clumsy" )
				spells = Teiravon.Spells.ClumsySpell;

			if ( spell == "Create Food" )
				spells = Teiravon.Spells.CreateFoodSpell;

			if ( spell == "Cunning" )
				spells = Teiravon.Spells.CunningSpell;

			if ( spell == "Cure" )
				spells = Teiravon.Spells.CureSpell;

			if ( spell == "Curse" )
				spells = Teiravon.Spells.CurseSpell;

			if ( spell == "Dispel" )
				spells = Teiravon.Spells.DispelSpell;

			if ( spell == "Dispel Field" )
				spells = Teiravon.Spells.DispelFieldSpell;

			if ( spell == "Earth Elemental" )
				spells = Teiravon.Spells.EarthElementalSpell;

			if ( spell == "Earthquake" )
				spells = Teiravon.Spells.EarthquakeSpell;

			if ( spell == "Energy Bolt" )
				spells = Teiravon.Spells.EnergyBoltSpell;

			if ( spell == "Energy Field" )
				spells = Teiravon.Spells.EnergyFieldSpell;

			if ( spell == "Energy Vortex" )
				spells = Teiravon.Spells.EnergyVortexSpell;

            if (spell == "Ethereal Mount")
				return true;

			if ( spell == "Explosion" )
				spells = Teiravon.Spells.ExplosionSpell;

			if ( spell == "Feeblemind" )
				spells = Teiravon.Spells.FeeblemindSpell;

			if ( spell == "Fireball" )
				spells = Teiravon.Spells.FireballSpell;

			if ( spell == "Fire Elemental" )
				spells = Teiravon.Spells.FireElementalSpell;

			if ( spell == "Fire Field" )
				spells = Teiravon.Spells.FireFieldSpell;

			if ( spell == "Flame Strike" )
				spells = Teiravon.Spells.FlamestrikeSpell;

			if ( spell == "Gate Travel" )
				spells = Teiravon.Spells.GateTravelSpell;

			if ( spell == "Greater Heal" )
				spells = Teiravon.Spells.GreaterHealSpell;

			if ( spell == "Harm" )
				spells = Teiravon.Spells.HarmSpell;

			if ( spell == "Heal" )
				spells = Teiravon.Spells.HealSpell;

			if ( spell == "Incognito" )
				spells = Teiravon.Spells.IncognitoSpell;

			if ( spell == "Invisibility" )
				spells = Teiravon.Spells.InvisibilitySpell;

			if ( spell == "Lightning" )
				spells = Teiravon.Spells.LightningSpell;

			if ( spell == "Magic Arrow" )
				spells = Teiravon.Spells.MagicArrowSpell;

			if ( spell == "Magic Lock" )
				spells = Teiravon.Spells.MagicLockSpell;

			if ( spell == "Magic Reflection" )
				spells = Teiravon.Spells.MagicReflectSpell;

			if ( spell == "Magic Trap" )
				spells = Teiravon.Spells.MagicTrapSpell;

			if ( spell == "Mana Drain" )
				spells = Teiravon.Spells.ManaDrainSpell;

			if ( spell == "Mana Vampire" )
				spells = Teiravon.Spells.ManaVampireSpell;

			if ( spell == "Mark" )
				spells = Teiravon.Spells.MarkSpell;

			if ( spell == "Mass Curse" )
				spells = Teiravon.Spells.MassCurseSpell;

			if ( spell == "Mass Dispel" )
				spells = Teiravon.Spells.MassDispelSpell;

			if ( spell == "Meteor Swarm" )
				spells = Teiravon.Spells.MeteorSwarmSpell;

			if ( spell == "Mind Blast" )
				spells = Teiravon.Spells.MindBlastSpell;

			if ( spell == "Night Sight" )
				spells = Teiravon.Spells.NightSightSpell;

			if ( spell == "Paladin" )
				return false;

			if ( spell == "Paralyze Field" )
				spells = Teiravon.Spells.ParalyzeFieldSpell;

			if ( spell == "Paralyze" )
				spells = Teiravon.Spells.ParalyzeSpell;

			if ( spell == "Poison Field" )
				spells = Teiravon.Spells.PoisonFieldSpell;

			if ( spell == "Poison" )
				spells = Teiravon.Spells.PoisonSpell;

			if ( spell == "Polymorph" )
				spells = Teiravon.Spells.PolymorphSpell;

			if ( spell == "Protection" )
				spells = Teiravon.Spells.ProtectionSpell;

			if ( spell == "Reactive Armor" )
				spells = Teiravon.Spells.ReactiveArmorSpell;

			if ( spell == "Recall" )
				spells = Teiravon.Spells.RecallSpell;

			if ( spell == "Remove Trap" )
				spells = Teiravon.Spells.RemoveTrapSpell;

			if ( spell == "Resurrection" )
				spells = Teiravon.Spells.ResurrectionSpell;

			if ( spell == "Reveal" )
				spells = Teiravon.Spells.RevealSpell;

			if ( spell == "Spirit Speak" )
				return true;
			//spells = Teiravon.Spells.SpiritSpeakSpell;

			if ( spell == "Strength" )
				spells = Teiravon.Spells.StrengthSpell;

			if ( spell == "Summon Creature" )
				spells = Teiravon.Spells.SummonCreatureSpell;

			if ( spell == "Summon Daemon" )
				spells = Teiravon.Spells.SummonDaemonSpell;

			if ( spell == "Telekinesis" )
				spells = Teiravon.Spells.TelekinesisSpell;

			if ( spell == "Teleport" )
				spells = Teiravon.Spells.TeleportSpell;

			if ( spell == "Unlock Spell" )
				spells = Teiravon.Spells.UnlockSpell;

			if ( spell == "Wall of Stone" )
				spells = Teiravon.Spells.WallOfStoneSpell;

			if ( spell == "Water Elemental" )
				spells = Teiravon.Spells.WaterElementalSpell;

			if ( spell == "Weaken" )
				spells = Teiravon.Spells.WeakenSpell;

			if ( ( m_PlayerSpells & spells ) != 0 )
				return true;

			return false;
		}

		public void SetLanguages( int value, bool toggle )
		{
			if ( toggle )
			{
				m_LanguagesKnown |= value;
			}
			else
			{
				m_LanguagesKnown &= ~value;
			}
		}

		public void SetLanguages( bool toggle )
		{
			if ( toggle )
			{
				m_LanguagesKnown |= LDrow | LCommon | LElven | LDwarven | LLupine | LOrc;
			}
			else
			{
				m_LanguagesKnown = 0x0;
			}
		}

		public bool IsShifted()
		{
			return ( m_DruidForm > 0 ) ? true : false;
		}

		private void LearnLanguage( int lang )
		{
			switch ( lang )
			{
				case LCommon:
					if ( m_CommonSkill >= 50.0000 )
					{
						SendMessage( "You have learned Common!" );
						SetLanguages( LCommon, true );
					}

					break;

				case LElven:
					if ( m_ElvenSkill >= 100.0000 )
					{
						SendMessage( "You have learned Elven!" );
						SetLanguages( LElven, true );
					}

					break;

				case LDrow:
					if ( m_DrowSkill >= 100.0000 )
					{
						SendMessage( "You have learned Drow!" );
						SetLanguages( LDrow, true );
					}

					break;

				case LLupine:
					if ( m_LupineSkill >= 100.0000 )
					{
						SendMessage( "You have learned Lupine!" );
						SetLanguages( LLupine, true );
					}

					break;

				case LDwarven:
					if ( m_DwarvenSkill >= 100.0000 )
					{
						SendMessage( "You have learned Dwarven!" );
						SetLanguages( LDwarven, true );
					}

					break;

				case LOrc:
					if ( m_OrcSkill >= 100.0000 )
					{
						SendMessage( "You have learned Orc!" );
						SetLanguages( LOrc, true );
					}

					break;
			}
		}

		public void Dismounted()
		{
			string modName = this.Serial.ToString();
			this.RemoveStatMod( modName + "MntDex" );
			this.RemoveStatMod( modName + "MntInt" );

		}

        public void MonkAttack(Mobile defender, int damageGiven)
        {
            BaseWeapon stance = (BaseWeapon)FindItemOnLayer(Layer.Unused_x9);
            
            if (stance != null )
                Server.Engines.XmlSpawner2.XmlAttach.OnWeaponHit(stance, this, defender, damageGiven);
        }

        public void EnchantedShot(Mobile defender, int damageGiven)
        {
            BaseWeapon quiver = (BaseWeapon)FindItemOnLayer(Layer.Unused_x9);

            if (quiver != null)
                Server.Engines.XmlSpawner2.XmlAttach.OnWeaponHit(quiver, this, defender, damageGiven);
        }

        public void EndAmbush()
        {
            m_Ambush = false;
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Mounted )
			{
				if ( from is TeiravonMobile && this == from )
				{
					TeiravonMobile tm = ( TeiravonMobile )from;
					tm.Dismounted();
				}
			}

			base.OnDoubleClick( from );

			SpellHelper.Turn( from, this );
		}
        
        //Override for Ambush
        public override void RevealingAction()
        {
            if (Hidden)
            {
                m_Ambush = true;
                Timer.DelayCall(TimeSpan.FromSeconds(.25), new TimerCallback(EndAmbush));
                m_Revealed = DateTime.Now;
            }
            base.RevealingAction();
        }

        //Override for Use to Prevent Actions while Manacled.

        public override void Use(Item item)
        {
            Item i = FindItemOnLayer(Layer.TwoHanded);
            if (i is Manacles)
            {
                item.OnDoubleClickNotAccessible(this);
                return;
            }

            base.Use(item);
        }

		public override bool CheckEquip( Item item )
		{

			//Prevent the usage of scytches, bardiches, pikes and halberds on horses
			if ( Mounted && ( item is BasePoleArm || item is Pike ) )
			{
				SendMessage( "You cannot equip that weapon while mounted." );
				return false;
			}

			// General
			if ( ( item is BaseSpear || item is Lance ) && Mounted && !HasFeat( Feats.MountedCombat ) )
			{
				SendMessage( "You must have the Mounted Combat feat to use this weapon on a horse." );
				return false;
			}
            if (item is BaseRanged && IsRavager())
            {
                SendMessage("Ravagers use no ranged weapons.");
                return false;
            }
			if ( item is BaseRanged && Mounted )
			{
				bool heavybow = false;

				Type[] m_HeavyBows = new Type[]
				{
					typeof( Recurve ),
					typeof( Crossbow ),
					typeof( Longbow ),
					typeof( CompositeBow ),
					typeof( HeavyCrossbow ),
					typeof( RepeatingCrossbow )
				};

				for ( int i = 0; i < m_HeavyBows.Length; i++ )
				{
					if ( item.GetType() == m_HeavyBows[i] )
					{
						heavybow = true;
						break;
					}
				}

				if ( heavybow )
				{
					SendMessage( "This bow is too unwieldy to be used while mounted." );
					return false;
				}
			}
            /*
			if ( item is Katana && !IsKensai() )
			{
				SendMessage( "Only Kensai can use this weapon." );
				return false;
			}
            */

			// Class specific
			switch ( m_Class )
			{
				// Fighters
				case Class.Fighter:
                    break;
                case Class.Monk:
                    if (item is BaseArmor)
                    {
                        BaseArmor armor = (BaseArmor)item;

                        if (IsMonk() && (int)armor.MaterialType != 0)
                        {
                            SendMessage("You cannot wear armor!");
                            return false;
                        }
                    }
                    break;
				case Class.Kensai:
					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = ( BaseWeapon )item;

						if ( IsKensai() && weapon.Skill != Skills.Swords.SkillName && !( weapon is Dagger ) )
						{
							SendMessage( "Kensai can only use swords" );
							return false;
						}

					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = ( BaseArmor )item;

						if ( IsKensai() && ( int )armor.MaterialType != 0 )
						{
							SendMessage( "You cannot wear armor!" );
							return false;
						}
					}
					else if ( item is BaseJewel )
					{
						if ( item is Drowmagering || item is Drowpriestring )
						{
							SendMessage( "You cannot wear that!" );
							return false;
						}
					}
					else if ( item is BaseClothing )
					{
						if ( item is Drowmagerobe )
						{
							SendMessage( "You cannot wear that!" );
							return false;
						}
					}


					break;

				// Mages
				case Class.Aeromancer:
				case Class.Aquamancer:
				case Class.Geomancer:
				case Class.Pyromancer:
				case Class.Necromancer:
					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = ( BaseWeapon )item;

						if ( ( ( weapon.Skill != Skills.Macing.SkillName ) && ( ( weapon is Dagger ) == false ) ) && !( weapon is ElvenLongsword ) && ( ( weapon is Hatchet ) == false ) && ( ( weapon is Pickaxe ) == false ) )
						{
							SendMessage( "You can only use maces and staves" );
							return false;
						}
					}

					if ( item is BaseArmor )
					{
						BaseArmor armor = ( BaseArmor )item;

						if ( armor.ArmorAttributes.MageArmor != 1 && armor.MaterialType != ArmorMaterialType.Cloth || armor.MaterialType == ArmorMaterialType.Plate )
						{
							SendMessage( "You cannot wear armor" );
							return false;
						}
					}

					if ( item is BaseJewel )
					{
						if ( item is Drowpriestring || item is Drowwarring )
						{
							SendMessage( "You cannot wear that!" );
							return false;
						}
					}

					break;

				// Clerics
				case Class.Cleric:
				case Class.DarkCleric:
					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = ( BaseWeapon )item;

						if ( ( weapon.Skill != Skills.Macing.SkillName ) && ( ( weapon is Dagger ) == false ) && ( ( weapon is Hatchet ) == false ) && ( ( weapon is Pickaxe ) == false ) )
						{
                            if (IsElf() && Faith == Deity.Valar && weapon is ElvenLongsword)
                            {
                                return true;
                            }
							SendMessage( "You can only use maces and staves" );
							return false;
						}
					}

					if ( item is BaseJewel )
					{
						if ( item is Drowmagering || item is Drowwarring )
						{
							SendMessage( "You cannot wear that!" );
							return false;
						}
					}

					if ( item is BaseClothing )
					{
						if ( item is Drowmagerobe )
						{
							SendMessage( "You cannot wear that!" );
							return false;
						}
					}

					break;

				// Paladins
				case Class.Paladin:
				case Class.UndeadHunter:
                case Class.DeathKnight:
				case Class.Cavalier:
					break;

				// Rangers
                case Class.MageSlayer:
				case Class.Archer:
				case Class.Ranger:
					if ( item is BaseArmor )
					{
						BaseArmor armor = ( BaseArmor )item;

						if ( armor.MaterialType == ArmorMaterialType.Plate )
						{
							SendMessage( "You cannot wear armor greater than chainmail" );
							return false;
						}
					}

					break;

				// Druids
				case Class.Shapeshifter:
				case Class.Forester:
					if ( IsShifted() )
					{
						if ( item is ShapeshifterArmor || item is ShapeshifterWeapon || item is CustomShapeshifterArmor || item is CustomShapeshifterWeapon )
							return true;
						else
						{
							SendMessage( Teiravon.Messages.NoEquip );
							return false;
						}
					}

					if ( item is BaseArmor )
					{
						BaseArmor armor = ( BaseArmor )item;

                        if (armor.MaterialType > ArmorMaterialType.Scaled && armor.MaterialType != ArmorMaterialType.Dwarven)
						{
							SendMessage( "You cannot wear armor greater than leather" );
							return false;
						}
					}

					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = ( BaseWeapon )item;

						if ( weapon.Skill == Skills.Swords.SkillName && ( item is Scimitar ? false : true ) )
						{
							SendMessage( "The only sword you can use is a scimitar" );
							return false;
						}
					}

					break;

				// Barbarians
				case Class.Berserker:
				case Class.Dragoon:
					break;

				// Rogues
				case Class.Thief:
				case Class.Assassin:
                case Class.Bard:
                case Class.Scoundrel:
					if ( item is BaseArmor )
					{
						BaseArmor armor = ( BaseArmor )item;

						if ( IsDrow() && armor is IDrowEquip )
						{
							return base.CheckEquip( item );
						}

						if ( armor.MaterialType > ArmorMaterialType.Scaled && armor.MaterialType != ArmorMaterialType.Dwarven )
						{
							SendMessage( "You cannot wear armor greater than leather" );
							return false;
						}
					}
                    break;
                case Class.Savage:
                    if (item is BaseArmor)
                    {
                        BaseArmor armor = (BaseArmor)item;

                        if (armor.MaterialType != ArmorMaterialType.Cloth && !(armor is OrcGloves || armor is OrcSpikedHelm))
                        {
                            SendMessage("You cannot wear armor");
                            return false;
                        }
                    }

                    if (item is BaseJewel)
                    {
                        if (item is Drowpriestring || item is Drowwarring)
                        {
                            SendMessage("You cannot wear that!");
                            return false;
                        }
                    }
                    break;
                default:
                    break;
			}

			return base.CheckEquip( item );
		}

		#region RunUO Core Speech Ripoff
		private static ArrayList iHears;
		private static ArrayList iOnSpeech;

		private void iAddSpeechItemsFrom( ArrayList list, Container cont )
		{
			for ( int num1 = 0; num1 < cont.Items.Count; num1++ )
			{
				Item item1 = ( Item )cont.Items[num1];
				if ( item1.HandlesOnSpeech )
				{
					list.Add( item1 );
				}
				if ( item1 is Container )
				{
					this.iAddSpeechItemsFrom( list, ( Container )item1 );
				}
			}
		}

		public override void DoSpeech( string text, int[] keywords, MessageType type, int hue )
		{
			SpeechEventArgs args1;

			if ( this.Deleted || Commands.Handle( this, text ) )
			{
				return;
			}

			int num1 = 15;

			if ( ( type == MessageType.Regular || type == MessageType.Yell || type == MessageType.Whisper ) && text.Substring( 0, 1 ) != "[" )
			{
				// Elven
				if ( m_CurrentLanguage == LElven )
					hue = 2596;

				// Drow
				if ( m_CurrentLanguage == LDrow )
					hue = 2581;

				// Orc
				if ( m_CurrentLanguage == LOrc )
					hue = 2598;

				// Dwarven
				if ( m_CurrentLanguage == LDwarven )
					hue = 2939;

				// Lupine
				if ( m_CurrentLanguage == LLupine )
					hue = 767;
			}

			if ( hue == 690 || hue == 549 )
				hue = 37;


			// Hear All
			foreach ( Mobile tm in HearAllEntries )
			{
				if ( tm.Map == Map.Internal )
				{
					HearAllEntries.Remove( tm );
					break;
				}

				if ( tm != this && this.AccessLevel <= tm.AccessLevel )
				{
					tm.SendMessage( hue, "*[" + Name + "]: " + text );
				}
			}


			switch ( type )
			{
				case MessageType.Regular:
					{
						this.SpeechHue = hue;
						goto Label_006E;
					}
				case MessageType.System:
					{
						break;
					}
				case MessageType.Emote:
					{
						this.EmoteHue = hue;
						goto Label_006E;
					}
				case MessageType.Whisper:
					{
						this.WhisperHue = hue;
						num1 = 1;
						goto Label_006E;
					}
				case MessageType.Yell:
					{
						this.YellHue = hue;
						num1 = 0x12;
						goto Label_006E;
					}
			}

			type = MessageType.Regular;

Label_006E:
			args1 = new SpeechEventArgs( this, text, type, hue, keywords );
			EventSink.InvokeSpeech( args1 );
			this.Region.OnSpeech( args1 );
			this.OnSaid( args1 );

			if ( !args1.Blocked )
			{
				text = args1.Speech;
				if ( ( text != null ) && ( text.Length != 0 ) )
				{
					if ( iHears == null )
					{
						iHears = new ArrayList();
					}
					else if ( iHears.Count > 0 )
					{
						iHears.Clear();
					}
					if ( iOnSpeech == null )
					{
						iOnSpeech = new ArrayList();
					}
					else if ( iOnSpeech.Count > 0 )
					{
						iOnSpeech.Clear();
					}

					ArrayList list1 = iHears;
					ArrayList list2 = iOnSpeech;

					if ( this.Map != null )
					{
						IPooledEnumerable enumerable1 = this.Map.GetObjectsInRange( this.Location, num1 );

						foreach ( object obj1 in enumerable1 )
						{
							if ( obj1 is Mobile )
							{
								Mobile mobile1 = ( Mobile )obj1;
								if ( !mobile1.CanSee( this ) || ( ( !Mobile.NoSpeechLOS && mobile1.Player ) && !mobile1.InLOS( this ) ) )
								{
									continue;
								}
								if ( mobile1.NetState != null )
								{
									list1.Add( mobile1 );
								}
								if ( mobile1.HandlesOnSpeech( this ) )
								{
									list2.Add( mobile1 );
								}
								for ( int num2 = 0; num2 < mobile1.Items.Count; num2++ )
								{
									Item item1 = ( Item )mobile1.Items[num2];
									if ( item1.HandlesOnSpeech )
									{
										list2.Add( item1 );
									}
									if ( item1 is Container )
									{
										this.iAddSpeechItemsFrom( list2, ( Container )item1 );
									}
								}
								continue;
							}
							if ( obj1 is Item )
							{
								if ( ( ( Item )obj1 ).HandlesOnSpeech )
								{
									list2.Add( obj1 );
								}
								if ( obj1 is Container )
								{
									this.iAddSpeechItemsFrom( list2, ( Container )obj1 );
								}
							}
						}

						object obj2 = null;
						string text1 = text;
						SpeechEventArgs args2 = null;
						if ( this.MutateSpeech( list1, ref text1, ref obj2 ) )
						{
							args2 = new SpeechEventArgs( this, text1, type, hue, new int[0] );
						}

						this.CheckSpeechManifest();
						this.ProcessDelta();

						Packet packet1 = null;
						Packet packet2 = null;

						for ( int num3 = 0; num3 < list1.Count; num3++ )
						{
							Mobile mobile2 = ( Mobile )list1[num3];
							if ( ( args2 == null ) || !this.CheckHearsMutatedSpeech( mobile2, obj2 ) || type == MessageType.Emote )
							{
								mobile2.OnSpeech( args1 );
								NetState state1 = mobile2.NetState;

								if ( state1 != null )
								{
									if ( packet1 == null )
									{
										packet1 = new UnicodeMessage( this.Serial, ( int )this.Body, type, hue, 3, "ENU", this.Name, text );
									}
									state1.Send( packet1 );
								}
							}
							else
							{
								mobile2.OnSpeech( args2 );
								NetState state2 = mobile2.NetState;

								if ( state2 != null )
								{
									if ( packet2 == null )
									{
										packet2 = new AsciiMessage( this.Serial, ( int )this.Body, type, hue, m_LanguageFont, this.Name, text1 );
									}
									state2.Send( packet2 );
								}
							}
						}
						/*if (list2.Count > 1)
						{
							  list2.Sort(new Mobile.LocationComparer(this));
						}*/
						for ( int num4 = 0; num4 < list2.Count; num4++ )
						{
							object obj3 = list2[num4];
							if ( obj3 is Mobile )
							{
								Mobile mobile3 = ( Mobile )obj3;
								if ( ( args2 == null ) || !this.CheckHearsMutatedSpeech( mobile3, obj2 ) )
								{
									mobile3.OnSpeech( args1 );
								}
								else
								{
									mobile3.OnSpeech( args2 );
								}
							}
							else
							{
								Item item2 = ( Item )obj3;
								item2.OnSpeech( args1 );
							}
						}
					}
				}
			}
		}
		#endregion

		public override bool CheckHearsMutatedSpeech( Mobile m, object context )
		{
			if ( !Player )
				return false;

			if ( !m.Player )
				return false;

			if ( m_SpeechType != MessageType.Regular && m_SpeechType != MessageType.Yell && m_SpeechType != MessageType.Whisper )
				return false;

			if ( m == this )
				return false;

			if ( !Alive && m.AccessLevel == AccessLevel.Player )
				return true;

			TeiravonMobile m_Player = ( TeiravonMobile )m;

			//Skill check for understanding
			if ( m_CurrentLanguage == LElven )
			{
				if ( m_Player.ElvenSkill * 100 > Utility.RandomMinMax( 1, 10000 ) )
				{
					m_Player.ElvenSkill += 0.005;
					return false;
				}
			}
			if ( m_CurrentLanguage == LDrow )
			{
				if ( m_Player.DrowSkill * 100 > Utility.RandomMinMax( 1, 10000 ) )
				{
					m_Player.DrowSkill += 0.005;
					return false;
				}
			}
			if ( m_CurrentLanguage == LDwarven )
			{
				if ( m_Player.DwarvenSkill * 100 > Utility.RandomMinMax( 1, 7000 ) )
				{
					m_Player.DwarvenSkill += 0.005;
					return false;
				}
			}
			if ( m_CurrentLanguage == LOrc )
			{
				if ( m_Player.OrcSkill * 100 > Utility.RandomMinMax( 1, 6500 ) )
				{
					m_Player.OrcSkill += 0.005;
					return false;
				}
			}
			if ( m_CurrentLanguage == LLupine )
			{
				if ( m_Player.LupineSkill * 100 > Utility.RandomMinMax( 1, 15000 ) )
				{
					m_Player.LupineSkill += 0.005;
					return false;
				}
			}

			// Common
			if ( m_CurrentLanguage == LCommon )
				if ( ( m_Player.m_LanguagesKnown & LCommon ) != 0 )
					return false;
				else
				{
					m_Player.CommonSkill += 0.001;
					m_Player.LearnLanguage( LCommon );
					m_LanguageFont = 3;
				}

			// Elven
			if ( m_CurrentLanguage == LElven )
				if ( ( m_Player.m_LanguagesKnown & LElven ) != 0 )
					return false;
				else
				{
					m_Player.ElvenSkill += 0.001;
					m_Player.LearnLanguage( LElven );
					m_LanguageFont = 8;
				}

			// Drow
			if ( m_CurrentLanguage == LDrow )
				if ( ( m_Player.m_LanguagesKnown & LDrow ) != 0 )
					return false;
				else
				{
					m_Player.DrowSkill += 0.001;
					m_Player.LearnLanguage( LDrow );
					m_LanguageFont = 8;
				}

			// Orc
			if ( m_CurrentLanguage == LOrc )
				if ( ( m_Player.m_LanguagesKnown & LOrc ) != 0 )
					return false;
				else
				{
					m_Player.OrcSkill += 0.001;
					m_Player.LearnLanguage( LOrc );
					m_LanguageFont = 8;
				}

			// Dwarven
			if ( m_CurrentLanguage == LDwarven )
				if ( ( m_Player.m_LanguagesKnown & LDwarven ) != 0 )
					return false;
				else
				{
					m_Player.DwarvenSkill += 0.001;
					m_Player.LearnLanguage( LDwarven );
					m_LanguageFont = 8;
				}

			// Lupine
			if ( m_CurrentLanguage == LLupine )
				if ( ( m_Player.m_LanguagesKnown & LLupine ) != 0 )
					return false;
				else
				{
					m_Player.LupineSkill += 0.001;
					m_Player.LearnLanguage( LLupine );
					m_LanguageFont = 8;
				}

			// Common
			if ( m_CurrentLanguage == LCelestial )
				if ( m_Player.LanguageCelestialSkill )
					return false;

			return true;
		}


		public override bool MutateSpeech( ArrayList hears, ref string text, ref object context )
		{
			string m_Text = text;
			string m_Scrambled = "";

			if ( Alive )
			{
				for ( int i = 0; i < m_Text.Length; i++ )
				{
					if ( m_Text.Substring( i, 1 ) != " " )
					{
						if ( m_CurrentLanguage == LElven || m_CurrentLanguage == LDrow )
							m_LanguageCase = 0;
						else if ( m_CurrentLanguage == LOrc )
							m_LanguageCase = 2;
						else if ( m_CurrentLanguage == LLupine )
							m_LanguageCase = 3;
						else
							m_LanguageCase = 1;

						if ( m_LanguageCase == 1 )
                            m_Scrambled += Microsoft.VisualBasic.Strings.Chr(Utility.RandomMinMax(65, 90));
						else if ( m_LanguageCase == 2 )
							m_Scrambled += Microsoft.VisualBasic.Strings.Chr( Utility.RandomMinMax( 48, 57 ) );
						else if ( m_LanguageCase == 3 )
							m_Scrambled += Microsoft.VisualBasic.Strings.Chr( Utility.RandomMinMax( 33, 42 ) );
						else
							m_Scrambled += Microsoft.VisualBasic.Strings.Chr( Utility.RandomMinMax( 97, 122 ) );
					}
					else
						m_Scrambled += " ";
				}
			}
			else
			{
				/* DO NOT UNCOMMENT		for ( int i = 0; i < m_Text.Length; i++ )
								{
									if ( m_Text.Substring(i, 1) != " " )
									{
										if ( Utility.RandomMinMax( 1, 3 ) == 3 )
											m_Scrambled += "o";
										else
											m_Scrambled += "O";
									} else {
										m_Scrambled += " ";
									}
								}
				DO NOT UNCOMMENT */
				return base.MutateSpeech( hears, ref text, ref context );
			}

			text = m_Scrambled;

			return true;
		}

        public override bool OnBeforeDeath()
        {
            if (Spells.Third.BlessSpell.kinarugi.Contains(this) && !DiedOnce.Contains(this))
            {
                DiedOnce.Add(this, DateTime.Now);
                XmlAttach.AttachTo(this, new XmlMorph(57, .5));
                Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, 0x835, 0);
                Effects.PlaySound(Location, Map, 0x3EA);
                Hits = MaxHits;
                Mana = MaxMana;
                Stam = MaxStam;
                ProcessDelta();
                return false;
            }
            else if (DiedOnce.Contains(this))
            {
                DiedOnce.Remove(this);
                return base.OnBeforeDeath();
            }

            if (Spells.Fourth.ArchProtectionSpell.Registry.Contains(this))
            {
                Effects.SendLocationParticles(this, 0x36FE, 5, 5, 800, 2, 0, 0);
                this.PlaySound(0x29A);
                if (Hits < (MaxHits *.25))
                    this.Hits = (int)(MaxHits * .25);
                Spells.Fourth.ArchProtectionSpell.Registry.Remove(this);
                return false;
            }
            else
            {

                return base.OnBeforeDeath();
            }
        }
        public override int OnBeforeHeal(int amount)
        {
            if (this.IsUndead())
                amount = (int)(amount * .5);

            return base.OnBeforeHeal(amount);
        }

		public override bool CanSee( Mobile m )
		{
			if ( m is BaseHealer )
				return true;

            if (m is BaseCreature)
            {
                BaseCreature m_Creature = (BaseCreature)m;

                if (m_Creature.Obscured)
                    return false;
            }

			if ( m is PlayerMobile )
			{
				TeiravonMobile m_Player = ( TeiravonMobile )m;
				//Regions.TeiravonRegion r = m.Region as Regions.TeiravonRegion;
				Region reg = m.Region;

				if ( AccessLevel > AccessLevel.Player && m_Player.AccessLevel > AccessLevel.Player )
				{
					if ( m_Player.AccessLevel <= AccessLevel )
						return true;
				}

                if (m_Player.Obscured)
                    return false;

                // DROW SIGHT
				//				if ( ( IsDrow() && Alive ) && ( m_Player.Alive && m_Player.AccessLevel == AccessLevel ) && !m_Player.HasFeat( Feats.AdvancedStealth ) && reg.Name == "Shadowrealm" )
				//if ( ( IsDrow() && Alive ) && ( m_Player.Alive && m_Player.AccessLevel == AccessLevel ) && !m_Player.HasFeat( Feats.AdvancedStealth ) )
				//	return true;

				if ( AccessLevel == AccessLevel.Player &&( m_Player.Hidden || m_Player.GetActivePotions( Server.Items.PotionEffect.Invisibility)) && m_Player.AccessLevel == AccessLevel.Player && m_Player != this )
				{
					//double stealthskill = m_Player.Skills.Stealth.Value;
					//double detecthidden = Skills[SkillName.DetectHidden].Value;
					Party p = Server.Engines.PartySystem.Party.Get( this );

					// Allow seeing of stealthed party members
					if ( p != null && p.Contains( m_Player ) )
						return true;
                    /*
					if ( detecthidden >= 30.0 && InRange( m_Player.Location, 5 ) )
					{
						if ( m_Player.HasFeat( TeiravonMobile.Feats.AdvancedStealth ) )
							stealthskill *= 1.3;

						if ( stealthskill < detecthidden )
							return true;
					}*/
				}
			}

			// Hide monsters from dead players to prevent 'ghosting' around dungeons
			if ( !Alive && m is BaseCreature && !IsUndead() )
				return false;

			return base.CanSee( m );
		}
        /*
        public override bool CanSee(Item item)
        {
            if (item.Parent is Corpse && this.AccessLevel == AccessLevel.Player)
            {
                Corpse c = item.Parent as Corpse;
                Engines.PartySystem.Party p = Engines.PartySystem.Party.Get( ( Mobile )this );
                if (this == c.Owner && item.Visible)
                    return true;
                else if (p.Contains(c.Owner) && item.Visible)
                    return true;
                else return false;
            }

            return base.CanSee(item);
        }
        */

        public virtual Mobile GetTaunt()
        {

            XmlTaunt a = (XmlTaunt)XmlAttach.FindAttachment(this, typeof(XmlTaunt));

            if (a != null)
            {
                return a.Taunt;
            }
            else
            {
                return null;
            }
        }

		public override void Damage( int amount, Mobile from )
		{
            if (Spells.Third.BlessSpell.lloth.Contains(this) && Utility.RandomDouble() < .15)
            {
                Effects.SendLocationParticles(EffectItem.Create(this.Location, this.Map, EffectItem.DefaultDuration), 0x3728, 1, 13, 2869, 1, 5023, 0);
                PlaySound(0x482);
                Hidden = true;
                Combatant = null;
                amount = 0;
            }

            if (HasFeat(Feats.Challenge))
            {

                if (GetTaunt() == from)
                {
                    amount = (int)(amount * .75);
                    amount -= 2;

                    if (amount < 0)
                        amount = 0;
                }
            }
            
			//DOUBLE DAMAGE AT NIGHT
				if ( from is BaseCreature )
				{
					BaseCreature m_Creature = from as BaseCreature;

					if ( !m_Creature.Controled )
					{
						//int hour, min;

						//Clock.GetTime( this.Map, this.X, this.Y, out hour, out min );

						//TeiravonRegion r = this.Region as TeiravonRegion;
                        double value = 1.5;
						//if ( ( hour > 20 || hour < 5 ) && r != null && !r.Dungeon )
                        if (this.PlayerLevel < 10)
                            value = 1 + this.PlayerLevel * .05;
                        amount = (int)(amount * value);
					}
				}
			 


            /*
            if (StoneSkinSpell.Registry.Contains(this) && from != null && from != this)
            {
                int reduce = 1;
                object o = StoneSkinSpell.Registry[this];

                if (o != null && o is int)
                {
                    reduce = (int)o;
                }

                int temp = amount;

                if (temp - reduce > 0)
                    amount -=reduce;

                if (temp - reduce < 0)
                    amount = 0;

                this.SendMessage("{0} Damage blocked.", reduce);
                if (Utility.RandomMinMax(1, 20) > 19)
                {
                    amount = 0;
                    SendMessage("Your tough skin deflects the attack!");
                    from.SendMessage("{0} deflects the attack!", Name);
                }

            } */
            /*if (HasFeat(Feats.BerserkerRage))
            {
                BAC += 2;
                Server.Items.BeverageBottle.CheckHeaveTimer(this);
            }
            */
            if (HasFeat(Feats.BattleHardened))
            {
                double ratio = (double)this.Hits / (double)this.HitsMax;
                double coef = (1 - ratio) * .0125 * this.PlayerLevel;
                coef +=.15;
                
                int newamount = (int)(amount * (1 - coef));
                amount = newamount;
            }
			base.Damage( amount, from );
		}


		private bool m_Guard = true;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Guard { get { return m_Guard; } set { m_Guard = value; } }

		public override bool CanBeHarmful( Mobile target, bool message, bool ignoreOurBlessedness )
		{
			if ( Grab.Grabbers.Contains( this ) )
				return false;
			else
			{
				foreach ( Grab.GrabTimer grab in Grab.Grabbers.Values )
					if ( grab.Grabbed == this )
						return false;
			}

			return base.CanBeHarmful( target, message, ignoreOurBlessedness );
		}


		public override void Attack( Mobile from )
		{
			if ( HasFeat( Feats.TacticalAssessment ) && from != this )
			{
				if ( PlayerLevel < 5 )
					SendMessage( "You figure they have about {0} hitpoints left...", from.Hits );
				else if ( PlayerLevel < 10 )
					SendMessage( "You figure they have about {0} hitpoints and {1} stamina left...", from.Hits, from.Stam );
				else if ( PlayerLevel < 15 )
					SendMessage( "You figure they have about {0} hitpoints, {1} stamina, and {2} mana left...", from.Hits, from.Stam, from.Mana );
				else if ( PlayerLevel >= 15 )
					SendMessage( "They have {0}/{1} hits, {2}/{3} stamina, and {4}/{5} mana left.", from.Hits, from.HitsMax, from.Stam, from.StamMax, from.Mana, from.ManaMax );
			}

			base.Attack( from );
		}
        public override bool CantWalk
        {
            get
            {
                return base.CantWalk || this.GetActiveFeats(Feats.DefensiveStance);
            }
        }
        public override int Luck
        {
            get
            {
                int bonus = 0;
                if (HasFeat(Feats.CharmedLife))
                    bonus += 25* PlayerLevel;

                if (HasFeat(Feats.RottenLuck))
                    return base.Luck + bonus > 500 ? 500 : base.Luck + bonus;

                return base.Luck + bonus;
            }
        }

        public override void OnMovement(Mobile m_Player, Point3D oldLocation)
        {
            base.OnMovement(m_Player, oldLocation);
        }
        /*
		public override void OnMovement( Mobile m_Player, Point3D oldLocation )
		{
			try
			{
				if ( Alive )
				{

					//Get player party
					Engines.PartySystem.Party p = Engines.PartySystem.Party.Get( ( Mobile )this );


					//Determines aura range
					int auraRange = 2 + ( int )( PlayerLevel / 5 );
					if ( auraRange > 6 )
						auraRange = 6;

					if ( GetActiveFeats( Feats.HolyAura ) || GetActiveFeats( Feats.DivineAura ) )
					{
						ArrayList HolyAura = new ArrayList();

						foreach ( Mobile m in GetMobilesInRange( auraRange ) )
						{
							if ( m == null )
								continue;

							if ( m.AccessLevel > AccessLevel )
								continue;

							if ( p != null && p.Contains( m ) )
								continue;

							if ( m is BaseCreature && m.Karma <= -300 )
							{
								if ( !m.Alive || m == null )
									continue;

								BaseCreature c = ( BaseCreature )m;

								if ( c.Controled || c.Summoned )
								{
									if ( c.ControlMaster == this || c.SummonMaster == this )
										continue;

									if ( p != null && ( p.Contains( c.ControlMaster ) || p.Contains( c.SummonMaster ) ) )
										continue;
									;
								}

								HolyAura.Add( m );
							}
							else if ( m is TeiravonMobile )
							{
								if ( m == this || !m.Alive || m == null )
									continue;

								TeiravonMobile m_Target = ( TeiravonMobile )m;

								if ( m_Target.IsEvil() || m_Target.Karma <= -3000 )
									HolyAura.Add( m_Target );
							}
						}

						foreach ( Mobile m in HolyAura )
						{
							if ( m != null )
							{
								//								int damage = (int)(PlayerLevel / 5);
								int damage = 3;

								//								if ( Utility.Random( 2 ) == 0 )
								//									Mana -= 1;

								if ( Mana <= 0 )
								{
									SetActiveFeats( TeiravonMobile.Feats.HolyAura, false );
									SendMessage( "You run out of spiritual energy to sustain the holy aura!" );
									break;
								}
								if ( Utility.Random( 1000 ) <= ( ( int )( PlayerLevel / 2 ) ) * 10 )
								{
									Mana -= 1;
									if ( damage >= 2 )
										m.Damage( Utility.RandomMinMax( damage - 1, damage + 1 ), ( Mobile )this );
									else
										m.Damage( 1, ( Mobile )this );
								}
							}
						}
					}


					if ( GetActiveFeats( Feats.DarkAura ) )
					{

						ArrayList DarkAura = new ArrayList();

						foreach ( Mobile m in GetMobilesInRange( auraRange ) )
						{
							if ( m == null )
								continue;

							if ( m.AccessLevel > AccessLevel )
								continue;

							if ( p != null && p.Contains( m ) )
								continue;

							if ( m is BaseCreature && m.Karma >= -3000 )
							{
								if ( !m.Alive || m == null )
									continue;

								BaseCreature c = ( BaseCreature )m;

								if ( c.Controled || c.Summoned )
								{
									if ( c.ControlMaster == this || c.SummonMaster == this )
										continue;

									if ( p != null && ( p.Contains( c.ControlMaster ) || p.Contains( c.SummonMaster ) ) )
										continue;
									;
								}

								DarkAura.Add( m );
							}
							else if ( m is TeiravonMobile )
							{
								if ( m == this || !m.Alive || m == null )
									continue;

								TeiravonMobile m_Target = ( TeiravonMobile )m;

								if ( m_Target.IsGood() || m_Target.Karma >= 3000 )
									DarkAura.Add( m_Target );
							}
						}

						foreach ( Mobile m in DarkAura )
						{
							if ( m != null )
							{
								int damage = 3;


								if ( Mana <= 0 )
								{
									SetActiveFeats( TeiravonMobile.Feats.DarkAura, false );
									SendMessage( "You run out of spiritual energy to sustain the dark aura!" );
									break;
								}

								if ( Utility.Random( 1000 ) <= ( ( int )( PlayerLevel / 2 ) ) * 10 )
								{
									Mana -= 2;
									if ( damage >= 2 )
										m.Damage( Utility.RandomMinMax( damage - 1, damage + 1 ), ( Mobile )this );
									else
										m.Damage( 1, ( Mobile )this );
								}
							}
						}
					}
				}
			}
			catch
			{
				m_Player.SendMessage( "Error detected. Please contact a scripter with exactly what you were doing and what was around you at the time you recieve this message." );
			}

			base.OnMovement( m_Player, oldLocation );
		}
        */

        private void GetUp()
        {
            bool jarethRes = false;

            foreach (Mobile m in GetMobilesInRange(3))
            {
                if (m is TeiravonMobile)
                {
                    TeiravonMobile t = m as TeiravonMobile;
                    if (t.IsGoblin() && t != this && t.Alive)
                        jarethRes = true;
                }
            }
            if (jarethRes)
            {
                this.Resurrect();
                bool found = false;
                Corpse body = null;
                this.Hits = this.HitsMax / 2;
                this.Mana = this.ManaMax / 2;
                foreach (Item i in GetItemsInRange(0))
                {
                    if (i is Corpse)
                    {
                        Corpse c = i as Corpse;
                        if (c.Owner == this)
                        {
                            found = true;
                            body = c;
                            break;
                        }
                    }
                }
                if (found)
                {
                    body.Open(this, true);
                    this.Animate(2, 4, 1, false, false, 0);
                }
            }
        }

		public override void OnDeath( Container c )
		{
            foreach (Mobile m in GetMobilesInRange(15))
            {
                if (!m.Player)
                    Send(m.RemovePacket);
            }
            if (IsUndead() && AccessLevel < AccessLevel.GameMaster)
            {
                Map = Map.Tokuno;
                Location = new Point3D(900, 775, 0);
                SendMessage("With nothing to seal your soul to this decayed mortal form, your spirit slips from its bond and sinks to the nether, knowing final rest at last.");
            }
            if (Spells.Third.BlessSpell.jareth.Contains(this))
            {
                Timer.DelayCall(TimeSpan.FromSeconds(Utility.RandomMinMax(4, 6)),new TimerCallback( GetUp));
            }

            if (m_DruidFormGroup == 5)
                Send(Server.Network.SpeedMode.Disabled);

			/*TeiravonRegion m_Region = this.Region as TeiravonRegion;

			if ( LastKiller != null && LastKiller is BaseCreature && m_Region != null && !m_Region.NoPD )
			{
				BaseCreature m_Killer = (BaseCreature)LastKiller;

				if ( ( m_Killer.Level - m_Level ) >= 10 && Utility.Random( 20 ) == 5 )
				{
					SendGump( new PermaDeathGump( this ) );
					ShowPermaDeathGump = true;
					CantWalk = true;
					Squelched = true;
				}
				else if ( ( m_Killer.Level - m_Level ) >= 5 && Utility.Random( 30 ) == 5 )
				{
					SendGump( new PermaDeathGump( this ) );
					ShowPermaDeathGump = true;
					CantWalk = true;
					Squelched = true;
				}
				else if ( m_Killer.Level >= 20 && Utility.Random( 20 ) == 5 )
				{
					SendGump( new PermaDeathGump( this ) );
					ShowPermaDeathGump = true;
					CantWalk = true;
					Squelched = true;
				}
				else if ( ( m_Killer.Level - m_Level ) >= 5 || m_Killer.Level >= 20 )
					SendMessage( "You narrowly escape with your existence..." );
			}*/

			base.OnDeath( c );
		}

		public override void Resurrect()
		{
			base.Resurrect();

			BodyMod = m_OBody;

            if (IsShifted())
            {
                BodyMod = m_DruidForm;
                if (DruidFormGroup == 5)
                {
                    Send(Server.Network.SpeedMode.Run);
                }
            }

			if ( PoisonShotReady )
			{
				if ( SunlightDamage != null )
				{
					SunlightDamage.Stop();
					SunlightDamage = null;
				}

				SunlightDamage = new SunlightDamageTimer( this, TimeSpan.FromSeconds( 30.0 ) );
				SunlightDamage.Start();
			}
            
            if (HasFeat(Feats.FeastoftheDamned))
            { 
                m_Feast = 0;
                NameHue = -1;
            }
		}

		public override void OnAfterResurrect()
		{
			foreach ( Mobile m in GetMobilesInRange( 15 ) )
				if ( !m.Hidden )
					Send( new Network.MobileIncoming( this, m ) );

			base.OnAfterResurrect();
		}

        public override bool OnMoveOver(Mobile m)
        {
            if (Spells.Third.BlessSpell.saerin.Contains(this) && !Spells.Third.BlessSpell.saerin.Contains(m))
                return false;

            return base.OnMoveOver(m);
        }

		public override int HitsMax
		{              
			get
			{           
				int strBase = 0;
				int strOffs = 0;

				strBase = this.Str;
				strOffs = AosAttributes.GetValue( this, AosAttribute.BonusHits );
                
                if (HasFeat(TeiravonMobile.Feats.FeastoftheDamned))
                {
                    strOffs += (int)(Feast * 0.02);
                } 
				return m_MaxHits + strOffs + m_HitsMod;
				// OLD --- return strBase + m_MaxHits + strOffs;
			}
		}

		public override int StamMax
		{
			get
			{
                int stamOffs = 0;
                if (HasFeat(TeiravonMobile.Feats.FeastoftheDamned))
                {
                    stamOffs += (int)(Feast * 0.02);
                } 
				return m_MaxStam + stamOffs + AosAttributes.GetValue( this, AosAttribute.BonusStam );
				// OLD --- return base.StamMax + m_MaxStam + AosAttributes.GetValue( this, AosAttribute.BonusStam );
			}
		}

		public override int ManaMax
		{
			get
			{
                int manaOffs = 0;
                if (HasFeat(TeiravonMobile.Feats.FeastoftheDamned))
                {
                    manaOffs += (int)(Feast * 0.02);
                } 
				return m_MaxMana + manaOffs + AosAttributes.GetValue( this, AosAttribute.BonusMana );
				// OLD --- return base.ManaMax + m_MaxMana + AosAttributes.GetValue( this, AosAttribute.BonusMana );
			}
		}

		public override int GetMinResistance( ResistanceType type )
		{
			int magicResist = ( int )( Skills[SkillName.MagicResist].Value * 10 );
			int min = int.MinValue;

            if (type != ResistanceType.Physical)
            {
                if (magicResist >= 1000)
                    min = 50 + ((magicResist - 1000) / 50);
                else if (magicResist >= 400)
                    min = (magicResist - 400) / 15;
            }

			if ( ( this.IsMage() || this.IsForester() ) && HasFeat( Feats.ElementalResistance ) )
			{
				if ( type == ResistanceType.Cold )
					min += ( int )( PlayerLevel / 2 ) + 5;

				if ( type == ResistanceType.Fire )
					min += ( int )( PlayerLevel / 2 ) + 5;

				if ( type == ResistanceType.Poison )
					min += ( int )( PlayerLevel / 2 ) + 5;

				if ( type == ResistanceType.Energy )
					min += ( int )( PlayerLevel / 2 ) + 5;
			}

			if ( min > MaxPlayerResistance )
				min = MaxPlayerResistance;

			int baseMin = base.GetMinResistance( type );

			if ( min < baseMin )
				min = baseMin;

			return min;
		}

		public override TimeSpan GetLogoutDelay() { return this.AccessLevel == AccessLevel.Player ? TimeSpan.FromSeconds( 45.0 ) : TimeSpan.FromSeconds( 0 ); }

		new public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( OnLogin );
			EventSink.Logout += new LogoutEventHandler( OnLogout );
		}

		//public EncounterTimer Encounters;

		private static void OnLogout( LogoutEventArgs e )
		{
			if ( TeiravonMobile.HearAllPartyChat.IndexOf( ( TeiravonMobile )e.Mobile ) > -1 )
				TeiravonMobile.HearAllPartyChat.Remove( ( TeiravonMobile )e.Mobile );

			//if ( ( ( TeiravonMobile )e.Mobile ).Encounters != null )
			//	( ( TeiravonMobile )e.Mobile ).Encounters.Stop();
		}

		private static void OnLogin( LoginEventArgs e )
		{
			Mobile from = e.Mobile;
			TeiravonMobile m_Player = ( TeiravonMobile )from;

            //Nods
            if (DateTime.Now - Nod_Duration > m_Player.Last_Nod_Paid)
            {
                m_Player.Last_Nod_Paid = DateTime.Now;
                int count = m_Player.Nods.Count;
                int worth = (int)(Math.Pow(count, 2) * 500 * m_Player.PlayerLevel);
                m_Player.Nods.Clear();
                if (worth > 0)
                {
                    m_Player.SendMessage(77, "You have recieved {0} experience from your fellow players in recognition of your actions.", worth);
                    m_Player.m_FromNods += worth;
                    Titles.AwardExp(m_Player, worth, true);
                    CommandLogging.WriteLine(m_Player, "{0}:{1} has recieved {2} exp from Nods", m_Player.Account, m_Player.Name, worth);
                }
            }

            //MOTD
            if (m_Player.IsUndead())
            {
                m_Player.SendMessage(42, "Occido is dead and your immortality is no more, beware, your next death will be your last.");
            }
            if (m_Player.AccessLevel < AccessLevel.Counselor)
            {
                m_Player.SendMessage(52, "Don't forget to use the new ]Nod system to reward good RPers you see.");
            }
            if (m_Player.Faith == Deity.Narindun || m_Player.Faith == Deity.Occido)
                m_Player.Faith = Deity.None;

            if (m_Player.IsCleric() && m_Player.Faith == Deity.None)
                from.SendMessage("Use the Faith command to declare the god you follow.");

            if (m_Player.GetActiveFeats(Feats.HolyAura))
                m_Player.SetActiveFeats(TeiravonMobile.Feats.HolyAura, false);
            if (m_Player.GetActiveFeats(Feats.DarkAura))
                m_Player.SetActiveFeats(TeiravonMobile.Feats.DarkAura, false);

			m_Player.ShowStatus = true;
            m_Player.BAC = 0;

            if (m_Player.HasFeat(Feats.RacialEnemy)){
                m_Player.RemoveFeat(Feats.RacialEnemy);
                m_Player.RacialEnemy = RacialEnemies.None; }

            if (m_Player.IsTinker() && from.Skills.Tinkering.Cap < 100)
                m_Player.Skills.Tinkering.Cap = 100;

            if (m_Player.IsBlacksmith() && from.Skills.Blacksmith.Cap < 100)
                m_Player.Skills.Blacksmith.Cap = 100;

            while (m_Player.LanguageElvenSkill > 116.6)
            {
                m_Player.LanguageElvenSkill -= 16.7;
                m_Player.LanguageElven = true;
                m_Player.PerkPoints += 1;
            }

            while (m_Player.LanguageDrowSkill > 116.6)
            {
                m_Player.LanguageDrowSkill -= 16.7;
                m_Player.LanguageDrow = true;
                m_Player.PerkPoints += 1;
            }

            while (m_Player.LanguageDwarvenSkill > 124.9)
            {
                m_Player.LanguageDwarvenSkill -= 25;
                m_Player.LanguageDwarven = true;
                m_Player.PerkPoints += 1;
            }
            
            while (m_Player.LanguageOrcSkill > 133.5)
            {
                m_Player.LanguageOrcSkill -= 33.6;
                m_Player.LanguageOrc = true;
                m_Player.PerkPoints += 1;
            }

			if ( m_Player.AccessLevel == AccessLevel.Player )
			{
				//m_Player.Encounters = new EncounterTimer( m_Player, TimeSpan.FromSeconds( ( double )EncounterTimer.InitialEncounterDelay ) );
				//m_Player.Encounters.Start();
			}

            if (m_Player.IsMonk())
            {
                Item stance = m_Player.FindItemOnLayer(Layer.Unused_x9);

                if (stance != null)
                    stance.Delete();
                
                m_Player.EquipItem(new MonkFists());
            }

            if (m_Player.IsGoblin())
            {
                if (m_Player.FindItemOnLayer(Layer.FacialHair) is GoboFace)
                {
                    GoboFace face = (GoboFace)m_Player.FindItemOnLayer(Layer.FacialHair);
                    face.Hue = m_Player.Hue;
                }
                if (m_Player.FindItemOnLayer(Layer.FacialHair) is OrcFace)
                {
                    OrcFace face = (OrcFace)m_Player.FindItemOnLayer(Layer.FacialHair);
                    int rank = (int)face.OrcRank;
                    face.Delete();
                    GoboFace goboface = new GoboFace(m_Player);
                    m_Player.EquipItem(goboface);
                    goboface.GoboRank = (GoboFace.Rank)rank;
                }
               
            }
            if (m_Player.IsFrostling())
            {
                m_Player.Skills.Carpentry.Cap = m_Player.Skills.Carpentry.Cap < 40 ? 40 : m_Player.Skills.Carpentry.Cap;
            }
			if ( from.AccessLevel >= AccessLevel.Seer )
			{

				OleDbConnection m_Database = new OleDbConnection( "Provider=Microsoft.Jet.OLEDB.4.0; data source=C:\\Teiravon2\\Applications\\Applications.mdb" );
				OleDbCommand m_SearchString = new OleDbCommand( "SELECT status FROM Applications WHERE status = 0", m_Database );

				try
				{
					m_Database.Open();

					OleDbDataReader m_ApplicationList = m_SearchString.ExecuteReader();

					int num = 0;

					while ( m_ApplicationList.Read() )
					{
						num += 1;
					}

					if ( num > 0 )
						from.SendMessage( "There are {0} applications pending.", num );

					m_ApplicationList.Close();
					m_Database.Close();
				}
				catch
				{

					from.SendMessage( "Error: Unable to open application database. Application file is probably in use." );
					Console.WriteLine( "CRITICAL ERROR: Unable to open application database on administrator login." );

					if ( m_Database.State == ConnectionState.Open )
						m_Database.Dispose();
				}
			}

			if ( from.AccessLevel >= AccessLevel.GameMaster )
			{
				from.SendMessage( Teiravon.Colors.FeatMessageColor, "New commands available: [Expboost <amount>, where amount 500, 5000, 15000, 50000." );
				from.SendMessage( Teiravon.Colors.FeatMessageColor, "[set OBody <bodyvalue>, allows you to use the bodymod function so that you can still access the paperdoll in the new form." );
			}


			if ( m_Player.ShowPermaDeathGump )
				m_Player.SendGump( new PermaDeathGump( m_Player ) );


			m_Player.CheckResistanceBonus();

			if ( m_Player.Mounted )
			{
				int dexb = 0;
				int intb = 0;
				string modName = m_Player.Serial.ToString();
				switch ( m_Player.RidingSkill )
				{
					case 0:
						dexb = -25;
						intb = -25;
						break;
					case 1:
						dexb = -15;
						intb = -15;
						break;
					case 2:
						dexb = -5;
						intb = -5;
						break;
					case 3:
						break;
					case 4:
						dexb = 10;
						intb = 5;
						break;
				}

				if ( dexb != 0 )
					m_Player.AddStatMod( new StatMod( StatType.Dex, modName + "MntDex", dexb, TimeSpan.Zero ) );
				if ( intb != 0 )
					m_Player.AddStatMod( new StatMod( StatType.Int, modName + "MntInt", intb, TimeSpan.Zero ) );
			}

			if ( m_Player.PoisonShotReady )
			{
				if ( m_Player.SunlightDamage != null )
				{
					m_Player.SunlightDamage.Stop();
					m_Player.SunlightDamage = null;
				}

				m_Player.SunlightDamage = new SunlightDamageTimer( m_Player, TimeSpan.FromSeconds( 20.0 ) );
				m_Player.SunlightDamage.Start();
			}


			/* Character Updates */
			/* *************** */

            if (m_Player.IsCrafter() && !m_Player.IsMerchant() &&!m_Player.IsAlchemist() && m_Player.NpcGuild != NpcGuild.MerchantsGuild)
            {
                m_Player.CloseGump(typeof(MerchantGump));
                m_Player.SendGump(new MerchantGump(m_Player));
            }

            if (m_Player.IsThief())
            {

                if (m_Player.HasFeat(Feats.NimbleFingers) || m_Player.HasFeat(Feats.Disarm))
                {
                    
                    m_Player.SendMessage("The Nimble Fingers and Disarm feats has been removed.");

                    Feats[] feats = m_Player.GetFeats();

                    for (int i = 0; i < feats.Length; i++)
                    {
                        if (feats[i] == TeiravonMobile.Feats.NimbleFingers)
                        {
                            feats[i] = TeiravonMobile.Feats.None;
                            m_Player.RemainingFeats += 1;
                        }
                        else if (feats[i] == TeiravonMobile.Feats.Disarm)
                        {
                            feats[i] = TeiravonMobile.Feats.None;
                            m_Player.RemainingFeats += 1;
                        }
                    }

                    m_Player.SetFeats(feats);
                }

                m_Player.SendMessage("Thieves are now scoundrels, but don't worry, scoundrels are way better. Here, have some throwing knives and some tactics cap.");
                m_Player.Skills.Tactics.Cap = m_Player.Skills.Tactics.Cap < 100 ? 100 : m_Player.Skills.Tactics.Cap;
                m_Player.Backpack.AddItem(new ThrowingKnife());
                m_Player.Backpack.AddItem(new ThrowingKnife());
                m_Player.PlayerClass = Class.Scoundrel;
            }

            if (m_Player.MasterWeapon > MasterWeapons.None && (int)m_Player.MasterWeapon < 100)
            {
                m_Player.CloseGump(typeof(Server.Teiravon.Feats.WpnMasterGump));
                m_Player.SendGump(new Server.Teiravon.Feats.WpnMasterGump(m_Player));
            }
            
			/* Information */
			/* *************** */
		}

		public Timer SunlightDamage = null;

		private class SunlightDamageTimer : Timer
		{
			TeiravonMobile m_Drow;
			TimeSpan Next;

			public SunlightDamageTimer( TeiravonMobile drow, TimeSpan duration )
				: base( duration )
			{
				m_Drow = drow;
				Priority = TimerPriority.FiveSeconds;

				Next = TimeSpan.FromSeconds( 10.0 );
			}

			protected override void OnTick()
			{
				bool damagearea = false;
				bool conditions = true;
				bool alive = m_Drow.Alive;

				Regions.TeiravonRegion r = m_Drow.Region as Regions.TeiravonRegion;

				if ( r != null && !r.Dungeon )
					damagearea = true;

				if ( m_Drow.AccessLevel > AccessLevel.Player || m_Drow.Map == Map.Internal )
					conditions = false;

				if ( damagearea && conditions && alive )
				{
					int hours, minutes;
					Clock.GetTime( m_Drow.Map, m_Drow.X, m_Drow.Y, out hours, out minutes );
					bool morning = false;
					bool day = false;
					bool evening = false;
					int mindmg = 0;
					int maxdmg = 0;

					if ( hours >= 5 && hours <= 10 )
					{
						morning = true;
						mindmg = 1;
						maxdmg = 2;
					}
					else if ( hours > 10 && hours < 17 )
					{
						day = true;
						mindmg = 2;
						maxdmg = 6;
					}
					else if ( hours >= 17 && hours <= 22 )
					{
						evening = true;
						mindmg = 1;
						maxdmg = 2;
					}


					if ( morning || day || evening )
					{
						Item shroud = m_Drow.FindItemOnLayer( Layer.OuterTorso );
						bool drowrobe = false;

						if ( shroud is DrowRobe )
						{

							//							if ( shroud.ItemID == 0x2683 )
							//							{
							drowrobe = true;

							if ( Utility.Random( 5 ) == 0 )
								m_Drow.SendMessage( "The shroud of darkness protects you from the rays of the sun." );
							//							}
							//							else
							//							{
							//								if ( Utility.Random( 5 ) == 0 )
							//									m_Drow.SendMessage( "The robe cannot protect you with the hood pulled down." );
							//							}

						}


						if ( !drowrobe )
						{
							Item item;

							item = m_Drow.FindItemOnLayer( Layer.FirstValid );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.TwoHanded );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Shoes );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Pants );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Shirt );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Helm );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Gloves );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Ring );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Neck );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Waist );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.InnerTorso );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Bracelet );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.MiddleTorso );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Earrings );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Arms );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.Cloak );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.OuterTorso );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.OuterLegs );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							item = m_Drow.FindItemOnLayer( Layer.OneHanded );
							if ( item is IDrowEquip )
							{
								IDrowEquip drowitem = ( IDrowEquip )item;
								drowitem.DrowHits -= Utility.RandomMinMax( mindmg, maxdmg );
							}

							/*							m_Drow.Damage( Utility.RandomMinMax( mindmg, maxdmg ) );

														if ( morning )
														{
															if ( Utility.Random( 2 ) == 0 )
															{
																if ( shrouded )
																	m_Drow.SendMessage( "The rays from the rising sun slightly burn you under your hood!" );

																else
																	m_Drow.SendMessage( "The rays from the rising sun burn you!" );
															}
														}
														else if ( day )
														{
															if ( Utility.Random( 3 ) == 0 )
															{
																if ( shrouded )
																	m_Drow.SendMessage( "The scorching sun burns you and your protective shroud!" );
																else
																	m_Drow.SendMessage( "The scorching sun burns you!" );
															}
														}
														else if ( evening )
														{
															if ( Utility.Random( 2 ) == 0 )
															{
																if ( shrouded )
																	m_Drow.SendMessage( "The rays from the setting sun slightly burn you under your hood!" );
																else
																	m_Drow.SendMessage( "The rays from the setting sun burn you!" );
															}
														}*/
						}
					}
				}

				if ( m_Drow.Map != Map.Internal && m_Drow.Alive )
				{
					m_Drow.SunlightDamage = new SunlightDamageTimer( m_Drow, Next );
					m_Drow.SunlightDamage.Start();
				}
				else
					m_Drow.SunlightDamage = null;
			}
		}

		[Constructable]
		public TeiravonMobile()
		{
			Title = "the Human";
			Hue = 0x83ea;

			for ( int i = 0; i < m_Feats.Length; i++ )
			{
				m_Feats[i] = Feats.None;
			}

			for ( int i = 0; i < m_ActiveFeats.Length; i++ )
			{
				m_ActiveFeats[i] = Feats.None;
			}

			for ( int i = 0; i < m_PotionEffect.Length; i++ )
			{
				m_PotionEffect[i] = Server.Items.PotionEffect.None;
			}

			Skills.Focus.Cap = 0.0;
			Skills.Focus.Base = 0.0;
		}


		public void Shapeshift( bool shift, int body )
		{

			if ( shift )
			{
				BodyMod = body;
				m_DruidForm = body;
				CheckResistanceBonus();
			}

			else
			{
				BodyMod = 0;
				m_DruidForm = 0;
				CheckResistanceBonus();

                if (IsGoblin())
                {
                    OBody = 17;
                }
			}
		}


		public ArrayList m_ResistanceMods = new ArrayList();

		public void CheckResistanceBonus()
		{

			try
			{
				for ( int i = 0; i < m_ResistanceMods.Count; i++ )
				{

					if ( m_ResistanceMods[i] == null )
						continue;

					RemoveResistanceMod( ( ResistanceMod )m_ResistanceMods[i] );
				}

				m_ResistanceMods.Clear();

				if ( HasFeat( Feats.PhysicalResistance ) && IsShifted() )
				{
					AddResistBonus( ResistanceType.Physical, PlayerLevel + 10 );
				}
				if ( HasFeat( Feats.ResistPoison ) )
				{
					AddResistBonus( ResistanceType.Poison, PlayerLevel * 2 + 10 );
				}
				if ( HasFeat( Feats.ResistEnergy ) )
				{
					AddResistBonus( ResistanceType.Energy, PlayerLevel * 2 + 10 );
				}
				if ( HasFeat( Feats.ElementalResistance ) )
				{
					AddResistBonus( ResistanceType.Poison, PlayerLevel + 2 );
					AddResistBonus( ResistanceType.Fire, PlayerLevel + 2 );
					AddResistBonus( ResistanceType.Cold, PlayerLevel + 2 );
					AddResistBonus( ResistanceType.Energy, PlayerLevel + 2 );
				}
                if (HasFeat(Feats.BodyOfIron))
                {
                    AddResistBonus(ResistanceType.Physical, (PlayerLevel * 3) + 20);
                    AddResistBonus(ResistanceType.Poison, PlayerLevel + 2);
                    AddResistBonus(ResistanceType.Fire, PlayerLevel + 2);
                    AddResistBonus(ResistanceType.Cold, PlayerLevel + 2);
                    AddResistBonus(ResistanceType.Energy, PlayerLevel + 2);
                }
				if ( HasFeat( Feats.MountedCombat ) && Mounted )
				{
					int resistbonus = 15;

					if ( Mount is WarMount )
						resistbonus += PlayerLevel + 5;

					AddResistBonus( ResistanceType.Physical, resistbonus );
				}
			}
			catch
			{
				SendMessage( "An error occured with the feat resistance bonus script. Please post an exact report of what you were doing in the bug forums." );
				Console.WriteLine( "ERROR: Error adding feat resistance bonuses." );
			}

		}

		public void AddResistBonus( ResistanceType type, int amount )
		{
			ResistanceMod m_ResistanceBonus;
			m_ResistanceBonus = new ResistanceMod( type, amount );

			AddResistanceMod( m_ResistanceBonus );
			m_ResistanceMods.Add( m_ResistanceBonus );
		}

		public TeiravonMobile( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )40 ); // version
            

			writer.Write( m_CanParty );

			writer.Write( m_WarStone );
			writer.Write( ( int )m_Race );
			writer.Write( ( int )m_Class );
			writer.Write( ( int )m_Alignment );
			writer.Write( ( int )m_Exp );
			writer.Write( ( int )m_Level );
			writer.Write( ( int )m_MaxHits );
			writer.Write( ( int )m_MaxStam );
			writer.Write( ( int )m_MaxMana );
			writer.Write( ( int )Hits );
			writer.Write( ( int )Stam );
			writer.Write( ( int )Mana );
			writer.Write( ( string )m_ApprenticeSkill );
			writer.Write( ( bool )m_Magic );
			writer.Write( ( bool )m_ShowStatus );
			writer.Write( ( ulong )m_PlayerSpells );
			writer.Write( ( int )m_LanguagesKnown );
			writer.Write( ( int )m_CurrentLanguage );
			writer.Write( ( double )m_ElvenSkill );
			writer.Write( ( double )m_DrowSkill );
			writer.Write( ( double )m_OrcSkill );
			writer.Write( ( double )m_DwarvenSkill );
			writer.Write( ( double )m_LupineSkill );
			writer.Write( ( double )m_CommonSkill );
			writer.Write( ( bool )m_CelestialSkill );
			writer.Write( ( bool )m_CloakOfDarkness );
			writer.Write( ( double )m_HumanReputation );
			writer.Write( ( double )m_ElfReputation );
			writer.Write( ( double )m_OrcReputation );
			writer.Write( ( double )m_DwarfReputation );
			writer.Write( ( double )m_DrowReputation );
			writer.Write( ( double )m_DragonReputation );
			writer.Write( ( double )m_AnimalReputation );
			writer.Write( ( double )m_DuergarReputation );
			writer.Write( ( int )m_FeatsRemaining );
			writer.Write( ( int )m_Feats.Length );
			writer.Write( ( int )m_ActiveFeats.Length );
			writer.Write( ( int )m_OBody );

			for ( int i = 0; i < m_Feats.Length; i++ )
			{
				writer.Write( ( int )m_Feats[i] );
			}

			for ( int i = 0; i < m_ActiveFeats.Length; i++ )
			{
				writer.Write( ( int )m_ActiveFeats[i] );
			}

			for ( int i = 0; i < m_PotionEffect.Length; i++ )
			{
				writer.Write( ( int )m_PotionEffect[i] );
			}

			if ( IsShapeshifter() || IsForester() )
			{
				int i = 0;

				writer.Write( ( int )m_DruidForm );

				//version 1:
				writer.Write( ( bool )m_ShapeshiftSpecial );
				writer.Write( ( bool )m_ShapeshiftTransformed );
				writer.Write( ( int )m_ShapeshiftHue );
				writer.WriteDeltaTime( m_ShapeshiftSlotDelete );
				for ( i = 0; i < m_ShapeshiftSlotName.Length; i++ ) { writer.Write( ( string )m_ShapeshiftSlotName[i] ); }
				for ( i = 0; i < m_ShapeshiftSlot.Length; i++ ) { writer.Write( ( int )m_ShapeshiftSlot[i] ); }
				for ( i = 0; i < m_ShapeshiftSlotType.Length; i++ ) { writer.Write( ( int )m_ShapeshiftSlotType[i] ); }
				for ( i = 0; i < m_ShapeshiftSlotHue.Length; i++ ) { writer.Write( ( int )m_ShapeshiftSlotHue[i] ); }
				//
			}

			if ( IsArcher() || IsRanger() )
			{
				writer.Write( m_DisarmShotReady );
				writer.Write( m_StunShotReady );
				writer.Write( m_PoisonShotReady );
				writer.Write( m_ShadowShotReady );
				writer.Write( m_CripplingShotReady );
				writer.Write( m_FatalShotReady );
				writer.Write( m_ChargedMissileReady );

				//version 1
				//writer.Write( (string)m_RacialEnemy );
				//writer.Write( (string)m_ExtraRacialEnemy );

				//version 2
				writer.Write( ( int )m_RaceEnemy );
				writer.Write( ( int )m_ExtraRaceEnemy );
			}

			if ( IsWoodworker() )
			{
				writer.Write( m_NextFletcherBulkOrder );
			}

			if ( IsCavalier() )
			{
				writer.Write( m_WarMountDeaths );
			}

			if ( IsFighter() || IsBerserker() || IsRavager() )
			{
				writer.Write( ( int )m_MasterWep );
			}

			//version 4
			writer.Write( ( int )m_perkpoints );
			writer.Write( ( int )m_ridingskill );
			writer.Write( ( int )m_farmingskill );
			writer.Write( ( int )m_teachingskill );
			writer.Write( m_nextteach );
			writer.Write( m_nextlearn );
			writer.Write( m_farmcrops );
			writer.Write( m_tavteachskill );
			writer.Write( ( int )m_EncounterMode );

            writer.Write((int)m_Feast);

            writer.Write((int)m_Diety);

            writer.Write((int)m_HitsMod);

            writer.Write((int)m_ExpertMining);
            writer.Write((int)m_ExpertSkinning);
            writer.Write((int)m_ExpertWoodsman);

            //Nods
            writer.Write((bool)m_CantNod);

            writer.Write((int)Nods.Keys.Count);

            foreach (string a in Nods.Keys)
                {
                    writer.Write((String)a);
                }
            writer.Write((DateTime)Last_Nod_Paid);
            writer.Write((int)m_FromNods);
		}

        public override int GetMaxResistance(ResistanceType type)
        {
            int value = 70;
            if (this.PlayerLevel > 20)
                value += (PlayerLevel - 20) * 2;
            return value;
        }
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if (version < 25)
                m_Ambush = false;
            if (version < 24)
            {
                Skills.Ninjitsu.Cap = 60.0;

                switch (m_Class)
                {
                    case Class.Shapeshifter:
                    case Class.Thief:
                    case Class.Bard:
                    case Class.Assassin:
                    case Class.Monk:
                        Skills.Ninjitsu.Cap = 100.0;
                        break;
                    case Class.Archer:
                    case Class.Berserker:
                        Skills.Ninjitsu.Cap = 80.0;
                        break;
                }
            }

			if ( version >= 9 )
			{
				m_CanParty = reader.ReadBool();
			}

			m_WarStone = ( WarStone )reader.ReadItem();
			m_Race = ( Race )reader.ReadInt();
			m_Class = ( Class )reader.ReadInt();
			m_Alignment = ( Alignment )reader.ReadInt();
			m_Exp = reader.ReadInt();
			m_Level = reader.ReadInt();
			m_MaxHits = reader.ReadInt();
			m_MaxStam = reader.ReadInt();
			m_MaxMana = reader.ReadInt();
			Hits = reader.ReadInt();
			Stam = reader.ReadInt();
			Mana = reader.ReadInt();
			m_ApprenticeSkill = reader.ReadString();
			m_Magic = reader.ReadBool();
			m_ShowStatus = reader.ReadBool();
			m_PlayerSpells = reader.ReadULong();
			m_LanguagesKnown = reader.ReadInt();
			m_CurrentLanguage = reader.ReadInt();
			m_ElvenSkill = reader.ReadDouble();
			m_DrowSkill = reader.ReadDouble();
			m_OrcSkill = reader.ReadDouble();
			m_DwarvenSkill = reader.ReadDouble();
			m_LupineSkill = reader.ReadDouble();
			m_CommonSkill = reader.ReadDouble();
			m_CelestialSkill = reader.ReadBool();
			m_CloakOfDarkness = reader.ReadBool();
			m_HumanReputation = reader.ReadDouble();
			m_ElfReputation = reader.ReadDouble();
			m_OrcReputation = reader.ReadDouble();
			m_DwarfReputation = reader.ReadDouble();
			m_DrowReputation = reader.ReadDouble();
			m_DragonReputation = reader.ReadDouble();
			m_AnimalReputation = reader.ReadDouble();
			m_DuergarReputation = reader.ReadDouble();
			m_FeatsRemaining = reader.ReadInt();
			int temp = reader.ReadInt();
			int activetemp = reader.ReadInt();
			m_OBody = reader.ReadInt();
			BodyMod = m_OBody;

			for ( int i = 0; i < temp; i++ )
			{
                Feats feat = ( Feats )reader.ReadInt();
                if (!HasFeat(feat))
                    m_Feats[i] = feat;
                else if (feat != Feats.None)
                {        
                    Console.WriteLine("Removing Redundant Feat '{0}' from player '{1}'", feat, Name);
                }
                    
			}

			for ( int i = 0; i < activetemp; i++ )
			{
				m_ActiveFeats[i] = ( Feats )reader.ReadInt();
			}

			for ( int i = 0; i < m_PotionEffect.Length; i++ )
			{
				m_PotionEffect[i] = ( Server.Items.PotionEffect )reader.ReadInt();
			}



			if ( IsShapeshifter() || IsForester() )
			{
				int i = 0;

				if ( version >= 1 )
				{
                    int x = version > 17 ? 10 : 5;
					m_DruidForm = reader.ReadInt();

					m_ShapeshiftSpecial = reader.ReadBool();
					m_ShapeshiftTransformed = reader.ReadBool();
					m_ShapeshiftHue = reader.ReadInt();
					m_ShapeshiftSlotDelete = reader.ReadDeltaTime();
					for ( i = 0; i < x; i++ ) { m_ShapeshiftSlotName[i] = reader.ReadString(); }
					for ( i = 0; i < x; i++ ) { m_ShapeshiftSlot[i] = reader.ReadInt(); }
					for ( i = 0; i < x; i++ ) { m_ShapeshiftSlotType[i] = reader.ReadInt(); }
					for ( i = 0; i < x; i++ ) { m_ShapeshiftSlotHue[i] = reader.ReadInt(); }

					if ( IsShifted() && !m_ShapeshiftTransformed )
						m_DruidForm = 0;

					if ( !IsShifted() && m_ShapeshiftTransformed )
					{
							NewShapeshiftGump.ShapeshiftFunctions( false, 1, this );
					}

					if ( IsShifted() )
					{
						BodyMod = m_DruidForm;
					}

                    if (version < 20 && HasFeat(Feats.WildShape))
                    {
                        SendMessage("Due to changes to the Wild Shape feat your shapeshifting forms have been reset.");
                        m_ShapeshiftSlotName = new string[10];
		                m_ShapeshiftSlot = new int[10];
		                m_ShapeshiftSlotType = new int[10];
		                m_ShapeshiftSlotHue = new int[10];
                    }
				}
			}


			if ( IsArcher() || IsRanger() )
			{
				m_DisarmShotReady = reader.ReadBool();
				m_StunShotReady = reader.ReadBool();
				m_PoisonShotReady = reader.ReadBool();
				m_ShadowShotReady = reader.ReadBool();
				m_CripplingShotReady = reader.ReadBool();
				m_FatalShotReady = reader.ReadBool();
				m_ChargedMissileReady = reader.ReadBool();


				if ( version <= 1 )
				{
					m_RacialEnemy = reader.ReadString();
					m_ExtraRacialEnemy = reader.ReadString();
				}

				if ( version >= 2 )
				{
					m_RaceEnemy = ( RacialEnemies )reader.ReadInt();
					m_ExtraRaceEnemy = ( RacialEnemies )reader.ReadInt();
				}
			}

			if ( ( IsWoodworker() && version > 9 ) || IsBowyer() )
			{
				m_NextFletcherBulkOrder = reader.ReadDateTime();
			}

			if ( IsCavalier() )
			{
				m_WarMountDeaths = reader.ReadInt();
			}

			if ( IsFighter() || IsBerserker() || IsRavager())
			{
                if (IsFighter() || (IsBerserker() && version > 33) || (IsRavager() && version > 38))
					m_MasterWep = ( MasterWeapons )reader.ReadInt();

			}

			if ( version < 4 )
			{
				m_perkpoints = this.PlayerLevel;
				if ( IsCavalier() )
					m_ridingskill = 2;
				else if ( IsRanger() || IsForester() || IsShapeshifter() )
					m_ridingskill = 1;
				else
					m_ridingskill = 0;
				m_farmingskill = 0;
				m_teachingskill = 0;
				m_nextteach = DateTime.Now;
				m_nextlearn = DateTime.Now;

			}
			else
			{
				m_perkpoints = reader.ReadInt();
				m_ridingskill = reader.ReadInt();
				m_farmingskill = reader.ReadInt();
				m_teachingskill = reader.ReadInt();
				m_nextteach = reader.ReadDateTime();
				m_nextlearn = reader.ReadDateTime();
			}

			if ( version < 6 )
				m_farmcrops = 0;
			else
				m_farmcrops = reader.ReadInt();

			if ( version < 7 )
				m_tavteachskill = 999;
			else
				m_tavteachskill = reader.ReadInt();

			if ( version < 8 )
				m_EncounterMode = EncounterModes.AlwaysEncounter;
			else
				m_EncounterMode = ( EncounterModes )reader.ReadInt();

            if (version < 11)
                m_Feast = 0;
            else
                m_Feast = reader.ReadInt();


            if (version < 12)
                m_Diety = Deity.None;
            else
                m_Diety = (Deity)reader.ReadInt();

            if (version < 13)
                m_HitsMod = 0;
            else
                m_HitsMod = reader.ReadInt();

			CheckResistanceBonus();

            if ((IsElf() || IsDrow()) && version < 14)
            {
                MaxHits += PlayerLevel * 2;
                RawStr += PlayerLevel * 2;
                SendMessage("Due to changes to your race's stat-pool, you have been awarded {0} str and hp.", (PlayerLevel * 2));
            }

            if (version < 15)
            {

                m_ExpertMining = 0;
                m_ExpertSkinning = 0;
                m_ExpertWoodsman = 0;
            }
            else
            {
                m_ExpertMining = reader.ReadInt();
                m_ExpertSkinning = reader.ReadInt();
                m_ExpertWoodsman = reader.ReadInt();
            }

            if (version < 16)
            {
                m_CantNod = false;
                Nods = new Hashtable();
                Last_Nod_Paid = DateTime.Now;
                m_FromNods = 0;
            }
            else
            {
                m_CantNod = reader.ReadBool();

                int length = reader.ReadInt();
                Nods = new Hashtable();

                for (int i = 0; i < length; i++)
                {
                    string s = reader.ReadString();
                    Nods.Add(s, s);
                }

                
                Last_Nod_Paid = reader.ReadDateTime();
                m_FromNods = reader.ReadInt();
            }
            if (!IsOrc() && !IsDwarf() && !IsFrostling() && version < 19)
            {
                MaxHits += PlayerLevel * 2;
                SendMessage("Due to changes to your race's stat-pool, you have been awarded {0} max hp.", (PlayerLevel * 2));
            }

            if (version < 21 && PlayerLevel > 20)
            {
                PerkPoints += (PlayerLevel - 20);
                SendMessage("You have gained {0} additional perk points for your epic level.", (PlayerLevel - 20));
            }

            if (version < 27 && (IsCleric() || IsDarkCleric()))
            {
                m_MaxMana -= (1 * PlayerLevel);
            }

            if (version < 28 && (HasFeat(Feats.LeapOfClouds)))
            {
                RemoveFeat(Feats.LeapOfClouds);
                RemainingFeats += 1;
                SendMessage("Leap of Clouds feat has been removed, your feat point has been refunded.");
            }

            if (version < 32 && (HasFeat(Feats.BarbarianInstinct)) && IsBerserker() || IsSavage())
            {
                RemoveFeat(Feats.BarbarianInstinct);
                RemainingFeats += 1;
                SendMessage("Dude to changes to Berserker Rage, your Barbarian Focus feat has been refunded. Rage will no longer consume stamina.");
                Skills.Focus.Cap = 40.0;
                Skills.Focus.Base = Skills.Focus.Base > 40.0 ? 40.0 : Skills.Focus.Base;
            }
            if (((IsBerserker() || IsSavage()) && Skills.Focus.Cap < 40.0))
            {
                
                        Skills.Focus.Cap = 40.0;
                        Skills.Focus.Base = Skills.Focus.Base > 40.0 ? 40.0 : Skills.Focus.Base;
            }
            if (version < 33 && IsMage())
            {
                zCastGateTravel = true;
            }
            if (version < 29 && IsGnome())
                if (Skills.Tinkering.Cap < 100 && Skills.Tinkering.Cap > 40)
                    Skills.Tinkering.Cap += 40;
			//rw3: Temporary functions for fixing bugs

            if (version < 35 && ( IsScoundrel() || IsThief() || IsBard() || IsAssassin()))
            {
                SendMessage("Rogue strength gain has been increased.");
                Str += ( PlayerLevel-1 );
            }

            if (version < 36 && ( !IsHuman()))
            {
                RemainingFeats++;
            }

            if (version < 36 && (IsHuman()))
            {
                SkillsCap+= 100;
            }
            if (version < 37 && (IsRavager()))
            {
                int newstr = 25 + ( 6 * (PlayerLevel - 1 ));
                if (RawStr < newstr){
                    RawStr = newstr;
                    SendMessage("Due to changes to your classes stat gain rate, your str has been raised.");
                }
                if (Skills.Tracking.Cap < 100.0)
                    Skills.Tracking.Cap = 100.0;
            }
			if ( IsOrc() && OBody != 0 )
			{
				OBody = 0;
				BodyMod = 0;
			}
            if (IsGoblin() && OBody != 17)
            {
                OBody = 17;
            }

            if (HasFeat(Feats.SkilledGatherer))
            {
                PerkPoints += 5;
                SendMessage("The Skilled Gatherer Feat has been disabled and replaced with Perks, you have been refunded 5 Perk Points as compensations.");

                Feats[] feats = GetFeats();

                for (int i = 0; i < feats.Length; i++)
                {
                    if (feats[i] == TeiravonMobile.Feats.SkilledGatherer)
                        feats[i] = TeiravonMobile.Feats.None;
                }

                SetFeats(feats);
            }
            if (version < 38 && HasFeat(Feats.AdvancedStealth))
            {
                SendMessage("Due to changes to stealth and detection, the advanced stealth feat has been refunded, if you decide its new function is still satisfactory feel free to retake the feat.");
                RemoveFeat(Feats.AdvancedStealth);
                RemainingFeats += 1;
            }
            if (Skills.Hiding.Cap > Skills.Stealth.Cap)
            {
                Skills.Stealth.Cap = Skills.Hiding.Cap;
                Skills.Hiding.Cap = 0;
            }
            if (Skills.Hiding.Base > Skills.Stealth.Base)
            {
                Skills.Stealth.Base = Skills.Hiding.Base;
                Skills.Hiding.Base = 0;
            }

            if (HasFeat(Feats.TrapLore))
            {
                Feats[] feats = GetFeats();

                for (int i = 0; i < feats.Length; i++)
                {
                    if (feats[i] == Feats.TrapLore)
                    {
                        feats[i] = TeiravonMobile.Feats.None;
                        RemainingFeats += 1;
                        Console.WriteLine("{0}: Trap Lore has been removed.", this.Name);
                    }
                }

                SetFeats(feats);
            }
            if (HasFeat(Feats.Dodge))
            {
                Feats[] feats = GetFeats();

                for (int i = 0; i < feats.Length; i++)
                {
                    if (feats[i] == Feats.Dodge)
                    {
                        feats[i] = TeiravonMobile.Feats.None;
                        RemainingFeats += 1;
                        Console.WriteLine("{0}: Dodge has been removed.", this.Name);
                    }
                }

                SetFeats(feats);
            }

            if (HasFeat(Feats.RacialEnemy))
            {
                Feats[] feats = GetFeats();
                bool hasOnce = false;
                for (int i = 0; i < feats.Length; i++)
                {
                    if (feats[i] == Feats.RacialEnemy)
                    {
                        feats[i] = TeiravonMobile.Feats.None;
                        hasOnce = true;
                        Console.WriteLine("{0}: Racial Enemy has been removed.", this.Name);
                    }
                }
                if (hasOnce)
                    RemainingFeats += 1;

                SetFeats(feats);
            }

            if (HasFeat(Feats.ExtraRacialEnemy))
            {
                Feats[] feats = GetFeats();

                for (int i = 0; i < feats.Length; i++)
                {
                    if (feats[i] == Feats.ExtraRacialEnemy)
                    {
                        feats[i] = TeiravonMobile.Feats.None;
                        RemainingFeats += 1;
                        Console.WriteLine("{0}: Extra Racial Enemy has been removed.", this.Name);
                    }
                }

                SetFeats(feats);
            }

			if ( HasFeat( Feats.ContractKilling ) )
			{
				Feats[] feats = GetFeats();

				for ( int i = 0; i < feats.Length; i++ )
				{
					if ( feats[i] == Feats.ContractKilling )
					{
						feats[i] = TeiravonMobile.Feats.None;
						RemainingFeats += 1;
						Console.WriteLine( "{0}: Contract Killing has been removed.", this.Name );
					}
				}

				SetFeats( feats );
			}
			// Fix Language problem			
			if ( version < 5 )
			{
				if ( !IsElf() && this.AccessLevel == AccessLevel.Player )
				{
					int tmplang = ( int )( LanguageElvenSkill / 30 );
					if ( tmplang > 0 )
					{
						LanguageElvenSkill -= tmplang * 30;
						PerkPoints += tmplang;
						LanguageElven = false;
						Console.WriteLine( "Elven skill reduced by {0} for {1}", tmplang, this.Name );
					}
				}

				if ( !IsDrow() && this.AccessLevel == AccessLevel.Player )
				{
					int tmplang = ( int )( LanguageDrowSkill / 30 );
					if ( tmplang > 0 )
					{
						LanguageDrowSkill -= tmplang * 30;
						PerkPoints += tmplang;
						LanguageDrow = false;
						Console.WriteLine( "Drow skill reduced by {0} for {1}", tmplang, this.Name );
					}
				}

				if ( !IsDwarf() && this.AccessLevel == AccessLevel.Player )
				{
					int tmplang = ( int )( LanguageDwarvenSkill / 30 );
					if ( tmplang > 0 )
					{
						LanguageDwarvenSkill -= tmplang * 30;
						PerkPoints += tmplang;
						LanguageDwarven = false;
						Console.WriteLine( "Dwarven skill reduced by {0} for {1}", tmplang, this.Name );
					}
				}

				if ( !IsOrc() && this.AccessLevel == AccessLevel.Player )
				{
					int tmplang = ( int )( LanguageOrcSkill / 30 );
					if ( tmplang > 0 )
					{
						LanguageOrcSkill -= tmplang * 30;
						PerkPoints += tmplang;
						LanguageOrc = false;
						Console.WriteLine( "Orc skill reduced by {0} for {1}", tmplang, this.Name );
					}
				}

				if ( this.AccessLevel == AccessLevel.Player )
				{
					int tmplang = ( int )( LanguageLupineSkill / 30 );
					if ( tmplang > 0 )
					{
						LanguageLupineSkill -= tmplang * 30;
						PerkPoints += ( int )tmplang;
						LanguageLupine = false;
						Console.WriteLine( "Lupine skill reduced by {0} for {1}", tmplang, this.Name );
					}
				}

			}

			if ( PerkPoints < 0 )
			{
				Console.WriteLine( "{0} has {1} perk points", this.Name, PerkPoints );
			}

			//End Language Fix
			// Turn off magic lock spell
			zCastMagicLock = false;

            if (version < 40 && IsStrider())
            {
                Skills.Magery.Cap = Math.Min(Skills.Magery.Cap, 60.0);
                Skills.Magery.Base =  Math.Min(Skills.Magery.Base, 20.0);
                Magicable = true;

                SetSpell(Teiravon.Spells.HealSpell, true);
                SetSpell(Teiravon.Spells.CureSpell, true);
                SetSpell(Teiravon.Spells.BlessSpell, true);

                Faith = Deity.Valar;
            }
		}

		#region Command Properties
		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowStatus
		{
			get { return ( m_ShowStatus ); }
			set { m_ShowStatus = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int WarMountDeaths
		{
			get { return m_WarMountDeaths; }
			set { m_WarMountDeaths = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RemainingFeats
		{
			get { return ( m_FeatsRemaining ); }
			set { m_FeatsRemaining = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double LanguageElvenSkill
		{
			get { return ( m_ElvenSkill ); }
			set { m_ElvenSkill = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double LanguageDrowSkill
		{
			get { return ( m_DrowSkill ); }
			set { m_DrowSkill = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double LanguageDwarvenSkill
		{
			get { return ( m_DwarvenSkill ); }
			set { m_DwarvenSkill = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double LanguageOrcSkill
		{
			get { return ( m_OrcSkill ); }
			set { m_OrcSkill = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double LanguageLupineSkill
		{
			get { return ( m_LupineSkill ); }
			set { m_LupineSkill = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double LanguageCommonSkill
		{
			get { return ( m_CommonSkill ); }
			set { m_CommonSkill = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LanguageCelestialSkill
		{
			get { return ( m_CelestialSkill ); }
			set { m_CelestialSkill = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastAgility
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.AgilitySpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.AgilitySpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastAirElemental
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.AirElementalSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.AirElementalSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastArchCure
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ArchCureSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ArchCureSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastArchProtect
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ArchProtectionSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ArchProtectionSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastBless
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.BlessSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.BlessSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastChainLight
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ChainLightningSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ChainLightningSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastClumsy
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ClumsySpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ClumsySpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastCreateFood
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.CreateFoodSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.CreateFoodSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastCunning
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.CunningSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.CunningSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastCure
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.CureSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.CureSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastCurse
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.CurseSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.CurseSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastDispel
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.DispelSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.DispelSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastDispelField
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.DispelFieldSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.DispelFieldSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastEarthElem
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.EarthElementalSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.EarthElementalSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastEarthquake
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.EarthquakeSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.EarthquakeSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastEnergyBolt
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.EnergyBoltSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.EnergyBoltSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastEnergyField
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.EnergyFieldSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.EnergyFieldSpell, value ); }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastEVortex
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.EnergyVortexSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.EnergyVortexSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastExplosion
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ExplosionSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ExplosionSpell, value ); }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastFeeblemind
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.FeeblemindSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.FeeblemindSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastFireElem
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.FireElementalSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.FireElementalSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastFireField
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.FireFieldSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.FireFieldSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastFlamestrike
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.FlamestrikeSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.FlamestrikeSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastGateTravel
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.GateTravelSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.GateTravelSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastGreaterHeal
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.GreaterHealSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.GreaterHealSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastHarm
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.HarmSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.HarmSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastHeal
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.HealSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.HealSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastIncognito
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.IncognitoSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.IncognitoSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastInvis
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.InvisibilitySpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.InvisibilitySpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastLightning
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.LightningSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.LightningSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMagicArrow
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MagicArrowSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MagicArrowSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMagicLock
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MagicLockSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MagicLockSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMagicRef
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MagicReflectSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MagicReflectSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMagicTrap
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MagicTrapSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MagicTrapSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastManaDrain
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ManaDrainSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ManaDrainSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastManaV
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ManaVampireSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ManaVampireSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMark
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MarkSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MarkSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMassCurse
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MassCurseSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MassCurseSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMassDispel
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MassDispelSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MassDispelSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMeteor
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MeteorSwarmSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MeteorSwarmSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastMindBlast
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.MindBlastSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.MindBlastSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastNightSight
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.NightSightSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.NightSightSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastParaField
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ParalyzeFieldSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ParalyzeFieldSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastParalyze
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ParalyzeSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ParalyzeSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastPoisonField
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.PoisonFieldSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.PoisonFieldSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastPoison
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.PoisonSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.PoisonSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastPolymorph
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.PolymorphSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.PolymorphSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastProtection
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ProtectionSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ProtectionSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastRA
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ReactiveArmorSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ReactiveArmorSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastRecall
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.RecallSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.RecallSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastRemoveTrap
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.RemoveTrapSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.RemoveTrapSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastResurrect
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.ResurrectionSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.ResurrectionSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastReveal
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.RevealSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.RevealSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastSpiritSpeak
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.SpiritSpeakSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.SpiritSpeakSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastStrength
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.StrengthSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.StrengthSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastCreature
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.SummonCreatureSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.SummonCreatureSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastDaemon
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.SummonDaemonSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.SummonDaemonSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastTelekinesis
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.TelekinesisSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.TelekinesisSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastTeleport
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.TeleportSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.TeleportSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastUnlock
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.UnlockSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.UnlockSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastWallOfStone
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.WallOfStoneSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.WallOfStoneSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastWaterElem
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.WaterElementalSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.WaterElementalSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool zCastWeaken
		{
			get { if ( ( m_PlayerSpells & Teiravon.Spells.WeakenSpell ) != 0 ) { return true; } return false; }
			set { SetSpell( Teiravon.Spells.WeakenSpell, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Magicable
		{
			get { return m_Magic; }
			set { m_Magic = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PlayerLevel
		{
			get { return m_Level; }
			set { m_Level = value; CheckResistanceBonus(); }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public int CheckPlayerLevel
		{
			get { return m_Level; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PlayerExp
		{
			get { return m_Exp; }
			set { m_Exp = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Alignment PlayerAlignment
		{
			get { return m_Alignment; }
			set { m_Alignment = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Race PlayerRace
		{
			get { return m_Race; }
			set { m_Race = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Class PlayerClass
		{
			get { return m_Class; }
			set { m_Class = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Class CheckPlayerClass
		{
			get { return m_Class; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LanguageLupine
		{
			get { return ( ( m_LanguagesKnown & LLupine ) != 0 ); }
			set
			{
				if ( value )
				{
					m_LanguagesKnown |= LLupine;
				}
				else
				{
					m_LanguagesKnown &= ~LLupine;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LanguageDwarven
		{
			get { return ( ( m_LanguagesKnown & LDwarven ) != 0 ); }
			set
			{
				if ( value )
				{
					m_LanguagesKnown |= LDwarven;
				}
				else
				{
					m_LanguagesKnown &= ~LDwarven;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LanguageOrc
		{
			get { return ( ( m_LanguagesKnown & LOrc ) != 0 ); }
			set
			{
				if ( value )
				{
					m_LanguagesKnown |= LOrc;
				}
				else
				{
					m_LanguagesKnown &= ~LOrc;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LanguageDrow
		{
			get { return ( ( m_LanguagesKnown & LDrow ) != 0 ); }
			set
			{
				if ( value )
				{
					m_LanguagesKnown |= LDrow;
				}
				else
				{
					m_LanguagesKnown &= ~LDrow;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LanguageElven
		{
			get { return ( ( m_LanguagesKnown & LElven ) != 0 ); }
			set
			{
				if ( value )
				{
					m_LanguagesKnown |= LElven;
				}
				else
				{
					m_LanguagesKnown &= ~LElven;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LanguageCommon
		{
			get { return ( ( m_LanguagesKnown & LCommon ) != 0 ); }
			set
			{
				if ( value )
				{
					m_LanguagesKnown |= LCommon;
				}
				else
				{
					m_LanguagesKnown &= ~LCommon;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxHits
		{
			get { return ( m_MaxHits ); }
			set { m_MaxHits = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxMana
		{
			get { return ( m_MaxMana ); }
			set { m_MaxMana = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxStam
		{
			get { return ( m_MaxStam ); }
			set { m_MaxStam = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextFletcherBulkOrder
		{
			get
			{
				TimeSpan ts = m_NextFletcherBulkOrder - DateTime.Now;

				if ( ts < TimeSpan.Zero )
					ts = TimeSpan.Zero;

				return ts;
			}
			set
			{
				try { m_NextFletcherBulkOrder = DateTime.Now + value; }
				catch { }
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WarStone Town
		{
			get { return m_WarStone; }
			set { m_WarStone = value; }
		}
		#endregion

		private class PermaDeathGump : Gump
		{
			private TeiravonMobile m_Player;

			public PermaDeathGump( TeiravonMobile m )
				: base( 175, 92 )
			{
				m_Player = m;

				Closable = false;
				Dragable = false;
				Disposable = false;
				Resizable = false;

				AddPage( 0 );
				AddBackground( 70, 80, 280, 245, 3600 );
				AddBackground( 95, 125, 230, 175, 9350 );

				AddHtml( 125, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Choose your fate...</basefont>", false, false );
				AddLabel( 110, 135, 150, "You've suffered a serious blow, and with your last breath of life you:" );

				AddLabel( 160, 170, 150, "Offer a prayer to the gods to save you. ( 100% EXP Loss )" );
				AddButton( 136, 173, 2224, 2224, 1, GumpButtonType.Reply, 0 );

				AddLabel( 160, 200, 150, "Cease to exist. You may delete and recreate in 21 days." );
				AddButton( 136, 203, 2224, 2224, 2, GumpButtonType.Reply, 0 );
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( info.ButtonID == 1 )
				{
					m_Player.PlayerExp = 0;
					m_Player.CantWalk = false;
					m_Player.ShowPermaDeathGump = false;
					m_Player.Squelched = false;

					m_Player.SendMessage( "The gods answer your plea, and grant you another chance." );
					m_Player.SendMessage( "Your experience is now zero." );
					m_Player.SendMessage( "Maybe you shouldn't be attacking NPCs so far above your level." );

					m_Player.Resurrect();
				}
				else
				{
					m_Player.Name = "Dead Character";
					m_Player.Map = Map.Internal;
					m_Player.NetState.Dispose();
				}
			}
		}
	}
}