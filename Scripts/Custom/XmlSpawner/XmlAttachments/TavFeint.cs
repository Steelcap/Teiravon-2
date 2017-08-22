using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class TavFeint : XmlAttachment
    {
        public TavFeint(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public TavFeint(string name, double expiresin)
        {
            Name = name;
            Expiration = TimeSpan.FromMinutes(expiresin);
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
                return String.Format("{2}: expires in {1} mins",Expiration.TotalMinutes, Name);
            }
            else
            {
                return String.Format("{1}", Name);
            }
        }
    }
}
