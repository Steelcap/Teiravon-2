using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal dragon corpse" )]
	public class SkeletalDragon : BaseCreature
	{
		[Constructable]
		public SkeletalDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a skeletal dragon";
			Body = 104;
			BaseSoundID = 0x488;
			Level = 19;

			SetStr( 1000, 1200 );
			SetDex( 100, 250 );
			SetInt( 488, 620 );

			SetHits( 1600, 2000 );

			SetDamage( 29, 35 );

			RangeFight = 1;

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 40, 60 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.3, 130.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			SetSkill( SkillName.DetectHidden, 60.1, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;


				PackScroll( 1, 6 );
				PackScroll( 4, 6 );
				PackScroll( 7, 8 );
				PackNecroScroll( Utility.RandomMinMax( 0, 15 ) );
				PackNecroScroll( Utility.RandomMinMax( 0, 15 ) );
				PackNecroScroll( Utility.RandomMinMax( 0, 15 ) );
				PackItem( Loot.RandomArmorOrShieldOrWeaponOrJewelry() );

//			PackGold( 750, 1000 );
			PackItem( Loot.RandomJewelry() );
			PackItem( Loot.RandomStatue() );
			
			if (0.20 > Utility.RandomDouble())
				PackItem( new WyrmHeart(1) );
			if (0.20 > Utility.RandomDouble())
				PackItem( new DragonBlood(Utility.RandomMinMax(1,2)) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x480; } }

		// TODO: Undead summoning?

		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int Meat{ get{ return 19; } } // where's it hiding these? :)
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

		public SkeletalDragon( Serial serial ) : base( serial )
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
