/*
	Modified: Spawner.cs
*/
using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Menus.Questions;
using Server.Targeting;

namespace Teiravon.Dungeons
{
	#region Dungeon Control
	public class DungeonControl : Item
	{
		private ArrayList m_Spawners = new ArrayList();
		private ArrayList m_Walls = new ArrayList();
		private ArrayList m_TreasureChests = new ArrayList();
		private TimeSpan m_TimeLimit = new TimeSpan( 0, 30, 0 );
		private bool m_Active = false;

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public bool Active { get { return m_Active; } set { m_Active = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public TimeSpan TimeLimit { get { return m_TimeLimit; } set { m_TimeLimit = value; } }
		
		public ArrayList Spawners { get { return m_Spawners; } }
		public ArrayList Walls { get { return m_Walls; } }
		public ArrayList TreasureChests { get { return m_TreasureChests; } }
				
		public override bool Decays { get { return false; } }
		
		// Start & Stop: Dungeon Spawners, Dungeon Walls, Dungeon Chests
		public void Start()
		{
			m_Active = true;
			
			foreach ( DungeonSpawner ds in m_Spawners )
			{
				if ( ds.Deleted || ds == null )
					continue;

				ds.MinDelay = TimeSpan.FromSeconds( 0.0 );
				ds.MaxDelay = TimeSpan.FromSeconds( 1.0 );
								
				if ( ds.Running )
				{
					ds.RemoveCreatures();
					ds.Respawn();
				} else {
					ds.Start();
					ds.Respawn();
				}
				
				ds.MinDelay = TimeSpan.FromDays( 1.0 );
				ds.MaxDelay = TimeSpan.FromDays( 1.0 );
			}

			
			foreach( DungeonWall dw in m_Walls )
			{
				if ( dw.Deleted || dw == null )
					continue;
					
				dw.Close();
			}
			
			foreach( DungeonChestSpawner dcs in m_TreasureChests )
			{
				if ( dcs == null || dcs.Deleted )
					continue;
				
				dcs.DeleteChest();
			}
		}
		
		public void Stop()
		{
			m_Active = false;
					
			foreach ( DungeonSpawner ds in m_Spawners )
			{
				if ( ds == null || ds.Deleted )
					continue;
				
				if ( ds.Running )
				{
					ds.Stop();
					ds.RemoveCreatures();
				}
			}
			
			foreach ( DungeonWall dw in m_Walls )
			{
				if ( dw == null || dw.Deleted )
					continue;
				
				dw.Close();
			}
			
			foreach( DungeonChestSpawner dcs in m_TreasureChests )
			{
				if ( dcs == null || dcs.Deleted )
					continue;
				
				dcs.DeleteChest();
			}
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel >= AccessLevel.Counselor )
			{
				DungeonControlMenu dcm = new DungeonControlMenu( this );
				dcm.SendTo( from.NetState );
			}
		}

		[Constructable]
		public DungeonControl() : base( 5020 )
		{
			Name = "Dungeon Control";
			LootType = LootType.Blessed;
		}
		
		public DungeonControl( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.WriteItemList( m_Spawners, true );
			writer.WriteItemList( m_Walls, true );
			writer.Write( m_Active );
			writer.Write( m_TimeLimit );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			m_Spawners = reader.ReadItemList();
			m_Walls = reader.ReadItemList();
			m_Active = reader.ReadBool();
			m_TimeLimit = reader.ReadTimeSpan();
			
			InternalTimer timer = new InternalTimer( this );
			timer.Start();
		}
		
		private class InternalTimer : Timer
		{
			private DungeonControl m_Control;
			
			public InternalTimer( DungeonControl dc ) : base( dc.TimeLimit )
			{
				m_Control = dc;
                Priority = TimerPriority.TwoFiftyMS;
			}
			
			protected override void OnTick()
			{
				m_Control.Stop();
			}
		}
		
		private class DungeonControlMenu : QuestionMenu
		{
			private DungeonControl m_DungeonControl;
			private static string[] m_Options = new string[]{ "Create Key", "Create Spawner", "Create 5 Spawners", "Create Wall Spawn", "Create Treasure Chest Spawn", "Stop" };
			
			public DungeonControlMenu( DungeonControl dc ) : base( "Select an option", m_Options )
			{
				m_DungeonControl = dc;
			}
			
			public override void OnResponse( Server.Network.NetState state, int index )
			{
				switch ( index )
				{
					case 0:
						DungeonKey key = new DungeonKey((uint)m_DungeonControl.Serial.Value );
						
						state.Mobile.AddToBackpack( key );
						
						break;
						
					case 1:
						DungeonSpawner ds = new DungeonSpawner();
						ds.Controller = m_DungeonControl;
						
						m_DungeonControl.Spawners.Add( ds );
						state.Mobile.AddToBackpack( ds );
						
						break;

					case 2:
						for ( int i = 0; i < 5; i++ )
						{
							DungeonSpawner ds1 = new DungeonSpawner();
							ds1.Controller = m_DungeonControl;

							m_DungeonControl.Spawners.Add( ds1 );
							state.Mobile.AddToBackpack( ds1 );
						}
						
						break;
						
					case 3:
						DungeonWall dw = new DungeonWall();
						dw.Controller = m_DungeonControl;
						
						m_DungeonControl.Walls.Add( dw );
						state.Mobile.AddToBackpack( dw );
						
						break;
						
					case 4:
						DungeonChestSpawner dcs = new DungeonChestSpawner();
						dcs.Controller = m_DungeonControl;
						
						m_DungeonControl.TreasureChests.Add( dcs );
						state.Mobile.AddToBackpack( dcs );
						
						break;
					
					case 5:
						m_DungeonControl.Stop();
						break;
				}
			}
		}
	}
	#endregion
	
	#region Dungeon Wall
	public class DungeonWall : Item
	{
		private DungeonControl m_Control;
		private DungeonSpawner m_SpawnerLink;
		private int m_OpenId = 16104;
		private int m_ClosedId = 16104;

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public DungeonControl Controller { get { return m_Control; } set { m_Control = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public DungeonSpawner SpawnerLink { get { return m_SpawnerLink; } set { m_SpawnerLink = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public int OpenId { get { return m_OpenId; } set { m_OpenId = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public int ClosedId { get { return m_ClosedId; } set { m_ClosedId = value; } }
		
		public override bool Decays { get { return false; } }
		
		public void Open()
		{
			ItemID = m_OpenId;
			Name = null;
		}
		
		public void Close()
		{
			ItemID = m_ClosedId;
			Name = null;
		}
		
		public bool IsEmpty()
		{
			for ( int i = 0; i < this.SpawnerLink.CurrentCreatures.Count; i++ )
			{
				if ( !( this.SpawnerLink.CurrentCreatures[ i ] is Mobile ) )
					continue;
				
				if ( !((BaseCreature)this.SpawnerLink.CurrentCreatures[ i ]).Deleted )
					return false;
			}
			
			return true;
		}
		
		public override void OnDelete()
		{
			if ( m_Control != null )
				m_Control.Walls.Remove( this );
				
			base.OnDelete();
		}

		[Constructable]
		public DungeonWall() : base( 16104 )
		{
			Name = "Dungeon Wall Control";
			Visible = true;
			
			InternalTimer timer = new InternalTimer( this );
			timer.Start();
			
			Movable = false;
		}
		
		public DungeonWall( Serial serial ) : base( serial )
		{
		}
			
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( m_Control.Serial );
			
			writer.Write( m_SpawnerLink == null ? false : true );
			
			if ( m_SpawnerLink != null )
				writer.Write( m_SpawnerLink.Serial );

			writer.Write( m_OpenId );
			writer.Write( m_ClosedId );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			m_Control = (DungeonControl)World.FindItem( (Serial)reader.ReadInt() );
			
			if ( reader.ReadBool() )
				m_SpawnerLink = (DungeonSpawner)World.FindItem( (Serial)reader.ReadInt() );
			
			m_OpenId = reader.ReadInt();
			m_ClosedId = reader.ReadInt();
			
			if ( m_Control == null )
				Delete();
			else
			{
				InternalTimer timer = new InternalTimer( this );
				timer.Start();
			}
		}
		
		private class InternalTimer : Timer
		{
			private DungeonWall m_Wall;
			
			public InternalTimer( DungeonWall dw ) : base( TimeSpan.FromSeconds( 30 ), TimeSpan.FromSeconds( 10 ) )
			{
				m_Wall = dw;
                Priority = TimerPriority.TwoFiftyMS;
			}
			
			protected override void OnTick()
			{
				if ( m_Wall.Controller == null || m_Wall.Controller.Deleted )
				{
					m_Wall.Delete();
					Stop();
				}
				else if ( !m_Wall.Controller.Active )
					m_Wall.Close();
				else if ( m_Wall.SpawnerLink != null && m_Wall.IsEmpty() )
					m_Wall.Open();
			}
		}
	}
	#endregion
	
	#region Dungeon Spawner
	public class DungeonSpawner : Spawner
	{
		private DungeonControl m_Control;

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public DungeonControl Controller { get { return m_Control; } set { m_Control = value; } }
		
		public override bool Decays { get { return false; } }
		
		public override void OnDelete()
		{
			if ( m_Control != null )
				m_Control.Spawners.Remove( this );
				
			base.OnDelete();
		}
		
		[Constructable]
		public DungeonSpawner() : base()
		{
			Name = "Dungeon Spawner";
			LootType = LootType.Blessed;
			
			ItemID = 16104;
			
			InternalTimer timer = new InternalTimer( this );
			timer.Start();
			
			Running = false;
			Movable = true;
		}
			
		public DungeonSpawner( Serial serial ) : base( serial )
		{
		}
			
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( m_Control.Serial );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			m_Control = (DungeonControl)World.FindItem( (Serial)reader.ReadInt() );
			
			if ( m_Control == null )
				Delete();
			else
			{
				InternalTimer timer = new InternalTimer( this );
				timer.Start();
			}
		}
		
		private class InternalTimer : Timer
		{
			private DungeonSpawner m_Spawner;
			
			public InternalTimer( DungeonSpawner ds ) : base( TimeSpan.FromSeconds( 30 ), TimeSpan.FromSeconds( 30 ) )
			{
				m_Spawner = ds;
                Priority = TimerPriority.TwoFiftyMS;
			}
			
			protected override void OnTick()
			{
				if ( m_Spawner.Controller == null || m_Spawner.Controller.Deleted )
				{
					m_Spawner.Delete();
					Stop();
				}
				else if ( m_Spawner.Parent != null)
					return;
				else if ( !m_Spawner.Controller.Active && m_Spawner.Running )
				{
					m_Spawner.Stop();
					m_Spawner.RemoveCreatures();
				}
			}
		}
	}
	#endregion
	
	#region Dungeon Chest Spawner
	public class DungeonChestSpawner : Item
	{
		private DungeonControl m_Control;
		private DungeonSpawner m_SpawnerLink;
		private Hashtable m_Items;
		private MetalGoldenTreasureChest m_Chest;
		private BaseTreasureChest.TreasureLevel m_ChestLevel = BaseTreasureChest.TreasureLevel.Level1;

		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public MetalGoldenTreasureChest Chest { get { return m_Chest; } set { m_Chest = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public DungeonControl Controller { get { return m_Control; } set { m_Control = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public DungeonSpawner SpawnerLink { get { return m_SpawnerLink; } set { m_SpawnerLink = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public BaseTreasureChest.TreasureLevel TreasureLevel { get { return m_ChestLevel; } set { m_ChestLevel = value; } }
		
		public override bool Decays { get { return false; } }
		
		public bool IsEmpty()
		{
			for ( int i = 0; i < this.SpawnerLink.CurrentCreatures.Count; i++ )
			{
				if ( !( this.SpawnerLink.CurrentCreatures[ i ] is Mobile ) )
					continue;
				
				if ( !((BaseCreature)this.SpawnerLink.CurrentCreatures[ i ]).Deleted )
					return false;
			}
			
			return true;
		}
		
		public void SpawnChest()
		{
			if ( m_Chest == null || m_Chest.Deleted )
			{		
				MetalGoldenTreasureChest chest = new MetalGoldenTreasureChest();
				chest.Level = m_ChestLevel;
				chest.Movable = false;
				chest.MoveToWorld( this.Location, this.Map );
			
				m_Chest = chest;
			}
		}
		
		public void DeleteChest()
		{
			if ( m_Chest != null )
				m_Chest.Delete();
			
			m_Chest = null;
		}
		
		public override void OnDelete()
		{
			if ( m_Control != null )
				m_Control.TreasureChests.Remove( this );
				
			base.OnDelete();
		}
		
		[Constructable]
		public DungeonChestSpawner() : base( 16104 )
		{
			Name = "Dungeon Chest Spawner";
			Visible = false;
			
			InternalTimer timer = new InternalTimer( this );
			timer.Start();
		}
		
		public DungeonChestSpawner( Serial serial ) : base( serial )
		{
		}
			
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( m_Control.Serial );
			
			writer.Write( m_SpawnerLink == null  || m_SpawnerLink.Deleted ? false : true );
			
			if ( m_SpawnerLink != null )
				writer.Write( m_SpawnerLink.Serial );
			
			writer.Write( m_Chest == null || m_Chest.Deleted ? false : true );
			
			if ( m_Chest != null )
				writer.Write( m_Chest.Serial );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			m_Control = (DungeonControl)World.FindItem( (Serial)reader.ReadInt() );
			
			if ( reader.ReadBool() )
				m_SpawnerLink = (DungeonSpawner)World.FindItem( (Serial)reader.ReadInt() );
			
			if ( reader.ReadBool() )
				m_Chest = (MetalGoldenTreasureChest)World.FindItem( (Serial)reader.ReadInt() );
				
			if ( m_Control == null )
				Delete();
			else
			{
				InternalTimer timer = new InternalTimer( this );
				timer.Start();
			}
		}
		
		private class InternalTimer : Timer
		{
			private DungeonChestSpawner m_Chest;
			
			public InternalTimer( DungeonChestSpawner dcs ) : base( TimeSpan.FromSeconds( 30 ), TimeSpan.FromSeconds( 10 ) )
			{
				m_Chest = dcs;
                Priority = TimerPriority.TwoFiftyMS;
			}
			
			protected override void OnTick()
			{
				if ( m_Chest.Controller == null || m_Chest.Controller.Deleted )
				{
					m_Chest.Delete();
					Stop();
				}
				else if ( !m_Chest.Controller.Active )
					m_Chest.DeleteChest();
				else if ( m_Chest.SpawnerLink != null && m_Chest.IsEmpty() )
					m_Chest.SpawnChest();
			}
		}
	}
	#endregion
	
	#region Dungeon Key
	public class DungeonKey : Key
	{
		public override void OnDoubleClick( Mobile from )
		{
			from.Target = new InternalTarget( this );
		}

		[Constructable]
		public DungeonKey() : base( KeyType.Gold )
		{
			Name = "a strange key";
		}
		
		[Constructable]
		public DungeonKey( uint serial ) : base( KeyType.Gold, serial )
		{
			Name = "a strange key";
		}
		
		public DungeonKey( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
		
		private class InternalTarget : Target
		{
			private DungeonKey m_Key;
			
			public InternalTarget( DungeonKey key ) : base( 3, false, TargetFlags.None )
			{
				m_Key = key;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is DungeonControl )
				{
					DungeonControl dc = (DungeonControl)targeted;
					
					if ( dc.Active )
						from.SendMessage( "This dungeon is already active. Try again later!" );
					else if ( dc.Serial != m_Key.KeyValue )
						from.SendMessage( "The key won't fit in the lock." );
					else 
					{
						from.SendMessage( "You place the key in the lock... and it turns!" );
						
						dc.Start();
						m_Key.Delete();
						
						InternalTimer timer = new InternalTimer( dc );
						timer.Start();
					}
				} 
				else
					from.SendMessage( "It doesn't seem to work on that." );
			}
		}
		
		private class InternalTimer : Timer
		{
			private DungeonControl m_Control;
			
			public InternalTimer( DungeonControl dc ) : base( dc.TimeLimit )
			{
				m_Control = dc;
                Priority = TimerPriority.TwoFiftyMS;
			}
			
			protected override void OnTick()
			{
				m_Control.Stop();
			}
		}
	}
	#endregion
}