using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a horse corpse" )]
	public class SerudianStallion : BaseMount
	{
		[Constructable]
		public SerudianStallion() : base( "A serudian stallion", 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.1, 0.3 )
		{
			BaseSoundID = 0xA8;
			Hue = Utility.RandomList(962, 843, 845, 747);

			SetStr( 21, 49 );
			SetDex( 56, 75 );
			SetInt( 16, 30 );
            Female = false;

			SetHits( 80, 100 );
			SetMana( 0 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 55 );
			SetResistance( ResistanceType.Fire, 20, 40 );
			SetResistance( ResistanceType.Cold, 20, 40 );
			SetResistance( ResistanceType.Poison, 20, 40 );
			SetResistance( ResistanceType.Energy, 20, 40 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 19.2, 29.0 );
			SetSkill( SkillName.Wrestling, 19.2, 29.0 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 100.0;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public SerudianStallion( Serial serial ) : base( serial )
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