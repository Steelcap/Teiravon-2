using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Reflection;

namespace Server.Engines.XmlSpawner2
{
    public class TAVFlourish : XmlAttachment
    {
        private WeaponAbility m_Ability = null;    // default data

        [CommandProperty(AccessLevel.GameMaster)]
        public WeaponAbility WeaponAbility
        {
            get { return m_Ability; }
            set { m_Ability = value; }
        }

        // These are the various ways in which the message attachment can be constructed.  
        // These can be called via the [addatt interface, via scripts, via the spawner ATTACH keyword.
        // Other overloads could be defined to handle other types of arguments

        // a serial constructor is REQUIRED
        public TAVFlourish(ASerial serial)
            : base(serial)
        {
        }
        
        [Attachable]
        public TAVFlourish(int weaponability)
        {
            m_Ability = WeaponAbility.Abilities[weaponability];
        }

        [Attachable]
        public TAVFlourish(string name, int weaponability)
        {
            Name = name;
            m_Ability = WeaponAbility.Abilities[weaponability];
        }

        [Attachable]
        public TAVFlourish(string name, int weaponability, double expiresin)
        {
            Name = name;
            m_Ability = WeaponAbility.Abilities[weaponability];
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
            this.Delete();
        }
    }
}
