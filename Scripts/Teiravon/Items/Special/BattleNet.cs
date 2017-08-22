using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Teiravon;

namespace Server.Items
{
    public class BattleNet : Item, ICarvable
    {
        protected int MessageHue = 951;
        private int m_EndX, m_EndY, m_EndZ;
        private ReleaseTimer m_Timer;
        private CarveTimer n_Timer;

        private int m_Range = 14; // Maximum range
        [CommandProperty(AccessLevel.GameMaster)]
        public int Range
        {
            get { return m_Range; }
            set { m_Range = value; }
        }

        private double m_Speed = 75.0; // Flying speed of net in milliseconds
        [CommandProperty(AccessLevel.GameMaster)]
        public double Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        [Constructable]
        public BattleNet()
            : base(0x0DCA)
        {
            Name = "A sturdy net";
            Weight = 3.5;
        }

        public override void OnDoubleClick(Mobile from)
        {
            //STARTMOD: Teiravon
            if (!from.CanBeginAction(typeof(BattleNet)))
                 from.SendMessage("You cannot do this yet.");
            if (from is TeiravonMobile)
            {
                TeiravonMobile m_Player = (TeiravonMobile)from;

                if ((m_Player.IsShapeshifter() || m_Player.IsForester()) && (m_Player.Shapeshifted || m_Player.IsShifted()))
                {
                    m_Player.SendMessage("You cannot use nets while shapeshifted.");
                    return;
                }
            }

            if (IsChildOf(from.Backpack))
            {
                from.SendMessage("Who is your target?");
                from.BeginTarget(-1, true, TargetFlags.None, new TargetCallback(OnTarget));
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

            if (!from.InRange(p, m_Range) || from.Location == p)
            {
                from.SendMessage("Target is out of range");
                return;
            }
            from.Emote("*Hurls a heavy net*");
            this.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
            this.ItemID = 7845; 
            m_EndX = p.X; m_EndY = p.Y;

            int countX = Math.Abs(this.X - m_EndX);
            int countY = Math.Abs(this.Y - m_EndY);
            int count = countX;

            if (countX < countY)
                count = countY;

            Timer.DelayCall(TimeSpan.FromMilliseconds(m_Speed), TimeSpan.FromMilliseconds(m_Speed), count, new TimerStateCallback(MoveNet), new object[] { this, from });
            Timer.DelayCall(TimeSpan.FromMinutes(15.0), new TimerStateCallback(TAVUtilities.EndCooldown), new object[] { from, typeof(BattleNet) });
            from.BeginAction(typeof (BattleNet));
        }

        private void MoveNet(object state)
        {
            object[] states = (object[])state;
            BattleNet bn = (BattleNet)states[0];
            PlayerMobile pm = (PlayerMobile)states[1];


            if ((Math.Abs(bn.Y - m_EndY) == 0 || (Math.Abs(bn.X - m_EndX) / Math.Abs(bn.Y - m_EndY)) >= 2) && bn.Movable)
            {
                if ((bn.X - m_EndX) < 0)
                    bn.X++;
                else if ((bn.X - m_EndX) > 0)
                    bn.X--;
            }
            else if ((Math.Abs(bn.X - m_EndX) == 0 || (Math.Abs(bn.Y - m_EndY) / Math.Abs(bn.X - m_EndX)) > 2) && bn.Movable)
            {
                if ((bn.Y - m_EndY) < 0)
                    bn.Y++;
                else if ((bn.Y - m_EndY) > 0)
                    bn.Y--;
            }
            else if (bn.Movable)
            {
                if ((bn.X - m_EndX) < 0)
                    bn.X++;
                else if ((bn.X - m_EndX) > 0)
                    bn.X--;

                if ((bn.Y - m_EndY) < 0)
                    bn.Y++;
                else if ((bn.Y - m_EndY) > 0)
                    bn.Y--;
            }

            if (bn.Y == m_EndY && bn.X == m_EndX && bn.Movable)
            {
                bn.ItemID = 3530;
            }

            foreach (Mobile m in bn.GetMobilesInRange(0))
            {
                if (m != null && m != pm && m.CantWalk != true && m.AccessLevel <= pm.AccessLevel)
                {
                    if ((m is BaseCreature && TAVUtilities.CalculateLevel(m) < 45) || m is TeiravonMobile)
                    {
                            double duration = 12.0;
                            duration -= (TAVUtilities.CalculateLevel(m) * .25);
                            m.CantWalk = true;
                            bn.Movable = false;
                            m_Timer = new ReleaseTimer(m, bn);
                            bn.ItemID = 7843;
                            m.LocalOverheadMessage(Server.Network.MessageType.Regular, MessageHue, true, "You've been ensnared in a net!");
                            m.Emote("*Struggles under the heavy netting*");
                            //m.Location = bn.Location;
                            m_Timer.Start();
                        
                    }
                    else
                    {
                        m.Emote("*Shreds the netting effortlessly*");
                        bn.Delete();
                    }
                }
            }
        }

        public void Carve(Mobile from, Item item)
        {
            if (!this.Movable)
            {
                n_Timer = new CarveTimer(from, this);
                n_Timer.Start();
                from.Emote("*Starts to cut up the net*");
            }
        }

        private class CarveTimer : Timer
        {
            private BattleNet n_Net;
            private ReleaseTimer c_Timer;
            private Mobile c_Mobile;

            public CarveTimer(Mobile from, BattleNet net)
                : base(TimeSpan.FromSeconds(4.0))
            {
                c_Timer = net.m_Timer;
                n_Net = net;
                c_Mobile = from;
            }

            protected override void OnTick()
            {
                if (c_Timer != null && n_Net != null && c_Mobile != null && c_Mobile.InRange(n_Net.Location, 2))
                {
                    if (c_Timer.mobile != null)
                        c_Timer.mobile.CantWalk = false;
                    c_Timer.Stop();
                    if (c_Timer.net != null)
                        c_Timer.net.Delete();
                    c_Mobile.Emote("*cuts up the net*");
                }
            }
        }

        private class ReleaseTimer : Timer
        {
            private BattleNet m_BattleNet;
            private Mobile m_Mobile;

            public Mobile mobile;
            public BattleNet net;

            public ReleaseTimer(Mobile m, BattleNet bn)
                : base(TimeSpan.FromSeconds(8.0)) // 15.0 - Time the target cannot walk
            {
                m_BattleNet = bn;
                m_Mobile = m;
                mobile = m;
                net = bn;
            }

            public ReleaseTimer(Mobile m, BattleNet bn, Double duration)
                : base(TimeSpan.FromSeconds(duration)) // 15.0 - Time the target cannot walk
            {
                m_BattleNet = bn;
                m_Mobile = m;
                mobile = m;
                net = bn;
            }

            protected override void OnTick()
            {
                m_BattleNet.Movable = true;
                m_Mobile.CantWalk = false;
                m_BattleNet.ItemID = 3530;
            }
        }

        public BattleNet(Serial serial)
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