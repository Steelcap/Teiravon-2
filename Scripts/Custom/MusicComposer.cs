using System;
using System.Collections.Generic;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.ContextMenus;
using Server.Prompts;

namespace Server.Gumps.Music
{
	public enum Notes
	{
		do_1 = 7,//do
		dod1 = 10,//do diesis
		re_1 = 12,//re
		red1 = 14,//re diesis
		mi_1 = 16,//mi
		fa_1 = 18,//fa
		fad1 = 20,//fa diesis
		so_1 = 22,//sol
		sod1 = 24,//sol diesis
		la_1 = 1,//la
		lad1 = 3,//la diesis
		si_1 = 5,//si
		do_2 = 8,//do
		dod2 = 11,//do diesis
		re_2 = 13,//re
		red2 = 15,//re diesis
		mi_2 = 17,//mi
		fa_2 = 19,//fa
		fad2 = 21,//fa diesis
		so_2 = 23,//sol
		sod2 = 25,//sol diesis
		la_2 = 2,//la
		lad2 = 4,//la diesis
		si_2 = 6,//si
		do_3 = 9//do
	}

	public enum NoteFlag : uint
	{
		do_3 = 0x00000001,
		si_2 = 0x00000002,
		lad2 = 0x00000004,
		la_2 = 0x00000008,
		sod2 = 0x00000010,
		so_2 = 0x00000020,
		fad2 = 0x00000040,
		fa_2 = 0x00000080,
		mi_2 = 0x00000100,
		red2 = 0x00000200,
		re_2 = 0x00000400,
		dod2 = 0x00000800,
		do_2 = 0x00001000,
		si_1 = 0x00002000,
		lad1 = 0x00004000,
		la_1 = 0x00008000,
		sod1 = 0x00010000,
		so_1 = 0x00020000,
		fad1 = 0x00040000,
		fa_1 = 0x00080000,
		mi_1 = 0x00100000,
		red1 = 0x00200000,
		re_1 = 0x00400000,
		dod1 = 0x00800000,
		do_1 = 0x01000000
	}

	public class MusicSheet : Item
	{
		private List<SheetPage> m_Pages = new List<SheetPage>();
		public List<SheetPage> Pages {get{return m_Pages;}}
		private BaseInstrument m_Instrument;
		private int m_BeatPerMinute=210;
		public int BeatPerMinute
		{
			get{return m_BeatPerMinute;}
			set
			{
				if(value<30)
					m_BeatPerMinute=30;
				else if(value>650)
					m_BeatPerMinute=650;
				else
					m_BeatPerMinute=value;
			}
		}

		private int m_Guida=8;
		public int Guida
		{
			get{return m_Guida;}
			set
			{
				if(value<2)
					m_Guida=2;
				else if(value>16)
					m_Guida=16;
				else
					m_Guida=value;
			}
		}

		[CommandProperty(AccessLevel.Seer)]
		public BaseInstrument Instrument{get{return m_Instrument;}set{m_Instrument=value;}}

		private bool m_Repeat;
		
		public bool Repeat{get{return m_Repeat;}set{m_Repeat=value;}}
		public bool Playing(Mobile m)
		{
			InternalTimer t;
			if(m_CurrentlyUsing.TryGetValue(m, out t) && t!=null && t.Running)
			{
				return true;
			}
			return false;
		}
		private Mobile m_LastDclicked;
		private static Dictionary<Mobile, InternalTimer> m_CurrentlyUsing = new Dictionary<Mobile, InternalTimer>();

		private class InternalTimer : Timer
		{
			private static readonly Array m_Values = Enum.GetValues(typeof(NoteFlag));
			private Mobile m_Mobile;
			private int m_Page;
			private int m_Column;
			private MusicSheet m_Sheet;
			private bool m_Repeat;
			private int m_Mus=-1;

			public InternalTimer(Mobile mob, MusicSheet sheet, int startpage) : base(TimeSpan.FromSeconds(0.5), TimeSpan.FromMilliseconds(60000.0 / sheet.BeatPerMinute))
			{
				Priority = TimerPriority.TenMS;
				m_Sheet=sheet;
				m_Page=startpage;
				m_Column=0;
				m_Repeat=sheet.Repeat;
				m_Mobile=mob;
				if(m_Sheet!=null)
				{
					if(m_Sheet.m_Instrument!=null)
					{
						if(m_Sheet.m_Instrument is Harp)
							m_Mus=0x497 - 1;
						else if(m_Sheet.m_Instrument is LapHarp)
							m_Mus=0x3CA - 1;
						else
							Stop();
					}
				}
			}

			protected override void OnTick()
			{
				if(m_Sheet==null)
				{
					Stop();
				}
				else if(m_Mobile==null || m_Mobile.Backpack==null || m_Mobile.NetState==null || !m_Mobile.Alive)
				{
					Stop();
				}
				else if(!m_Mobile.Backpack.Items.Contains(m_Sheet) || (m_Mus>-1 && !m_Mobile.Backpack.Items.Contains(m_Sheet.m_Instrument)))
				{
					Stop();
				}
				else
				{
					MusicSheet.SheetPage sheet = m_Sheet.Pages[m_Page];
					uint tocheck = sheet.GetColumn(m_Column);
					foreach(uint val in m_Values)
					{
						if((tocheck & val) != 0)
						{
							string name = ((NoteFlag)val).ToString();
							if(m_Mus>-1)
								m_Mobile.PlaySound(m_Mus+((int)Enum.Parse(typeof(Notes), name)));
						}
					}
					m_Column++;
					if(m_Column>=sheet.Count)
					{
						m_Page++;
						m_Column=0;
					}
					if(m_Page>=m_Sheet.Pages.Count)
					{
						if(!m_Sheet.Repeat)
						{
							m_Mobile.CloseGump(typeof(MusicComposer));
							Stop();
							m_Mobile.SendGump(new MusicComposer(m_Mobile, m_Sheet, 1));
						}
						else
							m_Page=0;
					}
				}
			}
		}

		public void StartPlayback(Mobile m, int page)
		{
			if(page<0)
				page=0;
			else if(page>=m_Pages.Count)
				page=m_Pages.Count -1;

			InternalTimer t;
			//we first check that we aren't using another sheet...
			if(m_CurrentlyUsing.TryGetValue(m, out t) && t!=null)
				t.Stop();
			m_CurrentlyUsing[m] = t = new MusicSheet.InternalTimer(m, this, page);
			t.Start();
		}

		public void StopPlayback(Mobile m)
		{
			InternalTimer t;
			if(m_CurrentlyUsing.TryGetValue(m, out t) && t!=null)
				t.Stop();
		}

		[ConstructableAttribute]
		public MusicSheet() : base(0x1C13)
		{
			Name="Music Composer";
			m_Pages.Add(new SheetPage(MusicComposer.DefaultPageSize));
			LootType = LootType.Blessed;
		}

		public MusicSheet(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if(from.Backpack!=null)
			{
				if(!Playing(from) && from.Backpack.Items.Contains(this))
				{
					from.SendMessage("Select an instrument!");
					from.Target=new InternalTarget(from, this);
				}
				else
					from.SendMessage("The composer MUST be in your backpack!");
			}
		}

		private class InternalTarget : Target
		{
			private Mobile m_Mobile;
			private MusicSheet m_Sheet;

			public InternalTarget(Mobile from, MusicSheet sheet) : base(1, false, TargetFlags.None)
			{
				m_Mobile=from;
				m_Sheet=sheet;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if(m_Sheet==null || from.Backpack==null || !from.Backpack.Items.Contains(m_Sheet)) return;
				from.CloseGump(typeof(MusicComposer));
				if(targeted is BaseInstrument)
				{
					BaseInstrument instrument = (BaseInstrument)targeted;
					if(!from.Backpack.Items.Contains(instrument))
					{
						from.SendMessage("The instrument must be in your backpack! Retry! (press ESC to cancel)");
						from.Target=new InternalTarget(from, m_Sheet);
					}
					else if(instrument is LapHarp || instrument is Harp)
					{
						m_Sheet.m_Instrument=instrument;
					}
				}
				else
				{
					from.SendMessage("That's not a valid musical instrument, if you want to cancel the operation press ESC!");
					from.Target=new InternalTarget(from, m_Sheet);
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				if(m_Sheet==null || from.Backpack==null || !from.Backpack.Items.Contains(m_Sheet)) return;

				m_Sheet.m_Instrument=null;
			}
		}

		public class SheetPage
		{
			private List<uint> m_Content = new List<uint>();

			public SheetPage(int pagesize)
			{
				for(int i=0; i<pagesize; i++)
					m_Content.Add(0);
			}

			private SheetPage()
			{
			}

			public uint GetColumn(int column)
			{
				return m_Content[column];
			}

			public bool SetColumn(int column, uint buttons)
			{
				if(m_Content.Count<=column)
					return false;
				m_Content[column] = buttons;
				return true;
			}

			public bool FlaggedBox(int col, int row)
			{
				return (m_Content[col] & ((uint)1 << row)) != 0;
			}

			public void Enlarge()
			{
				if(m_Content.Count<36)
					m_Content.Add(0);
			}

			public void Reduce()
			{
				if(m_Content.Count>1)
					m_Content.RemoveAt(m_Content.Count - 1);
			}

			public void Insert(int atindex)
			{
				if(atindex>=0 && atindex<=m_Content.Count)
					m_Content.Insert(atindex, 0);
			}

			public void Remove(int atindex)
			{
				if(atindex>=0 && atindex<m_Content.Count)
					m_Content.RemoveAt(atindex);
			}

			public int Count{get{return m_Content.Count;}}

			public List<uint> GetButtons{ get{return m_Content;} }

			public void SetButtons(List<uint> buttonlist)
			{
				m_Content = buttonlist;
			}

			public void Serialize(GenericWriter writer)
			{
				int x = m_Content.Count;
				writer.Write(x);
				for(int i=0; i<x; i++)
					writer.Write(m_Content[i]);
			}

			public static SheetPage Deserialize(GenericReader reader)
			{
				int x = reader.ReadInt();
				SheetPage page = new SheetPage();

				for(int i=0; i<x; i++)
					page.m_Content.Add(reader.ReadUInt());

				return page;
			}
		}

		private string m_SongName="";
		public string SongName{get{return m_SongName;}}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);

			int x = m_Pages.Count;
			writer.Write(x);
			for(int i=0; i<x; i++)
				m_Pages[i].Serialize(writer);
			writer.Write(m_Instrument);
			writer.Write(m_BeatPerMinute);
			writer.Write(m_Guida);
			writer.Write(Repeat);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			reader.ReadInt();
			int x = reader.ReadInt();
			for(int i=0; i<x; i++)
				m_Pages.Add(SheetPage.Deserialize(reader));
			m_Instrument=(BaseInstrument)reader.ReadItem();
			m_BeatPerMinute=reader.ReadInt();
			m_Guida=reader.ReadInt();
			Repeat=reader.ReadBool();
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties(list);
			if(!string.IsNullOrEmpty(m_SongName))
				list.Add(m_SongName);
		}

        public override void GetContextMenuEntries(Mobile from, ArrayList list)
        {
            base.GetContextMenuEntries(from, list);
			list.Add(new NominaBrano(from, this));
			list.Add(new Compositore(from, this));
		}

		private class NominaBrano : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private MusicSheet m_Sheet;

			public NominaBrano(Mobile m, MusicSheet sheet) : base(75)
			{
				m_Mobile=m;
				m_Sheet=sheet;
			}

			public override void OnClick()
			{
				if(m_Mobile==null || m_Mobile.Deleted || m_Mobile.Backpack==null || m_Sheet==null) return;
				if(m_Mobile.Backpack.Items.Contains(m_Sheet))
				{
					m_Mobile.Prompt=new RenameSheetPrompt(m_Sheet, m_Mobile);
				}
				else
					m_Mobile.SendMessage("The composer must be in your backpack");
			}

			private class RenameSheetPrompt : Prompt
			{
				private MusicSheet m_Sheet;
				private Mobile m_From;
		
				public RenameSheetPrompt( MusicSheet sheet, Mobile from )
				{
					m_Sheet = sheet;
					m_From = from;
					m_From.SendMessage("Select the name of the composition");
				}

				public override void OnResponse( Mobile from, string text )
				{
					if(m_Sheet==null || from == null || from.Backpack==null || !from.Backpack.Items.Contains(m_Sheet)) return;

					text = text.Trim();
		
					if ( text.Length > 40 )
						text = text.Substring( 0, 40 );
		
					if ( text.Length > 0 )
					{
						m_Sheet.m_SongName=text;
						m_Sheet.InvalidateProperties();
					}

					m_From.SendMessage("You renamed your sheet as '{0}'", m_Sheet.m_SongName);
				}
			}
		}

		private class Compositore : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private MusicSheet m_Sheet;

			public Compositore(Mobile m, MusicSheet sheet) : base(2132)
			{
				m_Mobile=m;
				m_Sheet=sheet;
			}

			public override void OnClick()
			{
				if(m_Mobile==null || m_Mobile.Deleted || m_Mobile.Backpack==null || m_Sheet==null) return;
				if(m_Mobile.Backpack.Items.Contains(m_Sheet))
				{
					m_Mobile.CloseGump(typeof(MusicComposer));
					m_Mobile.SendGump(new MusicComposer(m_Mobile, m_Sheet, 1));
				}
				else
					m_Mobile.SendMessage("The composer must be in your backpack");
			}
		}
	}

	public class MusicComposer : Gump
	{
		public const int DefaultPageSize = 36;
		private const int DefHue = 996;
		private int m_Page;
		private MusicSheet m_MusicSheet;
		private MusicSheet.SheetPage m_SheetPage;
		private Mobile m_Mobile;

		//the starter gump
		public MusicComposer(Mobile m, MusicSheet sheet, int page) : base(0,0)
		{
			if(page>sheet.Pages.Count || sheet.Instrument==null) return;
			m_Mobile=m;
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			m_MusicSheet=sheet;
			m_Page=page;
			PreGump();
		}

		private void PreGump()
		{
			m_SheetPage=m_MusicSheet.Pages[m_Page-1];
			AddPage(0);
			int multiuse = 25;
			int cols = m_SheetPage.Count;
			AddBackground(0, 0, 472+(12*Math.Max(0, cols-DefaultPageSize)), 132+14*multiuse, 9300);
			//row 1
			AddHtml( 5, 4, 452, 18, m_MusicSheet.SongName, false, false);
			//salvataggio manuale
			AddButton(325, 6, 11410, 11412, 1, GumpButtonType.Reply, 0);
			AddHtml( 343, 5, 123, 18, "Save Everything", false, false);
			//row 2
			AddButton(6, 28, 2224, 2224, 2, GumpButtonType.Reply, 0);
			AddHtml( 26, 25, 53, 18, (m_MusicSheet.Playing(m_Mobile) ? "Stop" : "Start"), false, false);
			multiuse = (m_MusicSheet.Repeat ? 0 : 10);
			AddButton(79, 27, 11400+multiuse, 11402+multiuse, 3, GumpButtonType.Reply, 0);
			AddHtml( 96, 25, 91, 18, "Repeat: "+(m_MusicSheet.Repeat ? "<BASEFONT COLOR=GREEN>ON</BASEFONT>" : "<BASEFONT COLOR=RED>OFF</BASEFONT>"), false, false);
			//page switching start
			AddButton(192, 26, 5002, 5002, 3000+m_Page-1, GumpButtonType.Reply, 0);
			AddLabel(196, 23, DefHue, "<");
			AddButton(330, 26, 5002, 5002, 3000+m_Page+1, GumpButtonType.Reply, 0);
			AddLabel(335, 23, DefHue, ">");
			//page swtiching end
			AddButton(361, 28, 2224, 2224, 4, GumpButtonType.Reply, 0);
			AddHtml( 381, 25, 76, 18, (m_MusicSheet.Playing(m_Mobile) ? "Stop" : "Start from Here"), false, false);
			//row 3
			//BPM BEGIN
			multiuse = m_MusicSheet.BeatPerMinute;
			AddHtml( 46, 47, 105, 18, "<CENTER>"+multiuse+" BPM</CENTER>", false, false);
			multiuse += 4000;
			AddButton(6, 48, 5002, 5002, multiuse-10, GumpButtonType.Reply, 0);//-10
			AddLabel(6, 46, DefHue, "<<");
			AddButton(26, 48, 5002, 5002, multiuse-1, GumpButtonType.Reply, 0);//-1
			AddLabel(30, 46, DefHue, "<");
			AddButton(154, 48, 5002, 5002, multiuse+1, GumpButtonType.Reply, 0);//+1
			AddLabel(159, 46, DefHue, ">");
			AddButton(174, 48, 5002, 5002, multiuse+10, GumpButtonType.Reply, 0);//+10
			AddLabel(175, 46, DefHue, ">>");
			//BPM END
			//PAGE SIZE
			AddButton(223, 48, 5002, 5002, 5, GumpButtonType.Reply, 0);//-1
			AddLabel(227, 46, DefHue, "<");
			AddHtml( 237, 47, 183, 18, "<CENTER>Page Size: "+cols+"</CENTER>", false, false);
			AddButton(416, 48, 5002, 5002, 6, GumpButtonType.Reply, 0);//+1
			AddLabel(422, 46, DefHue, ">");
			//PAGE SIZE END
			//row 4
			AddButton(7, 71, 2362, 2361, 7, GumpButtonType.Reply, 0);
			AddHtml( 20, 68, 87, 18, "New Page", false, false);
			AddButton(111, 71, 2362, 2361, 8, GumpButtonType.Reply, 0);
			AddHtml( 124, 68, 87, 18, "Copy Page", false, false);
			AddButton(216, 71, 2362, 2361, 9, GumpButtonType.Reply, 0);
			AddHtml( 229, 68, 87, 18, "Remove Page", false, false);
			//GUIDA
			multiuse=m_MusicSheet.Guida;
			AddButton(321, 69, 5002, 5002, 5000+multiuse-1, GumpButtonType.Reply, 0);//-1
			AddLabel(326, 67, DefHue, "<");
			AddHtml( 341, 68, 90, 18, "<CENTER>Guide: "+multiuse+"</CENTER>", false, false);
			AddButton(433, 69, 5002, 5002, 5000+multiuse+1, GumpButtonType.Reply, 0);//+1
			AddLabel(439, 67, DefHue, @">");
			//GUIDA
			EvalGump(m_Page);
		}

		public void EvalGump(int page)
		{
			AddHtml( 212, 25, 116, 18, "<CENTER>Page "+(page)+"/"+m_MusicSheet.Pages.Count+"</CENTER>", false, false);
			AddHtml( 5, 94, 30, 14, @"INS", false, false);
			int start = 97;
			AddButton(5, start+=14, 214, 214, 1009, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1006, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1004, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1002, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1025, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1023, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1021, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1019, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1017, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1015, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1013, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1011, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1008, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1005, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1003, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1001, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1024, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1022, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1020, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1018, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1016, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1014, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1012, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 3600, 3600, 1010, GumpButtonType.Reply, 0);
			AddButton(5, start+=14, 214, 214, 1007, GumpButtonType.Reply, 0);
			AddHtml( 5, (start+=14)-3, 30, 14, @"DEL", false, false);
			//34
			start=m_MusicSheet.Guida;
			int cols = m_SheetPage.Count;
			int rows = 25;

			for(int col=0; col<cols; col++)
			{
				AddButton(34+12*col, 97, 2437, 2438, 6000+col, GumpButtonType.Reply, 0);//inserimento
				if(col!=0 && col%start==0)
					AddImageTiled(34+12*col-1, 111, 1, 14*rows, 9103);
				//for each col we have all the rows...
				for(int row = 0; row<rows; row++)
				{
					AddCheck(34+12*col, 111+14*row, 2362, 2361, m_SheetPage.FlaggedBox(col, row), col*32+row);
				}
				AddButton(34+12*col, 111+rows*14, 2437, 2438, 7000+col, GumpButtonType.Reply, 0);//rimozione
			}
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if(sender==null || info==null) return;
			Mobile m = sender.Mobile;
			if(m==null || m.Backpack==null || m_MusicSheet==null || !m.Backpack.Items.Contains(m_MusicSheet)) return;
			if(info.ButtonID==0)
			{
				m.CloseGump(typeof(MusicComposer));
				return;
			}
			bool abort = false;
			if(m_MusicSheet.Instrument == null || !m.Backpack.Items.Contains(m_MusicSheet.Instrument))
			{
				m_MusicSheet.Instrument=null;
				abort=true;
				m.SendMessage("The instrument is not in your backpack!");
			}
			//save the state
			int cols=m_SheetPage.Count;
			int rows=32;//everything
			if(info.Switches!=null)
			{
				for(int col=0; col<cols; col++)
				{
					uint rowcont=0;
					for(int row=0; row<rows; row++)
					{
						if(info.IsSwitched(col*rows+row))
							rowcont |= (uint)1 << row;
						else
							rowcont &= ~((uint)1 << row);
					}
					m_SheetPage.SetColumn(col, rowcont);
				}
			}
			rows=info.ButtonID;
			if(abort)
			{
				m.CloseGump(typeof(MusicComposer));
				return;
			}
			if(rows==2 || rows==4)
			{
				if(m_MusicSheet.Playing(m))
					m_MusicSheet.StopPlayback(m);
				else
					m_MusicSheet.StartPlayback(m, (rows == 4 ? m_Page - 1 : 0));
			}
			else if(rows==3)
			{
				m_MusicSheet.Repeat=!m_MusicSheet.Repeat;
			}
			else if(rows==5)
			{
				m_SheetPage.Reduce();
			}
			else if(rows==6)
			{
				m_SheetPage.Enlarge();
			}
			else if(rows==7)
			{
				if(m_MusicSheet.Pages.Count<60)
					m_MusicSheet.Pages.Add(new MusicSheet.SheetPage(DefaultPageSize));
			}
			else if(rows==8)
			{
				if(m_MusicSheet.Pages.Count<60)
				{
					MusicSheet.SheetPage sheet = new MusicSheet.SheetPage(cols);
					sheet.SetButtons(new List<uint>(m_SheetPage.GetButtons));
					m_MusicSheet.Pages.Add(sheet);
				}
			}
			else if(rows==9)
			{
				if(m_MusicSheet.Pages.Count>1)
					m_MusicSheet.Pages.Remove(m_SheetPage);
			}
			else if(rows>=1000 && rows<2000)
			{
				int note=-1;
				if(m_MusicSheet.Instrument is Harp)
					note=0x497;
				else if(m_MusicSheet.Instrument is LapHarp)
					note=0x3CA;
				m.PlaySound(note+rows-1001);
			}
			else if(rows>=3000 && rows<4000)
			{
				if(rows>3000 && (rows-3000)<=m_MusicSheet.Pages.Count)
					m_Page=rows-3000;
			}
			else if(rows>=4000 && rows<5000)
			{
				m_MusicSheet.BeatPerMinute=rows-4000;
			}
			else if(rows>=5000 && rows<6000)
			{
				m_MusicSheet.Guida=rows-5000;
			}
			else if(rows>=6000 && rows<7000)
			{
				if(m_SheetPage.Count<36)
					m_SheetPage.Insert(rows-6000);
			}
			else if(rows>=7000)
			{
				if(m_SheetPage.Count>1)
					m_SheetPage.Remove(rows-7000);
			}
			m.CloseGump(typeof(MusicComposer));
			m.SendGump(new MusicComposer(m, m_MusicSheet, m_Page));
		}
	}
}