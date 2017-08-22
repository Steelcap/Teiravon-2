/*
	Modified: SpellHelper.cs, Spell.cs, BaseWand.cs
*/
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Targeting;
using Server.Menus.Questions;
using Server.Spells;

namespace Teiravon.Magecraft
{
	#region Craft System
	public class DefMagecraft : CraftSystem
	{
		public override SkillName MainSkill
		{
			get{ return SkillName.Magery; }
		}

		public override string GumpTitleString { get { return "Magecraft Menu"; } }
		public override int GumpTitleNumber { get{ return -1; } }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefMagecraft();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item ) { return 0.0; }

		private DefMagecraft() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
		}

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			return true;
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
			int index = -1;
			TextDefinition lesser = new TextDefinition( "Lesser Items" );
			TextDefinition normal = new TextDefinition( "Normal Items" );
			TextDefinition greater = new TextDefinition( "Greater Items" );
			TextDefinition legendary = new TextDefinition( "Legendary Items" );
			TextDefinition exp = new TextDefinition( "Charged Experience Gem" );
			TextDefinition skill = new TextDefinition( "Charged Skill Gem" );
			
			// Lesser Items
			AddCraft( typeof( ExpGem ), lesser, new TextDefinition( "Experience Gem" ), 30.0, 30.0, typeof( ArcaneGem ), new TextDefinition( "Arcane Gem" ), 2 );
			AddCraft( typeof( SkillGem ), lesser, new TextDefinition( "Skill Gem" ), 30.0, 30.0, typeof( ArcaneGem ), new TextDefinition( "Arcane Gem" ), 2 );
				
			index = AddCraft( typeof( LesserManaBracer ), lesser, new TextDefinition( "Lesser Mana Bracer" ), 30.0, 40.0, typeof( ChargedExpGem ), exp, 1 );
			AddRes( index, typeof( SilverBracelet ), new TextDefinition( "Silver Bracelet" ), 1 );

			index = AddCraft( typeof( LesserRingOfElementalPower ), lesser, new TextDefinition( "Lesser Ring of Elemental Power" ), 35.0, 40.0, typeof ( ChargedExpGem ), exp, 1 );
			AddRes( index, typeof( SilverRing ), new TextDefinition( "Silver Ring" ), 1 );
			AddRes( index, typeof( Sapphire ), new TextDefinition( "Sapphire" ), 1 );
			
			// Normal Items
			index = AddCraft( typeof( ManaBracer ), normal, new TextDefinition( "Mana Bracer" ), 40.0, 75.0, typeof( ChargedExpGem ), exp, 2 );
			AddRes( index, typeof( LesserManaBracer ), new TextDefinition( "Lesser Mana Bracer" ), 1 );
			
			index = AddCraft( typeof( RingOfElementalPower ), normal, new TextDefinition( "Ring of Elemental Power" ), 40.0, 75.0, typeof( ChargedExpGem ), exp, 2 );
			AddRes( index, typeof( LesserRingOfElementalPower ), new TextDefinition( "Lesser Ring of Elemental Power" ), 1 );
			
			index = AddCraft( typeof( WardingNecklace ), normal, new TextDefinition( "Warding Necklace" ), 40.0, 75.0, typeof( ChargedExpGem ), exp, 2 );
			AddRes( index, typeof( SilverNecklace ), new TextDefinition( "Silver Necklace" ), 1 );
			AddRes( index, typeof( Sapphire ), new TextDefinition( "Sapphire" ), 1 );
			
			index = AddCraft( typeof( EnchantedWizardsHat ), normal, new TextDefinition( "Enchanted Wizards Hat" ), 40.0, 75.0, typeof ( ChargedExpGem ), exp, 2 );
			AddRes( index, typeof( WizardsHat ), new TextDefinition( "Wizards Hat" ), 1 );
			
			index = AddCraft( typeof( MasteringTheArcaneI ), normal, new TextDefinition( "Mastering the Arcane: Volume I" ), 40.0, 75.0, typeof( ChargedExpGem ), exp, 2 );
			AddRes( index, typeof( BlankScroll ), new TextDefinition( "Blank Scrolls" ), 10 );
			
			index = AddCraft( typeof( EmptyWand ), normal, new TextDefinition( "Empty Wand" ), 40.0, 75.0, typeof( ChargedExpGem ), exp, 2 );
			AddRes( index, typeof( Log ), new TextDefinition( "Oak Logs" ), 1 );
			AddRes( index, typeof( Ruby ), new TextDefinition( "Ruby" ), 3 );
			AddRes( index, typeof( GoldIngot ), new TextDefinition( "Gold Ingot" ), 1 );

			index = AddCraft( typeof( MageStaff ), normal, new TextDefinition( "Mage Staff" ), 40.0, 75.0, typeof ( QuarterStaff ), "Quarter Staff", 1 );
			AddRes( index, typeof( PowerCrystal ), new TextDefinition( "Power Crystal" ), 1 );
			AddRes( index, typeof( ArcaneGem ), new TextDefinition( "Arcane Gem" ), 3 );
			
			// Greater Items
			index = AddCraft( typeof( GreaterManaBracer ), greater, new TextDefinition( "Greater Mana Bracer" ), 75.0, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( ManaBracer ), new TextDefinition( "Mana Bracer" ), 1 );
			
			index = AddCraft( typeof( GreaterRingOfElementalPower ), greater, new TextDefinition( "Greater Ring of Elemental Power" ), 75.0, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( RingOfElementalPower ), new TextDefinition( "Ring of Elemental Power" ), 1 );
			
			index = AddCraft( typeof( MasteringTheArcaneII ), greater, new TextDefinition( "Mastering the Arcane: Volume II" ), 75.0, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( MasteringTheArcaneI ), new TextDefinition( "Mastering the Arcane: Volume I" ), 1 );
			
			index = AddCraft( typeof( MasteringTheArcaneIII ), greater, new TextDefinition( "Mastering the Arcane: Volume III" ), 90.0, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( MasteringTheArcaneII ), new TextDefinition( "Mastering the Arcane: Volume II" ), 1 );
		
			index = AddCraft( typeof( RobeOfReflection ), greater, new TextDefinition( "Robe of Reflection" ), 95.0, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( Robe ), new TextDefinition( "Robe (Exceptional)" ), 1 );
			AddRes( index, typeof( ArcaneGem ), new TextDefinition( "Arcane Gem" ), 5 );
			
			/*index = AddCraft( typeof( RuneTravelGem ), greater, new TextDefinition( "Rune Travel Gem" ), 95.0, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( Ruby ), new TextDefinition( "Ruby" ), 1 );
			AddRes( index, typeof( ArcaneGem ), new TextDefinition( "Arcane Gem" ), 1 );*/

			index = AddCraft( typeof( MageStaffGem ), greater, new TextDefinition( "Mage Staff Gem" ), 95.0, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( Diamond ), new TextDefinition( "Diamond" ), 10 );

			// Legendary Items
			index = AddCraft( typeof( MasteringTheArcaneIV ), legendary, new TextDefinition( "Mastering the Arcane: Volume IV" ), 97.5, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( MasteringTheArcaneIII ), new TextDefinition( "Mastering the Arcane: Volume III" ), 1 );

			index = AddCraft( typeof( LegendaryManaBracer ), legendary, new TextDefinition( "Legendary Mana Bracer" ), 97.5, 120.0, typeof( ChargedSkillGem ), skill, 1 );
			AddRes( index, typeof( GreaterManaBracer ), new TextDefinition( "Greater Mana Bracer" ), 1 );
		}
	}
	#endregion
	
	#region Commands
	public class MagecraftCommands
	{
		public static void Initialize()
		{
			Commands.Register( "ImbueWand", AccessLevel.Player, new CommandEventHandler( ImbueWand_OnCommand ) );
		}
		
		[Usage( "ImbueWand" )]
		[Description( "Gives an Empty Wand its spell." )]
		private static void ImbueWand_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
			
			if ( !m_Player.IsMage() )
				return;
			
			if ( m_Player.Mana < m_Player.ManaMax )
			{
				m_Player.SendMessage( "You must be fully rested to do this!" );
				return;
			}
			
			m_Player.Target = new WandTarget();
			m_Player.SendMessage( "Target an empty wand..." );
		}
		
		#region Wand Target/Menu
		private class WandTarget : Target
		{
			public static ArrayList Spells1 = new ArrayList(8);
			public static ArrayList Spells2 = new ArrayList(8);
			public static ArrayList Spells3 = new ArrayList(8);
			public static ArrayList Spells4 = new ArrayList(8);
			public static ArrayList Spells5 = new ArrayList(8);
			public static ArrayList Spells6 = new ArrayList(8);
			public static ArrayList Spells7 = new ArrayList(8);
			public static ArrayList Spells8 = new ArrayList(8);
			
			public WandTarget() : base( 1, false, TargetFlags.None )
			{
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( from.Mana < from.ManaMax )
					from.SendMessage( "You must be fully rested to do this." );
				else if ( !( targeted is EmptyWand ) )
					from.SendMessage( "You must target an empty wand!" );
				else if ( ((EmptyWand)targeted).Spell != null )
					from.SendMessage( "You must target an empty wand!" );
				else {
					EmptyWand wand = (EmptyWand)targeted;
					Spell s = null;
                    
                    Spells1.Clear();
                    Spells2.Clear();
                    Spells3.Clear();
                    Spells4.Clear();
                    Spells5.Clear();
                    Spells6.Clear();
                    Spells7.Clear();
                    Spells8.Clear();

					for ( int i = 0; i < 64; i++ )
					{
						s = (Spell)Activator.CreateInstance( (Type)SpellRegistry.Types.GetValue( i ), new object[]{ from, null } );
						
						if ( s.Circle == SpellCircle.First )
							Spells1.Add( s.Name );
						else if ( s.Circle == SpellCircle.Second )
							Spells2.Add( s.Name );
						else if ( s.Circle == SpellCircle.Third )
							Spells3.Add( s.Name );
						else if ( s.Circle == SpellCircle.Fourth )
							Spells4.Add( s.Name );
						else if ( s.Circle == SpellCircle.Fifth )
							Spells5.Add( s.Name );
						else if ( s.Circle == SpellCircle.Sixth )
							Spells6.Add( s.Name );
						else if ( s.Circle == SpellCircle.Seventh )
							Spells7.Add( s.Name );
						else if ( s.Circle == SpellCircle.Eighth )
							Spells8.Add( s.Name );
					}
					
					Spells1.Add( "Next" );
					Spells2.Add( "Next" );
					Spells3.Add( "Next" );
					Spells4.Add( "Next" );
					Spells5.Add( "Next" );
					Spells6.Add( "Next" );
					Spells7.Add( "Next" );
					Spells8.Add( "Next" );
					
					
					WandMenu menu = new WandMenu( (EmptyWand)targeted, (string[])Spells1.ToArray( typeof( string ) ), SpellCircle.First );
					menu.SendTo( from.NetState );
				}
			}
			
			private class WandMenu : QuestionMenu
			{
				EmptyWand m_Wand;
				SpellCircle m_SpellCircle;
				
				public WandMenu( EmptyWand wand, string[] spells, SpellCircle circle ) : base( "Select a spell...", spells )
				{
					m_Wand = wand;
					m_SpellCircle = circle;
				}
				
				public override void OnResponse( Server.Network.NetState state, int index )
				{
					if ( this.Answers[ index ] == "Next" )
					{
						WandMenu menu = null;
						
						if ( m_SpellCircle == SpellCircle.First )
							menu = new WandMenu( m_Wand, (string[])Spells2.ToArray( typeof( string ) ), SpellCircle.Second );
						else if ( m_SpellCircle == SpellCircle.Second )
							menu = new WandMenu( m_Wand, (string[])Spells3.ToArray( typeof( string ) ), SpellCircle.Third );
						else if ( m_SpellCircle == SpellCircle.Third )
							menu = new WandMenu( m_Wand, (string[])Spells4.ToArray( typeof( string ) ), SpellCircle.Fourth );
						else if ( m_SpellCircle == SpellCircle.Fourth )
							menu = new WandMenu( m_Wand, (string[])Spells5.ToArray( typeof( string ) ), SpellCircle.Fifth );
						else if ( m_SpellCircle == SpellCircle.Fifth )
							menu = new WandMenu( m_Wand, (string[])Spells6.ToArray( typeof( string ) ), SpellCircle.Sixth );
						else if ( m_SpellCircle == SpellCircle.Sixth )
							menu = new WandMenu( m_Wand, (string[])Spells7.ToArray( typeof( string ) ), SpellCircle.Seventh );
						else if ( m_SpellCircle == SpellCircle.Seventh )
							menu = new WandMenu( m_Wand, (string[])Spells8.ToArray( typeof( string ) ), SpellCircle.Eighth );
						else if ( m_SpellCircle == SpellCircle.Eighth )
							menu = new WandMenu( m_Wand, (string[])Spells1.ToArray( typeof( string ) ), SpellCircle.First );
											
						state.Mobile.CloseAllGumps();
						menu.SendTo( state );

						return;
					}
					
					TeiravonMobile m_Player = (TeiravonMobile)state.Mobile;
					
					if ( m_Player.CanCast( this.Answers[ index ] ) )
					{
						m_Player.Mana = 0;
						m_Wand.Spell = this.Answers[ index ];
					
						m_Player.SendMessage( "You charge the wand!" );
					} else {
						m_Player.SendMessage( "You can't cast this spell!" );
					}
				}
			}
		}
		#endregion
	}
	#endregion
}