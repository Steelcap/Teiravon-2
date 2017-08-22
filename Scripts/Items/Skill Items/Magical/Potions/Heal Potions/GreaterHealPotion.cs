using System;
using Server;

namespace Server.Items
{
	public class GreaterHealPotion : BaseHealPotion
	{
		public override int MinHeal { get { return (Core.AOS ? 20 : 9); } }
		public override int MaxHeal { get { return (Core.AOS ? 25 : 30); } }
		public override double Delay{ get{ return 30.0; } }

		[Constructable]
		public GreaterHealPotion() : this( 1 )
		{
		}

        [Constructable]
        public GreaterHealPotion(int amount) : base(PotionEffect.HealGreater)
        {
            Stackable = true;
            Amount = amount;
        }

		public GreaterHealPotion( Serial serial ) : base( serial )
		{
		}

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GreaterHealPotion(amount), amount);
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