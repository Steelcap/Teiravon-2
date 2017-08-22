using Server;
using System;
using Server.Items;
using Server.Spells;
using Server.Mobiles;

namespace Server.Regions
{
	public class CustomRegion : GuardedRegion
	{
		RegionControl m_Controller;

		public CustomRegion( RegionControl m, Map map ) : base( "", "Custom Region", map, typeof( WarriorGuard ) )
		{
			LoadFromXml = false;
			
            m_Controller = m;
		}

		public override bool IsDisabled()
		{
			if( base.Disabled != !m_Controller.GetFlag( RegionFlag.IsGuarded ) )
			{
				m_Controller.IsGuarded = !Disabled;
			}

			return Disabled;
		}

		public override bool AllowBenificial( Mobile from, Mobile target )
		{
			if( ( !m_Controller.AllowBenifitPlayer && target is PlayerMobile ) || ( !m_Controller.AllowBenifitNPC && target is BaseCreature ))
			{
				from.SendMessage( "You cannot perform benificial acts on your target." );
				return false;
			}

			return base.AllowBenificial( from, target );
		}

		public override bool AllowHarmful( Mobile from, Mobile target )
		{
			if( ( !m_Controller.AllowHarmPlayer && target is PlayerMobile ) || ( !m_Controller.AllowHarmNPC && target is BaseCreature ))
			{
				from.SendMessage( "You cannot perform harmful acts on your target." );
				return false;
			}

			return base.AllowHarmful( from, target );
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return m_Controller.AllowHousing;
		}

		public override bool AllowSpawn()
		{
			return m_Controller.AllowSpawn;
		}

		public override bool CanUseStuckMenu( Mobile m )
		{
			if ( ! m_Controller.CanUseStuckMenu )
				m.SendMessage( "You cannot use the Stuck menu here." );
			return m_Controller.CanUseStuckMenu;
		}

		public override bool OnDamage( Mobile m, ref int Damage )
		{
			if ( !m_Controller.CanBeDamaged )
			{
				m.SendMessage( "You cannot be damaged here." );
			}

			return m_Controller.CanBeDamaged;
		}
		public override bool OnResurrect( Mobile m )
		{
			if ( ! m_Controller.CanRessurect && m.AccessLevel == AccessLevel.Player)
				m.SendMessage( "You cannot ressurect here." );
			return m_Controller.CanRessurect;
		}

		public override bool OnBeginSpellCast( Mobile from, ISpell s )
		{
			if ( from.AccessLevel == AccessLevel.Player )
			{
				bool restricted = m_Controller.IsRestrictedSpell( s );
				if ( restricted )
				{
					from.SendMessage( "You cannot cast that spell here." ); 
					return false;
				}

				//if ( s is EtherealSpell && !CanMountEthereal ) Grr, EthereealSpell is private :<
				if ( ! m_Controller.CanMountEthereal && ((Spell)s).Info.Name == "Ethereal Mount" ) //Hafta check with a name compare of the string to see if ethy
				{
					from.SendMessage( "You cannot mount your ethereal here." );
					return false; 
				}
			}

			//Console.WriteLine( m_Controller.GetRegistryNumber( s ) );

			//return base.OnBeginSpellCast( from, s );
			return true;	//Let users customize spells, not rely on weather it's guarded or not.
		}

		public override bool OnDecay( Item item )
		{
			return m_Controller.ItemDecay;
		}

		public override bool OnHeal( Mobile m, ref int Heal )
		{
			if ( !m_Controller.CanHeal )
			{
				m.SendMessage( "You cannot be healed here." );
			}

			return m_Controller.CanHeal;
		}

		public override bool OnSkillUse( Mobile m, int skill )
		{
			bool restricted = m_Controller.IsRestrictedSkill( skill );
			if ( restricted && m.AccessLevel == AccessLevel.Player )
			{
				m.SendMessage( "You cannot use that skill here." ); 
				return false;
			}

			return base.OnSkillUse( m, skill );
		}

		public override void OnExit( Mobile m )
		{
			if ( m_Controller.ShowExitMessage )
				m.SendMessage("You have left {0}", this.Name );

			base.OnExit( m );

		}

		public override void OnEnter( Mobile m )
		{
			if ( m_Controller.ShowEnterMessage )
				m.SendMessage("You have entered {0}", this.Name );

			base.OnEnter( m );
		}


		
		public override bool OnMoveInto( Mobile m, Direction d, Point3D newLocation, Point3D oldLocation )
		{
			if( m_Controller.CannotEnter && ! this.Contains( oldLocation ) )
			{
				m.SendMessage( "You cannot enter this area." );
				return false; 
			}

			return true;
		}

		public override TimeSpan GetLogoutDelay( Mobile m )
		{
			if( m.AccessLevel == AccessLevel.Player )
				return m_Controller.PlayerLogoutDelay;

			return base.GetLogoutDelay( m );
		}


		
		public override bool OnDoubleClick( Mobile m, object o )
		{
			if( o is BasePotion && !m_Controller.CanUsePotions )
			{
				m.SendMessage( "You cannot drink potions here." );
				return false;
			}
			
			if( o is Corpse )
			{
				Corpse c = (Corpse)o;

				bool canLoot;

				if( c.Owner == m )
					canLoot = !m_Controller.CannotLootOwnCorpse;
				else if ( c.Owner is PlayerMobile )
					canLoot =  m_Controller.CanLootPlayerCorpse;
				else
					canLoot =  m_Controller.CanLootNPCCorpse;

				if( !canLoot )
					m.SendMessage( "You cannot loot that corpse here." );

				if ( m.AccessLevel >= AccessLevel.GameMaster && !canLoot )
				{
					m.SendMessage( "This is unlootable but you are able to open that with your Godly powers." );
					return true;
				}
				
				return canLoot;
			}


			return base.OnDoubleClick( m, o );
		}


		
		public override void AlterLightLevel( Mobile m, ref int global, ref int personal ) 
		{
			if( m_Controller.LightLevel >= 0 )
				global = m_Controller.LightLevel;
			else
				base.AlterLightLevel( m, ref global, ref personal );
		}
	}
}
