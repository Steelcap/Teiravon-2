using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a mechanized chicken corpse" )]
	public class MechanizedChicken : BaseCreature
	{
		[Constructable]
		public MechanizedChicken() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a mechanized chicken";
			Body = 0xD0;
			BaseSoundID = 0x6E;
			Hue = Utility.RandomList(2588, 2258, 2693);

			SetStr( 5 );
			SetDex( 15 );
			SetInt( 5 );

			SetHits( 3 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 50);
			SetResistance( ResistanceType.Poison, 60, 80);

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 2;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = -0.9;


            if (0.05 > Utility.RandomDouble())
                PackItem(new PowerCrystal());

            if (0.05 > Utility.RandomDouble())
                PackItem(new ClockworkAssembly());

            if (0.25 > Utility.RandomDouble())
                PackItem(new Gears());
		}

			

		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public MechanizedChicken(Serial serial) : base(serial)
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