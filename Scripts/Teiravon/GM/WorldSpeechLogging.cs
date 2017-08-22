/** World Speech Logging **/

/* Scripted on 3/3/2005 by Khaz
 *
 * For support, please e-mail me at dreki_herra@yahoo.com
 *
 * This script is free to use and modify as you see fit.
 * When sharing, please give credit where it is due.
*/

using System;
using System.IO;
using Server;
using Server.Accounting;
using Server.Mobiles;
using Server.Network;

namespace Server
{
	public class SpeechLogging
	{
		private static StreamWriter m_toWrite;
		private static bool m_Enabled = true;
		private static bool m_ConsoleEnabled = false;

		public static bool Enabled{ get{ return m_Enabled; } set{ m_Enabled = value; } }
		public static bool ConsoleEnabled{ get{ return m_ConsoleEnabled; } set{ m_ConsoleEnabled = value; } }

		public static StreamWriter toWrite{ get{ return m_toWrite; } }

		public static void Initialize()
		{
			EventSink.Speech += new SpeechEventHandler( EventSink_Speech );

			if( !Directory.Exists( "Logs" ) )
				Directory.CreateDirectory( "Logs" );

			string directory = "Logs/Speech";

			if( !Directory.Exists( directory ) )
				Directory.CreateDirectory( directory );

			Console.Write( "Speech Logging: Loading..." );

			try
			{
				m_toWrite = new StreamWriter( Path.Combine( directory, String.Format( "{0}.log", DateTime.Now.ToLongDateString() ) ), true );

				m_toWrite.AutoFlush = true;

				m_toWrite.WriteLine( "####################################" );
				m_toWrite.WriteLine( "Logging started on {0}", DateTime.Now );
				m_toWrite.WriteLine();

				Console.WriteLine( "done" );
			}
			catch
			{
				Console.WriteLine( "failed" );
			}

			Commands.Register( "ConsoleListen", AccessLevel.Administrator, new CommandEventHandler( ConsoleListen_OnCommand ) );
		}

		public static object Format( object o )
		{
			if( o is Mobile )
			{
				Mobile m = (Mobile)o;

				if( m.Account == null )
					return String.Format( "{0} (no account)", m );
				else
					return String.Format( "{0} ('{1}')", m, ((Account)m.Account).Username );
			}
			else if( o is Item )
			{
				Item item = (Item)o;

				return String.Format( "0x{0:X} ({1})", item.Serial.Value, item.GetType().Name );
			}

			return o;
		}

		public static void WriteLine( Mobile from, string format, params object[] args )
		{
			WriteLine( from, String.Format( format, args ) );
		}

		public static void WriteLine( Mobile from, string text )
		{
			if( !m_Enabled )
				return;

			try
			{
				m_toWrite.WriteLine( "{0}: {1}: {2}", DateTime.Now, from.NetState, text );

				string path = Core.BaseDirectory;

				Account acct = from.Account as Account;

				string name = ( acct == null ? from.Name : acct.Username );

				AppendPath( ref path, "Logs" );
				AppendPath( ref path, "Speech" );
				AppendPath( ref path, from.AccessLevel.ToString() );
				path = Path.Combine( path, String.Format( "{0}.log", name ) );

				using( StreamWriter sw = new StreamWriter( path, true ) )
					sw.WriteLine( "{0}: {1}: {2}", DateTime.Now, from.NetState, text );
			}
			catch
			{
			}
		}

		private static char[] m_NotSafe = new char[]{ '\\', '/', ':', '<', '>', '|', '{', '}' };

		public static void AppendPath( ref string path, string toAppend )
		{
			path = Path.Combine( path, toAppend );

			if( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );
		}

		public static string Safe( string ip )
		{
			if ( ip == null )
				return "null";

			ip = ip.Trim();

			if ( ip.Length == 0 )
				return "empty";

			bool isSafe = true;

			for ( int i = 0; isSafe && i < m_NotSafe.Length; ++i )
				isSafe = ( ip.IndexOf( m_NotSafe[i] ) == -1 );

			if ( isSafe )
				return ip;

			System.Text.StringBuilder sb = new System.Text.StringBuilder( ip );

			for ( int i = 0; i < m_NotSafe.Length; ++i )
				sb.Replace( m_NotSafe[i], '_' );

			return sb.ToString();
		}

		public static void EventSink_Speech( SpeechEventArgs e )
		{
			WriteLine( e.Mobile, "{0}: {1}", Format( e.Mobile ), e.Speech );

			if( ConsoleEnabled )
				Console.WriteLine( e.Mobile.Name + String.Format( " ({0}): ", ((Account)e.Mobile.Account).Username ) + e.Speech );
		}

		[Usage( "ConsoleListen <true | false>")]
		[Description( "Enables or disables outputting speech to the console." )]
		public static void ConsoleListen_OnCommand( CommandEventArgs e )
		{
			if( e.Length == 1 )
			{
				m_ConsoleEnabled = e.GetBoolean( 0 );
				e.Mobile.SendMessage( "Console speech output has been {0}.", m_ConsoleEnabled ? "enabled" : "disabled" );
				Console.WriteLine( "World listen has been {0}.", m_ConsoleEnabled ? "enabled" : "disabled" );
			}
			else
			{
				e.Mobile.SendMessage( "Format: ConsoleListen <true | false >" );
			}
		}
	}
}
