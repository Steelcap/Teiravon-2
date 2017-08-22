using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Engines.PartySystem;
using Server.Mobiles;

namespace Server.Spells.Fourth
{
	public class ArchProtectionSpell : Spell
	{
        private static Hashtable m_Registry = new Hashtable();
        public static Hashtable Registry { get { return m_Registry; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Arch Protection", "Vas Uus Sanct",
				SpellCircle.Fourth,
				Core.AOS ? 239 : 215,
				9011,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public ArchProtectionSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
        public override int CastDelayCircleScalar { get { return 8;}}
        public override void OnCast()
        {
            if (CheckSequence())
            {
                if (Caster is TeiravonMobile)
                {
                    TeiravonMobile tm_caster = (TeiravonMobile)Caster;
                    if (tm_caster.IsMage())
                    {
                        tm_caster.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
                        tm_caster.PlaySound(0x1F7);

                        int bonus = tm_caster.PlayerLevel * 5;
                        /*
                       double spelllength = (tm_caster.Skills[SkillName.Magery].Value / 10) + 5.0;
    tm_caster.AddSkillMod( new TimedSkillMod( SkillName.MagicResist, true, bonus, TimeSpan.FromMinutes( spelllength ) ) );
    new InternalTimer( Caster ).Start();
    */
                        tm_caster.VirtualArmorMod = (int)GetAosDamage(bonus, 2, 10);
                        string shield = tm_caster.VirtualArmorMod.ToString();
                        tm_caster.LocalOverheadMessage(MessageType.Emote, 90, true, shield);

                    }
                    else
                    {


                        ArrayList targets = new ArrayList();

                        Map map = Caster.Map;

                        if (map != null)
                        {
                            IPooledEnumerable eable = map.GetMobilesInRange(Caster.Location, 4);

                            foreach (Mobile m in eable)
                            {
                                if (Caster.CanBeBeneficial(m, false))
                                    targets.Add(m);
                            }

                            eable.Free();
                        }

                        Party party = Party.Get(Caster);

                        for (int i = 0; i < targets.Count; ++i)
                        {
                            Mobile m = (Mobile)targets[i];

                            if (m == Caster || (party != null && party.Contains(m)))
                            {
                                if (!Spells.Fourth.ArchProtectionSpell.Registry.ContainsKey(m))
                                {
                                    Caster.DoBeneficial(m);
                                    m.PlaySound(0x1E9);
                                    m.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);
                                    Spells.Fourth.ArchProtectionSpell.Registry.Add(m, 1);
                                }
                            }
                        }
                    }
                }
            }
        }


		private class InternalTimer : Timer
		{

			Mobile m_Owner;
			
			public InternalTimer( Mobile caster ) : base( TimeSpan.FromMinutes( 0 ) )
			{
				m_Owner = caster;
				double time = 5.1 + caster.Skills[SkillName.Magery].Value /10;
				Delay = TimeSpan.FromMinutes( time );
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Owner.EndAction( typeof( ArchProtectionSpell ) );
				m_Owner.AddSkillMod(new DefaultSkillMod(SkillName.MagicResist, true,0));
			}
		}
	}
}
