using System;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server
{
	public class LightCycle
	{
		public const int DayLevel = 0;
		public const int NightLevel = 20;
		public const int DungeonLevel = 25;
		public const int JailLevel = 9;

		private static int m_LevelOverride = int.MinValue;

		public static int LevelOverride
		{
			get{ return m_LevelOverride; }
			set
			{
				m_LevelOverride = value;

				for ( int i = 0; i < NetState.Instances.Count; ++i )
				{
					NetState ns = (NetState)NetState.Instances[i];
					Mobile m = ns.Mobile;

					if ( m != null )
						m.CheckLightLevels( false );
				}
			}
		}

		public static void Initialize()
		{
			new LightCycleTimer().Start();
			EventSink.Login += new LoginEventHandler( OnLogin );

			Server.Commands.Register( "GlobalLight", AccessLevel.GameMaster, new CommandEventHandler( Light_OnCommand ) );
		}

		[Usage( "GlobalLight <value>" )]
		[Description( "Sets the current global light level." )]
		private static void Light_OnCommand( CommandEventArgs e )
		{
			if ( e.Length >= 1 )
			{
				LevelOverride = e.GetInt32( 0 );
				e.Mobile.SendMessage( "Global light level override has been changed to {0}.", m_LevelOverride );
			}
			else
			{
				LevelOverride = int.MinValue;
				e.Mobile.SendMessage( "Global light level override has been cleared." );
			}
		}

		public static void OnLogin( LoginEventArgs args )
		{
			Mobile m = args.Mobile;

			m.CheckLightLevels( true );
		}

		public static int ComputeLevelFor( Mobile from )
		{
			if ( m_LevelOverride > int.MinValue )
				return m_LevelOverride;

			bool teiravon = false;
			bool drow = false;
			bool lowlight = false;
			bool shapeshifted = false;

			if ( from is TeiravonMobile )
			{
				teiravon = true;

				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( m_Player.IsDrow() )
					drow = true;

				else if ( m_Player.IsElf() || m_Player.IsDwarf() || m_Player.IsDuergar() || m_Player.IsOrc() || m_Player.IsHalfElf() || m_Player.IsGoblin() || m_Player.IsGnome() )
					lowlight = true;

				else if ( m_Player.IsShifted() && m_Player.Shapeshifted )
					shapeshifted = true;
			}

			int hours, minutes;

			Server.Items.Clock.GetTime( from.Map, from.X, from.Y, out hours, out minutes );

			/* OSI times:
			 *
			 * Midnight ->  3:59 AM : Night
			 *  4:00 AM -> 11:59 PM : Day
			 *
			 * RunUO times:
			 *
			 * 10:00 PM -> 11:59 PM : Scale to night
			 * Midnight ->  3:59 AM : Night
			 *  4:00 AM ->  5:59 AM : Scale to day
			 *  6:00 AM ->  9:59 PM : Day
			 *
			 * Drow times:
			 *
			 * 10:00 PM -> 11:59 PM : Scale to day
			 * Midnight ->  3:59 AM : Day
			 *  4:00 AM ->  5:59 AM : Scale to night
			 *  6:00 AM ->  9:59 PM : Night
			 */

			if ( !teiravon )
			{
				if ( hours < 4 )
					return NightLevel;

				if ( hours < 6 )
					return NightLevel + (((((hours - 4) * 60) + minutes) * (DayLevel - NightLevel)) / 120);

				if ( hours < 22 )
					return DayLevel;

				if ( hours < 24 )
					return DayLevel + (((((hours - 22) * 60) + minutes) * (NightLevel - DayLevel)) / 120);
			}
			else
			{
				if ( !drow && !lowlight && !shapeshifted )
				{
					// No Low-Light vision or Infravision
					if ( hours < 4 )
						return NightLevel;

					if ( hours < 6 )
						return NightLevel + (((((hours - 4) * 60) + minutes) * (DayLevel - NightLevel)) / 120);

					if ( hours < 22 )
						return DayLevel;

					if ( hours < 24 )
						return DayLevel + (((((hours - 22) * 60) + minutes) * (NightLevel - DayLevel)) / 120);

				}
				else if ( shapeshifted && !drow )
				{
					if ( hours < 4 ) //goes from 10 to 5
						return 10 - ((((hours * 60) + minutes) * 5 ) / 240);

					if (hours < 6 ) //goes from 5 -> 0
						return 5 - (((((hours - 4) * 60) + minutes) * 5) / 120);

					if ( hours < 22 ) //is zero during daytime
						return DayLevel;

					if ( hours < 24 ) //goes from 0 -> 10
						return DayLevel + (((((hours - 22) * 60) + minutes) * 10) / 120);
				}
				else if ( lowlight )
				{
					// Low-Light vision
					if ( hours < 4 ) //goes from 12 to 6
						return 12 - ((((hours * 60) + minutes) * 6 ) / 240);

					if ( hours < 6 ) //goes from 6 to 0
						return 6 - (((((hours - 4) * 60) + minutes) * 6) / 120);

					if ( hours < 22 ) //zero
						return DayLevel;

					if ( hours < 24 ) //goes from 0 to 12
						return DayLevel + (((((hours - 22) * 60) + minutes) * 12) / 120);
				}
				else
				{

					// Simulate Drow infravision
					int light = 0;

					if ( hours < 4 )
						return DayLevel;

					if ( hours < 6 )
					{
						light = DayLevel + (((((hours - 4) * 60) + minutes) * (NightLevel- DayLevel)) / 120);
						return ( (light > DungeonLevel) || (light < 0 ) ) ? DungeonLevel : light;
					}

					if ( hours < 22 )
						return NightLevel;

					if ( hours < 24 )
					{
						light = NightLevel + (((((hours - 22) * 60) + minutes) * (DayLevel - NightLevel)) / 120);
						return ( (light > DungeonLevel) || (light < 0 ) ) ? DungeonLevel : light;
					}
				}

			}

			return NightLevel; // should never be
		}

		private class LightCycleTimer : Timer
		{
			public LightCycleTimer() : base( TimeSpan.FromSeconds( 0 ), TimeSpan.FromSeconds( 5.0 ) )
			{
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				for ( int i = 0; i < NetState.Instances.Count; ++i )
				{
					NetState ns = (NetState)NetState.Instances[i];
					Mobile m = ns.Mobile;

					if ( m != null )
						m.CheckLightLevels( false );
				}
			}
		}

		public class NightSightTimer : Timer
		{
			private Mobile m_Owner;

			public NightSightTimer( Mobile owner ) : base( TimeSpan.FromMinutes( Utility.Random( 15, 25 ) ) )
			{
				m_Owner = owner;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m_Owner.EndAction( typeof( LightCycle ) );
				m_Owner.LightLevel = 0;
			}
		}
	}
}
