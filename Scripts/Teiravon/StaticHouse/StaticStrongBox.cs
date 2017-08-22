using System;
using Server;
using Server.Multis;
using Server.Network;
using Server.StaticHousing;

namespace Server.Items
{
	[FlipableAttribute( 0xe80, 0x9a8 )]
	public class StaticStrongBox : Container, IChopable
	{
		private StaticHouseSign m_House;
		private Mobile m_Owner;
		
		public override int DefaultGumpID{ get{ return 0x4B; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 16, 51, 168, 73 ); }
		}
		
		public StaticStrongBox( Mobile owner, StaticHouseSign house ) : base( 0xE80 )
		{
			
			m_Owner = owner;
			m_House = house;

			MaxItems = 25;
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get
			{
				return m_Owner;
			}
			set
			{
				m_Owner = value;
				InvalidateProperties();
			}
		}

		public override int MaxWeight
		{
			get
			{
				return 0;
			}
		}

		public StaticStrongBox( Serial serial ) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Owner );
			writer.Write( m_House );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Owner = reader.ReadMobile();
					m_House = reader.ReadItem() as StaticHouseSign;

					break;
				}
			}
		}

		public override bool Decays
		{
			get
			{
				if ( m_House != null && m_Owner != null && !m_Owner.Deleted )
					return !m_House.IsCoOwner( m_Owner );
				else
					return true;
			}
		}

		public override TimeSpan DecayTime
		{
			get
			{
				return TimeSpan.FromMinutes( 30.0 );
			}
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( m_Owner != null )
				list.Add( 1042887, m_Owner.Name ); // a strong box owned by ~1_OWNER_NAME~
			else
				base.AddNameProperty( list );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( m_Owner != null )
			{
				LabelTo( from, 1042887, m_Owner.Name ); // a strong box owned by ~1_OWNER_NAME~

				if ( CheckContentDisplay( from ) )
					LabelTo( from, "({0} items, {1} stones)", TotalItems, TotalWeight );
			}
			else
			{
				base.OnSingleClick( from );
			}
		}

		public override bool IsAccessibleTo( Mobile m )
		{
			if ( m_Owner == null || m_Owner.Deleted || m_House == null || m_House.Deleted )
				return true;

			return ( m.AccessLevel >= AccessLevel.GameMaster || m_House.Owner == m || (m_House.CoOwners.Contains( m ) && m == m_Owner) ) && base.IsAccessibleTo( m );
		}

		private void Chop( Mobile from )
		{
			while ( this.Items.Count > 0 )
				((Item)Items[0]).MoveToWorld( this.Location, this.Map );
			Effects.PlaySound( Location, Map, 0x11C );
			from.SendLocalizedMessage( 500461 ); // You destroy the item.
			this.Delete();
		}

		public void OnChop( Mobile from )
		{
			if ( m_House != null && !m_House.Deleted && m_Owner != null && !m_Owner.Deleted )
			{
				if ( from == m_Owner || m_House.IsOwner( from ) )
					Chop( from );
			}
			else
			{
				Chop( from );
			}
		}
	}
}
