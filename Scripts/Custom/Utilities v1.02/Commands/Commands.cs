using System;
using System.Collections;
using System.IO;
using Server;
using Server.Scripts.Commands;

namespace Knives.Utils
{
	public class Commands
	{
		public static void Initialize()
		{
			Server.Commands.Register( "Commands", AccessLevel.Administrator, new CommandEventHandler( OnCommands ) );
			EventSink.Login += new LoginEventHandler( OnLogin );
		}

		private static void OnCommands( CommandEventArgs e )
		{
			CommandsGump.SendTo( e.Mobile );
		}

		private static Hashtable s_Defaults = new Hashtable();
		private static ArrayList s_InitInfo = new ArrayList();
		private static bool s_Inited;

		public static bool HasMod( string command )
		{
			return s_Defaults[command] != null && Server.Commands.Entries[command] != null;
		}

		public static void RestoreCommand( string command )
		{try{

			if ( !HasMod( command ) )
			{
				s_Defaults.Remove( command );
				Server.Commands.Entries.Remove( command );
				return;
			}

			DefaultInfo info = (DefaultInfo)s_Defaults[command];
			CommandEntry entry = new CommandEntry( info.OldCommand, ((CommandEntry)Server.Commands.Entries[command]).Handler, info.OldAccess );
			Server.Commands.Entries.Remove( command );

			if ( HasMod( info.OldCommand ) )
				RestoreCommand( info.OldCommand );

			Server.Commands.Entries[info.OldCommand] = entry;

			s_Defaults.Remove( command );

			foreach( BaseCommandImplementor imp in BaseCommandImplementor.Implementors )
				foreach( string str in new ArrayList( imp.Commands.Keys ) )
					if ( str == command )
					{
						imp.Commands[info.OldCommand] = imp.Commands[str];
						((BaseCommand)imp.Commands[str]).AccessLevel = info.OldAccess;

						if ( str != info.OldCommand )
							imp.Commands.Remove( str );
					}

		}catch{ Errors.Report( "Commands-> RestoreDefault" ); } }

		public static void ApplyCommand( string old, string txt )
		{try{

			if ( Server.Commands.Entries[txt] != null
			|| Server.Commands.Entries[old] == null )
				return;

			if ( HasMod( old ) )
			{
				((DefaultInfo)s_Defaults[old]).NewCommand = txt;
				s_Defaults[txt] = s_Defaults[old];
				s_Defaults.Remove( old );
			}
			else
			{
				DefaultInfo info = new DefaultInfo();
				info.OldCommand = old;
				info.NewCommand = txt;
				info.NewAccess = ((CommandEntry)Server.Commands.Entries[old]).AccessLevel;
				info.OldAccess = info.NewAccess;
				s_Defaults[txt] = info;
			}

			Server.Commands.Entries[txt] = Server.Commands.Entries[old];
			Server.Commands.Entries.Remove( old );

			foreach( BaseCommandImplementor imp in BaseCommandImplementor.Implementors )
				foreach( string str in new ArrayList( imp.Commands.Keys ) )
					if ( str == old )
					{
						imp.Commands[txt] = imp.Commands[str];
						imp.Commands.Remove( str );
					}

		}catch{ Errors.Report( "Commands-> ApplyCommand" ); } }

		public static void Access( string command, int num )
		{try{

			if ( Server.Commands.Entries[command] == null )
				return;

			DefaultInfo info = new DefaultInfo();

			if ( !HasMod( command ) )
			{
				info = new DefaultInfo();
				info.OldCommand = command;
				info.NewCommand = command;
				info.NewAccess = ((CommandEntry)Server.Commands.Entries[command]).AccessLevel+num;
				info.OldAccess = ((CommandEntry)Server.Commands.Entries[command]).AccessLevel;
				s_Defaults[command] = info;
			}
			else
			{
				info = (DefaultInfo)s_Defaults[command];
				info.NewAccess = info.NewAccess+num;
			}

			CommandEntry entry = new CommandEntry( command, ((CommandEntry)Server.Commands.Entries[command]).Handler, info.NewAccess );
			Server.Commands.Entries[command] = entry;

			foreach( BaseCommandImplementor imp in BaseCommandImplementor.Implementors )
				foreach( string str in new ArrayList( imp.Commands.Keys ) )
					if ( str == command )
						((BaseCommand)imp.Commands[str]).AccessLevel = info.NewAccess;

		}catch{ Errors.Report( "Commands-> Access-> Int" ); } }

		public static void Access( string command, AccessLevel level )
		{try{

			if ( Server.Commands.Entries[command] == null )
				return;

			DefaultInfo info = new DefaultInfo();

			if ( !HasMod( command ) )
			{
				info = new DefaultInfo();
				info.OldCommand = command;
				info.NewCommand = command;
				info.NewAccess = level;
				info.OldAccess = ((CommandEntry)Server.Commands.Entries[command]).AccessLevel;
				s_Defaults[command] = info;
			}
			else
			{
				info = (DefaultInfo)s_Defaults[command];
				info.NewAccess = level;
			}

			CommandEntry entry = new CommandEntry( command, ((CommandEntry)Server.Commands.Entries[command]).Handler, info.NewAccess );
			Server.Commands.Entries[command] = entry;

			foreach( BaseCommandImplementor imp in BaseCommandImplementor.Implementors )
				foreach( string str in new ArrayList( imp.Commands.Keys ) )
					if ( str == command )
						((BaseCommand)imp.Commands[str]).AccessLevel = info.NewAccess;

		}catch{ Errors.Report( "Commands-> Access-> AccessLevel" ); } }

		public static void Configure()
		{
			EventSink.WorldLoad += new WorldLoadEventHandler( OnLoad );
			EventSink.WorldSave += new WorldSaveEventHandler( OnSave );
		}

		private static void OnSave( WorldSaveEventArgs e )
		{try{

			if ( !Directory.Exists( "Saves/Commands/" ) )
				Directory.CreateDirectory( "Saves/Commands/" );

			GenericWriter writer = new BinaryFileWriter( Path.Combine( "Saves/Commands/", "Commands.bin" ), true );

			writer.Write( 0 ); // version

			ArrayList list = new ArrayList( s_Defaults.Values );

			writer.Write( list.Count );

			foreach( DefaultInfo info in list )
			{
				writer.Write( info.NewCommand );
				writer.Write( info.OldCommand );
				writer.Write( (int)info.NewAccess );
			}

			writer.Close();

		}catch{ Errors.Report( "Commands-> OnSave" ); } }

		private static void OnLoad()
		{try{

			if ( !File.Exists( Path.Combine( "Saves/Commands/", "Commands.bin" ) ) )
				return;

			using ( FileStream bin = new FileStream( Path.Combine( "Saves/Commands/", "Commands.bin" ), FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				GenericReader reader = new BinaryFileReader( new BinaryReader( bin ) );

				int version = reader.ReadInt();

				int count = reader.ReadInt();

				object[] obj;
				for( int i = 0; i < count; ++i )
				{
					obj = new object[3];
					obj[0] = reader.ReadString();
					obj[1] = reader.ReadString();
					obj[2] = reader.ReadInt();
					s_InitInfo.Add( obj );
				}
			}
		}catch{ Errors.Report( "Commands-> OnLoad" ); } }

		private static void OnLogin( LoginEventArgs e )
		{
			if ( s_Inited )
				return;

			s_Inited = true;

			foreach( object[] obj in s_InitInfo )
			{
				ApplyCommand( obj[1].ToString(), obj[0].ToString() );
				Access( obj[0].ToString(), (AccessLevel)obj[2] );
			}
		}

		private class DefaultInfo
		{
			private string c_NewCommand;
			private string c_OldCommand;
			private AccessLevel c_NewAccess;
			private AccessLevel c_OldAccess;

			public string NewCommand{ get{ return c_NewCommand; } set{ c_NewCommand = value; } }
			public string OldCommand{ get{ return c_OldCommand; } set{ c_OldCommand = value; } }
			public AccessLevel NewAccess{ get{ return c_NewAccess; } set{ c_NewAccess = value; } }
			public AccessLevel OldAccess{ get{ return c_OldAccess; } set{ c_OldAccess = value; } }

			public DefaultInfo()
			{
			}
		}
	}
}