using System;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Items
{
    public class PerkPoint : Item
    {
        		[Constructable]
        public PerkPoint()
            : base(0xF21)
		{
			Stackable = false;
			Weight = 0.1;
            Name = "A Perk Point";
		}

                public PerkPoint(Serial serial)
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

                public override void OnAdded(object parent)
                {
                    base.OnAdded(parent);

                    if (parent != null && parent is Container)
                    {
                        // find the parent of the container
                        // note, the only valid additions are to the player pack.  Anything else is invalid.  This is to avoid exploits involving storage or transfer of questtokens
                        object from = ((Container)parent).Parent;

                        // check to see if it can be added
                        if (from != null && from is TeiravonMobile)
                        {
                            TeiravonMobile owner = from as TeiravonMobile;
                            owner.PerkPoints++;
                            owner.SendMessage("You've gained a Perk Point.");
                            Delete();
                            return;
                        }
                    
                    }
                }
    }
}
