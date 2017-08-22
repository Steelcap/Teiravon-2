using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a mechanized messenger corpse" )]
	public class MechanizedMessenger : BaseCreature
	{
		[Constructable]
		public MechanizedMessenger() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a mechanized messenger";
			Body = 5;
			Hue = Utility.RandomList(2588, 2258, 2693);
			BaseSoundID = 0x2EE;

			SetStr( 31, 47 );
			SetDex( 36, 60 );
			SetInt( 8, 20 );

			SetHits( 20, 27 );
			SetMana( 0 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50);
			SetResistance( ResistanceType.Poison, 60, 80);
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 15.3, 30.0 );
			SetSkill( SkillName.Tactics, 18.1, 37.0 );
			SetSkill( SkillName.Wrestling, 20.1, 30.0 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 22;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 17.1;

            if (0.05 > Utility.RandomDouble())
                PackItem(new PowerCrystal());

            if (0.05 > Utility.RandomDouble())
                PackItem(new ClockworkAssembly());

            if (0.25 > Utility.RandomDouble())
                PackItem(new Gears());
		}


		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public MechanizedMessenger(Serial serial) : base(serial)
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