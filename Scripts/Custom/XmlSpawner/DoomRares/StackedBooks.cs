using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x1E25, 0x1E25 )] 
   public class StackedBooks : Artifact
   { 
   
        public override int ArtifactRarity{ get{ return 3; } }

      [Constructable] 
      public StackedBooks() : base( 0x1E25 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Books";
		 ItemFlags.SetStealable(this,true);
      } 
	
	  			
      public StackedBooks( Serial serial ) : base( serial ) 
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
