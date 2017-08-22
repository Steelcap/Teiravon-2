using System;

namespace Server.Items
{

	public class NinjaMask : BaseHat
	{
		String name;
		String title;
		String profile;
		
		
		//[CommandProperty( AccessLevel.GameMaster )]

		[Constructable]
		public NinjaMask() : this( 2224 )
		{
		}

		[Constructable]
		public NinjaMask( int hue ) : base( 10126, hue )
		{
			Weight = 2.0;
			Name = "a mask";
			Hue = hue;
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

			if ( parent is Mobile ){
				Mobile m = (Mobile) parent;
				title = m.Title;
				m.Title = "a masked creature";
				name = m.Name;
				m.Name = "Unknown";
				profile = m.Profile;
				m.Profile = "You cannot say much about this creature.";
				
			}
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent);
			
			if ( parent is Mobile )
			{
				
				Mobile m = (Mobile) parent;
				m.Title = title;
				m.Name = name;
				m.Profile = profile;
				
			}

		}

		public NinjaMask( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (string)title );
			writer.Write( (string)name );
			writer.Write( (string)profile );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			title = reader.ReadString();
			name = reader.ReadString();
			profile = reader.ReadString();
		}
	}

}