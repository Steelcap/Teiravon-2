using System;
using Server;

namespace Server.Misc
{
	public class ClientVerification
	{
		public static void Initialize()
		{
			//ClientVersion.Required = null;
			//ClientVersion.Required = new ClientVersion( "3.0.8q" );
			ClientVersion.Required = new ClientVersion( "4.0.4" );

			ClientVersion.AllowGod = false;
			ClientVersion.AllowUOTD = false;
			ClientVersion.AllowRegular = true;

			ClientVersion.KickDelay = TimeSpan.FromSeconds( 10.0 );
		}
	}
}