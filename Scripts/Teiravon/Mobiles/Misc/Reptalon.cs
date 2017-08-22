using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a reptalon corpse" )]
	public class Reptalon : BaseMount
	{
		[Constructable]
		public Reptalon() : base( "a reptalon", 0x114, 16016, AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.1, 0.3 )
		{
			BaseSoundID = 362;

			SetStr( 40, 70 );
			SetDex( 70, 90 );
			SetInt( 16, 30 );
            Female = true;

			SetHits( 50, 75 );
			SetMana( 0 );

			SetDamage( 13, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 15.1, 50.0 );
			SetSkill( SkillName.Tactics, 19.2, 79.0 );
			SetSkill( SkillName.Wrestling, 19.2, 79.0 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 95.0;
		}

		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 6; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Green; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public Reptalon( Serial serial ) : base( serial )
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