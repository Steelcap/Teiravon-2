using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Targeting;

namespace Server
{
    public class IdanVis
    {
		private static Hashtable m_IdanBlood = new Hashtable();

        public static void Initialize()
        {
            Commands.Register( "IdanVis", AccessLevel.GameMaster, new CommandEventHandler( IdanVis_OnCommand ) );
			Commands.Register( "IdanBlood", AccessLevel.GameMaster, new CommandEventHandler( IdanBlood_OnCommand ) );
			Commands.Register( "IdanCloud", AccessLevel.GameMaster, new CommandEventHandler( IdanCloud_OnCommand ) );

			EventSink.Movement += new MovementEventHandler( EventSink_Movement );
        }

		private static void IdanCloud_OnCommand( CommandEventArgs args )
		{
			TeiravonMobile m_Player = ( TeiravonMobile )args.Mobile;

			m_Player.Target = new InternalTarget();
		}

		private class InternalTarget : Target
		{
			public InternalTarget()
				: base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( ( targeted is TeiravonMobile ) )
				{
					IdanCloud ic = new IdanCloud( ( TeiravonMobile )targeted );
					ic.MoveToWorld( ( ( TeiravonMobile )targeted ).Location, ( ( TeiravonMobile )targeted ).Map );
				}
				else
				return;
			}
		}

		private class IdanCloud : Item
		{
			private TeiravonMobile m_Player;
			public TeiravonMobile Player { get { return m_Player; } }

			public IdanCloud( TeiravonMobile player )
				: base( 14120 )
			{
				Name = player.Name + "'s Raincloud";
				Movable = false;
				Hue = 2402;

				m_Player = player;

				InternalMoveTimer mt = new InternalMoveTimer( this );
				mt.Start();

				InternalTimer it = new InternalTimer( this );
				it.Start();
			}

			public IdanCloud( Serial serial )
				: base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( 1 );
				writer.Write( m_Player );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
				m_Player = ( TeiravonMobile )reader.ReadMobile();

				InternalMoveTimer mt = new InternalMoveTimer( this );
				mt.Start();

				InternalTimer it = new InternalTimer( this );
				it.Start();
			}

			private class InternalMoveTimer : Timer
			{
				IdanCloud m_Cloud;

				public InternalMoveTimer( IdanCloud cloud )
					: base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.5 ) )
				{
					m_Cloud = cloud;

					Priority = TimerPriority.OneSecond;
				}

				protected override void OnTick()
				{
					if ( m_Cloud == null || m_Cloud.Deleted || m_Cloud.Player == null )
						Stop();
					else
						m_Cloud.MoveToWorld( new Point3D( m_Cloud.Player.X, m_Cloud.Player.Y, m_Cloud.Player.Z + 12 ), m_Cloud.Player.Map );
				}
			}

			private class InternalTimer : Timer
			{
				private int m_NormalHue = 2402;
				private int m_StormHue = 2497;
				IdanCloud m_Cloud;

				public InternalTimer( IdanCloud cloud )
					: base( TimeSpan.Zero, TimeSpan.FromSeconds( 10.0 ) )
				{
					m_Cloud = cloud;
					Priority = TimerPriority.FiveSeconds;
				}

				protected override void OnTick()
				{
					//if ( m_Cloud == null || m_Cloud.Deleted || m_Cloud.Player == null )
						Stop();
/*
					switch ( Utility.RandomMinMax( 1, 3 ) )
					{
						case 1:
							// Grey Cloud & Rain
							m_Cloud.Hue = 0;

							for ( int i = 0; i < Utility.RandomMinMax( 1, 3 ); i++ )
							{
								Static rain = new Static( Utility.RandomMinMax( 4650, 4654 ) );
								rain.Hue = 2120;
								rain.Name = m_Cloud.Player.Name + "'s Rain Puddle";

								Point3D loc = new Point3D( m_Cloud.Player.X + Utility.RandomMinMax( -1, 1 ), m_Cloud.Player.Y + Utility.RandomMinMax( -1, 1 ), m_Cloud.Player.Z );

								rain.MoveToWorld( loc, m_Cloud.Player.Map );

								Timer.DelayCall( TimeSpan.FromSeconds( 3.0 ), new TimerCallback( rain.Delete ) );
							}

							m_Cloud.Player.Emote( "*You see {0} is soaked in the rain!*", m_Cloud.Player.Name );

							m_Cloud.Player.PlaySound( Utility.RandomList( new int[] { 20, 16, 868, 16, 20, 1233  } ) );

							Delay = TimeSpan.FromSeconds( Utility.RandomMinMax( 5, 8 ) );
							break;

						case 2:
							// Darker Grey Cloud & Rain & Thunder
							m_Cloud.Hue = m_NormalHue;

							for ( int i = 0; i < Utility.RandomMinMax( 2, 4 ); i++ )
							{
								Static rain = new Static( Utility.RandomMinMax( 4650, 4654 ) );
								rain.Hue = 2120;
								rain.Name =" Rain Puddle";

								Point3D loc = new Point3D( m_Cloud.Player.X + Utility.RandomMinMax( -1, 1 ), m_Cloud.Player.Y + Utility.RandomMinMax( -1, 1 ), m_Cloud.Player.Z );

								rain.MoveToWorld( loc, m_Cloud.Player.Map );

								Timer.DelayCall( TimeSpan.FromSeconds( 3.0 ), new TimerCallback( rain.Delete ) );
							}

							m_Cloud.Player.Emote( "*You see {0} is drenched in the rain!*", m_Cloud.Player.Name );
							m_Cloud.Player.PlaySound( Utility.RandomList( new int[] { 20, 16, 868, 16, 20, 1233 } ) );

							Delay = TimeSpan.FromSeconds( Utility.RandomMinMax( 5, 8 ) );

							break;

						case 3:
							// Dark Cloud & Raind & Thunder & Lightning
							m_Cloud.Hue = m_StormHue;
							bool lightning = false;

							for ( int i = 0; i < Utility.RandomMinMax( 1, 3 ); i++ )
							{
								Static rain = new Static( Utility.RandomMinMax( 4650, 4654 ) );
								rain.Hue = 2120;
								rain.Name = m_Cloud.Player.Name + "'s Rain Puddle";

								Point3D loc = new Point3D( m_Cloud.Player.X + Utility.RandomMinMax( -1, 1 ), m_Cloud.Player.Y + Utility.RandomMinMax( -1, 1 ), m_Cloud.Player.Z );

								rain.MoveToWorld( loc, m_Cloud.Player.Map );

								Timer.DelayCall( TimeSpan.FromSeconds( 3.0 ), new TimerCallback( rain.Delete ) );

								if ( Utility.RandomBool() )
								{
									m_Cloud.Player.BoltEffect( 0 );
									m_Cloud.Player.Damage( Utility.RandomMinMax( 10, 20 ), m_Cloud.Player );
									
									lightning = true;
								}
							}
					
							if ( lightning )
								m_Cloud.Player.Emote( "*You see {0} is struck by lightning!*", m_Cloud.Player.Name );

							m_Cloud.Player.Emote( "*You see {0} is drenched in the rain!*", m_Cloud.Player.Name );
							m_Cloud.Player.PlaySound( Utility.RandomList( new int[] { 20, 16, 868, 16, 20, 1233 } ) );

							Delay = TimeSpan.FromSeconds( Utility.RandomMinMax( 8, 10 ) );

							break;
					}
					*/
				}
			}
		}

		static void EventSink_Movement( MovementEventArgs e )
		{
			if ( ( e.Mobile is TeiravonMobile ) && m_IdanBlood.Contains( ( TeiravonMobile )e.Mobile ) )
			{
				if ( Utility.RandomMinMax( 1, 2 ) == 2 )
				{
					Static blood = new Static( Utility.RandomMinMax( 4650, 4654 ) );

					blood.MoveToWorld( e.Mobile.Location, e.Mobile.Map );

					Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), new TimerStateCallback( DeleteBlood_Callback ), blood );
				}
			}
		}

		private static void DeleteBlood_Callback( object state )
		{
			( ( Static )state ).Delete();
		}

		private static void IdanBlood_OnCommand( CommandEventArgs args )
		{
			TeiravonMobile m_Player = ( TeiravonMobile )args.Mobile;

			if ( m_IdanBlood.Contains( m_Player ) )
			{
				m_Player.SendMessage( "Blood trail off." );
				m_IdanBlood.Remove( m_Player );
			}
			else
			{
				m_Player.SendMessage( "Blood trail on." );
				m_IdanBlood.Add( m_Player, null );
			}
		}

        private static void IdanVis_OnCommand( CommandEventArgs args )
        {
            TeiravonMobile m_Player = ( TeiravonMobile )args.Mobile;

            SoundTimer stimer = new SoundTimer( args.Mobile );
            stimer.Start();
        }

        private class EffectTimer : Timer
        {
            private Mobile m_Player;
            private int m_Count = 0;
            private Static m_Sparkle;

            public EffectTimer( Mobile player )
                : base( TimeSpan.FromSeconds( 0.0 ), TimeSpan.FromSeconds( 1.0 ) )
            {
                m_Player = player;
            }

            protected override void OnTick()
            {
                if ( m_Count == 0 )
                {
                    m_Sparkle = new Static( 14201 );
                    m_Sparkle.MoveToWorld( m_Player.Location, m_Player.Map );
                }

                if ( m_Count == 2 )
                {
                    if ( !m_Player.Hidden )
                        Timer.DelayCall( TimeSpan.FromSeconds( 1.2 ), new TimerStateCallback( Hide_Callback ), m_Player );
                    else
                        m_Player.Hidden = !m_Player.Hidden;

                    m_Player.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.CenterFeet );
                    m_Player.PlaySound( 0x208 );

                    m_Sparkle.Delete();

                    Stop();
                    return;
                }

                m_Count++;
            }
        }

        private static void Hide_Callback( object state )
        {
            TeiravonMobile m_Player = ( TeiravonMobile )state;

            m_Player.Hidden = !m_Player.Hidden;
        }

        private class SoundTimer : Timer
        {
            private Mobile m_Player;
            int m_Count = 1;

            public SoundTimer( Mobile player )
                : base( TimeSpan.FromSeconds( 0.0 ), TimeSpan.FromSeconds( 1.0 ) )
            {
                m_Player = player;
            }

            protected override void OnTick()
            {
                foreach ( NetState state in m_Player.GetClientsInRange( 15 ) )
                {
                    if ( m_Count % 2 != 0 )
                        state.Mobile.PlaySound( 287 );
                    else
                        state.Mobile.PlaySound( 288 );
                }

                m_Count++;

                if ( m_Count == 2 )
                {
                    EffectTimer et = new EffectTimer( m_Player );
                    et.Start();
                }

                if ( m_Count > 4 )
                {
                    Stop();
                    return;
                }
            }
        }
    }
}