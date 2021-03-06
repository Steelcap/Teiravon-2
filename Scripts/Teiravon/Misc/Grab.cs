/*
 * BasePotion - add blockequip
 * TeiravonMobile - override canbeharmful
*/
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Scripts.Commands;

namespace Server.Mobiles
{
	public class Grab
	{
		public static void Initialize()
		{
			Commands.Register( "Grab", AccessLevel.Player, new CommandEventHandler( Grab_OnCommand ) );
			Commands.Register( "Guard", AccessLevel.Player, new CommandEventHandler( Guard_OnCommand ) );
		}

		// TeiravonMobile, GrabTimer
		public static Hashtable Grabbers = new Hashtable();

		public static void GetValues( TeiravonMobile grabber, TeiravonMobile grabbed, out int grabvalue, out int defendvalue )
		{
			// Less than 1/4 life left or 1/2 stam left = subdued = no resistance
            if (!grabbed.Guard || grabbed.Hits <= (int)(grabbed.HitsMax * 0.25) || grabbed.Stam <= (int)(grabbed.StamMax * 0.25) || grabbed.FindItemOnLayer(Layer.TwoHanded) is Manacles)
				defendvalue = 0;
			else
				defendvalue = grabbed.Str + grabbed.Dex + Utility.RandomMinMax( 1, 10 );

			// No resistance = minimal cost to grab
			if ( defendvalue == 0 || !grabbed.Guard )
				grabvalue = 100;
			else if ( grabber.Stam <= 5 )
				grabvalue = 0;
			else
				grabvalue = grabber.Str + grabber.Stam + Utility.RandomMinMax( 1, 10 );

			if ( grabber.AccessLevel >= AccessLevel.GameMaster )
				grabber.SendMessage( "defendvalue: {0}, grabvalue: {1}", defendvalue, grabvalue );
		}

		public static void Guard_OnCommand( CommandEventArgs e )
		{
			( ( TeiravonMobile )e.Mobile ).Guard = !( ( TeiravonMobile )e.Mobile ).Guard;

			if ( ( ( TeiravonMobile )e.Mobile ).Guard )
				e.Mobile.Emote( "*Stands ready to defend {0}*", e.Mobile.Female ? "herself" : "himself" );
			else
				e.Mobile.Emote( "*Lets down {0} guard*", e.Mobile.Female ? "her" : "his" );
		}

		public static void Grab_OnCommand( CommandEventArgs e )
		{
			if ( !e.Mobile.Alive )
				return;

			foreach ( GrabTimer grab in Grabbers.Values )
				if ( grab.Grabbed == e.Mobile )
				{
					e.Mobile.SendMessage( "You can't grab anyone right now." );
					return;
				}

			GrabTimer timer = Grabbers[e.Mobile] as GrabTimer;

			// Setting them down.
			if ( timer != null )
			{
				timer.Release();
				timer.Grabbed.Blessed = true;
				timer.Grabbed.Freeze( TimeSpan.FromSeconds( 2.0 ) );
				e.Mobile.Emote( "*sets down {0}*", timer.Grabbed.Name );

				Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( GrabRelease_OnCallback ), timer.Grabbed );

				Grabbers.Remove( e.Mobile );
			}
			else
			{
                if (e.Mobile.FindItemOnLayer(Layer.OneHanded) != null || e.Mobile.FindItemOnLayer(Layer.TwoHanded) != null)
                {
                    if (e.Mobile.Weapon is CustomShapeshifterWeapon)
                        e.Mobile.Target = new GrabTarget();
                    else
                        e.Mobile.SendMessage("You must have both hands free to do this.");
                }
                else
                    e.Mobile.Target = new GrabTarget();
			}
		}

		public static void GrabRelease_OnCallback( object state )
		{
			TeiravonMobile grabbed = ( TeiravonMobile )state;
			grabbed.Blessed = false;
			grabbed.Frozen = false;
		}

		private class GrabTarget : Target
		{
			public GrabTarget()
				: base( 1, false, TargetFlags.Harmful )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( !( targeted is TeiravonMobile ) )
					from.SendMessage( "You must target another player." );
				else if ( !( ( TeiravonMobile )targeted ).Alive || !from.Alive )
					from.SendMessage( "You must target a living player." );
				else if ( ( TeiravonMobile )targeted == ( TeiravonMobile )from )
					from.SendMessage( "You must target another player." );
				else if ( from.Mounted )
					from.SendMessage( "You must be on foot to initially grab a player." );
				else if ( ( ( TeiravonMobile )targeted ).Mounted )
					from.SendMessage( "Your target must be on foot to be grabbed." );
				//else if ( ( ( TeiravonMobile )targeted ).FindItemOnLayer( Layer.OneHanded ) != null || ( ( TeiravonMobile )targeted ).FindItemOnLayer( Layer.TwoHanded ) != null )
				//	from.SendMessage( "You must first disarm your target." );
				else if ( Grabbers.Contains( from ) )		// Sanity check
					from.SendMessage( "You can only grab one person at a time." );
				else if ( Grabbers.Contains( ( ( TeiravonMobile )targeted ) ) )
					from.SendMessage( "You can't get a hold on {0} right now.", ( ( TeiravonMobile )targeted ).Female ? "her" : "him" );
				else
				{
					TeiravonMobile grabbed = ( TeiravonMobile )targeted;

					foreach ( GrabTimer timer in Grabbers.Values )
					{
						if ( timer.Grabbed == grabbed )
						{
							from.SendMessage( "{0} is already being held.", grabbed.Name );
							return;
						}
					}

					int grabvalue = 0;
					int defendvalue = 0;

					GetValues( ( TeiravonMobile )from, grabbed, out grabvalue, out defendvalue );

					// We grabbed them.
					if ( grabvalue > defendvalue || ( grabvalue == defendvalue && Utility.RandomBool() ) )
					{
						GrabTimer timer = new GrabTimer( ( TeiravonMobile )from, grabbed );

						Grabbers.Add( ( TeiravonMobile )from, timer );
						timer.Start();

						from.Emote( "*successfully grabs {0}!*", grabbed.Name );
						BaseWeapon.BlockEquip( from, TimeSpan.FromMinutes( 10.0 ) );
						BasePotion.BlockEquip( from, TimeSpan.FromMinutes( 10.0 ) );
                        grabbed.ClearHands();
						BaseWeapon.BlockEquip( grabbed, TimeSpan.FromMinutes( 10.0 ) );
						BasePotion.BlockEquip( grabbed, TimeSpan.FromMinutes( 10.0 ) );
						grabbed.BeginAction( typeof( BaseMount ) );
						grabbed.Freeze( TimeSpan.FromMinutes( 10.0 ) );

						BaseMount.Dismount( grabbed );
					}
					else
						from.Emote( "*fails to grab {0}!*", grabbed.Name );

				}
			}
		}

		public class GrabTimer : Timer
		{
			private TeiravonMobile m_Grabber;
			private TeiravonMobile m_Grabbed;
			private Point3D m_OldLocation;
			private int m_Count = 1;

			public TeiravonMobile Grabber { get { return m_Grabber; } }
			public TeiravonMobile Grabbed { get { return m_Grabbed; } }

			public GrabTimer( TeiravonMobile grabber, TeiravonMobile grabbed )
				: base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.25 ) )
			{
				Priority = TimerPriority.TwoFiftyMS;

				m_Grabber = grabber;
				m_Grabbed = grabbed;
				m_OldLocation = m_Grabbed.Location;
			}

			protected override void OnTick()
			{
				if ( !m_Grabber.Alive || !m_Grabbed.Alive || m_Grabber.NetState == null || m_Grabbed.NetState == null )
				{
					m_Grabber.Emote( "*loses {0} hold on {1}!*", m_Grabber.Female ? "her" : "his", m_Grabbed.Name );
					Release();
					return;
				}

				if ( m_Count % 6 == 0 )
				{
					int grabvalue = 0;
					int defendvalue = 0;

					GetValues( m_Grabber, m_Grabbed, out grabvalue, out defendvalue );

					// We have a hold on them still.
					if ( grabvalue > defendvalue || ( grabvalue == defendvalue && Utility.RandomBool() ) )
					{
						m_Grabber.Stam -= GetStamLoss( grabvalue - defendvalue );
						m_Count = 0;	// We increment count later on.
					}
					// No longer have our hold
					else
					{
						m_Grabber.Emote( "*loses {0} hold on {1}!*", m_Grabber.Female ? "her" : "his", m_Grabbed.Name );

						Release();
						return;
					}
				}

				if ( m_OldLocation != m_Grabber.Location )
					m_Grabbed.MoveToWorld( m_Grabber.Location, m_Grabber.Map );

				m_OldLocation = m_Grabber.Location;

				m_Count++;
			}

			public void Release()
			{
				m_Grabber.EndAction( typeof( BasePotion ) );
				m_Grabber.EndAction( typeof( BaseWeapon ) );
				m_Grabbed.EndAction( typeof( BasePotion ) );
				m_Grabbed.EndAction( typeof( BaseWeapon ) );
				m_Grabbed.EndAction( typeof( BaseMount ) );

				m_Grabbed.CantWalk = false;
				m_Grabbed.Frozen = false;

				Grabbers.Remove( m_Grabber );
				Stop();
			}

			private int GetStamLoss( int val )
			{
                if (val <= 20.0)
                    return 10;
                else if (val >= 21.0 && val <= 40.0)
                    return 8;
                else if (val > 40 && val < 100)
                    return 5;
                else
                    return 0;
			}
		}
	}
}