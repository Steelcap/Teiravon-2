using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class SerpentineDragon : BaseCreature
	{
		[Constructable]
		public SerpentineDragon() : base( AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 )
		{
			Name = "a serpentine dragon";
			Body = 103;
			BaseSoundID = 362;
			Level = 15;

//			SetStr( 151, 190 );
//			SetDex( 201, 220 );
			SetStr( 351, 500 );
			SetDex( 301, 350 );
			SetInt( 1001, 1040 );

//			SetHits( 480 );
			SetHits( 1200 );
			
			SetDamage( 7, 14 );

			RangeFight = 1;

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 45, 60 );
			SetResistance( ResistanceType.Cold, 45, 60 );
			SetResistance( ResistanceType.Poison, 45, 60 );
			SetResistance( ResistanceType.Energy, 45, 60 );
//			SetResistance( ResistanceType.Fire, 35, 45 );
//			SetResistance( ResistanceType.Cold, 35, 45 );
//			SetResistance( ResistanceType.Poison, 35, 45 );
//			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.EvalInt, 100.1, 110.0 );
			SetSkill( SkillName.Magery, 110.1, 120.0 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 50.1, 60.0 );
			SetSkill( SkillName.Wrestling, 30.1, 100.0 );
			
			SetSkill( SkillName.DetectHidden, 60.1, 105.0 );

			Fame = 15000;
			Karma = 15000;

			VirtualArmor = 36;
			
			if (0.10 > Utility.RandomDouble())
				PackItem( new WyrmHeart(1) );
			if (0.10 > Utility.RandomDouble())
				PackItem( new DragonBlood(Utility.RandomMinMax(1,3)) );
		}

		public override void GenerateLoot()
		{
			//AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}

		public override int GetIdleSound()
		{
			return 0x2C4;
		}

		public override int GetAttackSound()
		{
			return 0x2C0;
		}

		public override int GetDeathSound()
		{
			return 0x2C1;
		}

		public override int GetAngerSound()
		{
			return 0x2C4;
		}

		public override int GetHurtSound()
		{
			return 0x2C3;
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Hides{ get{ return 20; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 6; } }
		public override ScaleType ScaleType{ get{ return ( Utility.RandomBool() ? ScaleType.Black : ScaleType.White ); } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public SerpentineDragon( Serial serial ) : base( serial )
		{
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.2 > Utility.RandomDouble() && attacker is BaseCreature )
			{
				BaseCreature c = (BaseCreature)attacker;

				if ( c.Controled && c.ControlMaster != null )
				{
					c.ControlTarget = c.ControlMaster;
					c.ControlOrder = OrderType.Attack;
					c.Combatant = c.ControlMaster;
				}
			}
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
