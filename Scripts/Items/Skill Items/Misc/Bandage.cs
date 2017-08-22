using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Gumps;
using Server.Spells;
using Server.Misc;

namespace Server.Items
{
	public class Bandage : Item, IDyable
	{
		[Constructable]
		public Bandage() : this( 1 )
		{
		}

		[Constructable]
		public Bandage( int amount ) : base( 0xE21 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public Bandage( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is TeiravonMobile )
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( m_Player.IsShifted() && m_Player.Shapeshifted )
				{
					m_Player.SendMessage( "You cannot apply bandages while shapeshifted." );
					return;
				}
			}

			if ( from.InRange( GetWorldLocation(), Core.AOS ? 2 : 1 ) )
			{
				from.RevealingAction();

				from.SendLocalizedMessage( 500948 ); // Who will you use the bandages on?

				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
			}
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Bandage(), amount );
		}
       
        private static SlayerEntry m_Slayer = SlayerGroup.GetEntryByName(SlayerName.Silver);

		public static void BandageClearHands( Mobile from )
		{
			BandageClearHand( from.FindItemOnLayer( Layer.OneHanded ), from );
			BandageClearHand( from.FindItemOnLayer( Layer.TwoHanded ), from );
		}

		private static void BandageClearHand( Item item, Mobile from )
		{
			if ( item != null && item.Movable )
			{
				Container pack = from.Backpack;

				if ( pack == null )
					from.AddToBackpack( item );
				else
					pack.DropItem( item );
			}
		}

        public class ReviveTimer : Timer
        {
            Mobile m_Healer;
            Mobile m_Ghost;
            Corpse m_Corpse;

            public ReviveTimer(Mobile from, Corpse corpse, Mobile Ghost) : base(TimeSpan.FromSeconds(8))
            {
                 m_Healer = from;
                 m_Ghost = Ghost;
                 m_Corpse = corpse;
            }

            protected override void OnTick()
            {
                if (m_Corpse.Deleted || m_Ghost.Deleted || m_Healer.Deleted)
                    return;

                if (!m_Healer.Alive)
                {
                    m_Healer.SendMessage("You were unable to finish your work before your died.");
                    return;
                }

                if (m_Healer.CheckSkill(SkillName.Healing, 40.0, 120.0))
                {
                    m_Ghost.Location = m_Corpse.Location;
                    m_Ghost.Resurrect();

                    if (m_Healer is TeiravonMobile && (((TeiravonMobile)m_Healer).IsCleric() || ((TeiravonMobile)m_Healer).IsDarkCleric() || ((TeiravonMobile)m_Healer).IsForester() || ((TeiravonMobile)m_Healer).IsPaladin()))
                    {
                        Titles.AwardExp((TeiravonMobile)(m_Healer), m_Ghost.Fame);
                    }
                    Titles.AwardFame(m_Ghost, -(m_Ghost.Fame / 15), true);
                    m_Corpse.Open(m_Ghost, true);
                    m_Ghost.Animate(m_Ghost.Body == 17 ? 2 : 21, 4, 1, false, false, 0);
                    m_Ghost.Emote("*Gasps back to life!*");
                    m_Ghost.PlaySound(m_Ghost.Female ? 0x319 : 0x429);
                    m_Healer.SendMessage("You revive the fallen!");
                    Titles.AwardFame(m_Healer, m_Ghost.Fame / 20, true);
                }
                else
                {
                    m_Healer.SendMessage("You were unable to revive the body.");
                }

            }
        }

		private class InternalTarget : Target
		{
			private Bandage m_Bandage;

			public InternalTarget( Bandage bandage ) : base( 1, false, TargetFlags.Beneficial )
			{
				m_Bandage = bandage;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Bandage.Deleted )
					return;

                if (targeted is Corpse)
                {
                    Corpse c = targeted as Corpse;
                    bool found = false;
                    Mobile owner = c.Owner;
                    Timer m_Revive;

                    Mobile ghost = null;

                    if (owner == null || owner.Deleted || (owner is TeiravonMobile && ((TeiravonMobile)owner).IsUndead()) || c.ItemID != 0x2006)
                    {
                        from.SendMessage("You cannot revive that!");
                        return;
                    }

                    IPooledEnumerable eable = c.Map.GetMobilesInRange(c.Location, 1);

                    foreach (Mobile m in eable)
                    {
                        if (m == c.Owner)
                        {
                            found = true;
                            ghost = m;
                        }
                    }
                    eable.Free();
                    if (!found && c.Owner !=null)
                    {
                        from.SendMessage("The body is cold and unresponsive as if the spirit has left it.");
                        c.Owner.SendMessage("You feel drawn to your body.");
                        return;
                    }
                    if (found && ghost != null && !ghost.Alive)
                    {
                        from.SendMessage("You attempt to revive the fallen.");
                        m_Revive = new ReviveTimer(from, c, ghost);
                        SpellHelper.Turn(from, c);
                        BandageClearHands(from);
                        from.Animate( 32, 5, 1, true, false, 0 );
                        m_Bandage.Consume();
                        m_Revive.Start();
                        return;
                    }
                }
                
				if ( targeted is Mobile )
				{
					if ( from.InRange( m_Bandage.GetWorldLocation(), Core.AOS ? 2 : 1 ) )
					{

						TeiravonMobile m_Player = targeted as TeiravonMobile;
                        Mobile m = targeted as Mobile;
                        if (targeted is TeiravonMobile && m_Player.PlayerRace == TeiravonMobile.Race.Undead)
                        {
                            from.SendMessage("The dead cannot be bandaged.");
                            return;
                        }


                        if ( m_Slayer.Slays(m))
                        {
                            from.SendLocalizedMessage(1060177); // You cannot heal a creature that is already dead!
                            return;
                        }

						if ( m_Player != null && m_Player.NextHeal - DateTime.Now > new TimeSpan( 0, 0, 3 ) )
						{
							m_Player.SendMessage( "It's too soon to do this again." );

							return;
						}

						if ( BandageContext.BeginHeal( from, (Mobile)targeted ) != null )
						{
							m_Bandage.Consume();
							//from.ClearHands();
							BandageClearHands( from );

							if ( m_Player != null )
								m_Player.NextHeal = DateTime.Now + TimeSpan.FromSeconds( 2.0 );
						}
					}
					else
					{
						from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
					}
				}
				else
				{
					from.SendLocalizedMessage( 500970 ); // Bandages can not be used on that.
				}
			}
		}
	}

	public class BandageContext
	{
		private Mobile m_Healer;
		private Mobile m_Patient;
		private int m_Slips;
		private Timer m_Timer;

		public Mobile Healer{ get{ return m_Healer; } }
		public Mobile Patient{ get{ return m_Patient; } }
		public int Slips{ get{ return m_Slips; } set{ m_Slips = value; } }
		public Timer Timer{ get{ return m_Timer; } }

		public void Slip()
		{
			m_Healer.SendLocalizedMessage( 500961 ); // Your fingers slip!
			++m_Slips;
		}

		public BandageContext( Mobile healer, Mobile patient, TimeSpan delay )
		{
			m_Healer = healer;
			m_Patient = patient;

			m_Timer = new InternalTimer( this, delay );
			m_Timer.Start();
		}

		public void StopHeal()
		{
			m_Table.Remove( m_Healer );

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private static Hashtable m_Table = new Hashtable();

		public static BandageContext GetContext( Mobile healer )
		{
			return (BandageContext)m_Table[healer];
		}

		public static SkillName GetPrimarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.Veterinary;
			else
				return SkillName.Healing;
		}

		public static SkillName GetSecondarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.AnimalLore;
			else
				return SkillName.Anatomy;
		}

		public void EndHeal()
		{
			StopHeal();

			int healerNumber = -1, patientNumber = -1;
			bool playSound = true;
			bool checkSkills = false;

			SkillName primarySkill = GetPrimarySkill( m_Patient );
			SkillName secondarySkill = GetSecondarySkill( m_Patient );

			BaseCreature petPatient = m_Patient as BaseCreature;

			if ( !m_Healer.Alive )
			{
				healerNumber = 500962; // You were unable to finish your work before you died.
				patientNumber = -1;
				playSound = false;
			}
			else if ( !m_Healer.InRange( m_Patient, Core.AOS ? 2 : 1 ) )
			{
				healerNumber = 500963; // You did not stay close enough to heal your target.
				patientNumber = -1;
				playSound = false;
			}
			
			else if (petPatient != null && petPatient.IsDeadPet) 
			{
				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing - 60.0) / 50.0) - (m_Slips * 0.12);

				if ( (checkSkills = (healing >= 80.0 && anatomy >= 80.0)) && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.Map == null || !m_Patient.Map.CanFit( m_Patient.Location, 16, false, false ) )
					{
						healerNumber = 501042; // Target can not be resurrected at that location.
						patientNumber = 502391; // Thou can not be resurrected there!
					}
					else
					{
						healerNumber = 500965; // You are able to resurrect your patient.
						patientNumber = -1;

						m_Patient.PlaySound( 0x214 );
						m_Patient.FixedEffect( 0x376A, 10, 16 );

						if ( petPatient != null && petPatient.IsDeadPet )
						{
							Mobile master = petPatient.ControlMaster;

							if ( master != null && master.InRange( petPatient, 3 ) )
							{
								healerNumber = 503255; // You are able to resurrect the creature.

								master.CloseGump( typeof( PetResurrectGump ) );
								master.SendGump( new PetResurrectGump( m_Healer, petPatient ) );
							}
							else
							{
								bool found = false;

								ArrayList friends = petPatient.Friends;

								for ( int i = 0; friends != null && i < friends.Count; ++i )
								{
									Mobile friend = (Mobile) friends[i];

									if ( friend.InRange( petPatient, 3 ) )
									{
										healerNumber = 503255; // You are able to resurrect the creature.

										friend.CloseGump( typeof( PetResurrectGump ) );
										friend.SendGump( new PetResurrectGump( m_Healer, petPatient ) );

										found = true;
										break;
									}
								}

								if ( !found )
									healerNumber = 1049670; // The pet's owner must be nearby to attempt resurrection.
							}
						}
						else
						{
							m_Patient.CloseGump( typeof( ResurrectGump ) );
							m_Patient.SendGump( new ResurrectGump( m_Patient, m_Healer ) );
						}
					}
				}
				else
				{
					if ( petPatient != null && petPatient.IsDeadPet )
						healerNumber = 503256; // You fail to resurrect the creature.
					else
						healerNumber = 500966; // You are unable to resurrect your patient.

					patientNumber = -1;
				}
			}

            else if (m_Patient.Poisoned && petPatient != null)
			{
				m_Healer.SendLocalizedMessage( 500969 ); // You finish applying the bandages.

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing - 30.0) / 50.0) - (m_Patient.Poison.Level * 0.1) - (m_Slips * 0.02);

				if ( (checkSkills = (healing >= 60.0 && anatomy >= 60.0)) && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.CurePoison( m_Healer ) )
					{
						healerNumber = (m_Healer == m_Patient) ? -1 : 1010058; // You have cured the target of all poisons.
						patientNumber = 1010059; // You have been cured of all poisons.
					}
					else
					{
						healerNumber = -1;
						patientNumber = -1;
					}
				}
				else
				{
					healerNumber = 1010060; // You have failed to cure your target!
					patientNumber = -1;
				}
			}
			
			else if ( BleedAttack.IsBleeding( m_Patient ) )
			{
				healerNumber = -1;
				patientNumber = 1060167; // The bleeding wounds have healed, you are no longer bleeding!

				BleedAttack.EndBleed( m_Patient, true );
			}
			else if ( MortalStrike.IsWounded( m_Patient ) )
			{
				healerNumber = ( m_Healer == m_Patient ? 1005000 : 1010398 );
				patientNumber = -1;
				playSound = false;
			}
			else if ( m_Patient.Hits == m_Patient.HitsMax )
			{
				healerNumber = 500967; // You heal what little damage your patient had.
				patientNumber = -1;
			}
			else
			{
				checkSkills = true;
				patientNumber = -1;

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing + 10.0) / 30.0) - (m_Slips * 0.22);

				if ( chance > Utility.RandomDouble() )
				{
					healerNumber = 500969; // You finish applying the bandages.

					double min, max;

					if ( Core.AOS )
					{
						min = (anatomy / 8.0) + (healing / 5.0) + 4.0;
						max = (anatomy / 6.0) + (healing / 2.5) + 4.0;
					}
					else
					{
						min = (anatomy / 5.0) + (healing / 5.0) + 3.0;
						max = (anatomy / 5.0) + (healing / 2.0) + 10.0;
					}

					double toHeal = min + (Utility.RandomDouble() * (max - min));

					if ( m_Patient.Body.IsMonster || m_Patient.Body.IsAnimal )
						toHeal += m_Patient.HitsMax / 100;

					if ( Core.AOS )
						toHeal -= toHeal * m_Slips * 0.20; // TODO: Verify algorithm
					else
						toHeal -= m_Slips * 4;

					if ( toHeal < 1 )
					{
						toHeal = 1;
						healerNumber = 500968; // You apply the bandages, but they barely help.
					}

					m_Patient.Heal( (int) toHeal );
				}
				else
				{
					healerNumber = 500968; // You apply the bandages, but they barely help.
					playSound = false;
				}
			}

			if ( healerNumber != -1 )
				m_Healer.SendLocalizedMessage( healerNumber );

			if ( patientNumber != -1 )
				m_Patient.SendLocalizedMessage( patientNumber );

			if ( playSound )
				m_Patient.PlaySound( 0x57 );

			if ( checkSkills )
			{
				m_Healer.CheckSkill( secondarySkill, 0.0, 120.0 );
				m_Healer.CheckSkill( primarySkill, 0.0, 120.0 );
			}
		}

		private class InternalTimer : Timer
		{
			private BandageContext m_Context;

			public InternalTimer( BandageContext context, TimeSpan delay ) : base( delay )
			{
				m_Context = context;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Context.EndHeal();
			}
		}

		public static BandageContext BeginHeal( Mobile healer, Mobile patient )
		{
			bool isDeadPet = ( patient is BaseCreature && ((BaseCreature)patient).IsDeadPet );

			if ( patient is Golem )
			{
				healer.SendLocalizedMessage( 500970 ); // Bandages cannot be used on that.
			}
			else if ( patient is BaseCreature && ((BaseCreature)patient).IsAnimatedDead )
			{
				healer.SendLocalizedMessage( 500951 ); // You cannot heal that.
			}
			else if ( !patient.Poisoned && patient.Hits == patient.HitsMax && !BleedAttack.IsBleeding( patient ) && !isDeadPet )
			{
				healer.SendLocalizedMessage( 500955 ); // That being is not damaged!
			}
			else if ( !patient.Alive && (patient.Map == null || !patient.Map.CanFit( patient.Location, 16, false, false )) )
			{
				healer.SendLocalizedMessage( 501042 ); // Target cannot be resurrected at that location.
			}
			else if ( healer.CanBeBeneficial( patient, true, true ) )
			{
				healer.DoBeneficial( patient );

				bool onSelf = ( healer == patient );
				int dex = healer.Dex;

				double seconds;
				double resDelay = ( patient.Alive ? 0.0 : 5.0 );

				if ( onSelf )
				{
                        seconds = 14 - ( ( healer.Skills.Healing.Value / 10)) + resDelay;

                        if (seconds < 1)
                            seconds = 1;
				}
				else
				{
					if ( Core.AOS && GetPrimarySkill( patient ) == SkillName.Veterinary )
					{
						//if ( dex >= 40 )
							seconds = 3.0;
						//else
						//	seconds = 3.0;
					}
					else
					{
                        seconds = 10 - ((healer.Skills.Healing.Value / 10)) + resDelay;

                        if (seconds < 1)
                            seconds = 1;
					}
				}

				BandageContext context = GetContext( healer );

				if ( context != null )
					context.StopHeal();

				context = new BandageContext( healer, patient, TimeSpan.FromSeconds( seconds ) );

                if ( DateTime.Now + TimeSpan.FromSeconds( seconds) > healer.NextCombatTime)
                    healer.NextCombatTime = (DateTime.Now + TimeSpan.FromSeconds(seconds));

				m_Table[healer] = context;

				if ( !onSelf )
					patient.SendLocalizedMessage( 1008078, false, healer.Name ); //  : Attempting to heal you.

				healer.SendLocalizedMessage( 500956 ); // You begin applying the bandages.
				return context;
			}

			return null;
		}
	}
}