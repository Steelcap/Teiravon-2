using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Spells;

namespace Server.Mobiles
{
    public class PustulentCorpse : BaseCreature
    {
        [Constructable]
        public PustulentCorpse()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a pustulent corpse";
            Body = 155;
            BaseSoundID = 471;
            Level = 14;

            SetStr(301, 350);
            SetDex(75);
            SetInt(151, 200);

            SetHits(1100);
            SetStam(150);
            SetMana(0);

            SetDamage(9, 12);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Cold, 25);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 50, 70);
            SetResistance(ResistanceType.Poison, 100, 100);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.Poisoning, 120.0);
            SetSkill(SkillName.MagicResist, 250.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 9000;
            Karma = -6000;

            VirtualArmor = 40;
            ControlSlots = 4;
            //PackGold( 150, 200 );
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override Poison HitPoison { get { return Poison.Deadly; } }


        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from != this)
            {
                if (willKill)
                {
                    Map map = this.Map;
                    Point3D loc = this.Location;

                    Effects.PlaySound(loc, map, 0x4F4);
                    AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.2), .05, map, loc, 14000, 10, 2848, 1, 3, 5, true, false, false);

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
                            int damage = (int)(200 * falloff);
                            AOS.Scale(damage, (100 - (int)(10 * distance)));

                            AOS.Damage(m, this, damage, 0, 0, 0, 100, 0);
                        }
                    }
                }
                else if (Utility.RandomDouble() < .20)
                {
                    Effects.SendTargetEffect(this, 14265, 10, 2848, 0);
                    this.PlaySound(308);


                    ArrayList list = new ArrayList();

                    foreach (Mobile m in this.GetMobilesInRange(1))
                        list.Add(m);

                    for (int i = 0; i < list.Count; ++i)
                    {
                        Mobile m = (Mobile)list[i];

                        if (m != this)
                        {
                            if (m == null || m.Deleted || m.Map != this.Map || !m.Alive || !this.Alive || !this.CanSee(m))
                                continue;

                            if (this.InLOS(m))
                            {

                                m.SendMessage("The virulent ichor burns your flesh");

                                AOS.Damage(m, this, 50, 0, 0, 0, 100, 0, false);
                            }
                        }
                    }
                }
            }
            base.OnDamage(amount, from, willKill);
        }
        
               
        public PustulentCorpse(Serial serial)
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