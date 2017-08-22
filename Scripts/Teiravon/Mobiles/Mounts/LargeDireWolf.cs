using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a large wolf corpse" )]
	public class LargeDireWolf : BaseMount
	{
		[Constructable]
		public LargeDireWolf() : base( "a large dire wolf", 277, 16017, AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.1, 0.3 )
		{
			BaseSoundID = 0xE5;
			Hue = 1908;
			Female = false;

			SetStr( 95, 110 );
			SetDex( 75, 100 );
			SetInt( 16, 30 );
         

			SetHits( 50, 75 );
			SetMana( 0 );

			SetDamage( 15, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 55 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 55, 70 );

			SetSkill( SkillName.MagicResist, 15.1, 80.0 );
			SetSkill( SkillName.Tactics, 19.2, 49.0 );
			SetSkill( SkillName.Wrestling, 19.2, 49.0 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 100.0;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public LargeDireWolf( Serial serial ) : base( serial )
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