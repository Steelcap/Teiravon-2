using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x13CA, 0x13CA )] 
   public class HangingLeatherTunic : Artifact
   { 
   
   public override int ArtifactRarity{ get{ return 9; } } 

      [Constructable] 
      public HangingLeatherTunic() : base( 0x13CA ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Leather Tunic";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public HangingLeatherTunic( Serial serial ) : base( serial ) 
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
