using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Recurve : BaseRanged
	{

		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 0; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 21; } }
		public override int AosSpeed{ get{ return 24; } }
		public override int DefMaxRange{ get{ return 15; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 80; } }


		[Constructable]
		public Recurve() : base( 0x13B2 )
		{
			Name = "Recurve Bow";
			Weight = 12.0;
			Layer = Layer.TwoHanded;
		}

		public Recurve( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}

    public class GreatBow : BaseRanged
    {

        public override int EffectID { get { return 0xF42; } }
        public override Type AmmoType { get { return typeof(Arrow); } }
        public override Item Ammo { get { return new Arrow(); } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

        public override int AosStrengthReq { get { return 0; } }
        public override int AosMinDamage { get { return 20; } }
        public override int AosMaxDamage { get { return 23; } }
        public override int AosSpeed { get { return 14; } }
        public override int DefMaxRange { get { return 19; } }

        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.ShootBow; } }

        public override int InitMinHits { get { return 60; } }
        public override int InitMaxHits { get { return 80; } }


        [Constructable]
        public GreatBow()
            : base(0x27A5)
        {
            Name = "Great Bow";
            Weight = 22.0;
            Layer = Layer.TwoHanded;
        }

        public GreatBow(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}