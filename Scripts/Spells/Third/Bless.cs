using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Teiravon;
using Server.Items;
using Server.Engines.XmlSpawner2;
using System.Collections;

namespace Server.Spells.Third
{
	public class BlessSpell : Spell
	{
        
        public static Hashtable adalia = new Hashtable();
        public static Hashtable valar = new Hashtable();
        public static Hashtable kamalini = new Hashtable();
        public static Hashtable kinarugi = new Hashtable();
        public static Hashtable gruumsh = new Hashtable();
        public static Hashtable saerin = new Hashtable();
        public static Hashtable narindun = new Hashtable();
        public static Hashtable talathas = new Hashtable();
        public static Hashtable lloth = new Hashtable();
        public static Hashtable jareth = new Hashtable();
        public static Hashtable occido = new Hashtable();

		private static SpellInfo m_Info = new SpellInfo(
				"Bless", "Rel Sanct",
				SpellCircle.Third,
				203,
				9061,
				Reagent.Garlic,
				Reagent.MandrakeRoot
			);

		public BlessSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        private BlessHelper m_Timer;

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override int GetMana()
		{
			return 20;
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckBSequence( m ) )
			{
                if (Caster is TeiravonMobile && (((TeiravonMobile)Caster).IsCleric() || ((TeiravonMobile)Caster).IsDarkCleric() || ((TeiravonMobile)Caster).IsStrider() || ((TeiravonMobile)Caster).IsPaladin()) && ((TeiravonMobile)Caster).Faith != TeiravonMobile.Deity.None)
                {
                    TeiravonMobile Cleric = Caster as TeiravonMobile;

                    SpellHelper.Turn(Caster, m);

                    switch (Cleric.Faith)
                    {
                        case TeiravonMobile.Deity.Adalia:
                            {
                                if (!adalia.Contains(m))
                                    adalia.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, adalia);
                                m_Timer.Start();

                                m.FixedParticles(0x375A, 10, 15, 5018, EffectLayer.Waist);
                                m.PlaySound(0x30C);
                                m.PlaySound(0x5CB);
                            }
                            break;
                        case TeiravonMobile.Deity.Cultist:
                            {
                                SpellHelper.AddStatBonus(Caster, m, StatType.Str); SpellHelper.DisableSkillCheck = true;
                                SpellHelper.AddStatBonus(Caster, m, StatType.Dex);
                                SpellHelper.AddStatBonus(Caster, m, StatType.Int); SpellHelper.DisableSkillCheck = false;

                                int mod = SpellHelper.GetOffset(Caster, m, StatType.Str, false);
                                TimeSpan duration = SpellHelper.GetDuration(Caster, m);

                                if (m is TeiravonMobile)
                                {
                                    TeiravonMobile tav = m as TeiravonMobile;
                                    tav.HitsMod = mod;
                                    BlessTimer timer = new BlessTimer(Caster, (TeiravonMobile)m, duration, mod);
                                    timer.Start();
                                }

                                m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                                m.PlaySound(0x1EA);

                                Caster.Damage(Utility.Random(6));
                            }
                            break;
                        case TeiravonMobile.Deity.Gruumsh:
                            {
                                if (!gruumsh.Contains(m))
                                    gruumsh.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, gruumsh);
                                m_Timer.Start();

                                m.FixedParticles(0x375A, 10, 15, 5018, 157, 4, EffectLayer.Waist);
                                BleedAttack.BeginBleed(m, m);
                                m.PlaySound(0x50F);
                                m.PlaySound(0x02B);
                            }
                            break;
                        case TeiravonMobile.Deity.Kamalini:
                            {
                                if (!kamalini.Contains(m))
                                    kamalini.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, kamalini);
                                m_Timer.Start();

                                m.FixedParticles(0x375A, 10, 15, 5018, 157, 4, EffectLayer.Waist);
                                m.PlaySound(0x4B7);
                            }
                            break;
                        case TeiravonMobile.Deity.Kinarugi:
                            {
                                if (!kinarugi.Contains(m))
                                    kinarugi.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, kinarugi);
                                m_Timer.Start();

                                m.FixedParticles(0x36FE, 10, 15, 5018, 2020, 4, EffectLayer.Waist);
                                m.PlaySound(0x09F);
                                m.PlaySound(0x5C4);
                            }
                            break;
                        case TeiravonMobile.Deity.Lloth:
                            {
                                if (m is TeiravonMobile)
                                {
                                    if (!lloth.Contains(m))
                                        lloth.Add(m, DateTime.Now);
                                    if (m_Timer != null)
                                        m_Timer.Stop();
                                    m_Timer = new BlessHelper(m, lloth);
                                    m_Timer.Start();

                                    Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 1, 13, 2869, 1, 5023, 0);
                                    m.PlaySound(0x381);
                                    m.PlaySound(0x4B0);
                                }
                                else
                                {
                                    Caster.SendMessage("You are unable to bless this target.");
                                }
                            }
                            break;
                        case TeiravonMobile.Deity.Narindun:
                            {
                                if (!narindun.Contains(m))
                                    narindun.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, narindun);
                                m_Timer.Start();

                                m.FixedParticles(0x3789, 1, 40, 0x3F,906, 3, EffectLayer.Waist);
                                m.PlaySound(0x029);
                                m.PlaySound(0x5C6);
                            }
                            break;
                        case TeiravonMobile.Deity.Occido:
                            {
                                if (!occido.Contains(m))
                                    occido.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, occido);
                                m_Timer.Start();

                                m.FixedParticles(0x3728, 1, 40, 0x3F, 0, 3, EffectLayer.Waist);
                                m.PlaySound(0x5C8);
                            }
                            break;
                        case TeiravonMobile.Deity.Saerin:
                            {
                                if (!saerin.Contains(m))
                                    saerin.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, saerin);
                                m_Timer.Start();

                                m.FixedParticles(0x36FE, 10, 15, 5018, 2020, 4, EffectLayer.Waist);
                                m.PlaySound(0x100);
                                m.PlaySound(0x5BE);
                            }
                            break;
                        case TeiravonMobile.Deity.Talathas:
                            {
                                if (!talathas.Contains(m))
                                    talathas.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, talathas);
                                m_Timer.Start();

                                m.FixedParticles(0x36FE, 10, 15, 5018, 2020, 4, EffectLayer.Waist);
                                m.PlaySound(0x100);
                                m.PlaySound(0x5C4);
                            }
                            break;
                        case TeiravonMobile.Deity.Valar:
                            {
                                if (!valar.Contains(m))
                                    valar.Add(m, DateTime.Now);
                                if (m_Timer != null)
                                    m_Timer.Stop();
                                m_Timer = new BlessHelper(m, valar);
                                m_Timer.Start();

                                m.FixedParticles(0x36FE, 10, 15, 5018, 2020, 4, EffectLayer.Waist);
                                m.PlaySound(0x100);
                                m.PlaySound(0x5C4);
                            }
                            break;
                        case TeiravonMobile.Deity.Jareth:
                            {
                                if (m is TeiravonMobile)
                                {
                                    TeiravonMobile tav = m as TeiravonMobile;
                                    if (!tav.IsGoblin())
                                    {
                                        Caster.SendMessage("The Goblin King would never bless that!");
                                        return;
                                    }
                                    if (!jareth.Contains(m))
                                        jareth.Add(m, DateTime.Now);
                                    if (m_Timer != null)
                                        m_Timer.Stop();
                                    m_Timer = new BlessHelper(m, jareth);
                                    m_Timer.Start();

                                    m.FixedParticles(0x2D92, 10, 15, 5018, 2829, 4, EffectLayer.Waist);
                                    m.PlaySound(0x547);
                                }
                                else
                                    Caster.SendMessage("The Goblin King would never bless that!");
                            }
                            break;
                    }
                }
                else
                {
                    SpellHelper.AddStatBonus(Caster, m, StatType.Str); SpellHelper.DisableSkillCheck = true;
                    SpellHelper.AddStatBonus(Caster, m, StatType.Dex);
                    SpellHelper.AddStatBonus(Caster, m, StatType.Int); SpellHelper.DisableSkillCheck = false;

                    int mod = SpellHelper.GetOffset(Caster, m, StatType.Str, false);
                    TimeSpan duration = SpellHelper.GetDuration(Caster, m);

                    if (m is TeiravonMobile)
                    {
                        TeiravonMobile tav = m as TeiravonMobile;
                        tav.HitsMod = mod;
                        BlessTimer timer = new BlessTimer(Caster, (TeiravonMobile)m, duration, mod);
                        timer.Start();
                    }

                    m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                    m.PlaySound(0x1EA);
                }
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private BlessSpell m_Owner;

			public InternalTarget( BlessSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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

        private class BlessHelper : Timer
        {
            Mobile m_From;
            Hashtable m_Source;

            public BlessHelper(Mobile from, Hashtable source)
                : base(TimeSpan.FromMinutes(40))
            {
                m_From = from;
                m_Source = source;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (m_Source.Contains(m_From))
                {
                    m_Source.Remove(m_From);

                    m_From.SendMessage("Your blessing has worn off...");
                }
            }
        }

        private class BlessTimer : Timer
        {
            Mobile m_Cleric;
            TeiravonMobile m_Targ;
            int m_Mod;
            

            public BlessTimer(Mobile from, TeiravonMobile targ, TimeSpan duration, int mod)
                : base(duration)
            {
                m_Cleric = from;
                m_Targ = targ;
                m_Mod = mod;
                Priority = TimerPriority.OneSecond;
            }
            protected override void OnTick()
            {
                m_Targ.HitsMod =0;
            }
        }
	}
}