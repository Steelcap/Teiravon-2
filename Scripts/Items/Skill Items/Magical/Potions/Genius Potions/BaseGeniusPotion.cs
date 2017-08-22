using System;
using Server;

namespace Server.Items
{
	public abstract class BaseGeniusPotion : BasePotion
	{
		public abstract int IntOffset{ get; }
		public abstract TimeSpan Duration{ get; }

        public BaseGeniusPotion(PotionEffect effect)
            : base(0x0F01, effect)
		{
		}

        public BaseGeniusPotion(Serial serial)
            : base(serial)
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

		public bool DoAgility( Mobile from )
		{
			// TODO: Verify scaled; is it offset, duration, or both?
			if ( Spells.SpellHelper.AddStatOffset( from, StatType.Int, Scale( from, IntOffset ), Duration ) )
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1EC );
				return true;
			}

			from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
			return false;
		}

		public override void Drink( Mobile from )
		{
			if ( DoAgility( from ) )
			{
				BasePotion.PlayDrinkEffect( from );

				this.Consume();
			}
		}
	}
}