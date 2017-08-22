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

    public class IgniteTarget : Target
    {

        public IgniteTarget()
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

                if (m_Player.Alive && m_Creature.Alive )
                {

                    int burntimes = 2 + Utility.Random(5);

                    m_Player.RevealingAction();

                    Timer m_Timer = new IgniteTimer(m_Player, m_Creature, 1, burntimes, DateTime.Now + TimeSpan.FromSeconds(2.0));
                    m_Timer.Start();

                    TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);
                    m_TimerHelper.Start();

                }
            }
        }
    }

    public class IgniteTimer : Timer
    {
        TeiravonMobile m_Caster;
        Mobile m_Target;
        int HpDrain;
        int RunCount;
        int MaxCount;

        public IgniteTimer(TeiravonMobile from, Mobile target, int runcount, int maxcount, DateTime end)
            : base(end - DateTime.Now)
        {
            m_Caster = from;
            m_Target = target;
            RunCount = runcount;
            MaxCount = maxcount;
        }

        protected override void OnTick()
        {
            if (m_Caster == null)
                return;

                if (m_Caster.Alive && m_Target.Alive)
                {
                    /*
                    if (RunCount == 1)
                    {
                        m_Target.PublicOverheadMessage(MessageType.Regular, 2117, false, "*You see " + m_Target.Name + " catch fire");
                    }
                    */
                    RunCount++;

                    int hpdrainamount = (m_Caster.PlayerLevel*2 - (RunCount * 2)) + Utility.RandomMinMax(-1, 2);

                    if (hpdrainamount <= 1)
                    {
                        m_Target.FixedParticles(0x3735, 1, 30, 9503, EffectLayer.Waist);
                        m_Target.SendMessage("You are extinguished");
                        return;
                    }

                    m_Target.FixedParticles(0x19AB, 10, 5, 5052, EffectLayer.Waist);
                    m_Target.PlaySound(0x208);
                    m_Target.SendLocalizedMessage(503000);
                    AOS.Damage(m_Target,m_Caster,hpdrainamount,0,100,0,0,0 );
                    m_Target.AggressiveAction(m_Caster, false);

                    if (RunCount == MaxCount)
                    {
                        m_Target.FixedParticles(0x3735, 1, 30, 9503, EffectLayer.Waist);
                    }

                    else if (RunCount < MaxCount)
                    {
                        if (m_Target.Alive)
                        {
                            Timer m_Timer = new IgniteTimer(m_Caster, m_Target, RunCount, MaxCount, DateTime.Now + TimeSpan.FromSeconds(1.5));
                            m_Timer.Start();
                        }
                    }
                }
            }
        }
}
