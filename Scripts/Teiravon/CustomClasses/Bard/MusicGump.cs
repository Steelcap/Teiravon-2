/*
 * Created by SharpDevelop.
 * User: gideon
 * Date: 2005/04/21
 * Time: 04:38 PM
 * 
 */

using System;
using System.Collections;
using System.IO;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	public class MusicGump : Gump
	{	
		public int[] Notes;
		public int Size;
		
		public MusicGump(int[] notes, int size) : base( 50, 50 )
		{
			Notes = notes;
			Size = size;
			
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);
			AddBackground(50, 10, 455, 260, 5054);
			AddAlphaRegion(57, 17, 437, 247);
			AddBackground(50, 280, 455, 80, 5054);
			AddAlphaRegion(57, 287, 437, 67);
			AddBackground(50, 370, 455, 60, 5054);
			AddAlphaRegion(57, 377, 437, 47);
			AddImage(480, 245, 10460);
			AddImage(480,   5, 10460);
			AddImage( 45, 245, 10460);
			AddImage( 45,   5, 10460);
			AddImage( 45, 275, 10460);
			AddImage( 45, 335, 10460);
			AddImage(480, 275, 10460);
			AddImage(480, 335, 10460);
			AddImage( 45, 365, 10460);
			AddImage(480, 365, 10460);
			AddImage( 45, 405, 10460);
			AddImage(480, 405, 10460);

			AddImageTiled(110, 90, 22, 98, 3004);
			AddImageTiled(133, 90, 22, 98, 3004);
			AddImageTiled(156, 90, 22, 98, 3004);
			AddImageTiled(179, 90, 22, 98, 3004);
			AddImageTiled(202, 90, 22, 98, 3004);
			AddImageTiled(225, 90, 22, 98, 3004);
			AddImageTiled(248, 90, 22, 98, 3004);
			AddImageTiled(271, 90, 22, 98, 3004);
			AddImageTiled(294, 90, 22, 98, 3004);
			AddImageTiled(317, 90, 22, 98, 3004);
			AddImageTiled(340, 90, 22, 98, 3004);
			AddImageTiled(363, 90, 22, 98, 3004);
			AddImageTiled(386, 90, 22, 98, 3004);
			AddImageTiled(409, 90, 22, 98, 3004);
			AddImageTiled(432, 90, 22, 98, 3004);
			AddImageTiled(122, 90, 14, 52, 3604);
			AddImageTiled(146, 90, 14, 52, 3604);
			AddImageTiled(191, 90, 14, 52, 3604);
			AddImageTiled(215, 90, 14, 52, 3604);
			AddImageTiled(237, 90, 14, 52, 3604);
			AddImageTiled(284, 90, 14, 52, 3604);
			AddImageTiled(306, 90, 14, 52, 3604);
			AddImageTiled(353, 90, 14, 52, 3604);
			AddImageTiled(375, 90, 14, 52, 3604);
			AddImageTiled(399, 90, 14, 52, 3604);

			AddLabel(75,  50, 1152, @"Play");
			AddLabel(75,  70, 1152, @"Add");
			AddLabel(75, 190, 1152, @"Play");
			AddLabel(75, 210, 1152, @"Add");
			
			for (int i = 0; i < 15; ++i)
			{
				AddButton(113+((i)*23), 192, 2117, 2118,  (i+1), GumpButtonType.Reply, 0);
				AddButton(113+((i)*23), 212, 2117, 2118, (i+26), GumpButtonType.Reply, 0);
			}
			
			AddButton(122, 52, 2117, 2118, 16, GumpButtonType.Reply, 0);
			AddButton(122, 72, 2117, 2118, 41, GumpButtonType.Reply, 0);
			AddButton(146, 52, 2117, 2118, 17, GumpButtonType.Reply, 0);
			AddButton(146, 72, 2117, 2118, 42, GumpButtonType.Reply, 0);
			AddButton(191, 52, 2117, 2118, 18, GumpButtonType.Reply, 0);
			AddButton(191, 72, 2117, 2118, 43, GumpButtonType.Reply, 0);
			AddButton(215, 52, 2117, 2118, 19, GumpButtonType.Reply, 0);
			AddButton(215, 72, 2117, 2118, 44, GumpButtonType.Reply, 0);
			AddButton(237, 52, 2117, 2118, 20, GumpButtonType.Reply, 0);
			AddButton(237, 72, 2117, 2118, 45, GumpButtonType.Reply, 0);
			AddButton(284, 52, 2117, 2118, 21, GumpButtonType.Reply, 0);
			AddButton(284, 72, 2117, 2118, 46, GumpButtonType.Reply, 0);
			AddButton(306, 52, 2117, 2118, 22, GumpButtonType.Reply, 0);
			AddButton(306, 72, 2117, 2118, 47, GumpButtonType.Reply, 0);
			AddButton(353, 52, 2117, 2118, 23, GumpButtonType.Reply, 0);
			AddButton(353, 72, 2117, 2118, 48, GumpButtonType.Reply, 0);
			AddButton(375, 52, 2117, 2118, 24, GumpButtonType.Reply, 0);
			AddButton(375, 72, 2117, 2118, 49, GumpButtonType.Reply, 0);
			AddButton(399, 52, 2117, 2118, 25, GumpButtonType.Reply, 0);
			AddButton(399, 72, 2117, 2118, 50, GumpButtonType.Reply, 0);
			
			AddButton( 95, 382, 2117, 2118, 51, GumpButtonType.Reply, 0);
			AddButton(262, 382, 2117, 2118, 53, GumpButtonType.Reply, 0);
			AddButton( 95, 402, 2117, 2118, 52, GumpButtonType.Reply, 0);
			
			AddLabel(120, 380, 1152, @"Clear All Notes");
			AddLabel(282, 380, 1152, @"Play Entire Song");
			AddLabel(120, 400, 1152, @"Clear One Note");

            for (int i = 0; i < size; ++i)
            {
                try
                {
                    if (Notes[i] != 0)
                    {
                        int y = 1;

                        if (i < 20) y = 0;
                        if (i > 39) y = 2;

                        int level = 0;

                        if (y == 0) level = 78 + (i * 20);
                        if (y == 1) level = 78 + ((i - 20) * 20);
                        if (y == 2) level = 78 + ((i - 40) * 20);

                        switch (Notes[i])
                        {
                            case 1028: { AddLabel(level, 290 + (y * 20), 1152, @"C"); } break;
                            case 1033: { AddLabel(level, 290 + (y * 20), 1152, @"D"); } break;
                            case 1038: { AddLabel(level, 290 + (y * 20), 1152, @"E"); } break;
                            case 1040: { AddLabel(level, 290 + (y * 20), 1152, @"F"); } break;
                            case 1044: { AddLabel(level, 290 + (y * 20), 1152, @"G"); } break;
                            case 1021: { AddLabel(level, 290 + (y * 20), 1152, @"A"); } break;
                            case 1025: { AddLabel(level, 290 + (y * 20), 1152, @"B"); } break;
                            case 1029: { AddLabel(level, 290 + (y * 20), 1152, @"C"); } break;
                            case 1034: { AddLabel(level, 290 + (y * 20), 1152, @"D"); } break;
                            case 1039: { AddLabel(level, 290 + (y * 20), 1152, @"E"); } break;
                            case 1041: { AddLabel(level, 290 + (y * 20), 1152, @"F"); } break;
                            case 1045: { AddLabel(level, 290 + (y * 20), 1152, @"G"); } break;
                            case 1022: { AddLabel(level, 290 + (y * 20), 1152, @"A"); } break;
                            case 1026: { AddLabel(level, 290 + (y * 20), 1152, @"B"); } break;
                            case 1030: { AddLabel(level, 290 + (y * 20), 1152, @"C"); } break;
                            case 1031: { AddLabel(level, 290 + (y * 20), 1152, @"D#"); } break;
                            case 1036: { AddLabel(level, 290 + (y * 20), 1152, @"C#"); } break;
                            case 1042: { AddLabel(level, 290 + (y * 20), 1152, @"E#"); } break;
                            case 1046: { AddLabel(level, 290 + (y * 20), 1152, @"F#"); } break;
                            case 1023: { AddLabel(level, 290 + (y * 20), 1152, @"G#"); } break;
                            case 1032: { AddLabel(level, 290 + (y * 20), 1152, @"C#"); } break;
                            case 1037: { AddLabel(level, 290 + (y * 20), 1152, @"D#"); } break;
                            case 1043: { AddLabel(level, 290 + (y * 20), 1152, @"E#"); } break;
                            case 1047: { AddLabel(level, 290 + (y * 20), 1152, @"F#"); } break;
                            case 1024: { AddLabel(level, 290 + (y * 20), 1152, @"G#"); } break;
                        }
                    }
                }
                catch { }
            }
           
		}
		
		public int notetoplay = 0;
		public Mobile player;
		
		public virtual void PlaySound()
		{
			player.PlaySound( Notes[notetoplay] );
			++notetoplay;
			if (notetoplay != Size)
				Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), new TimerCallback(PlaySound));
		}
		
		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile m = sender.Mobile;
			player = m;
			
			if (m == null)
				return;

            try
            {
                switch (info.ButtonID)
                {
                    case 1: { m.PlaySound(1028); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 2: { m.PlaySound(1033); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 3: { m.PlaySound(1038); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 4: { m.PlaySound(1040); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 5: { m.PlaySound(1044); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 6: { m.PlaySound(1021); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 7: { m.PlaySound(1025); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 8: { m.PlaySound(1029); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 9: { m.PlaySound(1034); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 10: { m.PlaySound(1039); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 11: { m.PlaySound(1041); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 12: { m.PlaySound(1045); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 13: { m.PlaySound(1022); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 14: { m.PlaySound(1026); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 15: { m.PlaySound(1030); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 16: { m.PlaySound(1031); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 17: { m.PlaySound(1036); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 18: { m.PlaySound(1042); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 19: { m.PlaySound(1046); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 20: { m.PlaySound(1023); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 21: { m.PlaySound(1032); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 22: { m.PlaySound(1037); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 23: { m.PlaySound(1043); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 24: { m.PlaySound(1047); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 25: { m.PlaySound(1024); m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 26: { if (Size < 60) { Notes[Size] = 1028; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 27: { if (Size < 60) { Notes[Size] = 1033; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 28: { if (Size < 60) { Notes[Size] = 1038; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 29: { if (Size < 60) { Notes[Size] = 1040; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 30: { if (Size < 60) { Notes[Size] = 1044; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 31: { if (Size < 60) { Notes[Size] = 1021; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 32: { if (Size < 60) { Notes[Size] = 1025; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 33: { if (Size < 60) { Notes[Size] = 1029; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 34: { if (Size < 60) { Notes[Size] = 1034; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 35: { if (Size < 60) { Notes[Size] = 1039; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 36: { if (Size < 60) { Notes[Size] = 1041; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 37: { if (Size < 60) { Notes[Size] = 1045; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 38: { if (Size < 60) { Notes[Size] = 1022; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 39: { if (Size < 60) { Notes[Size] = 1026; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 40: { if (Size < 60) { Notes[Size] = 1030; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 41: { if (Size < 60) { Notes[Size] = 1031; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 42: { if (Size < 60) { Notes[Size] = 1036; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 43: { if (Size < 60) { Notes[Size] = 1042; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 44: { if (Size < 60) { Notes[Size] = 1046; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 45: { if (Size < 60) { Notes[Size] = 1023; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 46: { if (Size < 60) { Notes[Size] = 1032; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 47: { if (Size < 60) { Notes[Size] = 1037; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 48: { if (Size < 60) { Notes[Size] = 1043; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 49: { if (Size < 60) { Notes[Size] = 1047; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 50: { if (Size < 60) { Notes[Size] = 1024; m.PlaySound(Notes[Size]); ++Size; } m.SendGump(new MusicGump(Notes, Size)); } break;
                    case 51: { m.SendGump(new MusicGump(Notes, 0)); } break;
                    case 52: { m.SendGump(new MusicGump(Notes, (Size - 1))); } break;
                    case 53: { if (Size != 0) Timer.DelayCall(TimeSpan.FromSeconds(0.0), new TimerCallback(PlaySound)); } break;
                }
            }

            catch { }
		}
	}
}
