using System;
using Server;

namespace Server.Items
{
	public class GreaterGeniusPotion : BaseGeniusPotion
	{
		public override int IntOffset{ get{ return 20; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 2.0 ); } }

        
        [Constructable]
        public GreaterGeniusPotion()
            : this(1)
		{
		}

        [Constructable]
        public GreaterGeniusPotion(int amount)
            : base(PotionEffect.GeniusGreater)
        {
            Stackable = true;
            Amount = amount;
        }

        public GreaterGeniusPotion(Serial serial)
            : base(serial)
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GreaterGeniusPotion(amount), amount);
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