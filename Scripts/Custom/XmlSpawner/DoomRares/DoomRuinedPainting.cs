using System;
using Server.Network;
using Server.SkillHandlers;

namespace Server.Items 
{ 

    [FlipableAttribute( 0xC2C, 0xC2C )]
    public class DoomRuinedPainting : Artifact
    {
   
        public override int ArtifactRarity{ get{ return 12; } }

        [Constructable]
        public DoomRuinedPainting() : base( 0xC2C )
        {
            Stackable = false;
            Weight = 10.0;
            Movable = false;
            Name = "a ruined painting";
            ItemFlags.SetStealable(this,true);
        }
			
        public DoomRuinedPainting( Serial serial ) : base( serial )
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
