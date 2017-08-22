using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Targeting;

namespace Server.Mobiles
{
    public class TradeVendor : BaseCreature
    {
        private Mobile m_Owner = null;
        private ArrayList m_ItemsForSale = new ArrayList();

        [CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
        public Mobile Owner { get { return m_Owner; } set { m_Owner = value; } }

        public ArrayList ItemsForSale { get { return m_ItemsForSale; } set { m_ItemsForSale = value; } }

        public override void OnSpeech( SpeechEventArgs e )
        {
            Mobile from = e.Mobile;

            if ( e.Handled || !from.Alive || from.GetDistanceToSqrt( this ) > 3 )
                return;

            if ( e.HasKeyword( 0x3C ) || ( e.HasKeyword( 0x171 ) && WasNamed( e.Speech ) ) ) // vendor buy, *buy*
            {
                from.SendLocalizedMessage( 503213 ); // Select the item you wish to buy.
                from.Target = new BuyTarget( this );

                e.Handled = true;
            }
            else if ( e.HasKeyword( 0x3D ) || ( e.HasKeyword( 0x172 ) && WasNamed( e.Speech ) ) ) // vendor browse, *browse
            {
                Backpack.DisplayTo( from );
                e.Handled = true;
            }
            else if ( e.Speech.ToLower().IndexOf( "price" ) > -1 && e.Mobile == m_Owner && WasNamed( e.Speech ) )
            {

                from.Target = new PriceTarget( this );

                e.Handled = true;
			}
			else if ( e.Speech.ToLower().IndexOf( "collect" ) > -1 && e.Mobile == m_Owner && WasNamed( e.Speech ) )
			{
				if ( BankBox.Items == null || BankBox.Items.Count == 0 )
				{
					m_Owner.SendMessage( "I have not sold any items." );
					return;
				}

				for ( int i = 0; i < BankBox.Items.Count; i++ )
					m_Owner.Backpack.AddItem( (Item)BankBox.Items[ i ] );

				e.Handled = true;
			}
        }

        public bool WasNamed( string speech )
        {
            return this.Name != null && Insensitive.StartsWith( speech, this.Name );
        }

        [Constructable]
        public TradeVendor()
            : base( AIType.AI_Vendor, FightMode.None, 5, 0, 0.0, 0.0 )
        {
            Name = "Trade Vendor";
            Title = "the Vendor";
            BodyValue = 0x190;
            SetHits( 15000 );
            SetStr( 15000 );
			CantWalk = true;

            Backpack pack = new Backpack();
            EquipItem( pack );

			BankBox bank = new BankBox( this );
			bank.MaxItems = 15000;
			EquipItem( bank );
        }

        public TradeVendor( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            if ( m_Owner == null || m_Owner.Deleted )
                writer.Write( ( Mobile )this );
            else
                writer.Write( ( Mobile )m_Owner );

            writer.Write( ( int )m_ItemsForSale.Count );

            for ( int i = 0; i < m_ItemsForSale.Count; i++ )
                ( ( ItemInfo )m_ItemsForSale[ i ] ).WriteInfo( writer );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            m_Owner = reader.ReadMobile();
            int count = reader.ReadInt();

            if ( count <= 0 )
                m_ItemsForSale = new ArrayList();
            else
                for ( int i = 0; i < count; i++ )
                {
                    ItemInfo info = new ItemInfo();
                    info.ReadInfo( reader );

                    m_ItemsForSale.Add( info );
                }

            /*if ( count > 0 )
            {
                ArrayList baditems = new ArrayList();

                foreach ( ItemInfo info in m_ItemsForSale )
                {
                    Item item = World.FindItem( info.ItemSerial );

                    if ( item == null || item.Deleted || !Backpack.Items.Contains( ( Item )item ) )
                        baditems.Add( info );
                }

                foreach ( ItemInfo badinfo in baditems )
                    m_ItemsForSale.Remove( badinfo );
            }*/
        }

        private class PriceTarget : Target
        {
            private TradeVendor m_Vendor;

            public PriceTarget( TradeVendor vendor )
                : base( -1, false, TargetFlags.None )
            {
                m_Vendor = vendor;
                AllowNonlocal = true;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( targeted is Item && ( from.Backpack.Items.Contains( ( Item )targeted ) || m_Vendor.Backpack.Items.Contains( ( Item )targeted ) ) )
                    from.SendGump( new TradeVendorGump( (Item)targeted, from, m_Vendor, false ) );
                else
                    from.SendMessage( "You must target a valid item in your pack." );
            }
        }

        private class BuyTarget : Target
        {
            private TradeVendor m_Vendor;

            public BuyTarget( TradeVendor vendor )
                : base( -1, false, TargetFlags.None )
            {
                m_Vendor = vendor;
                AllowNonlocal = true;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( targeted is Item && m_Vendor.Backpack.Items.Contains( ( Item )targeted ) )
                    from.SendGump( new TradeVendorGump( ( Item )targeted, from, m_Vendor, true ) );
            }
        }

        public class ItemInfo
        {
            #region Resources
            // Gold & Silver
            public int GoldCost = 0;
            public int SilverCost = 0;

            // Logs
            public int OakLogCost = 0;
            public int PineLogCost = 0;
            public int RedwoodLogCost = 0;
            public int WhitePineLogCost = 0;
            public int AshwoodLogCost = 0;
            public int SilverBirchLogCost = 0;
            public int YewLogCost = 0;
            public int BlackOakLogCost = 0;

            // Boards
            public int OakBoardCost = 0;
            public int PineBoardCost = 0;
            public int RedwoodBoardCost = 0;
            public int WhitePineBoardCost = 0;
            public int AshwoodBoardCost = 0;
            public int SilverBirchBoardCost = 0;
            public int YewBoardCost = 0;
            public int BlackOakBoardCost = 0;

            // Ingots
            public int IronIngotCost = 0;
            public int DullCopperIngotCost = 0;
            public int ShadowIronIngotCost = 0;
            public int CopperIngotCost = 0;
            public int BronzeIngotCost = 0;
            public int GoldIngotCost = 0;
            public int AgapiteIngotCost = 0;
            public int VeriteIngotCost = 0;
            public int ValoriteIngotCost = 0;
            public int MithrilIngotCost = 0;
            public int BloodrockIngotCost = 0;
            public int SteelIngotCost = 0;
            public int AdamantiteIngotCost = 0;
            public int IthilmarIngotCost = 0;

            // Ores
            public int IronOreCost = 0;
            public int DullCopperOreCost = 0;
            public int ShadowIronOreCost = 0;
            public int CopperOreCost = 0;
            public int BronzeOreCost = 0;
            public int GoldOreCost = 0;
            public int AgapiteOreCost = 0;
            public int VeriteOreCost = 0;
            public int ValoriteOreCost = 0;
            public int MithrilOreCost = 0;
            public int BloodrockOreCost = 0;
            public int SteelOreCost = 0;
            public int AdamantiteOreCost = 0;
            public int IthilmarOreCost = 0;

            // Jewels
            public int StarSapphireCost = 0;
            public int EmeraldCost = 0;
            public int RubyCost = 0;
            public int CitrineCost = 0;
            public int AmethystCost = 0;
            public int TourmalineCost = 0;
            public int AmberCost = 0;
            public int DiamondCost = 0;

            // Hides
            public int NormalHidesCost = 0;
            public int SpinedHidesCost = 0;
            public int BarbedHidesCost = 0;
            public int HornedHidesCost = 0;

            // Scales
            public int RedScalesCost = 0;
            public int YellowScalesCost = 0;
            public int BlackScalesCost = 0;
            public int GreenScalesCost = 0;
            public int WhiteScalesCost = 0;
            public int BlueScalesCost = 0;
            #endregion

            public Serial ItemSerial = Serial.Zero;

            public static ItemInfo GetItemInfo( Item item, ArrayList info )
            {
                for ( int i = 0; i < info.Count; i++ )
                    if ( item.Serial == ( ( ItemInfo )info[ i ] ).ItemSerial )
                        return ( ( ItemInfo )info[ i ] );

                return null;
            }

            public ItemInfo()
            {
            }

            public void WriteInfo( GenericWriter writer )
            {
                #region Save
                // Gold & Silver
                writer.Write( GoldCost );
                writer.Write( SilverCost );

                // Logs
                writer.Write( OakLogCost );
                writer.Write( PineLogCost );
                writer.Write( RedwoodLogCost );
                writer.Write( WhitePineLogCost );
                writer.Write( AshwoodLogCost );
                writer.Write( SilverBirchLogCost );
                writer.Write( YewLogCost );
                writer.Write( BlackOakLogCost );

                // Boards
                writer.Write( OakBoardCost );
                writer.Write( PineBoardCost );
                writer.Write( RedwoodBoardCost );
                writer.Write( WhitePineBoardCost );
                writer.Write( AshwoodBoardCost );
                writer.Write( SilverBirchBoardCost );
                writer.Write( YewBoardCost );
                writer.Write( BlackOakBoardCost );

                // Ingots
                writer.Write( IronIngotCost );
                writer.Write( DullCopperIngotCost );
                writer.Write( ShadowIronIngotCost );
                writer.Write( CopperIngotCost );
                writer.Write( BronzeIngotCost );
                writer.Write( GoldIngotCost );
                writer.Write( AgapiteIngotCost );
                writer.Write( VeriteIngotCost );
                writer.Write( ValoriteIngotCost );
                writer.Write( MithrilIngotCost );
                writer.Write( BloodrockIngotCost );
                writer.Write( SteelIngotCost );
                writer.Write( AdamantiteIngotCost );
                writer.Write( IthilmarIngotCost );

                // Ores
                writer.Write( IronOreCost );
                writer.Write( DullCopperOreCost );
                writer.Write( ShadowIronOreCost );
                writer.Write( CopperOreCost );
                writer.Write( BronzeOreCost );
                writer.Write( GoldOreCost );
                writer.Write( AgapiteOreCost );
                writer.Write( VeriteOreCost );
                writer.Write( ValoriteOreCost );
                writer.Write( MithrilOreCost );
                writer.Write( BloodrockOreCost );
                writer.Write( SteelOreCost );
                writer.Write( AdamantiteOreCost );
                writer.Write( IthilmarOreCost );

                // Jewels
                writer.Write( StarSapphireCost );
                writer.Write( EmeraldCost );
                writer.Write( RubyCost );
                writer.Write( CitrineCost );
                writer.Write( AmethystCost );
                writer.Write( TourmalineCost );
                writer.Write( AmberCost );
                writer.Write( DiamondCost );

                // Hides
                writer.Write( NormalHidesCost );
                writer.Write( SpinedHidesCost );
                writer.Write( BarbedHidesCost );
                writer.Write( HornedHidesCost );

                // Scales
                writer.Write( RedScalesCost );
                writer.Write( YellowScalesCost );
                writer.Write( BlackScalesCost );
                writer.Write( GreenScalesCost );
                writer.Write( WhiteScalesCost );
                writer.Write( BlueScalesCost );
                #endregion

                writer.Write( ( int )ItemSerial );
            }

            public void ReadInfo( GenericReader reader )
            {
                #region Load
                // Gold & Silver
                GoldCost = reader.ReadInt();
                SilverCost = reader.ReadInt();

                // Logs
                OakLogCost = reader.ReadInt();
                PineLogCost = reader.ReadInt();
                RedwoodLogCost = reader.ReadInt();
                WhitePineLogCost = reader.ReadInt();
                AshwoodLogCost = reader.ReadInt();
                SilverBirchLogCost = reader.ReadInt();
                YewLogCost = reader.ReadInt();
                BlackOakLogCost = reader.ReadInt();

                // Boards
                OakBoardCost = reader.ReadInt();
                PineBoardCost = reader.ReadInt();
                RedwoodBoardCost = reader.ReadInt();
                WhitePineBoardCost = reader.ReadInt();
                AshwoodBoardCost = reader.ReadInt();
                SilverBirchBoardCost = reader.ReadInt();
                YewBoardCost = reader.ReadInt();
                BlackOakBoardCost = reader.ReadInt();

                // Ingots
                IronIngotCost = reader.ReadInt();
                DullCopperIngotCost = reader.ReadInt();
                ShadowIronIngotCost = reader.ReadInt();
                CopperIngotCost = reader.ReadInt();
                BronzeIngotCost = reader.ReadInt();
                GoldIngotCost = reader.ReadInt();
                AgapiteIngotCost = reader.ReadInt();
                VeriteIngotCost = reader.ReadInt();
                ValoriteIngotCost = reader.ReadInt();
                MithrilIngotCost = reader.ReadInt();
                BloodrockIngotCost = reader.ReadInt();
                SteelIngotCost = reader.ReadInt();
                AdamantiteIngotCost = reader.ReadInt();
                IthilmarIngotCost = reader.ReadInt();

                // Ores
                IronOreCost = reader.ReadInt();
                DullCopperOreCost = reader.ReadInt();
                ShadowIronOreCost = reader.ReadInt();
                CopperOreCost = reader.ReadInt();
                BronzeOreCost = reader.ReadInt();
                GoldOreCost = reader.ReadInt();
                AgapiteOreCost = reader.ReadInt();
                VeriteOreCost = reader.ReadInt();
                ValoriteOreCost = reader.ReadInt();
                MithrilOreCost = reader.ReadInt();
                BloodrockOreCost = reader.ReadInt();
                SteelOreCost = reader.ReadInt();
                AdamantiteOreCost = reader.ReadInt();
                IthilmarOreCost = reader.ReadInt();

                // Jewels
                StarSapphireCost = reader.ReadInt();
                EmeraldCost = reader.ReadInt();
                RubyCost = reader.ReadInt();
                CitrineCost = reader.ReadInt();
                AmethystCost = reader.ReadInt();
                TourmalineCost = reader.ReadInt();
                AmberCost = reader.ReadInt();
                DiamondCost = reader.ReadInt();

                // Hides
                NormalHidesCost = reader.ReadInt();
                SpinedHidesCost = reader.ReadInt();
                BarbedHidesCost = reader.ReadInt();
                HornedHidesCost = reader.ReadInt();

                // Scales
                RedScalesCost = reader.ReadInt();
                YellowScalesCost = reader.ReadInt();
                BlackScalesCost = reader.ReadInt();
                GreenScalesCost = reader.ReadInt();
                WhiteScalesCost = reader.ReadInt();
                BlueScalesCost = reader.ReadInt();
                #endregion

                ItemSerial = (Serial)reader.ReadInt();
            }
        }

		public class TradeVendorDeed : Item
		{
			public override void OnDoubleClick( Mobile from )
			{
				if ( from.AccessLevel == AccessLevel.Player && ( BaseHouse.FindHouseAt( from ) == null || !BaseHouse.FindHouseAt( from ).CoOwners.Contains( from ) ) )
					from.SendMessage( "You must get a staff member to place this deed." );
				else
				{
					TradeVendor vendor = new TradeVendor();
					vendor.Owner = from;

					vendor.MoveToWorld( from.Location, from.Map );

					Delete();
				}
			}

			[Constructable]
			public TradeVendorDeed()
				: base( 0x14F0 )
			{
				Name = "Trade Vendor Deed";
				LootType = LootType.Blessed;
			}

			public TradeVendorDeed( Serial serial )
				: base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
			}
		}
    }
}