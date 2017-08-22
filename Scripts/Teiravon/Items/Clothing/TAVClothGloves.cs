using System;

namespace Server.Items
{

	public abstract class BaseGloves : BaseClothing
	{
		public BaseGloves( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGloves( int itemID, int hue ) : base( itemID, Layer.Gloves, hue )
		{
		}

		public BaseGloves( Serial serial ) : base( serial )
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


[Flipable( 0x13C6, 0x13CE )]
public class ClothGloves : BaseGloves
	{
		
		[Constructable]
		public ClothGloves() : this( 0 )
		{
		}

		[Constructable]
		public ClothGloves( int hue ) : base( 0x13C6 )
		{
			Name = "Cloth Gloves";
			Weight = 1.0;
			base.Hue = 0x388;
		}

		public ClothGloves( Serial serial ) : base( serial )
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
