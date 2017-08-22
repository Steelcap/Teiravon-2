using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Prompts;
using Server.Multis;

namespace Server.Gumps
{
    namespace MarketGump
    {
        struct AddSpacesToNames
        {
            public static String Format(String theName)
            {
                String LowerString = theName.ToLower();
                String HighString = theName;

                int longStringIndex = 0;
                for(int i = 0; i < LowerString.Length; ++i, ++longStringIndex)
                {
                    if(LowerString[i] != HighString[longStringIndex])
                    {
                        HighString = HighString.Insert(longStringIndex, " ");
                        ++longStringIndex;
                    }
                }

                return HighString;
            }
        }

        struct CancelAllGumpsAndPrompts
        {
            public static void Run(Mobile from)
            {
                from.Prompt = null;
                from.CloseGump(typeof(MarketItemBuySearchGump));
                from.CloseGump(typeof(PurchaseSellOrderGump));
                from.CloseGump(typeof(SellBuyOrderGump));
                from.CloseGump(typeof(MarketBuyOrderSearchGump));
                from.CloseGump(typeof(ItemTypeSelectGump));
                from.CloseGump(typeof(UserAccountSearchGump));
                from.CloseGump(typeof(DoYouWantToFulfillABuyOrderGump));
            }
        }
        

        //Sell Orders 
        public class MarketItemBuySearchGump : Gump
        {
            private String m_Market;
            private string m_SearchString;
            private List<MarketDatabase.SellOrder> m_SearchResults;
            private int m_Page;

            public static void Initialize()
            {
                Commands.Register("MarketBuy", AccessLevel.GameMaster, new CommandEventHandler(AddMenu_OnCommand));
            }

            [Usage("MarketBuy")]
            [Description("Opens the Market to Buy something.")]
            private static void AddMenu_OnCommand(CommandEventArgs e)
            {
                string val = e.ArgString.Trim();
                List<MarketDatabase.SellOrder> results = new List<MarketDatabase.SellOrder>();
                e.Mobile.SendGump(new MarketItemBuySearchGump(e.Mobile, val, 0, results, "Global"));
            }

            public MarketItemBuySearchGump(Mobile from, string searchString, int page, List<MarketDatabase.SellOrder> searchResults, String Market)
                : base(50, 50)
            {
                m_SearchString = searchString;
                m_SearchResults = searchResults;
                m_Page = page;
                m_Market = Market;

                if (m_SearchString == "" && m_Page == 0 && m_SearchResults.Count == 0)
                {
                    m_SearchResults = SearchForObjectTypes(MarketDatabase.GetAllSellOrderTypes(m_Market), m_Market);
                }

                CancelAllGumpsAndPrompts.Run(from);

                AddPage(0);

                AddBackground(0, 0, 420, 330/*310/*280*/, 5054);

                AddLabel(10, 6, 0x480, "Viewing the \"" + m_Market + "\" market's orders of sale.");

                AddImageTiled(10, 30, 400, 20, 2624);
                AddAlphaRegion(10, 30, 400, 20);
                AddImageTiled(41, 31, 184, 18, 0xBBC);
                AddImageTiled(42, 32, 182, 16, 2624);
                AddAlphaRegion(42, 32, 182, 16);

                AddButton(10, 29, 4011, 4013, 1, GumpButtonType.Reply, 0);
                AddTextEntry(44, 30, 180, 20, 0x480, 0, m_SearchString);

                AddHtmlLocalized(230, 30, 100, 20, 3010005, 0x7FFF, false, false);

                AddButton(110, 300, 4011, 4013, 4, GumpButtonType.Reply, 0);
                AddLabel(140, 300, 0x480, "Click here to sell something!");

                AddImageTiled(10, 60, 400, 200, 2624);
                AddAlphaRegion(10, 60, 400, 200);

                if (m_SearchResults.Count > 0)
                {
                    for (int i = (m_Page * 10); i < ((m_Page + 1) * 10) && i < m_SearchResults.Count; ++i)
                    {
                        int index = i % 10;

                        AddLabel(44, 59 + (index * 20), 0x480, m_SearchResults[i].itemType.Name);
                        AddLabel(184, 59 + (index * 20), 0x480, "x" + m_SearchResults[i].amount);
                        AddLabel(254, 59 + (index * 20), 0x480, "" + m_SearchResults[i].unitPrice + " gold each");

                        AddButton(10, 59 + (index * 20), 4005, 4007, 5 + i, GumpButtonType.Reply, 0);
                    }
                }
                else
                {
                    if (m_SearchString == "")
                        AddLabel(15, 64, 0x480, "Enter a name of an item or animal to search for.");
                    else
                    {
                        Type t = ScriptCompiler.FindTypeByName(String.Join("", m_SearchString.Split(' ')));

                        if (t == null)
                        {
                            AddLabel(15, 64, 0x480, "No item or animal with that name was found.");
                        }
                        else
                        {
                            AddLabel(15, 64, 0x480, "Searched for " + t.Name + " and found none.");
                        }
                    }
                }

                AddImageTiled(10, 270, 400, 20, 2624);
                AddAlphaRegion(10, 270, 400, 20);

                if (m_Page > 0)
                    AddButton(10, 269, 4014, 4016, 2, GumpButtonType.Reply, 0);
                else
                    AddImage(10, 269, 4014);

                AddHtmlLocalized(44, 270, 170, 20, 1061028, m_Page > 0 ? 0x7FFF : 0x5EF7, false, false); // Previous page

                if (((m_Page + 1) * 10) < m_SearchResults.Count)
                    AddButton(210, 269, 4005, 4007, 3, GumpButtonType.Reply, 0);
                else
                    AddImage(210, 269, 4005);

                AddHtmlLocalized(244, 270, 170, 20, 1061027, ((m_Page + 1) * 10) < m_SearchResults.Count ? 0x7FFF : 0x5EF7, false, false); // Next page
            }

            private static Type typeofItem = typeof(Item), typeofMobile = typeof(Mobile);

            private static void Match(string match, Type[] types, List<Type> results)
            {
                if (match.Length == 0)
                    return;

                match = match.ToLower();

                for (int i = 0; i < types.Length; ++i)
                {
                    Type t = types[i];

                    if ((typeofMobile.IsAssignableFrom(t) || typeofItem.IsAssignableFrom(t)) && t.Name.ToLower().IndexOf(match) >= 0 && !results.Contains(t))
                    {
                        ConstructorInfo[] ctors = t.GetConstructors();

                        for (int j = 0; j < ctors.Length; ++j)
                        {
                            if (ctors[j].GetParameters().Length == 0 && ctors[j].IsDefined(typeof(ConstructableAttribute), false))
                            {
                                results.Add(t);
                                break;
                            }
                        }
                    }
                }
            }

            public static List<Type> Match(string match)
            {
                List<Type> results = new List<Type>();
                Type[] types;

                Assembly[] asms = ScriptCompiler.Assemblies;

                for (int i = 0; i < asms.Length; ++i)
                {
                    types = ScriptCompiler.GetTypeCache(asms[i]).Types;
                    Match(match, types, results);
                }

                types = ScriptCompiler.GetTypeCache(Core.Assembly).Types;
                Match(match, types, results);

                results.Sort(new TypeNameComparer());

                return results;
            }

            public static List<MarketDatabase.SellOrder> SearchForObjectTypes(List<Type> types, String Market)
            {
                List<MarketDatabase.SellOrder> results = new List<MarketDatabase.SellOrder>();
                for (int i = 0; i < types.Count; ++i)
                {
                    String Name = types[i].Name;

                    Hashtable SellOrders = MarketDatabase.GetSellOrders(Market);
                    if (SellOrders.ContainsKey(Name))
                        results.AddRange((List<MarketDatabase.SellOrder>)SellOrders[Name]);
                }
                return results;
            }


            private class TypeNameComparer : IComparer<Type>
            {
                public int Compare(Type x, Type y)
                {
                    return x.Name.CompareTo(y.Name);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile from = sender.Mobile;

                switch (info.ButtonID)
                {
                    case 4: // Sell Something
                        {
                            if (from.Target != null)
                            {
                                from.SendMessage("You can't sell something like your target cursor is in use.");
                                break;
                            }

                            from.SendMessage("Select an item in your inventory, bank box or " + m_Market + " market account box, or tamed pet you wish to put for sale.");
                            from.Target = new MarketDatabase.SellOrderTarget(m_Market);
                            break;
                        }
                    case 1: // Search
                        {
                            TextRelay te = info.GetTextEntry(0);
                            String match = (te == null ? "" : te.Text.Trim());

                            if (match.Length < 3)
                            {
                                from.SendMessage("Invalid search string.");
                                from.SendGump(new MarketItemBuySearchGump(from, match, m_Page, m_SearchResults,m_Market));
                            }
                            else
                            {
                                from.SendGump(new MarketItemBuySearchGump(from, match, 0, SearchForObjectTypes(Match(String.Join("", match.Split(' '))), m_Market), m_Market));
                            }

                            break;
                        }
                    case 2: // Previous page
                        {
                            if (m_Page > 0)
                                from.SendGump(new MarketItemBuySearchGump(from, m_SearchString, m_Page - 1, m_SearchResults,m_Market));

                            break;
                        }
                    case 3: // Next page
                        {
                            if ((m_Page + 1) * 10 < m_SearchResults.Count)
                                from.SendGump(new MarketItemBuySearchGump(from, m_SearchString, m_Page + 1, m_SearchResults,m_Market));

                            break;
                        }
                    default:
                        {
                            int index = info.ButtonID - 5;

                            //Give them more info on item m_SearchResults[index].
                            if ( index >= 0 && index < m_SearchResults.Count )
                            {
                                if (from is PlayerMobile)
                                {
                                    if (m_SearchResults[index].amount > 1)
                                        from.Prompt = new AmountSellOrderPrompt(from as PlayerMobile, m_SearchResults[index], m_Market);
                                    else
                                        from.SendGump(new PurchaseSellOrderGump(from as PlayerMobile, m_SearchResults[index], m_Market, 1));
                                }
                            }

                            break;
                        }
                }
            }
        }

        public class AmountSellOrderPrompt : Prompt
        {
            private PlayerMobile m_from;
            private MarketDatabase.SellOrder m_theOrder;
            private String m_theMarket;

            public AmountSellOrderPrompt(PlayerMobile from, MarketDatabase.SellOrder target, String market)
            {
                m_from = from;
                m_theOrder = target;
                m_theMarket = market;
                from.SendMessage(53, "Please enter how many of the " + target.amount + " available " + target.itemType.Name + " would you like to purchase." );
            }

            public override void OnResponse(Mobile from, string text)
            {
                int amount = Utility.ToInt32(text);

                if (amount <= 0 || amount > m_theOrder.amount)
                {
                    from.SendMessage(53, "That is not an available amount.");
                    from.SendMessage(53, "Sale Order creation cancelled.");
                    return;
                }

                from.SendGump(new PurchaseSellOrderGump(from as PlayerMobile, m_theOrder, m_theMarket, amount));

            }

            public override void OnCancel(Mobile from)
            {
                from.SendMessage(53, "Sale Order creation cancelled.");
            }
        }

        public class PurchaseSellOrderGump : Gump
        {
            private PlayerMobile m_From;
            private MarketDatabase.SellOrder m_Order;
            private String m_Market;
            private int m_amountToBuy;

            public PurchaseSellOrderGump(PlayerMobile from, MarketDatabase.SellOrder order, String market, int amount)
                : base(20, 30)
            {
                m_From = from;
                m_Order = order;
                m_Market = market;
                m_amountToBuy = amount;

                AddBackground(0, 0, 270, 150, 5054);
                AddBackground(10, 10, 250, 130, 3000);
                CancelAllGumpsAndPrompts.Run(from);

                int totalCost = m_amountToBuy * order.unitPrice;

                AddHtml(20, 15, 240, 90, "Are you sure you wish to purchase (" + m_amountToBuy + ") " + order.itemType.Name + " for the sum of " + totalCost + " gold?", true, true);

                AddHtmlLocalized(55, 110, 75, 20, 1011011, false, false); // CONTINUE
                AddButton(20, 110, 4005, 4007, 1, GumpButtonType.Reply, 0);

                AddHtmlLocalized(170, 110, 75, 20, 1011012, false, false); // CANCEL
                AddButton(135, 110, 4005, 4007, 2, GumpButtonType.Reply, 0);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                switch (info.ButtonID)
                {
                    case 1: // continue
                        {
                            MarketDatabase.BuySellOrder(m_From, m_Order, m_Market, m_amountToBuy);
                            break;
                        }
                    case 2: // cancel
                        {
                            m_From.SendMessage("Purchase aborted.");
                            break;
                        }
                }
            }
        }
        
        public class SellBuyOrderGump : Gump
        {
            private PlayerMobile m_From;
            private MarketDatabase.BuyOrder m_Order;
            private String m_Market;
            private object m_Target;
            private int m_amountToBuy;

            public SellBuyOrderGump(PlayerMobile from, MarketDatabase.BuyOrder order, String market, object target, int amount)
                : base(20, 30)
            {
                m_From = from;
                m_Order = order;
                m_Market = market;
                m_Target = target;
                m_amountToBuy = Math.Min(amount, order.amount);
                CancelAllGumpsAndPrompts.Run(from);

                AddBackground(0, 0, 270, 150, 5054);
                AddBackground(10, 10, 250, 130, 3000);

                int totalCost = m_amountToBuy * order.unitPrice;

                AddHtml(20, 15, 240, 90, "Are you sure you want to try to sell (" + m_amountToBuy + ") " + order.itemType.Name + " for the sum of " + totalCost + " gold?", true, true);

                AddHtmlLocalized(55, 110, 75, 20, 1011011, false, false); // CONTINUE
                AddButton(20, 110, 4005, 4007, 1, GumpButtonType.Reply, 0);

                AddHtmlLocalized(170, 110, 75, 20, 1011012, false, false); // CANCEL
                AddButton(135, 110, 4005, 4007, 2, GumpButtonType.Reply, 0);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                switch (info.ButtonID)
                {
                    case 1: // continue
                        {
                            MarketDatabase.FulfillBuyOrder(m_From, m_Order, m_Market, m_Target);
                            break;
                        }
                    case 2: // cancel
                        {
                            m_From.SendMessage("Sale aborted.");
                            break;
                        }
                }
            }
        }


        //Buy Orders
        public class MarketBuyOrderSearchGump : Gump
        {
            private String m_Market;
            private string m_SearchString;
            private List<MarketDatabase.BuyOrder> m_SearchResults;
            private int m_Page;

            public MarketBuyOrderSearchGump(Mobile from, string searchString, int page, List<MarketDatabase.BuyOrder> searchResults, String Market)
                : base(50, 50)
            {
                m_SearchString = searchString;
                m_SearchResults = searchResults;
                m_Page = page;
                m_Market = Market;
                CancelAllGumpsAndPrompts.Run(from);

                if (m_SearchResults.Count == 0 && page == 0 && m_SearchString != "")
                {
                    m_SearchResults = SearchForObjectTypes(Match(String.Join("", m_SearchString.Split(' '))), m_Market);
                }
                else if (m_SearchString == "" && m_Page == 0 && m_SearchResults.Count == 0)
                {
                    m_SearchResults = SearchForObjectTypes(MarketDatabase.GetAllBuyOrderTypes(m_Market), m_Market);
                }

                AddPage(0);

                AddBackground(0, 0, 420, 330/*310/*280*/, 5054);

                AddLabel(10, 6, 0x480, "Viewing the \"" + m_Market + "\" market's user requests.");

                AddImageTiled(10, 30, 400, 20, 2624);
                AddAlphaRegion(10, 30, 400, 20);
                AddImageTiled(41, 31, 184, 18, 0xBBC);
                AddImageTiled(42, 32, 182, 16, 2624);
                AddAlphaRegion(42, 32, 182, 16);

                AddButton(10, 29, 4011, 4013, 1, GumpButtonType.Reply, 0);
                AddTextEntry(44, 30, 180, 20, 0x480, 0, m_SearchString);

                AddHtmlLocalized(230, 30, 100, 20, 3010005, 0x7FFF, false, false);

                AddButton(110, 300, 4011, 4013, 4, GumpButtonType.Reply, 0);
                AddLabel(140, 300, 0x480, "Click here to place an order!");

                AddImageTiled(10, 60, 400, 200, 2624);
                AddAlphaRegion(10, 60, 400, 200);

                if (m_SearchResults.Count > 0)
                {
                    for (int i = (page * 10); i < ((page + 1) * 10) && i < m_SearchResults.Count; ++i)
                    {
                        int index = i % 10;

                        AddLabel(44, 59 + (index * 20), 0x480, m_SearchResults[i].itemType.Name);
                        AddLabel(184, 59 + (index * 20), 0x480, "x" + m_SearchResults[i].amount);
                        AddLabel(254, 59 + (index * 20), 0x480, "" + m_SearchResults[i].unitPrice + " gold each");

                        AddButton(10, 59 + (index * 20), 4005, 4007, 5 + i, GumpButtonType.Reply, 0);
                    }
                }
                else
                {
                    if (m_SearchString == "")
                        AddLabel(15, 64, 0x480, "Enter a name of an item or animal to search for.");
                    else
                    {
                        Type t = ScriptCompiler.FindTypeByName(String.Join("", m_SearchString.Split(' ')));

                        if (t == null)
                        {
                            AddLabel(15, 64, 0x480, "No item or animal with that name was found.");
                        }
                        else
                        {
                            AddLabel(15, 64, 0x480, "Searched for " + t.Name + " and found none.");
                        }
                    }
                }

                AddImageTiled(10, 270, 400, 20, 2624);
                AddAlphaRegion(10, 270, 400, 20);

                if (m_Page > 0)
                    AddButton(10, 269, 4014, 4016, 2, GumpButtonType.Reply, 0);
                else
                    AddImage(10, 269, 4014);

                AddHtmlLocalized(44, 270, 170, 20, 1061028, m_Page > 0 ? 0x7FFF : 0x5EF7, false, false); // Previous page

                if (((m_Page + 1) * 10) < m_SearchResults.Count)
                    AddButton(210, 269, 4005, 4007, 3, GumpButtonType.Reply, 0);
                else
                    AddImage(210, 269, 4005);

                AddHtmlLocalized(244, 270, 170, 20, 1061027, ((m_Page + 1) * 10) < m_SearchResults.Count ? 0x7FFF : 0x5EF7, false, false); // Next page
            }

            private static Type typeofItem = typeof(Item), typeofMobile = typeof(Mobile);

            private static void Match(string match, Type[] types, List<Type> results)
            {
                if (match.Length == 0)
                    return;

                match = match.ToLower();

                for (int i = 0; i < types.Length; ++i)
                {
                    Type t = types[i];

                    if ((typeofMobile.IsAssignableFrom(t) || typeofItem.IsAssignableFrom(t)) && t.Name.ToLower().IndexOf(match) >= 0 && !results.Contains(t))
                    {
                        ConstructorInfo[] ctors = t.GetConstructors();

                        for (int j = 0; j < ctors.Length; ++j)
                        {
                            if (ctors[j].GetParameters().Length == 0 && ctors[j].IsDefined(typeof(ConstructableAttribute), false))
                            {
                                results.Add(t);
                                break;
                            }
                        }
                    }
                }
            }

            public static List<Type> Match(string match)
            {
                List<Type> results = new List<Type>();
                Type[] types;

                Assembly[] asms = ScriptCompiler.Assemblies;

                for (int i = 0; i < asms.Length; ++i)
                {
                    types = ScriptCompiler.GetTypeCache(asms[i]).Types;
                    Match(match, types, results);
                }

                types = ScriptCompiler.GetTypeCache(Core.Assembly).Types;
                Match(match, types, results);

                results.Sort(new TypeNameComparer());

                return results;
            }

            public static List<MarketDatabase.BuyOrder> SearchForObjectTypes(List<Type> types, String Market)
            {
                List<MarketDatabase.BuyOrder> results = new List<MarketDatabase.BuyOrder>();
                for (int i = 0; i < types.Count; ++i)
                {
                    String Name = types[i].Name;

                    Hashtable BuyOrders = MarketDatabase.GetBuyOrders(Market);
                    if (BuyOrders.ContainsKey(Name))
                        results.AddRange((List<MarketDatabase.BuyOrder>)BuyOrders[Name]);
                }
                return results;
            }


            private class TypeNameComparer : IComparer<Type>
            {
                public int Compare(Type x, Type y)
                {
                    return x.Name.CompareTo(y.Name);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile from = sender.Mobile;

                switch (info.ButtonID)
                {
                    case 4: // Buy order something
                        {
                            List<Type> results = new List<Type>();
                            from.SendGump(new ItemTypeSelectGump(from,"",0,results,m_Market, false));
                            break;
                        }
                    case 1: // Search
                        {
                            TextRelay te = info.GetTextEntry(0);
                            String match = (te == null ? "" : te.Text.Trim());

                            if (match.Length < 3)
                            {
                                from.SendMessage("Invalid search string.");
                                from.SendGump(new MarketBuyOrderSearchGump(from, match, m_Page, m_SearchResults,m_Market));
                            }
                            else
                            {
                                from.SendGump(new MarketBuyOrderSearchGump(from, match, 0, SearchForObjectTypes(Match(String.Join("", match.Split(' '))), m_Market), m_Market));
                            }

                            break;
                        }
                    case 2: // Previous page
                        {
                            if (m_Page > 0)
                                from.SendGump(new MarketBuyOrderSearchGump(from, m_SearchString, m_Page - 1, m_SearchResults,m_Market));

                            break;
                        }
                    case 3: // Next page
                        {
                            if ((m_Page + 1) * 10 < m_SearchResults.Count)
                                from.SendGump(new MarketBuyOrderSearchGump(from, m_SearchString, m_Page + 1, m_SearchResults,m_Market));

                            break;
                        }
                    default:
                        {
                            int index = info.ButtonID - 5;

                            //Give them more info on item m_SearchResults[index].
                            if ( index >= 0 && index < m_SearchResults.Count )
                            {
                                if (from is PlayerMobile)
                                {
                                    from.SendMessage("Please target the item or pet you wish to sell to this Buy Order"); 
                                    from.Target = new MarketDatabase.BuyOrderTarget(from, m_SearchResults[index], m_Market);
                                }
                            }

                            break;
                        }
                }
            }
        }
   
        public class ItemTypeSelectGump : Gump
	    {
		    private string m_SearchString;
		    private List<Type> m_SearchResults;
		    private int m_Page;
            private String m_Market;


		    public ItemTypeSelectGump( Mobile from, string searchString, int page, List<Type> searchResults, String Market, bool explicitSearch ) : base( 50, 50 )
		    {
			    m_SearchString = searchString;
			    m_SearchResults = searchResults;
                m_Market = Market;
			    m_Page = page;

                CancelAllGumpsAndPrompts.Run(from);

			    AddPage( 0 );

			    AddBackground( 0, 0, 420, 300, 5054 );

                AddLabel(10, 6, 0x480, "Identify what type of object you wish to purchase.");

                AddImageTiled(10, 50, 400, 20, 2624);
                AddAlphaRegion(10, 50, 400, 20);

			    AddImageTiled( 10, 30, 400, 20, 2624 );
			    AddAlphaRegion( 10, 30, 400, 20 );
			    AddImageTiled( 41, 31, 184, 18, 0xBBC );
			    AddImageTiled( 42, 32, 182, 16, 2624 );
			    AddAlphaRegion( 42, 32, 182, 16 );

			    AddButton( 10, 29, 4011, 4013, 1, GumpButtonType.Reply, 0 );
			    AddTextEntry( 44, 30, 180, 20, 0x480, 0, searchString );

			    AddHtmlLocalized( 230, 30, 100, 20, 3010005, 0x7FFF, false, false );

			    AddImageTiled( 10, 60, 400, 200, 2624 );
			    AddAlphaRegion( 10, 60, 400, 200 );

			    if ( searchResults.Count > 0 )
			    {
				    for ( int i = (page * 10); i < ((page + 1) * 10) && i < searchResults.Count; ++i )
				    {
					    int index = i % 10;

                        AddLabel(44, 59 + (index * 20), 0x480, AddSpacesToNames.Format(searchResults[i].Name));
					    AddButton( 10, 59 + (index * 20), 4023, 4025, 4 + i, GumpButtonType.Reply, 0 );
				    }
			    }
			    else
			    {
				    AddLabel( 15, 64, 0x480, explicitSearch ? "Nothing matched your search terms." : "No results to display." );
			    }

			    AddImageTiled( 10, 270, 400, 20, 2624 );
			    AddAlphaRegion( 10, 270, 400, 20 );

			    if ( m_Page > 0 )
				    AddButton( 10, 269, 4014, 4016, 2, GumpButtonType.Reply, 0 );
			    else
				    AddImage( 10, 269, 4014 );

			    AddHtmlLocalized( 44, 270, 170, 20, 1061028, m_Page > 0 ? 0x7FFF : 0x5EF7, false, false ); // Previous page

			    if ( ((m_Page + 1) * 10) < searchResults.Count )
				    AddButton( 210, 269, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			    else
				    AddImage( 210, 269, 4005 );

			    AddHtmlLocalized( 244, 270, 170, 20, 1061027, ((m_Page + 1) * 10) < searchResults.Count ? 0x7FFF : 0x5EF7, false, false ); // Next page
		    }

		    private static Type typeofItem = typeof( Item ), typeofMobile = typeof( Mobile );

		    private static void Match( string match, Type[] types, List<Type> results )
		    {
			    if ( match.Length == 0 )
				    return;

			    match = match.ToLower();

			    for ( int i = 0; i < types.Length; ++i )
			    {
				    Type t = types[i];

				    if ( (typeofMobile.IsAssignableFrom( t ) || typeofItem.IsAssignableFrom( t )) && t.Name.ToLower().IndexOf( match ) >= 0 && !results.Contains( t ) )
				    {
					    ConstructorInfo[] ctors = t.GetConstructors();

					    for ( int j = 0; j < ctors.Length; ++j )
					    {
						    if ( ctors[j].GetParameters().Length == 0 && ctors[j].IsDefined( typeof( ConstructableAttribute ), false ) )
						    {
							    results.Add( t );
							    break;
						    }
					    }
				    }
			    }
		    }

		    public static List<Type> Match( string match )
		    {
			    List<Type> results = new List<Type>();
			    Type[] types;

			    Assembly[] asms = ScriptCompiler.Assemblies;

			    for ( int i = 0; i < asms.Length; ++i )
			    {
				    types = ScriptCompiler.GetTypeCache( asms[i] ).Types;
				    Match( match, types, results );
			    }

			    types = ScriptCompiler.GetTypeCache( Core.Assembly ).Types;
			    Match( match, types, results );

			    results.Sort( new TypeNameComparer() );

			    return results;
		    }

		    private class TypeNameComparer : IComparer<Type>
		    {
			    public int Compare( Type x, Type y )
			    {
				    return x.Name.CompareTo( y.Name );
			    }
		    }

		    public override void OnResponse( NetState sender, RelayInfo info )
		    {
			    Mobile from = sender.Mobile;

			    switch ( info.ButtonID )
			    {
				    case 1: // Search
				    {
					    TextRelay te = info.GetTextEntry( 0 );
					    String match = ( te == null ? "" : te.Text.Trim() );

					    if ( match.Length < 3 )
					    {
						    from.SendMessage( "Invalid search string." );
						    from.SendGump( new ItemTypeSelectGump( from, match, m_Page, m_SearchResults, m_Market, false ) );
					    }
					    else
					    {
                            from.SendGump(new ItemTypeSelectGump(from, match, 0, Match(String.Join("", match.Split(' '))), m_Market, true));
					    }

					    break;
				    }
				    case 2: // Previous page
				    {
					    if ( m_Page > 0 )
						    from.SendGump( new ItemTypeSelectGump( from, m_SearchString, m_Page - 1, m_SearchResults, m_Market, true ) );

					    break;
				    }
				    case 3: // Next page
				    {
					    if ( (m_Page + 1) * 10 < m_SearchResults.Count )
						    from.SendGump( new ItemTypeSelectGump( from, m_SearchString, m_Page + 1, m_SearchResults, m_Market, true ) );

					    break;
				    }
				    default:
				    {
					    int index = info.ButtonID - 4;

					    if ( index >= 0 && index < m_SearchResults.Count )
					    {
						    from.SendMessage( "Enter how many " + m_SearchResults[index].Name + " you wish to order." );
                            
                            from.Prompt = new AmountBuyOrderPrompt(from as PlayerMobile, m_SearchResults[index], m_Market);
					    }

					    break;
				    }
			    }
		    }

            public class AmountBuyOrderPrompt : Prompt
            {
                private PlayerMobile m_from;
                private Type m_theItemType;
                private String m_theMarket;

                public AmountBuyOrderPrompt(PlayerMobile from, Type type, String market)
                {
                    m_from = from;
                    m_theItemType = type;
                    m_theMarket = market;
                    from.SendMessage(53, "Please enter how many " +  type.Name + " would you like to order. (10,000 limit)" );
                }

                public override void OnResponse(Mobile from, string text)
                {
                    int amount = Utility.ToInt32(text);

                    if (amount <= 0 || amount > 10000)
                    {
                        from.SendMessage(53, "That is not an available amount.");
                        from.SendMessage(53, "Sale Order creation cancelled.");
                        return;
                    }

                    if(amount > 1)
                        from.SendMessage(53, "How much gold are you willing to pay for each of them?");
                    else
                        from.SendMessage(53, "How much gold are you willing to pay for it?");

                    from.Prompt = new MarketDatabase.BuyOrderUnitPricePrompt(m_theItemType,amount,m_theMarket);
                }

                public override void OnCancel(Mobile from)
                {
                    from.SendMessage(53, "Sale Order creation cancelled.");
                }
            }
	    }


        public class DoYouWantToFulfillABuyOrderGump : Gump
        {
            private PlayerMobile m_From;
            private object m_item;
            private String m_Market;
            private int m_amountToBuy;

            public DoYouWantToFulfillABuyOrderGump(PlayerMobile from, object item, String market)
                : base(20, 30)
            {
                m_From = from;
                m_Market = market;
                m_item = item;

                AddBackground(0, 0, 270, 190, 5054);
                AddBackground(10, 10, 250, 170, 3000);
                CancelAllGumpsAndPrompts.Run(from);

                AddHtml(20, 15, 240, 130, "There is a buy order available for your " + m_item.GetType().Name + ". Press continue to open the Buy Order menu to see the details - or cancel to proceed to post this sell order.", true, true);

                AddHtmlLocalized(55, 150, 75, 20, 1011011, false, false); // CONTINUE
                AddButton(20, 150, 4005, 4007, 1, GumpButtonType.Reply, 0);

                AddHtmlLocalized(170, 150, 75, 20, 1011012, false, false); // CANCEL
                AddButton(135, 150, 4005, 4007, 2, GumpButtonType.Reply, 0);
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                switch (info.ButtonID)
                {
                    case 1: // continue
                        {
                            List<MarketDatabase.BuyOrder> results = new List<MarketDatabase.BuyOrder>();
                            m_From.SendGump(new MarketBuyOrderSearchGump(m_From, m_item.GetType().Name, 0, results, m_Market));
                            break;
                        }
                    case 2: // cancel
                        {
                            if (m_item is Item && ((Item)m_item).Amount > 1)
                            {
                                m_From.SendMessage("Please enter a unit price for the " + m_item.GetType().Name);
                                m_From.Prompt = new MarketDatabase.SellOrderUnitPricePrompt(m_item, m_Market);
                            }
                            else
                            {
                                m_From.SendMessage("Please enter a sale price for your " + m_item.GetType().Name);
                                m_From.Prompt = new MarketDatabase.SellOrderUnitPricePrompt(m_item, m_Market);
                            }
                            break;
                        }
                }
            }
        }
        

        // User Account
        public class UserAccountSearchGump : Gump
        {
            private String m_Market;
            private List<MarketUserContainer.OrderRecord> m_SearchResults;
            private int m_Page;

            public UserAccountSearchGump(Mobile from, int page, List<MarketUserContainer.OrderRecord> searchResults, String Market)
                : base(50, 50)
            {
                m_SearchResults = (searchResults != null) ? searchResults : MarketUserContainer.GetUserOrders(from, Market);
                m_Page = page;
                m_Market = Market;

                CancelAllGumpsAndPrompts.Run(from);

                AddPage(0);

                AddBackground(0, 0, 420, 270, 5054);

                AddLabel(10, 6, 0x480, "Viewing your account for the \"" + m_Market + "\" market.");

                AddImageTiled(10, 30, 400, 200, 2624);
                AddAlphaRegion(10, 30, 400, 200);

                if (m_SearchResults.Count > 0)
                {
                    for (int i = (page * 10); i < ((page + 1) * 10) && i < m_SearchResults.Count; ++i)
                    {
                        int index = i % 10;

                        AddLabel(44, 29 + (index * 20), 0x480, (m_SearchResults[i].m_type == 1) ? "Sell" : "Buy");
                        AddLabel(70, 29 + (index * 20), 0x480, m_SearchResults[i].m_itemType.Name);
                        int amountToShow = (m_SearchResults[i].m_type == 1) ? m_SearchResults[i].m_amountCurrent : (m_SearchResults[i].m_amountStart - m_SearchResults[i].m_amountCurrent);
                        AddLabel(184, 29 + (index * 20), 0x480, m_SearchResults[i].m_unitPrice + "gp, " + amountToShow + "/" + m_SearchResults[i].m_amountStart + " %" + m_SearchResults[i].PercentComplete() + " complete.");
                        //AddLabel(254, 59 + (index * 20), 0x480, "" + searchResults[i].unitPrice + " gold each");

                        AddButton(10, 29 + (index * 20), 4005, 4007, 5 + i, GumpButtonType.Reply, 0);
                    }
                }
                else
                {
                   AddLabel(15, 34, 0x480, "You currently have no buy or sell orders.");
                }

                AddImageTiled(10, 240, 400, 20, 2624);
                AddAlphaRegion(10, 240, 400, 20);

                if (m_Page > 0)
                    AddButton(10, 239, 4014, 4016, 2, GumpButtonType.Reply, 0);
                else
                    AddImage(10, 239, 4014);

                AddHtmlLocalized(44, 240, 170, 20, 1061028, m_Page > 0 ? 0x7FFF : 0x5EF7, false, false); // Previous page

                if (((m_Page + 1) * 10) < m_SearchResults.Count)
                    AddButton(210, 239, 4005, 4007, 3, GumpButtonType.Reply, 0);
                else
                    AddImage(210, 239, 4005);

                AddHtmlLocalized(244, 240, 170, 20, 1061027, ((m_Page + 1) * 10) < m_SearchResults.Count ? 0x7FFF : 0x5EF7, false, false); // Next page
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile from = sender.Mobile;

                switch (info.ButtonID)
                {
                    case 2: // Previous page
                        {
                            if (m_Page > 0)
                                from.SendGump(new UserAccountSearchGump(from, m_Page - 1, m_SearchResults, m_Market));

                            break;
                        }
                    case 3: // Next page
                        {
                            if ((m_Page + 1) * 10 < m_SearchResults.Count)
                                from.SendGump(new UserAccountSearchGump(from, m_Page + 1, m_SearchResults, m_Market));

                            break;
                        }
                    default:
                        {
                            int index = info.ButtonID - 5;

                            //Give them more info on item m_SearchResults[index].
                            if (index >= 0 && index < m_SearchResults.Count)
                            {
                                if (from is PlayerMobile)
                                {
                                    MarketUserContainer.RemoveUserOrder(m_SearchResults[index]);
                                    from.SendGump(new UserAccountSearchGump(from, 0, null , m_Market));
                                }
                            }

                            break;
                        }
                }
            }
        }

    }
}