using Server.Items;
using System;
using Server.Engines.XmlSpawner2;

namespace Server.Mobiles
{
	public abstract class BaseMount : BaseCreature, IMount
	{
		private Mobile m_Rider;
		private Item m_InternalItem;

		public virtual bool AllowMaleRider{ get{ return true; } }
		public virtual bool AllowFemaleRider{ get{ return true; } }

		public BaseMount( string name, int bodyID, int itemID, AIType aiType, FightMode fightMode, int rangePerception, int rangeFight, double activeSpeed, double passiveSpeed ) : base ( aiType, fightMode, rangePerception, rangeFight, activeSpeed, passiveSpeed )
		{
			Name = name;
			Body = bodyID;

			m_InternalItem = new MountItem( this, itemID );
		}

		public BaseMount( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Rider );
			writer.Write( m_InternalItem );
		}

		[Hue, CommandProperty( AccessLevel.GameMaster )]
		public override int Hue
		{
			get
			{
				return base.Hue;
			}
			set
			{
				base.Hue = value;

				if ( m_InternalItem != null )
					m_InternalItem.Hue = value;
			}
		}

		public override bool OnBeforeDeath()
		{
			Rider = null;

            if (Saddlebags)
                PackAnimal.CombineBackpacks(this);

			return base.OnBeforeDeath();
		}

		public override void OnAfterDelete()
		{
			if ( m_InternalItem != null )
				m_InternalItem.Delete();

			m_InternalItem = null;

			base.OnAfterDelete();
		}

		public override void OnDelete()
		{
			Rider = null;

			base.OnDelete();
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Rider = reader.ReadMobile();
					m_InternalItem = reader.ReadItem();

					if ( m_InternalItem == null )
						Delete();

					break;
				}
			}
		}

		public virtual void OnDisallowedRider( Mobile m )
		{
			m.SendMessage( "You may not ride this creature." );
		}

        #region Pack Animal Methods
        public virtual bool Saddlebags
        { get{
            bool hasSaddleBag = false;
            XmlData check = (XmlData)XmlAttach.FindAttachmentOnMobile(this, typeof(XmlData), "saddlebag");
            hasSaddleBag = check != null;

            return hasSaddleBag;} }
 

        public override DeathMoveResult GetInventoryMoveResultFor(Item item)
        {
            return DeathMoveResult.MoveToCorpse;
        }

        public override bool IsSnoop(Mobile from)
        {
            if (Saddlebags)
            {
                if (PackAnimal.CheckAccess(this, from))
                    return false;

                return base.IsSnoop(from);
            }
            return false;
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            if (Saddlebags)
            {
                if (CheckFeed(from, item))
                    return true;

                if (PackAnimal.CheckAccess(this, from))
                {
                    AddToBackpack(item);
                    return true;
                }
            }

            return base.OnDragDrop(from, item);
        }

        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override bool CheckNonlocalLift(Mobile from, Item item)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override void GetContextMenuEntries(Mobile from, System.Collections.ArrayList list)
        {
            base.GetContextMenuEntries(from, list);

            if (Saddlebags)
            {
                PackAnimal.GetContextMenuEntries(this, from, list);
            }

        }
        #endregion

		public override void OnDoubleClick( Mobile from )
		{

			if ( from is TeiravonMobile )
			{
				TeiravonMobile m_Player = (TeiravonMobile) from;

				if ( m_Player.IsOrc() && !(this is Horngoth)  && !(this is Warg) && !(this is WarMount))
				{
					m_Player.SendMessage( "You cannot ride that.");
					return;
				}

                if (!m_Player.IsFrostling() && this is FrostlingPolarBear)
                {
                    m_Player.SendMessage("You can't ride a bear! What do you think you are, some kind of frozen viking?");
                    return;
                }

				//Prevent the usage of scytches, bardiches, pikes and halberds on horses
				if ( m_Player.Weapon is BasePoleArm || m_Player.Weapon is Pike )
				{
					m_Player.SendMessage( "You cannot ride with your weapon." );
					return;
				}

				//Prevent usage of spears and lances on horses unless the player has the mounted combat feat
				if ( ( m_Player.Weapon is BaseSpear || m_Player.Weapon is Lance ) && !m_Player.HasFeat( TeiravonMobile.Feats.MountedCombat ) )
				{
					m_Player.SendMessage( "You must have the Mounted Combat Feat to ride with that weapon." );
					return;
				}


				//Prevent usage of certain "heavy bows" on horses

				bool heavybow = false;

				Type[] m_HeavyBows = new Type[]
				{
					typeof( Recurve ),
					typeof( Crossbow ),
					typeof( Longbow ),
					typeof( CompositeBow ),
					typeof( HeavyCrossbow ),
					typeof( RepeatingCrossbow )
				};

				for (int i=0; i < m_HeavyBows.Length; i++ )
				{
				 	if ( m_Player.Weapon.GetType() == m_HeavyBows[i] )
				 	{
				 		heavybow = true;
				 		break;
				 	}
				}

				if ( heavybow )
				{
					m_Player.SendMessage( "Your bow is too unwieldy to be used while mounted." );
					return;
				}

			}

			if ( IsDeadPet )
				return;

			if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				if ( Core.AOS ) // You cannot ride a mount in your current form.
					PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 1062061, from.NetState );
				else
					from.SendLocalizedMessage( 1061628 ); // You can't do that while polymorphed.

				return;
			}

			if ( Core.AOS && Server.Items.Bola.UnderEffect( from ) )
			{
				from.SendLocalizedMessage( 1062910 ); // You cannot mount while recovering from a bola throw.
				return;
			}

			if ( !from.CanBeginAction( typeof( BaseMount ) ) )
			{
				from.SendLocalizedMessage( 1040024 ); // You are still too dazed from being knocked off your mount to ride!
				return;
			}

			if ( from.Mounted )
			{
				from.SendLocalizedMessage( 1005583 ); // Please dismount first.
				return;
			}

			if ( from.Female ? !AllowFemaleRider : !AllowMaleRider )
			{
				OnDisallowedRider( from );
				return;
			}

			if ( !Multis.DesignContext.Check( from ) )
				return;

			if ( from.HasTrade )
			{
				from.SendLocalizedMessage( 1042317, "", 0x41 ); // You may not ride at this time
				return;
			}

			if ( from.InRange( this, 1 ) )
			{
				bool canAccess = ( from.AccessLevel >= AccessLevel.GameMaster )
					|| ( Controled && ControlMaster == from )
					|| ( Summoned && SummonMaster == from );

				if ( canAccess )
				{
					if ( this.Poisoned )
						PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 1049692, from.NetState ); // This mount is too ill to ride.
					else
					{
						Rider = from;
						if (from.AccessLevel == AccessLevel.Player)
							from.Hidden = false;
						if (from is TeiravonMobile)
						{
							TeiravonMobile m = (TeiravonMobile)from;
							int dexb = 0;
							int intb = 0;
							string modName = m.Serial.ToString();
							switch (m.RidingSkill)
							{
								case 0:
									dexb = -25;
									intb = -25;
									break;
								case 1:
									dexb = -15;
									intb = -15;
									break;
								case 2:
									dexb = -5;
									intb = -5;
									break;
								case 3:
									break;
								case 4:
									dexb = 10;
									intb = 5;
									break;
							}
							
							if ( dexb != 0 )
								m.AddStatMod( new StatMod( StatType.Dex, modName + "MntDex", dexb, TimeSpan.Zero ) );
							if ( intb != 0 )
								m.AddStatMod( new StatMod( StatType.Int, modName + "MntInt", intb, TimeSpan.Zero ) );
						}
					}
				}
				else if ( !Controled && !Summoned )
				{
					// That mount does not look broken! You would have to tame it to ride it.
					PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 501263, from.NetState );
				}
				else
				{
					// This isn't your mount; it refuses to let you ride.
					PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 501264, from.NetState );
				}
			}
			else
			{
				from.SendLocalizedMessage( 500206 ); // That is too far away to ride.
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ItemID
		{
			get
			{
				if ( m_InternalItem != null )
					return m_InternalItem.ItemID;
				else
					return 0;
			}
			set
			{
				if ( m_InternalItem != null )
					m_InternalItem.ItemID = value;
			}
		}

		public static void Dismount( Mobile m )
		{
			IMount mount = m.Mount;

			if ( mount != null )
			{
				mount.Rider = null;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Rider
		{
			get
			{
				return m_Rider;
			}
			set
			{
				if ( m_Rider != value )
				{
					if ( value == null )
					{
						Point3D loc = m_Rider.Location;
						Map map = m_Rider.Map;

						if ( map == null || map == Map.Internal )
						{
							loc = m_Rider.LogoutLocation;
							map = m_Rider.LogoutMap;
						}

						Direction = m_Rider.Direction;
						Location = loc;
						Map = map;

						if ( m_InternalItem != null )
							m_InternalItem.Internalize();
					}
					else
					{
						if ( m_Rider != null )
							Dismount( m_Rider );

						Dismount( value );

						if ( m_InternalItem != null )
							value.AddItem( m_InternalItem );

						value.Direction = this.Direction;

						Internalize();
					}

					//MOD Mounted Combat variable
					Mobile m_OldRider = m_Rider;
					//

					m_Rider = value;

					//STARTMOD: TEIRAVON Mounted Combat
					if ( (m_OldRider != null ) && ( m_Rider == null ) && m_OldRider is TeiravonMobile )
					{
						TeiravonMobile m_Player = (TeiravonMobile)m_OldRider;
						m_Player.CheckResistanceBonus();

						if ( m_Player.HasFeat( TeiravonMobile.Feats.MountedCombat ) )
						{
							m_Player.Delta( MobileDelta.WeaponDamage );
							m_Player.SendMessage( "You loose your mounted damage bonus." );
						}

					}
					else if ( ( m_Rider != null ) && m_Rider is TeiravonMobile )
					{
						TeiravonMobile m_Player = (TeiravonMobile)m_Rider;
						m_Player.CheckResistanceBonus();

						if ( m_Player.HasFeat( TeiravonMobile.Feats.MountedCombat ) )
						{
							m_Player.Delta( MobileDelta.WeaponDamage );

							if ( m_Player.Weapon is Lance )
								m_Player.SendMessage( "Your skill in mounted combat with lances grants you a powerful damage bonus." );
							else
								m_Player.SendMessage( "Your skill in mounted combat grants you a damage bonus." );
						}
					}
					//ENDMOD: TEIRAVON Mounted Combat
				}
			}
		}
	}

	public class MountItem : Item, IMountItem
	{
		private BaseMount m_Mount;

		public MountItem( BaseMount mount, int itemID ) : base( itemID )
		{
			Layer = Layer.Mount;
			Movable = false;

			m_Mount = mount;
		}

		public MountItem( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterDelete()
		{
			if ( m_Mount != null )
				m_Mount.Delete();

			m_Mount = null;

			base.OnAfterDelete();
		}

		public override DeathMoveResult OnParentDeath(Mobile parent)
		{
			if ( m_Mount != null )
				m_Mount.Rider = null;

			return DeathMoveResult.RemainEquiped;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Mount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Mount = reader.ReadMobile() as BaseMount;

					if ( m_Mount == null )
						Delete();

					break;
				}
			}
		}

		public IMount Mount
		{
			get
			{
				return m_Mount;
			}
		}
	}
}
