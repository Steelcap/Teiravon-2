using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
    [CorpseName("a giant spider corpse")]
    public class Spiderling : BaseCreature
    {
        private Mobile m_Target;
        private DateTime m_ExpireTime;

        [Constructable]
        public Spiderling(Mobile caster, Mobile target, TimeSpan duration)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a spiderling";
            Body = 342;
            BaseSoundID = 0x388;
            Level = 2;

            m_Target = target;
            m_ExpireTime = DateTime.Now + duration;

            SetStr(26, 50);
            SetDex(176, 295);
            SetInt(36, 60);

            SetHits(6, 12);
            SetMana(0);

            SetDamage(6, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);
            SetResistance(ResistanceType.Poison, 25, 35);

            SetSkill(SkillName.Poisoning, 60.1, 80.0);
            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 35.1, 50.0);
            SetSkill(SkillName.Wrestling, 100.1, 165.0);

            Fame = 100;
            Karma = -600;

            VirtualArmor = 16;

            Tamable = true;
            ControlSlots = 0;
            MinTameSkill = 19.1;
            ControlMaster = caster;
            Controled = true;

            //PackItem(new SpidersSilk(6));
            //PackGold( 25, 30 );
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
        }

        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Arachnid; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        public override Poison HitPoison { get { return Poison.Regular; } }
        public override bool AlwaysMurderer { get { return true; } }

        public override void OnThink()
        {
	    if(this == null)
		return;
            if ( m_Target == null || !m_Target.Alive || DateTime.Now > m_ExpireTime)
            {
                Kill();
                return;
            }
            else if (Map != m_Target.Map || !InRange(m_Target, 15))
            {
                Map fromMap = Map;
                Point3D from = Location;

                Map toMap = m_Target.Map;
                Point3D to = m_Target.Location;

                if (toMap != null)
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        Point3D loc = new Point3D(to.X - 4 + Utility.Random(9), to.Y - 4 + Utility.Random(9), to.Z);

                        if (toMap.CanSpawnMobile(loc))
                        {
                            to = loc;
                            break;
                        }
                        else
                        {
                            loc.Z = toMap.GetAverageZ(loc.X, loc.Y);

                            if (toMap.CanSpawnMobile(loc))
                            {
                                to = loc;
                                break;
                            }
                        }
                    }
                }

                Map = toMap;
                Location = to;

                ProcessDelta();

                Effects.SendLocationParticles(EffectItem.Create(from, fromMap, EffectItem.DefaultDuration), 0x3728, 1, 13, 37, 7, 5023, 0);
                FixedParticles(0x3728, 1, 13, 5023, 37, 7, EffectLayer.Waist);

                PlaySound(0x37D);
            }

            if (m_Target.Hidden && InRange(m_Target, 3) && DateTime.Now >= this.NextSkillTime && UseSkill(SkillName.DetectHidden))
            {
                Target targ = this.Target;

                if (targ != null)
                    targ.Invoke(this, this);
            }

            Combatant = m_Target;
            FocusMob = m_Target;

            if (AIObject != null)
                AIObject.Action = ActionType.Combat;

            base.OnThink();
        }

        public override bool OnBeforeDeath()
        {
            Effects.SendLocationParticles(EffectItem.Create(Location, Map, TimeSpan.FromSeconds(10.0)), 0x3735, 1, 50, 2101, 7, 9909, 0);

            Delete();
            return false;
        }

        public Spiderling(Serial serial)
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