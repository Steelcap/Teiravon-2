using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.BulkOrders;
using Server.Scripts.Commands;

namespace Server.PlaneWalk
{
	public class MazeTile : Item
	{
		#region Command Handler
		public static void Initialize()
		{
			Commands.Register( "GenerateMaze", AccessLevel.Administrator, new CommandEventHandler( GenerateMaze_OnCommand ) );
			Commands.Register( "DeleteMaze", AccessLevel.Administrator, new CommandEventHandler( DeleteMaze_OnCommand ) );
		}

		public static void DeleteMaze_OnCommand( CommandEventArgs args )
		{
			ArrayList todelete = new ArrayList();
			int uid = -1;
			int count = 0;

			try
			{
				uid = args.GetInt32( 0 );
			}
			catch
			{
				args.Mobile.SendMessage( "Error reading UID" );
				return;
			}

			foreach ( Item item in World.Items.Values )
			{
				if ( item is MazeTile && ( ( MazeTile )item ).UID == uid )
				{
					todelete.Add( item );
					count++;
				}
			}

			for ( int i = 0; i < todelete.Count; i++ )
			{
				( ( Item )todelete[ i ] ).Delete();
			}

			args.Mobile.SendMessage( "{0} items deleted.", count );
		}

		public static void GenerateMaze_OnCommand( CommandEventArgs args )
		{
			FileStream file = null;

			try
			{
				File.OpenRead( Path.Combine( Core.BaseDirectory, "maze.bmp" ) );
			}
			catch
			{
				args.Mobile.SendMessage( "Error opening file." );
				return;
			}

			Bitmap maze = new Bitmap( file );
			int count = 0;
			int uid = 0;
			bool first = true;

			for ( int i = 0; i < maze.Width; i++ )
			{
				for ( int j = 0; j < maze.Height; j++ )
				{
					if ( maze.GetPixel( i, j ) == Color.FromArgb( 0, 0, 255 ) || maze.GetPixel( i,j ) == Color.FromArgb( 0,0,0 ) )
					{
						MazeTile tile = new MazeTile();
						tile.MoveToWorld( new Point3D( args.Mobile.X + i, args.Mobile.Y + j, args.Mobile.Z ), args.Mobile.Map );

						if ( first )
						{
							uid = tile.Serial.Value;
							first = false;
						}

						tile.UID = uid;

						count++;
					}
				}
			}

			args.Mobile.SendMessage( "{0} tiles created", count );

			file.Close();
		}
		#endregion

		private int m_UID = 0;

		[CommandProperty( AccessLevel.GameMaster)]
		public int UID { get { return m_UID; } set { if ( value < 0 ) value = 0;  m_UID = value; } }

		public override bool HandlesOnMovement
		{
			get
			{
				return true;
			}
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !m.InRange( Location, 1 ) )
				return;

			if ( ( m.AccessLevel == AccessLevel.Player || ( m.AccessLevel > AccessLevel.Player && !m.Hidden ) ) )
				Visible = true;
			else
				Visible = false;
		}

		[Constructable]
		public MazeTile()
			: base( 1181 )
		{
			Visible = false;
			Movable = false;
		}

		public MazeTile( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( 1 ); //version
			writer.Write( UID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			UID = reader.ReadInt();
		}
	}
}