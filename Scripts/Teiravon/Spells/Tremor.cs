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

namespace Server.Spells
{

    public class TremorTarget : Target
    {

        public TremorTarget()
            : base(6, false, TargetFlags.None)
        {
            CheckLOS = true;
        }

        protected override void OnTarget(Mobile from, object o)
        {
            if (o is Mobile)
            {
                Mobile m_Target = (Mobile)o;
                TeiravonMobile m_Player = (TeiravonMobile)from;

                if (m_Player.Alive && m_Target.Alive)
                {

                    int burntimes = 4;

                    m_Player.RevealingAction();

                    Timer m_Timer = new TremorTimer(m_Player, 1, burntimes, DateTime.Now + TimeSpan.FromSeconds(1.0));
                    m_Timer.Start();

                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.Start();

                }
            }
        }
    }

    public class TremorTimer : Timer
    {
        TeiravonMobile t_Caster;
        int Run;
        int Max;

        public TremorTimer(TeiravonMobile from, int RunCount, int MaxCount, DateTime end)
            : base(end - DateTime.Now)
        {
            t_Caster = from;
            Run = RunCount;
            Max = MaxCount;
        }

        protected override void OnTick()
        {
            if (t_Caster == null)
                return;
            if (Run == Max)
                return;

            t_Caster.PlaySound(0x2F3);
            Engines.PartySystem.Party p = Engines.PartySystem.Party.Get(t_Caster);


            //Determines aura range
            int auraRange = 6;

                ArrayList Tremor = new ArrayList();

                foreach (Mobile m in t_Caster.GetMobilesInRange(auraRange))
                {
                    if (m == null)
                        continue;

                    if (m.AccessLevel > t_Caster.AccessLevel)
                        continue;

                    if (p != null && p.Contains(m))
                        continue;

                    if (m is BaseCreature )
                    {
                        if (!m.Alive || m == null)
                            continue;

                        BaseCreature c = (BaseCreature)m;

                        if (c.Controled || c.Summoned)
                        {
                            if (c.ControlMaster == t_Caster || c.SummonMaster == t_Caster)
                                continue;

                            if (p != null && (p.Contains(c.ControlMaster) || p.Contains(c.SummonMaster)))
                                continue;
                            ;
                        }

                        Tremor.Add(m);
                    }
                    else if (m is TeiravonMobile)
                    {
                        if (m == t_Caster || !m.Alive || m == null)
                            continue;

                        TeiravonMobile m_Target = (TeiravonMobile)m;

                        if (m_Target.IsEvil() || m_Target.Karma <= -3000)
                            Tremor.Add(m_Target);
                    }
                }

                foreach (Mobile m in Tremor)
                {
                    if (m != null)
                    {
                        int damage = (int)(t_Caster.PlayerLevel / 3);



                        if (damage >= 3)
                            m.Damage(Utility.RandomMinMax(damage - 2, damage + 2), t_Caster);
                        else
                            m.Damage(1, t_Caster);

                        m.Paralyze(TimeSpan.FromSeconds(0.5));
                    }
                }
                Run++;
                Timer m_Timer = new TremorTimer(t_Caster, Run ,4 , DateTime.Now + TimeSpan.FromSeconds(0.5));
            }
        }
    }
