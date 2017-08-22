using System;
using Server;
using System.Collections;
using Server.Regions;
using Server.Targeting;
using Server.Items;

namespace Server.Scripts.Commands
{
	public class RegionBounds
	{
		public static void Initialize() 
		{
			Server.Commands.Register( "RegionBounds", AccessLevel.GameMaster, new CommandEventHandler( RegionBounds_OnCommand ) ); 
		}

		[Usage( "RegionBounds" )]
		[Description( "Displays the bounding area of either a targeted Mobile's region or the Bounding area of a targeted RegionControl." )]
		private static void RegionBounds_OnCommand( CommandEventArgs e ) 
		{
			e.Mobile.Target = new RegionBoundTarget();
			e.Mobile.SendMessage( "Target a Mobile or RegionControl" );
			e.Mobile.SendMessage( "Please note that Players will also be able to see the bounds of the Region." );
		}

		private class RegionBoundTarget : Target
		{
			public RegionBoundTarget() : base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if( targeted is Mobile )
				{
					Mobile m = (Mobile)targeted;

					Region r = m.Region;			

					if( r == m.Map.DefaultRegion )
					{
						from.SendMessage( "The Region is the Default region for the entire map and as such, cannot have it's bounds displayed." );
						return;
					}

					from.SendMessage( String.Format( "That Mobile's region is of type {0}, with a priority of {1}.", r.GetType().FullName, r.Priority.ToString() ));

					ShowRegionBounds( r );
				}
				else if( targeted is RegionControl )
				{
					Region r = ((RegionControl)targeted).MyRegion;

					if ( r == null || r.Coords == null || r.Coords.Count == 0 )
					{
						from.SendMessage( "Region area not defined for targeted RegionControl." );
						return;
					}

					from.SendMessage( "Displaying targeted RegionControl's Region..." );
					
					ShowRegionBounds( r );
				}
				else
				{
					from.SendMessage( "That is not a Mobile or a RegionControl" );
				}
			}
		}


		public static void ShowRectBounds( Rectangle2D r, Map m )
		{
			if( m == Map.Internal || m == null )
				return;

			Point3D p1 = new Point3D( r.X, r.Y - 1, 0 );	//So we dont' need to create a new one each point
			Point3D p2 = new Point3D( r.X, r.Y + r.Height - 1, 0 );	//So we dont' need to create a new one each point

			Effects.SendLocationEffect( new Point3D( r.X -1, r.Y - 1, m.GetAverageZ( r.X, r.Y -1 ) ) , m, 251, 75, 1, 1151, 3 );	//Top Corner	//Testing color

			for( int x = r.X; x <= ( r.X + r.Width -1 ); x++ )
			{
				p1.X = x;
				p2.X = x;

				p1.Z = m.GetAverageZ( p1.X, p1.Y );
				p2.Z = m.GetAverageZ( p2.X, p2.Y );

				Effects.SendLocationEffect( p1, m, 249, 75, 1, 1151, 3 );	//North bound
				Effects.SendLocationEffect( p2, m, 249, 75, 1, 1151, 3 );	//South bound
			}

			p1 = new Point3D( r.X -1 , r.Y -1 , 0 );
			p2 = new Point3D( r.X + r.Width - 1, r.Y, 0 );

			for( int y = r.Y; y <= ( r.Y + r.Height -1 ); y++ )
			{
				p1.Y = y;
				p2.Y = y;

				p1.Z = m.GetAverageZ( p1.X, p1.Y );
				p2.Z = m.GetAverageZ( p2.X, p2.Y );

				Effects.SendLocationEffect( p1, m, 250, 75, 1, 1151, 3 );	//West Bound
				Effects.SendLocationEffect( p2, m, 250, 75, 1, 1151, 3 );	//East Bound
			}
		}


		public static void ShowRegionBounds( Region r )
		{
			if( r == null || r.Coords == null || r.Coords.Count == 0)
				return;

			ArrayList c = r.Coords;

			for( int i = 0; i < c.Count; i++ )
			{
				if( c[i] is Rectangle2D )
					ShowRectBounds( (Rectangle2D)c[i], r.Map );
			}
		}
	}
}
