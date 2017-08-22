using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Teiravon;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
    public class EnchantedQuiver : BaseRanged
    {

        public override int EffectID { get { return 0x1BFE; } }
        public override Type AmmoType { get { return typeof(Bolt); } }
        public override Item Ammo { get { return new Bolt(); } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.MortalStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

        public override int AosStrengthReq { get { return 0; } }
        public override int AosMinDamage { get { return 2; } }
        public override int AosMaxDamage { get { return 8; } }
        public override int AosSpeed { get { return 50; } }

        public override int OldStrengthReq { get { return 0; } }
        public override int OldMinDamage { get { return 2; } }
        public override int OldMaxDamage { get { return 10; } }
        public override int OldSpeed { get { return 30; } }

        public override int DefHitSound { get { return -1; } }
        public override int DefMissSound { get { return -1; } }
        public int WeaponLevel = 1;

        public override SkillName DefSkill { get { return SkillName.Archery; } }
        public override WeaponType DefType { get { return WeaponType.Ranged; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.ShootBow; } }

        public EnchantedQuiver()
            : base(0)
        {
            Weight = 0.0;
            Visible = false;
            Movable = false;
            Layer = Layer.Unused_x9;
            XmlAttach.AttachTo(this,
            new XmlCustomAttacks(new XmlCustomAttacks.SpecialAttacks[] 
            {XmlCustomAttacks.SpecialAttacks.ForceArrow,
             XmlCustomAttacks.SpecialAttacks.BurstShot,
             XmlCustomAttacks.SpecialAttacks.SnareArrow,
             XmlCustomAttacks.SpecialAttacks.SnakeShot,
             XmlCustomAttacks.SpecialAttacks.VoidArrow,
             }
)
);
        }

        public EnchantedQuiver(Serial serial)
            : base(serial)
        {
        }

        public override void OnMiss(Mobile attacker, Mobile defender)
        {
            base.PlaySwingAnimation(attacker);
        }

        public override DeathMoveResult OnParentDeath(Mobile parent)
        {
            return DeathMoveResult.RemainEquiped;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (this != null)
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

            Delete();
        }
    }
}