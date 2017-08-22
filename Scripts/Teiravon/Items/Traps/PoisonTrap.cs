using System;

namespace Server.Items
{
	public class PoisonTrap : BaseTrap
	{
		private Poison m_Poison;

		public override void OnTrigger( Mobile from )
		{
			from.SendMessage( "You've been struck with a poisonous dart!" );
			from.ApplyPoison( from, m_Poison );

			Delete();
		}

		public PoisonTrap( Poison p ) : base( 1173 )
		{
			m_Poison = p;
		}

		public PoisonTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			Poison.Serialize(	 m_Poison, writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			m_Poison = Poison.Deserialize( reader );
		}
	}
}