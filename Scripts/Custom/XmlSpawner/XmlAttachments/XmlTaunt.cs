using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class XmlTaunt : XmlAttachment
    {
        private Mobile m_Taunt = null;    // default data

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Taunt { get { return m_Taunt; } set { m_Taunt = value; } }

        // These are the various ways in which the message attachment can be constructed.  
        // These can be called via the [addatt interface, via scripts, via the spawner ATTACH keyword.
        // Other overloads could be defined to handle other types of arguments

        // a serial constructor is REQUIRED
        public XmlTaunt(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public XmlTaunt(Mobile taunt)
        {
            Taunt = taunt;
            Name = taunt.Name;
            Expiration = TimeSpan.FromSeconds(45);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            // version 0
            writer.Write((Mobile)m_Taunt);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            // version 0
            m_Taunt = reader.ReadMobile();
        }
    }
}
