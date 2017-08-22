using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a bat corpse" )]
	public class FlyingFox : BaseCreature
	{
		[Constructable]
		public FlyingFox() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a flying fox";
			Body = 317;
			BaseSoundID = 0x270;
			Hue = 963;
			Level = 3;

			SetStr( 31, 47 );
			SetDex( 36, 60 );
			SetInt( 8, 20 );

			SetHits( 20, 27 );
			SetMana( 0 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 15.3, 30.0 );
			SetSkill( SkillName.Tactics, 18.1, 37.0 );
			SetSkill( SkillName.Wrestling, 20.1, 30.0 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 22;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 17.1;

		}

		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }

		public FlyingFox(Serial serial) : base(serial)
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