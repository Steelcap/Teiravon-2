using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dread horn corpse" )]
	public class Dreadhorn : BaseCreature
	{
		public override bool AllowFemaleTamer{ get{ return false; } }

		[Constructable]
		public Dreadhorn() : this( "a dread horn" )
		{
		}

		[Constructable]
		public Dreadhorn( string name ) : base(AIType.AI_Mage, FightMode.Agressor, 10, 1, 0.1, 0.3 )
		{
			BaseSoundID = 0x4BC;

			SetStr( 140, 175 );
			SetDex( 96, 115 );
			SetInt( 186, 225 );

			SetHits( 120, 180 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 50, 65 );
			SetResistance( ResistanceType.Energy, 35, 50 );

			SetSkill( SkillName.EvalInt, 80.1, 90.0 );
			SetSkill( SkillName.Magery, 60.2, 80.0 );
			SetSkill( SkillName.Meditation, 50.1, 60.0 );
			SetSkill( SkillName.MagicResist, 75.3, 90.0 );
			SetSkill( SkillName.Tactics, 20.1, 22.5 );
			SetSkill( SkillName.Wrestling, 80.5, 92.5 );

			Fame = 9000;
			Karma = -9000;

			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 100.0;
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public Dreadhorn( Serial serial ) : base( serial )
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