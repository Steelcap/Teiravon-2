using System;
using Server;
using Server.Items;
using Server.Factions;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Engines.Craft
{
	public class DefTinkering : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tinkering; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044007; } // <CENTER>TINKERING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTinkering();

				return m_CraftSystem;
			}
		}

		public DefTinkering() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			if ( item.NameNumber == 1044258 || item.NameNumber == 1046445 ) // potion keg and faction trap removal kit
				return 0.5; // 50%

			return 0.0; // 0%
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.
			else if ( itemType != null && ( itemType.IsSubclassOf( typeof( BaseFactionTrapDeed ) ) || itemType == typeof( FactionTrapRemovalKit ) ) && Faction.Find( from ) == null )
				return 1044573; // You have to be in a faction to do that.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no sound
			//from.PlaySound( 0x241 );
		}

		private static Type[] m_TinkerColorables = new Type[]
			{
				typeof( ForkLeft ), typeof( ForkRight ),
				typeof( SpoonLeft ), typeof( SpoonRight ),
				typeof( KnifeLeft ), typeof( KnifeRight ),
				typeof( Plate ),
				typeof( Goblet ), typeof( PewterMug ),
				// Key ring
				typeof( Candelabra ), typeof( Scales ),
				// Keys
				typeof( Globe ),
				typeof( Spyglass ), typeof( Lantern ),
				typeof( HeatingStand )
			};

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( !type.IsSubclassOf( typeof( BaseIngot ) ) )
				return false;

			type = item.ItemType;

			bool contains = false;

			for ( int i = 0; !contains && i < m_TinkerColorables.Length; ++i )
				contains = ( m_TinkerColorables[i] == type );

			return contains;
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

		public override bool ConsumeOnFailure( Mobile from, Type resourceType, CraftItem craftItem )
		{
			if ( resourceType == typeof( Silver ) )
				return false;

			return base.ConsumeOnFailure( from, resourceType, craftItem );
		}

		public void AddJewelrySet( GemType gemType, Type itemType )
		{
			int offset = (int)gemType - 1;

            int index = AddCraft(typeof(GoldRing), 1044049, 1044176 + offset, 40.0, 90.0, typeof(IronIngot), 1044036, 2, 1044037);
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

            index = AddCraft(typeof(SilverBeadNecklace), 1044049, 1044185 + offset, 40.0, 90.0, typeof(IronIngot), 1044036, 2, 1044037);
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

            index = AddCraft(typeof(GoldNecklace), 1044049, 1044194 + offset, 40.0, 90.0, typeof(IronIngot), 1044036, 2, 1044037);
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

            index = AddCraft(typeof(GoldEarrings), 1044049, 1044203 + offset, 40.0, 90.0, typeof(IronIngot), 1044036, 2, 1044037);
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

            index = AddCraft(typeof(GoldBeadNecklace), 1044049, 1044212 + offset, 40.0, 90.0, typeof(IronIngot), 1044036, 2, 1044037);
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

            index = AddCraft(typeof(GoldBracelet), 1044049, 1044221 + offset, 40.0, 90.0, typeof(IronIngot), 1044036, 2, 1044037);
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );
		}

		public override void InitCraftList()
		{
			
		}
		
		public override void CustomSystem( Mobile thePlayer )
		{

			TeiravonMobile m_Player = (TeiravonMobile)thePlayer;
			int index = -1;

			#region Wooden Items
			AddCraft( typeof( JointingPlane ), 1044042, 1024144, 0.0, 50.0, typeof( Log ), 1044041, 4, 1044351 );
			AddCraft( typeof( MouldingPlane ), 1044042, 1024140, 0.0, 50.0, typeof( Log ), 1044041, 4, 1044351 );
			AddCraft( typeof( SmoothingPlane ), 1044042, 1024146, 0.0, 50.0, typeof( Log ), 1044041, 4, 1044351 );
			AddCraft( typeof( ClockFrame ), 1044042, 1024173, 0.0, 50.0, typeof( Log ), 1044041, 6, 1044351 );
			AddCraft( typeof( Axle ), 1044042, 1024187, -25.0, 25.0, typeof( Log ), 1044041, 2, 1044351 );
			AddCraft( typeof( RollingPin ), 1044042, 1024163, 0.0, 50.0, typeof( Log ), 1044041, 5, 1044351 );

			if( Core.SE )
			{
			/*
				index = AddCraft( typeof( Nunchaku ), 1044042, 1030158, 70.0, 120.0, typeof( IronIngot ), 1044036, 3, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				SetNeedSE( index, true );
			*/
			}
			#endregion

			#region Tools
			AddCraft( typeof( Scissors ), 1044046, 1023998, 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( MortarPestle ), 1044046, 1023739, 5.0, 5.0,  typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( Scorp ), 1044046, 1024327, 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( TinkerTools ), 1044046, 1044164, 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Hatchet ), 1044046, 1023907, 5.0, 5.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( DrawKnife ), 1044046, 1024324, 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SewingKit ), 1044046, 1023997, 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Saw ), 1044046, 1024148, 5.0, 5.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( DovetailSaw ), 1044046, 1024136, 5.0, 5.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Froe ), 1044046, 1024325, 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Shovel ), 1044046, 1023898, 5.0, 5.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Hammer ), 1044046, 1024138, 5.0, 5.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Tongs ), 1044046, 1024028, 5.0, 5.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( SmithHammer ), 1044046, 1025091, 5.0, 5.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Inshave ), 1044046, 1024326, 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Pickaxe ), 1044046, 1023718, 5.0, 5.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Lockpick ), 1044046, 1025371, 5.0, 5.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Skillet ), 1044046, 1044567, 5.0, 5.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( FlourSifter ), 1044046, 1024158, 5.0, 5.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( FletcherTools ), 1044046, 1044166, 5.0, 5.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( MapmakersPen ), 1044046, 1044167, 5.0, 5.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( ScribesPen ), 1044046, 1044168, 5.0, 5.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Blowpipe ), 1044046, "Blowpipe", 5.0, 5.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( MalletAndChisel ), 1044046, "Mallet and Chisel", 5.0, 5.0, typeof( IronIngot ), 1044036, 6, 1044037 );
			#endregion

			#region Parts
			AddCraft( typeof( Gears ), 1044047, 1024179, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( ClockParts ), 1044047, 1024175, 25.0, 75.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( BarrelTap ), 1044047, 1024100, 35.0, 85.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( Springs ), 1044047, 1024189, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SextantParts ), 1044047, 1024185, 30.0, 80.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( BarrelHoops ), 1044047, 1024321, -15.0, 35.0, typeof( IronIngot ), 1044036, 5, 1044037 );
			AddCraft( typeof( Hinge ), 1044047, 1024181, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( BolaBall ), 1044047, 1023699, 45.0, 95.0, typeof( IronIngot ), 1044036, 10, 1044037 );
			#endregion

			#region Utensils
			AddCraft( typeof( ButcherKnife ), 1044048, 1025110, 25.0, 75.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SpoonLeft ), 1044048, 1044158, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( SpoonRight ), 1044048, 1044159, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Plate ), 1044048, 1022519, 0.0, 50.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( ForkLeft ), 1044048, 1044160, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( ForkRight ), 1044048, 1044161, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Cleaver ), 1044048, 1023778, 20.0, 70.0, typeof( IronIngot ), 1044036, 3, 1044037 );
			AddCraft( typeof( KnifeLeft ), 1044048, 1044162, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( KnifeRight ), 1044048, 1044163, 0.0, 50.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddCraft( typeof( Goblet ), 1044048, 1022458, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( PewterMug ), 1044048, 1024097, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( SkinningKnife ), 1044048, 1023781, 25.0, 75.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			#endregion

			#region Misc
			AddCraft( typeof( Candelabra ), 1044050, 1022599, 55.0, 105.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Scales ), 1044050, 1026225, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Globe ), 1044050, 1024167, 55.0, 105.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Spyglass ), 1044050, 1025365, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Lantern ), 1044050, 1022597, 30.0, 80.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddCraft( typeof( HeatingStand ), 1044050, 1026217, 60.0, 110.0, typeof( IronIngot ), 1044036, 4, 1044037 );
			AddCraft( typeof( Engines.Plants.PlantBowl ), 1044050, "plant bowl", 15.0, 30.0, typeof( IronIngot ), "Ingots", 2, 1044037 );
			AddCraft( typeof( Pitcher ), 1044050, "metal pitcher", 35.0, 55.0, typeof ( IronIngot ), "Ingots", 3 );
			AddCraft( typeof( Key ), 1044050, "blank key", 15.0, 25.0, typeof ( IronIngot ), "Ingots", 1 );
			AddCraft( typeof( KeyRing ), 1044050, "key ring", 10.0, 20.0, typeof ( IronIngot ), "Ingots", 2 );
			AddCraft( typeof( Caltrops ), 1044050, "caltrops", 50.0, 90.0, typeof ( IronIngot ), "Ingots", 2 );
            AddCraft(typeof(Manacles), 1044050, "Shackles", 60.0, 100.0, typeof(IronIngot), "Ingots", 20);
            AddCraft(typeof(ManacleKey), 1044050, "Shackle Key", 40.0, 80.0, typeof(IronIngot), "Ingots", 5);

			if( Core.SE )
			{
			/*
				index = AddCraft( typeof( ShojiLantern ), 1044050, 1029404, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( PaperLantern ), 1044050, 1029406, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( RoundPaperLantern ), 1044050, 1029418, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( WindChimes ), 1044050, 1030290, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );
				SetNeedSE( index, true );

				index = AddCraft( typeof( FancyWindChimes ), 1044050, 1030291, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );
				SetNeedSE( index, true );
			*/

			}
			#endregion

			#region Jewelry
            index = AddCraft(typeof(SilverBracelet), 1044049, "Silver Bracelet", 45.0, 95.0, typeof(SilverIngot), "Silver Ingot", 4);
            index = AddCraft(typeof(SilverRing), 1044049, "Silver Ring", 40.0, 90.0, typeof(SilverIngot), "Silver Ingot", 3);
			AddJewelrySet( GemType.StarSapphire, typeof( StarSapphire ) );
			AddJewelrySet( GemType.Emerald, typeof( Emerald ) );
			AddJewelrySet( GemType.Sapphire, typeof( Sapphire ) );
			AddJewelrySet( GemType.Ruby, typeof( Ruby ) );
			AddJewelrySet( GemType.Citrine, typeof( Citrine ) );
			AddJewelrySet( GemType.Amethyst, typeof( Amethyst ) );
			AddJewelrySet( GemType.Tourmaline, typeof( Tourmaline ) );
			AddJewelrySet( GemType.Amber, typeof( Amber ) );
			AddJewelrySet( GemType.Diamond, typeof( Diamond ) );


			#endregion

			#region Multi-Component Items
			index = AddCraft( typeof( AxleGears ), 1044051, 1024177, 0.0, 0.0, typeof( Axle ), 1044169, 1, 1044253 );
			AddRes( index, typeof( Gears ), 1044254, 1, 1044253 );

			index = AddCraft( typeof( ClockParts ), 1044051, 1024175, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Springs ), 1044171, 1, 1044253 );

			index = AddCraft( typeof( SextantParts ), 1044051, 1024185, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Hinge ), 1044172, 1, 1044253 );

			index = AddCraft( typeof( ClockRight ), 1044051, 1044257, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			index = AddCraft( typeof( ClockLeft ), 1044051, 1044256, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			AddCraft( typeof( Sextant ), 1044051, 1024183, 0.0, 0.0, typeof( SextantParts ), 1044175, 1, 1044253 );

			index = AddCraft( typeof( Bola ), 1044051, 1046441, 60.0, 110.0, typeof( BolaBall ), 1046440, 4, 1042613 );
			AddRes( index, typeof( Leather ), 1044462, 3, 1044463 );

			index = AddCraft( typeof( PotionKeg ), 1044051, 1044258, 75.0, 100.0, typeof( Keg ), 1044255, 1, 1044253 );
			AddRes( index, typeof( BarrelTap ), 1044252, 1, 1044253 );
			AddRes( index, typeof( BarrelLid ), 1044251, 1, 1044253 );
			AddRes( index, typeof( Bottle ), 1044250, 10, 1044253 );
			#endregion

            if ((m_Player.HasFeat(TeiravonMobile.Feats.RacialCrafting) || m_Player.HasFeat(TeiravonMobile.Feats.MasterCraftsman)) && (m_Player.IsTinker() || m_Player.IsMerchant()))
			{
				if (m_Player.IsHuman())
				{
					index = AddCraft( typeof( HumanPortableForge ), "Racials", "Portable Forge", 95.0, 120.0, typeof( DullCopperIngot ), "Dull Copper Ingots", 100 );
					AddRes( index, typeof( Granite ), "Granite", 5 );
					AddRes( index, typeof( Log ), "Logs", 20 );
					index = AddCraft( typeof( HumanPortableAnvil ), "Racials", "Portable Anvil", 95.0, 120.0, typeof( ShadowIronIngot ), "Shadow Iron Ingots", 100 );
					AddRes( index, typeof( Granite ), "Granite", 5 );
					AddRes( index, typeof( Log ), "Logs", 20 );
					index = AddCraft( typeof( HumanSaddlebags ), "Racials", "Saddlebags", 95.0, 120.0, typeof( BarbedLeather ), "Barbed Leather", 80 );
					AddRes( index, typeof( WhitePineLog ), "White Pine Logs", 10 );
				}
				else if (m_Player.IsOrc())
				{
					index = AddCraft( typeof( OrcWhetstone ), "Racials", "Whetstone", 95.0, 120.0, typeof( Granite ), "Granite", 1 );
					AddRes( index, typeof( OilCloth ), "Oil Cloth", 1 );
					index = AddCraft( typeof( OrcSplint ), "Racials", "Splint", 95.0, 120.0, typeof( Cloth ), "Cloth", 10 );
					AddRes( index, typeof( BlackOakLog ), "Black Oak Logs", 5 );
				}
				else if (m_Player.IsDrow())
				{
					index = AddCraft( typeof( Drowmagering ), "Racials", "Ring of Sorcere", 95.0, 120.0, typeof( IronIngot ), "Ingots", 2 );
					AddRes( index, typeof( Diamond ), "Diamond", 5 );
					index = AddCraft( typeof( Drowwarring ), "Racials", "Ring of Melee-Magthere", 95.0, 120.0, typeof( IronIngot ), "Ingots", 2 );
					AddRes( index, typeof( Amber ), "Amber", 5 );
					index = AddCraft( typeof( Drowpriestring ), "Racials", "Ring of Arach-Tinilith", 95.0, 120.0, typeof( IronIngot ), "Ingots", 2 );
					AddRes( index, typeof( Emerald ), "Emerald", 5 );
					index = AddCraft( typeof( Drowhouseinsig ), "Racials", "House Insignia", 95.0, 120.0, typeof( VeriteIngot ), "Verite Ingots", 5 );
					AddRes( index, typeof( Sapphire ), "Sapphire", 5 );
				}
				else if (m_Player.IsDwarf())
				{
					index = AddCraft( typeof( DwarvenPowderOfTemperament ), "Racials", "Strengthening Compound", 95.0, 120.0, typeof( ValoriteIngot ), "Valorite Ingots", 10 );
					AddRes( index, typeof( BaseGranite ), "Granite", 1 );
					index = AddCraft( typeof( DwarvenPickaxe ), "Racials", "Dwarven Pickaxe", 95.0, 120.0, typeof( AgapiteIngot ), "Agapite Ingots", 10 );
					AddRes( index, typeof( BaseGranite ), "Granite", 1 );
                    index = AddCraft(typeof(DwarvenForgeDeed), "Racials", "Dwarven Forge", 95.0, 120.0, typeof(BlackrockIngot), "Blackrock ingots", 10);
                    AddRes(index, typeof(BaseGranite), "Granite", 1);
				}
				else if (m_Player.IsElf())
				{
					index = AddCraft( typeof( ElvenHatchet ), "Racials", "Elven Hatchet", 95.0, 120.0, typeof( IronIngot ), "Ingots", 10 );
					AddRes( index, typeof( Vines ), "Vines", 1 );
					AddRes( index, typeof( YewLog ), "Yew Logs", 5 );
					index = AddCraft( typeof( ElvenAmulet ), "Racials", "Elven Amulet", 90.0, 110.0, typeof( GoldNecklace ), "Gold Necklace", 1 );
					AddRes( index, typeof( Diamond ), "Diamond", 1 );
					AddRes( index, typeof( RedScales ), "Red Scales", 1 );
				}
			}


			/*
			#region Traps
			// Faction Gas Trap
			index = AddCraft( typeof( FactionGasTrapDeed ), 1044052, 1044598, 65.0, 115.0, typeof( Silver ), 1044572, 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( BasePoisonPotion ), 1044571, 1, 1044253 );

			// Faction explosion Trap
			index = AddCraft( typeof( FactionExplosionTrapDeed ), 1044052, 1044599, 65.0, 115.0, typeof( Silver ), 1044572, 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( BaseExplosionPotion ), 1044569, 1, 1044253 );

			// Faction Saw Trap
			index = AddCraft( typeof( FactionSawTrapDeed ), 1044052, 1044600, 65.0, 115.0, typeof( Silver ), 1044572, 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( Gears ), 1044254, 1, 1044253 );

			// Faction Spike Trap
			index = AddCraft( typeof( FactionSpikeTrapDeed ), 1044052, 1044601, 65.0, 115.0, typeof( Silver ), 1044572, 1000, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			AddRes( index, typeof( Springs ), 1044171, 1, 1044253 );

			// Faction trap removal kit
			index = AddCraft( typeof( FactionTrapRemovalKit ), 1044052, 1046445, 90.0, 115.0, typeof( Silver ), 1044572, 500, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			#endregion
			*/

			// Set the overidable material
			SetSubRes( typeof( IronIngot ), 1044022 );

			// Add every material you want the player to be able to chose from
			// This will overide the overidable material
			AddSubRes( typeof( IronIngot ),			1044022, 00.0, 1044036, 1044267 );
			AddSubRes( typeof( DullCopperIngot ),	1044023, 45.0, 1044036, 1044268 );
			AddSubRes( typeof( ShadowIronIngot ),	1044024, 70.0, 1044036, 1044268 );
			AddSubRes( typeof( CopperIngot ),		1044025, 75.0, 1044036, 1044268 );
			AddSubRes( typeof( BronzeIngot ),		1044026, 80.0, 1044036, 1044268 );
			AddSubRes( typeof( GoldIngot ),			1044027, 85.0, 1044036, 1044268 );
			AddSubRes( typeof( AgapiteIngot ),		1044028, 90.0, 1044036, 1044268 );
			AddSubRes( typeof( VeriteIngot ),		1044029, 95.0, 1044036, 1044268 );
			AddSubRes( typeof( ValoriteIngot ),		1044030, 99.0, 1044036, 1044268 );
            AddSubRes(typeof(ElectrumIngot), "Electrum", 100.0, 1044268);

			MarkOption = true;
			Repair = true;
            CanFinish = true;
		}
	}
}
