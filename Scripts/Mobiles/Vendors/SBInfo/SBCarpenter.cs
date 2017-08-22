using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBCarpenter: SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCarpenter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				/*
				Add( new GenericBuyInfo( typeof( Lute ), 21, 20, 0xEB3, 0 ) );
				Add( new GenericBuyInfo( typeof( LapHarp ), 21, 20, 0xEB2, 0 ) );
				Add( new GenericBuyInfo( typeof( Tambourine ), 21, 20, 0xE9D, 0 ) );
				Add( new GenericBuyInfo( typeof( Drums ), 21, 20, 0xE9C, 0 ) );
				*/
				Add( new GenericBuyInfo( typeof( JointingPlane ), 5, 20, 0x1030, 0 ) );
				Add( new GenericBuyInfo( typeof( SmoothingPlane ), 5, 20, 0x1032, 0 ) );
				Add( new GenericBuyInfo( typeof( MouldingPlane ), 5, 20, 0x102C, 0 ) );
				Add( new GenericBuyInfo( typeof( Hammer ), 5, 20, 0x102A, 0 ) );
				Add( new GenericBuyInfo( typeof( Saw ), 5, 20, 0x1034, 0 ) );
				Add( new GenericBuyInfo( typeof( DovetailSaw ), 5, 20, 0x1028, 0 ) );
				Add( new GenericBuyInfo( typeof( Inshave ), 5, 20, 0x10E6, 0 ) );
				Add( new GenericBuyInfo( typeof( Scorp ), 5, 20, 0x10E7, 0 ) );
				Add( new GenericBuyInfo( typeof( Froe ), 5, 20, 0x10E5, 0 ) );
				Add( new GenericBuyInfo( typeof( DrawKnife ), 5, 20, 0x10E4, 0 ) );
				Add( new GenericBuyInfo( typeof( Board ), 1, 20, 0x1BD7, 0 ) );
				Add( new GenericBuyInfo( typeof( Axle ), 1, 20, 0x105B, 0 ) );
				Add( new GenericBuyInfo( typeof( Nails ), 1, 20, 0x102E, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( SmallCrate ), 5 );
				Add( typeof( MediumCrate ), 6 );
				Add( typeof( LargeCrate ), 7 );
				Add( typeof( WoodenChest ), 15 );

				Add( typeof( LargeTable ), 10 );
				Add( typeof( Nightstand ), 7 );
				Add( typeof( YewWoodTable ), 10 );

				Add( typeof( Throne ), 24 );
				Add( typeof( WoodenThrone ), 6 );
				Add( typeof( Stool ), 6 );
				Add( typeof( FootStool ), 6 );

				Add( typeof( FancyWoodenChairCushion ), 12 );
				Add( typeof( WoodenChairCushion ), 10 );
				Add( typeof( WoodenChair ), 8 );
				Add( typeof( BambooChair ), 6 );
				Add( typeof( WoodenBench ), 6 );

				Add( typeof( Saw ), 9 );
				Add( typeof( Scorp ), 6 );
				Add( typeof( SmoothingPlane ), 6 );
				Add( typeof( DrawKnife ), 6 );
				Add( typeof( Froe ), 6 );
				Add( typeof( Hammer ), 14 );
				Add( typeof( Inshave ), 6 );
				Add( typeof( JointingPlane ), 6 );
				Add( typeof( MouldingPlane ), 6 );
				Add( typeof( DovetailSaw ), 7 );
				Add( typeof( Board ), 1 );
				Add( typeof( Axle ), 1 );

				Add( typeof( WoodenShield ), 31 );
				Add( typeof( BlackStaff ), 24 );
				Add( typeof( GnarledStaff ), 12 );
				Add( typeof( QuarterStaff ), 15 );
				Add( typeof( ShepherdsCrook ), 12 );
				Add( typeof( Club ), 13 );

				Add( typeof( Lute ), 10 );
				Add( typeof( LapHarp ), 10 );
				Add( typeof( Tambourine ), 10 );
				Add( typeof( Drums ), 10 );
			}
		}
	}
}
