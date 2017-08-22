using System;
using System.Collections;
using Server.Items;
using Server.Teiravon;

namespace Server.Items
{
	public class Walnut : Food
	{
		[Constructable]
		public Walnut() : this( 1 )
		{
		}

		[Constructable]
		public Walnut( int amount ) : base( amount, 2485 )
		{
			Name = "Walnut";
			Hue = 0x9ea;

			FillFactor = 1;
			Weight = .1;
		}

		public Walnut( Serial serial ) : base( serial )
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
	}

	public class Cherry : Food
	{
		[Constructable]
		public Cherry() : this( 1 )
		{
		}

		[Constructable]
		public Cherry( int amount ) : base( amount, 2512 )
		{
			Name = "Cherry";
			Hue = 2455;

			FillFactor = 1;
			Weight = .1;
		}

		public Cherry( Serial serial ) : base( serial )
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
	}

	public class FruitTree : Item
	{
		public enum FruitType {
			Apple,
			Peach,
			Cherry,
			Pear,
			Walnut
		}

		private FruitType m_FruitType = FruitType.Apple;
		private int m_Amount = 0;
		private ArrayList m_Deco = new ArrayList();

		public ArrayList Deco { get { return m_Deco; } set { m_Deco = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public FruitType Fruit { get { return m_FruitType; } set { m_FruitType = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int FruitAmount { get { return m_Amount; } set { m_Amount = value; } }

		public override void OnDoubleClick( Mobile from )
		{
			base.OnDoubleClick( from );

			if ( FruitAmount > 0 )
			{
				switch ( Fruit )
				{
					case FruitType.Apple:
						from.Backpack.DropItem( new Apple() );
						FruitAmount -= 1;

						break;
					
					case FruitType.Peach:
						from.Backpack.DropItem( new Peach() );
						FruitAmount -= 1;

						break;
					
					case FruitType.Cherry:
						from.Backpack.DropItem( new Cherry() );
						FruitAmount -= 1;
						
						break;
					
					case FruitType.Pear:
						from.Backpack.DropItem( new Pear() );
						FruitAmount -= 1;

						break;
					
					case FruitType.Walnut:
						from.Backpack.DropItem( new Walnut() );
						FruitAmount -= 1;

						break;
				}
			} else if ( FruitAmount == 0 ) {
				for ( int i = 0; i < m_Deco.Count; i++ )
				{
					((Item)m_Deco[i]).Delete();
				}

				m_Deco = new ArrayList();

				InternalTimer theTimer = new InternalTimer( this );
				theTimer.Start();

				FruitAmount = -1;

				from.SendMessage( "You can't seem to find anything." );
			} else {
				from.SendMessage( "You can't seem to find anything." );
			}
		}

		[Constructable]
		public FruitTree() : base( 3277 )
		{
			Name = "tree";
			Movable = false;

			FruitAmount = Utility.RandomMinMax( 5, 10 );
		}

		public FruitTree( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_FruitType );
			writer.Write( m_Amount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_FruitType = (FruitType)reader.ReadInt();
			m_Amount = reader.ReadInt();

			if ( FruitAmount == 0 )
			{
				InternalTimer theTimer = new InternalTimer( this );
				theTimer.Start();

				FruitAmount = -1;
			}
		}

		private class InternalTimer : Timer
		{
			private FruitTree m_Tree;

			public InternalTimer( FruitTree tree ) : base( TimeSpan.FromMinutes( 30.0 ) )
			{
				m_Tree = tree;
			}

			protected override void OnTick()
			{
				if ( m_Tree == null )
					return;

				m_Tree.FruitAmount += Utility.RandomMinMax( 5, 10 );

				switch ( m_Tree.Fruit )
				{
					case FruitType.Apple:
						m_Tree.Deco.Add( new Apple() );
						m_Tree.Deco.Add( new Apple() );
						m_Tree.Deco.Add( new Apple() );
						m_Tree.Deco.Add( new Apple() );

						break;
					
					case FruitType.Peach:
						m_Tree.Deco.Add( new Peach() );
						m_Tree.Deco.Add( new Peach() );
						m_Tree.Deco.Add( new Peach() );
						m_Tree.Deco.Add( new Peach() );

						break;
					
					case FruitType.Cherry:
						m_Tree.Deco.Add( new Cherry() );
						m_Tree.Deco.Add( new Cherry() );
						m_Tree.Deco.Add( new Cherry() );
						m_Tree.Deco.Add( new Cherry() );
						
						break;
					
					case FruitType.Pear:
						m_Tree.Deco.Add( new Pear() );
						m_Tree.Deco.Add( new Pear() );
						m_Tree.Deco.Add( new Pear() );
						m_Tree.Deco.Add( new Pear() );

						break;
					
					case FruitType.Walnut:
						m_Tree.Deco.Add( new Walnut() );
						m_Tree.Deco.Add( new Walnut() );
						m_Tree.Deco.Add( new Walnut() );
						m_Tree.Deco.Add( new Walnut() );

						break;
				}

				for ( int i = 0; i < m_Tree.Deco.Count; i++ )
				{
					Item fruit =  (Item)m_Tree.Deco[ i ];

					fruit.MoveToWorld( new Point3D( m_Tree.X + Utility.RandomMinMax( -1, 1 ), m_Tree.Y + Utility.RandomMinMax( -1, 1 ), Utility.RandomMinMax( 15, 20 ) ), m_Tree.Map );
					fruit.Movable = false;
				}
			}
		}
	}
}