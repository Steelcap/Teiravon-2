using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a white wyrm corpse" )]
	public class WhiteWyrm : BaseCreature
	{
		[Constructable]
		public WhiteWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = Core.AOS ? 180 : 49;
			Name = "a white wyrm";
			BaseSoundID = 362;
			Level = 19;

			SetStr( 1000, 1250 );
			SetDex( 180, 250 );
			SetInt( 386, 425 );

			SetHits( 1500, 1750 );

			SetDamage( 19, 26 );

			RangeFight = 1;

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			
			SetSkill( SkillName.DetectHidden, 35.1, 80.0 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 64;

			Tamable = false;
			ControlSlots = 3;
			MinTameSkill = 96.3;
			
			if (0.20 > Utility.RandomDouble())
				PackItem( new WyrmHeart(1) );
			if (0.20 > Utility.RandomDouble())
				PackItem( new DragonBlood(Utility.RandomMinMax(3,5)) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			//AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}

		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Scales{ get{ return 9; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override ScaleType ScaleType{ get{ return ScaleType.White; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Gold; } }

		public WhiteWyrm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Core.AOS && Body == 49 )
				Body = 180;
		}
	}
}
