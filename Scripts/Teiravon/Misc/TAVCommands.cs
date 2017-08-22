using System;
using System.Collections;
using System.IO;
using System.Xml;
using Server;
using Server.Accounting;
using Server.Targeting;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Gumps;
using Server.Teiravon;
using Server.Spells;
using Server.Network;
using Server.Regions;
using SpawnMapsE = Server.Teiravon.SpawnMaps.Maps;
using Server.Engines.Craft;
using System.Data;

namespace Server.Scripts.Commands
{
	public class TAVGenCommands
	{
        public static Hashtable Faith = new Hashtable();

        public static void AddFaith(TeiravonMobile from)
        {
            Faith.Add(from, from);
            Timer.DelayCall(TimeSpan.FromHours(2), new TimerStateCallback(EndFaith_Callback),
            new object[] { from });
        }

        public static void EndFaith_Callback(object state)
        {
            object[] args = (object[])state;

            TeiravonMobile from = (TeiravonMobile)args[0];

            if (Faith.Contains(from))
                Faith.Remove(from);
        }

		public static void Initialize()
		{
			// Player
			Server.Commands.Register( "RemoveDisguise", AccessLevel.Player, new CommandEventHandler( RemoveDisguise_OnCommand ) );
			Server.Commands.Register( "Climb", AccessLevel.Player, new CommandEventHandler( Climb_OnCommand ) );
			Server.Commands.Register( "Status", AccessLevel.Player, new CommandEventHandler( Status_OnCommand ) );
			Server.Commands.Register( "Exp", AccessLevel.Player, new CommandEventHandler( Exp_OnCommand ) );
			Server.Commands.Register( "Language", AccessLevel.Player, new CommandEventHandler( Language_OnCommand ) );
			Server.Commands.Register( "Bash", AccessLevel.Player, new CommandEventHandler( Bash_OnCommand ) );
			Server.Commands.Register( "Hunger", AccessLevel.Player, new CommandEventHandler( Hunger_OnCommand ) );
			Server.Commands.Register( "Alloys", AccessLevel.Player, new CommandEventHandler( Alloys_OnCommand ) );
			Server.Commands.Register( "GM", AccessLevel.Player, new CommandEventHandler( GM_OnCommand ) );
			Server.Commands.Register( "Encounters", AccessLevel.Player, new CommandEventHandler( Encounters_OnCommand ) );
			Server.Commands.Register( "Pray", AccessLevel.Player, new CommandEventHandler( Pray_OnCommand ) );
			Server.Commands.Register( "DropTrops", AccessLevel.Player, new CommandEventHandler( DropTrops_OnCommand ) );
            Server.Commands.Register( "Con", AccessLevel.Player, new CommandEventHandler(Con_OnCommand));
            Server.Commands.Register( "ThrowPotion", AccessLevel.Player, new CommandEventHandler(ThrowPotion_OnCommand));
            Server.Commands.Register( "Faith", AccessLevel.Player, new CommandEventHandler(Faith_OnCommand));
            Server.Commands.Register( "Nod", AccessLevel.Player, new CommandEventHandler(Nod_OnCommand));
            Server.Commands.Register( "PlayLute", AccessLevel.Player, new CommandEventHandler(PlayLute_OnCommand));
            Server.Commands.Register( "Escape", AccessLevel.Player, new CommandEventHandler(Escape_OnCommand));
            Server.Commands.Register( "Swim", AccessLevel.Player, new CommandEventHandler(Swim_OnCommand));
            Server.Commands.Register( "Goal", AccessLevel.Player, new CommandEventHandler(Goal_OnCommand));
			
			// Counsellor
			Server.Commands.Register( "ExpBoost", AccessLevel.Counselor, new CommandEventHandler( ExpBoost_OnCommand ) );
			Server.Commands.Register( "SpeedBoost", AccessLevel.Counselor, new CommandEventHandler( SpeedBoost_OnCommand ) );
            Server.Commands.Register( "GMCon", AccessLevel.Counselor, new CommandEventHandler(GMCon_OnCommand));
            Server.Commands.Register( "ConReport", AccessLevel.Counselor, new CommandEventHandler(ConReport_OnCommand));

			// GameMaster
            Server.Commands.Register( "RemoveFeats", AccessLevel.GameMaster, new CommandEventHandler(RemoveFeats_OnCommand));
			Server.Commands.Register( "EnableAllSpells", AccessLevel.GameMaster, new CommandEventHandler( EnableAllSpells_OnCommand ) );
			Server.Commands.Register( "xSay", AccessLevel.Counselor, new CommandEventHandler( xSay_OnCommand ) );
			Server.Commands.Register( "xEmote", AccessLevel.Counselor, new CommandEventHandler( xEmote_OnCommand ) );
			Server.Commands.Register( "HearAll", AccessLevel.Counselor, new CommandEventHandler( HearAll_OnCommand ) );
			Server.Commands.Register( "HearAllParty", AccessLevel.Counselor, new CommandEventHandler( HearAllParty_OnCommand ) );
            Server.Commands.Register( "HearAllNods", AccessLevel.Counselor, new CommandEventHandler(HearAllNods_OnCommand));
			Server.Commands.Register( "LinkKey", AccessLevel.GameMaster, new CommandEventHandler( LinkKey_OnCommand ) );
			Server.Commands.Register( "ListFeats", AccessLevel.GameMaster, new CommandEventHandler( ListFeats_OnCommand ) );
			Server.Commands.Register( "RPBC", AccessLevel.Counselor, new CommandEventHandler( RPBC_OnCommand ) );
			Server.Commands.Register( "DivineNotice", AccessLevel.Counselor, new CommandEventHandler( DivineNotice_OnCommand ) );

			// Administrator
			Server.Commands.Register( "ClearFeats", AccessLevel.Administrator, new CommandEventHandler( ClearFeats_OnCommand ) );
			Server.Commands.Register( "DeletePlayer", AccessLevel.Administrator, new CommandEventHandler( DeletePlayer_OnCommand ) );
			Server.Commands.Register( "InitSpawns", AccessLevel.Administrator, new CommandEventHandler( InitSpawns_OnCommand ) );
			Server.Commands.Register( "MoveSpawn", AccessLevel.Administrator, new CommandEventHandler( MoveSpawn_OnCommand ) );
            Server.Commands.Register( "CleanOld", AccessLevel.Administrator, new CommandEventHandler(CleanOld_OnCommand));
            Server.Commands.Register( "RemoveTag", AccessLevel.Administrator, new CommandEventHandler(RemoveTag_OnCommand));
			// Temp
			//Server.Commands.Register( "Z42", AccessLevel.Administrator, new CommandEventHandler( Z42_OnCommand ) );
			Server.Commands.Register( "Moo", AccessLevel.Administrator, new CommandEventHandler( Moo_OnCommand ) );
			Server.Commands.Register( "Moo2", AccessLevel.Administrator, new CommandEventHandler( Moo2_OnCommand ) );
            Server.Commands.Register( "ExportFeats", AccessLevel.Administrator, new CommandEventHandler(ExportFeats_OnCommand));
            Server.Commands.Register( "ImportFeats", AccessLevel.Administrator, new CommandEventHandler(ImportFeats_OnCommand));
           // Server.Commands.Register("ExportChar", AccessLevel.Administrator, new CommandEventHandler(ExportChar_OnCommand));
           // Server.Commands.Register("ImportChar", AccessLevel.Administrator, new CommandEventHandler(ImportChar_OnCommand));
            Server.Commands.Register("GiveAnOrc", AccessLevel.Administrator, new CommandEventHandler(GiveOrc_OnCommand));
            
           // Server.Commands.Register("ReportAggressors", AccessLevel.Administrator, new CommandEventHandler(ReportAggressors_OnCommand));
		}

        /*
        [Usage("ReportAggressors")]
        [Description("Toggle reporting to warning.log")]
        private static void ReportAggressors_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = e.Mobile as TeiravonMobile;

            World.ReportAggressor = !World.ReportAggressor;
            m_Player.SendMessage("ReportAggressors set to {0}", World.ReportAggressor);
        }
        */

        [Usage("GiveAnOrc")]
        [Description("Add a Temporary Orc to a player's account.")]
        private static void GiveOrc_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = e.Mobile as TeiravonMobile;

            if (e.Length >= 1)
            {
                string tag = e.Arguments[0];
                m_Player.Target = new AddAnOrcTarget(tag);
            }
            else
                m_Player.SendMessage("Please Specify what kind of orc to add.");
        }

        private class AddAnOrcTarget : Target
        {
            private string m_Tag;

            public AddAnOrcTarget(string tag)
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
                m_Tag = tag;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                TeiravonMobile newOrc = new TeiravonMobile();
                newOrc.AddItem(new Backpack());
                newOrc.BodyValue = 400;
                bool OrcAdded = false;
                if (o is TeiravonMobile)
                {
                    Account source = (Account)((TeiravonMobile)o).Account;
                    for (int i = 0; i < 7 && !OrcAdded; ++i)
                    {
                        if (source[i] == null)
                        {
                            source[i] = newOrc;
                            OrcAdded = true;
                        }
                    }
                    if (OrcAdded)
                    {
                        TeiravonMobile m = newOrc;
                        newOrc.Name = NameList.RandomName("orc");
                        newOrc.PlayerAlignment = TeiravonMobile.Alignment.ChaoticEvil;
                        newOrc.PlayerRace = TeiravonMobile.Race.Orc;
                        newOrc.NameHue = 1926;
                        switch (m_Tag)
                        {
                            case "cleric":
                                {
                                    newOrc.PlayerClass = TeiravonMobile.Class.DarkCleric;
                                    #region clericSkills
                                    m.AddToBackpack(new HolySymbol(HolySymbol.SymbolType.DarkCleric));

                                    m.Magicable = true;

                                    m.SetSpell(Teiravon.Spells.HealSpell, true);
                                    m.SetSpell(Teiravon.Spells.HarmSpell, true);
                                    m.SetSpell(Teiravon.Spells.GreaterHealSpell, true);
                                    m.SetSpell(Teiravon.Spells.BlessSpell, true);
                                    m.SetSpell(Teiravon.Spells.LightningSpell, true);
                                    m.SetSpell(Teiravon.Spells.CurseSpell, true);
                                    m.SetSpell(Teiravon.Spells.CureSpell, true);
                                    m.SetSpell(Teiravon.Spells.ResurrectionSpell, true);
                                    m.SetSpell(Teiravon.Spells.NightSightSpell, true);
                                    m.SetSpell(Teiravon.Spells.ProtectionSpell, true);
                                    m.SetSpell(Teiravon.Spells.StrengthSpell, true);
                                    m.SetSpell(Teiravon.Spells.AgilitySpell, true);
                                    m.SetSpell(Teiravon.Spells.CunningSpell, true);
                                    m.SetSpell(Teiravon.Spells.ArchCureSpell, true);
                                    m.SetSpell(Teiravon.Spells.ArchProtectionSpell, true);
                                    m.SetSpell(Teiravon.Spells.DispelSpell, true);
                                    m.SetSpell(Teiravon.Spells.MassCurseSpell, true);
                                    m.SetSpell(Teiravon.Spells.MassDispelSpell, true);

                                    m.Skills.Macing.Base = 80.0;
                                    m.Skills.Macing.Cap = 80.0;

                                    m.Skills.Ninjitsu.Cap = 60.0;
                                    m.Skills.Ninjitsu.Base = 60.0;

                                    m.Skills.Wrestling.Base = 40.0;
                                    m.Skills.Wrestling.Cap = 80.0;

                                    m.Skills.Tactics.Base = 100.0;
                                    m.Skills.Tactics.Cap = 100.0;

                                    m.Skills.Parry.Base = 60.0;
                                    m.Skills.Parry.Cap = 60.0;

                                    m.Skills.Alchemy.Base = 20.0;
                                    m.Skills.Alchemy.Cap = 100.0;

                                    m.Skills.Anatomy.Base = 30.0;
                                    m.Skills.Anatomy.Cap = 100.0;

                                    m.Skills.Camping.Base = 30.0;
                                    m.Skills.Camping.Cap = 100.0;

                                    m.Skills.Cooking.Base = 20.0;
                                    m.Skills.Cooking.Cap = 80.0;

                                    m.Skills.Fishing.Base = 30.0;
                                    m.Skills.Fishing.Cap = 100.0;

                                    m.Skills.Healing.Base = 100.0;
                                    m.Skills.Healing.Cap = 100.0;

                                    m.Skills.EvalInt.Base = 75.0;
                                    m.Skills.EvalInt.Cap = 75.0;

                                    m.Skills.Lumberjacking.Base = 10.0;
                                    m.Skills.Lumberjacking.Cap = 60.0;

                                    m.Skills.Magery.Base = 100.0;
                                    m.Skills.Magery.Cap = 100.0;

                                    m.Skills.Mining.Base = 10.0;
                                    if (m.IsDwarf())
                                        m.Skills.Mining.Cap = 90.0;
                                    else
                                        m.Skills.Mining.Cap = 60.0;

                                    m.Skills.MagicResist.Base = 100.0;
                                    m.Skills.MagicResist.Cap = 100.0;

                                    m.Skills.SpiritSpeak.Base = 100.0;
                                    m.Skills.SpiritSpeak.Cap = 100.0;

                                    m.Skills.TasteID.Base = 20.0;
                                    m.Skills.TasteID.Cap = 80.0;

                                    m.Skills.Inscribe.Base = 30.0;
                                    m.Skills.Inscribe.Cap = 30.0;

                                    #endregion
                                    break;
                                }
                            case "berserker":
                                {
                                    newOrc.PlayerClass = TeiravonMobile.Class.Berserker;
                                    #region berserkerSkills
                                    m.Skills.Ninjitsu.Base = 80.0;
                                    m.Skills.Ninjitsu.Cap = 80.0;

                                    m.Skills.Swords.Base = 100.0;
                                    m.Skills.Swords.Cap = 100.0;

                                    m.Skills.Macing.Base = 100.0;
                                    m.Skills.Macing.Cap = 100.0;

                                    m.Skills.Fencing.Base = 10.0;
                                    m.Skills.Fencing.Cap = 60.0;

                                    m.Skills.Wrestling.Base = 30.0;
                                    m.Skills.Wrestling.Cap = 100.0;

                                    m.Skills.Tactics.Base = 30.0;
                                    m.Skills.Tactics.Cap = 100.0;

                                    m.Skills.Parry.Base = 10.0;
                                    m.Skills.Parry.Cap = 60.0;

                                    m.Skills.ArmsLore.Base = 20.0;
                                    m.Skills.ArmsLore.Cap = 80.0;

                                    m.Skills.Camping.Base = 30.0;
                                    m.Skills.Camping.Cap = 100.0;

                                    m.Skills.Cooking.Base = 10.0;
                                    m.Skills.Cooking.Cap = 60.0;

                                    m.Skills.Fishing.Base = 30.0;
                                    m.Skills.Fishing.Cap = 100.0;

                                    m.Skills.Healing.Base = 10.0;
                                    m.Skills.Healing.Cap = 60.0;

                                    m.Skills.Lumberjacking.Base = 10.0;
                                    m.Skills.Lumberjacking.Cap = 60.0;

                                    m.Skills.Mining.Base = 10.0;
                                    if (m.IsDwarf())
                                        m.Skills.Mining.Cap = 90.0;
                                    else
                                        m.Skills.Mining.Cap = 60.0;

                                    m.Skills.MagicResist.Base = 30.0;
                                    m.Skills.MagicResist.Cap = 100.0;

                                    m.Skills.Tracking.Base = 20.0;
                                    m.Skills.Tracking.Cap = 80.0;

                                    m.Skills.Veterinary.Base = 20.0;
                                    m.Skills.Veterinary.Cap = 80.0;

                                    m.Skills.Anatomy.Base = 10.0;
                                    m.Skills.Anatomy.Cap = 60.0;

                                    m.Skills.Focus.Cap = 40.0;
                                    m.Skills.Focus.Base = 10.0;
                                    #endregion
                                    break;
                                }
                            case "dragoon":
                                {
                                    newOrc.PlayerClass = TeiravonMobile.Class.Dragoon;
                                    #region dragoonSkills

                                    m.Skills.Ninjitsu.Cap = 60.0;
                                    m.Skills.Ninjitsu.Base = 60.0;

                                    m.Skills.Swords.Base = 100.0;
                                    m.Skills.Swords.Cap = 100.0;

                                    m.Skills.Macing.Base = 105.0;
                                    m.Skills.Macing.Cap = 105.0;

                                    m.Skills.Fencing.Base = 30.0;
                                    m.Skills.Fencing.Cap = 100.0;

                                    m.Skills.Wrestling.Base = 30.0;
                                    m.Skills.Wrestling.Cap = 100.0;

                                    m.Skills.Tactics.Base = 100.0;
                                    m.Skills.Tactics.Cap = 100.0;

                                    m.Skills.Parry.Base = 60.0;
                                    m.Skills.Parry.Cap = 60.0;

                                    m.Skills.ArmsLore.Base = 80.0;
                                    m.Skills.ArmsLore.Cap = 80.0;

                                    m.Skills.Camping.Base = 30.0;
                                    m.Skills.Camping.Cap = 100.0;

                                    m.Skills.Cooking.Base = 10.0;
                                    m.Skills.Cooking.Cap = 60.0;

                                    m.Skills.Fishing.Base = 30.0;
                                    m.Skills.Fishing.Cap = 100.0;

                                    m.Skills.Healing.Base = 10.0;
                                    m.Skills.Healing.Cap = 60.0;

                                    m.Skills.Lumberjacking.Base = 10.0;
                                    m.Skills.Lumberjacking.Cap = 60.0;

                                    m.Skills.Mining.Base = 10.0;
                                    if (m.IsDwarf())
                                        m.Skills.Mining.Cap = 90.0;
                                    else
                                        m.Skills.Mining.Cap = 60.0;

                                    m.Skills.MagicResist.Base = 100.0;
                                    m.Skills.MagicResist.Cap = 100.0;

                                    m.Skills.Tracking.Base = 20.0;
                                    m.Skills.Tracking.Cap = 80.0;

                                    m.Skills.Veterinary.Base = 20.0;
                                    m.Skills.Veterinary.Cap = 80.0;

                                    m.Skills.Anatomy.Base = 60.0;
                                    m.Skills.Anatomy.Cap = 60.0;
                                    #endregion
                                    break;
                                }
                            default:
                                {
                                    newOrc.PlayerClass = TeiravonMobile.Class.Fighter;
                                    #region FighterSkills

                                    m.Skills.Ninjitsu.Cap = 60.0;
                                    m.Skills.Ninjitsu.Base = 60.0;

                                    m.Skills.Tactics.Base = 100.0;
                                    m.Skills.Tactics.Cap = 100.0;

                                    m.Skills.Swords.Base = 100.0;
                                    m.Skills.Swords.Cap = 100.0;

                                    m.Skills.Macing.Base = 100.0;
                                    m.Skills.Macing.Cap = 100.0;

                                    m.Skills.Fencing.Base = 30.0;
                                    m.Skills.Fencing.Cap = 100.0;

                                    m.Skills.Archery.Base = 90.0;
                                    m.Skills.Archery.Cap = 100.0;

                                    m.Skills.Wrestling.Base = 30.0;
                                    m.Skills.Wrestling.Cap = 100.0;

                                    m.Skills.Parry.Base = 100.0;
                                    m.Skills.Parry.Cap = 100.0;

                                    m.Skills.Anatomy.Base = 80.0;
                                    m.Skills.Anatomy.Cap = 80.0;

                                    m.Skills.MagicResist.Base = 100.0;
                                    m.Skills.MagicResist.Cap = 100.0;

                                    m.Skills.Camping.Base = 30.0;
                                    m.Skills.Camping.Cap = 100.0;

                                    m.Skills.Cartography.Base = 10.0;
                                    m.Skills.Cartography.Cap = 60.0;

                                    m.Skills.Cooking.Base = 10.0;
                                    m.Skills.Cooking.Cap = 60.0;

                                    m.Skills.Fishing.Base = 30.0;
                                    m.Skills.Fishing.Cap = 100.0;

                                    m.Skills.Lumberjacking.Base = 60.0;
                                    m.Skills.Lumberjacking.Cap = 60.0;

                                    m.Skills.Mining.Base = 10.0;
                                    if (m.IsDwarf())
                                        m.Skills.Mining.Cap = 90.0;
                                    else
                                        m.Skills.Mining.Cap = 60.0;

                                    m.Skills.ArmsLore.Base = 100.0;
                                    m.Skills.ArmsLore.Cap = 100.0;

                                    m.Skills.Healing.Base = 60.0;
                                    m.Skills.Healing.Cap = 60.0;
                                    #endregion
                                    break;
                                }
                        }
                        #region UniversalAppliers
                        if (m.Hair != null)
                            (m.Hair).Delete();

                        if (m.Beard != null)
                            (m.Beard).Delete();

                        OrcFace f = new OrcFace(m);
                        m.EquipItem(f);
                        f.OrcRank = OrcFace.Rank.Grunt;
                        m.Hue = Utility.RandomSnakeHue();
                        f.Hue = m.Hue;
                        
                        if (m.Skills.Macing.Cap == 100.0)
                            m.Skills.Macing.Cap = 105.0;
                        else
                        {
                            if (m.IsDragoon())
                            {
                                m.Skills.Macing.Cap = 105.0;
                                m.Skills.Macing.Base = 105.0;
                            }
                            else{
                                m.Skills.Macing.Cap = 90.0;
                                m.Skills.Macing.Base = 90.0;
                            }
                        }

                        newOrc.PlayerExp = 1306300;
                        newOrc.LanguageOrc = true;
                        LevelingFunctions.CheckLevelUp(newOrc);
                        Point3D loc = new Point3D(5779, 1135, -28);
                        m.MoveToWorld(loc, Map.Felucca);
                        m.LogoutLocation = loc;
                        m.LogoutMap = Map.Felucca;
                        #endregion
                    }
                    else
                    {
                        newOrc.Delete();
                        from.SendMessage("Failed, too many characters.");
                        return;
                    }
                }
            }
        }

        private class AddAFrostlingTarget : Target
        {
            private string m_Tag;

            public AddAFrostlingTarget(string tag)
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
                m_Tag = tag;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                TeiravonMobile newOrc = new TeiravonMobile();
                newOrc.AddItem(new Backpack());
                newOrc.BodyValue = 400;
                bool OrcAdded = false;
                if (o is TeiravonMobile)
                {
                    Account source = (Account)((TeiravonMobile)o).Account;
                    for (int i = 0; i < 7 && !OrcAdded; ++i)
                    {
                        if (source[i] == null)
                        {
                            source[i] = newOrc;
                            OrcAdded = true;
                        }
                    }
                    if (OrcAdded)
                    {
                        TeiravonMobile m = newOrc;
                        newOrc.Name = NameList.RandomName("orc");
                        newOrc.PlayerAlignment = TeiravonMobile.Alignment.ChaoticEvil;
                        newOrc.PlayerRace = TeiravonMobile.Race.Orc;
                        newOrc.NameHue = 1926;
                        switch (m_Tag)
                        {
                            case "berserker":
                                {
                                    newOrc.PlayerClass = TeiravonMobile.Class.Berserker;
                                    #region berserkerSkills
                                    m.Skills.Ninjitsu.Base = 80.0;
                                    m.Skills.Ninjitsu.Cap = 80.0;

                                    m.Skills.Swords.Base = 100.0;
                                    m.Skills.Swords.Cap = 100.0;

                                    m.Skills.Macing.Base = 100.0;
                                    m.Skills.Macing.Cap = 100.0;

                                    m.Skills.Fencing.Base = 10.0;
                                    m.Skills.Fencing.Cap = 60.0;

                                    m.Skills.Wrestling.Base = 30.0;
                                    m.Skills.Wrestling.Cap = 100.0;

                                    m.Skills.Tactics.Base = 30.0;
                                    m.Skills.Tactics.Cap = 100.0;

                                    m.Skills.Parry.Base = 10.0;
                                    m.Skills.Parry.Cap = 60.0;

                                    m.Skills.ArmsLore.Base = 20.0;
                                    m.Skills.ArmsLore.Cap = 80.0;

                                    m.Skills.Camping.Base = 30.0;
                                    m.Skills.Camping.Cap = 100.0;

                                    m.Skills.Cooking.Base = 10.0;
                                    m.Skills.Cooking.Cap = 60.0;

                                    m.Skills.Fishing.Base = 30.0;
                                    m.Skills.Fishing.Cap = 100.0;

                                    m.Skills.Healing.Base = 10.0;
                                    m.Skills.Healing.Cap = 60.0;

                                    m.Skills.Lumberjacking.Base = 10.0;
                                    m.Skills.Lumberjacking.Cap = 60.0;

                                    m.Skills.Mining.Base = 10.0;
                                    if (m.IsDwarf())
                                        m.Skills.Mining.Cap = 90.0;
                                    else
                                        m.Skills.Mining.Cap = 60.0;

                                    m.Skills.MagicResist.Base = 30.0;
                                    m.Skills.MagicResist.Cap = 100.0;

                                    m.Skills.Tracking.Base = 20.0;
                                    m.Skills.Tracking.Cap = 80.0;

                                    m.Skills.Veterinary.Base = 20.0;
                                    m.Skills.Veterinary.Cap = 80.0;

                                    m.Skills.Anatomy.Base = 10.0;
                                    m.Skills.Anatomy.Cap = 60.0;

                                    m.Skills.Focus.Cap = 40.0;
                                    m.Skills.Focus.Base = 10.0;
                                    #endregion
                                    break;
                                }
                            case "dragoon":
                                {
                                    newOrc.PlayerClass = TeiravonMobile.Class.Dragoon;
                                    #region dragoonSkills

                                    m.Skills.Ninjitsu.Cap = 60.0;
                                    m.Skills.Ninjitsu.Base = 60.0;

                                    m.Skills.Swords.Base = 100.0;
                                    m.Skills.Swords.Cap = 100.0;

                                    m.Skills.Macing.Base = 105.0;
                                    m.Skills.Macing.Cap = 105.0;

                                    m.Skills.Fencing.Base = 30.0;
                                    m.Skills.Fencing.Cap = 100.0;

                                    m.Skills.Wrestling.Base = 30.0;
                                    m.Skills.Wrestling.Cap = 100.0;

                                    m.Skills.Tactics.Base = 100.0;
                                    m.Skills.Tactics.Cap = 100.0;

                                    m.Skills.Parry.Base = 60.0;
                                    m.Skills.Parry.Cap = 60.0;

                                    m.Skills.ArmsLore.Base = 80.0;
                                    m.Skills.ArmsLore.Cap = 80.0;

                                    m.Skills.Camping.Base = 30.0;
                                    m.Skills.Camping.Cap = 100.0;

                                    m.Skills.Cooking.Base = 10.0;
                                    m.Skills.Cooking.Cap = 60.0;

                                    m.Skills.Fishing.Base = 30.0;
                                    m.Skills.Fishing.Cap = 100.0;

                                    m.Skills.Healing.Base = 10.0;
                                    m.Skills.Healing.Cap = 60.0;

                                    m.Skills.Lumberjacking.Base = 10.0;
                                    m.Skills.Lumberjacking.Cap = 60.0;

                                    m.Skills.Mining.Base = 10.0;
                                    if (m.IsDwarf())
                                        m.Skills.Mining.Cap = 90.0;
                                    else
                                        m.Skills.Mining.Cap = 60.0;

                                    m.Skills.MagicResist.Base = 100.0;
                                    m.Skills.MagicResist.Cap = 100.0;

                                    m.Skills.Tracking.Base = 20.0;
                                    m.Skills.Tracking.Cap = 80.0;

                                    m.Skills.Veterinary.Base = 20.0;
                                    m.Skills.Veterinary.Cap = 80.0;

                                    m.Skills.Anatomy.Base = 60.0;
                                    m.Skills.Anatomy.Cap = 60.0;
                                    #endregion
                                    break;
                                }
                            default:
                                {
                                    newOrc.PlayerClass = TeiravonMobile.Class.Fighter;
                                    #region FighterSkills

                                    m.Skills.Ninjitsu.Cap = 60.0;
                                    m.Skills.Ninjitsu.Base = 60.0;

                                    m.Skills.Tactics.Base = 100.0;
                                    m.Skills.Tactics.Cap = 100.0;

                                    m.Skills.Swords.Base = 100.0;
                                    m.Skills.Swords.Cap = 100.0;

                                    m.Skills.Macing.Base = 100.0;
                                    m.Skills.Macing.Cap = 100.0;

                                    m.Skills.Fencing.Base = 30.0;
                                    m.Skills.Fencing.Cap = 100.0;

                                    m.Skills.Archery.Base = 90.0;
                                    m.Skills.Archery.Cap = 100.0;

                                    m.Skills.Wrestling.Base = 30.0;
                                    m.Skills.Wrestling.Cap = 100.0;

                                    m.Skills.Parry.Base = 100.0;
                                    m.Skills.Parry.Cap = 100.0;

                                    m.Skills.Anatomy.Base = 80.0;
                                    m.Skills.Anatomy.Cap = 80.0;

                                    m.Skills.MagicResist.Base = 100.0;
                                    m.Skills.MagicResist.Cap = 100.0;

                                    m.Skills.Camping.Base = 30.0;
                                    m.Skills.Camping.Cap = 100.0;

                                    m.Skills.Cartography.Base = 10.0;
                                    m.Skills.Cartography.Cap = 60.0;

                                    m.Skills.Cooking.Base = 10.0;
                                    m.Skills.Cooking.Cap = 60.0;

                                    m.Skills.Fishing.Base = 30.0;
                                    m.Skills.Fishing.Cap = 100.0;

                                    m.Skills.Lumberjacking.Base = 60.0;
                                    m.Skills.Lumberjacking.Cap = 60.0;

                                    m.Skills.Mining.Base = 10.0;
                                    if (m.IsDwarf())
                                        m.Skills.Mining.Cap = 90.0;
                                    else
                                        m.Skills.Mining.Cap = 60.0;

                                    m.Skills.ArmsLore.Base = 100.0;
                                    m.Skills.ArmsLore.Cap = 100.0;

                                    m.Skills.Healing.Base = 60.0;
                                    m.Skills.Healing.Cap = 60.0;
                                    #endregion
                                    break;
                                }
                        }
                        #region UniversalAppliers
                        if (m.Hair != null)
                            (m.Hair).Delete();

                        if (m.Beard != null)
                            (m.Beard).Delete();

                        OrcFace f = new OrcFace(m);
                        m.EquipItem(f);
                        f.OrcRank = OrcFace.Rank.Grunt;
                        m.Hue = Utility.RandomSnakeHue();
                        f.Hue = m.Hue;

                        if (m.Skills.Macing.Cap == 100.0)
                            m.Skills.Macing.Cap = 105.0;
                        else
                        {
                            if (m.IsDragoon())
                            {
                                m.Skills.Macing.Cap = 105.0;
                                m.Skills.Macing.Base = 105.0;
                            }
                            else
                            {
                                m.Skills.Macing.Cap = 90.0;
                                m.Skills.Macing.Base = 90.0;
                            }
                        }

                        newOrc.PlayerExp = 1306300;
                        newOrc.LanguageOrc = true;
                        LevelingFunctions.CheckLevelUp(newOrc);
                        Point3D loc = new Point3D(5779, 1135, -28);
                        m.MoveToWorld(loc, Map.Felucca);
                        m.LogoutLocation = loc;
                        m.LogoutMap = Map.Felucca;
                        #endregion
                    }
                    else
                    {
                        newOrc.Delete();
                        from.SendMessage("Failed, too many characters.");
                        return;
                    }
                }
            }
        }

        [Usage("RemoveTag")]
        [Description("Remove a Tag from an account.")]
        private static void RemoveTag_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = e.Mobile as TeiravonMobile;

            if (!m_Player.Alive)
                return;

            if (e.Length >= 1)
            {
                string tag = e.Arguments[0];
                m_Player.Target = new RemoveTagTarget(tag);
            }
        }

		private class RemoveTagTarget : Target
		{
            private string m_Tag;

            public RemoveTagTarget(string tag)
                : base(-1, false, TargetFlags.None)
			{
				CheckLOS = false;
                m_Tag = tag;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is TeiravonMobile )
				{
                    Account source = (Account)((TeiravonMobile)o).Account;
                    source.RemoveTag(m_Tag);
					from.SendMessage( "Tag cleared" );
				}
			}
		}
        public class CharacterExporter
        {
            public class ExportCharCommand : BaseCommand
            {
                public ExportCharCommand()
                {
                    AccessLevel = AccessLevel.Administrator;
                    Supports = CommandSupport.Area | CommandSupport.Region | CommandSupport.Global | CommandSupport.Multi | CommandSupport.Single;
                    Commands = new string[] { "ExportChar" };
                    ObjectTypes = ObjectTypes.Mobiles;
                    Usage = "ExportChar";
                    Description = "Exports all Characters.";
                    ListOptimized = true;
                }

                public override void ExecuteList(CommandEventArgs e, ArrayList list)
                {
                    string filename = e.GetString(0);

                    ArrayList characters = new ArrayList();

                    for (int i = 0; i < list.Count; ++i)
                    {
                        if (list[i] is TeiravonMobile)
                        {
                            TeiravonMobile tav = (TeiravonMobile)list[i];
                            if (tav != null && !tav.Deleted)
                                characters.Add(tav);
                        }
                    }
                    NewExportChars(characters);

                    AddResponse(String.Format("{0} characters exported to Teiravon2\\CharacterTransfer\\ ", characters.Count.ToString()));

                    
                }

                public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
                {
                    if (e.Arguments.Length >= 1)
                        return true;

                    e.Mobile.SendMessage("Usage: " + Usage);
                    return false;
                }

                private void ExportChars(ArrayList characters)
                {
                    if (characters.Count == 0)
                        return;
                    for (int x = 0; x < characters.Count; x++)
                    {
                        Mobile m = (Mobile)characters[x];
                        ExportDummy example = new ExportDummy();
                        example.Name = m.RawName;
                        example.Acct = m.Account.ToString();
                        example.Race = ((Mobiles.TeiravonMobile)m).PlayerRace.ToString();
                        example.Hue = m.Hue;
                        example.Female = m.Female;
                        example.X = m.X;
                        example.Y = m.Y;
                        example.Z = m.Z;
                        example.Items = new DummyItem[m.Items.Count];
                        for (int i = 0; i < m.Items.Count; i++)
                        {
                            if (((Item)m.Items[i]).Layer > Layer.LastUserValid || ((Item)m.Items[i]).Layer == Layer.Backpack || ((Item)m.Items[i]).Layer == Layer.Unused_x9 || ((Item)m.Items[i]).Layer == Layer.Unused_xF || ((Item)m.Items[i]).Layer == Layer.Bank)
                                continue;
                            DummyItem d = new DummyItem();
                            d.Hue = ((Item)m.Items[i]).Hue;
                            d.Name = ((Item)m.Items[i]).Name;
                            d.Layer = (int)((Item)m.Items[i]).Layer;
                            d.ID = ((Item)m.Items[i]).ItemID;
                            example.Items[i] = d;
                        }
                        Type[] knownTypes = new Type[] { typeof(DummyItem) };
                        System.Xml.Serialization.XmlSerializer writer =
                        new System.Xml.Serialization.XmlSerializer(typeof(ExportDummy), knownTypes);
                        string path = "c:\\Teiravon2\\CharacterTransfer\\" + example.Name + ".xml";
                        System.IO.StreamWriter file = new System.IO.StreamWriter(path);
                        try { writer.Serialize(file, example); }
                        catch (Exception e) { Console.WriteLine(e.InnerException); }
                        file.Close();
                    }
                }
                private void NewExportChars(ArrayList characters)
                {
                    if (characters.Count == 0)
                        return;
                    for (int x = 0; x < characters.Count; x++)
                    {
                        string path = "c:\\Teiravon2\\CharacterTransfer\\" + ((Mobile)characters[x]).Name + ".xml";
                        XmlWriter writer = XmlWriter.Create(new System.IO.StreamWriter(path));
                        using (writer)
                        {
                            Mobile m = (Mobile)characters[x];
                            writer.WriteStartDocument();
                            writer.WriteStartElement("ExportDummy");
                            writer.WriteElementString("Acct", m.Account.ToString());
                            writer.WriteElementString("Name", m.RawName);
                            writer.WriteElementString("Race", ((Mobiles.TeiravonMobile)m).PlayerRace.ToString());
                            writer.WriteElementString("Hue", m.Hue.ToString());
                            string fem = m.Female.ToString();
                            writer.WriteElementString("Female", fem.ToLower());
                            writer.WriteStartElement("Items");
                            for (int i = 0; i < m.Items.Count; i++)
                            {
                                if (((Item)m.Items[i]).Layer > Layer.LastUserValid || ((Item)m.Items[i]).Layer == Layer.Backpack || ((Item)m.Items[i]).Layer == Layer.Unused_x9 || ((Item)m.Items[i]).Layer == Layer.Unused_xF || ((Item)m.Items[i]).Layer == Layer.Bank)
                                    continue;
                                writer.WriteStartElement("DummyItem");
                                writer.WriteElementString("ID", ((Item)m.Items[i]).ItemID.ToString());
                                writer.WriteElementString("Layer", ((int)((Item)m.Items[i]).Layer).ToString());
                                writer.WriteElementString("Hue", ((Item)m.Items[i]).Hue.ToString());
                                writer.WriteElementString("Name", ((Item)m.Items[i]).Name);
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                            writer.WriteElementString("X", m.X.ToString());
                            writer.WriteElementString("Y", m.Y.ToString());
                            writer.WriteElementString("Z", m.Z.ToString());
                            writer.WriteEndElement();
                        }
                    }
                }
            }
        }

        #region outdated
        [Usage("ExportChar")]
        [Description("Don't use this command.")]
        private static void ExportChar_OnCommand(CommandEventArgs e)
        {
            ExportDummy example = new ExportDummy();
            example.Name = e.Mobile.RawName;
            example.Acct = e.Mobile.Account.ToString();
            example.Race = ((Mobiles.TeiravonMobile)e.Mobile).PlayerRace.ToString();
            example.Hue = e.Mobile.Hue;
            example.Female = e.Mobile.Female;
            example.X = e.Mobile.X;
            example.Y = e.Mobile.Y;
            example.Z = e.Mobile.Z;
            example.Items = new DummyItem[e.Mobile.Items.Count];
            for (int i = 0; i < e.Mobile.Items.Count; i++)
            {
                if (((Item)e.Mobile.Items[i]).Layer > Layer.LastUserValid || ((Item)e.Mobile.Items[i]).Layer == Layer.Backpack || ((Item)e.Mobile.Items[i]).Layer == Layer.Unused_x9 || ((Item)e.Mobile.Items[i]).Layer == Layer.Unused_xF || ((Item)e.Mobile.Items[i]).Layer == Layer.Bank)
                    continue;
                DummyItem d = new DummyItem();
                d.Hue = ((Item)e.Mobile.Items[i]).Hue;
                d.Name = ((Item)e.Mobile.Items[i]).Name;
                d.Layer = (int)((Item)e.Mobile.Items[i]).Layer;
                d.ID = ((Item)e.Mobile.Items[i]).ItemID;
                example.Items[i] = d;
            }
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(ExportDummy));
            string path = "c:\\Teiravon2\\CharacterTransfer\\" + example.Name + ".xml";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            writer.Serialize(file, example);
            file.Close();
        }
        public void DoExportPlayer(TeiravonMobile m)
        {
            ExportDummy example = new ExportDummy();
            example.Name = m.RawName;
            example.Acct = m.Account.ToString();
            example.Race = ((Mobiles.TeiravonMobile)m).PlayerRace.ToString();
            example.Hue = m.Hue;
            example.Female = m.Female;
            example.X = m.X;
            example.Y = m.Y;
            example.Z = m.Z;
            example.Items = new DummyItem[20];
            for (int i = 0; i < m.Items.Count; i++)
            {
                if (((Item)m.Items[i]).Layer > Layer.LastUserValid || ((Item)m.Items[i]).Layer == Layer.Backpack || ((Item)m.Items[i]).Layer == Layer.Unused_x9 || ((Item)m.Items[i]).Layer == Layer.Unused_xF || ((Item)m.Items[i]).Layer == Layer.Bank)
                    continue;
                DummyItem d = new DummyItem();
                d.Hue = ((Item)m.Items[i]).Hue;
                d.Name = ((Item)m.Items[i]).Name;
                d.Layer = (int)((Item)m.Items[i]).Layer;
                d.ID = ((Item)m.Items[i]).ItemID;
                example.Items[i] = d;
            }
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(Server.Scripts.Commands.TAVGenCommands.ExportDummy));
            string path = "c:\\Teiravon2\\CharacterTransfer\\" + example.Name + ".xml";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            try { writer.Serialize(file, example); }
            catch (Exception e) { Console.WriteLine(e.InnerException); }
            file.Close();
        }
        #endregion

        [Serializable] 
        public class ExportDummy
        {
            public string Acct;
            public string Name;
            public string Race;
            public int Hue;
            public bool Female;
            public DummyItem[] Items;
            public int X;
            public int Y;
            public int Z;
            public ExportDummy() { } 
        }

        [Serializable] 
        public class DummyItem
        {
            public string Name;
            public int ID;
            public int Layer;
            public int Hue;
            public DummyItem() { }
        }

        [Usage("ImportChar")]
        [Description("Don't use this command.")]
        private static void ImportChar_OnCommand(CommandEventArgs e)
        {
            string path = "c:\\Teiravon2\\CharacterTransfer\\" + e.Mobile.RawName + ".xml";
            if (!File.Exists(path))
            {
                e.Mobile.SendMessage("NO STORED CHARACTER DATA FOUND."); return;
            }
            System.Xml.Serialization.XmlSerializer reader =
        new System.Xml.Serialization.XmlSerializer(typeof(ExportDummy));
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            ExportDummy imported = new ExportDummy();
            imported = (ExportDummy)reader.Deserialize(file);

            e.Mobile.Hue = imported.Hue;
            e.Mobile.Female = imported.Female;

            for (int i = 0; i < imported.Items.Length; i++)
            {
                // Layer 11 == Hair
                if (((DummyItem)imported.Items[i]).Layer == 11)
                {

                }
                // Layer 16 == Facial Hair
                if (((DummyItem)imported.Items[i]).Layer == 16)
                {

                }
            }
        }

        [Usage("ExportFeats")]
        [Description("Don't use this command.")]
        private static void ExportFeats_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile from = e.Mobile as TeiravonMobile;

            DataSet ds = new DataSet("AllCharFeats");
            ds.Tables.Add("Character");

            ds.Tables[0].Columns.Add("serial");
            ds.Tables[0].Columns.Add("0");
            ds.Tables[0].Columns.Add("1");
            ds.Tables[0].Columns.Add("2");
            ds.Tables[0].Columns.Add("3");
            ds.Tables[0].Columns.Add("4");
            ds.Tables[0].Columns.Add("5");
            ds.Tables[0].Columns.Add("6");
            ds.Tables[0].Columns.Add("7");
            ds.Tables[0].Columns.Add("8");
            ds.Tables[0].Columns.Add("9");
            ds.Tables[0].Columns.Add("10");

            ArrayList mobs = new ArrayList(World.Mobiles.Values);

            foreach (Mobile m in mobs)
            {
                if (m is TeiravonMobile)
                {
                    TeiravonMobile tav = m as TeiravonMobile;
                    DataRow dr = ds.Tables[0].NewRow();
                    string row;
                    dr["serial"] = (int)((Mobile)tav).Serial;

                    TeiravonMobile.Feats[] feats = tav.GetFeats();
                    for (int i = 0; i < 11; i++)
                    {
                        row = i.ToString();
                        dr[row] = (int)feats[i];
                    }
                    ds.Tables[0].Rows.Add(dr);
                }
            }

            bool file_error = false;
            string dirname = String.Format("{0}.feats", "saved");
            try
            {
                ds.WriteXml(dirname);
            }
            catch { file_error = true; }

            if (file_error)
            {
                if (from != null && !from.Deleted)
                    from.SendMessage("Error trying to save to file {0}", dirname);
            }
            else
            {
                if (from != null && !from.Deleted)
                    from.SendMessage("Saved feats to file {0}", dirname);
            }
        }

        [Usage("ImportFeats")]
        [Description("Don't use this command.")]
        private static void ImportFeats_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile from = e.Mobile as TeiravonMobile;

            string dirname = String.Format("{0}.feats", "saved");
            if (System.IO.File.Exists(dirname) == true)
            {
                FileStream fs = null;
                try
                {
                    fs = File.Open(dirname, FileMode.Open, FileAccess.Read);
                }
                catch { }

                if (fs == null)
                {
                    from.SendMessage("Unable to open {0} for loading", dirname);
                    return;
                }
                DataSet ds = new DataSet("AllCharFeats");

                bool fileerror = false;

                try
                {
                    ds.ReadXml(fs);
                }
                catch { fileerror = true; }

                // close the file
                fs.Close();

                if (fileerror)
                {
                    if (from != null && !from.Deleted)
                        from.SendMessage(33, "Error reading feats file {0}", dirname);
                    return;
                }
                if (ds.Tables != null && ds.Tables.Count > 0)
                {
                    // get the npc info
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Mobile owner;
                            
                            try
                            {
                                owner = World.FindMobile(int.Parse((string)dr["serial"]));
                                TeiravonMobile.Feats[] feats = ((TeiravonMobile)owner).GetFeats();
                                string row;
                                for (int i = 0; i < 11; i++)
                                {
                                    row = i.ToString();
                                    TeiravonMobile.Feats feat = TeiravonMobile.Feats.None;
                                    try { feat = (TeiravonMobile.Feats)int.Parse((string)dr[row]); }
                                    catch { }
                                    feats[i] = feat;
                                }
                                ((TeiravonMobile)owner).SetFeats(feats);
                            }
                            catch
                            {
                                from.SendMessage("I borked."); 
                            }
                           
                        }
                    }
                }

                if (from != null && !from.Deleted)
                    from.SendMessage("Loaded feats from file {0}", dirname);
            }
        }

        [Usage("Swim")]
        [Description("Dive into the water in an attempt to reach dry land.")]
        private static void Swim_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = e.Mobile as TeiravonMobile;

            if (!m_Player.Alive)
                return;

        }

        [Usage("Goal")]
        [Description("Describe your hopes and dreams.")]
        private static void Goal_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = e.Mobile as TeiravonMobile;
            
            if (m_Player.AccessLevel > AccessLevel.Player)
                m_Player.Target = new GoalTarget();
            else
                return;

        }
        private class GoalTarget : Target
        {
            private TeiravonMobile targ;

            public GoalTarget()
                : base(12, false, TargetFlags.None)
			{
			}

            protected override void OnTarget(Mobile from, object o)
            {
                if (!(o is TeiravonMobile))
                {
                    from.SendMessage("Only player characters have goals.");
                    return;
                }
                else
                {
                    targ = (TeiravonMobile)o;
                    
                }
            }
        }


        [Usage("Escape")]
        [Description("Attempt to free yourself from your shackles.")]
        private static void Escape_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = e.Mobile as TeiravonMobile;

            Item i = m_Player.FindItemOnLayer(Layer.TwoHanded);
            if (!m_Player.Alive)
            {
                m_Player.SendMessage("Some people might consider death an escape in itself, tradgic isn't it..");
                return;
            }
            else if (i != null || !(i is Manacles))
            {
                m_Player.SendMessage("You are not shackled, if you require further escapism you might try reading a book.");
                return;
            }
            else
            {
            	foreach ( Grab.GrabTimer grab in Grab.Grabbers.Values ){
                    if (grab.Grabbed == e.Mobile)
                    {
                        e.Mobile.SendMessage("The firm grasp of your captor would make escape impossible.");
                        return;
                    }
                }

                Manacles manacles = i as Manacles;
                manacles.StartEscape(m_Player);
            }

        }


        [Usage("PlayLute [Note#]")]
        [Description("Play a single note on your rune lute.")]
        private static void PlayLute_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            bool okay = false;
            Container c = m.Backpack;
            string[] notes = e.Arguments; 

            if (c != null)
            {
                RuneLute r = (RuneLute)c.FindItemByType(typeof(RuneLute));
                if (r != null)
                    okay = true;
            }
            if (!okay)
            {
                m.SendMessage("You havn't got a Rune Lute to play.");
                return;
            }
            if (e.Length >= 1)
            {
                for (int i = 0; i < e.Length; i++)
                {
                    int note = 0;
                    try
                    {
                        note = Int32.Parse(notes[i]);
                        //if (Int32.Parse(notes[i]) )
                        //{
                        switch (note)
                        {
                            case 1: { m.PlaySound(1028); } break;
                            case 2: { m.PlaySound(1033); } break;
                            case 3: { m.PlaySound(1038); } break;
                            case 4: { m.PlaySound(1040); } break;
                            case 5: { m.PlaySound(1044); } break;
                            case 6: { m.PlaySound(1021); } break;
                            case 7: { m.PlaySound(1025); } break;
                            case 8: { m.PlaySound(1029); } break;
                            case 9: { m.PlaySound(1034); } break;
                            case 10: { m.PlaySound(1039); } break;
                            case 11: { m.PlaySound(1041); } break;
                            case 12: { m.PlaySound(1045); } break;
                            case 13: { m.PlaySound(1022); } break;
                            case 14: { m.PlaySound(1026); } break;
                            case 15: { m.PlaySound(1030); } break;
                            case 16: { m.PlaySound(1031); } break;
                            case 17: { m.PlaySound(1036); } break;
                            case 18: { m.PlaySound(1042); } break;
                            case 19: { m.PlaySound(1046); } break;
                            case 20: { m.PlaySound(1023); } break;
                            case 21: { m.PlaySound(1032); } break;
                            case 22: { m.PlaySound(1037); } break;
                            case 23: { m.PlaySound(1043); } break;
                            case 24: { m.PlaySound(1047); } break;
                            case 25: { m.PlaySound(1024); } break;
                            default: { m.SendMessage("Unknown note."); } break;

                        }
                    }
                    catch
                    {
                        m.SendMessage("Unknown note.");
                    }
                }
            }
            else
                m.SendMessage("Play a note from 1 to 25, Play Chords by seperating each number with a space.");

        }


		[Usage( "SpeedBoost [true|false]" )]
		[Description( "Enables a speed boost for the invoker.  Disable with paramaters." )]
		private static void SpeedBoost_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( e.Length <= 1 )
			{
				if ( e.Length == 1 && !e.GetBoolean( 0 ) )
				{
					from.Send( SpeedBoost.Disabled );
					from.SendMessage( "Speed boost has been disabled." );
				}
				else
				{
					from.Send( SpeedBoost.Enabled );
					from.SendMessage( "Speed boost has been enabled." );
				}
			}
			else
			{
				from.SendMessage( "Format: SpeedBoost [true|false]" );
			}
		}

		public static void RemoveDisguise_OnCommand( CommandEventArgs e )
		{
			if ( DisguiseGump.IsDisguised( e.Mobile ) )
				DisguiseGump.OnDisguiseExpire( e.Mobile );
		}

        public static void CleanOld_OnCommand(CommandEventArgs e)
        {
            ArrayList mobs = new ArrayList(World.Mobiles.Values);
            foreach (Mobile mobile in mobs)
            {
                Account account = mobile.Account as Account;
                if (mobile.Player && account == null)
                    //mobile.NetState.Dispose();
                    mobile.Delete();
            }
        }
        [Usage("Nod")]
        [Description("Acknowledge a fellow player for their behavior or performance.")]
        private static void Nod_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile from = e.Mobile as TeiravonMobile;

            if (from.CantNod)
                return;
            from.Target = new NodTarget();
        }
        private class NodTarget : Target
        {
            TeiravonMobile targ;

            public NodTarget() : base(16, false, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object o)
            {
                if (!(o is TeiravonMobile))
                {
                    from.SendMessage("That would not benefit from your acolades.");
                    return;
                }
                else
                {
                    targ = (TeiravonMobile)o;
                    targ.GetNod((TeiravonMobile)from);
                }
            }
        }

        [Usage("ThrowPotion")]
        [Description("Flings a potion at the target to soak them with the contents.")]
        private static void ThrowPotion_OnCommand(CommandEventArgs e)
        {


            TeiravonMobile from = e.Mobile as TeiravonMobile;
            //STARTMOD: Teiravon
            if (from is TeiravonMobile)
            {
                TeiravonMobile m_Player = (TeiravonMobile)from;

                if ((m_Player.IsShapeshifter() || m_Player.IsForester()) && (m_Player.Shapeshifted || m_Player.IsShifted()))
                {
                    m_Player.SendMessage("You cannot throw things while shapeshifted.");
                    return;
                }
            }
            from.Target = new ThrowPotionTarget();
            from.SendMessage("What will you throw?");
        }

        private class ThrowPotionTarget : Target
        {
            Item targItem;

            public ThrowPotionTarget()
                : base(1, false, TargetFlags.None)
			{
			}

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BasePotion)
                {
                    if (o is TeiravonPotion)
                    {
                        if (!(o is FloatPotion || o is LesserFloatPotion))
                        {
                            from.SendMessage("This potion is too unstable to throw.");
                        }
                        else
                        {
                            targItem = (BasePotion)o;
                            from.Target = new PotionTarg(targItem as BasePotion);
                            from.SendMessage("Who will you throw the potion at?");
                        }
                    }

                    if (o is BasePoisonPotion)
                    {
                        from.SendMessage("The poison must be applied, simply throwing it will do nothing.");
                    }
                    else
                    {
                        targItem = (BasePotion)o;
                        from.Target = new PotionTarg(targItem as BasePotion);
                        from.SendMessage("Who will you throw the potion at?");
                    }
                }
               // else if (o is DyeTub)
               // {
               //     targItem = (DyeTub)o;
               // }
                else
                    from.SendMessage("You can't throw that.");
            }
        }

        private class PotionTarg : Target
        {
            Mobile targMob;
            BasePotion potion;
            public PotionTarg(BasePotion p)
                : base(8, false, TargetFlags.None)
			{
                potion = p;
			}


            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {

                    targMob = (Mobile)o;
                    Effects.SendMovingEffect(from, targMob, potion.ItemID, 10, 1, false, false);



                    if (potion.Amount > 1)
                    {
                        potion.Dupe(potion.Amount - 1);
                        potion.Amount = 1;
                    }
                    if (targMob.Backpack != null)
                        targMob.Backpack.AddItem(potion);
					else
						potion.Location = targMob.Location;

                    from.Animate(from.Mounted ? 26 : 9, 7, 1, true, false, 0); 
                    from.Emote("*Throws a potion*");

                    potion.Drink(targMob);
                }
                else
                    from.SendMessage("You can't throw a potion at that.");
            }
        }

        [Usage( "Con" )]
        [Description( "Consider the difficulty of a target mob")]
        private static void Con_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new ConTarget();

        }

        private class ConTarget : Target
        {
            Mobile targmob;

            public ConTarget( ) : base( 8, false, TargetFlags.None )
			{
			}

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    targmob = (Mobile)o;
                    int diff = TAVUtilities.CalculateLevel(from) - TAVUtilities.CalculateLevel(targmob);
                    from.SendMessage(TAVUtilities.ConsiderDif(diff));
                }
                else
                    from.SendMessage("You can't fight that.");
            }
        }

        [Usage("ConReport")]
        [Description("A verbose breakdown of Combat Rating")]
        private static void ConReport_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new ConReportTarget();

        }

        private class ConReportTarget : Target
        {
            Mobile targmob;

            public ConReportTarget()
                : base(8, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m = o as Mobile;
                    TAVUtilities.ReportLevel(from, m);
                }
                else
                    from.SendMessage("You can't fight that.");
            }
        }


        [Usage("GMCon")]
        [Description("Consider the difficulty of a target mob")]
        private static void GMCon_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new GMConTarget();

        }

        private class GMConTarget : Target
        {
            Mobile targmob;

            public GMConTarget()
                : base(8, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    targmob = (Mobile)o;
                    int lev = TAVUtilities.CalculateLevel(targmob);
                    from.SendMessage("Level : {0}", lev);
                }
                else
                    from.SendMessage("You can't fight that.");
            }
        }

        private static void DropTrops_OnCommand( CommandEventArgs e )
	{
		if ( e.Mobile.Backpack != null )
		{
			Caltrops ct = e.Mobile.Backpack.FindItemByType( typeof( Caltrops ), true ) as Caltrops;

			if ( ct != null )
			{
				e.Mobile.Emote( "*drops several caltrops on the ground*" );
				ct.MoveToWorld( e.Mobile.Location, e.Mobile.Map );
				ct.Movable = false;
			}
			else
				e.Mobile.SendMessage( "You're out of Caltrops." );
		}
	}

		[Usage( "Pray" )]
        [Description( "Commune with your God" )]
        private static void Pray_OnCommand( CommandEventArgs e )
        {
            e.Mobile.PrivateOverheadMessage(MessageType.Whisper, e.Mobile.SpeechHue, true,e.ArgString, e.Mobile.NetState);
			Server.Scripts.Commands.CommandHandlers.BroadcastMessage( AccessLevel.Counselor, e.Mobile.SpeechHue, String.Format( "[{0}] PRAYER: {1}", e.Mobile.Name, e.ArgString ) );
        }

	    [Usage( "DivineNotice" )]
		[Description( "Shows Divine Notice" )]
		private static void DivineNotice_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new DNoticeTarget();
		}

		private class DNoticeTarget : Target
		{
			Mobile targmob;
			
			public DNoticeTarget( ) : base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					targmob = (Mobile)o;
					Effects.SendLocationParticles( EffectItem.Create( targmob.Location, targmob.Map, EffectItem.DefaultDuration ), 0, 0, 0, 0, 0, 5060, 0 );
					Effects.PlaySound( targmob.Location, targmob.Map, 0x243 );

					Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( targmob.X - 6, targmob.Y - 6, targmob.Z + 15 ), targmob.Map ), targmob, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
					Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( targmob.X - 4, targmob.Y - 6, targmob.Z + 15 ), targmob.Map ), targmob, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
					Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( targmob.X - 6, targmob.Y - 4, targmob.Z + 15 ), targmob.Map ), targmob, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );

					Effects.SendTargetParticles( targmob, 0x375A, 35, 90, 0x00, 0x00, 9502, (EffectLayer)255, 0x100 );
				}
				else
					from.SendMessage( "Target a Mobile." );
			}
		}

		[Usage( "GM" )]
        [Description( "Toggles between player/gm access" )]
        private static void GM_OnCommand( CommandEventArgs e )
        {
            if ( ((Account)e.Mobile.Account).AccessLevel == AccessLevel.Player )
                return;
            else if ( e.Mobile.AccessLevel == AccessLevel.Player )
                e.Mobile.AccessLevel = ((Account)e.Mobile.Account).AccessLevel;
            else
                e.Mobile.AccessLevel = AccessLevel.Player;
        }


	        
	    [Usage( "ExpBoost amount" )]
		[Description( "Boosts target's exp by <amount>" )]
		private static void ExpBoost_OnCommand( CommandEventArgs e )
		{
			if ( e.Length >= 1 )
			{
				if ( e.Arguments[0] == "1" || e.Arguments[0] == "2" || e.Arguments[0] == "3" || e.Arguments[0] == "4" )
					e.Mobile.Target = new ExpBoostTarget( Int32.Parse( e.Arguments[0] ) );
				else
					e.Mobile.SendMessage( "Usage: [Expboost <level>, where level coresponds to 1, 2, 3 or 4" );
			}
			else
			{
                e.Mobile.SendMessage( "Usage: [Expboost <level>, where level coresponds to 1, 2, 3 or 4");
			}
		}

		private class ExpBoostTarget : Target
		{
			int boostamount;

			public ExpBoostTarget( int amount ) : base( -1, false, TargetFlags.None )
			{
				boostamount = amount;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is TeiravonMobile )
				{
					TeiravonMobile m_Player = (TeiravonMobile)o;

                    boostamount = ((boostamount * (boostamount + 1)) * 500) * m_Player.PlayerLevel;
                    Misc.Titles.AwardExp(m_Player, boostamount, true);
					m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have been rewarded with {0} exp!", boostamount );
					from.SendMessage( Teiravon.Colors.FeatMessageColor, "You have rewarded {0} with {1} exp.", m_Player.Name, boostamount );

				}
				else
					from.SendMessage( "Target a player." );
			}
		}

		[Usage( "Exp" )]
		[Description( "Shows your current Experience and Level" )]
		private static void Exp_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            m_Player.SendMessage("You currently have " + m_Player.PlayerExp + " experience and are level " + m_Player.PlayerLevel + ".");
            m_Player.SendMessage("You have an effective combat rating of : {0}",TAVUtilities.CalculateLevel(m_Player));

            if (m_Player.IsFighter() || m_Player.IsKensai() || m_Player.IsCavalier() || m_Player.IsMonk() || m_Player.IsStrider())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1000 - m_Player.PlayerExp) );
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2749 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (5449 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (9999 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (17199 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (35199 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (57199 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (83599 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (118699 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (194000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (352000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (562000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (812000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1244000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1922000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3024000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4342000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (5924000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7424000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7424000 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }
            else if (m_Player.IsUndeadHunter() || m_Player.IsPaladin() || m_Player.IsDeathKnight())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1250 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3350 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6500 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (11700 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (19800 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (39600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (63600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (92200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (130000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (203700 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (369600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (590100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (852600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1306300 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2018100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3175200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4559100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6220200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7720960 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7720960 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }

            else if (m_Player.IsCleric() || m_Player.IsDarkCleric() || m_Player.IsForester())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1250 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3350 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6500 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (11700 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (19800 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (39600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (63600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (92200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (130000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (203700 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (369600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (590100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (852600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1306300 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2018100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3175200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4559100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6220200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7720960 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7720960 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));
            }

            else if (m_Player.IsShapeshifter())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (12200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (19200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (39200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (63200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (96200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (133200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (205640 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (373120 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (595720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (860720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1318640 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2037320 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3205440 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4602520 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6279440 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7795200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7795200 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }

            else if (m_Player.IsRanger() || m_Player.IsArcher() || m_Player.IsMageSlayer())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1250 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3349 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6549 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (11699 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (19799 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (39599 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (63599 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (92199 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (129999 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (199820 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (362560 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (578860 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (836360 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1281320 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1979660 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3114720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4472260 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6101720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7572480 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7572480 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }

            else if (m_Player.IsNecromancer() || m_Player.IsAquamancer() || m_Player.IsAeromancer() || m_Player.IsGeomancer() || m_Player.IsPyromancer())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1500 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7550 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (13400 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (22400 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (44000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (70000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (100800 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (141300 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (209520 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (380160 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (606960 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (876960 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1343520 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2075760 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3265920 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4689360 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6397920 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7943680 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7943680 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }

            else if (m_Player.IsThief() || m_Player.IsAssassin())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (900 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2510 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (5030 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (9320 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (16160 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (33440 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (55040 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (81440 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (117080 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (199820 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (362560 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (578860 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (836360 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1281320 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1979660 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3114720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4472260 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6101720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7572480 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7572480 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }

            else if (m_Player.IsBerserker() || m_Player.IsDragoon() || m_Player.IsSavage())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1000 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2749 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (5449 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (9999 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (17199 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (35199 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (57199 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (83599 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (118699 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (199820 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (362560 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (578860 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (836360 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1281320 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1979660 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3114720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4472260 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6101720 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7572480 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((7572480 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }

            else if (m_Player.IsTailor() || m_Player.IsBlacksmith() || m_Player.IsAlchemist() || m_Player.IsWoodworker() || m_Player.IsTinker() || m_Player.IsCook() || m_Player.IsMerchant())
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (250 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1250 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3250 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (7300 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (14300 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (27100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (43700 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (72450 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (114450 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (168450 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (252950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (378950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (539950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (771950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1077950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1455950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1896950 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2438450 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3108450 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((3108450 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));


            }

            else
            {
                if (m_Player.PlayerLevel == 1)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1250 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 2)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3350 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 3)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6500 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 4)
                    m_Player.SendMessage("You will need {0} more experience to level.", (11700 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 5)
                    m_Player.SendMessage("You will need {0} more experience to level.", (20700 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 6)
                    m_Player.SendMessage("You will need {0} more experience to level.", (40500 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 7)
                    m_Player.SendMessage("You will need {0} more experience to level.", (64500 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 8)
                    m_Player.SendMessage("You will need {0} more experience to level.", (93100 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 9)
                    m_Player.SendMessage("You will need {0} more experience to level.", (130900 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 10)
                    m_Player.SendMessage("You will need {0} more experience to level.", (213400 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 11)
                    m_Player.SendMessage("You will need {0} more experience to level.", (387200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 12)
                    m_Player.SendMessage("You will need {0} more experience to level.", (618200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 13)
                    m_Player.SendMessage("You will need {0} more experience to level.", (893200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 14)
                    m_Player.SendMessage("You will need {0} more experience to level.", (1368400 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 15)
                    m_Player.SendMessage("You will need {0} more experience to level.", (2114200 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 16)
                    m_Player.SendMessage("You will need {0} more experience to level.", (3326400 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 17)
                    m_Player.SendMessage("You will need {0} more experience to level.", (4776600 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 18)
                    m_Player.SendMessage("You will need {0} more experience to level.", (6616400 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel == 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", (8116400 - m_Player.PlayerExp));
                if (m_Player.PlayerLevel > 19)
                    m_Player.SendMessage("You will need {0} more experience to level.", ((8116400 * Math.Pow(1.75, (m_Player.PlayerLevel - 19))) - m_Player.PlayerExp));

            }
		}

		[Usage( "Alloys" )]
		[Description( "Alloy crafting Gump" )]
		private static void Alloys_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile from = (TeiravonMobile)e.Mobile;
			if (from.HasFeat( TeiravonMobile.Feats.RacialCrafting ) ||( from.HasFeat( TeiravonMobile.Feats.BlacksmithTraining) && from.HasFeat( TeiravonMobile.Feats.MasterCraftsman) ) )
			{
				bool anvil;
				bool forge;
				Server.Engines.Craft.DefBlacksmithy.CheckAnvilAndForge( from, 2, out anvil, out forge );
				if (anvil && forge)
				{
					Item hammercheck = from.FindItemOnLayer(Layer.FirstValid);
					if (hammercheck is SmithHammer)
						from.SendGump( new AlloyGump( from, 1, 1, null, null, null, null, null, null ) );
					else
						from.SendMessage("You must have a smith hammer equipped for this");
				}
				else
					from.SendMessage("You must be near an anvil and forge to do this");
			}
			else
				from.SendMessage("You must be a master smith to do this!");
		}
		
		[Usage( "Hunger" )]
		[Description( "Shows your current Hunger level" )]
		private static void Hunger_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile from = (TeiravonMobile)e.Mobile;
			int m_hunger = from.Hunger;

			if (m_hunger == 0)
				from.SendMessage ("You are starving");
			else if (m_hunger > 0 && m_hunger < 4)
				from.SendMessage ("You are extremely hungry");
			else if (m_hunger > 3 && m_hunger < 7)
				from.SendMessage ("You are very hungry");
			else if (m_hunger > 6 && m_hunger < 13)
				from.SendMessage("You are hungry");
			else if (m_hunger > 12 && m_hunger < 16)
				from.SendMessage("You are satiatied");
			else if (m_hunger > 15 && m_hunger < 20)
				from.SendMessage("You are full");
			else
				from.SendMessage("You are stuffed");
		}


		[Usage( "Language" )]
		[Description( "Shows what languages you understand and which is currently in use" )]
		private static void Language_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile from = (TeiravonMobile)e.Mobile;
			int Languages = from.Languages;
			int CurrentLang = from.CurrentLanguage;

			if ( e.Length >= 1 )
			{
				if ( from.IsShifted() )
				{
					from.SendMessage( "You can only speak animal language (lupine) in this form" );
					return;
				}

				switch ( e.Arguments[0].ToLower() )
				{
					case "celestial":
						if ( from.AccessLevel >= AccessLevel.GameMaster )
						{
							from.SendMessage( "Your language is now Celestial." );
							from.CurrentLanguage = TeiravonMobile.LCelestial;

						}
						else
							from.SendMessage( "You don't know Celestial." );
						break;

					case "common":
						if ( (Languages & TeiravonMobile.LCommon) != 0 )
						{
							from.SendMessage( "Your language is now Common." );
							from.CurrentLanguage = TeiravonMobile.LCommon;

						}
						else
							from.SendMessage( "You don't know Common." );
						break;

					case "elven":
						if ( (Languages & TeiravonMobile.LElven) != 0 )
						{
							from.SendMessage( "Your language is now Elven." );
							from.CurrentLanguage = TeiravonMobile.LElven;

						}
						else
							from.SendMessage( "You don't know Elven." );

						break;

					case "drow":
						if ( (Languages & TeiravonMobile.LDrow) != 0 )
						{
							from.SendMessage( "Your language is now Drow." );
							from.CurrentLanguage = TeiravonMobile.LDrow;
						}
						else
							from.SendMessage( "You don't know Drow." );

						break;

					case "dwarven":
						if ( (Languages & TeiravonMobile.LDwarven) != 0 )
						{
							from.SendMessage( "Your language is now Dwarven." );
							from.CurrentLanguage = TeiravonMobile.LDwarven;
						}
						else
							from.SendMessage( "You don't know Dwarven." );

						break;

					case "orc":
						if ( (Languages & TeiravonMobile.LOrc) != 0 )
						{
							from.SendMessage( "Your language is now Orc." );
							from.CurrentLanguage = TeiravonMobile.LOrc;
						}
						else
							from.SendMessage( "You don't know Orc." );

						break;

					case "lupine":
						if ( (Languages & TeiravonMobile.LLupine) != 0 )
						{
							from.SendMessage( "Your language is now Lupine." );
							from.CurrentLanguage = TeiravonMobile.LLupine;

						}
						else
							from.SendMessage( "You don't know Lupine." );

						break;

					default:
						from.SendMessage( "That isn't a known language" );
						break;
				}
			}
			else
			{
				from.SendMessage( "You are currently speaking: " );

				if ( (CurrentLang & TeiravonMobile.LCommon) != 0 )
					from.SendMessage( "Common" );

				if ( (CurrentLang & TeiravonMobile.LElven) != 0 )
					from.SendMessage( 2591, "Elven" );

				if ( (CurrentLang & TeiravonMobile.LDrow) != 0 )
					from.SendMessage( 2581, "Drow" );

				if ( (CurrentLang & TeiravonMobile.LDwarven) != 0 )
					from.SendMessage( 2939, "Dwarven" );

				if ( (CurrentLang & TeiravonMobile.LOrc) != 0 )
					from.SendMessage( 2598, "Orc" );

				if ( (CurrentLang & TeiravonMobile.LLupine) != 0 )
					from.SendMessage( 2287, "Lupine" );

				from.SendMessage( " " );

				from.SendMessage( "You have knowledge of the following: " );

				if ( (Languages & TeiravonMobile.LCommon) != 0 )
					from.SendMessage( "Common" );

				if ( (Languages & TeiravonMobile.LElven) != 0 )
					from.SendMessage( 2591, "Elven" );

				if ( (Languages & TeiravonMobile.LDrow) != 0 )
					from.SendMessage( 2581, "Drow" );

				if ( (Languages & TeiravonMobile.LDwarven) != 0 )
					from.SendMessage( 2939, "Dwarven" );

				if ( (Languages & TeiravonMobile.LOrc) != 0 )
					from.SendMessage( 2598, "Orc" );

				if ( (Languages & TeiravonMobile.LLupine) != 0 )
					from.SendMessage( 2287, "Lupine" );
			}
		}

        [Usage("Faith")]
        [Description("Shows what gods you could follow and which you worship")]
        private static void Faith_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile from = (TeiravonMobile)e.Mobile;

            if (from.Faith != TeiravonMobile.Deity.None)
            {
                from.SendMessage("You worship: {0} ", from.Faith == TeiravonMobile.Deity.None ? "Nothing." : from.Faith.ToString());
                return;
            }
            if (Faith.Contains(from))
            {
                from.SendMessage("Be not fickle in your worship, you must wait before changing your faith.");
                return;
            }

            if (e.Length >= 1)
            {
                switch (e.Arguments[0].ToLower())
                {
                    case "adalia":
                        if (from.IsHuman())
                        {
                            from.SendMessage("You follow Adalia.");
                            from.Faith = TeiravonMobile.Deity.Adalia;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of Adalia.");
                        break;

                    case "talathas":
                        if (from.IsHuman())
                        {
                            from.SendMessage("You follow Talathas.");
                            from.Faith = TeiravonMobile.Deity.Talathas;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of Talathas.");
                        break;

                    case "kamalini":
                        if (from.IsHuman())
                        {
                            from.SendMessage("You follow Kamalini.");
                            from.Faith = TeiravonMobile.Deity.Kamalini;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of Kamalini.");
                        break;

                    case "kinarugi":
                         if (from.IsHuman())
                        {
                            from.SendMessage("You follow Kinarugi.");
                            from.Faith = TeiravonMobile.Deity.Kinarugi;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of Kinarugi.");
                        break;

                    case "lloth":
                        if (from.IsDrow())
                        {
                            from.SendMessage("You follow Lloth.");
                            from.Faith = TeiravonMobile.Deity.Lloth;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of Lloth.");
                        break;

                    case "gruumsh":
                        if (from.IsOrc())
                        {
                            from.SendMessage("You follow Gruumsh.");
                            from.Faith = TeiravonMobile.Deity.Gruumsh;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of Gruumsh.");

                        break;

                    case "saerin":
                        if (from.IsDwarf())
                        {
                            from.SendMessage("You follow Saerin.");
                            from.Faith = TeiravonMobile.Deity.Saerin;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of Saerin.");
                        
                            break;

                    case "valar":
                        if (from.IsElf())
                        {
                            from.SendMessage("You follow the Valar.");
                            from.Faith = TeiravonMobile.Deity.Valar;

                        AddFaith(from);}
                        else
                            from.SendMessage("You don't know the teachings of the Valar.");

                        break;


                    case "jareth":
                        if (from.IsGoblin())
                        {
                            from.SendMessage("You follow the Goblin King!");
                            from.Faith = TeiravonMobile.Deity.Jareth;
                            AddFaith(from);
                        }
                        break;

                    case "cultist":
                            from.SendMessage("You reject the known gods of the world to follow another.");
                            from.Faith = TeiravonMobile.Deity.Cultist;
                            AddFaith(from);
                        break;

                    default:
                        from.SendMessage("That isn't a known god");
                        break;

                }
            }
            else
            {
                from.SendMessage("You are currently worshipping: {0} ",  from.Faith == TeiravonMobile.Deity.None ? "Nothing." : from.Faith.ToString());

                from.SendMessage("You have knowledge of the following: ");

                if (from.IsHuman())
                {
                    from.SendMessage("Adalia");
                    from.SendMessage("Talathas");
                    from.SendMessage("Kamalini");
                    from.SendMessage("Kinarugi");
                    //from.SendMessage("Narindun");
                }

                if (from.IsElf())
                    from.SendMessage(2591, "Valar");

                if (from.IsDrow())
                    from.SendMessage(2581, "Lloth");

                if (from.IsDwarf())
                    from.SendMessage(2939, "Saerin");

                if (from.IsOrc())
                    from.SendMessage(2598, "Gruumsh");

                if (from.IsGoblin())
                    from.SendMessage(2763, "The Goblin King");
                //if (from.IsUndead())
                 //   from.SendMessage(2228, "Occido");
                
                from.SendMessage(2287, "Cultist");
            }
        }


		[Usage( "Status" )]
		[Description( "Hides you on the webstatus page" )]
		private static void Status_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			if ( m_Player.ShowStatus )
				m_Player.ShowStatus = false;
			else
			{
				m_Player.ShowStatus = true;
				m_Player.SendMessage( "You should only use this when absolutely required." );
			}

			m_Player.SendMessage( "You're now {0} on the status page.", ( m_Player.ShowStatus ) ? "visible" : "invisible" );
		}

		[Usage( "Climb" )]
		[Description( "Allows you to climb trees" )]
		private static void Climb_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			if ( m_Player.Hits < m_Player.MaxHits - 20 )
				m_Player.SendMessage( "You're too weak..." );
			else if ( m_Player.Stam < m_Player.MaxStam )
				m_Player.SendMessage( "You're too fatigued..." );
			else if ( m_Player.Mounted )
				m_Player.SendMessage( "You can't figure out how to get the mount up the tree..." );
			else
				m_Player.Target = new ClimbTarget();
		}

		private class ClimbTarget : Target
		{
			public ClimbTarget() : base( 1, true, TargetFlags.None )
			{
				CheckLOS = true;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is StaticTarget )
				{
					StaticTarget m_Target = (StaticTarget)o;
					TeiravonMobile m_Player = (TeiravonMobile) from;

					if ( m_Target.ItemID >= 3274 && m_Target.ItemID <= 3277 || m_Target.ItemID == 3280 || m_Target.ItemID == 3283 || m_Target.ItemID == 3286 || m_Target.ItemID == 3288 || m_Target.ItemID == 3290 || m_Target.ItemID == 3293 || m_Target.ItemID == 3296 || m_Target.ItemID == 3299 || m_Target.ItemID == 3302 || m_Target.ItemID == 3320 || m_Target.ItemID == 3326 || m_Target.ItemID == 3329 || m_Target.ItemID >= 3393 && m_Target.ItemID <= 3499  )
					{
						m_Player.Location = m_Target.Location;
						m_Player.Z = m_Target.Z;
						m_Player.ClearHands();
					}
					else
						m_Player.SendMessage( "You can only climb trees." );
				}
				else
					from.SendMessage( "You can only climb trees." );
			}
		}


		private static void LinkKey_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			m.SendMessage( "Target a key" );
			m.Target = new LinkKeyTarget();
		}

		private class LinkKeyTarget : Target
		{
			public LinkKeyTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Key )
				{
					Key key = (Key)o;
					key.KeyValue = (uint)((int)key.Serial);
					from.Target = new LinkKeyTarget2( key.KeyValue );
					from.SendMessage( "Target a door or chest to link the key to" );
				}
				else
					from.SendMessage( "That is not a key." );
			}
		}

		private class LinkKeyTarget2 : Target
		{
			private uint KeyValue;

			public LinkKeyTarget2( uint keyvalue ) : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
				KeyValue = keyvalue;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is BaseDoor )
				{
					BaseDoor door = (BaseDoor)o;
					door.KeyValue = KeyValue;
					from.SendMessage( "The key has been linked to that door." );
				}
				else if ( o is LockableContainer )
				{
					LockableContainer box = (LockableContainer)o;
					box.KeyValue = KeyValue;
					from.SendMessage( "The key has been linked to that container." );
				}
				else
					from.SendMessage( "That is not a door or a lockable container." );
			}
		}

		private static void MoveSpawn_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			m.Target = new MoveSpawnTarget();
		}

		private class MoveSpawnTarget : Target
		{
			public MoveSpawnTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Spawner )
				{
					Spawner spawner = (Spawner)o;
					spawner.InitSpawnMap2();
				}
				else
					from.SendMessage( "Invalid target." );
			}
		}

        private static void HearAllNods_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (TeiravonMobile.HearAllNods.IndexOf(m_Player) > -1)
            {
                TeiravonMobile.HearAllNods.Remove(m_Player);
                m_Player.SendMessage("HearAllNods Off");
            }
            else
            {
                TeiravonMobile.HearAllNods.Add(m_Player);
                m_Player.SendMessage("HearAllNods On");
            }
        }

		private static void HearAllParty_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			if ( TeiravonMobile.HearAllPartyChat.IndexOf( m_Player ) > -1 )
			{
				TeiravonMobile.HearAllPartyChat.Remove( m_Player );
				m_Player.SendMessage( "HearAllPartyChat Off" );
			}
			else
			{
				TeiravonMobile.HearAllPartyChat.Add( m_Player );
				m_Player.SendMessage( "HearAllPartyChat On" );
			}
		}

		private static void Moo2_OnCommand( CommandEventArgs e )
		{

			ArrayList list = new ArrayList();

			foreach ( Item item in World.Items.Values )
			{
				if ( item is Spawner )
				{
					Spawner temp = (Spawner) item;

					if ( temp.SpawnMap == SpawnMapsE.OneUnderdark
					|| temp.SpawnMap == SpawnMapsE.TwoUnderdark
					|| temp.SpawnMap == SpawnMapsE.ThreeUnderdark
					|| temp.SpawnMap == SpawnMapsE.UnderdarkPathOne
					|| temp.SpawnMap == SpawnMapsE.UnderdarkPathTwo
					|| temp.SpawnMap == SpawnMapsE.UnderdarkTeleportPathOne
					|| temp.SpawnMap == SpawnMapsE.UnderdarkTeleportPathTwo
					|| temp.SpawnMap == SpawnMapsE.UnderdarkTeleportPathThree
					|| temp.SpawnMap == SpawnMapsE.UnderdarkTeleportPathFour
					|| temp.SpawnMap == SpawnMapsE.UnderdarkTeleportPathFive
					|| temp.SpawnMap == SpawnMapsE.UnderdarkOne
					|| temp.SpawnMap == SpawnMapsE.UnderdarkTwo
					|| temp.SpawnMap == SpawnMapsE.UnderdarkThree
					|| temp.SpawnMap == SpawnMapsE.UnderdarkFour
					|| temp.SpawnMap == SpawnMapsE.TerathanKeep )

					list.Add( item );
				}
			}

			for ( int i = 0; i < list.Count; i++ )
			{
				Item m = (Item)list[i];
				m.Delete();

				World.Broadcast( 0x35, true, "Wiping underdark spawn #" + i.ToString() );
			}
		}

		private static void HearAll_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			if ( !m_Player.HearAll ) {
				m_Player.HearAll = true;
				m_Player.SendMessage( "HearAll On" );
			}
			else
			{
				m_Player.HearAll = false;
				m_Player.SendMessage( "HearAll Off" );
			}
		}

		private static void Moo_OnCommand( CommandEventArgs e )
		{
			int counter = 0;
			SpawnMapsE themap;

			SpawnMapsE[] spawnmaps = new SpawnMapsE[48];

			spawnmaps[0] = SpawnMapsE.SouthwestEdanaTerritory;
			spawnmaps[1] = SpawnMapsE.NortheastEdanaTerritory;
			spawnmaps[2] = SpawnMapsE.NorthwestSouthernWilds;
			spawnmaps[3] = SpawnMapsE.NortheastSouthernWilds;
			spawnmaps[4] = SpawnMapsE.CentralSouthernWilds;
			spawnmaps[5] = SpawnMapsE.SouthwestNorthernWilds;
			spawnmaps[6] = SpawnMapsE.NorthwestNorthernWilds;
			spawnmaps[7] = SpawnMapsE.NortheastNorthernWilds;
			spawnmaps[8] = SpawnMapsE.NorthwestCentralWilds;
			spawnmaps[9] = SpawnMapsE.CentralCentralWilds;
			spawnmaps[10] = SpawnMapsE.SoutheastCentralWilds;
			spawnmaps[11] = SpawnMapsE.TilvertonTerritory;
			spawnmaps[12] = SpawnMapsE.SouthSouthernWilds;
			spawnmaps[13] = SpawnMapsE.SouthernForest;
			spawnmaps[14] = SpawnMapsE.MurkwoodForest;
			spawnmaps[15] = SpawnMapsE.ArcticanForest;
			spawnmaps[16] = SpawnMapsE.GreatDesert;
			spawnmaps[17] = SpawnMapsE.ForestOfWyrms;
			spawnmaps[18] = SpawnMapsE.GreatSwamp;
			spawnmaps[19] = SpawnMapsE.SarangraveFlat;
			spawnmaps[20] = SpawnMapsE.WestgateTerritory;
			spawnmaps[21] = SpawnMapsE.MountainPass;
			spawnmaps[22] = SpawnMapsE.Rainforest;
			spawnmaps[23] = SpawnMapsE.BlackrockSwamp;
			spawnmaps[24] = SpawnMapsE.BlackrockTerritory;
			spawnmaps[25] = SpawnMapsE.Andalain;
			spawnmaps[26] = SpawnMapsE.Spiderfang;
			spawnmaps[27] = SpawnMapsE.SouthronWastes;
			spawnmaps[28] = SpawnMapsE.NorthwestGreatPlains;
			spawnmaps[29] = SpawnMapsE.SoutheastGreatPlains;
			spawnmaps[30] = SpawnMapsE.GiantWood;
			spawnmaps[31] = SpawnMapsE.Ruinwash;
			spawnmaps[32] = SpawnMapsE.ElasianFields;
			spawnmaps[33] = SpawnMapsE.OneUnderdark;
			spawnmaps[34] = SpawnMapsE.TwoUnderdark;
			spawnmaps[35] = SpawnMapsE.ThreeUnderdark;
			spawnmaps[36] = SpawnMapsE.UnderdarkPathOne;
			spawnmaps[37] = SpawnMapsE.UnderdarkPathTwo;
			spawnmaps[38] = SpawnMapsE.UnderdarkTeleportPathOne;
			spawnmaps[39] = SpawnMapsE.UnderdarkTeleportPathTwo;
			spawnmaps[40] = SpawnMapsE.UnderdarkTeleportPathThree;
			spawnmaps[41] = SpawnMapsE.UnderdarkTeleportPathFour;
			spawnmaps[42] = SpawnMapsE.UnderdarkTeleportPathFive;
			spawnmaps[43] = SpawnMapsE.UnderdarkOne;
			spawnmaps[44] = SpawnMapsE.UnderdarkTwo;
			spawnmaps[45] = SpawnMapsE.UnderdarkThree;
			spawnmaps[46] = SpawnMapsE.UnderdarkFour;
			spawnmaps[47] = SpawnMapsE.TerathanKeep;

			//String[] spawntypes = new String[48];
			ArrayList spawntypes = new ArrayList();

			spawntypes.Add( NPC.ForestLevel1 );
			spawntypes.Add( NPC.ForestLevel1 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel1 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel3 );
			spawntypes.Add( NPC.ForestLevel3 );
			spawntypes.Add( NPC.SnowLevel1 );
			spawntypes.Add( NPC.DesertLevel2 );
			spawntypes.Add( NPC.ForestLevel5 );
			spawntypes.Add( NPC.ForestLevel3 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel3 );
			spawntypes.Add( NPC.DungeonLevel1 );
			spawntypes.Add( NPC.ForestLevel3 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel1 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel3 );
			spawntypes.Add( NPC.DesertLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel3 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.ForestLevel2 );
			spawntypes.Add( NPC.DungeonLevel2 ); // 33
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel1 );// 35
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel1 ); // Linked to SR
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel2 ); // 40
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel1 ); // Linked to SR
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel2 ); // 45
			spawntypes.Add( NPC.DungeonLevel2 );
			spawntypes.Add( NPC.DungeonLevel3 );

			for ( int h = 33; h < 48; h++ )
			{
				themap = spawnmaps[h];

				for ( int i = 0; i < 20; i++ )
				{
					ArrayList m_Spawns = new ArrayList();
					Spawner m_Spawner = new Spawner();

					for ( int j = 0; j < Utility.RandomMinMax( 1, 2 ); j++ )
					{
						String[] temp = (String[])spawntypes[h];
						m_Spawns.Add( temp[ Utility.Random( temp.Length ) ] );
					}

					m_Spawner.CreaturesName = m_Spawns;

					m_Spawner.MinDelay = TimeSpan.FromMinutes( 3 );
					m_Spawner.MaxDelay = TimeSpan.FromMinutes( 5 );
					m_Spawner.HomeRange = 15;
					m_Spawner.Count = Utility.RandomMinMax( 1, 2 );
					m_Spawner.MaxSpawns = Utility.RandomMinMax( 5, 10 );
					m_Spawner.MaxRoamCount = Utility.RandomMinMax( 7, 15 );
					m_Spawner.SpawnMap = themap;

					m_Spawner.Running = true;
					m_Spawner.InitSpawnMap();
					m_Spawner.Respawn();

					counter++;

					World.Broadcast( 0x35, true, "Creating spawn #" + counter.ToString() + " Number " + h.ToString() + " of 47 maps" );
				}
			}
		}

		private static void Z42_OnCommand( CommandEventArgs e )
		{
			BoundingBoxPicker.Begin( e.Mobile, new BoundingBoxCallback( Z42_Callback ), typeof( Item ) );
		}

		private static void Z42_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
		{
			Rectangle2D rect = new Rectangle2D( start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1 );

			foreach( Item item in map.GetItemsInBounds( rect ) )
			{
				/*if ( item.ItemID == 1306 )
					item.ItemID = 1173;*/

				if ( item.ItemID == 1173 )
					item.Hue = 2827;
				else
					item.Hue = 2577;

				/*if ( item.Z == 22 )
					item.Z = 20;

				if ( item.Z == 44 )
					item.Z = 40;

				if ( item.Z == 66 )
					item.Z = 60;

				if ( item.Z == 88 )
					item.Z = 80;

				if ( item.Z == 110 )
					item.Z = 100;*/
			}
		}

		[Usage( "xSay" )]
		[Description( "Makes target say <args>" )]
		private static void xSay_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new xSayTarget( e.Arguments );
		}

		private class xSayTarget : Target
		{
			private string m_Say;

			public xSayTarget( string[] StrToSay ) : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;

				for ( int i = 0; i < StrToSay.Length; ++i )
				{
					m_Say += StrToSay[i];
					m_Say += " ";
				}
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					Mobile m_Player = (Mobile)o;
					m_Player.PublicOverheadMessage( MessageType.Regular, m_Player.SpeechHue, true, m_Say );
				}
				else if ( o is Item )
				{
					Item m_Item = (Item)o;
					m_Item.PublicOverheadMessage( MessageType.Regular, 0x3B2, true, m_Say );
				}
			}
		}


		[Usage( "xEmote" )]
		[Description( "Makes target emote <args>" )]
		private static void xEmote_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new xEmoteTarget( e.Arguments );
		}

		private class xEmoteTarget : Target
		{
			private string m_Say;

			public xEmoteTarget( string[] StrToSay ) : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;

				for ( int i = 0; i < StrToSay.Length; ++i )
				{
					m_Say += StrToSay[i];
					m_Say += " ";
				}
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					Mobile m_Player = (Mobile)o;
					m_Player.PublicOverheadMessage( MessageType.Emote, m_Player.EmoteHue, true, m_Say );
				}
				else if ( o is Item )
				{
					Item m_Item = (Item)o;
                    m_Item.PublicOverheadMessage(MessageType.System, 0x2B2, true, m_Say);
				}
			}
		}

		[Usage( "EnableAllSpells" )]
		[Description( "Enables all spells" )]
		private static void EnableAllSpells_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile from = (TeiravonMobile)e.Mobile;

			from.SetAllSpells( true );
		}

        private static void RemoveFeats_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            m_Player.Target = new RemoveFeatsTarget();
        }

        private class RemoveFeatsTarget : Target
        {
            public RemoveFeatsTarget()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is TeiravonMobile)
                {
                    TeiravonMobile m_Target = (TeiravonMobile)o;
                    from.SendGump(new RemoveFeatGump(m_Target));
                }
            }
        }

		private static void ClearFeats_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			m_Player.Target = new ClearFeatsTarget();
		}

		private class ClearFeatsTarget : Target
		{
			public ClearFeatsTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is TeiravonMobile )
				{
					TeiravonMobile m_Target = (TeiravonMobile)o;

					TeiravonMobile.Feats[] feats = m_Target.GetFeats();

					for ( int i = 0; i < feats.Length; i++ )
					{
						feats[i] = TeiravonMobile.Feats.None;
					}

					m_Target.SetFeats( feats );

					from.SendMessage( "Feats cleared" );
				}
			}
		}


		private static void ListFeats_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			m_Player.Target = new ListFeatsTarget();
		}

		private class ListFeatsTarget : Target
		{

			public ListFeatsTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is TeiravonMobile )
				{
					TeiravonMobile m_Target = (TeiravonMobile)o;
					TeiravonMobile.Feats[] feats = m_Target.GetFeats();

					for ( int i = 0; i < feats.Length; i++ )
					{
						if ( feats[i] != TeiravonMobile.Feats.None )
							from.SendMessage( "{0} has the feat {1}", m_Target.Name, feats[i] );
					}
				}
			}
		}

		private static void DeletePlayer_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			m_Player.Target = new DeletePlayerTarget();
		}

		private class DeletePlayerTarget : Target
		{
			public DeletePlayerTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is TeiravonMobile )
				{
					TeiravonMobile m_Target = (TeiravonMobile)o;

					if ( m_Target.NetState != null )
						m_Target.NetState.Dispose();

					m_Target.Delete();

					from.SendMessage( "Player deleted." );
				}
				else
					from.SendMessage( "This command does not work on that." );
			}
		}

		private static void Bash_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			m_Player.Target = new BashTarget();
		}

		private class BashTarget : Target
		{

			public BashTarget() : base( -1, false, TargetFlags.None )
			{
				CheckLOS = true;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is BaseTreasureChest )
				{
					TeiravonMobile m_from = (TeiravonMobile)from;
					BaseTreasureChest cont = (BaseTreasureChest) o;
					int rskill = cont.RequiredSkill;
					int str = from.Str;
					int bashrnd = Utility.RandomMinMax(1,100);

					if (!(m_from.InRange( cont.GetWorldLocation(), 2 )))
					{
						from.SendMessage( "You are too far away!");
						return;
					}

					if (!cont.Locked)
					{
						from.SendMessage( "This chest is already unlocked." );
						return;
					}

					if (rskill < str/3)
					{
						if (bashrnd > 80)
						{
							// Nothing Happens
							from.SendMessage( "Your attempt to open the chest failed." );
						}
						else if (bashrnd > 60)
						{
							// Trap set off
							from.SendMessage( "Your attempt to open the chest failed and you set off a trap!" );
							cont.ExecuteTrap(m_from);
						}
						else if (bashrnd > 40)
						{
							// Contents destroyed
							from.SendMessage( "Your attempt to open the chest failed and you destroy the chest!" );
							cont.Delete();
						}
						else
						{
							// Success
							from.SendMessage( "Your succeed in destroying the lock!" );
							cont.Locked = false;
						}
					}
					else
					{
						if (bashrnd > 40)
						{
							// Nothing Happens
							from.SendMessage( "Your attempt to open the chest failed." );
						}
						else if (bashrnd > 20)
						{
							// Trap set off
							from.SendMessage( "Your attempt to open the chest failed and you set off a trap!" );
							cont.ExecuteTrap(m_from);
						}
						else
						{
							// Contents destroyed
							from.SendMessage( "Your attempt to open the chest failed and you destroy the chest!" );
							cont.Delete();
						}
					}
				}
				else
					from.SendMessage( "You can only bash treasure chests." );
			}
		}



		private static void InitSpawns_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
			ArrayList bob = new ArrayList();

			foreach ( Item item in World.Items.Values )
			{
				if ( item is Spawner )
					bob.Add( item );
			}


			for ( int i = 0; i < bob.Count; i++ )
			{
				i += 1;

				Spawner blah = (Spawner)bob[i];

				World.Broadcast( 0x35, true, "Remapping spawn #{0} of {1}", i, bob.Count );
				blah.InitSpawnMap();
			}
		}
		
         private static void Encounters_OnCommand( CommandEventArgs e )
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            
            if ( m_Player.EncounterMode == TeiravonMobile.EncounterModes.AlwaysAvoid )
            {
                m_Player.EncounterMode = TeiravonMobile.EncounterModes.AlwaysEncounter;
                m_Player.SendMessage( "You won't try and avoid encounters." );
                
            } else if ( m_Player.EncounterMode == TeiravonMobile.EncounterModes.AlwaysEncounter ) {
                
                m_Player.EncounterMode = TeiravonMobile.EncounterModes.Prompt;
                m_Player.SendMessage( "You will now be prompted for action." );
                
            } else if ( m_Player.EncounterMode == TeiravonMobile.EncounterModes.Prompt ) {
                
                m_Player.EncounterMode = TeiravonMobile.EncounterModes.AlwaysAvoid;
                m_Player.SendMessage( "You will always try to avoid encounters." );
            }
        } 
         
		private static void RPBC_OnCommand( CommandEventArgs e )
        {
            e.Mobile.SendGump( new BroadcastGump() );
        }
        
        private class BroadcastGump : Server.Gumps.Gump
        {
            public enum Buttons
            {
                Send = 1,
                Cancel = 2,
                RangeEntry = 3,
                MessageEntry = 4
            }

            public BroadcastGump() : base( 0, 0 )
            {
                Closable = true;
                Dragable = true;

                AddPage( 0 );

                AddBackground( 22, 26, 689, 164, 9270 );

                AddLabel( 266, 40, 100, @"In-Character Broadcast System" );
                AddLabel( 46, 66, 100, @"Range:" );
                AddLabel( 46, 100, 100, @"Message:" );
            
                AddButton( 286, 154, 247, 248, (int)Buttons.Send, GumpButtonType.Reply, 0 );
                AddButton( 374, 154, 241, 242, (int)Buttons.Cancel, GumpButtonType.Reply, 0 );
            
                AddTextEntry( 92, 67, 46, 20, 100, (int)Buttons.RangeEntry, @"5000" );
                AddTextEntry( 110, 100, 579, 20, 100, (int)Buttons.MessageEntry, @"" );
            }
            
            public override void OnResponse( NetState sender, RelayInfo info )
            {
                if ( info.ButtonID == (int)Buttons.Send )
                {
                    int range = 5000;
                    string message = info.TextEntries[ 1 ].Text;
                    
                    try {
                        range = Int32.Parse( info.TextEntries[ 0 ].Text );
                        
                        if ( range > 5000 )
                            range = 5000;
                    
                    for ( int i = 0; i < NetState.Instances.Count; i++ )
                    {
                        if ( ((NetState)NetState.Instances[ i ]).Mobile.InRange( sender.Mobile.Location, range ) )
                            ((NetState)NetState.Instances[ i ]).Mobile.SendMessage( sender.Mobile.SpeechHue, message );
                    }
                    } catch { }
                }
            }
        }

        public class RemoveFeatGump : BaseGridGump
        {
            private TeiravonMobile m_From;
            private TeiravonMobile.Feats[] feats;

            public RemoveFeatGump(TeiravonMobile from)
                : base(30, 30)
            {
                m_From = from;
                feats = m_From.GetFeats();
                Closable = true;
                Render();

            }

            public void Render()
            {
                AddNewPage();

                /* Header */
                AddEntryHeader(20);
                AddEntryHtml(140, Center("Remove Feats"));
                AddEntryHeader(20);

                /* Options */
                for (int i = 0; i < m_From.GetFeats().Length; ++i)
                {
                        if (feats[i] != TeiravonMobile.Feats.None)
                        {
                            AddNewLine();

                            AddEntryLabel(20 + OffsetSize + 140, feats[i].ToString());
                            AddEntryButton(20, ArrowRightID1, ArrowRightID2, GetButtonID(1, 0, i), ArrowRightWidth, ArrowRightHeight);
                        }
                }
                FinishPage();
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID > 0)
                {
                    feats[info.ButtonID - 1] = TeiravonMobile.Feats.None;
                    m_From.SetFeats(feats);

                    sender.Mobile.SendGump(new RemoveFeatGump(m_From));
                }
                else
                    return;
            }
        }
	}
}
