using System;
using Server;
using Server.Items;
using System.Collections;
using Server.ContextMenus;
using Server.Mobiles;


namespace Addons
{
	public class BallistaEastAddon : BaseAddon
	{
		private BaseWeapon m_controller;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public BaseWeapon Controller
		{
			get{ return m_controller; }
			set{ m_controller = value; InvalidateProperties(); }
		}
		
		public override BaseAddonDeed Deed
		{
			get{return new BallistaEastAddonDeed();}
		}

		[ Constructable ]
		public BallistaEastAddon()
		{
			AddComponent( new AddonComponent( 16227 ), 1, -1, 0 );
			AddComponent( new AddonComponent( 16191 ), 2, 1, 0 );
			AddComponent( new AddonComponent( 16228 ), 2, 0, 0 );
			AddComponent( new AddonComponent( 16214 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 16226 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 16225 ), 1, 1, 0 );
			AddComponent( new AddonComponent( 16196 ), 2, -1, 0 );
			AddComponent( new AddonComponent( 16208 ), -2, 0, 0 );
			AddComponent( new AddonComponent( 16212 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 16210 ), -1, 1, 0 );
			AddComponent( new AddonComponent( 16216 ), 0, 2, 0 );
			AddComponent( new AddonComponent( 16218 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 16220 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 16222 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 16224 ), 0, -2, 0 );
		}

		public BallistaEastAddon( Serial serial ) : base( serial )
		{
		}
		
		public override void OnChop(Mobile from)
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile player = (TeiravonMobile)from;
				Item[] wep = player.Backpack.FindItemsByType(typeof(DwarvenBallista), true);
				Item wepn = null;

				for (int i=0; i < wep.Length; i++)
				{
						wepn = wep[i];
				}

				if (wepn != null)
				{
					Timer timer = new TakeDownTimer(from, this, wepn);
					timer.Start();
				}
				else 
				{
					Timer timer = new DestroyTimer(from, this);
					timer.Start();
				}
			}
		}
		
		private class TakeDownTimer : Timer
		{
			private Mobile m_Mobile;
			private BallistaEastAddon m_ballista;
			private DwarvenBallista wpn;

			public TakeDownTimer( Mobile from, BallistaEastAddon ballista, Item wep ) : base ( TimeSpan.FromSeconds( 60.0 ) )
			{
				m_Mobile = from;
				m_ballista = ballista;
				wpn = (DwarvenBallista)wep;
				m_Mobile.Frozen = true;
				m_Mobile.SendMessage("You begin disassembling the ballista.");
			}

			protected override void OnTick()
			{
				m_Mobile.Frozen = false;
				if (m_Mobile.Alive)
				{
					m_ballista.Delete();
					wpn.Delete();
					if (m_Mobile is TeiravonMobile)
					{
						TeiravonMobile player = (TeiravonMobile)m_Mobile;
						if (player.Backpack != null)
							player.AddToBackpack(new BallistaEastAddonDeed());
					}
				}
			}
		}

		private class DestroyTimer : Timer
		{
			private Mobile m_Mobile;
			private BaseWeapon weapon;
			private BallistaEastAddon m_ballista;

			public DestroyTimer( Mobile from, BallistaEastAddon ballista ) : base ( TimeSpan.FromSeconds( 60.0 ) )
			{
				m_Mobile = from;
				weapon = (BaseWeapon)ballista.Controller;
				m_ballista = ballista;
				m_Mobile.Frozen = true;
				m_Mobile.SendMessage("You begin working to destroy the ballista!");
				m_Mobile.Emote("{0} begins destroying the ballista", m_Mobile.Name);
			}

			protected override void OnTick()
			{
				m_Mobile.Frozen = false;
				if (m_Mobile.Alive)
				{
					
					if (m_ballista != null)
						m_ballista.Delete();
					if (weapon != null)
						weapon.Delete();
				}
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
			writer.Write( (BaseWeapon)m_controller);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_controller = (BaseWeapon)reader.ReadItem();
		}
	}

	public class BallistaEastAddonDeed : BaseAddonDeed
	{
		private bool dclicked = false;
		
		public override BaseAddon Addon
		{
			get{return new BallistaEastAddon();}
		}

		[Constructable]
		public BallistaEastAddonDeed()
		{
			Name = "Ballista";
		}

		public BallistaEastAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (!(dclicked))
			{
				from.Frozen = true;
				Timer timer = new BuildTimer(from, this);
				timer.Start();
			}
			else
				base.OnDoubleClick(from);
		}
		
		private class BuildTimer : Timer
		{
			private Mobile m_Mobile;
			private BallistaEastAddonDeed m_deed;

			public BuildTimer( Mobile from, BallistaEastAddonDeed deed ) : base ( TimeSpan.FromSeconds( 60.0 ) )
			{
				m_Mobile = from;
				m_deed = deed;
				m_Mobile.SendMessage("You begin assembling the ballista.");
				m_Mobile.Emote("{0} begins assembling a ballista", m_Mobile.Name);
				m_Mobile.Frozen = false;
			}

			protected override void OnTick()
			{
				m_deed.dclicked = true;
				m_deed.OnDoubleClick(m_Mobile);
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
