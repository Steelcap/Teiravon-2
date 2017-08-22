using System;
using Server;

namespace Server.Items
{
	public class AgilityPotion : BaseAgilityPotion
	{
		public override int DexOffset{ get{ return 10; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 5.0 ); } }

        [Constructable]
		public AgilityPotion() : this( 1 )
		{
		}

        [Constructable]
        public AgilityPotion(int amount) : base(PotionEffect.Agility)
        {
            Stackable = true;
            Amount = amount;
        }
        
		public AgilityPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new AgilityPotion(amount), amount);
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