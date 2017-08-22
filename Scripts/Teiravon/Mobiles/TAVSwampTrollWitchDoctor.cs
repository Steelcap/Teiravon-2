using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a swamp troll corpse" )]
	public class SwampTrollWitchDoctor : BaseCreature
	{
		[Constructable]
		public SwampTrollWitchDoctor() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "a swamp troll witch doctor";
			Body = 765;
			BaseSoundID = 461;
			Hue = 2547;
			Level = 14;

			SetStr( 150, 265 );
			SetDex( 66, 85 );
			SetInt( 166, 210 );

			SetHits( 300, 500 );
			SetMana( 400, 500 );

			SetDamage( 16, 24 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.EvalInt, 80.1, 100.0 );

			Fame = 5000;
			Karma = -4000;

			VirtualArmor = 50;

			PackItem( new BlackStaff() ); // TODO: Weapon??
			PackItem( new TrollLiver());
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Gems );
		}
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Hides{ get{ return 20; } }
		public override int Meat{ get{ return 10; } }

		public SwampTrollWitchDoctor( Serial serial ) : base( serial )
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
