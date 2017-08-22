using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a red wolf corpse" )]
	public class RedWolf : BaseCreature
	{
		[Constructable]
		public RedWolf() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a red wolf";
			Body = Utility.RandomList( 34, 37 );
			Hue = Utility.RandomList (437, 439, 438);
			BaseSoundID = 0xE5;

			SetStr( 56, 80 );
			SetDex( 66, 85 );
			SetInt( 31, 55 );

			SetHits( 60, 80 );
			SetMana( 0 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 30, 45 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 20.1, 35.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 80.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 20;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 65.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public RedWolf( Serial serial ) : base( serial )
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