using System;
using Server;
using Server.Gumps;

namespace Knives.Utils
{
	public class ResponseGump : GumpPlus
	{
		public static void SendTo( Mobile m, int width, int height, string text, string title, TimerStateCallback callback )
		{
			new ResponseGump( m, width, height, text, title, callback );
		}

		private int c_Width, c_Height;
		private string c_Text, c_Title;
		private TimerStateCallback c_Callback;

		public ResponseGump( Mobile m, int width, int height, string text, string title, TimerStateCallback callback ) : base( m, 100, 100 )
		{
			c_Width = width;
			c_Height = height;
			c_Text = text;
			c_Title = title;
			c_Callback = callback;

			NewGump();
		}

		protected override void BuildGump()
		{
			AddBackground( 0, 0, c_Width, c_Height, 0x13BE );

			AddHtml( 0, 10, c_Width, 25, HTML.White + "<CENTER>" + c_Title, false, false );

			AddImageTiled( 20, 40, c_Width-40, c_Height-90, 0xBBC );
			AddTextField( 20, 40, c_Width-40, c_Height-90, 0x480, 0, c_Text );

			AddButton( c_Width/2-30, c_Height-35, 0x98B, 0x98B, "Respond", new TimerCallback( Respond ) );
			AddHtml( c_Width/2-23, c_Height-32, 51, 20, HTML.White + "<CENTER>Submit", false, false );
		}

		private void Respond()
		{
			c_Callback.Invoke( GetTextField( 0 ) );
		}

		protected override void OnClose()
		{
			c_Callback.Invoke( c_Text );
		}
	}
}