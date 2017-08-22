using System;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Items
{
    public class ExpBall5 : Item
    {
        [Constructable]
        public ExpBall5()
            : base(0xF21)
        {
            Stackable = false;
            Weight = 0.1;
            Name = "An Exp Orb";
        }

        public ExpBall5(Serial serial)
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
                    if (owner.PlayerLevel < 20)
                    {
                        int exp = (50 * 1000);
                        Misc.Titles.AwardExp(owner, exp);
                        owner.SendMessage("You have gained {0}) experience", exp);
                        Delete();
                        return;
                    }
                    
                }

            }
        }
    }

    public class ExpBall10 : Item
    {
        [Constructable]
        public ExpBall10()
            : base(0xF21)
        {
            Stackable = false;
            Weight = 0.1;
            Name = "An Exp Orb";
        }

        public ExpBall10(Serial serial)
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
                    if (owner.PlayerLevel < 20)
                    {
                        int exp = (250 * 1000);
                        Misc.Titles.AwardExp(owner, exp);
                        owner.SendMessage("You have gained {0}) experience", exp);
                        Delete();
                        return;
                    }

                }

            }
        }
    }

    public class ExpBall20 : Item
    {
        [Constructable]
        public ExpBall20()
            : base(0xF21)
        {
            Stackable = false;
            Weight = 0.1;
            Name = "An Exp Orb";
        }

        public ExpBall20(Serial serial)
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
                    if (owner.PlayerLevel < 20)
                    {
                        int exp = (1000 * 1000);
                        Misc.Titles.AwardExp(owner, exp);
                        owner.SendMessage("You have gained {0}) experience", exp);
                        Delete();
                        return;
                    }

                }

            }
        }
    }

    public class ExpBall40 : Item
    {
        [Constructable]
        public ExpBall40()
            : base(0xF21)
        {
            Stackable = false;
            Weight = 0.1;
            Name = "An Exp Orb";
        }

        public ExpBall40(Serial serial)
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
                    if (owner.PlayerLevel < 20)
                    {
                        int exp = (4000 * 1000);
                        Misc.Titles.AwardExp(owner, exp);
                        owner.SendMessage("You have gained {0}) experience", exp);
                        Delete();
                        return;
                    }

                }

            }
        }
    }
}
