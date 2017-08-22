using System;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Perfect for the foot-soldier, the Dismount special attack can unseat a mounted opponent.
	/// The fighter using this ability must be on his own two feet and not in the saddle of a steed
	/// (with one exception: players may use a lance to dismount other players while mounted).
	/// If it works, the target will be knocked off his own mount and will take some extra damage from the fall!
	/// </summary>
	public class Dismount : WeaponAbility
	{
		public Dismount()
		{
		}

//		public override int BaseMana{ get{ return 20; } }

		public override bool Validate( Mobile from )
		{
			if ( !base.Validate( from ) )
				return false;

			if ( from.Mounted && (!(from.Weapon is Lance) && !(from.Weapon is HumanBandedLance) ) )
			{
				from.SendLocalizedMessage( 1061283 ); // You cannot perform that attack while mounted!
				return false;
			}

			return true;
		}

		public static readonly TimeSpan BlockMountDuration = TimeSpan.FromSeconds( 10.0 ); // TODO: Taken from bola script, needs to be verified

		public override void OnHit( Mobile attacker, Mobile defender, int damage )
		{
			if ( !Validate( attacker ) )
				return;

			if (attacker is TeiravonMobile && defender is TeiravonMobile)
			{
				TeiravonMobile tmd = (TeiravonMobile)defender;
				TeiravonMobile tma = (TeiravonMobile)attacker;
				int dlevel = tmd.PlayerLevel;
				int alevel = tma.PlayerLevel;
				int adj = (dlevel - alevel) * 3;
				switch(tmd.RidingSkill)
				{
					case 0:
						adj -= 10;
						break;
					case 2:
						adj += 5;
						break;
					case 3:
						adj += 10;
						break;
					case 4:
						adj += 20;
						break;
					default:
						break;
				}
				if (Utility.Random(100) < 50 + adj)
				{
					ClearCurrentAbility( attacker );
					attacker.SendMessage("Your dismount attempt fails.");
					return;
				}
			}
			
//			if ( attacker.Mounted && !(defender.Weapon is Lance) ) // TODO: Should there be a message here?
//				return;

			ClearCurrentAbility( attacker );

			IMount mount = defender.Mount;

			if ( mount == null )
			{
				attacker.SendLocalizedMessage( 1060848 ); // This attack only works on mounted targets
				return;
			}

			if ( !CheckMana( attacker, true ) )
				return;

			attacker.SendLocalizedMessage( 1060082 ); // The force of your attack has dislodged them from their mount!

			//if ( attacker.Mounted )
			//	defender.SendLocalizedMessage( 1062315 ); // You fall off your mount!
			//else
				defender.SendLocalizedMessage( 1060083 ); // You fall off of your mount and take damage!

			defender.PlaySound( 0x140 );
			defender.FixedParticles( 0x3728, 10, 15, 9955, EffectLayer.Waist );
			
			if (mount.Rider is TeiravonMobile)
			{
				TeiravonMobile tm = (TeiravonMobile)mount.Rider;
				tm.Dismounted();
			}
			mount.Rider = null;

			defender.BeginAction( typeof( BaseMount ) );
			Timer.DelayCall( BlockMountDuration, new TimerStateCallback( ReleaseMountLock ), defender );
//			if ( !attacker.Mounted )
				AOS.Damage( defender, attacker, Utility.RandomMinMax( 15, 25 ), 100, 0, 0, 0, 0 );
		}

		private void ReleaseMountLock( object state )
		{
			((Mobile)state).EndAction( typeof( BaseMount ) );
		}
	}
}
