using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Items;
using Server.Regions;
using System.Collections;
using Server.SkillHandlers;
using Server.Gumps;

namespace Server.Items 
{
	public enum RegionFlag
	{
		None				= 0x00000000,
		AllowBenifitPlayer	= 0x00000001,
		AllowHarmPlayer		= 0x00000002,
		AllowHousing		= 0x00000004,
		AllowSpawn			= 0x00000008,

		CanBeDamaged		= 0x00000010,
		CanHeal				= 0x00000020,
		CanRessurect		= 0x00000040,
		CanUseStuckMenu		= 0x00000080,
		ItemDecay			= 0x00000100,

		ShowEnterMessage	= 0x00000200,
		ShowExitMessage		= 0x00000400,

		AllowBenifitNPC		= 0x00000800,
		AllowHarmNPC		= 0x00001000,

		CanMountEthereal	= 0x000002000,
		CannotEnter			= 0x000004000,

		CanLootPlayerCorpse	= 0x000008000,
		CanLootNPCCorpse	= 0x000010000,
		CannotLootOwnCorpse = 0x000020000,

		CanUsePotions		= 0x000040000,

		IsGuarded			= 0x000080000
	}

	public enum CustomRegionPriority
	{
		HighestPriority	= 0x96,	
		HousePriority	= 0x96,
		HighPriority	= 0x90,
		MediumPriority	= 0x64,
		LowPriority		= 0x60,
		InnPriority		= 0x33,
		TownPriority	= 0x32,
		LowestPriority	= 0x0
	}

	public class RegionControl : Item
	{
		#region Flags

		public bool GetFlag( RegionFlag flag )
		{
			return ( (m_Flags & flag) != 0 );
		}

		public void SetFlag( RegionFlag flag, bool value )
		{
			if ( value )
				m_Flags |= flag;
			else
				m_Flags &= ~flag;
		}

		public RegionFlag Flags
		{
			get{ return m_Flags; }
			set{ m_Flags = value; }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowBenifitPlayer
		{
			get{ return GetFlag( RegionFlag.AllowBenifitPlayer ); }
			set{ SetFlag( RegionFlag.AllowBenifitPlayer, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowHarmPlayer
		{
			get{ return GetFlag( RegionFlag.AllowHarmPlayer ); }
			set{ SetFlag( RegionFlag.AllowHarmPlayer, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowHousing
		{
			get{ return GetFlag( RegionFlag.AllowHousing ); }
			set{ SetFlag( RegionFlag.AllowHousing, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowSpawn
		{
			get{ return GetFlag( RegionFlag.AllowSpawn ); }
			set{ SetFlag( RegionFlag.AllowSpawn, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanBeDamaged
		{
			get{ return GetFlag( RegionFlag.CanBeDamaged ); }
			set{ SetFlag( RegionFlag.CanBeDamaged, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanMountEthereal
		{
			get{ return GetFlag( RegionFlag.CanMountEthereal ); }
			set{ SetFlag( RegionFlag.CanMountEthereal, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CannotEnter
		{
			get{ return GetFlag( RegionFlag.CannotEnter ); }
			set{ SetFlag( RegionFlag.CannotEnter, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanHeal
		{
			get{ return GetFlag( RegionFlag.CanHeal ); }
			set{ SetFlag( RegionFlag.CanHeal, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanRessurect
		{
			get{ return GetFlag( RegionFlag.CanRessurect ); }
			set{ SetFlag( RegionFlag.CanRessurect, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanUseStuckMenu
		{
			get{ return GetFlag( RegionFlag.CanUseStuckMenu ); }
			set{ SetFlag( RegionFlag.CanUseStuckMenu, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ItemDecay
		{
			get{ return GetFlag( RegionFlag.ItemDecay ); }
			set{ SetFlag( RegionFlag.ItemDecay, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowBenifitNPC
		{
			get{ return GetFlag( RegionFlag.AllowBenifitNPC ); }
			set{ SetFlag( RegionFlag.AllowBenifitNPC, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowHarmNPC
		{
			get{ return GetFlag( RegionFlag.AllowHarmNPC ); }
			set{ SetFlag( RegionFlag.AllowHarmNPC, value ); }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowEnterMessage
		{
			get{ return GetFlag( RegionFlag.ShowEnterMessage ); }
			set{ SetFlag( RegionFlag.ShowEnterMessage, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowExitMessage
		{
			get{ return GetFlag( RegionFlag.ShowExitMessage ); }
			set{ SetFlag( RegionFlag.ShowExitMessage, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanLootPlayerCorpse
		{
			get{ return GetFlag( RegionFlag.CanLootPlayerCorpse ); }
			set{ SetFlag( RegionFlag.CanLootPlayerCorpse, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanLootNPCCorpse
		{
			get{ return GetFlag( RegionFlag.CanLootNPCCorpse ); }
			set{ SetFlag( RegionFlag.CanLootNPCCorpse, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CannotLootOwnCorpse
		{
			get{ return GetFlag( RegionFlag.CannotLootOwnCorpse ); }
			set{ SetFlag( RegionFlag.CannotLootOwnCorpse, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanUsePotions
		{
			get{ return GetFlag( RegionFlag.CanUsePotions ); }
			set{ SetFlag( RegionFlag.CanUsePotions, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsGuarded
		{
			get{ 
				if( m_Region != null )
					return !m_Region.IsDisabled();
				else
					return GetFlag( RegionFlag.IsGuarded ); 
			}
			set{ 
				SetFlag( RegionFlag.IsGuarded, value );

				UpdateRegion();
			}
		}

		#endregion

		private CustomRegion m_Region;
		
		private RegionFlag m_Flags;
		private BitArray m_RestrictedSpells;
		private BitArray m_RestrictedSkills;

		private string m_RegionName;
		private CustomRegionPriority m_Priority;

		private MusicName m_Music;

		private TimeSpan m_PlayerLogoutDelay;

		private ArrayList m_Coords;

		private int m_LightLevel;

		public CustomRegion MyRegion
		{
			get{ return m_Region; }
		}


		public BitArray RestrictedSpells
		{
			get{ return m_RestrictedSpells; }
		}

		public BitArray RestrictedSkills
		{
			get{ return m_RestrictedSkills; }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan PlayerLogoutDelay
		{
			get{ return m_PlayerLogoutDelay; }
			set{ m_PlayerLogoutDelay = value; }
		}


		public ArrayList Coords
		{
			get{ return m_Coords; }
			set{ m_Coords = value; UpdateRegion(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string RegionName
		{
			get{ return m_RegionName; }
			set{ 
				m_RegionName = value; 
				UpdateRegion();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public MusicName Music
		{
			get{ return m_Music; }
			set
			{ 
				m_Music = value; 
				UpdateRegion();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int LightLevel
		{
			get{ return m_LightLevel; }
			set{ m_LightLevel = value; }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public CustomRegionPriority Priority
		{
			get{ return m_Priority; }
			set
			{ 
				m_Priority = value; 
				UpdateRegion();
			}
		}


		[Constructable]
		public RegionControl() : base ( 5609 )
		{
			Visible = false;
			Movable = false;
			Name = "Region Controller";
			m_RegionName = "Custom Region";
			m_Priority = CustomRegionPriority.HighPriority;

			m_RestrictedSpells = new BitArray( SpellRegistry.Types.Length );
			m_RestrictedSkills = new BitArray( SkillInfo.Table.Length );

			Coords = new ArrayList();
			UpdateRegion();
		}

		[Constructable]
		public RegionControl( Rectangle2D rect ) : base ( 5609 )
		{
			Coords = new ArrayList();

			Coords.Add( rect );

			m_RestrictedSpells = new BitArray( SpellRegistry.Types.Length );
			m_RestrictedSkills = new BitArray( SkillInfo.Table.Length );

			Visible = false;
			Movable = false;
			Name = "Region Controller";
			m_RegionName = "Custom Region";
			m_Priority = CustomRegionPriority.HighPriority;

			UpdateRegion();
		}

		public RegionControl( Serial serial ) : base( serial )
		{
		}


		public override void OnDoubleClick( Mobile m )
		{
			if( m.AccessLevel >= AccessLevel.GameMaster)
			{
			//	m.SendGump( new RestrictGump( m_RestrictedSpells, RestrictType.Spells ) );
			//	m.SendGump( new RestrictGump( m_RestrictedSkills, RestrictType.Skills ) );

				m.CloseGump( typeof( RegionControlGump ) );
				m.SendGump( new RegionControlGump( this ) );
				m.SendMessage( "Don't forget to props this object for more options!" );

				m.CloseGump( typeof( RemoveAreaGump ) );
				m.SendGump( new RemoveAreaGump( this ) );
			}
		}

		public override void OnMapChange()
		{
			UpdateRegion();
			base.OnMapChange();
		}



		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version

			writer.Write( (int)m_LightLevel );

			writer.Write( (int) m_Music );

			WriteRect2DArray( writer, Coords );
			writer.Write( (int)m_Priority );
			writer.Write( (TimeSpan)m_PlayerLogoutDelay );


			//writer.Write( m_Area );
			WriteBitArray( writer, m_RestrictedSpells );
			WriteBitArray( writer, m_RestrictedSkills );

			writer.Write( (int) m_Flags );
			writer.Write( m_RegionName );
		}
		#region Serialization Helpers
		public static void WriteBitArray( GenericWriter writer, BitArray ba )
		{
			writer.Write( ba.Length );

				for( int i = 0; i < ba.Length; i++ )
				{
					writer.Write( ba[i] );
				}
			return;
		}

		public static BitArray ReadBitArray( GenericReader reader )
		{
			int size = reader.ReadInt();
			BitArray newBA = new BitArray( size );

			for( int i = 0; i < size; i++ )
			{
				newBA[i] = reader.ReadBool();
			}

			return newBA;
		}


		public static void WriteRect2DArray( GenericWriter writer, ArrayList ary )
		{
			writer.Write( ary.Count );

			for( int i = 0; i < ary.Count; i++ )
			{
				writer.Write( (Rectangle2D)ary[i] );	//Rect2D
			}
			return;
		}

		public static ArrayList ReadRect2DArray( GenericReader reader )
		{
			int size = reader.ReadInt();
			ArrayList newAry = new ArrayList();

			for( int i = 0; i < size; i++ )
			{
				newAry.Add( reader.ReadRect2D() );
			}
            
            return newAry;
		}

		#endregion
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				{
					m_LightLevel = reader.ReadInt();
					goto case 2;
				}
				case 2:
				{
					m_Music = (MusicName)reader.ReadInt();
					goto case 1;
				}
				case 1:
				{
					Coords = ReadRect2DArray( reader );
					m_Priority = (CustomRegionPriority) reader.ReadInt();
					m_PlayerLogoutDelay = reader.ReadTimeSpan();

					m_RestrictedSpells = ReadBitArray( reader );
					m_RestrictedSkills = ReadBitArray( reader );
            
					m_Flags = (RegionFlag)reader.ReadInt();

					m_RegionName = reader.ReadString();
					break;
				}
				case 0:
				{
					Coords = new ArrayList();

					Coords.Add( reader.ReadRect2D() );
					m_RestrictedSpells = ReadBitArray( reader );
					m_RestrictedSkills = ReadBitArray( reader );
            
					m_Flags = (RegionFlag)reader.ReadInt();

					m_RegionName = reader.ReadString();
					break;
				}
			}

			UpdateRegion();

		}


		public void UpdateRegion()
		{
			if( Coords != null && Coords.Count != 0 )
			{
				if( m_Region == null )
				{
					m_Region = new CustomRegion( this, this.Map );
					//Region.AddRegion( m_Region );	//Maybe not needed cause setting map will call this?
				}

				Region.RemoveRegion( m_Region );

				m_Region.Coords = Coords;

				m_Region.Disabled = !(GetFlag( RegionFlag.IsGuarded ));

				m_Region.Music = Music;
				m_Region.Name = m_RegionName;

				m_Region.Priority = (int)m_Priority;

				m_Region.Map = this.Map;

				Region.AddRegion( m_Region );
			}

			return;
		}


		public static int GetRegistryNumber( ISpell s )
		{
			Type[] t = SpellRegistry.Types;

			for( int i = 0; i < t.Length; i++ )
			{
				if( s.GetType() == t[i] )
					return i;
			}

			return -1;
		}


		public bool IsRestrictedSpell( ISpell s )
		{
			int regNum = GetRegistryNumber( s );
			
			if( regNum < 0 )	//Happens with unregistered Spells
				return false;

			return m_RestrictedSpells[regNum];
		}

		public bool IsRestrictedSkill( int skill )
		{
			if( skill < 0 )
				return false;

			return m_RestrictedSkills[skill];
		}


		public void ChooseArea( Mobile m )
		{
			BoundingBoxPicker.Begin( m, new BoundingBoxCallback( CustomRegion_Callback ), this );
		}

		private static void CustomRegion_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
		{
			DoChooseArea( from, map, start, end, state );
		}

		private static void DoChooseArea( Mobile from, Map map, Point3D start, Point3D end, object control )
		{
try {

			RegionControl r = (RegionControl)control;
			Rectangle2D rect = new Rectangle2D( start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1 );

			// Hopefully this line fixes it... if not, it'll get caught in Try/Catch.
			if ( r.m_Coords== null )
				r.m_Coords= new ArrayList();

			r.m_Coords.Add( rect );

			r.UpdateRegion();
} catch {
from.SendMessage( "Caught a crash while trying to set a static housing region. Tell Valik or Rook." );
Console.WriteLine( "Still crashing in DoChooseArea..." );
}
		}



		public override void OnDelete()
		{
			if( m_Region != null )
				Region.RemoveRegion( m_Region );

			base.OnDelete();
		}
	}
}
