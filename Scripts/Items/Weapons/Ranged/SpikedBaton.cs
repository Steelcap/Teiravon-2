using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class SpikedBaton : BaseRanged
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.CrushingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.BleedAttack; } }

        public override int EffectID { get { return 0x2563; } }
        public override Type AmmoType { get { return typeof(SpikedBaton); } }
        public override Item Ammo { get { return new SpikedBaton(); } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x238; } }

        public override int AosStrengthReq { get { return 35; } }
        public override int AosMinDamage { get { return 19; } }
        public override int AosMaxDamage { get { return 22; } }
        public override int AosSpeed { get { return 26; } }

        public override int OldStrengthReq { get { return 30; } }
        public override int OldMinDamage { get { return 10; } }
        public override int OldMaxDamage { get { return 30; } }
        public override int OldSpeed { get { return 32; } }

        public override int InitMinHits { get { return 10; } }
        public override int InitMaxHits { get { return 15; } }

        public override int DefMaxRange { get { return 4; } }
        public override WeaponType DefType { get { return WeaponType.Thrown; } }

        [Constructable]
        public SpikedBaton()
            : base(0x2563)
        {
            Weight = 17.0;
        }

        public SpikedBaton(Serial serial)
            : base(serial)
        {
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