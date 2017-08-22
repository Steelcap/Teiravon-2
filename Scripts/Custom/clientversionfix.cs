/*
 * ** modified from the RunUO 1.0 clientversion packethandler
 * */

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Net;
using Server.Accounting;
using Server.Gumps;
using Server.Targeting;
using Server.Items;
using Server.Menus;
using Server.Mobiles;
using Server.Movement;
using Server.Prompts;
using Server.HuePickers;
using Server.ContextMenus;
using CV = Server.ClientVersion;

namespace Server.Network
{
	public class ClientVersionFix
	{
		public static void GetClientVersion(string fmt, ref int m_Major, ref int m_Minor, ref int m_Revision, ref int m_Patch, ref ClientType m_Type)
		{

			try
			{
				//Format: Major.Minor.Revision[Patch] [Type]
				//Exmple: 4.0.10a UO3D
				fmt = fmt.ToLower();

				string[] fmts = fmt.Split( new char[] {'.'}, 3 );
				string patch = fmts[2];

				int letter_index;
				for (letter_index = 0; letter_index < patch.Length; letter_index++)
					if (!Char.IsNumber(patch[letter_index]))
						break;

				m_Major = int.Parse( fmts[0] );
				m_Minor = int.Parse( fmts[1] );

				//m_Revision = int.Parse( patch.Substring( 0, letter_index + 1 ) );
				m_Revision = int.Parse(patch.Substring(0, letter_index));

				//if (Char.IsLetter(patch[letter_index]))
				if (letter_index < patch.Length && Char.IsLetter(patch[letter_index]))
					m_Patch = (patch[letter_index] - 'a') + 1;
				else
					m_Patch = 0;

				if ( patch.IndexOf( "god" ) >= 0 || patch.IndexOf( "gq" ) >= 0 )
					m_Type = ClientType.God;
				else if ( patch.IndexOf( "third dawn" ) >= 0 || patch.IndexOf( "uo:td" ) >= 0 || patch.IndexOf( "uotd" ) >= 0 || patch.IndexOf( "uo3d" ) >= 0 || patch.IndexOf( "uo:3d" ) >= 0 )
					m_Type = ClientType.UOTD;
				else
					m_Type = ClientType.Regular;
			}
			catch
			{
				m_Major = 0;
				m_Minor = 0;
				m_Revision = 0;
				m_Patch = 0;
				m_Type = ClientType.Regular;
			}
		}

		public static void Initialize()
		{
			PacketHandlers.Register(0xBD, 0, true, new OnPacketReceive(ClientVersion));
		}

		public static void ClientVersion(NetState state, PacketReader pvSrc)
		{
			string versionstring = pvSrc.ReadString();
			int m_Major = 0;
			int m_Minor = 0;
			int m_Revision = 0;
			int m_Patch = 0;
			ClientType m_Type = ClientType.Regular;

			// fixed client version parsing
			GetClientVersion(versionstring, ref m_Major, ref m_Minor, ref m_Revision, ref m_Patch, ref m_Type);

			CV version = state.Version = new ClientVersion(m_Major, m_Minor, m_Revision, m_Patch, m_Type);

			string kickMessage = null;

			if (CV.Required != null && version < CV.Required)
			{
				kickMessage = String.Format("This server requires your client version be at least {0}.", CV.Required);
			}
			else if (!CV.AllowGod || !CV.AllowRegular || !CV.AllowUOTD)
			{
				if (!CV.AllowGod && version.Type == ClientType.God)
					kickMessage = "This server does not allow god clients to connect.";
				else if (!CV.AllowRegular && version.Type == ClientType.Regular)
					kickMessage = "This server does not allow regular clients to connect.";
				else if (!CV.AllowUOTD && version.Type == ClientType.UOTD)
					kickMessage = "This server does not allow UO:TD clients to connect.";

				if (!CV.AllowGod && !CV.AllowRegular && !CV.AllowUOTD)
				{
					kickMessage = "This server does not allow any clients to connect.";
				}
				else if (CV.AllowGod && !CV.AllowRegular && !CV.AllowUOTD && version.Type != ClientType.God)
				{
					kickMessage = "This server requires you to use the god client.";
				}
				else if (kickMessage != null)
				{
					if (CV.AllowRegular && CV.AllowUOTD)
						kickMessage += " You can use regular or UO:TD clients.";
					else if (CV.AllowRegular)
						kickMessage += " You can use regular clients.";
					else if (CV.AllowUOTD)
						kickMessage += " You can use UO:TD clients.";
				}
			}

			if (kickMessage != null)
			{
				state.Mobile.SendMessage(0x22, kickMessage);
				state.Mobile.SendMessage(0x22, "You will be disconnected in {0} seconds.", CV.KickDelay.TotalSeconds);

				new KickTimer(state, CV.KickDelay).Start();
			}
		}

		private class KickTimer : Timer
		{
			private NetState m_State;

			public KickTimer(NetState state, TimeSpan delay)
				: base(delay)
			{
				m_State = state;
			}

			protected override void OnTick()
			{
				if (m_State.Socket != null)
				{
					Console.WriteLine("Client: {0}: Disconnecting, bad version", m_State);
					m_State.Dispose();
				}
			}
		}
	}

}