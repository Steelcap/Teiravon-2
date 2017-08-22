using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Teiravon;
using Server.Targeting;
using Server.ContextMenus;
using Server.Engines.XmlSpawner2;

namespace Server.Mobiles
{
	public class AnimalTrainer : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public AnimalTrainer() : base( "the animal trainer" )
		{
			SetSkill( SkillName.AnimalLore, 64.0, 100.0 );
			SetSkill( SkillName.AnimalTaming, 90.0, 100.0 );
			SetSkill( SkillName.Veterinary, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAnimalTrainer() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( Utility.RandomBool() ? (Item)new QuarterStaff() : (Item)new ShepherdsCrook() );
		}

		private class StableEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public StableEntry( AnimalTrainer trainer, Mobile from ) : base( 6126, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.BeginStable( m_From );
			}
		}

		private class ClaimListGump : Gump
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;
			private ArrayList m_List;

			public ClaimListGump( AnimalTrainer trainer, Mobile from, ArrayList list ) : base( 50, 50 )
			{
				m_Trainer = trainer;
				m_From = from;
				m_List = list;

				from.CloseGump( typeof( ClaimListGump ) );

				AddPage( 0 );

				AddBackground( 0, 0, 325, 50 + (list.Count * 20), 9250 );
				AddAlphaRegion( 5, 5, 315, 40 + (list.Count * 20) );

				AddHtml( 15, 15, 275, 20, "<BASEFONT COLOR=#FFFFFF>Select a pet to retrieve from the stables:</BASEFONT>", false, false );

				for ( int i = 0; i < list.Count; ++i )
				{
					BaseCreature pet = list[i] as BaseCreature;

					if ( pet == null || pet.Deleted )
						continue;

					AddButton( 15, 39 + (i * 20), 10006, 10006, i + 1, GumpButtonType.Reply, 0 );
					AddHtml( 32, 35 + (i * 20), 275, 18, String.Format( "<BASEFONT COLOR=#C0C0EE>{0}</BASEFONT>", pet.Name ), false, false );
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				int index = info.ButtonID - 1;

				if ( index >= 0 && index < m_List.Count )
					m_Trainer.EndClaimList( m_From, m_List[index] as BaseCreature );
			}
		}

		private class ClaimAllEntry : ContextMenuEntry
		{
			private AnimalTrainer m_Trainer;
			private Mobile m_From;

			public ClaimAllEntry( AnimalTrainer trainer, Mobile from ) : base( 6127, 12 )
			{
				m_Trainer = trainer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Trainer.Claim( m_From );
			}
		}

		public override void AddCustomContextEntries( Mobile from, ArrayList list )
		{
			if ( from.Alive )
			{
				list.Add( new StableEntry( this, from ) );

				if ( from.Stabled.Count > 0 )
					list.Add( new ClaimAllEntry( this, from ) );     
			}

			base.AddCustomContextEntries( from, list );
		}

		public static int GetMaxStabled( Mobile from )
		{
			double taming = from.Skills[SkillName.AnimalTaming].Value;
			double anlore = from.Skills[SkillName.AnimalLore].Value;
			double vetern = from.Skills[SkillName.Veterinary].Value;
			double sklsum = taming + anlore + vetern;

			int max;

			if ( sklsum >= 240.0 )
				max = 5;
			else if ( sklsum >= 200.0 )
				max = 4;
			else if ( sklsum >= 160.0 )
				max = 3;
			else
				max = 2;

			if ( taming >= 100.0 )
				max += (int)((taming - 90.0) / 10);

			if ( anlore >= 100.0 )
				max += (int)((anlore - 90.0) / 10);

			if ( vetern >= 100.0 )
				max += (int)((vetern - 90.0) / 10);

            if (from is TeiravonMobile && ((TeiravonMobile)from).HasFeat(TeiravonMobile.Feats.AnimalHusbandry))
                max += ((TeiravonMobile)from).PlayerLevel / 3;
			return max;
		}

        private bool CanBreed(Mobile from)
        {
            bool gender;
            bool can = false;
            for (int i = 0; i < from.Stabled.Count; i++)
            {
                BaseCreature c = ((BaseCreature)from.Stabled[i]);
                gender = ((BaseCreature)from.Stabled[i]).Female;
                for (int j = 0; j < from.Stabled.Count; j++)
                {
                    BaseCreature x = ((BaseCreature)from.Stabled[j]);
                    if (x.Equals(c) && (x.Female != gender))
                    {
                        can = true;
                    }
                }
            }

            return can;
        }

		private class StableTarget : Target
		{
			private AnimalTrainer m_Trainer;

			public StableTarget( AnimalTrainer trainer ) : base( 1, false, TargetFlags.None )
			{
				m_Trainer = trainer;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseCreature )
				{
					BaseCreature m_Creature = (BaseCreature)targeted;
                    if (!m_Creature.InLOS(from))
                        from.SendMessage("You cannot see your target.");

					if ( m_Creature.IsUndeadNPC() || m_Creature.IsDemonNPC() )
						m_Trainer.SayTo( from, "I won't stable that!" );
					else
						m_Trainer.EndStable( from, (BaseCreature)targeted );
				}
				else if ( targeted == from )
					m_Trainer.SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn.
				else
					m_Trainer.SayTo( from, 1048053 ); // You can't stable that!
			}
		}

		public void BeginClaimList( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			ArrayList list = new ArrayList();

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

                if (pet.BardEndTime < DateTime.Now)
                {
                    list.Add(pet);
                }
			}

			if ( list.Count > 0 )
				from.SendGump( new ClaimListGump( this, from, list ) );
			else
				SayTo( from, 502671 ); // But I have no animals stabled with me at the moment!
		}

		public void EndClaimList( Mobile from, BaseCreature pet )
		{
			if ( pet == null || pet.Deleted || from.Map != this.Map || !from.InRange( this, 14 ) || !from.Stabled.Contains( pet ) || !from.CheckAlive() )
				return;

                if (pet.BardEndTime >= DateTime.Now)
                {
                    SayTo(from, "{0} must remain in the stables awhile longer.", pet.Name);
                    return;
                }
            
			if ( (from.Followers + pet.ControlSlots) <= from.FollowersMax )
			{
				pet.SetControlMaster( from );

				if ( pet.Summoned )
					pet.SummonMaster = from;

				pet.ControlTarget = from;
				pet.ControlOrder = OrderType.Follow;

				pet.MoveToWorld( from.Location, from.Map );

				pet.IsStabled = false;
				from.Stabled.Remove( pet );

				SayTo( from, 1042559 ); // Here you go... and good day to you!
			}
			else
			{
				SayTo( from, 1049612, pet.Name ); // ~1_NAME~ remained in the stables because you have too many followers.
			}
		}

		public void BeginStable( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( from.Stabled.Count >= GetMaxStabled( from ) )
			{
				SayTo( from, 1042565 ); // You have too many pets in the stables!
			}
			else
			{
				SayTo( from, 1042558 ); /* I charge 30 gold per pet for a real week's stable time.
										 * I will withdraw it from thy bank account.
										 * Which animal wouldst thou like to stable here?
										 */

				from.Target = new StableTarget( this );
			}
		}

		public void EndStable( Mobile from, BaseCreature pet )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			if ( !pet.Controled || pet.ControlMaster != from )
			{
				SayTo( from, 1042562 ); // You do not own that pet!
			}
			else if ( pet.IsDeadPet )
			{
				SayTo( from, 1049668 ); // Living pets only, please.
			}
			else if ( pet.Summoned )
			{
				SayTo( from, 502673 ); // I can not stable summoned creatures.
			}
			else if ( pet.Body.IsHuman )
			{
				SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn.
			}
			else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) )
			{
				SayTo( from, 1042563 ); // You need to unload your pet.
			}
			else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map )
			{
				SayTo( from, 1042564 ); // I'm sorry.  Your pet seems to be busy.
			}
			else if ( from.Stabled.Count >= GetMaxStabled( from ) )
			{
				SayTo( from, 1042565 ); // You have too many pets in the stables!
			}
			else
			{
				Container bank = from.BankBox;

				if ( bank != null && bank.ConsumeTotal( typeof( Gold ), 30 ) )
				{
					pet.ControlTarget = null;
					pet.ControlOrder = OrderType.Stay;
					pet.Internalize();

					pet.SetControlMaster( null );
					pet.SummonMaster = null;

					pet.IsStabled = true;
					from.Stabled.Add( pet );

					SayTo( from, 502679 ); // Very well, thy pet is stabled. Thou mayst recover it by saying 'claim' to me. In one real world week, I shall sell it off if it is not claimed!
				}
				else
				{
					SayTo( from, 502677 ); // But thou hast not the funds in thy bank account!
				}
			}
		}

		public void Claim( Mobile from )
		{
			if ( Deleted || !from.CheckAlive() )
				return;

			bool claimed = false;
			int stabled = 0;

			for ( int i = 0; i < from.Stabled.Count; ++i )
			{
				BaseCreature pet = from.Stabled[i] as BaseCreature;



				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;
					from.Stabled.RemoveAt( i );
					--i;
					continue;
				}

				++stabled;

                if (pet.BardEndTime >= DateTime.Now)
                {
                    SayTo(from, "I'm afraid {0} must remain in the stables for awhile longer.", pet.Name);
                    continue;
                }

                if ((from.Followers + pet.ControlSlots) <= from.FollowersMax)
                {
                    pet.SetControlMaster(from);

                    if (pet.Summoned)
                        pet.SummonMaster = from;

                    pet.ControlTarget = from;
                    pet.ControlOrder = OrderType.Follow;

                    pet.MoveToWorld(from.Location, from.Map);

                    pet.IsStabled = false;
                    from.Stabled.RemoveAt(i);
                    --i;

                    claimed = true;
                }
                else
                {
                    SayTo(from, 1049612, pet.Name); // ~1_NAME~ remained in the stables because you have too many followers.
                }
			}

            if (claimed)
                SayTo(from, 1042559); // Here you go... and good day to you!
            else if (stabled > 0 && !claimed)
                SayTo(from, "I'm afraid they must remain in the stable for awhile longer.");
            else if (stabled == 0)
                SayTo(from, 502671); // But I have no animals stabled with me at the moment!
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			return true;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && e.HasKeyword( 0x0008 ) )
			{
				e.Handled = true;
				BeginStable( e.Mobile );
			}
			else if ( !e.Handled && e.HasKeyword( 0x0009 ) )
			{
				e.Handled = true;

				if ( !Insensitive.Equals( e.Speech, "claim" ) )
					BeginClaimList( e.Mobile );
				else
					Claim( e.Mobile );
			}
            else if (e.Speech.ToLower().IndexOf("breed") > -1 )
            {
                SayTo(e.Mobile, false, "Which animals do you wish to breed?");
                e.Mobile.Target = new BreedTarget(this);
                e.Mobile.SendMessage("Please target both animals you wish to breed.");
            }
            else
            {
                base.OnSpeech(e);
            }
		}

        private class BreedTarget : Target
        {
            private AnimalTrainer m_Trainer;

            public BreedTarget(AnimalTrainer trainer)
                : base(5, false, TargetFlags.None)
            {
                m_Trainer = trainer;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseCreature)
                {
                    BaseCreature m_Creature = (BaseCreature)targeted;
                    if (!m_Creature.Controled || m_Creature.ControlMaster != from)
                    {
                        m_Trainer.SayTo(from, false, "You've no claim to that animal.");
                    }
                    else if (!m_Creature.Alive || m_Creature == null)
                    {
                        m_Trainer.SayTo(from, false, "They don't look up to the task.");
                    }
                    else
                    {
                        m_Trainer.SayTo(from, false, "and the other?");
                        from.SendMessage("Please target the second animal you wish to breed.");
                        from.Target = new SecondTarget(m_Trainer, m_Creature); 
                    }
                }
                else if (targeted == from)
                    m_Trainer.SayTo(from, 502672); // HA HA HA! Sorry, I am not an inn.
                else
                    m_Trainer.SayTo(from, false, "I'm afraid I wouldn't know anything about that."); // You can't stable that!
            }
        }

        private class SecondTarget : Target
        {
            private AnimalTrainer m_Trainer;
            private BaseCreature m_First;

            public SecondTarget(AnimalTrainer trainer, BaseCreature first)
                : base(5, false, TargetFlags.None)
            {
                m_Trainer = trainer;
                m_First = first;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseCreature)
                {
                    BaseCreature m_Creature = (BaseCreature)targeted;
                    if (!m_Creature.Controled || m_Creature.ControlMaster != from)
                    {
                        m_Trainer.SayTo(from, false, "You've no claim to that animal.");
                    }
                    else if (!m_Creature.Alive || m_Creature == null)
                    {
                        m_Trainer.SayTo(from, false, "They don't look up to the task.");
                    }
                    else if (m_Creature.Female == m_First.Female || m_Creature.GetType() != m_First.GetType())
                    {
                        m_Trainer.SayTo(from, false, "I'm afraid that's biologically impossible.");
                        // DEBUG TEXT
                        //from.SendMessage("{0},{1}",m_First.GetType().ToString(), m_Creature.GetType().ToString());
                    }
                    else if (from.Stabled.Count + 3 >= GetMaxStabled(from))
                    {
                        m_Trainer.SayTo(from, false, "I'm afraid there's no room in the stable to breed these animals.");
                    }
                    else
                    {
                        Type type = m_First.GetType();
                        if (type != null)
                        {
                            try
                            {
                                object o = Activator.CreateInstance(type);

                                if (o is BaseCreature)
                                {
                                    BaseCreature child = (BaseCreature)o;

                                    m_First.ControlTarget = null;
                                    m_First.ControlOrder = OrderType.Stay;

                                    m_First.SetControlMaster(null);
                                    m_First.SummonMaster = null;

                                    m_First.IsStabled = true;

                                    m_Creature.ControlTarget = null;
                                    m_Creature.ControlOrder = OrderType.Stay;

                                    m_Creature.SetControlMaster(null);
                                    m_Creature.SummonMaster = null;

                                    m_Creature.IsStabled = true;

                                    child.IsStabled = true;
                                    
                                    from.Stabled.Add(child);
                                    from.Stabled.Add(m_First);
                                    from.Stabled.Add(m_Creature);
                                    child.BardEndTime = DateTime.Now.AddDays(1);
                                    m_First.BardEndTime = DateTime.Now.AddDays(1);
                                    m_Creature.BardEndTime = DateTime.Now.AddDays(1);
                                    child.Internalize();
                                    m_First.Internalize();
                                    m_Creature.Internalize();
                                    TAVUtilities.Reproduce(m_Creature, m_First, child);
                                    from.SendMessage("The animals have been stabled, neither the new born nor the parents may be removed from the stable for 24 hours.");
                                    m_Trainer.SayTo(from, false, "Very well, the animals will be stored in the stable overnight.");
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else if (targeted == from)
                    m_Trainer.SayTo(from, 502672); // HA HA HA! Sorry, I am not an inn.
                else
                    m_Trainer.SayTo(from, false, "I'm afraid I wouldn't know anything about that."); // You can't stable that!
            }
        }

		public AnimalTrainer( Serial serial ) : base( serial )
		{
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
	}
}