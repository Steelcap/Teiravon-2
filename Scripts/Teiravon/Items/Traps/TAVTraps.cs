using System;
using System.Collections;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class TrapKit : Item
	{
		[Constructable]
		public TrapKit() : base( 7864 )
		{
			Name = "Trap Kit";
			Weight = 1.0;
		}

		public TrapKit( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new TrapGump( from as TeiravonMobile ) );
		}
	}

	public class TrapGump : Gump
	{
		private TeiravonMobile m_From;

		private const int LabelHue = 0x480;
		private const int LabelColor = 0x7FFF;
		private const int FontColor = 0xFFFFFF;
		private string notice = "";

		private int y = 40;
		private int y2 = 40;

		public TrapGump( TeiravonMobile from ) : base( 40, 40 )
		{
			m_From = from;

			from.CloseGump( typeof( TrapGump ) );

			AddPage( 0 );

			AddBackground( 0, 0, 530, 437, 5054 );
			AddImageTiled( 10, 10, 510, 22, 2624 );
			AddImageTiled( 10, 292, 150, 45, 2624 );
			AddImageTiled( 165, 292, 355, 45, 2624 );
			AddImageTiled( 10, 342, 510, 85, 2624 );
			AddImageTiled( 10, 37, 200, 250, 2624 );
			AddImageTiled( 215, 37, 305, 250, 2624 );
			AddAlphaRegion( 10, 10, 510, 417 );

			AddHtml( 10, 12, 510, 20, String.Format( "<BASEFONT COLOR=#{0:X6}><Center>TRAP CREATION SYSTEM</Center></BASEFONT>", FontColor ), false, false );

			AddHtmlLocalized( 10, 37, 200, 22, 1044010, LabelColor, false, false ); // <CENTER>CATEGORIES</CENTER>
			AddHtmlLocalized( 215, 37, 305, 22, 1044011, LabelColor, false, false ); // <CENTER>SELECTIONS</CENTER>
			AddHtmlLocalized( 10, 302, 150, 25, 1044012, LabelColor, false, false ); // <CENTER>NOTICES</CENTER>

			AddButton( 15, 402, 4017, 4019, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 405, 150, 18, 1011441, LabelColor, false, false ); // EXIT

			/* Trap Selection */
			ListTrapCategory( 0, 1, "Spike Traps" );
			ListTrapCategory( 0, 2, "Snare Traps" );
			ListTrapCategory( 0, 3, "Poison Traps" );
			ListTrapCategory( 0, 4, "Gas Traps" );
			ListTrapCategory( 0, 5, "Explosion Traps" );
			//ListTrapCategory( 0, 6, "Magical Traps" );

			AddHtml( 170, 295, 350, 40, String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", FontColor, notice ), false, false );

			// Spike Trap
			AddPage( 1 );

			ListTrapSelection( 1, "Spike Trap" );
			ListTrapSelection( 2, "Giant Spike Trap" );

			// Snare Trap
			AddPage( 2 );

			ListTrapSelection( 3, "Snare Trap" );

			// Poison Trap
			AddPage( 3 );

			ListTrapSelection( 4, "Lesser Poison" );
			ListTrapSelection( 5, "Normal Poison" );
			ListTrapSelection( 6, "Greater Poison" );
			ListTrapSelection( 7, "Lethal Poison" );

			// Gas Trap
			AddPage( 4 );

			ListTrapSelection( 8, "Gas Trap" );

			// Explosion Trap
			AddPage( 5 );

			ListTrapSelection( 9, "Flame Trap" );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID <= 0 )
				return; // Canceled
			
			BaseTrap m_Trap = null;
			TeiravonMobile m_Player = (TeiravonMobile)sender.Mobile;
		
			switch ( info.ButtonID )
			{
				case 1:
					m_Trap = new SpikeTrap();

					break;
				
				case 2:
					m_Trap = new GiantSpikeTrap();

					break;
				
				/* case 3:

				case 4:
					m_Trap = new GasTrap();

					break;
				
				case 5:
					m_Trap = new FlameSpurtTrap();

					break;
				*/
			}

			if ( m_Trap != null )
			{
				m_Trap.Trapper = (int)m_Player.Serial;
				m_Trap.TrapSkill = m_Player.Skills.RemoveTrap.Base;

				m_Trap.MoveToWorld( m_Player.Location, m_Player.Map );
				m_Player.SendMessage( "You place a trap..." );
			}
		}

		private void ListTrapSelection( int id, string title )
		{
			AddLabel( 260, y2 += 25, 0x480, title );
			AddButton( 225, y2, 4005, 4007, id, GumpButtonType.Reply, 0 );
		}

		private void ListTrapCategory( int id, int page, string title )
		{
			AddLabel( 50, y += 25, 0x480, title );
			AddButton( 15, y, 4005, 4007, id, GumpButtonType.Page, page );
		}
	}
}