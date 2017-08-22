using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class WebItem : Item
	{
		private string m_webaddress = "http://www.teiravon.com";

		[Constructable]
		public WebItem() : this( 1 )
		{
		}

		[Constructable]
		public WebItem( int amount ) : base( 0x14F0 )
		{
			Name = "Web Item";
			Weight = 1.0;
			Hue = 2001;
			Stackable = false;
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new WebItem( amount ), amount );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string WebAddress
		{
			get{ return m_webaddress; }
			set{ m_webaddress = value;}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
				return;

			if (this.WebAddress != null)
			{
				from.SendGump(new WebItemGump((PlayerMobile)from, this));
			}
		}

		public WebItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (string) m_webaddress );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_webaddress = reader.ReadString();
		}
	}
	
	public class WebItemGump : Gump
	{
		PlayerMobile player;
		WebItem webby;
		
		public enum Buttons
		{
			ButtonOK,
		}

		public WebItemGump(PlayerMobile m_player, WebItem m_webby): base( 0, 0 )
		{
			player = m_player;
			webby = m_webby;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 100, 250, 200, 9200);
			this.AddHtml( 135, 110, 180, 117, @"You are about to open a website using your default browser.  Click Ok to continue or right click to close this window.", (bool)false, (bool)false);
			this.AddButton(205, 249, 4005, 248, (int)Buttons.ButtonOK, GumpButtonType.Reply, 0);

		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			switch ( info.ButtonID )
			{
				case (int)Buttons.ButtonOK:
				{
					try
					{
						player.LaunchBrowser(webby.WebAddress);
					}
					catch
					{
						if (player.AccessLevel > AccessLevel.Player)
							player.SendMessage("Bad URL");
					}
					break;
				}
				
				default:
					break;
			}
		}
	}
	
}
