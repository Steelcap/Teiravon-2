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

	// this is the return message for object data information
	[ Serializable ]
	public class ReturnSpawnerData : TransferMessage
	{
		private byte[] m_Data;

		public byte[] Data
		{
			get { return m_Data; }
			set { m_Data = value; }
		}

		public ReturnSpawnerData( byte[] stream )
		{
			m_Data = stream;
		}

		public ReturnSpawnerData()
		{
		}
	}

	// this message will collect and return a list of Object locations filtered by map and type

	public class GetSpawnerData : TransferMessage
	{

		private int m_Map;
		public int X;
		public int Y;
		public int Width;
		public int Height;
		public string NameFilter;
		public string EntryFilter;
		public short ContainerFilter;
		public short SequentialFilter;
		public short SmartSpawnFilter;
		public bool NameCase;
		public bool EntryCase;
		public short Modified;
		public short Proximity;
		public short Running;
		public DateTime ModifiedDate;
		public short SpawnTime;
		public double AvgSpawnTime;
		public string ModifiedName;
		public short ModifiedBy;
						
		public int SelectedMap
		{
			get { return m_Map; }
			set { m_Map = value; }
		}

		public GetSpawnerData()
		{
		}

		public GetSpawnerData(int map)
		{
			m_Map = map;
		}


		public GetSpawnerData(int map, int x, int y, int w, int h)
		{
			m_Map = map;
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}

		// fill the return message with a list of object locations and names
		// access can be restricted with the TransferAccess attribute

		[ TransferAccess(AccessLevel.Administrator) ]
		public override TransferMessage ProcessMessage()
		{

			ArrayList tmparray = new ArrayList();
			ArrayList itemarray = null;

			itemarray = new ArrayList(World.Items.Values);

			if(itemarray != null)
			{
				foreach(Item m in itemarray)			
				{
					if(m.Map == null || m.Map == Map.Internal) continue;
				
				
					if(m is XmlSpawner && (m.Map.MapID == SelectedMap || SelectedMap == -1))
					{
						XmlSpawner s = (XmlSpawner)m;

						bool passed = true;
						// area restricted?
						if(Width >= 0 && Height >= 0)
						{
							if(s.Location.X < X || s.Location.X > X + Width || s.Location.Y < Y || s.Location.Y > Y + Height)
							{
								passed = false;
							}
						}

						// name restricted?
						if(passed && NameFilter != null && NameFilter.Length > 0)
						{
							if(NameCase)
							{
								// case sensitive
								if(s.Name.IndexOf(NameFilter) < 0)
								{
									passed = false;
								}
							} 
							else
							{
								if(s.Name.ToLower().IndexOf(NameFilter.ToLower()) < 0)
								{
									passed = false;
								}
							}
						}

						// entry restricted?
						if(passed && EntryFilter != null && EntryFilter.Length > 0)
						{
							// search the entries of the spawner
							bool found = false;
							if(s.SpawnObjects != null)
							{
								foreach ( XmlSpawner.SpawnObject so in s.SpawnObjects)
								{
									if(EntryCase)
									{
										// case sensitive
										if(so.TypeName != null && so.TypeName.IndexOf(EntryFilter) >= 0) 
										{
											found = true;
											break;
										}
									} 
									else
									{
										if(so.TypeName != null && so.TypeName.ToLower().IndexOf(EntryFilter.ToLower()) >= 0) 
										{
											found = true;
											break;
										}
									}
								}
							}
							if(!found) passed = false;
						}
					
						// container restricted?  1=containeronly 2=nocontainers
						if(passed && ContainerFilter > 0)
						{

							if(ContainerFilter == 1 && !(s.RootParent is Container))
							{
								passed = false;
							} 
							else
								if(ContainerFilter == 2 && (s.Parent is Container))
							{
								passed = false;
							} 
						}
					
						// sequential restricted?  1=sequentialonly 2=nosequential
						if(passed && SequentialFilter > 0)
						{
							if(SequentialFilter == 1 && (s.SequentialSpawn < 0 ))
							{
								passed = false;
							} 
							else
								if(SequentialFilter == 2 && (s.SequentialSpawn >= 0))
							{
								passed = false;
							}
						}

						// smartspawn restricted?  1=smartspawnonly 2=nosmartspawn
						if(passed && SmartSpawnFilter > 0)
						{
							if(SmartSpawnFilter == 1 && !s.SmartSpawning)
							{
								passed = false;
							} 
							else
								if(SmartSpawnFilter == 2 && s.SmartSpawning)
							{
								passed = false;
							}
						}

						// modified restricted?  1=modifiedbefore 2=modifiedafter
						if(passed && Modified > 0)
						{
							if(Modified == 1 && s.LastModified > ModifiedDate)
							{
								passed = false;
							} 
							else
								if(Modified == 2 && s.LastModified < ModifiedDate)
							{
								passed = false;
							}
						}

						// proximitytrigger restricted?  1=proximitytriggered 2=not proximitytrigger
						if(passed && Proximity > 0)
						{
							if(Proximity == 1 && s.ProximityRange < 0)
							{
								passed = false;
							} 
							else
								if(Proximity == 2 && s.ProximityRange >= 0)
							{
								passed = false;
							}
						}

						// running restricted?  1=running 2=not not running
						if(passed && Running > 0)
						{
							if(Running == 1 && !s.Running)
							{
								passed = false;
							} 
							else
								if(Running == 2 && s.Running)
							{
								passed = false;
							}
						}

						// spawn time restricted?  1=less than 2=greater than
						if(passed && SpawnTime > 0)
						{
							double avgtime = (s.MinDelay.TotalMinutes + s.MaxDelay.TotalMinutes)/2.0;

							if(SpawnTime == 1 && avgtime >= AvgSpawnTime)
							{
								passed = false;
							} 
							else
								if(SpawnTime == 2 && avgtime <= AvgSpawnTime)
							{
								passed = false;
							}
						}

						
						// modifiedby restricted?  1=firstmodified 2=lastmodified 3=not firstmodified 4=not lastmodified
						if(passed && ModifiedBy > 0)
						{
							if(ModifiedBy == 1 && s.FirstModifiedBy != ModifiedName)
							{
								passed = false;
							} 
							else
								if(ModifiedBy == 2 && s.LastModifiedBy != ModifiedName)
							{
								passed = false;
							} else
							if(ModifiedBy == 3 && s.FirstModifiedBy == ModifiedName)
							{
								passed = false;
							} 
							else
								if(ModifiedBy == 4 && s.LastModifiedBy == ModifiedName)
							{
								passed = false;
							}
						}


						if(passed)
						{
							tmparray.Add(s);
						}
					}
				}
			}

			// serialize the xml spawner info into a memory buffer
			MemoryStream mstream = new MemoryStream();

			XmlSpawner.SaveSpawnList(tmparray, mstream );

			return new ReturnSpawnerData( mstream.GetBuffer() );
		}
	}
}