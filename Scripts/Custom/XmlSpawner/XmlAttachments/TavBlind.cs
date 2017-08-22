using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class TavBlind : XmlAttachment
    {
        // a serial constructor is REQUIRED
        public TavBlind(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public TavBlind()
        {
        }

        [Attachable]
        public TavBlind(string name, double seconds)
        {
            Name = name;
            Expiration = TimeSpan.FromSeconds(seconds);
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

        public override void OnDelete()
        {
            if (Owner is Mobile)
            {
                Mobile m = (Mobile)Owner;
                if( m.Deleted || m == null)
                    return;
                m.CloseGump(typeof(BlindGump));
            }
            base.OnDelete();
        }

        public override void OnAttach()
        {
            if (Owner is Mobile)
            {
                Mobile m = (Mobile)Owner;
                if (m.Deleted || m == null)
                    return;

                m.SendGump(new BlindGump());
            }
            base.OnAttach();
        }
    }
}
