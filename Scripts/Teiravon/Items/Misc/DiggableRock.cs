using System;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Items
{
    public class DiggableRock : Item
    {
        private int m_Hits;
        [CommandProperty(AccessLevel.GameMaster)]
        public int HitPoints
        {
            get { return m_Hits; }
            set { m_Hits = value; }
        }

        [Constructable]
        public DiggableRock()
            : base(0x1363)
        {
            Movable = false;
            HitPoints = 25;
            ItemID = Utility.RandomList(4952,4957,4948);
            Weight = 20.0;
        }

        public DiggableRock(Serial serial)
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
        public void DigIt(Mobile digger)
        {
            digger.Animate(11, 5, 1, true, false, 0);
            digger.PlaySound(0x126);
            HitPoints -= 1;

            if (HitPoints < 1)
            {
                EttinRock debris = new EttinRock();
                debris.ItemID = 4970 + Utility.RandomMinMax(0, 3);
                debris.Name = "rubble";
                debris.MoveToWorld(Location,Map);
                Effects.SendLocationEffect(Location, Map, 14154, 10,10,962,4);
                Effects.PlaySound(Location,Map,0x120);
                Delete();
            }
            return;
        }
    }
}