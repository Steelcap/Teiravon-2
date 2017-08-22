using System;

namespace Server.Items
{
    public abstract class BaseOuterTorso : BaseClothing
    {
        public BaseOuterTorso(int itemID)
            : this(itemID, 0)
        {
        }

        public BaseOuterTorso(int itemID, int hue)
            : base(itemID, Layer.OuterTorso, hue)
        {
        }

        public BaseOuterTorso(Serial serial)
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

    [Flipable(0x230E, 0x230D)]
    public class GildedDress : BaseOuterTorso
    {
        [Constructable]
        public GildedDress()
            : this(0)
        {
        }

        [Constructable]
        public GildedDress(int hue)
            : base(0x230E, hue)
        {
            Weight = 3.0;
        }

        public GildedDress(Serial serial)
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

    [Flipable(0x1F00, 0x1EFF)]
    public class FancyDress : BaseOuterTorso
    {
        [Constructable]
        public FancyDress()
            : this(0)
        {
        }

        [Constructable]
        public FancyDress(int hue)
            : base(0x1F00, hue)
        {
            Weight = 3.0;
        }

        public FancyDress(Serial serial)
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

    public class DeathRobe : Robe
    {
        public override bool DisplayLootType
        {
            get { return false; }
        }

        [Constructable]
        public DeathRobe()
        {
            LootType = LootType.Newbied;
        }

        public new bool Scissor(Mobile from, Scissors scissors)
        {
            from.SendLocalizedMessage(502440); // Scissors can not be used on that to produce anything.
            return false;
        }

        public DeathRobe(Serial serial)
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

    [Flipable]
    public class RewardRobe : BaseOuterTorso, Engines.VeteranRewards.IRewardItem
    {
        private int m_LabelNumber;
        private bool m_IsRewardItem;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get { return m_IsRewardItem; }
            set { m_IsRewardItem = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Number
        {
            get { return m_LabelNumber; }
            set { m_LabelNumber = value; InvalidateProperties(); }
        }

        public override int LabelNumber
        {
            get
            {
                if (m_LabelNumber > 0)
                    return m_LabelNumber;

                return base.LabelNumber;
            }
        }

        public override int BasePhysicalResistance { get { return 3; } }

        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile)
                ((Mobile)parent).VirtualArmorMod += 2;
        }

        public override void OnRemoved(object parent)
        {
            base.OnRemoved(parent);

            if (parent is Mobile)
                ((Mobile)parent).VirtualArmorMod -= 2;
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public override bool CanEquip(Mobile m)
        {
            if (!base.CanEquip(m))
                return false;

            return !m_IsRewardItem || Engines.VeteranRewards.RewardSystem.CheckIsUsableBy(m, this, new object[] { Hue, m_LabelNumber });
        }

        [Constructable]
        public RewardRobe()
            : this(0)
        {
        }

        [Constructable]
        public RewardRobe(int hue)
            : this(hue, 0)
        {
        }

        [Constructable]
        public RewardRobe(int hue, int labelNumber)
            : base(0x1F03, hue)
        {
            Weight = 3.0;
            LootType = LootType.Blessed;

            m_LabelNumber = labelNumber;
        }

        public RewardRobe(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_LabelNumber);
            writer.Write((bool)m_IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_LabelNumber = reader.ReadInt();
                        m_IsRewardItem = reader.ReadBool();
                        break;
                    }
            }

            if (Parent is Mobile)
                ((Mobile)Parent).VirtualArmorMod += 2;
        }
    }

    [Flipable]
    public class Robe : BaseOuterTorso, IArcaneEquip
    {
        #region Arcane Impl
        private int m_MaxArcaneCharges, m_CurArcaneCharges;

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxArcaneCharges
        {
            get { return m_MaxArcaneCharges; }
            set { m_MaxArcaneCharges = value; InvalidateProperties(); Update(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int CurArcaneCharges
        {
            get { return m_CurArcaneCharges; }
            set { m_CurArcaneCharges = value; InvalidateProperties(); Update(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsArcane
        {
            get { return (m_MaxArcaneCharges > 0 && m_CurArcaneCharges >= 0); }
        }

        public void Update()
        {
            if (IsArcane)
                ItemID = 0x26AE;
            else if (ItemID == 0x26AE)
                ItemID = 0x1F04;

            if (IsArcane && CurArcaneCharges == 0)
                Hue = 0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (IsArcane)
                list.Add(1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges); // arcane charges: ~1_val~ / ~2_val~
        }

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);

            if (IsArcane)
                LabelTo(from, 1061837, String.Format("{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges));
        }

        public void Flip()
        {
            if (ItemID == 0x1F03)
                ItemID = 0x1F04;
            else if (ItemID == 0x1F04)
                ItemID = 0x1F03;
        }
        #endregion

        [Constructable]
        public Robe()
            : this(0)
        {
        }

        [Constructable]
        public Robe(int hue)
            : base(0x1F03, hue)
        {
            Weight = 3.0;
        }

        public Robe(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            if (IsArcane)
            {
                writer.Write(true);
                writer.Write((int)m_CurArcaneCharges);
                writer.Write((int)m_MaxArcaneCharges);
            }
            else
            {
                writer.Write(false);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        if (reader.ReadBool())
                        {
                            m_CurArcaneCharges = reader.ReadInt();
                            m_MaxArcaneCharges = reader.ReadInt();

                            if (Hue == 2118)
                                Hue = ArcaneGem.DefaultArcaneHue;
                        }

                        break;
                    }
            }
        }
    }

    [Flipable(0x2684, 0x2683)]
    public class HoodedShroudOfShadows : BaseOuterTorso
    {
        [Constructable]
        public HoodedShroudOfShadows()
            : this(0x455)
        {
        }

        [Constructable]
        public HoodedShroudOfShadows(int hue)
            : base(0x2684, hue)
        {
            LootType = LootType.Blessed;
            Weight = 3.0;
        }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendLocalizedMessage(sender.FailMessage);
            return false;
        }

        public HoodedShroudOfShadows(Serial serial)
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

    [Flipable(0x1f01, 0x1f02)]
    public class PlainDress : BaseOuterTorso
    {
        [Constructable]
        public PlainDress()
            : this(0)
        {
        }

        [Constructable]
        public PlainDress(int hue)
            : base(0x1F01, hue)
        {
            Weight = 2.0;
        }

        public PlainDress(Serial serial)
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

            if (Weight == 3.0)
                Weight = 2.0;
        }
    }

    // Teiravon custom

    public class BeltedCoat : BaseOuterTorso
    {
        [Constructable]
        public BeltedCoat() : this(0) { }

        [Constructable]
        public BeltedCoat(int hue)
            : base(0x3ca8, hue)
        {
            Weight = 1.0;
            Name = "Belted Coat";
        }

        public BeltedCoat(Serial serial) : base(serial) { }

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

    public class BeltedDress : BaseOuterTorso
    {
        [Constructable]
        public BeltedDress() : this(0) { }

        [Constructable]
        public BeltedDress(int hue)
            : base(0x3ca0, hue)
        {
            Weight = 1.0;
            Name = "Belted Dress";
        }

        public BeltedDress(Serial serial) : base(serial) { }

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

    public class WinterCoat : BaseOuterTorso
    {
        [Constructable]
        public WinterCoat() : this(0) { }

        [Constructable]
        public WinterCoat(int hue)
            : base(0x3c9c, hue)
        {
            Weight = 1.0;
            Name = "Winter Coat";
        }

        public WinterCoat(Serial serial) : base(serial) { }

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

    public class LayeredDress : BaseOuterTorso
    {
        [Constructable]
        public LayeredDress() : this(0) { }

        [Constructable]
        public LayeredDress(int hue)
            : base(0x3c9a, hue)
        {
            Weight = 1.0;
            Name = "Layered Dress";
        }

        public LayeredDress(Serial serial) : base(serial) { }

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

    public class FancyGown : BaseOuterTorso
    {
        [Constructable]
        public FancyGown() : this(0) { }

        [Constructable]
        public FancyGown(int hue)
            : base(0x3c99, hue)
        {
            Weight = 1.0;
            Name = "Fancy Gown";
        }

        public FancyGown(Serial serial) : base(serial) { }

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

    public class NobleGown : BaseOuterTorso
    {
        [Constructable]
        public NobleGown() : this(0) { }

        [Constructable]
        public NobleGown(int hue)
            : base(0x3c98, hue)
        {
            Weight = 1.0;
            Name = "Noble Gown";
        }

        public NobleGown(Serial serial) : base(serial) { }

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

    public class SashDress : BaseOuterTorso
    {
        [Constructable]
        public SashDress() : this(0) { }

        [Constructable]
        public SashDress(int hue)
            : base(0x3c97, hue)
        {
            Weight = 1.0;
            Name = "Sash Dress";
        }

        public SashDress(Serial serial) : base(serial) { }

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

    public class PeasantGown : BaseOuterTorso
    {
        [Constructable]
        public PeasantGown() : this(0) { }

        [Constructable]
        public PeasantGown(int hue)
            : base(0x3c96, hue)
        {
            Weight = 1.0;
            Name = "Peasant Gown";
        }

        public PeasantGown(Serial serial) : base(serial) { }

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

    public class PeasantDress : BaseOuterTorso
    {
        [Constructable]
        public PeasantDress() : this(0) { }

        [Constructable]
        public PeasantDress(int hue)
            : base(0x3c95, hue)
        {
            Weight = 1.0;
            Name = "Peasant Dress";
        }

        public PeasantDress(Serial serial) : base(serial) { }

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

    public class HousewifeDress : BaseOuterTorso
    {
        [Constructable]
        public HousewifeDress() : this(0) { }

        [Constructable]
        public HousewifeDress(int hue)
            : base(0x3c94, hue)
        {
            Weight = 1.0;
            Name = "Housewife Dress";
        }

        public HousewifeDress(Serial serial) : base(serial) { }

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

    public class BarwenchDress : BaseOuterTorso
    {
        [Constructable]
        public BarwenchDress() : this(0) { }

        [Constructable]
        public BarwenchDress(int hue)
            : base(0x3c93, hue)
        {
            Weight = 1.0;
            Name = "Barwench Dress";
        }

        public BarwenchDress(Serial serial) : base(serial) { }

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

    public class LayeredRobe : BaseOuterTorso
    {
        [Constructable]
        public LayeredRobe() : this(0) { }

        [Constructable]
        public LayeredRobe(int hue)
            : base(0x3ca7, hue)
        {
            Weight = 1.0;
            Name = "Layered Robe";
        }

        public LayeredRobe(Serial serial) : base(serial) { }

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