using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Engines.XmlSpawner2
{
    class TavGenome : XmlAttachment
    {
        private bool[,,] m_Genome;

        public bool[,,] Genome{ get { return m_Genome; } set { m_Genome = value; } }

        public TavGenome(ASerial serial)
            : base(serial)
        {
        }

        private int GetPhenome(int phenome)
        {
            int total = 0;
            for (int j = 0; j < 3; ++j)
            {
                if (m_Genome[phenome,j,0] && m_Genome[phenome,j,1])
                    total += 1;
            }
            return total;
        }

        #region props
        [CommandProperty(AccessLevel.GameMaster)]
        public int Hp
        { get{return GetPhenome(0);}}
        [CommandProperty(AccessLevel.GameMaster)]
        public int Stam
        { get { return GetPhenome(1); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Str
        { get { return GetPhenome(2); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Dex
        { get { return GetPhenome(3); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Int
        { get { return GetPhenome(4); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int DamageMin
        { get { return GetPhenome(5); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int DamageMax
        { get { return GetPhenome(6); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int PhysResist
        { get { return GetPhenome(7); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int FireResist
        { get { return GetPhenome(8); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ColdResist
        { get { return GetPhenome(9); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int PoisonResist
        { get { return GetPhenome(10); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int EnergyResist
        { get { return GetPhenome(11); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Wrestling
        { get { return GetPhenome(12); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Tactics
        { get { return GetPhenome(13); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int MagicResist
        { get { return GetPhenome(14); } }
        [CommandProperty(AccessLevel.GameMaster)]
        public int Anatomy
        { get { return GetPhenome(15); } }
        #endregion

        [Attachable]
        public TavGenome()
        {
            m_Genome = new bool[,,]
			{
				 // HP phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Stamina phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Str phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Dex phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Int phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // MinDamage phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // MaxDamage phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // PhysResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                //FireResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                //ColdResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // PoisonResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // EnergyResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // WrestlingCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // TacticsCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // MagicResistCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // AnatomyCap phenotype
                {
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                }
			};

            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        m_Genome[i,j,k] = Utility.RandomBool();
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        writer.Write((bool)m_Genome[i,j,k]);
                    }
                }
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Genome = new bool[,,]
			{
				 // HP phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Stamina phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Str phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Dex phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // Int phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // MinDamage phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // MaxDamage phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // PhysResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                //FireResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                //ColdResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // PoisonResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                // EnergyResist phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // WrestlingCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // TacticsCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // MagicResistCap phenotype
                { 
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                },
                 // AnatomyCap phenotype
                {
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()},
                    {Utility.RandomBool(),Utility.RandomBool()}
                }
			};

            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 2; ++k)
                    {
                        bool aliel = reader.ReadBool();
                        m_Genome[i,j,k] = aliel;
                    }
                }
            }
        }
    }
}
