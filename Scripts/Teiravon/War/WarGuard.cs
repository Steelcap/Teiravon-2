using System;
using System.Collections;
using Server;
using Server.Items;
using Server.ContextMenus;
using Server.Scripts.Commands;
using Server.Mobiles;

namespace Server.Teiravon.War
{
	public class WarGuard : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		private WarStone m_WarStone = new WarStone();

		[CommandProperty( AccessLevel.GameMaster )]
		public WarStone WarStone { get { return m_WarStone; } set { m_WarStone = value; } }

		public void Init()
		{
			if ( m_WarStone == null )
				return;

			Longsword sword = new Longsword();

			//Backpack.DropItem( new PineShortbow() );
			//Backpack.DropItem( new Arrow( 1000 ) );
			AddItem( sword );

			if ( !Female )
			{
				PlateChest chest = new PlateChest();
				chest.Hue = m_WarStone.ColorTwo;
				chest.LootType = LootType.Blessed;

				AddItem( chest );
			}
			else
			{
				FemalePlateChest fchest = new FemalePlateChest();
				fchest.Hue = m_WarStone.ColorTwo;
				fchest.LootType = LootType.Blessed;

				AddItem( fchest );
			}

			PlateHelm helm = new PlateHelm();
			PlateGorget gorget = new PlateGorget();
			PlateArms arms = new PlateArms();
			PlateGloves gloves = new PlateGloves();
			PlateLegs legs = new PlateLegs();
			HeaterShield shield = new HeaterShield();

			helm.Hue = m_WarStone.ColorOne;
			helm.LootType = LootType.Blessed;

			gorget.Hue = m_WarStone.ColorOne;
			gorget.LootType = LootType.Blessed;

			arms.Hue = m_WarStone.ColorOne;
			arms.LootType = LootType.Blessed;

			gloves.Hue = m_WarStone.ColorOne;
			gloves.LootType = LootType.Blessed;

			legs.Hue = m_WarStone.ColorOne;
			legs.LootType = LootType.Blessed;

			shield.Hue = m_WarStone.ColorTwo;
			shield.LootType = LootType.Blessed;

			AddItem( helm );
			AddItem( gorget );
			AddItem( arms );
			AddItem( gloves );
			AddItem( legs );
			AddItem( shield );
		}

		[Constructable]
		public WarGuard() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Guard";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

			Level = 0;

			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			Fame = 1000;
			Karma = 1000;

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
		}

		public override void GenerateLoot()
		{
		}

		public WarGuard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( m_WarStone );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_WarStone = (WarStone)reader.ReadItem();
		}
	}
}