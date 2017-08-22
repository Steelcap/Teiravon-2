using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class TavRotten : XmlAttachment
    {
        private double m_Value;

        public double Value { get { return m_Value; } set { m_Value = value; } }

        public TavRotten(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public TavRotten(string name, double value, double expiresin)
        {
            Name = name;
            Value = value;
            Expiration = TimeSpan.FromSeconds(expiresin);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            // version 0

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            // version 0
        }

        public override string OnIdentify(Mobile from)
        {
            if (from == null || from.AccessLevel == AccessLevel.Player) return null;

            if (Expiration > TimeSpan.Zero)
            {
                return String.Format("{2}: expires in {1} seconds", Expiration.TotalSeconds, Name);
            }
            else
            {
                return String.Format("{1}", Name);
            }
        }
    }
}
