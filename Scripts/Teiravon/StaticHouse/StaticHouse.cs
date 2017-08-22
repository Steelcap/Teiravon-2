/************* Static Housing script version 2 *****************************
RunUO Version scripted for Beta 32

Description:
This script allows gms to make any building area space etc. to act as if it
where a house. Useful for making town buildings that are part of the map able
to be accessed by players.

Files Needed:
- StaticHouse.cs
- StaticHouseRegion.cs
- SetStaticSecureLevelGump.cs
- StaticHouseGump.cs
- StaticStrongBox.cs

Files that need editing or replacement to work in the Static House
- PlayerVendorDeed.cs
- GuildDeed.cs
- BaseHouse.cs
- BaseAddon.cs

Known Bugs:
- Addons could be placed outside of house if player is in the house
(needs to check at target for a statichouse)
- This has not been tested for version compatibility though it is written
to be compatible with all versions.

Instructions on use:
Place the files above into a custom folder and compile. I suggest
placing the modified files into this folder as well so you can add
future versions of RunUO easily.

Work to be done in future Versions:
- A town taxing system to charge players for use of public housing with
an on/off capability
- Addon deed check the target location for a house
- Change the entire gump to the new AOS style gump.
- Add compatability with House Decorator

Questions and Comments can be sent to Fineous at racineraiders@hotmail.com
*/

using System;
using Server;
using Server.Gumps;
using Server.Regions;
using System.Collections;
using Server.Targeting;
using Server.Multis.Deeds;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Accounting;

namespace Server.StaticHousing
{
	[FlipableAttribute(0xBD2, 0xBD1)]
	public class StaticHouseSign : Item
	{
		public const int MaxCoOwners = 15;
		public  int m_MaxLockDowns;
		public  int m_MaxSecures;
		public  bool m_Public;
		public  Mobile m_Owner;
		public  Mobile m_OrgOwner;
		public  StaticHouseRegion m_Region;
		public  ArrayList m_Access;
		public  ArrayList m_CoOwners;
		public  ArrayList m_Friends;
		public  StaticHouseSign m_Sign;
		public  TrashBarrel m_Trash;
		public  ArrayList m_Doors;
		public  ArrayList m_LockDowns;
		public  ArrayList m_Secures;
		public  ArrayList m_Bans;
		public  ArrayList houseBoundries;
		public  bool houseSetUp;
		public  bool houseInitialized;
		public  Rectangle2D Area;
		public  Rectangle2D targetArea;
		public  static Hashtable m_Table = new Hashtable();
		public  ArrayList m_Addons;
		public  Mobile owner;
		public  int m_Price;
		public  bool forSale;
		public  Point3D m_BanLocation;

		public bool Contains( Mobile m )
		{
			if ( m.Region == this.Region )
				return true;
			else
				return false;
		}

		public bool Contains( Item item )
		{
		if ( item.Map == this.Map )
			return true;
		else
			return false;
		}

		[Constructable]
		public StaticHouseSign() : base( 0xBD2 )
		{
			houseBoundries = new ArrayList();
			m_Owner = null;
			m_OrgOwner = null;
			Name = "a house sign";
			Movable = false;
			m_Region = new StaticHouseRegion( this );
			m_Region.Coords = new ArrayList();
			m_Sign = this;
			m_Doors = new ArrayList();
			m_LockDowns = new ArrayList();
			m_Secures = new ArrayList();
			m_CoOwners = new ArrayList();
			m_Friends = new ArrayList();
			m_Bans = new ArrayList();
			m_Access = new ArrayList();
			m_Addons = new ArrayList();
			owner = null;
			m_Price = 0;
			forSale = false;
			m_Public = true;
			houseSetUp = false;
			houseInitialized = false;
			m_BanLocation.X = 0;
			m_BanLocation.Y = 0;
			m_BanLocation.Z = 0;
		}

		public static StaticHouseSign StaticFindHouseAt( Mobile m )
		{
			if ( m == null || m.Deleted )
			{
				return null;
			}

			return StaticFindHouseAt( m.Location, m.Map, 16, m);
		}

		public static StaticHouseSign StaticFindHouseAt( Item item )
		{
			if ( item == null || item.Deleted )
			{
				return null;
			}

			return StaticFindHouseAt( item.GetWorldLocation(), item.Map, item.ItemData.Height, item );
		}

		public static StaticHouseSign StaticFindHouseAt( Point3D loc, Map map, int height, Mobile m )
		{
		if ( map == null || map == Map.Internal )
			return null;
		try
		{
			StaticHouseRegion tempRegion = m.Region as StaticHouseRegion ;
			return tempRegion.m_House as StaticHouseSign;
		}
		catch{}
			return null;
		}

		public static StaticHouseSign StaticFindHouseAt( Point3D loc, Map map, int height, Item item)
		{
			if ( map == null || map == Map.Internal )
				return null;

			Sector sector = map.GetSector( loc );

			for ( int i = 0; i < sector.Regions.Count; ++i )
			{
				StaticHouseRegion region = sector.Regions[i] as StaticHouseRegion;
				try
				{
					if (region.m_House != null)
					{
						StaticHouseSign house = region.m_House;

						if ( house != null && house.IsInside(loc, height, house, item ) )
							return house;
					}
				}
				catch{}
			}

			return null;
		}

		public bool IsInside( Mobile m )
		{
			if ( m == null || m.Deleted || m.Map != this.Map )
			{
				return false;
			}

			return IsInside( m.Location, 16, m);
		}

		public bool IsInside( Item item )
		{
			if ( item == null || item.Deleted || item.Map != this.Map )
			{
				return false;
			}

			return IsInside( item.Location, item.ItemData.Height, item );
		}

		public bool IsInside( Point3D p, int height, StaticHouseSign house, Item compare)
		{
			try{
				StaticHouseRegion region = house.Region as StaticHouseRegion;

				Map mapTemp = region.Map;

				if ( mapTemp == null )
				{
					return false;
				}

				IPooledEnumerable eable;
				foreach( Rectangle2D rect in m_Region.Coords )
				{
					eable = region.Map.GetItemsInBounds(rect);
					foreach ( Item item in eable )
					{
						if ( item == compare )
						{
							eable.Free();
							return true;
						}
					}
					eable.Free();
				}
				return false;
			}
			catch{}
			return false;
		}

		public bool IsInside( Point3D p, int height, Mobile m )
		{
			StaticHouseRegion region = m.Region as StaticHouseRegion;

			if (m.Region as StaticHouseRegion == region)
			{
				return true;
			}

			return false;
		}

		public virtual void StaticChangeSignType( int itemID )
		{
			if ( m_Sign != null )
			{
				m_Sign.ItemID = itemID;
			}
		}

		public bool IsInside( Point3D p, int height, Item item )
		{
			try
			{
				StaticHouseRegion region = this.Region as StaticHouseRegion;
				IPooledEnumerable eable;

				foreach( Rectangle2D rect in m_Region.Coords )
				{
					eable = region.Map.GetItemsInBounds(rect);

					foreach ( Item checkItem in eable )
					{
						if ( checkItem == item )
						{
							eable.Free();
							return true;
						}
					}

					eable.Free();
				}

				return false;
			}
			catch{}

			return false;
		}

		public SecureStaticAccessResult CheckSecureAccess( Mobile m, Item item )
		{
			if ( m_Secures == null || !(item is Container) )
			{
				return SecureStaticAccessResult.Insecure;
			}

			for ( int i = 0; i < m_Secures.Count; ++i )
			{
				SecureStaticInfo info = (SecureStaticInfo)m_Secures[i];

				if ( info.Item == item )
				{
					return HasSecureAccess( m, info.Level ) ? SecureStaticAccessResult.Accessible : SecureStaticAccessResult.Inaccessible;
				}
			}

			return SecureStaticAccessResult.Insecure;
		}

		public uint CreateKeys( Mobile m )
		{
			uint value = Key.RandomValue();

			Key packKey = new Key( KeyType.Gold );
			Key bankKey = new Key( KeyType.Gold );

			packKey.KeyValue = value;
			bankKey.KeyValue = value;

			packKey.LootType = LootType.Newbied;
			bankKey.LootType = LootType.Newbied;

			BankBox box = m.BankBox;

			if ( box == null || !box.TryDropItem( m, bankKey, false ) )
				bankKey.Delete();

			m.AddToBackpack( packKey );

			return value;
		}

		public StaticHouseSign( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile owner )
		{
			if( this.forSale == false )
			{
				owner.SendGump( new StaticHouseGump( owner, this ) );
			}
			else
			{
				owner.SendGump( new StaticBuyHouseGump( owner, this ) );
			}
		}

		public void CreateYard(Mobile from)
		{
			ChooseArea(from);
		}

		public void ChooseArea( Mobile m )
		{
			BoundingBoxPicker.Begin( m, new BoundingBoxCallback( CustomRegion_Callback ), this );
		}

		public static void CustomRegion_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
		{
			DoChooseArea( from, map, start, end, state );
		}

		public static void DoChooseArea( Mobile from, Map map, Point3D start, Point3D end, object control )
		{
			StaticHouseSign r = (StaticHouseSign)control;
			Rectangle2D rect = new Rectangle2D( start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1 );

			r.houseBoundries.Add( rect );
		}

		public void InitializeHouse(Mobile from)
		{
			if(! houseSetUp)
			{
				m_Owner = from;
				m_OrgOwner = from;
				m_Region.Map = from.Map;
				m_Region = new StaticHouseRegion( this );
				m_Region.Coords = new ArrayList();

				for( int i = 0; i < this.houseBoundries.Count; i++ )
				{
					Area = (Rectangle2D)this.houseBoundries[i];
					m_Region.Coords.Add(Area);
					Region.AddRegion( m_Region );
				}

				m_BanLocation = new Point3D(from.Location);

				m_MaxLockDowns = 15000;
				m_MaxSecures= 5;
				houseSetUp = true;
				houseInitialized = true;

				from.SendMessage("The house has been initialized");
			}
			else
			{
				from.SendMessage("This house must be reset first");
			}
		}

		public virtual void ChangeSignType( int itemID )
		{
			if ( m_Sign != null )
				m_Sign.ItemID = itemID;
		}

		public StaticHouseSign Sign
		{
			get
			{
				return m_Sign;
			}
			set
			{
				m_Sign = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HouseMaxHeight
		{
			get
			{
				return m_Region.MaxZ;
			}
			set
			{
				m_Region.MaxZ = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HouseMinHeight
		{
			get
			{
				return m_Region.MinZ;
			}
			set
			{
				m_Region.MinZ = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get
			{
				return m_Owner;
			}
			set
			{
				m_Owner = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile OriginalOwner
		{
			get
			{
				return m_OrgOwner;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int GetMap
		{
			get
			{
				return m_Region.Map.MapID;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ArrayList HouseBoundry
		{
			get
			{
				return m_Region.Coords;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile CurrentOwner
		{
			get
			{
				return m_Owner;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Price
		{
			get
			{
				return m_Price;
			}
			set
			{
				m_Price = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Selling
		{
			get
			{
				return forSale;
			}
			set
			{
				forSale = value;
				if (value == true)
				{
					Name = "For Sale " + m_Price;
				}
			}
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();
			Region.RemoveRegion( m_Region );

			if ( m_Sign != null )
			{
				m_Sign.Delete();
			}

			if ( m_Doors != null )
			{
				for ( int i = 0; i < m_Doors.Count; ++i )
				{
					Item item = (Item)m_Doors[i];
					if ( item != null )
					{
						item.Delete();
					}
				}
				m_Doors.Clear();
			}

			if ( m_LockDowns != null )
			{
				for ( int i = 0; i < m_LockDowns.Count; ++i )
				{
					Item item = (Item)m_LockDowns[i];

					if ( item != null )
					{
						item.Movable = true;
					}
				}
				m_LockDowns.Clear();
			}

			if ( m_Secures != null )
			{
				for ( int i = 0; i < m_Secures.Count; ++i )
				{
					SecureStaticInfo info = (SecureStaticInfo)m_Secures[i];

					if ( info.Item != null )
					{
						info.Item.Movable = true;
					}
				}
				m_Secures.Clear();
			}
		}

		public static bool HasHouse( Mobile m )
		{
			if ( m == null )
			{
				return false;
			}

			ArrayList list = (ArrayList)m_Table[m];

			if ( list == null )
			{
				return false;
			}

			for ( int i = 0; i < list.Count; ++i )
			{
				StaticHouseSign h = (StaticHouseSign)list[i];

				if ( !h.Deleted )
					return true;
			}
			return false;
		}

		public static bool HasAccountHouse( Mobile m )
		{
			Account a = m.Account as Account;

			if ( a == null )
			{
				return false;
			}

			for ( int i = 0; i < 5; ++i )
			{
				if ( a[i] != null && HasHouse( a[i] ) )
				{
					return true;
				}
			}
			return false;
		}

		public bool CheckAccount( Mobile mobCheck, Mobile accCheck )
		{
			if ( accCheck != null )
			{
			Account a = accCheck.Account as Account;

				if ( a != null )
				{
					for ( int i = 0; i < 5; ++i )
					{
						if ( a[i] == mobCheck )
						{
								return true;
						}
					}
				}
			}
			return false;
		}

		public bool IsOwner( Mobile m )
		{
			if ( m == null )
			{
				return false;
			}

			if ( m == m_Owner || m.AccessLevel >= AccessLevel.GameMaster )
			{
				return true;
			}

			return Core.AOS && CheckAccount( m, m_Owner );
		}

		public bool IsCoOwner( Mobile m )
		{
			if ( m == null || m_CoOwners == null )
			{
				return false;
			}

			if ( IsOwner( m ) || m_CoOwners.Contains( m ) )
			{
				return true;
			}

			return !Core.AOS && CheckAccount( m, m_Owner );
		}

		public void RemoveKeys( Mobile m )
		{
			if ( m_Doors != null )
			{
				uint keyValue = 0;

				for ( int i = 0; keyValue == 0 && i < m_Doors.Count; ++i )
				{
					BaseDoor door = m_Doors[i] as BaseDoor;

					if ( door != null )
					{
						keyValue = door.KeyValue;
					}
				}

				Key.RemoveKeys( m, keyValue );
			}
		}

		public void ChangeLocks( Mobile m )
		{
			uint keyValue = CreateKeys( m );

			if ( m_Doors != null )
			{
				for ( int i = 0; i < m_Doors.Count; ++i )
				{
					BaseDoor door = m_Doors[i] as BaseDoor;

					if ( door != null )
					{
						door.KeyValue = keyValue;
					}
				}
			}
		}

		public void RemoveLocks()
		{
			if ( m_Doors != null )
			{
				for (int i=0;i<m_Doors.Count;++i)
				{
					BaseDoor door = m_Doors[i] as BaseDoor;

					if ( door != null )
					{
						door.KeyValue = 0;
						door.Locked = false;
					}
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write((int)0 ); // version
			writer.Write((int) m_MaxLockDowns);
			writer.Write((int) m_MaxSecures);
			writer.Write((int) m_Price);

			writer.Write((bool) m_Public);
			writer.Write((bool) houseSetUp);
			writer.Write((bool) forSale);

			writer.Write( m_Sign as StaticHouseSign);
			writer.Write( m_Trash as TrashBarrel);

			writer.Write( m_Owner );
			writer.Write( m_OrgOwner );

			writer.WriteMobileList( m_CoOwners );
			writer.WriteMobileList( m_Friends );
			writer.WriteMobileList( m_Bans );
			writer.WriteMobileList( m_Access );

			writer.WriteItemList( m_Doors );
			writer.WriteItemList( m_LockDowns );
			writer.WriteItemList( m_Addons );

			writer.Write( m_Secures.Count );

			for ( int i = 0; i < m_Secures.Count; ++i )
			{
				((SecureStaticInfo)m_Secures[i]).Serialize( writer );
			}

			writer.Write( (int)m_Region.Coords.Count );

			foreach( Rectangle2D rect in m_Region.Coords )
			{
				writer.Write( rect );
			}

			writer.Write( (int)m_Region.MaxZ );
			writer.Write( (int)m_Region.MinZ );

			writer.Write( m_BanLocation );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			int count;

			m_Region = new StaticHouseRegion( this );

			switch ( version )
			{
				case 0:
				{
					m_MaxLockDowns = reader.ReadInt();
					m_MaxSecures = reader.ReadInt();
					m_Price = reader.ReadInt();

					m_Public = reader.ReadBool();
					houseSetUp = reader.ReadBool();
					forSale = reader.ReadBool();

					m_Sign = reader.ReadItem() as StaticHouseSign;
					m_Trash = reader.ReadItem() as TrashBarrel;

					m_Owner = reader.ReadMobile();
					m_OrgOwner = reader.ReadMobile();

					m_CoOwners = reader.ReadMobileList();
					m_Friends = reader.ReadMobileList();
					m_Bans = reader.ReadMobileList();
					m_Access = reader.ReadMobileList();

					m_Doors = reader.ReadItemList();
					m_LockDowns = reader.ReadItemList();
					for ( int i = 0; i < m_LockDowns.Count; ++i )
						((Item)m_LockDowns[i]).IsLockedDown = true;
					m_Addons = reader.ReadItemList();

					count = reader.ReadInt();
					m_Secures = new ArrayList( count );

					for ( int i = 0; i < count; ++i )
					{
						SecureStaticInfo info = new SecureStaticInfo( reader );

						if ( info.Item != null )
						{
							info.Item.IsSecure = true;
							m_Secures.Add( info );
						}
					}

					count = reader.ReadInt();
					m_Region.Coords = new ArrayList();

					for(int i=0; i < count; i++)
					{
						m_Region.Coords.Add(reader.ReadRect2D());
					}

					Region.AddRegion(m_Region);

					m_Region.MaxZ = reader.ReadInt();
					m_Region.MinZ = reader.ReadInt();

					m_BanLocation = reader.ReadPoint3D();

					if (!houseSetUp)
					{
						houseBoundries = new ArrayList();
						m_CoOwners = new ArrayList();
						m_Friends = new ArrayList();
						m_Bans = new ArrayList();
						m_Access = new ArrayList();

						m_Doors = new ArrayList();
						m_LockDowns = new ArrayList();
						m_Addons = new ArrayList();
					}
					break;
				}
			}
		}

		public void SetLockdown( Item i, bool locked )
		{
			if ( m_LockDowns == null )
			{
				return;
			}

			i.Movable = !locked;
			i.IsLockedDown = locked;
			if ( locked )
			{
				m_LockDowns.Add( i );
			}
			else
			{
				m_LockDowns.Remove( i );
			}

			foreach ( Item c in i.Items )
			{
				SetLockdown( c, false );
			}
		}

		public void LockDown( Mobile m, Item item )
		{
			if ( !IsCoOwner( m ) )
			{
				return;
			}

			if ( item.Movable && !CheckSecure( item ) )
			{
				if ( item.RootParent != null )
				{
					if ( item.RootParent is Mobile )
					{
						m.SendLocalizedMessage( 1005525 );//That is not in your house
						return;
					}
					else if ( !IsInside( item ) )
					{
						m.SendLocalizedMessage( 1005525 );//That is not in your house
						return;
					}
				}
				else if ( !IsInside( item ) )
				{
					m.SendLocalizedMessage( 1005525 );//That is not in your house
				}
				else
				{
					if ( this.LockDownCount < m_MaxLockDowns )
					{
						SetLockdown( item, true );
					}
					else
					{
						m.SendLocalizedMessage( 1005379 );//That would exceed the maximum lock down limit for this house
					}
				}
			}
			else if ( m_LockDowns.IndexOf( item ) != -1 )
			{
				m.SendLocalizedMessage( 1005526 );//That is already locked down
			}
			else
			{
				m.SendLocalizedMessage( 1005377 );//You cannot lock that down
			}
		}

		public void Release( Mobile m, Item item )
		{
			if ( !IsCoOwner( m ) )
			{
				return;
			}
			if ( m_LockDowns.Contains( item ) )
			{
				item.PublicOverheadMessage( Server.Network.MessageType.Label, 0x3B2, 501657 );//[no longer locked down]
				SetLockdown( item, false );
			}
			else if ( CheckSecure( item ) )
			{
				ReleaseSecure( m, item );
			}
			else
			{
				m.SendLocalizedMessage( 501722 );//That isn't locked down...
			}
		}

		public void AddSecure( Mobile m, Item item )
		{
			if ( m_Secures == null || !IsOwner( m ) )
			{
				return;
			}

			if ( !IsInside( item ) )
			{
				m.SendLocalizedMessage( 1005525 ); // That is not in your house
			}
			else if ( checkLockedDown( item ) )
			{
				m.SendLocalizedMessage( 1010550 ); // This is already locked down and cannot be secured.
			}
			else if ( !(item is Container) )
			{
				LockDown( m, item );
			}
			else
			{
				SecureStaticInfo info = null;

				for ( int i = 0; info == null && i < m_Secures.Count; ++i )
				{
					if ( ((SecureStaticInfo)m_Secures[i]).Item == item )
					{
						info = (SecureStaticInfo)m_Secures[i];
					}
				}
				if ( info != null )
				{
					m.SendGump( new Gumps.SetStaticSecureLevelGump( m_Owner, info ) );
				}
				else if ( item.Parent != null )
				{
					m.SendLocalizedMessage( 1010423 ); // You cannot secure this, place it on the ground first.
				}
				else if ( !item.Movable )
				{
					m.SendLocalizedMessage( 1010424 ); // You cannot secure this.
				}
				else if ( SecureCount >= MaxSecures )
				{
					m.SendLocalizedMessage( 1008142, true, MaxSecures.ToString() ); // The maximum number of secure items has been reached :
				}
				else if ( (LockDownCount + 125) >= MaxLockDowns )
				{
					m.SendLocalizedMessage( 1005379 ); // That would exceed the maximum lock down limit for this house
				}
				else
				{
					info = new SecureStaticInfo( (Container)item, SecureLevelStatic.CoOwners );
					info.Item.IsSecure = true;
					m_Secures.Add( info );
					m_LockDowns.Remove( item );
					item.Movable = false;

					m.SendGump( new Gumps.SetStaticSecureLevelGump( m_Owner, info ) );
				}
			}
		}

		public bool HasSecureAccess( Mobile m, SecureLevelStatic level )
		{
			if ( m.AccessLevel >= AccessLevel.GameMaster )
			{
				return true;
			}

			switch ( level )
			{
				case SecureLevelStatic.Owner: return IsOwner( m );
				case SecureLevelStatic.CoOwners: return IsCoOwner( m );
				case SecureLevelStatic.Friends: return IsFriend( m );
				case SecureLevelStatic.Anyone: return true;
			}

			return false;
		}

		public void ReleaseSecure( Mobile m, Item item )
		{
			if ( m_Secures == null || !IsOwner( m ) || item is StrongBox )
				return;

			for ( int i = 0; i < m_Secures.Count; ++i )
			{
				SecureStaticInfo info = (SecureStaticInfo)m_Secures[i];

				if ( info.Item == item && HasSecureAccess( m, info.Level ) )
				{
					info.Item.IsSecure = false;
					item.Movable = true;
					item.PublicOverheadMessage( Server.Network.MessageType.Label, 0x3B2, 501656 );//[no longer secure]
					m_Secures.RemoveAt( i );
					return;
				}
			}

			m.SendLocalizedMessage( 501717 );//This isn't secure...
		}

		public override bool Decays
		{
			get
			{
				return false;
			}
		}

		public void Kick( Mobile from, Mobile targ )
		{
			if ( !IsFriend( from ) )
				return;

			if ( IsFriend( targ ) )
			{
				from.SendLocalizedMessage( 501348 ); // You cannot eject a friend of the house!
			}
			else if ( targ.AccessLevel > AccessLevel.Player && from.AccessLevel <= targ.AccessLevel )
			{
				from.SendLocalizedMessage( 501354 ); // Uh oh...a bigger boot may be required.
			}
			else if ( targ.Region != m_Region )
			{
				from.SendLocalizedMessage( 501352 ); // You may not eject someone who is not in your house!
			}
			else
			{
				targ.Location = m_BanLocation;
				targ.Map = Map;

				from.SendLocalizedMessage( 1042840, targ.Name ); // ~1_PLAYER NAME~ has been ejected from this house.
				targ.SendLocalizedMessage( 501341 ); 	/* You have been ejected from this house.
									 * If you persist in entering, you may be banned from the house.
									 */
			}
		}

		public void AddStrongBox( Mobile from )
		{
			if ( !IsCoOwner( from ) )
				return;

			if ( from == Owner )
			{
				from.SendLocalizedMessage( 502109 ); // Owners don't get a strong box
				return;
			}

			if ( LockDownCount + 1 > m_MaxLockDowns )
			{
				from.SendLocalizedMessage( 1005379 );//That would exceed the maximum lock down limit for this house
				return;
			}

			foreach ( SecureStaticInfo info in m_Secures )
			{
				Container c = info.Item;

				if ( !c.Deleted && c is StaticStrongBox && ((StaticStrongBox)c).Owner == from )
				{
					from.SendLocalizedMessage( 502112 );//You already have a strong box
					return;
				}
			}

			StaticStrongBox sb = new StaticStrongBox( from, this );
			sb.Movable = false;
			m_Secures.Add( new SecureStaticInfo( sb, SecureLevelStatic.CoOwners ) );
			sb.MoveToWorld( from.Location, from.Map );
		}

		public void RemoveAccess( Mobile from, Mobile targ )
		{
			if ( !IsFriend( from ) || m_Access == null )
				return;

			if ( m_Access.Contains( targ ) )
			{
				m_Access.Remove( targ );

				if ( !HasAccess( targ ) && IsInside( targ ) )
				{
					targ.Location = m_BanLocation;
					targ.SendLocalizedMessage( 1060734 ); // Your access to this house has been revoked.
				}

				from.SendLocalizedMessage( 1050051 ); // The invitation has been revoked.
			}
		}

		public void RemoveBan( Mobile from, Mobile targ )
		{
			if ( !IsCoOwner( from ) )
				return;

			if ( m_Bans.Contains( targ ) )
			{
				m_Bans.Remove( targ );
				from.SendLocalizedMessage( 501297 ); // The ban is lifted.
			}
		}

		public void Ban( Mobile from, Mobile targ )
		{
			if ( !IsCoOwner( from ) )
				return;

			if ( IsFriend( targ ) )
			{
				from.SendLocalizedMessage( 501348 ); // You cannot eject a friend of the house!
			}
			else if ( targ.AccessLevel > AccessLevel.Player && from.AccessLevel <= targ.AccessLevel )
			{
				from.SendLocalizedMessage( 501354 ); // Uh oh...a bigger boot may be required.
			}
			else if ( targ.Region != m_Region )
			{
				from.SendLocalizedMessage( 501352 ); // You may not eject someone who is not in your house!
			}
			else if ( m_Bans.Contains( targ ) )
			{
				from.SendLocalizedMessage( 501356 ); // This person is already banned!
			}
			else
			{
				targ.Location = m_BanLocation;
				targ.Map = Map;
				from.SendLocalizedMessage( 1042839, targ.Name ); // ~1_PLAYER_NAME~ has been banned from this house.
				targ.SendLocalizedMessage( 501340 ); // You have been banned from this house.

				if ( m_Bans != null )
					m_Bans.Add( targ );
			}
		}

		public PlayerVendor FindPlayerVendor()
		{
			Region r = m_Region;

			if ( r == null )
				return null;

			ArrayList list = r.Mobiles;

			for ( int i = 0; i < list.Count; ++i )
			{
				PlayerVendor pv = list[i] as PlayerVendor;

				if ( pv != null && Contains( pv ) )
					return pv;
			}

			return null;
		}

		public virtual bool CanPlaceNewVendor()
		{
				return true;
		}
		public const int MaximumBarkeepCount = 2;

		public virtual bool CanPlaceNewBarkeep()
		{
			ArrayList mobs = m_Region.Mobiles;

			int avail = MaximumBarkeepCount;

			for ( int i = 0; avail > 0 && i < mobs.Count; ++i )
			{
				if ( mobs[i] is PlayerBarkeeper )
					--avail;
			}

			return ( avail > 0 );
		}

		public void GrantAccess( Mobile from, Mobile targ )
		{
			if ( !IsFriend( from ) || m_Access == null )
				return;

			if ( HasAccess( targ ) )
			{
				from.SendLocalizedMessage( 1060729 ); // That person already has access to this house.
			}
			else if ( !targ.Player )
			{
				from.SendLocalizedMessage( 1060712 ); // That is not a player.
			}
			else if ( IsBanned( targ ) )
			{
				from.SendLocalizedMessage( 501367 ); // This person is banned!  Unban them first.
			}
			else
			{
				m_Access.Add( targ );
				targ.SendLocalizedMessage( 1060735 ); // You have been granted access to this house.
			}
		}

		public void AddCoOwner( Mobile from, Mobile targ )
		{
			if ( m_Owner.Serial != from.Serial )
				return;

			if ( IsFriend( targ ) )
				RemoveFriend( from, targ );
			else if ( IsBanned( targ ) )
				RemoveBan( from, targ );

			if ( IsCoOwner( targ ) )
			{
				from.SendLocalizedMessage( 501363 );//already coowner
			}
			else if ( m_CoOwners.Count > MaxCoOwners )
			{
				from.SendLocalizedMessage( 501368 );//CoOwner list full
			}
			else
			{
				m_CoOwners.Add( targ );
				targ.SendLocalizedMessage( 501343 );//you have been made coowner
				from.SendMessage( "They are now a co-owner of the house." );
			}
		}

		public void RemoveCoOwner( Mobile from, Mobile targ )
		{
			if ( m_Owner.Serial != from.Serial )
				return;

			if ( m_CoOwners.IndexOf( targ ) != -1 )
			{
				m_CoOwners.Remove( targ );
				targ.SendLocalizedMessage( 501300 );//You have been removed as coowner
				from.SendLocalizedMessage( 501299 );//coowner removed
				foreach ( Container c in m_Secures )
				{
					if ( c is StrongBox )
					{
						if ( ((StrongBox)c).Owner.Serial == targ.Serial )
						{
							m_Secures.Remove( c );
							c.Movable = true;
							((StrongBox)c).Owner = null;
							break;
						}
					}
				}
			}
			else
			{
			from.SendMessage( "They are not a co-owner of the house." );
			}
		}

		public void AddFriend( Mobile from, Mobile targ )
		{
			if ( !IsCoOwner( from ) )
				return;

			if ( m_Friends == null )
				return;

			if ( IsBanned( targ ) )
				RemoveBan( from, targ );

			if ( IsCoOwner( targ ) )
			{
				from.SendLocalizedMessage( 501363 );//already a coowner
			}
			else if ( !m_Friends.Contains( targ ) )
			{
				m_Friends.Add( targ );
				targ.SendLocalizedMessage( 501337 );//you have been made a friend
				from.SendMessage( "They have been made a friend of the household" );
			}
			else
			{
				from.SendLocalizedMessage( 501376 );//Already a friend
			}
		}

		public void RemoveFriend( Mobile from, Mobile targ )
		{
			if ( !IsCoOwner( from ) )
				return;

			if ( m_Friends == null )
				return;

			if ( m_Friends.Contains( targ ) )
			{
				m_Friends.Remove( targ );
				from.SendLocalizedMessage( 501298 );
				targ.SendMessage( "You have been removed as a friend of the house." );
			}
			else
			{
				from.SendMessage( "That person is not a friend of the house." );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Public
		{
			get
			{
				return m_Public;
			}
			set
			{
				if ( m_Public != value )
				{
					m_Public = value;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxSecures
		{
			get
			{
				return m_MaxSecures;
			}
			set
			{
				m_MaxSecures = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D BanLocation
		{
			get
			{
				return m_BanLocation;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxLockDowns
		{
			get
			{
				return m_MaxLockDowns;
			}
			set
			{
				m_MaxLockDowns = value;
			}
		}

		public Region Region
		{
			get
			{
				return m_Region;
			}
		}

		public ArrayList CoOwners
		{
			get
			{
				return m_CoOwners;
			}
		}

		public ArrayList CanAccess
		{
			get
			{
				return m_Access;
			}
		}

		public ArrayList Friends
		{
			get
			{
				return m_Friends;
			}
		}

		public ArrayList Bans
		{
			get
			{
				return m_Bans;
			}
		}

		public ArrayList Doors
		{
			get
			{
				return m_Doors;
			}
		}

		public int LockDownCount
		{
			get
			{
				int count = 0;

				if ( m_LockDowns != null )
					count += m_LockDowns.Count;

				if ( m_Secures != null )
				{
					for ( int i = 0; i < m_Secures.Count; ++i )
					{
						SecureStaticInfo info = (SecureStaticInfo)m_Secures[i];

						if ( info.Item.Deleted )
							continue;
						else if ( info.Item is StrongBox )
							count += 1;
						else
							count += 125;
					}
				}

				return count;
			}
		}

		public int SecureCount
		{
			get
			{
				int count = 0;

				if ( m_Secures != null )
				{
					for ( int i = 0; i < m_Secures.Count; i++ )
					{
						SecureStaticInfo info = (SecureStaticInfo)m_Secures[i];

						if ( info.Item.Deleted )
							continue;
						else if ( !(info.Item is StrongBox) )
							count += 1;
					}
				}

				return count;
			}
		}

		public ArrayList Addons
		{
			get
			{
				return m_Addons;
			}
			set
			{
				m_Addons = value;
			}
		}

		public ArrayList LockDowns
		{
			get
			{
				return m_LockDowns;
			}
		}

		public ArrayList Secures
		{
			get
			{
				return m_Secures;
			}
		}

		public bool IsFriend( Mobile m )
		{
			if ( m == null )
				return false;

			if ( m == Owner )
				return true;

			if ( m_Friends == null )
				return false;

			return m_Friends.Contains( m ) || IsCoOwner( m );
		}

		public bool IsBanned( Mobile m )
		{
			if ( m == null )
				return false;

			if ( m == Owner )
				return false;

			if ( m_Bans == null )
				return false;

			return m_Bans.Contains( m );
		}

		public bool checkLockedDown( Item i )
		{
			if ( i == null )
				return false;

			if ( m_LockDowns == null )
				return false;

			return m_LockDowns.Contains( i );
		}


		public bool HasAccess( Mobile m )
		{
			if ( m == null )
				return false;

			if ( m.AccessLevel > AccessLevel.Player || IsFriend( m ) || ( m_Access != null && m_Access.Contains( m ) ) )
				return true;

			if ( m is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)m;

				if ( bc.Controled || bc.Summoned )
				{
					m = bc.ControlMaster;

					if ( m == null )
						m = bc.SummonMaster;

					if ( m == null )
						return false;

					if ( m.AccessLevel > AccessLevel.Player || IsFriend( m ) || ( m_Access != null && m_Access.Contains( m ) ) )
						return true;
				}
			}

			return false;
		}

		public bool CheckSecure( Item item ) //RW3
		{
			if ( item == null )
				return false;

			if ( m_Secures == null )
				return false;

			bool contains = false;

			for ( int i = 0; !contains && i < m_Secures.Count; ++i )
				contains = ( ((SecureStaticInfo)m_Secures[i]).Item == item );

			return contains;
		}


		public Guildstone FindGuildstone(Mobile m)
		{
			StaticHouseRegion region = m.Region as StaticHouseRegion;
			Rectangle2D temp = (Rectangle2D)region.Coords[0];
			Map mapTemp = region.Map;

			if ( mapTemp == null )
				return null;

			IPooledEnumerable eable = mapTemp.GetItemsInBounds( temp);

			foreach ( Item item in eable )
			{
				if ( item is Guildstone && Contains( item ) )
				{
					eable.Free();
					return (Guildstone)item;
				}
			}

			eable.Free();
			return null;
		}

		public void AddTrashBarrel( Mobile from )
		{
			if ( m_Trash == null || m_Trash.Deleted )
			{
				m_Trash = new TrashBarrel();

				m_Trash.Movable = false;
				m_Trash.MoveToWorld( from.Location, from.Map );

				from.SendLocalizedMessage( 502121 ); 	/* You have a new trash barrel.
									 * Three minutes after you put something in the barrel, the trash will be emptied.
									 * Be forewarned, this is permanent! */
			}
			else
			{
				m_Trash.MoveToWorld( from.Location, from.Map );
			}
		}

		public void addDoor(Mobile from)
		{
			from.Target = new DoorAddTarget(this);
		}
	}

	public class LockdownTarget : Target
	{
		public bool m_Release;
		public StaticHouseSign m_House;

		public LockdownTarget( bool release, StaticHouseSign house ) : base( 12, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_Release = release;
			m_House = house;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Item && m_House.IsInside(targeted as Item))
			{
				if ( m_Release )
					m_House.Release( from, (Item)targeted );
				else
					m_House.LockDown( from, (Item)targeted );
			}
			else
			{
				from.SendLocalizedMessage( 1005377 );//You cannot lock that down
			}
		}
	}

	public class SecureTarget : Target
	{
		public bool m_Release;
		public StaticHouseSign m_House;

		public SecureTarget( bool release, StaticHouseSign house ) : base( 12, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_Release = release;
			m_House = house;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Item && m_House.IsInside(targeted as Item) )
			{
				if ( m_Release )
					m_House.ReleaseSecure( from, (Item)targeted );
				else
					m_House.AddSecure( from, (Item)targeted );
			}
			else
			{
				from.SendMessage("Im inthis thing");
				from.SendLocalizedMessage( 1010424 );//You cannot secure this
			}
		}
	}

	public class StaticHouseKickTarget : Target
	{
		public StaticHouseSign m_House;

		public StaticHouseKickTarget( StaticHouseSign house ) : base( -1, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_House = house;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile && m_House.IsInside(targeted as Mobile) )
			{
				m_House.Kick( from, (Mobile)targeted );
			}
			else
			{
				from.SendLocalizedMessage( 501347 );//You cannot eject that from the house!
			}
		}
	}

	public class StaticHouseBanTarget : Target
	{
		public StaticHouseSign m_House;
		public bool m_Banning;

		public StaticHouseBanTarget( bool ban, StaticHouseSign house ) : base( -1, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_House = house;
			m_Banning = ban;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile && m_House.IsInside(targeted as Mobile))
			{
				if ( m_Banning )
					m_House.Ban( from, (Mobile)targeted );
				else
					m_House.RemoveBan( from, (Mobile)targeted );
			}
			else
			{
				from.SendLocalizedMessage( 501347 );//You cannot eject that from the house!
			}
		}
	}

	public class StaticHouseAccessTarget : Target
	{
		public StaticHouseSign m_House;

		public StaticHouseAccessTarget( StaticHouseSign house ) : base( -1, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_House = house;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( !from.Alive || !m_House.IsFriend( from ) )
				return;

			if ( targeted is Mobile )
				m_House.GrantAccess( from, (Mobile)targeted );
			else
				from.SendLocalizedMessage( 1060712 ); // That is not a player.
		}
	}

	public class StaticCoOwnerTarget : Target
	{
		public StaticHouseSign m_House;
		public bool m_Add;

		public StaticCoOwnerTarget( bool add, StaticHouseSign house ) : base( 12, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_House = house;
			m_Add = add;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
			{
				if ( m_Add )
					m_House.AddCoOwner( from, (Mobile)targeted );
				else
					m_House.RemoveCoOwner( from, (Mobile)targeted );
			}
			else
			{
				from.SendLocalizedMessage( 501362 );//That can't be a coowner
			}
		}
	}

	public class StaticHouseFriendTarget : Target
	{
		public StaticHouseSign m_House;
		public bool m_Add;

		public StaticHouseFriendTarget( bool add, StaticHouseSign house ) : base( 12, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_House = house;
			m_Add = add;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
			{
				if ( m_Add )
					m_House.AddFriend( from, (Mobile)targeted );
				else
					m_House.RemoveFriend( from, (Mobile)targeted );
			}
			else
			{
				from.SendLocalizedMessage( 501371 );//That can't be a friend
			}
		}
	}

	public enum SecureStaticAccessResult
	{
		Insecure,
		Accessible,
		Inaccessible
	}

	public enum SecureLevelStatic
	{
		Owner,
		CoOwners,
		Friends,
		Anyone
	}

	public class SecureStaticInfo
	{
		public Container m_Item;
		public SecureLevelStatic m_Level;

		public Container Item{ get{ return m_Item; } }
		public SecureLevelStatic Level{ get{ return m_Level; } set{ m_Level = value; } }

		public SecureStaticInfo( Container item, SecureLevelStatic level )
		{
			m_Item = item;
			m_Level = level;
		}

		public SecureStaticInfo( GenericReader reader )
		{
			m_Item = reader.ReadItem() as Container;
			m_Level = (SecureLevelStatic)reader.ReadByte();
		}

		public void Serialize( GenericWriter writer )
		{
			writer.Write( m_Item );
			writer.Write( (byte) m_Level );
		}
	}

	public class StaticHouseOwnerTarget : Target
	{
		public StaticHouseSign m_House;

		public StaticHouseOwnerTarget( StaticHouseSign house ) : base( 12, false, TargetFlags.None )
		{
			CheckLOS = false;

			m_House = house;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
			{
				Mobile t = (Mobile)targeted;
				if ( t.Player )
				{
					from.SendLocalizedMessage( 501338 );//You have transfered ownership
					m_House.Owner = t;
					t.SendLocalizedMessage( 501339 );//You are now the owner.  Friends & bans removed
					m_House.Bans.Clear();
					m_House.Friends.Clear();
					m_House.CoOwners.Clear();
					m_House.RemoveKeys( t );
					m_House.ChangeLocks( t );
				}
				else
				{
					from.SendMessage( "Only players my own houses!" );
				}
			}
			else
			{
				from.SendMessage( "That can't own a house!" );
			}
		}
	}

	public class BanLocationTarget : Target
	{
		public StaticHouseSign m_sign;

		public BanLocationTarget( StaticHouseSign sign ) : base( -1, true, TargetFlags.None )
		{
			m_sign = sign;
		}

		protected override void OnTarget( Mobile from, object o )
		{
			IPoint3D p = o as IPoint3D;

			if ( p != null )
			{
				if ( p is Item )
				{
					p = ((Item)p).GetWorldLocation();
				}
				m_sign.m_BanLocation = new Point3D( p );
			}
		}
	}

	public class DoorAddTarget : Target
	{
		public StaticHouseSign m_House;

		public DoorAddTarget( StaticHouseSign house ) : base( -1, true, TargetFlags.None )
		{
			m_House = house;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is BaseDoor )
			{
				if (! m_House.m_Doors.Contains(targeted))
				{
					m_House.m_Doors.Add( targeted );
					from.SendMessage( "The Door Was Added to this House" );
				}
				else
					from.SendMessage( "This door already belongs to this house!" );
			}
			else
			{
				from.SendMessage( "This is not a door" );
			}
		}
	}
}