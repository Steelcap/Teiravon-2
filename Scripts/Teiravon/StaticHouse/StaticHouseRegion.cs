using Server;
using System;
using Server.Items;
using Server.Spells;
using Server.Spells.Sixth;
using Server.StaticHousing;


namespace Server.Regions
{
	public class StaticHouseRegion : Region
	{
		public StaticHouseSign m_House;

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( OnLogin );
		}

		public static void OnLogin( LoginEventArgs e )
		{
			StaticHouseSign house = StaticHouseSign.StaticFindHouseAt( e.Mobile );

			if ( house != null && !house.Public && !house.IsFriend( e.Mobile ) )
				e.Mobile.Location = house.BanLocation;
		}

		public StaticHouseRegion( StaticHouseSign house) : base( "", "", house.Map )
		{
			Priority = Region.HousePriority;
			LoadFromXml = false;
			m_House = house;
		}
		private bool m_Recursion;

		public override void OnLocationChanged( Mobile m, Point3D oldLocation )
		{
			if ( m_Recursion )
				return;

			m_Recursion = true;

			if ( (m_House.Public || !Core.AOS) && m_House.IsBanned( m ) && m_House.IsInside( m )  )
			{
				m.Location = m_House.BanLocation;
				m.SendLocalizedMessage( 501284 ); // You may not enter.
			}
			else if (Core.AOS && !m_House.Public && !m_House.HasAccess( m ) && m_House.IsInside( m ) )
			{
				m.Location = m_House.BanLocation;
				m.SendLocalizedMessage( 501284 ); // You may not enter.
			}

			m_Recursion = false;
		}

		public override bool SendInaccessibleMessage( Item item, Mobile from )
		{

			if ( item is Container )
				item.SendLocalizedMessageTo( from, 501647 ); // That is secure.

			else
				item.SendLocalizedMessageTo( from, 1061637 ); // You are not allowed to access this.

			return true;

		}

		public override bool OnMoveInto( Mobile from, Direction d, Point3D newLocation, Point3D oldLocation )
		{
			if ( (m_House.Public || !Core.AOS) && m_House.IsBanned( from ) && m_House.IsInside( newLocation, 16 , from ) )
			{
				from.Location = m_House.BanLocation;
				from.SendLocalizedMessage( 501284 ); // You may not enter.
				return false;
			}
			else if ( Core.AOS && !m_House.Public && !m_House.HasAccess( from ) && m_House.IsInside( newLocation, 16, from ) )
			{
				from.SendLocalizedMessage( 501284 ); // You may not enter.
				return false;
			}

			return true;
		}

		public override bool OnDecay( Item item )
		{
			return false;

			//if ( item.Parent != null && ( item.Parent is Container ) && m_House.checkLockedDown( item.Parent as Item ) )
			//	return false;

			//if ( m_House.checkLockedDown( item ) || m_House.CheckSecure( item ) )
			//	return false;
			//else
			//	return base.OnDecay(item);
		}

		public override TimeSpan GetLogoutDelay( Mobile m )
		{
			if ( m_House.IsFriend( m ) && m_House.IsInside( m ) )
				return TimeSpan.Zero;
			else
				return base.GetLogoutDelay( m );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !from.Alive || !m_House.IsInside( from ) )
				return;

			bool isOwner = ( from.AccessLevel >= AccessLevel.GameMaster || from == m_House.Owner);
			bool isCoOwner = isOwner || m_House.IsCoOwner( from );
			bool isFriend = isCoOwner || m_House.IsFriend( from );

			if ( !isFriend )
				return;

			if ( e.HasKeyword( 0x33 ) ) // remove thyself
			{
				if ( isFriend )
				{
					from.SendLocalizedMessage( 501326 ); // Target the individual to eject from this house.
					from.Target = new StaticHouseKickTarget( m_House );
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x34 ) ) // I ban thee
			{
				if ( isFriend )
				{
					from.Target = new StaticHouseBanTarget( true, m_House );
					from.SendLocalizedMessage( 501325 ); // Target the individual to ban from this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x23 ) ) // I wish to lock this down
			{
				if ( isCoOwner )
				{
					from.Target = new LockdownTarget( false, m_House );
					from.SendLocalizedMessage( 502097 ); // Lock what down?


				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x24 ) ) // I wish to release this
			{
				if ( isCoOwner )
				{
					from.Target = new LockdownTarget( true, m_House );
					from.SendLocalizedMessage( 502100 ); // Choose the item you wish to release
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			/*else if ( e.HasKeyword( 0x25 ) ) // I wish to secure this
			{
				if ( isCoOwner )
				{
					from.Target = new  SecureTarget( false, m_House );
					from.SendLocalizedMessage( 502103 ); // Choose the item you wish to secure
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}*/
			else if ( e.HasKeyword( 0x26 ) ) // I wish to unsecure this
			{
				if ( isCoOwner )
				{
					from.Target = new  SecureTarget( true, m_House );
					from.SendLocalizedMessage( 502106 ); // Choose the item you wish to unsecure
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x27 ) ) // I wish to place a strong box
			{
				if ( isOwner )
				{
					from.SendLocalizedMessage( 502109 ); // Owners do not get a strongbox of their own.
				}
				else if ( isCoOwner )
				{
					m_House.AddStrongBox( from );
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x28 ) )
			{
				if ( isCoOwner )
				{
					m_House.AddTrashBarrel( from );
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
		}

		public override void OnExit( Mobile m )
		{
			m.SendMessage( "Bye!" );
			m.SendMessage(this.ToString());

		}

		public override bool OnDoubleClick( Mobile from, object o )
		{
			if ( o is Container )
			{
				Container c = (Container)o;

				if ( m_House.CheckSecure( c ) )
				{
					m_House.ReleaseSecure( m_House.Owner, c );
					m_House.SetLockdown( c, true );
				}

				/*SecureStaticAccessResult res = m_House.CheckSecureAccess( from, c );

				switch ( res )
				{
					case SecureStaticAccessResult.Insecure: break;
					case SecureStaticAccessResult.Accessible: return true;
					case SecureStaticAccessResult.Inaccessible: c.SendLocalizedMessageTo( from, 1010563 ); return false;
				}*/
			}

			return true;
		}

		public override bool OnSingleClick( Mobile from, object o )
		{
			if ( o is Item )
			{
				Item i = (Item)o;
				if ( m_House.checkLockedDown( i ) )
				{
					i.LabelTo( from, 501643 );//[locked down]
				} else if ( m_House.CheckSecure( i ) )
				{
					i.LabelTo( from, 501644 );//[locked down & secure]
				}
			}
			return true;
		}

		public StaticHouseSign House
		{
			get
			{
				return m_House;
			}
		}
	}
}