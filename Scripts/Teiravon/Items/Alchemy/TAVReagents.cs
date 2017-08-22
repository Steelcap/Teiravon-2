using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TeiravonReagent : BaseReagent, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( "{0} {1}", Amount, Name );
			}
		}

		public TeiravonReagent( int itemid, int amount ) : base( itemid, amount )
		{
		}

		public TeiravonReagent( Serial serial ) : base( serial )
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

	public class WyrmHeart : TeiravonReagent
	{
		[Constructable]
		public WyrmHeart() : this( 1 )
		{
		}

		[Constructable]
		public WyrmHeart( int amount ) : base( 0xF91, amount )
		{
		}

		public WyrmHeart( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new WyrmHeart( amount ), amount );
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

	public class Pumice : TeiravonReagent
	{
		[Constructable]
		public Pumice() : this( 1 )
		{
		}

		[Constructable]
		public Pumice( int amount ) : base( 0xF8B, amount )
		{
		}

		public Pumice( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Pumice( amount ), amount );
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

	public class EyeOfNewt : TeiravonReagent
	{
		[Constructable]
		public EyeOfNewt() : this( 1 )
		{
		}

		[Constructable]
		public EyeOfNewt( int amount ) : base( 0xF87, amount )
		{
		}

		public EyeOfNewt( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new EyeOfNewt( amount ), amount );
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

	public class DragonBlood : TeiravonReagent
	{
		[Constructable]
		public DragonBlood() : this( 1 )
		{
		}

		[Constructable]
		public DragonBlood( int amount ) : base( 0xF82, amount )
		{
		}

		public DragonBlood( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new DragonBlood( amount ), amount );
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