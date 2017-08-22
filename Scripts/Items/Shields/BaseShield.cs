using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class BaseShield : BaseArmor
	{
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }

		public BaseShield( int itemID ) : base( itemID )
		{
		}

		public BaseShield( Serial serial ) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override double ArmorRating
		{
			get
			{
				Mobile m = this.Parent as Mobile;
				double ar = base.ArmorRating;

				if ( m != null )
					return ( ( m.Skills[SkillName.Parry].Value * ar ) / 200.0 ) + 1.0;
				else
					return ar;
			}
		}

		public override int OnHit( BaseWeapon weapon, int damage )
		{
			Mobile owner = this.Parent as Mobile;
			if ( owner == null )
				return damage;

			double ar = this.ArmorRating;
			double chance = ( owner.Skills[SkillName.Parry].Value - ( ar * 2.0 ) ) / 100.0;

			if ( chance < 0.01 )
				chance = 0.01;
			/*
			FORMULA: Displayed AR = ((Parrying Skill * Base AR of Shield) ÷ 200) + 1

			FORMULA: % Chance of Blocking = parry skill - (shieldAR * 2)

			FORMULA: Melee Damage Absorbed = (AR of Shield) / 2 | Archery Damage Absorbed = AR of Shield
			*/
			if ( owner.CheckSkill( SkillName.Parry, chance ) )
			{
				if ( weapon.Skill == SkillName.Archery )
					damage -= (int)ar;
				else
					damage -= (int)(ar / 2.0);

				if ( damage < 0 )
					damage = 0;

				owner.FixedEffect( 0x37B9, 10, 16 );

				if ( Utility.Random( 3 ) == 0 )
				{
					int wear = Utility.RandomMinMax(1, 2 );
					
					if ( Core.AOS && ArmorAttributes.SelfRepair > Utility.Random( 10 ) )
						wear = 0;

					if ( wear > 0 && MaxHitPoints > 0 )
					{
						if ( HitPoints >= wear )
						{
							HitPoints -= wear;
							wear = 0;
						}
						else
						{
							wear -= HitPoints;
							HitPoints = 0;
						}

						if ( wear > 0 )
						{
							if ( MaxHitPoints > wear )
							{
								MaxHitPoints -= wear;

								if ( Parent is Mobile )
									((Mobile)Parent).LocalOverheadMessage( MessageType.Regular, 0x3B2, 1061121 ); // Your equipment is severely damaged.
							}
							else
							{
								Delete();
							}
						}
					}
				}
			}

			return damage;
		}

		public override void OnDoubleClick( Mobile from )
		{
			TeiravonMobile m_Player = (TeiravonMobile) from;

			if ( m_Player.HasFeat( TeiravonMobile.Feats.ShieldBash ) && m_Player.FindItemOnLayer( Layer.TwoHanded) == this )
			{
				if ( m_Player.GetActiveFeats( TeiravonMobile.Feats.ShieldBash ) )
					m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.TooSoon );
				else
//					m_Player.Target = new ShieldBashTarget( (int)this.ArmorRating );
					m_Player.Target = new ShieldBashTarget( (int)this.ArmorBase, this );
			}
		}

		private class ShieldBashTarget : Target
		{
			private int m_ar;
			private BaseShield shield;

			public ShieldBashTarget( int ar, object sh) : base( 1, false, TargetFlags.Harmful )
			{
				CheckLOS = true;
				m_ar = ar;
				shield = (BaseShield)sh;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					if ( o == from )
					{
						from.SendMessage( "You can't target yourself." );
						return;
					}

					TeiravonMobile m_Player = (TeiravonMobile)from;
					Mobile m_Target = (Mobile)o;

					if ( m_Target != null )
					{
						int bashdamage = m_ar;
						
						if (shield is SpikedShield)
							bashdamage = bashdamage + (int)(m_Player.PlayerLevel * 1.5);
						
						m_Target.Damage( bashdamage, from );

						if (Utility.Random( 1, 20) <= m_Player.PlayerLevel)
						{
							from.SendMessage( "You deliver a stunning blow!" );
							m_Target.SendMessage( "The attack has stunned you!" );
							m_Target.Paralyze( m_Target.Player ? TimeSpan.FromSeconds( 3.0 ) : TimeSpan.FromSeconds( 6.0 ) );
							m_Target.FixedEffect( 0x376A, 9, 32 );
							m_Target.PlaySound( 0x204 );
						}
					}

					// Reuse Timer
					TimerHelper m_TimerHelper = new TimerHelper( (int)m_Player.Serial );
					m_TimerHelper.DoFeat = true;
					m_TimerHelper.Feat = TeiravonMobile.Feats.ShieldBash;
					m_TimerHelper.Duration = Server.Teiravon.AbilityCoolDown.AtWill;
					m_TimerHelper.Start();

					m_Player.SetActiveFeats( TeiravonMobile.Feats.ShieldBash, true );

				}
				else
					from.SendMessage( "That is not a living creature." );
			}
		}
	}
}
