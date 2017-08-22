using System;
using System.Collections;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Multis;

namespace Server.Gumps
{
	public class DruidCraftGump : Gump
	{
		TeiravonMobile m_Player;

		public DruidCraftGump( TeiravonMobile from ): base( 0, 0 )
		{

			m_Player = (TeiravonMobile) from;
			m_Player.CloseGump( typeof( DruidCraftGump ) );

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);

			//corner-, sidestones, background & topic
			AddBackground(162, 164, 321, 231, 9200);
			AddImage(145, 144, 206);
			AddImageTiled(189, 144, 274, 44, 201);
			AddImage(462, 144, 207);
			AddImageTiled(145, 188, 44, 195, 202);
			AddImageTiled(462, 188, 44, 195, 203);
			AddImage(145, 383, 204);
			AddImageTiled(189, 383, 274, 44, 233);
			AddImage(462, 383, 205);
			AddLabel(265, 162, 65, @"Nature's Enchantment");

			AddPage(1);

			AddHtml( 332, 227, 136, 146, @"A true druid is capable of combining the floating leaves of a water lily with herbs, in order to create his own 'natural armor'. The lilypads generate some magical properties in the process, and the added herbs allows the druid to draw energy from the earth at a quicker rate. The item requires the following materials: 10 Knot Grass, 5 lilypads and 5 mandrakes.", true, true );
			AddLabel(344, 202, 65, @"Robe of Leaves");

			AddButton(234, 380, 4006, 4007, (int)Buttons.DruidRobe, GumpButtonType.Reply, 0);
			AddButton(408, 383, 9702, 9703, (int)Buttons.PageNext, GumpButtonType.Page, 2);

			AddImage(158, 144, 12);
			AddImage(158, 144, 60469, 2210);

			AddPage(2);

			AddHtml( 200, 227, 258, 146, @"One of the first items young druids learn to create is their personal bag of reagents. As druids tend to store whatever reagents they come by, they always carry this handy pouch with them. They are also able to enchant this pouch, making it extremely light in weight and highly capacious. The item requires the following materials: 10 lilypads and 5 dried vines. [NOTE: The item is newbified and will not accept anything but druid reagents, you will not be able to extract anything from it either.]", true, true );
			AddLabel(245, 202, 65, @"Druid's Bag of Reagents");
			AddButton(234, 380, 4006, 4007, (int)Buttons.DruidBag, GumpButtonType.Reply, 0);
			AddButton(408, 383, 9702, 9703, (int)Buttons.PageNext, GumpButtonType.Page, 3);
			AddButton(365, 383, 9706, 9707, (int)Buttons.PageBack, GumpButtonType.Page, 1);

			AddPage(3);

			AddHtml( 200, 227, 258, 146, @"A regular mortar and pestle is enchanted by the druid. A few herbs and vines are used to create the desired effect. A druid's mortar and pestle will allow both shapeshifters and foresters to mold herbs into healing and curing substances which can be used without fear of side effects. The druids enchant the mortar and pestle by allowing them to fill the ground herbs with earth magic. The item requires the following materials: 1 regular mortar and pestle, 5 dried herbs and 5 dried vines.", true, true );
			AddLabel(245, 202, 65, @"Druid's Mortar and Pestle");

			AddButton(234, 380, 4006, 4007, (int)Buttons.DruidMortar, GumpButtonType.Reply, 0);
			AddButton(408, 383, 9702, 9703, (int)Buttons.PageNext, GumpButtonType.Page, 4);
			AddButton(365, 383, 9706, 9707, (int)Buttons.PageBack, GumpButtonType.Page, 2);

			AddPage(4);

			AddHtml( 332, 227, 136, 146, @"A bundle of vines and herbs is magically bound together by the druid. The materials most often assume the form of a walking stick or a staff. The vines allow the druid to channel earth magic through this staff. The item requires the following materials: 8 Amaranth, 6 Rauwolfia and 5 dried vines.", true, true );
			AddLabel(344, 202, 65, @"Druidic Staff");

			AddButton(234, 380, 4006, 4007, (int)Buttons.DruidStaff, GumpButtonType.Reply, 0);
			AddButton(408, 383, 9702, 9703, (int)Buttons.PageNext, GumpButtonType.Page, 5);
			AddButton(365, 383, 9706, 9707, (int)Buttons.PageBack, GumpButtonType.Page, 3);

			AddImage(158, 144, 12);
			AddImage(158, 144, 50430);
			AddImage(158, 144, 50628, 766);

			AddPage(5);

			AddHtml( 332, 227, 136, 146, @"Druids consider the procuring of this item as a rite of passage. The necessary materials are a gift the druid recieves when he is truly one with the grove. The item requires the following materials: 20 dried vines, 15 dried herbs and 10 fallen branches.", true, true );
			AddLabel(344, 202, 65, @"Druidic Shield");

			AddButton(234, 380, 4006, 4007, (int)Buttons.DruidShield, GumpButtonType.Reply, 0);
			AddButton(408, 383, 9702, 9703, (int)Buttons.PageNext, GumpButtonType.Page, 6);
			AddButton(365, 383, 9706, 9707, (int)Buttons.PageBack, GumpButtonType.Page, 4);

			AddImage(158, 144, 12);
			AddImage(158, 144, 50430);
			AddImage(158, 144, 50578, 769);

			AddPage(6);

			AddHtml( 332, 227, 136, 146, @"A branch from an ancient oak is worked to perfection by the druid. Some herbs and other raw materials are added to ensure a powerful outcome. It is also imbued with the powers of the forest and serves as a conduit for channeling earth magic. The item requires the following materials: 25 fallen branches, 20 dried vines, 15 dried herbs and 10 Rauwolfia.", true, true );
			AddLabel(344, 202, 65, @"Shillelagh");

			AddButton(234, 380, 4006, 4007, (int)Buttons.DruidClub, GumpButtonType.Reply, 0);
			AddButton(365, 383, 9706, 9707, (int)Buttons.PageBack, GumpButtonType.Page, 5);

			AddImage(158, 144, 12);
			AddImage(158, 144, 50430);
			AddImage(158, 144, 50620, 146);

		}

		public enum Buttons
		{
			ExitMenu,
			PageNext,
			PageBack,
			DruidRobe,
			DruidStaff,
			DruidShield,
			DruidClub,
			DruidMortar,
			DruidBag
		}

		public bool ManaCost( TeiravonMobile player, int cost )
		{
			if (player.Mana < cost)
			{
				player.SendMessage( "You need at least {0} mana to do that.", cost );
				return true;
			}

			return false;
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{

			bool fallenbranches = false;
			bool driedvines = false;
			bool driedherbs = false;
			bool knotgrass = false;
			bool amaranth = false;
			bool rauwolfia = false;
			bool lilypad = false;
			bool mandrake = false;
			bool mortarpestle = false;

			int m_MortarPestle;
			int m_FallenBranches;
			int m_DriedVines;
			int m_DriedHerbs;
			int m_KnotGrass;
			int m_Amaranth;
			int m_Rauwolfia;
			int m_Lilypad;
			int m_Mandrake;


			Container pack = m_Player.Backpack;

			if (pack == null)
			{
				m_Player.SendMessage( "You don't have a backpack. ERROR CODE: ID: 107" );
				return;
			}

			switch ( info.ButtonID )
			{

				case (int)Buttons.DruidRobe:
								if ( ManaCost( m_Player, 20 ) )
									break;

								m_Lilypad = pack.GetAmount( typeof( Lilypad ) );

								if ( m_Lilypad >= 5 )
									lilypad = true;

								m_KnotGrass = pack.GetAmount( typeof( KnotGrass ) );

								if ( m_KnotGrass >= 10 )
									knotgrass = true;

								m_Mandrake = pack.GetAmount( typeof( Mandrake ) );

								if ( m_Mandrake >= 5 )
									mandrake = true;

								if ( lilypad && knotgrass && mandrake )
								{
									pack.ConsumeTotal( typeof( Lilypad ), 5 );
									pack.ConsumeTotal( typeof( KnotGrass ), 10 );
									pack.ConsumeTotal( typeof( Mandrake ), 5 );
									m_Player.AddToBackpack( new DruidRobe( m_Player.PlayerLevel ) );
									m_Player.SendMessage( "You place the newly crafted robe in your pack." );
									m_Player.Mana -= 20;
									break;
								}

								if ( !lilypad )
									m_Player.SendMessage( "You need more lilypads." );

								if ( !knotgrass )
									m_Player.SendMessage( "You need more knot grass." );

								if ( !mandrake )
									m_Player.SendMessage( "You need more mandrake." );
								break;


				case (int)Buttons.DruidBag:
								if ( m_Player.PlayerLevel < 2)
								{
									m_Player.SendMessage( "You need to be level 2 or higher to create that." );
									break;
								}

								if ( ManaCost( m_Player, 25 ) )
									break;

								m_Lilypad = pack.GetAmount( typeof( Lilypad ) );

								if ( m_Lilypad >= 10 )
									lilypad = true;

								m_DriedVines = pack.GetAmount( typeof( DriedVines ) );

								if ( m_DriedVines >= 5 )
									driedvines = true;

								if ( lilypad && driedvines )
								{
									int itemlimit;

									if ( m_Player.PlayerLevel < 5 )
										itemlimit = 50;

									else if ( m_Player.PlayerLevel < 10 )
										itemlimit = 100;

									else if ( m_Player.PlayerLevel < 15 )
										itemlimit = 200;

									else
										itemlimit = 300;

									pack.ConsumeTotal( typeof( Lilypad ), 10);
									pack.ConsumeTotal( typeof( DriedVines ), 5 );
									m_Player.AddToBackpack( new DruidBag( itemlimit ) );
									m_Player.SendMessage( "You place the newly crafted bag of reagents in your pack." );
									m_Player.Mana -= 25;
									break;
								}

								if ( !lilypad )
									m_Player.SendMessage( "You need more lilypads." );

								if ( !driedvines )
									m_Player.SendMessage( "You need more dried vines." );
								break;


				case (int)Buttons.DruidMortar:

								if ( m_Player.PlayerLevel < 3)
								{
									m_Player.SendMessage( "You need to be level 3 or higher to create that." );
									break;
								}

								if ( ManaCost( m_Player, 30 ) )
									break;

								m_MortarPestle = pack.GetAmount( typeof( MortarPestle ) );

								if ( m_MortarPestle >= 1 )
									mortarpestle = true;

								m_DriedHerbs = pack.GetAmount( typeof( DriedHerbs ) );

								if ( m_DriedHerbs >= 5 )
									driedherbs = true;

								m_DriedVines = pack.GetAmount( typeof( DriedVines ) );

								if ( m_DriedVines >= 5 )
									driedvines = true;

								if ( mortarpestle && driedherbs && driedvines )
								{
									int uses;

									if ( m_Player.PlayerLevel < 5 )
										uses = 25;
									else if ( m_Player.PlayerLevel < 10 )
										uses = 35;
									else if ( m_Player.PlayerLevel < 15 )
										uses = 45;
									else
										uses = 55;

									pack.ConsumeTotal( typeof( MortarPestle ), 1);
									pack.ConsumeTotal( typeof( DriedHerbs ), 5 );
									pack.ConsumeTotal( typeof( DriedVines ), 5 );
									m_Player.AddToBackpack( new ForesterMortar( uses ) );
									m_Player.SendMessage( "You place the newly crafted mortar and pestle in your pack." );
									m_Player.Mana -= 30;
									break;
								}

								if ( !mortarpestle )
									m_Player.SendMessage( "You need a regular mortar and pestle." );

								if ( !driedherbs )
									m_Player.SendMessage( "You need more dried herbs." );

								if ( !driedvines )
									m_Player.SendMessage( "You need more dried vines." );
								break;


				case (int)Buttons.DruidStaff:

								if ( m_Player.PlayerLevel < 5)
								{
									m_Player.SendMessage( "You need to be level 5 or higher to create that." );
									break;
								}

								if ( ManaCost( m_Player, 50 ) )
									break;

								m_Amaranth = pack.GetAmount( typeof( Amaranth ) );

								if ( m_Amaranth >= 8 )
									amaranth = true;

								m_Rauwolfia = pack.GetAmount( typeof( Rauwolfia ) );

								if ( m_Rauwolfia >= 6 )
									rauwolfia = true;

								m_DriedVines = pack.GetAmount( typeof( DriedVines ) );

								if ( m_DriedVines >= 5 )
									driedvines = true;

								if ( amaranth && rauwolfia && driedvines )
								{
									pack.ConsumeTotal( typeof( DriedVines ), 4);
									pack.ConsumeTotal( typeof( Amaranth ), 6 );
									pack.ConsumeTotal( typeof( Rauwolfia ), 5 );
									m_Player.AddToBackpack( new DruidStaff( m_Player.PlayerLevel ) );
									m_Player.SendMessage( "You place the newly crafted staff in your pack." );
									m_Player.Mana -= 50;
									break;
								}

								if ( !amaranth )
									m_Player.SendMessage( "You need more amaranth." );

								if ( !rauwolfia )
									m_Player.SendMessage( "You need more rauwolfia." );

								if ( !driedvines )
									m_Player.SendMessage( "You need more dried vines." );
								break;

				case (int)Buttons.DruidShield:
								if ( m_Player.PlayerLevel < 10)
								{
									m_Player.SendMessage( "You need to be level 10 or higher to create that." );
									break;
								}

								if ( ManaCost( m_Player, 70 ) )
									break;

								m_DriedVines = pack.GetAmount( typeof( DriedVines ) );

								if ( m_DriedVines >= 20 )
									driedvines = true;

								m_FallenBranches = pack.GetAmount( typeof( FallenBranches ) );

								if ( m_FallenBranches >= 10 )
									fallenbranches = true;

								m_DriedHerbs = pack.GetAmount( typeof( DriedHerbs ) );

								if ( m_DriedHerbs >= 15 )
									driedherbs = true;

								if ( driedvines && fallenbranches && driedherbs )
								{
									pack.ConsumeTotal( typeof( DriedVines ), 20 );
									pack.ConsumeTotal( typeof( FallenBranches ), 10 );
									pack.ConsumeTotal( typeof( DriedHerbs ), 15 );
									m_Player.AddToBackpack( new DruidShield( m_Player.PlayerLevel ) );
									m_Player.SendMessage( "You place the newly crafted shield in your pack." );
									m_Player.Mana -= 70;
									break;
								}

								if ( !driedvines )
									m_Player.SendMessage( "You need more dried vines." );

								if ( !fallenbranches )
									m_Player.SendMessage( "You need more fallen branches." );

								if ( !driedherbs )
									m_Player.SendMessage( "You need more dried herbs." );
								break;

				case (int)Buttons.DruidClub:

								if ( m_Player.PlayerLevel < 15)
								{
									m_Player.SendMessage( "You need to be level 15 or higher to create that." );
									break;
								}

								if ( ManaCost( m_Player, 90 ) )
									break;

								m_FallenBranches = pack.GetAmount( typeof( FallenBranches ) );

								if ( m_FallenBranches >= 25 )
									fallenbranches = true;

								m_DriedVines = pack.GetAmount( typeof( DriedVines ) );

								if ( m_DriedVines >= 20 )
									driedvines = true;

								m_DriedHerbs = pack.GetAmount( typeof( DriedHerbs ) );

								if ( m_DriedHerbs >= 15 )
									driedherbs = true;

								m_Rauwolfia = pack.GetAmount( typeof( Rauwolfia ) );

								if ( m_Rauwolfia >= 10)
									rauwolfia = true;


								if ( fallenbranches && driedvines && driedherbs && rauwolfia )
								{
									pack.ConsumeTotal( typeof( FallenBranches ), 25 );
									pack.ConsumeTotal( typeof( DriedVines ), 20 );
									pack.ConsumeTotal( typeof( DriedHerbs ), 15 );
									pack.ConsumeTotal( typeof( Rauwolfia ), 10 );
									m_Player.AddToBackpack( new Shillelagh( m_Player.PlayerLevel ) );
									m_Player.SendMessage( "You place the newly crafted shillelagh in your pack." );
									m_Player.Mana -= 90;
									break;
								}

								if ( !fallenbranches )
									m_Player.SendMessage( "You need more fallen branches." );

								if ( !driedvines )
									m_Player.SendMessage( "You need more dried vines." );

								if ( !driedherbs )
									m_Player.SendMessage( "You need more dried herbs." );

								if ( !rauwolfia )
									m_Player.SendMessage( "You need more rauwolfia." );
								break;
			}
		}
	}


	public class DruidMortarGump : Gump
	{
		TeiravonMobile m_Player;
		ForesterMortar m_Mortar;
		int m_LastItem;

		public DruidMortarGump( TeiravonMobile from, Item tool, int last ): base( 0, 0 )
		{

			m_Player = (TeiravonMobile) from;
			m_Player.CloseGump( typeof( DruidMortarGump ) );
			m_Mortar = (ForesterMortar)tool;
			m_LastItem = last;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(33, 57, 335, 169, 9200);
			AddImageTiled(41, 69, 317, 149, 3604);
			AddImageTiled(40, 89, 318, 5, 200);
			AddImageTiled(156, 65, 5, 155, 200);
			AddImageTiled(40, 185, 318, 5, 200);
			AddImageTiled(41, 69, 5, 121, 200);
			AddImageTiled(353, 91, 5, 97, 200);

			AddAlphaRegion(41, 68, 317, 144);
			AddImageTiled(161, 190, 197, 29, 200);
			AddImageTiled(156, 63, 202, 26, 200);
			AddImageTiled(40, 190, 121, 29, 200);
			AddImageTiled(38, 63, 121, 26, 200);

			AddLabel(67, 96, 2930, @"Categories");
			AddLabel(87, 123, 65, @"Heal");
			AddLabel(87, 151, 65, @"Cure");
			AddLabel(51, 69, 2930, @"Herbal Science");
			AddButton(65, 154, 5601, 5605, (int)Buttons.Cure, GumpButtonType.Page, 3);
			AddButton(65, 126, 5601, 5605, (int)Buttons.Heal, GumpButtonType.Page, 2);


			if (m_LastItem > 0)
			{
				AddLabel(79, 192, 2930, @"Make last");
				AddButton(56, 195, 5601, 5605, m_LastItem, GumpButtonType.Reply, 0);
			}

			AddButton(328, 65, 4017, 4019, (int)Buttons.Exit, GumpButtonType.Reply, 0);

			AddPage(2);

			AddLabel(306, 192, 2930, @"Create");
			AddHtml(163, 96, 188, 87, @"When the druid uses the enchanted mortar and pestle he is able to combine Rauwolfia and Knot Grass into a healing herbal substance. Shapeshifters often rely on the healing herbs for sustenance during battle. Required ingredients: 1 Rauwolfia and 1 Knot Grass.", true, true);
			AddButton(284, 194, 5601, 5605, (int)Buttons.CreateHeal, GumpButtonType.Reply, 0);

			AddPage(3);

			AddLabel(306, 192, 2930, @"Create");
			AddHtml(163, 96, 188, 87, @"Druids have discovered early on that, when ground in the mortar, Mandrake and Amaranth have shown great potential as an antidote to most of the common poisons. When eaten, the curing herbal mass quickly flushes out toxins from the druid's system. Required ingredients: 1 Mandrake and 1 Amaranth.", true, true);
			AddButton(284, 194, 5601, 5605, (int)Buttons.CreateCure, GumpButtonType.Reply, 0);

		}

		public enum Buttons
		{
			None,
			Exit,
			Cure,
			Heal,
			CreateHeal,
			CreateCure
		}

		public bool ManaCost( TeiravonMobile player, int cost )
		{
			if (player.Mana < cost)
			{
				player.SendMessage( "You need at least {0} mana to do that.", cost );
				return true;
			}

			return false;
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{

			Container pack = m_Player.Backpack;

			if (pack == null)
			{
				m_Player.SendMessage( "You don't have a backpack. ERROR CODE: ID: 107" );
				return;
			}

			bool rauwolfia = false;
			bool mandrake = false;
			bool amaranth = false;
			bool knotgrass = false;
			bool success = false;

			int m_Rauwolfia;
			int m_Mandrake;
			int m_Amaranth;
			int m_KnotGrass;

			switch( info.ButtonID )
			{

				default:
							break;

				case (int)Buttons.Exit:
							m_Player.CloseGump( typeof( DruidMortarGump ) );
							break;


				case (int)Buttons.CreateHeal:
								if ( ManaCost( m_Player, 10 ) )
									break;

								m_Rauwolfia = pack.GetAmount( typeof( Rauwolfia ) );

								if ( m_Rauwolfia >= 1 )
									rauwolfia = true;

								m_KnotGrass = pack.GetAmount( typeof( KnotGrass ) );

								if ( m_KnotGrass >= 1 )
									knotgrass = true;


								if ( rauwolfia && knotgrass )
								{
									pack.ConsumeTotal( typeof( Rauwolfia ), 1 );
									pack.ConsumeTotal( typeof( KnotGrass ), 1 );
									m_Player.Mana -= 10;;
									m_Mortar.Uses -= 1;

									double chance = ( m_Player.Skills[SkillName.Alchemy].Value / 100.0 ) + 0.1 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 1.0)
										chance = 0.95;

									int healamount = (int)(m_Player.PlayerLevel / 2) + Utility.Random( 10 ) + (int)( m_Player.Skills[SkillName.Alchemy].Value / 5.0 );

									success = m_Player.CheckSkill( SkillName.Alchemy, chance );

									if ( success )
									{
										m_Player.AddToBackpack( new HealHerb( healamount ) );
										m_Player.SendMessage( "You place the newly created herb in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the herb and lose the ingredients." );

									if (m_Mortar.Uses == 0)
									{
										m_Mortar.Delete();
										m_Player.SendMessage( "You have worn out your mortar!" );
									}
									else
										m_Player.SendGump( new DruidMortarGump( m_Player, m_Mortar, (int)Buttons.CreateHeal ) );

									break;
								}

								if ( !rauwolfia )
									m_Player.SendMessage( "You need more Rauwolfia." );

								if ( !knotgrass )
									m_Player.SendMessage( "You need more Knot Grass." );

								break;

				case (int)Buttons.CreateCure:
								if ( ManaCost( m_Player, 10 ) )
									break;

								m_Mandrake = pack.GetAmount( typeof( Mandrake ) );

								if ( m_Mandrake >= 1 )
									mandrake = true;

								m_Amaranth = pack.GetAmount( typeof( Amaranth ) );

								if ( m_Amaranth >= 1 )
									amaranth = true;


								if ( mandrake && amaranth )
								{
									pack.ConsumeTotal( typeof( Mandrake ), 1 );
									pack.ConsumeTotal( typeof( Amaranth ), 1 );
									m_Player.Mana -= 10;
									m_Mortar.Uses -= 1;

									double chance = ( m_Player.Skills[SkillName.Alchemy].Value / 100.0 ) + 0.2 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 1.0)
										chance = 0.95;

									int curelevel =  (int)(m_Player.PlayerLevel / 10) + (int)( m_Player.Skills[SkillName.Alchemy].Value / 30.0 );

									if (curelevel > 3)
										curelevel = 3;

									success = m_Player.CheckSkill( SkillName.Alchemy, chance );

									if ( success )
									{
										m_Player.AddToBackpack( new CureHerb( (CureHerb.HerbStrength)curelevel ) );
										m_Player.SendMessage( "You place the newly created herb in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the herb and lose the ingredients." );


									if (m_Mortar.Uses == 0)
									{
										m_Mortar.Delete();
										m_Player.SendMessage( "You have worn out your mortar!" );
									}
									else
										m_Player.SendGump( new DruidMortarGump( m_Player, m_Mortar, (int)Buttons.CreateCure ) );

									break;
								}

								if ( !mandrake )
									m_Player.SendMessage( "You need more Mandrake." );

								if ( !amaranth )
									m_Player.SendMessage( "You need more Amaranth." );

								break;
			}
		}
	}
}

namespace Server.Scripts.Commands
{
	public class DruidCraftCommands
	{
		#region Command Initializers
		public static void Initialize()
		{
			Server.Commands.Register( "Druidcraft", AccessLevel.Player, new CommandEventHandler( DruidCraft_OnCommand ) );
		}
		#endregion

		[Usage( "Druidcraft" )]
		[Description( "Allows foresters to create special items" )]
		private static void DruidCraft_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			if ( !m_Player.HasFeat( TeiravonMobile.Feats.NaturesEnchantment ) )
				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility );

			else if ( m_Player.IsForester() )
				m_Player.SendGump( new DruidCraftGump( m_Player ) );
			else
				m_Player.SendMessage( "Only foresters can do that. ");
		}
	}
}

namespace Server.Items
{
	public class DruidBag : BaseContainer
	{

		public override int DefaultGumpID{ get{ return 0x3C; } }
		public override int DefaultDropSound{ get{ return 0x48; } }
		public override bool DisplayLootType{ get{ return false; } }
		public override int MaxWeight{ get{ return 500; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 44, 65, 142, 94 ); }
		}


		[Constructable]
		public DruidBag() : this( 40 )
		{
		}

		public DruidBag( int itemlimit ) : base( 0xE79 )
		{
			Weight = 1.0;
			Name = "Druid Reagent Bag";
			LootType = LootType.Blessed;
			Hue = 2129;
			MaxItems = itemlimit;
		}

		public Type[] m_DruidRegs = new Type[]
		{
			typeof( Amaranth ),
			typeof( Brambles ),
			typeof( DriedHerbs ),
			typeof( DriedVines ),
			typeof( FallenBranches ),
			typeof( KnotGrass ),
			typeof( Lilypad ),
			typeof( Mandrake ),
			typeof( Rauwolfia )
		};

		public override void OnDoubleClick( Mobile from )
		{
			int amount;
			int total = 0;
			string name;
			Item m_Reg;

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				this.TotalItems = (int)( this.GetAmount( m_DruidRegs ) / 10 );
	
				for( int i = 0; i < m_DruidRegs.Length; i++ )
				{
					m_Reg = (Item)Activator.CreateInstance( m_DruidRegs[i] );
					amount = this.GetAmount( m_DruidRegs[i] );
					total += amount;
					name = m_Reg.Name;

					if (m_Reg != null )
						m_Reg.Delete();

					if (amount > 0 )
						from.SendMessage( "You see {0} {1} in the bag.", amount, name );
				}

				if (total == 0)
					from.SendMessage( "The bag is empty." );
			}
			else
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
		}


		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
			list.Add( 1060658, "{0}\t{1}", "Reagents", this.GetAmount( m_DruidRegs ) );
			list.Add( 1060659, "{0}\t{1}", "Capacity", MaxItems );
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( this.GetAmount( m_DruidRegs ) >= MaxItems )
			{
				from.SendMessage( "The bag is full." );
				return false;
			}

			for( int i = 0; i < m_DruidRegs.Length; i++ )
			{
				if ( item != null )
				{
					if ( item.GetType() == m_DruidRegs[i] )
					{
						item.Weight = 0.0;
						this.TotalItems = (int)( this.GetAmount( m_DruidRegs ) / 10 );
						return base.OnDragDrop( from, item );
					}
				}
			}

			from.SendMessage( "That bag is for druid reagents only." );
			return false;
		}

		public DruidBag( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class CureHerb : Item
	{

		private static CureLevelInfo[] m_CureNone = new CureLevelInfo[]
		{
				new CureLevelInfo( Poison.Lesser,  0.50 ),
				new CureLevelInfo( Poison.Regular, 0.25 )
		};

		private static CureLevelInfo[] m_CureWeak = new CureLevelInfo[]
		{
				new CureLevelInfo( Poison.Lesser,  0.75 ),
				new CureLevelInfo( Poison.Regular, 0.50 ),
				new CureLevelInfo( Poison.Greater, 0.25 )
		};

		private static CureLevelInfo[] m_CureRegular = new CureLevelInfo[]
		{
				new CureLevelInfo( Poison.Lesser,  1.00 ),
				new CureLevelInfo( Poison.Regular, 0.95 ),
				new CureLevelInfo( Poison.Greater, 0.75 ),
				new CureLevelInfo( Poison.Deadly,  0.50 ),
				new CureLevelInfo( Poison.Lethal,  0.25 )
		};

		private static CureLevelInfo[] m_CureStrong = new CureLevelInfo[]
		{
				new CureLevelInfo( Poison.Lesser,  1.00 ),
				new CureLevelInfo( Poison.Regular, 1.00 ),
				new CureLevelInfo( Poison.Greater, 1.00 ),
				new CureLevelInfo( Poison.Deadly,  0.95 ),
				new CureLevelInfo( Poison.Lethal,  0.75 )
		};


		private HerbStrength m_Strength;

		public enum HerbStrength
		{
			Insignificant,
			Weak,
			Regular,
			Strong
		}

		public CureLevelInfo[] LevelInfo
		{
			get
			{

				if ( m_Strength == HerbStrength.Insignificant )
					return m_CureNone;
				else if ( m_Strength == HerbStrength.Weak )
					return m_CureWeak;
				else if ( m_Strength == HerbStrength.Regular )
					return m_CureRegular;
				else if ( m_Strength == HerbStrength.Strong )
					return m_CureStrong;
				else
					return m_CureRegular;
			}
		}

		[Constructable]
		public CureHerb() : this( HerbStrength.Regular )
		{
		}

		public CureHerb( HerbStrength strength ) : base( 0x1E85 )
		{
			Name = "Curing Herb";
			Hue = 568;
			Stackable = false;
			m_Strength = strength;
		}


		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				if ( from is TeiravonMobile )
				{
					TeiravonMobile m_Player = (TeiravonMobile)from;

					if ( m_Player.IsShapeshifter() || m_Player.IsForester() )
						Drink( from );
					else
						m_Player.SendMessage( "You cannot use that." );
				}
			}
			else
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
		}

		public void DoCure( Mobile from )
		{
			bool cure = false;

			CureLevelInfo[] info = LevelInfo;

			for ( int i = 0; i < info.Length; ++i )
			{
				CureLevelInfo li = info[i];

				if ( li.Poison == from.Poison && BasePotion.Scale( from, li.Chance ) > Utility.RandomDouble() )
				{
					cure = true;
					break;
				}
			}

			if ( cure && from.CurePoison( from ) )
			{
				from.PlaySound( Utility.Random( 0x3A, 3 ) );

				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 34, 5, 1, true, false, 0 );

				from.SendLocalizedMessage( 500231 ); // You feel cured of poison!
				from.FixedEffect( 0x373A, 10, 15 );

			}
			else if ( !cure )
				from.SendMessage( "The herb was not strong enough to cure your ailment!" ); // That potion was not strong enough to cure your ailment!
		}

		public void Drink( Mobile from )
		{
			if ( from.Poisoned )
			{
				DoCure( from );
				from.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
				this.Delete();
			}
			else
				from.SendLocalizedMessage( 1042000 ); // You are not poisoned.
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060658, "{0}\t{1}", "Herb strength", m_Strength );
		}

		public CureHerb( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Strength );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Strength = (HerbStrength)reader.ReadInt();
		}
	}


	public class HealHerb : Item
	{
		private int m_Heal;
		private HerbStrength m_Strength;

		public enum HerbStrength
		{
			None,
			Weak,
			Regular,
			Strong
		}

		[Constructable]
		public HealHerb() : this( 10 )
		{
		}

		public HealHerb( int heal ) : base( 0x1E85 )
		{
			Name = "Healing Herb";
			m_Heal = heal;
			Hue = 250;
			Stackable = false;

			if ( heal < 10 )
				m_Strength = HerbStrength.Weak;
			else if ( heal < 20 )
				m_Strength = HerbStrength.Regular;
			else
				m_Strength = HerbStrength.Strong;
		}

		public override void OnDoubleClick( Mobile from )
		{

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				if ( from is TeiravonMobile )
				{
					TeiravonMobile m_Player = (TeiravonMobile)from;

					if ( m_Player.IsShapeshifter() || m_Player.IsForester() )
						Drink( from );
					else
						m_Player.SendMessage( "You cannot use that." );
				}
			}
			else
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
		}

		public void DoHeal( Mobile from )
		{
            int min = from.HitsMax / 8;
            AOS.Scale(min, 100 + (int)m_Strength * 40);
            int max = min * 2;

			from.Heal( Utility.RandomMinMax( min, max ) );

			this.Delete();
			Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( ReleaseHealLock ), from );
		}

		public void Drink( Mobile from )
		{
			if ( from.Hits < from.HitsMax )
			{
				if ( from.BeginAction( typeof( HealHerb ) ) )
				{
					from.PlaySound( Utility.Random( 0x3A, 3 ) );

					if ( from.Body.IsHuman && !from.Mounted )
						from.Animate( 34, 5, 1, true, false, 0 );

					DoHeal( from );

				}
				else
					from.PrivateOverheadMessage( Network.MessageType.Regular, 0x22, false, "You cannot consume another herb yet.", from.NetState );
			}
			else
				from.SendMessage( "You are already at full health." );
		}

		private static void ReleaseHealLock( object state )
		{
			((Mobile)state).EndAction( typeof( HealHerb ) );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060658, "{0}\t{1}", "Herb strength", m_Strength );
		}

		public HealHerb( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Heal );

			writer.Write( (int) m_Strength );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Heal = reader.ReadInt();

			m_Strength = (HerbStrength)reader.ReadInt();
		}
	}


	public class ForesterMortar : Item
	{

		private int m_Uses;
		public int Uses { get { return m_Uses; } set { m_Uses = value; InvalidateProperties(); } }

		[Constructable]
		public ForesterMortar() : this( 25 )
		{
		}

		public ForesterMortar( int uses ) : base( 0xE9B )
		{
			Name = "Druid's Mortar and Pestle";
			Weight = 1.0;
			Hue = 60;
			m_Uses = uses;
		}

		public override void OnDoubleClick( Mobile from )
		{

			if ( IsChildOf( from.Backpack ) || Parent == from )
			{
				if ( from is TeiravonMobile )
				{
					TeiravonMobile m_Player = (TeiravonMobile)from;

					if ( m_Player.IsForester() || m_Player.IsShapeshifter() )
					{
						m_Player.SendGump( new DruidMortarGump( m_Player, this, 0 ) );
					}
					else
						m_Player.SendMessage( "You cannot use that!" );
				}
			}
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}


		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060584, m_Uses.ToString() );

		}

		public ForesterMortar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Uses = reader.ReadInt();
		}
	}



	[FlipableAttribute( 0x13b4, 0x13b3 )]
	public class Shillelagh : BaseBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 48; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 50; } }

		private DeityProp m_Deity;
		private MaceQuality m_MaceQuality;

		public enum DeityProp
		{
			None,
			NatureDefender = 1,
		}

		public enum MaceQuality
		{
			None,
			Oak = 1,
			Holly = 2,
			Mistletoe = 3
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DeityProp Deity
		{
			get { return m_Deity; }
			set
			{
				m_Deity = value;

				Name = "Shillelagh";
				Hue = 146;

				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 45;
				WeaponAttributes.HitPhysicalArea = 4;
				WeaponAttributes.SelfRepair = 8;
				WeaponAttributes.HitLeechStam = 15;
				m_MaceQuality = MaceQuality.Mistletoe;

				Attributes.RegenHits = 0;

				if (m_Deity == DeityProp.NatureDefender)
				{
					Name = "Nature's Defender";
					Hue = 1712;

					Attributes.RegenHits = 5;
					Attributes.WeaponDamage = 60;
					WeaponAttributes.HitPhysicalArea = 6;

				}
			}
		}


		[Constructable]
		public Shillelagh() : this( 10 )
		{
		}

		[Constructable]
		public Shillelagh( int playerlevel ) : base( 0x13b4 )
		{
			Name = "Shillelagh";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			m_Deity = DeityProp.None;
			Weight = 5.0;
			Hue = 146;

			if (playerlevel < 13)
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 30;
				m_MaceQuality = MaceQuality.Oak;

			}
			else if (playerlevel < 16)
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 35;
				WeaponAttributes.HitPhysicalArea = 2;
				WeaponAttributes.SelfRepair = 5;
				m_MaceQuality = MaceQuality.Holly;

			}
			else if (playerlevel <= 20)
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 45;
				WeaponAttributes.HitPhysicalArea = 4;
				WeaponAttributes.SelfRepair = 8;
				WeaponAttributes.HitLeechStam = 15;
				m_MaceQuality = MaceQuality.Mistletoe;
			}
			else
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 35;
				WeaponAttributes.HitPhysicalArea = 2;
				WeaponAttributes.SelfRepair = 5;
				m_MaceQuality = MaceQuality.Holly;
			}

			if ( playerlevel >= 20 )
				Deity = DeityProp.NatureDefender;
		}


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 20;
			fire = 20;
			cold = 20;
			pois = 20;
			nrgy = 20;
		}


		public override bool CanEquip( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( !(m_Player.IsForester()) && !(m_Player.IsShapeshifter()) )
				{
					from.SendMessage( "Only druids can equip that.");
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}", "Quality", m_MaceQuality );

		}


		public Shillelagh( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int)m_Deity );

			writer.Write( (int)m_MaceQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Deity = (DeityProp)reader.ReadInt();

			m_MaceQuality = (MaceQuality)reader.ReadInt();
		}
	}


	[FlipableAttribute( 0x13f8, 0x13f9 )]
	public class DruidStaff : BaseStaff
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosMinDamage{ get{ return 16; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 50; } }

		private DeityProp m_Deity;
		private MaceQuality m_MaceQuality;

		public enum DeityProp
		{
			None,
			Valar = 1,
		}

		public enum MaceQuality
		{
			None,
			Oak = 1,
			Holly = 2,
			Mistletoe = 3
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DeityProp Deity
		{
			get { return m_Deity; }
			set
			{
				m_Deity = value;

				Name = "Druid Staff";
				Hue = 766;

				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 45;
				WeaponAttributes.HitEnergyArea = 4;
				WeaponAttributes.SelfRepair = 8;
				WeaponAttributes.HitLeechMana = 15;
				m_MaceQuality = MaceQuality.Mistletoe;

				Attributes.RegenHits = 0;
				Attributes.ReflectPhysical = 0;
				Attributes.DefendChance = 0;
				Attributes.SpellDamage = 0;
				Attributes.WeaponSpeed = 0;


				if (m_Deity == DeityProp.Valar)
				{
					Name = "Staff of the Valar";
					Hue = 1620;

					Attributes.RegenHits = 5;
					Attributes.WeaponDamage = 60;
					WeaponAttributes.HitEnergyArea = 6;

				}
			}
		}


		[Constructable]
		public DruidStaff() : this( 10 )
		{
		}

		[Constructable]
		public DruidStaff( int playerlevel ) : base( 0x13F8 )
		{
			Name = "Druid Staff";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			m_Deity = DeityProp.None;
			Weight = 5.0;
			Hue = 766;

			if (playerlevel < 13)
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 30;
				m_MaceQuality = MaceQuality.Oak;

			}
			else if (playerlevel < 16)
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 35;
				WeaponAttributes.HitEnergyArea = 2;
				WeaponAttributes.SelfRepair = 5;
				m_MaceQuality = MaceQuality.Holly;

			}
			else if (playerlevel <= 20)
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 45;
				WeaponAttributes.HitEnergyArea = 4;
				WeaponAttributes.SelfRepair = 8;
				WeaponAttributes.HitLeechStam = 15;
				m_MaceQuality = MaceQuality.Mistletoe;
			}
			else
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 35;
				WeaponAttributes.HitEnergyArea = 2;
				WeaponAttributes.SelfRepair = 5;
				m_MaceQuality = MaceQuality.Holly;
			}

			if (playerlevel >= 15)
				Deity = DeityProp.Valar;
		}


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 20;
			fire = 20;
			cold = 20;
			pois = 20;
			nrgy = 20;
		}


		public override bool CanEquip( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( !(m_Player.IsForester()) && !(m_Player.IsShapeshifter()) )
				{
					from.SendMessage( "Only druids can equip that.");
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}", "Quality", m_MaceQuality );

		}


		public DruidStaff( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int)m_Deity );

			writer.Write( (int)m_MaceQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Deity = (DeityProp)reader.ReadInt();

			m_MaceQuality = (MaceQuality)reader.ReadInt();
		}
	}



	public class DruidShield : BaseShield
	{

		private DeityProp m_Deity;
		private ShieldQuality m_ShieldQuality;


		public enum DeityProp
		{
			None,
			NatureDefender = 1,
		}

		public enum ShieldQuality
		{
			None,
			Oak = 1,
			Holly = 2,
			Mistletoe = 3
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DeityProp Deity
		{
			get { return m_Deity; }
			set
			{
				m_Deity = value;

				Name = "Druid Shield";
				Hue = 769;

				Attributes.DefendChance = 10;
				Attributes.BonusHits = 10;
				Attributes.BonusStam = 10;
				m_ShieldQuality = ShieldQuality.Holly;

				Attributes.RegenHits = 0;
				Attributes.WeaponDamage = 0;

				if (m_Deity == DeityProp.NatureDefender)
				{
					Name = "Nature's Defender";
					Hue = 2375;

					Attributes.RegenHits = 3;
					Attributes.WeaponDamage = 30;

				}
			}
		}



		[Constructable]
		public DruidShield() : this( 10 )
		{
		}

		[Constructable]
		public DruidShield( int playerlevel ) : base( 0x1b7a)
		{
			Name = "Druid Shield";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			m_Deity = DeityProp.None;
            Attributes.SpellChanneling = 1;
			Hue = 769;

			if (playerlevel < 10)
			{
				Attributes.BonusHits = 5;
				Attributes.BonusStam = 5;
				m_ShieldQuality = ShieldQuality.Oak;
			}
			else if (playerlevel < 15)
			{
				Attributes.DefendChance = 5;
				Attributes.BonusHits = 8;
				Attributes.BonusStam = 8;
				m_ShieldQuality = ShieldQuality.Holly;
			}
			else if (playerlevel <= 20)
			{
				Attributes.DefendChance = 10;
				Attributes.BonusHits = 10;
				Attributes.BonusStam = 10;
				m_ShieldQuality = ShieldQuality.Mistletoe;
			}
			else
			{
				Attributes.DefendChance = 5;
				Attributes.BonusHits = 8;
				Attributes.BonusStam = 8;
				m_ShieldQuality = ShieldQuality.Holly;
			}

			if ( playerlevel >= 10 )
				Deity = DeityProp.NatureDefender;

		}

		public override bool CanEquip( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if (!m_Player.IsForester() && !m_Player.IsShapeshifter())
				{
					from.SendMessage( "Only Druids can equip that.");
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}", "Quality", m_ShieldQuality );

		}


		public DruidShield( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int)m_Deity );

			writer.Write( (int)m_ShieldQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Deity = (DeityProp)reader.ReadInt();

			m_ShieldQuality = (ShieldQuality)reader.ReadInt();
		}
	}


	public class DruidRobe : BaseArmor
	{

		private DeityProp m_Deity;
		private RobeQuality m_RobeQuality;

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Cloth; } }

		public enum DeityProp
		{
			None,
			Valar = 1,
		}

		public enum RobeQuality
		{
			None,
			Oak = 1,
			Holly = 2,
			Mistletoe = 3
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DeityProp Deity
		{
			get { return m_Deity; }
			set
			{
				m_Deity = value;

				Name = "Robe of Leaves";
				Hue = 2210;

				Attributes.RegenMana = 4;
				Attributes.BonusInt = 15;
				Attributes.BonusMana = 15;
				m_RobeQuality = RobeQuality.Mistletoe;

				Attributes.LowerManaCost = 0;
				Attributes.RegenHits = 0;

				if (m_Deity == DeityProp.Valar)
				{
					Name = "Valar Robe of Leaves";
					Hue = 2350;

					Attributes.LowerManaCost = 20;
					Attributes.RegenHits = 2;
				}
			}
		}



		[Constructable]
		public DruidRobe() : this( 10 )
		{
		}

		[Constructable]
		public DruidRobe( int playerlevel ) : base( 0x26AE )
		{
			Name = "Robe of Leaves";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			m_Deity = DeityProp.None;
			Hue = 2210;

			if (playerlevel < 10)
			{
				Attributes.BonusInt = 5;
				Attributes.BonusMana = 5;
				m_RobeQuality = RobeQuality.Oak;
			}
			else if (playerlevel < 15)
			{
				Attributes.RegenMana = 2;
				Attributes.BonusInt = 10;
				Attributes.BonusMana = 10;
				m_RobeQuality = RobeQuality.Holly;
			}
			else if (playerlevel <= 20)
			{
				Attributes.RegenMana = 4;
				Attributes.BonusInt = 15;
				Attributes.BonusMana = 15;
				m_RobeQuality = RobeQuality.Mistletoe;
			}
			else
			{
				Attributes.RegenMana = 2;
				Attributes.BonusInt = 10;
				Attributes.BonusMana = 10;
				m_RobeQuality = RobeQuality.Holly;
			}

			if (playerlevel >= 10)
				Deity = DeityProp.Valar;

		}

		public override bool CanEquip( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( !(m_Player.IsForester()) && !(m_Player.IsShapeshifter()) )
				{
					from.SendMessage( "Only druids can equip that.");
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}", "Quality", m_RobeQuality );

		}


		public DruidRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int)m_Deity );

			writer.Write( (int)m_RobeQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Deity = (DeityProp)reader.ReadInt();

			m_RobeQuality = (RobeQuality)reader.ReadInt();
		}
	}

	public class FallenBranches : Item
	{
        [Constructable]
		public FallenBranches( int amount ) : base( 0x1B9D )
		{
			Name = "Fallen Branches";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public FallenBranches() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new FallenBranches(amount), amount);
        }

		public FallenBranches( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}


	public class Lilypad : Item
	{

		[Constructable]
		public Lilypad( int amount ) : base( 0xD09 )
		{
			Name = "Lilypad";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public Lilypad() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Lilypad(amount), amount);
        }

		public Lilypad( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DriedVines : Item
	{
		[Constructable]
		public DriedVines( int amount ) : base( 0xC5F )
		{
			Name = "Dried Vines";
			Weight = 1.0;
			Hue = 50;
            Stackable = true;
            Amount = amount;

		}

        [Constructable]
        public DriedVines() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new DriedVines(amount), amount);
        }

		public DriedVines( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DriedHerbs : Item
	{
		[Constructable]
		public DriedHerbs( int amount ) : base( 0xC42 )
		{
			Name = "Dried Herbs";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public DriedHerbs() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new DriedHerbs(amount), amount);
        }

		public DriedHerbs( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class KnotGrass : Item
	{
		[Constructable]
		public KnotGrass( int amount ) : base( 0x1782 )
		{
			Name = "Knot Grass";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public KnotGrass() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new KnotGrass(amount), amount);
        }

		public KnotGrass( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class Amaranth : Item
	{

		[Constructable]
		public Amaranth( int amount ) : base( 0xCB4 )
		{
			Name = "Amaranth";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public Amaranth() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Amaranth(amount), amount);
        }

		public Amaranth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class Brambles : Item
	{
		[Constructable]
		public Brambles( int amount ) : base( 0xDD1 )
		{
			Name = "Brambles";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public Brambles() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Brambles(amount), amount);
        }

		public Brambles( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class Rauwolfia : Item
	{
		[Constructable]
		public Rauwolfia( int amount ) : base( 0xCB6 )
		{
			Name = "Rauwolfia";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public Rauwolfia() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Rauwolfia(amount), amount);
        }

		public Rauwolfia( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class Mandrake : Item
	{
		[Constructable]
		public Mandrake( int amount ) : base( 0x18DE )
		{
			Name = "Mandrake";
			Weight = 1.0;
			Stackable = true;
            Amount = amount;
		}

        [Constructable]
        public Mandrake() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Mandrake(amount), amount);
        }

		public Mandrake( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

	}

}
