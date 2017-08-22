using System;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using System.Collections;
using System.Collections.Generic;
using Server.Accounting;
using Server.Gumps.MarketGump;
using Server.Targeting;
using Server.Prompts;
using Server.Network;
using System.Threading;
using System.Reflection;

namespace Server.Items
{
	public class MarketDatabase : Container
	{
        public class SellOrderTarget : Target
        {
            private String m_Market;

            public SellOrderTarget(String Market)
                : base(30, true, TargetFlags.None)
            {
                m_Market = Market;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if (targ is Container && ((Container)targ).Items.Count > 0)
                {
                    from.SendMessage("Containers holding objects cannot be sold.");
                    return;
                }

                if (targ is Item)
                {
                    Item theItem = (Item)targ;

                    bool itemInValidLocation = false;
                    if (theItem.IsChildOf(from.Backpack))
                        itemInValidLocation = true;
                    else if (from.FindBankNoCreate() != null && theItem.IsChildOf(from.FindBankNoCreate()))
                        itemInValidLocation = true;
                    else if ( theItem.Parent is MarketUserContainer)
                        itemInValidLocation = true;

                    if (itemInValidLocation)
                    {
                        //dupecheck
                        if(theItem.Amount > 1)
                        {
                            ConstructorInfo c = theItem.GetType().GetConstructor(Type.EmptyTypes);
                            bool isDupeable = false;
                            if (c != null)
                            {
                                try
                                {
                                    object o = c.Invoke(null);
                                    if (o != null && o is Item)
                                    {
                                        Item tempCopy = o as Item;
                                        tempCopy.Delete();
                                        isDupeable = true;
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (!isDupeable)
                            {
                                from.SendMessage("This item cannot be easily sub-divided; and so cannot be sold in amounts greater than 1.");
                                return;
                            }
                        }

                        //Check if there is a buy order to fulfill. 
                        if (MarketDatabase.HasBuyOrderForType(theItem.GetType(),m_Market))
                        {
                            from.SendGump(new DoYouWantToFulfillABuyOrderGump(from as PlayerMobile, theItem, m_Market));
                        }
                        else
                        {
                            from.SendMessage("Please enter a unit price for the " + theItem.GetType().Name);
                            from.Prompt = new SellOrderUnitPricePrompt(theItem, m_Market);
                        }

                    }
                    else
                    {
                        from.SendMessage("You may only select an item in your backpack, bank or market-account-box, or a tamed creature you own.");
                    }
                }
                else if (targ is BaseCreature)
                {
                    BaseCreature thePet = (BaseCreature)targ;
                    if (thePet.Owners.Contains(from))
                    {
                        if (thePet.Summoned)
                        {
                            from.SendMessage("You can't sell summoned creatures; they're too intangible...");
                        }
                        else
                        {
                            //Check if there is a buy order to fulfill. 
                            if (MarketDatabase.HasBuyOrderForType(thePet.GetType(), m_Market))
                            {
                                from.SendGump(new DoYouWantToFulfillABuyOrderGump(from as PlayerMobile, thePet, m_Market));
                            }
                            else
                            {
                                from.SendMessage("Please enter a sale price for your " + thePet.GetType().Name);
                                from.Prompt = new SellOrderUnitPricePrompt(thePet, m_Market);
                            }
                        }
                    }
                    else
                    {
                        from.SendMessage("You may only select an item in your backpack, bank or market-account-box, or a tamed creature you own.");
                    }
                }
                else
                {
                    from.SendMessage("You may only select an item in your backpack, or a tamed creature you own.");
                }
            }
        }

        public class BuyOrderTarget : Target
        {
            private String m_Market;
            private Mobile m_From;
            private BuyOrder m_Order;

            public BuyOrderTarget(Mobile from, BuyOrder order, String Market)
                : base(30, true, TargetFlags.None)
            {
                m_Market = Market;
                m_From = from;
                m_Order = order;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if(targ.GetType() != m_Order.itemType)
                {
                    from.SendMessage("That is not type of commodity requested.");
                    return;
                }

                if (targ is Container && ((Container)targ).Items.Count > 0)
                {
                    from.SendMessage("Containers holding objects cannot be sold.");
                    return;
                }

                if (targ is Item)
                {
                    Item theItem = (Item)targ;

                    bool itemInValidLocation = false;
                    if (theItem.IsChildOf(from.Backpack))
                        itemInValidLocation = true;
                    else if (from.FindBankNoCreate() != null && theItem.IsChildOf(from.FindBankNoCreate()))
                        itemInValidLocation = true;
                    else if (theItem.Parent is MarketUserContainer)
                        itemInValidLocation = true;

                    if (itemInValidLocation)
                    {
                        //dupecheck
                        if (theItem.Amount > 1)
                        {
                            ConstructorInfo c = theItem.GetType().GetConstructor(Type.EmptyTypes);
                            bool isDupeable = false;
                            if (c != null)
                            {
                                try
                                {
                                    object o = c.Invoke(null);
                                    if (o != null && o is Item)
                                    {
                                        Item tempCopy = o as Item;
                                        tempCopy.Delete();
                                        isDupeable = true;
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (!isDupeable)
                            {
                                from.SendMessage("This item cannot be easily sub-divided; and so cannot be sold in amounts greater than 1.");
                                return;
                            }
                        }

                        from.SendGump(new SellBuyOrderGump(from as PlayerMobile, m_Order, m_Market, targ, theItem.Amount));
                    }
                    else
                    {
                        from.SendMessage("You may only select an item in your backpack, bank or market-account-box, or a tamed creature you own.");
                    }
                }
                else if (targ is BaseCreature)
                {
                    BaseCreature thePet = (BaseCreature)targ;
                    if (thePet.Owners.Contains(from))
                    {
                        if (thePet.Summoned)
                        {
                            from.SendMessage("You can't sell summoned creatures; they're too intangible...");
                        }
                        else
                        {
                            from.SendGump(new SellBuyOrderGump(from as PlayerMobile, m_Order, m_Market, targ, 1));
                        }
                    }
                    else
                    {
                        from.SendMessage("You may only select an item in your backpack, bank or market-account-box, or a tamed creature you own.");
                    }
                }
                else
                {
                    from.SendMessage("You may only select an item in your backpack, or a tamed creature you own.");
                }
            }
        }

        public static bool HasBuyOrderForType(Type type, String Market)
        {
            try
            {
                List<BuyOrder> list = (List<BuyOrder>)GetBuyOrders(Market)[type.Name];
                if (list.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false; 
        }

        private static volatile Mutex s_BuyOrderMutex = new Mutex();
        public static bool FulfillBuyOrder(PlayerMobile from, BuyOrder order, String Market, object target)
        {
            s_BuyOrderMutex.WaitOne();
            try
            {
                if (from != null && order != null)
                {
                    Container sellerUserContainer = GetAccountBox(from, Market);
                    Container authorUserContainer = GetAccountBox(order.author as PlayerMobile, Market);

                    if (sellerUserContainer == null || authorUserContainer == null)
                    {
                        from.SendMessage("Problem! Either you or the author of this buy order does not have a market container; inform a GM of this message.");
                        s_BuyOrderMutex.ReleaseMutex();
                        return false;
                    }

                    //Final check to see if its still there. 

                    List<BuyOrder> list = (List<BuyOrder>)GetBuyOrders(Market)[order.itemType.Name];

                    if (!list.Contains(order))
                    {
                        from.SendMessage("So sorry!, this order has already been fulfilled!");
                        s_BuyOrderMutex.ReleaseMutex();
                        return false;
                    }

                    //if (order.amount < amountToBuy)
                    //{
                    //    from.SendMessage("So sorry!, the amount you want to buy is nolonger available.");
                    //    s_SaleMutex.ReleaseMutex();
                    //    return false;
                    //}
                    int amountToFulfill = 1;

                    if (target is Item)
                    {
                        Item objectToSell = target as Item;

                        amountToFulfill = Math.Min(objectToSell.Amount, order.amount);
                    }

                    int totalCost = amountToFulfill * order.unitPrice;


                    Gold gold = (Gold)sellerUserContainer.FindItemByType(typeof(Gold));
                    if (gold == null)
                    {
                        gold = new Gold(totalCost);
                        sellerUserContainer.DropItem(gold);
                    }
                    else
                        gold.Amount += totalCost;

                    
                    if (NetState.Instances.Contains((NetState)from.NetState))
                    {
                        from.SendMessage("You have recieved " + totalCost + " gold on the " + Market + " market!");
                        from.SendSound(55);
                    }
                 

                    if (target is Item)
                    {
                        Item objectToSell = target as Item;
                        if (amountToFulfill < objectToSell.Amount)
                        {
                            Type t = order.itemType;

                            //ConstructorInfo[] info = t.GetConstructors();

                            ConstructorInfo c = t.GetConstructor(Type.EmptyTypes);

                            if (c != null)
                            {
                                try
                                {
                                    object o = c.Invoke(null);

                                    if (o != null && o is Item)
                                    {
                                        // RunUO 1.0 compatability revisions
                                        Item newItem = (Item)o;
                                        //Server.Commands.Dupe.CopyProperties(newItem, objectToSell);//copy.Dupe( item, copy.Amount );
                                        //objectToSell.OnAfterDuped(newItem);
                                        newItem = newItem.Dupe(objectToSell, objectToSell.Amount);
                                        newItem.Parent = null;

                                        newItem.Amount = amountToFulfill;
                                        objectToSell.Amount-= amountToFulfill;

                                        authorUserContainer.DropItem(newItem);

                                        newItem.InvalidateProperties();
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        else
                        {
                            authorUserContainer.DropItem(objectToSell);
                        }

                        if (order.author is PlayerMobile)
                        {
                            PlayerMobile Author = order.author as PlayerMobile;
                            if (NetState.Instances.Contains((NetState)Author.NetState))
                            {
                                Author.SendMessage("Your new " + order.itemType.Name + "(" + amountToFulfill + ") has been placed in your " + Market + " user account box. Say \"claim\" to a " + Market + " Marketeer to collect it.");
                                Author.SendSound(55);
                            }
                        }

                        order.amount -= amountToFulfill;
                    }
                    else if (target is BaseCreature)
                    {
                        BaseCreature pet = target as BaseCreature;
                        
                        pet.Owners.Clear();
                        pet.Owners.Add(from);
                        pet.SetControlMaster(from);
                        pet.ControlTarget = from;
                        pet.ControlOrder = OrderType.Stay;

                        pet.Loyalty = PetLoyalty.WonderfullyHappy; // Wonderfully Happy
                     
                        if (pet.Name.Contains(" "))
                            pet.Name = order.itemType.Name;

                        
                        PlayerMobile Author = order.author as PlayerMobile;
                        if (NetState.Instances.Contains((NetState)Author.NetState))
                        {
                            Author.SendMessage("Your new " + order.itemType.Name + " called \"" + pet.Name + "\" has been placed in the stables.");
                            Author.SendSound(55);
                        }
                        Author.Stabled.Add(pet);

                        order.amount -= 1;
                    }

                    if (order.amount == 0)
                    {
                        MarketUserContainer.RemoveUserOrder(order, Market);
                        list.Remove(order);
                    }
                    else
                    {
                        MarketUserContainer.ChangeAmountUserOrder(order, Market);
                    }
                }
            }
            catch (Exception exc)
            {
                s_BuyOrderMutex.ReleaseMutex();
                return false;
            }
            s_BuyOrderMutex.ReleaseMutex();

            return true;
        }

        public class SellOrderUnitPricePrompt : Prompt
        {
            private object m_theObject;
            private String m_theMarket;

            public SellOrderUnitPricePrompt(object target, String market)
            {
                m_theObject = target;
                m_theMarket = market;
            }

            public override void OnResponse(Mobile from, string text)
            {
                int price = Utility.ToInt32(text);

                if (price < 0 || price > 250000000)
                {
                    from.SendLocalizedMessage(1062390); // The price you requested is outrageous!
                }

                AddSellOrder(from, m_theObject, price, m_theMarket);
      
            }

            public override void OnCancel(Mobile from)
            {
                from.SendMessage(53, "Sale Order creation cancelled.");
            }
        }

        public class BuyOrderUnitPricePrompt : Prompt
        {
            private Type m_theObject;
            private String m_theMarket;
            private int m_theAmount;

            public BuyOrderUnitPricePrompt(Type target, int amount, String market)
            {
                m_theObject = target;
                m_theMarket = market;
                m_theAmount = amount;
            }

            public override void OnResponse(Mobile from, string text)
            {
                int price = Utility.ToInt32(text);

                if (price < 0 || price > 250000000)
                {
                    from.SendLocalizedMessage(1062390); // The price you requested is outrageous!
                    return;
                }

                //Take the gold.
                int totalCost = m_theAmount * price;

                bool hadTheGold = false;
                if (totalCost == 0)
                    hadTheGold = true;

                Container cont = from.Backpack;
			    if ( !hadTheGold && cont != null )
			    {
				    if ( cont.ConsumeTotal( typeof( Gold ), totalCost ) )
				    {
	                    hadTheGold = true;
				    }
			    }

                Container buyersBank = from.FindBankNoCreate();
                if (!hadTheGold && buyersBank != null)
			    {

                    if (buyersBank.ConsumeTotal(typeof(Gold), totalCost))
				    {
					    hadTheGold = true;
					    from.SendMessage("Funds from your bank were used to complete the transaction.");
				    }
				    else
				    {
					    from.SendMessage("Begging thy pardon, but neither thy pockets nor thy bank account possess these funds in full.");
				    }
			    }


                if ( !hadTheGold  )
			    {
                    MarketUserContainer buyerUserContainer = GetAccountBox(from as PlayerMobile,m_theMarket);
                    if (buyerUserContainer != null && buyerUserContainer.ConsumeTotal(typeof(Gold), totalCost))
				    {
					    hadTheGold = true;
					    from.SendMessage("Funds from your market account were used to complete the transaction.");
				    }
				    else
				    {
					    from.SendMessage("Begging thy pardon, but neither thy pockets nor thy bank account possess these funds in full.");
				    }
			    }

                if (hadTheGold)
                {
                    from.SendMessage(53,"The sum of " + totalCost + " gold has been taken in escrow, and your order for " + m_theAmount + " " + m_theObject.Name + " has been placed.");
                    AddBuyOrder(from, m_theObject, m_theAmount, price, m_theMarket);
                }
            }

            public override void OnCancel(Mobile from)
            {
                from.SendMessage(53, "Sale Order creation cancelled.");
            }
        }

        public class Order
        {
            public long uniqueID;
            public Mobile author;
            public Type itemType;
            public int amount;
            public int unitPrice;
            public DateTime creation;

            public void Serialize(GenericWriter writer)
            {
			    writer.Write( (long) uniqueID );
			    writer.Write( (Mobile) author );
			    writer.Write( itemType == null ? null : itemType.FullName );
			    writer.Write( (int) amount );
			    writer.Write( (int) unitPrice );
			    writer.Write(creation);
            }

            public void DeSerialize(GenericReader reader)
            {
                uniqueID = reader.ReadLong();
                author = (Mobile)reader.ReadMobile();

                string type = reader.ReadString();
				if ( type != null )
					itemType = ScriptCompiler.FindTypeByFullName( type );
                
                amount = reader.ReadInt();
                unitPrice = reader.ReadInt();

                creation = reader.ReadDateTime();
            }
        }

        public class BuyOrder : Order
        {
        }

        public class SellOrder : Order
        {
            public bool             isItem;
            public Item             itemLink;
            public BaseCreature     creatureLink;

            public void StoreItem(MarketDatabase foundMarket)
            {
                if (isItem && itemLink != null)
                {
                    foundMarket.AddItem(itemLink);
                }
                else if (!isItem && creatureLink != null && !creatureLink.IsStabled)
                {
                    creatureLink.ControlTarget = null;
                    creatureLink.ControlOrder = OrderType.Stay;
                    creatureLink.Internalize();

                    creatureLink.SetControlMaster(null);
                    creatureLink.SummonMaster = null;

                    creatureLink.IsStabled = true;

                    if (Core.SE)
                        creatureLink.Loyalty = PetLoyalty.WonderfullyHappy; // Wonderfully happy

                    creatureLink.Hits = creatureLink.HitsMax;
                    creatureLink.Mana = creatureLink.ManaMax;
                    creatureLink.Stam = creatureLink.StamMax; 
                }
            }

            public void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(isItem);
                writer.Write(itemLink);
                writer.Write(creatureLink);
            }

            public void DeSerialize(GenericReader reader)
            {
                base.DeSerialize(reader);
                isItem = reader.ReadBool();
                itemLink = (Item)reader.ReadItem();
                creatureLink = (BaseCreature)reader.ReadMobile();
            }
        }

        public Hashtable SellOrders = new Hashtable();
        public Hashtable BuyOrders = new Hashtable();

        public List<Type> AllSellOrderTypes = new List<Type>();
        public List<Type> AllBuyOrderTypes = new List<Type>();

        
        public static List<Type> GetAllSellOrderTypes()
        {
            String MarketName = "Global";
            return GetAllSellOrderTypes(MarketName);
        }

        public static List<Type> GetAllSellOrderTypes(String MarketName)
        {
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == MarketName)
                    return s_AllMarketDatabase[i].AllSellOrderTypes;
            }
            List<Type> Empty = new List<Type>();
            return Empty;
        }

        public static List<Type> GetAllBuyOrderTypes()
        {
            String MarketName = "Global";
            return GetAllBuyOrderTypes(MarketName);
        }

        public static List<Type> GetAllBuyOrderTypes(String MarketName)
        {
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == MarketName)
                    return s_AllMarketDatabase[i].AllBuyOrderTypes;
            }
            List<Type> Empty = new List<Type>();
            return Empty;
        }

        public Hashtable UserContainers = new Hashtable();

        public long m_uniqueNumber;

        public String m_MarketName;

        public static void Initialize()
        {
            Server.Commands.Register("MarketAdd", AccessLevel.GameMaster, new CommandEventHandler(AddMenu_OnCommand));
        }

        [Usage("MarketAdd")]
        [Description("Debug adds an item to the market.")]
        private static void AddMenu_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Select with object in your inventory, or pet you wish to sell.");
            e.Mobile.Target = new SellOrderTarget("Global");
        }

        public static Hashtable GetSellOrders(){ return GetSellOrders("Global"); }
        public static Hashtable GetSellOrders(String MarketName)
        {
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == MarketName)
                    return s_AllMarketDatabase[i].SellOrders; 
            }
            Hashtable Empty = new Hashtable();
            return Empty;
        }

        public static MarketUserContainer GetAccountBox(PlayerMobile user, String MarketName)
        {
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == MarketName)
                {
                    if (!s_AllMarketDatabase[i].UserContainers.Contains(user))
                        s_AllMarketDatabase[i].UserContainers.Add(user, new MarketUserContainer(user, MarketName));
                    return (MarketUserContainer)s_AllMarketDatabase[i].UserContainers[user];
                }
            }
            return null;
        }

        public static Hashtable GetBuyOrders()
        {
            String MarketName = "Global";
            return GetBuyOrders(MarketName);
        }

        public static Hashtable GetBuyOrders(String MarketName)
        {
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == MarketName)
                    return s_AllMarketDatabase[i].BuyOrders;
            }
            Hashtable Empty = new Hashtable();
            return Empty;
        }

        private static volatile Mutex s_CreateSaleMutex = new Mutex(); 
        public static bool AddSellOrder( Mobile author, object item, int unitPrice, String Market){ return AddSellOrder(author, item, unitPrice, Market, false);}
        public static bool AddSellOrder( Mobile author, object item, int unitPrice, String Market, bool serialized)
        {
            SellOrder newOrder = new SellOrder();

            newOrder.author = author;
            newOrder.itemType = item.GetType();
            
            newOrder.isItem = false;
            if (item is Item)
            {
                newOrder.isItem = true;
                newOrder.itemLink = (Item)item;
                newOrder.amount = newOrder.itemLink.Amount;
            }
            else if (item is BaseCreature)
            {
                newOrder.creatureLink = (BaseCreature)item;
                newOrder.amount = 1;
            }
            else
                return false;

            newOrder.creation = DateTime.UtcNow;

            s_CreateSaleMutex.WaitOne();
            try
            {
                MarketDatabase foundMarket = null;
                for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
                {
                    if (s_AllMarketDatabase[i].MarketName == Market)
                    {
                        foundMarket = s_AllMarketDatabase[i];
                        newOrder.uniqueID = foundMarket.m_uniqueNumber;
                        foundMarket.m_uniqueNumber += 1;
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                Console.Write(err);
            }
            s_CreateSaleMutex.ReleaseMutex();

            newOrder.unitPrice = unitPrice;

            return AddSellOrder(newOrder, Market);
        }
        public static bool AddBuyOrder( Mobile author, Type item, int amount, int unitPrice, String Market ) { return AddBuyOrder( author, item, amount, unitPrice,Market, false);}
        public static bool AddBuyOrder( Mobile author, Type item, int amount, int unitPrice, String Market, bool serialized )
        {
            BuyOrder newOrder = new BuyOrder();

            newOrder.author = author;
            newOrder.itemType = item;
            newOrder.amount =amount;
            newOrder.unitPrice = unitPrice;

            newOrder.creation = DateTime.UtcNow;

            s_CreateSaleMutex.WaitOne();
            try
            {
                MarketDatabase foundMarket = null;
                for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
                {
                    if (s_AllMarketDatabase[i].MarketName == Market)
                    {
                        foundMarket = s_AllMarketDatabase[i];
                        newOrder.uniqueID = foundMarket.m_uniqueNumber;
                        foundMarket.m_uniqueNumber += 1;
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                Console.Write(err);
            }
            s_CreateSaleMutex.ReleaseMutex();

            return AddBuyOrder(newOrder, Market);
        }
        public static bool AddSellOrder(SellOrder order, String Market) { return AddSellOrder( order, Market, false);}
        public static bool AddSellOrder(SellOrder order, String Market, bool serialized)
        {
            Hashtable foundSellOrders = null;
            MarketDatabase foundMarket = null;
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == Market)
                {
                    foundMarket = s_AllMarketDatabase[i];
                    foundSellOrders = s_AllMarketDatabase[i].SellOrders;
                }
            }

            if (foundSellOrders != null)
            {
                if (foundSellOrders.ContainsKey(order.itemType.Name))
                {
                    List<SellOrder> foundSellOrderList = (List<SellOrder>)foundSellOrders[order.itemType.Name];
                    foundSellOrderList.Add(order);
                }
                else
                {
                    List < SellOrder > newSellOrderList = new List<SellOrder>();
                    newSellOrderList.Add(order);
                    foundSellOrders[order.itemType.Name] = newSellOrderList;
                }

                if (!serialized)
                {
                    order.StoreItem(foundMarket);
                    MarketUserContainer.AddUserOrder(order, Market);
                }

                if (!foundMarket.AllSellOrderTypes.Contains(order.itemType))
                    foundMarket.AllSellOrderTypes.Add(order.itemType);

                return true;
            }

            return false;
        }
        public static bool AddBuyOrder(BuyOrder order, String Market){ return AddBuyOrder(order, Market, false);}
        public static bool AddBuyOrder(BuyOrder order, String Market, bool serialized)
        {
            Hashtable foundBuyOrders = null;
            MarketDatabase foundMarket = null;
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == Market)
                {
                    foundMarket = s_AllMarketDatabase[i];
                    foundBuyOrders = s_AllMarketDatabase[i].BuyOrders;
                }
            }

            if (foundBuyOrders != null)
            {
                if (foundBuyOrders.ContainsKey(order.itemType.Name))
                {
                    List<BuyOrder> foundSellOrderList = (List<BuyOrder>)foundBuyOrders[order.itemType.Name];
                    foundSellOrderList.Add(order);
                }
                else
                {
                    List < BuyOrder > newBuyOrderList = new List<BuyOrder>();
                    newBuyOrderList.Add(order);
                    foundBuyOrders[order.itemType.Name] = newBuyOrderList;
                }

                if(!serialized)
                    MarketUserContainer.AddUserOrder(order, Market);

                if (!foundMarket.AllBuyOrderTypes.Contains(order.itemType))
                    foundMarket.AllBuyOrderTypes.Add(order.itemType);

                if (!serialized)
                    AutoFulfillOrder(order, Market);

                return true;
            }
            return false;
        }

        public class SellOrderUnitPriceSorter : IComparer<SellOrder>
        {
            public int Compare(SellOrder a, SellOrder b)
            {
                if (a.unitPrice >= b.unitPrice)
                    return 1;
                return -1;
            }
        }

        public static bool AutoFulfillOrder(BuyOrder order, String Market)
        {
            Hashtable foundSellOrders = null;
            MarketDatabase foundMarket = null;
            bool Success = false;
            int totalGoldRefund = 0; 

            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == Market)
                {
                    foundMarket = s_AllMarketDatabase[i];
                    foundSellOrders = s_AllMarketDatabase[i].SellOrders;
                }
            }

            if (foundSellOrders != null)
            {
                List<SellOrder> foundSellOrderList = null;
                if (foundSellOrders.ContainsKey(order.itemType.Name))
                {
                    foundSellOrderList = (List<SellOrder>)foundSellOrders[order.itemType.Name];
                }

                if (foundSellOrderList != null && foundSellOrderList.Count > 0)
                {
                    foundSellOrderList.Sort(new SellOrderUnitPriceSorter());

                    int index = 0;
                    do 
                    {
                        SellOrder currentSellOrder = foundSellOrderList[index];
                        ++index;
                        if (currentSellOrder.unitPrice <= order.unitPrice)
                        {
                            int priceRefund = order.unitPrice - currentSellOrder.unitPrice;
                            if (currentSellOrder.amount <= order.amount)
                            {
                                if (BuySellOrder(order.author as PlayerMobile, currentSellOrder, Market, currentSellOrder.amount, false))
                                {
                                    totalGoldRefund += (priceRefund * currentSellOrder.amount);
                                    order.amount -= currentSellOrder.amount;
                                    MarketUserContainer.ChangeAmountUserOrder(order, Market);
                                    --index;
                                }
                            }
                            else
                            {
                                if (BuySellOrder(order.author as PlayerMobile, currentSellOrder, Market, order.amount, false))
                                {
                                    totalGoldRefund += (priceRefund * order.amount);
                                    order.amount = 0;
                                }
                            }

                            if (order.amount == 0)
                            {
                                CancelBuyOrder(order.uniqueID, order.itemType, Market);
                                Success = true;
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }

                    }while( index < foundSellOrderList.Count );
                }
            }

            if (totalGoldRefund > 0)
            {
                Container authorUserContainer = GetAccountBox(order.author as PlayerMobile, Market);
                Gold gold = (Gold)authorUserContainer.FindItemByType(typeof(Gold));
                if (gold == null)
                {
                    gold = new Gold(totalGoldRefund);
                    authorUserContainer.DropItem(gold);
                }
                else
                    gold.Amount += totalGoldRefund;

                if (order.author is PlayerMobile)
                {
                    PlayerMobile Author = order.author as PlayerMobile;
                    if (NetState.Instances.Contains((NetState)Author.NetState))
                    {
                        String amountString = "";
                        if (order.amount > 1)
                            amountString = "" + order.amount + " ";
                        Author.SendMessage("Your have recieved " + totalGoldRefund + " gold in your market account as refund from the automatic purchase of cheap products from sell orders.");
                        Author.SendSound(55);
                    }
                }
            }

            return Success;
        }
        
        public static bool CancelBuyOrder(long uniqueID, Type itemType, String Market)
        {
            Hashtable foundBuyOrders = null;
            MarketDatabase foundMarket = null;
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == Market)
                {
                    foundMarket = s_AllMarketDatabase[i];
                    foundBuyOrders = s_AllMarketDatabase[i].BuyOrders;
                }
            }

            if (foundBuyOrders != null)
            {
                if (foundBuyOrders.ContainsKey(itemType.Name))
                {
                    List<BuyOrder> foundSellOrderList = (List<BuyOrder>)foundBuyOrders[itemType.Name];
                    for (int i = 0; i < foundSellOrderList.Count; ++i)
                    {
                        if (foundSellOrderList[i].uniqueID == uniqueID)
                        {
                            BuyOrder foundOrder = foundSellOrderList[i];

                            int totalRemainingCash = foundOrder.unitPrice * foundOrder.amount;

                            MarketUserContainer authorUserContainer = GetAccountBox(foundOrder.author as PlayerMobile, Market);

                            Gold gold = (Gold)authorUserContainer.FindItemByType(typeof(Gold));
                            if (gold == null)
                            {
                                gold = new Gold(totalRemainingCash);
                                authorUserContainer.DropItem(gold);
                            }
                            else
                                gold.Amount += totalRemainingCash;

                            MarketUserContainer.RemoveUserOrder(foundOrder, Market);
                            foundSellOrderList.Remove(foundOrder);

                            return true;
                        }
                    }
                }

            }
            return false;
        }
        public static bool CancelSellOrder(long uniqueID, Type itemType, String Market)
        {
            Hashtable foundSellOrders = null;
            MarketDatabase foundMarket = null;
            for (int i = 0; i < s_AllMarketDatabase.Count; ++i)
            {
                if (s_AllMarketDatabase[i].MarketName == Market)
                {
                    foundMarket = s_AllMarketDatabase[i];
                    foundSellOrders = s_AllMarketDatabase[i].SellOrders;
                }
            }

            if (foundSellOrders != null)
            {
                if (foundSellOrders.ContainsKey(itemType.Name))
                {
                    List<SellOrder> foundSellOrderList = (List<SellOrder>)foundSellOrders[itemType.Name];
                    for (int i = 0; i < foundSellOrderList.Count; ++i)
                    {
                        if (foundSellOrderList[i].uniqueID == uniqueID)
                        {
                            SellOrder order = foundSellOrderList[i];
                            MarketUserContainer authorUserContainer = GetAccountBox(order.author as PlayerMobile, Market);

                            if (order.isItem)
                            {
                                authorUserContainer.DropItem(order.itemLink);
                            }
                            else
                            {
                                BaseCreature pet = order.creatureLink;
                                order.author.Stabled.Add(pet);
                            }

                            MarketUserContainer.RemoveUserOrder(order, Market);
                            foundSellOrderList.Remove(order);
                            return true;
                        }
                    }
                }

            }
            return false;
        }
        
		private static volatile Mutex s_SaleMutex = new Mutex(); 
        public static bool BuySellOrder(PlayerMobile from, SellOrder order, String Market, int amountToBuy){ return BuySellOrder( from,  order,  Market, amountToBuy, true);}
        public static bool BuySellOrder(PlayerMobile from, SellOrder order, String Market, int amountToBuy, bool charge)
        {
            bool bought = false;
            s_SaleMutex.WaitOne();
            try
            {
                if (from != null && order != null)
                {
                    Container buyerUserContainer = GetAccountBox(from, Market);
                    Container authorUserContainer = GetAccountBox(order.author as PlayerMobile, Market);

                    if (buyerUserContainer == null || authorUserContainer == null)
                    {
                        from.SendMessage("Problem! Either you or the author of this sell order does not have a market container; inform a GM of this message.");
                        s_SaleMutex.ReleaseMutex();
                        return false;
                    }

                    //Final check to see if its still there. 

                    List<SellOrder> list = (List<SellOrder>)GetSellOrders(Market)[order.itemType.Name];

                    if( ! list.Contains(order))
                    {
                        from.SendMessage("So sorry!, this item has already been sold!");
                        s_SaleMutex.ReleaseMutex();
                        return false;
                    }

                    if (order.amount < amountToBuy)
                    {
                        from.SendMessage("So sorry!, the amount you want to buy is nolonger available.");
                        s_SaleMutex.ReleaseMutex();
                        return false;
                    }


                    int totalCost = amountToBuy * order.unitPrice;

                    if (charge)
                    {
                        if (totalCost == 0)
                            bought = true;

                        Container cont = from.Backpack;
                        if (cont != null)
                        {
                            if (cont.ConsumeTotal(typeof(Gold), totalCost))
                            {
                                bought = true;
                            }
                        }

                        Container buyersBank = from.FindBankNoCreate();
                        if (!bought && buyersBank != null)
                        {

                            if (buyersBank.ConsumeTotal(typeof(Gold), totalCost))
                            {
                                bought = true;
                                from.SendMessage("Funds from your bank were used to complete the transaction.");
                            }
                            else
                            {
                                from.SendMessage("Begging thy pardon, but neither thy pockets nor thy bank account possess these funds in full.");
                            }
                        }

                        if (!bought)
                        {
                            if (buyerUserContainer.ConsumeTotal(typeof(Gold), totalCost))
                            {
                                bought = true;
                                from.SendMessage("Funds from your market account were used to complete the transaction.");
                            }
                            else
                            {
                                from.SendMessage("Begging thy pardon, but neither thy pockets nor thy bank account possess these funds in full.");
                            }
                        }
                    }
                    else
                    {
                        bought = true;
                    }

                    if (bought)
                    {
				        
                        if (authorUserContainer != null)
                        {
                            Gold gold = (Gold)authorUserContainer.FindItemByType(typeof(Gold));
                            if (gold == null)
                            {
                                gold = new Gold(totalCost);
                                authorUserContainer.DropItem(gold);
                            }
                            else
                                gold.Amount += totalCost;

                            if (order.author is PlayerMobile)
                            {
                                PlayerMobile Author = order.author as PlayerMobile;
                                if(NetState.Instances.Contains( (NetState)Author.NetState ))
                                {
                                    String amountString = "";
                                    if(order.amount > 1)
                                        amountString = "" + order.amount + " ";
                                    Author.SendMessage("Your have recieved " + totalCost + " gold in your market account for the sale of " + amountToBuy + " of your " + amountString + order.itemType.Name + " on the " + Market + " market!");
                                    Author.SendSound(55);
                                }
                            }
                        }

                        bool removeOrder = true;

                        if (order.isItem)
                        {
                            if (amountToBuy < order.amount)
                            {
                                Type t = order.itemType;

                                //ConstructorInfo[] info = t.GetConstructors();

                                ConstructorInfo c = t.GetConstructor(Type.EmptyTypes);

                                if (c != null)
                                {
                                    try
                                    {
                                        object o = c.Invoke(null);

                                        if (o != null && o is Item)
                                        {
                                            // RunUO 1.0 compatability revisions
                                            Item newItem = (Item)o;
                                            //Server.Commands.Dupe.CopyProperties(newItem, objectToSell);//copy.Dupe( item, copy.Amount );
                                            //objectToSell.OnAfterDuped(newItem);
                                            newItem = newItem.Dupe(order.itemLink, order.itemLink.Amount);
                                            newItem.Parent = null;

                                            newItem.Amount = amountToBuy;
                                            order.amount -= amountToBuy;

                                            buyerUserContainer.DropItem(newItem);

                                            newItem.InvalidateProperties();
                                        }
                                        removeOrder = false;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            else
                            {
                                buyerUserContainer.DropItem(order.itemLink);
                                
                            }

                            from.SendMessage("Your new " + order.itemType.Name + "(" + amountToBuy + ") has been placed in your " + Market + " user account box. Say \"claim\" to a " + Market + " Marketeer to collect it.");

                        }
                        else
                        {
                            BaseCreature pet = order.creatureLink;
                            if ((from.Followers + pet.ControlSlots) <= from.FollowersMax)
                            {
                                pet.Owners.Clear();
                                pet.Owners.Add(from);
                                pet.SetControlMaster(from);
                                pet.ControlTarget = from;
                                pet.ControlOrder = OrderType.Follow;

                                pet.MoveToWorld(from.Location, from.Map);

                                pet.IsStabled = false;

                                pet.Loyalty =  PetLoyalty.WonderfullyHappy; // Wonderfully Happy
                            }
                            else
                            {
                                if(pet.Name.Contains(" "))
                                    pet.Name = order.itemType.Name;
                                from.Stabled.Add(pet);
                                from.SendMessage("Your new " + order.itemType.Name + " called \"" + pet.Name + "\" has been placed in the stables because you have too many followers.");
                            }
                        }

                        if (removeOrder)
                        {
                            MarketUserContainer.RemoveUserOrder(order,Market);
                            list.Remove(order);
                        }
                        else
                        {
                            MarketUserContainer.ChangeAmountUserOrder(order, Market);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                s_SaleMutex.ReleaseMutex();
                return false; 
            }
            s_SaleMutex.ReleaseMutex();

            return bought;
        }

        public static List<MarketDatabase> s_AllMarketDatabase = new List<MarketDatabase>();

        [CommandProperty(AccessLevel.GameMaster)]
        public String MarketName
        {
            get { return m_MarketName; }
            set
            {
                m_MarketName = value;
                Name = "Market Database (" + m_MarketName + ")";
            }
        }

		[Constructable]
		public MarketDatabase() : base( 2608 )
		{
            MarketName = "Global";

            MaxItems = 100000000;

            m_uniqueNumber = 0;

            Movable = false;
          
            if (s_AllMarketDatabase.Contains(this) == false)
                s_AllMarketDatabase.Add(this);

            Visible = false;
		}

        public MarketDatabase(Serial serial)
            : base(serial)
		{
            if(s_AllMarketDatabase.Contains(this) == false)
                s_AllMarketDatabase.Add(this);
		}

        //public override void OnDoubleClick (Mobile from) 
        //{
        //    from.PlaySound(502);
        //    from.SendGump(new TownMemberGump(from, this));
        //}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

            writer.Write(m_MarketName);

            writer.Write(m_uniqueNumber);

            //write out sell orders.
   
			writer.Write( (int) SellOrders.Count ); // amount of Sell Orders

            if (SellOrders.Count > 0)
            {
                DictionaryEntry[] tempArray = new DictionaryEntry[SellOrders.Count];
                SellOrders.CopyTo(tempArray, 0);

                for (int index = 0; index < tempArray.Length; ++index)
                {
                    List<SellOrder> theList = (List<SellOrder>)tempArray[index].Value;
                    SellOrder[] theArray = theList.ToArray();

                    writer.Write((String)tempArray[index].Key);
                    writer.Write((int)theArray.Length); // amount of Sell Orders of this type. 
                    for (int i = 0; i < theArray.Length; ++i)
                    {
                        theArray[i].Serialize(writer);
                    }
                }
            }

            writer.Write((int)BuyOrders.Count); // amount of Sell Orders

            if (BuyOrders.Count > 0)
            {
                //write out buy orders.
                DictionaryEntry[] tempArray = new DictionaryEntry[BuyOrders.Count];
                BuyOrders.CopyTo(tempArray, 0);

                for (int index = 0; index < tempArray.Length; ++index)
                {
                    List<BuyOrder> theList = (List<BuyOrder>)tempArray[index].Value;
                    BuyOrder[] theArray = theList.ToArray();

                    writer.Write((String)tempArray[index].Key);
                    writer.Write((int)theArray.Length); // amount of Sell Orders of this type. 
                    for (int i = 0; i < theArray.Length; ++i)
                    {
                        theArray[i].Serialize(writer);
                    }
                }
            }

            writer.Write((int)UserContainers.Count); // amount of UserContainers

            if (UserContainers.Count > 0)
            {
                //write out buy orders.
                DictionaryEntry[] tempArray = new DictionaryEntry[UserContainers.Count];
                UserContainers.CopyTo(tempArray, 0);

                for (int index = 0; index < tempArray.Length; ++index)
                {
                    PlayerMobile user = (PlayerMobile)tempArray[index].Key;
                    Container accountBox = (Container)tempArray[index].Value;
                
                    writer.Write(user);
                    writer.Write(accountBox);
                }
            }

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            MarketName = reader.ReadString();

            m_uniqueNumber = reader.ReadLong();

            //Load Sell Orders
            int sellOrderCount = reader.ReadInt();

            for (int sellOrderHashIndex = 0; sellOrderHashIndex < sellOrderCount; ++sellOrderHashIndex)
            {
                String hashName = reader.ReadString();
                int entryCount = reader.ReadInt();

                for(int entry = 0; entry < entryCount; ++ entry)
                {
                    SellOrder loadedSellOrder = new SellOrder();
                    loadedSellOrder.DeSerialize(reader);
                    AddSellOrder(loadedSellOrder, MarketName, true);
                }
            }

            //Load Buy Orders
            int buyOrderCount = reader.ReadInt();

            for (int buyOrderHashIndex = 0; buyOrderHashIndex < buyOrderCount; ++buyOrderHashIndex)
            {
                String hashName = reader.ReadString();
                int entryCount = reader.ReadInt();
                for (int entry = 0; entry < entryCount; ++entry)
                {
                    BuyOrder loadedBuyOrder = new BuyOrder();
                    loadedBuyOrder.DeSerialize(reader);
                    AddBuyOrder(loadedBuyOrder, MarketName, true);
                }
            }

            if (version >= 2)
            {
                int UserContainersCount = reader.ReadInt();

                for (int userContainersHashIndex = 0; userContainersHashIndex < UserContainersCount; ++userContainersHashIndex)
                {
                    PlayerMobile user = (PlayerMobile)reader.ReadMobile();
                    Container userContainer = (Container)reader.ReadItem();

                    if (user != null)
                        UserContainers.Add(user, userContainer);
                }
            }
		}

        public override void OnDoubleClick(Mobile from)
        {
            // from.SendGump(
        }
	}
}