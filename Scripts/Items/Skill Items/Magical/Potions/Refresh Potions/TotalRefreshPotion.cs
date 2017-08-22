using System;
using Server;

namespace Server.Items
{
	public class TotalRefreshPotion : BaseRefreshPotion
	{
		public override double Refresh{ get{ return 1.0; } }

        [Constructable]
		public TotalRefreshPotion() : this( 1 )
		{
		}

        [Constructable]
        public TotalRefreshPotion(int amount) : base(PotionEffect.RefreshTotal)
        {
            Stackable = true;
            Amount = amount;
        }
        
		public TotalRefreshPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new TotalRefreshPotion(amount), amount);
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