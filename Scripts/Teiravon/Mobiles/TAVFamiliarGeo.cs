using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Spells;
using Server.Engines;

namespace Server.Mobiles
{
    [CorpseName("a rumbler corpse")]
    public class TAVGeoFamiliar : BaseFamiliar
    {

        public TAVGeoFamiliar()
        {
            Name = "a rumbler";
            Body = 0x12C;
            Hue = 0x718;
            BaseSoundID = 268;
            Level = 6;

            SetStr(50);
            SetDex(60);
            SetInt(100);

            SetHits(250);
            SetStam(60);

            SetDamage(7, 12);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Fire, 35, 55);
            SetResistance(ResistanceType.Cold, 35, 55);
            SetResistance(ResistanceType.Poison, 40, 55);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill(SkillName.Wrestling, 70.0);
            SetSkill(SkillName.Tactics, 80.0);

            ControlSlots = 1;

        }

        private DateTime m_NextPound;

        public override void OnThink()
        {
            base.OnThink();

            if (DateTime.Now < m_NextPound)
                return;

            m_NextPound = DateTime.Now + TimeSpan.FromSeconds(15.0 + (15.0 * Utility.RandomDouble()));



            Timer.DelayCall(TimeSpan.FromSeconds(0.5), new TimerCallback(Pound));
        }

        private void Pound()
        {
		
            TeiravonMobile caster = this.ControlMaster as TeiravonMobile;
            int poundtimes = 2 + Utility.Random(3);

            if (caster == null)
                caster = this.SummonMaster as TeiravonMobile;

            if (caster == null)
                return;
		
            if(!caster.Warmode)
		        return;

            this.PublicOverheadMessage(MessageType.Regular, 0x2E, false, "*You see " + this.Name + " start to slam the ground.");
            this.Animate(9, 5, 4, true, false, 0);
            this.PlaySound(0x2F3);
            this.Paralyze(TimeSpan.FromSeconds(2.5));
            PoundAoE();
            Timer m_Timer = new TremorTimer(caster, 1, poundtimes, (DateTime.Now + TimeSpan.FromSeconds(0.5)));
            m_Timer.Start();
		
		return;
        }

        private void PoundAoE()
        {
            Mobile caster = this.ControlMaster;

            if (caster == null)
                caster = this.SummonMaster;

            if (caster == null)
                return;

            ArrayList list = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(3))
            {
                if (caster != m && caster.InLOS(m) && SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false))
                    list.Add(m);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                Mobile m = (Mobile)list[i];

                caster.DoHarmful(m);
                m.Paralyze(TimeSpan.FromSeconds(1.0));
            }
        }

        public TAVGeoFamiliar(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}