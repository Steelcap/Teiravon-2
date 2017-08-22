using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public abstract class WeaponAbility
	{
		public virtual int BaseMana{ get{ return 80; } }

		public virtual double AccuracyScalar{ get{ return 1.0; } }
		public virtual double DamageScalar{ get{ return 1.0; } }

		public virtual void OnHit( Mobile attacker, Mobile defender, int damage )
		{
		}

		public virtual void OnMiss( Mobile attacker, Mobile defender )
		{
		}

		public virtual double GetRequiredSkill( Mobile from )
		{
			BaseWeapon weapon = from.Weapon as BaseWeapon;

			if ( weapon != null && weapon.PrimaryAbility == this )
			{
				if (from is TeiravonMobile)
				{
					TeiravonMobile m = (TeiravonMobile)from;
					if (m.HasFeat(TeiravonMobile.Feats.AxeFighter) && (m.FindItemOnLayer(Layer.TwoHanded) is BaseAxe || m.FindItemOnLayer(Layer.OneHanded) is WarAxe))
						return 90.0;
					else if (this is InfectiousStrike && m.HasFeat(TeiravonMobile.Feats.ExoticPoisons))
						return 90.0;
					else
						return 100.0;
				}
				else
				{
					return 100.0;
				}
			}
			else if ( weapon != null && weapon.SecondaryAbility == this )
			{
				if (from is TeiravonMobile)
				{
					TeiravonMobile m = (TeiravonMobile)from;
					if (m.HasFeat(TeiravonMobile.Feats.AxeFighter) && (m.FindItemOnLayer(Layer.TwoHanded) is BaseAxe) || m.FindItemOnLayer(Layer.OneHanded) is WarAxe)
						return 90.0;
					else if (this is InfectiousStrike && m.HasFeat(TeiravonMobile.Feats.ExoticPoisons))
						return 90.0;
					else
						return 100.0;
				}
				else
				{
					return 100.0;
				}
			}

			return 200.0;
		}

		public virtual int CalculateMana( Mobile from )
		{
			int mana = BaseMana;

			int reduction = (int)(from.Skills[SkillName.ArmsLore].Value / 2);
			
			mana -= reduction;


/*			double skillTotal = GetSkill( from, SkillName.Swords ) + GetSkill( from, SkillName.Macing )
				+ GetSkill( from, SkillName.Fencing ) + GetSkill( from, SkillName.Archery ) + GetSkill( from, SkillName.Parry )
				+ GetSkill( from, SkillName.Lumberjacking ) + GetSkill( from, SkillName.Stealth )
				+ GetSkill( from, SkillName.Poisoning );

			if ( skillTotal >= 400.0 )
				mana -= 10;
			else if ( skillTotal >= 300.0 )
				mana -= 5;

			double scalar = 1.0;
			if ( !Server.Spells.Necromancy.MindRotSpell.GetMindRotScalar( from, ref scalar ) )
				scalar = 1.0;

			// Lower Mana Cost = 40%
			int lmc = AosAttributes.GetValue( from, AosAttribute.LowerManaCost );
			if ( lmc > 40 )
				lmc = 40;

			scalar -= (double)lmc / 100;
			mana = (int)(mana * scalar);
*/
			// Using a special move within 3 seconds of the previous special move costs double mana
			if ( GetContext( from ) != null )
				mana *= 2;

			if (from is TeiravonMobile)
			{
				TeiravonMobile m = (TeiravonMobile)from;
                BaseWeapon w = m.Weapon as BaseWeapon;
				if (m.HasFeat(TeiravonMobile.Feats.AxeFighter) && (m.FindItemOnLayer(Layer.TwoHanded) is BaseAxe) || m.FindItemOnLayer(Layer.OneHanded) is WarAxe)
					mana /= 2;
                if (m.IsMonk() && (w is BaseStaff || w is Scimitar || w is Club || w is CrescentBlade || w is DoubleBladedStaff))
                    mana /= 2;
			}

			return mana;
		}

		public virtual bool CheckWeaponSkill( Mobile from )
		{
			BaseWeapon weapon = from.Weapon as BaseWeapon;

			if ( weapon == null )
				return false;

			Skill skill = from.Skills[weapon.Skill];
			double reqSkill = GetRequiredSkill( from );

			if ( skill != null && skill.Base >= reqSkill )
				return true;

			/* <UBWS> */
			if ( weapon.WeaponAttributes.UseBestSkill > 0 && (from.Skills[SkillName.Swords].Base >= reqSkill || from.Skills[SkillName.Macing].Base >= reqSkill || from.Skills[SkillName.Fencing].Base >= reqSkill) )
				return true;
			/* </UBWS> */

            TAVFlourish flourish = (TAVFlourish)XmlAttach.FindAttachmentOnMobile(from, typeof(TAVFlourish), "flourish");

            bool f = (flourish != null);

            bool t = (from is TeiravonMobile && ((TeiravonMobile)from).ChargedMissileReady);
            
            if (f && t )
                return true;

			from.SendLocalizedMessage( 1060182, reqSkill.ToString() ); // You need ~1_SKILL_REQUIREMENT~ weapon skill to perform that attack
            //from.SendMessage("flourish? {0} ; Ready? {1}",f,t);
			return false;
		}

		public virtual bool CheckSkills( Mobile from )
		{
			return CheckWeaponSkill( from );
		}

		public virtual double GetSkill( Mobile from, SkillName skillName )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return 0.0;

			return skill.Value;
		}

		public virtual bool CheckMana( Mobile from, bool consume )
		{
			int mana = CalculateMana( from );

			if ( from.Mana < mana )
			{
				from.SendLocalizedMessage( 1060181, mana.ToString() ); // You need ~1_MANA_REQUIREMENT~ mana to perform that attack
				return false;
			}

			if ( consume )
			{
				if ( GetContext( from ) == null )
				{
					Timer timer = new WeaponAbilityTimer( from );
					timer.Start();

					AddContext( from, new WeaponAbilityContext( timer ) );
				}

				from.Mana -= mana;
			}

			return true;
		}

		public virtual bool Validate( Mobile from )
		{
			if ( !from.Player )
				return true;

			return CheckSkills( from ) && CheckMana( from, false );
		}

		private static WeaponAbility[] m_Abilities = new WeaponAbility[14]
			{
				null,
				new ArmorIgnore(),
				new BleedAttack(),
				new ConcussionBlow(),
				new CrushingBlow(),
				new Disarm(),
				new Dismount(),
				new DoubleStrike(),
				new InfectiousStrike(),
				new MortalStrike(),
				new MovingShot(),
				new ParalyzingBlow(),
				new ShadowStrike(),
				new WhirlwindAttack()
			};

		public static WeaponAbility[] Abilities{ get{ return m_Abilities; } }

		private static Hashtable m_Table = new Hashtable();

		public static Hashtable Table{ get{ return m_Table; } }

		public static readonly WeaponAbility ArmorIgnore		= m_Abilities[ 1];
		public static readonly WeaponAbility BleedAttack		= m_Abilities[ 2];
		public static readonly WeaponAbility ConcussionBlow		= m_Abilities[ 3];
		public static readonly WeaponAbility CrushingBlow		= m_Abilities[ 4];
		public static readonly WeaponAbility Disarm				= m_Abilities[ 5];
		public static readonly WeaponAbility Dismount			= m_Abilities[ 6];
		public static readonly WeaponAbility DoubleStrike		= m_Abilities[ 7];
		public static readonly WeaponAbility InfectiousStrike	= m_Abilities[ 8];
		public static readonly WeaponAbility MortalStrike		= m_Abilities[ 9];
		public static readonly WeaponAbility MovingShot			= m_Abilities[10];
		public static readonly WeaponAbility ParalyzingBlow		= m_Abilities[11];
		public static readonly WeaponAbility ShadowStrike		= m_Abilities[12];
		public static readonly WeaponAbility WhirlwindAttack	= m_Abilities[13];

		public static bool IsWeaponAbility( Mobile m, WeaponAbility a, bool Flourish )
		{
			if ( a == null )
				return true;

			if ( !m.Player )
				return true;

            if (Flourish)
                return true;

			BaseWeapon weapon = m.Weapon as BaseWeapon;

			return ( weapon != null && (weapon.PrimaryAbility == a || weapon.SecondaryAbility == a) );
		}

		public static WeaponAbility GetCurrentAbility( Mobile m )
		{
			if ( !Core.AOS )
			{
				ClearCurrentAbility( m );
				return null;
			}

            TAVFlourish flourish = (TAVFlourish)XmlAttach.FindAttachmentOnMobile(m, typeof(TAVFlourish), "flourish");
            
            bool f = (flourish != null && m is TeiravonMobile && ((TeiravonMobile)m).ChargedMissileReady);
            WeaponAbility a;
            if (f)
                a = flourish.WeaponAbility;
            else
                a = (WeaponAbility)m_Table[m];

            
            if (!IsWeaponAbility(m, a, f))
			{
				ClearCurrentAbility( m );
				return null;
			}

			if ( a != null && !a.Validate( m ) )
			{
				ClearCurrentAbility( m );
				return null;
			}

			return a;
		}

		public static bool SetCurrentAbility( Mobile m, WeaponAbility a )
		{
			if ( !Core.AOS )
			{
				ClearCurrentAbility( m );
				return false;
			}

            TAVFlourish flourish = (TAVFlourish)XmlAttach.FindAttachmentOnMobile(m, typeof(TAVFlourish), "flourish");

            bool f = (flourish == null && m is TeiravonMobile && ((TeiravonMobile)m).ChargedMissileReady);

            if (!IsWeaponAbility(m, a, f))
            {
				ClearCurrentAbility( m );
				return false;
			}

			if ( a != null && !a.Validate( m ) )
			{
				ClearCurrentAbility( m );
				return false;
			}

			if ( a == null )
				m_Table.Remove( m );
			else
				m_Table[m] = a;

			return true;
		}

		public static void ClearCurrentAbility( Mobile m )
		{
			m_Table.Remove( m );

			if ( m.NetState != null )
				m.Send( ClearWeaponAbility.Instance );

            if (m is TeiravonMobile)
                ((TeiravonMobile)m).ChargedMissileReady = false;
		}

		public static void Initialize()
		{
			EventSink.SetAbility += new SetAbilityEventHandler( EventSink_SetAbility );
		}

		public WeaponAbility()
		{
		}

		private static void EventSink_SetAbility( SetAbilityEventArgs e )
		{
			int index = e.Index;

			if ( index == 0 )
				ClearCurrentAbility( e.Mobile );
			else if ( index >= 1 && index < m_Abilities.Length )
				SetCurrentAbility( e.Mobile, m_Abilities[index] );
		}


		private static Hashtable m_PlayersTable = new Hashtable();

		private static void AddContext( Mobile m, WeaponAbilityContext context )
		{
			m_PlayersTable[m] = context;
		}

		private static void RemoveContext( Mobile m )
		{
			WeaponAbilityContext context = GetContext( m );

			if ( context != null )
				RemoveContext( m, context );
		}

		private static void RemoveContext( Mobile m, WeaponAbilityContext context )
		{
			m_PlayersTable.Remove( m );

			context.Timer.Stop();
		}

		private static WeaponAbilityContext GetContext( Mobile m )
		{
			return ( m_PlayersTable[m] as WeaponAbilityContext );
		}

		private class WeaponAbilityTimer : Timer
		{
			private Mobile m_Mobile;

			public WeaponAbilityTimer( Mobile from ) : base ( TimeSpan.FromSeconds( 3.0 ) )
			{
				m_Mobile = from;

				Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				RemoveContext( m_Mobile );
			}
		}

		private class WeaponAbilityContext
		{
			private Timer m_Timer;

			public Timer Timer{ get{ return m_Timer; } }

			public WeaponAbilityContext( Timer timer )
			{
				m_Timer = timer;
			}
		}
	}
}
