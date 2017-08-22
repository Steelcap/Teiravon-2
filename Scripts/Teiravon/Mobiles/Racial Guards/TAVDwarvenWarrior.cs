using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{
	[CorpseName( "a dwarven corpse" )]
	public class DwarvenWarrior : BaseCreature 
	{ 
		public override bool ClickTitle{ get{ return false; } }
	
		[Constructable] 
		public DwarvenWarrior() : base( AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = NameList.RandomName( "dwarfmale" );	
			Title = "the Warrior";
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = Utility.RandomSkinHue(); 
			Body = 0x190; 
			Level = 7;
			
			LongBeard facialhair = new LongBeard( Utility.RandomDyedHue() );
			facialhair.Movable = false;
			AddItem( facialhair );
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
			NorseHelm helm = new NorseHelm();
			helm.Hue = 0x966;
			helm.Movable = false;
			AddItem( helm );
			Cloak cloak = new Cloak();
			cloak.Hue = 0x966;
			cloak.Movable = false;
			AddItem( cloak );

			PackGold( 6, 8 );

			SetSkill( SkillName.MagicResist, 50.0, 61.5 );
			SetSkill( SkillName.Swords, 70.0, 90.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 25.0, 47.5 );
			
			SetStr( 106, 150 );
			SetDex( 91, 125 );
			SetInt( 41, 65 );
			SetHits( 140, 180 );
			SetDamage( 9, 16 );
			
			Karma = 1000;
			Fame = 1000;
			
			switch ( Utility.Random( 5 ))
			{
				case 0: AddItem( new BattleAxe() ); break;
				case 1: AddItem( new LargeBattleAxe() ); break;
				case 2: AddItem( new TwoHandedAxe() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new DoubleAxe() ); break;
				
			}			

		} 

		public DwarvenWarrior( Serial serial ) : base( serial ) 
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