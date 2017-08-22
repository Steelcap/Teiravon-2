using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x1E91, 0x1E88 )] 
   public class SkinnedGoat : Item
   { 
      [Constructable] 
      public SkinnedGoat() : base( 0x1E88 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "a skinned goat";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public SkinnedGoat( Serial serial ) : base( serial ) 
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
