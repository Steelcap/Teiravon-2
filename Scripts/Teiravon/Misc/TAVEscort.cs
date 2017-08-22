using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Teiravon;

namespace Teiravon.Escorts
{
	
	public class TalkEntry : ContextMenuEntry
	{
		private TAVEscort m_Escort;

		public TalkEntry( TAVEscort escort ) : base( escort.TalkNumber )
		{
			m_Escort = escort;
		}

		public override void OnClick()
		{
			Mobile from = Owner.From;

			if ( from.CheckAlive() && from is PlayerMobile && m_Escort.CanTalkTo( (PlayerMobile)from ) )
				m_Escort.OnTalk( (PlayerMobile)from, true );
		}
	}

	[NoSortAttribute]
	public class TAVEscort : BaseCreature
	{
		private string m_beginspeech;
		private string m_replyspeech;
		private string m_endspeech;
		private Point3D m_destination;
		private Map m_destmap;
		public virtual int TalkNumber{ get{ return 6146; } } // Talk
		
		[Constructable]
		public TAVEscort () : base( AIType.AI_Melee, FightMode.None, 10, 1, 0.2, 0.4 )
		{

			SetStr( 90, 100 );
			SetDex( 90, 100 );
			SetInt( 15, 25 );

			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 401;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName( "male" );
			}

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 0, 25 );
			SetResistance( ResistanceType.Fire, 0, 25 );
			SetResistance( ResistanceType.Cold, 0, 25 );
			SetResistance( ResistanceType.Poison, 0, 25 );
			SetResistance( ResistanceType.Energy, 0, 25 );

			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 0;
			m_destmap = Map.Felucca;
			
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string BeginSpeech
		{
			get{ return m_beginspeech; }
			set{ m_beginspeech = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string ReplySpeech
		{
			get{ return m_replyspeech; }
			set{ m_replyspeech = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string EndSpeech
		{
			get{ return m_endspeech; }
			set{ m_endspeech = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Destination
		{
			get{return m_destination;}
			set{m_destination = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map DestinationMap
		{
			get{return m_destmap;}
			set{m_destmap = value;}
		}

		public TAVEscort( Serial serial ) : base( serial )
		{
		}

		public virtual void OnTalk( PlayerMobile player, bool contextMenu )
		{
			if (this.Controled == false)
			{
				Say(this.BeginSpeech);
				this.Controled = true;
				this.ControlMaster = player;
				this.ControlOrder = OrderType.Follow;
				this.ControlTarget = player;
			}
			else
			{
				if (this.ControlMaster == player)
				{
					if (InRange(this.Destination, 20))
					    {
					    	Say(this.EndSpeech);
					    	MessageScroll ms = new MessageScroll();
					    	ms.Addressee = player.Name;
					    	ms.Sender = this.Name;
					    	ms.Message = "has completed the escort of";
					    	ms.Name = "Escort completion scroll";
					    	player.AddToBackpack(ms);
					    	Timer deltimer = new EscDelTimer(this);
					    	deltimer.Start();
					    }
						else
						{
							Say(this.ReplySpeech);
						}
				}
				else
				{
					SayTo(player, "I already have an escort");
				}
			}
		
		}

		
		public virtual bool CanTalkTo( PlayerMobile to )
		{
			return true;
		}

		public virtual int GetAutoTalkRange( PlayerMobile m )
		{
			return -1;
		}

		public override bool CanBeDamaged()
		{
			return true;
		}

		protected Item SetHue( Item item, int hue )
		{
			item.Hue = hue;
			return item;
		}

		public override void AddCustomContextEntries( Mobile from, ArrayList list )
		{
			base.AddCustomContextEntries( from, list );

			if ( from.Alive && from is PlayerMobile && TalkNumber > 0 && CanTalkTo( (PlayerMobile)from ) )
				list.Add( new TalkEntry( this ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( m_beginspeech );
			writer.Write( m_replyspeech );
			writer.Write( (Point3D)m_destination );
			writer.Write( (Map)m_destmap );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_beginspeech = reader.ReadString();
			m_replyspeech = reader.ReadString();
			m_destination = reader.ReadPoint3D();
			m_destmap = reader.ReadMap();
		}

		private class EscDelTimer : Timer
		{
			private TAVEscort m_escort;

			public EscDelTimer( TAVEscort escrt ) : base ( TimeSpan.FromSeconds( 3.0 ) )
			{
				m_escort = escrt;
			}

			protected override void OnTick()
			{
				m_escort.Delete();
			}
		}
		
	}
}
