using System;
using System.Collections;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using Server;
using Server.Scripts.Commands;

namespace Teiravon
{
	public class LoadScript
	{
		public static void Initialize()
		{
			Commands.Register( "LoadScript", AccessLevel.Administrator, new CommandEventHandler( LoadScript_OnCommand ) );
		}

		public static void LoadScript_OnCommand( CommandEventArgs args )
		{
			try
			{
				string path = Path.Combine( String.Format( @"{0}\To Load\", Core.BaseDirectory ), args.ArgString );

				if ( File.Exists( path ) )
					Compile( false, args.Mobile, path );
				else
					args.Mobile.SendMessage( "Scripts: File does not exist." );
			}
			catch ( Exception e )
			{
				args.Mobile.SendMessage( "Scripts: Error loading file." );
			}
		}

		public static bool Compile( bool debug, Mobile from, string file )
		{
			CSharpCodeProvider provider = new CSharpCodeProvider();
			ICodeCompiler compiler = provider.CreateCompiler();

			CompilerParameters parms = new CompilerParameters( Server.ScriptCompiler.GetReferenceAssemblies(), null, false );
			parms.GenerateExecutable = false;
			parms.GenerateInMemory = true;

			from.SendMessage( "Compiling C# scripts..." );
			CompilerResults csResults = compiler.CompileAssemblyFromFile( parms, file );

			if ( csResults.Errors.Count > 0 )
			{
				int errorCount = 0, warningCount = 0;

				foreach ( CompilerError e in csResults.Errors )
				{
					if ( e.IsWarning )
						++warningCount;
					else
						++errorCount;
				}

				if ( errorCount > 0 )
				{
					from.SendMessage( "Failed to load: ({0} errors, {1} warnings)\nDetails are on the console.", errorCount, warningCount );
					Console.WriteLine( "Errors Loading Script:" );
				}
				else
					from.SendMessage( "Loaded successfully: ({0} errors, {1} warnings)", errorCount, warningCount );

				foreach ( CompilerError e in csResults.Errors )
				{
					Console.WriteLine( " - {0}: {1}: {2}: (line {3}, column {4}) {5}", e.IsWarning ? "Warning" : "Error", e.FileName, e.ErrorNumber, e.Line, e.Column, e.ErrorText );
				}
			}
			else
			{
				from.SendMessage( "Scripts: Done. (0 errors, 0 warnings)" );
			}

			if ( ( csResults == null || !csResults.Errors.HasErrors ) && ( csResults != null ) )
			{
				ArrayList temp = new ArrayList();

				foreach ( Assembly assembly in Server.ScriptCompiler.Assemblies )
					temp.Add( assembly );

				if ( csResults != null )
					temp.Add( csResults.CompiledAssembly );

				ScriptCompiler.Assemblies = ( Assembly[] )temp.ToArray( typeof( Assembly ) );

				int a = ScriptCompiler.Assemblies.Length - 1;

				ArrayList invoke = new ArrayList();

				Type[] types = Server.ScriptCompiler.Assemblies[ a ].GetTypes();

				for ( int i = 0; i < types.Length; ++i )
					from.SendMessage( "Scripts: Loaded {0}", types[ i ].Name );

				for ( int i = 0; i < types.Length; ++i )
				{
					MethodInfo m = types[ i ].GetMethod( "Configure", BindingFlags.Static | BindingFlags.Public );

					if ( m != null )
						invoke.Add( m );
				}

				invoke.Sort( new CallPriorityComparer() );

				for ( int i = 0; i < invoke.Count; ++i )
					( ( MethodInfo )invoke[ i ] ).Invoke( null, null );

				invoke.Clear();

				Type[] typess = Server.ScriptCompiler.Assemblies[ a ].GetTypes();

				for ( int i = 0; i < typess.Length; ++i )
				{
					MethodInfo m = typess[ i ].GetMethod( "Initialize", BindingFlags.Static | BindingFlags.Public );

					if ( m != null )
						invoke.Add( m );
				}

				invoke.Sort( new CallPriorityComparer() );

				for ( int i = 0; i < invoke.Count; ++i )
					( ( MethodInfo )invoke[ i ] ).Invoke( null, null );

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}