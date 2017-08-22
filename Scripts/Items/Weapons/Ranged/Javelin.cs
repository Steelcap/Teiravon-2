using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class Javelin : BaseRanged
    {

        public override int EffectID { get { return 0x2572; } }
        public override Type AmmoType { get { return typeof(Javelin); } }
        public override Item Ammo { get { return new Javelin(); } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x238; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ParalyzingBlow; } }

        public override int AosStrengthReq { get { return 40; } }
        public override int AosMinDamage { get { return 17; } }
        public override int AosMaxDamage { get { return 20; } }
        public override int AosSpeed { get { return 42; } }

        public override int OldStrengthReq { get { return 30; } }
        public override int OldMinDamage { get { return 2; } }
        public override int OldMaxDamage { get { return 36; } }
        public override int OldSpeed { get { return 46; } }

        public override int DefMaxRange { get { return 6; } }

        public override int InitMinHits { get { return 3; } }
        public override int InitMaxHits { get { return 8; } }

        public override WeaponType DefType { get { return WeaponType.Thrown; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Slash1H; } }

        [Constructable]
        public Javelin()
            : base(0x2572)
        {
            Weight = 7.0;
        }

        public Javelin(Serial serial)
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