using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x0E23, 0x0E23 )] 
   public class BloodyWater : Artifact
   { 
   
   public override int ArtifactRarity{ get{ return 5; } }

      [Constructable] 
      public BloodyWater() : base( 0x0E23 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Bloody Water";
		 ItemFlags.SetStealable(this,true);
      } 
	
	 
	  			
      public BloodyWater( Serial serial ) : base( serial ) 
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
