using System;

namespace Server.Items
{
	public abstract class BaseTrap : Item
	{

		// Trap System
		private int m_Trapper = 0;
		private double m_TrapSkill = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Trapper{ get { return m_Trapper; } set { m_Trapper = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public double TrapSkill{ get { return m_TrapSkill; } set { m_TrapSkill = value; } }


		public virtual bool PassivelyTriggered{ get{ return false; } }
		public virtual TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public virtual int PassiveTriggerRange{ get{ return -1; } }
		public virtual TimeSpan ResetDelay{ get{ return TimeSpan.Zero; } }

		private DateTime m_NextPassiveTrigger, m_NextActiveTrigger;

		public virtual void OnTrigger( Mobile from )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public virtual int GetEffectHue()
		{
			int hue = this.Hue & 0x3FFF;

			if ( hue < 2 )
				return 0;

			return hue - 1;
		}

		public bool CheckRange( Point3D loc, Point3D oldLoc, int range )
		{
			return CheckRange( loc, range ) && !CheckRange( oldLoc, range );
		}

		public bool CheckRange( Point3D loc, int range )
		{
			return ( (this.Z + 8) >= loc.Z && (loc.Z + 16) > this.Z )
				&& Utility.InRange( GetWorldLocation(), loc, range );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			base.OnMovement( m, oldLocation );

			if ( m.Location == oldLocation )
				return;

			if ( CheckRange( m.Location, oldLocation, 0 ) && DateTime.Now >= m_NextActiveTrigger )
			{
				m_NextActiveTrigger = m_NextPassiveTrigger = DateTime.Now + ResetDelay;

				if ( CheckTrap( m ) )
					OnTrigger( m );
			}
			else if ( PassivelyTriggered && CheckRange( m.Location, oldLocation, PassiveTriggerRange ) && DateTime.Now >= m_NextPassiveTrigger )
			{
				m_NextPassiveTrigger = DateTime.Now + PassiveTriggerDelay;

				if ( CheckTrap( m ) )
					OnTrigger( m );
			}
		}

		public bool CheckTrap( Mobile from )
		{
			if ( from.Serial == (Serial)m_Trapper )
			{
				from.SendMessage( "You carefully avoid your trap." );
				return false;
			}

			if ( from.Serial != (Serial)m_Trapper && from.CheckSkill( SkillName.RemoveTrap, 30, m_TrapSkill ) )
			{
				from.SendMessage( "You step on a hidden trap, but disarm it before it triggers!" );
				this.Delete();

				return false;
			}

			return true;
		}

		public BaseTrap( int itemID ) : base( itemID )
		{
			Movable = false;
			Visible = false;
		}

		public BaseTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( m_Trapper );
			writer.Write( m_TrapSkill );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			Trapper = reader.ReadInt();
			TrapSkill = reader.ReadDouble();
		}
	}
}