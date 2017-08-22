using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a doppleganger corpse" )]
	public class Doppleganger : BaseCreature
	{
		[Constructable]
		public Doppleganger() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a doppleganger";
			Body = 0x309;
			BaseSoundID = 0x451;
			Level = 12;

			SetStr( 111, 130 );
			SetDex( 56, 75 );
			SetInt( 81, 105 );

			SetHits( 101, 120 );

			SetDamage( 11, 16 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 75.1, 85.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 55;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public Doppleganger( Serial serial ) : base( serial )
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