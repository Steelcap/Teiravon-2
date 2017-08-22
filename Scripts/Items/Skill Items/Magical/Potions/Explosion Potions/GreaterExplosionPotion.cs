using System;
using Server;

namespace Server.Items
{
	public class GreaterExplosionPotion : BaseExplosionPotion
	{
		public override int MinDamage { get {  return 15; } }
		public override int MaxDamage { get {  return 30; } }

        [Constructable]
        public GreaterExplosionPotion()
            : base(PotionEffect.ExplosionGreater)
        {
            Stackable = false;
        }

        public GreaterExplosionPotion(Serial serial)
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
	}
}