using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ice elemental corpse" )]
	public class IceElemental : BaseCreature
	{
		[Constructable]
		public IceElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a ice elemental";
			Body = 161;
			BaseSoundID = 268;
			Level = 11;
			Hue = 2916;

			SetStr( 156, 185 );
			SetDex( 96, 115 );
			SetInt( 171, 192 );

			SetHits( 194, 211 );

			SetDamage( 11, 22 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Cold, 75 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 10.5, 60.0 );
			SetSkill( SkillName.Magery, 10.5, 60.0 );
			SetSkill( SkillName.MagicResist, 30.1, 80.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 40;

			PackItem( new BlackPearl( 8 ) );
			PackReg( 8 );

			if ( Utility.Random( 4 ) == 0 )
				PackScroll( 1, 4 );

			//PackScroll( 4, 6 );

			//PackGold( 35, 40 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.Potions );
		}

		public IceElemental( Serial serial ) : base( serial )
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