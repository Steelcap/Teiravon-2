using System;
using Server;
using Server.Items;

namespace Server.Items
{
	#region Cantrips

	#region Cure Minor Wounds
	public class CureMinorWoundsScroll : SpellScroll
	{
		[Constructable]
		public CureMinorWoundsScroll() : this( 1 )
		{
		}

		[Constructable]
		public CureMinorWoundsScroll( int amount ) : base( 300, 0x1F31, amount )
		{
			Name = "Cure Minor Wounds Scroll";
			Hue = 2245;
		}

		public CureMinorWoundsScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new CureMinorWoundsScroll( amount ), amount );
		}
	}
	#endregion

	#endregion
}