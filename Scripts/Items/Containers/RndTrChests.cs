using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using System;

namespace Server.Items
{
	[FlipableAttribute( 0xe43, 0xe42 )] 
	public class TrChest1 : BaseTreasureChest 
	{ 
		public override int DefaultGumpID{ get{ return 0x49; } }
		public override int DefaultDropSound{ get{ return 0x42; } }
		public override bool Decays{ get{ return true;}}
 		private TreasureLevel TrLvl;
		
		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 20, 105, 150, 180 ); }
		}

		[Constructable] 
		public TrChest1() : base( 0xE43 ) 
		{ 
			int rndm = Utility.Random(6);
			if (rndm < 3)
				TrLvl = TreasureLevel.Level1;
			else if (rndm < 5)
				TrLvl = TreasureLevel.Level2;
			else 
				TrLvl = TreasureLevel.Level3;
			
			this.Level = TrLvl;
			SetLockLevel();
			GenerateTreasure();
		}

		public TrChest1( Serial serial ) : base( serial ) 
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


	[FlipableAttribute( 0x9ab, 0xe7c )] 
	public class TrChest2 : BaseTreasureChest 
	{ 
		public override int DefaultGumpID{ get{ return 0x4A; } }
		public override int DefaultDropSound{ get{ return 0x42; } }
		public override bool Decays{ get{ return true;}}
 		private TreasureLevel TrLvl;

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 20, 105, 150, 180 ); }
		}

		[Constructable] 
		public TrChest2() : base( 0x9AB ) 
		{ 
			int rndm = Utility.Random(6);
			if (rndm < 3)
				TrLvl = TreasureLevel.Level3;
			else if (rndm < 5)
				TrLvl = TreasureLevel.Level4;
			else 
				TrLvl = TreasureLevel.Level5;
			
			this.Level = TrLvl;
			SetLockLevel();
			GenerateTreasure();
		} 

		public TrChest2( Serial serial ) : base( serial ) 
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
	
	
	[FlipableAttribute( 0xe41, 0xe40 )] 
	public class TrChest3 : BaseTreasureChest 
	{ 
		public override int DefaultGumpID{ get{ return 0x42; } }
		public override int DefaultDropSound{ get{ return 0x42; } }
		public override bool Decays{ get{ return true;}}
 		private TreasureLevel TrLvl;

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 20, 105, 150, 180 ); }
		}

		[Constructable] 
		public TrChest3() : base( 0xE41 ) 
		{ 
			int rndm = Utility.Random(3);
			if (rndm < 2)
				TrLvl = TreasureLevel.Level5;
			else 
				TrLvl = TreasureLevel.Level6;
			
			this.Level = TrLvl;
			SetLockLevel();
			GenerateTreasure();
		} 

		public TrChest3( Serial serial ) : base( serial ) 
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
	
		[FlipableAttribute( 0xe43, 0xe42 )] 
	public class TrChest4 : BaseTreasureChest 
	{ 
		public override int DefaultGumpID{ get{ return 0x49; } }
		public override int DefaultDropSound{ get{ return 0x42; } }
		public override bool Decays{ get{ return true;}}
 		private TreasureLevel TrLvl;
		
		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 20, 105, 150, 180 ); }
		}

		[Constructable] 
		public TrChest4() : base( 0xE43 ) 
		{ 
			int rndm = Utility.Random(21);
			if (rndm < 6)
				TrLvl = TreasureLevel.Level1;
			else if (rndm < 11)
				TrLvl = TreasureLevel.Level2;
			else if (rndm < 15)
				TrLvl = TreasureLevel.Level3;
			else if (rndm < 18)
				TrLvl = TreasureLevel.Level4;
			else if (rndm < 20)
				TrLvl = TreasureLevel.Level5;
			else 
				TrLvl = TreasureLevel.Level6;
			
			this.Level = TrLvl;
			SetLockLevel();
			GenerateTreasure();
		}

		public TrChest4( Serial serial ) : base( serial ) 
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
