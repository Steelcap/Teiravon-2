using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class Guardian : BaseCreature
	{
		[Constructable]
		public Guardian() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			InitStats( 100, 125, 25 );
			Title = "the guardian";
			Level = 10;

			SpeechHue = Utility.RandomDyedHue();

			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

			new ForestOstard().Rider = this;

			PlateChest chest = new PlateChest();
			chest.Hue = 0x966;
			chest.Movable = false;
			AddItem( chest );
			PlateArms arms = new PlateArms();
			arms.Hue = 0x966;
			arms.Movable = false;
			AddItem( arms );
			PlateGloves gloves = new PlateGloves();
			gloves.Hue = 0x966;
			gloves.Movable = false;
			AddItem( gloves );
			PlateGorget gorget = new PlateGorget();
			gorget.Hue = 0x966;
			gorget.Movable = false;
			AddItem( gorget );
			PlateLegs legs = new PlateLegs();
			legs.Hue = 0x966;
			legs.Movable = false;
			AddItem( legs );
			PlateHelm helm = new PlateHelm();
			helm.Hue = 0x966;
			helm.Movable = false;
			AddItem( helm );


			VikingSword sword = new VikingSword();

			sword.Movable = false;
			sword.Crafter = this;
			sword.Quality = WeaponQuality.Exceptional;

			AddItem( sword );

			PackGold( 2, 8 );

			SetSkill( SkillName.Swords, 80.0, 90.0 );
			Skills[SkillName.Anatomy].Base = 120.0;
			Skills[SkillName.Tactics].Base = 120.0;
			Skills[SkillName.MagicResist].Base = 120.0;
			Skills[SkillName.DetectHidden].Base = 100.0;

		}

		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( mount is Mobile )
				((Mobile)mount).Kill();

			return base.OnBeforeDeath();
		}

		public Guardian( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}