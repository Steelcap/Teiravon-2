using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x13D9, 0x13D9 )] 
   public class HangingStuddedTunicSouth : Artifact
   { 
   
   public override int ArtifactRarity{ get{ return 7; } }

      [Constructable] 
      public HangingStuddedTunicSouth() : base( 0x13D9 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "studded tunic";
		 ItemFlags.SetStealable(this,true);
      } 
			
      public HangingStuddedTunicSouth( Serial serial ) : base( serial ) 
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
