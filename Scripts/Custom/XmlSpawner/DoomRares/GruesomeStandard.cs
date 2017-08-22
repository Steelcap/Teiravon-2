using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x428, 0x428 )] 
   public class GruesomeStandard : Artifact
   {
   
    public override int ArtifactRarity{ get{ return 5; } }

      [Constructable] 
      public GruesomeStandard() : base( 0x428 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Gruesome Standard";
		 ItemFlags.SetStealable(this,true);
      } 
			
	  
	  public GruesomeStandard( Serial serial ) : base( serial ) 
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
