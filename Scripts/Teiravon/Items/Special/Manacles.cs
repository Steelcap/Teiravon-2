using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Teiravon;

namespace Server.Items
{
    class Manacles : Item
    {
        private bool m_Set;
        private Timer m_Escape;
        private int m_Attempts;
        private Timer m_Apply;

        [Constructable]
        public Manacles()
            : base(0x1A07)
        {
            Name = "A set of manacles";
            Weight = 5;
        }
        public Manacles(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)m_Attempts);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Attempts = reader.ReadInt();
        }

        public bool CheckCaught()
        {
            if (m_Escape != null)
            {
                m_Attempts = 0;
                return true;
            }
            else return false;
        }

        public void DoShackle(Mobile from, Mobile m)
        {
            TeiravonMobile m_Player = from as TeiravonMobile;
            from.Emote("*Tries to place heavy shackles on {0}*", m.Name);
            m_Apply = new ApplyTimer(m_Player, m, this);
            m_Apply.Start();
        }

        public void StartEscape(Mobile from)
        {
            TeiravonMobile m = from as TeiravonMobile;
            m_Escape = new EscapeTimer(m, this);
            m_Escape.Start();
            m.Emote("*Struggles against their shackles*");
        }

        public override void OnDoubleClick(Mobile from)
        {
            //if (IsChildOf(from.Backpack))
            //{
            //    from.SendMessage("Who will you attempt to shackle?");
            //    from.BeginTarget(1, false, TargetFlags.None, new TargetCallback(OnTarget));
            //}
            //else if (m_Set) from.SendMessage("These shackles are firmly attached already.");
            //else from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
        }

        private void OnTarget(Mobile from, object obj)
        {
            if (Deleted)
                return;

            IPoint3D p = obj as IPoint3D;
            if (p == null)
                return;
            if (obj is Mobile)
            {
                Mobile m = obj as Mobile;
                from.ClearHands();
		from.RevealingAction();
                DoShackle(from, m);
            }

        }

        private class EscapeTimer : Timer
        {
            private Manacles chains;
            private TeiravonMobile m_Player;

            public EscapeTimer(TeiravonMobile from, Manacles manacles)
                : base(TimeSpan.FromSeconds(10))
            {
                m_Player = from;
                chains = manacles;
            }

            protected override void OnTick()
            {
                double chance = .015 * chains.m_Attempts;
                if (Utility.RandomDouble() < chance)
                {
                    m_Player.SendMessage("You've freed yourself from the shackles! Sweet freedom.");
                    m_Player.AddItem(chains);
                    chains.Layer = Layer.Invalid;
                    chains.m_Attempts = 0;
                    chains.Movable = true;
                    m_Player.Send(new SpeedMode(0));
                }
            }
        }

        private class ApplyTimer : Timer
        {
            private TeiravonMobile m_Player;
            private Mobile m_Targ;
            private Point3D startLoc;
            private Manacles chains;

            public ApplyTimer(TeiravonMobile from, Mobile m, Manacles shackles)
                : base(TimeSpan.FromSeconds(3))
            {
                chains = shackles;
                m_Player = from;
                m_Targ = m;
                startLoc = m.Location;
            }

            protected override void OnTick()
            {
                //m_Player.SendMessage("Start Location: {0} , Current Location: {1}", startLoc.ToString(), m_Targ.Location.ToString());
                
                if (m_Player.Deleted || m_Targ.Deleted || chains.Deleted)
                {
                    return;
                }
                else if (!m_Player.Alive || !m_Targ.Alive)
                {
                    m_Player.SendMessage("Your attempt to shackle them has become pointless.");
                    return;
                }
                else if (m_Targ.Location != startLoc)
                {
                    m_Player.SendMessage("You were unable to keep your target restrained while you applied the shackles.");
                    return;
                }
                else
                {
                    m_Player.Emote("*Shackles {0}, rendering them helpless*", m_Targ.Name);
                    m_Targ.SendMessage("You've been shackled!");
                    m_Targ.ClearHands();
                    chains.Layer = Layer.TwoHanded;
                    chains.Movable = false;
                    chains.m_Attempts = 0;
                    m_Targ.EquipItem(chains);
                    m_Targ.Send(new SpeedMode(2));
                }
            }
        }
    }

    class ManacleKey : Item
    {
        [Constructable]
        public ManacleKey()
            : base(0x1010)
        {
            Name = "A manacle key";
            Weight = .2;
        }
        public ManacleKey(Serial serial)
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

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.SendMessage("Who will you attempt to release from their shackles?");
                from.BeginTarget(1, false, TargetFlags.None, new TargetCallback(OnTarget));
            }
            else from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
        }

        private void OnTarget(Mobile from, object obj)
        {
            if (Deleted)
                return;
            
            IPoint3D p = obj as IPoint3D;
            if (p == null)
                return;

            if (obj is Mobile)
            {
                Mobile m = obj as Mobile;


                Item i = m.FindItemOnLayer(Layer.TwoHanded);

                if (i != null && i is Manacles)
                {
                    Manacles manacles = i as Manacles;
                    from.AddToBackpack(manacles);
                    from.SendMessage("You release them from their restraints.");
                    m.SendMessage("You have been freed from your restraints.");
                    m.Send(new SpeedMode(0));
                    manacles.Layer = Layer.Invalid;
                    manacles.CheckCaught();
                    manacles.Movable = true;
                    return;
                }
                else
                {
                    from.SendMessage("They are not restrained.");
                    return;
                }

            }

        }
    }
}
