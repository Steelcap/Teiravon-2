using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using SpawnMapsE = Server.Teiravon.SpawnMaps.Maps;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;

namespace Teiravon.Farming
{
		public enum Crops
		{
			Null,
			Carrot,
			Cabbage,
			Onion,
			Lettuce,
			Pumpkin,
			Squash,
			YellowGourd,
			GreenGourd,
			Watermelon,
			Cantaloupe,
			HoneyDewMelon,
			Grapes,
			Cotton,
			Coconut,
			Banana,
			Lemon,
			Lime,
			Dates,
			Peach,
			Pear,
			Apple,
			Chicken,
			Sheep,
			Cow,
			Pig,
            Ginseng,
            Garlic,
            Nightshade,
            MandrakeRoot
		}

	public class FarmCommands
	{
		public static void Initialize()
		{
			Commands.Register( "Farm", AccessLevel.Player, new CommandEventHandler( FarmMenu_OnCommand ) );
		}
		
		[Usage( "Farm" )]
		[Description( "Activates Farm Menu" )]
		private static void FarmMenu_OnCommand( CommandEventArgs e )
		{
			Mobile m_Player = e.Mobile;
			m_Player.SendGump(new FarmGump(m_Player));
		}
	}

	public class TAVFarmSpawn : Item
	{
		private int m_HomeRange = 0;
		private int m_Count = 2;
		private TimeSpan m_MinDelay;
		private TimeSpan m_MaxDelay;
		private ArrayList m_CreaturesName;
		private ArrayList m_Creatures;
		private DateTime m_End;
		private InternalTimer m_Timer;
		private bool m_Running;
		private Mobile m_Farmer;
		private bool runonce = false;


		public bool IsFull{ get{ return ( m_Creatures != null && m_Creatures.Count >= m_Count ); } }

		public ArrayList CreaturesName
		{
			get { return m_CreaturesName; }
			set
			{
				m_CreaturesName = value;
				if ( m_CreaturesName.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Farmer
		{
			get { return m_Farmer; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Count
		{
			get { return m_Count; }
			set { m_Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Running
		{
			get { return m_Running; }
			set
			{
				if ( value )
					Start();
				else
					Stop();

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HomeRange
		{
			get { return m_HomeRange; }
			set { m_HomeRange = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MinDelay
		{
			get { return m_MinDelay; }
			set { m_MinDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MaxDelay
		{
			get { return m_MaxDelay; }
			set { m_MaxDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextSpawn
		{
			get
			{
				if ( m_Running )
					return m_End - DateTime.Now;
				else
					return TimeSpan.FromSeconds( 0 );
			}
			set
			{
				Start();
				DoTimer( value );
			}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan Reduction
        {
            get
            {
                return TimeSpan.FromSeconds(0);
            }
            set
            {
                TimeSpan lower = NextSpawn.Subtract(value);
                Start();
                DoTimer(lower);
            }
        }
		

		[Constructable]
		public TAVFarmSpawn( Crops m_crop, Mobile m_planter ) : base( 0x1f13 )
		{
			ArrayList creaturesName = new ArrayList();
			m_Farmer = m_planter;
			
			TeiravonMobile Planter = (TeiravonMobile)m_planter;
			
			int growtime = 45 - (Planter.FarmingSkill * 2);
			
			switch (m_crop)
			{
				case Crops.Carrot:
					ItemID = 3190;
					Name = "a carrot crop";
					creaturesName.Add("Carrot");
					break;
				case Crops.Cabbage:
					ItemID = 3170;
					Name = "a cabbage crop";
					creaturesName.Add("Cabbage");
					break;
				case Crops.Onion:
					ItemID = 3177;
					Name = "a onion crop";
					creaturesName.Add("Onion");
					break;
				case Crops.Lettuce:
					ItemID = 3169;
					Name = "a lettuce crop";
					creaturesName.Add("Lettuce");
					break;
				case Crops.Pumpkin:
					ItemID = 3168;
					Name = "a pumpkin crop";
					creaturesName.Add("Pumpkin");
					break;
				case Crops.Squash:
					ItemID = 3168;
					Name = "a squash crop";
					creaturesName.Add("Squash");
					break;
				case Crops.YellowGourd:
					ItemID = 3168;
					Name = "a yellow gourd crop";
					creaturesName.Add("YellowGourd");
					break;
				case Crops.GreenGourd:
					ItemID = 3168;
					Name = "a green gourd crop";
					creaturesName.Add("GreenGourd");
					break;
				case Crops.Watermelon:
					ItemID = 3167;
					Name = "a watermelon crop";
					creaturesName.Add("Watermelon");
					break;
				case Crops.Cantaloupe:
					ItemID = 3167;
					Name = "a cantaloupe crop";
					creaturesName.Add("Cantaloupe");
					break;
				case Crops.HoneyDewMelon:
					ItemID = 3167;
					Name = "a honeydew melon crop";
					creaturesName.Add("HoneydewMelon");
					break;
				case Crops.Grapes:
					ItemID = 3166;
					Name = "a grapes crop";
					creaturesName.Add("Grapes");
					break;
				case Crops.Cotton:
					ItemID = 3153;
					Name = "a cotton crop";
					creaturesName.Add("Cotton");
					break;
				case Crops.Coconut:
					ItemID = 3221;
					Name = "a coconut tree";
					creaturesName.Add("Coconut");
					break;
				case Crops.Banana:
					ItemID = 3242;
					Name = "a banana tree";
					creaturesName.Add("Banana");
					break;
				case Crops.Lemon:
					ItemID = 3273;
					Name = "a lemon tree";
					creaturesName.Add("Lemon");
					break;
				case Crops.Lime:
					ItemID = 3273;
					Name = "a Lime tree";
					creaturesName.Add("Lime");
					break;
				case Crops.Dates:
					ItemID = 3273;
					Name = "a date tree";
					creaturesName.Add("Dates");
					break;
				case Crops.Peach:
					ItemID = 3273;
					Name = "a peach tree";
					creaturesName.Add("Peach");
					break;
				case Crops.Pear:
					ItemID = 3273;
					Name = "a pear tree";
					creaturesName.Add("Pear");
					break;
				case Crops.Apple:
					ItemID = 3273;
					Name = "an apple tree";
					creaturesName.Add("Apple");
					break;
				case Crops.Chicken:
					ItemID = 8401;
					Name = "a farm chicken";
					creaturesName.Add("Eggs");
					creaturesName.Add("Eggs");
					creaturesName.Add("Eggs");
					creaturesName.Add("Eggs");
					creaturesName.Add("RawBird");
					break;
				case Crops.Sheep:
					ItemID = 8422;
					Name = "a farm sheep";
					creaturesName.Add("Wool");
					creaturesName.Add("Wool");
					creaturesName.Add("Wool");
					creaturesName.Add("Wool");
					creaturesName.Add("RawLambLeg");
					break;
				case Crops.Cow:
					ItemID = 8451;
					Name = "a farm cow";
					creaturesName.Add("PitcherofMilk");
					creaturesName.Add("PitcherofMilk");
					creaturesName.Add("PitcherofMilk");
					creaturesName.Add("PitcherofMilk");
					creaturesName.Add("RawRibs");
					break;
				case Crops.Pig:
					ItemID = 8449;
					Name = "a farm pig";
					creaturesName.Add("Bacon");
					creaturesName.Add("Ham");
					creaturesName.Add("RawRibs");
					break;
                case Crops.Garlic:
                    ItemID = 0x18E1;
                    Name = "a garlic plant";
                    creaturesName.Add("Garlic");
                    creaturesName.Add("Garlic");
                    creaturesName.Add("Garlic");
                    break;
                case Crops.MandrakeRoot:
                    ItemID = 0x18DF;
                    Name = "a Mandrake plant";
                    creaturesName.Add("MandrakeRoot");
                    creaturesName.Add("MandrakeRoot");
                    creaturesName.Add("MandrakeRoot");
                    break;
                case Crops.Ginseng:
                    ItemID = 0x18E9;
                    Name = "a Ginseng plant";
                    creaturesName.Add("Ginseng");
                    creaturesName.Add("Ginseng");
                    creaturesName.Add("Ginseng");
                    break;
                case Crops.Nightshade:
                    ItemID = 0x18E5;
                    Name = "a Nightshade flower";
                    creaturesName.Add("Nightshade");
                    creaturesName.Add("Nightshade");
                    creaturesName.Add("Nightshade");
                    break;
			}

			InitSpawn( 1, TimeSpan.FromMinutes( growtime ), TimeSpan.FromMinutes( growtime + 5 ), 0, 0, creaturesName );
//			InitSpawn( 1, TimeSpan.FromMinutes( 1 ), TimeSpan.FromMinutes( 1 ), 0, 0, creaturesName );
		}


		public void InitSpawn( int amount, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, ArrayList creaturesName )
		{
			Visible = true;
			Movable = false;
			m_Running = true;
			m_MinDelay = minDelay;
			m_MaxDelay = maxDelay;
			m_Count = amount;
			m_HomeRange = homeRange;
			m_CreaturesName = creaturesName;
			m_Creatures = new ArrayList();
			DoTimer( TimeSpan.FromSeconds( 1 ) );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
		}
		
		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

		}
		
		
		public TAVFarmSpawn( Serial serial ) : base( serial )
		{
		}

		public void Start()
		{
			if ( !m_Running )
			{
				if ( m_CreaturesName.Count > 0 )
				{
					m_Running = true;
					DoTimer();
				}
			}
		}

		public void Stop()
		{
			if ( m_Running )
			{
				m_Timer.Stop();
				m_Running = false;
			}
		}

		public void Defrag()
		{
			bool removed = false;

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				object o = m_Creatures[i];

				if ( o is Item )
				{
					Item item = (Item)o;

					if ( item.Deleted || item.Parent != null )
					{
						m_Creatures.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					if ( m.Deleted )
					{
						m_Creatures.RemoveAt( i );
						--i;
						removed = true;

					}
					else if ( m is BaseCreature )
					{
						if ( ((BaseCreature)m).Controled || ((BaseCreature)m).IsStabled )
						{
							m_Creatures.RemoveAt( i );
							--i;
							removed = true;

						}
					}
				}
				else
				{
					m_Creatures.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			if ( removed )
				InvalidateProperties();
		}

		public void OnTick()
		{
			DoTimer();
			
			if (runonce)
				Spawn();
			
			runonce = true;
		}

		public void Respawn()
		{
//			RemoveCreatures();

			for ( int i = 0; i < m_Count; i++ )
				Spawn();
		}

		public void Spawn()
		{
			if ( m_CreaturesName.Count > 0 )
				Spawn( Utility.Random( m_CreaturesName.Count ) );
		}

		public void Spawn( string creatureName )
		{
			for ( int i = 0; i < m_CreaturesName.Count; i++ )
			{
				if ( (string)m_CreaturesName[i] == creatureName )
				{
					Spawn( i );
					break;
				}
			}
		}

		public void Spawn( int index )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || m_CreaturesName.Count == 0 || index >= m_CreaturesName.Count || Parent != null )
				return;

			Defrag();

			if ( m_Creatures.Count >= m_Count )
				return;

			Type type = SpawnerType.GetType( (string)m_CreaturesName[index] );
			
			if ( type != null )
			{
				try
				{
					object o = Activator.CreateInstance( type );

					if ( o is Mobile )
					{
						Mobile m = (Mobile)o;

						m_Creatures.Add( m );


						Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );

						m.OnBeforeSpawn( loc, map );
						InvalidateProperties();


						m.MoveToWorld( loc, map );

						if ( m is BaseCreature )
						{
							BaseCreature c = (BaseCreature)m;

							c.RangeHome = m_HomeRange;

							c.Home = this.Location;
						}

						m.OnAfterSpawn();
					}
					else if ( o is Item )
					{
						Item item = (Item)o;

						m_Creatures.Add( item );

						Point3D loc = GetSpawnPosition();

						item.OnBeforeSpawn( loc, map );
						InvalidateProperties();

						item.MoveToWorld( loc, map );

						item.OnAfterSpawn();
					}
				}
				catch
				{
				}
			}
		}

		public Point3D GetSpawnPosition()
		{
			Map map = Map;

			if ( map == null )
				return Location;

			// Try 10 times to find a Spawnable location.
			for ( int i = 0; i < 10; i++ )
			{
				int x = Location.X + (Utility.Random( (m_HomeRange * 2) + 1 ) - m_HomeRange);
				int y = Location.Y + (Utility.Random( (m_HomeRange * 2) + 1 ) - m_HomeRange);
				int z = Map.GetAverageZ( x, y );

				if ( Map.CanSpawnMobile( new Point2D( x, y ), this.Z ) )
					return new Point3D( x, y, this.Z );
				else if ( Map.CanSpawnMobile( new Point2D( x, y ), z ) )
					return new Point3D( x, y, z );
			}

			return this.Location;
		}

		public void DoTimer()
		{
			if ( !m_Running )
				return;

			int minSeconds = (int)m_MinDelay.TotalSeconds;
			int maxSeconds = (int)m_MaxDelay.TotalSeconds;

			TimeSpan delay = TimeSpan.FromSeconds( Utility.RandomMinMax( minSeconds, maxSeconds ) );
			DoTimer( delay );
		}

		public void DoTimer( TimeSpan delay )
		{
			if ( !m_Running )
				return;

			m_End = DateTime.Now + delay;

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = new InternalTimer( this, delay );
			m_Timer.Start();
		}

		private class InternalTimer : Timer
		{
			private TAVFarmSpawn m_Spawner;

			public InternalTimer( TAVFarmSpawn spawner, TimeSpan delay ) : base( delay )
			{
				if ( spawner.IsFull )
					Priority = TimerPriority.FiveSeconds;
				else
					Priority = TimerPriority.OneSecond;

				m_Spawner = spawner;
			}

			protected override void OnTick()
			{
				if ( m_Spawner != null )
					if ( !m_Spawner.Deleted )
						m_Spawner.OnTick();
			}
		}

		public int CountCreatures( string creatureName )
		{
			Defrag();

			int count = 0;

			for ( int i = 0; i < m_Creatures.Count; ++i )
				if ( Insensitive.Equals( creatureName, m_Creatures[i].GetType().Name ) )
					++count;

			return count;
		}

		public void RemoveCreatures( string creatureName )
		{
			Defrag();

			creatureName = creatureName.ToLower();

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				object o = m_Creatures[i];

				if ( Insensitive.Equals( creatureName, o.GetType().Name ) )
				{
					if ( o is Item )
						((Item)o).Delete();
					else if ( o is Mobile )
						((Mobile)o).Delete();
				}
			}

			InvalidateProperties();
		}

		public void RemoveCreatures()
		{
			Defrag();

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				object o = m_Creatures[i];

				if ( o is Item )
					((Item)o).Delete();
				else if ( o is Mobile )
					((Mobile)o).Delete();
			}

			InvalidateProperties();
		}

		public void BringToHome()
		{
			Defrag();

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				object o = m_Creatures[i];

				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;

					m.MoveToWorld( Location, Map );
				}
				else if ( o is Item )
				{
					Item item = (Item)o;

					item.MoveToWorld( Location, Map );
				}
			}
		}

		public override void OnDelete()
		{
			base.OnDelete();

			TeiravonMobile tm = (TeiravonMobile)this.m_Farmer;
			if (tm != null)
				tm.FarmCrops -= 1;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_MinDelay );
			writer.Write( m_MaxDelay );
			writer.Write( m_Count );
			writer.Write( m_HomeRange );
			writer.Write( m_Running );

			if ( m_Running )
				writer.WriteDeltaTime( m_End );

			writer.Write( m_CreaturesName.Count );

			for ( int i = 0; i < m_CreaturesName.Count; ++i )
				writer.Write( (string)m_CreaturesName[i] );

			writer.Write( m_Creatures.Count );

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				object o = m_Creatures[i];

				if ( o is Item )
					writer.Write( (Item)o );
				else if ( o is Mobile )
					writer.Write( (Mobile)o );
				else
					writer.Write( Serial.MinusOne );
			}
			
			writer.Write( m_Farmer );
		}

		private static WarnTimer m_WarnTimer;

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

					m_MinDelay = reader.ReadTimeSpan();
					m_MaxDelay = reader.ReadTimeSpan();
					m_Count = reader.ReadInt();
					m_HomeRange = reader.ReadInt();
					m_Running = reader.ReadBool();

					TimeSpan ts = TimeSpan.Zero;

					if ( m_Running )
						ts = reader.ReadDeltaTime() - DateTime.Now;

					int size = reader.ReadInt();

					m_CreaturesName = new ArrayList( size );

					for ( int i = 0; i < size; ++i )
					{
						string typeName = reader.ReadString();

						m_CreaturesName.Add( typeName );

						if ( SpawnerType.GetType( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int count = reader.ReadInt();

					m_Creatures = new ArrayList( count );

					for ( int i = 0; i < count; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_Creatures.Add( e );
					}

					m_Farmer = reader.ReadMobile();

					if ( m_Running )
						DoTimer( ts );

			}

		private class WarnTimer : Timer
		{
			private ArrayList m_List;

			private class WarnEntry
			{
				public Point3D m_Point;
				public Map m_Map;
				public string m_Name;

				public WarnEntry( Point3D p, Map map, string name )
				{
					m_Point = p;
					m_Map = map;
					m_Name = name;
				}
			}

			public WarnTimer() : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_List = new ArrayList();
				Start();
			}

			public void Add( Point3D p, Map map, string name )
			{
				m_List.Add( new WarnEntry( p, map, name ) );
			}

			protected override void OnTick()
			{
				try
				{
					Console.WriteLine( "Warning: {0} bad spawns detected, logged: 'badspawn.log'", m_List.Count );

					using ( StreamWriter op = new StreamWriter( "badspawn.log", true ) )
					{
						op.WriteLine( "# Bad spawns : {0}", DateTime.Now );
						op.WriteLine( "# Format: X Y Z F Name" );
						op.WriteLine();

						foreach ( WarnEntry e in m_List )
							op.WriteLine( "{0}\t{1}\t{2}\t{3}\t{4}", e.m_Point.X, e.m_Point.Y, e.m_Point.Z, e.m_Map, e.m_Name );

						op.WriteLine();
						op.WriteLine();
					}
				}
				catch
				{
				}
			}
		}
	}
	

	public class PitcherofMilk : Pitcher
	{
		[Constructable]
		public PitcherofMilk(): base( BeverageType.Milk)
		{
			Weight = 1;
			Name = "Pitcher of Milk";
		}

		public PitcherofMilk( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FarmGump : Gump
	{
		Mobile mob;
		TeiravonMobile tm;
		int maxcrops;
		int currentcrops;
		
		public enum Buttons
		{
			Page1OK,
			DeleteCrop,
			Vegetables,
			Fruits,
			Trees,
            Reagents,
			Other,
			CarrotCrop,
			CabbageCrop,
			OnionCrop,
			LettuceCrop,
			PumpkinCrop,
			SquashCrop,
			YellowGourdCrop,
			GreenGourdCrop,
			BananaCrop,
			LemonCrop,
			LimeCrop,
			CoconutCrop,
			DatesCrop,
			PeachCrop,
			PearCrop,
			AppleCrop,
			GrapesCrop,
			WatermelonCrop,
			CantaloupeCrop,
			HoneydewMelonCrop,
			ChickenCrop,
			SheepCrop,
			CowCrop,
			PigCrop,
			CottonCrop,
            GinsengCrop,
            GarlicCrop,
            NightshadeCrop,
            MandrakeCrop
		}

		public FarmGump(Mobile player): base( 0, 0 )
		{
			mob = player;
			tm = (TeiravonMobile)mob;
			maxcrops = tm.FarmingSkill * 3;
			currentcrops = tm.FarmCrops;
			string tmpmax = maxcrops.ToString();
			string tmpcur = currentcrops.ToString();
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(139, 14, 296, 320, 9350);
			this.AddHtml( 190, 21, 200, 21, @"<p class=MsoNormal align=center style='text-align:center'><b><spanstyle='color:green'>FARMING<o:p></o:p></span></b></p>", (bool)false, (bool)false);

			this.AddPage(1);
			this.AddImage(190, 27, 12);
			this.AddImage(168, 5, 1811);
			this.AddImage(190, 27, 50700);
			this.AddImage(190, 28, 50636);
			this.AddImage(190, 26, 50434);
			this.AddImage(169, 9, 1886);
			this.AddButton(242, 268, 247, 248, (int)Buttons.Page1OK, GumpButtonType.Page, 2);

			this.AddPage(2);
			this.AddLabel(235, 50, 0, @"Maximum Crops");			
			this.AddLabel(275, 75, 0, @tmpmax);
			this.AddLabel(235, 100, 0, @"Current Crops");
			this.AddLabel(275, 130, 0, @tmpcur);
			this.AddLabel(200, 170, 71, @"Vegetables");
			this.AddLabel(200, 210, 71, @"Fruits");
			this.AddLabel(350, 170, 71, @"Trees");
			this.AddLabel(350, 210, 71, @"Other");
            this.AddLabel(275, 250, 71, @"Reagents");
			this.AddLabel(240, 275, 31, @"Delete Crop");
			this.AddButton(263, 297, 2472, 2473, (int)Buttons.DeleteCrop, GumpButtonType.Reply, 0);
			this.AddButton(160, 165, 10830, 10830, (int)Buttons.Vegetables, GumpButtonType.Page, 3);
			this.AddButton(160, 205, 10830, 10830, (int)Buttons.Fruits, GumpButtonType.Page, 5);
			this.AddButton(310, 165, 10830, 10830, (int)Buttons.Trees, GumpButtonType.Page, 4);
			this.AddButton(310, 205, 10830, 10830, (int)Buttons.Other, GumpButtonType.Page, 6);
            this.AddButton(240, 245, 10830, 10830, (int)Buttons.Reagents, GumpButtonType.Page, 7);

			this.AddPage(3);
			this.AddLabel(250, 70, 71, @"Vegetables");
			this.AddLabel(175, 115, 0, @"Carrot");
			this.AddItem(160, 135, 3192);
			this.AddButton(196, 145, 1209, 1210, (int)Buttons.CarrotCrop, GumpButtonType.Reply, 0);
			this.AddLabel(260, 115, 0, @"Cabbage");
			this.AddItem(236, 144, 3195);
			this.AddButton(281, 145, 1209, 1210, (int)Buttons.CabbageCrop, GumpButtonType.Reply, 0);
			this.AddLabel(350, 115, 0, @"Onion");
			this.AddItem(333, 137, 3181);
			this.AddButton(371, 145, 1209, 1210, (int)Buttons.OnionCrop, GumpButtonType.Reply, 0);
			this.AddLabel(175, 185, 0, @"Lettuce");
			this.AddItem(159, 214, 3184);
			this.AddButton(196, 215, 1209, 1210, (int)Buttons.LettuceCrop, GumpButtonType.Reply, 0);
			this.AddLabel(260, 185, 0, @"Pumpkin");
			this.AddItem(236, 215, 3178);
			this.AddButton(281, 215, 1209, 1210, (int)Buttons.PumpkinCrop, GumpButtonType.Reply, 0);
			this.AddLabel(350, 185, 0, @"Squash");
			this.AddItem(327, 210, 3186);
			this.AddButton(371, 215, 1209, 1210, (int)Buttons.SquashCrop, GumpButtonType.Reply, 0);
			this.AddLabel(195, 255, 0, @"Yellow Gourd");
			this.AddItem(184, 285, 3172);
			this.AddButton(236, 285, 1209, 1210, (int)Buttons.YellowGourdCrop, GumpButtonType.Reply, 0);
			this.AddLabel(305, 255, 0, @"Green Gourd");
			this.AddItem(313, 286, 3174);
			this.AddButton(351, 285, 1209, 1210, (int)Buttons.GreenGourdCrop, GumpButtonType.Reply, 0);
			
			this.AddPage(4);
			this.AddLabel(265, 70, 71, @"Trees");
			this.AddLabel(175, 115, 0, @"Banana");
			this.AddItem(155, 143, 5919);
			this.AddButton(196, 145, 1209, 1210, (int)Buttons.BananaCrop, GumpButtonType.Reply, 0);
			this.AddLabel(260, 115, 0, @"Lemon");
			this.AddItem(244, 147, 5928);
			this.AddButton(281, 145, 1209, 1210, (int)Buttons.LemonCrop, GumpButtonType.Reply, 0);
			this.AddLabel(350, 115, 0, @"Lime");
			this.AddItem(333, 148, 5930);
			this.AddButton(371, 145, 1209, 1210, (int)Buttons.LimeCrop, GumpButtonType.Reply, 0);
			this.AddLabel(175, 185, 0, @"Coconut");
			this.AddItem(156, 214, 5926);
			this.AddButton(196, 215, 1209, 1210, (int)Buttons.CoconutCrop, GumpButtonType.Reply, 0);
			this.AddLabel(260, 185, 0, @"Dates");
			this.AddItem(241, 216, 5927);
			this.AddButton(281, 215, 1209, 1210, (int)Buttons.DatesCrop, GumpButtonType.Reply, 0);
			this.AddLabel(350, 185, 0, @"Peach");
			this.AddItem(336, 217, 2514);
			this.AddButton(371, 215, 1209, 1210, (int)Buttons.PeachCrop, GumpButtonType.Reply, 0);
			this.AddLabel(210, 255, 0, @"Pear");
			this.AddItem(194, 286, 2452);
			this.AddButton(230, 285, 1209, 1210, (int)Buttons.PearCrop, GumpButtonType.Reply, 0);
			this.AddLabel(330, 255, 0, @"Apple");
			this.AddItem(313, 286, 2512);
			this.AddButton(351, 285, 1209, 1210, (int)Buttons.AppleCrop, GumpButtonType.Reply, 0);
			
			this.AddPage(5);
			this.AddLabel(265, 70, 71, @"Fruit");
			this.AddLabel(203, 128, 0, @"Grapes");
			this.AddItem(184, 159, 2513);
			this.AddButton(223, 158, 1209, 1210, (int)Buttons.GrapesCrop, GumpButtonType.Reply, 0);
			this.AddLabel(308, 128, 0, @"Watermelon");
			this.AddItem(311, 156, 3164);
			this.AddButton(344, 158, 1209, 1210, (int)Buttons.WatermelonCrop, GumpButtonType.Reply, 0);
			this.AddLabel(193, 208, 0, @"Cantaloupe");
			this.AddItem(187, 239, 3193);
			this.AddButton(228, 238, 1209, 1210, (int)Buttons.CantaloupeCrop, GumpButtonType.Reply, 0);
			this.AddLabel(303, 208, 0, @"Honeydew Melon");
			this.AddItem(306, 239, 3188);
			this.AddButton(352, 238, 1209, 1210, (int)Buttons.HoneydewMelonCrop, GumpButtonType.Reply, 0);
			
			this.AddPage(6);
			this.AddLabel(265, 70, 71, @"Other");
			this.AddLabel(208, 112, 0, @"Chicken");
			this.AddItem(194, 127, 8401);
			this.AddButton(235, 141, 1209, 1210, (int)Buttons.ChickenCrop, GumpButtonType.Reply, 0);
			this.AddLabel(322, 112, 0, @"Sheep");
			this.AddItem(311, 132, 8422);
			this.AddButton(349, 142, 1209, 1210, (int)Buttons.SheepCrop, GumpButtonType.Reply, 0);
			this.AddLabel(213, 192, 0, @"Cow");
			this.AddItem(181, 213, 8451);
			this.AddButton(233, 222, 1209, 1210, (int)Buttons.CowCrop, GumpButtonType.Reply, 0);
			this.AddLabel(331, 192, 0, @"Pig");
			this.AddItem(301, 212, 8449);
			this.AddButton(357, 222, 1209, 1210, (int)Buttons.PigCrop, GumpButtonType.Reply, 0);
			this.AddLabel(255, 259, 0, @"Cotton");
			this.AddItem(240, 285, 3577);
			this.AddButton(285, 290, 1209, 1210, (int)Buttons.CottonCrop, GumpButtonType.Reply, 0);

            this.AddPage(7);
            this.AddLabel(265, 70, 71, @"Reagents");
            this.AddLabel(208, 112, 0, @"Mandrake");
            this.AddItem(194, 127, 0xF86);
            this.AddButton(235, 141, 1209, 1210, (int)Buttons.MandrakeCrop, GumpButtonType.Reply, 0);
            this.AddLabel(322, 112, 0, @"Garlic");
            this.AddItem(311, 132, 0xF84);
            this.AddButton(349, 142, 1209, 1210, (int)Buttons.GarlicCrop, GumpButtonType.Reply, 0);
            this.AddLabel(213, 192, 0, @"Ginseng");
            this.AddItem(181, 213, 0xF85);
            this.AddButton(233, 222, 1209, 1210, (int)Buttons.GinsengCrop, GumpButtonType.Reply, 0);
            this.AddLabel(331, 192, 0, @"Nightshare");
            this.AddItem(301, 212, 0xF88);
            this.AddButton(357, 222, 1209, 1210, (int)Buttons.NightshadeCrop, GumpButtonType.Reply, 0);
           

		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Crops croppick;
			bool deletecrop = false;
			
			if (currentcrops >= maxcrops && info.ButtonID != (int)Buttons.DeleteCrop)
			{
				tm.SendMessage("You have planted your maximun amount of crops!");
				return;
			}
			
			switch (info.ButtonID)
			{
				case (int)Buttons.DeleteCrop:
					croppick = Crops.Carrot;
					deletecrop = true;
					break;
				case (int)Buttons.CarrotCrop:
					croppick = Crops.Carrot;
					break;
				case (int)Buttons.CabbageCrop:
					croppick = Crops.Cabbage;
					break;
				case (int)Buttons.OnionCrop:
					croppick = Crops.Onion;
					break;
				case (int)Buttons.LettuceCrop:
					croppick = Crops.Lettuce;
					break;
				case (int)Buttons.PumpkinCrop:
					croppick = Crops.Pumpkin;
					break;
				case (int)Buttons.SquashCrop:
					croppick = Crops.Squash;
					break;
				case (int)Buttons.YellowGourdCrop:
					croppick = Crops.YellowGourd;
					break;
				case (int)Buttons.GreenGourdCrop:
					croppick = Crops.GreenGourd;
					break;
				case (int)Buttons.WatermelonCrop:
					croppick = Crops.Watermelon;
					break;
				case (int)Buttons.CantaloupeCrop:
					croppick = Crops.Cantaloupe;
					break;
				case (int)Buttons.HoneydewMelonCrop:
					croppick = Crops.HoneyDewMelon;
					break;
				case (int)Buttons.GrapesCrop:
					croppick = Crops.Grapes;
					break;
				case (int)Buttons.CottonCrop:
					croppick = Crops.Cotton;
					break;
				case (int)Buttons.CoconutCrop:
					croppick = Crops.Coconut;
					break;
				case (int)Buttons.BananaCrop:
					croppick = Crops.Banana;
					break;
				case (int)Buttons.LemonCrop:
					croppick = Crops.Lemon;
					break;
				case (int)Buttons.LimeCrop:
					croppick = Crops.Lime;
					break;
				case (int)Buttons.DatesCrop:
					croppick = Crops.Dates;
					break;
				case (int)Buttons.PeachCrop:
					croppick = Crops.Peach;
					break;
				case (int)Buttons.PearCrop:
					croppick = Crops.Pear;
					break;
				case (int)Buttons.AppleCrop:
					croppick = Crops.Apple;
					break;
				case (int)Buttons.ChickenCrop:
					croppick = Crops.Chicken;
					break;
				case (int)Buttons.SheepCrop:
					croppick = Crops.Sheep;
					break;
				case (int)Buttons.CowCrop:
					croppick = Crops.Cow;
					break;
				case (int)Buttons.PigCrop:
					croppick = Crops.Pig;
					break;
                case (int)Buttons.MandrakeCrop:
                    croppick = Crops.MandrakeRoot;
                    break;
                case (int)Buttons.NightshadeCrop:
                    croppick = Crops.Nightshade;
                    break;
                case (int)Buttons.GarlicCrop:
                    croppick = Crops.Garlic;
                    break;
                case (int)Buttons.GinsengCrop:
                    croppick = Crops.Ginseng;
                    break;
				default:
					croppick = Crops.Null;
					break;
			}

			if (croppick != Crops.Null)
			{
				mob.SendMessage("Where do you want to plant that?");
				mob.Target = new FarmTarget( mob, croppick, deletecrop );
			}
		}
	}

	public class FarmTarget : Target
	{
		Mobile mob;
		Crops pick;
		bool cropdel;
		
		public FarmTarget( Mobile player, Crops crop, bool delcrop ) : base( -1, true, TargetFlags.None )
		{
			mob = player;
			pick = crop;
			cropdel = delcrop;
		}

		protected override void OnTarget( Mobile from, object o )
		{
			IPoint3D p = o as IPoint3D;
			
			if (o is Mobile)
			{
				mob.SendMessage("You can't plant that on a creature!");
			}
			else if (o is Item && o is TAVFarmSpawn)
			{
				if (cropdel)
				{
					TAVFarmSpawn FarmCrop = (TAVFarmSpawn)o;
					if (FarmCrop.Farmer == mob)
						FarmCrop.Delete();
					else
						mob.SendMessage("That is not your crop!");
				}
				else
				{
					mob.SendMessage("You can't plant on top of another crop!");
				}
			}
			else if (!cropdel)
			{
				TAVFarmSpawn plantit = new TAVFarmSpawn(pick, mob);
				plantit.MoveToWorld(new Point3D(p.X, p.Y, p.Z), mob.Map);
				TeiravonMobile tm = (TeiravonMobile)mob;
				tm.FarmCrops += 1;
			}
		}
	}


}
