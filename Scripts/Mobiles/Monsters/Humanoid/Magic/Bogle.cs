using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Bogle : BaseCreature
	{
		[Constructable]
		public Bogle() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bogle";
			Body = 153;
			BaseSoundID = 0x482;
			Level = 8;

			SetStr( 76, 100 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits( 56, 80 );

			SetDamage( 7, 12 );

			SetSkill( SkillName.EvalInt, 55.1, 70.0 );
			SetSkill( SkillName.Magery, 65.1, 75.0 );
			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 28;

			//PackScroll( 1, 6 );
			//PackScroll( 4, 6 );

			if ( Utility.Random( 10 ) == 0 )
				PackGold( 1, 12 );

			//PackItem( Loot.RandomWeapon() );
			PackItem( new Bone( 7 ) );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public Bogle( Serial serial ) : base( serial )
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