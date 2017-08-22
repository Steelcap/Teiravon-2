using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{

[Flipable( 0x9A8, 0xE80 )]
	public class TaxBox : LockableContainer
	{
		public override int DefaultGumpID{ get{ return 0x4B; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 16, 51, 168, 73 ); }
		}

		[Constructable]
		public TaxBox() : base( 0x9A8 )
		{
			Weight = 3.0; // TODO: Real weight
			Name = "Tax Collection Box";
			Hue = 253;
			Locked = true;
			Movable = false;
			LockLevel = RequiredSkill = 500;
			DropItem( new TaxBook() );
		}

		public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
		{
//			Item[] m_book = this.FindItemsByType( typeof( TaxBook), true);
//			TaxBook txbook = (TaxBook) m_book[0];
			TaxBook txbook = (TaxBook)this.FindItemByType( typeof( TaxBook ), true ); 
 
			if ( txbook == null ) 
			{
				from.SendMessage("The Tax Book is missing so your payment will need to wait.");
				return false;
			}
				
			
			int taxents = txbook.Entries;
			
			if (dropped is Gold && taxents < 500)
			{
				string payer = from.Name;
				string payamt = String.Format("{0} gold",dropped.Amount);
				txbook.Pages[taxents].Lines = new string[]
				{
					payer,
					payamt,
				};
				taxents = taxents + 1;
				txbook.Entries = taxents;
				return base.TryDropItem( from, dropped, sendFullMessage );
			}
			else if (dropped is Gold && taxents > 499)
			{
				from.SendMessage("The box is too full to accept your payment now");
				return false;
			}
			else
			{
				from.SendMessage("Only Gold can be placed in the Tax Chest");
				return false;
			}
		}
		
		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			bool check = this.Locked;
			
			if ( item is Gold)
				return base.OnDragDropInto( from, item, p );
			else if ( from.AccessLevel < AccessLevel.GameMaster && check )
			{
				from.SendLocalizedMessage( 501747 ); // It appears to be locked.
				return false;
			}
	
			return base.OnDragDropInto( from, item, p );

		}


		public TaxBox( Serial serial ) : base( serial )
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

	public class TaxBook : RedBook
	{
		public int Entries;

		[Constructable]
		public TaxBook() : base( "Tax Payments", "Tax Collector", 500, true )
		{
			Hue = 253;
			Movable = false;
			Entries = 0;

		}

		public TaxBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}	
	
}
