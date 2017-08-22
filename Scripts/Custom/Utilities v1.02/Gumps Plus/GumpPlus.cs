using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;

namespace Knives.Utils
{
	public class GumpPlus : Gump
	{
		private Mobile c_Owner;
		private Hashtable c_Buttons, c_Fields;
		private bool c_Override;

		public Mobile Owner{ get{ return c_Owner; } }
        public GumpInfo Info { get { return GumpInfo.GetInfo(c_Owner, this.GetType()); } }
		public bool Override{ get{ return c_Override; } set{ c_Override = value; } }

		public GumpPlus( Mobile m, int x, int y ) : base( x, y )
		{
			c_Owner = m;

			c_Buttons = new Hashtable();
			c_Fields = new Hashtable();
			c_Override = true;
		}

		public void Clear()
		{
			Entries.Clear();
			c_Buttons.Clear();
			c_Fields.Clear();
		}

		public void NewGump()
		{
			NewGump( true );
		}

		public void NewGump( bool clear )
		{
			if ( clear )
				Clear();

			BuildGump();

			if ( c_Override )
				ModifyGump();

			c_Owner.SendGump( this );
		}

		public void SameGump()
		{
			c_Owner.SendGump( this );
		}

		protected virtual void BuildGump()
		{
		}

		private void ModifyGump()
		{try{

			AddPage( 0 );

			int maxWidth = 0;
			int maxHeight = 0;
			GumpBackground bg;

			foreach( GumpEntry entry in Entries )
				if ( entry is GumpBackground )
				{
					bg = (GumpBackground)entry;

					if ( bg.X + bg.Width > maxWidth )
						maxWidth = bg.X + bg.Width;
					if ( bg.Y + bg.Height > maxHeight )
						maxHeight = bg.Y + bg.Height;
				}

			AddImage( maxWidth, maxHeight, 0x28DC, 0x387 );
            AddButton(maxWidth + 10, maxHeight + 4, 0x93A, 0x93A, "Transparency", new TimerCallback(Trans));
            AddButton(maxWidth + 10, maxHeight + 15, 0x938, 0x938, "Default", new TimerCallback(Default));
            AddButton(maxWidth - 5, maxHeight + 2, 0x2626, 0x2627, "BackgroundDown", new TimerCallback(BackDown));
            AddButton(maxWidth + 19, maxHeight + 2, 0x2622, 0x2623, "BackgroundUp", new TimerCallback(BackUp));
            AddButton(maxWidth - 5, maxHeight + 13, 0x2626, 0x2627, "TextColorDown", new TimerCallback(TextDown));
            AddButton(maxWidth + 19, maxHeight + 13, 0x2622, 0x2623, "TextColorUp", new TimerCallback(TextUp));

			if ( !GumpInfo.HasMods( c_Owner, GetType() ) )
				return;
            
            ArrayList backs = new ArrayList();

            foreach( GumpEntry entry in new ArrayList( Entries ) )
			{
				if ( entry is GumpBackground )
				{
					if ( Info.Background != -1 )
						((GumpBackground)entry).GumpID = Info.Background;

					backs.Add( entry );
				}
				else if ( entry is GumpAlphaRegion && !Info.DefaultTrans && !Info.Transparent )
				{
					((GumpAlphaRegion)entry).Width = 0;
					((GumpAlphaRegion)entry).Height = 0;
				}
				else if ( entry is HtmlPlus )
				{
					if ( !((HtmlPlus)entry).Override || Info.TextColorRGB == "" )
						continue;

					string text = ((HtmlPlus)entry).Text;
					int num = 0;
					int length = 0;
					char[] chars;

					if ( text == null )
						continue;

					while( (num = text.ToLower().IndexOf( "<basefont" )) != -1 || (num = text.ToLower().IndexOf( "</font" )) != -1 )
					{
						length = 0;
						chars = text.ToCharArray();

						for( int i = num; i < chars.Length; ++i )
							if ( chars[i] == '>' )
							{
								length = i-num+1;
								break;
							}

						if ( length == 0 )
							break;

						text = text.Substring( 0, num ) + text.Substring( num+length, text.Length-num-length );
					}

					((HtmlPlus)entry).Text = Info.TextColor + text;
				}
			}

			if ( !Info.DefaultTrans && Info.Transparent )
				foreach( GumpBackground back in backs )
					AddAlphaRegion( back.X, back.Y, back.Width, back.Height );

			SortEntries();

		}catch{ Errors.Report( "GumpPlus-> ModifyGump-> " + GetType() ); } }

		private void SortEntries()
		{
			ArrayList list = new ArrayList();

			foreach( GumpEntry entry in new ArrayList( Entries ) )
				if ( entry is GumpBackground )
				{
					list.Add( entry );
					Entries.Remove( entry );
				}

			foreach( GumpEntry entry in new ArrayList( Entries ) )
				if ( entry is GumpAlphaRegion )
				{
					list.Add( entry );
					Entries.Remove( entry );
				}

			list.AddRange( Entries );

			Entries.Clear();
			Entries.AddRange( list );
		}

		private int UniqueButton()
		{
			int random = 0;

			do
			{
				random = Utility.Random( 20000 );

			}while( c_Buttons[random] != null );

			return random;
		}

		public void AddButton( int x, int y, int up, int down, TimerCallback callback )
		{
			AddButton( x, y, up, down, "None", callback );
		}

		public void AddButton( int x, int y, int up, int down, string name, TimerCallback callback )
		{
			int id = UniqueButton();

			ButtonPlus button = new ButtonPlus( x, y, up, down, id, name, callback );

			Add( button );

			c_Buttons[id] = button;
		}

		public void AddButton( int x, int y, int up, int down, TimerStateCallback callback, object arg )
		{
			AddButton( x, y, up, down, "None", callback, arg );
		}

		public void AddButton( int x, int y, int up, int down, string name, TimerStateCallback callback, object arg )
		{
			int id = UniqueButton();

			ButtonPlus button = new ButtonPlus( x, y, up, down, id, name, callback, arg );

			Add( button );

			c_Buttons[id] = button;
		}

		public new void AddHtml( int x, int y, int width, int height, string text, bool back, bool scroll )
		{
			AddHtml( x, y, width, height, text, back, scroll, true );
		}

		public void AddHtml( int x, int y, int width, int height, string text, bool back, bool scroll, bool over )
		{
			HtmlPlus html = new HtmlPlus( x, y, width, height, text, back, scroll, over );

			Add( html );
		}

		public void AddTextField( int x, int y, int width, int height, int color, int id, string text )
		{
			base.AddTextEntry( x, y, width, height, color, id, text );

			c_Fields[id] = text;
		}

		public string GetTextField( int id )
		{
			if ( c_Fields[id] == null )
				return "";

			return c_Fields[id].ToString();
		}

		protected virtual void OnClose()
		{
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			string name = "";

			try{
            
            if ( info.ButtonID == -5 )
            {
                NewGump();
				return;
			}

			foreach( TextRelay t in info.TextEntries )
				c_Fields[t.EntryID] = t.Text;

			if ( info.ButtonID == 0 )
				OnClose();

			if ( c_Buttons[info.ButtonID] == null || !(c_Buttons[info.ButtonID] is ButtonPlus) )
				return;

			name = ((ButtonPlus)c_Buttons[info.ButtonID]).Name;

			((ButtonPlus)c_Buttons[info.ButtonID]).Invoke();

		}catch{ Errors.Report( String.Format( "GumpPlus-> OnResponse-> |{0}|-> {1}-> {2}", c_Owner, GetType(), name ) ); } }

        private void Trans()
        {
            Info.Transparent = !Info.Transparent;

            NewGump();
        }

        private void BackUp()
        {
            Info.BackgroundUp();

            NewGump();
        }

        private void BackDown()
        {
            Info.BackgroundDown();

            NewGump();
        }

        private void TextUp()
        {
            Info.TextColorUp();
            
            NewGump();
        }

        private void TextDown()
        {
            Info.TextColorDown();
            
            NewGump();
        }

        private void Default()
        {
            Info.Default();
            
            NewGump();
        }
    }
}