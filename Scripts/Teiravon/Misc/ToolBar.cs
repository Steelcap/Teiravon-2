using System; 
using System.Net; 
using Server; 
using Server.Accounting; 
using Server.Gumps; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network;
using Server.Targeting;
using Server.Targets;
using Server.Scripts.Commands;
using Scripts.ToolBarGump;

namespace Server.Scripts.Commands
{
	public class CustomCmdHandler4
	{
		public static void Initialize()
		{
			Register( "GmTool", AccessLevel.GameMaster, new CommandEventHandler( Lhide_OnCommand ) );
		}
		
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
			Server.Commands.Register( command, access, handler );
		}
	
		[Usage( "Lhide" )]
		[Description( "Hides/Unhides the staff member" )]
		public static void Lhide_OnCommand( CommandEventArgs e )
		{
			e.Mobile.CloseGump( typeof( ToolBarGump ) );
			e.Mobile.SendGump( new ToolBarGump() );	
		}	
	}
}

namespace Scripts.ToolBarGump
{
	public class ToolBarGump : Gump
	{
		public ToolBarGump() : base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddPage(1);
			this.AddImageTiled(-1, 33, 12, 34, 5057);
			this.AddImageTiled(5, 35, 593, 32, 5058);
			this.AddImageTiled(12, 65, 587, 12, 5061);
			this.AddImageTiled(10, 28, 587, 12, 5055);
			this.AddImage(0, 28, 5054);
			this.AddImage(0, 65, 5060);
			this.AddImage(597, 65, 5062);
			this.AddImage(597, 28, 5056);
			this.AddLabel(35, 32, 0, @"[Props");
			this.AddButton(32, 51, 2311, 2312, 1, GumpButtonType.Reply, 1);
			this.AddLabel(87, 32, 0, @"[Tele");
			this.AddButton(80, 51, 2311, 2312, 2, GumpButtonType.Reply, 1);
			this.AddLabel(130, 32, 0, @"[M Tele");
			this.AddButton(129, 51, 2311, 2312, 3, GumpButtonType.Reply, 1);
			this.AddLabel(185, 32, 0, @"[Who");
			this.AddButton(177, 51, 2311, 2312, 4, GumpButtonType.Reply, 1);
			this.AddLabel(239, 32, 0, @"[Go");
			this.AddButton(226, 51, 2311, 2312, 5, GumpButtonType.Reply, 1);
			this.AddLabel(284, 32, 0, @"[Res");
			this.AddButton(275, 51, 2311, 2312, 6, GumpButtonType.Reply, 1);
			this.AddLabel(330, 32, 0, @"[Kill");
			this.AddButton(325, 51, 2311, 2312, 7, GumpButtonType.Reply, 1);
			this.AddLabel(370, 32, 0, @"[Delete");
			this.AddButton(374, 51, 2311, 2212, 8, GumpButtonType.Reply, 1);
			this.AddLabel(418, 31, 0, @"[M Delete  [Hide");
			this.AddButton(423, 51, 2311, 2312, 9, GumpButtonType.Reply, 1);
			this.AddButton(9, 40, 5541, 5540, 0, GumpButtonType.Page, 2);
			this.AddImageTiled(598, 38, 12, 27, 5059);
			this.AddButton(475, 52, 2311, 2312, 11, GumpButtonType.Reply, 1);
			this.AddButton(523, 52, 2311, 2312, 12, GumpButtonType.Reply, 1);
			this.AddLabel(524, 32, 0, @"[UnHide");
			
			this.AddPage(2);
			this.AddImageTiled(-1, 33, 12, 34, 5057);
			this.AddImageTiled(5, 35, 25, 32, 5058);
			this.AddImageTiled(12, 65, 25, 12, 5061);
			this.AddImageTiled(10, 28, 25, 12, 5055);
			this.AddImage(0, 28, 5054);
			this.AddImage(0, 65, 5060);
			this.AddImage(32, 65, 5062);
			this.AddImage(32, 28, 5056);
			this.AddButton(9, 40, 5538, 5539, 0, GumpButtonType.Page, 1);
			this.AddImageTiled(30, 38, 12, 27, 5059);
		
		}
		

		public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
        { 
			Mobile from = state.Mobile;
			
			switch ( info.ButtonID ) 
            { 
				case 1:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}props", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				} 
				
				case 2:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Tele", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				case 3:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}M Tele", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				case 4:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Who", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				case 5:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Go", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				
				case 6:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Res", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				case 7:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Kill", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				case 8:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Delete", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				case 9:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}M Delete", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				
				case 11:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Self Hide", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
				
				case 12:
				{
					string prefix = Server.Commands.CommandPrefix;

					if ( from.AccessLevel > AccessLevel.Counselor )
					{
					Commands.Handle( from, String.Format( "{0}Self Unhide", prefix ) );	
					}
					else
					{
					from.SendMessage( "NOT" );
					}
				from.SendGump( new ToolBarGump() );	
                break;
				}
			}	
        }	 
	} 
}


