using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a Volitile FlameSpawn corpse")]
    public class VolitileFlameSpawn : BaseCreature
    {
        [Constructable]
        public VolitileFlameSpawn()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a Volitile FlameSpawn";
            Body = 22;
            BaseSoundID = 377;
            Level = 14;
            Hue = 2988;

            SetStr(111, 200);
            SetDex(101, 125);
            SetInt(301, 390);

            SetHits(251, 300);

            SetDamage(17, 23);

            SetDamageType(ResistanceType.Physical, 15);
            SetDamageType(ResistanceType.Fire, 85);

            SetResistance(ResistanceType.Physical, 10, 20);
            SetResistance(ResistanceType.Fire, 80, 95);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Poison, 50, 75);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 50.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 70.0);

            Fame = 2000;
            Karma = -2000;

            VirtualArmor = 44;

        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from != this)
            {
                if (willKill)
                {
                    Map map = this.Map;
                    Point3D loc = this.Location;

                    Effects.PlaySound(loc, map, 0x4F4);
                    AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.2), .05, map, loc, 14000, 10, 0, 1, 3, 5, true, false, false);

                    ArrayList toExplode = new ArrayList();

                    foreach (Mobile m in this.GetMobilesInRange(3))
                        toExplode.Add(m);


                    for (int i = 0; i < toExplode.Count; ++i)
                    {
                        Mobile m = (Mobile)toExplode[i];

                        if (SpellHelper.ValidIndirectTarget(this, m) && this.CanBeHarmful(m, false))
                        {
                            if (this != null)
                                this.DoHarmful(m);
                            double distance = m.GetDistanceToSqrt(loc);
                            double falloff = (100 - (10 * distance)) * .01;
                            int damage = (int)(150 * falloff);
                            AOS.Scale(damage, (100 - (int)(10 * distance)));

                            AOS.Damage(m, this, damage, 0, 100, 0,0 , 0);
                        }
                    }
                }
            }
            base.OnDamage(amount, from, willKill);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }

        public VolitileFlameSpawn(Serial serial)
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