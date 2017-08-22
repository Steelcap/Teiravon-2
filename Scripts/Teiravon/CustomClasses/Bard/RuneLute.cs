/*
 * Created by SharpDevelop.
 * User: gideon
 * Date: 2005/06/06
 * Time: 11:17 AM
 * 
 */

using System;
using Server.Gumps;

namespace Server.Items
{
	public class RuneLute : Item
	{
		[Constructable]
		public RuneLute() : base( 0xEB3 )
		{
			Name = "Rune Lute";
			Weight = 5.0;
		}

		public RuneLute( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				int[] Notes = new int[60];
			
				for (int i = 0; i < 60; ++i)
					Notes[i] = 0;
			
				from.CloseGump( typeof ( MusicGump ) );
				from.SendGump( new MusicGump(Notes, 0) );
			}
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

			if ( Weight == 3.0 )
				Weight = 5.0;
		}
	}
}
