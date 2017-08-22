using System;
using Server;
using Server.Targeting;
using System.Collections;

namespace Server.Items
{
    public class XmlExporter
    {
        public static Hashtable Exports = new Hashtable();
    }
    
    public class ExportController : Item
    {
        private string m_p1;
        private string m_p2;
        private string m_p3;
        private string m_p4;


        [CommandProperty(AccessLevel.GameMaster)]
        public string P1
        { get { return m_p1; } set { m_p1 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string P2
        { get { return m_p2; } set { m_p2 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string P3
        { get { return m_p3; } set { m_p3 = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string P4
        { get { return m_p4; } set { m_p4 = value; } }

        [Constructable]
        public ExportController()
            : base(0x1F20)
        {
            this.Hue = 0x498;
            this.Visible = false;
            this.Name = "An Xml Exporter";
            this.Movable = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
            {
                if (!XmlExporter.Exports.Contains(this))
                {
                    XmlExporter.Exports.Add(this, this);
                    from.SendMessage("Exporter is now reporting");
                    ItemID = 0x1F1F;
                }
                else
                {
                    XmlExporter.Exports.Remove(this);
                    from.SendMessage("Exporter is no longer reporting");
                    ItemID = 0x1F20;
                }

            }
        }
        public override void OnDelete()
        {
            if (XmlExporter.Exports.Contains(this))
                XmlExporter.Exports.Remove(this);
            base.OnDelete();
        }

        public ExportController(Serial serial)
            : base(serial)
        {

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_p1 = reader.ReadString();
            m_p2 = reader.ReadString();
            m_p3 = reader.ReadString();
            m_p4 = reader.ReadString();
            
            if (ItemID == 0x1F1F)
                XmlExporter.Exports.Add(this, this);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write((String)P1);
            writer.Write((String)P2);
            writer.Write((String)P3);
            writer.Write((String)P4);
        }
    }
}
