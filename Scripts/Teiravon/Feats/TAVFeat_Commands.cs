using System;
using System.Collections;
using System.IO;
using System.Xml;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Gumps;
using Server.Teiravon;
using Server.Spells;
using Server.Network;
using Server.Engines.XmlSpawner2;
using Server.Prompts;
using Server.Spells.Seventh;

using TAVCoolDown = Server.Teiravon.AbilityCoolDown;

namespace Server.Scripts.Commands
{
    public class TeiravonCommands
    {

        public static Hashtable dashing = new Hashtable();

        #region Command Initializers
        public static void Initialize()
        {
            // Player
            Server.Commands.Register("Feats", AccessLevel.Player, new CommandEventHandler(Feats_OnCommand));
            Server.Commands.Register("CloakOfDarkness", AccessLevel.Player, new CommandEventHandler(CloakOfDarkness_OnCommand));
            Server.Commands.Register("GlobeOfDarkness", AccessLevel.Player, new CommandEventHandler(GlobeOfDarkness_OnCommand));
            Server.Commands.Register("Cripple", AccessLevel.Player, new CommandEventHandler(Cripple_OnCommand));
            Server.Commands.Register("Bluudlust", AccessLevel.Player, new CommandEventHandler(Bluudlust_OnCommand));
            Server.Commands.Register("KaiShot", AccessLevel.Player, new CommandEventHandler(KaiShot_OnCommand));
            Server.Commands.Register("CriticalStrike", AccessLevel.Player, new CommandEventHandler(CriticalStrike_OnCommand));
            //			Server.Commands.Register( "Stealth", AccessLevel.Player, new CommandEventHandler( Stealth_OnCommand ) );
            Server.Commands.Register("Bite", AccessLevel.Player, new CommandEventHandler(Bite_OnCommand));
            Server.Commands.Register("ArcaneTransfer", AccessLevel.Player, new CommandEventHandler(ArcaneTransfer_OnCommand));
            Server.Commands.Register("DetectEvil", AccessLevel.Player, new CommandEventHandler(DetectEvil_OnCommand));
            Server.Commands.Register("LayOnHands", AccessLevel.Player, new CommandEventHandler(LayOnHands_OnCommand));
            Server.Commands.Register("Forage", AccessLevel.Player, new CommandEventHandler(Forage_OnCommand));
            //			Server.Commands.Register( "Berserk", AccessLevel.Player, new CommandEventHandler( Berserk_OnCommand ) );
            Server.Commands.Register("Disarm", AccessLevel.Player, new CommandEventHandler(Disarm_OnCommand));
            Server.Commands.Register("Telepathy", AccessLevel.Player, new CommandEventHandler(Telepathy_OnCommand));
            Server.Commands.Register("Backstab", AccessLevel.Player, new CommandEventHandler(Backstab_OnCommand));
            Server.Commands.Register("CalledShot", AccessLevel.Player, new CommandEventHandler(CalledShot_OnCommand));
            Server.Commands.Register("ChargedMissile", AccessLevel.Player, new CommandEventHandler(ChargedMissile_OnCommand));
            Server.Commands.Register("DragonRoar", AccessLevel.Player, new CommandEventHandler(DragonRoar_OnCommand));
            //			Server.Commands.Register( "Instinct", AccessLevel.Player, new CommandEventHandler( BarbarianInstinct_OnCommand ) );
            Server.Commands.Register("HolyAura", AccessLevel.Player, new CommandEventHandler(HolyAura_OnCommand));
            Server.Commands.Register("DivineAura", AccessLevel.Player, new CommandEventHandler(DivineAura_OnCommand));
            Server.Commands.Register("DarkAura", AccessLevel.Player, new CommandEventHandler(DarkAura_OnCommand));
            Server.Commands.Register("Companion", AccessLevel.Player, new CommandEventHandler(Companion_OnCommand));
            Server.Commands.Register("CallMount", AccessLevel.Player, new CommandEventHandler(CallMount_OnCommand));
            Server.Commands.Register("NecroSpeak", AccessLevel.Player, new CommandEventHandler(NecroSpeak_OnCommand));
            Server.Commands.Register("DrainUndead", AccessLevel.Player, new CommandEventHandler(DrainUndead_OnCommand));
            Server.Commands.Register("Lunge", AccessLevel.Player, new CommandEventHandler(PowerLunge_OnCommand));
            Server.Commands.Register("SE", AccessLevel.Player, new CommandEventHandler(SilentEmote_OnCommand));
            Server.Commands.Register("ThrowAxe", AccessLevel.Player, new CommandEventHandler(ThrowAxe_OnCommand));
            Server.Commands.Register("SummonFamiliar", AccessLevel.Player, new CommandEventHandler(SummonFamiliar_OnCommand));
            //Server.Commands.Register("Rage", AccessLevel.Player, new CommandEventHandler(Rage_OnCommand));
            //Server.Commands.Register("Calm", AccessLevel.Player, new CommandEventHandler(Calm_OnCommand));
            Server.Commands.Register("Flurry", AccessLevel.Player, new CommandEventHandler(Flurry_OnCommand));
            Server.Commands.Register("Leap", AccessLevel.Player, new CommandEventHandler(Leap_OnCommand));
            Server.Commands.Register("KiStrike", AccessLevel.Player, new CommandEventHandler(KiStrike_OnCommand));
            Server.Commands.Register("Melody", AccessLevel.Player, new CommandEventHandler(EnchantingMelody_OnCommand));
            Server.Commands.Register("Stance", AccessLevel.Player, new CommandEventHandler(Stance_OnCommand));
            Server.Commands.Register("Pounce", AccessLevel.Player, new CommandEventHandler(Pounce_OnCommand));
            Server.Commands.Register("Quiver", AccessLevel.Player, new CommandEventHandler(Quiver_OnCommand));
            Server.Commands.Register("Cipher", AccessLevel.Player, new CommandEventHandler(Cipher_OnCommand));
            Server.Commands.Register("Eavesdrop", AccessLevel.Player, new CommandEventHandler(EavesDrop_OnCommand));
            Server.Commands.Register("Challenge", AccessLevel.Player, new CommandEventHandler(Challenge_OnCommand));
            Server.Commands.Register("DarkRebirth", AccessLevel.Player, new CommandEventHandler(DarkRebirth_OnCommand));
            Server.Commands.Register("SinisterForm", AccessLevel.Player, new CommandEventHandler(SinisterForm_OnCommand));
            Server.Commands.Register("Feast", AccessLevel.Player, new CommandEventHandler(Feast_OnCommand));
            Server.Commands.Register("Dash", AccessLevel.Player, new CommandEventHandler(Dash_OnCommand));
            Server.Commands.Register("Sense", AccessLevel.Player, new CommandEventHandler(Sense_OnCommand));
            Server.Commands.Register("Mindblast", AccessLevel.Player, new CommandEventHandler(MindBlast_OnCommand));
            Server.Commands.Register("Banish", AccessLevel.Player, new CommandEventHandler(Banish_OnCommand));
            Server.Commands.Register("Disrupt", AccessLevel.Player, new CommandEventHandler(Disrupt_OnCommand));
            Server.Commands.Register("Hibernate", AccessLevel.Player, new CommandEventHandler(Hibernate_OnCommand));
            Server.Commands.Register("Defend", AccessLevel.Player, new CommandEventHandler(Defend_OnCommand));
            Server.Commands.Register("Assault", AccessLevel.Player, new CommandEventHandler(Assault_OnCommand));
            Server.Commands.Register("Feint", AccessLevel.Player, new CommandEventHandler(Feint_OnCommand));
            Server.Commands.Register("Roll", AccessLevel.Player, new CommandEventHandler(Roll_OnCommand));
            Server.Commands.Register("Flourish", AccessLevel.Player, new CommandEventHandler(Flourish_OnCommand));
            Server.Commands.Register("Tricks", AccessLevel.Player, new CommandEventHandler(Tricks_OnCommand));


            // Disabled
            //Server.Commands.Register( "RegionCoords", AccessLevel.Administrator, new CommandEventHandler( RegionCoords_OnCommand ) );
        }
        #endregion


        [Usage("Tricks")]
        [Description("Accesses the scoundrel's Dirty Tricks")]
        private static void Tricks_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            if (!m_Player.HasFeat(TeiravonMobile.Feats.DirtyTricks)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility); return; }
            int trick = 0;
            if (e.Length >= 1)
            {
                if (e.Arguments[0] == "1" || e.Arguments[0] == "2" || e.Arguments[0] == "3" || e.Arguments[0] == "4")
                    trick = Int32.Parse(e.Arguments[0]);
            }
            m_Player.CloseGump(typeof(TricksGump));
            if (trick == 0)
                m_Player.SendGump(new TricksGump(m_Player));
            else
                TricksGump.DoTrick(m_Player, trick);
        }
        
        class TrickTarget : Target
        {
            public TrickTarget(TeiravonMobile from, int trick )
                : base(8, false, TargetFlags.Harmful)
            {

            }
        }

        class MolotovTarget : Target
        {
            private TeiravonMobile m_Player;
            public MolotovTarget(TeiravonMobile from)
                : base(8, true, TargetFlags.Harmful)
            {
                m_Player = from;
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if ( targ is IPoint3D )
                {
							IPoint3D p = targ as IPoint3D;
							Point3D loc = new Point3D( p.X, p.Y, p.Z );
							bool blessed = false;

                            IPooledEnumerable eable = from.Map.GetItemsInRange(loc, 5);

							foreach ( Item item in eable )
							{
								if ( item is MolotovResidue )
								{
									blessed = true;
									break;
								}
							}

							eable.Free();

							if ( !blessed )
							{
								MolotovResidue m_Vial = new MolotovResidue( ((TeiravonMobile)from).PlayerLevel / 5, from );
								m_Vial.MoveToWorld( loc, from.Map );
								m_Player.SendMessage( "You lob a firebomb!" );
							}
							else
								m_Player.SendMessage( "You add more fuel to the fire" );
						

						m_Player.Emote( "*lobs a firebomb*" );

                        // Reuse timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.DirtyTricks;
                        m_TimerHelper.Duration = TimeSpan.FromSeconds(30.0);
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.DirtyTricks, true);
                }
            }
        }
        
        class SmokeTarget : Target
        {
            private TeiravonMobile m_Player;
            public SmokeTarget(TeiravonMobile from)
                : base(4, true, TargetFlags.None)
            {
                m_Player = from;
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if (targ is IPoint3D)
                {
                    IPoint3D p = targ as IPoint3D;
                    Point3D loc = new Point3D(p.X, p.Y, p.Z);

                    m_Player.Emote("*Tosses down a smokebomb*");
                    Map CastMap = m_Player.Map;
                    int radius = 3;
                    Regions.FogRegion fog = new Regions.FogRegion(m_Player, radius);
                    Rectangle2D rect = new Rectangle2D(loc.X - radius, loc.Y - radius, radius * 2, radius * 2);

                    // Hopefully this line fixes it... if not, it'll get caught in Try/Catch.
                    if (fog.Coords == null)
                        fog.Coords = new ArrayList();
                    fog.Priority = 200;
                    fog.Coords.Add(rect);
                    Region.AddRegion(fog);
                    AbilityCollection.AreaEffect(TimeSpan.Zero, TimeSpan.FromSeconds(.15), .15, CastMap, loc, 0x372A, 15, 2859, 1, radius, 5, true, false, false);
                    Timer.DelayCall(TimeSpan.FromSeconds(15), new TimerStateCallback(Fog.RemoveZone), new object[] { fog });
                    Timer.DelayCall(TimeSpan.FromSeconds(0.15), TimeSpan.FromSeconds(2), 7, new TimerStateCallback(Fog.FillFog), new object[] { m_Player, loc, radius, CastMap });

                    // Reuse timer
                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.DirtyTricks;
                    m_TimerHelper.Duration = TimeSpan.FromSeconds(30.0);
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.DirtyTricks, true);
                }
            }
        }

        class SilenceTarget : Target
        {
            public SilenceTarget()
                : base(1, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m_Target = (Mobile)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Player == m_Target)
                    {
                        m_Player.SendMessage("You refuse to blind yourself.");
                        return;
                    }

                    if (m_Target == null)
                        return;

                    if (!m_Target.Alive || m_Target.AccessLevel > from.AccessLevel || !from.CanBeHarmful(m_Target))
                        return;

                    from.DoHarmful(m_Target);
                    XmlData silence = new XmlData("silence","silence",5.0);
                    XmlAttach.AttachTo(m_Target, silence);
                    string name = m_Target.ToString();
                    from.Emote("*Chops the throat of {0}*", name.Remove(0, 10));
                    // Reuse timer
                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.DirtyTricks;
                    m_TimerHelper.Duration = TimeSpan.FromSeconds(30.0);
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.DirtyTricks, true);


                }
                else
                    from.SendMessage(Teiravon.Messages.NoTarget);
            }
        }

        class BlindTarget : Target
        {
            public BlindTarget()
                : base(1, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m_Target = (Mobile)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Player == m_Target)
                    {
                        m_Player.SendMessage("You refuse to blind yourself.");
                        return;
                    }

                    if (m_Target == null)
                        return;

                    if (!m_Target.Alive || m_Target.AccessLevel > from.AccessLevel  || !from.CanBeHarmful(m_Target) )
                        return;

                    from.DoHarmful(m_Target);
                    TavBlind blind = new TavBlind("blind",5.0);
                    XmlAttach.AttachTo(m_Target, blind);
                    string name = m_Target.ToString();
                    from.Emote("*Gouges the eyes of {0}*",name.Remove(0,10));
                        // Reuse timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.DirtyTricks;
                        m_TimerHelper.Duration = TimeSpan.FromSeconds(30.0);
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.DirtyTricks, true);
                    

                }
                else
                    from.SendMessage(Teiravon.Messages.NoTarget);
            }
        }

        class TricksGump : Gump
        {
            public static void DoTrick(TeiravonMobile from, int trick)
            {
                if (from.GetActiveFeats(TeiravonMobile.Feats.DirtyTricks)) { from.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon); return; }
                else if (!from.Alive) { from.SendMessage("You cannot do this while dead."); return; }
                switch (trick)
                {
                    case 1:
                        from.SendMessage("Who will you blind?");
                        from.Target = new BlindTarget();
                        break;
                    case 2:
                        from.SendMessage("Who will you silence?");
                        from.Target = new SilenceTarget();
                        break;
                    case 3:
                        if (from.Backpack.ConsumeTotal(typeof(BaseBeverage), 1))
                            from.Target = new MolotovTarget(from);
                        else
                            from.SendMessage("You'll need some booze to throw.");
                        break;
                    case 4:
                        from.Target = new SmokeTarget(from);
                        break;
                }
            }
            public TricksGump(TeiravonMobile from)
                : base((Int32)0, (Int32)0)
            {
                int y = 4;
                this.Closable = true;
                this.Disposable = false;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                AddBackground(312, 25, 75, 26 + (y * 56), 9260);
                AddAlphaRegion(325, 35, 50, 10 + (y * 56));
                this.AddImage(305, 15, 30061);
                this.AddImage(305, 36 + (y * 56), 30077);

                int Icon, Hue;
                string text;
                int i = 0;

                Icon = 0x8C9;
                Hue = 32;
                text = "Eye Gouge";
                this.AddButton(328, 40 + i * 56, Icon, Icon, i + 1, GumpButtonType.Reply, 0);
                this.AddLabel(320, 78 + i * 56, Hue, text);
                i++;

                Icon = 0x8E5;
                Hue = 443;
                text = "Throat Chop";
                this.AddButton(328, 40 + i * 56, Icon, Icon, i + 1, GumpButtonType.Reply, 0);
                this.AddLabel(320, 78 + i * 56, Hue, text);
                i++;

                Icon = 0x8DB;
                Hue = 343;
                text = "Explosive Flask";
                this.AddButton(328, 40 + i * 56, Icon, Icon, i + 1, GumpButtonType.Reply, 0);
                this.AddLabel(320, 78 + i * 56, Hue, text);
                i++;

                Icon = 0x8FB;
                Hue = 402;
                text = "Smoke Bomb";
                this.AddButton(328, 40 + i * 56, Icon, Icon, i + 1, GumpButtonType.Reply, 0);
                this.AddLabel(320, 78 + i * 56, Hue, text);
                i++;

            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID > 0)
                    DoTrick((TeiravonMobile)sender.Mobile, info.ButtonID);
                sender.Mobile.SendGump(new TricksGump((TeiravonMobile)sender.Mobile));
                base.OnResponse(sender, info);
            }
        }

        [Usage("Flourish")]
        [Description("Store a weapon attack attack to use with any future weapon")]
        private static void Flourish_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.CunningFlourish)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility); return; }
            if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else
            {

                TAVFlourish flourish = (TAVFlourish)XmlAttach.FindAttachmentOnMobile(m_Player, typeof(TAVFlourish), "flourish");
            
                bool f = (flourish != null);

                WeaponAbility ability = WeaponAbility.GetCurrentAbility(m_Player);
                if (ability != null)
                {
                    int a = 0;
                    for (int i = 0; i < WeaponAbility.Abilities.Length; i++)
                    {
                        if (WeaponAbility.Abilities[i] == ability)
                            a = i;
                    }

                    XmlAttach.AttachTo(m_Player, new TAVFlourish("flourish",a));
                    m_Player.SendMessage("You store the ability to be flourished later. {0}", ability.ToString());
                }

                else if (ability == null && f)
                {
                    WeaponAbility.SetCurrentAbility(m_Player, flourish.WeaponAbility);

                    m_Player.ChargedMissileReady = true;

                    m_Player.SendMessage("You ready the flourished ability. {0}", flourish.WeaponAbility);
                }
                else
                    m_Player.SendMessage("You must ready an attack to store it with cunning flourish.");
                
            }
        }

        
        [Usage("Roll")]
        [Description("An acrobatic roll forward")]
        private static void Roll_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.AcrobaticCombat)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility); }
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.Paralyzed)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You cannot move.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Dodge)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon); }
            else
            {
                m_Player.SetActiveFeats(TeiravonMobile.Feats.Dodge, true);

                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Duration = TimeSpan.FromSeconds(10);
                m_TimerHelper.Feat = TeiravonMobile.Feats.Dodge;
                m_TimerHelper.Start();

                int x = m_Player.X, y = m_Player.Y;

                Movement.Movement.Offset(m_Player.Direction, ref x, ref y);
                Movement.Movement.Offset(m_Player.Direction, ref x, ref y);
                Point2D destination = new Point2D(x, y);
                Roller dummy = new Roller(destination, m_Player);
                dummy.MoveToWorld(m_Player.Location, m_Player.Map);
                m_Player.Animate(32, 3, 1, true, false, 0);
            }
        }

        [Usage("Feint")]
        [Description("Lowers the enemy's defences")]
        private static void Feint_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.AcrobaticCombat)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility); }
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.AcrobaticCombat)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon); }
            else
            {
                m_Player.SetActiveFeats(TeiravonMobile.Feats.AcrobaticCombat, true);

                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Duration = TimeSpan.FromSeconds(15);
                m_TimerHelper.Feat = TeiravonMobile.Feats.AcrobaticCombat;
                m_TimerHelper.Start();

                m_Player.CripplingShotReady = true;
                m_Player.SendMessage("You prepare a cunning feint");
            }
        }

        [Usage("Assault")]
        [Description("Performa an immediate attack at the cost of stamina")]
        private static void Assault_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.FuriousAssault)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility); return; }
            if (m_Player.Combatant == null) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoTarget); return; }


            IWeapon weapon = m_Player.Weapon;

            if (!m_Player.InRange(m_Player.Combatant, weapon.MaxRange))
                return;

            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.FuriousAssault))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon); return;
            }
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else
            {
                double stamratio = 2;
                if (m_Player.HasFeat(TeiravonMobile.Feats.AxeFighter) && m_Player.Weapon is BaseAxe)
                    stamratio = 1.25;

                int stam = 2;
                XmlValue counter = (XmlValue)XmlAttach.FindAttachmentOnMobile(m_Player, typeof(XmlValue), "assault");
                if (counter != null)
                {
                    stam = 2 + (int)(stamratio * counter.Value);
                    counter.Value += 1;
                    counter.Expiration = TimeSpan.FromSeconds(15);
                }
                else
                    XmlAttach.AttachTo(m_Player, new XmlValue("assault", 1, .25));
                if (m_Player.HasFeat(TeiravonMobile.Feats.BerserkerRage))
                {
                    if (m_Player.Hits < stam) { m_Player.SendMessage("You cannot muster the energy."); return; }
                    m_Player.Damage(stam);
                    new Blood().MoveToWorld(m_Player.Location, m_Player.Map);
                }
                else
                {
                    if (m_Player.Stam < stam) { m_Player.SendMessage("You are simply too tired."); return; }
                    m_Player.Stam -= stam;
                }
                m_Player.SendMessage("You strike with furious anger.");
                DateTime combat = m_Player.NextCombatTime;
                m_Player.Weapon.OnSwing(m_Player, m_Player.Combatant);
                m_Player.NextCombatTime = combat;

                m_Player.SetActiveFeats(TeiravonMobile.Feats.FuriousAssault, true);

                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Duration = TAVCoolDown.FuriousAssault;
                m_TimerHelper.Feat = TeiravonMobile.Feats.FuriousAssault;
                m_TimerHelper.Start();

            }
        }

        [Usage("Defend")]
        [Description("Enters a Defensive Stance.")]
        private static void Defend_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.DefensiveStance)) { m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility); }
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.DefensiveStance))
            {
                m_Player.SendMessage("You break your defensive stance.");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.DefensiveStance, false);
            }
            else if (m_Player.Stam > 5)
            {
                m_Player.Stam -= 5;
                m_Player.SendMessage("You root yourself firmly, unmoving and unbreakable.");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.DefensiveStance, true);
            }
        }

        [Usage("Hibernate")]
        [Description("Encase yourself in solid ice for a short time.")]
        private static void Hibernate_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.FrostlingHibernation))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.FrostlingHibernation))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            }
            else
            {
                HibernationTimer timer = new HibernationTimer(m_Player);
                AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.2), 0.05, m_Player.Map, m_Player.Location, 0x1145, 12, 2290, 0, 2, 5, false, true, false);
                Effects.PlaySound(m_Player.Location, m_Player.Map, 0x5C8);
                Effects.SendLocationEffect(m_Player.Location, m_Player.Map, 0x2FDC, (60 * 3), 2151, 5);
                m_Player.SendMessage("The frosts gather near as you're encased in ice.");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.FrostlingHibernation, true);
                timer.Start();

                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Duration = TAVCoolDown.Daily;
                m_TimerHelper.Feat = TeiravonMobile.Feats.FrostlingHibernation;
                m_TimerHelper.Start();

                m_Player.Blessed = true;
                m_Player.CantWalk = true;
                m_Player.Paralyzed = true;


                m_Player.SetActiveFeats(TeiravonMobile.Feats.FrostlingHibernation, true);
            }
        }

        public class HibernationTimer : Timer
        {
            TeiravonMobile m_Player;

            public HibernationTimer(TeiravonMobile from)
                : base(TimeSpan.FromSeconds(10 - (from.PlayerLevel / 4)))
            {
                m_Player = from;
            }

            protected override void OnTick()
            {
                m_Player.Hits = m_Player.HitsMax;
                m_Player.Stam = m_Player.StamMax;
                m_Player.Mana = m_Player.ManaMax;

                m_Player.CantWalk = false;
                m_Player.Blessed = false;
                m_Player.Paralyzed = false;
            }
        }

        [Usage("Disrupt")]
        [Description("Create an area of volitility around you making casting difficult.")]
        private static void Disrupt_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.DisruptingPresence))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.DisruptingPresence))
            {
                m_Player.SendMessage("You release your focus upon the ether around you.");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.DisruptingPresence, false);
            }
            else if (m_Player.Mana < 15)
                m_Player.SendMessage("You do not have enough mana to perform this ability.");
            else
            {
                m_Player.Mana -= 15;
                DisruptionTimer timer = new DisruptionTimer(m_Player);
                AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.2), 0.05, m_Player.Map, m_Player.Location, 0x1158, 12, 2295, 0, 5, 1, false, true, false);
                Effects.PlaySound(m_Player.Location, m_Player.Map, 0x37B);
                m_Player.SendMessage("You gather your focus upon the ether and concentrate a disrupting presense around yourself.");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.DisruptingPresence, true);
                timer.Start();
            }
        }

        public class DisruptionTimer : Timer
        {
            TeiravonMobile m_Player;

            public DisruptionTimer(TeiravonMobile from)
                : base(TimeSpan.FromSeconds(10))
            {
                m_Player = from;
            }

            protected override void OnTick()
            {
                if (!m_Player.GetActiveFeats(TeiravonMobile.Feats.DisruptingPresence))
                    return;

                if (m_Player.Mana > 5)
                {
                    m_Player.Mana -= 5;
                    DisruptionTimer timer = new DisruptionTimer(m_Player);
                    AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.2), 0.05, m_Player.Map, m_Player.Location, 0x1158, 12, 2295, 0, 5, 1, false, true, false);
                    Effects.PlaySound(m_Player.Location, m_Player.Map, 0x37B);
                    timer.Start();
                }
                else
                {
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.DisruptingPresence, false);
                    m_Player.SendMessage("You can no longer maintain your focus.");
                }
            }
        }

        [Usage("Banish")]
        [Description("Destroy all nearby magical creatures.")]
        private static void Banish_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            ArrayList toBanish = new ArrayList();
            if (!m_Player.HasFeat(TeiravonMobile.Feats.Banish))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Banish))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Mana < 30)
                m_Player.SendMessage("You do not have enough mana to perform this ability.");
            else
            {
                int radius = (int)(m_Player.Mana / 30);

                IPooledEnumerable eable = m_Player.GetMobilesInRange(radius);

                foreach (Mobile m in eable)
                {
                    if (m is BaseCreature)
                    {
                        BaseCreature c = m as BaseCreature;
                        if (c.Summoned)
                            toBanish.Add(c);
                    }
                }

                eable.Free();

                foreach (BaseCreature f in toBanish)
                {
                    if (f != null)
                        f.Dispel(f);
                }

                AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(.2), 0.1, m_Player.Map, m_Player.Location, 0x37C4, 5, 0, 0, radius, 2, true, false, false);
                Effects.PlaySound(m_Player.Location, m_Player.Map, 0x0FA);
                m_Player.Mana = 0;
            }
        }

        [Usage("Mindblast")]
        [Description("Cause a violent vortex within the target's lack of mana.")]
        private static void MindBlast_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.MindBlast))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.MindBlast))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Mana < 30)
                m_Player.SendMessage("You do not have enough mana to perform this ability.");
            else
            {
                m_Player.SendMessage("Who will you target?");
                m_Player.Target = new MindBlastTarget();

            }
        }

        private class MindBlastTarget : Target
        {
            public MindBlastTarget()
                : base(8, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                TeiravonMobile player = from as TeiravonMobile;

                if (targeted is Mobile && targeted != from)
                {
                    Mobile m = targeted as Mobile;

                    from.DoHarmful(m);
                    m.VirtualArmorMod = 0;
                    int damage = 5 + player.PlayerLevel + (int)((.05 * player.PlayerLevel) * (m.ManaMax - m.Mana));
                    m.Damage(damage, from);

                    Effects.PlaySound(from.Location, from.Map, 0x213);
                    Effects.SendTargetEffect(m, 0x374B, 16);

                    TimerHelper m_TimerHelper = new TimerHelper((int)from.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Duration = TimeSpan.FromMinutes(2);
                    m_TimerHelper.Feat = TeiravonMobile.Feats.MindBlast;
                    m_TimerHelper.Start();

                    player.SetActiveFeats(TeiravonMobile.Feats.MindBlast, true);
                }
                else
                {
                    from.SendMessage("You can't target that!");
                }
            }
        }

        [Usage("Sense")]
        [Description("Detect the flow of the ether around you.")]
        private static void Sense_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.WyrdSense))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.WyrdSense))
            {
                m_Player.SendMessage("You begin to cloud your wyrd sense...");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.WyrdSense, false);
                XmlData a = (XmlData)XmlAttach.FindAttachment(m_Player, typeof(XmlData), "wyrd");
                if (a != null)
                    a.Delete();
            }
            else
            {
                m_Player.SendMessage("You open your mind to the flows of ether...");
                XmlAttach.AttachTo(m_Player, new XmlData("wyrd", ""));
                m_Player.SetActiveFeats(TeiravonMobile.Feats.WyrdSense, true);
            }
        }


        [Usage("Dash")]
        [Description("A quick burst of speed at the cost of stamina.")]
        private static void Dash_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.LegIt))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.LegIt))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
            {
                m_Player.Send(SpeedBoost.Enabled);
                if (!dashing.Contains(m_Player))
                    dashing.Add(m_Player, 1);
                DashTimer timer = new DashTimer(m_Player);
                timer.Start();
            }

        }

        public class DashTimer : Timer
        {
            TeiravonMobile m_Player;

            public DashTimer(TeiravonMobile from)
                : base(TimeSpan.FromSeconds(3))
            {
                m_Player = from;
            }

            protected override void OnTick()
            {
                m_Player.Send(SpeedBoost.Disabled);
                if (dashing.Contains(m_Player))
                    dashing.Remove(m_Player);
            }
        }

        [Usage("Sinister Form")]
        [Description("Let loose your inner demons.")]
        private static void SinisterForm_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            Spells.Necromancy.TransformationSpell.UnderTransformation(m_Player);

            if (!m_Player.HasFeat(TeiravonMobile.Feats.SinisterForm))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.SinisterForm))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (Spells.Necromancy.TransformationSpell.UnderTransformation(m_Player) || !m_Player.CanBeginAction(typeof(PolymorphSpell)))
                m_Player.SendMessage("You cannot transform the transformed.");
            else if (m_Player.Shapeshifted)
            {
                m_Player.Shapeshifted = false;
                m_Player.OBody = 0;
                m_Player.PlaySound(0xFA);
                m_Player.FixedParticles(0x3728, 1, 13, 5042, EffectLayer.Waist);

            }
            else
            {
                switch (m_Player.PlayerClass)
                {
                    case TeiravonMobile.Class.Alchemist:
                    case TeiravonMobile.Class.Blacksmith:
                    case TeiravonMobile.Class.Bowyer:
                    case TeiravonMobile.Class.Cook:
                    case TeiravonMobile.Class.Tailor:
                    case TeiravonMobile.Class.Woodworker:
                    case TeiravonMobile.Class.Tinker:
                        m_Player.OBody = 267; break;
                    case TeiravonMobile.Class.DarkCleric:
                        m_Player.OBody = 148; break;
                    case TeiravonMobile.Class.Necromancer:
                        m_Player.OBody = 24; break;
                    case TeiravonMobile.Class.Fighter:
                        m_Player.OBody = 147; break;
                    case TeiravonMobile.Class.Cavalier:
                        m_Player.OBody = 309; break;
                    case TeiravonMobile.Class.Archer:
                        m_Player.OBody = 50; break;
                    case TeiravonMobile.Class.Assassin:
                        m_Player.OBody = 146; break;
                    case TeiravonMobile.Class.Thief:
                        m_Player.OBody = 26; break;
                    case TeiravonMobile.Class.Dragoon:
                        m_Player.OBody = 304; break;
                    case TeiravonMobile.Class.Berserker:
                        m_Player.OBody = 307; break;
                    default: m_Player.OBody = 3; break;
                }
                m_Player.Shapeshifted = true;
                m_Player.PlaySound(0xFA);
                m_Player.FixedParticles(0x3728, 1, 13, 5042, EffectLayer.Waist);

                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Duration = TAVCoolDown.SinisterForm;
                m_TimerHelper.Feat = TeiravonMobile.Feats.SinisterForm;
                m_TimerHelper.Start();

                m_Player.SetActiveFeats(TeiravonMobile.Feats.SinisterForm, true);

                if (m_Player.Mounted)
                {
                    m_Player.Mount.Rider = null;
                    m_Player.Dismounted();
                }
            }
        }

        [Usage("Feast")]
        [Description("Consume the soul of the fallen.")]
        private static void Feast_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.FeastoftheDamned))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.FeastoftheDamned))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
                m_Player.Target = new FeastTarget();
        }

        private class FeastTarget : Target
        {
            public FeastTarget()
                : base(2, false, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                TeiravonMobile player = from as TeiravonMobile;

                if (!(targeted is Corpse))
                    from.SendMessage("That is not a corpse.");
                else if (((Corpse)targeted).Channeled || ((Corpse)targeted).Bitten)
                {
                    from.SendMessage("This corpse has already been consumed.");
                }
                else
                {
                    Corpse c = targeted as Corpse;

                    Point3D p = c.GetWorldLocation();
                    Map map = c.Map;

                    player.PlaySound(0x58D);
                    Effects.SendLocationParticles(EffectItem.Create(p, map, EffectItem.DefaultDuration), 0x374B, 5, 20, 0x3F, 3, 9907, 0);

                    new Blood().MoveToWorld(c.Location, c.Map);

                    if (Utility.RandomBool())
                    {
                        new Blood().MoveToWorld(new Point3D(
                            c.X + Utility.RandomMinMax(-1, 1),
                            c.Y + Utility.RandomMinMax(-1, 1),
                            c.Z), c.Map);
                    }

                    c.Bitten = true;
                    c.Channeled = true;

                    c.ProcessDelta();
                    c.SendRemovePacket();
                    c.ItemID = Utility.Random(0xECA, 9); // bone graphic
                    c.Hue = 0;
                    c.ProcessDelta();

                    if (c.Owner != null)
                    {
                        if (c.Owner is TeiravonMobile)
                        {
                            TeiravonMobile tav = c.Owner as TeiravonMobile;
                            player.Feast += tav.PlayerLevel / 2;
                        }
                        if (c.Owner is BaseCreature)
                        {
                            BaseCreature creature = c.Owner as BaseCreature;
                            player.Feast += TAVUtilities.CalculateLevel(creature) / 2;
                        }
                    }

                    player.SendMessage("You consume the traces of the soul still lingering in this corpse");

                    // Name coloring
                    if (player.Feast > 10000)
                        player.NameHue = 1000;
                    if (player.Feast > 13000)
                        player.NameHue = 999;
                    if (player.Feast > 16000)
                        player.NameHue = 998;
                    if (player.Feast > 19000)
                        player.NameHue = 997;
                    if (player.Feast > 22000)
                        player.NameHue = 1999;
                    if (player.Feast > 25000)
                        player.NameHue = 2000;
                    if (player.Feast > 30000)
                        player.NameHue = 1992;
                    if (player.Feast > 35000)
                        player.NameHue = 1980;

                    TimerHelper m_TimerHelper = new TimerHelper((int)from.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Duration = TAVCoolDown.Feast;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.FeastoftheDamned;
                    m_TimerHelper.Start();

                    player.SetActiveFeats(TeiravonMobile.Feats.FeastoftheDamned, true);
                }
            }
        }

        [Usage("DarkRebirth")]
        [Description("You are born again in the flesh of the fallen.")]
        private static void DarkRebirth_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.DarkRebirth))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must not be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.DarkRebirth))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
                m_Player.Target = new RebirthTarget();
        }

        private class RebirthTarget : Target
        {
            public RebirthTarget()
                : base(2, false, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                TeiravonMobile player = from as TeiravonMobile;
                BaseWeapon wpn = (BaseWeapon)player.Weapon;

                if (!(targeted is Corpse))
                    from.SendMessage("That is not a corpse.");
                else if (((Corpse)targeted).Channeled || ((Corpse)targeted).Bitten)
                {
                    from.SendMessage("This vessel has already been ravaged.");
                }
                else
                {
                    Corpse c = targeted as Corpse;
                    if (c.Owner == from)
                    {
                        from.SendMessage("If that corpse were suitable to inhabit you would not have left it.");
                        return;
                    }
                    player.Resurrect();
                    player.Animate(21, 5, 1, false, false, 0);
                    player.Location = c.Location;

                    Point3D p = c.GetWorldLocation();
                    Map map = c.Map;

                    Effects.PlaySound(p, map, 0x1FB);
                    Effects.SendLocationParticles(EffectItem.Create(p, map, EffectItem.DefaultDuration), 0x3789, 1, 40, 0x3F, 3, 9907, 0);

                    new Blood().MoveToWorld(c.Location, c.Map);

                    if (Utility.RandomBool())
                    {
                        new Blood().MoveToWorld(new Point3D(
                            c.X + Utility.RandomMinMax(-1, 1),
                            c.Y + Utility.RandomMinMax(-1, 1),
                            c.Z), c.Map);
                    }

                    c.Bitten = true;
                    c.Channeled = true;

                    c.ProcessDelta();
                    c.SendRemovePacket();
                    c.ItemID = Utility.Random(0xECA, 9); // bone graphic
                    c.Hue = 0;
                    c.ProcessDelta();

                    TimerHelper m_TimerHelper = new TimerHelper((int)from.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Duration = TAVCoolDown.DarkRebirth;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.DarkRebirth;
                    m_TimerHelper.Start();

                    player.SetActiveFeats(TeiravonMobile.Feats.DarkRebirth, true);
                }
            }
        }

        [Usage("Cipher")]
        [Description("Allows a rogue to place and reveal secret messages.")]
        private static void Cipher_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Espionage))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else if (!m_Player.Alive)
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            }
            else
            {
                m_Player.Target = new CipherTarget();
            }
        }
        public static String Ciphered(Item i)
        {

            XmlData a = (XmlData)XmlAttach.FindAttachment(i, typeof(XmlData));

            if (a != null)
            {
                return a.Data;
            }
            else
            {
                return null;
            }
        }

        private class CipherTarget : Target
        {
            public CipherTarget()
                : base(1, false, TargetFlags.None)
            {

            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Item)
                {
                    Item I = targeted as Item;

                    if (I is BaseBook || I is MessageScroll)
                    {
                        if (Ciphered(I) != null)
                        {
                            from.SendMessage("There appears to be a secret message encoded here!");
                            from.PlaySound(from.Body.IsFemale ? 0x30a : 0x41a);
                            from.LocalOverheadMessage(MessageType.Label, Teiravon.Colors.YewColor, false, Ciphered(I));
                        }
                        else
                        {
                            from.SendMessage("What secret message will you leave?");
                            from.Prompt = new CipherPrompt(I);
                        }

                    }
                    else
                        from.SendMessage("You cannot encode a message on that!");
                }
                else
                    from.SendMessage("You cannot encode a message on that!");
            }
        }

        public class CipherPrompt : Prompt
        {
            private Item i_Note;

            public CipherPrompt(Item i)
            {
                i_Note = i;
            }

            public override void OnResponse(Mobile from, string text)
            {
                XmlAttach.AttachTo(i_Note, new XmlData(from.Name, text));
                from.PlaySound(0x249);
                from.SendMessage("You covertly encode the message.");
            }
        }

        [Usage("EavesDrop")]
        [Description("Allows a rogue to listen through doors to hear conversations on the other side.")]
        private static void EavesDrop_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Espionage))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else if (!m_Player.Alive)
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            }
            else
            {
                m_Player.Target = new Eaves_Target();
            }
        }

        private class Eaves_Target : Target
        {
            public Eaves_Target()
                : base(1, false, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseDoor)
                {
                    XmlAttach.AttachTo(targeted, new XmlEavesDrop(from));
                }
                else
                {
                    from.SendMessage("You must target a door to listen to.");
                }
            }
        }

        [Usage("Challenge")]
        [Description("Valliently challenges your target, focusing their attention to you.")]
        private static void Challenge_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Challenge))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (!m_Player.Alive)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be alive to do this.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Challenge))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Mana < 15)
                m_Player.SendMessage("You have insufficient mana for this attack.");
            else
                m_Player.Target = new ChallengeTarget();
        }

        private class ChallengeTarget : Target
        {
            public ChallengeTarget()
                : base(2, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                TeiravonMobile player = from as TeiravonMobile;
                BaseWeapon wpn = (BaseWeapon)player.Weapon;

                if (targeted == from)
                    from.SendMessage("While challenging yourself is an important part of self improvement, please select a different target.");
                else if (targeted is Item)
                {
                    from.SendMessage("You cannot challenge that!");
                }
                else if (targeted is Mobile)
                {
                    player.Mana -= 15;
                    Mobile target = targeted as Mobile;
                    target.Combatant = player;


                    // if (from.GetDistanceToSqrt(target) >= 2)
                    //{
                    XmlAttach.AttachTo(from, new XmlTaunt(target));
                    player.Emote("*Shouts a challenge at {0}", target.Name);
                    player.PlaySound(player.Body.IsFemale ? 0x338 : 0x44A);
                    player.FixedParticles(0x376A, 1, 31, 9961, 1160, 0, EffectLayer.Waist);
                    //}
                    // else
                    // {
                    int min, max;
                    from.Weapon.GetStatusDamage(from, out min, out max);
                    int damage = Utility.RandomMinMax(min, max) * (1 + (player.PlayerLevel / 50));
                    AOS.Damage(target, from, damage, 60, 10, 10, 10, 10);
                    wpn.PlaySwingAnimation(from); ;
                    player.PlaySound(0x520);

                    // }
                    // Reuse timer
                    TimerHelper m_TimerHelper = new TimerHelper((int)from.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Duration = TimeSpan.FromSeconds(45);
                    m_TimerHelper.Feat = TeiravonMobile.Feats.Challenge;
                    m_TimerHelper.Start();

                    player.SetActiveFeats(TeiravonMobile.Feats.Challenge, true);
                }
            }
        }

        [Usage("KiStrike")]
        [Description("Imbues a Monks attacks with force.")]
        private static void KiStrike_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.KiStrike))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.KiStrike))
            {
                m_Player.SendMessage("You allow your ki to flow again.");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.KiStrike, false);
            }
            else
            {
                if (m_Player.Mana < 10)
                    m_Player.SendMessage("You cannot gather sufficient ki.");
                else
                {
                    m_Player.SendMessage("You concentrate your ki into your attacks.");
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.KiStrike, true);
                }
            }
        }

        [Usage("Stance")]
        [Description("Changes the Monk's martial stance")]
        private static void Stance_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            if (!m_Player.IsMonk())
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else
            {
                Item stance = m_Player.FindItemOnLayer(Layer.Unused_x9);
                /*
                if (stance is BaseArmor)
                {
                    m_Player.Emote("*Opens their fists with arms closed in tight*");
                    if (stance != null)
                    {
                        m_Player.RemoveItem(stance);
                        stance.Delete();
                    }
                    m_Player.EquipItem(new MonkBlock());
                }
                else
                {*/
                //m_Player.Emote("*Closes their fists with arms raised up*");
                if (stance != null)
                {
                    m_Player.RemoveItem(stance);
                    stance.Delete();
                }
                m_Player.EquipItem(new MonkFists());
                //}
            }

        }

        [Usage("Quiver")]
        [Description("Draws an arrow from an enchanted quiver")]
        private static void Quiver_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            if (!m_Player.HasFeat(TeiravonMobile.Feats.EnchantedQuiver))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else
            {
                Item quiver = m_Player.FindItemOnLayer(Layer.Unused_x9);
                if (quiver != null)
                {
                    m_Player.RemoveItem(quiver);
                    quiver.Delete();
                }
                m_Player.EquipItem(new EnchantedQuiver());
            }

        }


        [Usage("Leap")]
        [Description("Allows monks to leap to the sky")]
        private static void Leap_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.LeapOfClouds) && m_Player.AccessLevel == AccessLevel.Player)
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.LeapOfClouds))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else
            {
                m_Player.Target = new LeapTarget();
            }
        }

        private class LeapTarget : Target
        {
            public LeapTarget()
                : base(12, true, TargetFlags.None)
            {
                CheckLOS = false;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                TeiravonMobile m_Player = (TeiravonMobile)from;
                IPoint3D p = o as IPoint3D;
                Map map = from.Map;
                if (p == null)
                    return;

                SpellHelper.GetSurfaceTop(ref p);

                Point3D fromLoc = from.Location;
                Point3D toLoc = new Point3D(p);
                Point3D apex = new Point3D();

                int countX = Math.Abs(fromLoc.X - toLoc.X);
                int countY = Math.Abs(fromLoc.Y - toLoc.Y);
                int count = countX;

                if (countX < countY)
                    count = countY;

                int dist = count;

                apex.X = (fromLoc.X + toLoc.X) / 2;
                apex.Y = (fromLoc.Y + toLoc.Y) / 2;
                apex.Z = (int)(5 * dist) + fromLoc.Z;

                if (apex.Z < toLoc.Z - 10)
                {
                    apex.Z = toLoc.Z;
                    apex.Z += 10;
                }
                bool tooHigh = false;
                tooHigh = (m_Player.PlayerLevel * 4 + 10) < apex.Z - fromLoc.Z;
                int max = ((int)(m_Player.PlayerLevel / 3) + 2);
                bool pastMax = false;
                pastMax = dist > max || tooHigh;
                bool seeTop = true;
                bool seeTarg = true;
                seeTop = from.InLOS(apex);
                seeTarg = from.Map.LineOfSight(apex, toLoc);

                if (pastMax)
                {
                    from.SendMessage("That's too far away.");
                    return;
                }

                if (!seeTop || !seeTarg || dist <= 1)
                {
                    //from.SendMessage(" distance {0} can see top {1} can see target {2}", dist, seeTop, seeTarg);
                    from.SendMessage("You can't leap there.");
                    return;
                }
                if (from.Stam < 30)
                {
                    from.SendMessage("You're too exausted to leap.");
                    return;
                }
                else if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
                {
                    from.SendLocalizedMessage(501942); // That location is blocked.
                }
                else if (SpellHelper.CheckMulti(new Point3D(p), map))
                {
                    from.SendLocalizedMessage(501942); // That location is blocked.
                }
                /*
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.LeapOfClouds))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
                 */
                else
                {
                    //from.SendMessage("apex at {0} final target at {1}", apex.ToString(), toLoc.ToString());
                    Timer A_Timer = new LeapAscent(m_Player, apex, toLoc, fromLoc, DateTime.Now + TimeSpan.FromSeconds(0.1));
                    A_Timer.Start();
                    Timer S_Timer = new SafeLeap(m_Player, fromLoc, A_Timer, DateTime.Now + TimeSpan.FromSeconds(10));
                    S_Timer.Start();
                    from.Animate(24, 4, 1, true, false, 0);
                    from.PlaySound(0x512);
                    from.Direction = from.GetDirectionTo(toLoc);
                    from.Stam -= 10;
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.LeapOfClouds, true);

                }
            }


        }


        private class SafeLeap : Timer
        {
            TeiravonMobile m_Player;
            Point3D start;
            Point3D dest;
            Timer Up;

            public SafeLeap(TeiravonMobile from, Point3D fromLoc, Timer ascent, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Player = from;
                start = fromLoc;
                Up = ascent;
            }

            protected override void OnTick()
            {
                if (m_Player.GetActiveFeats(TeiravonMobile.Feats.LeapOfClouds) && Up != null)
                {
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.LeapOfClouds, false);
                    m_Player.Location = start;
                    Up.Stop();
                }
            }
        }

        private class LeapAscent : Timer
        {
            TeiravonMobile m_Player;
            Point3D peak;
            int m_EndX;
            int m_EndY;
            Point3D dest;
            Point3D FromLoc;

            public LeapAscent(TeiravonMobile from, Point3D apex, Point3D toLoc, Point3D fromLoc, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Player = from;
                m_EndX = apex.X;
                m_EndY = apex.Y;
                dest = toLoc;
                peak = apex;
                FromLoc = fromLoc;
            }

            protected override void OnTick()
            {
                if (m_Player.X == peak.X && m_Player.Y == peak.Y && (peak.Z - m_Player.Z) < 10)
                {
                    Timer D_Timer = new LeapDescent(m_Player, peak, dest, FromLoc, 1, DateTime.Now + TimeSpan.FromSeconds(0.2));
                    D_Timer.Start();
                    //m_Player.SendMessage("Falling");
                    return;
                }
                if ((m_Player.X - peak.X) + (m_Player.Y - peak.Y) != 0)
                {
                    if (Math.Abs(m_Player.Y - peak.Y) == 0 || (Math.Abs(m_Player.X - peak.X) / Math.Abs(m_Player.Y - peak.Y)) >= 2)
                    {
                        if ((m_Player.X - peak.X) < 0)
                            m_Player.X++;
                        else if ((m_Player.X - peak.X) > 0)
                            m_Player.X--;
                    }
                    else if (Math.Abs(m_Player.X - peak.X) == 0 || (Math.Abs(m_Player.Y - peak.Y) / Math.Abs(m_Player.X - peak.X)) > 2)
                    {
                        if ((m_Player.Y - peak.Y) < 0)
                            m_Player.Y++;
                        else if ((m_Player.Y - peak.Y) > 0)
                            m_Player.Y--;
                    }
                    else
                    {
                        if ((m_Player.X - peak.X) < 0)
                            m_Player.X++;
                        else if ((m_Player.X - peak.X) > 0)
                            m_Player.X--;

                        if ((m_Player.Y - peak.Y) < 0)
                            m_Player.Y++;
                        else if ((m_Player.Y - peak.Y) > 0)
                            m_Player.Y--;
                    }
                }
                int height = peak.Z - m_Player.Z;
                int oldz = m_Player.Z;
                m_Player.Z += (int)(height * .35);
                //m_Player.SendMessage("peak at {0} and height of {1}",peak.Z,height);

                if (m_Player.Z == oldz)
                    m_Player.Z = peak.Z;
                if (oldz == peak.Z)
                {
                    m_Player.X = peak.X;
                    m_Player.Y = peak.Y;
                }

                Timer A_Timer = new LeapAscent(m_Player, peak, dest, FromLoc, DateTime.Now + TimeSpan.FromSeconds(0.1));
                A_Timer.Start();
            }
        }

        private class LeapDescent : Timer
        {
            TeiravonMobile m_Player;
            Point3D apex;
            int m_EndX;
            int m_EndY;
            int Runcount;
            Point3D dest;
            Point3D FromLoc;

            public LeapDescent(TeiravonMobile from, Point3D apex, Point3D toLoc, Point3D fromLoc, int runcount, DateTime end)
                : base(end - DateTime.Now)
            {
                dest = toLoc;
                FromLoc = fromLoc;
                m_Player = from;
                m_EndX = dest.X;
                m_EndY = dest.Y;
                Runcount = runcount;
            }

            protected override void OnTick()
            {
                if ((m_Player.X - dest.X) + (m_Player.Y + dest.Y) != 0)
                {
                    if (Math.Abs(m_Player.Y - dest.Y) == 0 || (Math.Abs(m_Player.X - dest.X) / Math.Abs(m_Player.Y - dest.Y)) >= 2)
                    {
                        if ((m_Player.X - dest.X) < 0)
                            m_Player.X++;
                        else if ((m_Player.X - dest.X) > 0)
                            m_Player.X--;
                    }
                    else if (Math.Abs(m_Player.X - dest.X) == 0 || (Math.Abs(m_Player.Y - dest.Y) / Math.Abs(m_Player.X - dest.X)) > 2)
                    {
                        if ((m_Player.Y - dest.Y) < 0)
                            m_Player.Y++;
                        else if ((m_Player.Y - dest.Y) > 0)
                            m_Player.Y--;
                    }
                    else
                    {
                        if ((m_Player.X - dest.X) < 0)
                            m_Player.X++;
                        else if ((m_Player.X - dest.X) > 0)
                            m_Player.X--;

                        if ((m_Player.Y - dest.Y) < 0)
                            m_Player.Y++;
                        else if ((m_Player.Y - dest.Y) > 0)
                            m_Player.Y--;
                    }
                }
                int height = Math.Abs(m_Player.Z - dest.Z);
                int newz = m_Player.Z - (Runcount * 3);

                if (newz <= dest.Z)
                {
                    m_Player.Location = dest;
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.LeapOfClouds, false);
                    return;
                }
                else
                    m_Player.Z = newz;

                if (Runcount > 25)
                {
                    m_Player.Location = FromLoc;
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.LeapOfClouds, false);
                    this.Stop();
                }
                Runcount++;
                Timer m_Timer = new LeapDescent(m_Player, apex, dest, FromLoc, Runcount, DateTime.Now + TimeSpan.FromSeconds(0.1));
                m_Timer.Start();
            }
        }

        [Usage("ThrowAxe")]
        [Description("Allows dwarves to throw axes")]
        private static void ThrowAxe_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            BaseWeapon wpn = (BaseWeapon)m_Player.Weapon;

            if (wpn is BaseAxe && m_Player.IsDwarf())
            {
                InternalTarget t = new InternalTarget(wpn);
                m_Player.Target = t;
            }
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else m_Player.SendMessage("You can not do that");
        }

        private class InternalTarget : Target
        {
            private BaseAxe m_Axe;

            public InternalTarget(BaseWeapon Axe)
                : base(10, false, TargetFlags.Harmful)
            {
                m_Axe = (BaseAxe)Axe;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {


                if (m_Axe.Deleted)
                {
                    return;
                }
                else if (!from.Items.Contains(m_Axe))
                {
                    from.SendMessage("You must be holding the axe to use it.");
                }
                else if (targeted is Mobile && from is TeiravonMobile)
                {
                    Mobile m = (Mobile)targeted;


                    if (from is TeiravonMobile && m != from && from.HarmfulCheck(m))
                    {
                        Direction to = from.GetDirectionTo(m);

                        from.Direction = to;

                        from.Animate(from.Mounted ? 26 : 9, 7, 1, true, false, 0);

                        TeiravonMobile TM = (TeiravonMobile)from;

                        if (from.Stam < 60)
                        {
                            from.SendMessage("You are too fatiqued to do this");
                            return;
                        }




                        if (BaseRanged.CheckAim(from, m))
                        {
                            from.MovingEffect(m, m_Axe.ItemID, 7, 1, false, false, 0x481, 0);

                            m_Axe.MoveToWorld(m.Location, m.Map);

                            m_Axe.OnHit(from, m);

                            //m_Axe.Hits = m_Axe.Hits - 10;


                            if (m_Axe.Hits >= 10)
                                m_Axe.Hits -= 10;
                            else
                            {

                                if (m_Axe.MaxHits > 10)
                                {
                                    m_Axe.MaxHits -= 10 - m_Axe.Hits;
                                }
                                else
                                {
                                    m_Axe.Delete();
                                    from.SendMessage("Your axe has been destroyed!");
                                }
                                m_Axe.Hits = 0;
                            }



                            from.Stam = from.Stam - 50;

                            if (m is TeiravonMobile)
                            {
                                TeiravonMobile t = (TeiravonMobile)m;
                                if (t.Mounted) t.Mount.Rider = null;
                                t.Dismounted();
                                t.BeginAction(typeof(BaseMount));
                                Timer.DelayCall(TimeSpan.FromSeconds(10.0), new TimerStateCallback(ReleaseMountLock), t);

                            }
                            m.PlaySound(0x140);
                            m.FixedParticles(0x3728, 10, 15, 9955, EffectLayer.Waist);


                        }
                        else
                        {
                            int x = 0, y = 0;

                            switch (to & Direction.Mask)
                            {
                                case Direction.North: --y; break;
                                case Direction.South: ++y; break;
                                case Direction.West: --x; break;
                                case Direction.East: ++x; break;
                                case Direction.Up: --x; --y; break;
                                case Direction.Down: ++x; ++y; break;
                                case Direction.Left: --x; ++y; break;
                                case Direction.Right: ++x; --y; break;
                            }

                            x += Utility.Random(-1, 3);
                            y += Utility.Random(-1, 3);

                            x += m.X;
                            y += m.Y;

                            m_Axe.MoveToWorld(new Point3D(x, y, m.Z), m.Map);

                            from.MovingEffect(m_Axe, m_Axe.ItemID, 7, 1, false, false, 0x481, 0);

                            from.SendMessage("You miss!");
                        }
                    }
                }
            }
            private void ReleaseMountLock(object state)
            {
                ((Mobile)state).EndAction(typeof(BaseMount));
            }

        }


        [Usage("SE")]
        [Description("Emote when hidden")]
        private static void SilentEmote_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            if (!m_Player.Hidden)
            {
                m_Player.SendMessage("You have to be hidden to do that.");
                return;
            }

            ArrayList targets = new ArrayList();
            foreach (Mobile m in m_Player.GetMobilesInRange(8))
            {
                if (m_Player != m && m_Player.InLOS(m) && m is PlayerMobile)
                    targets.Add(m);
            }

            if (targets.Count > 0)
            {

                for (int i = 0; i < targets.Count; ++i)
                {
                    PlayerMobile m = (PlayerMobile)targets[i];
                    m.SendMessage(e.ArgString);
                }
            }


        }



        [Usage("Rage")]
        [Description("Rage Meter")]
        private static void Rage_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.BerserkerRage))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else
            {
                m_Player.SendMessage(Teiravon.Colors.RedwoodColor, "You've built up {0}% Rage.", m_Player.BAC);
            }
        }

        [Usage("Calm")]
        [Description("Bite back your rage")]
        private static void Calm_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.BerserkerRage))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            }
            else if (m_Player.BAC > 80)
            {
                m_Player.SendMessage(Teiravon.Colors.RedwoodColor, "Your rage is uncontrollable!");
            }
            else if (m_Player.BAC < 20)
            {
                m_Player.SendMessage(Teiravon.Colors.RedwoodColor, "You're calm enough already.");
            }
            else
            {
                m_Player.BAC -= 16;
                m_Player.SendMessage(Teiravon.Colors.RedwoodColor, "You bite back your rage, calming yourself somewhat.");
            }
        }



        [Usage("Feats")]
        [Description("Feat selection menu")]
        private static void Feats_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            m_Player.SendGump(new FeatsGump(m_Player));
        }

        [Usage("Lunge")]
        [Description("Allows Dragoons to do a powerlunge attack")]
        private static void PowerLunge_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            BaseWeapon wpn = (BaseWeapon)m_Player.Weapon;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.PowerLunge))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.PowerLunge))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (wpn is Fists)
                m_Player.SendMessage("You must have a weapon equipped for this attack.");
            else
            {
                m_Player.Target = new LungeTarget();
            }
        }

        private class LungeTarget : Target
        {
            int atkrng, damage;
            int phys, fire, cold, pois, nrgy;

            public LungeTarget()
                : base(3, true, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                IPoint3D p = o as IPoint3D;
                TeiravonMobile m_Player = from as TeiravonMobile;
                BaseWeapon wpn = (BaseWeapon)m_Player.Weapon;
                Engines.PartySystem.Party party = Engines.PartySystem.Party.Get(m_Player);

                if (wpn is BasePoleArm)
                    atkrng = 2;
                else
                    atkrng = 1;

                if (((p != null) && (from.Map.CanSpawnMobile(p.X, p.Y, p.Z))) || o is Mobile)
                {

                    damage = 20 + (m_Player.PlayerLevel * 4);

                    wpn.GetDamageTypes(m_Player, out phys, out fire, out cold, out pois, out nrgy);
                    Point3D to = new Point3D(p);

                    m_Player.Emote("*lunges forward*");
                    m_Player.Location = to;
                    m_Player.PlaySound(0x524);


                    if (o is Mobile && m_Player.HarmfulCheck((Mobile)o))
                    {
                        Mobile mob = o as Mobile;
                        AOS.Damage(mob, m_Player, damage, phys, fire, cold, pois, nrgy);
                    }

                    try
                    {
                        foreach (Mobile m in m_Player.GetMobilesInRange(atkrng))
                        {
                            if (party != null && party.Contains(m))
                                continue;

                            else if (m != m_Player && m_Player.HarmfulCheck(m))
                                wpn.OnHit(m_Player, m);
                        }
                    }
                    catch { }

                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.PowerLunge;
                    m_TimerHelper.Duration = TAVCoolDown.AtWill;
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.PowerLunge, true);
                }
            }
        }

        [Usage("Callmount")]
        [Description("Allows a cavalier to summon a war mount")]
        private static void CallMount_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.WarMount))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.WarMount))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Mounted)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "Please dismount first.");
            else
            {
                WarMount m_Horse = new WarMount();
                m_Horse.Name = String.Format("{0}'s War Horse", m_Player.Name);
                if (m_Player.PlayerAlignment == TeiravonMobile.Alignment.LawfulGood)
                    m_Horse.Hue = 2229;
                else if (m_Player.PlayerAlignment == TeiravonMobile.Alignment.LawfulNeutral)
                    m_Horse.Hue = 2413;
                else if (m_Player.PlayerAlignment == TeiravonMobile.Alignment.LawfulEvil)
                    m_Horse.Hue = 2406;

                if (m_Player.IsDwarf())
                {
                    m_Horse.ItemID = 16058;
                    m_Horse.BodyValue = 187;
                    m_Horse.BaseSoundID = 0x3F3;
                }

                if (m_Player.IsOrc())
                {
                    m_Horse.ItemID = 16017;
                    m_Horse.BodyValue = 277;
                    m_Horse.BaseSoundID = 0xE5;
                    m_Horse.Name = String.Format("{0}'s Warg", m_Player.Name);
                }

                if (BaseCreature.Summon(m_Horse, m_Player, m_Player.Location, -1, TimeSpan.FromDays(1.0)))
                {
                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.WarMount;
                    m_TimerHelper.Duration = new TimeSpan(2 + m_Player.WarMountDeaths, 0, 0);
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.WarMount, true);
                }
            }
        }

        [Usage("Companion")]
        [Description("Allows rangers and druids to call animal companions")]
        private static void Companion_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.AnimalCompanion))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.AnimalCompanion))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            }
            else
            {
                try
                {
                    Type companion = typeof(BearCompanion);
                    if (m_Player.IsDwarf())
                    {
                        switch (Utility.RandomBool())
                        {
                            case true:
                                companion = typeof(BearCompanion); break;
                            case false:
                                companion = typeof(MountainGoatCompanion); break;
                        }
                    }
                    else if (m_Player.IsDrow())
                    {
                        companion = typeof(SpiderCompanion);
                    }
                    else
                    {

                        switch (Utility.Random(1, 5))
                        {
                            case 1:
                                companion = typeof(BearCompanion); break;
                            case 2:
                                companion = typeof(EagleCompanion); break;
                            case 3:
                                companion = typeof(WolfCompanion); break;
                            case 4:
                                companion = typeof(PantherCompanion); break;
                            case 5:
                                companion = typeof(GreatHartCompanion); break;
                        }
                    }

                    BaseCreature bc = (BaseCreature)Activator.CreateInstance(companion);

                    int str = 25;
                    int dex = 40;
                    int intel = 15;
                    bc.SetDamage(5, 10);

                    switch (m_Player.PlayerLevel)
                    {
                        case 1: break;
                        case 2:
                        case 3:
                            str = 50; dex = 50; intel = 25; break;
                        case 4:
                        case 5:
                            str = 70; dex = 60; intel = 35; bc.SetDamage(8, 13); break;
                        case 6:
                        case 7:
                            str = 80; dex = 70; intel = 45; bc.SetDamage(10, 15); break;
                        case 8:
                        case 9:
                            str = 100; dex = 80; intel = 55; bc.SetDamage(15, 20); break;
                        case 10:
                        case 11:
                            str = 150; dex = 90; intel = 55; bc.SetDamage(18, 22); break;
                        case 12:
                        case 13:
                            str = 200; dex = 100; intel = 65; bc.SetDamage(20, 26); break;
                        case 14:
                        case 15:
                            str = 250; dex = 110; intel = 75; bc.SetDamage(24, 28); break;
                        case 16:
                        case 17:
                            str = 300; dex = 120; intel = 85; bc.SetDamage(26, 32); break;
                        default:
                            str = 350; dex = 130; intel = 95; bc.SetDamage(30, 34); break;
                    }
                    bc.SetStr(str);
                    bc.SetDex(dex);
                    bc.SetInt(intel);

                    bc.SetHits(str);
                    bc.SetStam(dex);
                    bc.SetMana(0);

                    if (m_Player.PlayerLevel <= 10)
                        bc.Level = m_Player.PlayerLevel;
                    else
                        bc.Level = 10;

                    bc.SetDamageType(ResistanceType.Physical, 100);

                    bc.SetResistance(ResistanceType.Physical, 40, 50);
                    bc.SetResistance(ResistanceType.Fire, 25, 40);
                    bc.SetResistance(ResistanceType.Cold, 25, 40);
                    bc.SetResistance(ResistanceType.Poison, 25, 40);
                    bc.SetResistance(ResistanceType.Energy, 25, 40);

                    bc.SetSkill(SkillName.Wrestling, 85.0, 100.0);
                    bc.SetSkill(SkillName.Tactics, 50.0, 100.0);

                    bc.Karma = m_Player.Karma;

                    bc.Skills.MagicResist = m_Player.Skills.MagicResist;

                    if (BaseCreature.Summon(bc, m_Player, m_Player.Location, -1, TimeSpan.FromDays(1.0)))
                    {
                        m_Player.SendMessage("An animal answers your call and is now bonded to you.");
                        bc.Summoned = false;
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.AnimalCompanion;
                        m_TimerHelper.Duration = TAVCoolDown.Daily;
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.AnimalCompanion, true);

                    }
                }
                catch
                {
                }
            }
        }

        [Usage("SummonFamiliar")]
        [Description("Allows mages to call their familiar")]
        private static void SummonFamiliar_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
            ArrayList jealousy = new ArrayList();
            if (!m_Player.HasFeat(TeiravonMobile.Feats.ImprovedFamiliar))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.ImprovedFamiliar) && m_Player.AccessLevel < AccessLevel.GameMaster)
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            }

            else
            {
                try
                {
                    Type familiar = typeof(TAVAquaFamiliar);

                    if (m_Player.IsAeromancer())
                        familiar = typeof(TAVAreoFamiliar);
                    if (m_Player.IsAquamancer())
                        familiar = typeof(TAVAquaFamiliar);
                    if (m_Player.IsGeomancer())
                        familiar = typeof(TAVGeoFamiliar);
                    if (m_Player.IsPyromancer())
                        familiar = typeof(TAVPyroFamiliar);

                    BaseCreature bc = (BaseCreature)Activator.CreateInstance(familiar);

                    if (BaseCreature.Summon(bc, m_Player, m_Player.Location, -1, TimeSpan.FromDays(1.0)))
                    {
                        m_Player.SendMessage("An elemental spirit answers your call");

                        if (m_Player.Followers > 1)
                        {
                            IPooledEnumerable eable = bc.GetMobilesInRange(8);
                            foreach (Mobile m in eable)
                            {
                                if (m is BaseFamiliar)
                                {
                                    BaseFamiliar f = m as BaseFamiliar;
                                    if (f.ControlMaster == bc.ControlMaster)
                                        jealousy.Add(f);
                                }
                            }
                            eable.Free();

                            if (jealousy.Count > 1)
                            {
                                foreach (BaseFamiliar f in jealousy)
                                {
                                    bc.Dispel(f);
                                }
                            
                                m_Player.SendMessage("but becomes jealous of the other spirit and vanishes.");
                            }
                        }

                        bc.Summoned = true;
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.ImprovedFamiliar;
                        m_TimerHelper.Duration = TAVCoolDown.Daily;
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.ImprovedFamiliar, true);
                    }
                }
                catch
                {
                }
            }
        }


        [Usage("DarkAura")]
        [Description("Activates the dark cleric's dark aura")]
        private static void DarkAura_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.DarkAura) || !m_Player.Magicable)
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
                return;
            }
            else if (m_Player.StunShotReady == true)
            {
                m_Player.SendMessage("You begin to mask your aura...");
                m_Player.StunShotReady = false;
                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.DarkAura;
                m_TimerHelper.Duration = TimeSpan.FromSeconds(5);
                m_TimerHelper.Start();

                m_Player.SetActiveFeats(TeiravonMobile.Feats.DarkAura, true);
                return;

            }
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.DarkAura))
            {
                m_Player.SendMessage("You must wait to use this ability again.");
                return;
            }
            else
            {
                int percent = (int)(m_Player.MaxMana * .02);
                if (m_Player.Mana < percent)
                    m_Player.SendMessage("You must have at least {0} mana to use this.", percent);

                m_Player.SendMessage("Your dark aura surrounds you.");
                m_Player.StunShotReady = true;

                Timer m_Timer = new DarkAuraTimer(m_Player, DateTime.Now + TimeSpan.FromSeconds(2.0));
                m_Timer.Start();

                m_Player.Mana -= m_Player.PlayerLevel;

            }
        }

        [Usage("DivineAura")]
        [Description("Activates the undead hunter's divine aura")]
        private static void DivineAura_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.DivineAura))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.DivineAura))
            {
                m_Player.SendMessage("You begin to mask your aura...");
                m_Player.SetActiveFeats(TeiravonMobile.Feats.DivineAura, false);
            }
            else
            {
                int percent = (int)(m_Player.MaxMana * .02);
                if (m_Player.Mana < percent)
                    m_Player.SendMessage("You must have at least {0} mana to use this.", percent);
                else
                {
                    m_Player.SendMessage("Your divine aura surrounds you.");

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.DivineAura, true);
                    Timer m_Timer = new HolyAuraTimer(m_Player, DateTime.Now + TimeSpan.FromSeconds(2.0));
                    m_Timer.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.DarkAura, false);
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.HolyAura, false);

                    m_Player.Mana -= m_Player.PlayerLevel;

                }
            }
        }



        [Usage("HolyAura")]
        [Description("Activates the cleric's holy aura")]
        private static void HolyAura_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.HolyAura) || !m_Player.Magicable)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.HolyAura))
            {
                m_Player.SendMessage("You must wait to use this ability again.");
            }
            else if (m_Player.DisarmShotReady)
            {
                m_Player.SendMessage("You begin to mask your aura...");
                m_Player.DisarmShotReady = false;
                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.HolyAura;
                m_TimerHelper.Duration = TimeSpan.FromSeconds(5);
                m_TimerHelper.Start();

                m_Player.SetActiveFeats(TeiravonMobile.Feats.HolyAura, true);
                return;
            }

            else
            {
                if (m_Player.Mana < 20)
                    m_Player.SendMessage("You must have at least 20 mana to use this.");
                else
                {
                    m_Player.SendMessage("Your holy aura surrounds you.");
                    m_Player.DisarmShotReady = true;
                    Timer m_Timer = new HolyAuraTimer(m_Player, DateTime.Now + TimeSpan.FromSeconds(2.0));
                    m_Timer.Start();
                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.HolyAura;
                    m_TimerHelper.Duration = TimeSpan.FromSeconds(2);
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.HolyAura, true);
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.DivineAura, false);
                    m_Player.SetActiveFeats(TeiravonMobile.Feats.DarkAura, false);

                    m_Player.Mana -= m_Player.PlayerLevel;
                }
            }
        }

        /*		[Usage( "BarbarianInstinct" )]
                [Description( "Allows a berserker to gain a powerful defensive bonus" )]
                public static void BarbarianInstinct_OnCommand( CommandEventArgs e )
                {
                    TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

                    if ( m_Player.Hits > ( m_Player.MaxHits / 3 ) )
                    {
                        m_Player.SendMessage( "You must have less then 30% life left to use this." );
                        return;
                    }

                    if ( m_Player.HasFeat( TeiravonMobile.Feats.BarbarianInstinct ) )
                    {
                        if ( !m_Player.GetActiveFeats( TeiravonMobile.Feats.BarbarianInstinct ) )
                        {
                            int str = 0;

                            if ( m_Player.PlayerLevel <= 3 )
                                str = 25;

                            if ( m_Player.PlayerLevel >= 4 && m_Player.PlayerLevel <= 7 )
                                str = 30;

                            if ( m_Player.PlayerLevel >= 8 && m_Player.PlayerLevel <= 11 )
                                str = 35;

                            if ( m_Player.PlayerLevel >= 12 && m_Player.PlayerLevel <= 15 )
                                str = 40;

                            if ( m_Player.PlayerLevel >= 16 && m_Player.PlayerLevel <= 19 )
                                str = 45;

                            if ( m_Player.PlayerLevel >= 20 )
                                str = 50;

                            m_Player.Emote( "*You see {0} appear to be going berserk!*", m_Player.Name );

                            StatMod BarbarianInstinctStatMod = new StatMod( StatType.Str, (int)m_Player.Serial + " BarbarianInstinct Str", str, TimeSpan.FromSeconds( 45 ) );
                            m_Player.AddStatMod( BarbarianInstinctStatMod );

                            // Stat timer
                            TimerHelper m_StatHelper = new TimerHelper( (int)m_Player.Serial );
                            m_StatHelper.MessageOn = true;
                            m_StatHelper.Duration = TimeSpan.FromSeconds( 45 );
                            m_StatHelper.Message = "You beging to calm down...";
                            m_StatHelper.Start();

                            // Reuse timer
                            TimerHelper m_TimerHelper = new TimerHelper( (int)m_Player.Serial );
                            m_TimerHelper.DoFeat = true;
                            m_TimerHelper.Feat = TeiravonMobile.Feats.BarbarianInstinct;
                            m_TimerHelper.Duration = TAVCoolDown.BarbarianInstinct;
                            m_TimerHelper.Start();

                            m_Player.SetActiveFeats( TeiravonMobile.Feats.BarbarianInstinct, true );

                        }
                        else
                            m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon );
                    }
                    else
                        m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility );
                } */

        [Usage("Melody")]
        [Description("Plays an enchanting tune that those around you cannot help but stop to hear.")]
        private static void EnchantingMelody_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.EnchantingMelody))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.EnchantingMelody))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Mana < 20)
                m_Player.SendMessage("You cannot muster the concentration.");
            else
            {
                m_Player.Emote("*Plays an enchanting tune*");
                m_Player.PlaySound(0x244);
                m_Player.PlaySound(0x418);

                Engines.PartySystem.Party p = Engines.PartySystem.Party.Get(m_Player);

                foreach (Mobile m in m_Player.GetMobilesInRange(10))
                {
                    if (m == m_Player)
                        continue;
                    if (p != null && p.Contains(m))
                        continue;

                    if (m is BaseCreature)
                    {
                        BaseCreature creature = (BaseCreature)m;

                        if (!creature.Unprovokable || !creature.Uncalmable)
                        {
                            creature.Freeze(TimeSpan.FromSeconds(10));
                            m.Emote("*Pauses to listen...*");
                        }
                    }
                    else
                    {
                        if (m is TeiravonMobile && m.AccessLevel == AccessLevel.Player)
                        {
                            TeiravonMobile tm = (TeiravonMobile)m;
                            if (m_Player.PlayerLevel + 2 >= tm.PlayerLevel)
                            {
                                m.ClearHands();
                                m.Freeze(TimeSpan.FromSeconds(10));
                                BaseWeapon.BlockEquip(m, TimeSpan.FromSeconds(10.0));

                                m.SendMessage("You hear an enchanting tune and you feel unable to do anything else...");
                            }

                            else if (m_Player.Int > m.Int)
                            {
                                m.ClearHands();
                                m.Freeze(TimeSpan.FromSeconds(10));
                                BaseWeapon.BlockEquip(m, TimeSpan.FromSeconds(10.0));

                                m.SendMessage("You hear an enchanting tune and you feel unable to do anything else...");
                            }
                        }
                    }
                }

                // Reuse timer
                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.EnchantingMelody;
                m_TimerHelper.Duration = TAVCoolDown.Encounter;
                m_TimerHelper.Start();

                m_Player.Mana -= 20;
                m_Player.SetActiveFeats(TeiravonMobile.Feats.EnchantingMelody, true);
            }
        }

        [Usage("DragonRoar")]
        [Description("Allows a berserker to let out a defeaning roar")]
        private static void DragonRoar_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.DragonRoar))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.DragonRoar))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
            {
                m_Player.Emote("*Lets out a deafening roar!*");

                Engines.PartySystem.Party p = Engines.PartySystem.Party.Get(m_Player);

                foreach (Mobile m in m_Player.GetMobilesInRange(10))
                {
                    if (m == m_Player)
                        continue;
                    if (p != null && p.Contains(m))
                        continue;

                    if (m is BaseCreature)
                    {
                        BaseCreature creature = (BaseCreature)m;

                        if (!creature.Unprovokable || !creature.Uncalmable || !creature.Blessed)
                        {
                            creature.BeginFlee(TimeSpan.FromSeconds(10));

                            m.Emote("*Flees in terror*");
                        }
                    }
                    else
                    {
                        if (m is TeiravonMobile)
                        {
                            TeiravonMobile tm = (TeiravonMobile)m;
                            if (m_Player.PlayerLevel + 2 >= tm.PlayerLevel)
                            {
                                Bandage.BandageClearHands(m);
                                m.Freeze(TimeSpan.FromSeconds(5));
                                BaseWeapon.BlockEquip(m, TimeSpan.FromSeconds(10.0));

                                m.SendMessage("You're hands begin to tremble and you feel unable to do anything...");
                            }
                        }
                        else if (m_Player.Str > m.Str)
                        {
                            Bandage.BandageClearHands(m);
                            m.Freeze(TimeSpan.FromSeconds(5));
                            BaseWeapon.BlockEquip(m, TimeSpan.FromSeconds(10.0));

                            m.SendMessage("You're hands begin to tremble and you feel unable to do anything...");
                        }
                    }
                }

                // Reuse timer
                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.DragonRoar;
                m_TimerHelper.Duration = TAVCoolDown.Encounter;
                m_TimerHelper.Start();

                m_Player.SetActiveFeats(TeiravonMobile.Feats.DragonRoar, true);
            }
        }

        private static void ChargedMissile_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.ChargedMissile))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.ChargedMissile))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
            {
                m_Player.SendMessage("Your hands tingle with arcane energy...");
                m_Player.ChargedMissileReady = true;

                // Reuse timer
                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.ChargedMissile;
                m_TimerHelper.Duration = TAVCoolDown.ChargedMissile;
                m_TimerHelper.Start();

                m_Player.SetActiveFeats(TeiravonMobile.Feats.ChargedMissile, true);
            }

        }

        private static void CalledShot_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.CalledShot))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.CalledShot))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
            {
                m_Player.FatalShotReady = true;
                m_Player.SendMessage("You call your shot!");

                // Reuse timer
                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.CalledShot;
                m_TimerHelper.Duration = TAVCoolDown.AtWill;
                m_TimerHelper.Start();

                m_Player.SetActiveFeats(TeiravonMobile.Feats.CalledShot, true);

                //				m_Player.CloseGump( typeof( Teiravon.Feats.CalledShotGump ) );
                //				m_Player.SendGump( new Teiravon.Feats.CalledShotGump( m_Player ) );
            }

        }

        [Usage("Backstab")]
        [Description("An assassin ability that allows them to stab targets in vulnerable places")]
        private static void Backstab_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Backstab))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Backstab))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
                m_Player.Target = new BackstabTarget();
        }

        private class BackstabTarget : Target
        {
            public BackstabTarget()
                : base(1, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {

                    TeiravonMobile m_Player = (TeiravonMobile)from;
                    Mobile m_Target = (Mobile)o;
                    BaseWeapon wep = (BaseWeapon)m_Player.Weapon;

                    if (o is BaseCreature && ((BaseCreature)o).AI == AIType.AI_Vendor)
                    {
                        CommandLogging.WriteLine(from, "ATTEMPTED VENDOR BACKSTAB : {0} {1}", m_Target.Name, m_Target.Location);
                        return;
                    }

                    if (m_Player == m_Target)
                    {
                        m_Player.SendMessage("You cannot backstab yourself!");
                        return;
                    }

                    if (wep.DefType == WeaponType.Ranged || wep.DefType == WeaponType.Thrown)
                    {
                        m_Player.SendMessage("You cannot use that to backstab.");
                        return;
                    }
                    // ++ behind the target

                    Direction DefDirection = m_Target.Direction;
                    Direction DefToAtt = m_Target.GetDirectionTo(m_Player.Location.X, m_Player.Location.Y);
                    /*
                    if (Math.Abs((int)DefDirection - (int)DefToAtt) < 3)
                    {*/
                    if (!wep.IsBehind(m_Player, m_Target))
                    {
                        from.SendMessage("You need to be behind the victim.");
                        return;
                    }
                    else

                        // -- behind the target

                        if (m_Target is TeiravonMobile)
                        {
                            TeiravonMobile targ = (TeiravonMobile)m_Target;
                            if (targ.HasFeat(TeiravonMobile.Feats.BarbarianInstinct) || targ.HasFeat(TeiravonMobile.Feats.UncannyDefense))
                            {
                                m_Player.Hidden = false;
                                targ.Emote("You see {0} avoid being backstabbed by {1}!", targ.Name, m_Player.Name);
                                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                                m_TimerHelper.DoFeat = true;
                                m_TimerHelper.Feat = TeiravonMobile.Feats.Backstab;

                                m_TimerHelper.Duration = TimeSpan.FromSeconds(5.0);

                                m_TimerHelper.Start();

                                m_Player.SetActiveFeats(TeiravonMobile.Feats.Backstab, true);
                                return;
                            }

                        }



                    if (from.Hidden || wep.CheckHit(m_Player, m_Target))
                    {

                            m_Player.RevealingAction();
                            int minDamage, maxDamage;
                            m_Player.Weapon.GetStatusDamage(m_Player, out minDamage, out maxDamage);
                            int damage = Utility.RandomMinMax(minDamage,maxDamage);
                            int phys, fire, cold, pois, nrgy;
                            int resPhys = AOS.Scale(m_Target.PhysicalResistance, 50);
                            int resFire = AOS.Scale(m_Target.FireResistance, 50);
                            int resCold = AOS.Scale(m_Target.ColdResistance, 50);
                            int resPois = AOS.Scale(m_Target.PoisonResistance, 50);
                            int resNrgy = AOS.Scale(m_Target.EnergyResistance, 50);

                            wep.GetDamageTypes(m_Player, out phys, out fire, out cold, out pois, out nrgy);
                            damage += AOS.Scale(damage, (m_Player.PlayerLevel * 5));
                            damage += m_Player.PlayerLevel * 2;
                            int totalDamage;

                            totalDamage = damage * phys * (100 - resPhys);
                            totalDamage += damage * fire * (100 - resFire);
                            totalDamage += damage * cold * (100 - resCold);
                            totalDamage += damage * pois * (100 - resPois);
                            totalDamage += damage * nrgy * (100 - resNrgy);

                            totalDamage /= 10000;

                            if (totalDamage < 1)
                                totalDamage = 1;


                            m_Target.Damage(totalDamage, m_Player);
                            if (m_Player.HasFeat(TeiravonMobile.Feats.GreivousWounds))
                            {
                                TimeSpan PlayerDuration = TimeSpan.FromSeconds(m_Player.PlayerLevel * 2);
                                TimeSpan NPCDuration = TimeSpan.FromSeconds(m_Player.PlayerLevel * 4);
                                MortalStrike.BeginWound(m_Target, m_Target.Player ? PlayerDuration : NPCDuration);
                                BleedAttack.BeginBleed(m_Target, m_Player);
                            }
                            m_Player.PlaySound(0x530);
                            m_Player.Emote("*You see {0} is backstabbed by {1}!*", m_Target.Name, m_Player.Name);

                            // Reuse timer
                            TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                            m_TimerHelper.DoFeat = true;
                            m_TimerHelper.Feat = TeiravonMobile.Feats.Backstab;

                            m_TimerHelper.Duration = TAVCoolDown.AtWill;

                            m_TimerHelper.Start();

                            m_Player.SetActiveFeats(TeiravonMobile.Feats.Backstab, true);




                            //						int damage = Utility.RandomMinMax( 3, 6 ) * m_Player.PlayerLevel;
                            //
                            //						if ( m_Player.PlayerLevel >= 15 && ( Utility.RandomMinMax( 0, 100 ) < ( 10 +  ( ( m_Player.PlayerLevel - 15 ) * 3 ) ) ) )
                            //						{
                            //							m_Player.Emote( "*You see {0} is fatally wounded by {1}!*", m_Target.Name, m_Player.Name );
                            //							m_Target.Damage( 100000, (Mobile) m_Player );
                            //						}
                            //						else
                            //						{
                            //							m_Player.Emote( "*You see {0} is backstabbed by {1}!*", m_Target.Name, m_Player.Name );
                            //							m_Target.Damage( damage, (Mobile) m_Player );
                            //						}
                        
                    }
                    else
                    {
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.Backstab;

                        m_TimerHelper.Duration = TimeSpan.FromSeconds(5.0);

                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.Backstab, true);

                        m_Player.RevealingAction();
                        m_Target.Emote("*You see {0} was almost backstabbed by {1}!*", m_Target.Name, m_Player.Name);
                    }

                }
                else
                    from.SendMessage(Teiravon.Messages.NoTarget);
            }
        }

        [Usage("Pounce")]
        [Description("A shapeshifter ability to leap from hiding and savage their target with fang and claw.")]
        private static void Pounce_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Pounce))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Pounce))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (!m_Player.Shapeshifted)
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You must be shapeshifted to pounce.");
            else if (!m_Player.Hidden)
                m_Player.Target = new PounceLungeTarget();
            else
                m_Player.Target = new PounceTarget();
        }

        private class PounceLungeTarget : Target
        {
            int atkrng, damage;
            int phys, fire, cold, pois, nrgy;

            public PounceLungeTarget()
                : base(3, true, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                IPoint3D p = o as IPoint3D;
                TeiravonMobile m_Player = from as TeiravonMobile;
                BaseWeapon wpn = (BaseWeapon)m_Player.Weapon;
                Engines.PartySystem.Party party = Engines.PartySystem.Party.Get(m_Player);

                if (((p != null) && (from.Map.CanSpawnMobile(p.X, p.Y, p.Z))) || o is Mobile)
                {

                    damage = 20 + (m_Player.PlayerLevel * 4);

                    wpn.GetDamageTypes(m_Player, out phys, out fire, out cold, out pois, out nrgy);
                    Point3D to = new Point3D(p);

                    m_Player.Emote("*pounces forward*");
                    m_Player.Location = to;
                    m_Player.PlaySound(0x23b);


                    if (o is Mobile && m_Player.HarmfulCheck((Mobile)o))
                    {
                        Mobile mob = o as Mobile;
                        AOS.Damage(mob, m_Player, damage, phys, fire, cold, pois, nrgy);
                    }

                    try
                    {
                        foreach (Mobile m in m_Player.GetMobilesInRange(1))
                        {
                            if (party != null && party.Contains(m))
                                continue;

                            else if (m != m_Player && m_Player.HarmfulCheck(m))
                                wpn.OnHit(m_Player, m);
                        }
                    }
                    catch { }

                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.Pounce;
                    m_TimerHelper.Duration = TAVCoolDown.Encounter;
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.Pounce, true);
                }
            }
        }
        private class PounceTarget : Target
        {
            public PounceTarget()
                : base(3, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    if (!from.Hidden)
                        return;

                    TeiravonMobile m_Player = (TeiravonMobile)from;
                    Mobile m_Target = (Mobile)o;
                    BaseWeapon wep = (BaseWeapon)m_Player.Weapon;

                    if (m_Player == m_Target)
                    {
                        m_Player.SendMessage("You cannot pounce on yourself!");
                        return;
                    }
                    if (m_Target is TeiravonMobile)
                    {
                        TeiravonMobile targ = (TeiravonMobile)m_Target;
                        if (targ.HasFeat(TeiravonMobile.Feats.BarbarianInstinct))
                        {
                            m_Player.Hidden = false;
                            targ.Emote("You see {0} avoid being pounced on by {1}!", targ.Name, m_Player.Name);
                            return;
                        }

                    }



                    if (Utility.RandomMinMax(0, 100) < (80 + m_Player.PlayerLevel) && m_Player.Shapeshifted)
                    {
                        m_Player.Hidden = false;
                        m_Player.Location = m_Target.Location;

                        int damage = 1;
                        damage += m_Player.PlayerLevel * 2;

                        int bleed = AOS.Scale(m_Target.Hits, (m_Player.PlayerLevel / 2) + 5);

                        m_Target.Damage(damage, m_Player);
                        BleedAttack.DoBleed(m_Target, m_Player, bleed);

                        m_Player.PlaySound(0x519);
                        m_Player.Combatant = m_Target;
                        BleedAttack.BeginBleed(m_Target, m_Player);

                        m_Player.Emote("*You see {1} leap from hiding and pounce on {0}!*", m_Target.Name, m_Player.Name);

                        // Reuse timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.Pounce;

                        m_TimerHelper.Duration = TAVCoolDown.Encounter;

                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.Pounce, true);
                    }
                    else
                    {
                        m_Player.Hidden = false;
                        m_Target.Emote("*You see {0} was almost pounced on by {1}!*", m_Target.Name, m_Player.Name);
                    }

                }
                else
                    from.SendMessage(Teiravon.Messages.NoTarget);
            }
        }

        [Usage("Telepathy")]
        [Description("Allows you to telepathically speak to other shapeshifters")]
        private static void Telepathy_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Telepathy))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else
            {
                foreach (NetState state in NetState.Instances)
                {
                    TeiravonMobile m_Target = state.Mobile as TeiravonMobile;

                    if (m_Target != null)
                        if (m_Target.IsShapeshifter() || m_Target.IsForester() || ((m_Target.AccessLevel > AccessLevel.Player) && m_Target.HearAll))
                            m_Target.SendMessage(m_Player.SpeechHue, "Telepathy: [{0}]: {1}", m_Player.Name, e.ArgString);
                }
            }
        }

        [Usage("Disarm")]
        [Description("Allows you to disarm your opponent")]
        private static void Disarm_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Disarm))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Disarm))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
                m_Player.Target = new DisarmTarget();

        }

        private class DisarmTarget : Target
        {
            public DisarmTarget()
                : base(1, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m_Target = (Mobile)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Player == m_Target)
                    {
                        m_Player.SendMessage("You refuse to disarm yourself.");
                        return;
                    }

                    if (m_Target == null)
                        return;

                    Item toDisarm = (Item)m_Target.Weapon;

                    if (toDisarm == null)
                        m_Player.SendLocalizedMessage(1060849); // Your target is already unarmed!

                    else if (!toDisarm.Movable || m_Target.Backpack == null)
                        m_Player.SendLocalizedMessage(1004001); // You cannot disarm your opponent.

                    else if (toDisarm != null)
                    {
                        from.SendLocalizedMessage(1060092); // You disarm their weapon!
                        m_Target.SendLocalizedMessage(1060093); // Your weapon has been disarmed!

                        m_Target.PlaySound(0x3B9);
                        m_Target.FixedParticles(0x37BE, 232, 25, 9948, EffectLayer.LeftHand);
                        m_Target.Backpack.DropItem(toDisarm);

                        BaseWeapon.BlockEquip(m_Target, TimeSpan.FromSeconds(10.0));

                        // Reuse timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.Disarm;
                        m_TimerHelper.Duration = TAVCoolDown.Disarm;
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.Disarm, true);
                    }
                }
                else
                    from.SendMessage(Teiravon.Messages.NoTarget);
            }
        }

        [Usage("Berserk")]
        [Description("Berserker feat that grants bonuses to attacks when a berserker activates it")]
        public static void Berserk_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.BerserkerRage))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.BerserkerRage))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
            {
                int str = 5;// Changed by Valik 50 + (m_Player.PlayerLevel * 5);
                int resadj = -(10 + (m_Player.PlayerLevel));

                m_Player.Emote("*You see {0} appear to be going berserk!*", m_Player.Name);

                StatMod BerserkStatMod = new StatMod(StatType.Str, (int)m_Player.Serial + " Berserk Str", str, TimeSpan.FromSeconds(30 + (m_Player.PlayerLevel * 2)));
                m_Player.AddStatMod(BerserkStatMod);

                ExpireTimer timer = (ExpireTimer)m_Table[m_Player];

                if (timer != null)
                    timer.DoExpire();

                TimeSpan duration = TimeSpan.FromSeconds(30 + (m_Player.PlayerLevel * 2));

                ResistanceMod[] mods = new ResistanceMod[5]
					{
						new ResistanceMod( ResistanceType.Fire, resadj ),
						new ResistanceMod( ResistanceType.Poison, resadj ),
						new ResistanceMod( ResistanceType.Cold, resadj ),
						new ResistanceMod( ResistanceType.Physical, resadj ),
						new ResistanceMod( ResistanceType.Energy, resadj )
					};

                timer = new ExpireTimer(m_Player, mods, duration);
                timer.Start();

                m_Table[m_Player] = timer;

                for (int i = 0; i < mods.Length; ++i)
                    m_Player.AddResistanceMod(mods[i]);

                // Stat timer
                TimerHelper m_StatHelper = new TimerHelper((int)m_Player.Serial);
                m_StatHelper.MessageOn = true;
                m_StatHelper.Duration = TimeSpan.FromSeconds(30 + (m_Player.PlayerLevel * 2));
                m_StatHelper.Message = "You beging to calm down...";
                m_StatHelper.Start();

                // Reuse timer
                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.BerserkerRage;
                m_TimerHelper.Duration = TAVCoolDown.Encounter;
                m_TimerHelper.Start();

                m_Player.SetActiveFeats(TeiravonMobile.Feats.BerserkerRage, true);
            }
        }

        private static Hashtable m_Table = new Hashtable();

        private class ExpireTimer : Timer
        {
            private Mobile m_Mobile;
            private ResistanceMod[] m_Mods;

            public ExpireTimer(Mobile m, ResistanceMod[] mods, TimeSpan delay)
                : base(delay)
            {
                m_Mobile = m;
                m_Mods = mods;
            }

            public void DoExpire()
            {
                for (int i = 0; i < m_Mods.Length; ++i)
                    m_Mobile.RemoveResistanceMod(m_Mods[i]);

                Stop();
                m_Table.Remove(m_Mobile);
            }

            protected override void OnTick()
            {
                DoExpire();
            }
        }


        [Usage("Forage")]
        [Description("Forage feat")]
        private static void Forage_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.AnimalHusbandry))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.AnimalHusbandry))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
                m_Player.Target = new ForageTarget();
        }

        private class ForageTarget : Target
        {
            public ForageTarget()
                : base(3, true, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is StaticTarget)
                {
                    StaticTarget m_Target = (StaticTarget)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    // Reuse timer
                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.AnimalHusbandry;
                    m_TimerHelper.Duration = TAVCoolDown.Foraging;
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.AnimalHusbandry, true);

                    if (m_Target.ItemID == 3291 || m_Target.ItemID == 3294 || m_Target.ItemID == 3287 || m_Target.ItemID == 3283 ||
                    m_Target.ItemID == 3278 || m_Target.ItemID == 3281 || m_Target.ItemID == 3303 || m_Target.ItemID == 3300 ||
                    m_Target.ItemID == 3297)
                    {
                        if (Utility.Random(1, 100) < Utility.Random(m_Player.PlayerLevel * 2, m_Player.PlayerLevel * 5))
                        {
                            Food m_Fruit = null;

                            switch (Utility.Random(1, 5))
                            {
                                case 1:
                                    m_Fruit = new Grapes(Utility.Random(1, 3));
                                    break;

                                case 2:
                                    m_Fruit = new Peach(Utility.Random(1, 3));
                                    break;

                                case 3:
                                    m_Fruit = new Apple(Utility.Random(1, 3));
                                    break;

                                case 4:
                                    m_Fruit = new Pear(Utility.Random(1, 3));
                                    break;

                                case 5:
                                    m_Fruit = new Dates(Utility.Random(1, 3));
                                    break;
                            }

                            from.SendMessage("You've found something!");
                            m_Fruit.MoveToWorld(m_Target.Location, m_Player.Map);

                        }
                        else
                            from.SendMessage("You fail to find anything...");
                    }
                    else
                        from.SendMessage(Teiravon.Messages.NoTarget);
                }
                else
                    from.SendMessage(Teiravon.Messages.NoTarget);
            }
        }


        [Usage("LayOnHands")]
        [Description("Activate divine healing ability")]
        private static void LayOnHands_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.LayOnHands))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.LayOnHands))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
                m_Player.Target = new LayOnHandsTarget();
        }

        private class LayOnHandsTarget : Target
        {
            public LayOnHandsTarget()
                : base(1, false, TargetFlags.Beneficial)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m_Target = (Mobile)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Target.Serial == m_Player.Serial)
                    {
                        from.SendMessage(Teiravon.Messages.NoTarget);
                        return;
                    }

                    m_Target.Hits += m_Player.PlayerLevel * 9;

                    if (m_Target.Hits >= m_Target.HitsMax)
                        m_Target.Hits = m_Target.HitsMax;

                    m_Target.Emote("*Is healed by {0}'s touch!*", m_Player.Name);

                    // Reuse timer
                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.LayOnHands;
                    m_TimerHelper.Duration = TAVCoolDown.LayOnHands;
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.LayOnHands, true);
                }
                else
                    from.SendMessage(Teiravon.Messages.NoTarget);
            }
        }

        [Usage("DetectEvil")]
        [Description("Detect evil around the player")]
        private static void DetectEvil_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.DetectEvil))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else
            {
                IPooledEnumerable eable = m_Player.GetMobilesInRange(20);
                ArrayList baddies = new ArrayList();
                ArrayList deadies = new ArrayList();
                foreach (Mobile m in eable)
                {
                    if (m == m_Player)
                        continue;

                    if (!m.Alive)
                        continue;
                    if (m.AccessLevel > m_Player.AccessLevel)
                        continue;
                    if (m is BaseCreature)
                    {
                        //bool baddy = false;
                        SlayerEntry m_Entry = SlayerGroup.GetEntryByName(SlayerName.Silver);
                        BaseCreature b = m as BaseCreature;
                        if(m_Entry.Slays(b))
                        {
                            deadies.Add(m);
                        }
                        else if (m.Karma < -8000)
                        {
                            baddies.Add(m);
                        }
                    }
                    else if (m is TeiravonMobile)
                    {
                        TeiravonMobile tav = m as TeiravonMobile;
                        if (tav.PlayerRace == TeiravonMobile.Race.Undead)
                        {
                            deadies.Add(m);
                        }
                        else if (tav.Karma < - 8000)
                        {
                            baddies.Add(m);
                        }
                    }
                }
                eable.Free();
                bool found = false;
                if (baddies.Count > 0)
                {
                    found = true;
                    m_Player.SendMessage("You can sense an unkind presence nearby.");
                    for (int i = 0; i < baddies.Count; i++)
                    {
                        Mobile guy = (Mobile)baddies[i];
                        //m_Player.SendMessage(guy.ToString());
                        //guy.Say("Nyahaha");
                        m_Player.Send(new TargetEffect(guy, 0x36FE, 1, 20, 1109, 2));
                    }
                }
                if (deadies.Count > 0)
                {
                    found = true;
                    m_Player.SendMessage("The rotting presence of undeath is near.");
                    for (int i = 0; i < deadies.Count; i++)
                    {
                        Mobile guy = (Mobile)deadies[i];
                        //m_Player.SendMessage(guy.ToString());
                        //guy.Say("Boo");
                        m_Player.Send(new TargetEffect(guy, 0x3728, 1, 20, 1100, 2));
                        m_Player.Send(new MovingEffect(m_Player,guy,0x37C4,1,20,true,false,0,2));
                    }
                }

                if (!found)
                    m_Player.SendMessage("You sense no malice.");
            }
        }

        private class DetectEvilTarget : Target
        {
            public DetectEvilTarget()
                : base(10, false, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is TeiravonMobile)
                {
                    TeiravonMobile m_Player = (TeiravonMobile)from;
                    TeiravonMobile m_Target = (TeiravonMobile)o;

                    if (!m_Target.IsEvil())
                        m_Player.SendMessage("You sense nothing wicked about this person.");

                    else if (m_Target.IsEvil())
                        if (m_Target.PlayerLevel > m_Player.PlayerLevel)
                        {
                            m_Player.SendMessage("You can determine nothing about this person.");
                        }
                        else
                        {
                            string align = "evil";
                            bool undead = false;

                            if (m_Player.PlayerLevel >= 13)
                            {
                                if (m_Target.IsLawfulEvil())
                                    align = "lawful evil";
                                else if (m_Target.IsNeutralEvil())
                                    align = "neutral evil";
                                else
                                    align = "chaotic evil";
                            }

                            if (m_Player.PlayerLevel >= 17)
                                if (m_Target.PlayerRace == TeiravonMobile.Race.Undead || m_Target.PlayerRace == TeiravonMobile.Race.Vampire)
                                    undead = true;

                            m_Player.SendMessage("You can tell this person is {0}.", align);

                            if (undead)
                                m_Player.SendMessage("You've got the feeling this being is unholy...");
                        }

                }
                else
                    from.SendMessage("You can't target that.");
            }
        }

        [Usage("ArcaneTransfer")]
        [Description("Mage Arcane Transfer feat")]
        private static void ArcaneTransfer_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.ArcaneTransfer))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.ArcaneTransfer))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else if (m_Player.Mana >= m_Player.MaxMana)
                m_Player.SendMessage("You're already fully energized.");
            else
                m_Player.Target = new ArcaneTransferTarget();
        }

        private class ArcaneTransferTarget : Target
        {
            public ArcaneTransferTarget()
                : base(1, false, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseWand)
                {
                    BaseWand m_Wand = (BaseWand)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Wand.Charges <= 0)
                        m_Player.SendMessage("The wand is out of charges.");
                    else
                    {
                        m_Player.Mana += (m_Wand.Charges / 4) * m_Player.PlayerLevel;
                        m_Wand.Charges = 0;

                        if (m_Player.Mana > m_Player.MaxMana)
                            m_Player.Mana = m_Player.MaxMana;

                        m_Player.SendMessage("You feel a rush of energy surge through you...");

                        // Reuse timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.ArcaneTransfer;
                        m_TimerHelper.Duration = TAVCoolDown.ArcaneTransfer;
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.ArcaneTransfer, true);
                    }
                }
                else
                {
                    from.SendMessage("That is not a wand.");
                }
            }
        }

        [Usage("Bite")]
        [Description("Shapeshifter Bite feat")]
        private static void Bite_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Bite))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.Frozen)
                m_Player.SendMessage("You cannot seem to move.");
            else if (!m_Player.Alive)
                m_Player.SendMessage("Dead things don't bite, except zombies, you aren't a zombie though.");
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Bite))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
            else
                m_Player.Target = new BiteTarget();
        }

        private class BiteTarget : Target
        {
            public BiteTarget()
                : base(2, false, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {

                if (o is Mobile && ((Mobile)o).Alive && o != from)
                {
                    
                    TeiravonMobile m_Player = (TeiravonMobile)from;
                    Mobile m_Target = (Mobile)o;
                    if (m_Player.CanBeHarmful(m_Target) && m_Target.CanBeDamaged())
                    {
                        m_Player.RevealingAction();
                        m_Player.SendMessage("You tear off a chunk of living flesh");

                        int damage = 15 + m_Player.PlayerLevel + (m_Player.Str / 6);
                        int pen = m_Player.Str / 8;

                        int resPhys = AOS.Scale(m_Target.PhysicalResistance, pen);

                        int totalDamage = AOS.Scale(damage, (100 - resPhys));


                        m_Player.DoHarmful(m_Target);
                        m_Target.Damage(totalDamage, m_Player);

                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.Bite;
                        m_TimerHelper.Duration = TAVCoolDown.AtWill;
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.Bite, true);

                        if (!MortalStrike.IsWounded( m_Player))
                            m_Player.Heal(totalDamage);

                    }
                    else
                    {
                        m_Player.SendMessage("You are unable to harm them.");
                    }

                }
                else if (o is Corpse)
                {
                    Corpse m_Corpse = (Corpse)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Corpse.Bitten)
                        m_Player.SendMessage("This corpse has already been fed off of...");
                    else
                    {
                        m_Player.SendMessage("You bite off a chunk of flesh, and begin to feel restored...");

                        if (m_Player.Hunger + 5 <= 20)
                            m_Player.Hunger += 5;
                        else
                            m_Player.Hunger = 20;

                        m_Player.Stam += (((m_Player.PlayerLevel * 10) / 2) + Utility.Random(15));

                        if (m_Player.Stam > m_Player.StamMax)
                            m_Player.Stam = m_Player.StamMax;

                        m_Player.Hits += (((m_Player.PlayerLevel * 10) / 2) + Utility.Random(15));

                        if (m_Player.Hits > m_Player.HitsMax)
                            m_Player.Hits = m_Player.HitsMax;

                        m_Corpse.Bitten = true;

                        // Reuse Timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.Bite;
                        m_TimerHelper.Duration = TAVCoolDown.AtWill;
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.Bite, true);
                    }
                }
                else if (o is BattleNet)
                {
                    BattleNet m_net = (BattleNet)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                        m_Player.SendMessage("You begin chewing on the netting...");
                        m_net.Carve(m_Player, m_net);
                        // Reuse Timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.Bite;
                        m_TimerHelper.Duration = TimeSpan.FromSeconds(15.0);
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.Bite, true);
                }
                else
                    from.SendMessage("You can't bite that.");
            }
        }

        [Usage("Stealth")]
        [Description("Activates assassin Advanced Stealth feat")]
        private static void Stealth_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.AdvancedStealth))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);

            else if (m_Player.Hidden)
            {
                m_Player.SendMessage("You have been revealed.");
                m_Player.Hidden = false;

            }
            else
            {
                m_Player.SendMessage("You vanish from view...");
                m_Player.Hidden = true;
            }
        }

        [Usage("CriticalStrike")]
        [Description("Activate critical strike feat")]
        private static void CriticalStrike_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.CriticalStrike))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
                return;
            }
            if (m_Player.Frozen)
            {
                m_Player.SendMessage("You cannot seem to move.");
                return;
            }
            if (m_Player.GetActiveFeats(TeiravonMobile.Feats.CriticalStrike))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
                return;
            }
            if (m_Player.Weapon is BaseRanged)
            {
                BaseRanged m_Weapon = m_Player.Weapon as BaseRanged;
                Container pack = m_Player.Backpack;
                Type ammoType = m_Weapon.AmmoType;

                bool isArrow = (ammoType == typeof(Arrow));
                bool isBolt = (ammoType == typeof(Bolt));
                bool isKnown = (isArrow || isBolt);

                if (pack == null || !pack.ConsumeTotal(ammoType, 1))
                {
                    if (isArrow)
                        m_Player.SendMessage("You do not have any arrows");
                    else if (isBolt)
                        m_Player.SendMessage("You do not have any bolts");
                    return;
                }
                e.Mobile.Target = new CriticalStrikeRanged(m_Player.Weapon);
            }
            else
                e.Mobile.Target = new CriticalStrikeMelee();

        }
        private class CriticalStrikeRanged : Target
        {
            private IWeapon m_used;
            public CriticalStrikeRanged(IWeapon weap)
                : base(12, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
                m_used = weap;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (from.Weapon != m_used)
                    return;
                if (o is Mobile)
                {
                    Mobile m_Target = (Mobile)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Target == from)
                        m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoTargetSelf);
                    else
                    {
                        BaseRanged m_Weapon = m_Player.Weapon as BaseRanged;
                        Container pack = m_Player.Backpack;
                        Type ammoType = m_Weapon.AmmoType;
                        Item ammo = m_Weapon.Ammo;

                        // Reuse Timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.CriticalStrike;
                        m_TimerHelper.Duration = TimeSpan.FromSeconds(30.0);
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.CriticalStrike, true);

                        m_Player.Combatant = m_Target;
                        m_Target.Warmode = true;
                        m_Player.Warmode = true;

                        /*Random 0 to 5.
                         *Vital Modifier 0-Fail 1-Liver 2-Kidney 3-Head 4-Neck 5-Heart
                         *Skill Modifier = Skill in anatomy / 5.0
                         *Base Max Damage + Vital Modifier * Skill Modifier = new damage
                         */

                        m_Weapon.PlaySwingAnimation(m_Player);
                        m_Player.PlaySound(m_Weapon.GetHitAttackSound(m_Player, m_Target));
                        m_Player.MovingEffect(m_Target, m_Weapon.EffectID, 18, 1, false, false);
                        ammo.Consume(1);

                        if (!BaseRanged.CheckAim(m_Player, m_Target))
                            return;

                        int vitalmod = 0,
                            skillmod = (int)(m_Player.Skills.Anatomy.Base / 15.0),
                            damage = 0;

                        int min, basedamage;
                        m_Player.Weapon.GetStatusDamage(m_Player, out min, out basedamage);
                        vitalmod = Utility.Random(6);

                        if (vitalmod > 5)
                            vitalmod = 5;

                        switch (vitalmod)
                        {
                            case 0:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    from.Emote("*Misses hitting a vital point*");
                                    break;
                                }
                            case 1:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the liver*");
                                    break;
                                }
                            case 2:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the kidney*");
                                    break;
                                }
                            case 3:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the head*");
                                    break;
                                }
                            case 4:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the neck*");
                                    break;
                                }
                            case 5:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the heart*");
                                    break;
                                }
                        }

                        int phys, fire, cold, pois, nrgy;
                        m_Weapon.GetDamageTypes(m_Player, out phys, out fire, out cold, out pois, out nrgy);

                        AOS.Damage(m_Target, m_Player, damage, phys, fire, cold, pois, nrgy, false, 50);

                        m_Weapon.PlayHurtAnimation(m_Target);
                        m_Target.PlaySound(m_Weapon.GetHitDefendSound(m_Player, m_Target));
                    }
                }
                else
                {
                    from.SendMessage("That's not a living being...");
                }
            }
        }
        private class CriticalStrikeMelee : Target
        {
            public CriticalStrikeMelee()
                : base(2, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m_Target = (Mobile)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Target.Serial == from.Serial)
                        m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoTargetSelf);
                    else
                    {
                        BaseWeapon m_Weapon = m_Player.Weapon as BaseWeapon;

                        // Reuse Timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.CriticalStrike;
                        m_TimerHelper.Duration = TimeSpan.FromSeconds(30.0);
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.CriticalStrike, true);

                        m_Player.Combatant = m_Target;
                        m_Target.Warmode = true;
                        m_Player.Warmode = true;

                        /*Random 0 to 5.
                         *Vital Modifier 0-Fail 1-Liver 2-Kidney 3-Head 4-Neck 5-Heart
                         *Skill Modifier = Skill in anatomy / 5.0
                         *Base Max Damage + Vital Modifier * Skill Modifier = new damage
                         */

                        m_Weapon.PlaySwingAnimation(m_Player);
                        m_Player.PlaySound(m_Weapon.GetHitAttackSound(m_Player, m_Target));

                        int vitalmod = 0,
                            skillmod = (int)(m_Player.Skills.Anatomy.Base / 15.0),
                            damage = 0;

                        int min, basedamage;
                        m_Player.Weapon.GetStatusDamage(m_Player, out min, out basedamage);
                        vitalmod = Utility.Random(6);

                        if (vitalmod > 5)
                            vitalmod = 5;


                        switch (vitalmod)
                        {
                            case 0:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    from.Emote("*Misses hitting a vital point*");
                                    break;
                                }
                            case 1:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the liver*");
                                    break;
                                }
                            case 2:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the kidney*");
                                    break;
                                }
                            case 3:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the head*");
                                    break;
                                }
                            case 4:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the neck*");
                                    break;
                                }
                            case 5:
                                {
                                    damage = basedamage + vitalmod * skillmod;
                                    m_Target.Emote("*Gets struck in the heart*");
                                    break;
                                }
                        }
                       
                        int phys, fire, cold, pois, nrgy;
                        m_Weapon.GetDamageTypes(m_Player, out phys, out fire, out cold, out pois, out nrgy);

                        AOS.Damage(m_Target, m_Player, damage, phys, fire, cold, pois, nrgy, false, 50);
                        m_Weapon.PlayHurtAnimation(m_Target);
                        m_Target.PlaySound(m_Weapon.GetHitDefendSound(m_Player, m_Target));
                    }
                }
                else
                {
                    from.SendMessage("That's not a living being...");
                }
            }
        }

        [Usage("KaiShot")]
        [Description("Activate kai shot feat")]
        private static void KaiShot_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.KaiShot))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
                return;
            }
            if (m_Player.GetActiveFeats(TeiravonMobile.Feats.KaiShot))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
                return;
            }
            if (m_Player.Mana < (int)(m_Player.MaxMana / 2.0))
            {
                m_Player.SendMessage("You do not have enough ki to do this!");
                return;
            }

            e.Mobile.Target = new KaiShot();

        }
        private class KaiShot : Target
        {
            public KaiShot()
                : base(12, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    Mobile m_Target = (Mobile)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Target.Serial == from.Serial)
                        m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoTargetSelf);
                    else
                    {
                        m_Player.Combatant = m_Target;
                        m_Target.Warmode = true;

                        // Reuse Timer
                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.KaiShot;
                        m_TimerHelper.Duration = TAVCoolDown.KaiShot;
                        m_TimerHelper.Start();

                        m_Player.SetActiveFeats(TeiravonMobile.Feats.KaiShot, true);

                        m_Player.Emote("*harnesses mana into a throwable form*");

                        double damage = 20 + (m_Player.PlayerLevel * 4);

                        m_Player.MovingParticles(m_Target, 0x36E4, 5, 0, false, true, 3006, 4006, 0);
                        m_Player.PlaySound(0x1E5);

                        SpellHelper.Damage(TimeSpan.Zero, m_Target, m_Player, damage, 0, 0, 0, 0, 100);

                        m_Player.Mana -= (int)(m_Player.MaxMana / 2.0);
                    }
                }
                else
                {
                    from.SendMessage("That's not a living being...");
                }
            }
        }

        [Usage("Bluudlust")]
        [Description("Activate bluud lust feat")]
        private static void Bluudlust_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Bluudlust))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
                return;
            }
            if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Bluudlust))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
                return;
            }

            // Reuse Timer
            TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
            m_TimerHelper.DoFeat = true;
            m_TimerHelper.Feat = TeiravonMobile.Feats.Bluudlust;
            m_TimerHelper.Duration = TAVCoolDown.Bluudlust;
            m_TimerHelper.Start();

            m_Player.SetActiveFeats(TeiravonMobile.Feats.Bluudlust, true);

            SpellHelper.AddStatBonus(m_Player, m_Player, StatType.Str, 6 * m_Player.PlayerLevel, TimeSpan.FromSeconds(30.0));
            SpellHelper.AddStatBonus(m_Player, m_Player, StatType.Dex, 4 * m_Player.PlayerLevel, TimeSpan.FromSeconds(30.0));

            m_Player.Emote("*roars in a blood frenzy*");
        }

        [Usage("Cripple")]
        [Description("Activate crippling blow feat")]
        private static void Cripple_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.CripplingBlow))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
                return;
            }
            if (!(m_Player.Weapon is BaseBashing) && !(m_Player.Weapon is BaseStaff))
            {
                m_Player.SendMessage("You must use a blunt weapon for this feat");
                return;
            }
            if (m_Player.Frozen)
            {
                m_Player.SendMessage("You cannot seem to move.");
                return;

            }
            if (m_Player.GetActiveFeats(TeiravonMobile.Feats.CripplingBlow))
            {
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);
                return;
            }

            e.Mobile.Target = new CripplingBlow();
        }
        private class CripplingBlow : Target
        {
            public CripplingBlow()
                : base(2, false, TargetFlags.Harmful)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                TeiravonMobile m_Player = (TeiravonMobile)from;

                if (o is Mobile)
                {
                    // Reuse Timer
                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.DoFeat = true;
                    m_TimerHelper.Feat = TeiravonMobile.Feats.CripplingBlow;
                    m_TimerHelper.Duration = TAVCoolDown.CripplingBlow;
                    m_TimerHelper.Start();

                    m_Player.SetActiveFeats(TeiravonMobile.Feats.CripplingBlow, true);
                }

                if (o is BaseCreature)
                {
                    BaseCreature m_Target = (BaseCreature)o;

                    // create reuse timer

                    m_Player.Combatant = m_Target;
                    m_Target.Warmode = true;
                    m_Player.Warmode = true;

                    m_Player.SendMessage("You deliver a crippling blow!");

                    m_Target.Freeze(TimeSpan.FromSeconds(6.0));
                    m_Target.Stam = 0;

                    m_Target.FixedEffect(0x376A, 9, 32);
                    m_Target.PlaySound(0x204);
                }
                else if (o is TeiravonMobile)
                {
                    TeiravonMobile m_Target = (TeiravonMobile)o;

                    m_Player.Combatant = m_Target;
                    m_Target.Warmode = true;
                    m_Player.Warmode = true;

                    // create reuse timer

                    m_Player.SendMessage("You deliver a crippling blow!");
                    m_Target.SendMessage("The attack has temporarily crippled you!");

                    m_Target.Freeze(TimeSpan.FromSeconds(3.0));
                    m_Target.Stam = (int)(m_Target.Stam * .5);

                    m_Target.FixedEffect(0x376A, 9, 32);
                    m_Target.PlaySound(0x204);
                }
                else
                {
                    from.SendMessage("That's not a living being...");
                }
            }
        }

        [Usage("GlobeOfDarkness")]
        [Description("Activate Drow feat Globe of Darkness")]
        private static void GlobeOfDarkness_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            m_Player.SendMessage("Disabled.");
            return;

            /*
            if ( !m_Player.HasFeat( TeiravonMobile.Feats.GlobeOfDarkness ) )
                m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility );
            else if ( m_Player.GetActiveFeats( TeiravonMobile.Feats.GlobeOfDarkness ) )
                m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon );
            else
            {
                GlobeOfDarkness theglobe = new GlobeOfDarkness( 10 + m_Player.PlayerLevel / 2 );
                theglobe.MoveToWorld( m_Player.Location, m_Player.Map );

                // Reuse Timer
                TimerHelper m_TimerHelper = new TimerHelper( (int)m_Player.Serial );
                m_TimerHelper.DoFeat = true;
                m_TimerHelper.Feat = TeiravonMobile.Feats.GlobeOfDarkness;
                m_TimerHelper.Duration = TAVCoolDown.GlobeOfDarkness;
                m_TimerHelper.Start();

                m_Player.SetActiveFeats( TeiravonMobile.Feats.GlobeOfDarkness, true );
            }
            */
        }
        [Usage("Flurry")]
        [Description("Rain a flurry of blows on your foe")]
        private static void Flurry_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.Flurry))
            {
                m_Player.SendMessage("You can't do that!");
                return;
            }
            if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Flurry) && !m_Player.BurningHands)
            {
                m_Player.SendMessage("You are not yet ready.");
                return;
            }
            if (m_Player.GetActiveFeats(TeiravonMobile.Feats.Flurry) && m_Player.BurningHands)
            {
                m_Player.SendMessage("You let your concentration lapse.");
                m_Player.BurningHands = false;
                return;
            }
            if (!m_Player.Alive)
            {
                m_Player.SendMessage("You can't do that!");
                return;
            }
            else if (m_Player.Stam <= 20)
            {
                m_Player.SendMessage("You're too exausted.");
                return;
            }
            else
            {
                m_Player.Stam -= 5;
                m_Player.SendMessage("You prepare your attack..");
                m_Player.BurningHands = true;
            }
        }


        [Usage("CloakOfDarkness")]
        [Description("Toggle Drow feat Cloak of Darkness")]
        private static void CloakOfDarkness_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.CloakOfDarkness))
            {
                m_Player.SendMessage("You can't do that!");
                return;
            }
            if (m_Player.m_CloakOfDarkness)
            {
                m_Player.m_CloakOfDarkness = false;
                m_Player.SendMessage("The darkness leaves your side.");
                return;
            }
            else
            {
                m_Player.m_CloakOfDarkness = true;
                m_Player.SendMessage("The darkness surrounds you.");
                return;
            }
        }

        [Usage("DrainUndead")]
        [Description("Drain mana from undead creatures")]
        private static void DrainUndead_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.UndeadBond))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else if (m_Player.GetActiveFeats(TeiravonMobile.Feats.UndeadBond))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon);

            else
                m_Player.Target = new DrainUndeadTarget();
        }

        private class DrainUndeadTarget : Target
        {

            public DrainUndeadTarget()
                : base(6, false, TargetFlags.None)
            {
                CheckLOS = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseCreature)
                {
                    BaseCreature m_Creature = (BaseCreature)o;
                    TeiravonMobile m_Player = (TeiravonMobile)from;

                    if (m_Player.Alive && m_Creature.Alive && (m_Creature.IsUndeadNPC() || m_Creature.GetType() == typeof(ChaosDaemon)))
                    {

                        bool success = false;

                        if (Utility.Random(100) < m_Player.PlayerLevel * 3 + 40)
                            success = true;

                        int draintimes = 5 + Utility.Random(5);

                        m_Player.Paralyze(TimeSpan.FromSeconds(draintimes + 2));
                        m_Player.RevealingAction();

                        Timer m_Timer = new NecroDrainTimer(m_Player, m_Creature, 1, draintimes, success, DateTime.Now + TimeSpan.FromSeconds(2.0));
                        m_Timer.Start();

                        TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                        m_TimerHelper.DoFeat = true;
                        m_TimerHelper.Feat = TeiravonMobile.Feats.UndeadBond;
                        m_TimerHelper.Duration = success ? TimeSpan.FromMinutes(2.0) : TimeSpan.FromSeconds(20.0);
                        m_TimerHelper.Start();
                        m_Player.SetActiveFeats(TeiravonMobile.Feats.UndeadBond, true);

                    }
                    else
                        from.SendMessage("You fail to establish a link to that creature.");
                }
                else
                    from.SendMessage("You cannot use it on that.");
            }
        }

        private class NecroDrainTimer : Timer
        {
            TeiravonMobile m_Drainer;
            BaseCreature m_Target;
            int ManaDrain;
            int HpDrain;
            int RunCount;
            int MaxCount;
            bool Success;

            public NecroDrainTimer(TeiravonMobile from, BaseCreature target, int runcount, int maxcount, bool success, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Drainer = from;
                m_Target = target;
                RunCount = runcount;
                MaxCount = maxcount;
                Success = success;
            }

            protected override void OnTick()
            {
                if (m_Drainer == null)
                    return;

                if (m_Target == null)
                {
                    m_Drainer.Paralyzed = false;
                    return;
                }

                if (Success)
                {

                    if (m_Drainer.Alive && m_Target.Alive)
                    {
                        if (RunCount == 1)
                        {
                            m_Drainer.SendMessage("You start to replenish some of your hitpoints and mana from the creature!");
                            m_Target.PublicOverheadMessage(MessageType.Regular, 2117, false, "*You see dark lifeforce being drained from " + m_Target.Name);
                        }

                        RunCount++;

                        int manadrainamount = (int)(m_Drainer.PlayerLevel / 2) + (int)(m_Drainer.Skills.Necromancy.Base / 20) + (int)(m_Drainer.Skills.SpiritSpeak.Base / 20) + Utility.Random(5);
                        int hpdrainamount = (int)(manadrainamount / 2) + Utility.Random(4);

                        m_Drainer.Hits += hpdrainamount;
                        m_Drainer.Mana += manadrainamount;

                        if (m_Drainer.HitsMax < m_Drainer.Hits)
                            m_Drainer.Hits = m_Drainer.HitsMax;

                        if (m_Drainer.ManaMax < m_Drainer.Mana)
                            m_Drainer.Mana = m_Drainer.ManaMax;

                        m_Target.Damage((int)(hpdrainamount), m_Drainer);
                        m_Target.AggressiveAction(m_Drainer, false);
                        m_Drainer.RevealingAction();

                        if (RunCount == MaxCount)
                        {
                            m_Target.Paralyzed = false;

                            if (m_Target.Controled && m_Target.ControlMaster == m_Drainer)
                            {
                                m_Target.Controled = false;
                                m_Target.ControlMaster = null;
                                m_Target.AggressiveAction(m_Drainer, false);
                            }
                        }

                        else if (RunCount < MaxCount)
                        {
                            if (m_Target.Alive)
                            {
                                Timer m_Timer = new NecroDrainTimer(m_Drainer, m_Target, RunCount, MaxCount, true, DateTime.Now + TimeSpan.FromSeconds(1.0));
                                m_Timer.Start();
                            }
                            else
                            {
                                m_Drainer.SendMessage("The link to the creature has been broken!");
                                m_Drainer.Paralyzed = false;
                            }
                        }
                    }
                    else
                        m_Drainer.SendMessage("The link to the creature has been broken!");
                }
                else
                {
                    m_Drainer.Paralyzed = false;
                    m_Drainer.SendMessage("Your attempt to replenish your lifeforce has failed and you have angered the creature!");
                    m_Target.AggressiveAction(m_Drainer, false);
                }
            }
        }

        private class HolyAuraTimer : Timer
        {
            TeiravonMobile m_Cleric;
            int RunCount;

            public HolyAuraTimer(TeiravonMobile from, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Cleric = from;

                Priority = TimerPriority.OneSecond;
            }
            public static void DoHeal(TeiravonMobile Caster, Mobile m)
            {
                {

                    int toHeal;
                    toHeal = (2 + (Caster.PlayerLevel / 3)) * (Caster.Fame / 6000);
                    toHeal += Utility.Random(1, 5);


                    if (Caster != m)
                    {
                        TeiravonMobile TAV = (TeiravonMobile)Caster;

                        if (TAV.HasFeat(TeiravonMobile.Feats.HealersOath))
                            toHeal +=(int)(toHeal * 1.5);

                        if ((m.Hits + toHeal) > m.HitsMax)
                        {
                            toHeal = m.HitsMax - m.Hits;
                        }
                        int exp = toHeal * (1 + (TAV.PlayerLevel / 2));
                        if (TAV.HasFeat(TeiravonMobile.Feats.HealersOath))
                            exp *= 2;
                        if (exp > 0 && Misc.Titles.AwardExp(TAV, exp)) { TAV.SendMessage("You have gained {0} exp", exp); }
                    }
                    m.Heal(toHeal);

                    m.FixedParticles(0x376A, 9, 32, 5005, EffectLayer.Waist);
                }
            }
            protected override void OnTick()
            {
                if (m_Cleric == null)
                    return;

                Engines.PartySystem.Party p = Engines.PartySystem.Party.Get(m_Cleric);
                m_Cleric.FixedParticles(0x3709, 1, 30, 9963, 2220, 3, EffectLayer.CenterFeet);


                //Determines aura range
                int auraRange = 2 + (int)(m_Cleric.Fame / 1000);
                if (auraRange > 12)
                    auraRange = 12;

                if (m_Cleric.DisarmShotReady || m_Cleric.GetActiveFeats(TeiravonMobile.Feats.DivineAura))
                {
                    ArrayList HolyAura = new ArrayList();

                    foreach (Mobile m in m_Cleric.GetMobilesInRange(auraRange))
                    {
                        if (m == null)
                            continue;

                        if (m_Cleric.Combatant == m || m.Combatant == m_Cleric)
                            continue;

                        if (m.AccessLevel > m_Cleric.AccessLevel)
                            continue;

                        if (m.Hits == m.HitsMax)
                            continue;
                        if (m.YellowHealthbar)
                            continue;

                        if (m is BaseCreature)
                        {
                            if (!m.Alive || m == null)
                                continue;

                            BaseCreature c = (BaseCreature)m;

                            if (c.Aggressors.Contains(m_Cleric))
                                continue;

                            if (c.Controled || c.Summoned)
                            {
                                if (c.ControlMaster == m_Cleric || c.SummonMaster == m_Cleric)
                                    HolyAura.Add(m);

                                if (p != null && (p.Contains(c.ControlMaster) || p.Contains(c.SummonMaster)))
                                    HolyAura.Add(m);
                            }
                        }
                        else if (m is TeiravonMobile)
                        {
                            if (!m.Alive || m == null)
                                continue;

                            TeiravonMobile m_Target = (TeiravonMobile)m;

                            if (p != null && !p.Contains(m))
                                continue;

                            HolyAura.Add(m);
                        }
                    }

                    foreach (Mobile m in HolyAura)
                    {
                        if (m != null)
                        {
                            //								int damage = (int)(PlayerLevel / 5);
                            int damage = 3 + (m_Cleric.PlayerLevel / 3) * (m_Cleric.Fame / 2000);

                            //								if ( Utility.Random( 2 ) == 0 )
                            //									Mana -= 1;
                            int percent = (int)(m_Cleric.MaxMana *.02);
                            if (m_Cleric.Mana <= percent)
                            {
                                m_Cleric.DisarmShotReady = false;
                                m_Cleric.SendMessage("You run out of spiritual energy to sustain the holy aura!");

                                break;
                            }

                            m_Cleric.Mana -= percent;
                            DoHeal(m_Cleric, m);
                        }
                    }

                    Timer m_Timer = new HolyAuraTimer(m_Cleric, DateTime.Now + TimeSpan.FromSeconds(5.0));
                    m_Timer.Start();
                }
            }
        }




        private class DarkAuraTimer : Timer
        {
            TeiravonMobile m_Cleric;
            int RunCount;

            public DarkAuraTimer(TeiravonMobile from, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Cleric = from;

                Priority = TimerPriority.OneSecond;
            }

            private bool IsAggressor(Mobile from, Mobile m)
            {
                foreach (AggressorInfo info in from.Aggressors)
                {
                    if (m == info.Attacker && !info.Expired)
                        return true;
                }

                return false;
            }

            private bool IsAggressed(Mobile from, Mobile m)
            {
                foreach (AggressorInfo info in from.Aggressed)
                {
                    if (m == info.Defender && !info.Expired)
                        return true;
                }

                return false;
            }

            protected override void OnTick()
            {
                if (m_Cleric == null)
                    return;

                Engines.PartySystem.Party p = Engines.PartySystem.Party.Get(m_Cleric);
                m_Cleric.FixedParticles(0x3728, 1, 13, 9912, 1150, 7, EffectLayer.CenterFeet);

                //Determines aura range
                int auraRange = 2 + (int)(m_Cleric.PlayerLevel / 4);
                if (auraRange > 6)
                    auraRange = 6;

                if (m_Cleric.StunShotReady)
                {
                    ArrayList DarkAura = new ArrayList();

                    foreach (Mobile m in m_Cleric.GetMobilesInRange(auraRange))
                    {
                        if (m == null)
                            continue;

                        if (m.AccessLevel > m_Cleric.AccessLevel)
                            continue;

                        if (p != null && p.Contains(m))
                            continue;

                        if (!IsAggressor(m_Cleric, m) && !IsAggressed(m_Cleric, m))
                            continue;

                        if (m is BaseCreature)
                        {
                            if (!m.Alive || m == null)
                                continue;

                            BaseCreature c = (BaseCreature)m;


                            if (c.Controled || c.Summoned)
                            {
                                if (c.ControlMaster == m_Cleric || c.SummonMaster == m_Cleric)
                                    continue;

                                if (p != null && (p.Contains(c.ControlMaster) || p.Contains(c.SummonMaster)))
                                    continue;

                                DarkAura.Add(m);
                            }
                            else
                                DarkAura.Add(m);
                        }

                        else if (m is TeiravonMobile)
                        {
                            if (m == m_Cleric || !m.Alive || m == null)
                                continue;

                            if (m.Combatant != m_Cleric && m_Cleric.Combatant != m)
                                continue;

                            DarkAura.Add(m);
                        }
                    }

                    foreach (Mobile m in DarkAura)
                    {
                        if (m != null)
                        {
                            //								int damage = (int)(PlayerLevel / 5);
                            int damage = (2 + (m_Cleric.PlayerLevel / 4)) * (m_Cleric.Fame / 2000);

                            //								if ( Utility.Random( 2 ) == 0 )
                            //									Mana -= 1;
                            int percent = (int)(m_Cleric.MaxMana * .02);
                            if (m_Cleric.Mana <= percent)
                            {
                                m_Cleric.StunShotReady = false;
                                m_Cleric.SendMessage("You run out of spiritual energy to sustain the unholy aura!");

                                break;
                            }

                            m_Cleric.Mana -= percent;
                            if (damage >= 30)
                                m.Damage(Utility.RandomMinMax(30 - 5, 30 + 5), m_Cleric);
                            else if (damage >= 5)
                                m.Damage(Utility.RandomMinMax(damage - 4, damage + 4), m_Cleric);
                            else
                                m.Damage(1, m_Cleric);
                        }
                    }

                    if (m_Cleric.Mana > 0)
                    {
                        Timer m_Timer = new DarkAuraTimer(m_Cleric, DateTime.Now + TimeSpan.FromSeconds(4.0));
                        m_Timer.Start();
                    }
                    else
                    {
                        m_Cleric.StunShotReady = false;
                        m_Cleric.SendMessage("You run out of spiritual energy to sustain the unholy aura!");

                    }
                }
            }
        }

        [Usage("NecroSpeak")]
        [Description("Makes undead say <args>")]
        private static void NecroSpeak_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            if (!m_Player.HasFeat(TeiravonMobile.Feats.UndeadBond))
                m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility);
            else
                m_Player.Target = new NecroSpeakTarget(e.Arguments);
        }

        private class NecroSpeakTarget : Target
        {
            private static string m_Say;

            public NecroSpeakTarget(string[] StrToSay)
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;

                for (int i = 0; i < StrToSay.Length; ++i)
                {
                    m_Say += StrToSay[i];
                    m_Say += " ";
                }
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseCreature)
                {
                    BaseCreature m_Creature = (BaseCreature)o;

                    if ((m_Creature.IsUndeadNPC() || (m_Creature.GetType() == typeof(BoneDemon) || (m_Creature.GetType() == typeof(ChaosDaemon))) && m_Creature.Alive))
                        m_Creature.PublicOverheadMessage(MessageType.Regular, 2117, false, m_Say);
                    else
                        from.SendMessage("You fail to manipulate that creature.");
                }
                else
                    from.SendMessage("You cannot speak through that.");

                m_Say = null;
            }
        }

        private static void RegionCoords_OnCommand(CommandEventArgs e)
        {
            TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

            FileStream m_RUORegions = new FileStream("D:\\RunUO\\sphereregions.xml", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_RUOWriter = new StreamWriter(m_RUORegions);

            FileStream m_SRegions = new FileStream("D:\\RunUO\\TAVSphereMap.scp", FileMode.Open, FileAccess.Read);
            StreamReader m_SReader = new StreamReader(m_SRegions);

            m_RUOWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            m_RUOWriter.WriteLine("<ServerRegions>");
            m_RUOWriter.WriteLine("\t<Facet name=\"Felucca\">");

            bool go = true;
            string data;
            string temp;
            string temp2;
            string temp3;
            int start;

            int x1;
            int y1;
            int x2;
            int y2;

            int c1;
            int c2;
            int c3;

            while ((data = m_SReader.ReadLine()) != null)
            {
                // [AREADEF name]
                try
                {
                    //m_Player.SendMessage( data.Length.ToString() );
                    //m_Player.SendMessage( data );
                    //m_Player.SendMessage( data.IndexOf( "]" ).ToString() );

                    temp = data.Substring(9, data.IndexOf("]") - 9);

                    m_RUOWriter.WriteLine("\t\t<region priority=\"25\" name=\"" + temp + "\">");

                    // TITLE=title
                    data = m_SReader.ReadLine();

                    // GROUP=Group
                    data = m_SReader.ReadLine();

                    // P=x, y		<go location="(2300,990,0)" />
                    data = m_SReader.ReadLine();

                    start = data.IndexOf("=") + 1;
                    temp3 = data.Substring(start);

                    // RECT=192,120,1248,780
                    // 1234 567890123
                    // x1 = 6

                    while ((data = m_SReader.ReadLine()) != null)
                    {
                        temp = data.Substring(0, 4);

                        if (temp == "RECT")
                        {
                            temp = data.Substring(5);
                            c1 = temp.IndexOf(",");

                            x1 = Convert.ToInt32(temp.Substring(0, c1));

                            temp = temp.Substring(c1 + 1);
                            c2 = temp.IndexOf(",");

                            y1 = Convert.ToInt32(temp.Substring(0, c2));

                            temp = temp.Substring(c2 + 1);
                            c3 = temp.IndexOf(",");

                            x2 = Convert.ToInt32(temp.Substring(0, c3));

                            temp = temp.Substring(c3 + 1);

                            y2 = Convert.ToInt32(temp);


                            m_Player.SendMessage("{0} {1} {2} {3}", x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString());


                            c1 = x2 - x1;
                            c2 = y2 - y1;

                            temp2 = "			<rect x=\"" + x1.ToString() + "\" y=\"" + y1.ToString() + "\" width=\"" + c1.ToString() + "\" height=\"" + c2.ToString() + "\" />";

                            m_RUOWriter.WriteLine(temp2);

                        }
                        else
                        {
                            m_RUOWriter.WriteLine("			<go location=\"(" + temp3 + ")\" />");
                            go = false;
                            break;
                        }
                    }

                    // FLAGS
                    if (go)
                        data = m_SReader.ReadLine();

                    // EVENTS
                    data = m_SReader.ReadLine();

                    // Blank Line
                    data = m_SReader.ReadLine();

                    m_RUOWriter.WriteLine("		</region>");

                }
                catch
                {
                    m_Player.SendMessage("Crashed it.");
                }
            }

            m_RUOWriter.WriteLine(" \t</Facet>");
            m_RUOWriter.Write("</ServerRegions>");
            m_RUOWriter.Flush();

            m_RUORegions.Close();
            m_SRegions.Close();
        }
    }
}