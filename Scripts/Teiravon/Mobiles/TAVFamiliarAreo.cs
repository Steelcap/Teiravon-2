using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Teiravon;
using Server.Scripts.Commands;
using Server.Engines;

namespace Server.Mobiles
{
    [CorpseName("a zephyr corpse")]
    public class TAVAreoFamiliar : BaseFamiliar
    {
        public TAVAreoFamiliar()
        {
            Name = "a zephyr";
            Body = 196;
            Hue = 0;
            BaseSoundID = 655;
            Level = 10;
            
            SetStr(50);
            SetDex(60);
            SetInt(100);

            SetHits(150);
            SetStam(60);
            SetMana(50);

            SetDamage(12, 20);

            SetDamageType(ResistanceType.Energy, 100);

            SetResistance(ResistanceType.Physical, 50, 55);
            SetResistance(ResistanceType.Fire, 40, 45);
            SetResistance(ResistanceType.Cold, 50, 65);
            SetResistance(ResistanceType.Poison, 40, 55);
            SetResistance(ResistanceType.Energy, 99);

            SetSkill(SkillName.Wrestling, 40.0);
            SetSkill(SkillName.Tactics, 40.0);

            ControlSlots = 1;
        }

        private DateTime m_NextGale;
        public override bool DeleteCorpseOnDeath { get { return true; } }
        protected override BaseAI ForcedAI { get { return new CloneAI(this); } }

        public override void OnThink()
        {
            base.OnThink();

            if (DateTime.Now < m_NextGale)
                return;

            m_NextGale = DateTime.Now + TimeSpan.FromSeconds(1.0 + (2.0 * Utility.RandomDouble()));

            Timer.DelayCall(TimeSpan.FromSeconds(0.5), new TimerCallback(Gale));
        }

        private void Gale()
        {	

            TeiravonMobile caster = (TeiravonMobile)this.ControlMaster;
            ArrayList list = new ArrayList();

            if (caster == null)
                caster = (TeiravonMobile)this.SummonMaster;

            if (caster == null)
                return;

		if(!caster.Warmode)
		return;

            Engines.PartySystem.Party party = Engines.PartySystem.Party.Get(this.SummonMaster);

            foreach (Mobile m in this.GetMobilesInRange(4))
            {
                if (party != null && party.Contains(m))
                    continue;
                if (m is PlayerVendor)
                    continue;

                if (m is BaseCreature && (((BaseCreature)m).ControlMaster == this.SummonMaster || ((BaseCreature)m).AI == AIType.AI_Vendor) || m.Blessed)
                    continue;

                if (m.Alive && !m.IsDeadBondedPet && m != this.SummonMaster && m != this)
                    list.Add(m);
            }

            if (list.Count > 0)
            {
                this.FixedEffect(0x3779, 10, 20);
                caster.PlaySound(0x594);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                Mobile m = (Mobile)list[i];

                if (m != this && m != caster && !m.Frozen)
                {
                    new Gust(caster, m).MoveToWorld(m.Location, m.Map);
                }
            }
            //int gusts = 1 + (caster.PlayerLevel / 4);
            //for (int i = 0; i < gusts; ++i)
            //    new Gust(caster).MoveToWorld(this.Location, caster.Map);


return;
        }

        public TAVAreoFamiliar(Serial serial)
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