using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 



   [FlipableAttribute( 2482, 2482 )] 
   public class ReversedBackPack : Artifact
   {

        public override int ArtifactRarity{ get{ return 5; } }
        
      [Constructable] 
      public ReversedBackPack() : base( 2482 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "a backpack";
		 ItemFlags.SetStealable(this,true);
      } 
	
	  			
      public ReversedBackPack( Serial serial ) : base( serial ) 
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
