using System;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// This attack allows you to disarm your foe.
	/// Now in Age of Shadows, a successful Disarm leaves the victim unable to re-arm another weapon for several seconds.
	/// </summary>
	public class Disarm : WeaponAbility
	{
		public Disarm()
		{
		}

//		public override int BaseMana{ get{ return 35; } }

		// No longer active in pub21:
		/*public override bool CheckSkills( Mobile from )
		{
			if ( !base.CheckSkills( from ) )
				return false;

			if ( !(from.Weapon is Fists) )
				return true;

			Skill skill = from.Skills[SkillName.ArmsLore];

			if ( skill != null && skill.Base >= 80.0 )
				return true;

			from.SendLocalizedMessage( 1061812 ); // You lack the required skill in armslore to perform that attack!

			return false;
		}*/

		public static readonly TimeSpan BlockEquipDuration = TimeSpan.FromSeconds( 4.0 );

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
				if (Utility.Random(100) < 50 + adj)
				{
					ClearCurrentAbility( attacker );
					attacker.SendMessage("Your disarm attempt fails.");
					return;
				}
			}
			else if (attacker is TeiravonMobile && defender is BaseCreature)
			{
				BaseCreature tmd = (BaseCreature)defender;
				TeiravonMobile tma = (TeiravonMobile)attacker;
				int dlevel = tmd.Level;
				int alevel = tma.PlayerLevel;
				int adj = (dlevel - alevel) * 3;
				if (Utility.Random(100) < 50 + adj)
				{
					ClearCurrentAbility( attacker );
					defender.Say("Nice try, fiend!");
					return;
				}
			}
			
			ClearCurrentAbility( attacker );

			Item toDisarm = defender.FindItemOnLayer( Layer.OneHanded );

			if ( toDisarm == null || !toDisarm.Movable )
				toDisarm = defender.FindItemOnLayer( Layer.TwoHanded );

			Container pack = defender.Backpack;

			if ( pack == null || (toDisarm != null && !toDisarm.Movable) )
			{
				attacker.SendLocalizedMessage( 1004001 ); // You cannot disarm your opponent.
			}
			else if ( toDisarm == null || toDisarm is BaseShield )
			{
				attacker.SendLocalizedMessage( 1060849 ); // Your target is already unarmed!
			}
			else if ( CheckMana( attacker, true ) )
			{
				attacker.SendLocalizedMessage( 1060092 ); // You disarm their weapon!
				defender.SendLocalizedMessage( 1060093 ); // Your weapon has been disarmed!

				defender.PlaySound( 0x3B9 );
				defender.FixedParticles( 0x37BE, 232, 25, 9948, EffectLayer.LeftHand );

				pack.DropItem( toDisarm );

				BaseWeapon.BlockEquip( defender, BlockEquipDuration );
				
				if (defender is BaseCreature)
				{
					Timer timer = new RearmTimer(defender);
					timer.Start();
				}
			}
		}
	}
	
	public class RearmTimer : Timer
	{
		private Mobile m_Mobile;

		public RearmTimer( Mobile from ) : base ( TimeSpan.FromSeconds( 5.0 ) )
		{
			m_Mobile = from;
		}

		protected override void OnTick()
		{
			if (m_Mobile.Alive)
			{
				Container pack = m_Mobile.Backpack;

				if ( pack == null )
					return;

				Item weapon = pack.FindItemByType( typeof( BaseWeapon ) );

				if ( weapon == null )
					return;

				m_Mobile.EquipItem( weapon );
			
				m_Mobile.Say("Aha, now you'll pay!");
			}
		}
	}
}
