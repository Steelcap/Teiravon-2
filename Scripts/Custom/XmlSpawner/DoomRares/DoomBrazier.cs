using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0xE31, 0xE31 )] 
   public class DoomBrazier : Artifact
   { 
   
   public override int ArtifactRarity{ get{ return 2; } }

      [Constructable] 
      public DoomBrazier() : base( 0xE31 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "a brazier";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public DoomBrazier( Serial serial ) : base( serial ) 
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
