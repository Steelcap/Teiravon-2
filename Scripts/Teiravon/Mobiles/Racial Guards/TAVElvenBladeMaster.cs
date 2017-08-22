using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	[CorpseName( "an elven corpse" )]
	public class ElvenBladeMaster : BaseCreature 
	{ 
		public override bool ClickTitle{ get{ return false; } }
		
		[Constructable] 
		public ElvenBladeMaster() : base( AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = NameList.RandomName( "elfmale" );
			Title = "the Blade Master";
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = 349; 
			Body = 0x190; 
			Level = 10;
			NameHue = 2454;

			new Horse().Rider = this; 

			LongHair hair = new LongHair( Utility.RandomNeutralHue() );
			hair.Movable = false;
			AddItem( hair );
			ChainChest chest = new ChainChest();
			chest.Hue = 0x9C4;
			chest.Movable = false;
			AddItem( chest );
			RingmailArms arms = new RingmailArms();
			arms.Hue = 0x9C4;
			arms.Movable = false;
			AddItem( arms );
			RingmailGloves gloves = new RingmailGloves();
			gloves.Hue = 0x9C4;
			gloves.Movable = false;
			AddItem( gloves );
			ChainLegs legs = new ChainLegs();
			legs.Hue = 0x9C4;
			legs.Movable = false;
			AddItem( legs );
			Cloak cloak = new Cloak();
			cloak.Hue = 0xA17;
			cloak.Movable = false;
			AddItem( cloak );
			OrderShield shield = new OrderShield();
			shield.Hue = 0x9C4;
			shield.Movable = false;
			AddItem( shield );
			Boots boots = new Boots();
			boots.Hue = 0xA17;
			boots.Movable = false;
			AddItem( boots );			

			PackGold( 10, 17 );

			SetSkill( SkillName.MagicResist, 80.0, 99.5 );
			SetSkill( SkillName.Swords, 90.0, 102.5 );
			SetSkill( SkillName.Tactics, 85.0, 97.5 );
			SetSkill( SkillName.Wrestling, 65.0, 77.5 );
			
			SetResistance( ResistanceType.Physical, 10, 25 );
			SetResistance( ResistanceType.Fire, 10, 25 );
			SetResistance( ResistanceType.Energy, 10, 25 );
			SetResistance( ResistanceType.Cold, 10, 25 );
			SetResistance( ResistanceType.Poison, 20, 30 );			
			
			SetStr( 316, 370 );
			SetDex( 351, 415 );
			SetInt( 141, 165 );
			SetHits( 420, 483 );
			SetDamage( 11, 17 );
			
			Karma = 5000;
			Fame = 5000;
			
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
	
		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( mount is Mobile )
				((Mobile)mount).Delete();

			return base.OnBeforeDeath();
		} 

		public ElvenBladeMaster( Serial serial ) : base( serial ) 
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