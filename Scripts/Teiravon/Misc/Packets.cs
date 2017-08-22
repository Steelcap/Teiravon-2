using System;
using System.Collections;
using System.IO;
using System.Net;
using Server.Accounting;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Menus;
using Server.Menus.ItemLists;
using Server.Menus.Questions;
using Server.Prompts;
using Server.HuePickers;
using Server.ContextMenus;

namespace Server.Network
{
	public sealed class SpeedBoost : Packet
	{
		public static readonly Packet Enabled = new SpeedBoost( true );
		public static readonly Packet Disabled = new SpeedBoost( false );

		public static Packet Instantiate( bool enable )
		{
			return ( enable ? Enabled : Disabled );
		}

		public SpeedBoost( bool enable )
			: base( 0xBF )
		{
			EnsureCapacity( 3 );

			m_Stream.Write( ( short )0x26 );
			m_Stream.Write( ( bool )enable );
		}
	}
}