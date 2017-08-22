using System;
using Server.Prompts;
using Server.Gumps;

namespace Server.Items
{
	public class QuestScroll1 : Item
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
		public QuestScroll1() : this( 1 )
		{
		}

		[Constructable]
		public QuestScroll1( int amount ) : base( 0x227B )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Scroll 1";
			Hue = 2;
		}

		public QuestScroll1( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new QuestScroll1( amount ), amount );
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
			private QuestScroll1 m_mscroll;

			public AddresseePrompt( QuestScroll1 scroll )
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
			private QuestScroll1 m_mscroll;

			public MessagePrompt1( QuestScroll1 scroll )
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
			private QuestScroll1 m_mscroll;

			public MessagePrompt2( QuestScroll1 scroll )
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
			private QuestScroll1 m_mscroll;

			public MessagePrompt3( QuestScroll1 scroll )
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
			private QuestScroll1 m_mscroll;

			public MessagePrompt4( QuestScroll1 scroll )
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
			private QuestScroll1 m_mscroll;

			public SenderPrompt( QuestScroll1 scroll )
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
	
	public class QuestScroll2 : Item
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
		public QuestScroll2() : this( 1 )
		{
		}

		[Constructable]
		public QuestScroll2( int amount ) : base( 0x227B )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Scroll 2";
			Hue = 17;
		}

		public QuestScroll2( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new QuestScroll2( amount ), amount );
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
			private QuestScroll2 m_mscroll;

			public AddresseePrompt( QuestScroll2 scroll )
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
			private QuestScroll2 m_mscroll;

			public MessagePrompt1( QuestScroll2 scroll )
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
			private QuestScroll2 m_mscroll;

			public MessagePrompt2( QuestScroll2 scroll )
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
			private QuestScroll2 m_mscroll;

			public MessagePrompt3( QuestScroll2 scroll )
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
			private QuestScroll2 m_mscroll;

			public MessagePrompt4( QuestScroll2 scroll )
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
			private QuestScroll2 m_mscroll;

			public SenderPrompt( QuestScroll2 scroll )
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

	public class QuestScroll3 : Item
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
		public QuestScroll3() : this( 1 )
		{
		}

		[Constructable]
		public QuestScroll3( int amount ) : base( 0x227B )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Scroll 3";
			Hue = 32;
		}

		public QuestScroll3( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new QuestScroll3( amount ), amount );
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
			private QuestScroll3 m_mscroll;

			public AddresseePrompt( QuestScroll3 scroll )
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
			private QuestScroll3 m_mscroll;

			public MessagePrompt1( QuestScroll3 scroll )
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
			private QuestScroll3 m_mscroll;

			public MessagePrompt2( QuestScroll3 scroll )
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
			private QuestScroll3 m_mscroll;

			public MessagePrompt3( QuestScroll3 scroll )
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
			private QuestScroll3 m_mscroll;

			public MessagePrompt4( QuestScroll3 scroll )
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
			private QuestScroll3 m_mscroll;

			public SenderPrompt( QuestScroll3 scroll )
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

	public class QuestToken1 : Item
	{
		[Constructable]
		public QuestToken1() : this( 1 )
		{
		}

		[Constructable]
		public QuestToken1( int amount ) : base( 0x2809 )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Token 1";
			Hue = 34;
		}

		public QuestToken1( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class QuestToken2 : Item
	{
		[Constructable]
		public QuestToken2() : this( 1 )
		{
		}

		[Constructable]
		public QuestToken2( int amount ) : base( 0x2809 )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Token 2";
			Hue = 2;
		}

		public QuestToken2( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class QuestToken3 : Item
	{
		[Constructable]
		public QuestToken3() : this( 1 )
		{
		}

		[Constructable]
		public QuestToken3( int amount ) : base( 0x2809 )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Token 3";
			Hue = 17;
		}

		public QuestToken3( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class QuestToken4 : Item
	{
		[Constructable]
		public QuestToken4() : this( 1 )
		{
		}

		[Constructable]
		public QuestToken4( int amount ) : base( 0x2809 )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Token 4";
			Hue = 32;
		}

		public QuestToken4( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class QuestToken5 : Item
	{
		[Constructable]
		public QuestToken5() : this( 1 )
		{
		}

		[Constructable]
		public QuestToken5( int amount ) : base( 0x2809 )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Token 5";
			Hue = 47;
		}

		public QuestToken5( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class QuestToken6 : Item
	{
		[Constructable]
		public QuestToken6() : this( 1 )
		{
		}

		[Constructable]
		public QuestToken6( int amount ) : base( 0x2809 )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Token 6";
			Hue = 62;
		}

		public QuestToken6( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class QuestToken7 : Item
	{
		[Constructable]
		public QuestToken7() : this( 1 )
		{
		}

		[Constructable]
		public QuestToken7( int amount ) : base( 0x2809 )
		{
			Stackable = false;
			Weight = 1.0;
			Amount = amount;
			Name = "Quest Token 7";
			Hue = 86;
		}

		public QuestToken7( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

}
