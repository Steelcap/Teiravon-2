using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   [FlipableAttribute( 0x12A6, 0x12A6 )] 
   public class DoomTarot : Artifact
   { 
   
        public override int ArtifactRarity{ get{ return 5; } }
        
        [Constructable]
        public DoomTarot() : base( 0x12A6 )
        {
            Stackable = false;
            Weight = 10.0;
            Movable = false;
            Name = "Tarot Cards";
            ItemFlags.SetStealable(this,true);  
        }

	  			
        public DoomTarot( Serial serial ) : base( serial )
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
