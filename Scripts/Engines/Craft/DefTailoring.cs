using System;
using Server.Items;
using Server.Mobiles;
using Server.Teiravon;


namespace Server.Engines.Craft
{
	public class DefTailoring : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044005; } // <CENTER>TAILORING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTailoring();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		public DefTailoring() : base( 1, 1, 3.25 )// base( 1, 1, 4.5 )
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
			from.PlaySound( 0x248 );
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

			// Hats
			AddCraft( typeof( SkullCap ), 1011375, 1025444, 0.0, 25.0, typeof( Cloth ), 1044286, 2, 1044287 );
			AddCraft( typeof( Bandana ), 1011375, 1025440, 0.0, 25.0, typeof( Cloth ), 1044286, 2, 1044287 );
			AddCraft( typeof( FloppyHat ), 1011375, 1025907, 6.2, 31.2, typeof( Cloth ), 1044286, 11, 1044287 );
			AddCraft( typeof( Cap ), 1011375, 1025909, -18.8, 6.2, typeof( Cloth ), 1044286, 11, 1044287 );
			AddCraft( typeof( WideBrimHat ), 1011375, 1025908, 6.2, 31.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( StrawHat ), 1011375, 1025911, 6.2, 31.2, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( TallStrawHat ), 1011375, 1025910, 6.7, 31.7, typeof( Cloth ), 1044286, 13, 1044287 );
			AddCraft( typeof( WizardsHat ), 1011375, 1025912, 7.2, 32.2, typeof( Cloth ), 1044286, 15, 1044287 );
			AddCraft( typeof( Bonnet ), 1011375, 1025913, 6.2, 31.2, typeof( Cloth ), 1044286, 11, 1044287 );
			AddCraft( typeof( FeatheredHat ), 1011375, 1025914, 6.2, 31.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( TricorneHat ), 1011375, 1025915, 6.2, 31.2, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( JesterHat ), 1011375, 1025916, 7.2, 32.2, typeof( Cloth ), 1044286, 15, 1044287 );
            // Teiravon custom
            AddCraft(typeof(Cowl), 1011375, "Cowl", 15, 40, typeof(Cloth), "Cloth", 5);

			if ( Core.AOS )
				AddCraft( typeof( FlowerGarland ), 1011375, 1028965, 10.0, 35.0, typeof( Cloth ), 1044286, 5, 1044287 );

			// Shirts
			AddCraft( typeof( Doublet ), 1015269, 1028059, 0, 25.0, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Shirt ), 1015269, 1025399, 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( FancyShirt ), 1015269, 1027933, 24.8, 49.8, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Tunic ), 1015269, 1028097, 00.0, 25.0, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( Surcoat ), 1015269, 1028189, 8.2, 33.2, typeof( Cloth ), 1044286, 14, 1044287 );
			AddCraft( typeof( PlainDress ), 1015269, 1027937, 12.4, 37.4, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( FancyDress ), 1015269, 1027935, 33.1, 58.1, typeof( Cloth ), 1044286, 12, 1044287 );
			AddCraft( typeof( Cloak ), 1015269, 1025397, 41.4, 66.4, typeof( Cloth ), 1044286, 14, 1044287 );
			AddCraft( typeof( Robe ), 1015269, 1027939, 53.9, 78.9, typeof( Cloth ), 1044286, 16, 1044287 );
			AddCraft( typeof( JesterSuit ), 1015269, 1028095, 8.2, 33.2, typeof( Cloth ), 1044286, 24, 1044287 );
            // Teiravon custom - shirts
            AddCraft(typeof(RuffledShirt), 1015269, "Ruffled Shirt", 70, 95, typeof(Cloth), "Cloth", 15);
            AddCraft(typeof(ButtonedShirt), 1015269, "Buttoned Shirt", 55, 85, typeof(Cloth), "Cloth", 20);
            AddCraft(typeof(LayeredShirt), 1015269, "Layered Shirt", 60, 85, typeof(Cloth), "Cloth", 15);
            AddCraft(typeof(SleevelessTop), 1015269, "Sleeveless Top", 55, 80, typeof(Cloth), "Cloth", 10);
            AddCraft(typeof(LayeredTop), 1015269, "Layered Top", 55, 80, typeof(Cloth), "Cloth", 15);
            AddCraft(typeof(BandedTop), 1015269, "Banded Top", 53, 78, typeof(Cloth), "Cloth", 18);
            index = AddCraft(typeof(DarkCorset), 1015269, "Dark Corset", 58, 83, typeof(Cloth), "Cloth", 10);
            AddRes(index, typeof(Leather), "Leather", 5);
            index = AddCraft(typeof(GirdleTop), 1015269, "Girdle Top", 55, 80, typeof(Cloth), "Cloth", 8);
            AddRes(index, typeof(Leather), "Leather", 4);
            AddCraft(typeof(CorsetTop), 1015269, "Corset Top", 55, 80, typeof(Cloth), "Cloth", 12);
            AddCraft(typeof(Corset), 1015269, "Corset", 40, 65, typeof(Cloth), "Cloth", 10);
            index = AddCraft(typeof(VestJacket), 1015269, "Vest Jacket", 65, 90, typeof(Cloth), "Cloth", 10);
            AddRes(index, typeof(Leather), "Leather", 5);
            index = AddCraft(typeof(NobleJacket), 1015269, "Noble Jacket", 70, 95, typeof(Cloth), "Cloth", 10);
            AddRes(index, typeof(Leather), "Leather", 5);
            index = AddCraft(typeof(VestedShirt), 1015269, "Vested Shirt", 65, 90, typeof(Cloth), "Cloth", 10);
            AddRes(index, typeof(Leather), "Leather", 5);
            AddCraft(typeof(PatternedShirt), 1015269, "Patterned Shirt", 70, 95, typeof(Cloth), "Cloth", 15);
            index = AddCraft(typeof(CheckeredShirt), 1015269, "Checkered Shirt", 40, 65, typeof(Cloth), "Cloth", 14);
            AddRes(index, typeof(Leather), "Leather", 1);
            // Teiravon custom - middle torso
            AddCraft(typeof(FittedSurcoat), 1015269, "FittedSurcoat", 60, 85, typeof(Cloth), "Cloth", 15);
            AddCraft(typeof(Tabbard), 1015269, "Tabbard", 55, 80, typeof(Cloth), "Cloth", 15);
            AddCraft(typeof(Vest), 1015269, "Vest", 40, 65, typeof(Cloth), "Cloth", 8);
            AddCraft(typeof(PirateShirt), 1015269, "Pirate Shirt", 65, 90, typeof(Cloth), "Cloth", 15);
            AddCraft(typeof(BeltedVest), 1015269, "Belted Vest", 60, 85, typeof(Leather), "Leather", 10);
            AddCraft(typeof(BuckledVest), 1015269, "Buckled Vest", 60, 85, typeof(Leather), "Leather", 10);
            index = AddCraft(typeof(NobleTabbard), 1015269, "Noble Tabbard", 65, 90, typeof(Cloth), "Cloth", 18);
            AddRes(index, typeof(Leather), "Leather", 2);
            AddCraft(typeof(AscotJacket), 1015269, "Ascot Jacket", 65, 90, typeof(Cloth), "Cloth", 16);
            index = AddCraft(typeof(TunicShirt), 1015269, "Tunic Shirt", 60, 85, typeof(Cloth), "Cloth", 15);
            AddRes(index, typeof(Leather), "Leather", 1);
            AddCraft(typeof(HalfDress), 1015269, "Half Dress", 55, 70, typeof(Cloth), "Cloth", 8);
            // Teiravon custom - outer torso
            index = AddCraft(typeof(BeltedCoat), 1015269, "Belted Coat", 63, 78, typeof(Cloth), "Cloth", 18);
            AddRes(index, typeof(Leather), "Leather", 1);
            AddCraft(typeof(BeltedDress), 1015269, "Belted Dress", 63, 78, typeof(Cloth), "Cloth", 16);
            AddCraft(typeof(WinterCoat), 1015269, "Winter Coat", 70, 95, typeof(Cloth), "Cloth", 18);
            AddCraft(typeof(LayeredDress), 1015269, "Layered Dress", 60, 85, typeof(Cloth), "Cloth", 16);
            AddCraft(typeof(FancyGown), 1015269, "Fancy Gown", 75, 120, typeof(Cloth), "Cloth", 28);
            AddCraft(typeof(NobleGown), 1015269, "Noble Gown", 75, 120, typeof(Cloth), "Cloth", 25);
            AddCraft(typeof(SashDress), 1015269, "Sash Dress", 60, 85, typeof(Cloth), "Cloth", 25);
            AddCraft(typeof(PeasantGown), 1015269, "Peasant Gown", 60, 85, typeof(Cloth), "Cloth", 25);
            AddCraft(typeof(PeasantDress), 1015269, "Peasant Dress", 50, 75, typeof(Cloth), "Cloth", 20);
            AddCraft(typeof(HousewifeDress), 1015269, "Housewife Dress", 50, 75, typeof(Cloth), "Cloth", 20);
            AddCraft(typeof(BarwenchDress), 1015269, "Barwench Dress", 70, 95, typeof(Cloth), "Cloth", 25);
            AddCraft(typeof(LayeredRobe), 1015269, "Layered Robe", 65, 90, typeof(Cloth), "Cloth", 35);
            // Teiravon custom - cloaks
            AddCraft(typeof(HoodedCloak), 1015269, "Hooded Cloak", 95, 110, typeof(Cloth), "Cloth", 25);
            AddCraft(typeof(FancyCloak), 1015269, "Fancy Cloak", 75, 100, typeof(Cloth), "Cloth", 18);
            AddCraft(typeof(HoodedCape), 1015269, "Hooded Cape", 60, 85, typeof(Cloth), "Cloth", 20);

            if (Core.AOS)
            {
                AddCraft(typeof(FurCape), 1015269, 1028969, 35.0, 60.0, typeof(Cloth), 1044286, 13, 1044287);
                AddCraft(typeof(GildedDress), 1015269, 1028973, 37.5, 62.5, typeof(Cloth), 1044286, 16, 1044287);
                AddCraft(typeof(FormalShirt), 1015269, 1028975, 26.0, 51.0, typeof(Cloth), 1044286, 16, 1044287);
            }

			AddCraft( typeof( HoodedRobe ), 1015269, "hooded robe", 95.0, 120.0, typeof( Cloth ), "Cloth", 10 );

			// Pants
			AddCraft( typeof( ShortPants ), 1015279, 1025422, 24.8, 49.8, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( LongPants ), 1015279, 1025433, 24.8, 49.8, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Kilt ), 1015279, 1025431, 20.7, 45.7, typeof( Cloth ), 1044286, 8, 1044287 );
			AddCraft( typeof( Skirt ), 1015279, 1025398, 29.0, 54.0, typeof( Cloth ), 1044286, 10, 1044287 );
            AddCraft(typeof(PeasantSkirt), 1015279, "Peasant Skirt", 35, 60, typeof(Cloth), "Cloth", 12);
            AddCraft(typeof(PlainSkirt), 1015279, "Plain Skirt", 35, 60, typeof(Cloth), "Cloth", 11);
            AddCraft(typeof(LongSkirt), 1015279, "Long Skirt", 35, 60, typeof(Cloth), "Cloth", 12);

			if ( Core.AOS )
				AddCraft( typeof( FurSarong ), 1015279, 1028971, 35.0, 60.0, typeof( Cloth ), 1044286, 12, 1044287 );


			// Misc
			AddCraft( typeof( BodySash ), 1015283, 1025441, 4.1, 29.1, typeof( Cloth ), 1044286, 4, 1044287 );
			AddCraft( typeof( HalfApron ), 1015283, 1025435, 20.7, 45.7, typeof( Cloth ), 1044286, 6, 1044287 );
			AddCraft( typeof( FullApron ), 1015283, 1025437, 29.0, 54.0, typeof( Cloth ), 1044286, 10, 1044287 );
			AddCraft( typeof( OilCloth ), 1015283, 1041498, 74.6, 99.6, typeof( Cloth ), 1044286, 1, 1044287 );
			AddCraft( typeof( Bag ), 1015283, "bag", 10.0, 30.0, typeof( Leather ), "Leather", 1 );
			AddCraft( typeof( Pouch ), 1015283, "pouch", 10.0, 30.0, typeof( Leather ), "Leather", 2 );
			AddCraft( typeof( Backpack ), 1015283, "backpack", 10.0, 30.0, typeof( Leather ), "Leather", 2 );
			AddCraft( typeof( ClothGloves ), 1015283, "cloth gloves", 45.0, 65.0, typeof (Cloth), "Cloth", 3 );
            AddCraft(typeof(BattleNet), 1015283, "sturdy net", 60.0, 85.0, typeof(Leather), "Leather", 10);

			// Footwear
			if ( Core.AOS )
				AddCraft( typeof( FurBoots ), 1015288, 1028967, 50.0, 75.0, typeof( Cloth ), 1044286, 12, 1044287 );


			AddCraft( typeof( Sandals ), 1015288, 1025901, 12.4, 37.4, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( Shoes ), 1015288, 1025904, 16.5, 41.5, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( Boots ), 1015288, 1025899, 33.1, 58.1, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( ThighBoots ), 1015288, 1025906, 41.4, 66.4, typeof( Leather ), 1044462, 10, 1044463 );
            // Teiravon custom
            AddCraft(typeof(TravelingBoots), 1015288, "Traveling Boots", 55, 80, typeof(Leather), "Leather", 15);
            AddCraft(typeof(HeeledBoots), 1015288, "Heeled Boots", 60, 85, typeof(Leather), "Leather", 20);

			// Leather Armor
			AddCraft( typeof( LeatherGorget ), 1015293, 1025063, 53.9, 78.9, typeof( Leather ), 1044462, 4, 1044463 );
			AddCraft( typeof( LeatherCap ), 1015293, 1027609, 6.2, 31.2, typeof( Leather ), 1044462, 2, 1044463 );
			AddCraft( typeof( LeatherGloves ), 1015293, 1025062, 51.8, 76.8, typeof( Leather ), 1044462, 3, 1044463 );
			AddCraft( typeof( LeatherArms ), 1015293, 1025061, 53.9, 78.9, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( LeatherLegs ), 1015293, 1025067, 66.3, 91.3, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( LeatherChest ), 1015293, 1025068, 70.5, 95.5, typeof( Leather ), 1044462, 12, 1044463 );

			// Studded Armor
			AddCraft( typeof( StuddedGorget ), 1015300, 1025078, 78.8, 103.8, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( StuddedGloves ), 1015300, 1025077, 82.9, 107.9, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( StuddedArms ), 1015300, 1025076, 87.1, 112.1, typeof( Leather ), 1044462, 10, 1044463 );
			AddCraft( typeof( StuddedLegs ), 1015300, 1025082, 91.2, 116.2, typeof( Leather ), 1044462, 12, 1044463 );
			AddCraft( typeof( StuddedChest ), 1015300, 1025083, 94.0, 119.0, typeof( Leather ), 1044462, 14, 1044463 );

			if( Core.SE )
			{
				/*
				index = AddCraft( typeof( StuddedMempo ), 1015300, 1030216, 80.0, 105.0, typeof( Leather ), 1044462, 8, 1044463 );
				SetNeedSE( index, true );
				index = AddCraft( typeof( StuddedDo ), 1015300, 1030183, 95.0, 120.0, typeof( Leather ), 1044462, 14, 1044463 );
				SetNeedSE( index, true );
				index = AddCraft( typeof( StuddedHiroSode ), 1015300, 1030186, 85.0, 110.0, typeof( Leather ), 1044462, 8, 1044463 );
				SetNeedSE( index, true );
				index = AddCraft( typeof( StuddedSuneate ), 1015300, 1030194, 92.0, 117.0, typeof( Leather ), 1044462, 14, 1044463 );
				SetNeedSE( index, true );
				index = AddCraft( typeof( StuddedHaidate ), 1015300, 1030198, 92.0, 117.0, typeof( Leather ), 1044462, 14, 1044463 );
				SetNeedSE( index, true );
				*/
			}


			// Female Armor
			AddCraft( typeof( LeatherShorts ), 1015306, 1027168, 62.2, 87.2, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( LeatherSkirt ), 1015306, 1027176, 58.0, 83.0, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( LeatherBustierArms ), 1015306, 1027178, 58.0, 83.0, typeof( Leather ), 1044462, 6, 1044463 );
			AddCraft( typeof( StuddedBustierArms ), 1015306, 1027180, 82.9, 107.9, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( FemaleLeatherChest ), 1015306, 1027174, 62.2, 87.2, typeof( Leather ), 1044462, 8, 1044463 );
			AddCraft( typeof( FemaleStuddedChest ), 1015306, 1027170, 87.1, 112.1, typeof( Leather ), 1044462, 10, 1044463 );

			// Bone Armor
			index = AddCraft( typeof( BoneHelm ), 1049149, 1025206, 85.0, 110.0, typeof( Leather ), 1044462, 4, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 2, 1049063 );

			index = AddCraft( typeof( BoneGloves ), 1049149, 1025205, 89.0, 114.0, typeof( Leather ), 1044462, 6, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 2, 1049063 );

			index = AddCraft( typeof( BoneArms ), 1049149, 1025203, 92.0, 117.0, typeof( Leather ), 1044462, 8, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 4, 1049063 );

			index = AddCraft( typeof( BoneLegs ), 1049149, 1025202, 95.0, 120.0, typeof( Leather ), 1044462, 10, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 6, 1049063 );

			index = AddCraft( typeof( BoneChest ), 1049149, 1025199, 96.0, 121.0, typeof( Leather ), 1044462, 12, 1044463 );
			AddRes( index, typeof( Bone ), 1049064, 10, 1049063 );

            if ((m_Player.HasFeat(TeiravonMobile.Feats.RacialCrafting) || m_Player.HasFeat(TeiravonMobile.Feats.MasterCraftsman)) && (m_Player.IsTailor() || m_Player.IsMerchant()))
			{
				if (m_Player.IsHuman())
				{
					index = AddCraft( typeof( HumanFurCloak ), "Racials", "Fur Cloak", 95.0, 120.0, typeof( Cloth ), "Cloth", 40 );
					AddRes( index, typeof( Leather ), "Leather", 5 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( HumanStamBoots ), "Racials", "Boots of Running", 95.0, 120.0, typeof( Leather ), "Leather", 40 );
					AddRes( index, typeof( YellowScales ), "Yellow Scale", 1 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
				}
				else if (m_Player.IsOrc())
				{
					index = AddCraft( typeof( OrcBoots ), "Racials", "Spiked Boots", 95.0, 120.0, typeof( Leather ), "Leather", 40 );
					AddRes( index, typeof( BronzeIngot ), "Bronze Ingots", 3 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( OrcShamanMask ), "Racials", "Orc Shaman Mask", 95.0, 120.0, typeof( Cloth ), "Cloth", 40 );
					AddRes( index, typeof( AshwoodLog ), "Ashwood Log", 2 );
					AddRes( index, typeof( Ruby ), "Ruby", 1 );
				}
				else if (m_Player.IsDrow())
				{
					index = AddCraft( typeof( DrowRobe ), "Racials", "Shroud of Darkness", 95.0, 120.0, typeof( Cloth ), "Cloth", 40 );
					AddRes( index, typeof( SpidersSilk ), "Spider Silk", 80 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( Drowcloak ), "Racials", "Piwafwi", 95.0, 120.0, typeof( Cloth ), "Cloth", 30 );
					AddRes( index, typeof( SpidersSilk ), "Spider Silk", 20 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( Drowboots ), "Racials", "Padded Boots", 95.0, 120.0, typeof( Leather ), "Leather", 20 );
					AddRes( index, typeof( SpidersSilk ), "Spider Silk", 10 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( Drowmagerobe ), "Racials", "Spider Silk Mage Robe", 95.0, 115.0, typeof( SpidersSilk ), "Spider Silk", 400 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
				}
				else if (m_Player.IsDwarf())
				{
					index = AddCraft( typeof( DwarvenLeatherChest ), "Racials", "Dwarven Armor Chest", 85.0, 105.0, typeof( Leather ), "Leather", 35 );
					AddRes( index, typeof( DwarvenArmorPlate ), "Armor Plate", 8 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( DwarvenLeatherLegs ), "Racials", "Dwarven Armor Legs", 85.0, 105.0, typeof( Leather ), "Leather", 35 );
					AddRes( index, typeof( DwarvenArmorPlate ), "Armor Plate", 6 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( DwarvenLeatherArms ), "Racials", "Dwarven Armor Arms", 85.0, 105.0, typeof( Leather ), "Leather", 35 );
					AddRes( index, typeof( DwarvenArmorPlate ), "Armor Plate", 4 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( DwarvenLeatherGloves ), "Racials", "Dwarven Armor Gloves", 85.0, 105.0, typeof( Leather ), "Leather", 35 );
					AddRes( index, typeof( DwarvenArmorPlate ), "Armor Plate", 3 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( DwarvenLeatherGorget ), "Racials", "Dwarven Armor Gorget", 85.0, 105.0, typeof( Leather ), "Leather", 35 );
					AddRes( index, typeof( DwarvenArmorPlate ), "Armor Plate", 2 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( DwarvenLeatherCap ), "Racials", "Dwarven Armor Cap", 85.0, 105.0, typeof( Leather ), "Leather", 35 );
					AddRes( index, typeof( DwarvenArmorPlate ), "Armor Plate", 1 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
				}
				else if (m_Player.IsElf())
				{
					index = AddCraft( typeof( ElvenCloak ), "Racials", "Elven Cloak", 95.0, 120.0, typeof( Cloth ), "Cloth", 45 );
					AddRes( index, typeof( Vines ), "Vines", 2 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
					index = AddCraft( typeof( ElvenBoots ), "Racials", "Elven Boots", 95.0, 120.0, typeof( Leather ), "Leather", 30 );
					AddRes( index, typeof( Lilypad ), "Lilly pads", 2 );
					AddRes( index, typeof( SpoolOfThread ), "Thread", 5 );
                    AddCraft(typeof(ElvenShirt), "Racials", "Elven Shirt", 70, 95, typeof(Cloth), "Cloth", 15);
				}
			}
			

			// Set the overidable material
			SetSubRes( typeof( Leather ), 1049150 );

			// Add every material you want the player to be able to chose from
			// This will overide the overidable material
			AddSubRes( typeof( Leather ),		1049150, 00.0, 1044462, 1049311 );
			AddSubRes( typeof( SpinedLeather ),	1049151, 65.0, 1044462, 1049311 );
            AddSubRes( typeof( TuftedLeather), "TUFTED LEATHER / HIDES", 70.0, 1049311);
			AddSubRes( typeof( HornedLeather ),	1049152, 80.0, 1044462, 1049311 );
			AddSubRes( typeof( BarbedLeather ),	1049153, 90.0, 1044462, 1049311 );
            AddSubRes(typeof(ScaledLeather), "SCALED LEATHER / HIDES", 99.0, 1049311);

			MarkOption = true;
			Repair = true;
			CanEnhance = Core.AOS;
            			CanFinish = true;
		}
	}
}
