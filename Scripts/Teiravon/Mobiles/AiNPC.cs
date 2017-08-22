using System;
using System.Data;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Gumps;
using Server.Targeting;
using System.Reflection;
using Server.Scripts.Commands;
using CPA = Server.CommandPropertyAttribute;
using System.Xml;
using Server.Spells;
using System.Text;
using Server.Accounting;
using System.Diagnostics;



namespace Server.Mobiles
{

    public class AiNPC : XmlQuestNPC
    {
        public override bool PlayerRangeSensitive{get{return false;}}
        
        [Constructable]
        public AiNPC()
            : base(-1, false)
        {
            BodyValue = 400;
            Name = "Always Active AI Dummy";
        }

        public AiNPC(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

        }

    }
}