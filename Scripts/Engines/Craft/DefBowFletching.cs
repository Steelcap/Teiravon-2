using System;
using Server.Items;
using Server.Mobiles;
using Server.Teiravon;
using Addons;

namespace Server.Engines.Craft
{
	public class DefBowFletching : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Fletching;	}
		}

		public override int GumpTitleNumber
		{
			get { return 1044006; } // <CENTER>BOWCRAFT AND FLETCHING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBowFletching();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		public DefBowFletching() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
			/*

			base( MinCraftEffect, MaxCraftEffect, Delay )

			MinCraftEffect	: The minimum number of time the mobile will play the craft effect
			MaxCraftEffect	: The maximum number of time the mobile will play the craft effect
			Delay			: The delay between each craft effect

			Example: (3, 6, 1.7) would make the mobile do the PlayCraftEffect override
			function between 3 and 6 time, with a 1.7 second delay each time.

			*/
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
			//	from.Animate( 33, 5, 1, true, false, 0 );

			from.PlaySound( 0x55 );
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

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override void InitCraftList()
		{
			
		}
		
		public override void CustomSystem( Mobile thePlayer )
		{

			TeiravonMobile m_Player = (TeiravonMobile)thePlayer;

			int index = -1;
			// Ammunition
			index = AddCraft( typeof( Arrow ), 1044565, 1023903, 0.0, 40.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( Bolt ), 1044565, 1027163, 0.0, 40.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( Shaft ), 1044565, 1027124, 0.0, 40.0, typeof( Log ), 1044041, 1, 1044351 );
			SetUseAllRes( index, true );

			AddCraft( typeof( Bow ), "Bows", "Shortbow", 15.0, 35.0, typeof( Log ), "Log", 5 );
			AddCraft( typeof( Longbow ), "Bows", "Longbow", 30.0, 60.0, typeof( Log ), "Log", 8 );
			AddCraft( typeof( CompositeBow ), "Bows", "Composite bow", 60.0, 90.0, typeof( Log ), "Log", 10 );
			AddCraft( typeof( Recurve ), "Bows", "Recurve bow", 85.0, 100.0, typeof( Log ), "Log", 10 );
            AddCraft( typeof ( GreatBow ), "Bows", "Great bow", 95.0, 120.0, typeof( Log ), "Log", 20);

			AddCraft( typeof( HandCrossbow ), "Bows", "Hand Crossbow", 35.0, 65.0, typeof( Log ), "Log", 8 );
			AddCraft( typeof( Crossbow ), "Bows", "Crossbow", 65.0, 95.0, typeof( Log ), "Log", 10 );
			index = AddCraft( typeof( HeavyCrossbow ), "Bows", "Heavy Crossbow", 95.0, 105.0, typeof( Log ), "Log", 12 );
				AddRes( index, typeof( Gears ), "Gears", 2 );
			index = AddCraft( typeof( LRepeatingXBow ), "Bows", "Light Repeating Crossbow", 75.0, 100.0, typeof( Log ), "Log", 5 );
				AddRes( index, typeof( ValoriteIngot ), "Valorite Ingots", 1 ); AddRes( index, typeof( Gears ), "Gears", 2 );
			index = AddCraft( typeof( RepeatingCrossbow ), "Bows", "Repeating Crossbow", 95.0, 105.0, typeof( Log ), "Log", 8 );
				AddRes( index, typeof( ValoriteIngot ), "Valorite Ingots", 2 ); AddRes( index, typeof( Gears ), "Gears", 3 );

                if ((m_Player.HasFeat(TeiravonMobile.Feats.RacialCrafting) || m_Player.HasFeat(TeiravonMobile.Feats.MasterCraftsman)) && (m_Player.IsWoodworker() || m_Player.IsMerchant()))
			{
				if (m_Player.IsHuman())
				{
					index = AddCraft( typeof( HumanCrossbow ), "Racials", "Armor Piercing Crossbow", 85.0, 105.0, typeof( Log ), "Logs", 50 );
					AddRes( index, typeof( AgapiteIngot ), "Agapite Ingots", 10 );
					AddRes( index, typeof( Gears ), "Gears", 5 );
                    index = AddCraft(typeof(HumanAPBolt), "Racials", "Armor Piercing Bolt", 85.0, 105.0, typeof(Shaft), "Shaft", 1);
					AddRes( index, typeof( DullCopperIngot ), "Dull Copper Ingots", 1 );
					SetUseAllRes( index, true );
				}
				else if (m_Player.IsOrc())
				{
                    index = AddCraft(typeof(OrcHorngothBow), "Racials", "Horngoth Bow", 85.0, 105.0, typeof(Log), "Logs", 50);
					AddRes( index, typeof( BronzeIngot ), "Bronze Ingots", 4 );
                    index = AddCraft(typeof(OrcSling), "Racials", "Sling", 85.0, 105.0, typeof(Log), "Logs", 5);
					AddRes( index, typeof( Cloth ), "Cloth", 10 );
                    index = AddCraft(typeof(OrcSlingBullet), "Racials", "Sling Bullet", 85.0, 105.0, typeof(ShadowIronIngot), "Shadow Iron Ingots", 1);
				}
				else if (m_Player.IsDrow())
				{
                    index = AddCraft(typeof(Drowhandbow), "Racials", "Drow Hand Crossbow", 85.0, 105.0, typeof(Log), "Logs", 50);
					AddRes( index, typeof( Gears ), "Gears", 1 );
					AddRes( index, typeof( PoisonPotion ), "Poison Potion", 1 );
                    index = AddCraft(typeof(Drowxbow), "Racials", "Drow Crossbow", 85.0, 105.0, typeof(Log), "Logs", 50);
					AddRes( index, typeof( Gears ), "Gears", 3 );
					AddRes( index, typeof( SpidersSilk ), "Spider Silk", 10 );
					
					AddCraft( typeof( ReinforcedHandXBowFrame ), "Racials", "Reinforced Hand Crossbow Frame", 90.0, 120.0, typeof( Board ), "Boards", 10 );
				}
				else if (m_Player.IsDwarf())
				{
                    index = AddCraft(typeof(BallistaEastAddonDeed), "Racials", "Ballista", 85.0, 105.0, typeof(WhitePineLog), "White Logs", 500);
					AddRes( index, typeof( Gears ), "Gears", 50 );
					AddRes( index, typeof( DullCopperIngot ), "Dull Copper Ingots", 100 );
                    index = AddCraft(typeof(DwarvenCrossbow), "Racials", "Dwarven Crossbow", 85.0, 105.0, typeof(Log), "Logs", 30);
					AddRes( index, typeof( Gears ), "Gears", 5 );
					AddRes( index, typeof( DullCopperIngot ), "Dull Copper Ingots", 8 );
                    index = AddCraft(typeof(DwarvenBallistaBolt), "Racials", "Ballista Bolt", 85.0, 105.0, typeof(AshwoodLog), "Ashwood Logs", 1);
					AddRes( index, typeof( IronIngot ), "Ingots", 2 );
                    index = AddCraft(typeof(DwarvenBolt), "Racials", "Iron Bolt", 85.0, 105.0, typeof(IronIngot), "Ingots", 1);
				}
				else if (m_Player.IsElf())
				{
                    index = AddCraft(typeof(Elvenbow), "Racials", "Elven Bow", 85.0, 105.0, typeof(Log), "Logs", 25);
					AddRes( index, typeof( ValoriteIngot ), "Valorite Ingots", 1 );
					AddRes( index, typeof( KnotGrass ), "Knot Grass", 2 );
                    index = AddCraft(typeof(ElvenQuiver), "Racials", "Elven Quiver", 85.0, 105.0, typeof(Cloth), "Cloth", 10);
					AddRes( index, typeof( KnotGrass ), "Knot Grass", 2 );
					AddRes( index, typeof( Diamond ), "Diamond", 1 );

                    AddCraft(typeof(ReinforcedBowFrame), "Racials", "Reinforced Shortbow Frame", 85.0, 105.0, typeof(Log), "Log", 5);
                    AddCraft(typeof(ReinforcedLongbowFrame), "Racials", "Reinforced Longbow Frame", 85.0, 105.0, typeof(Log), "Log", 5);
				}
			}


			// Set the overidable material
			SetSubRes( typeof( Log ), "Oak" );

			// Add every material you want the player to be able to chose from
			// This will overide the overidable material
			AddSubRes( typeof( Log ), "Oak", 0.0, "You are not skilled enough to use that material." );
			AddSubRes( typeof( PineLog ), "Pine", 35.0, "You are not skilled enough to use that material" );
			AddSubRes( typeof( RedwoodLog ), "Redwood", 45.0, "You are not skilled enough to use that material" );
			AddSubRes( typeof( WhitePineLog ), "White Pine", 55.0, "You are not skilled enough to use that material" );
			AddSubRes( typeof( AshwoodLog ), "Ashwood", 65.0, "You are not skilled enough to use that material" );
			AddSubRes( typeof( SilverBirchLog ), "Silver Birch", 75.0, "You are not skilled enough to use that material" );
			AddSubRes( typeof( YewLog ), "Yew", 85.0, "You are not skilled enough to use that material" );
			AddSubRes( typeof( BlackOakLog ), "Black Oak", 90.0, "You are not skilled enough to use that material" );

			MarkOption = true;
			CanEnhance = false;
            CanFinish = true;
			Repair = true;

		}
	}
}
