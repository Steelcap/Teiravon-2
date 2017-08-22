using System;

namespace Server.Items
{
	public enum DockPoleTypes
	{
		VShort1,
		VShort2,
		Short1,
		Short2
	}

	public class DockPole : BaseWall
	{
		[Constructable]
		public DockPole( DockPoleTypes type ) : base( 0x01CB + (int)type )
		{
		}

		public DockPole( Serial serial ) : base( serial )
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