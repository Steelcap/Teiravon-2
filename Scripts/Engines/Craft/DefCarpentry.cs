using System;
using Server.Items;
using Server.Teiravon;
using Server.Mobiles;
using Server.Multis;
using Server.Multis.Deeds;

namespace Server.Engines.Craft
{
	public class DefCarpentry : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Carpentry;	}
		}

		public override int GumpTitleNumber
		{
			get { return 1044004; } // <CENTER>CARPENTRY MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefCarpentry();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		public DefCarpentry() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
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
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			from.PlaySound( 0x23D );
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

			// Other Items
            index = AddCraft(typeof(Board), 1044294, "Boards", 0.0, 20.0, typeof(Log), "Logs", 1, 1044351);
            SetUseAllRes(index, true);
			AddCraft( typeof( BarrelStaves ),				1044294, 1027857,	00.0,  25.0,	typeof( Log ), 1044041,  5, 1044351 );
			AddCraft( typeof( BarrelLid ),					1044294, 1027608,	11.0,  36.0,	typeof( Log ), 1044041,  4, 1044351 );
			AddCraft( typeof( ShortMusicStand ),				1044294, 1044313,	78.9, 100.0,	typeof( Log ), 1044041, 15, 1044351 );
			AddCraft( typeof( TallMusicStand ),				1044294, 1044315,	81.5, 100.0,	typeof( Log ), 1044041, 20, 1044351 );
			AddCraft( typeof( Easle ),					1044294, 1044317,	86.8, 100.0,	typeof( Log ), 1044041, 20, 1044351 );

			if( Core.SE )
			{
				/*
				index = AddCraft( typeof( RedHangingLantern ), 1044294, 1029412, 65.0, 90.0, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( BlankScroll ), 1044377, 10, 1044378 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( WhiteHangingLantern ), 1044294, 1029416, 65.0, 90.0, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( BlankScroll ), 1044377, 10, 1044378 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( ShojiScreen ), 1044294, 1029423, 80.0, 105.0, typeof( Log ), 1044041, 75, 1044351 );
				AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
				AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( BambooScreen ), 1044294, 1029428, 80.0, 105.0, typeof( Log ), 1044041, 75, 1044351 );
				AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
				AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
				SetNeedSE( index, true );
				*/
			}

			// Furniture
			AddCraft( typeof( FootStool ),					1044291, 1022910,	11.0,  36.0,	typeof( Log ), 1044041,  9, 1044351 );
			AddCraft( typeof( Stool ),					1044291, 1022602,	11.0,  36.0,	typeof( Log ), 1044041,  9, 1044351 );
			AddCraft( typeof( BambooChair ),				1044291, 1044300,	21.0,  46.0,	typeof( Log ), 1044041, 13, 1044351 );
			AddCraft( typeof( WoodenChair ),				1044291, 1044301,	21.0,  46.0,	typeof( Log ), 1044041, 13, 1044351 );
			AddCraft( typeof( WoodenChairCushion ),			1044291, "cushioned chair",	42.1,  67.1,	typeof( Log ), "Boards or Logs", 13, 1044351 );
			AddCraft( typeof( FancyWoodenChairCushion ),	1044291, "fancy cushioned chair",	42.1,  67.1,	typeof( Log ), "Boards or Logs", 15, 1044351 );
			AddCraft( typeof( WoodenBench ),				1044291, 1022860,	52.6,  77.6,	typeof( Log ), 1044041, 17, 1044351 );
			AddCraft( typeof( WoodenThrone ),			1044291, "wooden throne",	52.6,  77.6,	typeof( Log ), "Boards or Logs", 17, 1044351 );
			AddCraft( typeof( Throne ),				1044291, "royal throne",	73.6,  98.6,	typeof( Log ), "Boards or Logs", 19, 1044351 );
			AddCraft( typeof( Nightstand ),					1044291, 1044306,	42.1,  67.1,	typeof( Log ), 1044041, 17, 1044351 );
            AddCraft( typeof( Counter ),                    1044291, "Counter", 62.1, 87.1,     typeof(Log), 1044041, 22, 1044351);
			AddCraft( typeof( WritingTable ),				1044291, 1022890,	63.1,  88.1,	typeof( Log ), 1044041, 17, 1044351 );
			AddCraft( typeof( YewWoodTable ),			1044291, "elegant table",	63.1,  88.1,	typeof( Log ), "Boards or Logs", 23, 1044351 );
			AddCraft( typeof( LargeTable ),				1044291, "dining table",	80.2, 100.0,	typeof( Log ), "Boards or Logs", 27, 1044351 );

			if( Core.SE )
			{
				/*
				index = AddCraft( typeof( ElegantLowTable ),	1044291, 1030265,	80.0, 105.0,	typeof( Log ), 1044041, 35, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( PlainLowTable ),		1044291, 1030266,	80.0, 105.0,	typeof( Log ), 1044041, 35, 1044351 );
				SetNeedSE( index, true );
				*/
			}

			// Containers
			AddCraft( typeof( WoodenBox ),					1044292, 1023709,	21.0,  46.0,	typeof( Log ), 1044041, 10, 1044351 );
			AddCraft( typeof( SmallCrate ),					1044292, 1044309,	10.0,  35.0,	typeof( Log ), 1044041, 8 , 1044351 );
			AddCraft( typeof( MediumCrate ),				1044292, 1044310,	31.0,  56.0,	typeof( Log ), 1044041, 15, 1044351 );
			AddCraft( typeof( LargeCrate ),					1044292, 1044311,	47.3,  72.3,	typeof( Log ), 1044041, 18, 1044351 );
			AddCraft( typeof( WoodenChest ),				1044292, 1023650,	73.6,  98.6,	typeof( Log ), 1044041, 20, 1044351 );
			AddCraft( typeof( EmptyBookcase ),				1044292, 1022718,	31.5,  56.5,	typeof( Log ), 1044041, 25, 1044351 );
			AddCraft( typeof( FancyArmoire ),				1044292, 1044312,	84.2, 105.0,	typeof( Log ), 1044041, 35, 1044351 );
			AddCraft( typeof( Armoire ),					1044292, 1022643,	84.2, 105.0,	typeof( Log ), 1044041, 35, 1044351 );
			index = AddCraft( typeof( GraniteBox ), 1044292, "Granite Storage Box", 80.0, 100.0, typeof( Log ), 1044041, 10, 1044351 );
			AddRes( index, typeof( Granite ), "Granite", 1 );
            index = AddCraft(typeof(Barrel), 1044292, "Barrel", 57.8, 82.8, typeof(BarrelStaves), 1044288, 6, 1044253);
            AddRes(index, typeof(BarrelHoops), 1044289, 2, 1044253);

			if( Core.SE )
			{
				/*
				index = AddCraft( typeof( PlainWoodenChest ),	1044292, 1030251, 90.0, 115.0,	typeof( Log ), 1044041, 30, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( OrnateWoodenChest ),	1044292, 1030253, 90.0, 115.0,	typeof( Log ), 1044041, 30, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( GildedWoodenChest ),	1044292, 1030255, 90.0, 115.0,	typeof( Log ), 1044041, 30, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( WoodenFootLocker ),	1044292, 1030257, 90.0, 115.0,	typeof( Log ), 1044041, 30, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( FinishedWoodenChest ),1044292, 1030259, 90.0, 115.0,	typeof( Log ), 1044041, 30, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( TallCabinet ),	1044292, 1030261, 90.0, 115.0,	typeof( Log ), 1044041, 35, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( ShortCabinet ),	1044292, 1030263, 90.0, 115.0,	typeof( Log ), 1044041, 35, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( RedArmoire ),	1044292, 1030328, 90.0, 115.0,	typeof( Log ), 1044041, 40, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( ElegantArmoire ),	1044292, 1030330, 90.0, 115.0,	typeof( Log ), 1044041, 40, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( MapleArmoire ),	1044292, 1030328, 90.0, 115.0,	typeof( Log ), 1044041, 40, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( CherryArmoire ),	1044292, 1030328, 90.0, 115.0,	typeof( Log ), 1044041, 40, 1044351 );
				SetNeedSE( index, true );
				*/
			}

			index = AddCraft( typeof( Keg ), 1044292, 1023711, 57.8, 82.8, typeof( BarrelStaves ), 1044288, 3, 1044253 );
			AddRes( index, typeof( BarrelHoops ), 1044289, 1, 1044253 );
			AddRes( index, typeof( BarrelLid ), 1044251, 1, 1044253 );

			// Staves and Shields
			AddCraft( typeof( ShepherdsCrook ), 1015108, "shepherd\'s crook", 78.9, 103.9, typeof( Log ), "Logs", 7 );
			AddCraft( typeof( QuarterStaff ), 1015108, "quarter staff", 73.6, 98.6, typeof( Log ), "Logs", 6 );
			AddCraft( typeof( GnarledStaff ), 1015108, "gnarled staff", 78.9, 103.9, typeof( Log ), "Logs", 7 );
			AddCraft( typeof( WoodenShield ), 1015108, "wooden shield", 52.6, 77.6, typeof( Log ), "Logs", 9 );
			index = AddCraft( typeof( FishingPole ), 1015108, "fising pole", 68.4, 93.4, typeof( Log ), "Logs", 5 );
			AddRes( index, typeof( Cloth ), 1044286, 5, 1044287 );

			if( Core.SE )
			{
				/*
				index = AddCraft( typeof( Bokuto ), 1044295, 1030227, 70.0, 95.0, typeof( Log ), 1044041, 6, 1044351 );
				SetNeedSE( index, true );

				//index = AddCraft( typeof( Fukiya ), 1044295, 1030229, 60.0, 85.0, typeof( Log ), 1044041, 6, 1044351 );
				//SetNeedSE( index, true );

				index = AddCraft( typeof( Tetsubo ), 1044295, 1030225, 85.0, 110.0, typeof( Log ), 1044041, 8, 1044351 );
				AddSkill( index, SkillName.Tinkering, 40.0, 45.0 );
				AddRes( index, typeof( IronIngot ), 1044036, 5, 1044037 );
				SetNeedSE( index, true );
				*/
			}


			// Instruments
			index = AddCraft( typeof( LapHarp ), 1044293, 1023762, 63.1, 88.1, typeof( Log ), 1044041, 20, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );

			index = AddCraft( typeof( Harp ), 1044293, 1023761, 78.9, 103.9, typeof( Log ), 1044041, 35, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 15, 1044287 );

			index = AddCraft( typeof( Drums ), 1044293, 1023740, 57.8, 82.8, typeof( Log ), 1044041, 20, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );

			index = AddCraft( typeof( Lute ), 1044293, 1023763, 68.4, 93.4, typeof( Log ), 1044041, 25, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );

			index = AddCraft( typeof( Tambourine ), 1044293, 1023741, 57.8, 82.8, typeof( Log ), 1044041, 15, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );

			index = AddCraft( typeof( TambourineTassel ), 1044293, 1044320, 57.8, 82.8, typeof( Log ), 1044041, 15, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 15, 1044287 );

			// Misc
			index = AddCraft( typeof( SmallBedSouthDeed ), 1044290, 1044321, 94.7, 113.1, typeof( Log ), 1044041, 100, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 100, 1044287 );
			index = AddCraft( typeof( SmallBedEastDeed ), 1044290, 1044322, 94.7, 113.1, typeof( Log ), 1044041, 100, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 100, 1044287 );
			index = AddCraft( typeof( LargeBedSouthDeed ), 1044290,1044323, 94.7, 113.1, typeof( Log ), 1044041, 150, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 150, 1044287 );
			index = AddCraft( typeof( LargeBedEastDeed ), 1044290, 1044324, 94.7, 113.1, typeof( Log ), 1044041, 150, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 150, 1044287 );
			index = AddCraft( typeof( PentagramDeed ), 1044290, 1044328, 100.0, 125.0, typeof( Log ), 1044041, 100, 1044351 );
			AddRes( index, typeof( IronIngot ), 1044036, 40, 1044037 );
			index = AddCraft( typeof( AbbatoirDeed ), 1044290, 1044329, 100.0, 125.0, typeof( Log ), 1044041, 100, 1044351 );
			AddRes( index, typeof( IronIngot ), 1044036, 40, 1044037 );

			if ( Core.AOS )
			{
				AddCraft( typeof( PlayerBBEast ), 1044290, 1062420, 85.0, 110.0, typeof( Log ), 1044041, 50, 1044351 );
				AddCraft( typeof( PlayerBBSouth ), 1044290, 1062421, 85.0, 110.0, typeof( Log ), 1044041, 50, 1044351 );
			}

			// Blacksmithy
			index = AddCraft( typeof( SmallForgeDeed ), 1044296, 1044330, 73.6, 98.6, typeof( Log ), 1044041, 5, 1044351 );
			AddRes( index, typeof( IronIngot ), 1044036, 75, 1044037 );
			index = AddCraft( typeof( LargeForgeEastDeed ), 1044296, 1044331, 78.9, 103.9, typeof( Log ), 1044041, 5, 1044351 );
			AddRes( index, typeof( IronIngot ), 1044036, 100, 1044037 );
			index = AddCraft( typeof( LargeForgeSouthDeed ), 1044296, 1044332, 78.9, 103.9, typeof( Log ), 1044041, 5, 1044351 );
			AddRes( index, typeof( IronIngot ), 1044036, 100, 1044037 );
			index = AddCraft( typeof( AnvilEastDeed ), 1044296, 1044333, 73.6, 98.6, typeof( Log ), 1044041, 5, 1044351 );
			AddRes( index, typeof( IronIngot ), 1044036, 150, 1044037 );
			index = AddCraft( typeof( AnvilSouthDeed ), 1044296, 1044334, 73.6, 98.6, typeof( Log ), 1044041, 5, 1044351 );
			AddRes( index, typeof( IronIngot ), 1044036, 150, 1044037 );

			// Training
			index = AddCraft( typeof( TrainingDummyEastDeed ), 1044297, 1044335, 68.4, 93.4, typeof( Log ), 1044041, 55, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
			index = AddCraft( typeof( TrainingDummySouthDeed ), 1044297, 1044336, 68.4, 93.4, typeof( Log ), 1044041, 55, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
			index = AddCraft( typeof( PickpocketDipEastDeed ), 1044297, 1044337, 73.6, 98.6, typeof( Log ), 1044041, 65, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );
			index = AddCraft( typeof( PickpocketDipSouthDeed ), 1044297, 1044338, 73.6, 98.6, typeof( Log ), 1044041, 65, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 60, 1044287 );

			// Tailoring
			index = AddCraft( typeof( Dressform ), 1044298, 1044339, 63.1, 88.1, typeof( Log ), 1044041, 25, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );
			index = AddCraft( typeof( SpinningwheelEastDeed ), 1044298, 1044341, 73.6, 98.6, typeof( Log ), 1044041, 75, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );
			index = AddCraft( typeof( SpinningwheelSouthDeed ), 1044298, 1044342, 73.6, 98.6, typeof( Log ), 1044041, 75, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );
			index = AddCraft( typeof( LoomEastDeed ), 1044298, 1044343, 84.2, 109.2, typeof( Log ), 1044041, 85, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );
			index = AddCraft( typeof( LoomSouthDeed ), 1044298, 1044344, 84.2, 109.2, typeof( Log ), 1044041, 85, 1044351 );
			AddRes( index, typeof( Cloth ), 1044286, 25, 1044287 );

			// Cooking
			index = AddCraft( typeof( StoneOvenEastDeed ), 1044298, 1044345, 68.4, 93.4, typeof( Log ), 1044041, 85, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 125, 1044037 );
			index = AddCraft( typeof( StoneOvenSouthDeed ), 1044298, 1044346, 68.4, 93.4, typeof( Log ), 1044041, 85, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 125, 1044037 );
			index = AddCraft( typeof( FlourMillEastDeed ), 1044298, 1044347, 94.7, 119.7, typeof( Log ), 1044041, 100, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 50, 1044037 );
			index = AddCraft( typeof( FlourMillSouthDeed ), 1044298, 1044348, 94.7, 119.7, typeof( Log ), 1044041, 100, 1044351 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( IronIngot ), 1044036, 50, 1044037 );
			AddCraft( typeof( WaterTroughEastDeed ), 1044298, 1044349, 94.7, 119.7, typeof( Log ), 1044041, 150, 1044351 );
			AddCraft( typeof( WaterTroughSouthDeed ), 1044298, 1044350, 94.7, 119.7, typeof( Log ), 1044041, 150, 1044351 );

            //Frostcraft
            if (m_Player.IsFrostling())
            {
                index = AddCraft(typeof(FrostPick), "Frostcraft", "Frostpick", 0.0, 20.0, typeof(Snow), "Snow", 2);
                AddRes(index, typeof(Ice), "Frost", 1);
                AddCraft(typeof(Frostcarver), "Frostcraft", "Frostcarver", 0, 25.0, typeof(Snow), "Snow", 3);

            }
            /*
            // Housing
            index = AddCraft(typeof(StonePlasterHouseDeed), "Housing", "Modest Stone and Plaster House", 95.0, 100.0, typeof(Log), 1044041, 400, 1044351);
            AddRes ( index, typeof ( Granite ), "Stone", 100);
            AddRes ( index, typeof (Nails ), "Nails", 500);
*/
            if ((m_Player.HasFeat(TeiravonMobile.Feats.RacialCrafting) || m_Player.HasFeat(TeiravonMobile.Feats.MasterCraftsman)) && (m_Player.IsWoodworker() || m_Player.IsMerchant()))
			{
				if (m_Player.IsHuman())
				{
					index = AddCraft( typeof( SmallBoatDeed ), "Racials", "Small Boat", 95.0, 120.0, typeof( RedwoodLog ), "Red Wood Logs", 500 );
					AddRes( index, typeof( IronIngot ), "Iron Ingots", 100 );
					AddRes( index, typeof( Cloth ), "Cloth", 100 );
					index = AddCraft( typeof( SmallDragonBoatDeed ), "Racials", "Small Dragon Boat", 95.0, 120.0, typeof( RedwoodLog ), "Red Wood Logs", 500 );
					AddRes( index, typeof( IronIngot ), "Iron Ingots", 100 );
					AddRes( index, typeof( Cloth ), "Cloth", 100 );
					index = AddCraft( typeof( MediumBoatDeed ), "Racials", "Medium Boat", 95.0, 120.0, typeof( RedwoodLog ), "Red Wood Logs", 750 );
					AddRes( index, typeof( IronIngot ), "Iron Ingots", 150 );
					AddRes( index, typeof( Cloth ), "Cloth", 150 );
					index = AddCraft( typeof( MediumDragonBoatDeed ), "Racials", "Medium Dragon Boat", 95.0, 120.0, typeof( RedwoodLog ), "Red Wood Logs", 750 );
					AddRes( index, typeof( IronIngot ), "Iron Ingots", 150 );
					AddRes( index, typeof( Cloth ), "Cloth", 150 );
					index = AddCraft( typeof( LargeBoatDeed ), "Racials", "Large Boat", 95.0, 120.0, typeof( RedwoodLog ), "Red Wood Logs", 1000 );
					AddRes( index, typeof( IronIngot ), "Iron Ingots", 200 );
					AddRes( index, typeof( Cloth ), "Cloth", 200 );
					index = AddCraft( typeof( LargeDragonBoatDeed ), "Racials", "Large Dragon Boat", 95.0, 120.0, typeof( RedwoodLog ), "Red Wood Logs", 1000 );
					AddRes( index, typeof( IronIngot ), "Iron Ingots", 200 );
					AddRes( index, typeof( Cloth ), "Cloth", 200 );
				}
				else if (m_Player.IsOrc())
				{
					index = AddCraft( typeof( OrcPalisadeSouthDeed ), "Racials", "Palisade (south)", 95.0, 120.0, typeof( PineLog ), "Pine Logs", 100 );
					index = AddCraft( typeof( OrcPalisadeEastDeed ), "Racials", "Palisade (east)", 95.0, 120.0, typeof( PineLog ), "Pine Logs", 100 );
					index = AddCraft( typeof( OrcCombatBridgeDeed ), "Racials", "Combat Bridge Section", 95.0, 120.0, typeof( PineLog ), "Pine Logs", 100 );
				}
				else if (m_Player.IsDrow())
				{
					index = AddCraft( typeof( DrowBladedStaff ), "Racials", "Drow Bladed Staff", 95.0, 120.0, typeof( Log ), "Logs", 15 );
					AddRes( index, typeof( VeriteIngot ), "Verite Ingots", 10 );
					index = AddCraft( typeof( DrowDoubleBladedStaff ), "Racials", "Drow Double Bladed Staff", 95.0, 120.0, typeof( Log ), "Logs", 15 );
					AddRes( index, typeof( VeriteIngot ), "Verite Ingots", 20 );
				}
				else if (m_Player.IsDwarf())
				{
				}
				else if (m_Player.IsElf())
				{
					index = AddCraft( typeof( ElvenPracticeSword ), "Racials", "Practice Sword", 95.0, 120.0, typeof( YewLog ), "Yew Logs", 10 );
					index = AddCraft( typeof( ElvenShield ), "Racials", "Elven Shield", 95.0, 120.0, typeof( Log ), "Logs", 25 );
					AddRes( index, typeof( HornedLeather ), "Horned Leather", 10 );
				}
			}
			
			
			// Set the overidable material
			SetSubRes( typeof( Log ), "Oak" );

			// Add every material you want the player to be able to chose from
			// This will overide the overidable material
			AddSubRes( typeof( Log ), "Oak", 0.0, "You don't have enough skill/resources for that." );
			AddSubRes( typeof( PineLog ), "Pine", 35.0, "You don't have enough skill/resources for that." );
			AddSubRes( typeof( RedwoodLog ), "Redwood", 55.0, "You don't have enough skill/resources for that." );
			AddSubRes( typeof( WhitePineLog ), "White Pine", 65.0, "You don't have enough skill/resources for that." );
			AddSubRes( typeof( AshwoodLog ), "Ashwood", 75.0, "You don't have enough skill/resources for that." );
			AddSubRes( typeof( SilverBirchLog ), "Silver Birch", 85.0, "You don't have enough skill/resources for that." );
			AddSubRes( typeof( YewLog ), "Yew", 95.0, "You don't have enough skill/resources for that." );
			AddSubRes( typeof( BlackOakLog ), "Black Oak", 100.0, "You don't have enough skill/resources for that." );

			MarkOption = true;
			Repair = Core.AOS;
			CanEnhance = false;
            CanFinish = true;
		}
	}
}
