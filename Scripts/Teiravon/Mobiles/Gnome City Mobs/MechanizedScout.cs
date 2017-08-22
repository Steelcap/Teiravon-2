using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a mechanized scout" )]
	public class MechanizedScout : BaseCreature
	{
		[Constructable]
		public MechanizedScout() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a mechanized scout";
			Body = 63;
			Hue = Utility.RandomList(2588, 2258, 2693);
			BaseSoundID = 0x73;

			SetStr( 56, 80 );
			SetDex( 66, 85 );
			SetInt( 26, 50 );

			SetHits( 100, 150 );
			SetMana( 0 );

			SetDamage( 4, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 60, 70);
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 40, 55 );
			SetResistance( ResistanceType.Poison, 70, 80 );

			SetSkill( SkillName.MagicResist, 15.1, 30.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );
			SetSkill( SkillName.DetectHidden, 200.1, 250.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 41.1;

            

		if ( 0.05 > Utility.RandomDouble() )
			PackItem( new PowerCrystal() );
		
		if ( 0.05 > Utility.RandomDouble() )
			PackItem( new ClockworkAssembly() );
		
		if ( 0.25 > Utility.RandomDouble() )
			PackItem( new Gears() );
		}

			

		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }


        public MechanizedScout(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}