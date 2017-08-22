using System;
using System.IO;
using System.Xml;

namespace Server.Engines.Reports
{
	public abstract class PersistanceWriter
	{
		public abstract void SetInt32( string key, int value );
		public abstract void SetBoolean( string key, bool value );
		public abstract void SetString( string key, string value );
		public abstract void SetDateTime( string key, DateTime value );

		public abstract void BeginObject( PersistableType typeID );
		public abstract void BeginChildren();
		public abstract void FinishChildren();
		public abstract void FinishObject();

		public abstract void WriteDocument( PersistableObject root );
		public abstract void Close();

		public PersistanceWriter()
		{
		}
	}

	public sealed class XmlPersistanceWriter : PersistanceWriter
	{
		private string m_RealFilePath;
		private string m_TempFilePath;

		private StreamWriter m_Writer;
		private XmlTextWriter m_Xml;

		public XmlPersistanceWriter( string filePath )
		{
			m_RealFilePath = filePath;
			m_TempFilePath = Path.ChangeExtension( filePath, ".tmp" );

			m_Writer = new StreamWriter( m_TempFilePath );
			m_Xml = new XmlTextWriter( m_Writer );
		}

		public override void SetInt32( string key, int value )
		{
			m_Xml.WriteAttributeString( key, XmlConvert.ToString( value ) );
		}

		public override void SetBoolean( string key, bool value )
		{
			m_Xml.WriteAttributeString( key, XmlConvert.ToString( value ) );
		}

		public override void SetString( string key, string value )
		{
			if ( value != null )
				m_Xml.WriteAttributeString( key, value );
		}

		public override void SetDateTime( string key, DateTime value )
		{
			m_Xml.WriteAttributeString( key, XmlConvert.ToString( value ) );
		}

		public override void BeginObject( PersistableType typeID )
		{
			m_Xml.WriteStartElement( typeID.Name );
		}

		public override void BeginChildren()
		{
		}

		public override void FinishChildren()
		{
		}

		public override void FinishObject()
		{
			m_Xml.WriteEndElement();
		}

		public override void WriteDocument( PersistableObject root )
		{
			Console.WriteLine( "Reports: Save started" );

			m_Xml.Formatting = Formatting.Indented;
			m_Xml.IndentChar = '\t';
			m_Xml.Indentation = 1;

			m_Xml.WriteStartDocument( true );

			root.Serialize( this );

			Console.WriteLine( "Reports: Save complete" );
		}

		public override void Close()
		{
			m_Xml.Close();
			m_Writer.Close();

			try
			{
				string renamed = null;

				if ( File.Exists( m_RealFilePath ) )
				{
					renamed = Path.ChangeExtension( m_RealFilePath, ".rem" );
					File.Move( m_RealFilePath, renamed );
					File.Move( m_TempFilePath, m_RealFilePath );
					File.Delete( renamed );
				}
				else
				{
					File.Move( m_TempFilePath, m_RealFilePath );
				}
			}
			catch ( Exception ex )
			{
				Console.WriteLine( ex );
			}
		}
	}
}