using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Ghoul : BaseCreature
	{
		[Constructable]
		public Ghoul() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a ghoul";
			Body = 153;
			BaseSoundID = 0x482;
			Level = 8;

			SetStr( 76, 100 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits( 76, 120 );
			SetMana( 0 );

			SetDamage( 9, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 28;

			PackItem( Loot.RandomWeapon() );
			//PackGold( 25, 30 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }

		public Ghoul( Serial serial ) : base( serial )
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