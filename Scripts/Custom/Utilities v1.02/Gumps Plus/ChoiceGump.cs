using System;
using System.Collections;
using Server;
using Server.Gumps;

namespace Knives.Utils
{
	public class ChoiceGump : GumpPlus
	{
		public static void SendTo( Mobile m, string title, int width, TimerStateCallback callback, Hashtable table )
		{
			new ChoiceGump( m, title, width, callback, table );
		}

		private string c_Title;
		private int c_Width;
		private TimerStateCallback c_Callback;
		private Hashtable c_Table;

		public ChoiceGump( Mobile m, string title, int width, TimerStateCallback callback, Hashtable table ) : base( m, 100, 100 )
		{
			c_Title = title;
			c_Width = width;
			c_Callback = callback;
			c_Table = table;

			NewGump();
		}

		protected override void BuildGump()
		{try{

			int height = 50+(25*c_Table.Count);

			if ( c_Title == "" )
				height-=20;

			AddBackground( 0, 0, c_Width, height, 0x13BE );

			int y = 10;

			if ( c_Title != "" )
				AddHtml( 0, 10, c_Width, 25, HTML.White + "<CENTER>" + c_Title, false, false );
			else
				y = -10;

			ArrayList list = new ArrayList( c_Table.Values );

			for( int i = 0; i < list.Count; ++i )
			{
				AddHtml( 40, y+=25, c_Width, 20, HTML.White + list[i].ToString(), false, false );
				AddButton( 15, y+2, 0x93A, 0x93A, "Respond", new TimerStateCallback( Respond ), i );
			}

		}catch{ Errors.Report( "ChoiceGump-> BuildGump" ); } }

		private void Respond( object obj )
		{
			if ( !(obj is int))
				return;

			ArrayList list = new ArrayList( c_Table.Keys );

			if ( list[(int)obj] != null )
				c_Callback.Invoke( list[(int)obj] );
			else
				c_Callback.Invoke( -1 );
		}

		protected override void OnClose()
		{
			c_Callback.Invoke( -1 );
		}
	}
}