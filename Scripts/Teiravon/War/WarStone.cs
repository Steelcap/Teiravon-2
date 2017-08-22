using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Spells;
using Server.Scripts.Commands;
using Server.Mobiles;
using Server.Items;

namespace Server.Teiravon.War
{
	public class WarStone : Item
	{
		private Mobile m_Leader = new Mobile();
		private Mobile m_Commander = new Mobile();
		private ArrayList m_Members = new ArrayList();
		private ArrayList m_SiegeWeapons = new ArrayList();
		private ArrayList m_Guards = new ArrayList();
		private ArrayList m_GuardPoints = new ArrayList();
		private ulong m_Gold = 0;
		private ulong m_Wood = 0;
		private int m_ColorOne = 0;
		private int m_ColorTwo = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Leader { get { return m_Leader; } set { m_Leader = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Commander { get { return m_Commander; } set { m_Commander = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public ArrayList Members { get { return m_Members; } set { m_Members = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public ArrayList SiegeWeapons { get { return m_SiegeWeapons; } set { m_SiegeWeapons = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public ArrayList Guards { get { return m_Guards; } set { m_Guards = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public ArrayList GuardPoints { get { return m_GuardPoints; } set { m_GuardPoints = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public ulong Gold { get { return m_Gold; } set { m_Gold = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public ulong Wood { get { return m_Wood; } set { m_Wood = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ColorOne { get { return m_ColorOne; } set { m_ColorOne = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ColorTwo { get { return m_ColorTwo; } set { m_ColorTwo = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxGuards { get { return m_Members.Count / 2 > 4 ? m_Members.Count / 2 : 4; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxSiegeWeapons { get { return m_Members.Count / 4 > 2 ? m_Members.Count / 4 : 2; } }

		public static void Defrag( WarStone theStone )
		{
			ArrayList checkMembers = new ArrayList();
			ArrayList checkGuards = new ArrayList();
			ArrayList checkGuardPoints = new ArrayList();
			ArrayList checkSiegeWeapons = new ArrayList();

			foreach ( Mobile m in theStone.Members )
			{
				TeiravonMobile m_Player = (TeiravonMobile)m;

				if ( m.Deleted || m == null || !m.Player || m_Player.Town != theStone )
					checkMembers.Add( m );
			}

			foreach ( Mobile m in theStone.Guards )
			{
				if ( m == null || m.Player || !m.Alive || m.Deleted )
					checkGuards.Add( m );
			}

			foreach ( Item m in theStone.GuardPoints )
			{
				if ( m.Deleted || m == null )
					checkGuardPoints.Add( m );
			}

			foreach ( Item m in theStone.SiegeWeapons )
			{
				if ( m.Deleted || m == null )
					checkSiegeWeapons.Add( m );
			}

			for ( int i = 0; i < checkMembers.Count; ++i )
				theStone.Members.Remove( ((Mobile)checkMembers[i]) );

			for ( int i = 0; i < checkGuards.Count; ++i )
				theStone.Guards.Remove( ((Mobile)checkGuards[i]) );

			for ( int i = 0; i < checkGuardPoints.Count; ++i )
				theStone.GuardPoints.Remove( ((Item)checkGuardPoints[i]) );

			for ( int i = 0; i < checkSiegeWeapons.Count; ++i )
				theStone.SiegeWeapons.Remove( ((Item)checkSiegeWeapons[i]) );

			return;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Defrag( this );

			if ( m_Leader == null )
				m_Leader = from;
			
			if ( m_Commander == null )
				m_Commander = from;

			if ( m_Leader == from || m_Commander == from || from.AccessLevel >= AccessLevel.Player )
				from.SendGump( new WarStoneGump( from, this ) );
			else
				from.SendMessage( "You may not command this stone." );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( Members.IndexOf( e.Mobile ) > -1 && e.Speech.ToLower().IndexOf( "i wish to leave my town" ) > -1 )
			{
				Members.Remove( e.Mobile );
				e.Mobile.SendMessage( "You are no longer a citizen of {0}.", Name );
			}
		}

		public override bool HandlesOnSpeech{ get { return true; } }

		[Constructable]
		public WarStone() : base( 3796 )
		{
			Name = "War Stone";
			Movable = false;
		}

		public WarStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Leader );
			writer.Write( m_Commander );
			writer.WriteMobileList( m_Members, true );
			writer.WriteItemList( m_SiegeWeapons, true );
			writer.WriteMobileList( m_Guards, true );
			writer.WriteItemList( m_GuardPoints, true );
			writer.Write( m_Gold );
			writer.Write( m_Wood );
			writer.Write( m_ColorOne );
			writer.Write( m_ColorTwo );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Leader = reader.ReadMobile();
			m_Commander = reader.ReadMobile();
			m_Members = reader.ReadMobileList();
			m_SiegeWeapons = reader.ReadItemList();
			m_Guards = reader.ReadMobileList();
			m_GuardPoints = reader.ReadItemList();
			m_Gold = reader.ReadULong();
			m_Wood = reader.ReadULong();
			m_ColorOne = reader.ReadInt();
			m_ColorTwo = reader.ReadInt();
		}
	}
}