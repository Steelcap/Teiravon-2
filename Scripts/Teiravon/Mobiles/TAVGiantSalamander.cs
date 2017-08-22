using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Giant Salamander corpse" )]
	public class GiantSalamander : BaseCreature
	{
		[Constructable]
		public GiantSalamander() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Giant Salamander";
			Body = 0xCA;
			BaseSoundID = 660;
			Level = 5;

			SetStr( 76, 100 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 46, 60 );
			SetStam( 46, 65 );
			SetMana( 0 );

			SetDamage( 5, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Poison, 45, 50 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 600;
			Karma = -600;
			
			switch(Utility.RandomMinMax(1,3))
	       {
				case 1:
			       	{Hue = 2250; break;}
			    case 2:
			       	{Hue = 2299; break;}
			    case 3:
			       	{Hue = 281; break;}
	       }

			VirtualArmor = 30;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 47.1;
			PackItem( new EyeOfNewt(Utility.RandomMinMax(1,2)) );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public GiantSalamander(Serial serial) : base(serial)
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

			if ( BaseSoundID == 0x5A )
				BaseSoundID = 660;
		}
	}
}
