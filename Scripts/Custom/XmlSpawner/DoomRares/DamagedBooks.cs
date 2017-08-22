using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0xC16, 0xC16 )] 
   public class DamagedBooks : Artifact
   { 

        public override int ArtifactRarity{ get{ return 1; } }

      [Constructable] 
      public DamagedBooks() : base( 0xC16 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Damaged Books";
		 ItemFlags.SetStealable(this,true);
      } 
	
	  			
      public DamagedBooks( Serial serial ) : base( serial ) 
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
