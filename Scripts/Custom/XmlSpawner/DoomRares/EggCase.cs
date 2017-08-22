using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x10D9, 0x10D9 )] 
   public class EggCase : Artifact
   { 
   
        public override int ArtifactRarity{ get{ return 5; } }

      [Constructable] 
      public EggCase() : base( 0x10D9 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "eggcase";
         ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public EggCase( Serial serial ) : base( serial ) 
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
