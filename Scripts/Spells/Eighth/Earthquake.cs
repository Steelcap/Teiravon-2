using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;

namespace Server.Spells.Eighth
{
	public class EarthquakeSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Earthquake", "In Vas Por",
				SpellCircle.Eighth,
				233,
				9012,
				false,
				Reagent.Bloodmoss,
				Reagent.Ginseng,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public EarthquakeSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool DelayedDamage{ get{ return !Core.AOS; } }


        public void DoIceNova(int damage, Mobile caster, ArrayList targets)
        {
            AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1), 0.1, caster.Map, caster.Location, 14000, 13, 2457, 4, 12, 2, true, true, true);
            Timer.DelayCall(TimeSpan.FromSeconds(1.5), new TimerStateCallback(IceNova_Callback), new object[] { caster, caster.Location, damage, targets });
        }

        public static void IceNova_Callback(object state)
        {
            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);
            int damage = ((int)states[2]);
            ArrayList targets = ((ArrayList)states[3]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;

            Effects.PlaySound(ag.Location, ag.Map, 0x015);            

            for (int i = 0; i < targets.Count; ++i)
            {
                Mobile m = (Mobile)targets[i];

                AOS.Damage(m, ag, damage, 0, 0, 100, 0, 0);
            }
        }

		public override void OnCast()
		{
            if (SpellHelper.CheckTown(Caster, Caster) && CheckSequence())
            {

                ArrayList targets = new ArrayList();

                Map map = Caster.Map;

                XmlData x = (XmlData)XmlAttach.FindAttachmentOnMobile(Caster, typeof(XmlData), "Cryomancer");
                if (x != null)
                {
                    if (map != null)
                    {
                        foreach (Mobile m in Caster.GetMobilesInRange(1 + (int)(Caster.Skills[SkillName.Magery].Value / 15.0)))
                        {
                            if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false) && (!Core.AOS || Caster.InLOS(m)))
                                targets.Add(m);
                        }
                    }
                    DoIceNova(GetNewAosDamage(30, 1, 12, false), Caster, targets);
                }
                else
                {

                    if (map != null)
                    {
                        foreach (Mobile m in Caster.GetMobilesInRange(1 + (int)(Caster.Skills[SkillName.Magery].Value / 15.0)))
                        {
                            if (Caster != m && SpellHelper.ValidIndirectTarget(Caster, m) && Caster.CanBeHarmful(m, false) && (!Core.AOS || Caster.InLOS(m)))
                                targets.Add(m);
                        }
                    }

                    Caster.PlaySound(0x2F3);

                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Mobile m = (Mobile)targets[i];

                        int damage;

                        if (Core.AOS)
                        {
                            damage = m.Hits / 4;

                            if (damage < 10)
                                damage = 10;
                            else if (damage > 100)
                                damage = 100;

                            damage = GetNewAosDamage(damage, 1, 5, m.Player);
                        }
                        else
                        {
                            damage = (m.Hits * 6) / 10;

                            if (!m.Player && damage < 10)
                                damage = 10;
                            else if (damage > 75)
                                damage = 75;
                        }

                        Caster.DoHarmful(m);
                        SpellHelper.Damage(TimeSpan.Zero, m, Caster, damage, 100, 0, 0, 0, 0);
                    }
                }
            }
			FinishSequence();
		}
	}
}