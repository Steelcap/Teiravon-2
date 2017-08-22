using System;
using Server;
using Server.Items;
using Server.Teiravon;

namespace Server.Mobiles
{
	[CorpseName( "a corpse of an lost soul" )]
	public class Lostsoul1 : BaseCreature
	{
		[Constructable]
		public Lostsoul1() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "an Lost Soul";
			Body = 26; 
			Hue = 0x4001;
			BaseSoundID = 0x482; 
			Level = 5;

		    	SetStr( 100 );
			SetDex( 100 );
			SetInt( 150 );

			SetHits( 100 );

			SetDamage( 8, 15 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 75 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Energy, 25 );
			SetDamageType( ResistanceType.Poison, 0 );

			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 0 );

			SetSkill( SkillName.MagicResist, 50.0, 50.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0, 100.0 );

			

			Fame = 1000;
			Karma = -5000;

			VirtualArmor = 15;

			
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Utility.RandomMinMax( 1, 10 ) == 10 )
			{
				 this.Say( "You shall not escape me!" );
			}
		}

	
		public override void OnDeath( Container c )
		{
			Lostsoul2 newlostsoul = new Lostsoul2();
			newlostsoul.MoveToWorld( this.Location, this.Map );
			
			base.OnDeath( c );
		}
		

		public Lostsoul1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}



	[CorpseName( "a corpse of an lost soul" )]
	public class Lostsoul2 : BaseCreature

	{
		[Constructable]
		public Lostsoul2() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an Lost Soul";
			Body = 67; 
			Hue = 0x4001;
			BaseSoundID = 0x174;
			Level = 7;

		    	SetStr( 250 );
			SetDex( 100 );
			SetInt( 150 );

			SetHits( 250 );

			SetDamage( 10, 20 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Energy, 0 );
			SetDamageType( ResistanceType.Poison, 0 );

			SetResistance( ResistanceType.Physical, 75 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 25 );
			SetResistance( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Energy, 0 );

			SetSkill( SkillName.MagicResist, 50.0, 50.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0, 100.0 );
			

			Fame = 1000;
			Karma = -5000;

			VirtualArmor = 25;
			
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Utility.RandomMinMax( 1, 10 ) == 10 )
			{
			 	this.Say( "You shall not escape me!" );
			}
		}

		public override void OnDeath( Container c )
		{
			Lostsoul3 newlostsoul = new Lostsoul3();
			newlostsoul.MoveToWorld( this.Location, this.Map );
			
			base.OnDeath( c );
		}


		public Lostsoul2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}


[CorpseName( "a corpse of an lost soul" )]
	public class Lostsoul3 : BaseCreature

	{
		[Constructable]
		public Lostsoul3() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an Lost Soul";
			Body = 318; 
			Hue = 0;
			BaseSoundID = 0x165;
			Level = 10;

		    	SetStr( 350 );
			SetDex( 100 );
			SetInt( 150 );

			SetHits( 350 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Energy, 0 );
			SetDamageType( ResistanceType.Poison, 0 );

			SetResistance( ResistanceType.Physical, 75 );
			SetResistance( ResistanceType.Fire, 25 );
			SetResistance( ResistanceType.Cold, 25 );
			SetResistance( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Energy, 0 );

			SetSkill( SkillName.MagicResist, 50.0, 50.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0, 100.0 );
			

			Fame = 1000;
			Karma = -5000;

			VirtualArmor = 30;

		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Utility.RandomMinMax( 1, 10 ) == 10 )
			{
				this.Say( "You shall not escape me!" );
			}
		}

		public override void OnDeath( Container c )
		{
			Lostsoul4 newlostsoul = new Lostsoul4();
			newlostsoul.MoveToWorld( this.Location, this.Map );
			
			base.OnDeath( c );
		}
	
		public Lostsoul3( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	[CorpseName( "a corpse of an lost soul" )]
	public class Lostsoul4 : BaseCreature
	{
		[Constructable]
		public Lostsoul4() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "an Lost Soul";
			Body = 308; 
			Hue = 0;
			BaseSoundID = 0x48d;
			Level = 12;

		    	SetStr( 350 );
			SetDex( 100 );
			SetInt( 150 );

			SetHits( 350 );

			SetDamage( 22, 25 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Energy, 0 );
			SetDamageType( ResistanceType.Poison, 0 );

			SetResistance( ResistanceType.Physical, 75 );
			SetResistance( ResistanceType.Fire, 25 );
			SetResistance( ResistanceType.Cold, 25 );
			SetResistance( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Energy, 25 );

			SetSkill( SkillName.MagicResist, 50.0, 70.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0, 100.0 );
		
			

			Fame = 1000;
			Karma = -5000;

			VirtualArmor = 30;
			
		}
		
		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Utility.RandomMinMax( 1, 10 ) == 10 )
			{
				this.Say( "You shall not escape me!" );
			}
		}

		public Lostsoul4( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	

	[CorpseName( "a corpse of an Demon Rager" )]
	public class Demonrager : BaseCreature
	{
		[Constructable]
		public Demonrager() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "an Demon Rager";
			Body = 9; 
			Hue = 0;
			BaseSoundID = 357;
			Level = 12;

		    	SetStr( 300 );
			SetDex( 200 );
			SetInt( 200 );

			SetHits( 300 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Energy, 0 );
			SetDamageType( ResistanceType.Poison, 0 );

			SetResistance( ResistanceType.Physical, 75 );
			SetResistance( ResistanceType.Fire, 25 );
			SetResistance( ResistanceType.Cold, 25 );
			SetResistance( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Energy, 0 );

	
			SetSkill( SkillName.MagicResist, 50.0, 50.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0, 100.0 );


			Fame = 1000;
			Karma = -5000;

			VirtualArmor = 30;		
		}
		
		public override void OnDeath( Container c )
		{
			Grimlin newgrimlin1 = new Grimlin();
			newgrimlin1.MoveToWorld( this.Location, this.Map );

			Grimlin newgrimlin2 = new Grimlin();
			newgrimlin2.MoveToWorld( this.Location, this.Map );

			Grimlin newgrimlin3 = new Grimlin();
			newgrimlin3.MoveToWorld( this.Location, this.Map );

			Grimlin newgrimlin4 = new Grimlin();
			newgrimlin4.MoveToWorld( this.Location, this.Map );

			Grimlin newgrimlin5 = new Grimlin();
			newgrimlin5.MoveToWorld( this.Location, this.Map );
			
			base.OnDeath( c );
		}

		public Demonrager( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a corpse of an Grimlin" )]
	public class Grimlin : BaseCreature

	{
		[Constructable]
		public Grimlin() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "an Grimlin";
			Body = 74; 
			Hue = 0;
			BaseSoundID = 357;
			Level = 5;

		    	SetStr( 40 );
			SetDex( 200 );
			SetInt( 200 );

			SetHits( 40 );

			SetDamage( 5,10 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Energy, 0 );
			SetDamageType( ResistanceType.Poison, 0 );

			SetResistance( ResistanceType.Physical, 75 );
			SetResistance( ResistanceType.Fire, 25 );
			SetResistance( ResistanceType.Cold, 25 );
			SetResistance( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Energy, 0 );

			SetSkill( SkillName.MagicResist, 50.0, 50.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 90.0, 100.0 );


			Fame = 1000;
			Karma = -5000;

			VirtualArmor = 30;

			
		}
		
		public Grimlin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}