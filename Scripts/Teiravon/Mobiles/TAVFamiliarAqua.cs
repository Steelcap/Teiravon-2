using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Teiravon;
using Server.Scripts.Commands;

namespace Server.Mobiles
{
    [CorpseName("an aqualing corpse")]
    public class TAVAquaFamiliar : BaseFamiliar
    {
        public TAVAquaFamiliar()
        {
            Name = "an aqualing";
            Body = 0x33;
            Hue = 0x8AB;
            BaseSoundID = 278;
            Level = 3;

            SetStr(50);
            SetDex(60);
            SetInt(100);

            SetHits(50);
            SetStam(60);
            SetMana(0);

            SetDamage(5, 10);

            SetDamageType(ResistanceType.Energy, 100);

            SetResistance(ResistanceType.Physical, 10, 15);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Cold, 10, 15);
            SetResistance(ResistanceType.Poison, 10, 15);
            SetResistance(ResistanceType.Energy, 99);

            SetSkill(SkillName.Wrestling, 40.0);
            SetSkill(SkillName.Tactics, 40.0);

            ControlSlots = 1;
        }

        private DateTime m_NextMirage;
        protected override BaseAI ForcedAI { get { return new CloneAI(this); } }
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override void OnThink()
        {
            base.OnThink();

            if (DateTime.Now < m_NextMirage)
                return;

            m_NextMirage = DateTime.Now + TimeSpan.FromSeconds(5.0 + (15.0 * Utility.RandomDouble()));



            Timer.DelayCall(TimeSpan.FromSeconds(0.5), new TimerCallback(Mirage));
        }

        private void Mirage()
        {

		

            TeiravonMobile caster = (TeiravonMobile)this.ControlMaster;

            if (caster == null)
                caster = (TeiravonMobile)this.SummonMaster;

            if (caster == null)
                return;
		
		if (!caster.Warmode)
		return;
            MirrorImage.AddClone(caster);

            caster.FixedParticles(0x376A, 1, 14, 0x13B5, EffectLayer.Waist);
            caster.PlaySound(0x594);

            new Clone(caster).MoveToWorld(caster.Location, caster.Map);
	
return;

        }

        public TAVAquaFamiliar(Serial serial)
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