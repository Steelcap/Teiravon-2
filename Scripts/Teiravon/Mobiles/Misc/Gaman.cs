using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a gaman corpse" )]
	public class Gaman : BaseCreature
	{
		[Constructable]
		public Gaman() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.4, 0.7 )
		{
			Name = "a gaman";
			Body = 248;
			BaseSoundID = 0x4F8;

			if ( 0.5 >= Utility.RandomDouble() )
				Hue = 0x901;

			SetStr( 77, 111 );
			SetDex( 100, 125 );
			SetInt( 47, 75 );

			SetHits( 300, 400);
			SetMana( 0 );

			SetDamage( 10, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 55 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 30, 50 );
			SetResistance( ResistanceType.Poison, 30, 50 );
			SetResistance( ResistanceType.Energy, 35, 40 );

			SetSkill( SkillName.MagicResist, 17.6, 50.0 );
			SetSkill( SkillName.Tactics, 67.6, 85.0 );
			SetSkill( SkillName.Wrestling, 40.1, 90.5 );

			Fame = 1000;
			Karma = 0;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 81.1;
		}

		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 15; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bull; } }

		public Gaman(Serial serial) : base(serial)
		{
		}

		public override int GetAttackSound() 
		{ 
			return 0x4F7; 
		} 

		public override int GetHurtSound() 
		{ 
			return 0x4FB; 
		} 

		public override int GetDeathSound() 
		{ 
			return 0x4F6; 
		} 
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}