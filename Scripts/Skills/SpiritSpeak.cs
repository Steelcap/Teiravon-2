using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	class SpiritSpeak
	{
        public static void GivePrayer(TeiravonMobile from)
        {
            TeiravonMobile Caster = from;
            int min, max;
            min = 1 + (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.25);
            max = min + Caster.PlayerLevel / 2;
            if (Caster.CheckSkill(SkillName.SpiritSpeak, 0.0, 120.0))
            {
                Caster.Mana += Utility.RandomMinMax(min, max);
                Caster.PublicOverheadMessage(MessageType.Emote, 0x3B2, true, "*Bows their head*");
                Caster.PlaySound(0x103);
                Caster.FixedParticles(0x375A, 1, 15, 9501, 391, 4, EffectLayer.Head);
                Caster.Freeze(TimeSpan.FromSeconds(1));
                return;
            }
            else
            {
                Caster.SendMessage("You are unable to clear your mind for prayer.");
                return;
            }

        }

		public static void Initialize()
		{
			SkillInfo.Table[32].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
            if (m is TeiravonMobile)
            {
                TeiravonMobile tav = m as TeiravonMobile;

                if (tav.IsPaladin() || tav.IsCleric() || tav.IsDarkCleric())
                {
                    GivePrayer(tav);
                    return TimeSpan.FromSeconds(10.0);
                }
                else
                {
                    Spell spell = new SpiritSpeakSpell(m);

                    spell.Cast();

                    if (spell.IsCasting)
                        return TimeSpan.FromSeconds(5.0);

                    return TimeSpan.Zero;
                }
            }

			else
			{
				Spell spell = new SpiritSpeakSpell( m );

				spell.Cast();

				if ( spell.IsCasting )
					return TimeSpan.FromSeconds( 5.0 );

				return TimeSpan.Zero;
			}

			m.RevealingAction();

			if ( m.CheckSkill( SkillName.SpiritSpeak, 0, 100 ) )
			{	
				if ( !m.CanHearGhosts )
				{
					Timer t = new SpiritSpeakTimer( m );
					double secs = m.Skills[SkillName.SpiritSpeak].Base / 50;
					secs *= 90;
					if ( secs < 15 )
						secs = 15;

					t.Delay = TimeSpan.FromSeconds( secs );//15seconds to 3 minutes
					t.Start();
					m.CanHearGhosts = true;
				}

				m.PlaySound( 0x24A );
				m.SendLocalizedMessage( 502444 );//You contact the neitherworld.
			}
			else
			{
				m.SendLocalizedMessage( 502443 );//You fail to contact the neitherworld.
				m.CanHearGhosts = false;
			}

			return TimeSpan.FromSeconds( 1.0 );
		}

		private class SpiritSpeakTimer : Timer
		{
			private Mobile m_Owner;
			public SpiritSpeakTimer( Mobile m ) : base( TimeSpan.FromMinutes( 2.0 ) )
			{
				m_Owner = m;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Owner.CanHearGhosts = false;
				m_Owner.SendLocalizedMessage( 502445 );//You feel your contact with the neitherworld fading.
			}
		}

		private class SpiritSpeakSpell : Spell
		{
			private static SpellInfo m_Info = new SpellInfo( "Spirit Speak", "", SpellCircle.Second, 269 );

			public override bool BlockedByHorrificBeast{ get{ return false; } }

			public SpiritSpeakSpell( Mobile caster ) : base( caster, null, m_Info )
			{
			}

			public override bool ClearHandsOnCast{ get{ return true; } }

			public override TimeSpan GetCastDelay()
			{
				return TimeSpan.FromSeconds( 1.0 );
			}

			public override int GetMana()
			{
				return 0;
			}

			public override void OnCasterHurt()
			{
				if ( IsCasting )
					Disturb( DisturbType.Hurt, false, true );
			}

			public override bool ConsumeReagents()
			{
				return true;
			}

			public override bool CheckFizzle()
			{
				return true;
			}

			public override bool CheckNextSpellTime{ get{ return false; } }

			public override void OnDisturb( DisturbType type, bool message )
			{
				Caster.NextSkillTime = DateTime.Now;

				base.OnDisturb( type, message );
			}

			/*public override bool CheckDisturb( DisturbType type, bool checkFirst, bool resistable )
			{
				if ( type == DisturbType.EquipRequest || type == DisturbType.UseRequest )
					return false;

				return true;
			}*/

			public override void SayMantra()
			{
				// Anh Mi Sah Ko
				Caster.PublicOverheadMessage( MessageType.Regular, 0x3B2, 1062074, "", false );
				Caster.PlaySound( 0x24A );
			}

			public override void OnCast()
			{
                int max, min, mana, number;
				Corpse toChannel = null;

				foreach ( Item item in Caster.GetItemsInRange( 3 ) )
				{
					if ( item is Corpse && !((Corpse)item).Channeled )
					{
						toChannel = (Corpse)item;
						break;
					}
				}

				if ( toChannel != null )
				{
					min = 1 + (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.25);
					max = min + 4;
					mana = 0;
					number = 1061287; // You channel energy from a nearby corpse to heal your wounds.
				}
				else
				{
					min = 1 + (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.25);
					max = min + 4;
					mana = 10;
					number = 1061286; // You channel your own spiritual energy to heal your wounds.
				}

				if ( Caster.Mana < mana )
				{
					Caster.SendLocalizedMessage( 1061285 ); // You lack the mana required to use this skill.
				}
				else
				{
					Caster.CheckSkill( SkillName.SpiritSpeak, 0.0, 120.0 );

                    if (Utility.RandomDouble() > (Caster.Skills[SkillName.SpiritSpeak].Value / 100.0))
                    {
                        Caster.SendLocalizedMessage(502443); // You fail your attempt at contacting the netherworld.
                    }
                    else if (MortalStrike.IsWounded(Caster))
                        Caster.SendMessage("You cannot heal yourself.");
                    else
                    {
                        if (toChannel != null)
                        {
                            toChannel.Channeled = true;
                            toChannel.Hue = 0x835;
                        }

                        Caster.Mana -= mana;
                        Caster.SendLocalizedMessage(number);

                        if (min > max)
                            min = max;

                        Caster.Hits += Utility.RandomMinMax(min, max);

                        Caster.FixedParticles(0x375A, 1, 15, 9501, 2100, 4, EffectLayer.Waist);
                    }
				}

				FinishSequence();
			}
		}
	}
}