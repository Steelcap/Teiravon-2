using System;
using Server;
using Server.Gumps;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;

namespace Server.Gumps
{
	public class OrcRankGump : Gump
	{
		Mobile m_Player;
		
		public OrcRankGump( Mobile from ) : base( 0, 0 )
		{
			m_Player = from;
			m_Player.CloseGump( typeof( OrcRankGump ) );		
			
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			
			AddPage(0);
			AddImageTiled(300, 125, 22, 180, 2625);
			AddImageTiled(173, 304, 130, 17, 2627);
			AddImage(302, 304, 2628);
			AddImage(171, 304, 2626);
			AddImageTiled(175, 114, 128, 17, 2621);
			AddImage(171, 114, 2620);
			AddImage(302, 114, 2622);
			AddImageTiled(171, 125, 21, 180, 2623);
			AddImageTiled(176, 119, 141, 198, 2624);
			AddAlphaRegion(176, 119, 141, 198 );
			AddLabel(214, 130, 1000, @"Orc Ranks");
			AddLabel(190, 155, 1000, @"Pug");
			AddLabel(190, 180, 1000, @"Grunt");
			AddLabel(190, 205, 1000, @"Sergeant");
			AddLabel(190, 230, 1000, @"Captain");
			AddLabel(190, 255, 1000, @"Shaman");
			AddLabel(190, 280, 1000, @"High-Shaman");
			AddButton(285, 158, 1209, 1210, (int)Buttons.Pug, GumpButtonType.Reply, 0);
			AddButton(285, 183, 1209, 1210, (int)Buttons.Grunt, GumpButtonType.Reply, 0);
			AddButton(285, 208, 1209, 1210, (int)Buttons.Sergeant, GumpButtonType.Reply, 0);	
			AddButton(285, 233, 1209, 1210, (int)Buttons.Captain, GumpButtonType.Reply, 0);
			AddButton(285, 258, 1209, 1210, (int)Buttons.Shaman, GumpButtonType.Reply, 0);
			AddButton(285, 283, 1209, 1210, (int)Buttons.HighShaman, GumpButtonType.Reply, 0);
		}
		public enum Buttons
		{
			Pug,
			Grunt,
			Sergeant,
			Captain,
			Shaman,
			HighShaman

		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{

			switch ( info.ButtonID )
			{

				case (int)Buttons.Pug:
				
							m_Player.Target = new ChangeRankTarget( Server.Items.OrcFace.Rank.None );
							break;

				case (int)Buttons.Grunt:
							
							m_Player.Target = new ChangeRankTarget( Server.Items.OrcFace.Rank.Grunt );
							break;
							
				case (int)Buttons.Sergeant:
							
							m_Player.Target = new ChangeRankTarget( Server.Items.OrcFace.Rank.Sergeant );							
							break;
							
				case (int)Buttons.Captain:
							
							m_Player.Target = new ChangeRankTarget( Server.Items.OrcFace.Rank.Captain );						
							break;
							
				case (int)Buttons.Shaman:
				
							m_Player.Target = new ChangeRankTarget( Server.Items.OrcFace.Rank.Shaman );							
							break;
				
				case (int)Buttons.HighShaman:
							
							m_Player.Target = new ChangeRankTarget( Server.Items.OrcFace.Rank.HighShaman );
							break;
			}
		}
		
		public class ChangeRankTarget : Target
		{
			OrcFace.Rank m_Rank;
			
			public ChangeRankTarget( OrcFace.Rank rank ) : base( -1, false, TargetFlags.None )
			{
				m_Rank = rank;
			}
			
			protected override void OnTarget( Mobile from, object targ )
			{
				if ( from is TeiravonMobile && ((TeiravonMobile)from).IsOrc() )
				{
					Item ownerface = from.FindItemOnLayer( Layer.FacialHair );
					OrcFace.Rank ownerrank = OrcFace.Rank.None;
					
					if ( ownerface is OrcFace )
						ownerrank = ((OrcFace)ownerface).OrcRank;
					else
					{
						from.SendMessage( "You are not a part of the tribe which uses this item." );
						return;
					}
						
					if ( ownerrank != OrcFace.Rank.Warboss )
					{
						from.SendMessage( "Only the orc warboss can set ranks." );
						return;
					}
				}
				else
				{
					from.SendMessage( "You're not orc." );
					return;
				}
			
				if ( (targ is TeiravonMobile) && ((TeiravonMobile)targ).IsOrc() )
				{				
					if ( from == (Mobile)targ )
						from.SendMessage( "You cannot change your own rank." );
					else
					{
						Item face = ((PlayerMobile)targ).FindItemOnLayer( Layer.FacialHair );
						
						if ( face is OrcFace )
							((OrcFace)face).OrcRank = m_Rank;			
						else
							from.SendMessage( "That orc is not a part of your tribe." );
					}
				}							
				else 
					from.SendMessage( "You can only target orcs.");
			}
		}
	}
}

namespace Server.Items
{
	public class OrcRankStone : Item
	{
		[Constructable]
		public OrcRankStone() : base(0x1181)
		{
			Name = "Orc Rank Stone";
			Weight = 0.0;
			LootType = LootType.Blessed;
			Hue = 0xA05;
		}
		
		public override void OnDoubleClick( Mobile from )
		{			
			if ( IsChildOf( from.Backpack ) || Parent == from )
			{			
				if (from is TeiravonMobile && ((TeiravonMobile)from).IsOrc() )
				{
					TeiravonMobile m_Player = (TeiravonMobile)from;
					m_Player.SendGump( new OrcRankGump( m_Player ) );
				}
				else
					from.SendMessage( "You cannot use that." );
			}
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.		
		}
		
		public OrcRankStone( Serial serial ) : base( serial )
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