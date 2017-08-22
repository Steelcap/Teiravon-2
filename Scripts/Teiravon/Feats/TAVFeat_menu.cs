using System;
using Server;
using Server.Accounting;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Teiravon.Magecraft;
 
namespace Server.Gumps
{
        public class FeatsGump : Gump
        {
                TeiravonMobile m_Player;
 
                public FeatsGump( Mobile from ) : base( 50, 20 )
                {
                        m_Player = (TeiravonMobile) from;
                        int temp = 0;
 
                        m_Player.CloseGump( typeof(FeatsGump) );
 
                        AddPage( 0 );
                        AddBackground( 50, 60, 600, 500, 3600 );
 
                        AddImage( 0, 0, 10440 );
                        AddImage( 615, 0, 10441 );
 
                        AddLabel( 255, 80, 150, "Teiravon\'s Feat System" );
                        AddLabel( 225, 115, 150, "You have " + m_Player.RemainingFeats + " Feat slot(s) available." );
 
                        AddLabel( 185, 151, 150, "Race Feats" );
                        AddButton( 164, 152, 1209, 1210, 1, GumpButtonType.Page, (int)m_Player.PlayerRace + 1 );
 
                        AddLabel( 448, 151, 150, "Class Feats" );
 
                        // Fighters
                        if (m_Player.IsFighter() || m_Player.IsKensai() || m_Player.IsCavalier())
                            temp = 100;

                        // Paladins
                        else if (m_Player.IsPaladin() || m_Player.IsUndeadHunter())
                            temp = 101;

                        // Barbarians
                        else if (m_Player.IsBerserker() || m_Player.IsDragoon())
                            temp = 102;

                        // Clerics
                        else if (m_Player.IsCleric() || m_Player.IsDarkCleric())
                            temp = 103;

                        // Rangers
                        else if (m_Player.IsRanger() || m_Player.IsArcher() || m_Player.IsMageSlayer())
                            temp = 104;

                        // Rouges
                        else if (m_Player.IsThief() || m_Player.IsAssassin() || m_Player.IsBard() || m_Player.IsScoundrel())
                            temp = 105;

                        // Mages
                        else if (m_Player.IsAeromancer() || m_Player.IsAquamancer() || m_Player.IsGeomancer() || m_Player.IsPyromancer() || m_Player.IsNecromancer())
                            temp = 106;

                        // Druids
                        else if (m_Player.IsForester() || m_Player.IsShapeshifter())
                            temp = 107;
                        // Monk
                        else if (m_Player.IsMonk())
                            temp = 109;
                        else if (m_Player.IsDeathKnight())
                            temp = 110;
                        else if (m_Player.IsDwarvenDefender())
                            temp = 111;
                        else if (m_Player.IsStrider())
                            temp = 112;
                        else if (m_Player.IsMerchant())
                            temp = 113;
                        else if (m_Player.IsSavage())
                            temp = 114;
                        else if (m_Player.IsRavager())
                            temp = 115;

                        else
                            temp = 108;
 
                        AddButton( 427, 152, 1209, 1210, 2, GumpButtonType.Page, temp );
 
                        int imgx = 220;
 
                        AddImage( 345, imgx, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
                        AddImage( 345, imgx += 25, 10004 );
 
                        // Human feats
                        AddPage( 2 );
 
                        AddTitle( "Human Race Feats" );
 
                        ListFeat( TeiravonMobile.Feats.WeaponSpecialization, "Weapon Specialization", 903, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.WealthyLineage, "Wealthy Lineage", 905, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.Apprenticeship, "Apprenticeship", 907, 132, 372 );
                        ListFeat( TeiravonMobile.Feats.WarfareTraining, "Warfare Training", 909, 387, 236 );
                        ListFeat( TeiravonMobile.Feats.Leadership, "Leadership", 9011, 387, 304 );
 
                        // Elven feats
                        AddPage( 3 );
 
                        AddTitle( "Elven Race Feats" );
 
                        ListFeat( TeiravonMobile.Feats.MagicResistance, "Magic Resistance", 9013, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.Blademaster, "Blademaster", 9015, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.Marksmanship, "Marksmanship", 9017, 132, 372 );
                        ListFeat( TeiravonMobile.Feats.Leadership, "Leadership", 9011, 387, 236 );
            //ListFeat( TeiravonMobile.Feats.Dodge, "Dodge", 90273, 387, 304);
 
                        // Drow feats
                        AddPage( 4 );
 
                        AddTitle( "Drow Race Feats" );
 
                        ListFeat( TeiravonMobile.Feats.MagicResistance, "Magic Resistance", 9013, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.Blademaster, "Blademaster", 9015, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.CloakOfDarkness, "Cloak of Darkness", 9019, 132, 372 );
//                      ListFeat( TeiravonMobile.Feats.GlobeOfDarkness, "Globe of Darkness", 9021, 387, 236 );
                        ListFeat( TeiravonMobile.Feats.Leadership, "Leadership", 9011, 387, 304 );
 
                        // Dwarven feats
                        AddPage( 5 );
 
                        AddTitle( "Dwarven Race Feats" );
 
                        ListFeat( TeiravonMobile.Feats.CripplingBlow, "Crippling Blow", 9023, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.ResistPoison, "Resist Poison", 9025, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.ExpertMining, "Expert Mining", 9027, 132, 372 );
                        ListFeat( TeiravonMobile.Feats.Leadership, "Leadership", 9011, 387, 236 );
 
                        // Duergar feats
                        AddPage( 6 );
 
                        AddTitle( "Duergar Race Feats" );
 
                        ListFeat( TeiravonMobile.Feats.ResistPoison, "Resist Poison", 9025, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.ResistPsionics, "Resist Psionics", 9029, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.ExpertMining, "Expert Mining", 9027, 132, 372 );
                        ListFeat( TeiravonMobile.Feats.Leadership, "Leadership", 9011, 387, 236 );
 
                        // Orc feats
                        AddPage( 7 );
 
                        AddTitle( "Orc Race Feats" );
 
                        ListFeat( TeiravonMobile.Feats.Bluudlust, "Bluudlust", 9031, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.CripplingBlow, "Cripplin\' Bluw", 9023, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.BuumBuum, "Buum Buum", 9033, 132, 372 );
                        ListFeat( TeiravonMobile.Feats.Leadership, "Leadurship", 9011, 387, 236 );
                        ListFeat( TeiravonMobile.Feats.Tuffness, "Tuffness", 9035, 387, 304 );
 
 
            // Undead feats
            AddPage(9);
 
            AddTitle("Undead Race Feats");
 
            ListFeat(TeiravonMobile.Feats.FeastoftheDamned, "Feast of the Damned", 90231, 132, 236);
            ListFeat(TeiravonMobile.Feats.BodyoftheGrave, "Body of the Grave", 90233, 132, 304);
            ListFeat(TeiravonMobile.Feats.SinisterForm, "Sinister Form", 90237, 132, 372);
            ListFeat(TeiravonMobile.Feats.DarkRebirth, "Dark Rebirth", 90235, 387, 304);
            ListFeat(TeiravonMobile.Feats.PhysicalResistance, "Physical Resistance", 90239, 387, 236);
 
            // Gnome feats
            AddPage(14);
 
            AddTitle("Gnome Race Feats");
 
            ListFeat(TeiravonMobile.Feats.MagicResistance, "Magic Resistance", 9013, 132, 236);
            ListFeat(TeiravonMobile.Feats.WealthyLineage, "Wealthy Lineage", 905, 132, 304);
            ListFeat(TeiravonMobile.Feats.CharmedLife, "Charmed Life", 90245, 387, 236);
            if (m_Player.IsGnome() && m_Player.IsMerchant())
                ListFeat(TeiravonMobile.Feats.Meticulous, "Meticulous", 90249, 387, 304);
 
            // Goblin feats
            AddPage(15);
 
            AddTitle("Goblin Race Feats");
 
            ListFeat(TeiravonMobile.Feats.StickyFingas, "Sticky Fingas", 90241, 132, 236);
            ListFeat(TeiravonMobile.Feats.LegIt, "Leg It!", 90243, 132, 304);
            ListFeat(TeiravonMobile.Feats.BuumBuum, "Buum Buum", 9033, 132, 372);
            if (m_Player.IsGoblin() && m_Player.IsMerchant())
                ListFeat(TeiravonMobile.Feats.ShoddyCrafts, "Shoddy Craftsmanship", 90247, 387, 236);

            // Frostling feats
            AddPage(17);
 
            AddTitle("Frostling Race Feats");
 
            ListFeat(TeiravonMobile.Feats.NorthernResilience, "Northern Resiliance", 90261, 132, 236);
            ListFeat(TeiravonMobile.Feats.FrostlingHibernation, "Hibernation", 90263, 132, 304);
            ListFeat(TeiravonMobile.Feats.ChilblainTouch, "Chilblain Touch", 90265, 132, 372);
 
                        // Fighter Class Feats
                        AddPage( 100 );
 
                        AddTitle( "Fighter Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
                        ListFeat( TeiravonMobile.Feats.TacticalAssessment, "Tactical Assessment", 9037, 132, 236 );
                        ListFeat(TeiravonMobile.Feats.ArmorProficiency, "Armor Proficiency", 9049, 132, 304);
                        ListFeat( TeiravonMobile.Feats.BattleHardened, "Battle Hardened", 90285, 132, 372);
                        //ListFeat( TeiravonMobile.Feats.WeaponMastery, "Weapon Mastery", 9039, 132, 304 );
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsFighter() )
                                ListFeat( TeiravonMobile.Feats.ShieldBash, "Shield Bash", 9041, 387, 236 );
 
                        else if ( m_Player.IsKensai() )
                                ListFeat( TeiravonMobile.Feats.KaiShot, "Kai Shot", 9043, 387, 236 );
 
                        else if ( m_Player.IsCavalier() )
                                ListFeat( TeiravonMobile.Feats.Riposte, "Riposte", 90219, 387, 236 );
 
                        // Two
                        if ( m_Player.IsFighter() )
                                ListFeat( TeiravonMobile.Feats.CriticalStrike, "Critical Strike", 9045, 387, 304 );
 
                        else if ( m_Player.IsKensai() )
                                ListFeat( TeiravonMobile.Feats.AdvancedHealing, "Advanced Healing", 9047, 387, 304 );
 
                        else if ( m_Player.IsCavalier() )
                                ListFeat( TeiravonMobile.Feats.Challenge, "Chellenging Strike", 90223, 387, 304 );
 
                        // Three
                        if ( m_Player.IsFighter() )
                                ListFeat(TeiravonMobile.Feats.WeaponMaster, "Weapon Master", 9055, 387, 372);
 
                        else if ( m_Player.IsKensai() )
                            ListFeat(TeiravonMobile.Feats.AcrobaticCombat, "Acrobatic Combat", 9051, 387, 372);
 
                        else if ( m_Player.IsCavalier() )
                                ListFeat( TeiravonMobile.Feats.WarMount, "War Mount", 9053, 387, 372 );
 
                        // Four
                        if ( m_Player.IsFighter() )
                                ListFeat( TeiravonMobile.Feats.FuriousAssault, "Furious Assault", 90277, 387, 440 );
 
                        if ( m_Player.IsKensai() )
                                ListFeat( TeiravonMobile.Feats.ExoticWeapons, "Exotic Weaponry", 9057, 387, 440 );
 
                        else if ( m_Player.IsCavalier() )
                                ListFeat( TeiravonMobile.Feats.MountedCombat, "Mounted Combat", 9059, 387, 440 );
 
                        // Paladin Class Feats
                        AddPage( 101 );
 
                        AddTitle( "Paladin Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
                        ListFeat( TeiravonMobile.Feats.TacticalAssessment, "Tactical Assessment", 9037, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.HolyAura, "Holy Aura", 9061, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.DetectEvil, "Detect Evil", 9063, 132, 372 );
                        ListFeat( TeiravonMobile.Feats.ArmorProficiency, "Armor Proficiency", 9049, 132, 440 );
 
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsPaladin() )
                                ListFeat( TeiravonMobile.Feats.LayOnHands, "Lay On Hands", 9065, 387, 236 );
 
                        else if ( m_Player.IsUndeadHunter() )
                                ListFeat( TeiravonMobile.Feats.TurnUndead, "Turn Undead", 9067, 387, 236 );
 
 
                        // Two
                        if ( m_Player.IsPaladin() )
                                ListFeat( TeiravonMobile.Feats.TurnUndead, "Turn Undead", 9067, 387, 304 );
 
                        else if ( m_Player.IsUndeadHunter() )
                                ListFeat( TeiravonMobile.Feats.ResistCurses, "Resist Curses", 9069, 387, 304 );
 
 
                        // Three
                        if ( m_Player.IsPaladin() )
                                ListFeat( TeiravonMobile.Feats.WarMount, "War Mount", 9053, 387, 372 );
 
                        //TODO: Replacement?
                        //else if ( m_Player.IsUndeadHunter() )
                        //      ListFeat( TeiravonMobile.Feats.DivineAura, "Divine Aura", 9071, 387, 372 );
 
 
 
                        // Barbarian Class Feats
                        AddPage( 102 );
 
                        AddTitle( "Barbarian Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
                        ListFeat( TeiravonMobile.Feats.ElementalResistance, "Elemental Resistance", 9073, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.AdvancedHealing, "Advanced Healing", 9047, 132, 304 );
                        ListFeat( TeiravonMobile.Feats.DragonRoar, "Dragon Roar", 9075, 132, 372 );
                        ListFeat( TeiravonMobile.Feats.AxeFighter, "Axe Fighter", 90191, 132, 440 );
                        ListFeat( TeiravonMobile.Feats.UncannyDefense, "Uncanny Defense", 90271, 387, 440);
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsBerserker() )
                                ListFeat( TeiravonMobile.Feats.BerserkerRage, "Berserker Rage", 9077, 387, 236 );
 
                        else if ( m_Player.IsDragoon() )
                                ListFeat( TeiravonMobile.Feats.PowerLunge, "Power Lunge", 9079, 387, 236 );
 
 
                        // Two
                        if ( m_Player.IsBerserker() )
                            ListFeat(TeiravonMobile.Feats.WeaponMastery, "Weapon Master", 9055, 387, 304);
 
                        if ( m_Player.IsDragoon() )
                                ListFeat( TeiravonMobile.Feats.FuriousAssault, "Furious Assault", 90277, 387, 304 );
 
 
                        // Three
                        if ( m_Player.IsBerserker() )
                                ListFeat( TeiravonMobile.Feats.FuriousAssault, "Furious Assault", 90277, 387, 372 );
 
                        else if ( m_Player.IsDragoon() )
                                ListFeat( TeiravonMobile.Feats.ArmorProficiency, "Armor Proficiency", 9049, 387, 372 );
 
 
 
                        // Cleric Class Feats
                        AddPage( 103 );
 
                        AddTitle( "Cleric Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
                        ListFeat( TeiravonMobile.Feats.AlchemyScience, "Alchemy Science", 9085, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.AdvancedHealing, "Advanced Healing", 9047, 132, 304 );
                        ListFeat(TeiravonMobile.Feats.HealersOath, "Healer's Oath", 90227, 132, 372);
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsCleric() )
                                ListFeat( TeiravonMobile.Feats.TurnUndead, "Turn Undead", 9067, 387, 236 );
 
                        else if ( m_Player.IsDarkCleric() )
                                ListFeat( TeiravonMobile.Feats.RebukeUndead, "Rebuke Undead", 9087, 387, 236 );
 
 
                        // Two
                        if ( m_Player.IsCleric() )
                                ListFeat( TeiravonMobile.Feats.HolyAura, "Holy Aura", 9061, 387, 304 );
 
                        else if ( m_Player.IsDarkCleric() )
                                ListFeat( TeiravonMobile.Feats.DarkAura, "Dark Aura", 9089, 387, 304 );
 
 
                        // Three
                        if ( m_Player.IsCleric() )
                                ListFeat( TeiravonMobile.Feats.BestowBlessing, "Bestow Blessing", 9091, 387, 372 );
 
                        else if ( m_Player.IsDarkCleric() )
                                ListFeat( TeiravonMobile.Feats.BestowDarkBlessing, "Bestow Dark Blessing", 9093, 387, 372 );
 
 
                        // Four
                        if ( m_Player.IsCleric() )
                                ListFeat( TeiravonMobile.Feats.DivineMight, "Divine Might", 9095, 387, 440 );
 
                        else if ( m_Player.IsDarkCleric() )
                                ListFeat( TeiravonMobile.Feats.UnholyMight, "Unholy Might", 9097, 387, 440 );
 
 
                        // Ranger Class Feats
                        AddPage( 104 );
 
                        AddTitle( "Ranger Class Feats" );
                        AddClassFeatHeading();
                        
                        // Standard Feats
                        ListFeat( TeiravonMobile.Feats.StalkingPrey, "Hunter Stalks His Prey", 90281, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.Orientation, "Orientation", 90101, 132, 289 );

                        if (!m_Player.IsMageSlayer())
                                    ListFeat( TeiravonMobile.Feats.AnimalCompanion, "Animal Companion", 90103, 132, 342 );
                        if (m_Player.IsMageSlayer())
                            ListFeat(TeiravonMobile.Feats.Inscription, "Inscription", 90155, 132, 342);
                        if (m_Player.IsRanger())
                            ListFeat(TeiravonMobile.Feats.JackOfAll, "Jack Of All Trades", 90215, 132, 395);
 
                        if ( m_Player.IsRanger() )
                            ListFeat(TeiravonMobile.Feats.AnimalHusbandry, "Animal Husbandry", 90107, 132, 448);
 
                        else if (m_Player.IsMageSlayer())
                            ListFeat(TeiravonMobile.Feats.UseMagicDevice, "Use Magic Device", 90129, 132, 395);
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsRanger() )
                                ListFeat( TeiravonMobile.Feats.AdvancedTracking, "Advanced Tracking", 90109, 387, 236 );
 
                        else if ( m_Player.IsArcher() )
                                ListFeat( TeiravonMobile.Feats.CalledShot, "Called Shot", 90111, 387, 236 );
           
                        else if (m_Player.IsMageSlayer())
                            ListFeat(TeiravonMobile.Feats.AcrobaticCombat, "Acrobatic Combat", 9051, 387, 236);
 
 
                        // Two
                        if ( m_Player.IsRanger() )
                                ListFeat( TeiravonMobile.Feats.BigGameHunter, "Big Game Hunter", 90283, 387, 289 );
 
                        else if ( m_Player.IsArcher() )
                                ListFeat( TeiravonMobile.Feats.EnchantedQuiver, "Enchanted Quiver", 90221, 387, 289 );
           
                        else if (m_Player.IsMageSlayer())
                            ListFeat(TeiravonMobile.Feats.Banish, "Banishment", 90251, 387, 289 );
             
                                    // Three
                        if (m_Player.IsRanger())
                            ListFeat(TeiravonMobile.Feats.BeastLore, "Beast Lore", 90117, 387, 342);
             
                        else if (m_Player.IsArcher())
                            ListFeat(TeiravonMobile.Feats.Marksmanship, "Marksmanship", 9017, 387, 342);
                       
                        else if (m_Player.IsMageSlayer())
                            ListFeat(TeiravonMobile.Feats.MindBlast, "Mind Blast", 90141, 387, 342);
               
 
 
                        // Four
                        if ( m_Player.IsRanger() )
                                ListFeat( TeiravonMobile.Feats.WildernessLore, "Wilderness Lore", 90119, 387, 395 );
                        else if ( m_Player.IsMageSlayer() )
                            ListFeat( TeiravonMobile.Feats.WyrdSense, "Wyrd Sense", 90255, 387, 395 );
                        else if ( m_Player.IsArcher() )
                            ListFeat(TeiravonMobile.Feats.Camouflage, "Camouflage", 90279, 387, 395);
 
 
                        // Five
                        if ( m_Player.IsRanger() )
                                ListFeat( TeiravonMobile.Feats.Forensics, "Forensics", 90121, 387, 448 );
 
                        else if ( m_Player.IsArcher() )
                                ListFeat( TeiravonMobile.Feats.BowSpecialization, "Bow Specialization", 90123, 387, 448 );
           
                        else if (m_Player.IsMageSlayer())
                            ListFeat(TeiravonMobile.Feats.DisruptingPresence, "Disrupting Presence", 90253, 387, 448);
             
                        // Rogue Class Feats
                        AddPage( 105 );
 
                        AddTitle( "Rogue Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
 
                        ListFeat( TeiravonMobile.Feats.Backstab, "Backstab", 90125, 132, 236 );
                        ListFeat(TeiravonMobile.Feats.AcrobaticCombat, "Acrobatic Combat", 9051, 132, 289);
                        ListFeat( TeiravonMobile.Feats.Disguise, "Disguise", 90127, 132, 342 );
                        ListFeat( TeiravonMobile.Feats.UseMagicDevice, "Use Magic Device", 90129, 132, 395 );
                        ListFeat(TeiravonMobile.Feats.Espionage, "Espionage", 90225, 132, 448);
 
                        // Kit Feats
                        // One
                        if (m_Player.IsThief() || m_Player.IsScoundrel())
                            ListFeat(TeiravonMobile.Feats.JackOfAll, "Jack Of All Trades", 90215, 387, 236);
                        //else if ( m_Player.IsAssassin() )
                                //ListFeat( TeiravonMobile.Feats.Apothecary, "Apothecary", 90133, 387, 236 );
 
                        else if ( m_Player.IsBard() )
                                ListFeat( TeiravonMobile.Feats.EnchantingMelody, "Enchanting Melody", 90211, 387, 236 );
 
 
                        // Two
                        if ( m_Player.IsThief() )
                                ListFeat( TeiravonMobile.Feats.NimbleFingers, "Nimble Fingers", 90137, 387, 289 );

                        else if (m_Player.IsScoundrel())
                            ListFeat(TeiravonMobile.Feats.CunningFlourish, "Cunning Flourish", 90291, 387, 289);

                        else if ( m_Player.IsAssassin() )
                                ListFeat( TeiravonMobile.Feats.ExoticPoisons, "Exotic Poisons", 90139, 387, 289 );
 
                        else if ( m_Player.IsBard() )
                                ListFeat( TeiravonMobile.Feats.CuttingWords, "Cutting Words", 90213, 387, 289 );
 
 
                        // Three
                        if ( m_Player.IsThief() )
                                ListFeat( TeiravonMobile.Feats.Disarm, "Disarm", 90143, 387, 342 );
                        else if ( m_Player.IsScoundrel() )
                            ListFeat(TeiravonMobile.Feats.RottenLuck, "Rotten Luck", 90287, 387, 342);
                        else if ( m_Player.IsAssassin() )
                                ListFeat( TeiravonMobile.Feats.AdvancedStealth, "Advanced Stealth", 90145, 387, 342 );
 
                        else if ( m_Player.IsBard() )
                                ListFeat( TeiravonMobile.Feats.JackOfAll, "Jack Of All Trades", 90215, 387, 342 );
 
 
                        // Four
                        if (m_Player.IsThief() || m_Player.IsScoundrel())
                            ListFeat(TeiravonMobile.Feats.Scavenger, "Scavenger", 90275, 387, 395);
                        else if (m_Player.IsAssassin())
                            ListFeat(TeiravonMobile.Feats.GreivousWounds, "Greivous Wounds", 90229, 387, 395);
//                      else if ( m_Player.IsAssassin() )
                        //                              ListFeat( TeiravonMobile.Feats.ContractKilling, "Contract Killing", 90149, 387, 448 );
 
                        // Five

                        if( m_Player.IsScoundrel())
                            ListFeat(TeiravonMobile.Feats.DirtyTricks, "Dirty Tricks", 90289, 387, 448);

                        // Mage Class Feats
                        AddPage( 106 );
 
                        AddTitle( "Mage Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
 
                        ListFeat( TeiravonMobile.Feats.AlchemyScience, "Alchemy Science", 9085, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.ArcaneEnchantment, "Arcane Enchantment", 90153, 132, 289 );
                        ListFeat( TeiravonMobile.Feats.Inscription, "Inscription", 90155, 132, 342 );
                        ListFeat(TeiravonMobile.Feats.MeditativeConcentration, "Meditative Concetration", 90197, 132, 395);
 
                        //if ( !m_Player.IsNecromancer() )
                        //      ListFeat( TeiravonMobile.Feats.ImprovedFamiliar, "Improved Familiar", 90151, 132, 395 );
 
 
                        // DISABLED - ListFeat( TeiravonMobile.Feats.Bookworm, "Bookworm", 90157, 132, 448 );
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.RebukeUndead, "Rebuke Undead", 9087, 387, 236 );
 
                        else if ( !m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.ElementalResistance, "Elemental Resistance", 9073, 387, 236 );
 
 
                        // Two
                        if ( m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.SummonUndead, "Summon Undead", 90159, 387, 289 );
 
                        if ( !m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.ImprovedFamiliar, "Improved Familiar", 90151, 387, 289 );
 
 
                        // Three
                        if ( m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.NecroSpellbook, "Advanced Necromancy", 90163, 387, 342 );
 
                        if ( !m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.Apprenticeship, "Apprenticeship", 907, 387, 342 );
 
 
                        // Four
                        if ( m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.UndeadBond, "Undead Bond", 90133, 387, 395 );
 
                        else if ( !m_Player.IsNecromancer() )
                                ListFeat( TeiravonMobile.Feats.RigorousTraining, "Rigorous Training", 90165, 387, 395 );
 
 
                        // Druid Class Feats
                        AddPage( 107 );
 
                        AddTitle( "Druid Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
 
                        ListFeat( TeiravonMobile.Feats.ResistPoison, "Resist Poison", 9025, 132, 236 );
                        ListFeat( TeiravonMobile.Feats.WildShape, "Wild Shape", 90169, 132, 289 );
                        ListFeat( TeiravonMobile.Feats.AnimalCompanion, "Animal Companion", 90103, 132, 342 );
                        ListFeat(TeiravonMobile.Feats.BeastLore, "Beast Lore", 90117, 132, 395);
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsForester() )
                                ListFeat( TeiravonMobile.Feats.ElementalResistance, "Elemental Resistance", 9073, 387, 236 );
 
                        else if ( m_Player.IsShapeshifter() )
                                ListFeat( TeiravonMobile.Feats.AdvancedHealing, "Advanced Healing", 9047, 387, 236 );
 
 
                        // Two
                        if ( m_Player.IsForester() )
                                ListFeat( TeiravonMobile.Feats.NaturesEnchantment, "Nature\'s Enchantment", 90171, 387, 289 );
 
                        else if ( m_Player.IsShapeshifter() )
                                ListFeat( TeiravonMobile.Feats.Telepathy, "Telepathy", 90173, 387, 289 );
 
 
                        // Three
                        if ( m_Player.IsForester() )
                                ListFeat( TeiravonMobile.Feats.TempestsWrath, "Tempest\'s Wrath", 90259, 387, 342 );
 
                        else if ( m_Player.IsShapeshifter() )
                                ListFeat( TeiravonMobile.Feats.Bite, "Bite", 90177, 387, 342 );
 
 
                        // Four
                        if ( m_Player.IsForester() )
                                ListFeat( TeiravonMobile.Feats.NaturesDefender, "Nature\'s Defender", 90257, 387, 395 );
 
                        else if ( m_Player.IsShapeshifter() )
                                ListFeat( TeiravonMobile.Feats.PhysicalResistance, "Physical Resistance", 90179, 387, 395 );
 
 
                        // Five
                        if ( m_Player.IsForester() )
                            ListFeat(TeiravonMobile.Feats.Inscription, "Inscription", 90155, 387, 448);
                        else if (m_Player.IsShapeshifter() )
                            ListFeat( TeiravonMobile.Feats.Pounce, "Pounce", 90217,387,448);

                        if (m_Player.IsForester())
                            ListFeat(TeiravonMobile.Feats.AlchemyScience, "Alchemy Science", 9085, 387, 491);
 
 
                        // Crafter Class Feats
                        AddPage( 108 );
 
                        AddTitle( "Crafter Class Feats" );
                        AddClassFeatHeading();
 
                        // Standard Feats
                        ListFeat( TeiravonMobile.Feats.RacialCrafting, "Racial Crafting", 90193, 132, 236 );
                        if (m_Player.IsGoblin() && !m_Player.IsAlchemist())
                            ListFeat(TeiravonMobile.Feats.ShoddyCrafts, "Shoddy Craftsmanship", 90247,  132, 289);
                        if (m_Player.IsGnome() && !m_Player.IsAlchemist())
                            ListFeat(TeiravonMobile.Feats.Meticulous, "Meticulous", 90249, 132, 289);
 
                        // Kit Feats
                        // One
                        if ( m_Player.IsTailor() )
                                ListFeat( TeiravonMobile.Feats.AdvancedDying, "Advanced Dying", 90181, 387, 236 );
 
                        else if ( m_Player.IsWoodworker() )
                                ListFeat( TeiravonMobile.Feats.FurnitureStaining, "Furniture Staining", 90185, 387, 236 );
 
                        else if ( m_Player.IsBlacksmith() )
                                ListFeat ( TeiravonMobile.Feats.ArmorEnameling, "Armor Enameling", 90187, 387, 236);
 
                        else if (m_Player.IsAlchemist() )
                                ListFeat ( TeiravonMobile.Feats.Research, "Alchemical Research", 90189, 387, 236);
 
                        // Two
                        if ( m_Player.IsTailor() )
                                ListFeat( TeiravonMobile.Feats.LeatherDying, "Leather Dying", 90183, 387, 289 );
                        /*
                        else if ( m_Player.IsBlacksmith() )
                                ListFeat( TeiravonMobile.Feats.SkilledGatherer, "Skilled Gatherer", 90195, 387, 289 );
 
                        else if ( m_Player.IsWoodworker() )
                                ListFeat( TeiravonMobile.Feats.SkilledGatherer, "Skilled Gatherer", 90195, 387, 289 );
 
                        else if ( m_Player.IsTinker() )
                                ListFeat( TeiravonMobile.Feats.SkilledGatherer, "Skilled Gatherer", 90195, 387, 289 );
            */
                        //else if ( m_Player.IsWoodworker() )
                        //      ListFeat( TeiravonMobile.Feats.SkilledGatherer, "Skilled Gatherer", 90195, 387, 289 );
                        // Three
 
 
                        // Four
 
 
                        // Five
 
            // Monk Class Feats
            AddPage(109);
 
            AddTitle("Monk Class Feats");
            AddClassFeatHeading();
 
            // Standard Feats
 
            ListFeat(TeiravonMobile.Feats.Flurry, "Flurry of Blows", 90199, 132, 236);
            ListFeat(TeiravonMobile.Feats.MindBodySoul, "Mind Body and Soul", 90201, 132, 316);
            ListFeat(TeiravonMobile.Feats.WholenessOfSelf, "Wholeness of Self", 90203, 132, 395);
            ListFeat(TeiravonMobile.Feats.KiStrike, "Ki Strikes", 90205, 387, 395);
            ListFeat(TeiravonMobile.Feats.BodyOfIron, "Body of Iron", 90207, 387, 236);
            ListFeat(TeiravonMobile.Feats.AcrobaticCombat, "Acrobatic Combat", 9051, 387, 316);
 
            // Deathknight Class Feats
            AddPage(110);
 
            AddTitle("Deathknight Class Feats");
            AddClassFeatHeading();
 
            // Standard Feats
 
            ListFeat(TeiravonMobile.Feats.ShieldBash, "Shield Bash", 9041, 132, 236);
            ListFeat(TeiravonMobile.Feats.DarkAura, "Dark Aura", 9089, 132, 304);
            ListFeat(TeiravonMobile.Feats.ArmorProficiency, "Armor Proficiency", 9049, 132, 372);
            ListFeat(TeiravonMobile.Feats.CriticalStrike, "Critical Strike", 9045, 132, 440);
 
            // Kit Feats
            ListFeat(TeiravonMobile.Feats.SummonUndead, "Summon Undead", 90159, 387, 236);
            ListFeat(TeiravonMobile.Feats.NecroSpellbook, "Advanced Necromancy", 90163, 387, 304);
            ListFeat(TeiravonMobile.Feats.DragonRoar, "Dragon Roar", 9075, 387, 372);
            ListFeat(TeiravonMobile.Feats.UndeadBond, "Undead Bond", 90133, 387, 440);
           
           
            // Dwarven Defender Class Feats
            AddPage(111);
 
            AddTitle("Defender Class Feats");
            AddClassFeatHeading();
 
            // Standard Feats
 
            ListFeat(TeiravonMobile.Feats.ShieldBash, "Shield Bash", 9041, 132, 236);
            ListFeat( TeiravonMobile.Feats.AdvancedHealing, "Advanced Healing", 9047, 132, 304);
            ListFeat(TeiravonMobile.Feats.ArmorProficiency, "Armor Proficiency", 9049, 132, 372);
            ListFeat(TeiravonMobile.Feats.CriticalStrike, "Critical Strike", 9045, 132, 440);
 
            // Kit Feats
            ListFeat(TeiravonMobile.Feats.Riposte, "Riposte", 90219, 387, 236);
            ListFeat(TeiravonMobile.Feats.BattleHardened, "Battle Hardened", 90285, 387, 304);
            ListFeat(TeiravonMobile.Feats.UncannyDefense, "Uncanny Defense", 90271, 387, 372);
            ListFeat(TeiravonMobile.Feats.DefensiveStance, "Defensive Stance", 90269, 387, 440);
            
                        // Dwarven Defender Class Feats
            AddPage(112);
 
            AddTitle("Strider Class Feats");
            AddClassFeatHeading();
 
            // Standard Feats
 
            ListFeat( TeiravonMobile.Feats.CriticalStrike, "Critical Strike", 9045, 387, 236 );
            ListFeat( TeiravonMobile.Feats.TacticalAssessment, "Tactical Assessment", 9037, 132, 236 );
            ListFeat(TeiravonMobile.Feats.ArmorProficiency, "Armor Proficiency", 9049, 387, 289);
			ListFeat( TeiravonMobile.Feats.StalkingPrey, "Hunter Stalks His Prey", 90281, 132, 289 );

            ListFeat(TeiravonMobile.Feats.JackOfAll, "Jack Of All Trades", 90215, 387, 342);
			ListFeat( TeiravonMobile.Feats.BigGameHunter, "Big Game Hunter", 90283, 132, 342 );
			ListFeat( TeiravonMobile.Feats.AnimalCompanion, "Animal Companion", 90103, 132, 395);

            AddPage(113);

            AddTitle("Merchant Class Feats");
            AddClassFeatHeading();

            // Standard Feats

            ListFeat(TeiravonMobile.Feats.CarpenterTraining, "Carpenter Training", 1002, 132, 236);
            ListFeat(TeiravonMobile.Feats.TailorTraining, "Tailor Training", 1006, 132, 289);
            ListFeat(TeiravonMobile.Feats.CookTraining, "Cook Training", 1010, 132, 342);
            ListFeat(TeiravonMobile.Feats.CombatTraining, "Combat Training", 1012, 132, 395);

            ListFeat(TeiravonMobile.Feats.BlacksmithTraining, "Blacksmith Training", 1000, 387, 236);
            ListFeat(TeiravonMobile.Feats.TinkerTraining, "Tinker Training", 1004, 387, 289);
            ListFeat(TeiravonMobile.Feats.FletcherTraining, "Fletcher Training", 1008, 387, 342);
            ListFeat(TeiravonMobile.Feats.MasterCraftsman, "Master Craftsman", 1014, 387, 395);


            // Savage Class Feats
            AddPage(114);

            AddTitle("Savage Class Feats");
            AddClassFeatHeading();

            // Standard Feats

            ListFeat(TeiravonMobile.Feats.BattleHardened, "Battle Hardened", 90285, 132, 236);
            ListFeat(TeiravonMobile.Feats.ElementalResistance, "Elemental Resistance", 9073, 132, 316);
            ListFeat(TeiravonMobile.Feats.Bite, "Savage Bite", 90177, 132, 395);

            
            ListFeat(TeiravonMobile.Feats.BerserkerRage, "Unbridled Rage", 9077, 387, 236);
            ListFeat(TeiravonMobile.Feats.AdvancedHealing, "Advanced Healing", 9047, 387, 316);
            //ListFeat(TeiravonMobile.Feats.BarbarianInstinct, "Savage Instincts", 9083, 387, 395);

            AddPage(115);

            AddTitle("Ravager Class Feats");
            AddClassFeatHeading();

            // Standard Feats

            ListFeat(TeiravonMobile.Feats.AdvancedHealing, "Advanced Healing", 9047, 132, 236);
            ListFeat(TeiravonMobile.Feats.ShieldBash, "Shield Bash", 9041, 132, 289);
            ListFeat(TeiravonMobile.Feats.CunningFlourish, "Cunning Flourish", 90291, 132, 342);
            ListFeat(TeiravonMobile.Feats.Riposte, "Venomous Riposte", 80219, 132, 395);

            ListFeat(TeiravonMobile.Feats.UncannyDefense, "Uncanny Defense", 90271, 387, 236);
            ListFeat(TeiravonMobile.Feats.WeaponMaster, "Weapon Master", 9055, 387, 289);
            ListFeat(TeiravonMobile.Feats.ResistPoison, "Resist Poison", 9025, 387, 342);
                }

 
                private void AddClassFeatHeading()
                {
                        AddLabel( 165, 195, 150, "Standard Feats" );
                        AddLabel( 438, 195, 150, "Kit Feats" );
                }
 
                private void AddTitle( string title )
                {
                        AddHtml( 45, 175, 600, 20, "<basefont size=\"8\" color=\"#ffffff\"><center>" + title + "</center></basefont>", false, false );
                }
 
                private void ListFeat( string title, int x, int y )
                {
                        ListFeat( TeiravonMobile.Feats.None, title, -1, x, y );
                }
 
                private void ListFeat( TeiravonMobile.Feats feat, string title, int index, int titlex, int titley )
                {
                        if ( index != -1 )
                        {
                                AddLabel( titlex, titley, 150, title );
                                AddButton( titlex - 17, titley + 4, 2360, 2360, ( m_Player.HasFeat( feat ) ) ? 0 : index, GumpButtonType.Reply, 0 );
                                AddImage( titlex - 17, titley + 4, ( m_Player.HasFeat( feat ) ) ? 2360 : 2361 );
                                AddLabel( titlex + 19, titley + 24, 150, "Description" );
                                AddButton( titlex, titley + 26, 9702, 9703, index + 1, GumpButtonType.Reply, 0 );
                        } else {
                                AddLabel( titlex, titley, 150, title );
                        }
                }
 
                public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
                {
                        TeiravonMobile m_Player = (TeiravonMobile) sender.Mobile;
 
                        string title, progress, usage, description, type, prerequisites;
 
                        title = progress = usage = description = type = prerequisites = "?";
                        bool sendgump = false;
 
            switch (info.ButtonID)
            {
                case 903:
                    if (m_Player.PlayerLevel < 4)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFighter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                        m_Player.SendGump(new Teiravon.Feats.WeaponSpecGump(m_Player));
 
                    break;
 
                case 904:
                    title = "Weapon Specialization";
                    type = "Instant";
                    progress = "One-Time";
                    prerequisites = "Level 4";
                    usage = "Automatic";
                    description = "You spent considerable time learning how to effectively wield weapons from a military tutor. Because of this training you are able to wield a specified weapon style better than a normal person. You may choose from Bladed, Blunted, or Piercing weaponry to specialize in. NOTE: You cannot exceed skill cap.";
 
                    sendgump = true;
 
                    break;
 
                case 905:
                    if (m_Player.PlayerLevel > 1)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsHuman() && !m_Player.IsGnome())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.WealthyLineage(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.WealthyLineage);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 906:
                    title = "Wealthy Lineage";
                    type = "Instant";
                    progress = "One-Time";
                    prerequisites = "Must be used at Level 1";
                    usage = "Automatic";
                    description = "You come from a wealthy family lineage. Perhaps from a wealthy merchant family or even from a noble bloodline. Because of this family past, you have acquired a princely inheritence that is granted to you once you have reached the required age. You begin your life with a large dowry of gold in your bank.";
 
                    sendgump = true;
 
                    break;
 
                case 907:
                    if (m_Player.PlayerLevel > 1)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsNecromancer() && !m_Player.IsHuman())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.SendGump(new Teiravon.Feats.ApprenticeshipGump(m_Player));
                    }
 
                    break;
 
                case 908:
                    title = "Apprenticeship";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Must be used at Level 1";
                    usage = "Automatic";
                    description = "As a youth, you took on a job working for a local merchant. Under his/her tutelage, you have learned some skill in a chosen trade. As a result, you have started your adult life with some knowledge in trade to assist you in making a living. (NOTE: You will only recieve 30 points in a chosen trade skill from the available list: Camping, Cooking, Healing, Fishing, Lumberjacking, or Mining)";
 
                    sendgump = true;
 
                    break;
 
                case 909:
                    if (m_Player.PlayerLevel > 1)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsHuman())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.WarfareTraining(m_Player);
 
                        //m_Player.AddFeat(TeiravonMobile.Feats.WarfareTraining);
                        //m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9010:
                    title = "Warfare Training";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Must be used at Level 1";
                    usage = "Automatic";
                    description = "Since Humans are constantly at war with one another and with other races, some have learned warfare tactics at a young age. As a result, they start out with better equipment than the standard items. With this Feat, the character is granted a full Copper Ringmail suit and a weapon one grade higher than the starting weapon of your class. Mages, Rogues, & Druids should not choose this Feat.";
 
                    sendgump = true;
 
                    break;
 
                case 9011:
                    if (m_Player.PlayerLevel < 10 || m_Player.Fame < 6500)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsHuman() && !m_Player.IsDrow() &&!m_Player.IsElf() &&!m_Player.IsDwarf() &&!m_Player.IsOrc())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.Leadership(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Leadership);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9012:
                    title = "Leadership";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 10+ and 6500+ fame";
                    usage = "Automatic";
                    description = "As a result of the character\'s adventures and deeds, the character has gained a good reputation and can now acquire new follower types in addition to the basic types. This Feat works alongside the Mercenary/Follower System. ";
 
                    sendgump = true;
 
                    break;
 
                case 9013:
                    if (m_Player.PlayerLevel > 1)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsElf() &&!m_Player.IsDrow() &&!m_Player.IsGnome())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.MagicResistance(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.MagicResistance);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9014:
                    title = "Magic Resistance";
                    type = "Passive";
                    progress = "One-time";
                    prerequisites = "Must be used at Level 1";
                    usage = "Automatic";
                    description = "Through some strange twist of fate at your birth, you have a strange ability to resist magic. As a result, you start out your adult life with a bonus to your magic resistance. (NOTE: You will gain a skill bonus of 30% to your skill Magic Resistance upon creation)";
 
                    sendgump = true;
 
                    break;
 
                case 9015:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsElf() &&!m_Player.IsDrow())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.Blademaster(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Blademaster);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9016:
                    title = "Blademaster";
                    type = "Passive";
                    progress = "One-time";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Those of Elven blood (including Drow) have made an Art out of combat. In this case, the weapon style is that of blades (sword types only). As a result of your training, you have learned to weild your sword with greater precision and dexterity.";
 
                    sendgump = true;
 
                    break;
 
                case 9017:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsElf() &&!m_Player.IsArcher())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.Marksmanship(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Marksmanship);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9018:
                    title = "Marksmanship";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "With your strange ability of keen eyesight, you can weild a bow with greater accuracy.";
 
                    sendgump = true;
 
                    break;
 
                case 9019:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDrow())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CloakOfDarkness);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9020:
                    title = "Cloak of Darkness";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "[CloakOfDarkness";
                    description = "You have gained the innate Drow ability to resist Charm and Curse spells as well as some forms of other Harm-type spells (not including elemental). This is a magical ability that must be activated.";
 
                    sendgump = true;
 
                    break;
 
                case 9021:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDrow())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.GlobeOfDarkness);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9022:
                    title = "Globe of Darkness";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Cloak of Darkness";
                    usage = "[GlobeOfDarkness";
                    description = "You have gained the innate Drow ability to create complete darkness in the immediate area for a small amount of time. Non-drow with Infravision within the radius will lose the ability for the remainder of the spell. This is a magical ability that must be activated.";
 
                    sendgump = true;
 
                    break;
 
                case 9023:
                    if (m_Player.Skills.Anatomy.Base < 30)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsOrc()&& !m_Player.IsDwarf())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CripplingBlow);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9024:
                    title = "Crippling Blow";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Anatomy 30%+";
                    usage = "[Cripple";
                    description = "You have learned the ability to cripple your opponent without killing him. When using this Feat there is a chance to stun and lower enemy stamina making them weak and vulnerable. Useful when trying to capture the target without killing them. This Feat can only be used when using blunt weapons.";
 
                    sendgump = true;
 
                    break;
 
                case 9025:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDwarf() && !m_Player.IsForester() && !m_Player.IsShapeshifter() && !m_Player.IsRavager())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ResistPoison);
                        m_Player.RemainingFeats -= 1;
 
                        m_Player.CheckResistanceBonus();
                    }
 
                    break;
 
                case 9026:
                    title = "Resist Poison";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Through some strange twist of fate at your birth, you have a strange ability to resist any form of natural poison. As a result, you gain a bonus to your poison resistance. (NOTE: This is a special resistance ability and is not magical)";
 
                    sendgump = true;
 
                    break;
 
                case 9027:
                    if (m_Player.PlayerLevel > 1)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDwarf())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.ExpertMining(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.ExpertMining);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9028:
                    title = "Expert Mining";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Must be used at Level 1";
                    usage = "Automatic";
                    description = "Because of your natural affinity with the earth and your passion for mining and smithing, you have learned to tap the earth to gain knowledge of ore veins making mining easier and allowing you to seek out particular ores. You are granted an additional bonus to your Mining Skills.";
 
                    sendgump = true;
 
                    break;
 
                case 9029:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDuergar())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ResistPsionics);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9030:
                    title = "Resist Psionics";
                    type = "Passive";
                    progress = "Usage";
                    prerequisites = "Resist Poison Feat";
                    usage = "Automatic";
                    description = "Through some strange twist of fate at your birth, you have a strange ability to resist psionic spells and spell-based attacks. As a result, you start out your adult life with a bonus to your resistance. (NOTE: This is a special resistance ability)";
 
                    sendgump = true;
 
                    break;
 
                case 9031:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsOrc())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Bluudlust);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9032:
                    title = "Bluudlust";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "[Bluudlust";
                    description = "Orcs have the ability to become enraged with bloodlust.";
 
                    sendgump = true;
 
                    break;
 
                case 9033:
                    if (!m_Player.IsCrafter())
                        m_Player.SendMessage("You are not a crafter.");
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsOrc() && !m_Player.IsGoblin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BuumBuum);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9034:
                    title = "Buum Buum";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Crafter class";
                    usage = "[Buumbuum";
                    description = "You have learned the secret in creating primitive explosion potions. This feat grants you access to a special alchemy system for orcs.";
 
                    sendgump = true;
 
                    break;
 
                case 9035:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsOrc())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Tuffness);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9036:
                    title = "Tuffness";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Your hide is tougher and hardier than most Orcs. As a result, you gain a natural resistance against attacks.";
 
                    sendgump = true;
 
                    break;
 
                case 9037:
                    if (m_Player.RawInt < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFighter()&& !m_Player.IsPaladin() && !m_Player.IsStrider())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.TacticalAssessment);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9038:
                    title = "Tactical Assessment";
                    type = "Passive";
                    progress = "Usage";
                    prerequisites = "Intelligence 50+";
                    usage = "Automatic";
                    description = "The character gains the ability to assess their opponent during combat. Periodically the combat statistics of a target will be displayed to the character. The skill level of this Feat determines the type of information gained.";
 
                    sendgump = true;
 
                    break;
 
                case 9039:
                    if (m_Player.PlayerLevel < 4)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFighter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.SendGump(new Teiravon.Feats.WeaponMasteryGump(m_Player));
                    }
 
                    break;
 
                case 9040:
                    title = "Weapon Mastery";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 4";
                    usage = "Automatic";
                    description = "You spent considerable time learning how to effectively weild weapons from a military tutor. Because of this training you are able to wield a specified weapon style better than a normal person. You may choose from Bladed, Blunted, or Piercing weaponry to specialize in. This Feat can stack with Weapon Specialization.";
 
                    sendgump = true;
 
                    break;
 
                case 9041:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.ArmorProficiency) && !m_Player.IsRavager())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFighter() && !m_Player.IsDeathKnight() && !m_Player.IsDwarvenDefender() && !m_Player.IsRavager())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ShieldBash);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9042:
                    title = "Shield Bash";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Armor Proficiency Feat or Ravager";
                    usage = "Double-Click an equipped shield";
                    description = "You can use your shield as a weapon in combat by hitting your opponent from the front. Shield Bash will inflict damage plus having a chance to stun your opponent. The effectiveness of the bash increases as the skill advances allowing for more damage.";
 
                    sendgump = true;
 
                    break;
 
                case 9043:
                    if (m_Player.Skills.Anatomy.Base < 40)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsKensai())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.KaiShot);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9044:
                    title = "Kai Shot";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Anatomy Skill 40+";
                    usage = "[KaiShot";
                    description = "You have learned to harness your Ki into a focused attack. Because of this concentrated energy focus, once this powerful attack is performed, you must wait and let your Ki energize before you can use it again. The power and damage of the Kai Shot depends on the skill level of the character. Kai Shot will drain mana and stamina when it is used.";
 
                    sendgump = true;
 
                    break;
 
                case 9045:
                    if (m_Player.Skills.Anatomy.Base < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFighter()&& !m_Player.IsDeathKnight()&&!m_Player.IsDwarvenDefender() &&!m_Player.IsStrider())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CriticalStrike);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9046:
                    title = "Critical Strike";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Anatomy Skill 50+";
                    usage = "[CriticalStrike";
                    description = "This ability can be activated when using any weapon. The character has a chance of finding a vulnerable place on the target (usually the vital points of the body) inflicting more damage than normal. The amount of the damage modifier will increase as the skill progresses.";
 
                    sendgump = true;
 
                    break;
 
                case 9047:
                    if (!m_Player.IsShapeshifter() && m_Player.Skills.Healing.Base < 60)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.IsShapeshifter() && (!m_Player.HasFeat(TeiravonMobile.Feats.WildShape) || !m_Player.HasFeat(TeiravonMobile.Feats.Bite)))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsKensai() && !m_Player.IsDragoon() && !m_Player.IsBerserker() && !m_Player.IsShapeshifter() && !m_Player.IsDwarvenDefender() && !m_Player.IsCleric() && !m_Player.IsDarkCleric() && !m_Player.IsSavage() && !m_Player.IsRavager())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.AdvancedHealing);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9048:
                    title = "Advanced Healing";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Non-Druid - Healing 60+; Druid - Wild Shape and Bite Feats";
                    usage = "Automatic";
                    description = "You have learned the strange ability to go into a deep meditative trance, thus allowing your body to heal itself much faster than otherwise possible. But, entering and leaving this deep trance takes alot of concentration and taxes the body. Once leaving a trance, you loose mana in return for your health and you must give your body time to rest before entering it again.";
 
                    sendgump = true;
 
                    break;
 
                case 9049:
                    if (m_Player.PlayerLevel < 5)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFighter()&& !m_Player.IsCavalier()&&!m_Player.IsDragoon()&&!m_Player.IsPaladin()&&!m_Player.IsDwarvenDefender()&&!m_Player.IsDeathKnight() &&!m_Player.IsStrider())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ArmorProficiency);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9050:
                    title = "Armor Proficiency";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 5";
                    usage = "Automatic";
                    description = "This Feat allows the character to use the heavier armor types (plate armor) if the character\'s class can wear plate armor. The strength requirements still remain for that plate armor type. This also pertains to the magical equivalent of that armor type.";
 
                    sendgump = true;
 
                    break;
 
                case 9051:
                    if (m_Player.RawDex < 60)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsKensai() && !m_Player.IsMageSlayer() && !m_Player.IsAssassin() && !m_Player.IsThief() && !m_Player.IsBard() && !m_Player.IsScoundrel() && !m_Player.IsMonk())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.AcrobaticCombat);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9052:
                    title = "Acrobatic Combat";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Dexterity 60+";
                    usage = "]feint, ]roll";
                    description = "You have honed your reflexes to a state above normal. You're able to dodge ranged attacks, even magical ones. As well as learning to trick your enemies into attempting to block a faked attack, devistating their defenses.";
 
                    sendgump = true;
 
                    break;
 
                case 9053:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCavalier()&& !m_Player.IsPaladin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.WarMount);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9054:
                    title = "War Mount";
                    type = "Active";
                    progress = "One-Time";
                    prerequisites = "Dexterity 60+, Level 10+";
                    usage = "[CallMount";
                    description = "When you have learned enough experience, you will gain the ability to call forth a mystical steed as your companion. This steed will carry you faithfully into battle and across the lands. Be forewarned, if your warmount is to die in battle, the price you pay to earn a new mount will not be light.";
 
                    sendgump = true;
 
                    break;
 
                case 9055:
                    if (m_Player.PlayerLevel < 11)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFighter() && !m_Player.IsBerserker() && !m_Player.IsRavager())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.WeaponMaster);
                        m_Player.SendGump(new Teiravon.Feats.WpnMasterGump(m_Player));
                    }
 
                    break;
 
                case 9056:
                    title = "Weapon Master";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 11+";
                    usage = "Automatic";
                    description = "You spent considerable time training in a specific Weapon of your choice. Because of this training you recieve weapon and tactics skill bonuses when wielding this weapon.";
 
                    sendgump = true;
 
                    break;
 
                case 9057:
                    m_Player.SendMessage("This feat has been disabled.");
                    break;
 
                /*
                if ( m_Player.PlayerLevel < 5 )
                    m_Player.SendMessage( Teiravon.Messages.NoReqs );
                else if ( m_Player.RemainingFeats < 1 )
                    m_Player.SendMessage( Teiravon.Messages.NoFeatSlots );
                else
                {
                    m_Player.AddFeat( TeiravonMobile.Feats.ExoticWeapons );
                    m_Player.RemainingFeats -= 1;
                }
 
                break;
                */
 
                case 9058:
                    title = "Exotic Weaponry";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 5";
                    usage = "Automatic";
                    description = "You have learned to weild different types of exotic weapons. As you learn to weild them more effeciently, your options of weapon types will also increase. Keep in mind that you can only use weapons that your character class can use. It is not known how many types of these strange weapons exist as their knowledge has been lost since during the wars in the Age of Chaos.";
 
                    sendgump = true;
 
                    break;
 
                case 9059:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.WarMount))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCavalier())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.MountedCombat);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9060:
                    title = "Mounted Combat";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Warmount Feat";
                    usage = "Automatic";
                    description = "You have learned the art of mounted combat through extensive and rigorous training. You are able to fight on horseback using weapons such as lances and other weapons designed for mounted combat. You have also learned how to use mounts, especially War Mounts, to their fullest extent, helping you attack and defend while mounted.";
 
                    sendgump = true;
 
                    break;
 
                case 9061:
                    if (m_Player.Karma < 5000)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCleric()&& !m_Player.IsPaladin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.HolyAura);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9062:
                    title = "Holy Aura";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Karma 5000+";
                    usage = "[HolyAura";
                    description = "Your training and deeds to your deity have granted you the ability to protect yourself from evil-aligned characters. As you continue to follow your Path and gain experience, so too will the potency of the aura increase when it is used.";
 
                    sendgump = true;
 
                    break;
 
                case 9063:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsPaladin()&&!m_Player.IsUndeadHunter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DetectEvil);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9064:
                    title = "Detect Evil";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "[DetectEvil";
                    description = "Your divine powers have grown to the extent that you are able to determine the alignment of another character if you are skilled enough. As you become more experienced, you will be able to determine the extremity of the person\'s alignment if they are evil. At higher levels, you will be able to detect any of the Unliving as well.";
 
                    sendgump = true;
 
                    break;
 
                case 9065:
                    if (m_Player.Skills.Healing.Base < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsPaladin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.LayOnHands);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9066:
                    title = "Lay on Hands";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Healing skill 50+";
                    usage = "[LayOnHands";
                    description = "You have been gifted with the touch of healing. By praying to your diety, you are able to call upon the healing powers of the Divine and heal the one that you touch (not including yourself). The potency of your touch is based on your level of experience. As you advance, so too will the power of your touch. This ability can only be used once per day.";
 
                    sendgump = true;
 
                    break;
 
                case 9067:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCleric()&& !m_Player.IsPaladin()&&!m_Player.IsUndeadHunter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.TurnUndead);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9068:
                    title = "Turn Undead";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Double-click your holy symbol";
                    description = "Through the character, the deity manifests a portion of its power, terrifying evil, undead creatures or blasting them right out of existence. No action can be taken while the character is attempting to turn undead. A lower skill levels, the character can turn undead so they lose moral and run away. At higher skill levels, the character will have a chance to destroy them outright.";
 
                    sendgump = true;
 
                    break;
 
                case 9069:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsUndeadHunter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ResistCurses);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9070:
                    title = "Resist Curses";
                    type = "Passive";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Because of your dedication to your Diety's cause, your Deity has granted you a unique ability to resist curses that are commonly used by the Unliving. As a result, you gain a bonus to your curse resistance. (NOTE: This is a special resistance ability and is not magical)";
 
                    sendgump = true;
 
                    break;
 
                case 9071:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsUndeadHunter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DivineAura);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9072:
                    title = "Divine Aura";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "[DivineAura";
                    description = "Your training and deeds to your deity have granted you the ability to protect yourself from undead characters. As you continue to follow your Path and gain experience, so too will the potency of the aura increase when it is used.";
 
                    sendgump = true;
 
                    break;
 
                case 9073:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDragoon()&& !m_Player.IsBerserker()&& !m_Player.IsForester()&& !m_Player.IsAeromancer()&& !m_Player.IsGeomancer()&& !m_Player.IsAquamancer()&& !m_Player.IsPyromancer() &&!m_Player.IsSavage())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ElementalResistance);
                        m_Player.RemainingFeats -= 1;
 
                        m_Player.CheckResistanceBonus();
                    }
 
                    break;
 
                case 9074:
                    title = "Elemental Resistance";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Due to the constant exposure to the elements, your body has developed a unique ability to resist the elements, be it magical or natural. As your skill in this ability increases, so too will the resistance percent increase. Each element has its own area of reistance: Earth, Air, Fire, and Water (Water includes Acid and Poison).";
 
                    sendgump = true;
 
                    break;
 
                case 9075:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDragoon()&& !m_Player.IsBerserker()&& !m_Player.IsDeathKnight())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DragonRoar);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9076:
                    title = "Dragon Roar";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "[DragonRoar";
                    description = "You have learned an ancient barbarian technique that mimics the power of a Dragon\'s roar, therefore striking fear into the heart of your enemy causing them to flee in terror. (NPCs will flee in terror, PCs will lose stamina and drop their weapon if they fail the save roll)";
 
                    sendgump = true;
 
                    break;
 
                case 9077:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsBerserker() && !m_Player.IsSavage())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BerserkerRage);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9078:
                    title = "Berserker Rage";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "You have learned the ability to become increasingly enraged while in combat, increasing their damage as they get drunk off the high of combat.";
 
                    sendgump = true;
 
                    break;
 
                case 9079:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDragoon())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.PowerLunge);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9080:
                    title = "Power Lunge";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "[Lunge";
                    description = "This Feat is a specialty of Barbarian Dragoons and allows him to sweep his weapon damaging all opponents within his range.  This ability is usable with polearms, axes, and viking swords with polearms having a longer reach.";
 
                    sendgump = true;
 
                    break;
 
                case 9081:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ChargedStrike);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9082:
                    title = "Charged Strike";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "[ChargedStrike";
                    description = "By tapping the character's Mana reserve, they are able to place a temporary, one-time damage enchantment on their weapon. If successful, it will penetrate the opponent\'s defenses for extra damage. This ability can only be parried with a defender\'s parrying skill equivalent or better to that of the attacker\'s weapon skill and the opponent must be wearing a shield in order to block the attack.";
 
                    sendgump = true;
 
                    break;
 
                case 9083:
                    /*if ( !m_Player.HasFeat( TeiravonMobile.Feats.WeaponMastery ) )
                        m_Player.SendMessage( Teiravon.Messages.NoReqs );
                    else*/
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsBerserker() || !m_Player.IsStrider())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BarbarianInstinct);
                        m_Player.Skills.DetectHidden.Base = 100.0;
                        m_Player.Skills.Focus.Cap = 100.0;
                        if (m_Player.Skills.Focus.Base <= 30.0)
                            m_Player.Skills.Focus.Base = 30.0;
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9084:
                    title = "Barbarian Focus";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "None";
                    usage = "None";
                    description = "This Feat gives the Berserker the ability to be supremenly aware of himself and his surroundings.  He can detect hidden enemies, avoid be surprise attacked and becomes supremely focused.";
 
                    sendgump = true;
 
                    break;
 
                case 9085:
                    if (m_Player.RawInt < 60)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCleric() && !m_Player.IsDarkCleric() && !m_Player.IsNecromancer() && !m_Player.IsAeromancer() && !m_Player.IsAquamancer() && !m_Player.IsPyromancer() && !m_Player.IsGeomancer() && !m_Player.IsForester())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.AlchemyScience(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.AlchemyScience);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9086:
                    title = "Alchemy Science";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Intelligence 60+";
                    usage = "Automatic";
                    description = "This Feat will grant the character knowledge of the Art of Alchemy and its uses. Upon learning this Feat, the character will receive 30 skill points in Alchemy and gain the character\'s proper Alchemy equipment.";
 
                    sendgump = true;
 
                    break;
 
                case 9087:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDarkCleric()&&!m_Player.IsNecromancer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.RebukeUndead);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9088:
                    title = "Rebuke Undead";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Double-click your holy symbol";
                    description = "Through the character, the deity manifests a portion of its power, taking control of undead creatures. However, since the power must be channeled through a mortal vessel, success is not always assured. No action can be taken while the character is attempting to rebuke undead. A lower skill levels, the character can control certain forms of undead. As the skill progresses, the types of controlled undead available increases.";
 
                    sendgump = true;
 
                    break;
 
                case 9089:
                    if (m_Player.Karma > -5000)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDarkCleric()&&!m_Player.IsDeathKnight())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DarkAura);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9090:
                    title = "Dark Aura";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Karma -5000";
                    usage = "[DarkAura";
                    description = "Your training and deeds to your deity have granted you the ability to protect yourself from good-aligned characters. As you continue to follow your Path and gain experience, so too will the potency of the aura increase when it is used.";
 
                    sendgump = true;
 
                    break;
 
                case 9091:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCleric())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You have gained the Bestow Blessing Feat.");
 
                        m_Player.AddFeat(TeiravonMobile.Feats.BestowBlessing);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9092:
                    title = "Bestow Blessing";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Double-click your holy symbol";
                    description = "You have been gifted with the ability to place blessings upon specially prepared items. This Feat is a requirement in creating holy items, such as Holy Water which is a bane of the Undead. The type of items that can be blessed increases based on the skill level of the Feat.";
 
                    sendgump = true;
 
                    break;
 
                case 9093:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDarkCleric())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You have gained the Bestow Dark Blessing Feat.");
 
                        m_Player.AddFeat(TeiravonMobile.Feats.BestowDarkBlessing);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9094:
                    title = "Bestow Dark Blessing";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Double-click your unholy symbol";
                    description = "You have been gifted with the ability to place blessings upon specially prepared items. This Feat is a requirement in creating unholy items, such as Unholy Water which is a boon to the Undead. The type of items that can be blessed increases based on the skill level of the Feat.";
 
                    sendgump = true;
 
                    break;
 
                case 9095:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCleric())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DivineMight);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9096:
                    title = "Divine Might";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Double-click your holy symbol";
                    description = "Using your Holy Symbol as a foci, you are able to call upon the Divine energies and grant yourself a damage boon in combat. For a limited time, you are able to deal extra damage to most undead creatures. Using this Feat will require you to expend a portion of your mana pool.";
 
                    sendgump = true;
 
                    break;
 
                case 9097:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDarkCleric())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.UnholyMight);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 9098:
                    title = "Unholy Might";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Double-click your unholy symbol";
                    description = "Using your Unholy Symbol as a foci, you are able to call upon the Divine energies and grant yourself a damage boon in combat. For a limited time, you are able to deal extra damage to most lawful creatures. Using this Feat will require you to expend a portion of your mana pool.";
 
                    sendgump = true;
 
                    break;
 
                case 9099:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger()&&!m_Player.IsArcher()&&!m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                        m_Player.SendGump(new RacialEnemyGump(m_Player, false));
 
                    break;
 
                case 90100:
                    title = "Racial Enemy";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "";
                    description = "Rangers (including Archers) are natural hunters in the wilderness. They have the ability to focus their efforts against a particular type of creature. The Ranger will gain an attack bonus against the chosen creature type. The damage inflicted increases as the Ranger gains experience.";
 
                    sendgump = true;
 
                    break;
 
                case 90101:
                    if (m_Player.Skills.Tracking.Base < 75)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger()&&!m_Player.IsArcher()&&!m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.Orientation(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Orientation);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90102:
                    title = "Orientation";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Tracking 75+";
                    usage = "Automatic";
                    description = "Rangers are explorers and guides of the Realm. Because of this, they have learned how to discern the lay of the land. This Feat grants the ability to learn Cartography and the use of maps. The Ranger receives 30 skill points in Cartography and a map kit. Additional materials can be purchased from scribes.";
                    sendgump = true;
 
                    break;
 
                case 90103:
                    if (m_Player.Skills.Tracking.Base < 75)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger()&&!m_Player.IsArcher()&&!m_Player.IsShapeshifter()&& !m_Player.IsForester() && !m_Player.IsStrider())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.AnimalCompanion);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90104:
                    title = "Animal Companion";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Tracking 75+";
                    usage = "[Companion";
                    description = "This Feat gives the Rangers/Druids the ability to gain an animal companion. Once this Feat is gained, the character must enter the Forest and send out the Call of Bonding. The animal type will be the animal that responds to the character\'s bonding call. As the Feat skill progresses, the higher types of that animal will answer the character\'s call. The death of a companion carries a heavy price.";
 
                    sendgump = true;
 
                    break;
 
                case 90105:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.FlushCreatures);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90106:
                    title = "Flush Creatures";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "[Flush";
                    description = "As a Ranger, you have learned how to read your surroundings and know the creatures that reside in it. This grants you the ability to flush out creatures into the open. When creatures are flushed out, some may appear but others in the area go deeper into hiding. Thus, you must wait before using the Feat again. Your skill and techique with flushing improves as you use it.";
 
                    sendgump = true;
 
                    break;
 
                case 90107:
                    
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.AnimalHusbandry);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90108:
                    title = "Animal Husbandry";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "None";
                    description = "A keen eye from the creatures of the wild has granted you a greater affinity for raising and training animals in captivity granting additional stable slots to help facilitate this process.";
 
                    sendgump = true;
 
                    break;
 
                case 90109:
                    if (m_Player.Skills.Tracking.Base < 100)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.AdvancedTracking);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90110:
                    title = "Advanced Tracking";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Tracking 100+";
                    usage = "Automatic";
                    description = "Using a little magic, this feat allows the Ranger to read the biological signs of his target. ( Click on the tracking arrow activate )";
 
                    sendgump = true;
 
                    break;
 
                case 90111:
                    if (m_Player.Skills.Anatomy.Base < 60)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsArcher())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CalledShot);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90112:
                    title = "Called Shot";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Anatomy 60+";
                    usage = "[CalledShot";
                    description = "As a master marksman with the bow, you can pinpoit vital spots on your opponent\'s body. When this Feat is activated, the Archer\'s next few arrows will do additional damage.";
 
                    sendgump = true;
 
                    break;
 
                case 90113:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.RacialEnemy) || m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                        m_Player.SendGump(new RacialEnemyGump(m_Player, true));
 
                    break;
 
                case 90114:
                    title = "Extra Racial Enemy";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Racial Enemy Feat, Level 10+";
                    usage = "";
                    description = "This Feat follows the Ranger's Racial Enemy Feat. It will grant the Ranger an additional creature type to add to his/her current Racial Enemy.  The Ranger will gain an attack bonus against the chosen creature type. The damage inflicted increases as the Ranger gains experience.";
 
                    sendgump = true;
 
                    break;
 
                case 90115:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.Marksmanship) || m_Player.Skills.Anatomy.Base < 45)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsArcher())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ChargedMissile);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90116:
                    title = "Charged Missile";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Marksmanship Feat, Anatomy 45+";
                    usage = "[ChargedMissle";
                    description = "You have learned how to tap into your mana pool to place an small enchantment on your arrows. This technique is a temporary, one-time minor enchantment on the weapon. The damage bonus will last only a small time before it expires. The strength of the enchantment will increase as the Archer becomes more skilled in its use.";
 
                    sendgump = true;
 
                    break;
 
                case 90117:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger()&& !m_Player.IsShapeshifter() && !m_Player.IsForester())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.BeastLore(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.BeastLore);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90118:
                    title = "Beast Lore";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 10+";
                    usage = "Automatic";
                    description = "Because you have spent your life in the wilderness, you have developed a natural affinity towards animals and have learned the ways of the creatures called \"monsters\". This Feat will grant you bonuses to the following skills: Animal Lore skill 45, Animal Taming skill 40, Herding skill 30, and Veterinary skill 45.";
 
                    sendgump = true;
 
                    break;
 
                case 90119:
                    if (m_Player.PlayerLevel < 5)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.WildernessLore(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.WildernessLore);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90120:
                    title = "Wilderness Lore";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 5+";
                    usage = "Automatic";
                    description = "Because you have spent your life in the wilderness, you have developed better skills and techniques with surviving in the wilderness. You know how to create a better campsite and have gained some skill in finding food. This Feat grants you bonuses to the following skills: Camping skill 30, Cooking skill 30, and Fishing skill 30.";
 
                    sendgump = true;
 
                    break;
 
                case 90121:
                    if (m_Player.PlayerLevel < 5)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsRanger())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.Forensics(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Forensics);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90122:
                    title = "Forensics";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 5+";
                    usage = "Automatic";
                    description = "You have the ability to become a skilled tracker. Because of this ability, you have learned to track your prey using skilled means. As a result, you are able to use those skills in other ways. This Feat grants you bonuses to the following skills: Forensic Evaluation skill 30, and Tracking skill 30.";
 
                    sendgump = true;
 
                    break;
 
                case 90123:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsArcher())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BowSpecialization);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90124:
                    title = "Bow Specialization";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Being of the Archer class, you have focused your training on learning how to weild the bow more effectively than any other class. Thus, you gain the ability to use the more powerful bow types. This will allow the character to advance the skill further than normally possible as most of the stronger bow types are denied to every character class type.";
 
                    sendgump = true;
 
                    break;
 
                case 90125:
                    if (m_Player.RawDex < 50 || m_Player.Skills.Stealth.Base < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAssassin() && !m_Player.IsThief() && !m_Player.IsScoundrel() && !m_Player.IsBard())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Backstab);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90126:
                    title = "Backstab";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Dexterity 50+, Stealth 50+";
                    usage = "[Backstab";
                    description = "You are a creatures of the shadows. As such, one of the skills you have learned is the ability to approach your target unseen and unheard. By taking them from behind, you are able to strike one of the critical areas of the body and inflict greater damage than normal. As you become more experienced, the damage you can inflict will increase.";
 
                    sendgump = true;
 
                    break;
 
                case 90127:
                    if (!m_Player.IsHalfElf() && !m_Player.IsThief() && !m_Player.IsScoundrel() && !m_Player.IsAssassin() && !m_Player.IsBard())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else
                    {
                        DisguiseKit m_Disguise = new DisguiseKit();
 
                        m_Player.Backpack.AddItem(m_Disguise);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Disguise);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90128:
                    title = "Disguise";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Half-Elven or Rogue";
                    usage = "Automatic";
                    description = "The character has learned the ability to use disguises and can use the disguise kit to its fullest extent (depending on skill). The character is also granted a disguise kit to their inventory when choosing this feat.";
 
                    sendgump = true;
 
                    break;
 
                case 90129:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAssassin() && !m_Player.IsBard() && !m_Player.IsThief() && !m_Player.IsScoundrel() && !m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.UseMagicDevice);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90130:
                    title = "Use Magical Device";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 10+";
                    usage = "Automatic";
                    description = "You have learned to use enchanted items that spellcasters use. The type of item you can use depends on the your skill in this Feat. Some items include: wands, staves, scrolls, as well as other arcane items. But there is a risk, however, just because you can use the item doesn\'t mean that you know what the item will do when activated. A rogue\'s life is never easy, especially when it comes to the arcane.";
 
                    sendgump = true;
 
                    break;
 
                case 90131:
                    if (m_Player.Skills.Lockpicking.Base < 50 || m_Player.Skills.RemoveTrap.Base < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsThief() && !m_Player.IsScoundrel())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.TrapLore);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90132:
                    title = "Trap Lore";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Lockpicking 50+, Remove Trap 50+";
                    usage = "[SetTrap";
                    description = "Because of your profession, you encounter many traps, most of which always seem to get in your way. Thus you have learned how to disarm traps. You have even learned how to set your own traps using certain resources. Your options will increase as you become more experienced.";
 
                    sendgump = true;
 
                    break;
 
                case 90133:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsNecromancer()&& !m_Player.IsDeathKnight())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.UndeadBond);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90134:
                    title = "Undead Bond";
                    type = "Active and Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "[necrospeak, [drainundead";
                    description = "Your years spent down in the crypts have made you forge a unique bond with the undead. This allows you to speak through them as well as replenish your energy through their dark lifeforce. As your strength grows, several forms of undead will consider you as one of their own.";
 
                    sendgump = true;
 
                    break;
 
                case 90135:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DispelMagic);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90136:
                    title = "Dispel Magic";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "[Dispel";
                    description = "Due to their intense training and constant exposure to magic, Mage Slayers have developed the innate ability to dispel magic. This includes offensive magic spells (poison, curses, etc) and enchantments (Agility, Strength, etc). The dispel strength is relative to the skill of the Feat.";
 
                    sendgump = true;
 
                    break;
 
                case 90137:
                    if (m_Player.RawDex < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsThief() && !m_Player.IsScoundrel())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.NimbleFingers);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90138:
                    title = "Nimble Fingers";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Dexterity 50+";
                    usage = "Automatic";
                    description = "Your profession grants you the ability to pickpocket unwary victims. This includes players and NPC\'s (if the NPC is carrying certain types of loot). Stealing can only be accessed through this Feat and cannot be gained by any other means. What can be stolen is based on the skill level of this Feat.";
 
                    sendgump = true;
 
                    break;
 
                case 90139:
                    if (m_Player.PlayerLevel < 5)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAssassin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ExoticPoisons);
                        m_Player.Skills.Poisoning.Base += 50;
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90140:
                    title = "Exotic Poisons";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Level 5";
                    usage = "Automatic";
                    description = "This Feat allows Assassins the ability to create various poisons and their antidotes.";
 
                    sendgump = true;
 
                    break;
 
                case 90141:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.WyrdSense))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.MindBlast);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90142:
                    title = "Mind Blast";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Wyrd Sense";
                    usage = "MindBlast";
                    description = "Sensing the hollow vacuum left within a mage when their mana is expelled, the MageSlayer creates a powerfully destructive surge within them causing damage dependant on how much mana they're missing.";
 
                    sendgump = true;
 
                    break;
 
                case 90143:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.NimbleFingers) || !m_Player.HasFeat(TeiravonMobile.Feats.AcrobaticCombat))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsThief() && !m_Player.IsScoundrel())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Disarm);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90144:
                    title = "Disarm";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Nimble Fingers Feat, Acrobatic Combat Feat";
                    usage = "[Disarm";
                    description = "Thieves are some of the most agile people in the Realm, mainly due to their lifestyle. Some thieves have learned to use this enhanced quickness to get themselves out of a few tough situations. This Feat allows thieves to disarm their opponent causing them to drop their weapon. At higher levels, the thief can actually snatch the weapon out of the opponent\'s hands.";
 
                    sendgump = true;
 
                    break;
 
                case 90145:
                    if (m_Player.Skills.Stealth.Base < 100 )
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAssassin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.AdvancedStealth(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.AdvancedStealth);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90146:
                    title = "Advanced Stealth";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Stealth 100+";
                    usage = "[Stealth";
                    description = "Assassins are masters of the shadows, even while running at a full speed they can pick their way through the underbrush or darkened corners to remain undetected by the inattentive. ";
 
                    sendgump = true;
 
                    break;
 
                case 90147:
                    if (m_Player.RawInt < 50 || m_Player.RawDex < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsThief() && !m_Player.IsScoundrel())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.Locksmithing(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Locksmithing);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90148:
                    title = "Locksmithing";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Intelligence 50+, Dexterity 50+";
                    usage = "Automatic";
                    description = "You have spent considerable time learning the complex and intricate ways of locks. Taking them apart, picking them with various tools, you let no lock go untested. Due to this effort, you have started learning how to disable any lock you come across and eagerly await the next lock to challenge your skill. (You recieve 30 in skill and a set of lockpicking tools.)";
 
                    sendgump = true;
 
                    break;
 
                case 90149:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.AcrobaticCombat) || !m_Player.HasFeat(TeiravonMobile.Feats.Backstab) || !m_Player.HasFeat(TeiravonMobile.Feats.Disguise))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAssassin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ContractKilling);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90150:
                    title = "Contract Killing";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Backstab Feat, Disguise Feat, Acrobatic Combat Feat";
                    usage = "Automatic";
                    description = "This is the meat for the Assassins profession. With this Feat, the Assassin is granted access to the Assassin Contract System. It will reveal the contacts for a city that the Assassin needs to visit to gain new contracts and to receive payment for completed contracts. As the skill progresses, the Assassin gains new contacts one city at a time.";
 
                    sendgump = true;
 
                    break;
 
                case 90151:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAeromancer() && !m_Player.IsAquamancer() && !m_Player.IsGeomancer() && !m_Player.IsPyromancer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ImprovedFamiliar);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90152:
                    title = "Improved Familiar";
                    type = "Active";
                    progress = "One-Time";
                    prerequisites = "Level 10+";
                    usage = "[SummonFamiliar";
                    description = "This feat allows a mage the capacity to take on a powerful magical familiar.";
 
                    sendgump = true;
 
                    break;
 
                case 90153:
                    if (m_Player.PlayerLevel < 10 || m_Player.RawInt < 100)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAeromancer() && !m_Player.IsAquamancer() && !m_Player.IsGeomancer() && !m_Player.IsPyromancer()&& !m_Player.IsNecromancer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ArcaneEnchantment);
                        m_Player.RemainingFeats -= 1;
                        CreationOrb m_orb = new CreationOrb();
                        m_orb.UsesRemaining = 20;
                        m_Player.AddToBackpack(m_orb);
                    }
 
                    break;
 
                case 90154:
                    title = "Arcane Enchantment";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 10+, Intelligence 100+";
                    usage = "Automatic";
                    description = "The powers of the Arcane can be harnessed if the Mage is skilled enough and well-versed in the proper Lore. This Feat will grant access to the Magecraft system. As the Mage becomes more skilled in creating enchantments, the types of items the Mage can enchant will increase.";
 
                    sendgump = true;
 
                    break;
 
                case 90155:
                    if (m_Player.RawInt < 50)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsForester()&& !m_Player.IsMageSlayer()&& !m_Player.IsAeromancer() && !m_Player.IsAquamancer() && !m_Player.IsGeomancer() && !m_Player.IsPyromancer()&& !m_Player.IsNecromancer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.Inscription(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.Inscription);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90156:
                    title = "Inscription";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Intelligence 50+";
                    usage = "Automatic";
                    description = "During your years of research, you have learned to master the pen and quill, among other things. Because of this aptness, you can use your skills to work on other aspects. This Feat grants you access to the Inscription system. When this Feat is first learned, the Mage receives 30 skill points in the Inscription skill and a Scribe's Kit.";
 
                    sendgump = true;
 
                    break;
 
                case 90157:
                case 90158:
                    // Bookworm - DISABLED
                    break;
 
                case 90159:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if ( !m_Player.IsNecromancer()&& !m_Player.IsDeathKnight())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.SummonUndead);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90160:
                    title = "Summon Undead";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "[SummonUndead";
                    description = "You have learned how to create a strong link between the material plane and the netherworld. This allows you to summon undead minions to serve you.";
 
                    sendgump = true;
 
                    break;
 
                case 90161:
                    if (m_Player.RawInt < 100)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAeromancer() && !m_Player.IsAquamancer() && !m_Player.IsGeomancer() && !m_Player.IsPyromancer()&& !m_Player.IsNecromancer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ArcaneTransfer);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90162:
                    title = "Arcane Transfer";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Intelligence 100+";
                    usage = "[ArcaneTransfer";
                    description = "You have discovered the lost knowledge of arcane transference. This Feat allows you to recharge your Mana pool by drawing magical energy from charged items, such as wands. The amount of mana returned and the number of charges consumed in the item is based on the transfer skill level of the Mage. Unfortunately, the item will often loose all it's charges even though the mage does not intentionally completely drain it.";
 
                    sendgump = true;
 
                    break;
 
                case 90163:
                    if (m_Player.PlayerLevel < 10 || m_Player.Skills.SpiritSpeak.Base < 60)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if ( !m_Player.IsNecromancer()&& !m_Player.IsDeathKnight())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.NecroSpellbook);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90164:
                    title = "Advanced Necromancy";
                    type = "Active";
                    progress = "One-time";
                    prerequisites = "Level 10, Spirit Speak 60+";
                    usage = "Through the necromancer spellbook.";
                    description = "Years of traning in the art of Necromancy has granted you the ability to use advanced spells and dark incantations for your sinister purposes.";
 
                    sendgump = true;
 
                    break;
 
                case 90165:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.PlayerLevel > 1)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsAeromancer() && !m_Player.IsAquamancer() && !m_Player.IsGeomancer() && !m_Player.IsPyromancer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        Teiravon.Feats.Functions.RigorousTraining(m_Player);
 
                        m_Player.AddFeat(TeiravonMobile.Feats.RigorousTraining);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90166:
                    title = "Rigorous Training";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Must be used at Level 1";
                    usage = "Automatic";
                    description = "As a novice, you attacked your studies with extreme dedication and passion. You studied relentlessly and honed your abilities in your chosen element to the extent that you gained more exposure to your element than most other novices. As a result, you begin your adult life with a greater understanding of your chosen element.";
 
                    sendgump = true;
 
                    break;
 
                case 90167:
                    if (m_Player.RawInt < 75)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsShapeshifter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CreateInfusion);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90168:
                    title = "Create Infusion";
                    type = "Passive";
                    progress = "Usage";
                    prerequisites = "Intelligence 75+";
                    usage = "Automatic";
                    description = "This Feat grants the ability to store a divine spell within a specially prepared herb. This is a very useful Feat for Shapeshifters, as when in shifted form, they do not have the ability to use their hands to consume potions. Once an infused plant is prepared, it can be hidden away in places that the Druid can gain access to in creature form. Infusion herbs will not decay. As the Feat progresses, the types of infusions that can be created expand as well as the strength of the infusion.";
 
                    sendgump = true;
 
                    break;
 
                case 90169:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsForester() && !m_Player.IsShapeshifter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.WildShape);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90170:
                    title = "Wild Shape";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "None";
                    usage = "[Shapeshift";
                    description = "Wild Shape will grant access to additional forms, but the Shapeshifter must still meet the requirements for that form. As the skill progresses, the number of forms available will also increase. The forester version of this feat has less forms available than the shapeshifter feat. ";
 
                    sendgump = true;
 
                    break;
 
                case 90171:
                    if (m_Player.RawInt < 60)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsForester())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.NaturesEnchantment);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90172:
                    title = "Nature\'s Enchantment";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Intelligence 60+";
                    usage = "[druidcraft";
                    description = "The powers of Nature can be harnessed if the Druid is skilled enough and well-versed in the proper Lore. This Feat will grant access to the Druidcraft system. As the Druid becomes more skilled in creating enchantments, the types of items the Druid can enchant will increase.";
 
                    sendgump = true;
 
                    break;
 
                case 90173:
                    if (m_Player.RawInt < 40 || !m_Player.HasFeat(TeiravonMobile.Feats.WildShape))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsForester() && !m_Player.IsShapeshifter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Telepathy);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90174:
                    title = "Telepathy";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Wild Shape Feat, Intelligence 40+";
                    usage = "[Telepathy";
                    description = "Shapeshifters have the capacity to take on creature abilities in every form, including the unique ability to communicate with each other. This Feat allows Shifters to contact other Shapeshifters when they are in creature form regardless of where they are located. Other Shapeshifters must also have this Feat in order to communicate; otherwise the receiver can only receive the mental messages and not send them.";
 
                    sendgump = true;
 
                    break;
 
                case 90175:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsForester())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ResistEnergy);
                        m_Player.RemainingFeats -= 1;
 
                        m_Player.CheckResistanceBonus();
                    }
 
                    break;
 
                case 90176:
                    title = "Resist Energy";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Due to the constant exposure to the elements, your body has developed a unique ability to resist certain types of magic, which includes any energy based spell. (NOTE: This is a special resistance ability)";
 
                    sendgump = true;
 
                    break;
 
                case 90177:
                    if ((m_Player.Skills.Wrestling.Base < 80 || !m_Player.HasFeat(TeiravonMobile.Feats.WildShape)) && !m_Player.IsSavage())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsShapeshifter() && !m_Player.IsSavage())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Bite);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90178:
                    title = "Bite";
                    type = "Active";
                    progress = "Usage";
                    prerequisites = "Savage, Wild Shape Feat, or Wrestling 80+";
                    usage = "[Bite";
                    description = "Harness your inner savagery and sink you teeth into your enemy. Deals a portion of damage based on your strength and recovers hitpoints based on the damage dealt. Can also be used on recently deceased corpses to provide sustainance.";
 
                    sendgump = true;
 
                    break;
 
                case 90179:
                    if (m_Player.Skills.Wrestling.Base < 80 || !m_Player.HasFeat(TeiravonMobile.Feats.WildShape))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsShapeshifter() && !m_Player.IsUndead())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.PhysicalResistance);
                        m_Player.RemainingFeats -= 1;
 
                        m_Player.CheckResistanceBonus();
                    }
 
                    break;
 
                case 90180:
                    title = "Physical Resistance";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Wild Shape Feat, Wrestling 80+";
                    usage = "Automatic";
                    description = "Although some creature forms have an innate ability to resist damage due to their tough hides, this Feat grants that toughness for all of a Shapeshifter\'s forms that are available. This only applies when the Shapeshifter is in creature form. The strength of the resistance will increase as the Shapeshifter gains experience.";
 
                    sendgump = true;
 
                    break;
 
                case 90181:
                    if (m_Player.PlayerLevel < 15)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsTailor())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.AdvancedDying);
                        m_Player.RemainingFeats -= 1;
 
                        Teiravon.Feats.Functions.AdvancedDying(m_Player);
                    }
                    break;
 
                case 90182:
                    title = "Advanced Dying";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 15+";
                    usage = "Automatic";
                    description = "Although you can dye cloth in a myriad of available colors, this feat will introduce several special colors not normally available to you.";
 
                    sendgump = true;
 
                    break;
 
                case 90183:
                    if (m_Player.PlayerLevel < 15)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsTailor())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.LeatherDying);
                        m_Player.RemainingFeats -= 1;
 
                        Teiravon.Feats.Functions.LeatherDying(m_Player);
                    }
                    break;
 
                case 90184:
                    title = "Leather Dying";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 15+";
                    usage = "Automatic";
                    description = "Although you can dye cloth in a myriad of colors, this feat will allow you to dye your leather goods as well.";
 
                    sendgump = true;
 
                    break;
 
                case 90185:
                    if (m_Player.PlayerLevel < 15)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsWoodworker())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.FurnitureStaining);
                        m_Player.RemainingFeats -= 1;
 
                        Teiravon.Feats.Functions.FurnitureStaining(m_Player);
                    }
                    break;
 
                case 90186:
                    title = "Furniture Staining";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 15+";
                    usage = "Automatic";
                    description = "Although you can craft furniture in a selection of woods, this feat will allow you to stain your crafted goods in a myriad of colors.";
 
                    sendgump = true;
 
                    break;
 
                case 90187:
                    if (m_Player.PlayerLevel < 15)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsTinker())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ArmorEnameling);
                        m_Player.RemainingFeats -= 1;
 
                        Teiravon.Feats.Functions.ArmorEnameling(m_Player);
                    }
                    break;
 
                case 90188:
                    title = "Armor Enameling";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "Level 15+";
                    usage = "Automatic";
                    description = "Although you can craft your armor in various metals, this feat will allow you to enamel your armor and shields in various colors.";
 
                    sendgump = true;
 
                    break;
 
                case 90189:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAlchemist())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                        m_Player.SendGump(new Teiravon.Feats.ResearchGump(m_Player));
 
                    break;
 
                case 90190:
                    title = "Alchemical Research";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Spending many hours researching for alchmemical formulas, one can come across many valuable and sometime archaic tomes.";
 
                    sendgump = true;
 
                    break;
 
                case 90191:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDragoon() && !m_Player.IsBerserker())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.AxeFighter);
                        m_Player.Skills.Lumberjacking.Cap = 100.0;
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
 
                case 90192:
                    title = "Axe Fighter";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Long time battle training and experience allow the axe fighter greater lumberjack capability and earlier and easier axe weapon abilities.";
 
                    sendgump = true;
 
                    break;
 
                case 90193:
                    bool skillcheck = false;
 
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else
                    {
                        if (m_Player.IsBlacksmith() && m_Player.Skills.Blacksmith.Base >= 100.0)
                        {
                            skillcheck = true;
                            if (!m_Player.IsDwarf())
                                m_Player.SendMessage("Check the racial feat description again for hints to crafting racial alloys");
                        }
                        if (m_Player.IsTailor() && m_Player.Skills.Tailoring.Base >= 100.0)
                            skillcheck = true;
                        if (m_Player.IsTinker() && m_Player.Skills.Tinkering.Base >= 100.0)
                            skillcheck = true;
                        if (m_Player.IsCook() && m_Player.Skills.Cooking.Base >= 100.0)
                            skillcheck = true;
                        if (m_Player.IsWoodworker() && (m_Player.Skills.Carpentry.Base >= 100.0 || m_Player.Skills.Fletching.Base >= 100))
                            skillcheck = true;
                        if (m_Player.IsAlchemist() && m_Player.Skills.Alchemy.Base >= 100.0)
                            skillcheck = true;
 
                        //if (m_Player.IsWoodworker() && m_Player.Skills.Fletching.Base >= 100.0)
                        //    skillcheck = true;
 
                        if (skillcheck)
                        {
                            m_Player.AddFeat(TeiravonMobile.Feats.RacialCrafting);
                            m_Player.RemainingFeats -= 1;
                        }
                        else
                        {
                            m_Player.SendMessage("You must be a master of your craft to choose this feat.");
                        }
                    }
 
                    break;
 
                case 90194:
                    title = "Racial Crafting";
                    type = "Passive";
                    progress = "One-Time";
                    prerequisites = "100 in main craft skill";
                    usage = "Automatic";
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.RacialCrafting))
                        description = "Long hours spent training at your craft allow you to create wondrous items specific to race.";
                    else if (!m_Player.IsBlacksmith())
                        description = "Long hours spent training at your craft allow you to create wondrous items specific to race.";
                    else
                    {
                        if (m_Player.IsHuman())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Steel - the alloy of Humans<br>Step 1: Most precious metal<br>Step 2: Lightest colored wood<br>Step 3: Repeat step 1<br>Step 4: Not dull metal<br>Step 5: Earth<br>Step 6: Metallic Wood?";
                        }
                        else if (m_Player.IsOrc())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Bloodrock - the alloy of Orcs<br>Step 1: Cold & Energy weapon damage<br>Step 2: most precious wood<br>Step 3: Repeat step 1<br>Step 4: dark colored metal<br>Step 5: toothpick?<br>Step 6: Repeat step 3";
                        }
                        else if (m_Player.IsDrow())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Adamantite - the alloy of Drow<br>Step 1: dark metal<br>Step 2: dark wood<br>Step 3: rare metal 1<br>Step 4: repeat step 1<br>Step 5: Crystal<br>Step 6: repeat step 4";
                        }
                        else if (m_Player.IsElf())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Ithilmar - the alloy of Elves<br>Step 1: most common metal<br>Step 2: metallic wood?<br>Step 3: rarest metal<br>Step 4: repeat step 1<br>Step 5: Gem<br>Step 6: repeat step 2";
                        }
                    }
                    sendgump = true;
 
                    break;
 
                case 90195:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsTinker() && !m_Player.IsWoodworker()&& !m_Player.IsAlchemist()&& !m_Player.IsCook())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.SkilledGatherer);
                        m_Player.RemainingFeats -= 1;
 
                        Teiravon.Feats.Functions.SkilledGatherer(m_Player);
                    }
 
                    break;
 
                case 90196:
                    title = "Skilled Gatherer";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Through dedicated understanding of the uses for raw materials you become more adept at harvesting them.";
 
                    sendgump = true;
 
                    break;
 
                case 90197:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAeromancer() && !m_Player.IsGeomancer()&& !m_Player.IsPyromancer()&& !m_Player.IsAquamancer() && !m_Player.IsNecromancer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.MeditativeConcentration);
                        m_Player.RemainingFeats -= 1;
 
                        Teiravon.Feats.Functions.MeditativeConcentration(m_Player);
                    }
                    break;
 
                case 90198:
                    title = "Meditative Concentration";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Through a focused concentration on the arcane forces flowing through the world, you are able to enter a restorative trance";
 
                    sendgump = true;
 
                    break;
 
                case 90199:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMonk())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Flurry);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90200:
                    title = "Flurry of Blows";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Flurry";
                    description = "By precise understanding of their martial art the monk is able to rain a number of blows on their fow seemingly simultaniously.";
 
                    sendgump = true;
 
                    break;
 
                case 90201:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMonk())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.MindBodySoul);
                        m_Player.RemainingFeats -= 1;
                        Teiravon.Feats.Functions.MindBodySoul(m_Player);
                    }
                    break;
 
                case 90202:
                    title = "Mind Body and Soul";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "Level 10+";
                    usage = "Automatic";
                    description = "Through dedication and discipline the monk is able to train to perfect their Mind, Body, and Soul to their peak condition.";
                    sendgump = true;
 
                    break;
 
                case 90203:
                    if (m_Player.Skills.MagicResist.Base < 100 || m_Player.Skills.Meditation.Base < 100 || m_Player.Skills.Focus.Base < 100)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMonk())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.WholenessOfSelf);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
               
                case 90204:
                    title = "Wholeness of Self";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Meditation, Magic Resist, and Focus 100+";
                    usage = "Automatic";
                    description = "With absolute mastery of their Mind Body and Soul the Monk can perfect their physical and metaphysical self.";
                    sendgump = true;
 
                    break;
 
                case 90205:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMonk())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.KiStrike);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90206:
                    title = "Ki Strike";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "KiStrike";
                    description = "Through a deep understanding of the movement of life force within all living things the Monk is able to imbue their attacks with their own living essence.";
                    sendgump = true;
 
                    break;
 
                case 90207:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMonk())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BodyOfIron);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90208:
                    title = "Body of Iron";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "Through intense training the Monk's flesh and bone have toughened considerably, as they grow in experience they become even more impervious.";
                    sendgump = true;
 
                    break;
               
                case 90209:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMonk())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.LeapOfClouds);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90210:
                    title = "Leap of Clouds";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Level 10+";
                    usage = "Leap";
                    description = "A Monk's peak physical condition allows them to perform feats of acrobatics far beyond the hope of any untrained warrior. A monk is able to take to the air and leap across the landscape.";
                    sendgump = true;
                    break;
               
                case 90211:
                    if (m_Player.PlayerLevel < 5)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsBard())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.EnchantingMelody);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90212:
                    title = "Enchanting Melody";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Level 5+";
                    usage = "Melody";
                    description = "Allows the Bard to play an enchanting tune that those nearby cannot help but stop to listen to.";
                    sendgump = true;
                    break;
           
                case 90213:
                    if (m_Player.Skills.Provocation.Base < 100)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsBard())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CuttingWords);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90214:
                    title = "Cutting Words";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "Provocation 100";
                    usage = "Automatic";
                    description = "Through cunning, guile, and a mastery of provocation the bard is able to sow discord and strife through provocation even among his peers.";
                    sendgump = true;
                    break;
 
                case 90215:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsBard() && !m_Player.IsThief() && !m_Player.IsScoundrel() && !m_Player.IsRanger() && !m_Player.IsStrider())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.JackOfAll);
                        Teiravon.Feats.Functions.JackOfAll(m_Player);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90216:
                    title = "Jack of All Trades";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "Level 10";
                    usage = "Automatic";
                    description = "As a traveller of the world, you have become well versed in a variety of skills, Allows the use of Cartography, Forensics, Tracking, Lockpicking, and Trap Removal. Increases the Total Skill Cap to 1200.";
                    sendgump = true;
                    break;
 
                case 90217:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.WildShape) || !m_Player.HasFeat(TeiravonMobile.Feats.Bite) || m_Player.Skills.Stealth.Base < 60)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsShapeshifter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Pounce);
                        Teiravon.Feats.Functions.Pounce(m_Player);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90218:
                    title = "Pounce";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "Wildshape & Bite Feats, 60+ Stealth Skill";
                    usage = "Pounce";
                    description = "Tapping into the shapeshifter's primal nature allows them to stalk their pray with brutal efficiency, picking the perfect moment to lunge from the shadows and pounce.";
                    sendgump = true;
                    break;
               
                case 90219:
                    if (m_Player.Skills.Parry.Base < 80)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCavalier() && !m_Player.IsDwarvenDefender() && !m_Player.IsRavager())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Riposte);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90220:
                    title = "Riposte";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "80+ Parrying & Level 10+";
                    usage = "Automatic";
                    description = "As skilled and formally trained fencers, the cavalier is able to take advantage of a foe's clumsy attack to turn it back against him.";
                    sendgump = true;
                    break;
 
                case 90221:
                    if (m_Player.Skills.Archery.Base < 80 || m_Player.PlayerLevel < 10 )
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsArcher())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.EnchantedQuiver);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90222:
                    title = "Enchanted Quiver";
                    type = "Active";
                    progress = "None";
                    prerequisites = "80+ Archery, Level 10";
                    usage = "Quiver";
                    description = "Tapping into your mana reserves you have learned to place enchantments upon your arrows midflight.";
                    sendgump = true;
                    break;
 
                case 90223:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCavalier())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Challenge);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90224:
                    title = "Challenging Strike";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Challenge";
                    description = "A quick sharp strike to your enemy forces their attention at you, when used at range issues a Challenging Shout, bolstering your defences agains the target but dealing no damage.";
                    sendgump = true;
                    break;
 
                case 90225:
                    if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAssassin() && !m_Player.IsThief() && !m_Player.IsScoundrel() && !m_Player.IsBard())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Espionage);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90226:
                    title = "Espionage";
                    type = "Active";
                    progress = "None";
                    prerequisites = "Level 10";
                    usage = "Eavesdrop & Cipher";
                    description = "A cool head and a quick wit allows the rogue to understand the finer points of espionage, Eavesdropping at doors to hear what happens inside and deciphered hidden messages left in books and scrolls.";
                    sendgump = true;
                    break;
               
                case 90227:
                    if (m_Player.Skills.Healing.Base < 60.0)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCleric()&&!m_Player.IsDarkCleric())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.HealersOath);
                        Teiravon.Feats.Functions.HealersOath(m_Player);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90228:
                    title = "Healer's Oath";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "60 Healing Skill";
                    usage = "Healing and Ressurection";
                    description = "By dedicating themselves to the preservation of life, the cleric casts off the mantle of zealotous warrior and gains a profound talent for the preservation of life, gaining greater experience through the practice of their arts.";
                    sendgump = true;
                    break;
 
                case 90229:
                    if (!m_Player.HasFeat(TeiravonMobile.Feats.Backstab))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsAssassin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.GreivousWounds);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90230:
                    title = "Greivous Wounds";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Backstab";
                    usage = "Automatic";
                    description = "Turning their expertise to murderous aims, the Assassin uses their carefully planned backstab to inflict a greivous and mortal would, rendering their target unable to be healed for a time.";
                    sendgump = true;
                    break;
 
                case 90231:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsUndead())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
 
                        m_Player.AddFeat(TeiravonMobile.Feats.FeastoftheDamned);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90232:
                    title = "Feast of the Damned";
                    type = "Active";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Feast";
                    description = "Having forsaken the cycle of life and death, the undead may now feed upon the souls of the living, drawing shreds of their very essence from the corpses of the expired.";
                    sendgump = true;
                    break;
 
                case 90233:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsUndead())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BodyoftheGrave);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90234:
                    title = "Body of the Grave";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "None";
                    description = "The body of the undead is naught but cold rotting flesh, through this feat you are rendered invulnerable to petty mortal concerns such as hunger or poison.";
                    sendgump = true;
                    break;
 
                case 90235:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsUndead())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DarkRebirth);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90236:
                    title = "Dark Rebirth";
                    type = "Active";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Rebirth";
                    description = "Through this feat the undead defies the call of the void and returns their ghostly form back into the world through a suitable host body, only those bodies which have not been consumed are suitable.";
                    sendgump = true;
                    break;
 
                case 90237:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsUndead())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.SinisterForm);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;
 
                case 90238:
                    title = "Sinister Form";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "SinisterForm";
                    description = "By surrendering their soul to the demonic urges within, the undead may transform themselves into their true sinister undead form, confering advantages based on their class.";
                    sendgump = true;
                    break;
 
                case 90239:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsUndead())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.PhysicalResistance);
                        m_Player.RemainingFeats -= 1;
 
                        m_Player.CheckResistanceBonus();
                    }
 
                    break;
 
                case 90240:
                    title = "Physical Resistance";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "With life gone from your body it has grown callous and stiff, physical attacks are rendered far less effective against you than any mere mortal.";
 
                    sendgump = true;
 
                    break;
 
                case 90241:
                    if (m_Player.Skills.Stealing.Base < 60.0)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsGoblin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.StickyFingas);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
 
                case 90242:
                    title = "Sticky Fingas";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "60+ Stealing";
                    usage = "Automatic";
                    description = "Shifty and untrustworthy characters by nature, it's hard to actually tell the difference between a goblin who's robbing you and one who isn't. So accustom have you become to the act of thievery that it is now impossible to detect your attempts regardless of how successful you were.";
                    sendgump = true;
 
                    break;
 
                case 90243:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsGoblin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.LegIt);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
 
                case 90244:
                    title = "Leg it!";
                    type = "Active";
                    progress = "None";
                    prerequisites = "Level 5+";
                    usage = "]Dash";
                    description = "Cowardly creatures always the first to leave the fight, goblins are acustom to taking flight at great speeds and at a moment's notice. The use of this feat allows the user to suddenly and dramatically increase their running speed for a short time at the cost of their stamina.";
                    sendgump = true;
 
                    break;
 
                case 90245:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsGnome())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CharmedLife);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
 
                case 90246:
                    title = "Charmed Life";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "Natural tricksters, gnomes seem to live a charmed life as if the dice of fate are loaded in their favor. This feat grants a bonus to luck based on level.";
                    sendgump = true;
 
                    break;
 
                case 90247:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsGoblin())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ShoddyCrafts);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90248:
                    title = "Shoddy Craftsmanship";
                    type = "Passive";
                    progress = "none";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "Learning a crafting trade is a job which requires a great deal of dedication and time, but only of course, if you care abou the quality of your work. Lacking any sort of discipline about their craft, goblins are able to pick up the tricks of the trade at a vastly increased rate. As a consequence however, they suffer a drastic decline in their ability to make truely exceptional items.";
                    sendgump = true;
 
                    break;
 
                case 90249:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsGnome())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Meticulous);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90250:
                    title = "Meticulous";
                    type = "Passive";
                    progress = "none";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "Unable to simply cut corners in any work of craftsmanship, a gnome takes the time to admire the intricacies of its construction, as a result they find that crafting anything takes far longer than normal but results in far more truely exceptional items.";
                    sendgump = true;
 
                    break;
                case 90251:
                    if (m_Player.RemainingFeats < 1 || !m_Player.HasFeat(TeiravonMobile.Feats.WyrdSense))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Banish);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90252:
                    title = "Banishment";
                    type = "Active";
                    progress = "None";
                    prerequisites = "Wyrd Sense";
                    usage = "Banish";
                    description = "Through a concentrated effort to channel existing flows of the ether back on to themselves, the MageSlayer is capable of creating a powerfully disruptive force focused on any magical incarnation. The process leaves them drained and spent of their own mana but will have a similar effect on the target.";
                    sendgump = true;
 
                    break;
                case 90253:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DisruptingPresence);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90254:
                    title = "Disrupting Presence";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Disrupt";
                    description = "Like a whirlpool in the vast ocean of the ether the MageSlayer's presence can disrupt the flow of ether around him. Requiring any magic user unfortunate enough to be caught in it's pull to require greater time and magical effort to succeed in casting.";
                    sendgump = true;
 
                    break;
 
                case 90255:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsMageSlayer())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.WyrdSense);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90256:
                    title = "Wyrd Sense";
                    type = "Active";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Sense";
                    description = "Like ripples in a pond, the force of mana upon the ether leaves ripples in its wake. The Mageslayer is capable of detecting the flow of mana into the ether and detect the remaining mana reserves of any caster.";
                    sendgump = true;
 
                    break;
 
 
                case 90257:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.HasFeat(TeiravonMobile.Feats.TempestsWrath) || m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsForester())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.NaturesDefender);
                        Teiravon.Feats.Functions.NaturesDefender(m_Player);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90258:
                    title = "Nature\'s Defender";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "Level 10+ Cannot take Tempest\'s Wrath";
                    usage = "Automatic";
                    description = "The call of the Defender is strong, by taking this oath you gain great insight into the ways of combat, increasing your potential with Mace Fighting and Parrying, as well as hieghtening your Focus.";
                    sendgump = true;
 
                    break;
 
                case 90259:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.HasFeat(TeiravonMobile.Feats.NaturesDefender) || m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsForester())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.TempestsWrath);
                        Teiravon.Feats.Functions.TempestsWrath(m_Player);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90260:
                    title = "Tempest\'s Wrath";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "Level 10+ Cannot take Nature\'s Defender";
                    usage = "Automatic";
                    description = "The fury of the Tempest is awe inspiring, by taking this oath you gain insight into the ways of Evaluating Inteligence and an affinity for Meditation, as well as augmenting your storm spells.";
                    sendgump = true;
 
                    break;
 
                case 90261:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.PlayerLevel > 1)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsFrostling())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.NorthernResilience);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90262:
                    title = "Northern Resilience";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "Level 1";
                    usage = "Automatic";
                    description = "Blizzards, harsh wind, high altitudes, have made the Frostlings especially resilient to getting tired, and can huff around in the harshest environments for longer periods of time. Their stamina is without peer.";
                    sendgump = true;
 
                    break;
 
                case 90263:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFrostling())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.FrostlingHibernation);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90264:
                    title = "Frozen Hibernation";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "]Hibernate";
                    description = "By embracing their frozen nature, a frostling becomes encased in ice for a short time, afterwards they are reborn restored to their full strength. Frostlings become more and more adept at this feat as they grow in power.";
                    sendgump = true;
 
                    break;
 
                case 90265:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsFrostling())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ChilblainTouch);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90266:
                    title = "Chilblain Touch";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "The cold of the Frostlings runs deep in their veins, chilling their attackers to the bone and slowing their attacks.";
                    sendgump = true;
 
                    break;
 
 
                case 90267:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDwarvenDefender())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.ShieldMastery);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90268:
                    title = "Shield Mastery";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "With your mastery of the shield you are capable of defering incoming damage on to your shield to take the brunt of the blow depending on the strength of your shield and your arm.";
                    sendgump = true;
 
                    break;
                case 90269:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDwarvenDefender())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DefensiveStance);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90270:
                    title = "Defensive Stance";
                    type = "Active";
                    progress = "Level";
                    prerequisites = "None";
                    usage = "Defend";
                    description = "By rooting yourself to the ground with firm footing you become utterly imovable. With that rigidity you gain a significant bonus to your defense based upon your level.";
                    sendgump = true;
 
                    break;
 
                case 90271:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsDwarvenDefender() && !m_Player.IsRavager() && !m_Player.IsDragoon())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.UncannyDefense);
                        m_Player.RemainingFeats -= 1;
 
                    }
 
                    break;
                case 90272:
                    title = "Uncanny Defense";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Automatic";
                    description = "With an almost otherworldly sense of perception, you have eliminated any flaws in your defense. You are capable of deflecting an attack from any angle as if they stood directly infront of you.";
                    sendgump = true;
 
                    break;
 
                case 90273:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsElf())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Dodge);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
                case 90274:
                    title = "Dodge";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 10+";
                    usage = "Automatic";
                    description = "With catlike grace and agility a deadly blow will strike only air where once you stood. Your dexterity and stamina determine your success but every successive dodge they become more difficult and more tiring.";
                    sendgump = true;
 
                    break;
 
                case 90275:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.PlayerLevel < 5)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsThief() && !m_Player.IsScoundrel())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Scavenger);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
                case 90276:
                    title = "Scavenge";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 5+";
                    usage = "Automatic";
                    description = "A keen attention to detail is a theif's best asset, with this feat a thief is able to look over a corpse that has already been seemingly picked clean to perhaps find some additional unseen treasure.";
                    sendgump = true;
 
                    break;
 
                case 90277:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.PlayerLevel < 5)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (!m_Player.IsDragoon() && !m_Player.IsFighter() && !m_Player.IsBerserker())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.FuriousAssault);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
                case 90278:
                    title = "Furious Assault";
                    type = "Active";
                    progress = "None";
                    prerequisites = "Level 5+";
                    usage = "assault";
                    description = "By channelling your anger you push yourself to strike beyond the normal limits of your body's speed, so long as your endurence holds...";
                    sendgump = true;
 
                    break;
               
                case 90279:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsArcher() || m_Player.Skills.Stealth.Value >= 80.0)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Camouflage);
                        Teiravon.Feats.Functions.Camouflage(m_Player);
                        m_Player.RemainingFeats -= 1;
                    }
 
                    break;
                case 90280:
                    title = "Camouflage";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You learn the great art of concealment. Stealth cap 80 and gain 30 stealth.";
                    sendgump = true;
 
                    break;

                case 90281:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.Skills.Tracking.Base < 75 || (!m_Player.IsRanger() && !m_Player.IsArcher() && !m_Player.IsMageSlayer() &&!m_Player.IsStrider()))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.StalkingPrey);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 90282:
                    title = "Hunter Stalks His Prey";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Tracking > 75";
                    usage = "Passive";
                    description = "Your keen eye for tracking lends you the ability to pick out flaws in your quarry's defenses, reducing the effective resistances of the target to your attacks while you're tracking them.";
                    sendgump = true;

                    break;

                case 90283:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.PlayerLevel < 10 || (!m_Player.IsRanger() &&!m_Player.IsStrider()))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BigGameHunter);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 90284:
                    title = "Big Game Hunter";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 10+";
                    usage = "Passive";
                    description = "The bigger they are, the harder they fall. Specializing in hunting exceptionally powerful monsters has given you an edge in maximizing the damage of your attacks against them. The bigger they are, the harder they'll fall.";
                    sendgump = true;

                    break;

                case 90285:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (m_Player.PlayerLevel < 10 || (!m_Player.IsCavalier() && !m_Player.IsFighter() && !m_Player.IsSavage() && !m_Player.IsDwarvenDefender()))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BattleHardened);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 90286:
                    title = "Battle Hardened";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "Level 10+";
                    usage = "Passive";
                    description = "A little pain reminds you you're alive. The lower your hitpoints get, the harder it gets to hurt you. You gain a damage mitigation based upon your level for each % of HP Missing.";
                    sendgump = true;
                    break;
                case 90287:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsScoundrel())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.RottenLuck);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 90288:
                    title = "Rotten Luck";
                    type = "Passive";
                    progress = "Luck";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "Misery loves company. Delight in your own misfortune by spreading it to others. Whenever you miss in combat apply a hitchance debuff onto your target based on how likely it was to hit. Additionally gain 10% of your luck as bonus damage%. Luck now caps at 500.";
                    sendgump = true;

                    break;

                case 90289:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsScoundrel() || m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.DirtyTricks);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 90290:
                    title = "Dirty Tricks";
                    type = "Active";
                    progress = "None";
                    prerequisites = "Level 10+";
                    usage = "Tricks";
                    description = "Every scoundrel knows to always have a backup plan. It helps of course to have a suite of devious dirty tricks in your arsenel incase that backup falls through.";
                    sendgump = true;

                    break;
                case 90291:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if ((!m_Player.IsScoundrel() && !m_Player.IsRavager()) || (m_Player.Skills.Fencing.Base < 100.0 && m_Player.Skills.Swords.Base < 100.0))
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CunningFlourish);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 90292:
                    title = "Cunning Flourish";
                    type = "Active";
                    progress = "None";
                    prerequisites = "100+ fencing or 100+ Swords";
                    usage = "Flourish";
                    description = "As the paragons of trickery, the scoundrel is well versed in the various tricks one can acomplish with a weapon, so well versed infact that they're capable of storing a weapon ability from one wepaon to deploy again on another.";
                    sendgump = true;

                    break;
                case 1000:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.BlacksmithTraining);
                        Teiravon.Feats.Functions.MerchantCraft(m_Player, Server.Teiravon.Feats.Crafts.Blacksmith);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1001:
                    title = "Blacksmith Training";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You've taken an interest in Blacksmithing, this is one of many trades a Merchant can specialize in but be warned, each specialization feat will hinder your ability to gain skill in any of these specialized crafts.";
                    sendgump = true;

                    break;
                case 1002:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CarpenterTraining);
                        Teiravon.Feats.Functions.MerchantCraft(m_Player, Server.Teiravon.Feats.Crafts.Carpenter);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1003:
                    title = "Carpentry Training";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You've taken an interest in Carpentry, this is one of many trades a Merchant can specialize in but be warned, each specialization feat will hinder your ability to gain skill in any of these specialized crafts.";
                    sendgump = true;

                    break;

                case 1004:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.TinkerTraining);
                        Teiravon.Feats.Functions.MerchantCraft(m_Player, Server.Teiravon.Feats.Crafts.Tinker);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1005:
                    title = "Tinker Training";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You've taken an interest in Tinkering, this is one of many trades a Merchant can specialize in but be warned, each specialization feat will hinder your ability to gain skill in any of these specialized crafts.";
                    sendgump = true;

                    break;

                case 1006:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.TailorTraining);
                        Teiravon.Feats.Functions.MerchantCraft(m_Player, Server.Teiravon.Feats.Crafts.Tailor);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1007:
                    title = "Tailor Training";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You've taken an interest in Tailoring, this is one of many trades a Merchant can specialize in but be warned, each specialization feat will hinder your ability to gain skill in any of these specialized crafts.";
                    sendgump = true;

                    break;

                case 1008:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.FletcherTraining);
                        Teiravon.Feats.Functions.MerchantCraft(m_Player, Server.Teiravon.Feats.Crafts.Fletcher);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1009:
                    title = "Fletcher Training";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You've taken an interest in Bowcraft and Fletching, this is one of many trades a Merchant can specialize in but be warned, each specialization feat will hinder your ability to gain skill in any of these specialized crafts.";
                    sendgump = true;

                    break;

                case 1010:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CookTraining);
                        Teiravon.Feats.Functions.MerchantCraft(m_Player, Server.Teiravon.Feats.Crafts.Cook);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1011:
                    title = "Cook Training";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You've taken an interest in Cooking, this is one of many trades a Merchant can specialize in but be warned, each specialization feat will hinder your ability to gain skill in any of these specialized crafts.";
                    sendgump = true;

                    break;

                case 80219:
                    if (m_Player.Skills.Poisoning.Base < 50 || m_Player.PlayerLevel < 10)
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if ( !m_Player.IsRavager())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.Riposte);
                        m_Player.RemainingFeats -= 1;
                    }
                    break;

                case 80220:
                    title = "Venomous Riposte";
                    type = "Passive";
                    progress = "Level";
                    prerequisites = "50+ Poisoning & Level 10+";
                    usage = "Automatic";
                    description = "As underhanded and cunning denizens of the underdark, the Ravager able to take advantage of a foe's clumsy attack to attack their vulnerable arm with poison.";
                    sendgump = true;
                    break;

                case 1012:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.CombatTraining);
                        Teiravon.Feats.Functions.CombatTraining(m_Player);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1013:
                    title = "Combat Training";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "None";
                    usage = "Passive";
                    description = "You've taken an interest in Combat, a wise decision in an unsafe and uncertain world, with this you'll learn the basics in all the major combat forms, though no mastery in any. It is not a specilization feat and has no impact on skill gain.";
                    sendgump = true;

                    break;

                case 1014:
                    if (m_Player.RemainingFeats < 1)
                        m_Player.SendMessage(Teiravon.Messages.NoFeatSlots);
                    else if (!m_Player.IsCrafter())
                        m_Player.SendMessage(Teiravon.Messages.NoReqs);
                    else
                    {
                        m_Player.AddFeat(TeiravonMobile.Feats.MasterCraftsman);
                        Teiravon.Feats.Functions.MasterCraftsman(m_Player);
                        m_Player.RemainingFeats -= 1;
                    }

                    break;
                case 1015:
                    title = "Master Craftsman";
                    type = "Passive";
                    progress = "None";
                    prerequisites = "A Craft Specialization Feat";
                    usage = "Passive";
                    description = "Take the time to perfect those crafts you've specialized in. A Mastercraftsman is able to go beyond the natural limits of merely specializing in a craft and will truely master it completely. Further this will unlock the secret traditional crafts handed down by artisans of your race.";
                    
                     if(m_Player.HasFeat(TeiravonMobile.Feats.BlacksmithTraining))
                     {
                        if (m_Player.IsHuman())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Steel - the alloy of Humans<br>Step 1: Most precious metal<br>Step 2: Lightest colored wood<br>Step 3: Repeat step 1<br>Step 4: Not dull metal<br>Step 5: Earth<br>Step 6: Metallic Wood?";
                        }
                        else if (m_Player.IsOrc())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Bloodrock - the alloy of Orcs<br>Step 1: Cold & Energy weapon damage<br>Step 2: most precious wood<br>Step 3: Repeat step 1<br>Step 4: dark colored metal<br>Step 5: toothpick?<br>Step 6: Repeat step 3";
                        }
                        else if (m_Player.IsDrow())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Adamantite - the alloy of Drow<br>Step 1: dark metal<br>Step 2: dark wood<br>Step 3: rare metal 1<br>Step 4: repeat step 1<br>Step 5: Crystal<br>Step 6: repeat step 4";
                        }
                        else if (m_Player.IsElf())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Ithilmar - the alloy of Elves<br>Step 1: most common metal<br>Step 2: metallic wood?<br>Step 3: rarest metal<br>Step 4: repeat step 1<br>Step 5: Gem<br>Step 6: repeat step 2";
                        }
                         else if (m_Player.IsGnome())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Electrum - the alloy of Gnomes<br>Step 1: some shiney metal<br>Step 2: an even shinier sort of metal?<br>Step 3:  a quite valuable gem<br>Step 4: repeat step 1<br>Step 5: repeat step 2<br>Step 6: just a bit more step 1";
                        }
                        else if (m_Player.IsGoblin())
                        {
                            description = "Alloys can be crafted using the ]alloy command<br>Skazz - the alloy of Goblins<br>Step 1: I think it's a metal? <br> Steps 2-5: ???? <br> Step 6: try your best not to spit in it.";
                        }
                    }
                    sendgump = true;

                    break;
 
            }
 
 
                        if ( sendgump )
                                m_Player.SendGump( new FeatDescGump( title, type, progress, prerequisites, usage, description, m_Player ) );
 
                }
        }
 
        public class FeatDescGump : Gump
        {
                public FeatDescGump( string title, string type, string progress, string prerequisites, string usage, string description, TeiravonMobile m_Player ) : base( 75, 92 )
                {
                        m_Player.CloseGump( typeof(FeatDescGump) );
 
                        AddPage( 0 );
 
                        AddBackground( 70, 80, 505, 360, 3600 );
                        //AddBackground( 95, 125, 455, 290, 9350 );
                        AddLabel( 255, 100, 150, title );
 
                        AddLabel( 115, 135, 150, "Feat Type: " + type );
                        AddLabel( 320, 135, 150, "Progress Type: " + progress );
                        AddLabel( 115, 160, 150, "Prerequisites: " + prerequisites );
                        AddLabel( 115, 185, 150, "Usage: " + usage );
                        AddLabel( 115, 210, 150, "Description: " );
                        AddHtml( 130, 235, 350, 200, "<basefont size=\"8\" color=\"#ffffff\">" + description + "</basefont>", false, false );
 
                        AddButton( 515, 388, 4014, 4016, 1, GumpButtonType.Reply, 0 );
                }
 
                public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
                {
                        TeiravonMobile m_Player = (TeiravonMobile) sender.Mobile;
 
                        m_Player.SendGump( new FeatsGump( m_Player ) );
                }
        }
}