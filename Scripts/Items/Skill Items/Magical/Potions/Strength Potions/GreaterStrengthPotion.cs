using System;
using Server;

namespace Server.Items
{
	public class GreaterStrengthPotion : BaseStrengthPotion
	{
		public override int StrOffset{ get{ return 20; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 5.0 ); } }

        [Constructable]
		public GreaterStrengthPotion() : this( 1 )
		{
		}

        [Constructable]
        public GreaterStrengthPotion(int amount) : base(PotionEffect.StrengthGreater)
        {
            Stackable = true;
            Amount = amount;
        }
        
		public GreaterStrengthPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GreaterStrengthPotion(amount), amount);
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