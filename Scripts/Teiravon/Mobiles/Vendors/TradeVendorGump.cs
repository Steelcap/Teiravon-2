using System;
using System.Collections;
using Server;
using Server.Factions;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	public class TradeVendorGump : Gump
	{
		Item m_Item;
		Mobile m_Player;
		TradeVendor m_Vendor;
		bool m_BuyMode = false;

		public TradeVendorGump( Item target, Mobile from, TradeVendor vendor, bool buymode )
			: base( 25, 25 )
		{
			Closable = true;
			Disposable = true;
			Dragable = true;

			m_Item = target;
			m_Player = from;
			m_Vendor = vendor;
			m_BuyMode = buymode;

			AddPage( 0 );
			AddBackground( 73, 33, 622, 422, 9270 ); // Background
			AddLabel( 273, 50, 308, @"Teiravon Resource Trading System" ); // Title

			if ( !m_BuyMode )
				AddButton( 626, 409, 1150, 1151, 2002, GumpButtonType.Reply, 0 ); // Delete

			AddButton( 398, 409, 241, 242, -1, GumpButtonType.Reply, 0 ); // Cancel
			AddButton( 321, 409, 247, 248, 2001, GumpButtonType.Reply, 0 ); // Okay
			AddButton( 104, 409, 4026, 4027, 2000, GumpButtonType.Reply, 0 ); // Help

			AddPage( 1 );
			AddButton( 644, 55, 9903, 9903, 0, GumpButtonType.Page, 2 ); // NextPage

			DrawTextEntries( 1 );

			#region AddItem
			AddItem( 365, 123, 7151, 0 ); // IronIngot
			AddItem( 365, 148, 7151, 2419 ); // DullCopperIngot
			AddItem( 365, 173, 7151, 2406 ); // ShadowIronIngot
			AddItem( 365, 198, 7151, 2413 ); // CopperIngot
			AddItem( 365, 223, 7151, 2418 ); // BronzeIngot
			AddItem( 365, 248, 7151, 2213 ); // GoldIngot
			AddItem( 365, 273, 7151, 2425 ); // AgapiteIngot
			AddItem( 365, 298, 7151, 2207 ); // VeriteIngot
			AddItem( 483, 123, 7151, 2219 ); // ValoriteIngot
			AddItem( 483, 148, 7151, 2229 ); // MithrilIngot
			AddItem( 483, 173, 7151, 2253 ); // BloodrockIngot
			AddItem( 483, 198, 7151, 2301 ); // SteelIngot
			AddItem( 483, 223, 7151, 2560 ); // AdamantiteIngot
			AddItem( 483, 248, 7151, 2292 ); // IthilmarIngot
			#endregion

			#region AddItem
			AddItem( 247, 123, 7130, 0 ); // OakBoard
			AddItem( 247, 148, 7130, 250 ); // PineBoard
			AddItem( 247, 173, 7130, 1872 ); // RedwoodBoard
			AddItem( 247, 198, 7130, 2971 ); // WhitePineBoard
			AddItem( 247, 223, 7130, 2553 ); // AshwoodBoard
			AddItem( 247, 248, 7130, 2229 ); // SilverBirchBoard
			AddItem( 247, 273, 7130, 1813 ); // YewBoard
			AddItem( 247, 298, 7130, 1109 ); // BlackOakBoard
			AddItem( 129, 123, 7133, 0 ); // OakLog
			AddItem( 129, 148, 7133, 250 ); // PineLog
			AddItem( 129, 173, 7133, 1872 ); // RedwoodLog
			AddItem( 129, 198, 7133, 2971 ); // WhitePineLog
			AddItem( 129, 223, 7133, 2553 ); // AshwoodLog
			AddItem( 129, 248, 7133, 2229 ); // SilverBirchLog
			AddItem( 129, 273, 7133, 1813 ); // YewLog
			AddItem( 129, 298, 7133, 1109 ); // BlackOakLog
			AddItem( 603, 123, 3820, 0 ); // Gold
			AddItem( 603, 169, 3826, 0 ); // Silver

			AddPage( 2 );
			AddButton( 101, 55, 9909, 9909, 0, GumpButtonType.Page, 1 ); // Button 5

			DrawTextEntries( 2 );

			AddItem( 296, 123, 6584, 0 ); // IronIngot
			AddItem( 296, 148, 6584, 2419 ); // DullCopperIngot
			AddItem( 296, 173, 6584, 2406 ); // ShadowIronIngot
			AddItem( 296, 198, 6584, 2413 ); // CopperIngot
			AddItem( 296, 223, 6584, 2418 ); // BronzeIngot
			AddItem( 296, 248, 6584, 2213 ); // GoldIngot
			AddItem( 296, 273, 6584, 2425 ); // AgapiteIngot
			AddItem( 296, 298, 6584, 2207 ); // VeriteIngot
			AddItem( 414, 123, 6584, 2219 ); // ValoriteOre
			AddItem( 414, 148, 6584, 2229 ); // MithrilIngot
			AddItem( 414, 173, 6584, 2253 ); // BloodrockIngot
			AddItem( 414, 198, 6584, 2301 ); // SteelIngot
			AddItem( 414, 223, 6584, 2560 ); // AdamantiteIngot
			AddItem( 414, 248, 6584, 2292 ); // IthilmarIngot
			#endregion

			#region AddItem
			AddItem( 550, 113, 4216, 0 ); // NormalHides
			AddItem( 550, 153, 4216, 643 ); // SpinedHides
			AddItem( 550, 193, 4216, 551 ); // HornedHides
			AddItem( 550, 233, 4216, 449 ); // BarbedHides
			AddItem( 194, 123, 9910, 1645 ); // RedScales
			AddItem( 194, 148, 9910, 2216 ); // YellowScales
			AddItem( 194, 173, 9910, 1109 ); // BlackScales
			AddItem( 194, 198, 9910, 2129 ); // GreenScales
			AddItem( 194, 223, 9910, 2301 ); // WhiteScales
			AddItem( 194, 248, 9910, 2224 ); // BlueScales
			#endregion
		}

		private void DrawTextEntries( int page )
		{
			TradeVendor.ItemInfo m_Info = TradeVendor.ItemInfo.GetItemInfo( m_Item, m_Vendor.ItemsForSale );
			bool m_HasInfo = true;

			if ( m_Info == null )
				m_HasInfo = false;

			if ( page == 1 )
			{
				AddTextEntry( 344, 122, 38, 20, 308, 1000, m_HasInfo ? m_Info.IronIngotCost.ToString() : "0" ); // Ingots
				AddTextEntry( 344, 146, 38, 20, 308, 1001, m_HasInfo ? m_Info.DullCopperIngotCost.ToString() : "0" );
				AddTextEntry( 344, 170, 38, 20, 308, 1002, m_HasInfo ? m_Info.ShadowIronIngotCost.ToString() : "0" );
				AddTextEntry( 344, 194, 38, 20, 308, 1003, m_HasInfo ? m_Info.CopperIngotCost.ToString() : "0" );
				AddTextEntry( 344, 218, 38, 20, 308, 1004, m_HasInfo ? m_Info.BronzeIngotCost.ToString() : "0" );
				AddTextEntry( 344, 242, 38, 20, 308, 1005, m_HasInfo ? m_Info.GoldIngotCost.ToString() : "0" );
				AddTextEntry( 344, 266, 38, 20, 308, 1006, m_HasInfo ? m_Info.AgapiteIngotCost.ToString() : "0" );
				AddTextEntry( 344, 290, 38, 20, 308, 1007, m_HasInfo ? m_Info.VeriteIngotCost.ToString() : "0" );
				AddTextEntry( 461, 122, 38, 20, 308, 1008, m_HasInfo ? m_Info.ValoriteIngotCost.ToString() : "0" );
				AddTextEntry( 461, 146, 38, 20, 308, 1009, m_HasInfo ? m_Info.MithrilIngotCost.ToString() : "0" );
				AddTextEntry( 461, 170, 38, 20, 308, 1010, m_HasInfo ? m_Info.BloodrockIngotCost.ToString() : "0" );
				AddTextEntry( 461, 194, 38, 20, 308, 1011, m_HasInfo ? m_Info.SteelIngotCost.ToString() : "0" );
				AddTextEntry( 461, 218, 38, 20, 308, 1012, m_HasInfo ? m_Info.AdamantiteIngotCost.ToString() : "0" );
				AddTextEntry( 461, 242, 38, 20, 308, 1013, m_HasInfo ? m_Info.IthilmarIngotCost.ToString() : "0" );
				AddTextEntry( 95, 122, 38, 20, 308, 1014, m_HasInfo ? m_Info.OakLogCost.ToString() : "0" ); // Logs
				AddTextEntry( 95, 146, 38, 20, 308, 1015, m_HasInfo ? m_Info.PineLogCost.ToString() : "0" );
				AddTextEntry( 95, 170, 38, 20, 308, 1016, m_HasInfo ? m_Info.RedwoodLogCost.ToString() : "0" );
				AddTextEntry( 95, 194, 38, 20, 308, 1017, m_HasInfo ? m_Info.WhitePineLogCost.ToString() : "0" );
				AddTextEntry( 95, 218, 38, 20, 308, 1018, m_HasInfo ? m_Info.AshwoodLogCost.ToString() : "0" );
				AddTextEntry( 95, 242, 38, 20, 308, 1019, m_HasInfo ? m_Info.SilverBirchLogCost.ToString() : "0" );
				AddTextEntry( 95, 266, 38, 20, 308, 1020, m_HasInfo ? m_Info.YewLogCost.ToString() : "0" );
				AddTextEntry( 95, 290, 38, 20, 308, 1021, m_HasInfo ? m_Info.BlackOakLogCost.ToString() : "0" );
				AddTextEntry( 220, 122, 38, 20, 308, 1022, m_HasInfo ? m_Info.OakBoardCost.ToString() : "0" ); // Boards
				AddTextEntry( 220, 146, 38, 20, 308, 1023, m_HasInfo ? m_Info.PineBoardCost.ToString() : "0" );
				AddTextEntry( 220, 170, 38, 20, 308, 1024, m_HasInfo ? m_Info.RedwoodBoardCost.ToString() : "0" );
				AddTextEntry( 220, 194, 38, 20, 308, 1025, m_HasInfo ? m_Info.WhitePineBoardCost.ToString() : "0" );
				AddTextEntry( 220, 218, 38, 20, 308, 1026, m_HasInfo ? m_Info.AshwoodBoardCost.ToString() : "0" );
				AddTextEntry( 220, 242, 38, 20, 308, 1027, m_HasInfo ? m_Info.SilverBirchBoardCost.ToString() : "0" );
				AddTextEntry( 220, 266, 38, 20, 308, 1028, m_HasInfo ? m_Info.YewBoardCost.ToString() : "0" );
				AddTextEntry( 220, 290, 38, 20, 308, 1029, m_HasInfo ? m_Info.BlackOakBoardCost.ToString() : "0" );
				AddTextEntry( 568, 122, 38, 20, 308, 1030, m_HasInfo ? m_Info.GoldCost.ToString() : "0" ); // GoldCost
				AddTextEntry( 568, 170, 38, 20, 308, 1031, m_HasInfo ? m_Info.SilverCost.ToString() : "0" ); // SilverCost
			}

			if ( page == 2 )
			{
				AddTextEntry( 371, 123, 38, 20, 308, 1032, m_HasInfo ? m_Info.ValoriteOreCost.ToString() : "0" ); // Valorite Ores
				AddTextEntry( 371, 147, 38, 20, 308, 1033, m_HasInfo ? m_Info.MithrilOreCost.ToString() : "0" ); // Mithril
				AddTextEntry( 371, 171, 38, 20, 308, 1034, m_HasInfo ? m_Info.BloodrockOreCost.ToString() : "0" ); // Bloodrock
				AddTextEntry( 371, 195, 38, 20, 308, 1035, m_HasInfo ? m_Info.SteelOreCost.ToString() : "0" ); // Steel
				AddTextEntry( 371, 219, 38, 20, 308, 1036, m_HasInfo ? m_Info.AdamantiteOreCost.ToString() : "0" ); // Adamantite
				AddTextEntry( 371, 243, 38, 20, 308, 1037, m_HasInfo ? m_Info.IthilmarOreCost.ToString() : "0" ); // Ithilmar

				AddTextEntry( 260, 123, 38, 20, 308, 1038, m_HasInfo ? m_Info.IronOreCost.ToString() : "0" ); // Iron Ores
				AddTextEntry( 260, 147, 38, 20, 308, 1039, m_HasInfo ? m_Info.DullCopperOreCost.ToString() : "0" ); // Dull Copper
				AddTextEntry( 260, 171, 38, 20, 308, 1040, m_HasInfo ? m_Info.ShadowIronOreCost.ToString() : "0" ); // Shadow Iron
				AddTextEntry( 260, 195, 38, 20, 308, 1041, m_HasInfo ? m_Info.CopperOreCost.ToString() : "0" ); // Copper
				AddTextEntry( 260, 219, 38, 20, 308, 1042, m_HasInfo ? m_Info.BronzeOreCost.ToString() : "0" ); // Bronze
				AddTextEntry( 260, 243, 38, 20, 308, 1043, m_HasInfo ? m_Info.GoldOreCost.ToString() : "0" ); // Gold
				AddTextEntry( 260, 267, 38, 20, 308, 1044, m_HasInfo ? m_Info.AgapiteOreCost.ToString() : "0" ); // Agapite
				AddTextEntry( 260, 291, 38, 20, 308, 1045, m_HasInfo ? m_Info.VeriteOreCost.ToString() : "0" ); // Verite

				AddTextEntry( 506, 123, 38, 20, 308, 1046, m_HasInfo ? m_Info.NormalHidesCost.ToString() : "0" ); // Hides
				AddTextEntry( 506, 164, 38, 20, 308, 1047, m_HasInfo ? m_Info.SpinedHidesCost.ToString() : "0" ); // Spined
				AddTextEntry( 506, 203, 38, 20, 308, 1048, m_HasInfo ? m_Info.HornedHidesCost.ToString() : "0" ); // Horned
				AddTextEntry( 506, 243, 38, 20, 308, 1049, m_HasInfo ? m_Info.BarbedHidesCost.ToString() : "0" ); // Barbed

				AddTextEntry( 163, 123, 38, 20, 308, 1050, m_HasInfo ? m_Info.RedScalesCost.ToString() : "0" ); // Scales
				AddTextEntry( 163, 147, 38, 20, 308, 1051, m_HasInfo ? m_Info.YellowScalesCost.ToString() : "0" );
				AddTextEntry( 163, 171, 38, 20, 308, 1052, m_HasInfo ? m_Info.BlackScalesCost.ToString() : "0" );
				AddTextEntry( 163, 195, 38, 20, 308, 1053, m_HasInfo ? m_Info.GreenScalesCost.ToString() : "0" );
				AddTextEntry( 163, 219, 38, 20, 308, 1054, m_HasInfo ? m_Info.WhiteScalesCost.ToString() : "0" );
				AddTextEntry( 163, 243, 38, 20, 308, 1055, m_HasInfo ? m_Info.BlueScalesCost.ToString() : "0" );
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int button = info.ButtonID;
			TextRelay[] textentries = info.TextEntries;

			switch ( button )
			{
				// Delete
				case 2002:
					for ( int i = 0; i < m_Vendor.ItemsForSale.Count; i++ )
						if ( ( ( TradeVendor.ItemInfo )m_Vendor.ItemsForSale[i] ).ItemSerial == m_Item.Serial )
						{
							m_Vendor.ItemsForSale.RemoveAt( i );
							break;
						}

					m_Player.Backpack.AddItem( m_Item );

					m_Player.SendMessage( "The item has been removed from inventory." );

					break;
				// Cancel
				case -1:

					break;
				// Okay
				case 2001:
					if ( !m_BuyMode )
					{
						#region Owner Setting Price
						TradeVendor.ItemInfo iteminfo = new TradeVendor.ItemInfo();

						iteminfo.ItemSerial = m_Item.Serial;

						#region Gold & Silver
						iteminfo.GoldCost = GetTextEntry( textentries, 1030 );
						iteminfo.SilverCost = GetTextEntry( textentries, 1031 );
						#endregion

						#region Ingots & Ores
						iteminfo.IronIngotCost = GetTextEntry( textentries, 1000 );
						iteminfo.DullCopperIngotCost = GetTextEntry( textentries, 1001 );
						iteminfo.ShadowIronIngotCost = GetTextEntry( textentries, 1002 );
						iteminfo.CopperIngotCost = GetTextEntry( textentries, 1003 );
						iteminfo.BronzeIngotCost = GetTextEntry( textentries, 1004 );
						iteminfo.GoldIngotCost = GetTextEntry( textentries, 1005 );
						iteminfo.AgapiteIngotCost = GetTextEntry( textentries, 1006 );
						iteminfo.VeriteIngotCost = GetTextEntry( textentries, 1007 );
						iteminfo.ValoriteIngotCost = GetTextEntry( textentries, 1008 );
						iteminfo.MithrilIngotCost = GetTextEntry( textentries, 1009 );
						iteminfo.BloodrockIngotCost = GetTextEntry( textentries, 1010 );
						iteminfo.SteelIngotCost = GetTextEntry( textentries, 1011 );
						iteminfo.AdamantiteIngotCost = GetTextEntry( textentries, 1012 );
						iteminfo.IthilmarIngotCost = GetTextEntry( textentries, 1013 );

						iteminfo.IronOreCost = GetTextEntry( textentries, 1038 );
						iteminfo.DullCopperOreCost = GetTextEntry( textentries, 1039 );
						iteminfo.ShadowIronOreCost = GetTextEntry( textentries, 1040 );
						iteminfo.CopperOreCost = GetTextEntry( textentries, 1041 );
						iteminfo.BronzeOreCost = GetTextEntry( textentries, 1042 );
						iteminfo.GoldOreCost = GetTextEntry( textentries, 1043 );
						iteminfo.AgapiteOreCost = GetTextEntry( textentries, 1044 );
						iteminfo.VeriteOreCost = GetTextEntry( textentries, 1045 );
						iteminfo.ValoriteOreCost = GetTextEntry( textentries, 1032 );
						iteminfo.MithrilOreCost = GetTextEntry( textentries, 1033 );
						iteminfo.BloodrockOreCost = GetTextEntry( textentries, 1034 );
						iteminfo.SteelOreCost = GetTextEntry( textentries, 1035 );
						iteminfo.AdamantiteOreCost = GetTextEntry( textentries, 1036 );
						iteminfo.IthilmarOreCost = GetTextEntry( textentries, 1037 );
						#endregion

						#region Hides & Scales
						iteminfo.NormalHidesCost = GetTextEntry( textentries, 1046 );
						iteminfo.SpinedHidesCost = GetTextEntry( textentries, 1047 );
						iteminfo.HornedHidesCost = GetTextEntry( textentries, 1048 );
						iteminfo.BarbedHidesCost = GetTextEntry( textentries, 1049 );

						iteminfo.RedScalesCost = GetTextEntry( textentries, 1050 );
						iteminfo.YellowScalesCost = GetTextEntry( textentries, 1051 );
						iteminfo.BlackScalesCost = GetTextEntry( textentries, 1052 );
						iteminfo.GreenScalesCost = GetTextEntry( textentries, 1053 );
						iteminfo.WhiteScalesCost = GetTextEntry( textentries, 1054 );
						iteminfo.BlueScalesCost = GetTextEntry( textentries, 1055 );
						#endregion

						#region Logs & Boards
						iteminfo.OakLogCost = GetTextEntry( textentries, 1014 );
						iteminfo.PineLogCost = GetTextEntry( textentries, 1015 );
						iteminfo.RedwoodLogCost = GetTextEntry( textentries, 1016 );
						iteminfo.WhitePineLogCost = GetTextEntry( textentries, 1017 );
						iteminfo.AshwoodLogCost = GetTextEntry( textentries, 1018 );
						iteminfo.SilverBirchLogCost = GetTextEntry( textentries, 1019 );
						iteminfo.YewLogCost = GetTextEntry( textentries, 1020 );
						iteminfo.BlackOakLogCost = GetTextEntry( textentries, 1021 );

						iteminfo.OakBoardCost = GetTextEntry( textentries, 1022 );
						iteminfo.PineBoardCost = GetTextEntry( textentries, 1023 );
						iteminfo.RedwoodBoardCost = GetTextEntry( textentries, 1024 );
						iteminfo.WhitePineBoardCost = GetTextEntry( textentries, 1025 );
						iteminfo.AshwoodBoardCost = GetTextEntry( textentries, 1026 );
						iteminfo.SilverBirchBoardCost = GetTextEntry( textentries, 1027 );
						iteminfo.YewBoardCost = GetTextEntry( textentries, 1028 );
						iteminfo.BlackOakBoardCost = GetTextEntry( textentries, 1029 );
						#endregion

						#region Jewels
						// Not Yet
						#endregion

						TradeVendor.ItemInfo m_Info = TradeVendor.ItemInfo.GetItemInfo( m_Item, m_Vendor.ItemsForSale );

						if ( m_Info != null )
							m_Vendor.ItemsForSale.Remove( m_Info );

						m_Vendor.ItemsForSale.Add( iteminfo );
						m_Vendor.Backpack.AddItem( m_Item );
						#endregion
					}
					else
					{
						#region Player Buying Item
						TradeVendor.ItemInfo m_Info = TradeVendor.ItemInfo.GetItemInfo( m_Item, m_Vendor.ItemsForSale );
						ArrayList m_ConsumeType = new ArrayList();
						ArrayList m_ConsumeAmt = new ArrayList();

						if ( m_Info != null )
						{
							if ( m_Info.AdamantiteIngotCost > 0 ) { m_ConsumeType.Add( typeof( AdamantiteIngot ) ); m_ConsumeAmt.Add( m_Info.AdamantiteIngotCost ); }
							if ( m_Info.AdamantiteOreCost > 0 ) { m_ConsumeType.Add( typeof( AdamantiteOre ) ); m_ConsumeAmt.Add( m_Info.AdamantiteOreCost ); }
							if ( m_Info.AgapiteIngotCost > 0 ) { m_ConsumeType.Add( typeof( AgapiteIngot ) ); m_ConsumeAmt.Add( m_Info.AgapiteIngotCost ); }
							if ( m_Info.AgapiteOreCost > 0 ) { m_ConsumeType.Add( typeof( AgapiteOre ) ); m_ConsumeAmt.Add( m_Info.AgapiteOreCost ); }
							if ( m_Info.AshwoodBoardCost > 0 ) { m_ConsumeType.Add( typeof( AshwoodBoard ) ); m_ConsumeAmt.Add( m_Info.AshwoodBoardCost ); }
							if ( m_Info.AshwoodLogCost > 0 ) { m_ConsumeType.Add( typeof( AshwoodLog ) ); m_ConsumeAmt.Add( m_Info.AshwoodLogCost ); }
							if ( m_Info.BarbedHidesCost > 0 ) { m_ConsumeType.Add( typeof( BarbedHides ) ); m_ConsumeAmt.Add( m_Info.BarbedHidesCost ); }
							if ( m_Info.BlackOakBoardCost > 0 ) { m_ConsumeType.Add( typeof( BlackOakBoard ) ); m_ConsumeAmt.Add( m_Info.BlackOakBoardCost ); }
							if ( m_Info.BlackOakLogCost > 0 ) { m_ConsumeType.Add( typeof( BlackOakLog ) ); m_ConsumeAmt.Add( m_Info.BlackOakLogCost ); }
							if ( m_Info.BlackScalesCost > 0 ) { m_ConsumeType.Add( typeof( BlackScales ) ); m_ConsumeAmt.Add( m_Info.BlackScalesCost ); }
							if ( m_Info.BloodrockIngotCost > 0 ) { m_ConsumeType.Add( typeof( BloodrockIngot ) ); m_ConsumeAmt.Add( m_Info.BloodrockIngotCost ); }
							if ( m_Info.BloodrockOreCost > 0 ) { m_ConsumeType.Add( typeof( BloodrockOre ) ); m_ConsumeAmt.Add( m_Info.BloodrockOreCost ); }
							if ( m_Info.BlueScalesCost > 0 ) { m_ConsumeType.Add( typeof( BlueScales ) ); m_ConsumeAmt.Add( m_Info.BlueScalesCost ); }
							if ( m_Info.BronzeIngotCost > 0 ) { m_ConsumeType.Add( typeof( BronzeIngot ) ); m_ConsumeAmt.Add( m_Info.BronzeIngotCost ); }
							if ( m_Info.BronzeOreCost > 0 ) { m_ConsumeType.Add( typeof( BronzeOre ) ); m_ConsumeAmt.Add( m_Info.BronzeOreCost ); }
							if ( m_Info.CopperIngotCost > 0 ) { m_ConsumeType.Add( typeof( CopperIngot ) ); m_ConsumeAmt.Add( m_Info.CopperIngotCost ); }
							if ( m_Info.CopperOreCost > 0 ) { m_ConsumeType.Add( typeof( CopperOre ) ); m_ConsumeAmt.Add( m_Info.CopperOreCost ); }
							if ( m_Info.DullCopperIngotCost > 0 ) { m_ConsumeType.Add( typeof( DullCopperIngot ) ); m_ConsumeAmt.Add( m_Info.DullCopperIngotCost ); }
							if ( m_Info.DullCopperOreCost > 0 ) { m_ConsumeType.Add( typeof( DullCopperOre ) ); m_ConsumeAmt.Add( m_Info.DullCopperOreCost ); }
							if ( m_Info.GoldCost > 0 ) { m_ConsumeType.Add( typeof( Gold ) ); m_ConsumeAmt.Add( m_Info.GoldCost ); }
							if ( m_Info.GoldIngotCost > 0 ) { m_ConsumeType.Add( typeof( GoldIngot ) ); m_ConsumeAmt.Add( m_Info.GoldIngotCost ); }
							if ( m_Info.GoldOreCost > 0 ) { m_ConsumeType.Add( typeof( GoldOre ) ); m_ConsumeAmt.Add( m_Info.GoldOreCost ); }
							if ( m_Info.GreenScalesCost > 0 ) { m_ConsumeType.Add( typeof( GreenScales ) ); m_ConsumeAmt.Add( m_Info.GreenScalesCost ); }
							if ( m_Info.HornedHidesCost > 0 ) { m_ConsumeType.Add( typeof( HornedHides ) ); m_ConsumeAmt.Add( m_Info.HornedHidesCost ); }
							if ( m_Info.IronIngotCost > 0 ) { m_ConsumeType.Add( typeof( IronIngot ) ); m_ConsumeAmt.Add( m_Info.IronIngotCost ); }
							if ( m_Info.IronOreCost > 0 ) { m_ConsumeType.Add( typeof( IronOre ) ); m_ConsumeAmt.Add( m_Info.IronOreCost ); }
							if ( m_Info.IthilmarIngotCost > 0 ) { m_ConsumeType.Add( typeof( IthilmarIngot ) ); m_ConsumeAmt.Add( m_Info.IthilmarIngotCost ); }
							if ( m_Info.IthilmarOreCost > 0 ) { m_ConsumeType.Add( typeof( IthilmarOre ) ); m_ConsumeAmt.Add( m_Info.IthilmarOreCost ); }
							if ( m_Info.MithrilIngotCost > 0 ) { m_ConsumeType.Add( typeof( MithrilIngot ) ); m_ConsumeAmt.Add( m_Info.MithrilIngotCost ); }
							if ( m_Info.MithrilOreCost > 0 ) { m_ConsumeType.Add( typeof( MithrilOre ) ); m_ConsumeAmt.Add( m_Info.MithrilOreCost ); }
							if ( m_Info.NormalHidesCost > 0 ) { m_ConsumeType.Add( typeof( Hides ) ); m_ConsumeAmt.Add( m_Info.NormalHidesCost ); }
							if ( m_Info.OakBoardCost > 0 ) { m_ConsumeType.Add( typeof( Board ) ); m_ConsumeAmt.Add( m_Info.OakBoardCost ); }
							if ( m_Info.OakLogCost > 0 ) { m_ConsumeType.Add( typeof( Log ) ); m_ConsumeAmt.Add( m_Info.OakLogCost ); }
							if ( m_Info.PineBoardCost > 0 ) { m_ConsumeType.Add( typeof( PineBoard ) ); m_ConsumeAmt.Add( m_Info.PineBoardCost ); }
							if ( m_Info.PineLogCost > 0 ) { m_ConsumeType.Add( typeof( PineLog ) ); m_ConsumeAmt.Add( m_Info.PineLogCost ); }
							if ( m_Info.RedScalesCost > 0 ) { m_ConsumeType.Add( typeof( RedScales ) ); m_ConsumeAmt.Add( m_Info.RedScalesCost ); }
							if ( m_Info.RedwoodBoardCost > 0 ) { m_ConsumeType.Add( typeof( RedwoodBoard ) ); m_ConsumeAmt.Add( m_Info.RedwoodBoardCost ); }
							if ( m_Info.RedwoodLogCost > 0 ) { m_ConsumeType.Add( typeof( RedwoodLog ) ); m_ConsumeAmt.Add( m_Info.RedwoodLogCost ); }
							if ( m_Info.ShadowIronIngotCost > 0 ) { m_ConsumeType.Add( typeof( ShadowIronIngot ) ); m_ConsumeAmt.Add( m_Info.ShadowIronIngotCost ); }
							if ( m_Info.ShadowIronOreCost > 0 ) { m_ConsumeType.Add( typeof( ShadowIronOre ) ); m_ConsumeAmt.Add( m_Info.ShadowIronOreCost ); }
							if ( m_Info.SilverBirchBoardCost > 0 ) { m_ConsumeType.Add( typeof( SilverBirchBoard ) ); m_ConsumeAmt.Add( m_Info.SilverBirchBoardCost ); }
							if ( m_Info.SilverBirchLogCost > 0 ) { m_ConsumeType.Add( typeof( SilverBirchLog ) ); m_ConsumeAmt.Add( m_Info.SilverBirchLogCost ); }
							if ( m_Info.SilverCost > 0 ) { m_ConsumeType.Add( typeof( Silver ) ); m_ConsumeAmt.Add( m_Info.SilverCost ); }
							if ( m_Info.SpinedHidesCost > 0 ) { m_ConsumeType.Add( typeof( SpinedHides ) ); m_ConsumeAmt.Add( m_Info.SpinedHidesCost ); }
							if ( m_Info.SteelIngotCost > 0 ) { m_ConsumeType.Add( typeof( SteelIngot ) ); m_ConsumeAmt.Add( m_Info.SteelIngotCost ); }
							if ( m_Info.SteelOreCost > 0 ) { m_ConsumeType.Add( typeof( SteelOre ) ); m_ConsumeAmt.Add( m_Info.SteelOreCost ); }
							if ( m_Info.ValoriteIngotCost > 0 ) { m_ConsumeType.Add( typeof( ValoriteIngot ) ); m_ConsumeAmt.Add( m_Info.ValoriteIngotCost ); }
							if ( m_Info.ValoriteOreCost > 0 ) { m_ConsumeType.Add( typeof( ValoriteOre ) ); m_ConsumeAmt.Add( m_Info.ValoriteOreCost ); }
							if ( m_Info.VeriteIngotCost > 0 ) { m_ConsumeType.Add( typeof( VeriteIngot ) ); m_ConsumeAmt.Add( m_Info.VeriteIngotCost ); }
							if ( m_Info.VeriteOreCost > 0 ) { m_ConsumeType.Add( typeof( VeriteOre ) ); m_ConsumeAmt.Add( m_Info.VeriteOreCost ); }
							if ( m_Info.WhitePineBoardCost > 0 ) { m_ConsumeType.Add( typeof( WhitePineBoard ) ); m_ConsumeAmt.Add( m_Info.WhitePineBoardCost ); }
							if ( m_Info.WhitePineLogCost > 0 ) { m_ConsumeType.Add( typeof( WhitePineLog ) ); m_ConsumeAmt.Add( m_Info.WhitePineLogCost ); }
							if ( m_Info.WhiteScalesCost > 0 ) { m_ConsumeType.Add( typeof( WhiteScales ) ); m_ConsumeAmt.Add( m_Info.WhiteScalesCost ); }
							if ( m_Info.YellowScalesCost > 0 ) { m_ConsumeType.Add( typeof( YellowScales ) ); m_ConsumeAmt.Add( m_Info.YellowScalesCost ); }
							if ( m_Info.YewBoardCost > 0 ) { m_ConsumeType.Add( typeof( YewBoard ) ); m_ConsumeAmt.Add( m_Info.YewBoardCost ); }
							if ( m_Info.YewLogCost > 0 ) { m_ConsumeType.Add( typeof( YewLog ) ); m_ConsumeAmt.Add( m_Info.YewLogCost ); }

							Type[] types = ( Type[] )m_ConsumeType.ToArray( typeof( Type ) );
							int[] amts = ( int[] )m_ConsumeAmt.ToArray( typeof( int ) );

							if ( m_Player.Backpack.ConsumeTotal( types, amts, true ) == -1 )
							{
								m_Player.Backpack.AddItem( m_Item );
								m_Vendor.ItemsForSale.Remove( m_Info );

								for ( int i = 0; i < m_ConsumeType.Count; i++ )
								{
									Item item = (Item)Activator.CreateInstance( ( Type )m_ConsumeType[i] );
									item.Amount = ( int )m_ConsumeAmt[i];

									m_Vendor.BankBox.AddItem( item );
								}

								m_Vendor.Say( "Here you go!" );
							}
							else
								m_Player.SendMessage( "You don't have the required resources for this." );
						}
						#endregion
					}

					break;
				// Help
				case 2000:
					m_Player.SendMessage( "I wonder what this is supposed to do?" );

					break;
			}
		}

		private bool HasResource( Item resource, int amount )
		{
			foreach ( Item item in m_Player.Backpack.Items )
				if ( item.GetType() == resource.GetType() && item.Amount >= amount )
					return true;

			return false;
		}

		private int GetTextEntry( TextRelay[] entries, int entryid )
		{
			for ( int i = 0; i < entries.Length; i++ )
				if ( entries[i].EntryID == entryid )
				{
					try
					{
						int value = Int32.Parse( entries[i].Text );

						if ( value > 100000 )
							return 100000;
						else if ( value < 0 )
							return 0;
						else
							return value;
					}
					catch
					{
						return 0;
					}
				}

			return 0;
		}
	}
}