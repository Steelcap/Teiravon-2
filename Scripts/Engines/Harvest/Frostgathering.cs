using System;
using Server;
using Server.Items;
using Server.Engines.Harvest;
using Server.Mobiles;

namespace Server.Engines.Harvest
{
    public class FrostGathering : HarvestSystem
    {
        private static FrostGathering m_System;

        public static FrostGathering System
        {
            get
            {
                if (m_System == null)
                    m_System = new FrostGathering();

                return m_System;
            }
        }

        private HarvestDefinition m_Definition;

        public HarvestDefinition Definition
        {
            get { return m_Definition; }
        }

        private FrostGathering()
        {
            HarvestResource[] res;
            HarvestVein[] veins;

            #region FrostGathering
            HarvestDefinition frost = new HarvestDefinition();

            // Resource banks are every 4x3 tiles
            frost.BankWidth = 4;
            frost.BankHeight = 3;

            // Every bank holds from 20 to 45 logs
            frost.MinTotal = 20;
            frost.MaxTotal = 45;

            // A resource bank will respawn its content every 20 to 30 minutes
            frost.MinRespawn = TimeSpan.FromMinutes(20.0);
            frost.MaxRespawn = TimeSpan.FromMinutes(30.0);

            // Skill checking is done on the FrostGathering skill
            frost.Skill = SkillName.Lumberjacking;

            // Set the list of harvestable tiles
            frost.Tiles = m_FrostTiles;

            // Players must be within 2 tiles to harvest
            frost.MaxRange = 2;

            // Ten logs per harvest action
            frost.ConsumedPerHarvest = 5;
            frost.ConsumedPerFeluccaHarvest = 5;

            // The chopping effect
            frost.EffectActions = new int[] { 11 };
            frost.EffectSounds = new int[] { 0x125, 0x126 };
            frost.EffectCounts = new int[] { 2 };
            frost.EffectDelay = TimeSpan.FromSeconds(1.6);
            frost.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            frost.NoResourcesMessage = "There is not enough ice to harvest here."; // There's not enough wood here to harvest.
            frost.FailMessage = "You dig in the ground but fail to recover any usable ice."; // You hack at the tree for a while, but fail to produce any useable wood.
            frost.OutOfRangeMessage = 500446; // That is too far away.
            frost.PackFullMessage = "You have no room for ice in your bag"; // You can't place any wood into your backpack!
            frost.ToolBrokeMessage = "You've shattered your pick."; // You broke your axe.

            res = new HarvestResource[]
				{
					new HarvestResource( 0.0, 0.0, 50.0, "You put some snow in your backpack.", typeof( Snow ) ),
					new HarvestResource( 30.0, 30.0, 100.0, "You put some frost in your backpack.", typeof( Frost ) ),
					new HarvestResource( 45.0, 45.0, 100.0, "You put some ice in your backpack.", typeof( Ice ) ),
					new HarvestResource( 55.0, 55.0, 100.0, "You put some permafrost in your backpack.", typeof( PermaFrost ) ),
					new HarvestResource( 70.0, 70.0, 100.0, "You put some glacial ice in your backpack.", typeof( GlacialIce ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 25.0, 0.0, res[0], null ),
					new HarvestVein( 25.0, 0.5, res[1], res[0] ),
					new HarvestVein( 25.0, 0.5, res[2], res[0] ),
					new HarvestVein( 15.0, 0.5, res[3], res[0] ),
					new HarvestVein( 10.0, 0.5, res[4], res[0] ),
				};

            frost.Resources = res;
            frost.Veins = veins;

            m_Definition = frost;
            Definitions.Add(frost);
            #endregion
        }

        public override bool CheckHarvest(Mobile from, Item tool)
        {
            if (!base.CheckHarvest(from, tool))
                return false;

            return true;
        }

        public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            if (!base.CheckHarvest(from, tool, def, toHarvest))
                return false;

            return true;
        }

        public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
        {
            from.SendMessage("That is not cold enough to gather any ice."); 
        }

        public static void Initialize()
        {
            Array.Sort(m_FrostTiles);
        }

        #region Tile lists
        private static int[] m_FrostTiles = new int[]
			{
                268,267,268,269,270,271,272,273,274,275,
                276,277,278,279,280,281,282,283,284,285,

                377,378,379,380,381,382,383,384,385,386,
                387,388,389,390,391,392,393,394,937,938,
                939,940,1503,1504,1505,1506,

                1861,1862,1863,1864,1865,1866,1867,1868,
                1869,1870,1871,1872,1873,1874,1875,1876,
                1878,1879,1880,1885,1886,1887,1888,1889,
                1890,1891,1892,1893,1894,1895,1896,1897,
                1898,1899,1900,1901,1902,1903,1904,1905,
                1906,1907
			};
        #endregion
    }
}

namespace Server.Items
{

    public class FrostPick : BaseHarvestTool
    {
        public override HarvestSystem HarvestSystem { get { return FrostGathering.System ; } }

        [Constructable]
        public FrostPick()
            : this(50)
        {
        }

        [Constructable]
        public FrostPick(int uses)
            : base(uses, 0x26BB)
        {
            Hue = 6;
            Name = "Frost Pick";
            Weight = 5.0;
        }

        public FrostPick(Serial serial)
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

    public abstract class BaseCold : Item, ICommodity
    {
        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set { m_Resource = value; InvalidateProperties(); }
        }

        string ICommodity.Description
        {
            get
            {
                return String.Format(Amount == 1 ? " pile of ice" : "{0} piles of ice", Amount);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((int)m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
                case 0:
                    {
                        OreInfo info = new OreInfo(reader.ReadInt(), reader.ReadInt(), reader.ReadString());

                        m_Resource = CraftResources.GetFromOreInfo(info);
                        break;
                    }
            }
        }

        public BaseCold(CraftResource resource)
            : this(resource, 1)
        {
        }

        public BaseCold(CraftResource resource, int amount)
            : base(0x09B5)
        {
            Stackable = true;
            Weight = 0.01;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);

            m_Resource = resource;
        }

        public BaseCold(Serial serial)
            : base(serial)
        {
        }
        
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!CraftResources.IsStandard(m_Resource))
            {
                int num = CraftResources.GetLocalizationNumber(m_Resource);

                if (num > 0)
                    list.Add(num);
                else
                    list.Add(CraftResources.GetName(m_Resource));
            }
        }

        public override int LabelNumber
        {
            get
            {
                if (m_Resource >= CraftResource.SpinedLeather && m_Resource <= CraftResource.BarbedLeather)
                    return 1049687 + (int)(m_Resource - CraftResource.SpinedLeather);

                return 1047023;
            }
        } 
    }

    public class Snow : Food
    {
        [Constructable]
        public Snow(int amount)
            : base(0x26B8)
        {
            Name = "Snow";
            Weight = 0.5;
            Hue = 2489;
            Stackable = true;
            FillFactor = 2;
            Amount = amount;
        }

        [Constructable]
        public Snow() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Snow(amount), amount);
        }

        public Snow(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (from.InRange(this.GetWorldLocation(), 1) && (from.Player && ((TeiravonMobile)from).IsFrostling()))
            {
                Eat(from);
            }
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
            if (version == 0)
                ItemID = 0x26B8;
        }
    }

    public class Frost : BaseCold
    {
        [Constructable]
        public Frost(int amount)
            : base(CraftResource.Frost, amount)
        {
            Name = "Frost";
            Weight = 0.2;
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public Frost() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Frost(amount), amount);
        }

        public Frost(Serial serial)
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

    public class Ice : BaseCold
    {
        [Constructable]
        public Ice(int amount)
            : base(CraftResource.Ice, amount)
        {
            Name = "Ice";
            Weight = 0.2;
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public Ice() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Ice(amount), amount);
        }

        public Ice(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            if (version == 0)
                ItemID = 0x0F91;
        }
    }

    public class PermaFrost : Item
    {
        [Constructable]
        public PermaFrost(int amount)
            : base(0x26B8)
        {
            Name = "Permafrost";
            Weight = 0.2;
            Stackable = true;
            Amount = amount;
            Hue = 2119;
        }

        [Constructable]
        public PermaFrost() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new PermaFrost(amount), amount);
        }

        public PermaFrost(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            if (version == 1)
            {
                ItemID = 0x26B8;
                Hue = 2119;
            }
        }
    }

    public class GlacialIce : BaseCold
    {
        [Constructable]
        public GlacialIce(int amount)
            : base(CraftResource.Glacial, amount)
        {
            Name = "Glacial Ice";
            Weight = 0.2;
            Hue = 0;
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public GlacialIce() : this(1) { }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GlacialIce(amount), amount);
        }

        public GlacialIce(Serial serial)
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