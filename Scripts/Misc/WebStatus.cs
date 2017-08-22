using System;
using System.IO;
using System.Text;
using System.Collections;

using Server;
using Server.Network;
using Server.Guilds;

using Server.Mobiles;

using Server.Items;

namespace Server.Misc
{

    public class StatusPage : Timer
    {
        public static void Initialize()
        {
            new StatusPage().Start();
        }

        public StatusPage()
            : base(TimeSpan.FromSeconds(5.0), TimeSpan.FromSeconds(60.0))
        {
            Priority = TimerPriority.FiveSeconds;
        }

        private static string Encode(string input)
        {
            StringBuilder sb = new StringBuilder(input);

            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\"", "&quot;");
            sb.Replace("'", "&acute;");

            return sb.ToString();
        }
        private static string GetCity(Mobile from)
        {
            String City = "None";

            if (from.X > 2050 && from.Y > 850 && from.X < 2500 && from.Y < 1250)
                return "Edana";
            
            if (from.X > 2700 && from.Y > 1900 && from.X < 2950 && from.Y < 2200)
                return "Valgaris";

            if (from.X > 2730 && from.Y > 2260 && from.X < 3200 && from.Y < 2500)
                return "Burzkal";
            
            if (from.X > 3800 && from.Y > 860 && from.X < 4000 && from.Y < 1100)
                return "Arandor";

            if (from.X > 3650 && from.Y > 1600 && from.X < 3850 && from.Y < 1750)
                return "Karagard";

            if (from.X > 5200 && from.Y > 2250 && from.X < 5600 && from.Y < 2500)
                return "Karagard";

            return City;
        }

        protected override void OnTick()
        {

            using (StreamWriter op = new StreamWriter("C:\\wamp\\www\\exported.xml"))
            {
                int items = 0;

                op.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                op.WriteLine("<items>");


                foreach (Item i in XmlExporter.Exports.Keys)
                {
                    ExportController m_Export = XmlExporter.Exports[i] as ExportController;

                    if (m_Export != null)
                    {
                        op.WriteLine("<item itemname=\"{0}\" itemweight=\"{1}\" p1=\"{2}\" p2=\"{3}\" p3=\"{4}\" > {5}", m_Export.Name, m_Export.Weight, m_Export.P1, m_Export.P2, m_Export.P3, m_Export.P4);
                        op.WriteLine("</item>");
                    }
                }

                op.WriteLine("</items>");
            }

            using (StreamWriter op = new StreamWriter("C:\\wamp\\www\\status.xml"))
            {
                int hiddencount = 0;
                int listedplayers = 0;

                op.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");


                foreach (NetState state in NetState.Instances)
                {
                    TeiravonMobile m_Player = (TeiravonMobile)state.Mobile;

                    if (m_Player != null)
                    {
                        if (m_Player.AccessLevel > AccessLevel.Player && m_Player.Hidden == true)
                            continue;

                        if (!m_Player.ShowStatus)
                        {
                            hiddencount += 1;
                            continue;
                        }
                        string playerRace = "Unknown";

                        switch (m_Player.PlayerRace)
                        {
                            case TeiravonMobile.Race.Drow:
                                playerRace = "Drow";
                                break;
                            case TeiravonMobile.Race.Duergar:
                                playerRace = "Duergar";
                                break;
                            case TeiravonMobile.Race.Dwarf:
                                playerRace = "Dwarf";
                                break;
                            case TeiravonMobile.Race.Elf:
                                playerRace = "Elf";
                                break;
                            case TeiravonMobile.Race.HalfElf:
                                playerRace = "HalfElf";
                                break;
                            case TeiravonMobile.Race.Human:
                                playerRace = "Human";
                                break;
                            case TeiravonMobile.Race.Orc:
                                playerRace = "Orc";
                                break;
                            case TeiravonMobile.Race.Undead:
                                playerRace = "Undead";
                                break;
                            case TeiravonMobile.Race.Goblin:
                                playerRace = "Goblin";
                                break;
                            case TeiravonMobile.Race.Gnome:
                                playerRace = "Gnome";
                                break;
                        }


                        op.Write("<character>");
                        op.Write("<name>{0}</name>", Encode(m_Player.Name));
                        op.Write("<title>{0}</title>", Encode(m_Player.Title));
                        op.Write("<race>{0}</race>", Encode(playerRace));
                        op.Write("<description>{0}</description>", Encode(m_Player.Profile));
                        op.Write("<city>{0}</city>", Encode(GetCity(m_Player) ) );
                        op.Write("<x>{0}</x>",Encode(m_Player.X.ToString()));
                        op.Write("<y>{0}</y>", Encode(m_Player.Y.ToString()));
                        op.Write("<map>{0}</map>", Encode(m_Player.Map.ToString()));
                        op.WriteLine("</character>");
                        listedplayers++;

                    }
                }

                op.WriteLine("<hidden>{0}</hidden>", hiddencount);
                op.WriteLine("<online>{0}</online>", listedplayers);
            }

            using (StreamWriter op = new StreamWriter("C:\\Inetpub\\wwwroot\\status.html"))
            {
                int hiddencount = 0;
                int listedplayers = 0;

                op.WriteLine("<html>");
                op.WriteLine("   <head>");
                op.WriteLine("      <title>Teiravon Status</title>");
                op.WriteLine("      <link rel=\"stylesheet\" href=\"status.css\" />");
                op.WriteLine("   </head>");

                op.WriteLine("   <body>");
                op.WriteLine("   <br><br>");
                op.WriteLine("   <div id=\"main\">");
                op.WriteLine("   <table><tr>");
                op.WriteLine("      <td class=\"head\"><center><br>Online Players<br><br></center></td>");
                op.WriteLine("   </tr></table>");

                op.WriteLine("   <table>");


                foreach (NetState state in NetState.Instances)
                {
                    TeiravonMobile m_Player = (TeiravonMobile)state.Mobile;

                    if (m_Player != null)
                    {
                        if (m_Player.AccessLevel > AccessLevel.Player && m_Player.Hidden == true)
                            continue;

                        if (!m_Player.ShowStatus)
                        {
                            hiddencount += 1;
                            continue;
                        }
                        op.Write("      <tr><td>{0}</td><td>{1}</td>", Encode(m_Player.Name), Encode(m_Player.Title));
                        op.WriteLine("</tr>");
                        listedplayers++;


                    }
                }


                op.WriteLine("   </table>");

                op.WriteLine("   <table>");
                op.WriteLine("   <tr>");
                op.WriteLine("      <td class=\"empty\">&nbsp;</font></td>");
                op.WriteLine("   </tr>");

                op.WriteLine("   <tr>");
                op.WriteLine("      <td class=\"empty\">Visible Online Players: " + listedplayers.ToString() + "</td>");
                op.WriteLine("   </tr>");

                op.WriteLine("   <tr>");
                op.WriteLine("      <td class=\"empty\">&nbsp;</font></td>");
                op.WriteLine("   </tr>");


                op.WriteLine("   <tr>");
                op.WriteLine("      <td class=\"empty\">Hidden Online Players: " + hiddencount.ToString() + "</td>");
                op.WriteLine("   </tr>");

                op.WriteLine("   <tr>");
                op.WriteLine("      <td class=\"empty\">&nbsp;</font></td>");
                op.WriteLine("   </tr>");

                op.WriteLine("   <tr>");
                op.WriteLine("      <td class=\"empty\">*Note: to hide/reveal yourself from the status page use [status<br><br></td>");
                op.WriteLine("   </tr>");

                op.WriteLine("   </table>");
                op.WriteLine(" </div>");
                op.WriteLine("   </body>");
                op.WriteLine("</html>");
            }
        }
    }
}