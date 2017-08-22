using System;
using System.Collections;
using System.IO;
using System.Xml;
using Server;
using Server.Accounting;
using Server.Targeting;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Gumps;
using Server.Teiravon;
using Server.Spells;
using Server.Network;
using Server.Regions;
using SpawnMapsE = Server.Teiravon.SpawnMaps.Maps;
using Server.Engines.Craft;

namespace Teiravon.Scripts.Teiravon.GM
{
    public class SeasonController
    {
        public static void Initialize()
        {
            Server.Commands.Register("SeasonChange", AccessLevel.Administrator, new CommandEventHandler(SeasonChange_OnCommand));
        }

        [Usage("SeasonChange")]
        [Description("Sets all maps to the designated integer season")]
        private static void SeasonChange_OnCommand(CommandEventArgs e)
        {
            int x = 1;
            if (e.Length >= 1)
            {
                x = e.GetInt32(0);
                Map.Felucca.Season = x;
                Map.Trammel.Season = x;
                Map.Ilshenar.Season = x;
            }
            else
            {
                e.Mobile.SendMessage("Seasons: {0}, {1}, {2}", Map.Felucca.Season, Map.Trammel.Season, Map.Ilshenar.Season);
            }
        }
    }
}
