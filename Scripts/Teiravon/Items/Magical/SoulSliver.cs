using System;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;
using Server.Teiravon;


namespace Server.Items
{
    public class SoulSliver : Item
    {
        private string m_Description;
        private bool m_Marked;
        private Mobile m_Target;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((string)m_Description);
            writer.Write((bool)m_Marked);
            writer.Write((Mobile)m_Target);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Description = reader.ReadString();
            m_Marked = reader.ReadBool();
            m_Target = reader.ReadMobile();

            CalculateHue();
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public bool Marked
        {
            get
            {
                return m_Marked;
            }
            set
            {
                if (m_Marked != value)
                {
                    m_Marked = value;
                    CalculateHue();
                    InvalidateProperties();
                }
            }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Target
        {
            get
            {
                return m_Target;
            }
            set
            {
                m_Target = value;
            }
        }

        private void CalculateHue()
        {
            if (!m_Marked)
                Hue = 2292;

	    else
		{
if (m_Target == null)
	Hue = 2585;
            else if (m_Target.Karma > 8000)
                Hue = 2991;
            else if (m_Target.Karma > 3000)
                Hue = 2648;
            else if (m_Target.Karma < -3000)
                Hue = 2626;
            else
                Hue = 2585;
}

        }

        public void Mark(Mobile m)
        {
            m_Marked = true;
            m_Target = m;
            Name = "The Soul of " + (string)m_Target.Name;
            CalculateHue();
            InvalidateProperties();
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Marked)
            {
                string desc;

                if ((desc = m_Description) == null || (desc = desc.Trim()).Length == 0)
                    desc = "an unknown location";
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            if (m_Marked)
            {
                string desc;

                if ((desc = m_Description) == null || (desc = desc.Trim()).Length == 0)
                    desc = "The Soul of " + (string)m_Target.Name;
            }
            else
            {
                LabelTo(from, "an unused soul sliver");
            }
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else
            {
                this.Mark(from);
                from.SendMessage("The sliver draws a trace of your soul into its depths.");
            }

        }

        [Constructable]
        public SoulSliver()
            : base(0x3155)
        {
            Name = "A Soul Sliver";
            Weight = 1.0;
            CalculateHue();
        }

        public SoulSliver(Serial serial)
            : base(serial)
        {
        }
    }
}