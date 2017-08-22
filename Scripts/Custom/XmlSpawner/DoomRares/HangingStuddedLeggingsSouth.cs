using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x13D8, 0x13D8 )] 
   public class HangingStuddedLeggingsSouth : Artifact
   { 
   
        public override int ArtifactRarity{ get{ return 5; } }

      [Constructable] 
      public HangingStuddedLeggingsSouth() : base( 0x13D8 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "studded leggings";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public HangingStuddedLeggingsSouth( Serial serial ) : base( serial ) 
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
