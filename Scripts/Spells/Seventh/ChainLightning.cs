using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Seventh
{
	public class ChainLightningSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Chain Lightning", "Vas Ort Grav",
				SpellCircle.Seventh,
				209,
				9022,
				false,
				Reagent.BlackPearl,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public ChainLightningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                if (p is Item)
                    p = ((Item)p).GetWorldLocation();

                ArrayList targets = new ArrayList();

                Map map = Caster.Map;

                bool playerVsPlayer = false;

                if (map != null)
                {
                    IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 2);

                    foreach (Mobile m in eable)
                    {
                        if (Core.AOS && m == Caster)
                            continue;

                        if (SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false))
                        {
                            targets.Add(m);

                            if (m.Player)
                                playerVsPlayer = true;
                        }
                    }

                    eable.Free();
                }
                if (targets.Count == 0)
                {
                    FinishSequence();
                    return;
                }

                double damage = 0;

                if (Caster is TeiravonMobile)
                {
                    TeiravonMobile tav = Caster as TeiravonMobile;

                    if (tav.IsForester() && tav.HasFeat(TeiravonMobile.Feats.TempestsWrath))
                    {
                        damage = GetNewAosDamage(48, 1, 5, Caster.Player && playerVsPlayer);
                        Mobile targ = targets[0] as Mobile;
                        Hashtable litup = new Hashtable();
                        Timer m_Chain = new ChainBounce(Caster, litup, targ, damage, this);
                        Caster.DoHarmful(targ);
                        m_Chain.Start();
                        FinishSequence();
                        return;
                    }
                    else
                    {
                        if (Core.AOS)
                            damage = GetNewAosDamage(48, 1, 5, Caster.Player && playerVsPlayer);
                        else
                            damage = Utility.Random(27, 22);

                        if (targets.Count > 0)
                        {
                            if (Core.AOS && targets.Count > 1)
                                damage = (damage * 2) / targets.Count;
                            else if (!Core.AOS)
                                damage /= targets.Count;

                            for (int i = 0; i < targets.Count; ++i)
                            {
                                Mobile m = (Mobile)targets[i];

                                double toDeal = damage;

                                if (!Core.AOS && CheckResisted(m))
                                {
                                    toDeal *= 0.5;

                                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                                }

                                Caster.DoHarmful(m);
                                SpellHelper.Damage(this, m, toDeal, 0, 0, 0, 0, 100);

                                m.BoltEffect(0);
                            }
                        }
                    }
                }
                else
                {
                    if (Core.AOS)
                        damage = GetNewAosDamage(48, 1, 5, Caster.Player && playerVsPlayer);
                    else
                        damage = Utility.Random(27, 22);

                    if (targets.Count > 0)
                    {
                        if (Core.AOS && targets.Count > 1)
                            damage = (damage * 2) / targets.Count;
                        else if (!Core.AOS)
                            damage /= targets.Count;

                        for (int i = 0; i < targets.Count; ++i)
                        {
                            Mobile m = (Mobile)targets[i];

                            double toDeal = damage;

                            if (!Core.AOS && CheckResisted(m))
                            {
                                toDeal *= 0.5;

                                m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                            }

                            Caster.DoHarmful(m);
                            SpellHelper.Damage(this, m, toDeal, 0, 0, 0, 0, 100);

                            m.BoltEffect(0);
                        }
                    }
                }
            }
			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private ChainLightningSpell m_Owner;

			public InternalTarget( ChainLightningSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

        private class ChainBounce : Timer
        {
            Mobile m_Caster;
            Hashtable m_bounced; 
            Mobile m_Target; 
            double m_Damage; 
            Spell m_Spell;
            IPoint3D m_Loc;

            public ChainBounce(Mobile caster, Hashtable bounced, Mobile targ, double damage, Spell spell)
                : base(TimeSpan.FromSeconds(0.15))
            {
                 m_Caster = caster;
                 m_bounced = bounced;
                 m_Target = targ;
                 m_Damage = damage;
                 m_Spell = spell;
                 m_Loc = targ.Location;
            }

            protected override void OnTick()
            {
                try
                {
                    if (m_Target != null)
                    {
                        m_Target.BoltEffect(0);
                        m_Caster.DoHarmful(m_Target);
                        SpellHelper.Damage(m_Spell, m_Target, m_Damage, 0, 0, 0, 0, 100);
                        m_bounced.Add(m_Target, m_Target);

                        ArrayList targets = new ArrayList();
                        IPooledEnumerable eable = m_Target.Map.GetMobilesInRange(m_Target.Location, 2);

                        foreach (Mobile m in eable)
                        {
                            if (m == null)
                                continue;
                            if (m == m_Caster || m_bounced.Contains(m))
                                continue;

                            if (SpellHelper.ValidIndirectTarget(m_Caster, m) && m_Caster.CanBeHarmful(m, false))
                                targets.Add(m);
                        }

                        eable.Free();

                        if (targets.Count > 0 && m_Damage > 10)
                        {
                            if (targets[0] != null)
                            {
                                Mobile targ = targets[0] as Mobile;
                                //Entity dummy = new Entity(Serial.Zero,(Point3D)m_Loc,m_Caster.Map);
                                //Effects.SendMovingEffect(dummy, targ, 0x3818, 16, 5, false, false);
                                Timer m_Chain = new ChainBounce(m_Caster, m_bounced, targ, m_Damage * .75, m_Spell);
                                m_Chain.Start();
                            }
                        }
                        else
                        {
                            m_bounced.Clear();
                        }
                    }
                }
                catch { m_bounced.Clear(); }
            }
        }
	}
}