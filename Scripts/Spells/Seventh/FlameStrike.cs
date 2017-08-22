using System;
using Server.Targeting;
using Server.Network;
using Server.Teiravon;
using Server.Mobiles;
using System.Collections;

namespace Server.Spells.Seventh
{
	public class FlameStrikeSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Flame Strike", "Kal Vas Flam",
				SpellCircle.Seventh,
				245,
				9042,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public FlameStrikeSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
            bool IsPyro = false;

            if (Caster is TeiravonMobile)
            {
                TeiravonMobile tav = Caster as TeiravonMobile;
                if (tav.IsPyromancer())
                    IsPyro = true;
            }
            if (IsPyro)
                Caster.Target = new InternalPyroTarget(this);
            else
			    Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				double damage;


                if (Caster is TeiravonMobile)
                {
                    TeiravonMobile tav = Caster as TeiravonMobile;

                    if (tav.IsAeromancer())
                    {
                        damage = GetNewAosDamage(48, 1, 5, Caster.Player && m.Player);
                        //m.FixedParticles(0x37CC, 10, 202, 5052, EffectLayer.LeftFoot);
                        m.FixedParticles(0x37C, 6, 15, 5052, 2693, 1, EffectLayer.LeftFoot);
                        m.PlaySound(0x5CE);

                        SpellHelper.Damage(this, m, damage, 0, 0, 50, 0, 50);
                        new Gust(tav, m).MoveToWorld(m.Location, m.Map);
                        FinishSequence();
                        return;
                    }
                    if (tav.IsGeomancer())
                    {
                        damage = GetNewAosDamage(48, 1, 5, Caster.Player && m.Player);
                        Caster.MovingParticles(m, 0x11B6, 5, 0, true, false, 0x1E00, 0x1E00, 0x13B);
                        m.PlaySound(0x21E);

                        SpellHelper.GeoDamage(this, m, damage);
                        m.Freeze(TimeSpan.FromSeconds(2.5));
                        FinishSequence();
                        return;
                    }

                    if (tav.IsAquamancer())
                    {
                        damage = GetNewAosDamage(45, 1, 25, Caster.Player && m.Player);
                        m.FixedParticles(0x3789, 30, 20, 5052, EffectLayer.CenterFeet);
                        m.PlaySound(0x026);

                        SpellHelper.Damage(this, m, damage, 0, 0, 100, 0, 0);
                        FinishSequence();
                        return;
                    }

                    if (tav.IsPyromancer())
                    {
                        damage = GetNewAosDamage(45, 1, 5, Caster.Player && m.Player);
                        m.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                        m.PlaySound(0x208);
                        SpellHelper.Damage(this, m, damage, 0, 100, 0, 0, 0);
                        int burntimes = 2 + Utility.Random(3);
                        Timer m_Timer = new IgniteTimer(tav, m, 1, burntimes, DateTime.Now + TimeSpan.FromSeconds(1.5));
                        m_Timer.Start();
                        FinishSequence();
                        return;
                    }

                }



				if ( Core.AOS )
				{
					damage = GetNewAosDamage( 48, 1, 5, Caster.Player && m.Player );
				}
				else
				{
					damage = Utility.Random( 27, 22 );

					if ( CheckResisted( m ) )
					{
						damage *= 0.6;

						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}

					damage *= GetDamageScalar( m );
				}

				m.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
				m.PlaySound( 0x208 );

				SpellHelper.Damage( this, m, damage, 0, 100, 0, 0, 0 );
			}

			FinishSequence();
		}


        public void PyroTarget(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                if (p is Item)
                    p = ((Item)p).GetWorldLocation();

                ArrayList targets = new ArrayList();

                Map map = Caster.Map;

                bool playerVsPlayer = false;
                Point3D loc = new Point3D(p);
                if (map != null)
                {
                    Effects.SendLocationEffect(loc, map, 0x3709, 30);

                    AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.3), TimeSpan.FromSeconds(0.6), 0.4, map, loc, 0x36CB, 16, 0, 0, 2, 0, true, false, false);
                    try
                    {
                        IPooledEnumerable eable = map.GetMobilesInRange(new Point3D(p), 2);

                        foreach (Mobile m in eable)
                        {
                            if (Core.AOS && m == Caster)
                                continue;
                            if (!map.LineOfSight(m, loc))
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
                    catch { }
                }

                double damage = GetNewAosDamage(45, 1, 5, Caster.Player && playerVsPlayer);
                if (targets.Count > 0)
                {

                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Mobile m = (Mobile)targets[i];

                        double toDeal = damage;
                        if (m.GetDistanceToSqrt(loc) > 0)
                            toDeal = damage / m.GetDistanceToSqrt(loc);
                        if (!Core.AOS && CheckResisted(m))
                        {
                            toDeal *= 0.5;

                            m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                        }

                        Caster.DoHarmful(m);
                        
                        SpellHelper.Damage(this, m, toDeal, 0, 100, 0, 0, 0); 
                    }
                }
                }

            FinishSequence();
        }

		private class InternalTarget : Target
		{
			private FlameStrikeSpell m_Owner;

			public InternalTarget( FlameStrikeSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

        private class InternalPyroTarget : Target
        {
            private FlameStrikeSpell m_Owner;

            public InternalPyroTarget(FlameStrikeSpell owner)
                : base(12, true, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                IPoint3D p = o as IPoint3D;

                if (p != null)
                    m_Owner.PyroTarget(p);

            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
	}
}