using System;
using System.Reflection;
using System.Collections;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Multis.Deeds;
using Server.Items;
using Server.StaticHousing;
using Server.Gumps;
using Server;
using Server.Regions;
using Server.Mobiles;

namespace Server.Gumps
{
	public class StaticHouseListGump : Gump
	{
		private StaticHouseSign m_House;

		public StaticHouseListGump( int number, ArrayList list, StaticHouseSign house ) : base( 20, 30 )
		{
			m_House = house;

			AddPage( 0 );

			AddBackground( 0, 0, 420, 430, 5054 );
			AddBackground( 10, 10, 400, 410, 3000 );

			AddButton( 20, 388, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 55, 388, 300, 20, 1011104, false, false ); // Return to previous menu

			AddHtmlLocalized( 20, 20, 350, 20, number, false, false );

			if ( list != null )
			{
				for ( int i = 0; i < list.Count; ++i )
				{
					if ( (i % 16) == 0 )
					{
						if ( i != 0 )
						{
							// Next button
							AddButton( 370, 20, 4005, 4007, 0, GumpButtonType.Page, (i / 16) + 1 );
						}

						AddPage( (i / 16) + 1 );

						if ( i != 0 )
						{
							// Previous button
							AddButton( 340, 20, 4014, 4016, 0, GumpButtonType.Page, i / 16 );
						}
					}

					Mobile m = (Mobile)list[i];

					string name;

					if ( m == null || (name = m.Name) == null || (name = name.Trim()).Length <= 0 )
						continue;

					AddLabel( 55, 55 + ((i % 16) * 20), 0, name );
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.SendGump( new StaticHouseGump( from, m_House ) );
		}
	}

	public class StaticHouseRemoveGump : Gump
	{
		private StaticHouseSign m_House;
		private ArrayList m_List, m_Copy;
		private int m_Number;

		public StaticHouseRemoveGump( int number, ArrayList list, StaticHouseSign house ) : base( 20, 30 )
		{
			m_House = house;
			m_List = list;
			m_Number = number;

			AddPage( 0 );

			AddBackground( 0, 0, 420, 430, 5054 );
			AddBackground( 10, 10, 400, 410, 3000 );

			AddButton( 20, 388, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 55, 388, 300, 20, 1011104, false, false ); // Return to previous menu

			AddButton( 20, 365, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 55, 365, 300, 20, 1011270, false, false ); // Remove now!

			AddHtmlLocalized( 20, 20, 350, 20, number, false, false );

			if ( list != null )
			{
				m_Copy = new ArrayList( list );

				for ( int i = 0; i < list.Count; ++i )
				{
					if ( (i % 15) == 0 )
					{
						if ( i != 0 )
						{
							// Next button
							AddButton( 370, 20, 4005, 4007, 0, GumpButtonType.Page, (i / 15) + 1 );
						}

						AddPage( (i / 15) + 1 );

						if ( i != 0 )
						{
							// Previous button
							AddButton( 340, 20, 4014, 4016, 0, GumpButtonType.Page, i / 15 );
						}
					}

					Mobile m = (Mobile)list[i];

					string name;

					if ( m == null || (name = m.Name) == null || (name = name.Trim()).Length <= 0 )
						continue;

					AddCheck( 34, 52 + ((i % 15) * 20), 0xD2, 0xD3, false, i );
					AddLabel( 55, 52 + ((i % 15) * 20), 0, name );
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			if ( m_List != null && info.ButtonID == 1 ) // Remove now
			{
				int[] switches = info.Switches;

				if ( switches.Length > 0 )
				{
					for ( int i = 0; i < switches.Length; ++i )
					{
						int index = switches[i];

						if ( index >= 0 && index < m_Copy.Count )
							m_List.Remove( m_Copy[index] );
					}

					if ( m_List.Count > 0 )
					{
						from.SendGump( new StaticHouseRemoveGump( m_Number, m_List, m_House ) );
						return;
					}
				}
			}

			from.SendGump( new StaticHouseGump( from, m_House ) );
		}
	}

	public class StaticBuyHouseGump : Gump
	{
		private StaticHouseSign m_House;
		private ArrayList m_List, m_Copy;

		public StaticBuyHouseGump( Mobile purchaser, StaticHouseSign house ) : base( 20, 30 )
		{
			m_House = house;

			AddPage( 0 );

			AddBackground( 0, 0, 260, 135, 5054 );
			AddBackground( 10, 10, 240, 115, 3000 );
			
			AddLabel( 55, 20, 0, "This Home is for Sale" );
			AddLabel( 55, 40, 0, "It comes with " + house.m_MaxLockDowns + " LockDowns");
			AddLabel( 55, 60, 0, "It comes with " + house.m_MaxSecures + " Secures");
			AddLabel( 55, 80, 0, "For the Price of " + house.m_Price);
			
			AddLabel( 55, 100, 0, "Click to purchase now!");
			AddButton( 20, 100, 4005, 4007, 1, GumpButtonType.Reply, 0 );




		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;


			switch ( info.ButtonID )
			{
				case 0:
				{
					break;
				}
				case 1:
				{
					if ( Banker.Withdraw( from, m_House.m_Price ) )
					{
						from.SendLocalizedMessage( 1060398, m_House.m_Price.ToString() ); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
						m_House.Owner = from;
						m_House.forSale = false;
						m_House.m_Public = false;
						m_House.RemoveKeys( from );
						m_House.ChangeLocks( from );

						from.SendGump( new StaticHouseGump( from, m_House ) );
					}	
					else
					{
						from.SendMessage( "You dont have enough gold to purchase this house!" );	
					}
					break;
				}
			}

			
		}
	}
	
	public class StaticHouseGump : Gump
	{
		private StaticHouseSign m_House;

		private ArrayList Wrap( string value )
		{
			if ( value == null || (value = value.Trim()).Length <= 0 )
				return null;

			string[] values = value.Split( ' ' );
			ArrayList list = new ArrayList();
			string current = "";

			for ( int i = 0; i < values.Length; ++i )
			{
				string val = values[i];

				string v = current.Length == 0 ? val : current + ' ' + val;

				if ( v.Length < 10 )
				{
					current = v;
				}
				else if ( v.Length == 10 )
				{
					list.Add( v );

					if ( list.Count == 6 )
						return list;

					current = "";
				}
				else if ( val.Length <= 10 )
				{
					list.Add( current );

					if ( list.Count == 6 )
						return list;

					current = val;
				}
				else
				{
					while ( v.Length >= 10 )
					{
						list.Add( v.Substring( 0, 10 ) );

						if ( list.Count == 6 )
							return list;

						v = v.Substring( 10 );
					}

					current = v;
				}
			}

			if ( current.Length > 0 )
				list.Add( current );

			return list;
		}

		public StaticHouseGump( Mobile from, StaticHouseSign house ) : base( 20, 30 )
		{
			m_House = house;

			bool isOwner = ( from.AccessLevel >= AccessLevel.GameMaster || from == m_House.Owner );
			bool isCoOwner;
			bool isFriend;

			if ( isOwner )
			{
				isCoOwner = isFriend = true;
			}
			else
			{
				isCoOwner = m_House.IsCoOwner( from );

				if ( isCoOwner )
					isFriend = true;
				else
					isFriend = m_House.IsFriend( from );
			}

			AddPage( 0 );

			if ( isFriend )
			{
				AddBackground( 0, 0, 420, 430, 5054 );
				AddBackground( 10, 10, 400,410, 3000 );
			}

			AddImage( 130, 0, 100 );

			if ( m_House.Sign != null )
			{
				ArrayList lines = Wrap( m_House.Sign.Name );

				if ( lines != null )
				{
					for ( int i = 0, y = (101 - (lines.Count * 14)) / 2; i < lines.Count; ++i, y += 14 )
					{
						string s = (string)lines[i];

						AddLabel( 130 + ((143 - (s.Length * 8)) / 2), y, 0, s );
					}
				}
			}

			if ( !isFriend )
				return;

			AddHtmlLocalized( 55, 103, 75, 20, 1011233, false, false ); // INFO
			AddButton( 20, 103, 4005, 4007, 0, GumpButtonType.Page, 1 );

			AddHtmlLocalized( 170, 103, 75, 20, 1011234, false, false ); // FRIENDS
			AddButton( 135, 103, 4005, 4007, 0, GumpButtonType.Page, 2 );

			AddHtmlLocalized( 295, 103, 75, 20, 1011235, false, false ); // OPTIONS
			AddButton( 260, 103, 4005, 4007, 0, GumpButtonType.Page, 3 );

			AddHtmlLocalized( 295, 390, 75, 20, 1011441, false, false );  // EXIT
			AddButton( 260, 390, 4005, 4007, 0, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 55, 390, 200, 20, 1011236, false, false ); // Change this house's name!
			AddButton( 20, 390, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			if (isGM(from)){
				AddButton( 20, 365, 4005, 4007, 0, GumpButtonType.Page, 6 );//door page
				AddLabel( 55, 365,0," Static House Setup ");
			}
			// Info page
			AddPage( 1 );

			AddHtmlLocalized( 20, 135, 100, 20, 1011242, false, false ); // Owned by:
			AddHtml( 120, 135, 100, 20, GetOwnerName(), false, false );

			AddHtmlLocalized( 20, 170, 275, 20, 1011237, false, false ); // Number of locked down items:
			AddHtml( 320, 170, 50, 20, m_House.LockDownCount.ToString(), false, false );

			AddHtmlLocalized( 20, 190, 275, 20, 1011238, false, false ); // Maximum locked down items:
			AddHtml( 320, 190, 50, 20, m_House.MaxLockDowns.ToString(), false, false );

			AddHtmlLocalized( 20, 210, 275, 20, 1011239, false, false ); // Number of secure containers:
			AddHtml( 320, 210, 50, 20, m_House.Secures == null ? "0" : m_House.Secures.Count.ToString(), false, false );

			AddHtmlLocalized( 20, 230, 275, 20, 1011240, false, false ); // Maximum number of secure containers:
			AddHtml( 320, 230, 50, 20, m_House.MaxSecures.ToString(), false, false );

			AddHtmlLocalized( 20, 260, 400, 20, 1018032, false, false ); // This house is properly placed.
			AddHtmlLocalized( 20, 280, 400, 20, 1018035, false, false ); // This house is of modern design.

			// Friends page
			AddPage( 2 );

			AddHtmlLocalized( 45, 130, 150, 20, 1011266, false, false ); // List of co-owners
			AddButton( 20, 130, 2714, 2715, 2, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 45, 150, 150, 20, 1011267, false, false ); // Add a co-owner
			AddButton( 20, 150, 2714, 2715, 3, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 45, 170, 150, 20, 1018036, false, false ); // Remove a co-owner
			AddButton( 20, 170, 2714, 2715, 4, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 45, 190, 150, 20, 1011268, false, false ); // Clear co-owner list
			AddButton( 20, 190, 2714, 2715, 5, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 225, 130, 155, 20, 1011243, false, false ); // List of Friends
			AddButton( 200, 130, 2714, 2715, 6, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 225, 150, 155, 20, 1011244, false, false ); // Add a Friend
			AddButton( 200, 150, 2714, 2715, 7, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 225, 170, 155, 20, 1018037, false, false ); // Remove a Friend
			AddButton( 200, 170, 2714, 2715, 8, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 225, 190, 155, 20, 1011245, false, false ); // Clear Friends list
			AddButton( 200, 190, 2714, 2715, 9, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 45, 215, 280, 20, 1011258, false, false ); // Ban someone from the house
			AddButton( 20, 215, 2714, 2715, 10, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 45, 235, 280, 20, 1011259, false, false ); // Eject someone from the house
			AddButton( 20, 235, 2714, 2715, 11, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 45, 255, 280, 20, 1011260, false, false ); // View a list of banned people
			AddButton( 20, 255, 2714, 2715, 12, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 45, 275, 280, 20, 1011261, false, false ); // Lift a ban
			AddButton( 20, 275, 2714, 2715, 13, GumpButtonType.Reply, 0 );
			
			AddLabel( 265, 215, 280, "Grant Access" );
			AddButton( 240, 215, 2714, 2715, 26, GumpButtonType.Reply, 0 );
			
			AddLabel( 265, 235, 280, "Remove Access" );
			AddButton( 240, 235, 2714, 2715, 27, GumpButtonType.Reply, 0 );
			
			AddLabel( 265, 255, 280, "View Access List" );
			AddButton( 240, 255, 2714, 2715, 28, GumpButtonType.Reply, 0 );
			
			AddLabel( 265, 275, 280, "Clear Access List" );
			AddButton( 240, 275, 2714, 2715, 29, GumpButtonType.Reply, 0 );

			// Options page
			AddPage( 3 );

			AddHtmlLocalized( 45, 150, 355, 30, 1011248, false, false ); // Transfer ownership of the house
			AddButton( 20, 150, 2714, 2715, 14, GumpButtonType.Reply, 0 );

			if (isGM(from))
			{
				AddHtmlLocalized( 45, 180, 355, 30, 1011249, false, false ); // Demolish house and get deed back
				AddButton( 20, 180, 2714, 2715, 15, GumpButtonType.Reply, 0 );
			}

			if ( !m_House.Public )
			{
				AddHtmlLocalized( 45, 210, 355, 30, 1011247, false, false ); // Change the house locks
				AddButton( 20, 210, 2714, 2715, 16, GumpButtonType.Reply, 0 );

				AddHtmlLocalized( 45, 240, 350, 90, 1011253, false, false ); // Declare this building to be public. This will make your front door unlockable.
				AddButton( 20, 240, 2714, 2715, 17, GumpButtonType.Reply, 0 );
			}
			else
			{
				AddHtmlLocalized( 45, 240, 350, 30, 1011252, false, false ); // Declare this building to be private.
				AddButton( 20, 240, 2714, 2715, 17, GumpButtonType.Reply, 0 );	
			}

			AddHtmlLocalized( 45, 280, 350, 30, 1011250, false, false ); // Change the sign type
			AddButton( 20, 280, 2714, 2715, 0, GumpButtonType.Page, 4 );
			
			// Change the sign type
			AddPage( 4 );

			for ( int i = 0; i < 24; ++i )
			{
				AddRadio( 53 + ((i / 4) * 50), 137 + ((i % 4) * 35), 210, 211, false, i + 1 );
				AddItem( 60 + ((i / 4) * 50), 130 + ((i % 4) * 35), 2980 + (i * 2) );
			}

			AddHtmlLocalized( 200, 305, 129, 20, 1011254, false, false ); // Guild sign choices
			AddButton( 350, 305, 252, 253, 0, GumpButtonType.Page, 5 );

			AddHtmlLocalized( 200, 340, 355, 30, 1011277, false, false ); // Okay that is fine.
			AddButton( 350, 340, 4005, 4007, 30, GumpButtonType.Reply, 0 );

			AddPage( 5 );

			for ( int i = 0; i < 29; ++i )
			{
				AddRadio( 53 + ((i / 5) * 50), 137 + ((i % 5) * 35), 210, 211, false, i + 25 );
				AddItem( 60 + ((i / 5) * 50), 130 + ((i % 5) * 35), 3028 + (i * 2) );
			}

			AddHtmlLocalized( 200, 305, 129, 20, 1011255, false, false ); // Shop sign choices
			AddButton( 350, 305, 250, 251, 0, GumpButtonType.Page, 4 );

			AddHtmlLocalized( 200, 340, 355, 30, 1011277, false, false ); // Okay that is fine.
			AddButton( 350, 340, 4005, 4007, 30, GumpButtonType.Reply, 0 );
			
			AddPage( 6 );
			AddLabel( 55, 145, 0, "Set the Yard for this House" );
			AddButton( 20, 145, 4005, 4007, 19, GumpButtonType.Reply, 0 );
			AddLabel( 55, 175, 0, "Initialize This House" );
			AddButton( 20, 175, 4005, 4007, 20, GumpButtonType.Reply, 0 );
			AddLabel( 55, 205, 0, "Add a Door to this House" );
			AddButton( 20, 205, 4005, 4007, 21, GumpButtonType.Reply, 0 );
			AddLabel( 55, 235, 0, "Set the maximum lockdowns of this house" );
			AddButton( 20, 235, 4005, 4007, 22, GumpButtonType.Reply, 0 );
			AddLabel( 55, 265, 0, "Set the maximum secures of this house" );
			AddButton( 20, 265, 4005, 4007, 23, GumpButtonType.Reply, 0 );
			AddLabel( 55, 295, 0, "Reset this house's region" );
			AddButton( 20, 295, 4005, 4007, 24, GumpButtonType.Reply, 0 );
			AddLabel( 55, 325, 0, "Set the Ban Location" );
			AddButton( 20, 325, 4005, 4007, 25, GumpButtonType.Reply, 0 );
		}
		
		public bool isGM (Mobile from){
			if (from.AccessLevel > AccessLevel.Player)
				return true;
			else
				return false;
		}

		
		private string GetOwnerName()
		{
			Mobile m = m_House.Owner;

			if ( m == null )
				return "(unowned)";

			string name;

			if ( (name = m.Name) == null || (name = name.Trim()).Length <= 0 )
				name = "(no name)";

			return name;
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			bool isOwner = ( from.AccessLevel >= AccessLevel.GameMaster || from == m_House.Owner );
			bool isCoOwner;
			bool isFriend;

			if ( isOwner )
			{
				isCoOwner = isFriend = true;
			}
			else
			{
				isCoOwner = m_House.IsCoOwner( from );

				if ( isCoOwner )
					isFriend = true;
				else
					isFriend = m_House.IsFriend( from );
			}

			if ( !isFriend )
				return;

			switch ( info.ButtonID )
			{
				case 1: // Rename sign
				{
					if (isOwner){
					from.Prompt = new StaticRenamePrompt( m_House );
					from.SendLocalizedMessage( 501302 ); // What dost thou wish the sign to say?
					}
					else
					{
						from.SendLocalizedMessage( 501307 ); // Only the house owner may do this.
					}

					break;
				}
				case 2: // List of co-owners
				{
					from.SendGump( new StaticHouseListGump( 1011275, m_House.CoOwners, m_House ) );

					break;
				}
				case 3: // Add co-owner
				{
					if ( isOwner )
					{
						from.Target = new StaticCoOwnerTarget( true, m_House );
						from.SendLocalizedMessage( 501328 ); // Target the person you wish to name a co-owner of your household.
					}
					else
					{
						from.SendLocalizedMessage( 501327 ); // Only the house owner may add Co-owners.
					}

					break;
				}
				case 4: // Remove co-owner
				{
					if ( isOwner )
					{
						from.SendGump( new StaticHouseRemoveGump( 1011274, m_House.CoOwners, m_House ) );
					}
					else
					{
						from.SendLocalizedMessage( 501329 ); // Only the house owner may remove co-owners.
					}

					break;
				}
				case 5: // Clear co-owners
				{
					if ( isOwner )
					{
						if ( m_House.CoOwners != null )
							m_House.CoOwners.Clear();

						from.SendLocalizedMessage( 501333 ); // All co-owners have been removed from this house.
					}
					else
					{
						from.SendLocalizedMessage( 501330 ); // Only the house owner may remove co-owners.
					}

					break;
				}
				case 6: // List friends
				{
					from.SendGump( new StaticHouseListGump( 1011273, m_House.Friends, m_House ) );

					break;
				}
				case 7: // Add friend
				{
					if ( isCoOwner )
					{
						from.Target = new StaticHouseFriendTarget( true, m_House );
						from.SendLocalizedMessage( 501317 ); // Target the person you wish to name a friend of your household.
					}
					else
					{
						from.SendLocalizedMessage( 501316 ); // Only the house owner may add friends.
					}

					break;
				}
				case 8: // Remove friend
				{
					if ( isCoOwner )
					{
						from.SendGump( new StaticHouseRemoveGump( 1011272, m_House.Friends, m_House ) );
					}
					else
					{
						from.SendLocalizedMessage( 501318 ); // Only the house owner may remove friends.
					}

					break;
				}
				case 9: // Clear friends
				{
					if ( isCoOwner )
					{
						if ( m_House.Friends != null )
							m_House.Friends.Clear();

						from.SendLocalizedMessage( 501332 ); // All friends have been removed from this house.
					}
					else
					{
						from.SendLocalizedMessage( 501319 ); // Only the house owner may remove friends.
					}

					break;
				}
				case 10: // Ban
				{
					from.Target = new StaticHouseBanTarget( true, m_House );
					from.SendLocalizedMessage( 501325 ); // Target the individual to ban from this house.

					break;
				}
				case 11: // Eject
				{
					from.Target = new StaticHouseKickTarget( m_House );
					from.SendLocalizedMessage( 501326 ); // Target the individual to eject from this house.

					break;
				}
				case 12: // List bans
				{
					from.SendGump( new StaticHouseListGump( 1011271, m_House.Bans, m_House ) );

					break;
				}
				case 13: // Remove ban
				{
					from.SendGump( new StaticHouseRemoveGump( 1011269, m_House.Bans, m_House ) );

					break;
				}
				case 14: // Transfer ownership
				{
					if ( isOwner )
					{
						from.Target = new StaticHouseOwnerTarget( m_House );
						from.SendLocalizedMessage( 501309 ); // Target the person to whom you wish to give this house.
					}
					else
					{
						from.SendLocalizedMessage( 501310 ); // Only the house owner may do this.
					}

					break;
				}
				case 15: // Demolish house
				{	
					if ( isOwner )
						m_House.Delete();
					else
						from.SendLocalizedMessage( 501320 ); // Only the house owner may do this.
					
					break;
				}
                case 16: // Change locks 
                { 
			if ( m_House.Public )
			{
				from.SendLocalizedMessage( 501669 );// Public houses are always unlocked.
			}
			else
			{
				if ( isOwner )
				{
					m_House.RemoveKeys( from );
					m_House.ChangeLocks( from );

					from.SendLocalizedMessage( 501306 ); // The locks on your front door have been changed, and new master keys have been placed in your bank and your backpack.
				}
				else
				{
					from.SendLocalizedMessage( 501303 ); // Only the house owner may change the house locks.
				}
			}

			break; 
                }
				case 17: // Declare public
				{
					if ( isOwner )
					{
						if ( m_House.Public && m_House.FindPlayerVendor() != null )
						{
							from.SendLocalizedMessage( 501887 ); // You have vendors working out of this building. It cannot be declared private until there are no vendors in place.
							break;
						}

						m_House.Public = !m_House.Public;
						if ( !m_House.Public )
						{
							m_House.ChangeLocks( from );

							from.SendLocalizedMessage( 501888 ); // This house is now private.
							from.SendLocalizedMessage( 501306 ); // The locks on your front door have been changed, and new master keys have been placed in your bank and your backpack.
						}
						else
						{
							m_House.RemoveKeys( from );
							m_House.RemoveLocks();
							from.SendLocalizedMessage( 501886 );//This house is now public. Friends of the house my now have vendors working out of this building.
						}
					}
					else
					{
						from.SendLocalizedMessage( 501307 ); // Only the house owner may do this.
					}

					break;
				}
				case 19: // Set the house's boundries
				{
					m_House.CreateYard(from);
					break;
				}
				case 20: // Creates the house Region and sets the initial values you should set yard first!
				{
					m_House.InitializeHouse(from);
					break;
				}
				case 21: // Add a Door to the door array this allows it to be rekeyed
				{
					m_House.addDoor(from);
					break;
				}
				case 22: // Set the Max Number of LockDowns
				{
					from.Prompt = new StaticSetLockDownsPrompt( m_House );
					from.SendMessage("Enter the number of lockdowns this house will have:" );
					break;
				}
				case 23: // Set the Max Secures Amount
				{
					from.Prompt = new StaticSetSecurePrompt( m_House );
					from.SendMessage("Enter the number of secures this house will have:"); 
					break;
				}
				case 24: // Reset this House
				{
					Region.RemoveRegion( m_House.m_Region );
					
					if ( m_House.m_LockDowns != null )
					{
						for ( int i = 0; i < m_House.m_LockDowns.Count; ++i )
						{
							Item item = (Item)m_House.m_LockDowns[i];
		
							if ( item != null )
								item.Movable = true;
						}
					}
					if ( m_House.m_Secures != null )
					{
						for ( int i = 0; i < m_House.m_Secures.Count; ++i )
						{
							Item item = (Item)m_House.m_Secures[i];
		
							if ( item != null )
								item.Movable = true;
						}
					}
					
					m_House.houseBoundries = new ArrayList();
					m_House.m_Owner = null;
					m_House.m_OrgOwner = null;
					m_House.Name = "a house sign";
					m_House.Movable = false;
					m_House.m_Region = new StaticHouseRegion( m_House );
					m_House.m_Region.Coords = new ArrayList();
					m_House.m_Sign = m_House;
					m_House.m_Doors = new ArrayList();
					m_House.m_LockDowns = new ArrayList();
					m_House.m_Secures = new ArrayList();
					m_House.m_CoOwners = new ArrayList();
					m_House.m_Friends = new ArrayList();
					m_House.m_Bans = new ArrayList();
					m_House.m_Access = new ArrayList();
					m_House.m_Addons = new ArrayList();
					m_House.owner = null;
					m_House.m_Price = 0;
					m_House.forSale = false;
					m_House.m_Public = true;
					m_House.houseSetUp = false;
					m_House.houseInitialized = false;
					m_House.m_BanLocation.X = 0;
					m_House.m_BanLocation.Y = 0;
					m_House.m_BanLocation.Z = 0;
					
					from.SendMessage("This house has been reset"); 
					break;
				}
				case 25: // Set the Banning Location
				{
					from.Target = new BanLocationTarget(m_House);
					from.SendMessage("Ban Location is now set!"); 
					break;
				}
				case 26: // Grant Access rights
				{
					from.Target = new StaticHouseAccessTarget(m_House);
					break;
				}
				case 27: // Remove Access rights
				{
					from.SendGump( new StaticHouseRemoveGump( 1060677, m_House.CanAccess, m_House));
					break;
				}
				case 28 : // View Access List
				{
					from.SendGump( new StaticHouseListGump( 1060677, m_House.CanAccess, m_House));
					break;
				}
				case 29 : // Wipe Access List
				{
					if ( isCoOwner )
					{
						if ( m_House.CanAccess != null )
							m_House.CanAccess .Clear();

						from.SendMessage( "The Access List is Wiped" ); // All friends have been removed from this house.
					}
					else
					{
						from.SendLocalizedMessage( 501319 ); // Only the house owner may remove friends.
					}

					break;
				}
				
				case 30: // Change type
				{
					if ( info.Switches.Length > 0 )
					{
						int index = info.Switches[0] - 1;

						if ( index >= 0 && index < 53 )
							m_House.StaticChangeSignType( 2980 + (index * 2) );
					}

					break;
				}
			}
		}
	}
}

namespace Server.Prompts
{
	public class StaticRenamePrompt : Prompt
	{
		private StaticHouseSign m_House;

		public StaticRenamePrompt( StaticHouseSign house )
		{
			m_House = house;
		}

		public override void OnResponse( Mobile from, string text )
		{
			if ( m_House.Sign != null )
				m_House.Sign.Name = text;

			from.SendMessage( "Sign changed." );
		}
	}
	
	public class StaticSetLockDownsPrompt : Prompt
	{
		private StaticHouseSign m_House;

		public StaticSetLockDownsPrompt( StaticHouseSign house )
		{
			m_House = house;
		}

		public bool isGM (Mobile from){
			if (from.AccessLevel == AccessLevel.GameMaster|| from.AccessLevel == AccessLevel.Counselor||from.AccessLevel == AccessLevel.Administrator)
				return true;
			else
				return false;
		}
		public override void OnResponse( Mobile from, string text )
		{
			int tempInt = m_House.m_MaxLockDowns;
			try
			{
				tempInt = Convert.ToInt32(text);
			}
			catch
			{
				from.SendMessage( "This must be an integer value!" );
			}
			if ( isGM( from ) )
			{

					m_House.m_MaxLockDowns = tempInt;
					from.SendMessage( "Maximum lockdowns set to "+ tempInt );

			}
			else
				return;
			}
		}
		
	public class StaticSetSecurePrompt : Prompt
	{
		private StaticHouseSign m_House;

		public StaticSetSecurePrompt( StaticHouseSign house )
		{
			m_House = house;
		}

		public bool isGM (Mobile from){
			if (from.AccessLevel == AccessLevel.GameMaster|| from.AccessLevel == AccessLevel.Counselor||from.AccessLevel == AccessLevel.Administrator)
				return true;
			else
				return false;
		}
		
		public override void OnResponse( Mobile from, string text )
		{
			int tempInt = m_House.m_MaxSecures;
			try
			{
				tempInt = Convert.ToInt32(text);
			}
			catch
			{
				from.SendMessage( "This must be an integer value!" );
			}
			
			if ( isGM( from ) )
			{
					m_House.m_MaxSecures = tempInt;
					from.SendMessage( "Maximum secures set to "+ tempInt );
			}
			else
				return;
		}
	}
}
