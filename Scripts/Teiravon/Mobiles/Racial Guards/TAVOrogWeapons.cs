using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13B6, 0x13B5 )]
	public class OrogScimitar : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 130; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 30; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 200; } }

		[Constructable]
		public OrogScimitar() : base( 0x13B6 )
		{
			Name = "Magma-Forged Scimitar";
			Attributes.WeaponDamage = 10;
			Hue = 0xA42;
			Weight = 15.0;
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 50;
			fire = 15;
			cold = 10;
			pois = 15;
			nrgy = 10;
		}		

		public OrogScimitar( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xF5E, 0xF5F )]
	public class OrogBroadsword : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }

		public override int AosStrengthReq{ get{ return 130; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 28; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 200; } }

		[Constructable]
		public OrogBroadsword() : base( 0xF5E )
		{
			Name = "Magma-Forged Broadsword";
			Hue = 0xA42;
			Attributes.WeaponSpeed = 10;
			Weight = 13.0;
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 60;
			fire = 10;
			cold = 15;
			pois = 15;
			nrgy = 0;
		}		

		public OrogBroadsword( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x13B9, 0x13Ba )]
	public class OrogVikingSword : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 160; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 19; } }
		public override int AosSpeed{ get{ return 26; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 120; } }
		public override int InitMaxHits{ get{ return 200; } }

		[Constructable]
		public OrogVikingSword() : base( 0x13B9 )
		{
			Name = "Magma-Forged Heavy Blade";
			Hue = 0xA3B;
			Attributes.DefendChance = 15;
			Weight = 20.0;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 60;
			fire = 10;
			cold = 10;
			pois = 15;
			nrgy = 5;
		}

		public OrogVikingSword( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1401, 0x1400 )]
	public class OrogKryss : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 135; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 32; } }

		public override int DefHitSound{ get{ return 0x23C; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 130; } }
		public override int InitMaxHits{ get{ return 150; } }

		[Constructable]
		public OrogKryss() : base( 0x1401 )
		{
			Name = "Magma-Forged Jagged Blade";
			Hue = 0xA3B;
			Attributes.ReflectPhysical = 20;
			Weight = 25.0;
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 60;
			fire = 15;
			cold = 5;
			pois = 15;
			nrgy = 5;
		}
		
		public OrogKryss( Serial serial ) : base( serial )
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