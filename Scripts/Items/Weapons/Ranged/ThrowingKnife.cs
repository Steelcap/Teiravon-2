using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
    public class ThrowingKnife : BaseRanged
    {
        public override int EffectID{ get{ return 0xF51; } }
		public override Type AmmoType{ get{ return typeof( ThrowingKnife ); } }
		public override Item Ammo{ get{ return new ThrowingKnife(); } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x238; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.InfectiousStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ShadowStrike; } }

        public override int AosStrengthReq { get { return 10; } }
        public override int AosMinDamage { get { return 9; } }
        public override int AosMaxDamage { get { return 12; } }
        public override int AosSpeed { get { return 50; } }

        public override int OldStrengthReq { get { return 1; } }
        public override int OldMinDamage { get { return 3; } }
        public override int OldMaxDamage { get { return 15; } }
        public override int OldSpeed { get { return 55; } }

        public override int DefMaxRange{ get{ return 4; } }

        public override int InitMinHits { get { return 2; } }
        public override int InitMaxHits { get { return 4; } }

        public override WeaponType DefType { get { return WeaponType.Thrown; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Slash1H; } }

        [Constructable]
        public ThrowingKnife()
            : base(0x257E)
        {
            Weight = 1.0;
        }

        public ThrowingKnife(Serial serial)
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