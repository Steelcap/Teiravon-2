using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Teiravon;

namespace Teiravon.Perks
{
	public class PerkCommands
	{
		public static void Initialize()
		{
			Commands.Register( "Perks", AccessLevel.Player, new CommandEventHandler( PerksMenu_OnCommand ) );
			Commands.Register( "SkillDrop", AccessLevel.Player, new CommandEventHandler( SkillDrop_OnCommand ) );
		}
		
		[Usage( "Perks" )]
		[Description( "Activates Perks Menu" )]
		private static void PerksMenu_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
			m_Player.SendGump( new PerksGump( m_Player ) );			
		}

		[Usage( "SkillDrop" )]
		[Description( "Lowers Skill" )]
		private static void SkillDrop_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
			m_Player.SendGump( new SkillDropGump( m_Player ) );			
		}
	}
	
	public class PerksGump : Gump
	{
		TeiravonMobile tm_player;
		
		public enum Buttons
		{
			NullButton,
			SkillTrain,
			SkillResearch,
			Languages,
			Teaching,
			Professions,
			Riding,
			TrainOK,
			TrainCancel,
			ResearchOK,
			ResearchCancel,
			LanguageOK,
			LanguageCancel,
			TeachingOK,
			TeachingCancel,
			ProfOK,
			ProfCancel,
			RidingOK,
			RidingCancel,
			ResearchMining,
			ResearchLJ,
			ResearchAnat,
			ResearchItemID,
			ResearchArmsLore,
			ResearchLP,
			ResearchCarto,
            ResearchSwords,
            ResearchFence,
            ResearchMace,
			LangElven,
			LangDrow,
			LangDwarven,
			LangOrc,
			LangLupine,
			ProfBeggar,
			ProfToolMaker,
			ProfFarmer,
            Gathering,
            GatherOK,
            GatherCancel,
            Haggling,
            HaggleOK,
            HaggleCancel,
            ExpMine,
            ExpSkin,
            ExpWood,
		}

		public PerksGump(TeiravonMobile Player): base( 0, 0 )
		{
			int page = 0;
			tm_player = Player;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			
			this.AddPage(page);
			this.AddBackground(100, 50, 300, 350, 9390);
			string temptxt = "Teiravon Perks System (" + tm_player.PerkPoints.ToString() +")";
			this.AddLabel(165, 85, 846, @temptxt);
			
			this.AddPage(++page);
			this.AddLabel(180, 130, 0, @"Skill Training");
			this.AddLabel(180, 160, 0, @"Skill Research");
			this.AddLabel(180, 190, 0, @"Languages");
			this.AddLabel(180, 220, 0, @"Teaching");
			this.AddLabel(180, 250, 0, @"Professions");
			this.AddLabel(180, 280, 0, @"Riding");
            this.AddLabel(180, 310, 0, @"Gathering");
            this.AddLabel(180, 340, 0, @"Haggling");
			if (tm_player.SkillsTotal <= tm_player.SkillsCap)
				this.AddButton(150, 133, 1209, 1210, (int)Buttons.SkillTrain, GumpButtonType.Page, 2);
			this.AddButton(150, 163, 1209, 1210, (int)Buttons.SkillResearch, GumpButtonType.Page, 3);
			this.AddButton(150, 193, 1209, 1210, (int)Buttons.Languages, GumpButtonType.Page, 4);
			this.AddButton(150, 223, 1209, 1210, (int)Buttons.Teaching, GumpButtonType.Page, 5);
			this.AddButton(150, 253, 1209, 1210, (int)Buttons.Professions, GumpButtonType.Page, 6);
			this.AddButton(150, 283, 1209, 1210, (int)Buttons.Riding, GumpButtonType.Page, 7);
            this.AddButton(150, 313, 1209, 1210, (int)Buttons.Gathering, GumpButtonType.Page, 8);
            this.AddButton(150, 343, 1209, 1210, (int)Buttons.Haggling, GumpButtonType.Page, 13);
			
			this.AddPage(++page);
			this.AddHtml( 150, 136, 210, 197, @"You spend time training your skills in order to increase proficiency.  The more proficient you are in a skill the harder it is to improve it.  Only crafting skills are unavailable for training in this manner.", (bool)true, (bool)false);
			this.AddLabel(210, 110, 0, @"Skill Training");
			this.AddButton(290, 340, 247, 248, (int)Buttons.TrainOK, GumpButtonType.Reply, 0);
			this.AddButton(150, 340, 241, 242, (int)Buttons.TrainCancel, GumpButtonType.Page, 1);
			
			this.AddPage(++page);
			this.AddHtml( 150, 136, 210, 197, @"You spend much of your free time expanding your theoretical knowledge of these skills.  You find and read any books or scrolls on the subject and even seek out counsel from masters of these skills to allow yourself to expand your ability.", (bool)true, (bool)false);
			this.AddLabel(210, 110, 0, @"Skill Research");
			this.AddButton(290, 340, 247, 248, (int)Buttons.ResearchOK, GumpButtonType.Page, 9);
			this.AddButton(150, 340, 241, 242, (int)Buttons.ResearchCancel, GumpButtonType.Page, 1);
			
			this.AddPage(++page);
			this.AddHtml( 150, 136, 210, 197, @"Research and tutoring allow you to gain some proficiency in the languages of the various races - Elven, Dwarven, Orchish, Drow, and Lupine.", (bool)true, (bool)false);
			this.AddLabel(215, 110, 0, @"Languages");
			this.AddButton(290, 340, 247, 248, (int)Buttons.LanguageOK, GumpButtonType.Page, 10);
			this.AddButton(150, 340, 241, 242, (int)Buttons.LanguageCancel, GumpButtonType.Page, 1);
			
			this.AddPage(++page);
			this.AddHtml( 150, 136, 210, 197, @"Through patient practice and much effort, you learn to impart your knowlege onto others.  Given time and a patient pupil, you can teach skills to others.", (bool)true, (bool)false);
			temptxt = "Teaching (" + tm_player.TeachingSkill.ToString() +")";
			this.AddLabel(220, 110, 0, @temptxt);
			this.AddButton(290, 340, 247, 248, (int)Buttons.TeachingOK, GumpButtonType.Reply, 0);
			this.AddButton(150, 340, 241, 242, (int)Buttons.TeachingCancel, GumpButtonType.Page, 1);
			
			this.AddPage(++page);
			this.AddHtml( 150, 136, 210, 197, @"You decide to learn a way to earn a few coins in your spare time.  Beggar, Farmer, and Toolmaker professions are available.", (bool)true, (bool)false);
			this.AddLabel(215, 110, 0, @"Professions");
			this.AddButton(290, 340, 247, 248, (int)Buttons.ProfOK, GumpButtonType.Page, 11);
			this.AddButton(150, 340, 241, 242, (int)Buttons.ProfCancel, GumpButtonType.Page, 1);
			
			this.AddPage(++page);
			this.AddHtml( 150, 136, 210, 197, @"Diligent practice and many bruises allow you to learn to fully master riding the various mounts of the land.", (bool)true, (bool)false);
			temptxt = "Riding (" + tm_player.RidingSkill.ToString() +")";
			this.AddLabel(220, 100, 0, @temptxt);
			if (tm_player.RidingSkill < 4)
				this.AddButton(290, 340, 247, 248, (int)Buttons.RidingOK, GumpButtonType.Reply, 0);
			this.AddButton(150, 340, 241, 242, (int)Buttons.RidingCancel, GumpButtonType.Page, 1);

            this.AddPage(++page);
            this.AddHtml(150, 136, 210, 197, @"Hours of expert care has gone into the training required to exact every last bit of usable material possible.", (bool)true, (bool)false);
            this.AddLabel(215, 110, 0, @"Gathering");
            if (tm_player.ExpertMining + tm_player.ExpertSkinning + tm_player.ExpertWoodsman < 12)
                this.AddButton(290, 340, 247, 248, (int)Buttons.GatherOK, GumpButtonType.Page, 12);
            this.AddButton(150, 340, 241, 242, (int)Buttons.GatherCancel, GumpButtonType.Page, 1);
			
			this.AddPage(++page);
			int labely = 100;
			int buttony = 103;
			if (tm_player.Skills.Mining.Cap < 100.0)
			{
				this.AddLabel(180, labely += 30, 0, @"Mining");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ResearchMining, GumpButtonType.Reply, 0);
			}
			if (tm_player.Skills.Lumberjacking.Cap < 100.0)
			{
				this.AddLabel(180, labely += 30, 0, @"Lumberjacking");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ResearchLJ, GumpButtonType.Reply, 0);
			}
			if (tm_player.Skills.Anatomy.Cap < 100.0)
			{
				this.AddLabel(180, labely += 30, 0, @"Anatomy");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ResearchAnat, GumpButtonType.Reply, 0);
			}
			if (tm_player.Skills.ItemID.Cap < 100.0)
			{
				this.AddLabel(180, labely += 30, 0, @"Item ID");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ResearchItemID, GumpButtonType.Reply, 0);
			}
			if (tm_player.Skills.ArmsLore.Cap < 100.0)
			{
				this.AddLabel(180, labely += 30, 0, @"Arms Lore");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ResearchArmsLore, GumpButtonType.Reply, 0);
			}
			if (tm_player.Skills.Lockpicking.Cap < 100.0)
			{
				this.AddLabel(180, labely += 30, 0, @"Lockpicking");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ResearchLP, GumpButtonType.Reply, 0);
			}
			if (tm_player.Skills.Cartography.Cap < 100.0)
			{
				this.AddLabel(180, labely += 30, 0, @"Cartography");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ResearchCarto, GumpButtonType.Reply, 0);
			}

			this.AddPage(++page);
			labely = 100;
			buttony = 103;
			if (!tm_player.LanguageElven)
			{
				this.AddLabel(180, labely += 30, 0, @"Elven");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.LangElven, GumpButtonType.Reply, 0);
			}
			if (!tm_player.LanguageDrow)
			{
				this.AddLabel(180, labely += 30, 0, @"Drow");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.LangDrow, GumpButtonType.Reply, 0);
			}
			if (!tm_player.LanguageDwarven)
			{
				this.AddLabel(180, labely += 30, 0, @"Dwarven");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.LangDwarven, GumpButtonType.Reply, 0);
			}
			if (!tm_player.LanguageOrc)
			{
				this.AddLabel(180, labely += 30, 0, @"Orcish");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.LangOrc, GumpButtonType.Reply, 0);
			}
			if (!tm_player.LanguageLupine && tm_player.LanguageLupineSkill < 100)
			{
				this.AddLabel(180, labely += 30, 0, @"Lupine");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.LangLupine, GumpButtonType.Reply, 0);
			}
			
			this.AddPage(++page);
			labely = 100;
			buttony = 103;
			if (tm_player.Skills.Tinkering.Cap < 5.1)
			{
				this.AddLabel(180, labely += 30, 0, @"Tool Maker");
				this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ProfToolMaker, GumpButtonType.Reply, 0);
			}
			temptxt = "Farmer (" + tm_player.FarmingSkill.ToString() +")";
			this.AddLabel(180, labely += 30, 0, @temptxt);
			this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ProfFarmer, GumpButtonType.Reply, 0);

            this.AddPage(++page);
            labely = 100;
            buttony = 103;
            if (tm_player.ExpertMining < 4)
            {
                this.AddLabel(180, labely += 30, 0, @"Mining");
                this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ExpMine, GumpButtonType.Reply, 0);
            }
            if (tm_player.ExpertSkinning < 4)
            {
                this.AddLabel(180, labely += 30, 0, @"Skinning");
                this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ExpSkin, GumpButtonType.Reply, 0);
            } 
            if (tm_player.ExpertWoodsman < 4)
            {
                this.AddLabel(180, labely += 30, 0, @"Lumberjacking");
                this.AddButton(150, buttony += 30, 1209, 1210, (int)Buttons.ExpWood, GumpButtonType.Reply, 0);
            }

            this.AddPage(++page);
            this.AddHtml(150, 136, 210, 197, @"The time honored tradition of paying less than they want you to, and getting more than they want to give.", (bool)true, (bool)false);
            this.AddLabel(215, 110, 0, @"Haggling");
            if (tm_player.Skills.Begging.Base < 100.0)
                this.AddButton(290, 340, 247, 248, (int)Buttons.HaggleOK, GumpButtonType.Reply, 0);
            this.AddButton(150, 340, 241, 242, (int)Buttons.HaggleCancel, GumpButtonType.Page, 1);

		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			if (tm_player.PerkPoints < 1)
			{
				tm_player.SendMessage("You don't have any Perk Points left");
				return;
			}
			
			tm_player.CloseGump(typeof(SkillTrainGump));
			
			switch (info.ButtonID)
			{
				case (int)Buttons.TrainOK:
					if (tm_player.SkillsTotal <= tm_player.SkillsCap)
						tm_player.SendGump( new SkillTrainGump( tm_player, 0, "Choice", "Choice", "Choice", 0, 0, 0 ) );
					else
						tm_player.SendMessage("You are above your total skills cap.  Lower some skills and try again.");
					break;
				case (int)Buttons.TeachingOK:
					if (tm_player.TeachingSkill < 4)
					{
						tm_player.TeachingSkill += 1;
						tm_player.SendMessage("Your teaching skill has improved.");
						tm_player.PerkPoints -= 1;
					}
					break;
				case (int)Buttons.ProfFarmer:
					tm_player.FarmingSkill += 1;
					tm_player.SendMessage("You farming skill has improved.");
					tm_player.PerkPoints -= 1;
					break;
				case (int)Buttons.RidingOK:
					tm_player.RidingSkill += 1;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your riding skill has improved");
					break;
                case (int)Buttons.HaggleOK:
                    tm_player.Skills.Begging.Cap = Math.Max(tm_player.Skills.Begging.Cap, 100.0);
                    tm_player.Skills.Begging.Base += Math.Min(20.0, tm_player.Skills.Begging.Cap - tm_player.Skills.Begging.Base);
                    
                    tm_player.PerkPoints -= 1;
                    tm_player.SendMessage("Your haggling skill has improved");
                    break;
				case (int)Buttons.ResearchMining:
					if (tm_player.Skills.Mining.Cap + 20.0 > 100.0)
						tm_player.Skills.Mining.Cap = 100.0;
					else
						tm_player.Skills.Mining.Cap += 20.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your skill cap in Mining has raised by 20");
					break;
				case (int)Buttons.ResearchLJ:
					if (tm_player.Skills.Lumberjacking.Cap + 20.0 > 100.0)
						tm_player.Skills.Lumberjacking.Cap = 100.0;
					else
						tm_player.Skills.Lumberjacking.Cap += 20.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your skill cap in Lumberjacking has raised by 20");
					break;
				case (int)Buttons.ResearchAnat:
					if (tm_player.Skills.Anatomy.Cap + 20.0 > 100.0)
						tm_player.Skills.Anatomy.Cap = 100.0;
					else
						tm_player.Skills.Anatomy.Cap += 20.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your skill cap in Anatomy has raised by 20");
					break;
				case (int)Buttons.ResearchItemID:
					if (tm_player.Skills.ItemID.Cap + 20.0 > 100.0)
						tm_player.Skills.ItemID.Cap = 100.0;
					else
						tm_player.Skills.ItemID.Cap += 20.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your skill cap in Item ID has raised by 20");
					break;
				case (int)Buttons.ResearchArmsLore:
					if (tm_player.Skills.ArmsLore.Cap + 20.0 > 100.0)
						tm_player.Skills.ArmsLore.Cap = 100.0;
					else
						tm_player.Skills.ArmsLore.Cap += 20.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your skill cap in Arms Lore has raised by 20");
					break;
				case (int)Buttons.ResearchLP:
					if (tm_player.Skills.Lockpicking.Cap + 20.0 > 100.0)
						tm_player.Skills.Lockpicking.Cap = 100.0;
					else
						tm_player.Skills.Lockpicking.Cap += 20.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your skill cap in Lockpicking has raised by 20");
					break;
				case (int)Buttons.ResearchCarto:
					if (tm_player.Skills.Cartography.Cap + 20.0 > 100.0)
						tm_player.Skills.Cartography.Cap = 100.0;
					else
						tm_player.Skills.Cartography.Cap += 20.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your skill cap in Cartography has raised by 20");
					break;
				case (int)Buttons.LangElven:
					tm_player.LanguageElvenSkill += 16.7;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("You learn some Elven.");
                    if (tm_player.LanguageElvenSkill >= 100.0)
                    {
                        tm_player.LanguageElven = true;
                        tm_player.SendMessage("You've learned to speak Elven.");
                    }
					break;
				case (int)Buttons.LangDrow:
					tm_player.LanguageDrowSkill += 16.7;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("You learn some Drow.");
                    if (tm_player.LanguageDrowSkill >= 100.0)
                    {
                        tm_player.LanguageDrow = true;
                        tm_player.SendMessage("You've learned to speak Drow.");
                    }
					break;
				case (int)Buttons.LangDwarven:
					tm_player.LanguageDwarvenSkill += 25.00;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("You learn some Dwarven.");
                    if (tm_player.LanguageDwarvenSkill >= 100.0)
                    {
                        tm_player.LanguageDwarven = true;
                        tm_player.SendMessage("You've learned to speak Dwarven.");
                    }
					break;
				case (int)Buttons.LangOrc:
					tm_player.LanguageOrcSkill += 33.6;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("You learn some Orcish.");
                    if (tm_player.LanguageOrcSkill >= 100.0)
                    {
                        tm_player.LanguageOrc = true;
                        tm_player.SendMessage("You've learned to speak Orcish.");
                    }

					break;
				case (int)Buttons.LangLupine:
					tm_player.LanguageLupineSkill +=25.5000;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("You learn some Lupine.");
					break;
				case (int)Buttons.ProfBeggar:
					tm_player.Skills.Begging.Cap = 100.0;
					tm_player.Skills.Begging.Base = 10.0;
					tm_player.PerkPoints -= 1;
					tm_player.SendMessage("Your begging skill cap is now 100");
					break;
				case (int)Buttons.ProfToolMaker:
					tm_player.Skills.Tinkering.Cap = 5.0;
					tm_player.Skills.Tinkering.Base = 5.0;
					tm_player.PerkPoints -= 1;
					BankBox m_Bank = tm_player.BankBox;
					m_Bank.DropItem( new TinkerTools() );
					m_Bank.DropItem( new IronIngot(50) );
					tm_player.SendMessage("You can now craft tools");
					tm_player.SendMessage("Tinker equipment has been placed in your bank");
					break;
                case (int)Buttons.ExpMine:
                    tm_player.ExpertMining += 1;
                    tm_player.PerkPoints -= 1;
                    tm_player.SendMessage("You've increased your yeild from Mining.");
                    break;
                case (int)Buttons.ExpWood:
                    tm_player.ExpertWoodsman += 1;
                    tm_player.PerkPoints -= 1;
                    tm_player.SendMessage("You've increased your yeild from Lumberjacking.");
                    break;
                case (int)Buttons.ExpSkin:
                    tm_player.ExpertSkinning += 1;
                    tm_player.PerkPoints -= 1;
                    tm_player.SendMessage("You've increased your yeild from Skinning.");
                    break;
				default:
					break;
			}
		}
	}
	
	public class SkillTrainGump : Gump
	{
		TeiravonMobile tm;
		int skillcnt;
		string[] skillname = new string[5];
		int[] trnskill = new int[5];
		int tmpval;
		
		
		public enum Buttons
		{
			NullButton = 9000,
			SkillTrainOK,
			SkillTrainCancel,
			Cooking,
			Fishing,
			Healing,
			Herding,
			Lockpicking,
			Lumberjacking,
			Magery,
			Mining,
			Musicianship,
			Necromancy,
			RemoveTrap,
			ResistSpells,
			Snooping,
			Stealing,
			Stealth,
			Veterinary,
			Archery,
			Fencing,
			Macing,
			Parry,
			Swords,
			Tactics,
			Wrestling,
			AnimalTaming,
			Begging,
			Camping,
			Cartography,
			DetectHidden,
			Hiding,
			Poisoning,
			SpiritSpeak,
			Tracking,
			Anatomy,
			AnimalLore,
			ArmsLore,
			EvalInt,
			Forensics,
			ItemID,
			TasteID,
            		Discordance,
           		Peacemaking,
            		Provocation,
            Dodge
		}

		public SkillTrainGump(TeiravonMobile TMPlayer, int cnt, string sk1, string sk2, string sk3, int s1, int s2, int s3): base( 0, 0 )
		{
			tm = TMPlayer;
			skillcnt = cnt;
			skillname[1] = sk1;
			skillname[2] = sk2;
			skillname[3] = sk3;
			trnskill[1] = s1;
			trnskill[2] = s2;
			trnskill[3] = s3;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 67, 640, 481, 9400);

			this.AddLabel(275, 80, 45, @"Skill Training");
			this.AddLabel(229, 441, 45, @"Choose three skills to train");
			this.AddLabel(25, 100, 0, @"Miscellaneous");
			this.AddLabel(205, 100, 0, @"Combat");
			this.AddLabel(356, 100, 0, @"Action");
			this.AddLabel(490, 100, 0, @"Lore/Knowledge");
			
			this.AddHtml( 39, 476, 130, 20, @skillname[1], (bool)false, (bool)false);
			this.AddHtml( 254, 476, 130, 20, @skillname[2], (bool)false, (bool)false);
			this.AddHtml( 469, 476, 130, 20, @skillname[3], (bool)false, (bool)false);
			
			this.AddButton(394, 511, 247, 248, (int)Buttons.SkillTrainOK, GumpButtonType.Reply, 0);
			this.AddButton(179, 511, 241, 243, (int)Buttons.SkillTrainCancel, GumpButtonType.Reply, 0);
			
			//Misc
			int laby = 98;
			int buty = 100;
			
			if(tm.Skills.Cooking.Base < tm.Skills.Cooking.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Cooking");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Cooking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fishing.Base < tm.Skills.Fishing.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Fishing");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Fishing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Healing.Base < tm.Skills.Healing.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Healing");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Healing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Herding.Base < tm.Skills.Herding.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Herding");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Herding, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Lockpicking.Base < tm.Skills.Lockpicking.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Lockpicking");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Lockpicking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Lumberjacking.Base < tm.Skills.Lumberjacking.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Lumberjacking");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Lumberjacking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Magery.Base < tm.Skills.Magery.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Magery");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Magery, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Mining.Base < tm.Skills.Mining.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Mining");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Mining, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Musicianship.Base < tm.Skills.Musicianship.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Musicianship");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Musicianship, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Necromancy.Base < tm.Skills.Necromancy.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Necromancy");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Necromancy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.RemoveTrap.Base < tm.Skills.RemoveTrap.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Remove Trap");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.RemoveTrap, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.MagicResist.Base < tm.Skills.MagicResist.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Resist Spells");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.ResistSpells, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Snooping.Base < tm.Skills.Snooping.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Snooping");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Snooping, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Stealing.Base < tm.Skills.Stealing.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Stealing");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Stealing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Stealth.Base < tm.Skills.Stealth.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Stealth");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Stealth, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Veterinary.Base < tm.Skills.Veterinary.Cap)
			{
				this.AddLabel(40, laby += 25, 772, @"Veterinary");
				if (skillcnt < 3)
					this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Veterinary, GumpButtonType.Reply, 0);
			}
			
			//Combat
			laby = 98;
			buty = 100;
			
			if(tm.Skills.Archery.Base < tm.Skills.Archery.Cap)
			{
				this.AddLabel(200, laby += 25, 772, @"Archery");
				if (skillcnt < 3)
					this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Archery, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fencing.Base < tm.Skills.Fencing.Cap)
			{
				this.AddLabel(200, laby += 25, 772, @"Fencing");
				if (skillcnt < 3)
					this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Fencing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Macing.Base < tm.Skills.Macing.Cap)
			{
				this.AddLabel(200, laby += 25, 772, @"Macing");
				if (skillcnt < 3)
					this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Macing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Parry.Base < tm.Skills.Parry.Cap)
			{
				this.AddLabel(200, laby += 25, 772, @"Block");
				if (skillcnt < 3)
					this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Parry, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Swords.Base < tm.Skills.Swords.Cap)
			{
				this.AddLabel(200, laby += 25, 772, @"Swords");
				if (skillcnt < 3)
					this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Swords, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tactics.Base < tm.Skills.Tactics.Cap)
			{
				this.AddLabel(200, laby += 25, 772, @"Tactics");
				if (skillcnt < 3)
					this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Tactics, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Wrestling.Base < tm.Skills.Wrestling.Cap)
			{
				this.AddLabel(200, laby += 25, 772, @"Wrestling");
				if (skillcnt < 3)
					this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Wrestling, GumpButtonType.Reply, 0);
			}
            if (tm.Skills.Ninjitsu.Base < tm.Skills.Ninjitsu.Cap)
            {
                this.AddLabel(200, laby += 25, 772, @"Dodge");
                if (skillcnt < 3)
                    this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Dodge, GumpButtonType.Reply, 0);
            }

			//Actions
			laby = 98;
			buty = 100;
			
			if(tm.Skills.AnimalTaming.Base < tm.Skills.AnimalTaming.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Animal Taming");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.AnimalTaming, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Begging.Base < tm.Skills.Begging.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Begging");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Begging, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Camping.Base < tm.Skills.Camping.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Camping");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Camping, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Cartography.Base < tm.Skills.Cartography.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Cartography");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Cartography, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.DetectHidden.Base < tm.Skills.DetectHidden.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Detect Hidden");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.DetectHidden, GumpButtonType.Reply, 0);
			}/*
			if(tm.Skills.Hiding.Base < tm.Skills.Hiding.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Hiding");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Hiding, GumpButtonType.Reply, 0);
			}*/
			if(tm.Skills.Poisoning.Base < tm.Skills.Poisoning.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Poisoning");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Poisoning, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.SpiritSpeak.Base < tm.Skills.SpiritSpeak.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Spirit Speak");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.SpiritSpeak, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tracking.Base < tm.Skills.Tracking.Cap)
			{
				this.AddLabel(360, laby += 25, 772, @"Tracking");
				if (skillcnt < 3)
					this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Tracking, GumpButtonType.Reply, 0);
			}
            if (tm.Skills.Discordance.Base < tm.Skills.Discordance.Cap)
            {
                this.AddLabel(360, laby += 25, 772, @"Discordance");
                if (skillcnt < 3)
                    this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Discordance, GumpButtonType.Reply, 0);
            }
            if (tm.Skills.Peacemaking.Base < tm.Skills.Peacemaking.Cap)
            {
                this.AddLabel(360, laby += 25, 772, @"Peacemaking");
                if (skillcnt < 3)
                    this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Peacemaking, GumpButtonType.Reply, 0);
            }
            if (tm.Skills.Provocation.Base < tm.Skills.Provocation.Cap)
            {
                this.AddLabel(360, laby += 25, 772, @"Provocation");
                if (skillcnt < 3)
                    this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Provocation, GumpButtonType.Reply, 0);
            }

			//Lore
			laby = 98;
			buty = 100;

			if(tm.Skills.Anatomy.Base < tm.Skills.Anatomy.Cap)
			{
				this.AddLabel(520, laby += 25, 772, @"Anatomy");
				if (skillcnt < 3)
					this.AddButton(492, buty += 25, 216, 216, (int)Buttons.Anatomy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.AnimalLore.Base < tm.Skills.AnimalLore.Cap)
			{
				this.AddLabel(520, laby += 25, 772, @"Animal Lore");
				if (skillcnt < 3)
					this.AddButton(492, buty += 25, 216, 216, (int)Buttons.AnimalLore, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.ArmsLore.Base < tm.Skills.ArmsLore.Cap)
			{
				this.AddLabel(520, laby += 25, 772, @"ArmsLore");
				if (skillcnt < 3)
					this.AddButton(492, buty += 25, 216, 216, (int)Buttons.ArmsLore, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.EvalInt.Base < tm.Skills.EvalInt.Cap)
			{
				this.AddLabel(520, laby += 25, 772, @"Eval Int");
				if (skillcnt < 3)
					this.AddButton(492, buty += 25, 216, 216, (int)Buttons.EvalInt, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Forensics.Base < tm.Skills.Forensics.Cap)
			{
				this.AddLabel(520, laby += 25, 772, @"Forensic Eval");
				if (skillcnt < 3)
					this.AddButton(492, buty += 25, 216, 216, (int)Buttons.Forensics, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.ItemID.Base < tm.Skills.ItemID.Cap)
			{
				this.AddLabel(520, laby += 25, 772, @"Item ID");
				if (skillcnt < 3)
					this.AddButton(492, buty += 25, 216, 216, (int)Buttons.ItemID, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.TasteID.Base < tm.Skills.TasteID.Cap)
			{
				this.AddLabel(520, laby += 25, 772, @"Taste ID");
				if (skillcnt < 3)
					this.AddButton(492, buty += 25, 216, 216, (int)Buttons.TasteID, GumpButtonType.Reply, 0);
			}

		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
            try
            {

                switch (info.ButtonID)
                {
                    case (int)Buttons.Cooking:
                        skillcnt += 1;
                        skillname[skillcnt] = "Cooking";
                        trnskill[skillcnt] = 13;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Fishing:
                        skillcnt += 1;
                        skillname[skillcnt] = "Fishing";
                        trnskill[skillcnt] = 18;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Healing:
                        skillcnt += 1;
                        skillname[skillcnt] = "Healing";
                        trnskill[skillcnt] = 17;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Herding:
                        skillcnt += 1;
                        skillname[skillcnt] = "Herding";
                        trnskill[skillcnt] = 20;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Lockpicking:
                        skillcnt += 1;
                        skillname[skillcnt] = "Lockpicking";
                        trnskill[skillcnt] = 24;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Lumberjacking:
                        skillcnt += 1;
                        skillname[skillcnt] = "Lumberjacking";
                        trnskill[skillcnt] = 44;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Magery:
                        skillcnt += 1;
                        skillname[skillcnt] = "Magery";
                        trnskill[skillcnt] = 25;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Mining:
                        skillcnt += 1;
                        skillname[skillcnt] = "Mining";
                        trnskill[skillcnt] = 45;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Musicianship:
                        skillcnt += 1;
                        skillname[skillcnt] = "Musicicanship";
                        trnskill[skillcnt] = 29;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Necromancy:
                        skillcnt += 1;
                        skillname[skillcnt] = "Necromancy";
                        trnskill[skillcnt] = 49;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.RemoveTrap:
                        skillcnt += 1;
                        skillname[skillcnt] = "Remove Trap";
                        trnskill[skillcnt] = 48;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.ResistSpells:
                        skillcnt += 1;
                        skillname[skillcnt] = "Resist Spells";
                        trnskill[skillcnt] = 26;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Snooping:
                        skillcnt += 1;
                        skillname[skillcnt] = "Snooping";
                        trnskill[skillcnt] = 28;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Stealing:
                        skillcnt += 1;
                        skillname[skillcnt] = "Stealing";
                        trnskill[skillcnt] = 33;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Stealth:
                        skillcnt += 1;
                        skillname[skillcnt] = "Stealth";
                        trnskill[skillcnt] = 47;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Veterinary:
                        skillcnt += 1;
                        skillname[skillcnt] = "Veterinary";
                        trnskill[skillcnt] = 39;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Archery:
                        skillcnt += 1;
                        skillname[skillcnt] = "Archery";
                        trnskill[skillcnt] = 31;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Fencing:
                        skillcnt += 1;
                        skillname[skillcnt] = "Fencing";
                        trnskill[skillcnt] = 42;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Macing:
                        skillcnt += 1;
                        skillname[skillcnt] = "Macing";
                        trnskill[skillcnt] = 41;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Parry:
                        skillcnt += 1;
                        skillname[skillcnt] = "Block";
                        trnskill[skillcnt] = 5;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Swords:
                        skillcnt += 1;
                        skillname[skillcnt] = "Swords";
                        trnskill[skillcnt] = 40;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Tactics:
                        skillcnt += 1;
                        skillname[skillcnt] = "Tactics";
                        trnskill[skillcnt] = 27;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Wrestling:
                        skillcnt += 1;
                        skillname[skillcnt] = "Wrestling";
                        trnskill[skillcnt] = 43;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.AnimalTaming:
                        skillcnt += 1;
                        skillname[skillcnt] = "AnimalTaming";
                        trnskill[skillcnt] = 35;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Begging:
                        skillcnt += 1;
                        skillname[skillcnt] = "Begging";
                        trnskill[skillcnt] = 6;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Camping:
                        skillcnt += 1;
                        skillname[skillcnt] = "Camping";
                        trnskill[skillcnt] = 10;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Cartography:
                        skillcnt += 1;
                        skillname[skillcnt] = "Cartography";
                        trnskill[skillcnt] = 12;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.DetectHidden:
                        skillcnt += 1;
                        skillname[skillcnt] = "Detect Hidden";
                        trnskill[skillcnt] = 14;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Hiding:
                        skillcnt += 1;
                        skillname[skillcnt] = "Hiding";
                        trnskill[skillcnt] = 21;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Poisoning:
                        skillcnt += 1;
                        skillname[skillcnt] = "Poisoning";
                        trnskill[skillcnt] = 30;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.SpiritSpeak:
                        skillcnt += 1;
                        skillname[skillcnt] = "Spirit Speak";
                        trnskill[skillcnt] = 32;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Tracking:
                        skillcnt += 1;
                        skillname[skillcnt] = "Tracking";
                        trnskill[skillcnt] = 38;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Anatomy:
                        skillcnt += 1;
                        skillname[skillcnt] = "Anatomy";
                        trnskill[skillcnt] = 1;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.AnimalLore:
                        skillcnt += 1;
                        skillname[skillcnt] = "AnimalLore";
                        trnskill[skillcnt] = 2;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.ArmsLore:
                        skillcnt += 1;
                        skillname[skillcnt] = "Arms Lore";
                        trnskill[skillcnt] = 4;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.EvalInt:
                        skillcnt += 1;
                        skillname[skillcnt] = "Eval Int";
                        trnskill[skillcnt] = 16;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Forensics:
                        skillcnt += 1;
                        skillname[skillcnt] = "Forensic Eval";
                        trnskill[skillcnt] = 19;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.ItemID:
                        skillcnt += 1;
                        skillname[skillcnt] = "Item ID";
                        trnskill[skillcnt] = 3;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.TasteID:
                        skillcnt += 1;
                        skillname[skillcnt] = "Taste ID";
                        trnskill[skillcnt] = 36;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Discordance:
                        skillcnt += 1;
                        skillname[skillcnt] = "Discordance";
                        trnskill[skillcnt] = 15;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Provocation:
                        skillcnt += 1;
                        skillname[skillcnt] = "Provocation";
                        trnskill[skillcnt] = 22;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Peacemaking:
                        skillcnt += 1;
                        skillname[skillcnt] = "Peacemaking";
                        trnskill[skillcnt] = 9;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;
                    case (int)Buttons.Dodge:
                        skillcnt += 1;
                        skillname[skillcnt] = "Dodge";
                        trnskill[skillcnt] = 53;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], trnskill[1], trnskill[2], trnskill[3]));
                        break;

                    case (int)Buttons.SkillTrainCancel:
                        skillcnt = 0;
                        tm.SendGump(new SkillTrainGump(tm, skillcnt, "Choice", "Choice", "Choice", 0, 0, 0));
                        break;
                    case (int)Buttons.SkillTrainOK:
                        if (skillcnt == 3)
                        {
                            for (int i = 1; i <= 3; ++i)
                            {
                                tmpval = trnskill[i];
                                //tm.SendMessage("Skill {0}: {1}", i, tmpval);
                                if (tm.Skills[tmpval].Base < 60.1)
                                    tm.Skills[tmpval].Base += 5.0;
                                else if (tm.Skills[tmpval].Base > 60.0 && tm.Skills[tmpval].Base < 90.1)
                                    tm.Skills[tmpval].Base += 3.0;
                                else if (tm.Skills[tmpval].Base > 90.0 && tm.Skills[tmpval].Base < 120.1)
                                    tm.Skills[tmpval].Base += 1.0;

                                if (tm.Skills[tmpval].Base > tm.Skills[tmpval].Cap)
                                    tm.Skills[tmpval].Base = tm.Skills[tmpval].Cap;
                            }
                            tm.PerkPoints -= 1;
                        }
                        else
                        {
                            tm.SendMessage("You must choose 3 skills you have only picked {0}", skillcnt);
                            tm.SendGump(new SkillTrainGump(tm, skillcnt, skillname[1], skillname[2], skillname[3], 0, 0, 0));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch { }
		}

	}
	
	public class SkillDropGump : Gump
	{
		TeiravonMobile tm;
		
		public enum Buttons
		{
			NullButton,
			Cooking,
			Fishing,
			Healing,
			Herding,
			Lockpicking,
			Lumberjacking,
			Magery,
			Mining,
			Musicianship,
			Necromancy,
			RemoveTrap,
			ResistSpells,
			Snooping,
			Stealing,
			Stealth,
			Veterinary,
			Archery,
			Fencing,
			Macing,
			Parry,
			Swords,
			Tactics,
			Wrestling,
			AnimalTaming,
			Begging,
			Camping,
			Cartography,
			DetectHidden,
			Hiding,
			Poisoning,
			SpiritSpeak,
			Tracking,
			Anatomy,
			AnimalLore,
			ArmsLore,
			EvalInt,
			Forensics,
			ItemID,
			TasteID,
			Alchemy,
			Blacksmith,
			Fletching,
			Carpentry,
			Tailoring,
			Tinkering,
			Inscription,
		}

		public SkillDropGump(TeiravonMobile TMPlayer): base( 0, 0 )
		{
			tm = TMPlayer;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 67, 640, 481, 9400);

			this.AddLabel(275, 80, 45, @"Skill Removal");
			this.AddLabel(229, 441, 45, @"Each click will lower 1.0");
			this.AddLabel(25, 100, 0, @"Miscellaneous");
			this.AddLabel(205, 100, 0, @"Combat");
			this.AddLabel(356, 100, 0, @"Action");
			this.AddLabel(490, 100, 0, @"Lore/Knowledge");
			
			
			//Misc
			int laby = 98;
			int buty = 100;
			
			if(tm.Skills.Inscribe.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Inscription");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Inscription, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tinkering.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Tinkering");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Tinkering, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tailoring.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Tailoring");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Tailoring, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Carpentry.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Carpentry");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Carpentry, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fletching.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Bowcraft/Fletchin");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Fletching, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Blacksmith.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Blacksmith");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Blacksmith, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Alchemy.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Alchemy");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Alchemy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Cooking.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Cooking");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Cooking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fishing.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Fishing");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Fishing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Healing.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Healing");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Healing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Herding.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Herding");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Herding, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Lockpicking.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Lockpicking");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Lockpicking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Lumberjacking.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Lumberjacking");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Lumberjacking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Magery.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Magery");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Magery, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Mining.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Mining");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Mining, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Musicianship.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Musicianship");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Musicianship, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Necromancy.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Necromancy");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Necromancy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.RemoveTrap.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Remove Trap");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.RemoveTrap, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.MagicResist.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Resist Spells");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.ResistSpells, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Snooping.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Snooping");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Snooping, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Stealing.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Stealing");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Stealing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Stealth.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Stealth");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Stealth, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Veterinary.Base > 0)
			{
				this.AddLabel(40, laby += 25, 772, @"Veterinary");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Veterinary, GumpButtonType.Reply, 0);
			}
			
			//Combat
			laby = 98;
			buty = 100;
			
			if(tm.Skills.Archery.Base > 0)
			{
				this.AddLabel(200, laby += 25, 772, @"Archery");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Archery, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fencing.Base > 0)
			{
				this.AddLabel(200, laby += 25, 772, @"Fencing");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Fencing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Macing.Base > 0)
			{
				this.AddLabel(200, laby += 25, 772, @"Macing");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Macing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Parry.Base > 0)
			{
				this.AddLabel(200, laby += 25, 772, @"Block");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Parry, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Swords.Base > 0)
			{
				this.AddLabel(200, laby += 25, 772, @"Swords");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Swords, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tactics.Base > 0)
			{
				this.AddLabel(200, laby += 25, 772, @"Tactics");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Tactics, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Wrestling.Base > 0)
			{
				this.AddLabel(200, laby += 25, 772, @"Wrestling");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Wrestling, GumpButtonType.Reply, 0);
			}

			//Actions
			laby = 98;
			buty = 100;
			
			if(tm.Skills.AnimalTaming.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Animal Taming");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.AnimalTaming, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Begging.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Begging");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Begging, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Camping.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Camping");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Camping, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Cartography.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Cartography");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Cartography, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.DetectHidden.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Detect Hidden");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.DetectHidden, GumpButtonType.Reply, 0);
			}/*
			if(tm.Skills.Hiding.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Hiding");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Hiding, GumpButtonType.Reply, 0);
			}
              */
			if(tm.Skills.Poisoning.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Poisoning");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Poisoning, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.SpiritSpeak.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Spirit Speak");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.SpiritSpeak, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tracking.Base > 0)
			{
				this.AddLabel(360, laby += 25, 772, @"Tracking");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Tracking, GumpButtonType.Reply, 0);
			}

			//Lore
			laby = 98;
			buty = 100;

			if(tm.Skills.Anatomy.Base > 0)
			{
				this.AddLabel(520, laby += 25, 772, @"Anatomy");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.Anatomy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.AnimalLore.Base > 0)
			{
				this.AddLabel(520, laby += 25, 772, @"Animal Lore");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.AnimalLore, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.ArmsLore.Base > 0)
			{
				this.AddLabel(520, laby += 25, 772, @"ArmsLore");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.ArmsLore, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.EvalInt.Base > 0)
			{
				this.AddLabel(520, laby += 25, 772, @"Eval Int");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.EvalInt, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Forensics.Base > 0)
			{
				this.AddLabel(520, laby += 25, 772, @"Forensic Eval");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.Forensics, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.ItemID.Base > 0)
			{
				this.AddLabel(520, laby += 25, 772, @"Item ID");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.ItemID, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.TasteID.Base > 0)
			{
				this.AddLabel(520, laby += 25, 772, @"Taste ID");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.TasteID, GumpButtonType.Reply, 0);
			}

		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			switch (info.ButtonID)
			{
				case (int)Buttons.Cooking:
					tm.Skills.Cooking.Base -= 1.0;
					if (tm.Skills.Cooking.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Fishing:
					tm.Skills.Fishing.Base -= 1.0;
					if (tm.Skills.Fishing.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Healing:
					tm.Skills.Healing.Base -= 1.0;
					if (tm.Skills.Healing.Base > 0)
					tm.SendGump( new SkillDropGump(tm));
						break;
				case (int)Buttons.Herding:
					tm.Skills.Herding.Base -= 1.0;
					if (tm.Skills.Herding.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Lockpicking:
					tm.Skills.Lockpicking.Base -= 1.0;
					if (tm.Skills.Lockpicking.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Lumberjacking:
					tm.Skills.Lumberjacking.Base -= 1.0;
					if (tm.Skills.Lumberjacking.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Magery:
					tm.Skills.Magery.Base -= 1.0;
					if (tm.Skills.Magery.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Mining:
					tm.Skills.Mining.Base -= 1.0;
					if (tm.Skills.Mining.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Musicianship:
					tm.Skills.Musicianship.Base -= 1.0;
					if (tm.Skills.Musicianship.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Necromancy:
					tm.Skills.Necromancy.Base -= 1.0;
					if (tm.Skills.Necromancy.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.RemoveTrap:
					tm.Skills.RemoveTrap.Base -= 1.0;
					if (tm.Skills.RemoveTrap.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.ResistSpells:
					tm.Skills.MagicResist.Base -= 1.0;
					if (tm.Skills.MagicResist.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Snooping:
					tm.Skills.Snooping.Base -= 1.0;
					if (tm.Skills.Snooping.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Stealing:
					tm.Skills.Stealing.Base -= 1.0;
					if (tm.Skills.Stealing.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Stealth:
					tm.Skills.Stealth.Base -= 1.0;
					if (tm.Skills.Stealth.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Veterinary:
					tm.Skills.Veterinary.Base -= 1.0;
					if (tm.Skills.Veterinary.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Archery:
					tm.Skills.Archery.Base -= 1.0;
					if (tm.Skills.Archery.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Fencing:
					tm.Skills.Fencing.Base -= 1.0;
					if (tm.Skills.Fencing.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Macing:
					tm.Skills.Macing.Base -= 1.0;
					if (tm.Skills.Macing.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Parry:
					tm.Skills.Parry.Base -= 1.0;
					if (tm.Skills.Parry.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Swords:
					tm.Skills.Swords.Base -= 1.0;
					if (tm.Skills.Swords.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Tactics:
					tm.Skills.Tactics.Base -= 1.0;
					if (tm.Skills.Tactics.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Wrestling:
					tm.Skills.Wrestling.Base -= 1.0;
					if (tm.Skills.Wrestling.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.AnimalTaming:
					tm.Skills.AnimalTaming.Base -= 1.0;
					if (tm.Skills.AnimalTaming.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Begging:
					tm.Skills.Begging.Base -= 1.0;
					if (tm.Skills.Begging.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Camping:
					tm.Skills.Camping.Base -= 1.0;
					if (tm.Skills.Camping.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Cartography:
					tm.Skills.Cartography.Base -= 1.0;
					if (tm.Skills.Cartography.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.DetectHidden:
					tm.Skills.DetectHidden.Base -= 1.0;
					if (tm.Skills.DetectHidden.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
                    /*
				case (int)Buttons.Hiding:
					tm.Skills.Hiding.Base -= 1.0;
					if (tm.Skills.Hiding.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break; */
				case (int)Buttons.Poisoning:
					tm.Skills.Poisoning.Base -= 1.0;
					if (tm.Skills.Poisoning.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.SpiritSpeak:
					tm.Skills.SpiritSpeak.Base -= 1.0;
					if (tm.Skills.SpiritSpeak.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Tracking:
					tm.Skills.Tracking.Base -= 1.0;
					if (tm.Skills.Tracking.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Anatomy:
					tm.Skills.Anatomy.Base -= 1.0;
					if (tm.Skills.Anatomy.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.AnimalLore:
					tm.Skills.AnimalLore.Base -= 1.0;
					if (tm.Skills.AnimalLore.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.ArmsLore:
					tm.Skills.ArmsLore.Base -= 1.0;
					if (tm.Skills.ArmsLore.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.EvalInt:
					tm.Skills.EvalInt.Base -= 1.0;
					if (tm.Skills.EvalInt.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Forensics:
					tm.Skills.Forensics.Base -= 1.0;
					if (tm.Skills.Forensics.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.ItemID:
					tm.Skills.ItemID.Base -= 1.0;
					if (tm.Skills.ItemID.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.TasteID:
					tm.Skills.TasteID.Base -= 1.0;
					if (tm.Skills.TasteID.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Alchemy:
					tm.Skills.Alchemy.Base -= 1.0;
					if (tm.Skills.Alchemy.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Blacksmith:
					tm.Skills.Blacksmith.Base -= 1.0;
					if (tm.Skills.Blacksmith.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Fletching:
					tm.Skills.Fletching.Base -= 1.0;
					if (tm.Skills.Fletching.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Carpentry:
					tm.Skills.Carpentry.Base -= 1.0;
					if (tm.Skills.Carpentry.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Tailoring:
					tm.Skills.Tailoring.Base -= 1.0;
					if (tm.Skills.Tailoring.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Tinkering:
					tm.Skills.Tinkering.Base -= 1.0;
					if (tm.Skills.Tinkering.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				case (int)Buttons.Inscription:
					tm.Skills.Inscribe.Base -= 1.0;
					if (tm.Skills.Inscribe.Base > 0)
						tm.SendGump( new SkillDropGump(tm));
					break;
				
				
				default:
				break;
				
			}
		}

	}


}
