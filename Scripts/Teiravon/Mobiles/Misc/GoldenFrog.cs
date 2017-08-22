using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a golden frog corpse" )]
	public class GoldenFrog : BaseCreature
	{
		[Constructable]
		public GoldenFrog() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a golden frog";
			Body = 81;
			Hue = 2233;
			BaseSoundID = 0x266;

			SetStr( 46, 70 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 20, 40 );
			SetMana( 0 );

			SetDamage( 1, 2 );

			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );
			SetResistance( ResistanceType.Poison, 90, 99 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );
			SetSkill( SkillName.Poisoning, 120.1, 140.0 );

			Fame = 350;
			Karma = 0;

			VirtualArmor = 6;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 63.1;

		}

public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 4; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }

		public GoldenFrog(Serial serial) : base(serial)
		{
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