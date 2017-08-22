using System;
using Server;

namespace Server.Items
{
	public class HealPotion : BaseHealPotion
	{
		public override int MinHeal { get { return (Core.AOS ? 13 : 6); } }
		public override int MaxHeal { get { return (Core.AOS ? 16 : 20); } }
		public override double Delay{ get{ return ( 20.0 ); } }

		[Constructable]
		public HealPotion() : this( 1 )
		{
		}

        [Constructable]
        public HealPotion(int amount) : base(PotionEffect.Heal)
        {
            Stackable = true;
            Amount = amount;
        }

        public HealPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new HealPotion(amount), amount);
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