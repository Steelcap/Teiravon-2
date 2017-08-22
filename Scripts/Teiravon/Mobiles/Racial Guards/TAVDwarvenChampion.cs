using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{
	[CorpseName( "a dwarven corpse" )]
	public class DwarvenChampion : BaseCreature 
	{ 
		public override bool ClickTitle{ get{ return false; } }
		
		[Constructable] 
		public DwarvenChampion() : base( AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = NameList.RandomName( "dwarfmale" );
			Title = "the Champion";
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = Utility.RandomSkinHue(); 
			Body = 0x190; 
			Level = 10;

			new RidableLlama().Rider = this; 

			LongBeard facialhair = new LongBeard( Utility.RandomDyedHue() );
			facialhair.Movable = false;
			AddItem( facialhair );
			PlateChest chest = new PlateChest();
			chest.Hue = 0x8AF;
			chest.Movable = false;
			AddItem( chest );
			PlateArms arms = new PlateArms();
			arms.Hue = 0x8AF;
			arms.Movable = false;
			AddItem( arms );
			PlateGloves gloves = new PlateGloves();
			gloves.Hue = 0x8AF;
			gloves.Movable = false;
			AddItem( gloves );
			PlateGorget gorget = new PlateGorget();
			gorget.Hue = 0x8AF;
			gorget.Movable = false;
			AddItem( gorget );
			PlateLegs legs = new PlateLegs();
			legs.Hue = 0x8AF;
			legs.Movable = false;
			AddItem( legs );
			NorseHelm helm = new NorseHelm();
			helm.Hue = 0x8AF;
			helm.Movable = false;
			AddItem( helm );
			Cloak cloak = new Cloak();
			cloak.Hue = 0x8B0;
			cloak.Movable = false;
			AddItem( cloak );

			PackGold( 10, 17 );

			SetSkill( SkillName.MagicResist, 80.0, 99.5 );
			SetSkill( SkillName.Swords, 90.0, 102.5 );
			SetSkill( SkillName.Tactics, 85.0, 97.5 );
			SetSkill( SkillName.Wrestling, 65.0, 77.5 );
			
			SetStr( 456, 530 );
			SetDex( 191, 255 );
			SetInt( 141, 165 );
			SetHits( 470, 580 );
			SetDamage( 11, 17 );
			
			SetResistance( ResistanceType.Physical, 10, 25 );
			SetResistance( ResistanceType.Fire, 10, 25 );
			SetResistance( ResistanceType.Energy, 10, 25 );
			SetResistance( ResistanceType.Cold, 10, 25 );
			SetResistance( ResistanceType.Poison, 20, 30 );			
			
			Karma = 5000;
			Fame = 5000;
			
			switch ( Utility.Random( 5 ))
			{
				case 0: AddItem( new BattleAxe() ); break;
				case 1: AddItem( new LargeBattleAxe() ); break;
				case 2: AddItem( new TwoHandedAxe() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new DoubleAxe() ); break;
				
			}
		}
	
		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( mount is Mobile )
				((Mobile)mount).Delete();

			return base.OnBeforeDeath();
		} 

		public DwarvenChampion( Serial serial ) : base( serial ) 
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