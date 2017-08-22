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
    [CorpseName("a salamander corpse")]
    public class TAVPyroFamiliar : BaseFamiliar
    {

        public TAVPyroFamiliar()
        {
            Name = "a salamander";
            Body = 0xCA;
            Hue = 0x4E9;
            BaseSoundID = 838;
            Level = 3;
            AI = AIType.AI_Mage;

            SetStr(50);
            SetDex(60);
            SetInt(100);

            SetHits(150);
            SetStam(60);
            SetMana(50);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Energy, 100);

            SetResistance(ResistanceType.Physical, 40, 55);
            SetResistance(ResistanceType.Fire, 70, 75);
            SetResistance(ResistanceType.Cold, 10, 15);
            SetResistance(ResistanceType.Poison, 40, 55);
            SetResistance(ResistanceType.Energy, 99);

            SetSkill(SkillName.Wrestling, 70.0);
            SetSkill(SkillName.Tactics, 40.0);
            SetSkill(SkillName.Magery, 30);
            SetSkill(SkillName.EvalInt, 50);

            ControlSlots = 1;

        }

        public override bool HasBreath { get { return true; } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        protected override BaseAI ForcedAI { get { return new CloneAI(this); } }
        private DateTime m_NextBlaze;

        public override void OnThink()
        {
            base.OnThink();

            if (DateTime.Now < m_NextBlaze)
                return;

            m_NextBlaze = DateTime.Now + TimeSpan.FromSeconds(1.0 + (4.0 * Utility.RandomDouble()));



            Timer.DelayCall(TimeSpan.FromSeconds(0.5), new TimerCallback(Blaze));
        }

        private void Blaze()
        {

            TeiravonMobile caster = this.ControlMaster as TeiravonMobile;
            int burntimes = 5 + Utility.Random(3);

            if (caster == null)
                caster = this.SummonMaster as TeiravonMobile;

            if (caster == null)
                return;
            if (!caster.Warmode && caster.Combatant == null)
                return;
            /*
            ArrayList list = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(5))
            {
                if (m.Alive && !m.IsDeadBondedPet)
                    list.Add(m);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                Mobile m = (Mobile)list[i];
                bool enemy = true;

                for (int j = 0; enemy && j < caster.Aggressors.Count; ++j)
                    enemy = (((AggressorInfo)caster.Aggressors[j]).Attacker == caster);

                if (enemy && m != this && m != caster)
                {
                    IgniteTimer m_Timer = new IgniteTimer(caster, m, 1, burntimes, (DateTime.Now + TimeSpan.FromSeconds(2.0)));
                    m_Timer.Start();
                }
            }
            */
            if (caster.Combatant != null && this.InLOS(caster.Combatant) && this.CanSee(caster.Combatant))
            {
                Mobile targ = caster.Combatant;
                Effects.SendMovingEffect(this, targ, 14036, 12, 1, false, false, 2578, 3);
                this.PlaySound(0x0DE);
                AOS.Damage(targ, caster, (int)(caster.PlayerLevel * 1.5 + 3), 0, 100, 0, 0, 0, false);
                return;
            }
        }

        public TAVPyroFamiliar(Serial serial)
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