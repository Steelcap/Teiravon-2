using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x1E21, 0x1E21 )] 
   public class DoomBooks : Artifact
   {
   
    public override int ArtifactRarity{ get{ return 3; } } 

      [Constructable] 
      public DoomBooks() : base( 0x1E21 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "ruined books";
		 ItemFlags.SetStealable(this,true);
      } 
	  
			
      public DoomBooks( Serial serial ) : base( serial ) 
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
