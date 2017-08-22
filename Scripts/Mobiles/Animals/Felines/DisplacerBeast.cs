using System;
using Server.Items;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Mobiles
{
    [CorpseName("a displacer beast corpse")]
    [TypeAlias("Server.Mobiles.DisplacerBeast")]
    public class DisplacerBeast : BaseCreature
    {
        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.ShadowStrike : WeaponAbility.ArmorIgnore;
        }

        [Constructable]
        public DisplacerBeast()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.1, 0.4)
        {
            Name = "a displacer beast";
            Body = 214;
            Hue = 0x4001;
            BaseSoundID = 0x462;
            Level = 19;

            SetStr(151, 200);
            SetDex(252, 350);
            SetInt(13, 85);

            SetHits(748, 967);

            SetDamage(16, 22);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Fire, 60);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 75, 85);
            SetResistance(ResistanceType.Fire, 80, 90);
            SetResistance(ResistanceType.Energy, 65, 80);

            SetSkill(SkillName.MagicResist, 95.1, 110.0);
            SetSkill(SkillName.Tactics, 90.1, 105.0);
            SetSkill(SkillName.Wrestling, 100.1, 140.0);

            Fame = 10000;
            Karma = -1000;

            VirtualArmor = 70;

            Tamable = false;
            ControlSlots = 1;
            MinTameSkill = 71.1;

            PackScroll(4, 8);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosRich);
            AddLoot(LootPack.HighScrolls);
        }

        //public override bool HasBreath { get { return true; } } // fire breath enabled
        public override void OnThink()
        {
            if (Utility.Random(90) == 0)
            {
                MirrorImage.AddClone(this);

                new Clone(this).MoveToWorld(this.Location, this.Map);
                FixedParticles(0x3728, 9, 32, 0x13AF, EffectLayer.Waist);
            }

            base.OnThink();
        }
        public override int Hides { get { return 10; } }
        public override HideType HideType { get { return HideType.Horned; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Feline; } }

        public override void OnActionCombat()
        {
            if (Utility.Random(50) == 0)
            {
                MirrorImage.AddClone(this);

                new Clone(this).MoveToWorld(this.Location, this.Map);
                FixedParticles(0x3728, 9, 32, 0x13AF, EffectLayer.Waist);
            }
            base.OnActionCombat();
        }

        public override void OnCombatantChange()
        {
            if (Utility.Random(5) == 0)
            {
                MirrorImage.AddClone(this);

                new Clone(this).MoveToWorld(this.Location, this.Map);

                FixedParticles(0x3728, 9, 32, 0x13AF, EffectLayer.Waist);
            }
            base.OnCombatantChange();
        }
        public override void OnDamagedBySpell(Mobile from)
        {
            PlaySound(0x1FE);
            FixedParticles(0x3728, 9, 32, 0x13AF, EffectLayer.Waist);
            Location = from.Location;
            base.OnDamagedBySpell(from);
        }
        public DisplacerBeast(Serial serial)
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