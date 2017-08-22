using System;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using System.Collections;

namespace Server.Items
{

    public class MarketUserContainer : BaseContainer
	{
        InternalTimer distanceTimer = null;
        String m_marketName;

        public class OrderRecord : Item
        {
            public String m_market;
            public int m_type;  //1 = sell order 2 = buy order.
            public long m_uniqueID;
            public Type m_itemType;
            public int m_amountStart;
            public int m_unitPrice;
            public int m_amountCurrent;

            public int PercentComplete()
            {
                return (int)((100.0f / m_amountStart) * (m_amountStart - m_amountCurrent));
            }

            public OrderRecord(int type, String MarketName, MarketDatabase.Order order) : base( 0x1 )
		    {
                m_type = type;
                m_market = MarketName;
                Movable = false;
                m_uniqueID = order.uniqueID;
                m_itemType = order.itemType;
                m_amountStart = order.amount;
                m_amountCurrent = m_amountStart;
                m_unitPrice = order.unitPrice;
		    }

            public OrderRecord(Serial serial)
                : base(serial)
		    {
		    }

            public override void Serialize(GenericWriter writer)
		    {
			    base.Serialize( writer );

			    writer.Write( (int) 2 ); // version

                writer.Write(m_market);
                writer.Write(m_type);
                writer.Write(m_uniqueID);
                writer.Write(m_itemType == null ? null : m_itemType.FullName);

                writer.Write(m_amountStart);
                writer.Write(m_amountCurrent);
                writer.Write(m_unitPrice);
		    }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();

                m_market = reader.ReadString();
                m_type = reader.ReadInt();
                m_uniqueID = reader.ReadLong();

                string type = reader.ReadString();
                if (type != null)
                    m_itemType = ScriptCompiler.FindTypeByFullName(type);

                if (version >= 2)
                {
                    m_amountStart = reader.ReadInt();
                    m_amountCurrent = reader.ReadInt();
                    m_unitPrice = reader.ReadInt();
                }
                else
                {
                    m_amountStart = 1;
                    m_amountCurrent = 1;
                    m_unitPrice = 1;
                }
            }
        }
       
        public override bool OnDecay()
        {
            if (Owner == null)
                return true;
            else
                return false;
        }

        public override void GetContextMenuEntries(Mobile from, ArrayList list)
        {
            base.GetContextMenuEntries(from, list);
            if (from == m_owner)
                list.Add(new RemoveEntry(this));
        }

        private class RemoveEntry : ContextMenuEntry
        {
            private MarketUserContainer m_Obsidian;

            public RemoveEntry(MarketUserContainer obsidian)
                : base(6129)
            {
                m_Obsidian = obsidian;
            }

            public override void OnClick()
            {
                m_Obsidian.OnMapChange();
                m_Obsidian.Internalize();
                return;
            }
        }

        public static void AddUserOrder(MarketDatabase.Order order, String market)
        {
            MarketUserContainer authorBox = MarketDatabase.GetAccountBox(order.author as PlayerMobile,market);
            authorBox.UserOrders.Add(new OrderRecord((order is MarketDatabase.SellOrder) ? 1 : 2, market, order));
        }
        public static void RemoveUserOrder(MarketDatabase.Order order, String market)
        {
            MarketUserContainer authorBox = MarketDatabase.GetAccountBox(order.author as PlayerMobile, market);
            List<OrderRecord> UserOrders = authorBox.UserOrders;
            for (int i = 0; i < UserOrders.Count; ++i)
            {
                if (UserOrders[i].m_uniqueID == order.uniqueID && UserOrders[i].m_market == market)
                {
                    UserOrders[i].Delete();
                    UserOrders.Remove(UserOrders[i]);
                    return;
                }
            }
        }

        public static void ChangeAmountUserOrder(MarketDatabase.Order order, String market)
        {
            MarketUserContainer authorBox = MarketDatabase.GetAccountBox(order.author as PlayerMobile, market);
            List<OrderRecord> UserOrders = authorBox.UserOrders;
            for (int i = 0; i < UserOrders.Count; ++i)
            {
                if (UserOrders[i].m_uniqueID == order.uniqueID && UserOrders[i].m_market == market)
                {
                    UserOrders[i].m_amountCurrent = order.amount;
                    return;
                }
            }
        }

        public static void RemoveUserOrder(OrderRecord order)
        {
            if (order.m_type == 1)
            {
                MarketDatabase.CancelSellOrder(order.m_uniqueID, order.m_itemType, order.m_market);
            }
            else
            {
                MarketDatabase.CancelBuyOrder(order.m_uniqueID, order.m_itemType, order.m_market);
            }
        }

        public static List<OrderRecord> GetUserOrders(Mobile from, String Market)
        {
            MarketUserContainer userContainer = MarketDatabase.GetAccountBox(from as PlayerMobile, Market);
            return userContainer.UserOrders;
        }

        public List<OrderRecord> UserOrders = new List<OrderRecord>();

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner { get { return m_owner; } }

        Mobile m_owner;

		public MarketUserContainer(Mobile owner, String MarketName) : base( 0xE41 )
		{
            m_owner = owner;
            m_marketName = MarketName;
            Movable = false;
            MaxItems = 10000000;
		}

        public override int DefaultMaxWeight { get { return 100000000; } }

        public MarketUserContainer(Serial serial)
            : base(serial)
		{
            UserOrders = new List<OrderRecord>();
		}

        public override bool IsAccessibleTo(Mobile from)
        {
            if (from == m_owner || from.AccessLevel >= AccessLevel.GameMaster)
                return base.IsAccessibleTo(from);
            else
                return false;
        }

        public override bool CheckHold(Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight)
        {
            if (!base.CheckHold(m, item, message, checkItems, plusItems, plusWeight))
                return base.CheckHold(m, item, message);
            else if (m == m_owner || m.AccessLevel >= AccessLevel.GameMaster)
                return true;
            else
                return false;
        }

        public bool Summon(Mobile to)
        {
            if (distanceTimer != null)
                distanceTimer.Stop();

            int x =0, y =0;
            switch (to.Direction & Direction.Mask)
            {
                case Direction.North: --y; break;
                case Direction.South: ++y; break;
                case Direction.West: --x; break;
                case Direction.East: ++x; break;
                case Direction.Right: ++x; --y; break;
                case Direction.Left: --x; ++y; break;
                case Direction.Down: ++x; ++y; break;
                case Direction.Up: --x; --y; break;
                default:
                    break;
            }
            Point3D chestPosition = new Point3D(to.Location.X + x, to.Location.Y + y, to.Location.Z);

            if(chestPosition == Location)
                return false;

            Name = m_owner.Name + "'s " + m_marketName + " market account chest";  

            MoveToWorld(chestPosition, to.Map);
            base.DisplayTo(to);

            distanceTimer = new InternalTimer(to, to.Location, this);
            distanceTimer.Start();

            return true;
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

            writer.Write(m_owner); 
            writer.Write(m_marketName);
            writer.Write((int)UserOrders.Count);
            for (int i = 0; i < UserOrders.Count; i++)
            {
                writer.Write((OrderRecord)UserOrders[i]);
            }
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;

            m_owner = reader.ReadMobile();
            m_marketName = reader.ReadString();

            int total = reader.ReadInt();
            for (int i = 0; i < total; i++)
            {
                UserOrders.Add((OrderRecord)reader.ReadItem());
            }

            if (m_owner != null && !m_owner.Deleted)
            {
                distanceTimer = new InternalTimer(m_owner, m_owner.Location, this);
                distanceTimer.Start();
            }
		}

        private class InternalTimer : Timer
        {
            private Mobile m_mob;
            private MarketUserContainer m_box;
            private Point3D m_mobLocation;

            public InternalTimer(Mobile mob, Point3D mobLocation, MarketUserContainer box)
                : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
            {
                m_mob = mob;
                m_box = box;
                m_mobLocation = mobLocation;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (m_mob == null || m_mob.Deleted)
                {
                    m_box.Internalize();
                    Stop();
                    return;
                }
                else if (m_mob.NetState == null)
                {
                    m_box.Internalize();
                    Stop();
                    return;
                }
                if (!m_mob.InRange(m_mobLocation, 3))
                {
                    m_box.OnMapChange();
                    m_box.Internalize();
                    Stop();
                    return;
                }
            }
        }

	}
}