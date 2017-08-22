using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Spells;
using MoveImpl = Server.Movement.MovementImpl;
using Server.Engines.XmlSpawner2;
using Knives.Utils;

namespace Server.Teiravon
{
    public class MerchantGump : Gump
    {
        public MerchantGump(Mobile owner)
            : base(300, 200)
        {
            string s_Help = " AUTO - Automatically be converted into a Merchant at your current skill and level. <br> REROLL - Revert to a level 1 state to start fresh as a new merchant, retaining your items but not your exp or skills. <BR> CANCEL - You'd rather stay as you are and not be bothered by this merchant stuff.<br><br>     The Merchant class is a new form of hybridized 'Every-Crafter'.<br> By use of feats you can select any combination of disciplines on which to focus your crafting in the following Fields;<br> 'Cooking' <br> 'Blacksmithing' <br> 'Tailoring' <br> 'Fletching' <br> 'Carpentry'<br> and 'Tinkering'.<br> Each time you take one of such training feats you diminish your skill gain rate in those skills making it more and more difficult to train a succession of subsequent craft skills. <br>Be advised, the 'MasterCraftsman' feat will permit you to excede the 100 skill cap in every skill you've currently taken a skill training feat in, however once taken no future skill training feat taken will benefit from this bonus. ";


            AddPage(0);

            AddBackground(0, 0, 400, 350, 2600);

            AddHtml(0, 20, 400, 35, "<center>Merchant Conversion Gump</center>", false, false); // <center>Resurrection</center>

            AddHtml(50, 55, 300, 240, s_Help, true, true); /* It is possible for you to be resurrected here by this healer. Do you wish to try?<br>
																				   * CONTINUE - You chose to try to come back to life now.<br>
																				   * CANCEL - You prefer to remain a ghost for now.
																				   */
            int x = 45;
            int y = 315;
            AddButton(x +200, y -3, 4005, 4007, 0, GumpButtonType.Reply, 0);
            AddHtml(x + 235, y, 110, 35,"CANCEL", false, false); // CANCEL

            AddButton(x + 20, y - 3, 4005, 4007, 1, GumpButtonType.Reply, 0);
            AddHtml(x+55, y, 110, 35, "AUTO", false, false); // AUTO

            AddButton(x + 100, y - 3, 4005, 4007, 2, GumpButtonType.Reply, 0);
            AddHtml(x + 135, y, 110, 35, "REROLL", false, false); // REROLL
        }


        private void ToMerchant(TeiravonMobile from)
        {
            TeiravonMobile m = from;
            TeiravonMobile.Feats[] m_Feats = from.GetFeats();

            for (int i = 0; i < m_Feats.Length; i++)
            {
                if (!IsSkillFeat(m_Feats[i]))
                {
                    if (m_Feats[i] != TeiravonMobile.Feats.None)
                        from.RemainingFeats++;

                    m_Feats[i] = TeiravonMobile.Feats.None;
                    
                }
            }

            m.Skills.Focus.Cap = 0.0;
            m.Skills.Swords.Cap = 40.0;
            m.Skills.Macing.Cap = 40.0;
            if (m.IsOrc())
                m.Skills.Macing.Cap = 90.0;
            m.Skills.Fencing.Cap = 40.0;
            m.Skills.Archery.Cap = 20.0;
            m.Skills.Wrestling.Cap = 40.0;
            m.Skills.Tactics.Cap = 40.0;
            m.Skills.Camping.Cap = 100.0;
            m.Skills.Cartography.Cap = 80.0;
            m.Skills.Cooking.Cap = 40.0;
            m.Skills.Tailoring.Cap = 40.0;
            m.Skills.Tinkering.Cap = 40.0;
            m.Skills.MagicResist.Cap = 100.0;

            if (m.IsGnome())
            {
                m.Skills.Tinkering.Cap = 80.0;
                m.Skills.MagicResist.Cap = 120;
            }
 
            m.Skills.Begging.Cap = 100.0;
            m.Skills.Blacksmith.Cap = 40.0;
            m.Skills.Carpentry.Cap = 40.0;
            m.Skills.Fletching.Cap = 40.0;
            m.Skills.Fishing.Cap = 100.0;
            m.Skills.Healing.Cap = 60.0;
            m.Skills.ItemID.Cap = 80.0;
            m.Skills.Lumberjacking.Cap = 100.0;
            m.Skills.Mining.Cap = 100.0;
            
            if (m.IsDwarf())
                m.Skills.Mining.Cap = 120.0;

            m.Skills.TasteID.Cap = 100.0;

            if (from.Skills.Blacksmith.Base > 40)
            {
                from.AddFeat(TeiravonMobile.Feats.BlacksmithTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Blacksmith);
                from.RemainingFeats--;
            }
            if (from.Skills.Tinkering.Base > 40)
            {
                from.AddFeat(TeiravonMobile.Feats.TinkerTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Tinker);
                from.RemainingFeats--;
            }
            if (from.Skills.Carpentry.Base > 40)
            {
                from.AddFeat(TeiravonMobile.Feats.CarpenterTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Carpenter);
                from.RemainingFeats--;
            }
            if (from.Skills.Cooking.Base > 40)
            {
                from.AddFeat(TeiravonMobile.Feats.CookTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Cook);
                from.RemainingFeats--;
            }
            if (from.Skills.Fletching.Base > 40)
            {
                from.AddFeat(TeiravonMobile.Feats.FletcherTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Fletcher);
                from.RemainingFeats--;
            }
            if (from.Skills.Tailoring.Base > 40)
            {
                from.AddFeat(TeiravonMobile.Feats.TailorTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Tailor);
                from.RemainingFeats--;
            }
            if (from.Skills.Blacksmith.Value > 100 || from.Skills.Tinkering.Value > 100 || from.Skills.Carpentry.Value > 100 || from.Skills.Cooking.Value > 100 || from.Skills.Fletching.Value > 100 || from.Skills.Tailoring.Value > 100)
            {
                from.AddFeat(TeiravonMobile.Feats.MasterCraftsman);
                Feats.Functions.MasterCraftsman(from);
                from.RemainingFeats--;
            }
            if (from.Skills.Swords.Base > 60 || from.Skills.Macing.Base > 60 || from.Skills.Fencing.Base > 60 || from.Skills.Archery.Base > 60)
            {
                from.AddFeat(TeiravonMobile.Feats.CombatTraining);
                Feats.Functions.CombatTraining(from);
                from.RemainingFeats--;
            }

            from.PlayerClass = TeiravonMobile.Class.Merchant;
        }

        private bool IsSkillFeat(TeiravonMobile.Feats feat)
        {
            if (feat == TeiravonMobile.Feats.WeaponSpecialization || feat == TeiravonMobile.Feats.Apprenticeship || feat == TeiravonMobile.Feats.Blademaster || feat == TeiravonMobile.Feats.ExpertMining)
                return true;

            return false;
        }

        private void Reroll(TeiravonMobile from)
        {
            TeiravonMobile m = from;

            TeiravonMobile.Feats[] m_Feats = from.GetFeats();

            for (int i = 0; i < m_Feats.Length; i++)
            {
                if (!IsSkillFeat(m_Feats[i]))
                {
                    m_Feats[i] = TeiravonMobile.Feats.None;
                }
            }

            for (int i = 0; i < m.m_ActiveFeats.Length; i++)
            {
                m.m_ActiveFeats[i] = TeiravonMobile.Feats.None;
            }

            for (int x = 0; x < m.Skills.Length; ++x)
                m.Skills[x].Base = 0.0;

            m.PlayerExp = 0;
            m.PlayerLevel = 1;
            m.PerkPoints = 1;
		    m.RidingSkill = 0;
		    m.FarmingSkill = 0;
		    m.TeachingSkill = 0;
            m.RemainingFeats = 1;
            m.ExpertMining = 0;
            m.ExpertSkinning = 0;
            m.ExpertWoodsman = 0;
            m.LanguageDrowSkill = 0;
            m.LanguageDwarvenSkill = 0;
            m.LanguageElvenSkill = 0;
            m.LanguageLupineSkill = 0;
            m.LanguageOrcSkill = 0;
            m.SkillsCap = 10000;

            m.SetLanguages(false);
            m.SetLanguages(0x1, true);
            
            if (m.IsHuman())
                m.SkillsCap = 11000;

            m.RawStr = 10;
            m.Hits = m.Str;
            m.MaxHits = 10;

            m.RawInt = 10;
            m.Mana = m.Int;
            m.MaxMana = 10;

            m.RawDex = 10;
            m.Stam = m.Dex;
            m.MaxStam = 10;

            if (m.IsHuman() || m.IsUndead())
            {
                m.RawStr = Utility.RandomMinMax(20, 30);
                m.RawDex = Utility.RandomMinMax(20, 30);
                m.RawInt = Utility.RandomMinMax(20, 30);

                m.MaxHits = Utility.RandomMinMax(40, 50);
                m.MaxStam = Utility.RandomMinMax(40, 50);
                m.MaxMana = Utility.RandomMinMax(40, 50);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;

            }
            else if (m.IsElf())
            {
                m.LanguageElven = true;

                m.RawStr = Utility.RandomMinMax(20, 30);
                m.RawDex = Utility.RandomMinMax(30, 40);
                m.RawInt = Utility.RandomMinMax(25, 35);

                m.MaxHits = Utility.RandomMinMax(30, 40);
                m.MaxStam = Utility.RandomMinMax(50, 60);
                m.MaxMana = Utility.RandomMinMax(45, 55);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;

            }
            else if (m.IsDrow())
            {

                m.LanguageDrow = true;

                m.RawStr = Utility.RandomMinMax(20, 30);
                m.RawDex = Utility.RandomMinMax(30, 40);
                m.RawInt = Utility.RandomMinMax(25, 35);

                m.MaxHits = Utility.RandomMinMax(30, 40);
                m.MaxStam = Utility.RandomMinMax(50, 60);
                m.MaxMana = Utility.RandomMinMax(45, 55);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;

            }
            else if (m.IsDwarf())
            {
                m.LanguageDwarven = true;

                m.RawStr = Utility.RandomMinMax(25, 35);
                m.RawDex = Utility.RandomMinMax(10, 20);
                m.RawInt = Utility.RandomMinMax(15, 25);

                m.MaxHits = Utility.RandomMinMax(50, 60);
                m.MaxStam = Utility.RandomMinMax(35, 45);
                m.MaxMana = Utility.RandomMinMax(30, 40);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;

            }
            else if (m.IsOrc())
            {
                m.LanguageOrc = true;

                m.RawStr = Utility.RandomMinMax(30, 40);
                m.RawDex = Utility.RandomMinMax(15, 25);
                m.RawInt = Utility.RandomMinMax(10, 20);

                m.MaxHits = Utility.RandomMinMax(50, 60);
                m.MaxStam = Utility.RandomMinMax(40, 50);
                m.MaxMana = Utility.RandomMinMax(30, 40);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;

            }
            else if (m.IsGnome())
            {

                m.LanguageDwarven = true;

                m.RawStr = Utility.RandomMinMax(20, 30);
                m.RawDex = Utility.RandomMinMax(35, 45);
                m.RawInt = Utility.RandomMinMax(30, 40);

                m.MaxHits = Utility.RandomMinMax(30, 40);
                m.MaxStam = Utility.RandomMinMax(40, 50);
                m.MaxMana = Utility.RandomMinMax(50, 60);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;
            }

            else if (m.IsGoblin())
            {
                m.LanguageOrc = true;

                m.RawStr = Utility.RandomMinMax(20, 30);
                m.RawDex = Utility.RandomMinMax(35, 45);
                m.RawInt = Utility.RandomMinMax(30, 40);

                m.MaxHits = Utility.RandomMinMax(30, 40);
                m.MaxStam = Utility.RandomMinMax(40, 50);
                m.MaxMana = Utility.RandomMinMax(50, 60);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;
            }
            else if (m.IsFrostling())
            {
                m.RawStr = Utility.RandomMinMax(25, 35);
                m.RawDex = Utility.RandomMinMax(15, 25);
                m.RawInt = Utility.RandomMinMax(25, 35);

                m.MaxHits = Utility.RandomMinMax(45, 55);
                m.MaxStam = Utility.RandomMinMax(40, 50);
                m.MaxMana = Utility.RandomMinMax(40, 50);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;
            }
            else if (m.IsHalfOrc())
            {
                m.LanguageOrc = true;

                m.RawStr = Utility.RandomMinMax(25, 35);
                m.RawDex = Utility.RandomMinMax(20, 30);
                m.RawInt = Utility.RandomMinMax(15, 25);

                m.MaxHits = Utility.RandomMinMax(45, 55);
                m.MaxStam = Utility.RandomMinMax(40, 50);
                m.MaxMana = Utility.RandomMinMax(35, 45);

                m.Hits = m.MaxHits;
                m.Stam = m.MaxStam;
                m.Mana = m.MaxMana;
            }

            m.PlayerClass = TeiravonMobile.Class.Merchant;
            BankBox m_Bank = m.BankBox;
            
            m.Skills.Focus.Cap = 0.0;
            m.Skills.Focus.Base = 0.0;

            m_Bank.DropItem(new MerchantBag());

            m.Skills.Swords.Base = 5.0;
            m.Skills.Swords.Cap = 40.0;

            m.Skills.Macing.Base = 5.0;
            m.Skills.Macing.Cap = 40.0;

            m.Skills.Fencing.Base = 5.0;
            m.Skills.Fencing.Cap = 40.0;

            m.Skills.Archery.Base = 5.0;
            m.Skills.Archery.Cap = 20.0;

            m.Skills.Wrestling.Base = 5.0;
            m.Skills.Wrestling.Cap = 40.0;

            m.Skills.Tactics.Base = 5.0;
            m.Skills.Tactics.Cap = 40.0;

            m.Skills.Camping.Base = 30.0;
            m.Skills.Camping.Cap = 100.0;

            m.Skills.Cartography.Base = 20.0;
            m.Skills.Cartography.Cap = 80.0;

            m.Skills.Cooking.Base = 10.0;
            m.Skills.Cooking.Cap = 40.0;

            m.Skills.Tailoring.Base = 10.0;
            m.Skills.Tailoring.Cap = 40.0;
            if (m.IsGnome())
            {
                m.Skills.Tinkering.Base = 30.0;
                m.Skills.Tinkering.Cap = 80.0;
            }
            else
            {
                m.Skills.Tinkering.Base = 10.0;
                m.Skills.Tinkering.Cap = 40.0;
            }

            m.Skills.Begging.Cap = 100.0;
            m.Skills.Begging.Base = 40.0;

            m.Skills.Blacksmith.Base = 10.0;
            m.Skills.Blacksmith.Cap = 40.0;

            m.Skills.Carpentry.Base = 10.0;
            m.Skills.Carpentry.Cap = 40.0;

            m.Skills.Fletching.Base = 10.0;
            m.Skills.Fletching.Cap = 40.0;

            m.Skills.Fishing.Base = 30.0;
            m.Skills.Fishing.Cap = 100.0;

            m.Skills.Healing.Base = 10.0;
            m.Skills.Healing.Cap = 60.0;

            m.Skills.ItemID.Base = 20.0;
            m.Skills.ItemID.Cap = 80.0;

            m.Skills.Lumberjacking.Base = 30.0;
            m.Skills.Lumberjacking.Cap = 100.0;

            m.Skills.Mining.Base = 30.0;
            if (m.IsDwarf())
                m.Skills.Mining.Cap = 120.0;
            else
                m.Skills.Mining.Cap = 100.0;

            m.Skills.MagicResist.Base = 30.0;
            m.Skills.MagicResist.Cap = 100.0;

            m.Skills.TasteID.Base = 30.0;
            m.Skills.TasteID.Cap = 100.0;

            Teiravon.Feats.Functions.CheckSkills(m);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            from.CloseGump(typeof(ResurrectGump));

            if (info.ButtonID == 1)
            {
                from.SendMessage("You have selected to be automatically converted into a Merchant.");
                ToMerchant((TeiravonMobile)from);
            }
            else if (info.ButtonID == 2)
            {
                from.SendMessage("You have selected to be rerolled back to a level 1 Merchant.");
                Reroll((TeiravonMobile)from);
            }
            else if (info.ButtonID == 0)
            {
                from.SendMessage("You would rather remain as you are.");
                ((PlayerMobile)from).NpcGuild = NpcGuild.MerchantsGuild;
            }
        }
    }
    public class MerchantConversionGump : GumpPlus
    {
        private static string s_Help = "     The Merchant class is a new form of hybridized 'Every-Crafter'. By use of feats you can select any combination of disciplines on which to focus your crafting in the following Fields; 'Cooking' , 'Blacksmithing' , 'Tailoring' , 'Fletching' , 'Carpentry' , and 'Tinkering'. Each time you take one of such training feats you diminish your skill gain rate in those skills making it more and more difficult to train a succession of subsequent craft skills. Be advised, the 'MasterCraftsman' feat will permit you to excede the 100 skill cap in every skill you've currently taken a skill training feat in, however once taken no future skill training feat taken will benefit from this bonus. ";

        public static void SendTo(Mobile m)
        {
            new ErrorsGump(m);
            Errors.Checked.Add(m);
        }

        private const int Width = 400;
        private const int Height = 340;

        public MerchantConversionGump(Mobile m)
            : base(m, 100, 100)
        {
            m.CloseGump(typeof(ErrorsGump));

            NewGump();
        }

        protected override void BuildGump()
        {
            try
            {

                AddBackground(0, 0, Width, Height, 0xE10);

                int y = 0;
                
                AddHtml(0, y += 15, Width, 45, HTML.White + "<CENTER>Merchant Conversion", false, false);
                AddBackground(30, y += 20, Width - 60, 3, 0x13BE);

                AddHtml(20, y += 20, Width - 40, Height - y - 50, s_Help, true, true);

                    AddButton( 30, Height - 40, 0x98B, 0x98B, "Option A", new TimerCallback(PickConversion));
                    AddHtml( 23, Height - 37, 10, 20, HTML.White + "<CENTER>AUTOMATIC CONVERSION", false, false);

                    AddButton(Width / 2 + 50, Height - 40, 0x98B, 0x98B, "Option B", new TimerCallback(LeaveAlone));
                    AddHtml(Width / 2 + 53, Height - 37, 100, 20, HTML.White + "<CENTER>STAY AS YOU ARE", false, false);

            }
            catch { Errors.Report(String.Format("MerchantGump-> BuildGump-> |{0}|", Owner)); }
        }

        private void Help()
        {
            NewGump();
            InfoGump.SendTo(Owner, 300, 300, HTML.White + s_Help, true);
        }

        private void PickConversion()
            {
                Owner.SendMessage("You have selected to be automatically converted into a Merchant.");
                ToMerchant((TeiravonMobile)Owner);
            }

        private void LeaveAlone()
        {
            Owner.SendMessage("You have opted to remain as you are.");
            ((PlayerMobile)Owner).NpcGuild = NpcGuild.MerchantsGuild;
        }

        #region Conversion



        private void ToMerchant(TeiravonMobile from)
        {
            TeiravonMobile.Feats[] m_Feats = from.GetFeats();

            for (int i = 0; i < m_Feats.Length; i++)
            {
                if (!IsSkillFeat(m_Feats[i]))
                {
                    m_Feats[i] = TeiravonMobile.Feats.None;
                    from.RemainingFeats++;
                }
            }
            if (from.Skills.Blacksmith.Cap > 90)
            {
                from.AddFeat(TeiravonMobile.Feats.BlacksmithTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Blacksmith);
                from.RemainingFeats--;
            }
            if (from.Skills.Tinkering.Cap > 90)
            {
                from.AddFeat(TeiravonMobile.Feats.TinkerTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Tinker);
                from.RemainingFeats--;
            }
            if (from.Skills.Carpentry.Cap > 90)
            {
                from.AddFeat(TeiravonMobile.Feats.CarpenterTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Carpenter);
                from.RemainingFeats--;
            }
            if (from.Skills.Cooking.Value > 90)
            {
                from.AddFeat(TeiravonMobile.Feats.CookTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Cook);
                from.RemainingFeats--;
            }
            if (from.Skills.Fletching.Cap > 90)
            {
                from.AddFeat(TeiravonMobile.Feats.FletcherTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Fletcher);
                from.RemainingFeats--;
            }
            if (from.Skills.Tailoring.Cap > 90)
            {
                from.AddFeat(TeiravonMobile.Feats.TailorTraining);
                Feats.Functions.MerchantCraft(from, Server.Teiravon.Feats.Crafts.Tailor);
                from.RemainingFeats--;
            }
            if (from.Skills.Blacksmith.Cap > 100 || from.Skills.Tinkering.Cap > 100 || from.Skills.Carpentry.Cap > 100 || from.Skills.Cooking.Cap > 100 || from.Skills.Fletching.Cap > 100 || from.Skills.Tailoring.Cap > 100)
            {
                from.AddFeat(TeiravonMobile.Feats.MasterCraftsman);
                Feats.Functions.MasterCraftsman(from);
                from.RemainingFeats--;
            }
            if (from.Skills.Swords.Base > 60 || from.Skills.Macing.Base > 60 || from.Skills.Fencing.Base > 60 || from.Skills.Archery.Base > 60)
            {
                from.AddFeat(TeiravonMobile.Feats.CombatTraining);
                Feats.Functions.CombatTraining(from);
                from.RemainingFeats--;
            }

            from.PlayerClass = TeiravonMobile.Class.Merchant;
        }

        private bool IsSkillFeat(TeiravonMobile.Feats feat)
        {
            if (feat == TeiravonMobile.Feats.WeaponSpecialization || feat == TeiravonMobile.Feats.Apprenticeship || feat == TeiravonMobile.Feats.Blademaster || feat == TeiravonMobile.Feats.ExpertMining)
                return true;

            return false;
        }

        #endregion
    }

    public class TAVUtilities
    {

        #region Hireables
        public enum MercClass
        {
            None,
            Warrior,
            Cleric,
            Mage,
            Ranger
        }
        public static void MercOutfit(Mobile merc, MercClass type, int level)
        {
            bool twohand = Utility.RandomBool();

            switch (type)
            {
                case MercClass.Warrior:
                    if (twohand)
                    {
                        int x = Utility.Random(7);

                        switch (x)
                        {
                            case 1:
                                merc.AddItem(new Bardiche());
                                break;
                            case 2:
                                merc.AddItem(new Halberd());
                                break;
                            case 3:
                                merc.AddItem(new Spear());
                                break;
                            case 4:
                                merc.AddItem(new TwoHandedAxe());
                                break;
                            case 5:
                                merc.AddItem(new ExecutionersAxe());
                                break;
                            case 6:
                                merc.AddItem(new WarHammer());
                                break;
                            case 7:
                                merc.AddItem(new Claymore());
                                break;
                            default:
                                merc.AddItem(new Pike());
                                break;
                        }

                    }
                    else
                    {
                        switch (Utility.Random(7))
                        {
                            case 1:
                                merc.AddItem(new WarAxe());
                                break;
                            case 2:
                                merc.AddItem(new WarFork());
                                break;
                            case 3:
                                merc.AddItem(new Longsword());
                                break;
                            case 4:
                                merc.AddItem(new Cutlass());
                                break;
                            case 5:
                                merc.AddItem(new Lance());
                                break;
                            case 6:
                                merc.AddItem(new Maul());
                                break;
                            case 7:
                                merc.AddItem(new WarMace());
                                break;
                            default:
                                merc.AddItem(new VikingSword());
                                break;
                        }
                        switch (Utility.Random(5))
                        {
                            case 1:
                                merc.AddItem(new BronzeShield());
                                break;
                            case 2:
                                merc.AddItem(new SpikedShield());
                                break;
                            case 3:
                                merc.AddItem(new WoodenShield());
                                break;
                            case 4:
                                merc.AddItem(new HeaterShield());
                                break;
                            case 5:
                                merc.AddItem(new OrderShield());
                                break;
                            default:
                                merc.AddItem(new ChaosShield());
                                break;
                        }
                    }
                    switch(Utility.Random(5)+level)
                    {
                        case 0:
                            break;
                        case 1:
                            merc.AddItem(new StuddedArms());
                            break;
                        case 2:
                        case 3:
                            merc.AddItem(new BoneArms());
                            break;
                        case 4:
                        case 5:
                            merc.AddItem(new RingmailArms());
                            break;
                        default:
                            merc.AddItem(new PlateArms());
                            break;
                    }
                    switch (Utility.Random(4) + level)
                    {
                        case 0:
                            break;
                        case 1:
                            merc.AddItem(new StuddedLegs());
                            break;
                        case 2:
                            merc.AddItem(new BoneLegs());
                            break;
                        case 3:
                            merc.AddItem(new RingmailLegs());
                            break;
                        case 4:
                            merc.AddItem(new ChainLegs());
                            break;
                        default:
                            merc.AddItem(new PlateLegs());
                            break;
                    }

                    switch (Utility.Random(4) + level)
                    {
                        case 0:
                            break;
                        case 1:
                            merc.AddItem(new StuddedChest());
                            break;
                        case 2:
                            merc.AddItem(new BoneChest());
                            break;
                        case 3:
                            merc.AddItem(new RingmailChest());
                            break;
                        case 4:
                            merc.AddItem(new ChainChest());
                            break;
                        default:
                            merc.AddItem(new PlateChest());
                            break;
                    }

                    switch (Utility.Random(3) + level)
                    {
                        case 0:
                            break;
                        case 1:
                            merc.AddItem(new StuddedGloves());
                            break;
                        case 2:
                            merc.AddItem(new BoneGloves());
                            break;
                        case 3:
                            merc.AddItem(new RingmailGloves());
                            break;
                        default:
                            merc.AddItem(new PlateGloves());
                            break;
                    }

                    switch (Utility.Random(4) + level)
                    {
                        case 0:
                            break;
                        case 1:
                        case 2:
                            merc.AddItem(new LeatherGorget());
                            break;
                        case 3:
                        case 4:
                        case 5:
                            merc.AddItem(new StuddedGorget());
                            break;
                        default:
                            merc.AddItem(new PlateGorget());
                            break;
                    }


                    switch (Utility.Random(3) + level)
                    {
                        case 0:
                            break;
                        case 1:
                            merc.AddItem(new LeatherCap());
                            break;
                        case 2:
                            merc.AddItem(new ChainCoif());
                            break;
                        case 3:
                            merc.AddItem(new Helmet());
                            break;
                        case 4:
                            merc.AddItem(new NorseHelm());
                            break;
                        case 5:
                            merc.AddItem(new CloseHelm());
                            break;
                        default:
                            merc.AddItem(new PlateHelm());
                            break;
                    }

                    break;
                case MercClass.Mage:
                    break;

            }
        }

        #endregion

        #region Genetics

        private static int GetPhenome(int phenome, TavGenome genome)
        {
            int total = 0;
            for (int j = 0; j < 3; ++j)
            {
                if (genome.Genome[phenome, j, 0] && genome.Genome[phenome, j, 1])
                    total += 1;
            }
            return total;
        }

        public static void ScaleGenetics(BaseCreature subject)
        {
            TavGenome genome = (TavGenome)XmlAttach.FindAttachment(subject, typeof(TavGenome));
            
            if (genome == null)
            {
                genome = new TavGenome();
                XmlAttach.AttachTo(subject, genome);
            }

            int scale = 0;
            int phenome = 0;
            bool full = (subject.Hits == subject.HitsMax);

            scale = GetPhenome(phenome, genome) * 15;

            subject.HitsMaxSeed = AOS.Scale(subject.HitsMaxSeed, 100 + scale);
            
            if (full)
                subject.Hits = subject.HitsMax;

            phenome = 1;
            scale = GetPhenome(phenome, genome) * 15;
            subject.StamMaxSeed = AOS.Scale(subject.StamMaxSeed, 100 + scale);

            phenome = 2;
            scale = GetPhenome(phenome, genome) * 15;
            subject.Str = AOS.Scale(subject.Str, 100 + scale);

            phenome = 3;
            scale = GetPhenome(phenome, genome) * 15;
            subject.Dex = AOS.Scale(subject.Dex, 100 + scale);

            phenome = 4;
            scale = GetPhenome(phenome, genome) * 15;
            subject.Int = AOS.Scale(subject.Int, 100 + scale);
            
            phenome = 5;
            scale = GetPhenome(phenome, genome) * 15;
            subject.DamageMin = AOS.Scale(subject.DamageMin, 100 + scale);

            phenome = 6;
            scale = GetPhenome(phenome, genome) * 15;
            subject.DamageMax = AOS.Scale(subject.DamageMax, 100 + scale);

            phenome = 7;
            scale = GetPhenome(phenome, genome) * 15;
            subject.PhysicalResistanceSeed = AOS.Scale(subject.PhysicalResistanceSeed, 100 + scale);

            phenome = 8;
            scale = GetPhenome(phenome, genome) * 15;
            subject.FireResistSeed = AOS.Scale(subject.FireResistSeed, 100 + scale);
            
            phenome = 9;
            scale = GetPhenome(phenome, genome) * 15;
            subject.ColdResistSeed = AOS.Scale(subject.ColdResistSeed, 100 + scale);

            phenome = 10;
            scale = GetPhenome(phenome, genome) * 15;
            subject.PoisonResistSeed = AOS.Scale(subject.PoisonResistSeed, 100 + scale);

            phenome = 11;
            scale = GetPhenome(phenome, genome) * 15;
            subject.EnergyResistSeed = AOS.Scale(subject.EnergyResistSeed, 100 + scale);
            
            phenome = 12;
            scale = GetPhenome(phenome, genome) * 15;
            subject.Skills.Wrestling.Base = subject.Skills.Wrestling.Base * (1 + (scale * .01));
            
            phenome = 13;
            scale = GetPhenome(phenome, genome) * 15;
            subject.Skills.Tactics.Base = subject.Skills.Tactics.Base * (1 + (scale * .01));
            
            phenome = 14;
            scale = GetPhenome(phenome, genome) * 15;
            subject.Skills.MagicResist.Base = subject.Skills.MagicResist.Base * (1 + (scale * .01));
            
            phenome = 15;
            scale = GetPhenome(phenome, genome) * 15;
            subject.Skills.Anatomy.Base = subject.Skills.Anatomy.Base * (1 + (scale * .01));
        }

        public static void Reproduce(BaseCreature parentA, BaseCreature parentB, BaseCreature child)
        {
            TavGenome genomeA = (TavGenome)XmlAttach.FindAttachment(parentA, typeof(TavGenome));
            TavGenome genomeB = (TavGenome)XmlAttach.FindAttachment(parentB, typeof(TavGenome));
            TavGenome genomeC = new TavGenome();

            if (genomeA == null)
            {
                genomeA = new TavGenome();
                XmlAttach.AttachTo(parentA, genomeA);
                TAVUtilities.ScaleGenetics(parentA);
            }
            if (genomeB == null)
            {
                genomeB = new TavGenome();
                XmlAttach.AttachTo(parentB, genomeB);
                TAVUtilities.ScaleGenetics(parentB);
            }

            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        bool alielA = genomeA.Genome[i,j,k];
                        bool alielB = genomeB.Genome[i,j,k];
                        genomeC.Genome[i,j,k] = Utility.RandomBool()? alielA : alielB;
                        
                    }
                }
            }

            XmlAttach.AttachTo(child, genomeC);
            TAVUtilities.ScaleGenetics(child);
            child.Hue = Utility.RandomBool() ? parentA.Hue : parentB.Hue;
            child.Female = Utility.RandomBool();
        }

        public static bool[,,] Genome = new bool[,,]
			{
				 // HP phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Stamina phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Str phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Dex phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Int phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // MinDamage phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // MaxDamage phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // PhysResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                //FireResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                //ColdResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // PoisonResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // EnergyResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // WrestlingCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // TacticsCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // MagicResistCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // AnatomyCap phenotype
                {
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                }
			};

        #endregion

        #region Movement
        private PathFollower m_Path;
        private Mobile m_ToMove;
        private static Queue m_Obstacles = new Queue();

        public bool MovePlayer(Mobile toMove, Mobile target, bool run, int range)
        {
            if (toMove.Deleted || toMove.Frozen || target == null || target.Deleted)
                return false;

            if (toMove.InRange(target, range))
            {
                m_Path = null;
                return true;
            }

            m_ToMove = toMove;

            if (m_Path != null && m_Path.Goal == target)
            {
                if (m_Path.Follow(run, 1))
                {
                    m_Path = null;
                    return true;
                }
            }
            else if (!DoMove(toMove.GetDirectionTo(target), true))
            {
                m_Path = new PathFollower(toMove, target);
                m_Path.Mover = new MoveMethod(DoMoveImpl);

                if (m_Path.Follow(run, 1))
                {
                    m_Path = null;
                    return true;
                }
            }
            else
            {
                m_Path = null;
                return true;
            }

            return false;
        }

        public MoveResult DoMoveImpl(Direction d)
        {
            if (m_ToMove.Deleted || m_ToMove.Frozen || m_ToMove.Paralyzed || (m_ToMove.Spell != null && m_ToMove.Spell.IsCasting))
                return MoveResult.BadState;

            // This makes them always move one step, never any direction changes
            m_ToMove.Direction = d;

            m_ToMove.Pushing = false;

            MoveImpl.IgnoreMovableImpassables = false;

            if ((m_ToMove.Direction & Direction.Mask) != (d & Direction.Mask))
            {
                bool v = m_ToMove.Move(d);

                MoveImpl.IgnoreMovableImpassables = false;
                return (v ? MoveResult.Success : MoveResult.Blocked);
            }
            else if (!m_ToMove.Move(d))
            {
                bool wasPushing = m_ToMove.Pushing;

                bool blocked = true;

                Map map = m_ToMove.Map;

                if (map != null)
                {
                    int x = m_ToMove.X, y = m_ToMove.Y;
                    Movement.Movement.Offset(d, ref x, ref y);

                    IPooledEnumerable eable = map.GetItemsInRange(new Point3D(x, y, m_ToMove.Location.Z), 1);

                    foreach (Item item in eable)
                    {
                        if (item is BaseDoor && (item.Z + item.ItemData.Height) > m_ToMove.Z && (m_ToMove.Z + 16) > item.Z)
                        {
                            if (item.X != x || item.Y != y)
                                continue;

                            BaseDoor door = (BaseDoor)item;

                            if (!door.Locked || !door.UseLocks())
                                m_Obstacles.Enqueue(door);
                        }
                    }

                    eable.Free();

                    if (m_Obstacles.Count > 0)
                        blocked = false; // retry movement

                    while (m_Obstacles.Count > 0)
                    {
                        Item item = (Item)m_Obstacles.Dequeue();

                        if (item is BaseDoor)
                            ((BaseDoor)item).Use(m_ToMove);
                    }

                    if (!blocked)
                        blocked = !m_ToMove.Move(d);
                }

                if (blocked)
                {
                    int offset = (Utility.RandomDouble() >= 0.6 ? 1 : -1);

                    for (int i = 0; i < 2; ++i)
                    {
                        int v = (int)m_ToMove.Direction;

                        m_ToMove.SetDirection((Direction)((((v & 0x7) + offset) & 0x7) | (v & 0x80)));

                        if (m_ToMove.Move(m_ToMove.Direction))
                        {
                            MoveImpl.IgnoreMovableImpassables = false;
                            return MoveResult.SuccessAutoTurn;
                        }
                    }

                    MoveImpl.IgnoreMovableImpassables = false;
                    return (wasPushing ? MoveResult.BadState : MoveResult.Blocked);
                }
                else
                {
                    MoveImpl.IgnoreMovableImpassables = false;
                    return MoveResult.Success;
                }
            }

            MoveImpl.IgnoreMovableImpassables = false;
            return MoveResult.Success;
        }

        public bool DoMove(Direction d, bool badStateOk)
        {
            MoveResult res = DoMoveImpl(d);

            return (res == MoveResult.Success || res == MoveResult.SuccessAutoTurn || (badStateOk && res == MoveResult.BadState));
        }
        #endregion

        public static void EndCooldown(object state)
        {
            object[] states = state as object[];
            Mobile from = states[0] as Mobile;
            Type Locked = states[1] as Type;

            if (from != null)
                from.EndAction(Locked);
        }


        //Takes 2 ints and divides them into an accurate double value. If int 2 is 0, 0 is returned.
        public static double DoubleDivide(int int1, int int2)
        {
            if (int2 == 0) //avoid division by 0
                return 0.0;
            double result = ((double)int1) / ((double)int2);
            return result;
        }







        // Normalized Randomizer

        public static int RandomNormalInt(int typical, int deviation)
        {
            int sum = Utility.Dice(10, 10, 0);
            double sigma = ((sum - 55) / 45.0);
            return typical + (int)(deviation * sigma);
            //return typical + (int)( bound * (Utility.Dice(10, 10, 0) - 55 / 45.0) );
        }

        public static double RandomNormalDouble(double typical, double bound)
        {
            return typical + (bound * ((Utility.Dice(10, 10, 0) - 55.0) / 45.0));
        }










        //returns the int percent value of a double
        public static int DoubleToPercentage(double value)
        {
            int result = ((int)(value * 100));
            return result;
        }

        public static int GetSkillLevel(Mobile m, SkillName skill)
        {
            if (m.Skills[skill].Value < 10.0)
                return 0;
            else if (m.Skills[skill].Value <= 25.0)
                return 1;
            else if (m.Skills[skill].Value <= 40.0)
                return 2;
            else if (m.Skills[skill].Value <= 55.0)
                return 3;
            else if (m.Skills[skill].Value <= 70.0)
                return 4;
            else if (m.Skills[skill].Value <= 85.0)
                return 5;
            else if (m.Skills[skill].Value <= 100.0)
                return 6;
            else if (m.Skills[skill].Value >= 115.0)
                return 7;

            return 0;
        }
        public static void FindCorpse(Mobile from)
        {
            Corpse m_Corpse = null;
            int highest = 0;

            foreach (Map map in Map.AllMaps)
            {
                IPooledEnumerable eable = map.GetItemsInRange(Point3D.Zero, 6144);

                foreach (Item i in eable)
                {

                    if (i is Corpse)
                    {
                        Corpse c = i as Corpse;

                        if (c.Owner == from && c.TotalItems > highest)
                        {
                            m_Corpse = c;
                        }
                    }
                }

                eable.Free();
            }

            if (m_Corpse != null && highest > 0)
            {
                m_Corpse.MoveToWorld(from.Location, from.Map);
                m_Corpse.Open(from, true);
            }
        }

        public static double PopulationBonus(TeiravonMobile from)
        {
            int total = 0;

            foreach (Map map in Map.AllMaps)
            {
                if (map == Map.Internal)
                    continue;

                IPooledEnumerable eable = map.GetMobilesInRange(Point3D.Zero, 6144);

                foreach (Mobile m in eable)
                {
                    if (m is TeiravonMobile)
                    {
                        TeiravonMobile tav = m as TeiravonMobile;

                        if (tav.PlayerRace == from.PlayerRace)
                        {
                            total++;
                        }
                    }
                }

                eable.Free();
            }

            double bonus = 3.4 - (total * .4);
            if (bonus < 1)
                bonus = 1;
            return bonus;
        }

        public static int CalculateLevel(Mobile creature)
        {
            int level = 0;
            int hitty = 0;
            int tanky = 0;
            int casty = 0;
            TeiravonMobile tav = null;

            if (creature is TeiravonMobile)
                tav = creature as TeiravonMobile;

            tanky = (int)(((creature.HitsMax + creature.StamMax) / 20) + ((creature.PhysicalResistance + creature.FireResistance + creature.ColdResistance + creature.PoisonResistance + creature.EnergyResistance) / 5));
            BaseWeapon wep = creature.Weapon as BaseWeapon;
            int DamageMax;
            int DamageMin;
            double combatskill = 0;
            double delay = 4.0;
            if (wep != null)
            {
                combatskill = creature.Skills[wep.Skill].Value;
                delay = wep.GetDelay(creature).TotalSeconds;
            }
            else
                combatskill = creature.Skills.Wrestling.Value;

            int damagebonus = 1;

            if (creature.Weapon != null)
            {
                creature.Weapon.GetStatusDamage(creature, out DamageMax, out DamageMin);
                damagebonus = (DamageMax + DamageMin) / 2;
            }


            hitty = (int)((combatskill) + (100 - (delay * 25)) + (damagebonus * 3));
            
            if (!creature.Player)
                casty = (int)((creature.Skills.Magery.Value) + (creature.Skills.EvalInt.Value) + ((creature.ManaMax + creature.Int) / 20));
            else
                casty = (int)(((creature.Skills.Magery.Value) + (creature.Skills.EvalInt.Value) + ((creature.ManaMax + creature.Int) / 8)) *(1 + 0.03 * tav.PlayerLevel));

            if (hitty + casty < tanky && !creature.Player)
                tanky = (int)(tanky * .5);

            if (tanky < 50 && !creature.Player)
                casty =(int)(casty * (tanky/50));
            
            if (tanky < 40 && creature.Player)
                casty = (int)(casty * (tanky / 40));

            casty = (int)(casty * .75);

            level = (int)Math.Round(((tanky + hitty + casty) * .085));

            #region OldCalc
            /*
           // Console.WriteLine("-----------");
           // Console.WriteLine("Creature: {0}", creature.Name);
            

            int stats = (creature.Str + creature.Dex + creature.Int + creature.HitsMax + creature.StamMax + creature.ManaMax) / 6;
            
            //Console.WriteLine("Stats: {0}", stats);
            if (stats <= 100)
                stats = stats * 2;
            else if (stats <= 200)
                stats = (int)(stats * 1.50);
            else if (stats > 200 && stats < 500) 
                stats = (int)(stats *1.1);
            else if (stats <= 1000 && stats >= 500)
                stats = (int)(stats * .80);
            else if (stats <= 5000)
                stats = (int)(stats * .60);
            else
                stats = (int)(stats * .4);

            level = (int)(stats / 75);

            if (level == 0)
                level = 1;

            
           // Console.WriteLine( "AStats: {0}, Level: {1}", stats, level );

            double[] combatskills = new double[] {
				creature.Skills.Archery.Base,
				creature.Skills.Fencing.Base,
				creature.Skills.Macing.Base,
				creature.Skills.Parry.Base,
				creature.Skills.Swords.Base,
				creature.Skills.Tactics.Base,
				//creature.Skills.Wrestling.Base,
				creature.Skills.Anatomy.Base,
				creature.Skills.Focus.Base };

            double combatbonus = 0.0;
            double count = 0.0;

            for (int i = 0; i < combatskills.Length; i++)
            {
                if (combatskills[i] > 20.0 )
                {
                    combatbonus += combatskills[i];
                    count++;
                }
            }
            
            combatbonus += creature.Skills.Wrestling.Base;
            count++;

            int combatlevel = 0;
            int magerylevel = 0;

            if (count != 0.0 && combatbonus != 0.0)
                combatbonus /= count;

            if (combatbonus == 0.0)
                combatlevel += 0;
            else if (combatbonus <= 30.0)
                combatlevel += 1;
            else if (combatbonus > 30.0 && combatbonus <= 45.0)
                combatlevel += 2;
            else if (combatbonus > 45.0 && combatbonus <= 60.0)
                combatlevel += 2;
            else if (combatbonus > 60.0 && combatbonus <= 75.0)
                combatlevel += 3;
            else if (combatbonus > 75.0 && combatbonus <= 90.0)
                combatlevel += 4;
            else if (combatbonus > 90.0 && combatbonus <= 105.0)
                combatlevel += 5;
            else
                combatlevel += 6;

            double magerybonus = 0.0;
            count = 0.0;

            if (creature.Skills.Magery.Base > 0.0)
            {
                magerybonus += creature.Skills.Magery.Base;
                count++;
            }

            if (creature.Skills.EvalInt.Base > 0.0)
            {
                magerybonus += creature.Skills.EvalInt.Base;
                count++;
            }

            if (magerybonus != 0.0 && count != 0.0)
                magerybonus /= count;

            if (magerybonus == 0.0)
                magerylevel += 0;
            else if (magerybonus <= 30.0)
                magerylevel += 0;
            else if (magerybonus > 30.0 && magerybonus <= 45.0)
                magerylevel += 1;
            else if (magerybonus > 45.0 && magerybonus <= 60.0)
                magerylevel += 2;
            else if (magerybonus > 60.0 && magerybonus <= 75.0)
                magerylevel += 3;
            else if (magerybonus > 75.0 && magerybonus <= 85.0)
                magerylevel += 4;
            else if (magerybonus > 85.0 && magerybonus <= 95.0)
                magerylevel += 5;
            else if (magerybonus > 95.0 && magerybonus <= 105.0)
                magerylevel += 6;
            else
                magerylevel += 7;

            if (magerylevel >= combatlevel)
                level += magerylevel + (combatlevel / 2);
            else if (magerylevel != 0)
                level += ((combatlevel) + (magerylevel / 2));
            else
                level += combatlevel;

            //Console.WriteLine( "Combats: {0} / Magery: {1}, Level: {2}", combatbonus, magerybonus, level );
            int DamageMax = 0;
            int DamageMin = 0;
            if (creature.Weapon != null)
                creature.Weapon.GetStatusDamage(creature,out DamageMax,out DamageMin);

            int damagebonus = (DamageMax + DamageMin) / 2;

            if (damagebonus == 0)
                level += 0;
            else if (damagebonus > 0 && damagebonus <= 6)
                level += 1;
            else if (damagebonus > 6 && damagebonus <= 12)
                level += 2;
            else if (damagebonus > 12 && damagebonus <= 20)
                level += 3;
            else if (damagebonus > 20 && damagebonus <= 28)
                level += 4;
            else if (damagebonus > 28 && damagebonus <= 38)
                level += 5;
            else if (damagebonus > 38 && damagebonus <= 48)
                level += 6;
            else if (damagebonus > 48 && damagebonus <= 60)
                level += 7;
            else
                level += 8;

            //Console.WriteLine( "Damage Avg: {0}, Level: {1}", damagebonus, level );

            int resistbonus = (creature.PhysicalResistance + creature.FireResistance + creature.ColdResistance + creature.PoisonResistance + creature.EnergyResistance) / 5;

            if (resistbonus <= 10)
                level += 0;
            else if (resistbonus > 10 && resistbonus <= 25)
                level += 0;
            else if (resistbonus > 25 && resistbonus <= 40)
                level += 1;
            else if (resistbonus > 40 && resistbonus <= 55)
                level += 2;
            else if (resistbonus > 55 && resistbonus <= 65)
                level += 3;
            else
                level += 4;

            //Console.WriteLine( "Resists: {0}, Level: {1}", resistbonus, level );

            if (level > 25)
                level = 25;
            else if (level < 0)
                level = 0;

            //Console.WriteLine( "Adjusted Level: {0}", level );
            */
            #endregion
            return level;
        }

        public static void ReportLevel(Mobile from, Mobile creature)
        {
            int level = 0;
            int hitty = 0;
            int tanky = 0;
            int casty = 0;
            TeiravonMobile tav = null;

            if (creature is TeiravonMobile)
                tav = creature as TeiravonMobile;

            tanky = (int)(((creature.HitsMax + creature.StamMax) / 20) + ((creature.PhysicalResistance + creature.FireResistance + creature.ColdResistance + creature.PoisonResistance + creature.EnergyResistance) / 5));
            from.SendMessage("Tank Rating : {0}", tanky);
            
            BaseWeapon wep = creature.Weapon as BaseWeapon;
            int DamageMax;
            int DamageMin;
            double combatskill = wep.GetAttackSkillValue(creature, creature);
            creature.Weapon.GetStatusDamage(creature, out DamageMax, out DamageMin);
            int damagebonus = (DamageMax + DamageMin) / 2;
            hitty = (int)((combatskill) + (100 - (wep.GetDelay(creature).TotalSeconds * 25)) + (damagebonus * 3));
            from.SendMessage("Attack Rating : {0}", hitty);

            if (!creature.Player)
                casty = (int)((creature.Skills.Magery.Value) + (creature.Skills.EvalInt.Value) + ((creature.ManaMax + creature.Int) / 20));
            else
                casty = (int)(((creature.Skills.Magery.Value) + (creature.Skills.EvalInt.Value) + ((creature.ManaMax + creature.Int) / 8)) * (1 + 0.03 * tav.PlayerLevel));
            from.SendMessage("Magic Rating : {0}", casty);

            if (hitty + casty < tanky && !creature.Player)
            {
                tanky = (int)(tanky * .5);
                from.SendMessage("Meatbag penalty, low damage, high HP. New Tank Rating : {0}", tanky);
            }
            if (tanky < 50 && !creature.Player)
            {
                casty = (int)(casty * (tanky / 50));
                from.SendMessage("Glass Cannon penalty, Magic Rating : {0}", casty);
            }
            if (tanky < 40 && creature.Player)
            {
                casty = (int)(casty * (tanky / 40));
                from.SendMessage("Glass Cannon penalty, Magic Rating : {0}", casty);
            }

            casty = (int)(casty * .75);

            level = (int)Math.Round(((tanky + hitty + casty) * .085));
            from.SendMessage("Final adjusted value : {0}", level);
        }

        public static void ScavengeLoot(TeiravonMobile from, Mobile creature)
        {
            int[][][] m_RewardTable = new int[][][]
			{
				new int[][] // 1-part (regular)
				{
					new int[]{ 150, 150, 300, 300 },
					new int[]{ 225, 225, 450, 450 },
					new int[]{ 300, 400, 600, 750 }
				},
				new int[][] // 1-part (exceptional)
				{
					new int[]{ 300, 300,  600,  600 },
					new int[]{ 450, 450,  900,  900 },
					new int[]{ 600, 750, 1200, 1800 }
				},
				new int[][] // 4-part (regular)
				{
					new int[]{  3000,  3000,  4000,  4000 },
					new int[]{  4500,  4500,  6000,  6000 },
					new int[]{  6000,  8000,  8000, 10000 }
				},
				new int[][] // 4-part (exceptional)
				{
					new int[]{  4000,  4000,  5000,  5000 },
					new int[]{  6000,  6000,  7500,  7500 },
					new int[]{  8000, 10000, 10000, 15000 }
				},
				new int[][] // 5-part (regular)
				{
					new int[]{  4000,  4000,  5000,  5000 },
					new int[]{  6000,  6000,  7500,  7500 },
					new int[]{  8000, 10000, 10000, 15000 }
				},
				new int[][] // 5-part (exceptional)
				{
					new int[]{  5000,  5000,  7500,  7500 },
					new int[]{  7500,  7500, 11250, 11250 },
					new int[]{ 10000, 15000, 15000, 20000 }
				},
				new int[][] // 6-part (regular)
				{
					new int[]{  5000,  5000,  7500,  7500 },
					new int[]{  7500,  7500, 11250, 11250 },
					new int[]{ 10000, 15000, 15000, 20000 }
				},
				new int[][] // 6-part (exceptional)
				{
					new int[]{  7500,  7500, 10000, 10000 },
					new int[]{ 11250, 11250, 15000, 15000 },
					new int[]{ 15000, 20000, 20000, 30000 }
				}
			};

            int crIndex = (CalculateLevel(creature) / 12);
            if (crIndex > 7) { crIndex = 7; };
            int levelIndex = (from.PlayerLevel / 10);
            if (levelIndex > 3) { levelIndex = 3; };
            int fameIndex = ( creature.Fame / 5000);
            if (fameIndex > 3){fameIndex = 3;}

            int rating = m_RewardTable[crIndex][levelIndex][fameIndex];

            if (Utility.RandomDouble() + from.PlayerLevel*.01 >= 1.0 - (double)(from.Luck / 1200)) // Lucky enough to scavenge something?
                return;

            if (rating < 1000)
            {
                int amount = Utility.RandomMinMax(rating/100, rating/50);
                from.AddToBackpack(new Factions.Silver(amount));
                from.SendMessage("You manage to find an additional {0} silver picking through the body.", amount);
                return;
            }
            if (rating < 10000)
            {
                int amount = Utility.RandomMinMax(rating / 1000, rating / 500);
                from.AddToBackpack(new Gold(amount));
                from.SendMessage("You manage to find an additional {0} gold picking through the body.", amount);
                return;
            }
            else
            {

                if (Utility.Random(5) > 2)
                {
                    int amount = Utility.RandomMinMax(rating / 1000, rating / 500);
                    from.AddToBackpack(new Gold(amount));
                    from.SendMessage("You manage to find an additional {0} gold picking through the body.", amount);
                    return;
                }
                else
                {
                    Item jewel = Loot.RandomJewelry();
                    BaseRunicTool.ApplyAttributesTo((BaseJewel)jewel, rating / 10000, rating / 1200, rating / 300);
                    from.AddToBackpack(jewel);
                    from.SendMessage("You manage to find a small trinket when picking through the body.");
                    return;
                }
            }

        }

        public static string MoraleToMessage(double amount)
        {
            if (amount == 0)
                return "You feel like yourself.";
            else if (amount > 0 && amount <= 2.0)
                return "You feel more confident than usual.";
            else if (amount > 2.0 && amount <= 4.0)
                return "You feel exceptionally more confident than usual.";
            else if (amount >= 5.0)
                return "You feel as if you could do anything.";
            else if (amount < 0 && amount >= -2.0)
                return "You feel weaker than usual.";
            else if (amount < -2.0 && amount >= -4.0)
                return "You feel exceptionally weaker than usual.";
            else if (amount <= -5.0)
                return "You feel as if you can't do anything right.";
            else
                return "You feel like yourself.";
        }
        public static string ConsiderDif(int amount)
        {
            if (amount == 0)
                return "It looks dead even.";
            else if (amount > 0 && amount <= 6)
                return "It's about even.";
            else if (amount > 6 && amount <= 12)
                return "You feel sure of victory.";
            else if (amount >= 12)
                return "They're no threat.";
            else if (amount < 0 && amount >= -6)
                return "They may be a little challenging.";
            else if (amount < -6 && amount >= -12)
                return "This appears risky.";
            else if (amount <= -12)
                return "I hope you brought some friends.";
            else
                return "It looks about even.";
        }
    }
}