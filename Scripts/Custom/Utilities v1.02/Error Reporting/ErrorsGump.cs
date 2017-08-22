using System;
using Server;
using Server.Network;
using Server.Gumps;

namespace Knives.Utils
{
	public class ErrorsGump : GumpPlus
	{
		private static string s_Help = "     Errors reported by either this chat system or other staff members!  Administrators have the power to clear this list.  All staff members can report an error using the [errors <text> command.";

		public static void SendTo( Mobile m )
		{
			new ErrorsGump( m );
			Errors.Checked.Add( m );
		}

		private const int Width = 400;
		private const int Height = 400;

		public ErrorsGump( Mobile m ) : base( m, 100, 100 )
		{
			m.CloseGump( typeof( ErrorsGump ) );

			NewGump();
		}

		protected override void BuildGump()
		{try{

			AddBackground( 0, 0, Width, Height, 0xE10 );

			AddButton( Width-20, Height-20, 0x5689, 0x5689, "Help", new TimerCallback( Help ) );

			int y = 0;

			AddHtml( 0, y+=15, Width, 45, HTML.White + "<CENTER>Error Log", false, false );
			AddBackground( 30, y+=20, Width-60, 3, 0x13BE );

			string str = HTML.Black;
			foreach( string text in Errors.ErrorLog )
				str += text;

			AddHtml( 20, y+=20, Width-40, Height-y-50, str, true, true );

			if ( Owner.AccessLevel == AccessLevel.Administrator )
			{
				AddButton( Width/2-30, Height-40, 0x98B, 0x98B, "Clear log", new TimerCallback( ClearLog ) );
				AddHtml( Width/2-23, Height-37, 51, 20, HTML.White + "<CENTER>Clear", false, false );
			}

		}catch{ Errors.Report( String.Format( "ErrorsGump-> BuildGump-> |{0}|", Owner ) ); } }

		private void Help()
		{
			NewGump();
			InfoGump.SendTo( Owner, 300, 300, HTML.White + s_Help, true );
		}

		private void ClearLog()
		{
			Errors.ErrorLog.Clear();
			Owner.SendMessage( "The error log is now clear." );
		}
	}
}