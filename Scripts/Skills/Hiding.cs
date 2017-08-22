using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	public class Hiding
	{
		public static void Initialize()
		{
			SkillInfo.Table[21].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
            if (m is TeiravonMobile && ((TeiravonMobile)m).m_Revealed + TimeSpan.FromSeconds(3.0) > DateTime.Now)
            {
                m.SendMessage("You canot find any cover so quickly after being revealed.");
                return TimeSpan.FromSeconds(1.0);
            }

			if ( m.Target != null || m.Spell != null )
			{
				m.SendLocalizedMessage( 501238 ); // You are busy doing something else and cannot hide.
				return TimeSpan.FromSeconds( 1.0 );
			}

			double bonus = 0.0;

			BaseHouse house = BaseHouse.FindHouseAt( m );

			if ( house != null && house.IsFriend( m ) )
			{
				bonus = 100.0;
			}
			else if ( !Core.AOS )
			{
				if ( house == null )
					house = BaseHouse.FindHouseAt( new Point3D( m.X - 1, m.Y, 127 ), m.Map, 16 );

				if ( house == null )
					house = BaseHouse.FindHouseAt( new Point3D( m.X + 1, m.Y, 127 ), m.Map, 16 );

				if ( house == null )
					house = BaseHouse.FindHouseAt( new Point3D( m.X, m.Y - 1, 127 ), m.Map, 16 );

				if ( house == null )
					house = BaseHouse.FindHouseAt( new Point3D( m.X, m.Y + 1, 127 ), m.Map, 16 );

				if ( house != null )
					bonus = 50.0;
			}

			int range = 18 - (int)(m.Skills[SkillName.Stealth].Value / 10);
            if (range < 8)
                range = 8;
			bool badCombat = ( m.Combatant != null && m.InRange( m.Combatant.Location, range ) && m.Combatant.InLOS( m ) ) || Grab.Grabbers.Contains(m);
			bool badLook = false;



			bool ok = ( !badCombat /*&& m.CheckSkill( SkillName.Hiding, 0.0 - bonus, 100.0 - bonus )*/ );

			if ( ok )
			{
				foreach ( Mobile check in m.GetMobilesInRange( range ) )
				{
					if ( check.InLOS( m ) && check.Combatant == m )
					{
						badCombat = true;
						ok = false;
						break;
					}
				}

				ok = ( !badCombat && !badLook && m.CheckSkill( SkillName.Stealth, 0.0 - bonus, 100.0 - bonus ) );
			}

			if ( badCombat )
			{
				m.RevealingAction();

				m.LocalOverheadMessage( MessageType.Regular, 0x22, 501237 ); // You can't seem to hide right now.

				return TimeSpan.FromSeconds( 1.0 );
			}
			else
			{
				if (m is PlayerMobile)
				{
					PlayerMobile pm = (PlayerMobile)m;
					if (pm.Looking != null && pm.InRange( pm.Looking.Location, 18 ) && pm.Looking.InLOS( pm ) && pm.Looking.LookingAt == pm)
					{
						ok = false;
						pm.SendMessage("You can not hide when someone is watching you!");
					}
					else pm.Looking = null;
				}




				if ( ok )
				{
					if (!m.Mounted || m.AccessLevel > AccessLevel.Player)
					{
						m.Hidden = true;
						m.LocalOverheadMessage( MessageType.Regular, 0x1F4, 501240 ); // You have hidden yourself well.
					}
					else
						m.SendMessage("You cannot hide while mounted");

				}
				else
				{
					m.RevealingAction();

					m.LocalOverheadMessage( MessageType.Regular, 0x22, 501241 ); // You can't seem to hide here.
				}

				return TimeSpan.FromSeconds( 3.0 );
			}
		}
	}
}
