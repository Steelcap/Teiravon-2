using System;
using Server;
using Server.Mobiles;
using Server.Teiravon;

namespace Server.Items
{
	public class GlobeOfDarkness : Item
	{
		private int m_MaxRange = 15;

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			TeiravonMobile m_Player = m as TeiravonMobile;
			
			if ( m_Player == null || m_Player.AccessLevel != AccessLevel.Player )
				return;

			if ( Utility.InRange( GetWorldLocation(), m.Location, m_MaxRange ) && m_Player.AccessLevel == AccessLevel.Player && !m_Player.IsDrow() )
				m_Player.GlobeOfDarkness = true;
			else
			{
				m_Player.GlobeOfDarkness = false;

				foreach ( Mobile blah in m_Player.GetMobilesInRange( 15 ) )
				{
					if ( !blah.Hidden )
						m_Player.Send( new Network.MobileIncoming( m, blah ) );
				}
			}
		}

		[Constructable]
		public GlobeOfDarkness( int maxRange ) : base( 0x1B72 )
		{
			Name = "Globe of Darkness";
			Movable = false;
			Visible = false;

			if ( maxRange < 10 )
				maxRange = 10;

			m_MaxRange = maxRange;
		}

		[Constructable]
		public GlobeOfDarkness() : base( 0x1B72 )
		{
			Name = "Globe of Darkness";
			Movable = false;
			Visible = false;
		}

		public GlobeOfDarkness( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			writer.Write( (int) m_MaxRange );

			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			m_MaxRange = reader.ReadInt();
			base.Deserialize( reader );
		}
	}
}