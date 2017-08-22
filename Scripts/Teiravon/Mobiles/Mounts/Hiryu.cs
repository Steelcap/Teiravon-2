using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a giant bird corpse" )]
	public class Hiryu : BaseMount
	{
		[Constructable]
		public Hiryu() : base( "a hiryu", 243, 16020, AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.1, 0.3 )
		{
			BaseSoundID = 0x4FE;

			SetStr( 45, 65 );
			SetDex( 90, 120 );
			SetInt( 50, 70 );
           	Female = true;

			SetHits( 100, 120 );
			SetMana( 150, 200 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 35, 40 );
			SetResistance( ResistanceType.Cold, 35, 40 );
			SetResistance( ResistanceType.Poison, 35, 40 );
			SetResistance( ResistanceType.Energy, 35, 40 );

			SetSkill( SkillName.MagicResist, 15.1, 80.0 );
			SetSkill( SkillName.Tactics, 19.2, 49.0 );
			SetSkill( SkillName.Wrestling, 19.2, 49.0 );
			SetSkill( SkillName.Magery, 19.2, 44.0 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 100.0;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public Hiryu( Serial serial ) : base( serial )
		{
		}

		public override int GetAttackSound() 
		{ 
			return 0x4FD; 
		} 

		public override int GetHurtSound() 
		{ 
			return 0x500; 
		} 

		public override int GetDeathSound() 
		{ 
			return 0x4FC; 
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