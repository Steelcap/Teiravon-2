using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a deep sea serpents corpse" )]
	public class DeepSeaSerpent : BaseCreature
	{
		[Constructable]
		public DeepSeaSerpent() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a deep sea serpent";
			Body = 150;
			BaseSoundID = 447;
			Level = 12;

			SetStr( 251, 425 );
			SetDex( 87, 135 );
			SetInt( 87, 155 );

			SetHits( 151, 255 );

			SetDamage( 6, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 60;
			CanSwim = true;
			CantWalk = true;

			PackItem( new SulfurousAsh( 4 ) );
			PackItem( new BlackPearl( 4 ) );

			if ( Utility.Random( 3 ) == 0 )
				PackItem( new SpecialFishingNet() );

			if ( Utility.Random( 3 ) == 0 )
				PackItem( new MessageInABottle( this.Map ) );


			if ( Utility.Random( 3 ) == 0 )
				PackScroll( 1, 6 );

			//PackScroll( 4, 6 );

			if ( Utility.Random( 15 ) == 0 )
				PackScroll( 4, 8 );

			//PackGold( 250, 300 );
			}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool HasBreath{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override int Scales{ get{ return 8; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Blue; } }

		public DeepSeaSerpent( Serial serial ) : base( serial )
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