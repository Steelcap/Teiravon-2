using System;
using Server.Items;

namespace Server.Items
{
	public abstract class BaseLog : Item, ICommodity
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
				return String.Format( Amount == 1 ? "{0} {1} log" : "{0} {1} logs", Amount, CraftResources.GetName( m_Resource ).ToLower() );
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

		public BaseLog( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseLog( CraftResource resource, int amount ) : base( 0x1bdd )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseLog( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t#{1}", Amount, 1044466 ); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add( 1044466 ); // Logs
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
	
	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class Log : BaseLog
	{
		[Constructable]
		public Log() : this( 1 )
		{
		}

		[Constructable]
		public Log( int amount ) : base( CraftResource.Oak, amount )
		{
		}

		public Log( Serial serial ) : base( serial )
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
			return base.Dupe( new Log( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class PineLog : BaseLog
	{
		[Constructable]
		public PineLog() : this( 1 )
		{
		}

		[Constructable]
		public PineLog( int amount ) : base( CraftResource.Pine, amount )
		{
		}

		public PineLog( Serial serial ) : base( serial )
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
			return base.Dupe( new PineLog( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class RedwoodLog : BaseLog
	{
		[Constructable]
		public RedwoodLog() : this( 1 )
		{
		}

		[Constructable]
		public RedwoodLog( int amount ) : base( CraftResource.Redwood, amount )
		{
		}

		public RedwoodLog( Serial serial ) : base( serial )
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
			return base.Dupe( new RedwoodLog( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class WhitePineLog : BaseLog
	{
		[Constructable]
		public WhitePineLog() : this( 1 )
		{
		}

		[Constructable]
		public WhitePineLog( int amount ) : base( CraftResource.WhitePine, amount )
		{
		}

		public WhitePineLog( Serial serial ) : base( serial )
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
			return base.Dupe( new WhitePineLog( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class AshwoodLog : BaseLog
	{
		[Constructable]
		public AshwoodLog() : this( 1 )
		{
		}

		[Constructable]
		public AshwoodLog( int amount ) : base( CraftResource.Ashwood, amount )
		{
		}

		public AshwoodLog( Serial serial ) : base( serial )
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
			return base.Dupe( new AshwoodLog( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class SilverBirchLog : BaseLog
	{
		[Constructable]
		public SilverBirchLog() : this( 1 )
		{
		}

		[Constructable]
		public SilverBirchLog( int amount ) : base( CraftResource.SilverBirch, amount )
		{
		}

		public SilverBirchLog( Serial serial ) : base( serial )
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
			return base.Dupe( new SilverBirchLog( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class YewLog : BaseLog
	{
		[Constructable]
		public YewLog() : this( 1 )
		{
		}

		[Constructable]
		public YewLog( int amount ) : base( CraftResource.Yew, amount )
		{
		}

		public YewLog( Serial serial ) : base( serial )
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
			return base.Dupe( new YewLog( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1bdd, 0x1be0 )]
	public class BlackOakLog : BaseLog
	{
		[Constructable]
		public BlackOakLog() : this( 1 )
		{
		}

		[Constructable]
		public BlackOakLog( int amount ) : base( CraftResource.BlackOak, amount )
		{
		}

		public BlackOakLog( Serial serial ) : base( serial )
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
			return base.Dupe( new BlackOakLog( amount ), amount );
		}
	}
}