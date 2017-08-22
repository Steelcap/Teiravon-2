using System;
using System.Collections;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class GoldSpawner : MetalGoldenChest
    {
        private int m_Gold = 0;
        private ArrayList m_Spawners = new ArrayList();

        [CommandProperty( AccessLevel.GameMaster )]
        public int Gold { get { return m_Gold; } set { m_Gold = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public Item LinkedSpawner
        {
            get { return null; }
            set
            {
                if ( !( value is Spawner ) )
                    return;

                if ( m_Spawners.Contains( value ) )
                {
                    PublicOverheadMessage( Server.Network.MessageType.Regular, 0, true, "Spawner Removed." );
                    ( ( Spawner )value ).GoldSpawner = null;
                    m_Spawners.Remove( value );
                }
                else
                {
                    PublicOverheadMessage( Server.Network.MessageType.Regular, 0, true, "Spawner Added." );
                    ( ( Spawner )value ).GoldSpawner = this;
                    m_Spawners.Add( value );
                }
            }
        }

        public override bool OnDragDrop( Mobile from, Item item )
        {
            if ( !( item is Gold ) || m_Spawners == null )
                return false;

            return base.OnDragDrop( from, item );
        }

        /*
         * 
            Gold g = FindItemByType( typeof( Gold ) ) as Gold;
            int amt = ( g == null ) ? 0 : g.Amount;
            int remainder = amt + item.Amount - m_Gold;

            if ( remainder < m_Gold )
                return base.OnDragDrop( from, item );

            if ( amt + item.Amount >= m_Gold )
            {
                if ( remainder > 0 )
                    item.Amount = remainder;
                else
                    item.Delete();

                if ( g != null )
                    g.Delete();
                
                DoSpawn( true );
            }

         */

        public bool DoSpawn()
        {
            bool goahead = false;

            if ( ConsumeTotal( typeof( Gold ), m_Gold, true ) )
                goahead = true;

            return goahead;
        }

        public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
        {
            return OnDragDrop( from, item );
        }

        [Constructable]
        public GoldSpawner()
        {
            Movable = false;
        }

        public GoldSpawner( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( ( int )m_Gold );
            writer.WriteItemList( m_Spawners, true );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            m_Gold = reader.ReadInt();
            m_Spawners = reader.ReadItemList();
        }
    }
}




