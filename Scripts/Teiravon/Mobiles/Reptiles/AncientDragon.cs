using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a dragon corpse")]
    public class AncientDragon : BaseCreature
    {
        [Constructable]
        public AncientDragon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ancient dragon";
            Body = 440;
            BaseSoundID = 362;
            Level = 21;

            SetStr(1400, 1600);
            SetDex(300, 400);
            SetInt(686, 775);

            SetHits(2000, 2500);

            SetDamage(29, 35);

            RangeFight = 3;
            AddItem(new MobRange(3));

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Fire, 50);

            SetResistance(ResistanceType.Physical, 70, 90);
            SetResistance(ResistanceType.Fire, 80, 90);
            SetResistance(ResistanceType.Cold, 55, 65);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 60, 70);

            SetSkill(SkillName.EvalInt, 80.1, 100.0);
            SetSkill(SkillName.Magery, 80.1, 100.0);
            SetSkill(SkillName.Meditation, 52.5, 75.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 97.6, 100.0);

            SetSkill(SkillName.DetectHidden, 90.1, 130.0);

            Fame = 22500;
            Karma = -22500;

            VirtualArmor = 70;

            PackGem(5);
            PackScroll(1, 6);
            PackScroll(4, 6);
            PackScroll(7, 8);
            PackItem(Loot.RandomArmorOrShieldOrWeaponOrJewelry());

            PackGold(950, 1800);
            PackItem(Loot.RandomJewelry());
            PackItem(Loot.RandomStatue());

            if (Utility.RandomDouble() < 0.5)
                PackItem(new BraceletOfBinding());

            if (0.25 > Utility.RandomDouble())
                PackItem(new WyrmHeart(1));
            if (0.25 > Utility.RandomDouble())
                PackItem(new DragonBlood(Utility.RandomMinMax(3, 5)));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 3);
        }

        public override int GetIdleSound()
        {
            return 0x2D3;
        }

        public override int GetHurtSound()
        {
            return 0x2D1;
        }

        public override bool HasBreath { get { return true; } } // fire breath enabled
        public override bool AutoDispel { get { return true; } }
        public override HideType HideType { get { return HideType.Barbed; } }
        public override int Hides { get { return 40; } }
        public override int Meat { get { return 19; } }
        public override int Scales { get { return 12; } }
        public override ScaleType ScaleType { get { return (ScaleType)Utility.Random(4); } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        public override int TreasureMapLevel { get { return 5; } }

        public AncientDragon(Serial serial)
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
