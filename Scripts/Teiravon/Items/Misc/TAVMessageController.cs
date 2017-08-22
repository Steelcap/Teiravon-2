using System;
using Server;
using Server.Network;

namespace Server.Items
{

	public class MessageController : Item
	{

		private int m_TriggerRange;
		private string m_Message;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int TriggerRange{ get{ return m_TriggerRange; } set{ m_TriggerRange = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public string Message{ get{ return m_Message; } set{ m_Message = value; } }

		[Constructable]
		public MessageController():base(0x1B72)
		{
			Movable = false;
			Visible = false;
		}

		private DateTime m_NextMessage;
		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			base.OnMovement( m, oldLocation );
			
			if ( m.Player && Utility.InRange( Location, m.Location, m_TriggerRange ) && !Utility.InRange( Location, oldLocation, m_TriggerRange ) )
			{
				if ( DateTime.Now >= m_NextMessage && m_Message != null )
				{
					if (Math.Abs(m.Z - this.Z) < 6)			
					{
						m.SendMessage(m_Message);
						m_NextMessage = DateTime.Now + TimeSpan.FromSeconds( 30.0 );
					}
				}
			}
		}
		
		public MessageController( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.WriteEncodedInt( m_TriggerRange );
			writer.Write( m_Message);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_TriggerRange = reader.ReadEncodedInt();
			m_Message = reader.ReadString();
		}
	}
	
	
	
	
	
	
	
}
