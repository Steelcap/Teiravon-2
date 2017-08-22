using System;
using Server;

namespace Server.Items
{
	public class RefreshPotion : BaseRefreshPotion
	{
		public override double Refresh{ get{ return 0.1; } }

        [Constructable]
		public RefreshPotion() : this( 1 )
		{
		}

        [Constructable]
        public RefreshPotion(int amount) : base(PotionEffect.Refresh)
        {
            Stackable = true;
            Amount = amount;
        }
        
		public RefreshPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new RefreshPotion(amount), amount);
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