using System;
using System.Collections;
using Server;
using Server.Scripts.Commands;

namespace Knives.Utils
{
	public class CommandsGump : GumpPlus
	{
		public static void SendTo( Mobile m )
		{
			new CommandsGump( m );
		}

		private static string s_Help = "     With this interface, you can modify every command and the Access Level required to use it!  Should I warn you about changing some of these?  Sure, why not.  "
			+ "Remember to only allow staff levels you can trust access to certain commands!  This system won't let you run two commands from a single input, nor can you input a blank command or one with spaces.  "
			+ "If you get lost, simple hit the red button just to the left of the command to reset it.  Enjoy!";

		private const int Width = 250;
		private const int Height = 350;

		public ArrayList c_List = new ArrayList();
		public int c_Page = 0;

		public CommandsGump( Mobile m ) : base( m, 100, 100 )
		{
			NewGump();
		}

		protected override void BuildGump()
		{
			AddBackground( 0, 0, Width, Height, 0x13BE );

			AddButton( Width-20, Height-20, 0x5689, 0x5689, "Help", new TimerCallback( Help ) );

			BuildList();
			DisplayList();
		}

		private void BuildList()
		{
			c_List.Clear();

			foreach( string str in Server.Commands.Entries.Keys )
				if ( ((CommandEntry)Server.Commands.Entries[str]).AccessLevel <= Owner.AccessLevel )
					c_List.Add( str );

			c_List.Sort( new InternalSorter() );
		}

		private void DisplayList()
		{
			int toList = 10;

			int beginAt = toList*c_Page;

			while( c_Page > 0 )
			{
				if ( beginAt > c_List.Count )
					beginAt = toList * --c_Page;
				else
					break;
			}

			if ( c_Page != 0 )
				AddButton( Width/2-7, 30, 0x15E0, 0x15E4, "Next Page", new TimerCallback( PageDown ) );

			if ( c_Page < (c_List.Count-1)/toList )
				AddButton( Width/2-7, Height-25, 0x15E2, 0x15E6, "Previous Page", new TimerCallback( PageUp ) );

			int y = 30;
			int x = 20;

			AddHtml( 20, 10, Width-40, 21, "<CENTER>Command Mod Menu", false, false );

			for( int i = beginAt; i < c_List.Count && i < beginAt+toList; ++i )
			{
				AddImageTiled( x, y+=25, 100, 21, 0xBBA );
				AddTextField( x, y, 100, 21, 0x480, i, c_List[i].ToString() );
				AddButton( x+105, y+3, 0x93A, 0x93A, "Submit Command Name", new TimerStateCallback( ApplyCommand ), i );

				if( Commands.HasMod( c_List[i].ToString() ) )
					AddButton( 5, y+3, 0x29F6, 0x29F6, "Restore Default", new TimerStateCallback( RestoreDefault ), i );

				AddHtml( x+130, y, 100, 21, ((CommandEntry)Server.Commands.Entries[c_List[i].ToString()]).AccessLevel.ToString(), false, false );

				if ( ((CommandEntry)Server.Commands.Entries[c_List[i].ToString()]).AccessLevel < AccessLevel.Administrator )				
					AddButton( x+210, y, 0x983, 0x983, "Access Up", new TimerStateCallback( AccessUp ), i );

				if ( ((CommandEntry)Server.Commands.Entries[c_List[i].ToString()]).AccessLevel > AccessLevel.Player )				
					AddButton( x+210, y+10, 0x985, 0x985, "Access Down", new TimerStateCallback( AccessDown ), i );
			}
		}

		private void Help()
		{
			NewGump();
			InfoGump.SendTo( Owner, 300, 300, s_Help, true );
		}

		private void PageUp()
		{
			c_Page++;
			NewGump();
		}

		private void PageDown()
		{
			c_Page--;
			NewGump();
		}

		private void RestoreDefault( object obj )
		{
			if ( !(obj is int) )
				return;

			if ( Commands.HasMod( c_List[(int)obj].ToString() ) )
				Commands.RestoreCommand( c_List[(int)obj].ToString() );

			NewGump();
		}

		private void ApplyCommand( object obj )
		{
			if ( !(obj is int) )
				return;

			if ( GetTextField( (int)obj ) == "" || GetTextField( (int)obj ).IndexOf( " " ) != -1 )
			{
				NewGump();
				return;
			}

			if ( Server.Commands.Entries[GetTextField( (int)obj )] != null )
				Owner.SendMessage( "That command already exists." );
			else
				Commands.ApplyCommand( c_List[(int)obj].ToString(), GetTextField( (int)obj ) );

			NewGump();
		}

		private void AccessUp( object obj )
		{
			if ( !(obj is int) )
				return;

			Commands.Access( c_List[(int)obj].ToString(), 1 );
			NewGump();
		}

		private void AccessDown( object obj )
		{
			if ( !(obj is int) )
				return;

			Commands.Access( c_List[(int)obj].ToString(), -1 );
			NewGump();
		}


		private class InternalSorter : IComparer
		{
			public InternalSorter()
			{
			}

			public int Compare( object x, object y )
			{
				if ( x == null && y == null )
					return 0;
				else if ( x == null )
					return -1;
				else if ( y == null )
					return 1;

				string a = x as string;
				string b = y as string;

				if ( a == null || b == null )
					return 0;

				return Insensitive.Compare( a, b );
			}
		}
	}
}