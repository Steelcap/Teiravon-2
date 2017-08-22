using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x1E91, 0x1E91 )] 
   public class SkinnedDeer : Item
   { 
      [Constructable] 
      public SkinnedDeer() : base( 0x1E91 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "a skinned deer";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public SkinnedDeer( Serial serial ) : base( serial ) 
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
