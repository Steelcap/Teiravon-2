using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class Dragon : BaseCreature
	{
		[Constructable]
		public Dragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dragon";
			Body = 46;
			BaseSoundID = 362;
			Level = 19;
			Hue = Utility.RandomList( 1645, 2216);

			SetStr( 796, 825 );
			SetDex( 120, 150 );
			SetInt( 436, 475 );

			SetHits( 1201, 1600 );

			SetDamage( 18, 26 );

			RangeFight = 1;

			SetDamageType( ResistanceType.Physical, 80 );

			SetDamageType( ResistanceType.Physical, 20 );

//			SetResistance( ResistanceType.Physical, 55, 65 );
//			SetResistance( ResistanceType.Fire, 60, 70 );
//			SetResistance( ResistanceType.Cold, 30, 40 );
//			SetResistance( ResistanceType.Poison, 45, 55 );
//			SetResistance( ResistanceType.Energy, 35, 45 );
			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 65, 80 );
			SetResistance( ResistanceType.Cold, 20, 35 );
			SetResistance( ResistanceType.Poison, 45, 55 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			SetSkill( SkillName.DetectHidden, 30.1, 75.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 60;

			Tamable = false;
			ControlSlots = 3;
			MinTameSkill = 93.9;

			PackGem( 8 );
			AddItem(new MobRange(2));
			PackScroll( 1, 6 );
			PackScroll( 4, 6 );
			PackScroll( 7, 8 );
			PackGold( 300, 400);
			PackItem( Loot.RandomJewelry() );
			PackItem( Loot.RandomArmorOrShieldOrWeaponOrJewelry() );
			PackItem( Loot.RandomArmorOrShieldOrWeaponOrJewelry() );
			PackItem( Loot.RandomStatue() );

			if ( Utility.RandomDouble() < 0.25 )
				PackItem( new BraceletOfBinding() );
			
			if (0.15 > Utility.RandomDouble())
				PackItem( new WyrmHeart(1) );
			if (0.15 > Utility.RandomDouble())
				PackItem( new DragonBlood(Utility.RandomMinMax(1,3)) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( Hue == 1645 ? ScaleType.Yellow : ScaleType.Red ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public Dragon( Serial serial ) : base( serial )
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
		}
	}
}
