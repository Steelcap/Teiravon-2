using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Server.Targeting;
using Server.Targets;

using Server.Accounting;
using Server.Network;

namespace Server.Targets
{

	public class SendTarget : Target
	{
		private Map m_SendMap;
		private Point3D m_SendPoint;
		private bool m_Jailed;
		private JailedMarker m_found;

		private Map m_SendBackMap;
		private Point3D m_SendBackLoc;
		private double m_SendBackTime;

		bool SendAll;
		bool SendInternal;

		public SendTarget( Point3D m_PointDest, Map m_MapDest, bool m_Jailing, double m_MaxJailTime, bool m_SendAll ) : base( -1, false, TargetFlags.None )
		{
			m_SendPoint = m_PointDest;
			m_SendMap = m_MapDest;
			m_Jailed = m_Jailing;

			m_SendBackTime = m_MaxJailTime;

			SendAll = m_SendAll;

		}

		protected override void OnTarget( Mobile from, object targ )
		{

			if ( !(targ is PlayerMobile) )
			{
				from.SendMessage( "You can only send players." );
				return;
			}
			else
			{
				PlayerMobile t = targ as PlayerMobile;

				if ( from.AccessLevel == AccessLevel.Player )
				{
					from.SendMessage( "You don't have sufficient accesslevel to send anyone!" );
					return;
				}
				else if (( from.AccessLevel <= t.AccessLevel ) && (from != t))
				{
					from.SendMessage( "You can't send targets of equal or greater Accesslevel." );
					return;
				}
				else
				{
					if (SendAll)
					{
						Account m_Account = (Account)t.Account;

						for ( int i = 0; i < 3; ++i )
						{
							TeiravonMobile m = (TeiravonMobile)m_Account[i];

							if ( m == null )
								continue;

							//from.SendMessage( "The character " + m.Name + " has been found on the account!");

							if (m == t)
								SendThem( from, m, m.Map, m.Location, false );
							else
								SendThem( from, m, m.LogoutMap, m.LogoutLocation, true );
						}
					}
					else
						SendThem( from, t, t.Map, t.Location, false );

				}
			}
		}


		private void SendThem( Mobile from, Mobile targ, Map m_Map, Point3D m_Loc, bool m_LoggedOut )
		{
			TeiravonMobile m_Target = targ as TeiravonMobile;

			m_SendBackMap = m_Map;
			m_SendBackLoc = m_Loc;
			SendInternal = m_LoggedOut;

			if ( m_Target.Backpack != null )
				m_found = (JailedMarker)m_Target.Backpack.FindItemByType( typeof( JailedMarker ), true );
			else
			{
				from.SendMessage( "That target has no backpack, and cannot be marked as jailed." );
				return;
			}


			if (m_Jailed)
			{
				if ( ( m_found == null ) && ( m_SendBackTime <= 0 ) )
				{
					from.SendMessage( m_Target.Name + " has been jailed for an undefined period of time!" );
					targ.SendMessage( "You have been jailed for an undefined period of time!" );
					m_Target.Backpack.AddItem( new JailedMarker( m_Target, m_SendPoint, m_SendMap, m_SendBackTime, m_SendBackMap, m_SendBackLoc ) );
				}
				else if ( ( m_found == null ) && (m_SendBackTime > 0) )
				{
					from.SendMessage( m_Target.Name + " has been jailed for " + m_SendBackTime + " hours." );
					m_Target.Backpack.AddItem( new JailedMarker( m_Target, m_SendPoint, m_SendMap, m_SendBackTime, m_SendBackMap, m_SendBackLoc ) );
					targ.SendMessage( "You have been jailed for " + m_SendBackTime + " hours." );
				}
				else
				{
					from.SendMessage( targ.Name + " is already tagged as jailed! Skipping." );
				}
			}
			else if (!m_Jailed)
			{
				if ( m_found != null )
				{
					Point3D m_OrigLoc = m_found.JailedFromLocation;
					Map m_OrigMap = m_found.JailedFromMap;
					m_Target.MoveToWorld( m_OrigLoc, m_OrigMap );

					from.SendMessage( m_Target.Name + " has been released from captivity and sent back to his jailing location!" );
					m_found.Delete();

					return;
				}
			}

			if (!SendInternal)
			{
				from.SendMessage( m_Target.Name + " has been sent to " + m_SendPoint );
				m_Target.MoveToWorld( m_SendPoint, m_SendMap );
			}
			else
			{
				from.SendMessage( m_Target.Name + " has been sent to " + m_SendPoint );
				m_Target.LogoutLocation = m_SendPoint;
				m_Target.LogoutMap = m_SendMap;
				m_Target.MoveToWorld( m_SendPoint, Map.Internal );
			}
		}
	}
}



namespace Server.Items
{
	public class StaffOfSending : BaseStaff
	{
		private Point3D m_PointDest;
		private Map m_MapDest;
		private string m_Location;
		private bool m_Jailing;
		private bool m_SendAll;

		private double m_MaxJailTime;

		[CommandProperty( AccessLevel.GameMaster )]
		public string SendLocation
		{
			get { return m_Location; }
			set { m_Location = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D SendTo
		{
			get { return m_PointDest; }
			set { m_PointDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map SendMap
		{
			get { return m_MapDest; }
			set { m_MapDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Jailing
		{
			get { return m_Jailing; }
			set { m_Jailing = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double MaxJailTimeHours
		{
			get { return m_MaxJailTime; }
			set { m_MaxJailTime = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SendAllChars
		{
			get { return m_SendAll; }
			set { m_SendAll = value; InvalidateProperties(); }
		}


		[Constructable]
		public StaffOfSending() : base( 0xE8A )
		{
			Name = "Staff Of Sending";
			LootType = LootType.Blessed;
			m_PointDest = new Point3D( 5900, 3915, 39 );
			m_MapDest = Map.Felucca;
			m_Location = "Jail";
			Hue = 0x58B;
			m_Jailing = true;
			m_MaxJailTime = 0.0;
			m_SendAll = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) || Parent == from )
			{
				from.Target = new SendTarget( m_PointDest, m_MapDest, m_Jailing, m_MaxJailTime, m_SendAll );
				from.SendMessage( "Whom do you wish to send to " + m_Location + "?" );
			}
			else
				from.SendLocalizedMessage( 1042001 );

		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name + " (" + m_Location + ")" );
			//list.Add( 500928, "StaffOfSendingProp1"); //Text in menu: This is a GM only tool.
		}


		public StaffOfSending( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_PointDest );

			writer.Write( m_MapDest );

			writer.Write( (string)m_Location );

			writer.Write( (bool)m_Jailing );

			writer.Write( (bool)m_SendAll );

			writer.Write( (double)m_MaxJailTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_PointDest = reader.ReadPoint3D();

			m_MapDest = reader.ReadMap();

			m_Location = reader.ReadString();

			m_Jailing = reader.ReadBool();

			m_SendAll = reader.ReadBool();

			m_MaxJailTime = reader.ReadDouble();
		}
	}
}