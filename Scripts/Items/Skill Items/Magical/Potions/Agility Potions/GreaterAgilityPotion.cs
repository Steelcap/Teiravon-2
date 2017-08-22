using System;
using Server;

namespace Server.Items
{
	public class GreaterAgilityPotion : BaseAgilityPotion
	{
		public override int DexOffset{ get{ return 20; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 5.0 ); } }

        
        [Constructable]
        public GreaterAgilityPotion()
            : this(1)
		{
		}

        [Constructable]
        public GreaterAgilityPotion(int amount)
            : base(PotionEffect.AgilityGreater)
        {
            Stackable = true;
            Amount = amount;
        }
        
		public GreaterAgilityPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GreaterAgilityPotion(amount), amount);
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