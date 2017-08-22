using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a coyote corpse" )]
	public class Coyote : BaseCreature
	{
		[Constructable]
		public Coyote() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a coyote";
			Body = 23;
			Hue = Utility.RandomList (1850, 1846, 1821);
			BaseSoundID = 0xE5;

			SetStr( 96, 120 );
			SetDex( 91, 105 );
			SetInt( 36, 60 );

			SetHits( 72, 100 );
			SetMana( 0 );

			SetDamage( 13, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 57.6, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 26;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 83.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 7; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public Coyote(Serial serial) : base(serial)
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