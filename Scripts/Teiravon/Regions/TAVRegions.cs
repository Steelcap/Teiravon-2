using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Teiravon;
using Server.Items;

namespace Server.Regions
{
	public class TeiravonRegion : Region
	{
		public enum SpawnTypes
		{
			None,

			Forest,
			Snow,
			Desert,
			Dungeon
		}

		private bool m_NoPD = false;
		private bool m_Dungeon = false;
		private int m_SpawnLevel = 1;
		private int m_MaxResourceLevel = -1;
		private SpawnTypes m_SpawnType = SpawnTypes.None;

		public bool NoPD { get { return m_NoPD; } set { m_NoPD = value; } }
		public bool Dungeon { get { return m_Dungeon; } set { m_Dungeon = value; } }
		public int SpawnLevel { get { return m_SpawnLevel; } set { m_SpawnLevel = value; } }
		public int MaxResourceLevel { get { return m_MaxResourceLevel; } set { m_MaxResourceLevel = value; } }
		public SpawnTypes SpawnType { get { return m_SpawnType; } set { m_SpawnType = value; } }

		public static void Initialize()
		{
			Region.AddRegion( new TeiravonRegion( "Easy Forest", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Medium Forest", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Hard Forest", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Easy Plains", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Medium Plains", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Hard Plains", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Swamp", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Medium Desert", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Hard Desert", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Easy Underdark", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Medium Underdark", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Hard Underdark", false, false ) );
			Region.AddRegion( new TeiravonRegion( "Gorge", false, false ) );
			Region.AddRegion( new TeiravonRegion( "No Encounter Area", false, false ) );
			
						// Rómen
						Region.AddRegion( new TeiravonRegion( "Edana", true, false ) );
						Region.AddRegion( new TeiravonRegion( "Edana Territory", false, false ) );
						Region.AddRegion( new TeiravonRegion( "Northwest Mercadia", false, false ) );
						Region.AddRegion( new TeiravonRegion( "Northeast Mercadia", false, false ) );
						Region.AddRegion( new TeiravonRegion( "Fortress Entrigan", true, false ) );

						Region.AddRegion( new TeiravonRegion( "Tilverton", true, false ) );
						Region.AddRegion( new TeiravonRegion( "Tilverton Territory", false, false ) );
						Region.AddRegion( new TeiravonRegion( "Devonshire", false, false ) );
						Region.AddRegion( new TeiravonRegion( "Devonshire Territory", false, false ) );

						Region.AddRegion( new TeiravonRegion( "Karagard", false, true ) );
						Region.AddRegion( new TeiravonRegion( "Burzkal", true, false ) );
						Region.AddRegion( new TeiravonRegion( "Azarad", true, false ) );

                        //Region.AddRegion(new TeiravonRegion("Blackwatch", true, true));

						// Márandor
						Region.AddRegion( new TeiravonRegion( "Arandor", true, false ) );
						Region.AddRegion( new TeiravonRegion( "Tihrindo", true, false ) );
						Region.AddRegion( new TeiravonRegion( "Luvalaire", false, false ) );
						Region.AddRegion( new TeiravonRegion( "Almiture", false, false ) );
			//			Region.AddRegion( new TeiravonRegion( "Ilmende", false, false ) );

						// Underdark
						Region.AddRegion( new TeiravonRegion( "Shadowrealm", true, true) );
						Region.AddRegion( new TeiravonRegion( "Underdark", false, true ) );

						//Vinyalonde
						Region.AddRegion( new TeiravonRegion( "Vinyalonde", false, false) );
			
						//MtThunder
						Region.AddRegion( new TeiravonRegion( "MtThunder", false, true) );
						Region.AddRegion( new TeiravonRegion( "UDEntrance", false, true) );
					}

		public TeiravonRegion( string name, int maxres, bool pd, bool dungeon ) : base( "", name, Map.Felucca )
		{
			Dungeon = dungeon;
			NoPD = pd;
			MaxResourceLevel = maxres;
		}

		public TeiravonRegion( string name, bool pd, bool dungeon ) : base( "", name, Map.Felucca )
		{
			Dungeon = dungeon;
			NoPD = pd;
			MaxResourceLevel = -1;
		}

		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			if ( Dungeon )
			{
				if ( m is TeiravonMobile )
				{
					TeiravonMobile tm = (TeiravonMobile)m;

					if ( tm.IsDrow() || tm.IsDwarf() || tm.IsGnome() || tm.AccessLevel > AccessLevel.Player )
						global = LightCycle.DayLevel;
					else if ( tm.IsElf() || tm.IsOrc() || tm.IsGoblin() )
						global = LightCycle.DungeonLevel / 2;
					else
						global = LightCycle.DungeonLevel;
				}
			}

			base.AlterLightLevel( m, ref global, ref personal );
		}

		public override Type GetResource( Type type )
		{
			Type[] m_OreResources = new Type[] {
												   typeof( IronOre ),
												   typeof( DullCopperOre ),
												   typeof( ShadowIronOre ),
												   typeof( CopperOre ),
												   typeof( BronzeOre ),
                                                   typeof( SilverOre ),
												   typeof( GoldOre ),
												   typeof( AgapiteOre ),
												   typeof( VeriteOre ),
												   typeof( ValoriteOre )
											   };

			Type[] m_WoodResources = new Type[] {
													typeof( Log ),
													typeof( PineLog ),
													typeof( RedwoodLog ),
													typeof( WhitePineLog ),
													typeof( AshwoodLog ),
													typeof( SilverBirchLog ),
													typeof( YewLog ),
													typeof( BlackOakLog )
												};

			Type newresource = type;

			if ( NoPD && Array.IndexOf( m_OreResources, type ) > 2 )
				//return m_OreResources[ Utility.Random( 2 ) ];
				newresource = m_OreResources[ Utility.Random( 2 ) ];

			else if ( NoPD && Array.IndexOf( m_WoodResources, type ) > 2  )
				//return m_WoodResources[ Utility.Random( 2 ) ];
				newresource = m_WoodResources[ Utility.Random( 2 ) ];

			else if ( MaxResourceLevel > -1 && Array.IndexOf( m_OreResources, type ) > MaxResourceLevel )
				//return m_OreResources[ Utility.Random( MaxResourceLevel ) ];
				newresource = m_OreResources[ Utility.Random( MaxResourceLevel ) ];

			else if ( MaxResourceLevel > -1 && Array.IndexOf( m_WoodResources, type ) > MaxResourceLevel )
				//return m_WoodResources[ Utility.Random( MaxResourceLevel ) ];
				newresource = m_WoodResources[ Utility.Random( MaxResourceLevel ) ];

			return newresource; //base.GetResource( newresource );
		}

		public override void OnEnter( Mobile m )
		{
		}

		public override void OnExit( Mobile m )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);

			writer.Write( m_NoPD );
			writer.Write( m_SpawnLevel );
			writer.Write( (int)m_SpawnType );
			writer.Write( (int)m_MaxResourceLevel );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);

			m_NoPD = reader.ReadBool();
			m_SpawnLevel = reader.ReadInt();
			m_SpawnType = (SpawnTypes)reader.ReadInt();
			m_MaxResourceLevel = reader.ReadInt();
		}
	}
}
