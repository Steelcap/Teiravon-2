using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
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
using Server.Engines.Craft;

namespace Server.Scripts.Commands
{
	public class TempCommands
	{
		public static void Initialize()
		{
			Server.Commands.Register( "DeleteSpawners", AccessLevel.Administrator, new CommandEventHandler( DeleteSpawners_OnCommand ) );
			Server.Commands.Register( "DeleteBannedPlayers", AccessLevel.Administrator, new CommandEventHandler( DeleteBannedPlayers_OnCommand ) );
		}

		private static void DeleteBannedPlayers_OnCommand( CommandEventArgs e )
		{
			ArrayList todel = new ArrayList();

			foreach ( Mobile m in World.Mobiles.Values )
			{
				if ( ( m is TeiravonMobile ) && m.Account != null && ((Account)m.Account).Banned == true )
					todel.Add( m );
			}

			e.Mobile.SendMessage( "Deleting {0} banned mobiles.", todel.Count );

			for ( int i = 0; i < todel.Count; i++ )
			{
				Mobile m = todel[ i ] as Mobile;

				if ( m != null )
					m.Delete();
			}
		}

		private static void DeleteSpawners_OnCommand( CommandEventArgs e )
		{
			ArrayList todel = new ArrayList();
			int skip = 0;

			foreach ( Item i in World.Items.Values )
			{
				if ( ( i is Spawner ) && !( i is CustomNPCSpawner ) )
					todel.Add( i );
				else if ( i is CustomNPCSpawner )
					skip++;
			}

			e.Mobile.SendMessage( "Deleting {0} spawners. ({1} skipped custom npc spawners.", todel.Count, skip );

			for ( int i = 0; i < todel.Count; i++ )
				( ( Item )todel[i] ).Delete();
		}
	}
}