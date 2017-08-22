using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Prompts;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;

namespace Server.Teiravon.War
{
	public class WarStoneGump : Gump
	{
		private Mobile m_Player;
		private WarStone m_WarStone;

		public WarStoneGump( Mobile beholder, WarStone warStone ) : base( 20, 30 )
		{
			m_Player = beholder;
			m_WarStone = warStone;

			Dragable = true;

			AddPage( 0 );
			AddBackground( 0, 0, 550, 400, 5054 );
			AddBackground( 10, 10, 530, 380, 3000 );

			AddHtml( 20, 15, 200, 35, warStone.Name, false, false );

			AddHtml( 220, 15, 250, 35, warStone.Leader.Name + " " + warStone.Leader.Title, false, false );

			AddHtml( 55, 50, 100, 20, "Loyal to:", false, false );

			AddHtml( 55, 70, 470, 20, warStone.Leader.Name, false, false );

			AddHtml( 250, 50, 170, 20, "Current state of affairs:", false, false );
			AddHtml( 250, 70, 200, 20, "Population: " + warStone.Members.Count, false, false );
			AddHtml( 250, 90, 200, 20, "Gold: " + warStone.Gold, false, false );
			AddHtml( 250, 110, 200, 20, "Wood: " + warStone.Wood, false, false );
			AddHtml( 250, 130, 200, 20, "Guards / Max: " + warStone.Guards.Count + " / " + warStone.MaxGuards, false, false );
			AddHtml( 250, 150, 200, 20, "Siege Weapons / Max: " + warStone.SiegeWeapons.Count + " / " + warStone.MaxSiegeWeapons, false, false );
			AddHtml( 250, 170, 200, 20, "Guard Points: " + warStone.GuardPoints.Count, false, false );

			AddButton( 20, 100, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtml( 55, 100, 470, 30, "View the current citizens.", false, false );

			AddButton( 20, 130, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtml( 55, 130, 470, 30, "Add citizen to the town.", false, false );

			AddButton( 20, 190, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			AddHtml( 55, 190, 470, 30, "Deposit gold.", false, false );

			AddButton( 20, 220, 4005, 4007, 4, GumpButtonType.Reply, 0 );
			AddHtml( 55, 220, 470, 30, "Deposit logs.", false, false );

			AddButton( 20, 250, 4005, 4007, 5, GumpButtonType.Reply, 0 );
			AddHtml( 55, 250, 470, 30, "Hire guards. (250 gp / 20 logs)", false, false );

			AddButton( 20, 280, 4005, 4007, 6, GumpButtonType.Reply, 0 );
			AddHtml( 55, 280, 470, 30, "Purchase guard point. (10 gp)", false, false );

			AddButton( 20, 310, 4005, 4007, 7, GumpButtonType.Reply, 0 );
			AddHtml( 55, 310, 470, 30, "Purchase Siege Weapon. (Cannon/1000gp)", false, false );

			AddButton( 20, 360, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 55, 360, 470, 30, 1011441, false, false ); // EXIT
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			try {
				switch ( info.ButtonID )
				{
					case 0:
						return;

					case 1:
						m_Player.SendMessage( "Leader: {0}", m_WarStone.Leader.Name );
						m_Player.SendMessage( "Commander: {0}", m_WarStone.Commander.Name );

						m_Player.SendMessage( "---------------------" );

						foreach ( Mobile m in m_WarStone.Members )
							m_Player.SendMessage( m.Name );
						
						break;
					
					case 2:
						m_Player.Target = new InternalTarget( 1, m_WarStone );

						break;

					case 3:
					case 4:
						m_Player.Target = new InternalTarget( 2, m_WarStone );

						break;

					case 5:
						if ( m_WarStone.Gold < 250 )
							m_Player.SendMessage( "You don't have enough gold." );
						else if ( m_WarStone.Wood < 25 )
							m_Player.SendMessage( "You don't have enough logs." );
						else if ( m_WarStone.Guards.Count >= m_WarStone.MaxGuards )
							m_Player.SendMessage( "You can't hire any more guards until the city grows." );
						else
						{
							WarGuard theGuard = new WarGuard();

							if ( BaseCreature.Summon( theGuard, m_Player, m_Player.Location, -1, TimeSpan.FromDays( 14.0 ) ) )
							{
								m_WarStone.Gold -= 250;
								m_WarStone.Wood -= 25;

								m_WarStone.Guards.Add( theGuard );

								theGuard.WarStone = m_WarStone;
								theGuard.Init();
							}
							else
								m_Player.SendMessage( "Error adding guard. Try again or page a GM if the problem persists." );
						}

						break;
					
					case 6:
						if ( m_WarStone.Gold < 10 )
							m_Player.SendMessage( "You don't have enough gold." );
						else
						{
							m_WarStone.Gold -= (ulong)10;

							WarGuardPoint point = new WarGuardPoint();
							point.WarStone = m_WarStone;

							m_Player.Backpack.DropItem( point );
						}

						break;
					//case 7: TODO Script cannons
				}

				m_Player.SendGump( new WarStoneGump( m_Player, m_WarStone ) );
			} catch {
				m_Player.SendMessage( "Error processing town info. Page a GM." );
			}
		}

		private class InternalTarget : Target
		{
			private int m_Task;
			private WarStone m_WarStone;

			public InternalTarget( int task, WarStone theStone ) : base( 1, false, TargetFlags.None )
			{
				m_Task = task;
				m_WarStone = theStone;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				switch ( m_Task )
				{
					case 1:
						if ( targeted is TeiravonMobile )
						{
							Mobile player = (Mobile)targeted;
							TeiravonMobile m_Player = (TeiravonMobile)player;

							if ( from == player )
								return;
							
							if ( player == m_WarStone.Commander )
								return;

							if ( m_WarStone.Members.IndexOf( player ) > -1 )
								from.SendMessage( "{0} is already a citizen of {1}.", player.Name, m_WarStone.Name );
							else if ( m_Player.Town != null )
								from.SendMessage( "{0} is already a citizen of {1}.", player.Name, m_Player.Town.Name );
							else
							{
								m_WarStone.Members.Add( player );
								m_Player.Town = m_WarStone;
								from.SendMessage( "{0} has been added to {1}.", player.Name, m_WarStone.Name );
								player.SendMessage( "You have been added to {0}.", m_WarStone.Name );
							}
						} else
							from.SendMessage( "You can only add players to the town." );

						break;
					
					case 2:
						if ( targeted is Gold )
						{
							Gold gold = (Gold)targeted;

							if ( !gold.Movable )
								from.SendMessage( "The gold must be unlocked and in your pack to use." );
							else
							{
								m_WarStone.Gold += (ulong)gold.Amount;
								gold.Delete();

								from.SendMessage( "You add the gold to the treasury." );
							}
						} else if ( targeted is Log ) {
							Log log = (Log)targeted;

							if ( !log.Movable )
								from.SendMessage( "The log{0} must be unlocked and in your pack to use.", log.Amount > 1 ? "s" : "" );
							else
							{
								m_WarStone.Wood += (ulong)log.Amount;
								
								from.SendMessage( "You add the log{0} to the treasury.", log.Amount > 1 ? "s" : "" );

								log.Delete();
							}
						}

						break;
				}
			}
		}
	}
}