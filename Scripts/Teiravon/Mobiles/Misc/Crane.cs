using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a crane corpse" )]
    public class Crane : BaseCreature
	{
		[Constructable]
		public Crane() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a crane";
			Body = 254;
			BaseSoundID = 0x4D9;

			SetStr( 5 );
			SetDex( 15 );
			SetInt( 5 );

			SetHits( 3 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 1, 5 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 2;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 100;
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }

		public override int Feathers{ get{ return 25; } }

		public Crane(Serial serial) : base(serial)
		{
		}

		public override int GetAttackSound() 
		{ 
			return 0x4D8; 
		} 

		public override int GetHurtSound() 
		{ 
			return 0x4DB; 
		} 

		public override int GetDeathSound() 
		{ 
			return 0x4D7; 
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