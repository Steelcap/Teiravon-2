using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class HumanPaladin : BaseCreature 
	{ 
		public override bool ClickTitle{ get{ return false; } }
		
		[Constructable] 
		public HumanPaladin() : base( AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.2, 0.4 ) 
		{ 
			Title = "the Righteous";
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = Utility.RandomSkinHue(); 
			Body = 0x190; 
			Level = 10;

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				FemalePlateChest fchest = new FemalePlateChest();
				fchest.Hue = 2122;
				fchest.Movable = false;
				AddItem( fchest );
				Kilt kilt = new Kilt();
				kilt.Movable = false;
				kilt.Hue = 2971;
				AddItem( kilt );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				PlateChest chest = new PlateChest();
				chest.Hue = 2122;
				chest.Movable = false;		
				AddItem( chest );
				Surcoat coat = new Surcoat();
				coat.Hue = 2971;
				coat.Movable = false;
				AddItem( coat );	
				CloseHelm helm = new CloseHelm();
				helm.Hue = 2122;
				helm.Movable = false;
				AddItem( helm );			
				
			}

			Fame = 2000;
			Karma = 8000;

			new Nightmare().Rider = this; 
			
			PlateArms arms = new PlateArms();
			arms.Hue = 2122;
			arms.Movable = false;
			AddItem( arms );
			PlateLegs legs = new PlateLegs();
			legs.Hue = 2122;
			legs.Movable = false;
			AddItem( legs );
			PlateGorget gorget = new PlateGorget();
			gorget.Hue = 2122;
			gorget.Movable = false;
			AddItem( gorget );
			PlateGloves gloves = new PlateGloves();
			gloves.Hue = 2122;
			gloves.Movable = false;
			AddItem( gloves );
			
			Cloak cloak = new Cloak();
			cloak.Hue = 2971;
			cloak.Movable = false;
			AddItem( cloak );
			
			PackGold( 10, 17 );

			SetSkill( SkillName.MagicResist, 80.0, 90.5 );
			SetSkill( SkillName.Swords, 85.0, 101.5 );
			SetSkill( SkillName.Tactics, 85.0, 93.5 );
			SetSkill( SkillName.Wrestling, 65.0, 72.5 );
			SetSkill( SkillName.Fencing, 85.0, 100.5 );
			SetSkill( SkillName.Macing, 70.3, 95.5 );
			
			SetStr( 316, 440 );
			SetDex( 161, 223 );
			SetInt( 141, 165 );
			SetHits( 380, 433 );
			SetDamage( 12, 20 );			

			switch ( Utility.Random( 8 ))
			{
				case 0: WarMace wmace = new WarMace();
					wmace.Movable = false;
					wmace.Hue = 2122;
					AddItem( wmace );
					
					OrderShield ashield = new OrderShield();
					ashield.Movable = false;
					ashield.Hue = 2122;
					AddItem( ashield );
					break;
				
				case 1: Bardiche bd = new Bardiche();
					bd.Movable = false;
					bd.Hue = 2122;
					AddItem( bd );
					break;
				
				case 2:	Broadsword bsword = new Broadsword();
					bsword.Movable = false;
					bsword.Hue = 2122;
					AddItem( bsword );
					
					OrderShield bshield = new OrderShield();
					bshield.Movable = false;
					bshield.Hue = 2122;
					AddItem( bshield );
					break;
				
				case 3: Pike pike = new Pike();
					pike.Movable = false;
					pike.Hue = 2122;
					AddItem( pike );
					break;
				
				case 4: Halberd hbrd = new Halberd();
					hbrd.Movable = false;
					hbrd.Hue = 2122;
					AddItem( hbrd );
					break;
				
				case 5: Maul maul = new Maul();
					maul.Movable = false;
					maul.Hue = 2122;
					AddItem( maul );
					
					OrderShield cshield = new OrderShield();
					cshield.Movable = false;
					cshield.Hue = 2122;
					AddItem( cshield );
					break;
				
				case 6: VikingSword vsword = new VikingSword();
					vsword.Movable = false;
					vsword.Hue = 2122;
					AddItem( vsword );
					
					OrderShield dshield = new OrderShield();
					dshield.Movable = false;
					dshield.Hue = 2122;
					AddItem( dshield );
					break;
				
				case 7: Lance lance = new Lance();
					lance.Movable = false;
					lance.Hue = 2122;
					AddItem( lance );
					break;
			}

			AddItem( Server.Items.Hair.GetRandomHair( Female ) );
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

		public HumanPaladin( Serial serial ) : base( serial ) 
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