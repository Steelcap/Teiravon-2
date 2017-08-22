using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

    public class MobRange : BaseSword
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.DoubleStrike; } }

        public override int AosStrengthReq { get { return 10; } }
        public override int AosMinDamage { get { return 9; } }
        public override int AosMaxDamage { get { return 12; } }
        public override int AosSpeed { get { return 50; } }

        public override int DefHitSound { get { return HitSound; } }
        public override int DefMissSound { get { return 0x239; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 90; } }

        private int HitSound = -1;

        public override SkillName DefSkill { get { return SkillName.Wrestling; } }
        public override WeaponType DefType { get { return WeaponType.Fists; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Wrestle; } }

        [Constructable]
        public MobRange(int range)
            : base(0x13B8)
        {
            MaxRange = range;
            Weight = 0.0;
        }
        [Constructable]
        public MobRange()
            : base(0x13B8)
        {
            Weight = 0.0;
        }

        public MobRange(Serial serial)
            : base(serial)
        {
        }
        public override bool OnEquip(Mobile from)
        {
            HitSound = from.BaseSoundID;
            return base.OnEquip(from);
        }
        public override void OnRemoved(object parent)
        {
            this.Delete();
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

        }
    }
}
