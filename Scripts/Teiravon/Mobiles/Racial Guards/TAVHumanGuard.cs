using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class HumanGuard : BaseCreature 
	{ 
		public override bool ClickTitle{ get{ return false; } }
		
		[Constructable] 
		public HumanGuard() : base( AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.2, 0.4 ) 
		{ 
			Title = "the Guard";
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = Utility.RandomSkinHue(); 
			Body = 0x190; 
			Level = 8;

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

			Fame = 1000;
			Karma = 5000;

			RingmailArms arms = new RingmailArms();
			arms.Hue = 0xA13;
			arms.Movable = false;
			AddItem( arms );
			ChainChest chest = new ChainChest();
			chest.Hue = 0xA0B;
			chest.Movable = false;
			AddItem( chest );
			ChainLegs legs = new ChainLegs();
			legs.Hue = 0xA13;
			legs.Movable = false;
			AddItem( legs );
			RingmailGloves gloves = new RingmailGloves();
			gloves.Hue = 0xA13;
			gloves.Movable = false;
			AddItem( gloves );
			MetalShield shield = new MetalShield();
			shield.Hue = 0xA0B;
			shield.Movable = false;
			AddItem( shield );
			
			Cloak cloak = new Cloak();
			cloak.Hue = 0x95D;
			cloak.Movable = false;
			AddItem( cloak );
			BodySash sash = new BodySash();
			sash.Hue = 0x95D;
			sash.Movable = false;
			AddItem( sash );
			Boots boots = new Boots();
			boots.Hue = 0x95D;
			boots.Movable = false;
			AddItem( boots );
			
			PackGold( 6, 8 );

			SetSkill( SkillName.MagicResist, 50.0, 61.5 );
			SetSkill( SkillName.Swords, 70.0, 90.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 25.0, 47.5 );
			SetSkill( SkillName.Macing, 70.0, 90.7 );
			
			SetStr( 130, 150 );
			SetDex( 98, 115 );
			SetInt( 61, 85 );
			SetHits( 145, 168 );
			SetDamage( 9, 16 );		

			switch ( Utility.Random( 7 ))
			{
				case 0: WarMace wmace = new WarMace();
					wmace.Movable = false;
					AddItem( wmace );
					break;
					
				case 1: Bardiche bd = new Bardiche();
					bd.Movable = false;
					AddItem( bd );
					break;
				
				case 2:	Broadsword bsword = new Broadsword();
					bsword.Movable = false;
					AddItem( bsword );
					break;
				
				case 3: Pike pike = new Pike();
					pike.Movable = false;
					AddItem( pike );
					break;
				
				case 4: Halberd hbrd = new Halberd();
					hbrd.Movable = false;
					AddItem( hbrd );
					break;
				
				case 5: Maul maul = new Maul();
					maul.Movable = false;
					AddItem( maul );
					break;
				
				case 6: VikingSword vsword = new VikingSword();
					vsword.Movable = false;
					AddItem( vsword );
					break;
			}

			AddItem( Server.Items.Hair.GetRandomHair( Female ) );
		}

		public HumanGuard( Serial serial ) : base( serial ) 
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