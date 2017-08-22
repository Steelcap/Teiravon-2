using System;
using System.Collections;
using Server;
using Server.Items;
using Server.ContextMenus;
using Server.Scripts.Commands;
using Server.Teiravon;

namespace Server.Mobiles
{
	public class Assassin : BaseCreature
	{
		public override bool CanTeach{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 3 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );

//			if ( e.Speech.ToLower() == "buy" )
//			{
				// Send gump
//			}
		}

		[Constructable]
		public Assassin() : base( AIType.AI_Melee, FightMode.None, 22, 1, 0.2, 1.0 )
		{
			Body = 0x190;
			Title = "the Shadowy Figure";
			Blessed = true;
		}

		public Assassin( Serial serial ) : base( serial )
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