using System;
using Server;
using Server.Spells;
using Server.Items;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Mobiles
{
	public class LevelingFunctions
	{
        public static void CheckRace(TeiravonMobile m_Player, out int NewStr, out int NewDex, out int NewInt, out int NewHp, out int NewStam, out int NewMana)
        {

            switch (m_Player.PlayerRace)
            {
                case TeiravonMobile.Race.Undead:
                case TeiravonMobile.Race.Human:
                    NewStr = 1;
                    NewDex = 1;
                    NewInt = 1;
                    NewHp = 3;
                    NewStam = 1;
                    NewMana = 1;
                    break;

                case TeiravonMobile.Race.Drow:
                case TeiravonMobile.Race.Elf:
                    NewStr = 1;
                    NewDex = 2;
                    NewInt = 2;
                    NewHp = 3;
                    NewStam = 2;
                    NewMana = 2;
                    break;

                case TeiravonMobile.Race.Duergar:
                case TeiravonMobile.Race.Dwarf:
                case TeiravonMobile.Race.Orc:
                    NewStr = 4;
                    NewDex = 0;
                    NewInt = -1;
                    NewHp = 3;
                    NewStam = 2;
                    NewMana = -1;
                    break;
                case TeiravonMobile.Race.Goblin:
                case TeiravonMobile.Race.Gnome:
                    NewStr = -2;
                    NewDex = 4;
                    NewInt = 2;
                    NewHp = 0;
                    NewStam = 2;
                    NewMana = 4;
                    break;
                case TeiravonMobile.Race.Frostling:
                    NewStr = 2;
                    NewDex = 0;
                    NewInt = 2;
                    NewHp = 3;
                    NewStam = 2;
                    NewMana = 2;
                    break;
                default:
                    NewStr = 1;
                    NewDex = 1;
                    NewInt = 1;
                    NewHp = 3;
                    NewStam = 1;
                    NewMana = 1;
                    break;
            }
        }
		public static void CheckLevelUp( TeiravonMobile m_Player )
		{
            if (m_Player.IsFighter() || m_Player.IsKensai() || m_Player.IsCavalier() || m_Player.IsMonk() || m_Player.IsStrider())
                FighterLevel(m_Player);

            else if (m_Player.IsUndeadHunter() || m_Player.IsPaladin() || m_Player.IsDeathKnight())
                PaladinLevel(m_Player);

            else if (m_Player.IsCleric() || m_Player.IsDarkCleric() || m_Player.IsForester())
                ClericLevel(m_Player);

            else if (m_Player.IsShapeshifter())
                ShapeshifterLevel(m_Player);

            else if (m_Player.IsMageSlayer() || m_Player.IsRanger() || m_Player.IsArcher() || m_Player.IsRavager())
                RangerLevel(m_Player);

            else if (m_Player.IsNecromancer() || m_Player.IsAquamancer() || m_Player.IsAeromancer() || m_Player.IsGeomancer() || m_Player.IsPyromancer())
                MageLevel(m_Player);

            else if (m_Player.IsThief() || m_Player.IsAssassin() || m_Player.IsBard() || m_Player.IsScoundrel())
                RogueLevel(m_Player);

            else if (m_Player.IsBerserker() || m_Player.IsDragoon() || m_Player.IsSavage())
                BarbarianLevel(m_Player);

            else if (m_Player.IsTailor() || m_Player.IsBlacksmith() || m_Player.IsAlchemist() || m_Player.IsWoodworker() || m_Player.IsTinker() || m_Player.IsCook() || m_Player.IsMerchant())
                CrafterLevel(m_Player);

            else if (m_Player.IsDwarvenDefender())
                DefenderLevel(m_Player);

            else
                AdvancedLevel(m_Player);
		}

        public static void CheckEpicLevelUp(TeiravonMobile m_Player)
        {
            int Class20 = 8000000;

            if (m_Player.IsFighter() || m_Player.IsKensai() || m_Player.IsCavalier() || m_Player.IsMonk() || m_Player.IsStrider())
                Class20 = 7424000;
            else if (m_Player.IsUndeadHunter() || m_Player.IsDwarvenDefender() || m_Player.IsPaladin() || m_Player.IsDeathKnight() || m_Player.IsCleric() || m_Player.IsDarkCleric() || m_Player.IsForester())
                Class20 = 7720960;
            else if (m_Player.IsShapeshifter())
                Class20 = 7795200;
            else if (m_Player.IsMageSlayer() || m_Player.IsRanger() || m_Player.IsArcher())
                Class20 = 7572480;
            else if (m_Player.IsNecromancer() || m_Player.IsAquamancer() || m_Player.IsAeromancer() || m_Player.IsGeomancer() || m_Player.IsPyromancer())
                Class20 = 7943680;
            else if (m_Player.IsThief() || m_Player.IsAssassin() || m_Player.IsBard() || m_Player.IsBerserker() || m_Player.IsDragoon() || m_Player.IsSavage())
                Class20 = 7572480;
            else if (m_Player.IsCrafter())
                Class20 = 3108450;
            else
                Class20 = 8116400;
            
            if (m_Player.PlayerLevel > 19 && m_Player.PlayerLevel < 25 && m_Player.PlayerExp > (Class20 * Math.Pow(1.75, m_Player.PlayerLevel - 19)))
            {
                m_Player.PlayerLevel += 1;
                m_Player.MaxHits += 10;
                m_Player.PerkPoints += 1;
                m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!");

            if (m_Player.IsFighter() || m_Player.IsKensai() || m_Player.IsCavalier() || m_Player.IsMonk() || m_Player.IsStrider())
                DoFighterLevel(m_Player);

            else if (m_Player.IsUndeadHunter() || m_Player.IsPaladin() || m_Player.IsDeathKnight())
                DoPaladinLevel(m_Player);

            else if (m_Player.IsCleric() || m_Player.IsDarkCleric() || m_Player.IsForester())
                DoClericLevel(m_Player);

            else if (m_Player.IsShapeshifter())
                DoShapeshifterLevel(m_Player);

            else if (m_Player.IsMageSlayer() || m_Player.IsRanger() || m_Player.IsArcher())
                DoRangerLevel(m_Player);

            else if (m_Player.IsNecromancer() || m_Player.IsAquamancer() || m_Player.IsAeromancer() || m_Player.IsGeomancer() || m_Player.IsPyromancer())
                DoMageLevel(m_Player);

            else if (m_Player.IsThief() || m_Player.IsAssassin() || m_Player.IsBard())
                DoRogueLevel(m_Player);

            else if (m_Player.IsBerserker() || m_Player.IsDragoon() || m_Player.IsSavage() )
                DoBarbarianLevel(m_Player);

            else if (m_Player.IsTailor() || m_Player.IsBlacksmith() || m_Player.IsAlchemist() || m_Player.IsWoodworker() || m_Player.IsTinker() || m_Player.IsCook() || m_Player.IsMerchant())
                DoCrafterLevel(m_Player);

            else
               DoAdvancedLevel(m_Player);

            CheckEpicLevelUp(m_Player);
            }
        }
        
		public static void FighterLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1000 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2749 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 5449 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 9999 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 17199 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 35199 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 57199 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 83599 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 118699 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 194000 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 352000 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 562000 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 812000 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1244000 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1922000 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3024000 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4342000 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 5924000 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7424000 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoFighterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
            //if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7424000; m_Player.PerkPoints +=1; m_Player.SendMessage("You have gained an additional perk."); }
		}
		private static void DoFighterLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);

			if ( m_Player.IsKensai() || m_Player.IsMonk() )
			{
				NewDex+= Utility.RandomMinMax( 3, 5 );
				//m_Player.Skills.Parry.Base += 3.5;
			}

            NewStr += Utility.RandomMinMax(4, 6);
            NewDex += Utility.RandomMinMax(3, 5);
            NewInt += Utility.RandomMinMax(1, 3);

            NewHp += Utility.RandomMinMax(5, 8);
            NewMana += Utility.RandomMinMax(1, 2);
            NewStam += Utility.RandomMinMax(3, 7);

            if (m_Player.IsStrider())
                NewMana += 3;

            m_Player.RawStr += NewStr > 0 ? NewStr : 0;
            m_Player.RawDex += NewDex > 0 ? NewDex : 0;
            m_Player.RawInt += NewInt > 0 ? NewInt : 0;

            m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
            m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
            m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;
			
			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void ClericLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1250 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3350 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6500 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 11700 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 19800 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 39600 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 63600 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 92200 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 130000 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 203700 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 369600 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 590100 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 852600 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1306300 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2018100 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3175200 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4559100 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6220200 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7720960 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoClericLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
            //if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7720960; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
		}

		private static void DoClericLevel( TeiravonMobile m_Player )
		{

            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);


            NewStr += Utility.RandomMinMax(2, 6);
            NewDex += Utility.RandomMinMax(2, 6);
            NewInt += Utility.RandomMinMax(3, 7);

            NewHp += Utility.RandomMinMax(2, 5);
            NewMana += Utility.RandomMinMax(4, 7);
            NewStam += Utility.RandomMinMax(3, 7);

            m_Player.RawStr += NewStr > 0 ? NewStr : 0;
            m_Player.RawDex += NewDex > 0 ? NewDex : 0;
            m_Player.RawInt += NewInt > 0 ? NewInt : 0;

            m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
            m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
            m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void ShapeshifterLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1200 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3200 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6200 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 12200 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 19200 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 39200 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 63200 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 96200 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 133200 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 205640 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 373120 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 595720 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 860720 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1318640 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2037320 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3205440 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4602520 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6279440 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7795200 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoShapeshifterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
            //if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7795200; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
        }

		private static void DoShapeshifterLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);

            NewStr += Utility.RandomMinMax(2, 6);
            NewDex += Utility.RandomMinMax(3, 7);
            NewInt += Utility.RandomMinMax(1, 4);

            NewHp += Utility.RandomMinMax(3, 6);
            NewMana += Utility.RandomMinMax(1, 2);
            NewStam += Utility.RandomMinMax(3, 9);


            m_Player.RawStr += NewStr > 0 ? NewStr : 0;
            m_Player.RawDex += NewDex > 0 ? NewDex : 0;
            m_Player.RawInt += NewInt > 0 ? NewInt : 0;

            m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
            m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
            m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void MageLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1500 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3950 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7550 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 13400 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 22400 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 44000 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 70000 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 100800 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 141300 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 209520 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 380160 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 606960 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 876960 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1343520 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2075760 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3265920 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4689360 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6397920 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7943680 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoMageLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
           // if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7943680; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
		}

		private static void DoMageLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);

					NewStr += Utility.RandomMinMax( 2, 5 );
					NewDex+= Utility.RandomMinMax( 1, 4 );
					NewInt += Utility.RandomMinMax( 3, 8 );

					NewHp += Utility.RandomMinMax( 2, 5 );
					NewMana += Utility.RandomMinMax( 4, 9 );
					NewStam += Utility.RandomMinMax( 3, 7 );

                    m_Player.RawStr += NewStr > 0 ? NewStr : 0;
                    m_Player.RawDex += NewDex > 0 ? NewDex : 0;
                    m_Player.RawInt += NewInt > 0 ? NewInt : 0;

                    m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
                    m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
                    m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void RogueLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 900 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2510 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 5030 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 9320 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 16160 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 33440 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 55040 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 81440 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 117080 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 199820 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 362560 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 578860 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 836360 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1281320 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1979660 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3114720 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4472260 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6101720 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7572480 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoRogueLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
           // if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7572480; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
        }

		private static void DoRogueLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);
					NewStr += Utility.RandomMinMax( 2, 5 );
					NewDex+= Utility.RandomMinMax( 2, 7 );
					NewInt += Utility.RandomMinMax( 2, 6 );

					NewHp += Utility.RandomMinMax( 3, 7 );
					NewMana += Utility.RandomMinMax( 3, 7 );
					NewStam += Utility.RandomMinMax( 4, 9 );

                    m_Player.RawStr += NewStr > 0 ? NewStr : 0;
                    m_Player.RawDex += NewDex > 0 ? NewDex : 0;
                    m_Player.RawInt += NewInt > 0 ? NewInt : 0;

                    m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
                    m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
                    m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void CrafterLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 250 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1250 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3250 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7300 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 14300 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 27100 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 43700 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 72450 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 114450 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 168450 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 252950 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 378950 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 539950 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 771950 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1077950 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1455950 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1896950 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2438450 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3108450 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoCrafterLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
            //if (m_Player.PlayerExp >= 5000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 3108450; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
		}

		private static void DoCrafterLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);

					NewStr += Utility.RandomMinMax( 3, 7 );
					NewDex+= Utility.RandomMinMax( 2, 6 );
					NewInt += Utility.RandomMinMax( 1, 3 );

					NewHp += Utility.RandomMinMax( 3, 7 );
					NewMana += Utility.RandomMinMax( 3, 7 );
					NewStam += Utility.RandomMinMax( 3, 7 );

                    m_Player.RawStr += NewStr > 0 ? NewStr : 0;
                    m_Player.RawDex += NewDex > 0 ? NewDex : 0;
                    m_Player.RawInt += NewInt > 0 ? NewInt : 0;

                    m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
                    m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
                    m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void RangerLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1250 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3349 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6549 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 11699 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 19799 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 39599 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 63599 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 92199 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 129999 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 199820 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 362560 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 578860 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 836360 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1281320 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1979660 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3114720 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4472260 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6101720 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7572480 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoRangerLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
           // if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7572480; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
		}

		private static void DoRangerLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);

                    if (m_Player.IsRavager())
                    {
                        NewStr += Utility.RandomMinMax(4, 6);
                    }
                    else
                        NewStr += Utility.RandomMinMax(2, 6);

					NewDex+= Utility.RandomMinMax( 3, 7 );
					NewInt += Utility.RandomMinMax( 2, 6 );

					NewHp += Utility.RandomMinMax( 3, 7 );
					NewMana += Utility.RandomMinMax( 3, 7 );
					NewStam += Utility.RandomMinMax( 4, 9 );
                    if (m_Player.IsRavager())
                    {
                        NewStam++;
                        NewMana++;
                    }
                    m_Player.RawStr += NewStr > 0 ? NewStr : 0;
                    m_Player.RawDex += NewDex > 0 ? NewDex : 0;
                    m_Player.RawInt += NewInt > 0 ? NewInt : 0;

                    m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
                    m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
                    m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void BarbarianLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1000 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2749 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 5449 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 9999 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 17199 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 35199 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 57199 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 83599 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 118699 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 199820 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 362560 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 578860 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 836360 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1281320 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1979660 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3114720 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4472260 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6101720 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7572480 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoBarbarianLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
           // if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7572480; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
		}

		private static void DoBarbarianLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);
					NewStr += Utility.RandomMinMax( 5, 10 );
					NewDex+= Utility.RandomMinMax( 2, 5 );
					NewInt += Utility.RandomMinMax( 2, 4 );

					NewHp += Utility.RandomMinMax( 5, 10 );
					NewMana += Utility.RandomMinMax( 1, 3 );
					NewStam += Utility.RandomMinMax( 4, 8 );

                    if (m_Player.IsSavage())
                    {
                        NewStr += 3;
                        NewHp += 3;
                        NewStam += 2;
                    }
                    m_Player.RawStr += NewStr > 0 ? NewStr : 0;
                    m_Player.RawDex += NewDex > 0 ? NewDex : 0;
                    m_Player.RawInt += NewInt > 0 ? NewInt : 0;

                    m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
                    m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
                    m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void PaladinLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1250 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3350 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6500 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 11700 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 19800 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 39600 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 63600 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 92200 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 130000 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 203700 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 369600 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 590100 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 852600 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1306300 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2018100 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3175200 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4559100 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6220200 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 7720960 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoPaladinLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
           // if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7720960; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
		}

		private static void DoPaladinLevel( TeiravonMobile m_Player )
		{
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);
					NewStr += Utility.RandomMinMax( 3, 8 );
					NewDex+= Utility.RandomMinMax( 2, 5 );
					NewInt += Utility.RandomMinMax( 2, 5 );

					NewHp += Utility.RandomMinMax( 4, 9 );
					NewMana += Utility.RandomMinMax( 1, 2 );
					NewStam += Utility.RandomMinMax( 3, 7 );

            m_Player.RawStr += NewStr > 0 ? NewStr : 0;
            m_Player.RawDex += NewDex > 0 ? NewDex : 0;
            m_Player.RawInt += NewInt > 0 ? NewInt : 0;

            m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
            m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
            m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}

		public static void AdvancedLevel( TeiravonMobile m_Player )
		{
			if ( m_Player.PlayerExp >= 1250 && m_Player.PlayerLevel == 1 ) { m_Player.PlayerLevel = 2; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3350 && m_Player.PlayerLevel == 2 ) { m_Player.PlayerLevel = 3; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6500 && m_Player.PlayerLevel == 3 ) { m_Player.PlayerLevel = 4; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 11700 && m_Player.PlayerLevel == 4 ) { m_Player.PlayerLevel = 5; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 20700 && m_Player.PlayerLevel == 5 ) { m_Player.PlayerLevel = 6; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 40500 && m_Player.PlayerLevel == 6 ) { m_Player.PlayerLevel = 7; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 64500 && m_Player.PlayerLevel == 7 ) { m_Player.PlayerLevel = 8; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 93100 && m_Player.PlayerLevel == 8 ) { m_Player.PlayerLevel = 9; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 130900 && m_Player.PlayerLevel == 9 ) { m_Player.PlayerLevel = 10; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 213400 && m_Player.PlayerLevel == 10 ) { m_Player.PlayerLevel = 11; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 387200 && m_Player.PlayerLevel == 11 ) { m_Player.PlayerLevel = 12; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 618200 && m_Player.PlayerLevel == 12 ) { m_Player.PlayerLevel = 13; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 893200 && m_Player.PlayerLevel == 13 ) { m_Player.PlayerLevel = 14; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 1368400 && m_Player.PlayerLevel == 14 ) { m_Player.PlayerLevel = 15; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 2114200 && m_Player.PlayerLevel == 15 ) { m_Player.PlayerLevel = 16; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 3326400 && m_Player.PlayerLevel == 16 ) { m_Player.PlayerLevel = 17; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 4776600 && m_Player.PlayerLevel == 17 ) { m_Player.PlayerLevel = 18; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 6616400 && m_Player.PlayerLevel == 18 ) { m_Player.PlayerLevel = 19; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
			if ( m_Player.PlayerExp >= 8116400 && m_Player.PlayerLevel == 19 ) { m_Player.PlayerLevel = 20; DoAdvancedLevel( m_Player ); m_Player.SendMessage( "You are now level " + m_Player.PlayerLevel + "!" ); }
            //if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 8116400; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
		}

		private static void DoAdvancedLevel( TeiravonMobile m_Player )
		{

            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);

			NewStr += Utility.RandomMinMax( 2, 10 );
			NewDex+= Utility.RandomMinMax( 2, 10 );
			NewInt += Utility.RandomMinMax( 2, 10 );

            NewHp += Utility.RandomMinMax(2, 10);
            NewMana += Utility.RandomMinMax(2, 10);
            NewStam += Utility.RandomMinMax(2, 10);

            m_Player.RawStr += NewStr > 0 ? NewStr : 0;
            m_Player.RawDex += NewDex > 0 ? NewDex : 0;
            m_Player.RawInt += NewInt > 0 ? NewInt : 0;

            m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
            m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
            m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

			if ( m_Player.PlayerLevel % 5 == 0 )
				m_Player.RemainingFeats += 1;

			m_Player.PerkPoints += 1;

			m_Player.Hits = m_Player.MaxHits;
			m_Player.Stam = m_Player.MaxStam;
			m_Player.Mana = m_Player.MaxMana;
		}
       
        public static void DefenderLevel(TeiravonMobile m_Player)
        {
            if (m_Player.PlayerExp >= 1250 && m_Player.PlayerLevel == 1) { m_Player.PlayerLevel = 2; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 3350 && m_Player.PlayerLevel == 2) { m_Player.PlayerLevel = 3; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 6500 && m_Player.PlayerLevel == 3) { m_Player.PlayerLevel = 4; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 11700 && m_Player.PlayerLevel == 4) { m_Player.PlayerLevel = 5; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 19800 && m_Player.PlayerLevel == 5) { m_Player.PlayerLevel = 6; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 39600 && m_Player.PlayerLevel == 6) { m_Player.PlayerLevel = 7; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 63600 && m_Player.PlayerLevel == 7) { m_Player.PlayerLevel = 8; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 92200 && m_Player.PlayerLevel == 8) { m_Player.PlayerLevel = 9; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 130000 && m_Player.PlayerLevel == 9) { m_Player.PlayerLevel = 10; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 203700 && m_Player.PlayerLevel == 10) { m_Player.PlayerLevel = 11; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 369600 && m_Player.PlayerLevel == 11) { m_Player.PlayerLevel = 12; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 590100 && m_Player.PlayerLevel == 12) { m_Player.PlayerLevel = 13; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 852600 && m_Player.PlayerLevel == 13) { m_Player.PlayerLevel = 14; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 1306300 && m_Player.PlayerLevel == 14) { m_Player.PlayerLevel = 15; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 2018100 && m_Player.PlayerLevel == 15) { m_Player.PlayerLevel = 16; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 3175200 && m_Player.PlayerLevel == 16) { m_Player.PlayerLevel = 17; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 4559100 && m_Player.PlayerLevel == 17) { m_Player.PlayerLevel = 18; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 6220200 && m_Player.PlayerLevel == 18) { m_Player.PlayerLevel = 19; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            if (m_Player.PlayerExp >= 7720960 && m_Player.PlayerLevel == 19) { m_Player.PlayerLevel = 20; DoDefenderLevel(m_Player); m_Player.SendMessage("You are now level " + m_Player.PlayerLevel + "!"); }
            // if (m_Player.PlayerExp >= 9000000 && m_Player.PlayerLevel == 20) { m_Player.PlayerExp = 7720960; m_Player.PerkPoints += 1; m_Player.SendMessage("You have gained an additional perk."); }
        }
  
        private static void DoDefenderLevel(TeiravonMobile m_Player)
        {
            int NewStr, NewDex, NewInt, NewHp, NewStam, NewMana = 0;

            CheckRace(m_Player, out NewStr, out NewDex, out NewInt, out NewHp, out NewStam, out NewMana);
            NewStr += Utility.RandomMinMax(5, 10);
            NewDex += Utility.RandomMinMax(2, 5);
            NewInt += Utility.RandomMinMax(2, 4);

            NewHp += Utility.RandomMinMax(5, 10);
            NewMana += Utility.RandomMinMax(1, 3);
            NewStam += Utility.RandomMinMax(4, 8);

            m_Player.RawStr += NewStr > 0 ? NewStr : 0;
            m_Player.RawDex += NewDex > 0 ? NewDex : 0;
            m_Player.RawInt += NewInt > 0 ? NewInt : 0;

            m_Player.MaxHits += NewHp > 0 ? NewHp : 0;
            m_Player.MaxMana += NewMana > 0 ? NewMana : 0;
            m_Player.MaxStam += NewStam > 0 ? NewStam : 0;

            if (m_Player.PlayerLevel % 5 == 0)
                m_Player.RemainingFeats += 1;

            m_Player.PerkPoints += 1;

            m_Player.Hits = m_Player.MaxHits;
            m_Player.Stam = m_Player.MaxStam;
            m_Player.Mana = m_Player.MaxMana;
        }

	}
}
