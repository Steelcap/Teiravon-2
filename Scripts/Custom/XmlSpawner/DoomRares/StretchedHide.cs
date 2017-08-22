using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x106E, 0x106B )] 
   public class StretchedHide : Artifact
   { 
   
    public override int ArtifactRarity{ get{ return 2; } }

      [Constructable] 
      public StretchedHide() : base( 0x106B ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Stretched Hide";
		 ItemFlags.SetStealable(this,true);
      } 
				  
	  public StretchedHide( Serial serial ) : base( serial ) 
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
