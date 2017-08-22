using System;
using Server.Items;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Engines.Craft
{
	public class DefCooking : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Cooking;	}
		}

		public override int GumpTitleNumber
		{
			get { return 1044003; } // <CENTER>COOKING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefCooking();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		public DefCooking() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			
		}
		
		public override void CustomSystem( Mobile thePlayer )
		{

			TeiravonMobile m_Player = (TeiravonMobile)thePlayer;
			int index = -1;

			/* Begin Ingrediants */
			//index = AddCraft( typeof( SackFlour ), 1044495, 1024153, 0.0, 100.0, typeof( Wheat ), 1044489, 1, 1044490 );

			index = AddCraft( typeof( Dough ), 1044495, 1024157, 20.0, 60.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );

			index = AddCraft( typeof( SweetDough ), 1044495, 1041340, 60.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );

			index = AddCraft( typeof( CakeMix ), 1044495, 1041002, 80.0, 110.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );

			index = AddCraft( typeof( CookieMix ), 1044495, 1024159, 70.0, 100.0, typeof( JarHoney ), 1044472, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );
			/* End Ingrediants */

			/* Begin Preparations */
			index = AddCraft( typeof( UnbakedQuiche ), 1044496, 1041339, 70.0, 110.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Eggs ), 1044477, 1, 1044253 );

			// TODO: This must also support chicken and lamb legs
			index = AddCraft( typeof( UnbakedMeatPie ), 1044496, 1041338, 50.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( RawRibs ), 1044482, 1, 1044253 );

			index = AddCraft( typeof( UncookedSausagePizza ), 1044496, 1041337, 60.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Sausage ), 1044483, 1, 1044253 );

			index = AddCraft( typeof( UncookedCheesePizza ), 1044496, 1041341, 60.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );

			index = AddCraft( typeof( UnbakedFruitPie ), 1044496, 1041334, 50.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Pear ), 1044481, 1, 1044253 );

			index = AddCraft( typeof( UnbakedPeachCobbler ), 1044496, 1041335, 50.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Peach ), 1044480, 1, 1044253 );

			index = AddCraft( typeof( UnbakedApplePie ), 1044496, 1041336, 50.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Apple ), 1044479, 1, 1044253 );

			index = AddCraft( typeof( UnbakedPumpkinPie ), 1044496, 1041342, 50.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), 1044484, 1, 1044253 );

			index = AddCraft( typeof( TribalPaint ), 1044496, 1040000, 80.0, 80.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( TribalBerry ), 1046460, 1, 1044253 );
			/* End Preparations */

			/* Begin Baking */
			index = AddCraft( typeof( BreadLoaf ), 1044497, 1024156, 60.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Cookies ), 1044497, 1025643, 70.0, 100.0, typeof( CookieMix ), 1044474, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Cake ), 1044497, 1022537, 80.0, 110.0, typeof( CakeMix ), 1044471, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Muffins ), 1044497, 1022539, 40.0, 90.0, typeof( SweetDough ), 1044475, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Quiche ), 1044497, 1041345, 70.0, 110.0, typeof( UnbakedQuiche ), 1044518, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MeatPie ), 1044497, 1041347, 50.0, 100.0, typeof( UnbakedMeatPie ), 1044519, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SausagePizza ), 1044497, 1044517, 60.0, 100.0, typeof( UncookedSausagePizza ), 1044520, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CheesePizza ), 1044497, 1044516, 60.0, 100.0, typeof( UncookedCheesePizza ), 1044521, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FruitPie ), 1044497, 1041346, 50.0, 100.0, typeof( UnbakedFruitPie ), 1044522, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PeachCobbler ), 1044497, 1041344, 50.0, 100.0, typeof( UnbakedPeachCobbler ), 1044523, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ApplePie ), 1044497, 1041343, 50.0, 100.0, typeof( UnbakedApplePie ), 1044524, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PumpkinPie ), 1044497, 1041348, 50.0, 100.0, typeof( UnbakedPumpkinPie ), 1046461, 1, 1044253 );
			SetNeedOven( index, true );
			/* End Baking */

			/* Begin Barbecue */
			index = AddCraft( typeof( CookedBird ), 1044498, 1022487, 20.0, 80.0, typeof( RawBird ), 1044470, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( ChickenLeg ), 1044498, 1025640, 0.0, 60.0, typeof( RawChickenLeg ), 1044473, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( FishSteak ), 1044498, 1022427, 0.0, 60.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( FriedEggs ), 1044498, 1022486, 15.0, 75.0, typeof( Eggs ), 1044477, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( LambLeg ), 1044498, 1025642, 10.0, 70.0, typeof( RawLambLeg ), 1044478, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( Ribs ), 1044498, 1022546, 0.0, 60.0, typeof( RawRibs ), 1044485, 1, 1044253 );
			SetNeedHeat( index, true );
			SetUseAllRes( index, true );
			/* End Barbecue */
			
			/* Begin Misc */
			index = AddCraft( typeof( Stew ), 1015283, "Stew", 20.0, 65.0, typeof( RawRibs ), 1044485, 1);
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( Carrot ), "Carrot", 2, 1044253 );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( GrapeJelly ), 1015283, "Grape Jelly", 40.0, 85.0, typeof( Grapes ), "Grapes", 1);
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
			index = AddCraft( typeof( PeachJam ), 1015283, "Peach Jam", 40.0, 85.0, typeof( Peach ), "Peach", 1);
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
			index = AddCraft( typeof( Tarte ), 1015283, "Tarte", 60.0, 105.0, typeof( Apple ), "Apple", 1);
			AddRes( index, typeof( Pear ), 1044481, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( Marshmallow ), 1015283, "Marshmallow", 50.0, 95.0, typeof( JarHoney ), 1044472, 1, 1044253);
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( SweetMutton ), 1015283, "Sweet Mutton", 30.0, 75.0, typeof( RawLambLeg ), 1044478, 1, 1044253);
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
			SetNeedHeat( index, true );
			
			/* End Misc */

            if ((m_Player.HasFeat(TeiravonMobile.Feats.RacialCrafting) || m_Player.HasFeat(TeiravonMobile.Feats.MasterCraftsman)) && (m_Player.IsCook() || m_Player.IsMerchant()))
			{
				if (m_Player.IsHuman())
				{
					index = AddCraft( typeof( HumanWine ), "Racials", "Mead", 95.0, 120.0, typeof( JarHoney ), "Honey", 3 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
					index = AddCraft( typeof( HumanAle ), "Racials", "Royal Ale", 95.0, 120.0, typeof( Dough ), "Dough", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
					index = AddCraft( typeof( HumanBrandy ), "Racials", "Peach Brandy", 95.0, 120.0, typeof( Peach ), "Peach", 2 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
					AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
				}
				else if (m_Player.IsOrc())
				{
					index = AddCraft( typeof( OrcMrog ), "Racials", "Mrog", 95.0, 120.0, typeof( RawLambLeg ), "Raw Lamb Leg", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
					index = AddCraft( typeof( OrcAle ), "Racials", "Blood Beer", 95.0, 120.0, typeof( Fish ), "Fish", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
				}
				else if (m_Player.IsDrow())
				{
					index = AddCraft( typeof( DrowWine ), "Racials", "Black Widow Wine", 95.0, 120.0, typeof( Nightshade ), "Nightshade", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
					index = AddCraft( typeof( DrowAle ), "Racials", "Arachnid Ale", 95.0, 120.0, typeof( SpidersSilk ), "Spider Silk", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
				}
				else if (m_Player.IsDwarf())
				{
					index = AddCraft( typeof( DwarvenAle ), "Racials", "Troll Liver Ale", 95.0, 120.0, typeof( TrollLiver ), "TrollLiver", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
					index = AddCraft( typeof( DwarvenBeer ), "Racials", "Granitebreaker Beer", 95.0, 120.0, typeof( Granite ), "Granite", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
				}
				else if (m_Player.IsElf())
				{
					index = AddCraft( typeof( ElvenFaeWine ), "Racials", "Fae Wine", 95.0, 120.0, typeof( Watermelon ), "Watermelon", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
					AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
					index = AddCraft( typeof( ElvenAle ), "Racials", "Winterfrost Ale", 95.0, 120.0, typeof( Pumpkin ), "Pumpkin", 1 );
					AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
				}
			}
			
		}
	}
}
