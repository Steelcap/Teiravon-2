using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
    public abstract class TAVOre : BaseOre
    {
        private CraftResource m_Resource;
        private MetalConcentration[] m_MetalCon;
        
        public override abstract BaseIngot GetIngot();
        public MetalConcentration[] MetalCon { get { return m_MetalCon; } }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public TAVOre(CraftResource resource)
            : this(resource, 1)
        {
        }

        public TAVOre(CraftResource resource, int amount)
            : base(resource)
        {
            ItemID = 0x19B9;
            Stackable = true;
            Weight = 3.0;
            Amount = amount;
            Hue = CraftResources.GetHue(resource);

            m_Resource = resource;
        }

        public TAVOre(Serial serial)
            : base(serial)
        {
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(1050039, "{0}\t#{1}", Amount, 1026583); // ~1_NUMBER~ ~2_ITEMNAME~
            else
                list.Add(1026583); // ore
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
                if (m_Resource >= CraftResource.DullCopper && m_Resource <= CraftResource.Ithilmar)
                    return 1042845 + (int)(m_Resource - CraftResource.DullCopper);

                return 1042853; // iron ore;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (from.InRange(this.GetWorldLocation(), 2))
            {
                from.SendLocalizedMessage(501971); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(501976); // The ore is too far away.
            }
        }

        private class InternalTarget : Target
        {
            private TAVOre m_Ore;

            public InternalTarget(TAVOre ore)
                : base(2, false, TargetFlags.None)
            {
                m_Ore = ore;
            }

            private bool IsForge(object obj)
            {
                if (obj.GetType().IsDefined(typeof(ForgeAttribute), false))
                    return true;

                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;
                else if (obj is StaticTarget)
                    itemID = ((StaticTarget)obj).ItemID & 0x3FFF;

                return (itemID == 4017 || (itemID >= 6522 && itemID <= 6569));
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Ore.Deleted)
                    return;

                if (!from.InRange(m_Ore.GetWorldLocation(), 2))
                {
                    from.SendLocalizedMessage(501976); // The ore is too far away.
                    return;
                }

                if (IsForge(targeted))
                {
                    from.CheckSkill(SkillName.Mining, 0,120);
                    int toConsume = m_Ore.Amount;
                    double Volume = 0;
                    int count = 0;
                    double Success = (double)((from.Skills.Mining.Value + 20 - Utility.Random(40)) / 100);
                    //from.Say("success value : {0}%", Success* 100);   -- Debug Text for Smelting

                    for (int i = 0; i < GetLength(m_Ore); ++i)
                    {
                        if (targeted is DwarvenForgeAddon || targeted is DwarvenForge)
                        {
                            if (i == (GetLength(m_Ore) -1))
                            {
                                Volume = (toConsume * Success * 4.0 * GetCon(i, m_Ore));
                            }
                            else
                                Volume = (toConsume * Success * GetCon(i, m_Ore));

                            from.SendMessage("Quantity : {0} {1}", Volume, GetMetals(i, m_Ore).Name);
                        }
                        else
                        {
                            Volume = (int)(toConsume * Success * GetCon(i, m_Ore));
                            from.SendMessage("Quantity : {0} of {1}", Volume, GetMetals(i, m_Ore).Name);
                        }
                        if (Volume > 0)
                        {
                            Item ingot = GetMetals(i, m_Ore);
                            ingot.Amount = (int)Volume;
                            from.AddToBackpack(ingot);
                            
                            count++;
                        }
                    }
                    
                        if (count <= 0)
                            from.SendLocalizedMessage(501987); // There is not enough metal-bearing ore in this pile to make an ingot.
                        else
                            from.SendLocalizedMessage(501988); // You smelt the ore removing the impurities and put the metal in your backpack.

                        if (toConsume < 1)
                            toConsume = 1;

                        m_Ore.Consume(toConsume);
                }
            }
        }

        public class MetalConcentration
        {
            private Type m_Metal;
            private double m_Concentration;

            public Type Metal { get { return m_Metal; } }
            public double Concentration { get { return m_Concentration; } }

            public MetalConcentration(double concentration, Type metal)
            {
                m_Metal = metal;
                m_Concentration = concentration;
            }
        }

        public static Item GetMetals(int index, TAVOre res)
        {
            MetalConcentration[] m_MetalCon;
            TAVOre Res = res;

            MetalConcentration[] m_FeriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(2.4, typeof(IronIngot)),
                    new MetalConcentration(0.4, typeof(BronzeIngot)),
                    new MetalConcentration(0.2, typeof(SilverIngot)),
                    new MetalConcentration(0.3, typeof(AgapiteIngot))
                };

            MetalConcentration[] m_MalachiteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(DullCopperIngot)),
                    new MetalConcentration(0.95, typeof(CopperIngot)),
                    new MetalConcentration(0.30, typeof(VeriteIngot))
                };

            MetalConcentration[] m_PyriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(IronIngot)),
                    new MetalConcentration(0.35, typeof(BronzeIngot)),
                    new MetalConcentration(0.65, typeof(GoldIngot)),
                    new MetalConcentration(0.15, typeof(ValoriteIngot))
                };

            MetalConcentration[] m_UmbriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(DullCopperIngot)),
                    new MetalConcentration(1.2, typeof(ShadowIronIngot)),
                    new MetalConcentration(0.075, typeof(BlackrockIngot))
                };

            MetalConcentration[] m_AmiriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(1.0, typeof(IronIngot)),
                    new MetalConcentration(1.10, typeof(SilverIngot)),
                    new MetalConcentration(0.375, typeof(AgapiteIngot)),
                    new MetalConcentration(0.025, typeof(MithrilIngot))
                };

            switch ( res.Resource)
            {
                default: m_MetalCon = m_FeriteCon; break;
                case CraftResource.Ferite: m_MetalCon = m_FeriteCon; break;
                case CraftResource.Malachite: m_MetalCon = m_MalachiteCon; break;
                case CraftResource.Pyrite: m_MetalCon = m_PyriteCon; break;
                case CraftResource.Umbrite: m_MetalCon = m_UmbriteCon; break;
                case CraftResource.Amirite: m_MetalCon = m_AmiriteCon; break;

            }

            try { return (Item)Activator.CreateInstance(m_MetalCon[index].Metal); }
            catch { return null; }
        }

        public static double GetCon(int index, TAVOre res)
        {
            MetalConcentration[] m_MetalCon;
            TAVOre Res = res;

            MetalConcentration[] m_FeriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(2.4, typeof(IronIngot)),
                    new MetalConcentration(0.4, typeof(BronzeIngot)),
                    new MetalConcentration(0.2, typeof(SilverIngot)),
                    new MetalConcentration(0.3, typeof(AgapiteIngot))
                };

            MetalConcentration[] m_MalachiteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(DullCopperIngot)),
                    new MetalConcentration(0.95, typeof(CopperIngot)),
                    new MetalConcentration(0.30, typeof(VeriteIngot))
                };

            MetalConcentration[] m_PyriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(IronIngot)),
                    new MetalConcentration(0.35, typeof(BronzeIngot)),
                    new MetalConcentration(0.65, typeof(GoldIngot)),
                    new MetalConcentration(0.15, typeof(ValoriteIngot))
                };

            MetalConcentration[] m_UmbriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(DullCopperIngot)),
                    new MetalConcentration(1.2, typeof(ShadowIronIngot)),
                    new MetalConcentration(0.075, typeof(BlackrockIngot))
                };

            MetalConcentration[] m_AmiriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(1.0, typeof(IronIngot)),
                    new MetalConcentration(1.10, typeof(SilverIngot)),
                    new MetalConcentration(0.375, typeof(AgapiteIngot)),
                    new MetalConcentration(0.025, typeof(MithrilIngot))
                };

            switch (res.Resource)
            {
                default: m_MetalCon = m_FeriteCon; break;
                case CraftResource.Ferite: m_MetalCon = m_FeriteCon; break;
                case CraftResource.Malachite: m_MetalCon = m_MalachiteCon; break;
                case CraftResource.Pyrite: m_MetalCon = m_PyriteCon; break;
                case CraftResource.Umbrite: m_MetalCon = m_UmbriteCon; break;
                case CraftResource.Amirite: m_MetalCon = m_AmiriteCon; break;
            }

            return m_MetalCon[index].Concentration;
        }

        public static int GetLength(TAVOre res)
        {
            MetalConcentration[] m_MetalCon;
            TAVOre Res = res;

            MetalConcentration[] m_FeriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(2.4, typeof(IronIngot)),
                    new MetalConcentration(0.4, typeof(BronzeIngot)),
                    new MetalConcentration(0.2, typeof(SilverIngot)),
                    new MetalConcentration(0.3, typeof(AgapiteIngot))
                };

            MetalConcentration[] m_MalachiteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(DullCopperIngot)),
                    new MetalConcentration(0.95, typeof(CopperIngot)),
                    new MetalConcentration(0.30, typeof(VeriteIngot))
                };

            MetalConcentration[] m_PyriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(IronIngot)),
                    new MetalConcentration(0.35, typeof(BronzeIngot)),
                    new MetalConcentration(0.65, typeof(GoldIngot)),
                    new MetalConcentration(0.15, typeof(ValoriteIngot))
                };

            MetalConcentration[] m_UmbriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(.7, typeof(DullCopperIngot)),
                    new MetalConcentration(1.2, typeof(ShadowIronIngot)),
                    new MetalConcentration(0.075, typeof(BlackrockIngot))
                };

            MetalConcentration[] m_AmiriteCon = new MetalConcentration[]
                {
                    new MetalConcentration(1, typeof(IronIngot)),
                    new MetalConcentration(1.10, typeof(SilverIngot)),
                    new MetalConcentration(0.375, typeof(AgapiteIngot)),
                    new MetalConcentration(0.025, typeof(MithrilIngot))
                };

            switch (res.Resource)
            {
                default: m_MetalCon = m_FeriteCon; break;
                case CraftResource.Ferite: m_MetalCon = m_FeriteCon; break;
                case CraftResource.Malachite: m_MetalCon = m_MalachiteCon; break;
                case CraftResource.Pyrite: m_MetalCon = m_PyriteCon; break;
                case CraftResource.Umbrite: m_MetalCon = m_UmbriteCon; break;
                case CraftResource.Amirite: m_MetalCon = m_AmiriteCon; break;

            }

             return m_MetalCon.Length;
        }




        public class FeriteOre : TAVOre
        {
 

            [Constructable]
            public FeriteOre()
                : this(1)
            {

            }

            [Constructable]
            public FeriteOre(int amount)
                : base(CraftResource.Ferite, amount)
            {
            }

            public FeriteOre(Serial serial)
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

            public override Item Dupe(int amount)
            {
                return base.Dupe(new FeriteOre(amount), amount);
            }

            public override BaseIngot GetIngot()
            {
                return new IronIngot();
            }
        }

        public class MalachiteOre : TAVOre
        {
            [Constructable]
            public MalachiteOre()
                : this(1)
            {

            }

            [Constructable]
            public MalachiteOre(int amount)
                : base(CraftResource.Malachite, amount)
            {
            }

            public MalachiteOre(Serial serial)
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

            public override Item Dupe(int amount)
            {
                return base.Dupe(new MalachiteOre(amount), amount);
            }

            public override BaseIngot GetIngot()
            {
                return new IronIngot();
            }
        }

        public class PyriteOre : TAVOre
        {


            [Constructable]
            public PyriteOre()
                : this(1)
            {

            }

            [Constructable]
            public PyriteOre(int amount)
                : base(CraftResource.Pyrite, amount)
            {
            }

            public PyriteOre(Serial serial)
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

            public override Item Dupe(int amount)
            {
                return base.Dupe(new PyriteOre(amount), amount);
            }

            public override BaseIngot GetIngot()
            {
                return new IronIngot();
            }
        }

        public class UmbriteOre : TAVOre
        {


            [Constructable]
            public UmbriteOre()
                : this(1)
            {

            }

            [Constructable]
            public UmbriteOre(int amount)
                : base(CraftResource.Umbrite, amount)
            {
            }

            public UmbriteOre(Serial serial)
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

            public override Item Dupe(int amount)
            {
                return base.Dupe(new UmbriteOre(amount), amount);
            }

            public override BaseIngot GetIngot()
            {
                return new IronIngot();
            }
        }

        public class AmiriteOre : TAVOre
        {


            [Constructable]
            public AmiriteOre()
                : this(1)
            {

            }

            [Constructable]
            public AmiriteOre(int amount)
                : base(CraftResource.Amirite, amount)
            {
            }

            public AmiriteOre(Serial serial)
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

            public override Item Dupe(int amount)
            {
                return base.Dupe(new AmiriteOre(amount), amount);
            }

            public override BaseIngot GetIngot()
            {
                return new IronIngot();
            }
        }
    }
}
