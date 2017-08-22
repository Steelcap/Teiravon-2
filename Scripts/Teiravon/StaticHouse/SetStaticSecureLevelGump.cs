using System;
using Server;
using Server.Multis;
using Server.Network;
using Server.StaticHousing;

namespace Server.Gumps
{
	public class SetStaticSecureLevelGump : Gump
	{
		private SecureStaticInfo m_infostatic;

		public SetStaticSecureLevelGump( Mobile owner, SecureStaticInfo info ) : base( 50, 50 )
		{
			m_infostatic = info;

			AddPage( 0 );

			AddBackground( 0, 0, 220, 160, 5054 );

			AddImageTiled( 10, 10, 200, 20, 5124 );
			AddImageTiled( 10, 40, 200, 20, 5124 );
			AddImageTiled( 10, 70, 200, 80, 5124 );

			AddAlphaRegion( 10, 10, 200, 140 );

			AddHtmlLocalized( 10, 10, 200, 20, 1061276, 32767, false, false ); // <CENTER>SET ACCESS</CENTER>
			AddHtmlLocalized( 10, 40, 100, 20, 1041474, 32767, false, false ); // Owner:

			AddLabel( 110, 40, 1152, owner == null ? "" : owner.Name );

			AddButton( 10, 70, GetFirstID( SecureLevelStatic.Owner ), 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 45, 70, 150, 20, 1061277, GetColor( SecureLevelStatic.Owner ), false, false ); // Owner Only

			AddButton( 10, 90, GetFirstID( SecureLevelStatic.CoOwners ), 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 45, 90, 150, 20, 1061278, GetColor( SecureLevelStatic.CoOwners ), false, false ); // Co-Owners

			AddButton( 10, 110, GetFirstID( SecureLevelStatic.Friends ), 4007, 3, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 45, 110, 150, 20, 1061279, GetColor( SecureLevelStatic.Friends ), false, false ); // Friends

			AddButton( 10, 130, GetFirstID( SecureLevelStatic.Anyone ), 4007, 4, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 45, 130, 150, 20, 1061626, GetColor( SecureLevelStatic.Anyone ), false, false ); // Anyone
		}
		
		public int GetColor( SecureLevelStatic level )
		{
			return ( m_infostatic.Level == level ) ? 0x7F18 : 0x7FFF;
		}

		public int GetFirstID( SecureLevelStatic level )
		{
			return ( m_infostatic.Level == level ) ? 4006 : 4005;
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			SecureLevelStatic level = m_infostatic.Level;

			switch ( info.ButtonID )
			{
				case 1: level = SecureLevelStatic.Owner; break;
				case 2: level = SecureLevelStatic.CoOwners; break;
				case 3: level = SecureLevelStatic.Friends; break;
				case 4: level = SecureLevelStatic.Anyone; break;
			}

			if ( m_infostatic.Level == level )
			{
				state.Mobile.SendLocalizedMessage( 1061281 ); // Access level unchanged.
			}
			else
			{
				m_infostatic.Level = level;
				state.Mobile.SendLocalizedMessage( 1061280 ); // New access level set.
			}
		}
	}
}