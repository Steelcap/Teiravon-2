using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x10DB, 0x10DB )] 
   public class Cocoon : Artifact
   { 
   
        public override int ArtifactRarity{ get{ return 7; } }

      [Constructable] 
      public Cocoon() : base( 0x10DB ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "a cocoon";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public Cocoon( Serial serial ) : base( serial ) 
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
