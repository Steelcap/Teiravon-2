using System;
using Server;

namespace Server.Factions
{
	public class Silver : Item
	{
		[Constructable]
		public Silver() : this( 1 )
		{
		}

		[Constructable]
		public Silver( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Silver( int amount ) : base( 0xEF0 )
		{
			Stackable = true;
			Weight = 0.02;
			Amount = amount;
		}

		public Silver( Serial serial ) : base( serial )
		{
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
		}
        protected override void OnAmountChange(int oldValue)
        {
            TotalGold = (int)(Amount / 10);
        }

        public override void UpdateTotals()
        {
            base.UpdateTotals();

            SetTotalGold((int)(this.Amount / 10));
        }

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Silver( amount ), amount );
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