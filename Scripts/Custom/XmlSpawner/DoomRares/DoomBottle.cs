using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x0E28, 0x0E28 )] 
   public class DoomBottle : Artifact
   { 
   
        public override int ArtifactRarity{ get{ return 1; } }

      [Constructable] 
      public DoomBottle() : base( 0x0E28 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Bottle";
		 ItemFlags.SetStealable(this,true);
      } 
	
	  			
      public DoomBottle( Serial serial ) : base( serial ) 
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
