using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0xF38, 0xF38 )] 
   public class DoomSaddle : Artifact
   { 
   
   public override int ArtifactRarity{ get{ return 9; } }

      [Constructable] 
      public DoomSaddle() : base( 0xF38 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Saddle";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public DoomSaddle( Serial serial ) : base( serial ) 
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
