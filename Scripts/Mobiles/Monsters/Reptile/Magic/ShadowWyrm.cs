using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a shadow wyrm corpse" )]
	public class ShadowWyrm : BaseCreature
	{
		[Constructable]
		public ShadowWyrm() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a shadow wyrm";
			Body = 46;
			BaseSoundID = 362;
			Level = 20;
			Hue = 0x4001;

			SetStr( 1000, 1200 );
			SetDex( 100, 225 );
			SetInt( 488, 620 );

			SetHits( 1600, 2000 );

			SetDamage( 31, 36 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 65, 85 );
			SetResistance( ResistanceType.Fire, 50, 70 );
			SetResistance( ResistanceType.Cold, 45, 55 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.Meditation, 52.5, 75.0 );
			SetSkill( SkillName.MagicResist, 100.3, 130.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );
			
			SetSkill( SkillName.DetectHidden, 80.1, 120.0 );


			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;

			//PackGold( 110, 200 );
			PackGem( 5 );

				PackScroll( 1, 6 );
				PackScroll( 4, 6 );
				PackScroll( 7, 8 );
				PackItem( Loot.RandomArmorOrShieldOrWeaponOrJewelry() );

			//PackGold( 950, 1800 );
			PackItem( Loot.RandomJewelry() );
			PackItem( Loot.RandomStatue() );
			
			if (0.20 > Utility.RandomDouble())
				PackItem( new WyrmHeart(1) );
			if (0.20 > Utility.RandomDouble())
				PackItem( new DragonBlood(Utility.RandomMinMax(2,4)) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override int GetIdleSound()
		{
			return 0x2D5;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override int Scales{ get{ return 10; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Black; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

		public ShadowWyrm( Serial serial ) : base( serial )
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
