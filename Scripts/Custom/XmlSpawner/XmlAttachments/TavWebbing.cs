using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class TavWebbing : XmlAttachment
    {
        private int m_Level;  // How webbed they are.

        [CommandProperty(AccessLevel.GameMaster)]
        public int Level { get { return m_Level; } set { m_Level = value; } }
        // These are the various ways in which the message attachment can be constructed.  
        // These can be called via the [addatt interface, via scripts, via the spawner ATTACH keyword.
        // Other overloads could be defined to handle other types of arguments

        // a serial constructor is REQUIRED
        public TavWebbing(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public TavWebbing()
        {
        }

        [Attachable]
        public TavWebbing(double seconds)
        {
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

        public override string OnIdentify(Mobile from)
        {
            base.OnIdentify(from);

            if (from == null || from.AccessLevel == AccessLevel.Player) return null;

            if (Expiration > TimeSpan.Zero)
            {
                return String.Format("Webbing level {0}, expires in {1} secs", Expiration.TotalSeconds);
            }
            else
            {
                return String.Format("Webbed");
            }
        }

        public override void OnDelete()
        {
            base.OnDelete();
            if (Owner is BaseCreature)
                ((BaseCreature)Owner).CurrentSpeed = ((BaseCreature)Owner).ActiveSpeed;
            ((Mobile)Owner).Send(Server.Network.SpeedMode.Disabled);
        }

        public override void OnAttach()
        {
            ((Mobile)Owner).Send(Server.Network.SpeedMode.Walk);
            if (Owner is BaseCreature)
                ((BaseCreature)Owner).CurrentSpeed = ((BaseCreature)Owner).PassiveSpeed;
            base.OnAttach();
        }
        
        public void DoWebbing()
        {
            m_Level++;

            if (m_Level >= 5)
            {
                ((Mobile)Owner).Freeze(TimeSpan.FromSeconds(3));
                ((Mobile)Owner).FixedParticles(0x10DA, 1, 30, 2, EffectLayer.Waist);
                Delete();
            }
        }
    }
}
