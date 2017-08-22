using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Engines.XmlSpawner2
{

	// this is the return message for spawner save status information
	[ Serializable ]
	public class ReturnSpawnerSaveStatus : TransferMessage
	{

		public int ProcessedMaps;
		public int ProcessedSpawners;

		public ReturnSpawnerSaveStatus( int nspawners, int nmaps )
		{
			ProcessedMaps = nmaps;
			ProcessedSpawners = nspawners;
		}

		public ReturnSpawnerSaveStatus()
		{
		}
	}

	// this message will collect and return a list of Object locations filtered by map and type
	[ Serializable ]
	public class SaveSpawnerData : TransferMessage
	{

		// this is the data that will be streamed to the XmlLoadFromStream method.
		private byte[] m_Data;
						
		public byte [] Data
		{
			get { return m_Data; }
			set { m_Data = value; }
		}

		public SaveSpawnerData(byte [] data)
		{
			Data = data;
		}

		public SaveSpawnerData()
		{
		}

		// fill the return message with a list of object locations and names
		// access can be restricted with the TransferAccess attribute

		[ TransferAccess(AccessLevel.Administrator) ]
		public override TransferMessage ProcessMessage()
		{

			// place the xml spawner info into a memory buffer
			MemoryStream mstream = new MemoryStream(Data);

			int processedmaps;
			int processedspawners;

			Mobile from = null;
			TransferServer.AuthEntry auth = TransferServer.GetAuthEntry(this);

			if(auth != null)
			{
				from = auth.User;
			}

			XmlSpawner.XmlLoadFromStream(mstream, "Spawn Editor MemStream", String.Empty, from, Point3D.Zero, Map.Internal, false, 0, false, out processedmaps, out processedspawners);

			return new ReturnSpawnerSaveStatus( processedspawners, processedmaps );
		}
	}
}