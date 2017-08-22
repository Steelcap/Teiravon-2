using System;
using Server.Mobiles;

namespace Server.Items
{

	public class AmphrosoposRing : BaseRing
	{
		String name;
		String title;
		String profile;
		int bmod;
		int bhue;
		
		
		//[CommandProperty( AccessLevel.GameMaster )]

//[Constructable]
//public AmphrosoposRing() : this( 0 )
//{
//}

		[Constructable]
		public AmphrosoposRing() : base( 0x108a )
		{
			Weight = 2.0;
			Name = "Runed Ring";
			LootType = LootType.Blessed;
		}

		public override bool CanEquip( Mobile m )
		{
			if ( !base.CanEquip( m ) )
				return false;

			return true;
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is TeiravonMobile ){
				TeiravonMobile m = (TeiravonMobile) parent;
				title = m.Title;
				m.Title = "the Human";
				name = m.Name;
				m.Name = "Amphiprosopos";
				bmod = m.OBody;
				m.OBody = 400;
				bhue = m.Hue;
				m.Hue = 1038;
				profile = m.Profile;
				m.Profile = "A rather tall man, quite pale and with a sickly appearance.  He has a well trimmed vandyke and his hair is usually kept in a ponytail.  He dresses himself in old robes and smells as if he had been kept in a library for ages.  He is old and is usually smiling kindly; quite a charming elderly man.";
				
			}
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent);
			
			if ( parent is TeiravonMobile )
			{
				
				TeiravonMobile m = (TeiravonMobile) parent;
				m.Title = title;
				m.Name = name;
				m.OBody = bmod;
				m.Hue = bhue;
				m.Profile = profile;
				
			}

		}

		public AmphrosoposRing( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (string)title );
			writer.Write( (string)name );
			writer.Write( (string)profile );
			writer.Write( (int)bmod);
			writer.Write( (int)bhue);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			title = reader.ReadString();
			name = reader.ReadString();
			profile = reader.ReadString();
			bmod = reader.ReadInt();
			bhue = reader.ReadInt();
		}
	}

}
