using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{
	[CorpseName( "a drow corpse" )]
	public class DrowFighter : BaseCreature 
	{ 
		public override bool ClickTitle{ get{ return false; } }
	
		[Constructable] 
		public DrowFighter() : base( AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = NameList.RandomName( "drowmale" );
			Title = "the Fighter";
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = 1109; 
			Body = 0x190; 
			Level = 7;
			NameHue = 2545;
			
			LongHair hair = new LongHair();
			hair.Movable = false;
			AddItem( hair );
			ChainChest chest = new ChainChest();
			chest.Hue = 0xA42;
			chest.Movable = false;
			AddItem( chest );
			RingmailArms arms = new RingmailArms();
			arms.Hue = 0xA42;
			arms.Movable = false;
			AddItem( arms );
			RingmailGloves gloves = new RingmailGloves();
			gloves.Hue = 0xA42;
			gloves.Movable = false;
			AddItem( gloves );
			ChainLegs legs = new ChainLegs();
			legs.Hue = 0xA42;
			legs.Movable = false;
			AddItem( legs );
			Cloak cloak = new Cloak();
			cloak.Hue = 0xA16;
			cloak.Movable = false;
			AddItem( cloak );
			MetalKiteShield shield = new MetalKiteShield();
			shield.Hue = 0xA42;
			shield.Movable = false;
			AddItem( shield );
			Boots boots = new Boots();
			boots.Hue = 0xA39;
			boots.Movable = false;
			AddItem( boots );

			PackGold( 6, 8 );

			SetSkill( SkillName.MagicResist, 50.0, 61.5 );
			SetSkill( SkillName.Swords, 70.0, 90.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 25.0, 47.5 );
			
			SetStr( 90, 110 );
			SetDex( 161, 125 );
			SetInt( 61, 85 );
			SetHits( 130, 160 );
			SetDamage( 10, 16 );
			
			Karma = -2000;
			Fame = 1000;
			
			switch ( Utility.Random( 4 ))
			{
				case 0:	Scimitar scim = new Scimitar();
					scim.Movable = false;
					AddItem( scim );
					break;
					
				case 1: Longsword lsword = new Longsword();
					lsword.Movable = false;
					AddItem( lsword );
					break;
					
				case 2: Broadsword bsword = new Broadsword();
					bsword.Movable = false;
					AddItem( bsword );
					break;
					
				case 3: Cutlass cl = new Cutlass();
					cl.Movable = false;
					AddItem( cl );
					break;
			}			
		} 

		public DrowFighter( Serial serial ) : base( serial ) 
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