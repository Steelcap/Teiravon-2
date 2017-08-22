using System;
using Server.Network; 

namespace Server.Items 
{ 

   [FlipableAttribute( 0x1363, 0x1363 )] 
   public class Rock1 : Item
   { 
	  
	  [Constructable]
		public Rock1() : base( 0x1363 )
		{
			Stackable = false;
			Weight = 10.0;
			Movable = false;
         	Name = "a rock";
         	ItemFlags.SetStealable(this,true);
		}

	  
	  public Rock1( Serial serial ) : base( serial ) 
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
