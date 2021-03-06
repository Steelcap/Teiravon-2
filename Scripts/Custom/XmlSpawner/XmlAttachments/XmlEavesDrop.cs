using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class XmlEavesDrop : XmlAttachment
    {
        // who owns it
        private Mobile m_Owner = null;
        private Point3D m_Location;

        // These are the various ways in which the message attachment can be constructed.  
        // These can be called via the [addatt interface, via scripts, via the spawner ATTACH keyword.
        // Other overloads could be defined to handle other types of arguments

        // a serial constructor is REQUIRED
        public XmlEavesDrop(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public XmlEavesDrop(Mobile owner)
        {
            m_Owner = owner;
            m_Location = m_Owner.Location;
        }
        public override bool HandlesOnSpeech { get { return true; } }

        public override void OnSpeech(SpeechEventArgs e)
        {
		if (m_Owner == null)
		{
		    this.Delete();
		}
            else
	    {
                if (m_Owner.Location != m_Location)
                {
                    m_Owner.SendMessage("You step away from the door and stop eavesdropping.");
                    this.Delete();
                }
                if (e.Type == MessageType.Emote)
                    return;

                Mobile from = e.Mobile;
                string speech = e.Speech;

                if (!m_Owner.InLOS(from) && m_Owner.AccessLevel >= from.AccessLevel)
                {
                    m_Owner.SendMessage(from.SpeechHue, speech);
                }
	    }
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
    }
}
