///////////////////////////////////////////
// C# Exporter Generated: 12/29/2006 1:02:59 AM
//
// Designed by Ravenal of OrBSydia
// Version: 2.0
//
// Script: AntiMacroCheckGump
///////////////////////////////////////////

using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Gumps
{
	public class AntiMacroTimer : Timer
	{
		private Mobile m_Mobile;

		public AntiMacroTimer( Mobile player )
			: base( TimeSpan.FromMinutes( 600.0 ) )
		{
			m_Mobile = player;
			Priority = TimerPriority.OneMinute;
		}

		public void Tick()
		{
			OnTick();
		}

		protected override void OnTick()
		{
			if ( m_Mobile.NetState != null )
				m_Mobile.NetState.Dispose();

			AntiMacroCheckGump.Timers.Remove( m_Mobile );
			AntiMacroCheckGump.SkillGains.Remove( m_Mobile );
		}
	}

	public class AntiMacroCheckGump : Gump
	{
		private string m_Code;
		private int m_Count = 0;
		public static Hashtable Timers = new Hashtable();
		public static Hashtable SkillGains = new Hashtable();

		public AntiMacroCheckGump( int count )
			: base( 30, 30 )
		{
			Dragable = true;
			Closable = false;

			AddPage( 0 );
			AddBackground( 20, 20, 250, 150, 9250 ); // Background
			AddLabel( 68, 38, 0, @"Unattended Macro Check" ); // Title

			string code = "";

			for ( int i = 0; i < 5; i++ )
			{
				switch ( Utility.RandomMinMax( 1, 3 ) )
				{
					case 1:
						code += Microsoft.VisualBasic.Strings.Chr( Utility.RandomMinMax( 49, 57 ) );
						break;

					case 2:
						code += Microsoft.VisualBasic.Strings.Chr( Utility.RandomMinMax( 65, 90 ) );
						break;

					case 3:
						code += Microsoft.VisualBasic.Strings.Chr( Utility.RandomMinMax( 97, 122 ) );
						break;
				}
			}

			m_Code = code;
			m_Count = count;

			AddLabel( 120, 68, 0, m_Code ); // Code
			AddTextEntry( 120, 94, 53, 20, 0, 0, "" ); // CodeEntry
			AddImageTiled( 120, 114, 53, 1, 2702 ); // Divider

			AddButton( 116, 128, 247, 248, 1000, GumpButtonType.Reply, 0 ); // Okay
			//AddButton( 223, 130, 22153, 22153, 1001, GumpButtonType.Reply, 0 ); // Help
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int button = info.ButtonID;
			Mobile player = sender.Mobile;

			switch ( button )
			{
				case 1000:
					if ( info.TextEntries[0].Text != m_Code && m_Count <= 3 )
					{
						player.SendMessage( "You've entered an incorrect code. Try again." );
						player.SendGump( new AntiMacroCheckGump( m_Count++ ) );
					}
					else if ( info.TextEntries[0].Text != m_Code && m_Count > 3 )
					{
						AntiMacroTimer amt = Timers[player] as AntiMacroTimer;

						player.SendMessage( "You are being disconnected..." );

						if ( amt != null )
							amt.Tick();
					}
					else
					{
						player.SendMessage( "Thanks." );

						AntiMacroTimer amt = Timers[player] as AntiMacroTimer;

						if ( amt != null )
							amt.Stop();

						Timers.Remove( player );
						SkillGains.Remove( player );
					}

					break;
			}
		}
	}
}