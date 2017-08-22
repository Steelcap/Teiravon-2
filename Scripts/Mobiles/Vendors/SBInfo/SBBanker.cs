
using System;
using System.Collections;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	public class SBBanker : SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBanker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				//Add( new GenericBuyInfo( "1047016", typeof( CommodityDeed ), 5, 20, 0x14F0, 0x47 ) );
				Add( new GenericBuyInfo( "1041243", typeof( ContractOfEmployment ), 1252, 20, 0x14F0, 0 ) );
                Add( new GenericBuyInfo( "10 Silver Coins", typeof( SilverCheck ), 1, 10, 0xEF0,0));
				//if ( Multis.BaseHouse.NewVendorSystem )
				//	Add( new GenericBuyInfo( "1062332", typeof( VendorRentalContract ), 1252, 20, 0x14F0, 0x672 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
}