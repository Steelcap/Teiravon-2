
using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBAnimalTrainer : SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAnimalTrainer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( Eagle ), 402, 10, 5, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( Cat ), 138, 10, 201, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( Horse ), 1000, 10, 204, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( Rabbit ), 78, 10, 205, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( BrownBear ), 855, 10, 167, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( GrizzlyBear ), 1767, 10, 212, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( Panther ), 1271, 10, 214, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( Dog ), 181, 10, 217, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( TimberWolf ), 768, 10, 225, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( PackHorse ), 606, 10, 291, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( PackLlama ), 491, 10, 292, 0 ) );
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
