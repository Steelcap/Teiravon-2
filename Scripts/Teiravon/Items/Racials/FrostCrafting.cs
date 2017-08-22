using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Engines.Craft;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x2645, 0x2646)]
    public class ArcticPlateHelm : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 8; } }
        public override int BasePoisonResistance { get { return 4; } }
        public override int BaseEnergyResistance { get { return 4; } }

        public override int InitMinHits { get { return 75; } }
        public override int InitMaxHits { get { return 85; } }

        public override int AosStrReq { get { return 80; } }
        public override int OldStrReq { get { return 40; } }

        public override int OldDexBonus { get { return -1; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public ArcticPlateHelm()
            : base(0x2646)
        {
            Name = "Arctic Plate Helm";
            Weight = 7.0;
            Hue = 2400;
        }

        public ArcticPlateHelm(Serial serial)
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

    [FlipableAttribute(0x2643, 0x2644)]
    public class ArcticPlateGloves : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 8; } }
        public override int BasePoisonResistance { get { return 4; } }
        public override int BaseEnergyResistance { get { return 4; } }

        public override int InitMinHits { get { return 65; } }
        public override int InitMaxHits { get { return 75; } }

        public override int AosStrReq { get { return 70; } }
        public override int OldStrReq { get { return 30; } }

        public override int OldDexBonus { get { return -2; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public ArcticPlateGloves()
            : base(0x2644)
        {
            Name = "Arctic Plate Gloves";
            Weight = 3.0;
            Hue = 2400;
        }

        public ArcticPlateGloves(Serial serial)
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

    [FlipableAttribute(11022, 11023)]
    public class ArcticPlateGorget : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 8; } }
        public override int BasePoisonResistance { get { return 4; } }
        public override int BaseEnergyResistance { get { return 4; } }

        public override int InitMinHits { get { return 60; } }
        public override int InitMaxHits { get { return 70; } }

        public override int AosStrReq { get { return 70; } }
        public override int OldStrReq { get { return 30; } }

        public override int OldDexBonus { get { return -1; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public ArcticPlateGorget()
            : base(11022)
        {
            Name = "Arctic Plate Gorget";
            Weight = 3.0;
            Hue = 2400;
        }

        public ArcticPlateGorget(Serial serial)
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

    [FlipableAttribute(0x2657, 0x2658)]
    public class ArcticPlateArms : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 8; } }
        public override int BasePoisonResistance { get { return 4; } }
        public override int BaseEnergyResistance { get { return 4; } }

        public override int InitMinHits { get { return 60; } }
        public override int InitMaxHits { get { return 70; } }

        public override int AosStrReq { get { return 90; } }
        public override int OldStrReq { get { return 40; } }

        public override int OldDexBonus { get { return -2; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public ArcticPlateArms()
            : base(0x2658)
        {
            Name = "Arctic Plate Arms";
            Weight = 7.0;
            Hue = 2400;
        }

        public ArcticPlateArms(Serial serial)
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

    [FlipableAttribute(0x2647, 0x2648)]
    public class ArcticPlateLegs : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 8; } }
        public override int BasePoisonResistance { get { return 4; } }
        public override int BaseEnergyResistance { get { return 4; } }

        public override int InitMinHits { get { return 70; } }
        public override int InitMaxHits { get { return 80; } }

        public override int AosStrReq { get { return 90; } }

        public override int OldStrReq { get { return 60; } }
        public override int OldDexBonus { get { return -6; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public ArcticPlateLegs()
            : base(0x2648)
        {
            Name = "Arctic Plate Legs";
            Weight = 5.0;
            Hue = 2400;
        }

        public ArcticPlateLegs(Serial serial)
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

            if (ItemID == 5137)
                ItemID = 11014;
        }
    }

    [FlipableAttribute(0x2641, 0x2642)]
    public class ArcticPlateChest : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 8; } }
        public override int BasePoisonResistance { get { return 4; } }
        public override int BaseEnergyResistance { get { return 4; } }

        public override int InitMinHits { get { return 80; } }
        public override int InitMaxHits { get { return 90; } }

        public override int AosStrReq { get { return 100; } }
        public override int OldStrReq { get { return 60; } }

        public override int OldDexBonus { get { return -8; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public ArcticPlateChest()
            : base(0x2641)
        {
            Name = "Arctic Plate Chest";
            Weight = 15.0;
            Hue = 2400;
        }

        public ArcticPlateChest(Serial serial)
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

            if (Weight == 1.0)
                Weight = 10.0;
            if (ItemID == 5141)
                ItemID = 11016;
        }
    }

    [FlipableAttribute(11026, 11027)]
    public class ArcticPlateBoots : BaseArmor
    {

        public override int InitMinHits { get { return 50; } }
        public override int InitMaxHits { get { return 60; } }

        public override int AosStrReq { get { return 100; } }
        public override int OldStrReq { get { return 60; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public ArcticPlateBoots()
            : base(11026)
        {
            Name = "Arctic Plate Boots";
            Weight = 5.0;
            Hue = 2400;
        }

        public ArcticPlateBoots(Serial serial)
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

    public class SnowScaleSleeves : BaseArmor
    {
        public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 1; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 35; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 22; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

        [Constructable]
        public SnowScaleSleeves()
            : base(0x3163)
        {
            Name = "Snow Scale Pauldrons";
            Weight = 4.0;
            Hue = 2292;
        }

        public SnowScaleSleeves(Serial serial)
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

    public class SnowScaleLeggings : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 3; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 6; } }
        public override int BasePoisonResistance { get { return 3; } }
        public override int BaseEnergyResistance { get { return 3; } }

        public override int InitMinHits { get { return 40; } }
        public override int InitMaxHits { get { return 50; } }

        public override int AosStrReq { get { return 35; } }
        public override int OldStrReq { get { return 20; } }

        public override int OldDexBonus { get { return -2; } }

        public override int ArmorBase { get { return 22; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

        [Constructable]
        public SnowScaleLeggings()
            : base(0x3162)
        {
            Name = "Snow Scale Leggings";
            Weight = 7.0;
            Hue = 2292;
        }

        public SnowScaleLeggings(Serial serial)
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

    public class SnowScaleChest : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 3; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 6; } }
        public override int BasePoisonResistance { get { return 3; } }
        public override int BaseEnergyResistance { get { return 3; } }

        public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 35; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 22; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

        [Constructable]
        public SnowScaleChest()
            : base(0x315E)
        {
            Name = "Snow Scale Chest";
            Weight = 7.0;
            Hue = 2292;
        }

        public SnowScaleChest(Serial serial)
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

    public class SnowScaleGloves : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 3; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 6; } }
        public override int BasePoisonResistance { get { return 3; } }
        public override int BaseEnergyResistance { get { return 3; } }

        public override int InitMinHits { get { return 40; } }
        public override int InitMaxHits { get { return 50; } }

        public override int AosStrReq { get { return 35; } }
        public override int OldStrReq { get { return 20; } }

        public override int OldDexBonus { get { return -1; } }

        public override int ArmorBase { get { return 22; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

        [Constructable]
        public SnowScaleGloves()
            : base(0x3161)
        {
            Name = "Snow Scale Gloves";
            Weight = 4.0;
            Hue = 2292;
        }

        public SnowScaleGloves(Serial serial)
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

    public class SnowScaleGorget : BaseArmor
    {
        public override int BasePhysicalResistance { get { return 3; } }
        public override int BaseFireResistance { get { return -4; } }
        public override int BaseColdResistance { get { return 6; } }
        public override int BasePoisonResistance { get { return 3; } }
        public override int BaseEnergyResistance { get { return 3; } }

        public override int InitMinHits { get { return 40; } }
        public override int InitMaxHits { get { return 50; } }

        public override int AosStrReq { get { return 35; } }
        public override int OldStrReq { get { return 20; } }

        public override int OldDexBonus { get { return -1; } }

        public override int ArmorBase { get { return 22; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Chainmail; } }

        [Constructable]
        public SnowScaleGorget()
            : base(0x3160)
        {
            Name = "Snow Scale Gorget";
            Weight = 4.0;
            Hue = 2292;
        }

        public SnowScaleGorget(Serial serial)
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

    public class FrostAxe : BaseAxe
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.DoubleStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.WhirlwindAttack; } }

        public override int AosStrengthReq { get { return 45; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 17; } }
        public override int AosSpeed { get { return 33; } }

        public override int OldStrengthReq { get { return 45; } }
        public override int OldMinDamage { get { return 5; } }
        public override int OldMaxDamage { get { return 35; } }
        public override int OldSpeed { get { return 37; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 110; } }

        [Constructable]
        public FrostAxe()
            : base(0x3DD4)
        {
            Name = "Frigid Axe";
            Hue = 2400;
            Weight = 5.0;
        }

        public FrostAxe(Serial serial)
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

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy)
        {
            cold = 50;
            phys = 50;
            fire = pois = nrgy = 0;
        }
    }
   
    public class IceBlade : BaseSword
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

        public override int AosStrengthReq { get { return 25; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 16; } }
        public override int AosSpeed { get { return 30; } }

        public override int OldStrengthReq { get { return 25; } }
        public override int OldMinDamage { get { return 5; } }
        public override int OldMaxDamage { get { return 33; } }
        public override int OldSpeed { get { return 35; } }

        public override int DefHitSound { get { return 0x237; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 110; } }

        [Constructable]
        public IceBlade()
            : base(0x3DC4)
        {
            Name = "Frozen Blade";
            Weight = 5.0;
            Hue = 2400;
        }

        public IceBlade(Serial serial)
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

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy)
        {
            cold = 50;
            phys = 50;
            fire = pois = nrgy = 0;
        }
    }

    public class GlacialMace : BaseBashing
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

        public override int AosStrengthReq { get { return 45; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 17; } }
        public override int AosSpeed { get { return 28; } }

        public override int OldStrengthReq { get { return 35; } }
        public override int OldMinDamage { get { return 6; } }
        public override int OldMaxDamage { get { return 33; } }
        public override int OldSpeed { get { return 30; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 70; } }

        [Constructable]
        public GlacialMace()
            : base(0x2D24)
        {
            Name = "Polar Mace";
            Weight = 5.0;
            Hue = 2400;
        }

        public GlacialMace(Serial serial)
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

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy)
        {
            cold = 50;
            phys = 50;
            fire = pois = nrgy = 0;
        }
    }

    public class IcicleLance :BaseSword
        {
            public override WeaponAbility PrimaryAbility { get { return WeaponAbility.Dismount; } }
            public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

            public override int AosStrengthReq { get { return 65; } }
            public override int AosMinDamage { get { return 17; } }
            public override int AosMaxDamage { get { return 18; } }
            public override int AosSpeed { get { return 24; } }

            public override int OldStrengthReq { get { return 95; } }
            public override int OldMinDamage { get { return 17; } }
            public override int OldMaxDamage { get { return 18; } }
            public override int OldSpeed { get { return 24; } }

            public override int DefMaxRange { get { return 2; } }

            public override int DefHitSound { get { return 0x23C; } }
            public override int DefMissSound { get { return 0x238; } }

            public override int InitMinHits { get { return 31; } }
            public override int InitMaxHits { get { return 62; } }

            public override SkillName DefSkill { get { return SkillName.Fencing; } }
            public override WeaponType DefType { get { return WeaponType.Piercing; } }
            public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Pierce1H; } }

            [Constructable]
            public IcicleLance()
                : base(0x26C0)
            {
                Weight = 12.0;
                Hue = 2292;
            }

            public IcicleLance(Serial serial)
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

            public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy)
            {
                cold = 50;
                phys = 50;
                fire = pois = nrgy = 0;
            }
        }

    public class FrozenSheers : Scissors
    {
        [Constructable]
        public FrozenSheers()
            : base(0xF9F)
        {
            Hue = 2400;
        }

        public FrozenSheers(Serial serial)
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

    public class Frostcarver : BaseTool
    {
        public override CraftSystem CraftSystem { get { return DefFrostcraft.CraftSystem; } }

        [Constructable]
        public Frostcarver()
            : base(0xEC4)
        {
            Name = "Frostcarver";
            Weight = 2.0;
            Hue = 2400;
        }

        [Constructable]
        public Frostcarver(int uses)
            : base(uses, 0xEC4)
        {
            Hue = 2400;
            Weight = 2.0;
        }

        public Frostcarver(Serial serial)
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