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
using Server.Engines.Craft;

namespace Server.Scripts.Commands
{
	public class TAVTempCommand
	{
		public static void Initialize()
		{
			Server.Commands.Register( "FindKeys", AccessLevel.GameMaster, new CommandEventHandler( FindKeys_OnCommand ) );
		}

		public static void FindKeys_OnCommand( CommandEventArgs e )
		{
			uint keyval = 0;

			try { keyval = uint.Parse( e.ArgString ); }
			catch { e.Mobile.SendMessage( "Error" ); }

			if ( keyval <= 0 )
			{
				e.Mobile.SendMessage( "Invalid keyval." );
				return;
			}

			foreach ( Item i in World.Items.Values )
			{
				if ( ( i is KeyRing ) && ( ( KeyRing )i ).Keys != null && ( ( KeyRing )i ).Keys.Count > 0 )
				{
					KeyRing ring = ( KeyRing )i;

					foreach ( KeyInfo info in ring.Keys )
						if ( info.KeyValue == keyval )
							e.Mobile.SendMessage( "Key Found On: {0}", ring.Serial );
				}
			}
		}
	}
}