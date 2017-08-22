using System;
using Server;
using Server.Items;
using Server.Teiravon;

namespace Server.Mobiles
{
    public class Roller : Mobile
    { 
        private Timer m_Timer;
        public int steps = 0;
        private Point2D m_Destination;
        private TeiravonMobile m_Player;
        [Constructable]
        public Roller(Point2D Destination, TeiravonMobile Subject)
        {
            this.Name = Subject.Name;
            this.Hidden = true;
            this.Body = 0x15;
            this.Direction = Subject.Direction;
            this.AccessLevel = AccessLevel.GameMaster;
            m_Destination = Destination;
            m_Player = Subject;
            

            m_Timer = new InternalTimer(this);
            m_Timer.Start();
        }

        public Roller(Serial serial)
            : base(serial)
        {
            m_Timer = new InternalTimer(this);
            m_Timer.Start();
        }

        public override void OnDelete()
        {
            m_Timer.Stop();

            base.OnDelete();
        }

        public void CheckRoll()
        {
            Point2D loc = new Point2D(Location.X, Location.Y);
            if (loc == m_Destination && m_Player != null)
            {
                m_Player.Location = Location;
                m_Player.PlaySound(0x512);
                m_Player.Animate(22, 3, 1, false, false, 0);
            }
            Delete();
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

        private class InternalTimer : Timer
        {
            private Roller m_Owner;
            private int m_Count = 0;

            public InternalTimer(Roller owner)
                : base(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1))
            {
                m_Owner = owner;
            }

            protected override void OnTick()
            {
                m_Owner.Move(m_Owner.Direction);
                m_Owner.steps++;
                if (m_Owner.steps > 1)
                    m_Owner.CheckRoll();
            }
        }
    }
}