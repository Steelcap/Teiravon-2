using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Teiravon;
using Server.Prompts;
using Server.Gumps;
using Server.Misc;

namespace Teiravon.Quests
{
	public enum ClassGroup
	{
		None,
		Fighter,
		Cleric,
		Mage,
		Ranger,
		Druid,
		Barbarian,
		Rogue,
		Crafter
	}
	
	public class TalkEntry : ContextMenuEntry
	{
		private TAVQuester m_Quester;

		public TalkEntry( TAVQuester quester ) : base( quester.TalkNumber )
		{
			m_Quester = quester;
		}

		public override void OnClick()
		{
			Mobile from = Owner.From;

			if ( from.CheckAlive() && from is PlayerMobile && m_Quester.CanTalkTo( (PlayerMobile)from ) )
				m_Quester.OnTalk( (PlayerMobile)from, true );
		}
	}

	[NoSortAttribute]
	public class TAVQuester : BaseVendor
	{
		protected ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }
		
		public override bool IsActiveVendor{ get{ return false; } }
		public override bool IsInvulnerable{ get{ return true; } }
		public override bool ClickTitle{ get { return false; } }
		public override bool CanTeach{ get{ return false; } }
		
		public virtual int TalkNumber{ get{ return 6146; } } // Talk

		private bool m_CanRepeat;
		private Hashtable CompletedList = new Hashtable();
		private string[] m_QuestText = new string[5];
        private string[] m_QuestTextB = new string[5];
        private string[] m_QuestTextC = new string[5];
        private Type[] m_QuestItem = new Type[5];
        private string[] m_QuestItemKey = new string[5];
		private int[] m_QuestAmt = new int[5];
		private string m_QuestDescription;
        private string m_QuestDescriptionB;
        private string m_QuestDescriptionC;
		private string m_QuestEnd;
        private string m_QuestEndB;
        private string m_QuestEndC;
        private int m_GoldAmt = 0;
		private int m_QuestExp = 0;
		private TeiravonMobile.Class m_class = TeiravonMobile.Class.None;
		private ClassGroup m_classgroup = ClassGroup.None;
		private int m_level = 1;
		private TeiravonMobile.Race m_race = TeiravonMobile.Race.None;

		[CommandProperty( AccessLevel.GameMaster )]
		public TeiravonMobile.Class AllowedClass
		{
			get{ return m_class; }
			set{ m_class = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public ClassGroup AllowedGroup
		{
			get{ return m_classgroup; }
			set{ m_classgroup = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int MinLevel
		{
			get{ return m_level; }
			set{ m_level = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public TeiravonMobile.Race AllowedRace
		{
			get{ return m_race; }
			set{ m_race = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanRepeat
		{
			get{ return m_CanRepeat; }
			set{ m_CanRepeat = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int GoldAmt
		{
			get{ return m_GoldAmt; }
			set{ m_GoldAmt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int QuestExp
		{
			get{ return m_QuestExp; }
			set{ m_QuestExp = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string QuestDescription
		{
			get{ return m_QuestDescription; }
			set{ m_QuestDescription = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestDescriptionB
        {
            get { return m_QuestDescriptionB; }
            set { m_QuestDescriptionB = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestDescriptionC
        {
            get { return m_QuestDescriptionC; }
            set { m_QuestDescriptionC = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public string QuestEnd
		{
			get{ return m_QuestEnd; }
			set{ m_QuestEnd = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestEndB
        {
            get { return m_QuestEndB; }
            set { m_QuestEndB = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestEndC
        {
            get { return m_QuestEndC; }
            set { m_QuestEndC = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public string QuestText1
		{
			get{ return m_QuestText[0]; }
			set{ m_QuestText[0] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText1B
        {
            get { return m_QuestTextB[0]; }
            set { m_QuestTextB[0] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText1C
        {
            get { return m_QuestTextC[0]; }
            set { m_QuestTextC[0] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public Type QuestItem1
		{
			get{ return m_QuestItem[0]; }
			set{ m_QuestItem[0] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestItemKey1
        {
            get { return m_QuestItemKey[0]; }
            set { m_QuestItemKey[0] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public int QuestAmt1
		{
			get{ return m_QuestAmt[0]; }
			set{ m_QuestAmt[0] = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string QuestText2
		{
			get{ return m_QuestText[1]; }
			set{ m_QuestText[1] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText2B
        {
            get { return m_QuestTextB[1]; }
            set { m_QuestTextB[1] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText2C
        {
            get { return m_QuestTextC[1]; }
            set { m_QuestTextC[1] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public Type QuestItem2
		{
			get{ return m_QuestItem[1]; }
			set{ m_QuestItem[1] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestItemKey2
        {
            get { return m_QuestItemKey[1]; }
            set { m_QuestItemKey[1] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public int QuestAmt2
		{
			get{ return m_QuestAmt[1]; }
			set{ m_QuestAmt[1] = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string QuestText3
		{
			get{ return m_QuestText[2]; }
			set{ m_QuestText[2] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText3B
        {
            get { return m_QuestTextB[2]; }
            set { m_QuestTextB[2] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText3C
        {
            get { return m_QuestTextC[2]; }
            set { m_QuestTextC[2] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public Type QuestItem3
		{
			get{ return m_QuestItem[2]; }
			set{ m_QuestItem[2] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestItemKey3
        {
            get { return m_QuestItemKey[2]; }
            set { m_QuestItemKey[2] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public int QuestAmt3
		{
			get{ return m_QuestAmt[2]; }
			set{ m_QuestAmt[2] = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string QuestText4
		{
			get{ return m_QuestText[3]; }
			set{ m_QuestText[3] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText4B
        {
            get { return m_QuestTextB[3]; }
            set { m_QuestTextB[3] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText4C
        {
            get { return m_QuestTextC[3]; }
            set { m_QuestTextC[3] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public Type QuestItem4
		{
			get{ return m_QuestItem[3]; }
			set{ m_QuestItem[3] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestItemKey4
        {
            get { return m_QuestItemKey[3]; }
            set { m_QuestItemKey[3] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public int QuestAmt4
		{
			get{ return m_QuestAmt[3]; }
			set{ m_QuestAmt[3] = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string QuestText5
		{
			get{ return m_QuestText[4]; }
			set{ m_QuestText[4] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText5B
        {
            get { return m_QuestTextB[4]; }
            set { m_QuestTextB[4] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestText5C
        {
            get { return m_QuestTextC[4]; }
            set { m_QuestTextC[4] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public Type QuestItem5
		{
			get{ return m_QuestItem[4]; }
			set{ m_QuestItem[4] = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestItemKey5
        {
            get { return m_QuestItemKey[4]; }
            set { m_QuestItemKey[4] = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public int QuestAmt5
		{
			get{ return m_QuestAmt[4]; }
			set{ m_QuestAmt[4] = value;}
		}
		
		public override void InitSBInfo()
		{
		}

		[Constructable]
		public TAVQuester() : this( null )
		{
		}

		public TAVQuester( string title ) : base( title )
		{
		}

		public TAVQuester( Serial serial ) : base( serial )
		{
		}

		public virtual void OnTalk( PlayerMobile player, bool contextMenu )
		{
			TeiravonMobile pm;
			bool passcheck = true;
				
			if (player is TeiravonMobile)
				pm = (TeiravonMobile)player;
			else 
				return;

			if ((int)AllowedRace != (int)TeiravonMobile.Class.None || (int)AllowedClass != (int)ClassGroup.None || (int)AllowedGroup != (int)ClassGroup.None)
			{
				
				if ((int)pm.PlayerClass != (int)AllowedClass && (int)AllowedClass != (int)TeiravonMobile.Class.None)
					passcheck = false;
					
				if ((int)pm.PlayerRace != (int)AllowedRace && (int)AllowedRace != (int)TeiravonMobile.Race.None)
					passcheck = false;
				
				if (((int)AllowedGroup == (int)ClassGroup.Fighter && !(pm.IsFighter() || pm.IsKensai() || pm.IsCavalier())) && (int)AllowedGroup != (int)ClassGroup.None )
					passcheck = false;

				if (((int)AllowedGroup == (int)ClassGroup.Cleric && !(pm.IsCleric() || pm.IsDarkCleric())) && (int)AllowedGroup != (int)ClassGroup.None )
					passcheck = false;

				if (((int)AllowedGroup == (int)ClassGroup.Mage && !(pm.IsMage())) && (int)AllowedGroup != (int)ClassGroup.None  )
					passcheck = false;

				if (((int)AllowedGroup == (int)ClassGroup.Ranger && !(pm.IsRanger() || pm.IsArcher())) && (int)AllowedGroup != (int)ClassGroup.None  )
					passcheck = false;

				if (((int)AllowedGroup == (int)ClassGroup.Druid && !(pm.IsForester() || pm.IsShapeshifter())) && (int)AllowedGroup != (int)ClassGroup.None  )
					passcheck = false;

				if (((int)AllowedGroup == (int)ClassGroup.Barbarian && !(pm.IsBerserker() || pm.IsDragoon())) && (int)AllowedGroup != (int)ClassGroup.None  )
					passcheck = false;

				if (((int)AllowedGroup == (int)ClassGroup.Rogue && !(pm.IsThief()|| pm.IsBard() || pm.IsAssassin())) && (int)AllowedGroup != (int)ClassGroup.None  )
					passcheck = false;

				if (((int)AllowedGroup == (int)ClassGroup.Crafter && !(pm.IsCrafter())) && (int)AllowedGroup != (int)ClassGroup.None  )
					passcheck = false;

				if (!passcheck)
				{
					this.SayTo(pm, "I have nothing for you at this time");
					return;
				}
			}
			
			if (pm.PlayerLevel < MinLevel)
			{
				this.SayTo(pm, "I have nothing for you at this time");
				return;
			}
				
			
			if (QuestText1 == null || QuestItem1 == null)
				return;
			
			if (!this.InLOS(player))
				return;
			
			if (!CompletedList.Contains(player))
				CompletedList.Add(player, -1);
			
			if ((int)CompletedList[player] > 5 && CanRepeat)
				CompletedList[player] = -1;
			
			switch((int)CompletedList[player])
			{
				case -1:
					this.SayTo(player, QuestDescription);
                    if (QuestDescriptionB != null)
                        this.SayTo(player, QuestDescriptionB);
                    if (QuestDescriptionC != null)
                        this.SayTo(player, QuestDescriptionC);
					CompletedList[player] = 0;
					break;
					
				case 0:
					this.SayTo(player, QuestText1);
                    if (QuestText1B != null)
                        this.SayTo(player, QuestText1B);
                    if (QuestText1C != null)
                        this.SayTo(player, QuestText1C);
                    CompletedList[player] = 1;
					break;
					
				case 1:
					if (FindQuestItem(QuestItem1, QuestAmt1, player, 1, QuestItemKey1))
					{
						if (QuestText2 == null || QuestItem2 == null)
							goto case 5;
						this.SayTo(player, QuestText2);
						CompletedList[player] = 2;
                        if (QuestText2B != null)
                            this.SayTo(player, QuestText2B);
                        if (QuestText2C != null)
                            this.SayTo(player, QuestText2C);
                    }
					else
					{
						this.SayTo(player, QuestText1);
                        if (QuestText1B != null)
                            this.SayTo(player, QuestText1B);
                        if (QuestText1C != null)
                            this.SayTo(player, QuestText1C);
                    }
					break;
					
				case 2:
					if (FindQuestItem(QuestItem2, QuestAmt2, player, 2, QuestItemKey2))
					{
						if (QuestText3 == null || QuestItem3 == null)
							goto case 5;
						this.SayTo(player, QuestText3);
						CompletedList[player] = 3;
                        if (QuestText3B != null)
                            this.SayTo(player, QuestText3B);
                        if (QuestText3C != null)
                            this.SayTo(player, QuestText3C);
                    }
					else
					{
						this.SayTo(player, QuestText2);
                        if (QuestText2B != null)
                            this.SayTo(player, QuestText2B);
                        if (QuestText2C != null)
                            this.SayTo(player, QuestText2C);
                    }
					break;

				case 3:
					if (FindQuestItem(QuestItem3, QuestAmt3, player, 3, QuestItemKey3))
					{
						if (QuestText4 == null || QuestItem4 == null)
							goto case 5;
						this.SayTo(player, QuestText4);
                        if (QuestText4B != null)
                            this.SayTo(player, QuestText4B);
                        if (QuestText4C != null)
                            this.SayTo(player, QuestText4C);
                        CompletedList[player] = 4;
					}
					else
					{
						this.SayTo(player, QuestText3);
                        if (QuestText3B != null)
                            this.SayTo(player, QuestText3B);
                        if (QuestText3C != null)
                            this.SayTo(player, QuestText3C);
                    }
					break;

				case 4:
					if (FindQuestItem(QuestItem4, QuestAmt4, player, 4, QuestItemKey4))
					{
						if (QuestText5 == null || QuestItem5 == null)
							goto case 5;
						this.SayTo(player, QuestText5);
                        if (QuestText5B != null)
                            this.SayTo(player, QuestText5B);
                        if (QuestText5C != null)
                            this.SayTo(player, QuestText5C);
                        CompletedList[player] = 5;
					}
					else
					{
						this.SayTo(player, QuestText4);
                        if (QuestText4B != null)
                            this.SayTo(player, QuestText4B);
                        if (QuestText4C != null)
                            this.SayTo(player, QuestText4C);
                    }
					break;

				case 5:
					if (QuestText5 == null || QuestItem5 == null)
					{
						this.SayTo(player, QuestEnd);
                        if (QuestEndB != null)
                            this.SayTo(player, QuestEndB);
                        if (QuestEndC != null)
                            this.SayTo(player, QuestEndC);
						CompletedList[player] = 6;
						EndQuest(player);
					}
					else if (FindQuestItem(QuestItem5, QuestAmt5, player, 5, QuestItemKey5))
					{
						this.SayTo(player, QuestEnd);
                        if (QuestEndB != null)
                            this.SayTo(player, QuestEndB);
                        if (QuestEndC != null)
                            this.SayTo(player, QuestEndC);
                        CompletedList[player] = 6;
						EndQuest(player);
					}
					else
					{
						this.SayTo(player, QuestText5);
                        if (QuestText5B != null)
                            this.SayTo(player, QuestText5B);
                        if (QuestText5C != null)
                            this.SayTo(player, QuestText5C);
                    }
					break;

				default:
					this.SayTo(player, "I have no need of your services at this time.");
					break;

			}
			
		}

		public virtual bool FindQuestItem (Type item, int amt, PlayerMobile pm, int questnum, string keystring)
		{
			if (pm.Backpack == null)
			{
				pm.SendMessage("Error:  You do not have a backpack");
				return false;
			}

            if (item == typeof(QuestScrollItem))
            {
                if (amt > 99)
                    pm.SendMessage("Error: QuestScrollItem amount > 99, inform a GM");
                
                Item[] scrolls = pm.Backpack.FindItemsByType(typeof(QuestScrollItem));
                Item[] scrollkey = new Item[125];
                int cntr = 0;
                for (int i = 0; i < scrolls.Length; ++i)
                {
                    QuestScrollItem scrl = scrolls[i] as QuestScrollItem;
                    if (scrl.Key == keystring)
                    {
                        scrollkey[cntr] = scrolls[i];
                        ++cntr;
                    }
                }

                if (cntr < amt)
                    return false;

                for (int i = 0; i < amt; ++i)
                {
                    scrollkey[i].Delete();
                }
                return true;
            }

            if (item == typeof(QuestTokenItem))
            {
                if (amt > 99)
                    pm.SendMessage("Error: QuestTokenItem amount > 99, inform a GM");

                Item[] tokens = pm.Backpack.FindItemsByType(typeof(QuestTokenItem));
                Item[] tokenkey = new Item[125];
                int cntr = 0;
                for (int i = 0; i < tokens.Length; ++i)
                {
                    QuestTokenItem tkn = tokens[i] as QuestTokenItem;
                    if (tkn.Key == keystring)
                    {
                        tokenkey[cntr] = tokens[i];
                        ++cntr;
                    }
                }

                if (cntr < amt)
                    return false;

                for (int i = 0; i < amt; ++i)
                {
                    tokenkey[i].Delete();
                }
                return true;
            }
            
            if (pm.Backpack.GetAmount(item) >= amt)
			{
				pm.Backpack.ConsumeTotal( item, amt );
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public virtual void EndQuest(PlayerMobile pm)
		{
			TeiravonMobile tm = (TeiravonMobile)pm;
            if (Titles.AwardExp(tm, QuestExp))
			    pm.SendMessage("You have gained {0} experience.", QuestExp);
			pm.AddToBackpack( new Gold( GoldAmt ) );
			if (this.Backpack != null)
			{
				if (this.Backpack.Items.Count > 0)
				{
					pm.AddToBackpack( (Item)this.Backpack.Items[Utility.Random(this.Backpack.Items.Count)]);
				}
			}
		}
		
		public virtual bool CanTalkTo( PlayerMobile to )
		{
			return true;
		}

		public virtual int GetAutoTalkRange( PlayerMobile m )
		{
			return -1;
		}

		public override bool CanBeDamaged()
		{
			return false;
		}

		protected Item SetHue( Item item, int hue )
		{
			item.Hue = hue;
			return item;
		}

		public override void AddCustomContextEntries( Mobile from, ArrayList list )
		{
			base.AddCustomContextEntries( from, list );

			if ( from.Alive && from is PlayerMobile && TalkNumber > 0 && CanTalkTo( (PlayerMobile)from ) )
				list.Add( new TalkEntry( this ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( (bool)m_CanRepeat );
			for (int i = 0; i < 5; i++)
			{
				writer.Write( m_QuestText[i] );
				if (m_QuestItem[i] != null)
					writer.Write( m_QuestItem[i].ToString() );
				else
					writer.Write( "" );
				writer.Write( (int)m_QuestAmt[i] );
			}
			writer.Write( m_QuestDescription );
			writer.Write( m_QuestEnd );
			writer.Write( (int)m_GoldAmt );
			writer.Write( (int)m_QuestExp );
			writer.Write( (int)CompletedList.Count );
			foreach ( PlayerMobile pm in CompletedList.Keys )
			{
				writer.Write( (Mobile)pm );
				writer.Write( (int)CompletedList[pm] );
			}
			writer.Write( (int)m_class );
			writer.Write( (int)m_classgroup );
			writer.Write( (int)m_level );
			writer.Write( (int)m_race );

            for (int i = 0; i < 5; i++)
            {
                writer.Write(m_QuestItemKey[i]);
                writer.Write(m_QuestTextB[i]);
                writer.Write(m_QuestTextC[i]);
            }
            writer.Write(m_QuestDescriptionB);
            writer.Write(m_QuestDescriptionC);
            writer.Write(m_QuestEndB);
            writer.Write(m_QuestEndC);

		}

        public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_CanRepeat = reader.ReadBool();
			String tempstring;
			String setstring;
			int strind = 0;
			for (int i = 0; i < 5; i++)
			{
				setstring = "";
				m_QuestText[i] = reader.ReadString();
				tempstring = reader.ReadString();
				
				for (int z = 0; z < tempstring.Length; z++)
				{
					if (String.Compare(tempstring.Substring(z,1),".") == 0)
						strind = z;
				}
				
				for (int z = strind + 1; z < tempstring.Length; z++)
					setstring = setstring + tempstring[z];
				
				m_QuestItem[i] = ScriptCompiler.FindTypeByName(setstring);
				m_QuestAmt[i] = reader.ReadInt();
			}
			m_QuestDescription = reader.ReadString();
			m_QuestEnd = reader.ReadString();
			m_GoldAmt = reader.ReadInt();
			m_QuestExp = reader.ReadInt();
			int hashcnt = reader.ReadInt();
			if (hashcnt > 0)
			{
				PlayerMobile pm;
				int qn;
				for (int ii = 0; ii < hashcnt; ii++)
				{
					try
					{
						pm = (PlayerMobile)reader.ReadMobile();
						qn = reader.ReadInt();
                        if (!CompletedList.Contains(pm))
						    CompletedList.Add(pm, qn);
					}
					catch
					{
						Console.WriteLine("Playermobile removed from quester");
					}
				}
			}
			else
			{
				CompletedList = new Hashtable();
			}
			
			if (version > 0)
			{
				m_class = (TeiravonMobile.Class)reader.ReadInt();
				m_classgroup = (ClassGroup)reader.ReadInt();
				m_level = reader.ReadInt();
				m_race = (TeiravonMobile.Race)reader.ReadInt();
			}
			else
			{
				m_class = TeiravonMobile.Class.None;
				m_classgroup = ClassGroup.None;
				m_level = 1;
				m_race = TeiravonMobile.Race.None;
			}

            if (version > 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    m_QuestItemKey[i] = reader.ReadString();
                    m_QuestTextB[i] = reader.ReadString();
                    m_QuestTextC[i] = reader.ReadString();
                }
                m_QuestDescriptionB = reader.ReadString();
                m_QuestDescriptionC = reader.ReadString();
                m_QuestEndB = reader.ReadString();
                m_QuestEndC = reader.ReadString();
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    m_QuestItemKey[i] = null;
                    m_QuestTextB[i] = null;
                    m_QuestTextC[i] = null;
                }
                m_QuestDescriptionB = null;
                m_QuestDescriptionC = null;
                m_QuestEndB = null;
                m_QuestEndC = null;
            }

		}
	}
}

namespace Server.Items
{
    public class QuestScrollItem : Item
    {
        private string m_addressee;
        private string m_message;
        private string m_sender;
        private string m_key;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public string Addressee
        {
            get { return m_addressee; }
            set { m_addressee = value; }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public string Sender
        {
            get { return m_sender; }
            set { m_sender = value; }
        }

        [Constructable]
        public QuestScrollItem()
            : this(1)
        {
        }

        [Constructable]
        public QuestScrollItem(int amount)
            : base(0x227B)
        {
            Stackable = false;
            Weight = 1.0;
            Amount = amount;
            Name = "Quest Scroll Item";
            Hue = 2;
        }

        public QuestScrollItem(Serial serial)
            : base(serial)
        {
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new QuestScrollItem(amount), amount);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your pack to use it");
                return;
            }
            if (this.Sender == null)
            {
                from.SendMessage("Enter the addressee or title");
                from.Prompt = new AddresseePrompt(this);
            }
            else
            {
                from.SendGump(new MScrollGump(this.Addressee, this.Message, this.Sender));
            }

        }

        private class AddresseePrompt : Prompt
        {
            private QuestScrollItem m_mscroll;

            public AddresseePrompt(QuestScrollItem scroll)
            {
                m_mscroll = scroll;
            }

            public override void OnResponse(Mobile from, string text)
            {
                m_mscroll.Addressee = text;
                from.SendMessage("Enter message part 1");
                from.Prompt = new MessagePrompt1(m_mscroll);
            }
        }

        private class MessagePrompt1 : Prompt
        {
            private QuestScrollItem m_mscroll;

            public MessagePrompt1(QuestScrollItem scroll)
            {
                m_mscroll = scroll;
            }

            public override void OnResponse(Mobile from, string text)
            {
                m_mscroll.Message = text;
                from.SendMessage("Enter message part 2");
                from.Prompt = new MessagePrompt2(m_mscroll);
            }
        }

        private class MessagePrompt2 : Prompt
        {
            private QuestScrollItem m_mscroll;

            public MessagePrompt2(QuestScrollItem scroll)
            {
                m_mscroll = scroll;
            }

            public override void OnResponse(Mobile from, string text)
            {
                m_mscroll.Message = m_mscroll.Message + text;
                from.SendMessage("Enter message part 3");
                from.Prompt = new MessagePrompt3(m_mscroll);
            }
        }

        private class MessagePrompt3 : Prompt
        {
            private QuestScrollItem m_mscroll;

            public MessagePrompt3(QuestScrollItem scroll)
            {
                m_mscroll = scroll;
            }

            public override void OnResponse(Mobile from, string text)
            {
                m_mscroll.Message = m_mscroll.Message + text;
                from.SendMessage("Enter message part 4");
                from.Prompt = new MessagePrompt4(m_mscroll);
            }
        }

        private class MessagePrompt4 : Prompt
        {
            private QuestScrollItem m_mscroll;

            public MessagePrompt4(QuestScrollItem scroll)
            {
                m_mscroll = scroll;
            }

            public override void OnResponse(Mobile from, string text)
            {
                m_mscroll.Message = m_mscroll.Message + text;
                from.SendMessage("Enter your signature");
                from.Prompt = new SenderPrompt(m_mscroll);
            }
        }

        private class SenderPrompt : Prompt
        {
            private QuestScrollItem m_mscroll;

            public SenderPrompt(QuestScrollItem scroll)
            {
                m_mscroll = scroll;
            }

            public override void OnResponse(Mobile from, string text)
            {
                m_mscroll.Sender = text;
                from.SendMessage("Your message is completed!");
                m_mscroll.Name = "A message scroll addressed to " + m_mscroll.Addressee;
            }
        }

        public class MScrollGump : Gump
        {
            public MScrollGump(string address, string msg, string signer)
                : base(0, 0)
            {
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(100, 10, 300, 450, 5170);
                this.AddHtml(130, 50, 240, 40, @address, (bool)false, (bool)false);
                this.AddHtml(130, 110, 240, 225, @msg, (bool)false, (bool)false);
                this.AddHtml(130, 375, 240, 40, @signer, (bool)false, (bool)false);

            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
            writer.Write((string)m_addressee);
            writer.Write((string)m_message);
            writer.Write((string)m_sender);
            writer.Write((string)m_key);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_addressee = reader.ReadString();
            m_message = reader.ReadString();
            m_sender = reader.ReadString();
            m_key = reader.ReadString();
        }
    }

    public class QuestTokenItem : Item
    {
        private string m_key;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }

        [Constructable]
        public QuestTokenItem()
            : this(1)
        {
        }

        [Constructable]
        public QuestTokenItem(int amount)
            : base(0x2809)
        {
            Stackable = false;
            Weight = 1.0;
            Amount = amount;
            Name = "Quest Token Item";
            Hue = 34;
        }

        public QuestTokenItem(Serial serial)
            : base(serial)
        {
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((string)m_key);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_key = reader.ReadString();
        }
    }

}