using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a MagmaRager corpse" )]
	public class MagmaRager : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.Dismount : WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public MagmaRager() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Magma Rager";
			Body = 314;
            Hue = 2988;
			BaseSoundID = 357;
			Level = 13;

			SetStr( 251, 295 );
			SetDex( 101, 125 );
			SetInt( 66, 90 );

			SetHits( 761, 975 );

			SetDamage( 19, 27 );

			SetDamageType( ResistanceType.Physical, 30 );
            SetDamageType( ResistanceType.Fire, 70 );

			SetResistance( ResistanceType.Physical, 60, 80 );
			SetResistance( ResistanceType.Fire, 90, 100 );
			SetResistance( ResistanceType.Cold, 20, 40 );
			SetResistance( ResistanceType.Poison, 35, 45 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.MagicResist, 50.1, 75.0 );
			SetSkill( SkillName.Tactics, 75.1, 100.0 );
			SetSkill( SkillName.Wrestling, 70.1, 90.0 );

			Fame = 3500;
			Karma = -3500;
            AddItem(new MobRange(2));
			VirtualArmor = 54;
		}
        
        public override void OnThink()
        {
            double HpScale = (double)Hits / (double)HitsMax;
            SetDamage(AOS.Scale(19, (int)(300 - 200 * HpScale)), AOS.Scale(27, (int)(300 - 200 * HpScale)));
            SetDex(AOS.Scale(110, (int)(500 - 400 * HpScale)));
            base.OnThink();
        }
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

        public MagmaRager(Serial serial)
            : base(serial)
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