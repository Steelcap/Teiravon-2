using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0xB24, 0xB24 )] 
   public class DoomLampPost : Artifact
   { 
   
        public override int ArtifactRarity{ get{ return 3; } } 

      [Constructable] 
      public DoomLampPost() : base( 0xB24 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Lamp Post";
		 ItemFlags.SetStealable(this,true);
      } 
	  
			
      public DoomLampPost( Serial serial ) : base( serial ) 
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
