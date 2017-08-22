using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Knives.Utils
{
	public class ErrorsNotifyGump : GumpPlus
	{
		public static void SendTo( Mobile m )
		{
			new ErrorsNotifyGump( m );
		}

		public ErrorsNotifyGump( Mobile m ) : base( m, 100, 100 )
		{
			m.CloseGump( typeof( ErrorsNotifyGump ) );

			Override = false;

			NewGump();
		}

		protected override void BuildGump()
		{
			AddItem( 0, 2, 0x25C6 );
			AddButton( 33, 40, 0x1523, 0x1523, "Errors", new TimerCallback( Errors ) );
		}

		private void Errors()
		{
			ErrorsGump.SendTo( Owner );
		}
	}
}