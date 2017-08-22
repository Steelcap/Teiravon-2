using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Engines.BulkOrders
{
	public class LargeBODAcceptGump : Gump
	{
		private LargeBOD m_Deed;
		private Mobile m_From;
		private Item m_Item;

		public LargeBODAcceptGump( Mobile from, LargeBOD deed ) : base( 50, 50 )
		{
			m_From = from;
			m_Deed = deed;

			m_From.CloseGump( typeof( LargeBODAcceptGump ) );
			m_From.CloseGump( typeof( SmallBODAcceptGump ) );

			LargeBulkEntry[] entries = deed.Entries;

			AddPage( 0 );

			AddBackground( 25, 10, 430, 240 + (entries.Length * 24), 5054 );

			AddImageTiled( 33, 20, 413, 221 + (entries.Length * 24), 2624 );
			AddAlphaRegion( 33, 20, 413, 221 + (entries.Length * 24) );

			AddImage( 20, 5, 10460 );
			AddImage( 430, 5, 10460 );
			AddImage( 20, 225 + (entries.Length * 24), 10460 );
			AddImage( 430, 225 + (entries.Length * 24), 10460 );

			AddHtmlLocalized( 180, 25, 120, 20, 1045134, 0x7FFF, false, false ); // A large bulk order

			AddHtmlLocalized( 40, 48, 350, 20, 1045135, 0x7FFF, false, false ); // Ah!  Thanks for the goods!  Would you help me out?

			AddHtmlLocalized( 40, 72, 210, 20, 1045138, 0x7FFF, false, false ); // Amount to make:
			AddLabel( 250, 72, 1152, deed.AmountMax.ToString() );

			AddHtmlLocalized( 40, 96, 120, 20, 1045137, 0x7FFF, false, false ); // Items requested:

			int y = 120;

			for ( int i = 0; i < entries.Length; ++i, y += 24 )
			{
				m_Item = (Item)Activator.CreateInstance( entries[i].Details.Type );

				if ( m_Item.Name == null )
					AddHtmlLocalized( 40, y, 210, 20, entries[i].Details.Number, 0x7FFF, false, false );
				else
					AddHtml( 40, y, 210, 20, "<basefont color=WHITE>"+m_Item.Name, false, false );

				m_Item.Delete();
			}

			if ( deed.RequireExceptional || deed.Material != BulkMaterialType.None )
			{
				AddHtmlLocalized( 40, y, 210, 20, 1045140, 0x7FFF, false, false ); // Special requirements to meet:
				y += 24;

				if ( deed.RequireExceptional )
				{
					AddHtmlLocalized( 40, y, 350, 20, 1045141, 0x7FFF, false, false ); // All items must be exceptional.
					y += 24;
				}

				if ( deed.Material != BulkMaterialType.None && deed.Material >= BulkMaterialType.Spined )
				{
					AddHtmlLocalized( 40, y, 350, 20, GetMaterialNumberFor( deed.Material ), 0x7FFF, false, false ); // All items must be made with x material.
					y += 24;
				}
				else switch ((int)deed.Material)
				{
					case 1: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with dull copper ingots", false, false ); break;
					case 2: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with shadow iron ingots", false, false ); break;
					case 3: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with copper ingots", false, false ); break;
					case 4: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with bronze ingots", false, false ); break;
					case 5: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with gold ingots", false, false ); break;
					case 6: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with agapite ingots", false, false ); break;
					case 7: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with verite ingots", false, false ); break;
					case 8: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with valorite ingots", false, false ); break;
					case 9: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with pine", false, false ); break;
					case 10: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with redwood", false, false ); break;
					case 11: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with white pine", false, false ); break;
					case 12: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with ashwood", false, false ); break;
					case 13: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with silver birch", false, false ); break;
					case 14: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with yew", false, false ); break;
					case 15: AddHtml( 40, y, 350, 25, "<basefont color=WHITE>All items must be crafted with black oak", false, false ); break;
				}
			}

			AddHtmlLocalized( 40, 192 + (entries.Length * 24), 350, 20, 1045139, 0x7FFF, false, false ); // Do you want to accept this order?

            AddHtml(320, 25, 210, 20, "<basefont color=YELLOW> 5000 gold", false, false);
			AddButton( 100, 216 + (entries.Length * 24), 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 135, 216 + (entries.Length * 24), 120, 20, 1006044, 0x7FFF, false, false ); // Ok

			AddButton( 275, 216 + (entries.Length * 24), 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 310, 216 + (entries.Length * 24), 120, 20, 1011012, 0x7FFF, false, false ); // CANCEL
		}

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Container cont;
            if (info.ButtonID == 1) // Ok
            {
                cont = m_From.Backpack;
                if (cont.ConsumeTotal(typeof(Gold), 5000))
                {
                    if (m_From.PlaceInBackpack(m_Deed))
                    {
                        m_From.SendLocalizedMessage(1045152); // The bulk order deed has been placed in your backpack.
                    }
                    else
                    {
                        m_From.SendLocalizedMessage(1045150); // There is not enough room in your backpack for the deed.
                        m_From.BankBox.AddItem(m_Deed);
                        m_From.SendMessage("The contract has been given to your banker for safe keeping.");
                    }
                }
                else
                {
                    if (m_From.BankBox.ConsumeTotal(typeof(Gold), 5000))
                    {
                        if (m_From.PlaceInBackpack(m_Deed))
                        {
                            m_From.SendLocalizedMessage(1045152); // The bulk order deed has been placed in your backpack.
                        }
                        else
                        {
                            m_From.SendLocalizedMessage(1045150); // There is not enough room in your backpack for the deed.
                            m_From.BankBox.AddItem(m_Deed);
                            m_From.SendMessage("The contract has been given to your banker for safe keeping.");
                        }
                    }
                    else
                    {
                        m_From.SendLocalizedMessage(500192);
                        m_Deed.Delete();
                    }
                }
            }
            else
            {
                m_Deed.Delete();
            }
        }

		public static int GetMaterialNumberFor( BulkMaterialType material )
		{
			if ( material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Valorite )
				return 1045142 + (int)(material - BulkMaterialType.DullCopper);
			else if ( material >= BulkMaterialType.Spined && material <= BulkMaterialType.Barbed )
				return 1049348 + (int)(material - BulkMaterialType.Spined);

			return 0;
		}
	}
}