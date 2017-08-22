using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseGranite : Item, ICommodity
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
				return String.Format( "{0} {1} high quality granite", Amount, CraftResources.GetName( m_Resource ).ToLower() );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
			}
		}

		public BaseGranite( CraftResource resource ) : base( 0x1779 )
		{
			Weight = 10.0;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
            Stackable = true;
		}

		public BaseGranite( Serial serial ) : base( serial )
		{
		}

		public override int LabelNumber{ get{ return 1044607; } } // high quality granite

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

	public class Granite : BaseGranite
	{
        [Constructable]
        public Granite() : base( CraftResource.Iron )
		{
		}
        
        [Constructable]
        public Granite( int amount) : base( CraftResource.Iron )
		{
            Amount = amount;
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Granite(amount), amount);
        }

		public Granite( Serial serial ) : base( serial )
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

	public class DullCopperGranite : BaseGranite
	{
        [Constructable]
        public DullCopperGranite(int amount) : base( CraftResource.DullCopper )
        {
            Amount = amount;
        }

		[Constructable]
		public DullCopperGranite() : base( CraftResource.DullCopper )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new DullCopperGranite(amount), amount);
        }

		public DullCopperGranite( Serial serial ) : base( serial )
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

	public class ShadowIronGranite : BaseGranite
	{
		[Constructable]
		public ShadowIronGranite() : base( CraftResource.ShadowIron )
		{
		}

        [Constructable]
        public ShadowIronGranite(int amount) : base( CraftResource.ShadowIron )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new ShadowIronGranite(amount), amount);
        }

		public ShadowIronGranite( Serial serial ) : base( serial )
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

	public class CopperGranite : BaseGranite
	{
		[Constructable]
		public CopperGranite() : base( CraftResource.Copper )
		{
		}

        [Constructable]
        public CopperGranite(int amount) : base( CraftResource.Copper )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new CopperGranite(amount), amount);
        }

		public CopperGranite( Serial serial ) : base( serial )
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

	public class BronzeGranite : BaseGranite
	{
		[Constructable]
		public BronzeGranite() : base( CraftResource.Bronze )
		{
		}

        [Constructable]
        public BronzeGranite(int amount) : base( CraftResource.Bronze )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new BronzeGranite(amount), amount);
        }

		public BronzeGranite( Serial serial ) : base( serial )
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

	public class GoldGranite : BaseGranite
	{
		[Constructable]
		public GoldGranite() : base( CraftResource.Gold )
		{
		}

        [Constructable]
        public GoldGranite(int amount) : base( CraftResource.Gold )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GoldGranite(amount), amount);
        }

		public GoldGranite( Serial serial ) : base( serial )
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

	public class AgapiteGranite : BaseGranite
	{
		[Constructable]
		public AgapiteGranite() : base( CraftResource.Agapite )
		{
		}

        [Constructable]
        public AgapiteGranite(int amount) : base( CraftResource.Agapite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new AgapiteGranite(amount), amount);
        }

		public AgapiteGranite( Serial serial ) : base( serial )
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

	public class VeriteGranite : BaseGranite
	{
		[Constructable]
		public VeriteGranite() : base( CraftResource.Verite )
		{
		}

        [Constructable]
        public VeriteGranite(int amount) : base( CraftResource.Verite)
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new VeriteGranite(amount), amount);
        }

		public VeriteGranite( Serial serial ) : base( serial )
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

	public class ValoriteGranite : BaseGranite
	{
		[Constructable]
		public ValoriteGranite() : base( CraftResource.Valorite )
		{
		}

        [Constructable]
        public ValoriteGranite(int amount) : base( CraftResource.Valorite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new ValoriteGranite(amount), amount);
        }

		public ValoriteGranite( Serial serial ) : base( serial )
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
	
	public class MithrilGranite : BaseGranite
	{
		[Constructable]
		public MithrilGranite() : base( CraftResource.Mithril )
		{
		}

        [Constructable]
        public MithrilGranite(int amount) : base( CraftResource.Verite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new MithrilGranite(amount), amount);
        }

		public MithrilGranite( Serial serial ) : base( serial )
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

    public class SilverGranite : BaseGranite
    {
        [Constructable]
        public SilverGranite() : base(CraftResource.Silver)
        {
        }

        [Constructable]
        public SilverGranite(int amount) : base( CraftResource.Verite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new SilverGranite(amount), amount);
        }

        public SilverGranite(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

	public class BloodrockGranite : BaseGranite
	{
		[Constructable]
		public BloodrockGranite() : base( CraftResource.Bloodrock )
		{
		}

        [Constructable]
        public BloodrockGranite(int amount) : base( CraftResource.Verite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new BloodrockGranite(amount), amount);
        }

		public BloodrockGranite( Serial serial ) : base( serial )
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
	
	public class SteelGranite : BaseGranite
	{
		[Constructable]
		public SteelGranite() : base( CraftResource.Steel )
		{
		}

        [Constructable]
        public SteelGranite(int amount) : base( CraftResource.Verite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new SteelGranite(amount), amount);
        }

		public SteelGranite( Serial serial ) : base( serial )
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
	
	public class AdamantiteGranite : BaseGranite
	{
		[Constructable]
		public AdamantiteGranite() : base( CraftResource.Adamantite )
		{
		}

        [Constructable]
        public AdamantiteGranite(int amount) : base( CraftResource.Verite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new AdamantiteGranite(amount), amount);
        }

		public AdamantiteGranite( Serial serial ) : base( serial )
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
	
	public class IthilmarGranite : BaseGranite
	{
		[Constructable]
		public IthilmarGranite() : base( CraftResource.Ithilmar )
		{
		}

        [Constructable]
        public IthilmarGranite(int amount) : base( CraftResource.Verite )
        {
            Amount = amount;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new IthilmarGranite(amount), amount);
        }

		public IthilmarGranite( Serial serial ) : base( serial )
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
	
}
