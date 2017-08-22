using System;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
	public delegate Item ConstructCallback( int type );

	public sealed class RewardType
	{
		private int m_Points;
		private Type[] m_Types;

		public int Points{ get{ return m_Points; } }
		public Type[] Types{ get{ return m_Types; } }

		public RewardType( int points, params Type[] types )
		{
			m_Points = points;
			m_Types = types;
		}

		public bool Contains( Type type )
		{
			for ( int i = 0; i < m_Types.Length; ++i )
			{
				if ( m_Types[i] == type )
					return true;
			}

			return false;
		}
	}

	public sealed class RewardItem
	{
		private int m_Weight;
		private ConstructCallback m_Constructor;
		private int m_Type;

		public int Weight{ get{ return m_Weight; } }
		public ConstructCallback Constructor{ get{ return m_Constructor; } }
		public int Type{ get{ return m_Type; } }

		public RewardItem( int weight, ConstructCallback constructor ) : this( weight, constructor, 0 )
		{
		}

		public RewardItem( int weight, ConstructCallback constructor, int type )
		{
			m_Weight = weight;
			m_Constructor = constructor;
			m_Type = type;
		}

		public Item Construct()
		{
			try{ return m_Constructor( m_Type ); }
			catch{ return null; }
		}
	}

	public sealed class RewardGroup
	{
		private int m_Points;
		private RewardItem[] m_Items;

		public int Points{ get{ return m_Points; } }
		public RewardItem[] Items{ get{ return m_Items; } }

		public RewardGroup( int points, params RewardItem[] items )
		{
			m_Points = points;
			m_Items = items;
		}

		public RewardItem AquireItem()
		{
			if ( m_Items.Length == 0 )
				return null;
			else if ( m_Items.Length == 1 )
				return m_Items[0];

			int totalWeight = 0;

			for ( int i = 0; i < m_Items.Length; ++i )
				totalWeight += m_Items[i].Weight;

			int randomWeight = Utility.Random( totalWeight );

			for ( int i = 0; i < m_Items.Length; ++i )
			{
				RewardItem item = m_Items[i];

				if ( randomWeight < item.Weight )
					return item;

				randomWeight -= item.Weight;
			}

			return null;
		}
	}

	public abstract class RewardCalculator
	{
		private RewardGroup[] m_Groups;

		public RewardGroup[] Groups{ get{ return m_Groups; } set{ m_Groups = value; } }

		public abstract int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type );
		public abstract int ComputeGold( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type );

		public virtual int ComputeFame( SmallBOD bod )
		{
			return ComputePoints( bod );
		}

		public virtual int ComputeFame( LargeBOD bod )
		{
			return ComputePoints( bod );
		}

		public virtual int ComputePoints( SmallBOD bod )
		{
			return ComputePoints( bod.AmountMax, bod.RequireExceptional, bod.Material, 1, bod.Type );
		}

		public virtual int ComputePoints( LargeBOD bod )
		{
			return ComputePoints( bod.AmountMax, bod.RequireExceptional, bod.Material, bod.Entries.Length, bod.Entries[0].Details.Type );
		}

		public virtual int ComputeGold( SmallBOD bod )
		{
			return ComputeGold( bod.AmountMax, bod.RequireExceptional, bod.Material, 1, bod.Type );
		}

		public virtual int ComputeGold( LargeBOD bod )
		{
			return ComputeGold( bod.AmountMax, bod.RequireExceptional, bod.Material, bod.Entries.Length, bod.Entries[0].Details.Type );
		}

		public virtual RewardGroup LookupRewards( int points )
		{
			for ( int i = m_Groups.Length - 1; i >= 1; --i )
			{
				RewardGroup group = m_Groups[i];

				if ( points >= group.Points )
					return group;
			}

			return m_Groups[0];
		}

		public virtual int LookupTypePoints( RewardType[] types, Type type )
		{
			for ( int i = 0; i < types.Length; ++i )
			{
				if ( types[i].Contains( type ) )
					return types[i].Points;
			}

			return 0;
		}

		public RewardCalculator()
		{
		}
	}

	public sealed class SmithRewardCalculator : RewardCalculator
	{
		#region Constructors
		private static readonly ConstructCallback SturdyShovel = new ConstructCallback( CreateSturdyShovel );
		private static readonly ConstructCallback SturdyPickaxe = new ConstructCallback( CreateSturdyPickaxe );
		private static readonly ConstructCallback MiningGloves = new ConstructCallback( CreateMiningGloves );
		private static readonly ConstructCallback GargoylesPickaxe = new ConstructCallback( CreateGargoylesPickaxe );
		private static readonly ConstructCallback ProspectorsTool = new ConstructCallback( CreateProspectorsTool );
		private static readonly ConstructCallback PowderOfTemperament = new ConstructCallback( CreatePowderOfTemperament );
		private static readonly ConstructCallback RunicHammer = new ConstructCallback( CreateRunicHammer );
		private static readonly ConstructCallback PowerScroll = new ConstructCallback( CreatePowerScroll );
		private static readonly ConstructCallback ColoredAnvil = new ConstructCallback( CreateColoredAnvil );
		private static readonly ConstructCallback AncientHammer = new ConstructCallback( CreateAncientHammer );

		private static Item CreateSturdyShovel( int type )
		{
			return new SturdyShovel();
		}

		private static Item CreateSturdyPickaxe( int type )
		{
			return new SturdyPickaxe();
		}

		private static Item CreateMiningGloves( int type )
		{
			if ( type == 1 )
				return new LeatherGlovesOfMining( 1 );
			else if ( type == 3 )
				return new StuddedGlovesOfMining( 3 );
			else if ( type == 5 )
				return new RingmailGlovesOfMining( 5 );

			throw new InvalidOperationException();
		}

		private static Item CreateGargoylesPickaxe( int type )
		{
			return new GargoylesPickaxe();
		}

		private static Item CreateProspectorsTool( int type )
		{
			return new ProspectorsTool();
		}

		private static Item CreatePowderOfTemperament( int type )
		{
			return new PowderOfTemperament();
		}

		private static Item CreateRunicHammer( int type )
		{
			if ( type >= 1 && type <= 8 )
				return new RunicHammer( CraftResource.Iron + type, Core.AOS ? ( Utility.RandomMinMax(2,5) ) : 50 );
//				return new RunicHammer( CraftResource.Iron + type, Core.AOS ? ( 55 - (type*5) ) : 50 );

			throw new InvalidOperationException();
		}

		private static Item CreatePowerScroll( int type )
		{
			if ( type == 5 || type == 10 || type == 15 || type == 20 )
				return new PowerScroll( SkillName.Blacksmith, 100 + type );

			throw new InvalidOperationException();
		}

		private static Item CreateColoredAnvil( int type )
		{
			return new ColoredAnvil();
		}

		private static Item CreateAncientHammer( int type )
		{
			if ( type == 10 || type == 15 || type == 30 || type == 60 )
				return new AncientSmithyHammer( type );

			throw new InvalidOperationException();
		}
		#endregion

		public static readonly SmithRewardCalculator Instance = new SmithRewardCalculator();

		private RewardType[] m_Types = new RewardType[]
			{
				new RewardType( 200, typeof( RingmailGloves ), typeof( RingmailChest ), typeof( RingmailArms ), typeof( RingmailLegs ) ),
				new RewardType( 200, typeof( Bardiche ), typeof( Halberd ) ),
				new RewardType( 300, typeof( ChainCoif ), typeof( ChainLegs ), typeof( ChainChest ) ),
				new RewardType( 300, typeof( Axe ), typeof( BattleAxe ), typeof( DoubleAxe ), typeof( ExecutionersAxe ), typeof( LargeBattleAxe ), typeof( TwoHandedAxe ) ),
				new RewardType( 300, typeof( Broadsword ), typeof( Cutlass ), typeof( Katana ), typeof( Longsword ), typeof( Scimitar ), typeof( ThinLongsword ), typeof( VikingSword ) ),
				new RewardType( 300, typeof( WarAxe ), typeof( HammerPick ), typeof( Mace ), typeof( Maul ), typeof( WarHammer ), typeof( WarMace ) ),
				new RewardType( 350, typeof( ShortSpear ), typeof( Spear ), typeof( WarFork ), typeof( Kryss ) ),
				new RewardType( 400, typeof( PlateArms ), typeof( PlateLegs ), typeof( PlateHelm ), typeof( PlateGorget ), typeof( PlateGloves ), typeof( PlateChest ) )
			};

		public override int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			int points = 0;

			if ( quantity == 10 )
				points += 10;
			else if ( quantity == 15 )
				points += 25;
			else if ( quantity == 20 )
				points += 50;

			if ( exceptional )
				points += 200;

			if ( itemCount > 1 )
				points += LookupTypePoints( m_Types, type );

			if ( material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Valorite )
				points += 200 + (50 * (material - BulkMaterialType.DullCopper));

			return points;
		}

		private static double RewardMod = 0.1;

		private static int[][][] m_GoldTable = new int[][][]
			{
				new int[][] // 1-part (regular)
				{
					new int[]{ (int)(RewardMod * 150), (int)(RewardMod * 250), (int)(RewardMod * 250), (int)(RewardMod * 400),  (int)(RewardMod * 400),  (int)(RewardMod * 750),  (int)(RewardMod * 750), (int)(RewardMod * 1200), (int)(RewardMod * 1200) },
					new int[]{ (int)(RewardMod * 225), (int)(RewardMod * 375), (int)(RewardMod * 375), (int)(RewardMod * 600),  (int)(RewardMod * 600), (int)(RewardMod * 1125), (int)(RewardMod * 1125), (int)(RewardMod * 1800), (int)(RewardMod * 1800) },
					new int[]{ (int)(RewardMod * 300), (int)(RewardMod * 500), (int)(RewardMod * 750), (int)(RewardMod * 800), (int)(RewardMod * 1050), (int)(RewardMod * 1500), (int)(RewardMod * 2250), (int)(RewardMod * 2400), (int)(RewardMod * 4000) }
				},
				new int[][] // 1-part (exceptional)
				{
					new int[]{ (int)(RewardMod * 250), (int)(RewardMod * 400),  (int)(RewardMod * 400),  (int)(RewardMod * 750),  (int)(RewardMod * 750), (int)(RewardMod * 1500), (int)(RewardMod * 1500), (int)(RewardMod * 3000),  (int)(RewardMod * 3000) },
					new int[]{ (int)(RewardMod * 375), (int)(RewardMod * 600),  (int)(RewardMod * 600), (int)(RewardMod * 1125), (int)(RewardMod * 1125), (int)(RewardMod * 2250), (int)(RewardMod * 2250), (int)(RewardMod * 4500),  (int)(RewardMod * 4500) },
					new int[]{ (int)(RewardMod * 500), (int)(RewardMod * 800), (int)(RewardMod * 1200), (int)(RewardMod * 1500), (int)(RewardMod * 2500), (int)(RewardMod * 3000), (int)(RewardMod * 6000), (int)(RewardMod * 6000), (int)(RewardMod * 12000) }
				},
				new int[][] // Ringmail (regular)
				{
					new int[]{ (int)(RewardMod * 3000),  (int)(RewardMod * 5000),  (int)(RewardMod * 5000),  (int)(RewardMod * 7500),  (int)(RewardMod * 7500), (int)(RewardMod * 10000), (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000) },
					new int[]{ (int)(RewardMod * 4500),  (int)(RewardMod * 7500),  (int)(RewardMod * 7500), (int)(RewardMod * 11250), (int)(RewardMod * 11500), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 22500), (int)(RewardMod * 22500) },
					new int[]{ (int)(RewardMod * 6000), (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 20000), (int)(RewardMod * 20000), (int)(RewardMod * 30000), (int)(RewardMod * 30000), (int)(RewardMod * 50000) }
				},
				new int[][] // Ringmail (exceptional)
				{
					new int[]{  (int)(RewardMod * 5000), (int)(RewardMod * 10000), (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 25000),  (int)(RewardMod * 25000),  (int)(RewardMod * 50000),  (int)(RewardMod * 50000) },
					new int[]{  (int)(RewardMod * 7500), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 22500), (int)(RewardMod * 22500), (int)(RewardMod * 37500),  (int)(RewardMod * 37500),  (int)(RewardMod * 75000),  (int)(RewardMod * 75000) },
					new int[]{ (int)(RewardMod * 10000), (int)(RewardMod * 20000), (int)(RewardMod * 30000), (int)(RewardMod * 30000), (int)(RewardMod * 50000), (int)(RewardMod * 50000), (int)(RewardMod * 100000), (int)(RewardMod * 100000), (int)(RewardMod * 200000) }
				},
				new int[][] // Chainmail (regular)
				{
					new int[]{ (int)(RewardMod * 4000),  (int)(RewardMod * 7500),  (int)(RewardMod * 7500), (int)(RewardMod * 10000), (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 25000),  (int)(RewardMod * 25000) },
					new int[]{ (int)(RewardMod * 6000), (int)(RewardMod * 11250), (int)(RewardMod * 11250), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 22500), (int)(RewardMod * 22500), (int)(RewardMod * 37500),  (int)(RewardMod * 37500) },
					new int[]{ (int)(RewardMod * 8000), (int)(RewardMod * 15000), (int)(RewardMod * 20000), (int)(RewardMod * 20000), (int)(RewardMod * 30000), (int)(RewardMod * 30000), (int)(RewardMod * 50000), (int)(RewardMod * 50000), (int)(RewardMod * 100000) }
				},
				new int[][] // Chainmail (exceptional)
				{
					new int[]{  (int)(RewardMod * 7500), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 25000),  (int)(RewardMod * 25000),  (int)(RewardMod * 50000),  (int)(RewardMod * 50000), (int)(RewardMod * 100000), (int)(RewardMod * 100000) },
					new int[]{ (int)(RewardMod * 11250), (int)(RewardMod * 22500), (int)(RewardMod * 22500), (int)(RewardMod * 37500),  (int)(RewardMod * 37500),  (int)(RewardMod * 75000),  (int)(RewardMod * 75000), (int)(RewardMod * 150000), (int)(RewardMod * 150000) },
					new int[]{ (int)(RewardMod * 15000), (int)(RewardMod * 30000), (int)(RewardMod * 50000), (int)(RewardMod * 50000), (int)(RewardMod * 100000), (int)(RewardMod * 100000), (int)(RewardMod * 200000), (int)(RewardMod * 200000), (int)(RewardMod * 200000) }
				},
				new int[][] // Platemail (regular)
				{
					new int[]{  (int)(RewardMod * 5000), (int)(RewardMod * 10000), (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 25000),  (int)(RewardMod * 25000),  (int)(RewardMod * 50000),  (int)(RewardMod * 50000) },
					new int[]{  (int)(RewardMod * 7500), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 22500), (int)(RewardMod * 22500), (int)(RewardMod * 37500),  (int)(RewardMod * 37500),  (int)(RewardMod * 75000),  (int)(RewardMod * 75000) },
					new int[]{ (int)(RewardMod * 10000), (int)(RewardMod * 20000), (int)(RewardMod * 30000), (int)(RewardMod * 30000), (int)(RewardMod * 50000), (int)(RewardMod * 50000), (int)(RewardMod * 100000), (int)(RewardMod * 100000), (int)(RewardMod * 200000) }
				},
				new int[][] // Platemail (exceptional)
				{
					new int[]{ (int)(RewardMod * 10000), (int)(RewardMod * 25000),  (int)(RewardMod * 25000),  (int)(RewardMod * 50000),  (int)(RewardMod * 50000), (int)(RewardMod * 100000), (int)(RewardMod * 100000), (int)(RewardMod * 100000), (int)(RewardMod * 100000) },
					new int[]{ (int)(RewardMod * 15000), (int)(RewardMod * 37500),  (int)(RewardMod * 37500),  (int)(RewardMod * 75000),  (int)(RewardMod * 75000), (int)(RewardMod * 150000), (int)(RewardMod * 150000), (int)(RewardMod * 150000), (int)(RewardMod * 150000) },
					new int[]{ (int)(RewardMod * 20000), (int)(RewardMod * 50000), (int)(RewardMod * 100000), (int)(RewardMod * 100000), (int)(RewardMod * 200000), (int)(RewardMod * 200000), (int)(RewardMod * 200000), (int)(RewardMod * 200000), (int)(RewardMod * 200000) }
				}
			};

		public override int ComputeGold( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			int[][][] goldTable = m_GoldTable;

			int typeIndex = (( itemCount == 1 ? 0 : m_Types[7].Contains( type ) ? 3 : m_Types[2].Contains( type ) ? 2 : 1 ) * 2) + (exceptional ? 1 : 0);
			int quanIndex = ( quantity == 20 ? 2 : quantity == 15 ? 1 : 0 );
			int mtrlIndex = ( material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Valorite ) ? 1 + (int)(material - BulkMaterialType.DullCopper) : 0;

			int gold = goldTable[typeIndex][quanIndex][mtrlIndex];

			int min = (gold * 9) / 10;
			int max = (gold * 10) / 9;

			return Utility.RandomMinMax( min, max );
		}

		public SmithRewardCalculator()
		{
			Groups = new RewardGroup[]
				{
					new RewardGroup(    0, new RewardItem( 1, SturdyShovel ) ),
					new RewardGroup(   25, new RewardItem( 1, SturdyPickaxe ) ),
					new RewardGroup(   50, new RewardItem( 45, SturdyShovel ), new RewardItem( 45, SturdyPickaxe ), new RewardItem( 10, MiningGloves, 1 ) ),
					new RewardGroup(  200, new RewardItem( 45, GargoylesPickaxe ), new RewardItem( 45, ProspectorsTool ), new RewardItem( 10, MiningGloves, 3 ) ),
					new RewardGroup(  400, new RewardItem( 2, GargoylesPickaxe ), new RewardItem( 2, ProspectorsTool ), new RewardItem( 1, PowderOfTemperament ) ),
					new RewardGroup(  450, new RewardItem( 9, PowderOfTemperament ), new RewardItem( 1, MiningGloves, 5 ) ),
					new RewardGroup(  500, new RewardItem( 1, MiningGloves, 5 ), new RewardItem(2, PowderOfTemperament) ),
//					new RewardGroup(  550, new RewardItem( 3, RunicHammer, 1 ), new RewardItem( 2, RunicHammer, 2 ) ),
//					new RewardGroup(  600, new RewardItem( 1, RunicHammer, 2 ) ),
					//new RewardGroup(  600, new RewardItem( 2, PowerScroll, 5 ), new RewardItem( 1, ColoredAnvil ) ),
//					new RewardGroup(  650, new RewardItem( 1, RunicHammer, 3 ) ),
					new RewardGroup(  600, new RewardItem( 1, ColoredAnvil )),
					new RewardGroup(  700, new RewardItem( 1, AncientHammer, 10 ), new RewardItem(1, ColoredAnvil) ),
					new RewardGroup(  750, new RewardItem( 1, AncientHammer, 10 ) ),
					new RewardGroup(  800, new RewardItem(2, RunicHammer, 1) ),
					new RewardGroup(  850, new RewardItem( 1, AncientHammer, 15 ), new RewardItem(2, RunicHammer, 1) ),
					new RewardGroup(  900, new RewardItem(2, RunicHammer, 2) ),
					new RewardGroup(  950, new RewardItem( 1, RunicHammer, 3 ) ),
					new RewardGroup( 1000, new RewardItem( 1, AncientHammer, 30 ), new RewardItem(2, RunicHammer, 4) ),
					new RewardGroup( 1050, new RewardItem( 1, RunicHammer, 5 ) ),
					new RewardGroup( 1100, new RewardItem( 1, AncientHammer, 60 ), new RewardItem(2, RunicHammer, 6) ),
					new RewardGroup( 1150, new RewardItem( 1, RunicHammer, 7 ) ),
					new RewardGroup( 1200, new RewardItem( 1, RunicHammer, 8 ) )
				};
		}
	}

	public sealed class TailorRewardCalculator : RewardCalculator
	{
		#region Constructors
		private static readonly ConstructCallback Cloth = new ConstructCallback( CreateCloth );
		private static readonly ConstructCallback Sandals = new ConstructCallback( CreateSandals );
		private static readonly ConstructCallback StretchedHide = new ConstructCallback( CreateStretchedHide );
		private static readonly ConstructCallback RunicKit = new ConstructCallback( CreateRunicKit );
		private static readonly ConstructCallback Tapestry = new ConstructCallback( CreateTapestry );
		private static readonly ConstructCallback PowerScroll = new ConstructCallback( CreatePowerScroll );
		private static readonly ConstructCallback BearRug = new ConstructCallback( CreateBearRug );
		private static readonly ConstructCallback ClothingBlessDeed = new ConstructCallback( CreateCBD );

		private static int[][] m_ClothHues = new int[][]
			{
				new int[]{ 0xA4D, 0x8E6, 0x8B1, 0xB7C },
				new int[]{ 0xA58, 0xA11, 0x966, 0x9A0 },
				new int[]{ 0x8C8, 0xB86, 0x9F2, 0x9EB },
				new int[]{ 0x9FF, 0xA6F, 0x9F9, 0x9F3 },
				new int[]{ 0x9F1, 0xA70, 0x9EA, 0xA23 }
			};

		private static Item CreateCloth( int type )
		{
			if ( type >= 0 && type < m_ClothHues.Length )
			{
				UncutCloth cloth = new UncutCloth( 100 );
				cloth.Hue = m_ClothHues[type][Utility.Random( m_ClothHues[type].Length )];
				return cloth;
			}

			throw new InvalidOperationException();
		}

		private static int[] m_SandalHues = new int[]
			{
				0x9EC, 0x9F1, 0x9FF,
				0x8C5, 0x8CA, 0x89F,
				0xA10, 0xA22
			};

		private static Item CreateSandals( int type )
		{
			return new Sandals( m_SandalHues[Utility.Random( m_SandalHues.Length )] );
		}

		private static Item CreateStretchedHide( int type )
		{
			switch ( Utility.Random( 4 ) )
			{
				default:
				case 0:	return new SmallStretchedHideEastDeed();
				case 1: return new SmallStretchedHideSouthDeed();
				case 2: return new MediumStretchedHideEastDeed();
				case 3: return new MediumStretchedHideSouthDeed();
			}
		}

		private static Item CreateTapestry( int type )
		{
			switch ( Utility.Random( 4 ) )
			{
				default:
				case 0:	return new LightFlowerTapestryEastDeed();
				case 1: return new LightFlowerTapestrySouthDeed();
				case 2: return new DarkFlowerTapestryEastDeed();
				case 3: return new DarkFlowerTapestrySouthDeed();
			}
		}

		private static Item CreateBearRug( int type )
		{
			switch ( Utility.Random( 4 ) )
			{
				default:
				case 0:	return new BrownBearRugEastDeed();
				case 1: return new BrownBearRugSouthDeed();
				case 2: return new PolarBearRugEastDeed();
				case 3: return new PolarBearRugSouthDeed();
			}
		}

		private static Item CreateRunicKit( int type )
		{
			if ( type >= 1 && type <= 3 )
				return new RunicSewingKit( CraftResource.RegularLeather + type, Utility.RandomMinMax(1,6) );
//				return new RunicSewingKit( CraftResource.RegularLeather + type, 60 - (type*15) );

			throw new InvalidOperationException();
		}

		private static Item CreatePowerScroll( int type )
		{
			if ( type == 5 || type == 10 || type == 15 || type == 20 )
				return new PowerScroll( SkillName.Tailoring, 100 + type );

			throw new InvalidOperationException();
		}

		private static Item CreateCBD( int type )
		{
			return new ClothingBlessDeed();
		}
		#endregion

		public static readonly TailorRewardCalculator Instance = new TailorRewardCalculator();

		public override int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			int points = 0;

			if ( quantity == 10 )
				points += 10;
			else if ( quantity == 15 )
				points += 25;
			else if ( quantity == 20 )
				points += 50;

			if ( exceptional )
				points += 100;

			if ( itemCount == 4 )
				points += 300;
			else if ( itemCount == 5 )
				points += 400;
			else if ( itemCount == 6 )
				points += 500;

			if ( material == BulkMaterialType.Spined )
				points += 50;
			else if ( material == BulkMaterialType.Horned )
				points += 100;
			else if ( material == BulkMaterialType.Barbed )
				points += 150;

			return points;
		}

		private static double RewardMod = 0.4;

		private static int[][][] m_AosGoldTable = new int[][][]
			{
				new int[][] // 1-part (regular)
				{
					new int[]{ (int)(RewardMod * 150), (int)(RewardMod * 150), (int)(RewardMod * 300), (int)(RewardMod * 300) },
					new int[]{ (int)(RewardMod * 225), (int)(RewardMod * 225), (int)(RewardMod * 450), (int)(RewardMod * 450) },
					new int[]{ (int)(RewardMod * 300), (int)(RewardMod * 400), (int)(RewardMod * 600), (int)(RewardMod * 750) }
				},
				new int[][] // 1-part (exceptional)
				{
					new int[]{ (int)(RewardMod * 300), (int)(RewardMod * 300),  (int)(RewardMod * 600),  (int)(RewardMod * 600) },
					new int[]{ (int)(RewardMod * 450), (int)(RewardMod * 450),  (int)(RewardMod * 900),  (int)(RewardMod * 900) },
					new int[]{ (int)(RewardMod * 600), (int)(RewardMod * 750), (int)(RewardMod * 1200), (int)(RewardMod * 1800) }
				},
				new int[][] // 4-part (regular)
				{
					new int[]{  (int)(RewardMod * 4000),  (int)(RewardMod * 4000),  (int)(RewardMod * 5000),  (int)(RewardMod * 5000) },
					new int[]{  (int)(RewardMod * 6000),  (int)(RewardMod * 6000),  (int)(RewardMod * 7500),  (int)(RewardMod * 7500) },
					new int[]{  (int)(RewardMod * 8000), (int)(RewardMod * 10000), (int)(RewardMod * 10000), (int)(RewardMod * 15000) }
				},
				new int[][] // 4-part (exceptional)
				{
					new int[]{  (int)(RewardMod * 5000),  (int)(RewardMod * 5000),  (int)(RewardMod * 7500),  (int)(RewardMod * 7500) },
					new int[]{  (int)(RewardMod * 7500),  (int)(RewardMod * 7500), (int)(RewardMod * 11250), (int)(RewardMod * 11250) },
					new int[]{ (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 20000) }
				},
				new int[][] // 5-part (regular)
				{
					new int[]{  (int)(RewardMod * 5000), (int)(RewardMod * 5000),  (int)(RewardMod * 7500),  (int)(RewardMod * 7500) },
					new int[]{  (int)(RewardMod * 7500),  (int)(RewardMod * 7500), (int)(RewardMod * 11250), (int)(RewardMod * 11250) },
					new int[]{ (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 20000) }
				},
				new int[][] // 5-part (exceptional)
				{
					new int[]{  (int)(RewardMod * 7500),  (int)(RewardMod * 7500), (int)(RewardMod * 10000), (int)(RewardMod * 10000) },
					new int[]{ (int)(RewardMod * 11250), (int)(RewardMod * 11250), (int)(RewardMod * 15000), (int)(RewardMod * 15000) },
					new int[]{ (int)(RewardMod * 15000), (int)(RewardMod * 20000), (int)(RewardMod * 20000), (int)(RewardMod * 30000) }
				},
				new int[][] // 6-part (regular)
				{
					new int[]{  (int)(RewardMod * 7500),  (int)(RewardMod * 7500), (int)(RewardMod * 10000), (int)(RewardMod * 10000) },
					new int[]{ (int)(RewardMod * 11250), (int)(RewardMod * 11250), (int)(RewardMod * 15000), (int)(RewardMod * 15000) },
					new int[]{ (int)(RewardMod * 15000), (int)(RewardMod * 20000), (int)(RewardMod * 20000), (int)(RewardMod * 30000) }
				},
				new int[][] // 6-part (exceptional)
				{
					new int[]{ (int)(RewardMod * 10000), (int)(RewardMod * 10000), (int)(RewardMod * 15000), (int)(RewardMod * 15000) },
					new int[]{ (int)(RewardMod * 15000), (int)(RewardMod * 15000), (int)(RewardMod * 22500), (int)(RewardMod * 22500) },
					new int[]{ (int)(RewardMod * 20000), (int)(RewardMod * 30000), (int)(RewardMod * 30000), (int)(RewardMod * 50000) }
				}
			};

		private static int[][][] m_OldGoldTable = new int[][][]
			{
				new int[][] // 1-part (regular)
				{
					new int[]{ 150, 150, 300, 300 },
					new int[]{ 225, 225, 450, 450 },
					new int[]{ 300, 400, 600, 750 }
				},
				new int[][] // 1-part (exceptional)
				{
					new int[]{ 300, 300,  600,  600 },
					new int[]{ 450, 450,  900,  900 },
					new int[]{ 600, 750, 1200, 1800 }
				},
				new int[][] // 4-part (regular)
				{
					new int[]{  3000,  3000,  4000,  4000 },
					new int[]{  4500,  4500,  6000,  6000 },
					new int[]{  6000,  8000,  8000, 10000 }
				},
				new int[][] // 4-part (exceptional)
				{
					new int[]{  4000,  4000,  5000,  5000 },
					new int[]{  6000,  6000,  7500,  7500 },
					new int[]{  8000, 10000, 10000, 15000 }
				},
				new int[][] // 5-part (regular)
				{
					new int[]{  4000,  4000,  5000,  5000 },
					new int[]{  6000,  6000,  7500,  7500 },
					new int[]{  8000, 10000, 10000, 15000 }
				},
				new int[][] // 5-part (exceptional)
				{
					new int[]{  5000,  5000,  7500,  7500 },
					new int[]{  7500,  7500, 11250, 11250 },
					new int[]{ 10000, 15000, 15000, 20000 }
				},
				new int[][] // 6-part (regular)
				{
					new int[]{  5000,  5000,  7500,  7500 },
					new int[]{  7500,  7500, 11250, 11250 },
					new int[]{ 10000, 15000, 15000, 20000 }
				},
				new int[][] // 6-part (exceptional)
				{
					new int[]{  7500,  7500, 10000, 10000 },
					new int[]{ 11250, 11250, 15000, 15000 },
					new int[]{ 15000, 20000, 20000, 30000 }
				}
			};

		public override int ComputeGold( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			int[][][] goldTable = ( Core.AOS ? m_AosGoldTable : m_OldGoldTable );

			int typeIndex = (( itemCount == 6 ? 3 : itemCount == 5 ? 2 : itemCount == 4 ? 1 : 0 ) * 2) + (exceptional ? 1 : 0);
			int quanIndex = ( quantity == 20 ? 2 : quantity == 15 ? 1 : 0 );
			int mtrlIndex = ( material == BulkMaterialType.Barbed ? 3 : material == BulkMaterialType.Horned ? 2 : material == BulkMaterialType.Spined ? 1 : 0 );

			int gold = goldTable[typeIndex][quanIndex][mtrlIndex];

			int min = (gold * 9) / 10;
			int max = (gold * 10) / 9;

			return Utility.RandomMinMax( min, max );
		}

		public TailorRewardCalculator()
		{
			Groups = new RewardGroup[]
				{
					new RewardGroup(   0, new RewardItem( 1, Cloth, 0 ) ),
					new RewardGroup(  50, new RewardItem( 1, Cloth, 1 ) ),
					new RewardGroup( 100, new RewardItem( 1, Cloth, 2 ) ),
					new RewardGroup( 150, new RewardItem( 9, Cloth, 3 ), new RewardItem( 1, Sandals ) ),
					new RewardGroup( 200, new RewardItem( 4, Cloth, 4 ), new RewardItem( 1, Sandals ) ),
					new RewardGroup( 300, new RewardItem( 1, StretchedHide ) ),
					new RewardGroup( 350, new RewardItem( 1, RunicKit, 1 ) ),
					new RewardGroup( 400, new RewardItem( 2, PowerScroll, 5 ), new RewardItem( 3, Tapestry ) ),
					new RewardGroup( 450, new RewardItem( 1, BearRug ) ),
					new RewardGroup( 500, new RewardItem( 1, PowerScroll, 10 ) ),
					new RewardGroup( 550, new RewardItem( 1, ClothingBlessDeed ) ),
					new RewardGroup( 575, new RewardItem( 1, PowerScroll, 15 ) ),
					new RewardGroup( 600, new RewardItem( 1, RunicKit, 2 ) ),
					new RewardGroup( 650, new RewardItem( 1, PowerScroll, 20 ) ),
					new RewardGroup( 700, new RewardItem( 1, RunicKit, 3 ) )
				};
		}
	}
}
