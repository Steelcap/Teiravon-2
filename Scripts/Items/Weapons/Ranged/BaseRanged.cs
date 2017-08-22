using System;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Mobiles;
using Addons;

namespace Server.Items
{
	public abstract class BaseRanged : BaseMeleeWeapon
	{
		private CraftResource m_Resource;

		public abstract int EffectID{ get; }
		public abstract Type AmmoType{ get; }
		public abstract Item Ammo{ get; }

		public override int DefHitSound{ get{ return 0x234; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override SkillName DefSkill{ get{ return SkillName.Archery; } }
		public override WeaponType DefType{ get{ return WeaponType.Ranged; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootXBow; } }

		public override SkillName AccuracySkill{ get{ return SkillName.Archery; } }

		private SkillMod m_Skilmod;

		public CraftAttributeInfo GetResourceAttrs()
		{
			CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

			if ( info == null )
				return CraftAttributeInfo.Blank;

			return info.AttributeInfo;
		}


		public BaseRanged( int itemID ) : base( itemID )
		{
		}

		public BaseRanged( Serial serial ) : base( serial )
		{
		}

		public override TimeSpan OnSwing( Mobile attacker, Mobile defender )
		{
			// Make sure we've been standing still for one second
			if ( DateTime.Now > (attacker.LastMoveTime + TimeSpan.FromSeconds( Core.AOS ? 0.5 : 1.0 )) || (Core.AOS && WeaponAbility.GetCurrentAbility( attacker ) is MovingShot) ||( attacker is TeiravonMobile && ((TeiravonMobile)attacker).Shapeshifted && ((TeiravonMobile)attacker).IsArcher() && ((TeiravonMobile)attacker).IsUndead()))
			{
				bool canSwing = true;

				if ( Core.AOS )
				{
					canSwing = ( !attacker.Paralyzed && !attacker.Frozen );

					if ( canSwing )
					{
						Spell sp = attacker.Spell as Spell;

						canSwing = ( sp == null || !sp.IsCasting || !sp.BlocksMovement );
					}
				}

				if (this is DwarvenBallista)
				{
					DwarvenBallista db = (DwarvenBallista)this;
					canSwing = false;
					foreach ( Item item in attacker.GetItemsInRange(1) )
					{
						if (item is AddonComponent)
						{
							AddonComponent aoc = (AddonComponent)item;
							if (aoc.Addon == db.Ballista)
							{
								canSwing = true;
							}
						}
					}
				}

				if ( canSwing && attacker.HarmfulCheck( defender ) )
				{
					attacker.DisruptiveAction();
					attacker.Send( new Swing( 0, attacker, defender ) );

					if ( OnFired( attacker, defender ) )
					{
						if ( CheckAim( attacker, defender ) && !CheckDodge(attacker , defender) )
							OnHit( attacker, defender );
						else
							OnMiss( attacker, defender );
					}
				}

				return GetDelay( attacker );
			}
			else
			{
				return TimeSpan.FromSeconds( 0.25 );
			}
		}

		public override bool OnEquip( Mobile from )
		{

			if ( from.Player && from.AccessLevel == AccessLevel.Player )
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if (  ( this is HeavyCrossbow || this is RepeatingCrossbow || this is Recurve ) && !m_Player.HasFeat( TeiravonMobile.Feats.BowSpecialization ) )
				{
					m_Player.SendMessage( "You don't have the skill to use this bow" );
					return false;
				}
			}

			return base.OnEquip( from );
		}


		public override void OnAdded( object parent )
		{

			if (parent is TeiravonMobile)
			{
				TeiravonMobile m_parent = (TeiravonMobile)parent;

				if ( ( m_parent.IsElf() ) && ( this is Bow || this is Longbow || this is CompositeBow || this is Recurve || this is Elvenbow ) )
				{
					if ( m_Skilmod != null )
						m_Skilmod.Remove();

					m_Skilmod = new DefaultSkillMod( SkillName.Archery, true, 10.0 );
					((Mobile)parent).AddSkillMod( m_Skilmod );

					m_parent.SendMessage( 0x9F2, "The bow hums in your hand..." );
				}

			}

			base.OnAdded( parent );
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );

			if ( m_Skilmod != null )
				m_Skilmod.Remove();

			m_Skilmod = null;
		}


		public override void OnHit( Mobile attacker, Mobile defender )
		{
			if ( Type != WeaponType.Thrown && attacker.Player && !defender.Player && (defender.Body.IsAnimal || defender.Body.IsMonster) && 0.4 >= Utility.RandomDouble() )
				defender.AddToBackpack( Ammo );

			if (attacker is TeiravonMobile)
			{
				TeiravonMobile tm = (TeiravonMobile) attacker;
				if (tm.Backpack != null)
				{
					if (tm.FindItemOnLayer(Layer.MiddleTorso) is ElvenQuiver && Utility.RandomMinMax(1,100) <= 50)
					{
						if (Ammo is Arrow)
						{
							Item[] ammopile = tm.Backpack.FindItemsByType( typeof(Arrow), true);
							bool firstfound = false;
							for ( int i = 0; i < ammopile.Length; ++i )
							{
								if (!firstfound)
								{
									Arrow ammostack = (Arrow)ammopile[0];
									ammostack.Amount += 1;
									firstfound = true;
								}
							}
						}
						else if (Ammo is Bolt)
						{
							Item[] ammopile = tm.Backpack.FindItemsByType( typeof(Bolt), true);
							bool firstfound = false;
							for ( int i = 0; i < ammopile.Length; ++i )
							{
								if (!firstfound)
								{
									Bolt ammostack = (Bolt)ammopile[0];
									ammostack.Amount += 1;
									firstfound = true;
								}
							}
						}
					}
				}
			}
			
			base.OnHit( attacker, defender );
		}

		public override void OnMiss( Mobile attacker, Mobile defender )
		{
            Container pack = attacker.Backpack;

            if (attacker.Player && 0.4 >= Utility.RandomDouble() && this.Type != WeaponType.Thrown)
            {
                if (pack == null || !pack.ConsumeTotal(AmmoType, 1))
                {
                    if (attacker is TeiravonMobile && ((((TeiravonMobile)attacker).IsArcher()) && (((TeiravonMobile)attacker).IsUndead()) && (((TeiravonMobile)attacker).Shapeshifted)))
                    {

                    }
                    else
                        Ammo.MoveToWorld(new Point3D(defender.X + Utility.RandomMinMax(-1, 1), defender.Y + Utility.RandomMinMax(-1, 1), defender.Z), defender.Map);
                }
                else
                    Ammo.MoveToWorld(new Point3D(defender.X + Utility.RandomMinMax(-1, 1), defender.Y + Utility.RandomMinMax(-1, 1), defender.Z), defender.Map);
            }

			if (attacker is TeiravonMobile)
			{
				TeiravonMobile tm = (TeiravonMobile) attacker;
				if (tm.Backpack != null)
				{
					if (tm.FindItemOnLayer(Layer.MiddleTorso) is ElvenQuiver && Utility.RandomMinMax(1,100) <= 50)
					{
						if (Ammo is Arrow)
						{
							Item[] ammopile = tm.Backpack.FindItemsByType( typeof(Arrow), true);
							bool firstfound = false;
							for ( int i = 0; i < ammopile.Length; ++i )
							{
								if (!firstfound)
								{
									Arrow ammostack = (Arrow)ammopile[0];
									ammostack.Amount += 1;
									firstfound = true;
								}
							}
						}
						else if (Ammo is Bolt)
						{
							Item[] ammopile = tm.Backpack.FindItemsByType( typeof(Bolt), true);
							bool firstfound = false;
							for ( int i = 0; i < ammopile.Length; ++i )
							{
								if (!firstfound)
								{
									Bolt ammostack = (Bolt)ammopile[0];
									ammostack.Amount += 1;
									firstfound = true;
								}
							}
						}
					}
				}
			}

			base.OnMiss( attacker, defender );
		}

		public virtual bool OnFired( Mobile attacker, Mobile defender )
		{
			Container pack = attacker.Backpack;
			if ( attacker.InRange( defender.Location, 2 ) )
				return false;

            if (attacker.Player && this.GetType() == AmmoType)
            {
                this.MoveToWorld(defender.Location, defender.Map);
                attacker.MovingEffect(defender, EffectID, 18, 2, false, false);
                attacker.EquipItem(pack.FindItemByType(AmmoType, true));
                return true;
            }

            if (attacker.Player && (pack == null || !pack.ConsumeTotal(AmmoType, 1)))
            {
                if (attacker is TeiravonMobile && ((((TeiravonMobile)attacker).IsArcher()) && (((TeiravonMobile)attacker).IsUndead()) && (((TeiravonMobile)attacker).Shapeshifted)))
                {
                    attacker.Damage(2);
                    attacker.MovingEffect(defender, 0xF7E, 18, 1, false, false);
                    return true;

                }
                else
                    return false;
            }
            


			attacker.MovingEffect( defender, EffectID, 18, 1, false, false );

			return true;
		}

        public static bool CheckAim(Mobile attacker, Mobile defender)
        {
            bool hasFeat = false;

            if (attacker is BaseCreature)
                return true;

            if (attacker is TeiravonMobile && ((TeiravonMobile)attacker).HasFeat(TeiravonMobile.Feats.MountedCombat))
                hasFeat = true;

            BaseWeapon atkWeapon = attacker.Weapon as BaseWeapon;
            double aimValue = attacker.Skills.Archery.Base;
            double distance = attacker.GetDistanceToSqrt(defender);
            double chance = aimValue/ (1 + ((20 * distance) -20));

            chance += (double)atkWeapon.Attributes.AttackChance * .1;
            chance += (double)((attacker.Dex /12) * .01);
            if (!hasFeat & attacker.Mounted)
                chance -= .3;

            return attacker.CheckSkill(SkillName.Archery, chance);
        }
		
        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				case 1:
				{
					break;
				}
				case 0:
				{
					/*m_EffectID =*/ reader.ReadInt();
					break;
				}
			}

			if ( version < 2 )
			{
				WeaponAttributes.MageWeapon = 0;
				WeaponAttributes.UseBestSkill = 0;
			}

			if (Parent is TeiravonMobile)
			{
				TeiravonMobile m_parent = (TeiravonMobile)Parent;

				if ( ( m_parent.IsElf() ) && ( this is Bow || this is Longbow || this is CompositeBow || this is Recurve || this is Elvenbow ) )
				{
					m_Skilmod = new DefaultSkillMod( SkillName.Archery, true, 10.0 );
					((Mobile)Parent).AddSkillMod( m_Skilmod );
				}
			}

		}
	}
}
