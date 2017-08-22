using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("an magma elemental corpse")]
    public class MagmaElemental : BaseCreature
    {
        public override double DispelDifficulty { get { return 117.5; } }
        public override double DispelFocus { get { return 45.0; } }
        private DateTime m_LastFlare;

        [Constructable]
        public MagmaElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a magma elemental";
            Body = 14;
            BaseSoundID = 268;
            Level = 13;
	    Hue = 2658;
            SetStr(200);
            SetDex(36, 55);
            SetInt(71, 92);

            SetHits(480, 800);

            SetDamage(9, 16);

            RangeFight = 1;

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Fire,50);

            SetResistance(ResistanceType.Physical, 65, 75);
            SetResistance(ResistanceType.Fire, 100, 100);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 15, 25);
            SetResistance(ResistanceType.Energy, 15, 25);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 34;
            ControlSlots = 2;

            PackGem(2);
            //PackGold( 30, 35 );
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.Gems);
        }

        public override void OnThink()
        {
            if (Utility.Random(3) == 0 && m_LastFlare <= (DateTime.Now - TimeSpan.FromSeconds(3.0)))
            {
                Map map = this.Map;
                Point3D loc = this.Location;
                m_LastFlare = DateTime.Now;
                ArrayList toExplode = new ArrayList();

                foreach (Mobile m in this.GetMobilesInRange(1))
                {
                    if (m.FireResistance <= 71)
                        toExplode.Add(m);
                }
                
                if (toExplode.Count >= 1)
                    
                Effects.PlaySound(loc, map, 0x228);
                Effects.SendTargetEffect(this, 0x3709, 30);

                for (int i = 0; i < toExplode.Count; ++i)
                {
                    Mobile m = (Mobile)toExplode[i];

                    if (SpellHelper.ValidIndirectTarget(this, m) && this.CanBeHarmful(m, false))
                    {
                        if (this != null)
                            this.DoHarmful(m);
                        AOS.Damage(m, this, 30, 0, 100, 0, 0, 0);
                    }
                }
            }
            base.OnThink();
        }
        public MagmaElemental(Serial serial)
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
