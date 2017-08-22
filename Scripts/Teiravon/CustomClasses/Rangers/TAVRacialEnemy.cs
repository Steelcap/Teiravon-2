using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
//using Server.Items;

namespace Server.Gumps
{
	public class RacialEnemyGump : Gump
	{
		TeiravonMobile m_Player;
		bool m_ExtraRacial;

		public RacialEnemyGump( TeiravonMobile from, bool extraracial ) : base( 0, 0 )
		{

			m_Player = from;
			m_ExtraRacial = extraracial;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddImage(102, 109, 39);
			AddImage(140, 155, 95);
			AddImageTiled(144, 164, 159, 3, 96);
			AddImage(302, 155, 97);
			AddImage(310, 352, 5178);
			AddImage(310, 109, 5172);
			AddImageTiled(265, 109, 58, 27, 5171);
			AddImageTiled(265, 352, 52, 43, 5177);

			if ( m_ExtraRacial )
				AddLabel(165, 138, 2101, @"Extra Racial Enemy");
			else
				AddLabel(185, 138, 2101, @"Racial Enemy");

			AddLabel(165, 173, 2101, @"Wild Animals");
			AddLabel(165, 192, 2101, @"Serpents");
			AddLabel(165, 213, 2101, @"Spiders");
			AddLabel(165, 233, 2101, @"Dragons");
			AddLabel(165, 252, 2101, @"Giants and Trolls");
			AddLabel(165, 271, 2101, @"Elementals");
			AddLabel(165, 290, 2101, @"Undead");
			AddLabel(165, 308, 2101, @"Daemons");
			AddLabel(165, 327, 2101, @"Orcs");

			AddButton(285, 176, 2087, 2086, (int)Buttons.WildAnimals, GumpButtonType.Page, 2);
			AddButton(285, 195, 2087, 2086, (int)Buttons.Snakes, GumpButtonType.Page, 3);
			AddButton(285, 215, 2087, 2086, (int)Buttons.Spiders, GumpButtonType.Page, 4);
			AddButton(285, 236, 2087, 2086, (int)Buttons.Dragons, GumpButtonType.Page, 5);
			AddButton(285, 255, 2087, 2086, (int)Buttons.GiantsAndTrolls, GumpButtonType.Page, 6);
			AddButton(285, 274, 2087, 2086, (int)Buttons.Elementals, GumpButtonType.Page, 7);
			AddButton(285, 293, 2087, 2086, (int)Buttons.Undead, GumpButtonType.Page, 8);
			AddButton(285, 311, 2087, 2086, (int)Buttons.Daemons, GumpButtonType.Page, 9);
			AddButton(285, 330, 2087, 2086, (int)Buttons.Orcs, GumpButtonType.Page, 10);

			AddPage (2);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">From the smallest forest birds to the mighty bears, you have learned how the wildlife behaves, making you a master of all forms of game hunting.</basefont>", false, true);
			AddLabel(400, 163, 2101, @"Wild Animals");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectWildAnimals, GumpButtonType.Reply, 0);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 3);

			AddPage (3);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">Serpents are patient stalkers, they prey rather than charge. However, you will not be fooled by their primitive yet effective ways. When ambushed, your strike falls swift and the reptiles are put at a disadvantage. Everything ranging from harmless snakes to the exotic silver serpents falls under this cathegory.</basefont>", false, true);
			AddLabel(405, 163, 2101, @"Serpents");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectSnakes, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 2);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 4);

			AddPage (4);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">Arachnids, spiders, children of Lloth. Every race calls them differently, but it doesn't change the fact that they are just this; Deadly, cold-blooded killers. Your study of the arachnid kin has taught you to aim your hits where they best pierce their thick exoskeleton, making you an expert spider slayer. Everything ranging from giant spiders, black widows to dreadful driders falls under this cathegory.</basefont>", false, true);
			AddLabel(410, 163, 2101, @"Spiders");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectSpiders, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 3);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 5);


			AddPage (5);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">These winged beasts are perhaps what is most fearsome on the face of Teiravon, a real portent of nature. You have learned everything about the draconic anatomy, magic, and most importantly, way of thinking. You can use their arrogance as your key to defeat them in combat. The young wrymlings, rather grown drakes and adult wryms all fall under this cathegory.</basefont>", false, true);
			AddLabel(410, 163, 2101, @"Dragons");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectDragons, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 4);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 6);

			AddPage (6);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">Giants are gargantuans that lumber around certain areas, seething with malice. You understand the importance of agility when it comes to fighting these hulking beasts. Trolls, ogres and other races and sub-races of giant kin fall under this cathegory.</basefont>", false, true);
			AddLabel(395, 163, 2101, @"Giants and Trolls");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectGiantsAndTrolls, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 5);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 7);

			AddPage (7);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">Beings summoned from different elemental planes share little similarity with each other, in all but name. You have learned and understood the structure of the different elementals and are capable of inflicting severe damage against them in combat. The primary primitive elementals, the acid and blood ones as well as the efreets and sand vortexes fall under this cathegory.</basefont>", false, true);
			AddLabel(405, 163, 2101, @"Elementals");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectElementals, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 6);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 8);

			AddPage (8);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">Typically, the undead are indifferent and emotionless, often servitors to wicked masters. However, you recognize their innate aspirations for power and learn to wield it as a sword against them. Skeletons in all their forms, spectral creatures such as wraiths and ghouls, zombies and other brainless undead drones, mummies, liches and the bone magi falls under this cathegory.</basefont>", false, true);
			AddLabel(415, 163, 2101, @"Undead");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectUndead, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 7);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 9);

			AddPage (9);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">Your study of daemon lords, minions and lower denizens make you an extremely capable daemon slayer. You see through their never-ending machinations, and through your study, learn to use this knowledge as weapon against them. Everything from lower denizens to powerful daemon lords falls under this cathegory.</basefont>", false, true);
			AddLabel(410, 165, 2101, @"Daemons");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectDaemons, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 8);
			AddButton(445, 325, 2509, 2509, (int)Buttons.Next, GumpButtonType. Page, 10);

			AddPage (10);

			AddImage(332, 135, 5170);
			AddImageTiled(370, 135, 144, 39, 5171);
			AddImage(514, 135, 5172);
			AddImageTiled(332, 173, 43, 150, 5173);
			AddImageTiled(370, 173, 144, 150, 5174);
			AddImageTiled(514, 173, 21, 150, 5175);
			AddImage(332, 323, 5176);
			AddImage(514, 323, 5178);
			AddImageTiled(370, 323, 144, 43, 5177);
			AddHtml( 378, 189, 131, 119, "<basefont color=\"#3A3B3A\">You gain insight about orcs, in all of their various forms and habits. You learn some of their weaknesses, their traits and tendancies in battle, and thusly are better equipped to fend off and kill any orc that might cross your path.</basefont>", false, true);
			AddLabel(415, 165, 2101, @"Orcs");
			AddButton(488, 315, 9720, 9723, (int)Buttons.SelectOrcs, GumpButtonType.Reply, 0);
			AddButton(383, 325, 2508, 2508, (int)Buttons.Previous, GumpButtonType.Page, 9);
		}

		public enum Buttons
		{
			ExitMenu,
			WildAnimals,
			Dragons,
			GiantsAndTrolls,
			Elementals,
			Undead,
			Daemons,
			Orcs,
			Spiders,
			Snakes,
			Next,
			Previous,
			SelectWildAnimals,
			SelectSnakes,
			SelectSpiders,
			SelectDragons,
			SelectElementals,
			SelectGiantsAndTrolls,
			SelectOrcs,
			SelectDaemons,
			SelectUndead
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			switch( info.ButtonID )
			{

				default:
					break;


				case (int)Buttons.SelectWildAnimals:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Animals, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemies are now wild animals and critters." );
									break;

				case (int)Buttons.SelectSnakes:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Serpents, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemies are now serpents." );
									break;

				case (int)Buttons.SelectSpiders:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Spiders, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemies are now spiders." );
									break;

				case (int)Buttons.SelectDragons:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Dragons, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemies are now dragons." );
									break;

				case (int)Buttons.SelectGiantsAndTrolls:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Giants, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemies are now giants and trolls." );
									break;

				case (int)Buttons.SelectElementals:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Elementals, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemy is now elementals." );
									break;

				case (int)Buttons.SelectUndead:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Undead, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemies are now the undead." );
									break;

				case (int)Buttons.SelectDaemons:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Daemons, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemy is now daemons and their minions." );
									break;

				case (int)Buttons.SelectOrcs:
									if ( AddEnemy( TeiravonMobile.RacialEnemies.Orcs, m_ExtraRacial ) )
										m_Player.SendMessage( "Your racial enemies are now the orcs." );
									break;

			}
		}

		public bool AddEnemy( TeiravonMobile.RacialEnemies enemy, bool extraracial )
		{
			if ( extraracial )
			{

				if ( m_Player.HasFeat( TeiravonMobile.Feats.ExtraRacialEnemy ) )
				{
					m_Player.SendMessage( "You already have the extra racial enemy feat!" );
					return false;
				}
			}
			else
			{

				if ( m_Player.HasFeat( TeiravonMobile.Feats.RacialEnemy ) )
				{
					m_Player.SendMessage( "You already have the racial enemy feat!" );
					return false;
				}
			}

			if ( enemy == m_Player.RacialEnemy )
			{
				m_Player.SendMessage( "You already have that race as racial enemy!" );
				return false;
			}
			else if ( enemy == m_Player.ExtraRacialEnemy )
			{
				m_Player.SendMessage( "You already have that race as extra racial enemy!" );
				return false;
			}

			if ( m_Player.RemainingFeats >= 1 )
			{
				if ( extraracial )
				{
					m_Player.ExtraRacialEnemy = enemy;
					m_Player.AddFeat( TeiravonMobile.Feats.ExtraRacialEnemy );
				}
				else
				{
					m_Player.RacialEnemy = enemy;
					m_Player.AddFeat( TeiravonMobile.Feats.RacialEnemy );
				}

				m_Player.RemainingFeats -= 1;
				return true;
			}

			m_Player.SendMessage( Teiravon.Messages.NoFeatSlots );
			return false;
		}
	}
}
