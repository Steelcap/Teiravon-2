using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Items;

namespace Server.Teiravon
{
    class TAVShiftUtilities
    {
        public class ShiftInfo
        {
            private int m_ShiftID;
            private string m_Name;
            private double m_StrScale;
            private double m_DexScale;
            private double m_IntScale;
            private double m_DamageScale;
            private double m_SwingSpeedScale;
            private double m_ArmorScale;
            private double m_HitScale;
            private double m_StamScale;
            private double m_ManaScale;
            private double m_HitRegen;
            private double m_StamRegen;
            private double m_ManaRegen;
            private int m_WeapID;
            private WeaponAbility m_Primary;
            private WeaponAbility m_Secondary;

            public ShiftInfo(int shiftID, string name, double strScale, double dexScale, double intScale, double damage, double speed, double armor, double hits, double stam, double mana, double hitRegen, double stamRegen, double manaRegen, int weapID, WeaponAbility primary, WeaponAbility secondary)
            {
                m_ShiftID = shiftID;
                m_Name = name;
                m_StrScale = strScale;
                m_DexScale = dexScale;
                m_IntScale = intScale;
                m_DamageScale = damage;
                m_SwingSpeedScale = speed;
                m_ArmorScale = armor;
                m_HitScale = hits;
                m_StamScale = stam;
                m_ManaScale = mana;
                m_HitRegen = hitRegen;
                m_StamRegen = stamRegen;
                m_ManaRegen = manaRegen;
                m_WeapID = weapID;
                m_Primary = primary;
                m_Secondary = secondary;
            }

            public int ShiftID
            {
                get
                {
                    return m_ShiftID;
                }
            }
            public string Name
            {
                get
                {
                    return m_Name;
                }
                set
                {
                    m_Name = value;
                }
            }
            public double StrScale
            {
                get
                {
                    return m_StrScale;
                }
                set
                {
                    m_StrScale = value;
                }
            }
            public double DexScale
            {
                get
                {
                    return m_DexScale;
                }
                set
                {
                    m_DexScale = value;
                }
            }
            public double IntScale
            {
                get
                {
                    return m_IntScale;
                }
                set
                {
                    m_IntScale = value;
                }
            }
            public double DamageScale
            {
                get
                {
                    return m_DamageScale;
                }
                set
                {
                    m_DamageScale = value;
                }
            }
            public double ArmorScale
            {
                get
                {
                    return m_ArmorScale;
                }
                set
                {
                    m_ArmorScale = value;
                }
            }
            public double SpeedScale
            {
                get
                {
                    return m_SwingSpeedScale;
                }
                set
                {
                    m_SwingSpeedScale = value;
                }
            }
            public double HpScale
            {
                get
                {
                    return m_HitScale;
                }
                set
                {
                    m_HitScale = value;
                }
            }
            public double StamScale
            {
                get
                {
                    return m_StamScale;
                }
                set
                {
                    m_StamRegen = value;
                }
            }
            public double ManaScale
            {
                get
                {
                    return m_ManaScale;
                }
                set
                {
                    m_ManaRegen = value;
                }
            }
            public double HpRegen
            {
                get
                {
                    return m_HitRegen;
                }
                set
                {
                    m_HitScale = value;
                }
            }
            public double StamRegen
            {
                get
                {
                    return m_StamRegen;
                }
                set
                {
                    m_StamRegen = value;
                }
            }
            public double ManaRegen
            {
                get
                {
                    return m_ManaRegen;
                }
                set
                {
                    m_ManaRegen = value;
                }
            }
            public int WeapID
            {
                get { return m_WeapID; }
                set { m_WeapID = value; }
            }
            public WeaponAbility PrimaryAbility
            {
                get { return m_Primary; }
                set { m_Primary = value; }
            }
            public WeaponAbility SecondaryAbility
            {
                get { return m_Secondary; }
                set { m_Secondary = value; }
            }

            private static ShiftInfo[] m_Table = new ShiftInfo[12]
			{
                //          index    name       str     dex     int    damage  speed   armor    hits    stam    mana    regen   sregen   mregen  WeaponItemID       Primary             Secondary
                new ShiftInfo(0,    "Avis",      .7,     1.2,  3.0,     .3,     1.4,    1.0,    1.2,    1.2,    2.5,    0.05,     .15,       .2,       0xF62,   WeaponAbility.ArmorIgnore,   WeaponAbility.ArmorIgnore),
                new ShiftInfo(1,    "Canis",     1.7,    2.2,  1.0,     .54,    1.8,    1.8,    2.4,    2.2,    0.5,    0.1,      .10,        0,      0x13B6,   WeaponAbility.DoubleStrike,   WeaponAbility.DoubleStrike),
                new ShiftInfo(2,    "Felis",     1.5,    2.5,  1.2,     .42,    2.2,    1.7,    1.28,    2.5,    0.7,    0.1,      .15,        0,      0x1441,  WeaponAbility.BleedAttack,   WeaponAbility.BleedAttack),
                new ShiftInfo(3,    "Ursine",    3.5,    1.5,   .4,     .66,    1.0,    2.2,    4.68,    2.5,    0.5,    0.3,      .30,        0,       0xF49,  WeaponAbility.CrushingBlow,   WeaponAbility.CrushingBlow),
                new ShiftInfo(4,    "Ungulate",  2.5,     .5,   .5,     .18,    0.5,    2.5,    6.6,    3.5,    0.0,    0.05,     .30,        0,      0x13B4,   WeaponAbility.Dismount,   WeaponAbility.Dismount),
                new ShiftInfo(5,    "Runner",    0.5,    0.5,   .4,     .18,    1.5,     .8,     .96,    2.5,    0.5,    0.05,     .40,        0,       0x13B6,  WeaponAbility.ParalyzingBlow,   WeaponAbility.ParalyzingBlow),
                new ShiftInfo(6,    "Reptile",   2.5,    1.5,  1.4,     1.02,   0.6,    2.5,   4.68,    2.5,    0.5,    0.3,      .30,        0,       0x1403,  WeaponAbility.MortalStrike,   WeaponAbility.MortalStrike),
                new ShiftInfo(7,    "Rodent",    0.5,    4.5,  2.0,     .18,    3.0,    0.8,    1.09,    1.5,    0.15,   0.0,      .05,        0,       0xEC4,  WeaponAbility.ShadowStrike,   WeaponAbility.ShadowStrike),
                new ShiftInfo(8,    "Stalker",   1.5,    2.5,  1.0,     .78,    2.0,    1.2,    1.09,    1.5,    0.15,   0.0,      .05,        0,       0xF52,  WeaponAbility.InfectiousStrike,   WeaponAbility.InfectiousStrike),
                new ShiftInfo(9,    "Draconic",  2.5,    1.5,  2.2,     .66,    1.1,    2.5,    3.38,    2.5,    1.95,   0.2,      .05,       .1,      0x13FB,  WeaponAbility.WhirlwindAttack,   WeaponAbility.WhirlwindAttack),
                new ShiftInfo(10,   "Floral",      1,      1,    1,     .36,    1.4,    1.5,    2.4,    1.5,    1.55,   0.5,      .5,        .2,      0xF5C,   WeaponAbility.Disarm,   WeaponAbility.Disarm),
                new ShiftInfo(11,   "Lycanthrope", 1.8,  2.2,  1.0,     .66,    2.0,    2.0,    3.0,    2.2,     0.5,   0.5,      .5,         0,      0x1441,   WeaponAbility.BleedAttack,   WeaponAbility.BleedAttack)
            };

            public static ShiftInfo[] Table
            {
                get
                {
                    return m_Table;
                }
                set
                {
                    m_Table = value;
                }
            }
        }



        public static int GetTransformGroup(Mobile target)
        {
            BaseCreature m_Transform;
            if (target is BaseCreature)
            {
                m_Transform = (BaseCreature)target;
            }
            else
                return (int)TransformGroup.Impossible; 


            Type[] Avis = new Type[]
			{
				typeof( Bird ),
				typeof( Chicken ),
				typeof( Eagle ),
				typeof( Parrot ),
				typeof( TropicalBird ),
                typeof( Phoenix ),
                typeof( CrownedEagle),
                typeof( FlyingFox ),
                typeof( Hawk ),
                typeof( Rooster),
                typeof( Vulture),
                typeof( Crane ),
                typeof( CommonEgret),
                typeof( Roadrunner)
			};

            Type[] Canis = new Type[]
			{
				typeof( Dog ),
			    typeof( GreyWolf ),
				typeof( WhiteWolf ),
				typeof( TimberWolf ),
                typeof( HellHound ),
				typeof( DireWolf ),
                typeof( Warg ),
                typeof( Jackal ),
                typeof( Coyote ),
                typeof( SnowWolf),
                typeof( RedWolf)
			};

            Type[] Felis = new Type[]
			{
				typeof( Cat ),
                typeof( Cougar ),
                typeof( HellCat ),
				typeof( Panther ),
                typeof( PredatorHellCat ),
				typeof( SnowLeopard ),
                typeof( DisplacerBeast )
			};

            Type[] Ursine = new Type[]
			{
				typeof( BlackBear ),
				typeof( BrownBear ),
                typeof( GrizzlyBear ),
				typeof( PolarBear ),
                typeof( Gorilla ),
				typeof( Walrus ),
                typeof( FrostlingPolarBear ),
                typeof( Ape),
                typeof(Orangutan)
			};

            Type[] Ungulate = new Type[]
			{
                typeof( Cow ),
				typeof( Hind ),
                typeof( GreatHart),
                typeof( PackHorse ),
				typeof( PackLlama ),
				typeof( Boar ),
				typeof( Pig ),
				typeof( MountainGoat ),
				typeof( Sheep ),
                typeof( Bull ),
                typeof( Beetle),
                typeof( Alpaca),
                typeof( Elk),
                typeof( Gazelle ),
                typeof( Antelope),
                typeof( Caribou),
                typeof( Gaman ),
                typeof( Javelina)
			};

            Type[] Runner = new Type[]
			{
				typeof( Llama ),
				typeof( Horse ),
				typeof( RidableLlama ),
                typeof( ForestOstard ),
                typeof( DesertOstard ),
                typeof( FrenziedOstard ),
                typeof( Ridgeback ),
                typeof( SavageRidgeback ),
                typeof( JackRabbit ),
				typeof( Rabbit ),
                typeof( Hiryu),
                typeof( Kirin),
                typeof( LargeDireWolf),
                typeof( LargeGreyWolf),
                typeof( LargeRedWolf),
                typeof( LargeSnowWolf),
                typeof( LargeTimberWolf),
                typeof( Mare),
                typeof(Reptalon),
                typeof(SerudianStallion),
                typeof( Stallion),
                typeof(SnowshoeRabbit)
            };

            Type[] Reptile = new Type[]
			{
                typeof( BullFrog ),
				typeof( GiantToad ),
				typeof( Alligator ),
                typeof( LavaLizard ),
                typeof( LavaSerpent ),
                typeof( GiantSerpent ),
                typeof( ScaledSwampDragon ),
                typeof( SwampDragon),
                typeof(GoldenFrog),
                typeof(Crocodile),
            };

            Type[] Rodent = new Type[]
			{
				typeof( GiantRat ),
				typeof( Rat ),
				typeof( Sewerrat ),
                typeof(Mouse),
                typeof(Squirrel),
                typeof(Chipmunk),
			};

            Type[] Stalker = new Type[]
			{
				
				typeof( Snake ),
                typeof( SilverSerpent ),
				typeof( IceSerpent ),
                typeof( GiantSpider),
                typeof( Scorpion ),
                typeof( GiantBlackWidow),
                typeof( FrostSpider),
                typeof( DreadSpider),
			};

            Type[] Draconic = new Type[]
			{
				typeof( Dragon ),
				typeof( Drake ),
				typeof( SerpentineDragon ),
				typeof( WhiteWyrm ),
				typeof( Wyvern ),
                typeof( AncientDragon )
			};

            Type[] Floral = new Type[]
			{
	              typeof( Treefellow),
                  typeof( Reaper),
                  typeof( WhippingVine),
                  typeof( Corpser),
                  typeof( SwampTentacle),
                  typeof( Bogling),
                  typeof( BogThing),
                  typeof( Quagmire)
			};

            Type[] Lycanthrope = new Type[]
			{
	              typeof( WerewolfShapeshiftForm),
                  typeof( VendigoShapeshiftForm)
			};

            foreach (Type type in Avis)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Avis;
            }

            foreach (Type type in Canis)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Canis;
            }

            foreach (Type type in Felis)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Felis;
            }

            foreach (Type type in Ursine)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Ursine;
            }

            foreach (Type type in Ungulate)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Ungulate;
            }

            foreach (Type type in Runner)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Runner;
            }

            foreach (Type type in Reptile)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Reptile;
            }

            foreach (Type type in Rodent)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Rodent;
            }

            foreach (Type type in Stalker)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Stalker;
            }

            foreach (Type type in Draconic)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Draconic;
            }
            foreach (Type type in Floral)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Floral;
            }
            foreach (Type type in Lycanthrope)
            {
                if (m_Transform.GetType() == type)
                    return (int)TransformGroup.Lycanthrope;
            }

            return (int)TransformGroup.Impossible;
        }

    }

    public enum TransformGroup
    {
        Avis,
        Canis,
        Felis,
        Ursine,
        Ungulate,
        Runner,
        Reptile,
        Rodent,
        Stalker,
        Draconic,
        Floral,
        Lycanthrope,
        Impossible
    }
}
