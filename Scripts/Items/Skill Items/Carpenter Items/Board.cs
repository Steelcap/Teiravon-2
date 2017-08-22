using System;

namespace Server.Items
{
	public abstract class BaseWoodBoard : Item, ICommodity
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}
		
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} board" : "{0} {1} boards", Amount, CraftResources.GetName( m_Resource ).ToLower() );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case 0:
				{
					OreInfo info;

					switch ( reader.ReadInt() )
					{
						case 0: info = OreInfo.Oak; break;
						case 1: info = OreInfo.Pine; break;
						case 2: info = OreInfo.Redwood; break;
						case 3: info = OreInfo.WhitePine; break;
						case 4: info = OreInfo.Ashwood; break;
						case 5: info = OreInfo.SilverBirch; break;
						case 6: info = OreInfo.Yew; break;
						case 7: info = OreInfo.BlackOak; break;
                        default: info = null; break;
					}

					m_Resource = CraftResources.GetFromOreInfo( info );
					break;
				}
			}
		}

		public BaseWoodBoard( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseWoodBoard( CraftResource resource, int amount ) : base( 0x1BD7 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseWoodBoard( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t#{1}", Amount, 1015101 ); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add( 1015101 ); // boards
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !CraftResources.IsStandard( m_Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( m_Resource ) );
			}
		}

		
	}	

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class Board : BaseWoodBoard
	{
		[Constructable]
		public Board() : this( 1 )
		{
		}

		[Constructable]
		public Board( int amount ) : base( CraftResource.Oak, amount )
		{
		}

		public Board( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Board( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class PineBoard : BaseWoodBoard
	{
		[Constructable]
		public PineBoard() : this( 1 )
		{
		}

		[Constructable]
		public PineBoard( int amount ) : base( CraftResource.Pine, amount )
		{
		}

		public PineBoard( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new PineBoard( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class RedwoodBoard : BaseWoodBoard
	{
		[Constructable]
		public RedwoodBoard() : this( 1 )
		{
		}

		[Constructable]
		public RedwoodBoard( int amount ) : base( CraftResource.Redwood, amount )
		{
		}

		public RedwoodBoard( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new RedwoodBoard( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class WhitePineBoard : BaseWoodBoard
	{
		[Constructable]
		public WhitePineBoard() : this( 1 )
		{
		}

		[Constructable]
		public WhitePineBoard( int amount ) : base( CraftResource.WhitePine, amount )
		{
		}

		public WhitePineBoard( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new WhitePineBoard( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class AshwoodBoard : BaseWoodBoard
	{
		[Constructable]
		public AshwoodBoard() : this( 1 )
		{
		}

		[Constructable]
		public AshwoodBoard( int amount ) : base( CraftResource.Ashwood, amount )
		{
		}

		public AshwoodBoard( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new AshwoodBoard( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class SilverBirchBoard : BaseWoodBoard
	{
		[Constructable]
		public SilverBirchBoard() : this( 1 )
		{
		}

		[Constructable]
		public SilverBirchBoard( int amount ) : base( CraftResource.SilverBirch, amount )
		{
		}

		public SilverBirchBoard( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new SilverBirchBoard( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class YewBoard : BaseWoodBoard
	{
		[Constructable]
		public YewBoard() : this( 1 )
		{
		}

		[Constructable]
		public YewBoard( int amount ) : base( CraftResource.Yew, amount )
		{
		}

		public YewBoard( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new YewBoard( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BD7, 0x1BDA )]
	public class BlackOakBoard : BaseWoodBoard
	{
		[Constructable]
		public BlackOakBoard() : this( 1 )
		{
		}

		[Constructable]
		public BlackOakBoard( int amount ) : base( CraftResource.BlackOak, amount )
		{
		}

		public BlackOakBoard( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new BlackOakBoard( amount ), amount );
		}
	}
}