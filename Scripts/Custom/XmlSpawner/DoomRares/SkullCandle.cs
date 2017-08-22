using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x1854, 0x1854 )] 
   public class SkullCandle : Item 
   { 
      [Constructable] 
      public SkullCandle() : base( 0x1854 ) 
      { 
         Stackable = false;
		 Weight = 10.0;
		 Movable = false;
         Name = "Skull with Candle"; 
         ItemFlags.SetStealable(this,true);
      } 
			

	  public SkullCandle( Serial serial ) : base( serial ) 
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
