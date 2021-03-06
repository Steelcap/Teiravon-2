using System;
using Server;
using Server.Accounting;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Teiravon;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
    public class CharGenBook : Item
    {
        [Constructable]
        public CharGenBook()
            : base(0x2252)
        {
            Name = "Begin Character Generation";
            Weight = 10000.0;
        }

        public CharGenBook(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            TeiravonMobile m = (TeiravonMobile)from;
            TeiravonMobile m_Player = m;					// Lazy.

            m_Player.OBody = 0;

            m_Player.Str = 10;
            m_Player.Hits = m_Player.Str;
            m_Player.MaxHits = 10;

            m_Player.Int = 10;
            m_Player.Mana = m_Player.Int;
            m_Player.MaxMana = 10;

            m_Player.Dex = 10;
            m_Player.Stam = m_Player.Dex;
            m_Player.MaxStam = 10;

            m_Player.BodyMod = 0;
            //			m_Player.Hue = 0x83ea;

            m_Player.Fame = 0;
            m_Player.Karma = 0;
            m_Player.KarmaLocked = false;

            for (int i = 0; i <= 53; ++i)
            {
                m_Player.Skills[i].Base = 0;
                m_Player.Skills[i].Cap = 0;
            }

            m_Player.SkillsCap = 10000;

            m_Player.PlayerRace = TeiravonMobile.Race.None;
            m_Player.PlayerClass = TeiravonMobile.Class.None;
            m_Player.PlayerAlignment = TeiravonMobile.Alignment.None;
            m_Player.PlayerExp = 0;
            m_Player.PlayerLevel = 1;
            m_Player.SetAllSpells(false);
            m_Player.SetLanguages(false);
            m_Player.SetLanguages(0x1, true);
            m_Player.RemainingFeats = 0;

            TeiravonMobile.Feats[] feats = m_Player.GetFeats();

            for (int i = 0; i < feats.Length; i++)
            {
                feats[i] = TeiravonMobile.Feats.None;
            }

            m_Player.SetFeats(feats);


            bool gump = m.CloseAllGumps();

            if (!gump)
                m.NetState.Dispose();

            m.Hidden = true;

            m_Player.SendGump(new AlignmentGump(m));
        }
    }

    public class AlignmentGump : Gump
    {
        int offsetx = 70;
        int offsety = 35;
        int buttonoffsetx = 40;
        int buttonoffsety = 35;
        int buttonup = 210;
        int buttondown = 211;
        int buttonid = 0;
        int page = 0;
        int hue = 1152;
        int alignoffsetx = 278;
        int alignoffsety = 43;
        int alignwidth = 335;
        int alignheight = 315;

        static string open = "<basefont size=\"8\" color=\"#0f0f0f\">";
        static string close = "</basefont>";

        string LawfulGood = String.Format("{0}You believe that an orderly, strong society with a well-organized government can work to make life better for the majority of the people. You believe in laws and justice. Laws must be obeyed to ensure the quality of life. Therefore, you strive for those things that will bring the greatest benefit to the most people and cause the least harm. Paladins are a good example of this alignment.{1}", open, close);
        string NeutralGood = String.Format("{0}You believe that a balance of forces is important, but that the concerns of law and chaos do not moderate the need for good. Since the universe is vast with many creatures with different goals, a determined pursuit of good will not upset the balance; it may even maintain it. If fostering good means supporting organized society, then that is what must be done. If good can only come about through the overthrow of existing social order, then so be it. A baron who violates the orders of his king to destroy something he sees as evil is an example.{1}", open, close);
        string ChaoticGood = String.Format("{0}A strong individualist marked by a streak of kindness and benevolence. You believe in all the virtues of goodness and right, but have little use for laws and regulations. You have no use for people who \"try to push folk around and tell them what to do\". Your actions are guided by your own moral compass, which may not be in perfect agreement with the rest of society. A brave frontiersman forever moving on as settlers follow in his wake is an example.{1}", open, close);
        string LawfulNeutral = String.Format("{0}Order and organization is of paramount importance to you. You believe in a strong, well-ordered government, whether that government is a tyranny or benevolent democracy. The benefits of organization and regimentation outweigh any moral questions raised by your actions. An inquisitor determined to ferret out any traitors at any cost or a soldier who never questions his orders are good examples.{1}", open, close);
        string TrueNeutral = String.Format("{0}You believe in the ultimate balance of forces, and refuse to see actions as either good or evil. Since the majority of the people in the world make judgements, true neutral characters are extremely rare. You do your best to avoid siding with forces of either good or evil, law or chaos. It is your duty to see that all of these forces remain in balanced contention. A true neutral druid might join the local barony to put down a tribe of evil gnolls, only to drop out or switch sides when the gnolls were brought to the brink of destruction. He would seek to prevent either side from becoming too powerful.{1}", open, close);
        string ChaoticNeutral = String.Format("{0}You believe that there is no order to anything, including your own actions. With this as a guiding principle, you tend to follow whatever whim strikes you at the moment. Good and evil are irrelevant when making a decision. You are extremely difficult to deal with. Such characters have been known to cheerfully and for no apparent purpose gamble away everything they have on a single roll of the dice. They are totally unreliable. In fact, the only reliable thing about them is that they cannot be relied upon! A lunatic or a madman would be a good example.{1}", open, close);
        string LawfulEvil = String.Format("{0}You believe in using society and its laws to benefit yourself. Structure and organization elevate those who deserve to rule as well as provide a clearly defined hierarchy between master and servant. If someone is hurt or suffers because of a law that benefits you, too bad. You obey laws out of fear of punishment, because you may be forced to honor an unfavorable contract or oath you have made. You are very careful about giving your word. You will only break your word if you can do it legally. An iron-fisted tyrant and a devious, greedy merchant are examples.{1}", open, close);
        string NeutralEvil = String.Format("{0}You are concerned with yourself and your own advancement. You have no particular objection to working with others or going it on your own. If there is a quick and easy way to gain profit, whether it be legal, questionable, or obviously illegal, you will take advantage of it. You have no qualms about betraying your friends and companions for personal gain. You typically base your allegiance on power and money, which make you quite receptive to bribes. An unscrupulous mercenary, a common thief are examples.{1}", open, close);
        string ChaoticEvil = String.Format("{0}You are the bane to all that is good and organized. You are motivated only by the desire for personal gain and pleasure. You see absolutely nothing wrong with taking whatever you want by whatever means possible. Laws and governments are tools for weaklings unable to fend for themselves. The strong have a right to take what they want, and the weak are to be exploited. When chaotic evil characters band together, they can only be held together by a strong leader capable of bullying his underlings into obedience. Since leadership is based on raw power, a leader is likely to be replaced at the first sign of weakness by anyone who can take his position from him by any method. Bloodthirsty buccaneers or monsters of low intelligence are fine examples.{1}", open, close);

        private int GetOffset() { offsety += 25; return offsety; }
        private int GetButtonOffset() { buttonoffsety += 25; return offsety; }
        private int GetButtonID() { buttonid += 1; return buttonid; }
        private int GetPage() { page += 1; return page; }

        public AlignmentGump(Mobile from)
            : base(175, 60)
        {
            Closable = false;
            Disposable = false;
            Resizable = false;

            TeiravonMobile m = (TeiravonMobile)from;

            m.CloseGump(typeof(AlignmentGump));

            AddPage(0);
            AddBackground(0, 0, 220, 400, 2600);
            AddBackground(250, 0, 385, 400, 2600);

            AddLabel(55, 20, 150, "Teiravon: Rise of Empires");
            AddLabel(55, 335, 150, "Alignment Selection");

            AddLabel(offsetx, GetOffset(), hue, "Lawful Good");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "Neutral Good");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "Chaotic Good");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "Lawful Neutral");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "True Neutral");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "Chaotic Neutral");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "Lawful Evil");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "Neutral Evil");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            AddLabel(offsetx, GetOffset(), hue, "Chaotic Evil");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, GetButtonID(), GumpButtonType.Page, GetPage());

            page = 0;

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, LawfulGood, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);			// 10

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, NeutralGood, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, ChaoticGood, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, LawfulNeutral, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, TrueNeutral, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, ChaoticNeutral, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);			// 15

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, LawfulEvil, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, NeutralEvil, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);

            AddPage(GetPage());
            AddHtml(alignoffsetx, alignoffsety, alignwidth, alignheight, ChaoticEvil, false, true);
            AddButton(82, 295, 2074, 2075, GetButtonID(), GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            TeiravonMobile m = (TeiravonMobile)sender.Mobile;

            switch (info.ButtonID)
            {
                case 10:
                    m.PlayerAlignment = TeiravonMobile.Alignment.LawfulGood;
                    m.Karma = 7000;
                    break;

                case 11:
                    m.PlayerAlignment = TeiravonMobile.Alignment.NeutralGood;
                    m.Karma = 5000;
                    break;

                case 12:
                    m.PlayerAlignment = TeiravonMobile.Alignment.ChaoticGood;
                    m.Karma = 3000;
                    break;

                case 13:
                    m.PlayerAlignment = TeiravonMobile.Alignment.LawfulNeutral;
                    m.Karma = 1000;
                    break;

                case 14:
                    m.PlayerAlignment = TeiravonMobile.Alignment.TrueNeutral;
                    m.Karma = 0;
                    break;

                case 15:
                    m.PlayerAlignment = TeiravonMobile.Alignment.ChaoticNeutral;
                    m.Karma = -1000;
                    break;

                case 16:
                    m.PlayerAlignment = TeiravonMobile.Alignment.LawfulEvil;
                    m.Karma = -3000;
                    m.KarmaLocked = true;
                    break;

                case 17:
                    m.PlayerAlignment = TeiravonMobile.Alignment.NeutralEvil;
                    m.Karma = -5000;
                    m.KarmaLocked = true;
                    break;

                case 18:
                    m.PlayerAlignment = TeiravonMobile.Alignment.ChaoticEvil;
                    m.Karma = -7000;
                    m.KarmaLocked = true;
                    break;

                default:
                    m.SendMessage("You must select an alignment to continue.");
                    return;
            }

            m.SendGump(new RaceGump(m));
        }
    }

    public class RaceGump : Gump
    {
        int offsetx = 80;
        int offsety = 35;
        int buttonoffsetx = 50;
        int buttonoffsety = 35;
        int buttonup = 210;
        int buttondown = 211;
        int buttonid = 0;
        int page = 0;
        int hue = 1152;

        private int GetOffset() { offsety += 25; return offsety; }
        private int GetButtonOffset() { buttonoffsety += 25; return offsety; }
        private int GetButtonID() { buttonid += 1; return buttonid; }
        private int GetPage() { page += 1; return page; }

        public RaceGump(Mobile from)
            : base(300, 60)
        {
            Closable = false;
            Disposable = false;
            Resizable = false;

            TeiravonMobile m = (TeiravonMobile)from;

            m.CloseGump(typeof(RaceGump));

            AddPage(0);
            AddBackground(0, 0, 220, 340, 2600);

            AddLabel(61, 20, 150, "Teiravon: Rise of Empires");
            AddLabel(65, 295, 150, "Race Selection");
            //AddButton( 77, 265, 2031, 2032, 7, GumpButtonType.Reply, 0 );		// Help button?

            // Can be any alignment
            AddLabel(offsetx, GetOffset(), 0, "Human");
            AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 1, GumpButtonType.Reply, 0);

            if (m.IsGood() || m.IsNeutral())
            {
                AddLabel(offsetx, GetOffset(), 0, "Elf");
                AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 2, GumpButtonType.Reply, 0);

                AddLabel(offsetx, GetOffset(), 0, "Dwarf");
                AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 3, GumpButtonType.Reply, 0);

                AddLabel(offsetx, GetOffset(), 0, "Gnome");
                AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 7, GumpButtonType.Reply, 0);
            }

            if (m.IsEvil())
            {
                AddLabel(offsetx, GetOffset(), 0, "Drow");
                AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 4, GumpButtonType.Reply, 0);

                AddLabel(offsetx, GetOffset(), 0, "Orc");
                AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 5, GumpButtonType.Reply, 0);

                AddLabel(offsetx, GetOffset(), 0, "Goblin");
                AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 8, GumpButtonType.Reply, 0);
                //  AddLabel(offsetx, GetOffset(), 0, "Undead");
                //  AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 10, GumpButtonType.Reply, 0);
                //AddLabel( offsetx, GetOffset(), 0, "Duergar" );
                //AddButton( buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 6, GumpButtonType.Reply, 0 );
            }
            if (m.IsNeutral())
            {
                AddLabel(offsetx, GetOffset(), 0, "Frostling");
                AddButton(buttonoffsetx, GetButtonOffset(), buttonup, buttondown, 9, GumpButtonType.Reply, 0);
            }

        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            TeiravonMobile m = (TeiravonMobile)sender.Mobile;
            string race = "";
            string description = "";
            string modifiers = "";
            string[] minmax = new string[3];		// str dex int
            string[] statcaps = new string[3];		// str dex int
            string[] modcaps = new string[3];	// str dex int

            switch (info.ButtonID)
            {
                case 1:
                    race = "Human";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Humans are the most well-rounded character in the realms.<br> Being a human probably appears difficult at first, but with proper management, they can become as powerful as any other race present.<br> When a class/occupation is chosen, Humans will receive no bonuses other than the ones that come with the occupation.<br>";
                    modifiers = "<basefont size=\"+5\"> Bonus Skill Cap at 1st Level<br> Race Specific Feats<br> Race Specific Items<br No Restrictions";
                    minmax[0] = "10/30";
                    minmax[1] = "10/30";
                    minmax[2] = "10/30";

                    statcaps[0] = "220";
                    statcaps[1] = "260";
                    statcaps[2] = "205";

                    modcaps[0] = "30/50";
                    modcaps[1] = "30/50";
                    modcaps[2] = "30/50";

                    break;

                case 2:
                    race = "Elf";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Elves are nearly as wide-ranging a set of beings as Humans are. Several variations of the race are found throughout the lands. From the Moon Elves to the Drow, they all have the slight frame which bespeaks great agility.<br> Choosing this Race, you receive the benefits inherent to them such as enhanced dexterity and the ability to see in the dark, not to mention an attunement to nature few other races enjoy.<br>";
                    modifiers = "<basefont size=\"+5\"> Infravision<br> Race Specific Feats<br> Race Specific Items<br>";
                    minmax[0] = "10/30";
                    minmax[1] = "20/40";
                    minmax[2] = "15/35";

                    statcaps[0] = "195";
                    statcaps[1] = "325";
                    statcaps[2] = "225";

                    modcaps[0] = "30/40";
                    modcaps[1] = "40/60";
                    modcaps[2] = "35/55";

                    break;

                case 3:
                    race = "Dwarf";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Dwarves are a hearty race of smaller stature humanoids. They revel in the mining of precious metals and gemstones. This, of course, is done before, during and after taking long pulls from their ever-present ale bottles.<br> A strong and fearsome race, their dexterity does take a penalty due to their preference for strong drink and from the deformities countless centuries of Mining and Blacksmithy have done to their bodies.<br> Dwarves make for excellent Warriors.<br>";
                    modifiers = "<basefont size=\"+5\"> Infravision<br> Race Specific Feats<br> Race Specific Items<br> Magic Resistance +30%<br> Mining +30%<br> Detect Hidden Passages<br> Cannot cast Arcane spells<br> Inept at Woodslore.";
                    minmax[0] = "20/35";
                    minmax[1] = "10/20";
                    minmax[2] = "15/25";

                    statcaps[0] = "245";
                    statcaps[1] = "250";
                    statcaps[2] = "180";

                    modcaps[0] = "40/60";
                    modcaps[1] = "30/45";
                    modcaps[2] = "30/40";

                    break;

                case 4:
                    race = "Drow";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Drow are another of the Elf family of races. Reclusive and secretive, Drow have an agenda all their own. Not much is known of them other than their fascination with all things of a dark nature and their superb skill with traversing the shadows.<br> Many of the characteristics of their Elven cousins are present; characteristics Drow do not share with their cousins are the white hair, dark skin, and their preference to bladed weapons over the bow.<br> They take great pains to craft Scimitars of exceeding quality and magical enhancements are quite common even in daggers. Drow can become Swordsmen with no peer and due to their dark nature, a Drow Assassin is one of the most feared entities in the lands.<br> They can become powerful Necromancers.<br> Although their craftsmanship is without peer, Drow crafts do not survive long on the surface due to their dark enchantments.<br>";
                    modifiers = "<basefont size=\"+5\"> Infravision<br> Race Specific Feats<br> Race Specific Items<br>";
                    minmax[0] = "10/30";
                    minmax[1] = "20/40";
                    minmax[2] = "15/35";

                    statcaps[0] = "195";
                    statcaps[1] = "325";
                    statcaps[2] = "225";

                    modcaps[0] = "30/40";
                    modcaps[1] = "40/60";
                    modcaps[2] = "35/55";

                    break;

                case 5:
                    race = "Orc";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Orcs are aggressive humanoids that raid, pillage, and battle other creatures. When not actually fighting other creatures, orcs are usually planning raids or practicing their fighting skills. They are familiar with the use of most weapons, preferring those that cause smash things. They enjoy attacking from concealment and setting ambushes, and they obey the rules of war (such as honoring a truce) so long as it\'s convenient for them. The chief orc deity is Gruumsh, the one-eyed god who tolerates no sign of peace among his people. An orc\'s favored class is barbarian, and orc leaders tend to be barbarians. Orc clerics worship Gruumsh (favored weapon: any spear). Spellcasters are few but tend to set themselves apart from other orcs.<br>Orcs are bloodthirsty warmongers. They distrust everyone outside their own clan including other Orc clans. They will attack without provocation, especially against Elves and Dwarves.<br>";
                    modifiers = "<basefont size=\"+5\"> Race Specific Feats<br> Race Specific Items<br> Evil Alignment only<br>";
                    minmax[0] = "20/40";
                    minmax[1] = "15/25";
                    minmax[2] = "10/20";

                    statcaps[0] = "270";
                    statcaps[1] = "250";
                    statcaps[2] = "155";

                    modcaps[0] = "40/60";
                    modcaps[1] = "30/50";
                    modcaps[2] = "30/40";

                    break;

                case 6:
                    race = "Duergar";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Duergar, also known as Dark Dwarves, they are the counterpart of the Dwarves. They have the same desires and lusts for precious metals, gemstones and drink that Dwarves have.<br> Duergar have many of the same attributes of Dwarves, but they have also learned subterfuge and secrecy from centuries in the Underdark thus granted them some skill in stealth.<br> Duergar still make excellent Warriors and thanks to their extended excavations of magical ores, they are naturally resistant to magic.<br>";
                    modifiers = "<basefont size=\"+5\"> Infravision<br> Race Specific Feats<br> Race Specific Items<br> Magic Resistance +30%<br> Mining +30%<br> Hiding & Stealth +20%<br> Detect Hidden Passages<br> Inept at Woodslore.";
                    minmax[0] = "20/35";
                    minmax[1] = "10/20";
                    minmax[2] = "15/25";

                    statcaps[0] = "245";
                    statcaps[1] = "250";
                    statcaps[2] = "180";

                    modcaps[0] = "40/60";
                    modcaps[1] = "30/45";
                    modcaps[2] = "30/40";

                    break;

                case 7:
                    race = "Gnome";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Gnomes, trickster cousins of the dwarves, are an old and engenious race. They have the same desires and lusts for precious metals, gemstones and drink that Dwarves have, but a further passion for innovation, exploration, and invention.<br> Gnomes are quite unlike their dwarven cousins as they have learned subterfuge and secrecy from centuries of wheeling and scheming thus granted them some natural skill in stealth.<br> Gnomes make poor Warriors but thanks to their extended excavations of magical ores, they are naturally resistant to magic.<br>";
                    modifiers = "<basefont size=\"+5\"> Infravision<br> Race Specific Feats<br> Magic Resistance +20%/40%<br> Tinkering +20%/40%";
                    minmax[0] = "10/30";
                    minmax[1] = "25/45";
                    minmax[2] = "20/40";

                    statcaps[0] = "195";
                    statcaps[1] = "225";
                    statcaps[2] = "325";

                    modcaps[0] = "20/40";
                    modcaps[1] = "30/50";
                    modcaps[2] = "30/60";

                    break;

                case 8:
                    race = "Goblin";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Goblins, sneaky underhanded foul and mischevious. They are the counterpart of the Orcs. Where orcs delight in the thrill of combat, a goblin is much more likely to run and hide at the first sign of trouble.<br> Goblins are quite unlike their orcish counsins, but they have learned subterfuge and secrecy from centuries under the heel of their far stronger orcish brethren thus granted them some natural skill in stealth.<br> Goblins make poor soldiers, but their quick wits and quicker feet make them ideal students of arcane arts and more mundane trickery.<br>";
                    modifiers = "<basefont size=\"+5\"> Infravision<br> Race Specific Feats<br> Hiding & Stealth +40%";
                    minmax[0] = "10/30";
                    minmax[1] = "25/45";
                    minmax[2] = "20/40";

                    statcaps[0] = "195";
                    statcaps[1] = "225";
                    statcaps[2] = "325";

                    modcaps[0] = "20/40";
                    modcaps[1] = "30/50";
                    modcaps[2] = "30/60";

                    break;

                case 9:
                    race = "Frostling";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Frostlings are a cold northern race, who are a cross between elemental beings of ice and humans. Having very light hair, ranging from lighter colours and a pale white with blue pigmented skin this race shows their homeland in every action. Their breath is frost, their heart cold and hard to warm to any outside their race.<br>";
                    modifiers = "<basefont size=\"+5\"> Infravision<br> Race Specific Feats<br> Race Specific Items<br>Unable to work with blacksmithing.<br>Minimum Frostcrafting cap of 40.0%";
                    minmax[0] = "20/35";
                    minmax[1] = "10/25";
                    minmax[2] = "20/35";

                    statcaps[0] = "245";
                    statcaps[1] = "190";
                    statcaps[2] = "245";

                    modcaps[0] = "40/55";
                    modcaps[1] = "20/45";
                    modcaps[2] = "35/50";

                    break;

                case 10:
                    race = "Undead";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">Reborn from the ancient Khallusian empire, the Undead now roam the world as the terrible and unnatural fusion of life and death.<br>";
                    modifiers = "<basefont size=\"+5\">Race Specific Feats<br> Race Specific Items";
                    minmax[0] = "10/30";
                    minmax[1] = "10/30";
                    minmax[2] = "10/30";

                    statcaps[0] = "220";
                    statcaps[1] = "260";
                    statcaps[2] = "205";

                    modcaps[0] = "30/50";
                    modcaps[1] = "30/50";
                    modcaps[2] = "30/50";

                    break;

                case 11:
                    race = "Half-Orc";
                    description = "<basefont size=\"8\" color=\"#0f0f0f\">An orphan race of a once proud but monstrous race.<br> Being a half-orc probably appears difficult at first, but their inhuman side grants enourmas potential, they are feared and mistrusted and perhaps rightly so, a monster lurks inside their blood.<br>";
                    modifiers = "<basefont size=\"+5\">Bonus to Focus<br> Race Specific Feats<br> Race Specific Items<br No Restrictions";
                    minmax[0] = "15/35";
                    minmax[1] = "15/30";
                    minmax[2] = "15/25";

                    statcaps[0] = "220";
                    statcaps[1] = "260";
                    statcaps[2] = "205";

                    modcaps[0] = "35/55";
                    modcaps[1] = "30/50";
                    modcaps[2] = "30/45";

                    break;

                default:
                    return;
            }

            m.SendGump(new RaceDetailsGump(race, description, modifiers, minmax, statcaps, modcaps));
        }
    }

    public class RaceDetailsGump : Gump
    {
        public string race = "";

        public RaceDetailsGump(string irace, string idescription, string imodifiers, string[] iminmax, string[] istatcaps, string[] imodcaps)
            : base(40, 30)
        {
            race = irace;
            string description = idescription;
            string modifiers = imodifiers;
            string[] minmax = iminmax;
            string[] statcaps = istatcaps;
            string[] modcaps = imodcaps;

            // minmax & statcaps[0] = str, [1] = dex, [2] = int

            Closable = false;
            Disposable = false;
            Resizable = false;


            AddPage(0);

            // Description
            AddBackground(50, 50, 350, 390, 2600);
            AddLabel(140, 70, 150, race + " Character Menu");

            AddHtml(75, 100, 305, 239, description, false, true);

            AddLabel(100, 400, 1152, "Accept>                   <Decline");
            AddButton(160, 400, 210, 211, 1, GumpButtonType.Reply, 0);		// 1 = accept
            AddButton(275, 400, 210, 211, 2, GumpButtonType.Reply, 0);		// 2 = decline

            // Race Modifiers
            AddBackground(420, 50, 250, 250, 2600);
            AddLabel(485, 70, 150, "Racial Modifiers");

            AddHtml(450, 95, 200, 160, modifiers, false, true);

            // Stats Info
            AddBackground(420, 315, 250, 280, 2600);
            AddLabel(510, 335, 150, "Stats Info");

            AddHtml(521, 360, 100, 30, "Starting", false, false);
            //AddHtml(590, 380, 100, 30, "Race Cap", false, false);
            AddHtml(520, 380, 200, 30, "<basefont size=\"+0\" color=\"#0080000\">min/max</basefont>", false, false);

            AddHtml(450, 405, 200, 30, "Strength", false, false);
            AddLabel(520, 405, 1150, minmax[0]);
            //AddLabel(605, 405, 1150, statcaps[0]);

            AddHtml(450, 430, 200, 30, "Dexterity", false, false);
            AddLabel(520, 430, 1150, minmax[1]);
            //AddLabel(605, 430, 1150, statcaps[1]);

            AddHtml(450, 455, 200, 30, "Intelligence", false, false);
            AddLabel(520, 455, 1150, minmax[2]);
            //AddLabel(605, 455, 1150, statcaps[2]);

            AddHtml(450, 480, 200, 30, "Hitpoints", false, false);
            AddLabel(520, 480, 1150, modcaps[0]);

            AddHtml(450, 505, 200, 30, "Stamina", false, false);
            AddLabel(520, 505, 1150, modcaps[1]);

            AddHtml(450, 530, 200, 30, "Mana", false, false);
            AddLabel(520, 530, 1150, modcaps[2]);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            TeiravonMobile m = (TeiravonMobile)sender.Mobile;

            if (info.ButtonID == 1)
            {
                switch (race.ToLower())
                {
                    case "human":
                        m.PlayerRace = TeiravonMobile.Race.Human;

                        m.Title = "the Human";
                        m.SkillsCap = 11000;
                        break;

                    case "undead":
                        m.PlayerRace = TeiravonMobile.Race.Undead;

                        m.Title = "the Undead";
                        m.Hue = Utility.RandomList(2228, 2565, 2648, 2550);
                        if (m.Hair != null)
                            m.Hair.Hue = 1001;
                        break;

                    case "elf":
                        m.PlayerRace = TeiravonMobile.Race.Elf;
                        m.SetLanguages(TeiravonMobile.LElven, true);

                        m.Title = "the Elf";

                        int elftype = Utility.RandomMinMax(1, 4);

                        switch (elftype)
                        {
                            case 1:  //High
                                m.Hue = 351;
                                break;

                            case 2:
                            case 3:  //Mixed
                                m.Hue = 1118;
                                break;

                            case 4:  //Wood
                                m.Hue = 2317;
                                break;
                        }

                        if (elftype == 1)
                        {
                            if (m.Hair != null)
                            {
                                switch (Utility.RandomMinMax(1, 6))
                                {
                                    case 1:
                                        m.Hair.Hue = 2125;
                                        break;
                                    case 2:
                                        m.Hair.Hue = 2418;
                                        break;
                                    case 3:
                                        m.Hair.Hue = 2648;
                                        break;
                                    case 4:
                                        m.Hair.Hue = 2101;
                                        break;
                                    case 5:
                                        m.Hair.Hue = 2545;
                                        break;
                                    case 6:
                                        m.Hair.Hue = 2254;
                                        break;
                                }
                            }
                        }
                        else if (elftype == 4)
                        {
                            if (m.Hair != null)
                            {
                                switch (Utility.RandomMinMax(1, 6))
                                {
                                    case 1:
                                        m.Hair.Hue = 2538;
                                        break;
                                    case 2:
                                        m.Hair.Hue = 2541;
                                        break;
                                    case 3:
                                        m.Hair.Hue = 2646;
                                        break;
                                    case 4:
                                        m.Hair.Hue = 2617;
                                        break;
                                    case 5:
                                        m.Hair.Hue = 1727;
                                        break;
                                    case 6:
                                        m.Hair.Hue = 2626;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (m.Hair != null)
                            {
                                switch (Utility.RandomMinMax(1, 10))
                                {
                                    case 1:
                                        m.Hair.Hue = 2538;
                                        break;
                                    case 2:
                                        m.Hair.Hue = 2541;
                                        break;
                                    case 3:
                                        m.Hair.Hue = 2646;
                                        break;
                                    case 4:
                                        m.Hair.Hue = 2617;
                                        break;
                                    case 5:
                                        m.Hair.Hue = 1727;
                                        break;
                                    case 6:
                                        m.Hair.Hue = 2125;
                                        break;
                                    case 7:
                                        m.Hair.Hue = 2418;
                                        break;
                                    case 8:
                                        m.Hair.Hue = 2648;
                                        break;
                                    case 9:
                                        m.Hair.Hue = 2101;
                                        break;
                                    case 10:
                                        m.Hair.Hue = 2545;
                                        break;
                                }
                            }
                        }


                        m.ElfReputation = 1.0;
                        m.AnimalReputation = 1.0;
                        m.OrcReputation = -1.0;
                        m.DrowReputation = -1.0;

                        break;

                    case "dwarf":
                        m.PlayerRace = TeiravonMobile.Race.Dwarf;
                        m.SetLanguages(TeiravonMobile.LDwarven, true);

                        m.Title = "the Dwarf";

                        m.DwarfReputation = 1.0;
                        m.OrcReputation = -1.0;
                        m.DuergarReputation = -1.0;

                        break;

                    case "gnome":
                        m.PlayerRace = TeiravonMobile.Race.Gnome;
                        m.SetLanguages(TeiravonMobile.LDwarven, true);

                        m.Title = "the Gnome";

                        m.DwarfReputation = 1.0;
                        m.OrcReputation = -1.0;
                        m.DuergarReputation = -1.0;

                        break;

                    case "drow":
                        m.PlayerRace = TeiravonMobile.Race.Drow;
                        m.SetLanguages(TeiravonMobile.LDrow, true);

                        m.Title = "the Drow";

                        m.Hue = 0x455;

                        m.DrowReputation = 1.0;
                        m.ElfReputation = -1.0;

                        break;

                    case "orc":
                        m.PlayerRace = TeiravonMobile.Race.Orc;
                        m.SetLanguages(TeiravonMobile.LOrc, true);

                        m.Title = "the Orc";

                        m.Hue = 2414;

                        m.OrcReputation = 1.0;
                        m.ElfReputation = -1.0;
                        m.DwarfReputation = -1.0;
                        m.AnimalReputation = -1.0;

                        //m.BodyMod = 182;
                        //m.OBody = 182;

                        break;

                    case "goblin":
                        m.PlayerRace = TeiravonMobile.Race.Goblin;
                        m.SetLanguages(TeiravonMobile.LOrc, true);

                        m.Title = "the Goblin";

                        m.Hue = Utility.RandomMinMax(2126, 2129);

                        m.OrcReputation = 1.0;
                        m.ElfReputation = -1.0;
                        m.DwarfReputation = -1.0;
                        m.AnimalReputation = -1.0;

                        //m.BodyMod = 182;
                        //m.OBody = 182;

                        break;

                    case "duergar":
                        m.PlayerRace = TeiravonMobile.Race.Duergar;
                        m.SetLanguages(TeiravonMobile.LDwarven, true);

                        m.Title = "the Duergar";

                        m.Hue = 0x5ee;

                        m.DuergarReputation = 1.0;

                        break;

                    case "frostling":
                        m.PlayerRace = TeiravonMobile.Race.Frostling;

                        m.Title = "the Frostling";


                        m.Hue = Utility.RandomList(2542, 2496, 2290, 2291, 2244, 2229);

                        if (m.Hair != null)
                        {
                            switch (Utility.RandomMinMax(1, 6))
                            {
                                case 1:
                                    m.Hair.Hue = 595;
                                    break;
                                case 2:
                                    m.Hair.Hue = 96;
                                    break;
                                case 3:
                                    m.Hair.Hue = 101;
                                    break;
                                case 4:
                                    m.Hair.Hue = 106;
                                    break;
                                case 5:
                                    m.Hair.Hue = 196;
                                    break;
                                case 6:
                                    m.Hair.Hue = Utility.Random(2119, 5);
                                    break;
                            }
                        }

                        break;

                    default:
                        break;
                }

                m.SendGump(new RollGump(m));
            }
            else
                m.SendGump(new RaceGump(m));
        }
    }

    public class RollGump : Gump
    {
        public RollGump(Mobile from)
            : base(300, 60)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            Closable = false;
            Disposable = false;
            Resizable = false;

            AddPage(0);

            AddBackground(0, 0, 220, 380, 2600);

            AddLabel(60, 20, 150, "Roll For Stats");

            AddLabel(30, 60, 150, "Strength");
            AddHtml(125, 60, 50, 23, m_Player.Str.ToString(), false, false);

            AddLabel(30, 85, 150, "Dexterity");
            AddHtml(125, 85, 50, 23, m_Player.Dex.ToString(), false, false);

            AddLabel(30, 110, 150, "Intelligence");
            AddHtml(125, 110, 50, 23, m_Player.Int.ToString(), false, false);

            AddLabel(30, 160, 150, "Hitpoints");
            AddHtml(125, 160, 50, 23, m_Player.MaxHits.ToString(), false, false);

            AddLabel(30, 185, 150, "Stamina");
            AddHtml(125, 185, 50, 23, m_Player.MaxStam.ToString(), false, false);

            AddLabel(30, 210, 150, "Mana");
            AddHtml(125, 210, 50, 23, m_Player.MaxMana.ToString(), false, false);

            AddHtml(87, 255, 100, 22, "REROLL STATS", false, false);
            AddButton(63, 255, 208, 209, 1, GumpButtonType.Reply, 0);

            AddHtml(87, 290, 100, 23, "ACCEPT", false, false);
            AddButton(63, 290, 208, 209, 2, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            TeiravonMobile m = (TeiravonMobile)sender.Mobile;

            if (info.ButtonID == 1)
            {
                m.Str = 10;
                m.Hits = m.Str;
                m.MaxHits = 10;

                m.Int = 10;
                m.Mana = m.Int;
                m.MaxMana = 10;

                m.Dex = 10;
                m.Stam = m.Dex;
                m.MaxStam = 10;

                if (m.IsHuman() || m.IsUndead())
                {
                    m.Str = Utility.RandomMinMax(10, 30);
                    m.Dex = Utility.RandomMinMax(10, 30);
                    m.Int = Utility.RandomMinMax(10, 30);

                    m.MaxHits = Utility.RandomMinMax(30, 50);
                    m.MaxStam = Utility.RandomMinMax(30, 50);
                    m.MaxMana = Utility.RandomMinMax(30, 50);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;

                }
                else if (m.IsElf())
                {
                    m.Str = Utility.RandomMinMax(10, 30);
                    m.Dex = Utility.RandomMinMax(20, 40);
                    m.Int = Utility.RandomMinMax(15, 35);

                    m.MaxHits = Utility.RandomMinMax(30, 40);
                    m.MaxStam = Utility.RandomMinMax(40, 60);
                    m.MaxMana = Utility.RandomMinMax(35, 55);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;

                }
                else if (m.IsDrow())
                {
                    m.Str = Utility.RandomMinMax(10, 30);
                    m.Dex = Utility.RandomMinMax(20, 40);
                    m.Int = Utility.RandomMinMax(15, 35);

                    m.MaxHits = Utility.RandomMinMax(30, 40);
                    m.MaxStam = Utility.RandomMinMax(40, 60);
                    m.MaxMana = Utility.RandomMinMax(35, 55);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;

                }
                else if (m.IsDwarf() || m.IsOrc() || m.IsDuergar())
                {
                    m.Str = Utility.RandomMinMax(20, 35);
                    m.Dex = Utility.RandomMinMax(10, 20);
                    m.Int = Utility.RandomMinMax(15, 25);

                    m.MaxHits = Utility.RandomMinMax(40, 60);
                    m.MaxStam = Utility.RandomMinMax(30, 45);
                    m.MaxMana = Utility.RandomMinMax(30, 40);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;

                }
                else if (m.IsGnome())
                {
                    m.Str = Utility.RandomMinMax(10, 30);
                    m.Dex = Utility.RandomMinMax(25, 45);
                    m.Int = Utility.RandomMinMax(20, 40);

                    m.MaxHits = Utility.RandomMinMax(20, 40);
                    m.MaxStam = Utility.RandomMinMax(30, 50);
                    m.MaxMana = Utility.RandomMinMax(30, 60);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;
                }

                else if (m.IsGoblin())
                {
                    m.Str = Utility.RandomMinMax(10, 30);
                    m.Dex = Utility.RandomMinMax(25, 45);
                    m.Int = Utility.RandomMinMax(20, 40);

                    m.MaxHits = Utility.RandomMinMax(20, 40);
                    m.MaxStam = Utility.RandomMinMax(30, 50);
                    m.MaxMana = Utility.RandomMinMax(30, 60);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;
                }
                else if (m.IsFrostling())
                {
                    m.Str = Utility.RandomMinMax(15, 35);
                    m.Dex = Utility.RandomMinMax(10, 25);
                    m.Int = Utility.RandomMinMax(20, 35);

                    m.MaxHits = Utility.RandomMinMax(30, 55);
                    m.MaxStam = Utility.RandomMinMax(10, 50);
                    m.MaxMana = Utility.RandomMinMax(30, 50);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;
                }
                else if (m.IsHalfOrc())
                {
                    m.Str = Utility.RandomMinMax(15, 35);
                    m.Dex = Utility.RandomMinMax(15, 30);
                    m.Int = Utility.RandomMinMax(15, 25);

                    m.MaxHits = Utility.RandomMinMax(35, 55);
                    m.MaxStam = Utility.RandomMinMax(30, 50);
                    m.MaxMana = Utility.RandomMinMax(30, 45);

                    m.Hits = m.MaxHits;
                    m.Stam = m.MaxStam;
                    m.Mana = m.MaxMana;
                }

                m.SendGump(new RollGump(m));
            }
            else
            {
                m.SendGump(new ClassGump(m));
            }
        }
    }

    public class ClassGump : Gump
    {
        public bool Get20(IAccount a, TeiravonMobile.Race race)
        {
            bool Has20 = false;
            int chars = a.Length;
            for (int i = 0; i < chars; ++i)
            {
                TeiravonMobile m = (TeiravonMobile)a[i];

                if (m == null)
                    continue;
                if (m.PlayerLevel > 19 && m.PlayerRace == race)
                    Has20 = true;
            }
            return Has20;
        }

        public ClassGump(Mobile from)
            : base(150, 30)
        {
            Closable = false;
            Disposable = false;
            Resizable = false;

            TeiravonMobile m = (TeiravonMobile)from;
            string open = "<basefont size=\"+8\" color=\"#E0E0E0\">";
            string close = "</basefont>";

            m.CloseGump(typeof(ClassGump));

            AddPage(1);
            AddBackground(0, 52, 500, 400, 2600);

            AddImage(201, 14, 1417);
            AddImage(124, 30, 1419);
            AddImage(211, 22, 5505);

            AddLabel(140, 95, 150, "Class Selection Main Menu");

            // Warriors
            AddLabel(42, 130, 150, "Warrior");

            AddHtml(66, 149, 110, 25, open + "Fighter" + close, false, false);
            AddButton(50, 153, 216, 216, 1, GumpButtonType.Reply, 0);

            if ((m.IsLawfulGood() || m.IsLawfulEvil() || m.IsLawfulNeutral()) && (!m.IsUndead() && !m.IsFrostling()))
            {
                AddHtml(66, 168, 110, 25, open + "Monk" + close, false, false);
                AddButton(50, 171, 216, 6501, 2, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(66, 168, 110, 25, open + "Unavailable" + close, false, false);

            if ((m.IsHuman() || m.IsUndead() || m.IsDwarf() || m.IsOrc()) && (m.IsLawfulGood() || m.IsLawfulEvil()))
            {
                AddHtml(66, 187, 110, 25, open + ( m.IsOrc() ? "Raider" : "Cavalier" ) + close, false, false);
                AddButton(50, 189, 216, 6501, 3, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(66, 187, 110, 25, open + "Unavailable" + close, false, false);

            // Barbarians
            AddLabel(42, 218, 150, "Barbarian");

            if ((!m.IsLawfulGood() || !m.IsLawfulNeutral() || !m.IsLawfulEvil()) && (m.IsHuman() || m.IsOrc() || m.IsUndead() || m.IsDwarf() || m.IsFrostling()))
            {
                AddHtml(66, 237, 110, 25, open + "Berserker" + close, false, false);
                AddButton(50, 240, 216, 6501, 4, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(66, 237, 110, 25, open + "Unavailable" + close, false, false);

            if ((!m.IsLawfulGood() || !m.IsLawfulNeutral() || !m.IsLawfulEvil()) && (m.IsHuman() || m.IsDwarf() || m.IsUndead() || m.IsOrc() || m.IsFrostling()))
            {
                AddHtml(66, 254, 110, 25, open + "Dragoon" + close, false, false);
                AddButton(50, 257, 216, 6501, 5, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(66, 254, 110, 25, open + "Unavailable" + close, false, false);

            // Rogues
            AddLabel(42, 285, 150, "Rogue");

            if (!m.IsLawfulGood() && !m.IsLawfulNeutral() && !m.IsOrc() && !m.IsDwarf() && !m.IsFrostling())
            {
                AddHtml(66, 304, 110, 25, open + "Scoundrel" + close, false, false);
                AddButton(50, 308, 216, 6501, 6, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(66, 304, 110, 25, open + "Unavailable" + close, false, false);

            if (m.IsEvil() && (m.IsHuman() || m.IsHalfElf() || m.IsDrow() || m.IsGoblin() || m.IsGnome()))
            {
                AddHtml(66, 323, 110, 25, open + "Assassin" + close, false, false);
                AddButton(50, 327, 216, 6501, 7, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(66, 323, 110, 25, open + "Unavailable" + close, false, false);

            if ((m.IsHuman() || m.IsHalfElf() || m.IsElf() || m.IsDrow() || m.IsGoblin() || m.IsGnome()))
            {
                AddHtml(66, 342, 110, 25, open + "Bard" + close, false, false);
                AddButton(50, 344, 216, 6501, 8, GumpButtonType.Reply, 0);
            }
            /* else if (m.IsOrc() || m.IsDwarf())
             {
                 AddHtml(66, 342, 110, 25, open + (m.IsOrc() ? "WarDrummer" : "WarCaller") + close, false, false);
                 AddButton(50, 344, 216, 6501, 8, GumpButtonType.Reply, 0);
             }*/
            else
                AddHtml(66, 342, 110, 25, open + "Unavailable" + close, false, false);

            // Rangers
            AddLabel(200, 130, 150, "Ranger");

            if (m.IsHuman() || m.IsElf() || m.IsHalfElf() || m.IsDrow() || m.IsDwarf() || m.IsFrostling() || m.IsFrostling() || m.IsOrc())
            {
                AddHtml(224, 149, 110, 25, open + "Ranger" + close, false, false);
                AddButton(208, 153, 216, 6501, 9, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 149, 110, 25, open + "Unavailable" + close, false, false);

            if (m.IsHuman() || m.IsElf() || m.IsHalfElf() || m.IsDrow() || m.IsGoblin() || m.IsUndead() || m.IsGnome())
            {
                AddHtml(224, 168, 110, 25, open + "Archer" + close, false, false);
                AddButton(208, 172, 216, 6501, 10, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 168, 110, 25, open + "Unavailable" + close, false, false);

            if (m.IsHuman() || m.IsElf() || m.IsHalfElf() || m.IsDrow() || m.IsGnome())
            {
                AddHtml(224, 187, 110, 25, open + "MageSlayer" + close, false, false);
                AddButton(208, 195, 216, 6501, 27, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 187, 110, 25, open + "Unavailable" + close, false, false);

            // Clerics
            AddLabel(200, 218, 150, "Clerics");

            if (m.IsGood() && (m.IsHuman() || m.IsElf() || m.IsHalfElf() || m.IsDwarf() || m.IsGnome()))
            {
                AddHtml(224, 237, 110, 25, open + "Light" + close, false, false);
                AddButton(208, 240, 216, 6501, 11, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 237, 110, 25, open + "Unavailable" + close, false, false);

            if (m.IsEvil() && (m.IsHuman() || m.IsDrow() || m.IsDuergar() || m.IsOrc() || m.IsUndead() || m.IsGoblin()))
            {
                AddHtml(224, 254, 110, 25, open + "Darkness" + close, false, false);
                AddButton(208, 257, 216, 6501, 12, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 254, 110, 25, open + "Unavailable" + close, false, false);

            // Druids
            AddLabel(200, 285, 150, "Druids");

            if (m.IsTrueNeutral() && (m.IsHuman() || m.IsElf() || m.IsHalfElf() || m.IsFrostling()))
            {
                AddHtml(224, 304, 110, 25, open + "Forester" + close, false, false);
                AddButton(208, 307, 216, 6501, 13, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 304, 110, 25, open + "Unavailable" + close, false, false);

            if (m.IsTrueNeutral() && (m.IsHuman() || m.IsElf() || m.IsHalfElf() || m.IsFrostling()))
            {
                AddHtml(224, 323, 110, 25, open + "Shapeshifter" + close, false, false);
                AddButton(208, 325, 216, 6501, 14, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 323, 110, 25, open + "Unavailable" + close, false, false);


            // Advanced
            AddLabel(200, 359, 150, "Prestige");

            if (m.IsDwarf() && Get20(m.Account, TeiravonMobile.Race.Dwarf))
            {
                AddHtml(224, 378, 111, 25, open + "<basefont color=YELLOW>Defender" + close, false, false);
                AddButton(208, 381, 216, 6501, 28, GumpButtonType.Reply, 0);
            }
            else if (m.IsOrc() && Get20(m.Account, TeiravonMobile.Race.Orc))
            {
                AddHtml(224, 378, 111, 25, open + "<basefont color=YELLOW>Savage" + close, false, false);
                AddButton(208, 381, 216, 6501, 29, GumpButtonType.Reply, 0);
            }
            else if (m.IsElf() && Get20(m.Account, TeiravonMobile.Race.Elf))
            {
                AddHtml(224, 378, 111, 25, open + "<basefont color=YELLOW>Strider" + close, false, false);
                AddButton(208, 381, 216, 6501, 30, GumpButtonType.Reply, 0);
            }
            else if (m.IsDrow() && Get20(m.Account, TeiravonMobile.Race.Drow))
            {
                AddHtml(224, 378, 111, 25, open + "<basefont color=YELLOW>Ravager" + close, false, false);
                AddButton(208, 381, 216, 6501, 31, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(224, 378, 110, 25, open + "Unavailable" + close, false, false);

            // Crafters
            AddLabel(348, 254, 150, "Crafters");

            AddHtml(372, 273, 110, 25, open + "Alchemist" + close, false, false);
            AddButton(356, 276, 216, 6501, 15, GumpButtonType.Reply, 0);

            AddHtml(372, 294, 110, 25, open + "Merchant" + close, false, false);
            AddButton(356, 297, 216, 6501, 21, GumpButtonType.Reply, 0);
            /*
            if (!m.IsFrostling())
            {
                AddHtml(372, 294, 110, 25, open + "Metalworker" + close, false, false);
                AddButton(356, 297, 216, 6501, 16, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(372, 294, 110, 25, open + "Unavailable" + close, false, false);

                //AddHtml( 372, 315, 110, 25, open + "Bowyer" + close, false, false );
                //AddButton( 356, 318, 216, 6501, 17, GumpButtonType.Reply, 0 );
            */
            if (m.IsFrostling())
            {
                AddHtml(372, 315, 110, 25, open + ("Frostcarver") + close, false, false);
                AddButton(356, 318, 216, 6501, 18, GumpButtonType.Reply, 0);
            }
            /*

           AddHtml(372, 336, 110, 25, open + "Tailor" + close, false, false);
           AddButton(356, 339, 216, 6501, 19, GumpButtonType.Reply, 0);

           //AddHtml( 372, 357, 110, 25, open + "Tinker" + close, false, false );
           //AddButton( 356, 360, 216, 6501, 20, GumpButtonType.Reply, 0 );

           AddHtml(372, 378, 110, 25, open + "Cook" + close, false, false);
           AddButton(356, 381, 216, 6501, 21, GumpButtonType.Reply, 0);
           */
            // Mages
            AddLabel(348, 130, 150, "Mage");

            if ((m.IsNeutral() || m.IsDrow()) && (m.IsElf() || m.IsHuman() || m.IsHalfElf() || m.IsDrow() || m.IsGnome()))
            {
                AddHtml(372, 149, 110, 25, open + "Aeromancer" + close, false, false);
                AddButton(356, 153, 216, 6501, 22, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(372, 149, 110, 25, open + "Unavailable" + close, false, false);

            if (m.IsNeutral() && (m.IsElf() || m.IsHuman() || m.IsHalfElf() || m.IsGnome() || m.IsFrostling()))
            {
                AddHtml(372, 168, 110, 25, open + (m.IsFrostling() ? "Cryomancer" : "Aquamancer") + close, false, false);
                AddButton(356, 172, 216, 6501, 23, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(372, 168, 110, 25, open + "Unavailable" + close, false, false);

            if ((m.IsNeutral() || m.IsDuergar() || m.IsDrow() || m.IsDwarf()) && (m.IsHuman() || m.IsHalfElf() || m.IsDuergar() || m.IsDrow()) || m.IsDwarf() || m.IsGnome())
            {
                AddHtml(372, 187, 110, 25, open + "Geomancer" + close, false, false);
                AddButton(356, 189, 216, 6501, 24, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(372, 187, 110, 25, open + "Unavailable" + close, false, false);

            if ((m.IsNeutral() && (m.IsHuman() || m.IsHalfElf() || m.IsGnome()) || (m.IsGoblin() || m.IsDrow())))
            {
                AddHtml(372, 206, 110, 25, open + "Pyromancer" + close, false, false);
                AddButton(356, 208, 216, 6501, 25, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(372, 206, 110, 25, open + "Unavailable" + close, false, false);

            if (m.IsEvil() && (m.IsHuman() || m.IsDrow() || m.IsDuergar() || m.IsUndead() || m.IsGoblin()))
            {
                AddHtml(372, 225, 110, 25, open + "Necromancer" + close, false, false);
                AddButton(356, 227, 216, 6501, 26, GumpButtonType.Reply, 0);
            }
            else
                AddHtml(372, 225, 110, 25, open + "Unavailable" + close, false, false);

        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            TeiravonMobile m = (TeiravonMobile)sender.Mobile;

            string open = "<basefont size=\"8\" color=\"#0f0f0f\">";
            string close = "</basefont>";
            string itemopen = "<basefont size=\"8\" color=\"#E0E0E0\">";
            string classname = "";
            string description = "";
            string skilltree = "";
            string bonuses = "";
            string items = "";
            int emblem = 0;

            switch (info.ButtonID)
            {
                case 1:
                    classname = "Fighter";
                    description = open + " The Fighter is a specialized combat character. He relies upon his strength and weapon to face monsters, enemy humans or whatever other dangers lurk around the next corner.<br> A veteran fighter knows when to use what weapon to reap its full benefits, since certain creatures have natural resistances to certain weapons. Knowing when you use your arsenal properly can mean the difference between life and death for a Fighter out in the wilds or in the depths of the dungeons." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Macefighting - 100.0<br>Fencing - 100.0<br>Aim - 100.0<br>Wrestling - 100.0<br>Tactics - 100.0<br>Parrying - 100.0<br>Anatomy - 80.0<br>Arms Lore - 100.0<br>Camping - 100.0<br>Cartography - 60.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Longsword<br>Leather<br>Wooden Shield" + close;
                    emblem = 5577;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 2:
                    classname = "Monk";
                    description = open + " The Monk is another specialized combat character. He relies upon disciplined training and martial arts to face monsters, other races or whatever other dangers lurk around the next corner.<br> while capable of weilding any weaponry, the Monk feels most comfortable honing the destructive power of their fists which, thanks to their dedication and training, are far stronger than the average warrior's. Some highly disciplined monks have even made their fists more lethal than any weapon carved of wood or forged from steel. However, this destructive power comes at the price of the monk's unwillingness to don any form of armor, a defecit which is overcome in time through the monk's training." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Macefighting - 100.0<br>Wrestling - 100.0<br>Tactics - 100.0<br>Parrying - 100.0<br>Anatomy - 100.0<br>Arms Lore - 60.0<br>Camping - 100.0<br>Cartography - 60.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 80.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Monk Fist ability<br>No spellcasting ability<br>Cannot wear armor" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal" + close;
                    emblem = 5563;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 3:
                    classname =  (m.IsOrc() ? "Raider" : "Cavalier");
                    description = open + (m.IsOrc() ? " The Raider represents the most common picture of a marauder; the rampaging warrior who epitomizes swift, brutality.<br> Riding down their enemies and driving them from their lands and their homes." : " The Cavalier represents the most common picture of a knight; the gentleman warrior who epitomizes honor, courage and loyalty.<br> He is specialized in battling \"classical\" evil monsters such as demons and dragons. The Cavalier lives by a Code that is very similar to the Paladin Creed, the exception is that a Cavalier can be either Lawful Good or Lawful Evil. Those of Lawful Good alignment will help anyone in needs, most especially the \'damsel in distress\', those of Lawful Evil only care for themselves but will do battle with honor.") + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Macefighting - 100.0<br>Fencing - 100.0<br>Wrestling - 400.0<br>Tactics - 100.0<br>Parrying - 100.0<br>Anatomy - 80.0<br>Arms Lore - 100.0<br>Camping - 100.0<br>Cartography - 60.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>War mount Access<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Longsword<br>Leather<br>Wooden Shield" + close;
                    emblem = 5547;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 4:
                    classname = "Berserker";
                    description = open + " The Berserker can be one of the most deadly of adversaries. A Berserker has learned to harness the blood rage that is infamous to Barbarians.<br> A Berserker is able to focus this rage at will making him stronger and faster than would otherwise be mortally possible. This disadvantage is that when the rage ends, the berserker is physically drained thus leaving an unprepared berserker vulnerable to counterattack. Because Berserkers need complete freedom for the blood rage, they do not wear any form of metal armor including platemail." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Macefighting - 100.0<br>Fencing - 60.0<br>Aim - 60.0<br>Wrestling - 100.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Arms Lore - 80.0<br>Anatomy - 60.0<br>Camping - 100.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Tracking - 80.0<br>Veterinary - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>Leather armor only<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "4d4 x 20 Gold<br>Short Pants<br>Sandals<br>Torches<br>Journal<br><br>Iron Longsword" + close;
                    emblem = 5585;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 5:
                    classname = "Dragoon";
                    description = open + " Where the Berserker relies on his blood rage, a Dragoon relies on his pure strength alone. This does not mean that the Dragoon is any less powerful.<br> The Dragoon\'s physical prowess alone grants him the ability to wear might plate armor that most mortals cannot wear thus making the Dragoon an effective close combat warrior. Dragoons can use most bladed weapons in combat and a few other weapons types if they prefer. Whether using great swords or their favored polearms, Dragoons are a force to be reckoned with." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Macefighting - 60.0<br>Fencing - 100.0<br>Wrestling - 100.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Arms Lore - 80.0<br>Anatomy - 60.0<br>Camping - 100.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Tracking - 80.0<br>Veterinary - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "4d4 x 40 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Shortspear<br>Leather" + close;
                    emblem = 5585;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 6:
                    classname = "Scoundrel";
                    description = open + " The life of a Scoundrel can be best described as exciting.<br> You posses a unique and varied set of skills at your disposal.<br> Through guile, cunning, and a few well planned contingencies, the scoundrel is an invaluable asset." + close;
                    skilltree = open + "Swordsmanship - 60.0<br>Fencing - 100.0<br>Aim - 60.0<br>Wrestling - 40.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Anatomy - 80.0<br>Begging - 100.0<br>Camping - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Hiding - 100.0<br>Lockpicking - 100.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Remove Trap - 100.0<br>Resist Spells - 100.0<br>Snooping - 100.0<br>Stealing - 100.0<br>Stealth - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>Leather armor only" + close;
                    items = itemopen + "5d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Brace of Throwing Knives<br>Kryss<br>Leather" + close;
                    emblem = 5571;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 7:
                    classname = "Assassin";
                    description = open + " Becoming an Assassin is not for the weak at heart. It is your task to carry out various covert assignments usually involving the killing of another character.<br> Since people really just don\'t get along very well, you should be one of the busiest people in the land.<br> Don\'t let something as meaningless as a murder count or low karma bother you, there are plenty of places for you to carry on without the hassles of the more law-abiding citizens continually calling law enforcement on you.<br> Stealth and a good disguise will get you a long way here." + close;
                    skilltree = open + "Swordsmanship - 80.0<br>Fencing - 100.0<br>Aim - 60.0<br>Wrestling - 40.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Arms Lore - 80.0<br>Camping - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Hiding - 100.0<br>Lockpicking - 100.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Poisoning - 100.0<br>Resist Spells - 100.0<br>Stealth - 100.0<br>Taste ID - 80.0<br>Tracking - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>Leather armor only" + close;
                    items = itemopen + "5d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Kryss<br>Oak Hand Crossbow<br>Quiver of Bolts<br>Leather" + close;
                    emblem = 5571;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 8:
                    classname = "Bard";
                    description = open + "While more at home in a tavern or entertaining the the masses at a festival,the Bard is no stranger to the dangers of the world. Cunning and guile are their watchwords as they spin a clever yarn to dazzle and confound the unwitting. But when clever words and a jaunty tune don't suffice, there's always a short sharp blade." + close;
                    skilltree = open + "Swordsmanship - 100 <br> Fencing - 100.0 <br> Aim 60.0 <br>Wrestling - 40.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Musicianship - 100.0<br>Discordance - 100.0<br>Peacemaking - 100.0<br>Provocation - 100.0<br>Anatomy - 100.0<br>Arms Lore - 80.0<br>Camping - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Hiding - 100.0<br>Lockpicking - 80.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Stealth - 100.0<br>Taste ID - 80.0<br>Tracking - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>Leather armor only" + close;
                    items = itemopen + "5d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br>Longsword<br>Iron Kryss<br>Lute<br>Leather" + close;
                    emblem = 5553;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 9:
                    classname = "Ranger";
                    description = open + " The Ranger is a hunter and a woodsman. He is skilled with weapons, especially the bow and the sword, and is knowledgeable in tracking and woodscraft.<br> Some Rangers often protect and guide lost travelers and honest peasant-folk through the wilderness both for their sake and the Forest itself. Others prefer protecting the Forest through harsher means, either by hunting down those who would destroy the essence of Gaia and her children or by keeping the creature populace in check, slaying as needed but letting nothing waste.<br> Rangers have the ability to befriend forest animals and they are exceptionally skilled at camouflage in the wilderness. Add to that a medium ability in spellcasting, the Ranger is a deadly adversary indeed when confronted in the wilderness..." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Fencing - 60.0<br>Aim - 100.0<br>Wrestling - 60.0<br>Tactics - 100.0<br>Parrying - 80.0<br>Anatomy - 80.0<br>Animal Lore - 100.0<br>Animal Taming - 100.0<br>Bowcraft - 40.0<br>Camping - 100.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Fishing - 100.0<br>Healing - 80.0<br>Hiding - 100.0<br>Lumberjacking - 60.0<br>Magery - 100.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Stealth - 100.0<br>Tracking - 100.0<br>Veterinary - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Divine Defense Spells<br>Cannot wear Plate Armor<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Longsword<br>Oak Shortbow<br>Quiver of Arrows<br>Leather" + close;
                    emblem = 5575;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 10:
                    classname = "Archer";
                    description = open + " The Archer prefers to do his killing from a distance trusting to his bow and aim to bring his opponents down.<br> Choosing this class is slightly restrictive since you won\'t have the full abilities of a Ranger and cannot wear anything stronger than chainmail armor. An Archer needs flexibility to operate his bow that greater armor types simply do not allow.<br> Archers are devastating to spellcasters since being struck by an arrow can break the spellcaster\'s concentration. At higher levels, and the right bow, even the strongest of enemies can be brought down with just a few shots." + close;
                    skilltree = open + "Swordsmanship - 60.0<br>Fencing - 60.0<br>Aim - 100.0<br>Wrestling - 40.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Anatomy - 80.0<br>Arms Lore - 60.0<br>Bowcraft - 40.0<br>Camping - 100.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Hiding - 100.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Tracking - 80.0<br>Forensics - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Cannot cast spells<br>Cannot wear Plate Armor<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Oak Shortbow<br>Quiver of Arrows<br>Leather" + close;
                    emblem = 5575;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 11:
                    classname = "Cleric";
                    description = open + " The Cleric of Light is the harbinger of of all that is good and holy in the lands.<br> It is a Cleric\'s job to look after the souls of the residents here and provide aid to those in need.<br> Armed with Divine powers of protection and a medium of ability to protect yourself from physical harm, you will be a welcome addition to any adventuring party. Add those devices to your natural ability to call back a fallen companion from the grasp of death, no party should leave home without you.<br> These special abilities come at a cost, however, you can never master any weapon style nor can you wield anything other than mace weapons.<br> Given the benefits of keeping you alive, your companions should be only too happy to keep you well protected." + close;
                    skilltree = open + "Macefighting - 80.0<br>Wrestling - 80.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cooking - 80.0<br>Fishing - 100.0<br>Healing - 100.0<br>Lumberjacking - 60.0<br>Magery - 100.0<br>Evaluate Int - 75.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Spirit Speak - 100.0<br>Taste ID - 80.0<br>Inscription - 30.0" + close;
                    bonuses = open + "Class Feats & Items<br>Healing Skills<br>Divine Spells Only" + close;
                    items = itemopen + "5d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Mace<br>Leather<br><br>Spellbook" + close;
                    emblem = 5563;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 12:
                    classname = "Dark Cleric";
                    description = open + " As the Cleric of Light is the harbinger of of all that is good and holy, you are the harbinger of chaos and destruction. <br> Pain and suffering are your trademarks, as is a small arsenal of curses. You revel in the pain of others.<br> Armed with Unholy powers and also a medium of ability to protect yourself from physical harm, you have the ability to hold your own against most foes. However, you are not above using others to do your dirty work, if they suffer or fall, so be it as long as the deed is done in the end.<br> These special abilities come at a cost, however, you can never master any weapon style nor can you wield anything other than mace weapons.<br> Given the benefits of your powers, the price is worth bearing so long as you keep a sharp wit about you..." + close;
                    skilltree = open + "Macefighting - 80.0<br>Wrestling - 80.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cooking - 80.0<br>Fishing - 100.0<br>Healing - 100.0<br>Lumberjacking - 60.0<br>Magery - 100.0<br>Evaluate Int - 75.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Spirit Speak - 100.0<br>Taste ID - 80.0<br>Inscription - 30.0" + close;
                    bonuses = open + "Class Feats & Items<br>Healing Skills<br>Divine Spells Only" + close;
                    items = itemopen + "5d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Mace<br>Leather<br><br>Spellbook" + close;
                    emblem = 5563;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 13:
                    classname = "Forester";
                    description = open + " Where the Elementalist commands the elemental powers of Gaia\'s being, you command the more physical aspects, the plant and animal life. Whether calling upon the local wildlife to your aid, bringing the area plant life to the waking world, even calming the most enraged of beasts, you have powers at your command that are equal to what the Elementalist holds. You have no equal when it comes to dealing with the sentient children of Gaia.<br> But as a Druid, you must still adhere to the standards and demands as befitting your position. Always, must you respect the wishes of Gaia and learn when to use her powers and when to refrain from using them.<br> As with any spellcasting, armor involving ferrous materials can not be equipped with the exception of Leather and Boots, and your combat weapons are limited those of non-metal make, daggers being allowed." + close;
                    skilltree = open + "Swordsmanship - 20.0<br>Macefighting - 80.0<br>Fencing - 20.0<br>Aim - 20.0<br>Wrestling - 60.0<br>Tactics - 80.0<br>Parrying - 80.0<br>Alchemy - 100.0<br>Anatomy - 80.0<br>Animal Lore - 100.0<br>Animal Taming - 100.0<br>Camping - 100.0<br>Cartography - 80.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Evaluate Int - 70.0<br>Fishing - 100.0<br>Healing - 80.0<br>Herding - 100.0<br>Hiding - 100.0<br>Inscribe - 100.0<br>Lumberjacking - 60.0<br>Magery - 100.0<br>Resist Spells - 100.0<br>Stealth - 100.0<br>Tracking - 80.0<br>Veterinary - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Druidcraft<br>Leather & Boots<br>Non-metal Weapons<br>Karma must remain      Neutral" + close;
                    items = itemopen + "2d4 x 20 Gold<br>Plain Shirt<br>Short Pants<br>Sandals<br>Robe<br>Lantern<br>Journal<br><br>Club<br>Quarter Staff<br>Spellbook" + close;
                    emblem = 5549;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 14:
                    classname = "Shapeshifter";
                    description = open + " The thrill of the hunt, the scent of fear from your prey, the whispers of the breeze, the feel of the wind along your body, these are all things that are familiar to you. You have the unique gift of shapeshifting, the ability to become various creatures and denizens that walk Gaia\'s great land. From the fierce bear, to the smallest rat, to the giants, these are all forms that become available to you as you gain experience.<br> But as a Druid, you must still adhere to the standards and demands as befitting your position. Always, must you respect the wishes of Gaia and learn when to use your abilities and when to refrain from using them.<br> In return for your unique ability, you have been stripped of all spellcasting ability. Though you can still use Leather and Boots, your weapons are limited those of non-metal make, daggers being allowed." + close;
                    skilltree = open + "Swordsmanship - 20.0<br>Macefighting - 80.0<br>Fencing - 20.0<br>Aim - 20.0<br>Wrestling - 100.0<br>Tactics - 100.0<br>Alchemy - 60.0<br>Anatomy - 80.0<br>Animal Lore - 100.0<br>Animal Taming - 100.0<br>Camping - 100.0<br>Cartography - 80.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Fishing - 100.0<br>Healing - 80.0<br>Herding - 100.0<br>Hiding - 100.0<br>Inscribe - 80.0<br>Lumberjacking - 60.0<br>Resist Spells - 100.0<br>Stealth - 100.0<br>Tracking - 80.0<br>Veterinary - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Shapeshifting<br>Leather & Boots<br>Non-metal Weapons<br>No spellcasting ability<br>Karma must remain       Neutral" + close;
                    items = itemopen + "2d4 x 20 Gold<br>Plain Shirt<br>Short Pants<br>Sandals<br>Lantern<br>Journal<br><br>Club" + close;
                    emblem = 5549;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 15:
                    classname = "Alchemist";
                    description = open + " Potions, oils, salves, and powders, these are the hallmarks of your trade. Where a smith is the master of metals, you are the master of more potent things; reagents gathered from every corner of the lands.<br> As you learn your trade and uncover lost secrets, you will be able to extract a myriad of liquids from even the most basic of ingredients with time, patience, and the right alchemical processes.<br> Some of the most valued and sought after items are potions that can be made by your hand. Adventurers seek you out for healing concoctions, Mages seek you for certain potions they require in their pursuits, are among just a few." + close;
                    skilltree = open + "Swordsmanship - 40.0<br>Macefighting - 40.0<br>Fencing - 40.0<br>Aim - 40.0<br>Wrestling - 40.0<br>Tactics - 40.0<br>Alchemy - 100.0<br>Anatomy - 80.0<br>Camping - 100.0<br>Cartography - 80.0<br>Cooking - 80.0<br>Fishing - 100.0<br>Healing - 80.0<br>Inscribe - 100.0<br>Item ID - 80.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Poisoning - 80.0<br>Resist Spells - 100.0<br>Taste ID - 100.0" + close;
                    bonuses = open + "Advanced Alchemy<br>Limited Weapon skills<br>Limited Armor<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Full Apron<br>Boots<br>Lantern<br>Journal<br><br>Alchemist\'s Kit" + close;
                    emblem = 5583;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 16:
                    classname = "Metalworker";
                    description = open + " Being a Metalworker takes patience and endurance. Hours toiling in the mines hoping to chance upon a pocket of rare Mytheril Ore only to fail trying to smelt it can discourage even the patient Elves.<br> The rewards however, are worth the effort. If you sell fine goods and your name gets out, warriors from far and wide will be willing to meet your price.<br> The ores are abundant throughout the lands and several unique samples can be found to craft items that only you can make." + close;
                    skilltree = open + "Swordsmanship - 40.0<br>Macefighting - 80.0<br>Fencing - 40.0<br>Wrestling - 80.0<br>Tactics - 60.0<br>Arms Lore - 100.0<br>Blacksmithing - 100.0<br>Camping - 100.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Item ID - 60.0<br>Lumberjacking - 60.0<br>Mining - 100.0<br>Resist Spells - 100.0<br>Tinkering - 100.0" + close;
                    bonuses = open + "Advanced Blacksmithing<br>Racial Modifiers<br>Limited Weapon skills<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Full Apron<br>Boots<br>Lantern<br>Journal<br><br>Metalworker\'s Kit" + close;
                    emblem = 5583;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 17:
                    classname = "Bowyer";
                    description = open + " Not everyone prefers the sting of metal to get the job done, there are those that prefer to remain afar and fight using ranged weaponry. That is where you, the maker of bows, comes into play.<br> Through your hands, finely crafted ranged weapons are created. As you find better quality wood, you will be able to craft stronger and more powerful bows and crossbows.<br> In times long past, it is said that master bowyers learned how to imbue their crafts with magical properties..." + close;
                    skilltree = open + "Swordsmanship - 40.0<br>Macefighting - 40.0<br>Fencing - 40.0<br>Aim - 80.0<br>Wrestling - 40.0<br>Tactics - 60.0<br>Bowcraft - 100.0<br>Camping - 100.0<br>Carpentry - 30.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Item ID - 80.0<br>Lumberjacking - 100.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Tinkering - 60.0" + close;
                    bonuses = open + "Advanced Bowcrafting<br>Racial Modifiers<br>Limited Weapon skills<br>Limited Armor<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Half Apron<br>Boots<br>Lantern<br>Journal<br><br>Bowcraft Kit" + close;
                    emblem = 5583;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 18:
                    classname = "Woodworker";
                    description = open + " Carpentry is a valuable skill to know in the lands. Nobles, Peasants and the occasional Warrior will undoubtedly need your specialized skills sooner or later.<br> Only you can provide the finer items the discriminating house decorator will desire.<br> If you choose to accept this occupation, rest assured that your high Swordsmanship and trusty hatchet will keep you relatively safe as you scour the forests for the raw materials you will need to further your craft.<br> Bear in mind that <def.it>Sawing Logs<def.io> here has nothing to do with sleeping. There are many craftable items here that will require your talents in one way or another." + close;
                    skilltree = open + "Swordsmanship - 40.0<br>Macefighting - 80.0<br>Fencing - 40.0<br>Aim - 40.0<br>Wrestling - 40.0<br>Tactics - 60.0<br>Camping - 100.0<br>Carpentry - 100.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Item ID - 80.0<br>Lumberjacking - 100.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Tinkering - 60.0" + close;
                    bonuses = open + "Advanced Carpentry<br>Limited Weapon skills<br>Limited Armor<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Half Apron<br>Boots<br>Lantern<br>Journal<br><br>Woodworker\'s Kit" + close;
                    emblem = 5583;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 19:
                    classname = "Tailor";
                    description = open + " Selecting Tailoring as your chosen field will ensure you will have plenty to do.<br> Though some people have a tendency to do their adventuring unclothed, the more normal people around will be seeking you out from time to time looking to purchase your wares. There are many craftable items here that will require your talents in one way or another and as your skills increase, you will have access to dying tubs that will bring out vibrant colors in even the most mundane articles of clothing and leatherwork." + close;
                    skilltree = open + "Swordsmanship - 40.0<br>Macefighting - 40.0<br>Fencing - 80.0<br>Aim - 40.0<br>Wrestling - 40.0<br>Tactics - 60.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cartography - 80.0<br>Cooking - 80.0<br>Fishing - 100.0<br>Healing - 80.0<br>Item ID - 80.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Tailoring - 100.0" + close;
                    bonuses = open + "Advanced Tailoring<br>Limited Weapon skills<br>Limited Armor<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Half Apron<br>Boots<br>Lantern<br>Journal<br><br>Tailor\'s Kit" + close;
                    emblem = 5583;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 20:
                    classname = "Tinker";
                    description = open + " Becoming a Tinker can be a rather rewarding occupation since there are certain items in the lands that only you will be able to provide.<br> Rest assured that your time will be well-spent fulfilling special orders and increasing your personal fortune.<br> Your fighting skills will also provide a means of occupying yourself in various hunting expeditions if the urge ever strikes you." + close;
                    skilltree = open + "Swordsmanship - 40.0<br>Macefighting - 40.0<br>Fencing - 60.0<br>Aim - 60.0<br>Wrestling - 40.0<br>Tactics - 60.0<br>Arms Lore - 80.0<br>Blacksmithing - 80.0<br>Bowcrafting - 80.0<br>Camping - 100.0<br>Carpentry - 80.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Item ID - 100.0<br>Lockpicking - 100.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Remove Trap - 100.0<br>Resist Spells - 100.0<br>Tinkering - 100.0" + close;
                    bonuses = open + "Advanced Tinkering<br>Limited Weapon skills<br>Limited Armor<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Full Apron<br>Boots<br>Lantern<br>Journal<br><br>Tinker\'s Kit" + close;
                    emblem = 5583;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 21:
                    classname = "Merchant";
                    description = open + " Though history remembers kings and warriors, the true shepherds of civilization are it's Merchants. Truly nothing has so great and far a reach as the effect of these practitioners of Trade and Manufacturing. Able to specialize in any number of crafts while keeping a good head for business, in the kingdom of economics the Merchant is king." + close;
                    skilltree = open + "Haggling 100.0 <br>Blacksmithing - 40.0<br> Tailoring - 40.0 <br> Carpentry 40.0 <br> Cooking - 40.0 <br> Tinkering - 40.0 <br> Bowcraft/Fletching - 40.0 <br> Swordsmanship - 40.0<br>Macefighting - 40.0<br>Fencing - 40.0<br>Aim - 20.0<br>Wrestling - 40.0<br>Tactics - 40.0<br>Camping - 100.0<br>Cartography - 80.0<br>Cooking - 40.0<br>Fishing - 100.0<br>Healing - 60.0<br>Inscribe - 100.0<br>Item ID - 80.0<br>Lumberjacking - 100.0<br>Mining - 100.0<br>Resist Spells - 100.0<br>Taste ID - 100.0" + close;
                    bonuses = open + "Advanced Cooking<br>Brewing System<br>Limited Weapon skills<br>Limited Armor<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Full Apron<br>Boots<br>Lantern<br>Journal<br><br>Cooking Kit<br>Sewing Kit<br>Fletching Kit<br>Smith's Hammer<br>Carpenter's Saw" + close;
                    emblem = 5583;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;
                /*
            case 21:
                classname = "Cook";
                description = open + " With just a few ingredients you can make a meal fit for a king, even a grand banquet. You can even create a drink befitting that grand meal, from the basic ale to the more elegant wines and ciders<br> Most cooks end up becoming tavernkeepers in the larger cities and even smaller towns. Others offer their services to noble households. Regardless of where your services are provided, as you learn your art and become more renowned you will be well sought after for your culinary delights. To be called to create the king\'s banquet is one of the highest honors that can be bestowed upon a cook..." + close;
                skilltree = open + "Swordsmanship - 40.0<br>Macefighting - 40.0<br>Fencing - 40.0<br>Aim - 40.0<br>Wrestling - 40.0<br>Tactics - 40.0<br>Alchemy - 80.0<br>Camping - 100.0<br>Cartography - 80.0<br>Cooking - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Inscribe - 100.0<br>Item ID - 80.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Taste ID - 100.0" + close;
                bonuses = open + "Advanced Cooking<br>Brewing System<br>Limited Weapon skills<br>Limited Armor<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Full Apron<br>Boots<br>Lantern<br>Journal<br><br>Cooking Kit" + close;
                emblem = 5583;

                m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                break;
                */

                case 22:
                    classname = "Aeromancer";
                    description = open + " Air - the Seeker of Secrets. Students of the Way of Air are the most interested in learning for its own sake. Curious and persistent, their peers consider them flighty because they shift from one subject of study to the next, apparently leaving things unfinished. What they do is try to keep the big picture in sight, always remembering where they left off on any of their studies and ready to pick them up again. The Way of Air values honesty and wit but loathes stagnation.<br>  As a Mage you will learn that you don\'t have to take a chance and get too close to something if you want it killed. Secretive and reclusive, the Mage keeps his own council and finds the company of the Arcanely Challenged to be tiresome. Be forewarned, the Path of the Mage is one of the most difficult as it is wrought with dangers and suspicion.<br> Due to your magical attunement, armor involving ferrous materials can not be equipped, and your combat weapons are limited to the staff and dagger.";
                    skilltree = open + "Swordsmanship - 20.0<br>Macefighting - 60.0<br>Fencing - 60.0<br>Wrestling - 20.0<br>Tactics - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Evaluate Int - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Inscribe - 100.0<br>Item ID - 100.0<br>Magery - 100.0<br>Resist Spells - 100.0<br>Taste ID - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>Magecraft<br>Cannot wear armor<br>Staff & Dagger Only" + close;
                    items = itemopen + "3d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Robe<br>Lantern<br>Journal<br><br>Mage Hat<br>Quarter Staff<br>Spellbook" + close;
                    emblem = 5569;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 23:
                    classname = "Aquamancer";
                    description = open + " Water - the Source of all Life. Serene is the keyword that describes those of the Way of Water. They seek the deeper wisdom of the world and take the time to learn them. They are nurturing and care for the safety of others, although they are not strange to duplicity, always hiding something beneath the surface. Students of Water adapt easily to any circumstance, flowing around their problems with ease. The Way of Water values wisdom and adaptability but loathes recklessness.<br>  As a Mage you will learn that you don\'t have to take a chance and get too close to something if you want it killed. Secretive and reclusive, the Mage keeps his own council and finds the company of the Arcanely Challenged to be tiresome. Be forewarned, the Path of the Mage is one of the most difficult as it is wrought with dangers and suspicion.<br> Due to your magical attunement, armor involving ferrous materials can not be equipped, and your combat weapons are limited to the staff and dagger.";
                    skilltree = open + "Swordsmanship - 20.0<br>Macefighting - 60.0<br>Fencing - 60.0<br>Wrestling - 20.0<br>Tactics - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Evaluate Int - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Inscribe - 100.0<br>Item ID - 100.0<br>Magery - 100.0<br>Resist Spells - 100.0<br>Taste ID - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>Magecraft<br>Cannot wear armor<br>Staff & Dagger Only" + close;
                    items = itemopen + "3d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Robe<br>Lantern<br>Journal<br><br>Mage Hat<br>Quarter Staff<br>Spellbook" + close;
                    emblem = 5569;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 24:
                    classname = "Geomancer";
                    description = open + " Earth - Unflinching Stability. Those who travel the Way of Earth are constant and dogged in their pursuit. The polar opposite of Air, the pursue a task until its completion and are the worst kind of perfectionists. They are reliable but sometimes stubborn, speaking with brutal honesty and always to the point. The Way of Earth values patience and responsibility but loathes slackers.<br>  As a Mage you will learn that you don\'t have to take a chance and get too close to something if you want it killed. Secretive and reclusive, the Mage keeps his own council and finds the company of the Arcanely Challenged to be tiresome. Be forewarned, the Path of the Mage is one of the most difficult as it is wrought with dangers and suspicion.<br> Due to your magical attunement, armor involving ferrous materials can not be equipped, and your combat weapons are limited to the staff and dagger.";
                    skilltree = open + "Swordsmanship - 20.0<br>Macefighting - 60.0<br>Fencing - 60.0<br>Wrestling - 20.0<br>Tactics - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Evaluate Int - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Inscribe - 100.0<br>Item ID - 100.0<br>Magery - 100.0<br>Resist Spells - 100.0<br>Taste ID - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>Magecraft<br>Cannot wear armor<br>Staff & Dagger Only<br>Relies on Strength" + close;
                    items = itemopen + "3d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Robe<br>Lantern<br>Journal<br><br>Mage Hat<br>Quarter Staff<br>Spellbook" + close;
                    emblem = 5569;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 25:
                    classname = "Pyromancer";
                    description = open + " Fire - the Cleansing Change. Shifting and energetic, these students are powderkegs ready to detonate. Whatever they undertake, they do so with the utmost enthusiasm, devoting all of their strength. Their demeanor can be exhausting to others, and the Fire followers are said to devour everything they come in contact with. One sure thing is that whatever they touch with powers or personality will never be the same. The Way of Fire values energy and drive but loathes passivity.<br>  As a Mage you will learn that you don\'t have to take a chance and get too close to something if you want it killed. Secretive and reclusive, the Mage keeps his own council and finds the company of the Arcanely Challenged to be tiresome. Be forewarned, the Path of the Mage is one of the most difficult as it is wrought with dangers and suspicion.<br> Due to your magical attunement, armor involving ferrous materials can not be equipped, and your combat weapons are limited to the staff and dagger.";
                    skilltree = open + "Swordsmanship - 20.0<br>Macefighting - 60.0<br>Fencing - 60.0<br>Wrestling - 20.0<br>Tactics - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Evaluate Int - 100.0<br>Fishing - 100.0<br>Healing - 60.0<br>Inscribe - 100.0<br>Item ID - 100.0<br>Magery - 100.0<br>Resist Spells - 100.0<br>Taste ID - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>Magecraft<br>Cannot wear armor<br>Staff & Dagger Only" + close;
                    items = itemopen + "3d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Robe<br>Lantern<br>Journal<br><br>Mage Hat<br>Quarter Staff<br>Spellbook" + close;
                    emblem = 5569;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 26:
                    classname = "Necromancer";
                    description = open + " Restoring a semblance of life to the discarded husks of flesh and bone, the necromancer doesn\'t walk the fine line between life and death so much as rape and manipulate it. They depend on a deep knowledge of anatomy and the dark powers to fuel their creations. Most of their magic resides in creatures and spirits raised from the dead rather than showy spells and illusions. They dig up bodies, dissect them, and pry the secrets of animation from their remains.<br> A Necromancer\'s strength is in his creations and his link to the afterlife. The ability to manipulate both body and soul is everything; material wealth, charisma, and even family/social standing are nothing.<br> As with a Mage, armor involving ferrous materials can not be equipped, and your combat weapons are limited to the staff and dagger.";
                    skilltree = open + "Swordsmanship - 20.0<br>Macefighting - 60.0<br>Fencing - 60.0<br>Wrestling - 20.0<br>Tactics - 60.0<br>Alchemy - 100.0<br>Anatomy - 100.0<br>Camping - 100.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Evaluate Int - 100.0<br>Fishing - 100.0<br>Forensic Eval - 100.0<br>Healing - 60.0<br>Hiding - 100.0<br>Inscribe - 100.0<br>Item ID - 100.0<br>Necromancy - 100.0<br><br>Magery - 100.0<br>Poisoning - 100.0<br>Resist Spells - 100.0<br>Spirit Speak - 100.0<br>Stealth - 100.0<br>Taste ID - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>Magecraft<br>Cannot wear armor<br>Staff & Dagger Only<br>Karma Range lower     than +500" + close;
                    items = itemopen + "3d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Robe<br>Lantern<br>Journal<br><br>Mage Hat<br>Quarter Staff<br>Spellbook" + close;
                    emblem = 5557;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;
                case 27:
                    classname = "MageSlayer";
                    description = open + "It is a strange gift for some to feel the flowing conduits of the ether that let one master the magical arts, and far rarer for one when faced with this ability turns it back against itself. The MageSlayer is not himself a magic user, though he understands their arts. It is with this understanding that he is capable of clouding the pathways of the ether and disrupting the flow of magic. But even with their aptitude for understanding the flow of magic, they find the sword or the bow to be more practical means of dispatching a foe.";
                    skilltree = open + "Swordsmanship - 100.0<br>Fencing - 60.0<br>Aim - 100.0<br>Wrestling - 60.0<br>Tactics - 100.0<br>Parrying - 80.0<br>Anatomy - 80.0<br>Bowcraft - 40.0<br>Camping - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Healing - 80.0<br>Hiding - 100.0<br>Lumberjacking - 60.0<br>Magery - 60.0<br>Mining - 60.0<br>Resist Spells - 120.0<br>Stealth - 100.0<br>Tracking - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Cannot wear Medium Armor" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Longsword<br>Oak Shortbow<br>Quiver of Arrows" + close;
                    emblem = 5575;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;
                case 28:
                    classname = "Defender";
                    description = open + " The Dwarven Defender is a paragon of the dwarven way of life. Stalwart, unwavering and as unmovable as the mountains in which they reside. As the name might imply, this character is a skilled combatant trained in the arts of defense. A line of dwarven defenders is a far better defense than a 10-foot-thick wall of stones, and much more dangerous.";
                    skilltree = open + "Swordsmanship - 100.0<br>Macefighting - 100.0<br>Fencing - 100.0<br>Wrestling - 80.0<br>Tactics - 100.0<br>Parrying - 100.0<br>Anatomy - 80.0<br>Arms Lore - 100.0<br>Camping - 100.0<br>Cartography - 60.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Longsword<br>Leather<br>Wooden Shield" + close;
                    emblem = 5577;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;
                case 29:
                    classname = "Savage";
                    description = open + " The Savage can be one of the most deadly of adversaries. A Savage has learned to harness the blood rage that is infamous to Barbarians.<br> A Savage is able to focus this rage at will making him stronger and faster than would otherwise be mortally possible. This disadvantage is that when the rage ends, the Savage is physically drained thus leaving an unprepared Savage vulnerable to counterattack. Because Savages need complete freedom for the blood rage, they do not wear any form of metal armor including platemail." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Macefighting - 100.0<br>Fencing - 60.0<br>Aim - 60.0<br>Wrestling - 100.0<br>Tactics - 100.0<br>Parrying - 60.0<br>Arms Lore - 80.0<br>Anatomy - 60.0<br>Camping - 100.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Tracking - 80.0<br>Veterinary - 80.0" + close;
                    bonuses = open + "Class Feats & Items<br>No spellcasting ability<br>NO ARMOR<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "4d4 x 20 Gold<br>Short Pants<br>Sandals<br>Torches<br>Journal<br><br>Iron Longsword" + close;
                    emblem = 5585;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;

                case 30:
                    classname = "Strider";
                    description = open + " The Strider is a hunter and a swordsman. He is skilled with weapons, especially the bow and the sword, and is knowledgeable in tracking and woodscraft.<br> Some Striders often protect and guide lost travelers and honest peasant-folk through the wilderness both for their sake and the Forest itself. Others prefer protecting the Forest through harsher means, either by hunting down those who would destroy the essence of Gaia and her children or by keeping the creature populace in check, slaying as needed but letting nothing waste.<br> Striders have the ability to befriend forest animals and they are exceptionally skilled at camouflage in the wilderness. The Strider is a deadly adversary indeed when confronted in the wilderness..." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Fencing - 60.0<br>Aim - 100.0<br>Wrestling - 60.0<br>Tactics - 100.0<br>Parrying - 80.0<br>Anatomy - 80.0<br>Animal Lore - 100.0<br>Animal Taming - 100.0<br>Bowcraft - 40.0<br>Camping - 100.0<br>Cartography - 100.0<br>Cooking - 60.0<br>Detect Hidden - 100.0<br>Fishing - 100.0<br>Healing - 80.0<br>Hiding - 100.0<br>Lumberjacking - 60.0<br>Magery - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0<br>Stealth - 100.0<br>Tracking - 100.0<br>Veterinary - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Cannot wear Plate Armor<br>Cannot use wands/scrolls" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Longsword<br>Oak Shortbow<br>Quiver of Arrows<br>Leather" + close;
                    emblem = 5575;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;
                case 31:
                    classname = "Ravager";
                    description = open + " The Ravager stalks the darkened caves and cooridors of the underdark unafraid. Well versed in treachery and duplicity they have risen in the ranks to a mastery of combat, boldly facing the dangers of the dark and ready to return every underhanded attack in equal measure." + close;
                    skilltree = open + "Swordsmanship - 100.0<br>Fencing - 100.0<br>Aim - 20.0<br>Wrestling - 100.0<br>Tactics - 100.0<br>Poisoning - 100.0<br>Tracking - 100.0<br>Focus - 40.0<br>Parrying - 100.0<br>Anatomy - 80.0<br>Arms Lore - 100.0<br>Camping - 100.0<br>Cartography - 60.0<br>Cooking - 60.0<br>Fishing - 100.0<br>Healing - 60.0<br>Lumberjacking - 60.0<br>Mining - 60.0<br>Resist Spells - 100.0" + close;
                    bonuses = open + "Class Feats & Items<br>Cannot wear Plate Armor<br>Cannot use ranged weapons" + close;
                    items = itemopen + "6d4 x 20 Gold<br>Plain Shirt<br>Long Pants<br>Boots<br>Lantern<br>Journal<br><br>Iron Longsword<br><br>Leather" + close;
                    emblem = 5575;

                    m.SendGump(new ClassInfoGump(classname, description, skilltree, bonuses, items, emblem, m));

                    break;
                    
            }
        }
    }

    public class ClassInfoGump : Gump
    {
        string classs;

        public bool Get20(IAccount a, TeiravonMobile.Race race)
        {
            bool Has20 = false;
            int chars = a.Length;
            for (int i = 0; i < chars; ++i)
            {
                TeiravonMobile m = (TeiravonMobile)a[i];

                if (m == null)
                    continue;
                if (m.PlayerLevel > 19 && m.PlayerRace == race)
                    Has20 = true;
            }
            return Has20;
        }

        public ClassInfoGump(string classname, string description, string skilltree, string bonuses, string items, int emblem, Mobile from)
            : base(150, 30)
        {
            Closable = false;
            Disposable = false;
            Resizable = false;

            TeiravonMobile m = (TeiravonMobile)from;
            classs = classname;

            m.CloseGump(typeof(ClassInfoGump));

            AddPage(0);

            // Class Description
            AddBackground(0, 52, 315, 430, 2600);

            AddImage(117, 14, 1417);
            AddImage(40, 30, 1419);
            AddImage(126, 22, emblem);

            AddLabel(65, 95, 150, classname + " Selection Menu");
            AddHtml(35, 130, 260, 300, description, false, true);

            AddLabel(48, 440, 1152, " Accept>              <Decline");

            AddButton(103, 442, 216, 6501, 1, GumpButtonType.Reply, 0);			// 1 = accept
            AddButton(195, 442, 216, 6051, 2, GumpButtonType.Reply, 0);			// 2 = decline

            // Skill Tree
            AddBackground(320, 52, 250, 260, 2600);

            AddLabel(393, 70, 150, "Skill Tree *");
            AddHtml(350, 97, 200, 160, skilltree, false, true);
            AddLabel(375, 260, 150, "*");

            AddHtml(380, 260, 150, 45, "<basefont size=\"2\" color=\"#0f0f0f\"><i><b> NOTE:</b>All other skills default to 0.0 each</i></basefont>\"", false, false);

            // Bonuses & Restrictions
            AddBackground(320, 315, 250, 180, 2600);

            AddLabel(370, 335, 150, "Bonuses/Restrictions");
            AddHtml(365, 365, 180, 105, bonuses, false, false);

            // Starting Items
            AddBackground(575, 117, 215, 315, 2600);

            AddLabel(630, 135, 150, "Starting Items");
            AddHtml(614, 170, 150, 250, items, false, false);

        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            TeiravonMobile m = (TeiravonMobile)sender.Mobile;

            if (info.ButtonID == 1)
            {
                BankBox m_Bank = m.BankBox;
                m.Skills.Musicianship.Cap = 100.0;
                m.Skills.Musicianship.Base = 10.0;
                m.Skills.DetectHidden.Cap = 100.0;
                m.Skills.DetectHidden.Base = 10.0;
                m.Skills.Archery.Cap = 15.0;
                m.Skills.Ninjitsu.Cap = 60.0;
                m.Skills.Ninjitsu.Base = 10.0;

                switch (classs.ToLower())
                {
                    case "fighter":
                        m.PlayerClass = TeiravonMobile.Class.Fighter;

                        m_Bank.DropItem(new FighterBag());

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Macing.Base = 30.0;
                        m.Skills.Macing.Cap = 100.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

                        m.Skills.Archery.Base = 30.0;
                        m.Skills.Archery.Cap = 100.0;

                        m.Skills.Wrestling.Base = 30.0;
                        m.Skills.Wrestling.Cap = 100.0;

                        m.Skills.Parry.Base = 30.0;
                        m.Skills.Parry.Cap = 100.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 10.0;
                        m.Skills.Cartography.Cap = 60.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.ArmsLore.Base = 30.0;
                        m.Skills.ArmsLore.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        break;

                    case "monk":
                        m.PlayerClass = TeiravonMobile.Class.Monk;

                        m_Bank.DropItem(new MonkBag());

                        m.Skills.Ninjitsu.Base = 30.0;
                        m.Skills.Ninjitsu.Cap = 100.0;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Wrestling.Base = 30.0;
                        m.Skills.Wrestling.Cap = 100.0;

                        m.Skills.Macing.Base = 30.0;
                        m.Skills.Macing.Cap = 100.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 30.0;
                        m.Skills.Parry.Cap = 100.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 100.0;

                        m.Skills.ArmsLore.Base = 30.0;
                        m.Skills.ArmsLore.Cap = 60.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 10.0;
                        m.Skills.Cartography.Cap = 60.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        break;
                    case "raider":
                    case "cavalier":
                        m.PlayerClass = TeiravonMobile.Class.Cavalier;

                        m_Bank.DropItem(new FighterBag());

                        m.RidingSkill = 2;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Macing.Base = 30.0;
                        m.Skills.Macing.Cap = 100.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 30.0;
                        m.Skills.Parry.Cap = 100.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.ArmsLore.Base = 30.0;
                        m.Skills.ArmsLore.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 10.0;
                        m.Skills.Cartography.Cap = 60.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        break;

                    case "berserker":
                        m.PlayerClass = TeiravonMobile.Class.Berserker;

                        m_Bank.DropItem(new BerserkerBag());

                        m.Skills.Ninjitsu.Base = 20.0;
                        m.Skills.Ninjitsu.Cap = 80.0;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Macing.Base = 30.0;
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

                        break;

                    case "savage":
                        if (!Get20(m.Account, TeiravonMobile.Race.Orc))
                            return;
                        m.PlayerClass = TeiravonMobile.Class.Savage;

                        m_Bank.DropItem(new BerserkerBag());

                        m.Skills.Ninjitsu.Base = 30.0;
                        m.Skills.Ninjitsu.Cap = 100.0;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Macing.Base = 30.0;
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
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tracking.Base = 20.0;
                        m.Skills.Tracking.Cap = 80.0;

                        m.Skills.Veterinary.Base = 20.0;
                        m.Skills.Veterinary.Cap = 80.0;

                        m.Skills.Anatomy.Base = 30.0;
                        m.Skills.Anatomy.Cap = 100.0;

                        m.Skills.Focus.Cap = 40.0;
                        m.Skills.Focus.Base = 10.0;

                        break;

                    case "strider":
                        if (!Get20(m.Account, TeiravonMobile.Race.Elf))
                            return;
                        m.PlayerClass = TeiravonMobile.Class.Strider;

                        m_Bank.DropItem(new RangerBag());

                        m.RidingSkill = 1;
                        
                        m.Magicable = true;

                        m.SetSpell(Teiravon.Spells.HealSpell, true);
                        m.SetSpell(Teiravon.Spells.CureSpell, true);
                        m.SetSpell(Teiravon.Spells.BlessSpell, true);

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Fencing.Base = 10.0;
                        m.Skills.Fencing.Cap = 60.0;

                        m.Skills.Archery.Base = 30.0;
                        m.Skills.Archery.Cap = 100.0;

                        m.Skills.Wrestling.Base = 10.0;
                        m.Skills.Wrestling.Cap = 60.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 20.0;
                        m.Skills.Parry.Cap = 80.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.AnimalLore.Base = 30.0;
                        m.Skills.AnimalLore.Cap = 100.0;

                        m.Skills.AnimalTaming.Base = 30.0;
                        m.Skills.AnimalTaming.Cap = 100.0;

                        m.Skills.Fletching.Base = 5.0;
                        m.Skills.Fletching.Cap = 40.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Cartography.Base = 30.0;
                        m.Skills.Cartography.Cap = 100.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Magery.Base = 10.0;
                        m.Skills.Magery.Cap = 60.0;

                        m.Skills.Snooping.Base = 30.0;
                        m.Skills.Snooping.Cap = 100.0;

                        m.Skills.Tracking.Base = 30.0;
                        m.Skills.Tracking.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;

                        m.Skills.Veterinary.Base = 30.0;
                        m.Skills.Veterinary.Cap = 100.0;

                        m.Skills.Forensics.Base = 30.0;
                        m.Skills.Forensics.Cap = 100.0;

                        m.Skills.ArmsLore.Base = 30.0;
                        m.Skills.ArmsLore.Cap = 100.0;

                        break;

                    case "dragoon":
                        m.PlayerClass = TeiravonMobile.Class.Dragoon;

                        m_Bank.DropItem(new DragoonBag());

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Macing.Base = 10.0;
                        m.Skills.Macing.Cap = 60.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

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

                        break;

                    case "scoundrel":
                        m.PlayerClass = TeiravonMobile.Class.Scoundrel;

                        m_Bank.DropItem(new ThiefBag());

                        m.Skills.Ninjitsu.Base = 30.0;
                        m.Skills.Ninjitsu.Cap = 100.0;

                        m.Skills.Swords.Base = 10.0;
                        m.Skills.Swords.Cap = 60.0;

                        m.Skills.Archery.Base = 20.0;
                        m.Skills.Archery.Cap = 60.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 10.0;
                        m.Skills.Parry.Cap = 60.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.Begging.Base = 30.0;
                        m.Skills.Begging.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;




                        m.Skills.Lockpicking.Base = 30.0;
                        m.Skills.Lockpicking.Cap = 100.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.RemoveTrap.Base = 30.0;
                        m.Skills.RemoveTrap.Cap = 100.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Snooping.Base = 30.0;
                        m.Skills.Snooping.Cap = 100.0;

                        m.Skills.Stealing.Base = 30.0;
                        m.Skills.Stealing.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;

                        break;

                    case "assassin":
                        m.PlayerClass = TeiravonMobile.Class.Assassin;

                        m_Bank.DropItem(new AssassinBag());

                        m.Skills.Ninjitsu.Base = 30.0;
                        m.Skills.Ninjitsu.Cap = 100.0;

                        m.Skills.Swords.Base = 20.0;
                        m.Skills.Swords.Cap = 80.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

                        m.Skills.Archery.Base = 10.0;
                        m.Skills.Archery.Cap = 60.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 10.0;
                        m.Skills.Parry.Cap = 60.0;

                        m.Skills.Alchemy.Base = 20.0;
                        m.Skills.Alchemy.Cap = 100.0;

                        m.Skills.Anatomy.Base = 30.0;
                        m.Skills.Anatomy.Cap = 100.0;

                        m.Skills.ArmsLore.Base = 20.0;
                        m.Skills.ArmsLore.Cap = 80.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;




                        m.Skills.Lockpicking.Base = 30.0;
                        m.Skills.Lockpicking.Cap = 100.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.Poisoning.Base = 30.0;
                        m.Skills.Poisoning.Cap = 100.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;

                        m.Skills.TasteID.Base = 20.0;
                        m.Skills.TasteID.Cap = 80.0;

                        m.Skills.Tracking.Base = 20.0;
                        m.Skills.Tracking.Cap = 80.0;

                        break;

                    case "bard":
                        m.PlayerClass = TeiravonMobile.Class.Bard;

                        m_Bank.DropItem(new BardBag());

                        m.Skills.Ninjitsu.Base = 30.0;
                        m.Skills.Ninjitsu.Cap = 100.0;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

                        m.Skills.Archery.Base = 10.0;
                        m.Skills.Archery.Cap = 60.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 10.0;
                        m.Skills.Parry.Cap = 60.0;

                        m.Skills.Musicianship.Base = 30.0;
                        m.Skills.Musicianship.Cap = 100.0;

                        m.Skills.Anatomy.Base = 30.0;
                        m.Skills.Anatomy.Cap = 100.0;

                        m.Skills.ArmsLore.Base = 20.0;
                        m.Skills.ArmsLore.Cap = 80.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;




                        m.Skills.Lockpicking.Base = 20.0;
                        m.Skills.Lockpicking.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.Provocation.Base = 30.0;
                        m.Skills.Provocation.Cap = 100.0;

                        m.Skills.Peacemaking.Base = 30.0;
                        m.Skills.Peacemaking.Cap = 100.0;

                        m.Skills.Discordance.Base = 30.0;
                        m.Skills.Discordance.Cap = 100.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;

                        m.Skills.TasteID.Base = 20.0;
                        m.Skills.TasteID.Cap = 80.0;

                        m.Skills.Tracking.Base = 20.0;
                        m.Skills.Tracking.Cap = 80.0;
                        break;

                    case "ranger":
                        m.PlayerClass = TeiravonMobile.Class.Ranger;

                        m_Bank.DropItem(new RangerBag());

                        m.RidingSkill = 1;

                        m.Magicable = true;

                        m.SetSpell(Teiravon.Spells.HealSpell, true);
                        m.SetSpell(Teiravon.Spells.GreaterHealSpell, true);
                        m.SetSpell(Teiravon.Spells.CureSpell, true);
                        m.SetSpell(Teiravon.Spells.ProtectionSpell, true);
                        m.SetSpell(Teiravon.Spells.ArchCureSpell, true);
                        m.SetSpell(Teiravon.Spells.ArchProtectionSpell, true);

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Fencing.Base = 10.0;
                        m.Skills.Fencing.Cap = 60.0;

                        m.Skills.Archery.Base = 30.0;
                        m.Skills.Archery.Cap = 100.0;

                        m.Skills.Wrestling.Base = 10.0;
                        m.Skills.Wrestling.Cap = 60.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 20.0;
                        m.Skills.Parry.Cap = 80.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.AnimalLore.Base = 30.0;
                        m.Skills.AnimalLore.Cap = 100.0;

                        m.Skills.AnimalTaming.Base = 30.0;
                        m.Skills.AnimalTaming.Cap = 100.0;

                        m.Skills.Fletching.Base = 5.0;
                        m.Skills.Fletching.Cap = 40.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Cartography.Base = 30.0;
                        m.Skills.Cartography.Cap = 100.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;




                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Magery.Base = 30.0;
                        m.Skills.Magery.Cap = 100.0;

                        m.Skills.Snooping.Base = 30.0;
                        m.Skills.Snooping.Cap = 100.0;

                        m.Skills.Tracking.Base = 30.0;
                        m.Skills.Tracking.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;

                        m.Skills.Veterinary.Base = 30.0;
                        m.Skills.Veterinary.Cap = 100.0;

                        m.Skills.Forensics.Base = 30.0;
                        m.Skills.Forensics.Cap = 100.0;

                        break;

                    case "archer":
                        m.PlayerClass = TeiravonMobile.Class.Archer;

                        m_Bank.DropItem(new ArcherBag());

                        m.Skills.Ninjitsu.Base = 20.0;
                        m.Skills.Ninjitsu.Cap = 80.0;

                        m.Skills.Swords.Base = 10.0;
                        m.Skills.Swords.Cap = 60.0;

                        m.Skills.Fencing.Base = 10.0;
                        m.Skills.Fencing.Cap = 60.0;

                        m.Skills.Archery.Base = 30.0;
                        m.Skills.Archery.Cap = 100.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 10.0;
                        m.Skills.Parry.Cap = 60.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.ArmsLore.Base = 10.0;
                        m.Skills.ArmsLore.Cap = 60.0;

                        m.Skills.Fletching.Base = 5.0;
                        m.Skills.Fletching.Cap = 40.0;

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
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tracking.Base = 20.0;
                        m.Skills.Tracking.Cap = 80.0;

                        m.Skills.Forensics.Base = 30.0;
                        m.Skills.Forensics.Cap = 100.0;

                        break;

                    case "cleric":
                        m.PlayerClass = TeiravonMobile.Class.Cleric;

                        m_Bank.DropItem(new ClericBag());

                        m.AddToBackpack(new HolySymbol(HolySymbol.SymbolType.Cleric));

                        m.Magicable = true;

                        m.SetSpell(Teiravon.Spells.HealSpell, true);
                        m.SetSpell(Teiravon.Spells.HarmSpell, true);
                        m.SetSpell(Teiravon.Spells.GreaterHealSpell, true);
                        m.SetSpell(Teiravon.Spells.BlessSpell, true);
                        m.SetSpell(Teiravon.Spells.LightningSpell, true);
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

                        m.Skills.Macing.Base = 20.0;
                        m.Skills.Macing.Cap = 80.0;

                        m.Skills.Wrestling.Base = 20.0;
                        m.Skills.Wrestling.Cap = 80.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 10.0;
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

                        m.Skills.Healing.Base = 30.0;
                        m.Skills.Healing.Cap = 100.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Magery.Base = 30.0;
                        m.Skills.Magery.Cap = 100.0;

                        m.Skills.EvalInt.Base = 0.0;
                        m.Skills.EvalInt.Cap = 75.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.SpiritSpeak.Base = 30.0;
                        m.Skills.SpiritSpeak.Cap = 100.0;

                        m.Skills.TasteID.Base = 20.0;
                        m.Skills.TasteID.Cap = 80.0;

                        m.Skills.Inscribe.Base = 10.0;
                        m.Skills.Inscribe.Cap = 30.0;

                        break;

                    case "dark cleric":
                        m.PlayerClass = TeiravonMobile.Class.DarkCleric;

                        m_Bank.DropItem(new DarkClericBag());

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

                        m.Skills.Macing.Base = 20.0;
                        m.Skills.Macing.Cap = 80.0;

                        m.Skills.Wrestling.Base = 20.0;
                        m.Skills.Wrestling.Cap = 80.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 10.0;
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

                        m.Skills.Healing.Base = 30.0;
                        m.Skills.Healing.Cap = 100.0;

                        m.Skills.EvalInt.Base = 0.0;
                        m.Skills.EvalInt.Cap = 75.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Magery.Base = 30.0;
                        m.Skills.Magery.Cap = 100.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.SpiritSpeak.Base = 30.0;
                        m.Skills.SpiritSpeak.Cap = 100.0;

                        m.Skills.TasteID.Base = 20.0;
                        m.Skills.TasteID.Cap = 80.0;

                        m.Skills.Inscribe.Base = 10.0;
                        m.Skills.Inscribe.Cap = 30.0;

                        break;

                    case "forester":
                        m.PlayerClass = TeiravonMobile.Class.Forester;

                        m_Bank.DropItem(new ForesterBag());
                        m.SetLanguages(TeiravonMobile.LLupine, true);

                        m.Magicable = true;

                        m.SetSpell(Teiravon.Spells.HealSpell, true);
                        m.SetSpell(Teiravon.Spells.CreateFoodSpell, true);
                        m.SetSpell(Teiravon.Spells.NightSightSpell, true);
                        m.SetSpell(Teiravon.Spells.CureSpell, true);
                        m.SetSpell(Teiravon.Spells.ProtectionSpell, true);
                        m.SetSpell(Teiravon.Spells.HarmSpell, true);
                        m.SetSpell(Teiravon.Spells.BlessSpell, true);
                        m.SetSpell(Teiravon.Spells.WallOfStoneSpell, true);
                        m.SetSpell(Teiravon.Spells.PoisonSpell, true);
                        m.SetSpell(Teiravon.Spells.ArchProtectionSpell, true);
                        m.SetSpell(Teiravon.Spells.ArchCureSpell, true);
                        m.SetSpell(Teiravon.Spells.GreaterHealSpell, true);
                        m.SetSpell(Teiravon.Spells.CurseSpell, true);
                        m.SetSpell(Teiravon.Spells.LightningSpell, true);
                        m.SetSpell(Teiravon.Spells.PoisonFieldSpell, true);
                        m.SetSpell(Teiravon.Spells.SummonCreatureSpell, true);
                        m.SetSpell(Teiravon.Spells.MassCurseSpell, true);
                        m.SetSpell(Teiravon.Spells.RevealSpell, true);
                        m.SetSpell(Teiravon.Spells.ChainLightningSpell, true);
                        m.SetSpell(Teiravon.Spells.PolymorphSpell, true);
                        m.SetSpell(Teiravon.Spells.EarthquakeSpell, true);
                        m.SetSpell(Teiravon.Spells.AirElementalSpell, true);
                        m.SetSpell(Teiravon.Spells.EarthElementalSpell, true);
                        m.SetSpell(Teiravon.Spells.WaterElementalSpell, true);

                        m.SetSpell(Teiravon.Spells.BlessSpell, true);
                        m.SetSpell(Teiravon.Spells.ResurrectionSpell, true);
                        m.SetSpell(Teiravon.Spells.StrengthSpell, true);
                        m.SetSpell(Teiravon.Spells.AgilitySpell, true);
                        m.SetSpell(Teiravon.Spells.CunningSpell, true);
                        m.SetSpell(Teiravon.Spells.DispelSpell, true);
                        m.SetSpell(Teiravon.Spells.MassDispelSpell, true);

                        m.Skills.Swords.Base = 0.0;
                        m.Skills.Swords.Cap = 20.0;

                        m.Skills.Macing.Base = 20.0;
                        m.Skills.Macing.Cap = 80.0;

                        m.Skills.Fencing.Base = 0.0;
                        m.Skills.Fencing.Cap = 20.0;

                        m.Skills.Wrestling.Base = 10.0;
                        m.Skills.Wrestling.Cap = 60.0;

                        m.Skills.Tactics.Base = 20.0;
                        m.Skills.Tactics.Cap = 80.0;

                        m.Skills.Parry.Base = 20.0;
                        m.Skills.Parry.Cap = 80.0;

                        m.Skills.Alchemy.Base = 20.0;
                        m.Skills.Alchemy.Cap = 100.0;

                        m.Skills.Inscribe.Base = 0.0;
                        m.Skills.Inscribe.Cap = 100.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.AnimalLore.Base = 30.0;
                        m.Skills.AnimalLore.Cap = 100.0;

                        m.Skills.AnimalTaming.Base = 30.0;
                        m.Skills.AnimalTaming.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 20.0;
                        m.Skills.Cartography.Cap = 80.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;

                        m.Skills.Herding.Base = 30.0;
                        m.Skills.Herding.Cap = 100.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Magery.Base = 30.0;
                        m.Skills.Magery.Cap = 100.0;

                        m.Skills.EvalInt.Base = 20.0;
                        m.Skills.EvalInt.Cap = 70.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tracking.Base = 20.0;
                        m.Skills.Tracking.Cap = 80.0;

                        m.Skills.Veterinary.Base = 30.0;
                        m.Skills.Veterinary.Cap = 100.0;

                        break;

                    case "shapeshifter":
                        m.PlayerClass = TeiravonMobile.Class.Shapeshifter;

                        m_Bank.DropItem(new ShapeshifterBag());
                        m.SetLanguages(TeiravonMobile.LLupine, true);

                        m.Skills.Ninjitsu.Base = 30.0;
                        m.Skills.Ninjitsu.Cap = 100.0;

                        m.Skills.Swords.Base = 0.0;
                        m.Skills.Swords.Cap = 20.0;

                        m.Skills.Macing.Base = 20.0;
                        m.Skills.Macing.Cap = 80.0;

                        m.Skills.Fencing.Base = 0.0;
                        m.Skills.Fencing.Cap = 20.0;

                        m.Skills.Archery.Base = 0.0;
                        m.Skills.Archery.Cap = 20.0;

                        m.Skills.Inscribe.Base = 0.0;
                        m.Skills.Inscribe.Cap = 80.0;

                        m.Skills.Wrestling.Base = 30.0;
                        m.Skills.Wrestling.Cap = 100.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Alchemy.Base = 20.0;
                        m.Skills.Alchemy.Cap = 60.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.AnimalLore.Base = 30.0;
                        m.Skills.AnimalLore.Cap = 100.0;

                        m.Skills.AnimalTaming.Base = 30.0;
                        m.Skills.AnimalTaming.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 20.0;
                        m.Skills.Cartography.Cap = 80.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Herding.Base = 30.0;
                        m.Skills.Herding.Cap = 100.0;




                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tracking.Base = 20.0;
                        m.Skills.Tracking.Cap = 80.0;

                        m.Skills.Veterinary.Base = 30.0;
                        m.Skills.Veterinary.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;

                        break;

                    case "necromancer":
                        m.PlayerClass = TeiravonMobile.Class.Necromancer;

                        m_Bank.DropItem(new NecroBag());

                        m.AddToBackpack(new HolySymbol(HolySymbol.SymbolType.Necromancer));

                        MageConfig(m);

                        m.Skills.Necromancy.Base = 30.0;
                        m.Skills.Necromancy.Cap = 100.0;

                        m.Skills.SpiritSpeak.Base = 30.0;
                        m.Skills.SpiritSpeak.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;




                        break;

                    case "aeromancer":
                        m.PlayerClass = TeiravonMobile.Class.Aeromancer;

                        m_Bank.DropItem(new MageBag());

                        MageConfig(m);

                        break;

                    case "aquamancer":
                        m.PlayerClass = TeiravonMobile.Class.Aquamancer;
                        if (m.PlayerRace == TeiravonMobile.Race.Frostling)
                            XmlAttach.AttachTo(m, new XmlData("Cryomancer"));
                        m_Bank.DropItem(new MageBag());

                        MageConfig(m);

                        break;

                    case "geomancer":
                        m.PlayerClass = TeiravonMobile.Class.Geomancer;

                        m_Bank.DropItem(new MageBag());

                        MageConfig(m);

                        break;

                    case "pyromancer":
                        m.PlayerClass = TeiravonMobile.Class.Pyromancer;

                        m_Bank.DropItem(new MageBag());

                        MageConfig(m);

                        break;

                    case "alchemist":
                        m.PlayerClass = TeiravonMobile.Class.Alchemist;

                        m_Bank.DropItem(new AlchemistBag());

                        m.Skills.Swords.Base = 5.0;
                        m.Skills.Swords.Cap = 40.0;

                        m.Skills.Macing.Base = 5.0;
                        m.Skills.Macing.Cap = 40.0;

                        m.Skills.Fencing.Base = 5.0;
                        m.Skills.Fencing.Cap = 40.0;

                        m.Skills.Archery.Base = 5.0;
                        m.Skills.Archery.Cap = 40.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 5.0;
                        m.Skills.Tactics.Cap = 40.0;

                        m.Skills.Alchemy.Base = 30.0;
                        m.Skills.Alchemy.Cap = 100.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 20.0;
                        m.Skills.Cartography.Cap = 80.0;

                        m.Skills.Cooking.Base = 20.0;
                        m.Skills.Cooking.Cap = 80.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;

                        m.Skills.Inscribe.Base = 30.0;
                        m.Skills.Inscribe.Cap = 100.0;

                        m.Skills.ItemID.Base = 20.0;
                        m.Skills.ItemID.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.Poisoning.Base = 20.0;
                        m.Skills.Poisoning.Cap = 80.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.TasteID.Base = 30.0;
                        m.Skills.TasteID.Cap = 100.0;

                        break;

                    case "metalworker":
                        m.PlayerClass = TeiravonMobile.Class.Blacksmith;

                        m_Bank.DropItem(new BlacksmithBag());
                        m_Bank.DropItem(new TinkerBag());

                        m.Skills.Swords.Base = 5.0;
                        m.Skills.Swords.Cap = 40.0;

                        m.Skills.Macing.Base = 20.0;
                        m.Skills.Macing.Cap = 80.0;

                        m.Skills.Fencing.Base = 5.0;
                        m.Skills.Fencing.Cap = 40.0;

                        m.Skills.Wrestling.Base = 20.0;
                        m.Skills.Wrestling.Cap = 80.0;

                        m.Skills.Tactics.Base = 10.0;
                        m.Skills.Tactics.Cap = 60.0;

                        m.Skills.ArmsLore.Base = 30.0;
                        m.Skills.ArmsLore.Cap = 100.0;

                        m.Skills.TasteID.Base = 30.0;
                        m.Skills.TasteID.Cap = 100.0;

                        m.Skills.Blacksmith.Base = 30.0;
                        if (m.IsDwarf())
                            m.Skills.Blacksmith.Cap = 105.0;
                        else
                            m.Skills.Blacksmith.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;


                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.ItemID.Base = 10.0;
                        m.Skills.ItemID.Cap = 60.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 30.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 105.0;
                        else
                            m.Skills.Mining.Cap = 100.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tinkering.Base = 20.0;
                        m.Skills.Tinkering.Cap = 100.0;

                        break;

                    case "bowyer":
                        m.PlayerClass = TeiravonMobile.Class.Bowyer;

                        m_Bank.DropItem(new BowyerBag());

                        m.Skills.Swords.Base = 5.0;
                        m.Skills.Swords.Cap = 40.0;

                        m.Skills.Macing.Base = 5.0;
                        m.Skills.Macing.Cap = 40.0;

                        m.Skills.Fencing.Base = 5.0;
                        m.Skills.Fencing.Cap = 40.0;

                        m.Skills.Archery.Base = 20.0;
                        m.Skills.Archery.Cap = 80.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 10.0;
                        m.Skills.Tactics.Cap = 60.0;

                        m.Skills.Fletching.Base = 30.0;
                        m.Skills.Fletching.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Carpentry.Base = 0.0;
                        m.Skills.Carpentry.Cap = 30.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Fishing.Base = 20.0;
                        m.Skills.Fishing.Cap = 80.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.ItemID.Base = 20.0;
                        m.Skills.ItemID.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 30.0;
                        m.Skills.Lumberjacking.Cap = 100.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tinkering.Base = 10.0;
                        m.Skills.Tinkering.Cap = 60.0;

                        break;

                    case "woodworker":
                        m.PlayerClass = TeiravonMobile.Class.Woodworker;

                        m_Bank.DropItem(new WoodworkerBag());

                        m.Skills.Swords.Base = 5.0;
                        m.Skills.Swords.Cap = 40.0;

                        m.Skills.Macing.Base = 20.0;
                        m.Skills.Macing.Cap = 80.0;

                        m.Skills.Fencing.Base = 5.0;
                        m.Skills.Fencing.Cap = 40.0;

                        m.Skills.Archery.Base = 20.0;
                        m.Skills.Archery.Cap = 80.0;

                        m.Skills.Fletching.Base = 30.0;
                        m.Skills.Fletching.Cap = 100.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 10.0;
                        m.Skills.Tactics.Cap = 60.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Carpentry.Base = 30.0;
                        m.Skills.Carpentry.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.ItemID.Base = 20.0;
                        m.Skills.ItemID.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 30.0;
                        m.Skills.Lumberjacking.Cap = 100.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tinkering.Base = 10.0;
                        m.Skills.Tinkering.Cap = 60.0;

                        break;

                    case "tailor":
                        m.PlayerClass = TeiravonMobile.Class.Tailor;

                        m_Bank.DropItem(new SewingBag());

                        m.Skills.Swords.Base = 5.0;
                        m.Skills.Swords.Cap = 40.0;

                        m.Skills.Macing.Base = 5.0;
                        m.Skills.Macing.Cap = 40.0;

                        m.Skills.Fencing.Base = 20.0;
                        m.Skills.Fencing.Cap = 80.0;

                        m.Skills.Archery.Base = 5.0;
                        m.Skills.Archery.Cap = 40.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 10.0;
                        m.Skills.Tactics.Cap = 60.0;

                        m.Skills.Anatomy.Base = 30.0;
                        m.Skills.Anatomy.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 20.0;
                        m.Skills.Cartography.Cap = 80.0;

                        m.Skills.Cooking.Base = 20.0;
                        m.Skills.Cooking.Cap = 80.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;

                        m.Skills.ItemID.Base = 20.0;
                        m.Skills.ItemID.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tinkering.Base = 10.0;
                        m.Skills.Tinkering.Cap = 60.0;

                        m.Skills.Tailoring.Base = 30.0;
                        m.Skills.Tailoring.Cap = 100.0;

                        break;

                    case "tinker":
                        m.PlayerClass = TeiravonMobile.Class.Tinker;

                        m_Bank.DropItem(new TinkerBag());

                        m.Skills.Swords.Base = 5.0;
                        m.Skills.Swords.Cap = 40.0;

                        m.Skills.Macing.Base = 5.0;
                        m.Skills.Macing.Cap = 40.0;

                        m.Skills.Fencing.Base = 10.0;
                        m.Skills.Fencing.Cap = 60.0;

                        m.Skills.Archery.Base = 10.0;
                        m.Skills.Archery.Cap = 60.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 10.0;
                        m.Skills.Tactics.Cap = 60.0;

                        m.Skills.ArmsLore.Base = 20.0;
                        m.Skills.ArmsLore.Cap = 80.0;

                        m.Skills.Blacksmith.Base = 20.0;
                        m.Skills.Blacksmith.Cap = 80.0;

                        m.Skills.Fletching.Base = 20.0;
                        m.Skills.Fletching.Cap = 80.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Carpentry.Base = 20.0;

                        if (m.IsDwarf())
                            m.Skills.Carpentry.Cap = 100.0;
                        else
                            m.Skills.Carpentry.Cap = 80.0;

                        m.Skills.Cartography.Base = 30.0;
                        m.Skills.Cartography.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.ItemID.Base = 30.0;
                        m.Skills.ItemID.Cap = 100.0;

                        m.Skills.Lockpicking.Base = 30.0;
                        m.Skills.Lockpicking.Cap = 100.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.RemoveTrap.Base = 30.0;
                        m.Skills.RemoveTrap.Cap = 100.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Tinkering.Base = 30.0;
                        m.Skills.Tinkering.Cap = 100.0;

                        break;

                    case "cook":
                        m.PlayerClass = TeiravonMobile.Class.Cook;

                        m_Bank.DropItem(new CookBag());

                        m.Skills.Swords.Base = 5.0;
                        m.Skills.Swords.Cap = 40.0;

                        m.Skills.Macing.Base = 5.0;
                        m.Skills.Macing.Cap = 40.0;

                        m.Skills.Fencing.Base = 5.0;
                        m.Skills.Fencing.Cap = 40.0;

                        m.Skills.Archery.Base = 5.0;
                        m.Skills.Archery.Cap = 40.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 5.0;
                        m.Skills.Tactics.Cap = 40.0;

                        m.Skills.Alchemy.Base = 0.0;
                        m.Skills.Alchemy.Cap = 30.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 20.0;
                        m.Skills.Cartography.Cap = 80.0;

                        m.Skills.Cooking.Base = 30.0;
                        m.Skills.Cooking.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.ItemID.Base = 20.0;
                        m.Skills.ItemID.Cap = 80.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        if (m.IsDwarf())
                            m.Skills.Mining.Cap = 90.0;
                        else
                            m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.TasteID.Base = 30.0;
                        m.Skills.TasteID.Cap = 100.0;

                        break;

                    case "merchant":
                        m.PlayerClass = TeiravonMobile.Class.Merchant;

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

                        break;

                    case "mageslayer":
                        m.PlayerClass = TeiravonMobile.Class.MageSlayer;

                        m_Bank.DropItem(new RangerBag());

                        m.RidingSkill = 0;

                        m.Magicable = true;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Fencing.Base = 10.0;
                        m.Skills.Fencing.Cap = 60.0;

                        m.Skills.Archery.Base = 30.0;
                        m.Skills.Archery.Cap = 100.0;

                        m.Skills.Wrestling.Base = 10.0;
                        m.Skills.Wrestling.Cap = 60.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 20.0;
                        m.Skills.Parry.Cap = 80.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.Fletching.Base = 5.0;
                        m.Skills.Fletching.Cap = 40.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.DetectHidden.Base = 30.0;
                        m.Skills.DetectHidden.Cap = 100.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 20.0;
                        m.Skills.Healing.Cap = 80.0;




                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 50.0;
                        m.Skills.MagicResist.Cap = 120.0;

                        m.Skills.Magery.Base = 20.0;
                        m.Skills.Magery.Cap = 60.0;

                        m.Skills.Tracking.Base = 30.0;
                        m.Skills.Tracking.Cap = 100.0;

                        m.Skills.Stealth.Base = 30.0;
                        m.Skills.Stealth.Cap = 100.0;

                        m.Skills.Forensics.Base = 30.0;
                        m.Skills.Forensics.Cap = 100.0;

                        break;

                    case "defender":
                        if (!Get20(m.Account, TeiravonMobile.Race.Dwarf))
                            return;
                        m.PlayerClass = TeiravonMobile.Class.DwarvenDefender;

                        m_Bank.DropItem(new FighterBag());

                        //m.RidingSkill = 2;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Macing.Base = 30.0;
                        m.Skills.Macing.Cap = 100.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

                        m.Skills.Wrestling.Base = 5.0;
                        m.Skills.Wrestling.Cap = 40.0;

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Parry.Base = 60.0;
                        m.Skills.Parry.Cap = 100.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.ArmsLore.Base = 30.0;
                        m.Skills.ArmsLore.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 10.0;
                        m.Skills.Cartography.Cap = 60.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        break;

                    case "ravager":
                        m.PlayerClass = TeiravonMobile.Class.Ravager;

                        m_Bank.DropItem(new SargBag());

                        m.Skills.Tactics.Base = 30.0;
                        m.Skills.Tactics.Cap = 100.0;

                        m.Skills.Tracking.Base = 30.0;
                        m.Skills.Tracking.Cap = 100.0;

                        m.Skills.Swords.Base = 30.0;
                        m.Skills.Swords.Cap = 100.0;

                        m.Skills.Fencing.Base = 30.0;
                        m.Skills.Fencing.Cap = 100.0;

                        m.Skills.Archery.Base = 10.0;
                        m.Skills.Archery.Cap = 20.0;

                        m.Skills.Wrestling.Base = 30.0;
                        m.Skills.Wrestling.Cap = 100.0;

                        m.Skills.Parry.Base = 30.0;
                        m.Skills.Parry.Cap = 100.0;

                        m.Skills.Poisoning.Base = 30.0;
                        m.Skills.Poisoning.Cap = 100.0;

                        m.Skills.Anatomy.Base = 20.0;
                        m.Skills.Anatomy.Cap = 80.0;

                        m.Skills.Focus.Base = 10.0;
                        m.Skills.Focus.Cap = 40.0;

                        m.Skills.MagicResist.Base = 30.0;
                        m.Skills.MagicResist.Cap = 100.0;

                        m.Skills.Camping.Base = 30.0;
                        m.Skills.Camping.Cap = 100.0;

                        m.Skills.Cartography.Base = 10.0;
                        m.Skills.Cartography.Cap = 60.0;

                        m.Skills.Cooking.Base = 10.0;
                        m.Skills.Cooking.Cap = 60.0;

                        m.Skills.Fishing.Base = 30.0;
                        m.Skills.Fishing.Cap = 100.0;

                        m.Skills.Lumberjacking.Base = 10.0;
                        m.Skills.Lumberjacking.Cap = 60.0;

                        m.Skills.Mining.Base = 10.0;
                        m.Skills.Mining.Cap = 60.0;

                        m.Skills.ArmsLore.Base = 30.0;
                        m.Skills.ArmsLore.Cap = 100.0;

                        m.Skills.Healing.Base = 10.0;
                        m.Skills.Healing.Cap = 60.0;

                        break;
                }

                m.RemainingFeats = 2;

                Point3D loc = new Point3D(2301, 992, 0);

                if (m.IsHuman() || m.IsHalfElf())
                {/*
                    if (m.IsEvil())
                    {
                        loc = new Point3D(2880, 2035, 0);
                        m.MoveToWorld(loc, Map.Felucca);
                    }

                    else
                    {*/
                    loc = new Point3D(1932, 1083, 1);
                    m.MoveToWorld(loc, Map.Trammel);
                    //}
                }

                else if (m.IsElf())
                {
                    //loc = new Point3D( 453, 2678, 0 );		// Moon Elf - 675, 3079, 0
                    //loc = new Point3D(3959, 978, 10); //arandor
                    loc = new Point3D(3178, 632, 0);
                    m.MoveToWorld(loc, Map.Felucca);

                    m.Skills.Ninjitsu.Cap += 10;
                }


                else if (m.IsUndead())
                {
                    loc = new Point3D(4519, 3582, 10);
                    m.MoveToWorld(loc, Map.Felucca);
                }
                else if (m.IsFrostling())
                {
                    loc = new Point3D(200, 565, 0);
                    m.MoveToWorld(loc, Map.Tokuno);
                    m.AddToBackpack(new FrostPick());
                    m.AddToBackpack(new SnowPile());
                }
                else if (m.IsDrow())
                {
                    if (m.Skills.Stealth.Cap == 0.0)
                    {
                        m.Skills.Stealth.Cap = 90.0;
                        m.Skills.Stealth.Base = 10.0;
                    }

                    //loc = new Point3D(5223, 189, 5);
                    loc = new Point3D(2870, 2092, 0);
                    m.MoveToWorld(loc, Map.Felucca);
                    m.SendMessage("You join the crowd of estranged drow. The wrath of Lloth is upon all of your kin now, the city of Rilauven overrun. Nearby militant drow look upon you with suspicion, some things for a drow never change...");
                }

                else if (m.IsDwarf() || m.IsGnome())
                {
                    //Dwarf town
                    //loc = new Point3D( 5422, 2344, 0 );

                    //Glindlmor
                    loc = new Point3D(100, 1034, 1);
                    m.MoveToWorld(loc, Map.Malas);
                    m.StoneMining = true;

                    if (m.IsGnome())
                    {
                        if (m.Skills.Tinkering.Cap < 80)
                            m.Skills.Tinkering.Cap += 40;

                        if (m.Skills.MagicResist.Cap < 40)
                            m.Skills.MagicResist.Cap = 40;

                        if (m.Skills.MagicResist.Cap == 100)
                            m.Skills.MagicResist.Cap = 120;

                        if (m.Skills.Tinkering.Cap == 100)
                            m.Skills.Tinkering.Cap = 120;
                    }
                }

                else if (m.IsOrc())
                {
                    //Burzkal Hont-Ob
                    if (m.Hair != null)
                        (m.Hair).Delete();

                    if (m.Beard != null)
                        (m.Beard).Delete();

                    m.EquipItem(new OrcFace(m));

                    if (m.Skills.Macing.Cap == 100.0)
                        m.Skills.Macing.Cap = 105.0;
                    else
                    {
                        if (m.IsDragoon())
                            m.Skills.Macing.Cap = 105.0;
                        else
                            m.Skills.Macing.Cap = 90.0;
                    }

                    loc = new Point3D(5779, 1135, -28);
                    m.MoveToWorld(loc, Map.Felucca);

                    //Ril'Auven
                    //loc = new Point3D( 5223, 189, 5 );

                }

                else if (m.IsGoblin())
                {
                    if (m.Hair != null)
                        (m.Hair).Delete();

                    if (m.Beard != null)
                        (m.Beard).Delete();

                    if (m.Skills.Stealth.Cap < 80)
                        m.Skills.Stealth.Cap = 80;

                    if (m.Skills.Stealth.Cap == 100)
                        m.Skills.Stealth.Cap = 120;

                    m.Skills.Ninjitsu.Cap += 10;

                    m.EquipItem(new GoboFace(m));

                    loc = new Point3D(5779, 1135, -28);
                    m.BodyValue = 17;
                    m.MoveToWorld(loc, Map.Felucca);
                }

                else if (m.IsDuergar())
                {
                    loc = new Point3D(5940, 2526, 0);
                    m.MoveToWorld(loc, Map.Felucca);
                }

                /* if (m.IsForester() || m.IsShapeshifter())
                 {
                     loc = new Point3D(37, 3542, 0);
                     m.MoveToWorld(loc, Map.Felucca);
                 } */
                m.PrivateOverheadMessage(MessageType.Regular, 0x35, true, "Your starting equipment has been placed in your bank.", m.NetState);
                m.PrivateOverheadMessage(MessageType.Regular, 0x35, true, "Don't forget to set your feats using ]Feats", m.NetState);

            }
            else
            {

                m.SendGump(new ClassGump(m));
            }
        }

        public class LocationGump : Gump
        {
            public LocationGump(Mobile from)
                : base(150, 30)
            {
                Closable = false;
                Disposable = false;
                Resizable = false;

                TeiravonMobile m = (TeiravonMobile)from;
                string open = "<basefont size=\"+8\" color=\"#E0E0E0\">";
                string close = "</basefont>";

                m.CloseGump(typeof(ClassGump));

                AddPage(1);
                AddBackground(0, 52, 500, 400, 2600);

                AddImage(201, 14, 1417);
                AddImage(124, 30, 1419);
                AddImage(211, 22, 5505);

                AddLabel(140, 95, 150, "Starting Location Selection");
            }
        }

        public static void MageConfig(TeiravonMobile m)
        {
            m.Magicable = true;

            m.SetSpell(Teiravon.Spells.ClumsySpell, true);
            m.SetSpell(Teiravon.Spells.FeeblemindSpell, true);
            m.SetSpell(Teiravon.Spells.MagicArrowSpell, true);
            m.SetSpell(Teiravon.Spells.NightSightSpell, true);
            m.SetSpell(Teiravon.Spells.ReactiveArmorSpell, true);
            m.SetSpell(Teiravon.Spells.WeakenSpell, true);
            m.SetSpell(Teiravon.Spells.AgilitySpell, true);
            m.SetSpell(Teiravon.Spells.CunningSpell, true);
            m.SetSpell(Teiravon.Spells.HarmSpell, true);
            m.SetSpell(Teiravon.Spells.MagicTrapSpell, true);
            m.SetSpell(Teiravon.Spells.RemoveTrapSpell, true);
            m.SetSpell(Teiravon.Spells.ProtectionSpell, true);
            m.SetSpell(Teiravon.Spells.StrengthSpell, true);
            m.SetSpell(Teiravon.Spells.FireballSpell, true);
            //m.SetSpell( Teiravon.Spells.MagicLockSpell, true );
            m.SetSpell(Teiravon.Spells.PoisonSpell, true);
            m.SetSpell(Teiravon.Spells.TelekinesisSpell, true);
            m.SetSpell(Teiravon.Spells.TeleportSpell, true);
            //m.SetSpell( Teiravon.Spells.UnlockSpell, true );
            m.SetSpell(Teiravon.Spells.WallOfStoneSpell, true);
            m.SetSpell(Teiravon.Spells.ArchProtectionSpell, true);
            m.SetSpell(Teiravon.Spells.FireFieldSpell, m.PlayerRace == TeiravonMobile.Race.Frostling ? false : true);
            m.SetSpell(Teiravon.Spells.LightningSpell, true);
            m.SetSpell(Teiravon.Spells.ManaDrainSpell, true);
            m.SetSpell(Teiravon.Spells.DispelFieldSpell, true);
            m.SetSpell(Teiravon.Spells.IncognitoSpell, true);
            m.SetSpell(Teiravon.Spells.MagicReflectSpell, true);
            m.SetSpell(Teiravon.Spells.MindBlastSpell, true);
            m.SetSpell(Teiravon.Spells.PoisonFieldSpell, true);
            m.SetSpell(Teiravon.Spells.SummonCreatureSpell, true);
            m.SetSpell(Teiravon.Spells.DispelSpell, true);
            m.SetSpell(Teiravon.Spells.EnergyBoltSpell, true);
            m.SetSpell(Teiravon.Spells.ExplosionSpell, true);
            m.SetSpell(Teiravon.Spells.InvisibilitySpell, true);
            m.SetSpell(Teiravon.Spells.RevealSpell, true);
            m.SetSpell(Teiravon.Spells.ChainLightningSpell, true);
            m.SetSpell(Teiravon.Spells.EnergyFieldSpell, true);
            m.SetSpell(Teiravon.Spells.FlamestrikeSpell, true);
            m.SetSpell(Teiravon.Spells.ManaVampireSpell, true);
            m.SetSpell(Teiravon.Spells.MassDispelSpell, true);
            m.SetSpell(Teiravon.Spells.MeteorSwarmSpell, true);
            m.SetSpell(Teiravon.Spells.PolymorphSpell, true);
            m.SetSpell(Teiravon.Spells.EarthquakeSpell, true);
            m.SetSpell(Teiravon.Spells.GateTravelSpell, true);
            if (m.IsAeromancer())
                m.SetSpell(Teiravon.Spells.AirElementalSpell, true);
            if (m.IsGeomancer())
                m.SetSpell(Teiravon.Spells.EarthElementalSpell, true);
            if (m.IsPyromancer())
                m.SetSpell(Teiravon.Spells.FireElementalSpell, true);
            if (m.IsAquamancer())
                m.SetSpell(Teiravon.Spells.WaterElementalSpell, true);

            m.Skills.Swords.Base = 0.0;
            m.Skills.Swords.Cap = 20.0;

            m.Skills.Macing.Base = 10.0;
            m.Skills.Macing.Cap = 60.0;

            m.Skills.Fencing.Base = 10.0;
            m.Skills.Fencing.Cap = 60.0;

            m.Skills.Wrestling.Base = 0.0;
            m.Skills.Wrestling.Cap = 20.0;

            m.Skills.Tactics.Base = 10.0;
            m.Skills.Tactics.Cap = 60.0;

            m.Skills.Alchemy.Base = 20.0;
            m.Skills.Alchemy.Cap = 100.0;

            m.Skills.Anatomy.Base = 30.0;
            m.Skills.Anatomy.Cap = 100.0;

            m.Skills.Camping.Base = 30.0;
            m.Skills.Camping.Cap = 100.0;

            m.Skills.Cartography.Base = 30.0;
            m.Skills.Cartography.Cap = 100.0;

            m.Skills.Cooking.Base = 10.0;
            m.Skills.Cooking.Cap = 60.0;

            m.Skills.EvalInt.Base = 30.0;
            m.Skills.EvalInt.Cap = 100.0;

            m.Skills.Fishing.Base = 30.0;
            m.Skills.Fishing.Cap = 100.0;

            m.Skills.Healing.Base = 10.0;
            m.Skills.Healing.Cap = 60.0;

            m.Skills.Inscribe.Base = 30.0;
            m.Skills.Inscribe.Cap = 100.0;

            m.Skills.ItemID.Base = 30.0;
            m.Skills.ItemID.Cap = 100.0;

            m.Skills.Magery.Base = 30.0;
            m.Skills.Magery.Cap = 100.0;

            m.Skills.MagicResist.Base = 30.0;
            m.Skills.MagicResist.Cap = 100.0;

            m.Skills.TasteID.Base = 20.0;
            m.Skills.TasteID.Cap = 80.0;

            if (m.IsDwarf())
                m.Skills.Mining.Cap = 90.0;

            if (m.IsNecromancer())
                m.Skills.Poisoning.Cap = 100.0;
        }
    }
}
