using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.LargeBOD" )]
	public abstract class LargeBOD : Item
	{
		private int m_AmountMax;
		//Startmod: Teiravon
		private Type m_Type;
		private Item m_Item;
		//Endmod: Teiravon
		private bool m_RequireExceptional;
		private BulkMaterialType m_Material;
		private LargeBulkEntry[] m_Entries;

		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountMax{ get{ return m_AmountMax; } set{ m_AmountMax = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RequireExceptional{ get{ return m_RequireExceptional; } set{ m_RequireExceptional = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public BulkMaterialType Material{ get{ return m_Material; } set{ m_Material = value; InvalidateProperties(); } }

		public LargeBulkEntry[] Entries{ get{ return m_Entries; } set{ m_Entries = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Complete
		{
			get
			{
				for ( int i = 0; i < m_Entries.Length; ++i )
				{
					if ( m_Entries[i].Amount < m_AmountMax )
						return false;
				}

				return true;
			}
		}

		public abstract ArrayList ComputeRewards( bool full );
		public abstract int ComputeGold();
		public abstract int ComputeFame();

		public virtual void GetRewards( out Item reward, out int gold, out int fame )
		{
			reward = null;
			gold = ComputeGold();
			fame = ComputeFame();

			ArrayList rewards = ComputeRewards( false );

			if ( rewards.Count > 0 )
			{
				reward = (Item)rewards[Utility.Random( rewards.Count )];

				for ( int i = 0; i < rewards.Count; ++i )
				{
					if ( rewards[i] != reward )
						((Item)rewards[i]).Delete();
				}
			}
		}

		public static BulkMaterialType GetRandomMaterial( BulkMaterialType start, double[] chances )
		{
			double random = Utility.RandomDouble();

			for ( int i = 0; i < chances.Length; ++i )
			{
				if ( random < chances[i] )
					return ( i == 0 ? BulkMaterialType.None : start + (i - 1) );

				random -= chances[i];
			}

			return BulkMaterialType.None;
		}

		public override int LabelNumber{ get{ return 1045151; } } // a bulk order deed

		public LargeBOD( int hue, int amountMax, bool requireExeptional, BulkMaterialType material, LargeBulkEntry[] entries ) : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			Hue = hue; // Blacksmith: 0x44E; Tailoring: 0x483
			LootType = LootType.Blessed;

			m_AmountMax = amountMax;
			m_RequireExceptional = requireExeptional;
			m_Material = material;
			m_Entries = entries;
		}

		public LargeBOD() : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060655 ); // large bulk order

			if ( m_RequireExceptional )
				list.Add( 1045141 ); // All items must be exceptional.

			if ( m_Material != BulkMaterialType.None && m_Material >= BulkMaterialType.Spined )
				list.Add( LargeBODGump.GetMaterialNumberFor( m_Material ) ); // All items must be made with x material.

			else switch ( (int)(m_Material) )
			{
					case 1: list.Add("All items must be crafted with dull copper ingots"); break;
					case 2: list.Add("All items must be crafted with shadow iron ingots"); break;
					case 3: list.Add("All items must be crafted with copper ingots"); break;
					case 4: list.Add("All items must be crafted with bronze ingots"); break;
					case 5: list.Add("All items must be crafted with gold ingots"); break;
					case 6: list.Add("All items must be crafted with agapite ingots"); break;
					case 7: list.Add("All items must be crafted with verite ingots"); break;
					case 8: list.Add("All items must be crafted with valorite ingots"); break;
					case 9: list.Add("All items must be crafted with pine"); break;
					case 10: list.Add("All items must be crafted with redwood"); break;
					case 11: list.Add("All items must be crafted with white pine"); break;
					case 12: list.Add("All items must be crafted with ashwood"); break;
					case 13: list.Add("All items must be crafted with silver birch"); break;
					case 14: list.Add("All items must be crafted with yew"); break;
					case 15: list.Add("All items must be crafted with black oak"); break;
			}

			switch ( m_Material )
			{
				case BulkMaterialType.DullCopper: list.Add("All items must be crafted with dull copper ingots"); break;
				case BulkMaterialType.ShadowIron: list.Add("All items must be crafted with shadow iron ingots"); break;
				case BulkMaterialType.Copper: list.Add("All items must be crafted with copper ingots"); break;
				case BulkMaterialType.Bronze: list.Add("All items must be crafted with bronze ingots"); break;
				case BulkMaterialType.Gold: list.Add("All items must be crafted with gold ingots"); break;
				case BulkMaterialType.Agapite: list.Add("All items must be crafted with agapite ingots"); break;
				case BulkMaterialType.Verite: list.Add("All items must be crafted with verite ingots"); break;
				case BulkMaterialType.Valorite: list.Add("All items must be crafted with valorite ingots"); break;
				case BulkMaterialType.Pine: list.Add("All items must be crafted with pine"); break;
				case BulkMaterialType.Redwood: list.Add("All items must be crafted with redwood"); break;
				case BulkMaterialType.WhitePine: list.Add("All items must be crafted with white pine"); break;
				case BulkMaterialType.Ashwood: list.Add("All items must be crafted with ashwood"); break;
				case BulkMaterialType.SilverBirch: list.Add("All items must be crafted with silver birch"); break;
				case BulkMaterialType.Yew: list.Add("All items must be crafted with yew"); break;
				case BulkMaterialType.BlackOak: list.Add("All items must be crafted with black oak"); break;
			}

			list.Add( 1060656, m_AmountMax.ToString() ); // amount to make: ~1_val~

			try
			{
				for ( int i = 0; i < m_Entries.Length; ++i )
				{
					m_Type =  m_Entries[i].Details.Type;
					m_Item = (Item)Activator.CreateInstance( m_Type );

					if ( m_Item.Name == null )
						list.Add( 1060658 + i, "#{0}\t{1}", m_Entries[i].Details.Number, m_Entries[i].Amount ); // ~1_val~: ~2_val~
					else
						list.Add("{0}:\t{1}", m_Item.Name, m_Entries[i].Amount );

					m_Item.Delete();
				}
			}
			catch
			{

			for ( int i = 0; i < m_Entries.Length; ++i )
				list.Add( 1060658 + i, "#{0}\t{1}", m_Entries[i].Details.Number, m_Entries[i].Amount ); // ~1_val~: ~2_val~
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.SendGump( new LargeBODGump( from, this ) );
			else
				from.SendLocalizedMessage( 1045156 ); // You must have the deed in your backpack to use it.
		}

		public void BeginCombine( Mobile from )
		{
			if ( !Complete )
				from.Target = new LargeBODTarget( this );
			else
				from.SendLocalizedMessage( 1045166 ); // The maximum amount of requested items have already been combined to this deed.
		}

		public void EndCombine( Mobile from, object o )
		{
			if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
			{
				if ( o is SmallBOD )
				{
					SmallBOD small = (SmallBOD)o;

					LargeBulkEntry entry = null;

					for ( int i = 0; entry == null && i < m_Entries.Length; ++i )
					{
						if ( m_Entries[i].Details.Type == small.Type )
							entry = m_Entries[i];
					}

					if ( entry == null )
					{
						from.SendLocalizedMessage( 1045160 ); // That is not a bulk order for this large request.
					}
					else if ( m_RequireExceptional && !small.RequireExceptional )
					{
						from.SendLocalizedMessage( 1045161 ); // Both orders must be of exceptional quality.
					}
					else if ( m_Material >= BulkMaterialType.DullCopper && m_Material <= BulkMaterialType.Valorite && small.Material != m_Material )
					{
						from.SendLocalizedMessage( 1045162 ); // Both orders must use the same ore type.
					}
					else if ( m_Material >= BulkMaterialType.Spined && m_Material <= BulkMaterialType.Barbed && small.Material != m_Material )
					{
						from.SendLocalizedMessage( 1049351 ); // Both orders must use the same leather type.
					}
					else if ( m_Material >= BulkMaterialType.Pine && m_Material <= BulkMaterialType.BlackOak && small.Material != m_Material )
					{
						from.SendMessage( "Both orders must use the same wood type." );
					}
					else if ( m_AmountMax != small.AmountMax )
					{
						from.SendLocalizedMessage( 1045163 ); // The two orders have different requested amounts and cannot be combined.
					}
					else if ( small.AmountCur < small.AmountMax )
					{
						from.SendLocalizedMessage( 1045164 ); // The order to combine with is not completed.
					}
					else if ( entry.Amount >= m_AmountMax )
					{
						from.SendLocalizedMessage( 1045166 ); // The maximum amount of requested items have already been combined to this deed.
					}
					else
					{
						entry.Amount += small.AmountCur;
						small.Delete();

						from.SendLocalizedMessage( 1045165 ); // The orders have been combined.

						from.SendGump( new LargeBODGump( from, this ) );

						if ( !Complete )
							BeginCombine( from );
					}
				}
				else
				{
					from.SendLocalizedMessage( 1045159 ); // That is not a bulk order.
				}
			}
			else
			{
				from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
			}
		}

		public LargeBOD( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_AmountMax );
			writer.Write( m_RequireExceptional );
			writer.Write( (int) m_Material );

			writer.Write( (int) m_Entries.Length );

			for ( int i = 0; i < m_Entries.Length; ++i )
				m_Entries[i].Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_AmountMax = reader.ReadInt();
					m_RequireExceptional = reader.ReadBool();
					m_Material = (BulkMaterialType)reader.ReadInt();

					m_Entries = new LargeBulkEntry[reader.ReadInt()];

					for ( int i = 0; i < m_Entries.Length; ++i )
						m_Entries[i] = new LargeBulkEntry( this, reader );

					break;
				}
			}

			if ( Weight == 0.0 )
				Weight = 1.0;

			if ( Core.AOS && ItemID == 0x14EF )
				ItemID = 0x2258;

			if ( Parent == null && Map == Map.Internal && Location == Point3D.Zero )
				Delete();
		}
	}
}