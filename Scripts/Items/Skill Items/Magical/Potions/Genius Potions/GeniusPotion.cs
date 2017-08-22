using System;
using Server;

namespace Server.Items
{
	public class GeniusPotion : BaseGeniusPotion
	{
		public override int IntOffset{ get{ return 10; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 2.0 ); } }

        [Constructable]
		public GeniusPotion() : this( 1 )
		{
		}

        [Constructable]
        public GeniusPotion(int amount) : base(PotionEffect.Genius)
        {
            Stackable = true;
            Amount = amount;
        }

        public GeniusPotion(Serial serial)
            : base(serial)
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GeniusPotion(amount), amount);
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