using System;
using Server.Targeting;
using Server.Network;
using Server.Teiravon;
using Server.Mobiles;
using System.Collections;

namespace Server.Spells.Fourth
{
	public class LightningSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Lightning", "Por Ort Grav",
				SpellCircle.Fourth,
				239,
				9021,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public LightningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }

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
                    ArrayList targets = new ArrayList();
 
                    if (tav.IsForester() && tav.HasFeat(TeiravonMobile.Feats.TempestsWrath))
                    {
                        m.BoltEffect(0);
                        damage = GetNewAosDamage(28, 2, 6, Caster.Player && m.Player);
                        int splash = (int)damage / 3;

                        
                        Engines.PartySystem.Party party = Engines.PartySystem.Party.Get(Caster);
                        
                        try
                        {
                            foreach (Mobile x in m.GetMobilesInRange(3))
                            {
                                if (party != null && party.Contains(x))
                                    continue;

                                if (x.AccessLevel > Caster.AccessLevel)
                                    continue;

                                if (x is BaseCreature)
                                {
                                    if (((BaseCreature)x).ControlMaster == Caster)
                                        continue;

                                    if (((BaseCreature)x).ControlMaster != null)
                                    {
                                        Mobile v = ((BaseCreature)x).ControlMaster;
                                        if (party != null && party.Contains(v))
                                            continue;
                                    }
                                    else
                                    {
                                        Effects.SendMovingEffect(m, x, 0x379F, 16, 5, true, false);
                                        AOS.Damage(x, Caster, splash, 0, 0, 0, 0, 100);
                                    }
                                }

                                else if (x != Caster && Caster.HarmfulCheck(x) && x != m)
                                {
                                    Effects.SendMovingEffect(m, x, 0x379F, 16, 5, true, false);
                                    AOS.Damage(x, Caster, splash, 0, 0, 0, 0, 100);

                                }
                            }
                        }
                        catch { }
                        SpellHelper.Damage(this, m, damage, 0, 0, 0, 0, 100);
                        
                        return;
                    }
                   // Effects.SendLocationEffect(m.Location, m.Map, 0x3789, 18);
                }

				if ( Core.AOS )
				{
					damage = GetNewAosDamage( 22, 1, 4, Caster.Player && m.Player );
				}
				else
				{
					damage = Utility.Random( 12, 9 );

					if ( CheckResisted( m ) )
					{
						damage *= 0.75;

						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}

					damage *= GetDamageScalar( m );
				}

				m.BoltEffect( 0 );

				SpellHelper.Damage( this, m, damage, 0, 0, 0, 0, 100 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private LightningSpell m_Owner;

			public InternalTarget( LightningSpell owner ) : base( 12, false, TargetFlags.Harmful )
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