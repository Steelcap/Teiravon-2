using System;
using Server.Prompts;
using Server.Gumps;

namespace Server.Items
{
	public class MessageScroll : Item
	{
		private string m_addressee;
		private string m_message;
		private string m_sender;

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public string Addressee
		{
			get{return m_addressee;}
			set{m_addressee = value;}
		}
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public string Message
		{
			get{return m_message;}
			set{m_message = value;}
		}
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public string Sender
		{
			get{return m_sender;}
			set{m_sender = value;}
		}

		[Constructable]
		public MessageScroll() : this( 1 )
		{
		}

		[Constructable]
		public MessageScroll( int amount ) : base( 0x227B )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "A blank message scroll";
		}

		public MessageScroll( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new MessageScroll( amount ), amount );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage("This must be in your pack to use it");
				return;
			}
			if (this.Sender == null)
			{
				from.SendMessage("Enter the addressee or title");
				from.Prompt = new AddresseePrompt( this );
			}
			else
			{
				from.SendGump( new MScrollGump( this.Addressee, this.Message, this.Sender ) );
			}
		
		}

		private class AddresseePrompt : Prompt
		{
			private MessageScroll m_mscroll;

			public AddresseePrompt( MessageScroll scroll )
			{
				m_mscroll = scroll;
			}

			public override void OnResponse( Mobile from, string text )
			{
					m_mscroll.Addressee = text;
					from.SendMessage("Enter message part 1");
					from.Prompt = new MessagePrompt1( m_mscroll );
			}
		}

		private class MessagePrompt1 : Prompt
		{
			private MessageScroll m_mscroll;

			public MessagePrompt1( MessageScroll scroll )
			{
				m_mscroll = scroll;
			}

			public override void OnResponse( Mobile from, string text )
			{
					m_mscroll.Message = text;
					from.SendMessage("Enter message part 2");
					from.Prompt = new MessagePrompt2 ( m_mscroll );
			}
		}

		private class MessagePrompt2 : Prompt
		{
			private MessageScroll m_mscroll;

			public MessagePrompt2( MessageScroll scroll )
			{
				m_mscroll = scroll;
			}

			public override void OnResponse( Mobile from, string text )
			{
					m_mscroll.Message = m_mscroll.Message + text;
					from.SendMessage("Enter message part 3");
					from.Prompt = new MessagePrompt3 ( m_mscroll );
			}
		}

		private class MessagePrompt3 : Prompt
		{
			private MessageScroll m_mscroll;

			public MessagePrompt3( MessageScroll scroll )
			{
				m_mscroll = scroll;
			}

			public override void OnResponse( Mobile from, string text )
			{
					m_mscroll.Message = m_mscroll.Message + text;
					from.SendMessage("Enter message part 4");
					from.Prompt = new MessagePrompt4 ( m_mscroll );
			}
		}

		private class MessagePrompt4 : Prompt
		{
			private MessageScroll m_mscroll;

			public MessagePrompt4( MessageScroll scroll )
			{
				m_mscroll = scroll;
			}

			public override void OnResponse( Mobile from, string text )
			{
					m_mscroll.Message = m_mscroll.Message + text;
					from.SendMessage("Enter your signature");
					from.Prompt = new SenderPrompt ( m_mscroll );
			}
		}

		private class SenderPrompt : Prompt
		{
			private MessageScroll m_mscroll;

			public SenderPrompt( MessageScroll scroll )
			{
				m_mscroll = scroll;
			}

			public override void OnResponse( Mobile from, string text )
			{
					m_mscroll.Sender = text;
					from.SendMessage("Your message is completed!");
					m_mscroll.Name = "A message scroll addressed to " + m_mscroll.Addressee;
			}
		}
		
	public class MScrollGump : Gump
	{
		public MScrollGump(string address, string msg, string signer): base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 10, 300, 450, 5170);
			this.AddHtml( 130, 50, 240, 40, @address, (bool)false, (bool)false);
			this.AddHtml( 130, 110, 240, 225, @msg, (bool)false, (bool)false);
			this.AddLabel(130, 350, 0, @"Signed,");
			this.AddHtml( 130, 375, 240, 40, @signer, (bool)false, (bool)false);

		}
	}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (string) m_addressee);
			writer.Write( (string) m_message);
			writer.Write( (string) m_sender);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_addressee = reader.ReadString();
			m_message = reader.ReadString();
			m_sender = reader.ReadString();
		}
	}
}
