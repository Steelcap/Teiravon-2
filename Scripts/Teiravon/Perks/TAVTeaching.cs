using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.PartySystem;
using Teiravon;

namespace Teiravon.Teaching
{
	public class TeachingCommands
	{
		Timer m_LearnTimer;
		
		public static void Initialize()
		{
			Commands.Register( "Teach", AccessLevel.Player, new CommandEventHandler( Teach_OnCommand ) );
			Commands.Register( "StartTeach", AccessLevel.Player, new CommandEventHandler( StartTeach_OnCommand ) );
		}
		
		[Usage( "Teach" )]
		[Description( "Activates Teaching System" )]
		private static void Teach_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
			Party p = Server.Engines.PartySystem.Party.Get(m_Player);

			if (p != null && m_Player.TeachSkill == 999)
				m_Player.SendMessage("You must quit your current party in order to teach!");
			else if (m_Player.TeachingSkill == 0)
				m_Player.SendMessage("You do not have enough teaching skill to teach!");
			else if (DateTime.Now < m_Player.NextTeach)
				m_Player.SendMessage("It is too soon teach again!");
			else if (p == null && m_Player.TeachSkill == 999)
				m_Player.SendGump(new TeachSkillGump(m_Player, m_Player.TeachingSkill));
			else if (p != null && m_Player.TeachSkill < 999)
				m_Player.Target = new AddStudentTarget(m_Player);
			else if (p == null && m_Player.TeachSkill < 999)
				m_Player.TeachSkill = 999;
		
		}
		[Usage( "StartTeach" )]
		[Description( "Begins Teaching" )]
		private static void StartTeach_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
			Party p = Server.Engines.PartySystem.Party.Get(m_Player);
			TeiravonMobile tm_member;
			if (p == null)
				m_Player.SendMessage("You must pick some students first using ]teach");
			else if (m_Player.TeachSkill == 999)
				m_Player.SendMessage("You must pick the skill to teach first by using ]teach");
			else if (m_Player.NextTeach > DateTime.Now)
				m_Player.SendMessage("It is too soon to teach again!");
			else if (m_Player != p.Leader)
				m_Player.SendMessage("Only the teacher can begin the teaching session!");
			else
			{
				m_Player.NextTeach = DateTime.Now + TimeSpan.FromHours(24.0);
				m_Player.SendMessage("The teaching session has begun");
				for ( int i = 0; i < p.Members.Count; ++i )
				{
					Mobile f = ((PartyMemberInfo)p.Members[i]).Mobile;
					
					if (f is TeiravonMobile)
					{
						tm_member = (TeiravonMobile)f;
						if (tm_member != m_Player)
							tm_member.NextLearn = DateTime.Now + TimeSpan.FromHours(24.0);
						tm_member.SendMessage("The teaching session has begun");
					}
				}

				Timer m_LearnTimer = new LearnTimer(m_Player, 1, m_Player.TeachingSkill * 2, DateTime.Now + TimeSpan.FromMinutes( 7.0 ));
				m_LearnTimer.Start();
			}
		}
		
		private class LearnTimer : Timer
		{
			private TeiravonMobile m_teacher;
			private int MaxCount;
			private int RunCount;
			private int skill;
			private double scalar;

			public LearnTimer( TeiravonMobile from, int runcount, int maxcount, DateTime end ) : base( end - DateTime.Now )
			{
				m_teacher = from;
				Priority = TimerPriority.FiveSeconds;
				RunCount = runcount;
				MaxCount = maxcount;
				skill = m_teacher.TeachSkill;
				if (m_teacher.TeachingSkill == 4)
					scalar = 0.85;
				else
					scalar = 0.8;
			}

			protected override void OnTick()
			{
				int partycount;
				int skillgain;
				TeiravonMobile m_student;
				double studentskill;
				double studentcap;
				double teachermax;
				
				Party p = Party.Get(m_teacher);
				if (p != null)
				{
					partycount = p.Members.Count - 1;

//				m_teacher.SendMessage("{0} party count", partycount);
				
					if (partycount == 1)
						skillgain = 3;
					else if (partycount > 1 && partycount < 5)
						skillgain = 2;
					else
						skillgain = 1;
				
//				m_teacher.SendMessage("{0} party count    {1} skill gain", partycount, skillgain);

					for ( int i = 0; i < p.Members.Count; ++i )
					{
						Mobile f = ((PartyMemberInfo)p.Members[i]).Mobile;
					
						if (f is TeiravonMobile)
						{
							m_student = (TeiravonMobile)f;
							if (m_student != m_teacher  && m_student.Alive && Utility.InUpdateRange( m_teacher, m_student ))
							{
								studentskill = m_student.Skills[skill].Base;
								studentcap = m_student.Skills[skill].Cap;
								teachermax = m_teacher.Skills[skill].Base * scalar;
								
								if (studentskill + skillgain >= teachermax)
								{
									if (studentskill + skillgain >= studentcap)
									{
										if (teachermax > studentcap)
										{
											m_student.Skills[skill].Base = studentcap;
											m_student.SendMessage("You learn as much as you are able to!");
										}
										else
										{
											m_student.Skills[skill].Base = teachermax;
											m_student.SendMessage("You learn as much as your teacher is capable of teaching!");
										}
									}
								}
								else
								{
									if (studentskill + skillgain > studentcap)
									{
										m_student.Skills[skill].Base = studentcap;
										m_student.SendMessage("You learn as much as you are capable of!");
									}
									else
									{
										m_student.Skills[skill].BaseFixedPoint += skillgain;
										m_student.SendMessage("You learn something!");
//									m_teacher.SendMessage("{0} party count    {1} skill gain   {2}skill", partycount, skillgain, m_student.Skills[skill].Base);
									
									}
								}
							}
							if (RunCount == MaxCount)
								f.SendMessage("The teaching session is over.");
						}
					}
				
					if ( RunCount == MaxCount)
					{
						m_teacher.SendMessage("The teaching session is over.");	
						m_teacher.TeachSkill = 999;
					}
				
					if ( RunCount < MaxCount)
					{
						RunCount++;
						Timer m_LearnTimer = new LearnTimer( m_teacher, RunCount, MaxCount, DateTime.Now + TimeSpan.FromMinutes( 7.0 ) );
						m_LearnTimer.Start();
					}
				}
			}
		}
		
	}

	public class TeachSkillGump : Gump
	{
		TeiravonMobile tm;
		int TeachLevel;
		
		public enum Buttons
		{
			NullButton,
			Cooking,
			Fishing,
			Healing,
			Herding,
			Lockpicking,
			Lumberjacking,
			Magery,
			Mining,
			Musicianship,
			Necromancy,
			RemoveTrap,
			ResistSpells,
			Snooping,
			Stealing,
			Stealth,
			Veterinary,
			Archery,
			Fencing,
			Macing,
			Parry,
			Swords,
			Tactics,
			Wrestling,
			AnimalTaming,
			Begging,
			Camping,
			Cartography,
			DetectHidden,
			Hiding,
			Poisoning,
			SpiritSpeak,
			Tracking,
			Anatomy,
			AnimalLore,
			ArmsLore,
			EvalInt,
			Forensics,
			ItemID,
			TasteID,
			Alchemy,
			Blacksmith,
			Fletching,
			Carpentry,
			Tailoring,
			Tinkering,
			Inscription,
		}

		public TeachSkillGump(TeiravonMobile TMPlayer, int tskill): base( 0, 0 )
		{
			tm = TMPlayer;
			TeachLevel = tskill;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 67, 640, 481, 9400);

			this.AddLabel(275, 80, 45, @"Skill Teaching");
			this.AddLabel(25, 100, 0, @"Miscellaneous");
			this.AddLabel(205, 100, 0, @"Combat");
			this.AddLabel(356, 100, 0, @"Action");
			this.AddLabel(490, 100, 0, @"Lore/Knowledge");
			
			
			//Misc
			int laby = 98;
			int buty = 100;
			
			if(tm.Skills.Inscribe.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Inscription");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Inscription, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tinkering.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Tinkering");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Tinkering, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tailoring.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Tailoring");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Tailoring, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Carpentry.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Carpentry");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Carpentry, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fletching.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Bowcraft/Fletchin");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Fletching, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Blacksmith.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Blacksmith");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Blacksmith, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Alchemy.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Alchemy");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Alchemy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Cooking.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Cooking");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Cooking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fishing.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Fishing");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Fishing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Healing.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Healing");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Healing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Herding.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Herding");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Herding, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Lockpicking.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Lockpicking");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Lockpicking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Lumberjacking.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Lumberjacking");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Lumberjacking, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Magery.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Magery");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Magery, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Mining.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Mining");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Mining, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Musicianship.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Musicianship");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Musicianship, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Necromancy.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Necromancy");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Necromancy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.RemoveTrap.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Remove Trap");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.RemoveTrap, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.MagicResist.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Resist Spells");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.ResistSpells, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Snooping.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Snooping");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Snooping, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Stealing.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Stealing");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Stealing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Stealth.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Stealth");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Stealth, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Veterinary.Base > 60)
			{
				this.AddLabel(40, laby += 25, 772, @"Veterinary");
				this.AddButton(15, buty += 25, 216, 216, (int)Buttons.Veterinary, GumpButtonType.Reply, 0);
			}
			
			//Combat
			laby = 98;
			buty = 100;
			
			if(tm.Skills.Archery.Base > 60)
			{
				this.AddLabel(200, laby += 25, 772, @"Archery");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Archery, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Fencing.Base > 60)
			{
				this.AddLabel(200, laby += 25, 772, @"Fencing");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Fencing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Macing.Base > 60)
			{
				this.AddLabel(200, laby += 25, 772, @"Macing");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Macing, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Parry.Base > 60)
			{
				this.AddLabel(200, laby += 25, 772, @"Parry");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Parry, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Swords.Base > 60)
			{
				this.AddLabel(200, laby += 25, 772, @"Swords");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Swords, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tactics.Base > 60)
			{
				this.AddLabel(200, laby += 25, 772, @"Tactics");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Tactics, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Wrestling.Base > 60)
			{
				this.AddLabel(200, laby += 25, 772, @"Wrestling");
				this.AddButton(175, buty += 25, 216, 216, (int)Buttons.Wrestling, GumpButtonType.Reply, 0);
			}

			//Actions
			laby = 98;
			buty = 100;
			
			if(tm.Skills.AnimalTaming.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Animal Taming");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.AnimalTaming, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Begging.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Begging");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Begging, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Camping.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Camping");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Camping, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Cartography.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Cartography");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Cartography, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.DetectHidden.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Detect Hidden");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.DetectHidden, GumpButtonType.Reply, 0);
			}/*
			if(tm.Skills.Hiding.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Hiding");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Hiding, GumpButtonType.Reply, 0);
			}*/
			if(tm.Skills.Poisoning.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Poisoning");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Poisoning, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.SpiritSpeak.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Spirit Speak");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.SpiritSpeak, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Tracking.Base > 60)
			{
				this.AddLabel(360, laby += 25, 772, @"Tracking");
				this.AddButton(335, buty += 25, 216, 216, (int)Buttons.Tracking, GumpButtonType.Reply, 0);
			}

			//Lore
			laby = 98;
			buty = 100;

			if(tm.Skills.Anatomy.Base > 60)
			{
				this.AddLabel(520, laby += 25, 772, @"Anatomy");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.Anatomy, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.AnimalLore.Base > 60)
			{
				this.AddLabel(520, laby += 25, 772, @"Animal Lore");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.AnimalLore, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.ArmsLore.Base > 60)
			{
				this.AddLabel(520, laby += 25, 772, @"ArmsLore");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.ArmsLore, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.EvalInt.Base > 60)
			{
				this.AddLabel(520, laby += 25, 772, @"Eval Int");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.EvalInt, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.Forensics.Base > 60)
			{
				this.AddLabel(520, laby += 25, 772, @"Forensic Eval");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.Forensics, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.ItemID.Base > 60)
			{
				this.AddLabel(520, laby += 25, 772, @"Item ID");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.ItemID, GumpButtonType.Reply, 0);
			}
			if(tm.Skills.TasteID.Base > 60)
			{
				this.AddLabel(520, laby += 25, 772, @"Taste ID");
				this.AddButton(492, buty += 25, 216, 216, (int)Buttons.TasteID, GumpButtonType.Reply, 0);
			}

		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			switch (info.ButtonID)
			{
				case (int)Buttons.Cooking:
					tm.TeachSkill = 13;
					break;
				case (int)Buttons.Fishing:
					tm.TeachSkill = 18;
					break;
				case (int)Buttons.Healing:
					tm.TeachSkill = 17;
						break;
				case (int)Buttons.Herding:
					tm.TeachSkill = 20;
					break;
				case (int)Buttons.Lockpicking:
					tm.TeachSkill = 24;
					break;
				case (int)Buttons.Lumberjacking:
					tm.TeachSkill = 44;
					break;
				case (int)Buttons.Magery:
					tm.TeachSkill = 25;
					break;
				case (int)Buttons.Mining:
					tm.TeachSkill = 45;
					break;
				case (int)Buttons.Musicianship:
					tm.TeachSkill = 29;
					break;
				case (int)Buttons.Necromancy:
					tm.TeachSkill = 49;
					break;
				case (int)Buttons.RemoveTrap:
					tm.TeachSkill = 48;
					break;
				case (int)Buttons.ResistSpells:
					tm.TeachSkill = 26;
					break;
				case (int)Buttons.Snooping:
					tm.TeachSkill = 28;
					break;
				case (int)Buttons.Stealing:
					tm.TeachSkill = 33;
					break;
				case (int)Buttons.Stealth:
					tm.TeachSkill = 47;
					break;
				case (int)Buttons.Veterinary:
					tm.TeachSkill = 39;
					break;
				case (int)Buttons.Archery:
					tm.TeachSkill = 31;
					break;
				case (int)Buttons.Fencing:
					tm.TeachSkill = 42;
					break;
				case (int)Buttons.Macing:
					tm.TeachSkill = 41;
					break;
				case (int)Buttons.Parry:
					tm.TeachSkill = 5;
					break;
				case (int)Buttons.Swords:
					tm.TeachSkill = 40;
					break;
				case (int)Buttons.Tactics:
					tm.TeachSkill = 27;
					break;
				case (int)Buttons.Wrestling:
					tm.TeachSkill = 43;
					break;
				case (int)Buttons.AnimalTaming:
					tm.TeachSkill = 35;
					break;
				case (int)Buttons.Begging:
					tm.TeachSkill = 6;
					break;
				case (int)Buttons.Camping:
					tm.TeachSkill = 10;
					break;
				case (int)Buttons.Cartography:
					tm.TeachSkill = 12;
					break;
				case (int)Buttons.DetectHidden:
					tm.TeachSkill = 14;
					break;
				case (int)Buttons.Hiding:
					tm.TeachSkill = 21;
					break;
				case (int)Buttons.Poisoning:
					tm.TeachSkill = 30;
					break;
				case (int)Buttons.SpiritSpeak:
					tm.TeachSkill = 32;
					break;
				case (int)Buttons.Tracking:
					tm.TeachSkill = 38;
					break;
				case (int)Buttons.Anatomy:
					tm.TeachSkill = 1;
					break;
				case (int)Buttons.AnimalLore:
					tm.TeachSkill = 2;
					break;
				case (int)Buttons.ArmsLore:
					tm.TeachSkill = 4;
					break;
				case (int)Buttons.EvalInt:
					tm.TeachSkill = 16;
					break;
				case (int)Buttons.Forensics:
					tm.TeachSkill = 19;
					break;
				case (int)Buttons.ItemID:
					tm.TeachSkill = 3;
					break;
				case (int)Buttons.TasteID:
					tm.TeachSkill = 36;
					break;
				case (int)Buttons.Alchemy:
					tm.TeachSkill = 0;
					break;
				case (int)Buttons.Blacksmith:
					tm.TeachSkill = 7;
					break;
				case (int)Buttons.Fletching:
					tm.TeachSkill = 8;
					break;
				case (int)Buttons.Carpentry:
					tm.TeachSkill = 11;
					break;
				case (int)Buttons.Tailoring:
					tm.TeachSkill = 34;
					break;
				case (int)Buttons.Tinkering:
					tm.TeachSkill = 37;
					break;
				case (int)Buttons.Inscription:
					tm.TeachSkill = 23;
					break;
				default:
					tm.TeachSkill = 999;
					break;
			}
			
			if (tm.TeachSkill < 999)
				tm.Target = new AddStudentTarget(tm);
		}
	}
	
	public class AddStudentTarget : Target
	{
		public AddStudentTarget( Mobile from ) : base( 8, false, TargetFlags.None )
		{
			from.SendMessage( "Who do you want to teach?" );
		}

		protected override void OnTarget( Mobile from, object o )
		{
			if ( o is Mobile )
			{
				Mobile m = (Mobile)o;
				Party p = Party.Get( from );
				Party mp = Party.Get( m );

				if ( from == m )
					from.SendMessage( "You cannot teach yourself!" );
				else if ( p != null && p.Leader != from )
					from.SendMessage( "Only the teacher can add students" );
				else if ( m.Party is Mobile )
					return;
				else if ( p != null && (p.Members.Count + p.Candidates.Count) >= Party.Capacity )
					from.SendLocalizedMessage( 1008095 ); // You may only have 10 in your party (this includes candidates).
				else if ( !m.Player && m.Body.IsHuman )
					m.SayTo( from, 1005443 ); // Nay, I would rather stay here and watch a nail rust.
				else if ( !m.Player )
					from.SendLocalizedMessage( 1005444 ); // The creature ignores your offer.
				else if ( mp != null && mp == p )
					from.SendMessage( "This person is already a student!" );
				else if ( mp != null )
					from.SendLocalizedMessage( 1005441 ); // This person is already in a party!
				else
				{
					if (m is TeiravonMobile)
					{
						TeiravonMobile tm = (TeiravonMobile)m;
						if (tm.NextLearn < DateTime.Now)
							Party.Invite( from, m );
						else
							tm.SendMessage("It is too soon for that person to learn again!");
					}
				}
			}
			else
			{
				from.SendLocalizedMessage( 1005442 ); // You may only add living things to your party!
			}
		}
	}


}
