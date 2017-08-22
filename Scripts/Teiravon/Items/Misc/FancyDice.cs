using System;
using Server;
using Server.Network;

namespace Server.Items
{
    public class FancyDice : Item
    {
        private int m_Sides;

        [CommandProperty(AccessLevel.Counselor)]
        public int Sides { get { return m_Sides; } set { m_Sides = value; } }

        [Constructable]
        public FancyDice()
            : base(0xFA7)
        {
            Sides = 6;
            Weight = 1.0;
        }

        public FancyDice(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 2))
                return;

            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("*{0} rolls {1}, on a {2} sided die.*", from.Name, Utility.Random(1, Sides), Sides));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write(m_Sides);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Sides = reader.ReadInt();
        }
    }
}