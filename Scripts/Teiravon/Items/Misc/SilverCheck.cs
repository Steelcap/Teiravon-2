using System;
using Server.Items;
using Server.Network;
using Server.Factions;

namespace Server.Items
{
    public class SilverCheck : Item
    {
  
        [Constructable]
        public SilverCheck()
            : base(0x14F0)
        {
            Name = "A Silver Cheque";
            Stackable = false;
            Weight = 0.5;
            Hue = 2926;
            LootType = LootType.Blessed;
        }

        public SilverCheck(Serial serial)
            : base(serial)
        {
        }

        public override bool DisplayLootType { get { return false; } }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 1))
            {
                from.LocalOverheadMessage(MessageType.Regular, 906, 1019045); // I can't reach that.
            }
            else
            {
                Silver money = new Silver();
                money.Amount = 10;
                from.AddToBackpack(money);
                from.SendMessage("You cache in the cheque for 10 silver coins.");
                this.Delete();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            LootType = LootType.Blessed;

            int version = reader.ReadInt();
        }
    }
}