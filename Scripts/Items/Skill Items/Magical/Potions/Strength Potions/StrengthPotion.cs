using System;
using Server;

namespace Server.Items
{
	public class StrengthPotion : BaseStrengthPotion
	{
		public override int StrOffset{ get{ return 10; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 5.0 ); } }

        [Constructable]
		public StrengthPotion() : this( 1 )
		{
		}

        [Constructable]
        public StrengthPotion(int amount) : base(PotionEffect.Strength)
        {
            Stackable = true;
            Amount = amount;
        }
        
		public StrengthPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new StrengthPotion(amount), amount);
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