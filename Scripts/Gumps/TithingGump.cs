using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	public class TithingGump : Gump
	{
		private Mobile m_From;
		private int m_Offer;

		public TithingGump( Mobile from, int offer ) : base( 160, 40 )
		{
			int totalGold = from.TotalGold;

			if ( offer > totalGold )
				offer = totalGold;
			else if ( offer < 0 )
				offer = 0;

			m_From = from;
			m_Offer = offer;

			AddPage( 0 );

			AddImage( 30, 30, 102 );

			AddHtmlLocalized( 95, 100, 120, 100, 1060198, 0, false, false ); // May your wealth bring blessings to those in need, if tithed upon this most sacred site.

			AddLabel( 57, 274, 0, "Gold:" );
			AddLabel( 87, 274, 53, (totalGold - offer).ToString() );

			AddLabel( 137, 274, 0, "Tithe:" );
			AddLabel( 172, 274, 53, offer.ToString() );

			AddButton( 105, 230, 5220, 5220, 2, GumpButtonType.Reply, 0 );
			AddButton( 113, 230, 5222, 5222, 2, GumpButtonType.Reply, 0 );
			AddLabel( 108, 228, 0, "<" );
			AddLabel( 112, 228, 0, "<" );

			AddButton( 127, 230, 5223, 5223, 1, GumpButtonType.Reply, 0 );
			AddLabel( 131, 228, 0, "<" );

			AddButton( 147, 230, 5224, 5224, 3, GumpButtonType.Reply, 0 );
			AddLabel( 153, 228, 0, ">" );

			AddButton( 168, 230, 5220, 5220, 4, GumpButtonType.Reply, 0 );
			AddButton( 176, 230, 5222, 5222, 4, GumpButtonType.Reply, 0 );
			AddLabel( 172, 228, 0, ">" );
			AddLabel( 176, 228, 0, ">" );

			AddButton( 217, 272, 4023, 4024, 5, GumpButtonType.Reply, 0 );

			// Teiravon Tithe
			TeiravonMobile m_Player = m_From as TeiravonMobile;

            if (m_Player != null && (m_Player.IsCavalier() || m_Player.IsPaladin()) && offer == 0 && totalGold > 0)
			{
				m_Player.SendMessage( "You have {0} War Mount Death{1}." , m_Player.WarMountDeaths, ( m_Player.WarMountDeaths > 0 ) ? "s" : "" );
				m_Player.SendMessage( "Every 500 tithed gold coin will reduce one war mount death." );
			}
			else if ( m_Player != null && offer == 0 && totalGold == 0 )
				m_Player.SendMessage( "You dont have any gold to tithe!" );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			switch ( info.ButtonID )
			{
				case 0:
				{
					// You have decided to tithe no gold to the shrine.
					m_From.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1060193 );
					break;
				}
				case 1:
				case 2:
				case 3:
				case 4:
				{
					int offer = 0;

					switch ( info.ButtonID )
					{
						case 1: offer = m_Offer - 100; break;
						case 2: offer = 0; break;
						case 3: offer = m_Offer + 100; break;
						case 4: offer = m_From.TotalGold; break;
					}

					m_From.SendGump( new TithingGump( m_From, offer ) );
					break;
				}
				case 5:
				{
					int totalGold = m_From.TotalGold;

					if ( m_Offer > totalGold )
						m_Offer = totalGold;
					else if ( m_Offer < 0 )
						m_Offer = 0;

					if ( (m_From.TithingPoints + m_Offer) > 100000 ) // TODO: What's the maximum?
						m_Offer = (100000 - m_From.TithingPoints);

					if ( m_Offer <= 0 )
					{
						// You have decided to tithe no gold to the shrine.
						m_From.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1060193 );
						break;
					}

					Container pack = m_From.Backpack;

					if ( pack != null && pack.ConsumeTotal( typeof( Gold ), m_Offer ) )
					{
						// You tithe gold to the shrine as a sign of devotion.
						m_From.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1060195 );
						m_From.TithingPoints += m_Offer;

						m_From.PlaySound( 0x243 );
						m_From.PlaySound( 0x2E6 );

						TeiravonMobile m_Player = m_From as TeiravonMobile;
						int reduce = (int)( m_Offer / 500 );

						if ( m_Player != null &&( m_Player.IsCavalier() || m_Player.IsPaladin() ) )
						{
							if ( (m_Player.WarMountDeaths - reduce) < 0 && (reduce > 0) )
							{
								m_Player.SendMessage( "You have erased {0} war mount deaths", m_Player.WarMountDeaths );
								m_Player.WarMountDeaths = 0;
							}
							else
							{
								m_Player.SendMessage( "You have erased {0} war mount deaths", reduce );
								m_Player.WarMountDeaths -= reduce;
							}
						}
					}
					else
					{
						// You do not have enough gold to tithe that amount!
						m_From.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1060194 );
					}

					break;
				}
			}
		}
	}
}