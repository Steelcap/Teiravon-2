using System;
using Server;
using Server.Items;
using Server.Mobiles;


namespace Server.Items
{

	public class UnknownMask : BaseHat
	{
		public override int BasePhysicalResistance { get { return 0; } }
		public override int BaseFireResistance { get { return 0; } }
		public override int BaseColdResistance { get { return 0; } }
		public override int BasePoisonResistance { get { return 0; } }
		public override int BaseEnergyResistance { get { return 0; } }

		private string m_Name;
		private string m_Title;
		private string m_OldTitle;

		[CommandProperty( AccessLevel.GameMaster )]
		public string MaskName
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string MaskTitle
		{
			get { return m_Title; }
			set { m_Title = value; }
		}

		[Constructable]
		public UnknownMask()
			: this( 0 )
		{
		}

		[Constructable]
		public UnknownMask( int hue )
			: base( 0x154B, hue )
		{
			Weight = 1.0;
			Name = "a strange mask";
		}

		public override bool OnEquip( Mobile from )
		{
			bool ok = base.OnEquip( from );

			if ( !ok )
				return false;

			TeiravonMobile mob = from as TeiravonMobile;

			if ( mob != null )
			{
				if ( m_Name != null )
					mob.NameMod = m_Name;

				if ( m_Title != null )
				{
					m_OldTitle = mob.Title;
					mob.Title = m_Title;
				}
			}

			return true;
		}
		
		public override void OnRemoved( object parent )
		{
			TeiravonMobile mob = parent as TeiravonMobile;

			if ( mob != null )
			{
				if ( m_Name != null )
					mob.NameMod = null;

				if ( m_Title != null && m_OldTitle != null )
					mob.Title = m_OldTitle;
				else if ( m_Title != null && m_OldTitle == null )
					mob.Title = "the " + mob.PlayerRace.ToString();

				m_OldTitle = null;
			}

			base.OnRemoved( parent );
		}

		public UnknownMask( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version

			writer.Write( m_Name );
			writer.Write( m_Title );
			writer.Write( m_OldTitle );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Name = reader.ReadString();
			m_Title = reader.ReadString();
			m_OldTitle = reader.ReadString();
		}
	}
}