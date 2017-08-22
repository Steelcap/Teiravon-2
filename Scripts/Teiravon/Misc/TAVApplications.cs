using System;
using System.Collections;
using System.Xml;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Server;
using Server.Misc;
using Server.Network;

namespace Server.Gumps
{
	public class TeirApplicationGump : Gump
	{
		public TeirApplicationGump() : base( 10, 10 )
		{
/*			AddPage( 0 );

			AddBackground( 0, 0, 750, 400, 2600 );


			 Application Listing
			AddPage( 1 );

			OleDbConnection m_Database = new OleDbConnection( "Provider=Microsoft.Jet.OLEDB.4.0; data source=C:\\RunUO\\Applications\\Applications.mdb" );
			OleDbCommand m_SearchString = new OleDbCommand( "SELECT * FROM Applications", m_Database );

			m_Database.Open();

			OleDbDataReader m_ApplicationList = m_SearchString.ExecuteReader();

			int y = 50;

			while( m_ApplicationList.Read() )
			{
				AddLabel( 20, y, 150, m_ApplicationList["submiton"].ToString() );
				AddLabel( 100, y += 20, 150, m_ApplicationList["username"].ToString() );
				AddLabel( 250, y, 150, m_ApplicationList["firstname"].ToString() );
				AddLabel( 400, y, 150, m_ApplicationList["lastname"].ToString() );
				AddLabel( 550, y, 150, m_ApplicationList["email"].ToString() );

				y += 20;
			}

			m_ApplicationList.Close();
			m_Database.Close();
*/
		}
	}
}