using System;
using Server.Targeting;
using Server.Network;
using Server.Teiravon;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;

namespace Server.Spells.Third
{
	public class FireballSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Fireball", "Vas Flam",
				SpellCircle.Third,
				203,
				9041,
				Reagent.BlackPearl
			);

		public FireballSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
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
				Mobile source = Caster;


				SpellHelper.Turn( source, m );

				SpellHelper.CheckReflect( (int)this.Circle, ref source, ref m );

				double damage;

                if (Caster is TeiravonMobile)
                {
                    TeiravonMobile tav = Caster as TeiravonMobile;

                    if (tav.IsAeromancer())
                    {
                        damage = GetNewAosDamage(19, 1, 5, Caster.Player && m.Player);
                        source.MovingParticles(m, 0x3728, 12, 0, false, false, 9502, 4019, 0x160);
                        source.PlaySound(0x5C7);

                        SpellHelper.Damage(this, m, damage, 0, 0, 50, 0, 50);
                        new Gust(tav, m).MoveToWorld(m.Location, m.Map);
                        FinishSequence();
                        return;
                    }
                    if (tav.IsGeomancer())
                    {
                        damage = GetNewAosDamage(21, 1, 5, Caster.Player && m.Player);
                        int randRock = Utility.RandomList(0x1364, 0x1365, 0x1366, 0x1366);
                        source.MovingParticles(m, randRock, 12, 0, false, false, 0x1E00, 0x1E00, 0x13B);
                        source.PlaySound(0x5BF);

                        SpellHelper.GeoDamage(this, m, damage);
                        m.Freeze(TimeSpan.FromSeconds(1));
                        FinishSequence();
                        return;
                    }

                    if (tav.IsAquamancer())
                    {
                        XmlData x = (XmlData)XmlAttach.FindAttachmentOnMobile(Caster, typeof(XmlData), "Cryomancer");
                        if (x != null && CheckSequence())
                        {
                            damage = GetNewAosDamage(20, 1, 5, Caster.Player && m.Player);
                            source.MovingParticles(m, 0x3678, 12, 5, false, false, 0x377a, 0x377a, 0x390);
                            source.PlaySound(0x390);

                            SpellHelper.Damage(this, m, damage, 0, 0, 100, 0, 0);
                            FinishSequence();
                            m.Send(SpeedMode.Walk);


                            TavChill chill = XmlAttach.FindAttachmentOnMobile(m, typeof(TavChill), "chill") as TavChill;

                            if (chill != null)
                            {
                                chill.Expiration = TimeSpan.FromSeconds(3);
                                chill.DoChill();
                            }
                            else
                            {
                                TavChill newchill = new TavChill(5);
                                newchill.Name = "chill";
                                XmlAttach.AttachTo(m, newchill);
                            }
                            return;
                        }
                        else
                        {
                            damage = GetNewAosDamage(25, 1, 5, Caster.Player && m.Player);
                            source.MovingParticles(m, 0x34FF, 12, 5, false, false, 0x377a, 0x377a, 0x027);
                            source.PlaySound(0x026);

                            SpellHelper.Damage(this, m, damage, 0, 0, 100, 0, 0);
                            FinishSequence();
                            return;
                        }
                    }

                    if (tav.IsPyromancer())
                    {
                        damage = GetNewAosDamage(5, 1, 5, Caster.Player && m.Player);
                        source.MovingParticles(m, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160);
                        source.PlaySound(Core.AOS ? 0x15E : 0x44B);

                        SpellHelper.Damage(this, m, damage, 0, 100, 0, 0, 0);
                        int burntimes = 2 + Utility.Random(5);
                        Timer m_Timer = new IgniteTimer(tav, m, 1, burntimes, DateTime.Now + TimeSpan.FromSeconds(0.5));
                        m_Timer.Start();
                        FinishSequence();
                        return;
                    }
                }

				if ( Core.AOS )
				{
					damage = GetNewAosDamage( 19, 1, 5, Caster.Player && m.Player );
				}
				else
				{
					damage = Utility.Random( 10, 7 );

					if ( CheckResisted( m ) )
					{
						damage *= 0.75;

						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}

					damage *= GetDamageScalar( m );
				}

				source.MovingParticles( m, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160 );
				source.PlaySound( Core.AOS ? 0x15E : 0x44B );

				SpellHelper.Damage( this, m, damage, 0, 100, 0, 0, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private FireballSpell m_Owner;

			public InternalTarget( FireballSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}