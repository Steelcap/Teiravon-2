using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Scripts.Commands;

namespace Server.Items
{
    public class BreakableTile : Item
    {
        private int m_Hits = 150;
        private int m_MaxHits = 500;
        private int m_MinHits = 250;
        private TimeSpan m_RegenDelay = TimeSpan.FromMinutes( 30.0 );
        private DateTime m_StartTime = DateTime.MinValue;
        private DateTime m_EndTime = DateTime.MinValue;
        private Point3D m_OldLocation = new Point3D( 1, 1, 1 );
        private Map m_OldMap = Map.Felucca;

        [CommandProperty( AccessLevel.GameMaster )]
        public int Hits { get { return m_Hits; } set { m_Hits = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int MaxHits { get { return m_MaxHits; } set { m_MaxHits = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int MinHits { get { return m_MinHits; } set { m_MinHits = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public TimeSpan RegenDelay { get { return m_RegenDelay; } set { m_RegenDelay = value; } }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public DateTime EndTime { get { return m_EndTime; } set { m_EndTime = value; } }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public DateTime StartTime { get { return m_StartTime; } set { m_StartTime = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public Point3D OldLocation { get { return m_OldLocation; } set { m_OldLocation = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public Map OldMap { get { return m_OldMap; } set { m_OldMap = value; } }

        public void Damage( PotionEffect effect )
        {
            switch ( effect )
            {
                case PotionEffect.Explosion:
                    Damage( 15 );
                    break;

                case PotionEffect.ExplosionLesser:
                    Damage( 10 );
                    break;

                case PotionEffect.ExplosionGreater:
                    Damage( 20 );
                    break;

                default:
                    Damage( 0 );
                    break;
            }
        }

        public void Damage( int damage )
        {
            if ( m_Hits - damage < 1 )
            {
                Effects.PlaySound( Location, Map, 0x207 );
                Effects.SendLocationEffect( Location, Map, 0x36BD, 20 );

                m_StartTime = DateTime.Now;
                m_EndTime = m_StartTime + m_RegenDelay;
                m_OldLocation = Location;
                m_OldMap = Map;

                MoveToWorld( new Point3D( 1, 1, 1 ), Map.Felucca );
                Visible = false;

                InternalTimer timer = new InternalTimer( m_RegenDelay, this );
                timer.Start();

                return;
            }

            Effects.PlaySound( Location, Map, 0x207 );

            m_Hits -= damage;
        }

        public void Rebuild()
        {
            Visible = true;
            MoveToWorld( OldLocation, OldMap );
            Hits = Utility.RandomMinMax( MinHits, MaxHits );
        }

        public override void OnDoubleClick( Mobile from )
        {
            double percent = ( (double)m_Hits / (double)m_MaxHits ) * 100.0;

            percent = Math.Round( percent );
            
            if ( percent >= 75.0 )
                from.SendMessage( "It looks solid. [" + percent + "]"  );
            else if ( percent >= 50.0 )
                from.SendMessage( "It's slightly damaged. [" + percent + "]" );
            else if ( percent >= 25.0 )
                from.SendMessage( "It's badly damaged. [" + percent + "]" );
            else if ( percent <= 24.9 )
                from.SendMessage( "It's falling apart! [" + percent + "]" );

            return;
        }

        [Constructable]
        public BreakableTile( int itemid )
            : base( itemid )
        {
            Hits = Utility.RandomMinMax( MinHits, MaxHits );
            Movable = false;
        }

        [Constructable]
        public BreakableTile()
            : base( 1315 )
        {
            MaxHits = Utility.RandomMinMax( 250, 500 );
            Hits = MaxHits;
            Movable = false;
        }

        public BreakableTile( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( ( int )m_Hits );
            writer.Write( ( int )m_MaxHits );
            writer.Write( ( int )m_MinHits );
            writer.Write( ( TimeSpan )m_RegenDelay );
            writer.Write( ( DateTime )m_EndTime );
            writer.Write( ( DateTime )m_StartTime );
            writer.Write( ( Point3D )m_OldLocation );
            writer.Write( ( Map )m_OldMap );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            m_Hits = reader.ReadInt();
            m_MaxHits = reader.ReadInt();
            m_MinHits = reader.ReadInt();
            m_RegenDelay = reader.ReadTimeSpan();
            m_EndTime = reader.ReadDateTime();
            m_StartTime = reader.ReadDateTime();
            m_OldLocation = reader.ReadPoint3D();
            m_OldMap = reader.ReadMap();

            if ( m_Hits <= 0 && m_EndTime.Subtract( DateTime.Now ) > TimeSpan.Zero )
            {
                InternalTimer timer = new InternalTimer( m_EndTime.Subtract( DateTime.Now ), this );
                timer.Start();
            }
        }

        private class InternalTimer : Timer
        {
            BreakableTile m_Tile = null;

            public InternalTimer( TimeSpan delay, BreakableTile tile )
                : base( delay )
            {
                Priority = TimerPriority.OneSecond;
                m_Tile = tile;
            }

            protected override void OnTick()
            {
                m_Tile.Rebuild();

                return;
            }
        }

        public class ConvertCommand
        {
            public static void Initialize()
            {
                Server.Commands.Register( "ConvertTile", AccessLevel.GameMaster, new CommandEventHandler( ConvertTile_OnCommand ) );
            }

            [Usage( "ConvertTile" )]
            [Description( "Converts static tiles to breakable tiles" )]
            private static void ConvertTile_OnCommand( CommandEventArgs e )
            {
	    		BoundingBoxPicker.Begin( e.Mobile, new BoundingBoxCallback( ConvertTileBox_Callback ), typeof( Item ) );
    		}

            private static void ConvertTileBox_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
            {
                Rectangle2D rect = new Rectangle2D( start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1 );
                ArrayList toDelete = new ArrayList();

                int staticTotal = 0;
                int nostaticTotal = 0;

                foreach ( object item in map.GetObjectsInBounds( rect ) )
                {
                    if ( item is Static && !( item is BreakableTile ) )
                    {
                        Static oldtile = ( Static )item;

                        BreakableTile tile = new BreakableTile( oldtile.ItemID );
                        tile.Hue = oldtile.Hue;
                        tile.Name = oldtile.Name;

                        tile.MoveToWorld( oldtile.Location, oldtile.Map );
                        toDelete.Add( oldtile );

                        staticTotal++;
                    }
                    else if ( item is Item )
                        nostaticTotal++;
                }

                for ( int i = 0; i < toDelete.Count; i++ )
                    ( ( Item )toDelete[ i ] ).Delete();

                from.SendMessage( "There were {0} total items found. {0} items were converted.", nostaticTotal + staticTotal, staticTotal );
            }
        }
    }
}