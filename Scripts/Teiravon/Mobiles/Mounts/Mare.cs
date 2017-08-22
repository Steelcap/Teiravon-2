using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a horse corpse" )]
	public class Mare : BaseMount
	{
		[Constructable]
		public Mare() : base( "A mare", 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.1, 0.3 )
		{
			BaseSoundID = 0xA8;
			Hue = Utility.RandomList(1878, 1874, 1894, 1849, 1821, 1908, 1815);

			SetStr( 21, 49 );
			SetDex( 56, 75 );
			SetInt( 16, 30 );
            	Female = true;

			SetHits( 50, 75 );
			SetMana( 0 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 40 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 15.1, 80.0 );
			SetSkill( SkillName.Tactics, 19.2, 49.0 );
			SetSkill( SkillName.Wrestling, 19.2, 49.0 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 95.0;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public Mare( Serial serial ) : base( serial )
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