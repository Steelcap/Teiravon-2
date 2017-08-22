using System;
using System.Threading;
using System.Runtime;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace Server.Engines.XmlSpawner2
{

	public class TransferAccess : Attribute
	{
		private AccessLevel m_Level;

		public AccessLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		public TransferAccess( AccessLevel level)
		{
			m_Level = level;
		}
	}

	public class TransferServer
	{
		private const string m_Version = "1.05";
		public const int Port = 9030;
		private static bool Enabled = true;

		private static bool ServerActive;


		public TransferServer()
		{			
		}

		public static void Initialize()
		{
			Server.Commands.Register( "XTS", AccessLevel.Administrator, new CommandEventHandler( XTS_OnCommand ) );

			RemoteMessaging.OnReceiveMessage += new RemoteMessaging.Message(RemoteMessaging_ReceiveMessage);

			if(Enabled)
			{
				StartServerThread();
			}			
		}

		private static void StartServerThread()
		{
			//TransferServer server = new TransferServer();
			ThreadPool.QueueUserWorkItem( new WaitCallback( StartServer ) );

			StartServerTimer(TimeSpan.FromSeconds(1));
		}

		public class QueuedMessage
		{
			public TransferMessage MessageIn;
			public TransferMessage MessageOut;
			public bool Completed;
			public bool Remove;

			public QueuedMessage(TransferMessage msg)
			{
				MessageIn = msg;
				Completed = false;
			}
		}

		protected static ArrayList ServerRequests = new ArrayList();

		private static void AddRequest(TransferMessage msg)
		{
			ServerRequests.Add(new QueuedMessage(msg));
		}

		private static void RemoveRequest(TransferMessage msg)
		{

			// search the queue to see if the message is still being processed
			foreach(QueuedMessage q in ServerRequests)
			{
				if(q.MessageIn == msg )
				{
					q.Remove = true;
					break;
				}
			}			
		}

		private static bool ServerRequestProcessed(TransferMessage msg)
		{
			// search the queue to see if the message is still being processed
			foreach(QueuedMessage q in ServerRequests)
			{
				if(q.MessageIn == msg && !q.Completed) return false;
			}

			// return true if the message is no longer on the queue or it there and has been completed
			return true;
		}

		private static TransferMessage ServerRequestResult(TransferMessage msg)
		{
			// find the queue entry for the message and return the result
			foreach(QueuedMessage q in ServerRequests)
			{
				if(q.MessageIn == msg) return q.MessageOut;
			}

			return null;
		}

		private static void DefragRequests()
		{
			ArrayList removelist = new ArrayList();

			foreach(QueuedMessage q in ServerRequests)
			{
				if(q.Remove) removelist.Add(q); 
			}

			foreach(QueuedMessage q in removelist)
			{
				ServerRequests.Remove(q);
			}
		}

		private static ServerTimer m_ServerTimer;

		// set up the timer service for messages that need to be processed in the main RunUO thread for safety reasons
		public static void StartServerTimer(TimeSpan delay)
		{
			if ( m_ServerTimer != null )
				m_ServerTimer.Stop();
	
			m_ServerTimer = new ServerTimer(delay);

			m_ServerTimer.Start();
		}

		private class ServerTimer : Timer
		{

			public ServerTimer( TimeSpan delay ) : base( delay, delay )
			{
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				// check the server requests list and process anything found on it
				// find the queue entry for the message and return the result
				DefragRequests();

				foreach(QueuedMessage q in ServerRequests)
				{
					if(!q.Completed)
					{
						// otherwise just run it in the current thread
						try
						{
							// process the message
							q.MessageOut = q.MessageIn.ProcessMessage();
						} 
						catch (Exception e)
						{ 
							q.MessageOut = new ErrorMessage( String.Format("Error processing outgoing message. {0}",e.Message) );
						}
						q.Completed = true;

						// just do one per tick to minimize server blocking
						break;
					}
				}
				
				if(!Enabled)
				{
					// clear any pending requests and stop the service
					ServerRequests.Clear();
					Stop();
				}
			}
		}


		[Usage( "XTS [auth id][start][stop][list]" )]
		[Description( "Issues commands to the XmlSpawner2 Transfer Server" )]
		public static void XTS_OnCommand( CommandEventArgs e )
		{

			if(e == null || e.Arguments == null || e.Mobile == null) return;

			if( e.Arguments.Length > 0 )
			{
				// syntax is "XTS auth id"
				if(e.Arguments[0].ToLower() == "auth" && e.Arguments.Length > 1)
				{
					try
					{
						// add the authentication id to the authentication list

						Guid id = new Guid(e.Arguments[1]);

						AddAuthenticationEntry(id, e.Mobile.AccessLevel, e.Mobile);

						e.Mobile.SendMessage("Transfer Server Authentication registered.");
					} 
					catch{}
				} 
				else
					if(e.Arguments[0].ToLower() == "list" && e.Mobile != null && e.Mobile.AccessLevel >= AccessLevel.Administrator)
				{
					// list all of the current auth tickets
					e.Mobile.SendMessage("Current Authentication Tickets:");

					foreach(AuthEntry a in AuthList)
					{
						string name = null;
						if(a.User != null)
							name = a.User.Name;

						e.Mobile.SendMessage("{0} {1} {2}",name, a.Level, a.Timestamp);
					}
				} 
				else
					if(e.Arguments[0].ToLower() == "start" && e.Mobile != null && e.Mobile.AccessLevel >= AccessLevel.Administrator)
				{
					if(ServerActive)
					{
						e.Mobile.SendMessage("TransferServer is currently active");
					} 
					else
					{
						e.Mobile.SendMessage("TransferServer starting up");
						Enabled = true;
						StartServerThread();
					}
				} 
				else
					if(e.Arguments[0].ToLower() == "stop" && e.Mobile != null && e.Mobile.AccessLevel >= AccessLevel.Administrator)
				{
					e.Mobile.SendMessage("TransferServer shutting down");
					Enabled = false;
				}
			} 
			else
			{
				if(ServerActive)
				{
					e.Mobile.SendMessage("TransferServer is active");
				} 
				else
				{
					e.Mobile.SendMessage("TransferServer is not running");
				}
			}
		}


		// Registers the TransferRemote class for remote access on the server system
		private static void StartServer( object obj )
		{
			ServerActive = true;

			TcpServerChannel channel = new TcpServerChannel( "transferserver", Port );
			ChannelServices.RegisterChannel( channel );

			RemotingConfiguration.RegisterWellKnownServiceType( typeof( RemoteMessaging ), "RemoteMessaging", WellKnownObjectMode.Singleton );

			Console.WriteLine( "TransferServer version {1} listening on port {0}", Port, m_Version );

			while ( Enabled )
			{
				Thread.Sleep( 10000 );
			}
			
			
			ServerActive = false;
			ChannelServices.UnregisterChannel( channel );

			Console.WriteLine( "TransferServer on port {0} shut down.", Port );
			
			// service terminates on exit
		}

		private static ArrayList AuthList = new ArrayList();
		private static TimeSpan AuthenticationLifetime = TimeSpan.FromMinutes(30);

		public class AuthEntry
		{
			public Guid AuthenticationID;
			public DateTime Timestamp;
			public AccessLevel Level;
			public Mobile User;

			public AuthEntry(Guid id, DateTime time, AccessLevel level, Mobile user)
			{
				AuthenticationID = id;
				Timestamp = time;
				Level = level;
				User = user;
			}
		}

		public static void AddAuthenticationEntry(Guid authid, AccessLevel level, Mobile user)
		{
			AuthList.Add(new AuthEntry(authid, DateTime.Now, level, user));
		}

		public static AuthEntry GetAuthEntry(TransferMessage msg)
		{
			// go through the auth list and find the corresponding auth entry for the message
			foreach(AuthEntry a in AuthList)
			{
				// confirm authentication id match and accesslevel
				if(a.AuthenticationID == msg.AuthenticationID)
				{
					return a;
				}
			}
			return null;
		}

		private static AccessLevel GetAccessLevel(TransferMessage msg)
		{
			// default accesslevel is admin
			AccessLevel level = AccessLevel.Administrator;

			MethodInfo minfo = msg.GetType().GetMethod("ProcessMessage");
			object [] attr = minfo.GetCustomAttributes(typeof(TransferAccess), false);
			if(attr != null)
			{
				for(int i=0;i<attr.Length;i++)
				{
					if(((TransferAccess)attr[i]).Level < level)
					{
						level = ((TransferAccess)attr[i]).Level;
					}
				}
			}

			return level;
		}

		private static string Authenticate(TransferMessage msg)
		{
			if(msg == null)
			{
				// unknown error
				return "Empty message";
			}

			// check to make sure that an authentication entry for this message is on the authentication list
			ArrayList removelist = new ArrayList();

			// default no authentication status
			string errorstatus = "Renew your Session Authentication";

			foreach(AuthEntry a in AuthList)
			{
				// check for entry expiration
				if(a.Timestamp < DateTime.Now - AuthenticationLifetime)
				{
					removelist.Add(a);
					continue;
				}

				// confirm authentication id match and accesslevel
				if(a.AuthenticationID == msg.AuthenticationID)
				{

					// confirm required accesslevel on the msg is below the access level of the auth entry					
					if(a.Level < GetAccessLevel(msg))
					{
						// authenticated but insufficient access
						errorstatus = "Insufficient Access Level";
					} 
					else
					{
						// authenticated and access confirmed
						errorstatus = null;
					}
				}				
			}

			// clean up the list
			foreach(AuthEntry a in removelist)
			{
				AuthList.Remove(a);
			}

			return errorstatus;
		}

		
		// process incoming messages
		public static byte[] RemoteMessaging_ReceiveMessage( string typeName, byte[] data, out string answerType )
		{

			answerType = null;

			TransferMessage inMsg = null;
			TransferMessage outMsg = null;

			Type type = Type.GetType( typeName );

			if ( type != null )
			{

				inMsg = TransferMessage.Decompress( data, type );

				if ( inMsg != null )
				{
					// check message authentication
					string authstatus = Authenticate(inMsg);

					if(authstatus != null)
					{
						outMsg = new ErrorMessage( String.Format("Message request refused. {0}", authstatus) );
					} 
					else
					{

						// if the message has been tagged for execution in the RunUO server thread
						// then queue it up and wait for the response
						if(inMsg.UseMainThread)
						{
							AddRequest(inMsg);
							while(!ServerRequestProcessed(inMsg))
							{
								Thread.Sleep( 100 );
							}
							outMsg = ServerRequestResult(inMsg);
							RemoveRequest(inMsg);
						} 
						else
						{

							// otherwise just run it in the current thread
							try
							{
								// process the message
								outMsg = inMsg.ProcessMessage();
							} 
							catch (Exception e)
							{ 
								outMsg = new ErrorMessage( String.Format("Error processing outgoing message. {0}",e.Message) );
							}
						}
					}

				}
				else
				{
					outMsg = new ErrorMessage( "Error decompressing incoming message. No zero arg msg constructor? DEBUG: {0} {1}",typeName,data.Length.ToString() );
				}
			}
			else
			{
				outMsg = null;
			}

			if ( outMsg != null )
			{
				answerType = outMsg.GetType().FullName;
				byte[] result = outMsg.Compress();
				
				return result;
			}
			else
			{
				answerType = null;
				return null;
			}
		}
	}
}
