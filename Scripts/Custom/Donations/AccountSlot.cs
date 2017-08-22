using System;
using Server.Mobiles;
using Server.Teiravon;
using Server.Scripts.Commands;
using Server.Accounting;

namespace Server.Items
{
    public class AccountSlot : Item
    {
        [Constructable]
        public AccountSlot()
            : base(0xF21)
        {
            Stackable = false;
            Weight = 0.1;
            Name = "An Account Slot";
        }

        public AccountSlot(Serial serial)
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
                    Account a = (Account)owner.Account;
                     if (a.GetTag("5Slot") != null)
                        {  
                         owner.SendMessage("You've made a terrible mistake.");
                         CommandLogging.WriteLine(owner, " Donation ERROR");
                         Delete();
                            return;
                        }
                     else{
                        if (a.GetTag("4Slot") != null)
                        {   
                           a.SetTag("5Slot", "1");
                        }
                        else
                            a.SetTag("4Slot", "1");
                         }

                    owner.SendMessage("You've gained an Account Slot.");
                   // DonationLogging.WriteLine(owner, " Donation Claimed:  {2}:{3}", this.Name, this.Serial);
                    Delete();
                    return;
                }

            }
        }
    }
}
