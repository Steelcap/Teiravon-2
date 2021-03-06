using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Spells;

namespace Server.Items
{

	public interface IDrowEquip
	{
		int DrowHits{ get; set; }
	}
	
	
	public class DrowRobe : BaseOuterTorso
	{
		bool decay;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RobeDecay
		{
			get { return decay; }
			set { decay = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan DecaysIn
		{
			get { return m_DecayTime - DateTime.Now; }
		}

		[Constructable]
		public DrowRobe() : base( 12217 )
		{
			Name = "Shroud of Darkness";
			Hue = 612;

			decay = true;
			m_DecayTime = DateTime.Now + TimeSpan.FromMinutes( 12000.0 );
			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		private class InternalTimer : Timer
		{
			private DrowRobe m_Item;

			public InternalTimer( DrowRobe item, DateTime end ) : base( end - DateTime.Now )
			{
				m_Item = item;
                Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				if (m_Item != null)
				{
					if ( m_Item.RobeDecay )
					{
						m_Item.PublicOverheadMessage( Network.MessageType.Regular, 438, false, "The shroud of darkness seems to vanish." );
						m_Item.Delete();
					}
				}
			}
		}

		public DrowRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( decay );

			writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			decay = reader.ReadBool();

            m_DecayTime = reader.ReadDeltaTime();

			if ( decay )
			{
				m_Timer = new InternalTimer( this, m_DecayTime );
				m_Timer.Start();
			}
			
			if (ItemID == 9708)
				ItemID = 12217;
		}
	}


	public class Drowcloak : BaseCloak, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion
		
		
		private bool m_insignia;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Insignia
		{
			get{ return m_insignia; }
			set{ m_insignia = value; }
		}

			[Constructable]
			public Drowcloak() : base( 0x1515 )
			{
				Name = "Piwafwi";
				Hue = 0x455;
				SkillBonuses.SetValues( 0, SkillName.Stealth, 10.0 );
				Weight = 17.0;
				Resistances.Physical = 5;
				Resistances.Energy = 3;
				Resistances.Cold = 3;
				Resistances.Fire = 3;
				Resistances.Poison = 3;
				Insignia = false;
				DrowHits = Utility.RandomMinMax(200,500);
			}

			public Drowcloak( Serial serial ) : base( serial )
			{
			}
			
			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}
			

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 );
				writer.Write( (int) m_DrowHits);
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
				m_DrowHits = reader.ReadInt();
			}
		}


	[FlipableAttribute( 11552, 11564 )]
	public class Drowdagger : BaseSword, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion
		
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 57; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 3; } }
		public override int OldMaxDamage{ get{ return 28; } }
		public override int OldSpeed{ get{ return 53; } }

		public override int DefHitSound{ get{ return 0x23C; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }
	
		
		[Constructable]
		public Drowdagger() : base( 11552 )
		{
			Name = "Drow Dagger";
			Weight = 2.0;
			Layer = Layer.OneHanded;
			DrowHits = Utility.RandomMinMax(200,500);
		}
		
		public Drowdagger( Serial serial ) : base( serial )
		{
			
		}
		
			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
			
			if (ItemID == 3922)
				ItemID = 11552;
		}
	}

	public class Drowmagering : GoldRing, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		[Constructable]
		public Drowmagering()
		{
			Name = "Ring of Sorcere";
			Attributes.LowerManaCost = 5;
			Attributes.CastSpeed = 1;
			Hue = 0x455;
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public Drowmagering( Serial serial ) : base( serial )
		{
		}

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_DrowHits);
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}

	
	public class Drowwarring : GoldRing, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		[Constructable]
		public Drowwarring()
		{
			Name = "Ring of Melee-Magthere";
			Attributes.DefendChance = 5;
			Attributes.AttackChance = 5;
			Hue = 0x455;
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public Drowwarring( Serial serial ) : base( serial )
		{
		}

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_DrowHits);
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}

	
	public class Drowpriestring : GoldRing, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		[Constructable]
		public Drowpriestring()
		{
			Name = "Ring of Arach-Tinilith";
			Attributes.LowerManaCost = 5;
			Attributes.RegenMana = 1;
			Hue = 0x455;
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public Drowpriestring( Serial serial ) : base( serial )
		{
		}

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_DrowHits);
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}

	public class Drowsword : BaseSword, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 15; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 44; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 4; } }
		public override int OldMaxDamage{ get{ return 30; } }
		public override int OldSpeed{ get{ return 43; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }
	
		
		[Constructable]
		public Drowsword() : base( 0x13B6 )
		{
			Name = "Drow Scimitar";
			Weight = 5.0;
			WeaponAttributes.HitLeechHits = 15;
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public Drowsword( Serial serial ) : base( serial )
		{
			
		}

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}


	public class Drowboots : Boots, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion
	
		[Constructable]
		public Drowboots() : base( 0x170B )
		{
			Name = "Padded Boots";
			SkillBonuses.SetValues( 0, SkillName.Stealth, 10.0 );
			DrowHits = Utility.RandomMinMax(200,500);
		}
	
		public Drowboots( Serial serial ) : base( serial )
		{

		}
		

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}


	public class Drowmagerobe : Robe, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		new public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		[Constructable]
		public Drowmagerobe() : base( 0x25EC )
		{
			Name = "Spider Silk Mage Robe";
			Hue = 0x455;
			SkillBonuses.SetValues( 0, SkillName.Stealth, 2.0 );
			SkillBonuses.SetValues( 0, SkillName.MagicResist, 5.0 );
				Resistances.Physical = 3;
				Resistances.Energy = 5;
				Resistances.Cold = 5;
				Resistances.Fire = 5;
				Resistances.Poison = 5;
			DrowHits = Utility.RandomMinMax(200,500);
		}
		public Drowmagerobe( Serial serial ) : base( serial )
		{
			
		}
		

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}


	public class Drowhandbow : BaseRanged, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( Bolt ); } }
		public override Item Ammo{ get{ return new Bolt(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 25; } }
		public override int AosMinDamage{ get{ return 10; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 8; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }
		
		[Constructable]
		public Drowhandbow() : base( 0xF50 )
		{
			Name = "Drow Hand Crossbow";
			Hue = 0x455;
			Weight = 7.0;
			Layer = Layer.OneHanded;
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public Drowhandbow( Serial serial ) : base( serial )
		{
			
		}
		

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}


	public class Drowxbow : BaseRanged, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( Bolt ); } }
		public override Item Ammo{ get{ return new Bolt(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 16; } }
		public override int AosMaxDamage{ get{ return 19; } }
		public override int AosSpeed{ get{ return 29; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		
		[Constructable]
		public Drowxbow() : base( 0xF50 )
		{
			Name = "Drow Crossbow";
			Hue = 0x455;
			Weight = 7.0;
			Layer = Layer.TwoHanded;
			SkillBonuses.SetValues( 0, SkillName.Archery, 10.0 );
			DrowHits = Utility.RandomMinMax(200,500);
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = fire = nrgy = 0;
			cold = 50; pois = 50;
		}
		
		public Drowxbow( Serial serial ) : base( serial )
		{
			
		}
		

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}

	public class Drowhouseinsig : Item
	{
	
		[Constructable]
		public Drowhouseinsig() : base( 0xF26 )
		{
			Name = "House Insignia";
			Weight = 0.1;
		}
		public Drowhouseinsig( Serial serial ) : base( serial )
		{
			
		}
		
		public override void OnDoubleClick( Mobile src )
		{
			if ( IsChildOf( src.Backpack ) )
			{
				src.SendMessage("Target the cloak to pin the house insignia to.");
				src.Target = new InsigTarget( this, (TeiravonMobile)src);
			}
			else
			{
				src.SendLocalizedMessage( 1042001 ); // That must be in yourpack for you to use it.
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x13bf, 0x13c4 )]
	public class DrowChainChest : BaseArmor, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 75; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -5; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public DrowChainChest() : base( 0x13BF )
		{
			Weight = 5.0;
			Name = "Spidermail Tunic";
			SkillBonuses.SetValues( 0, SkillName.Stealth, 10.0 );
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public DrowChainChest( Serial serial ) : base( serial )
		{
		}
		
			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_DrowHits);
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}	

	[FlipableAttribute( 0x13be, 0x13c3 )]
	public class DrowChainLegs : BaseArmor, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 75; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -5; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public DrowChainLegs() : base( 0x13BE )
		{
			Weight = 5.0;
			Name = "Spidermail Legs";
			SkillBonuses.SetValues( 0, SkillName.Stealth, 10.0 );
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public DrowChainLegs( Serial serial ) : base( serial )
		{
		}
		
			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_DrowHits);
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}	
	
	[FlipableAttribute( 0x13BB, 0x13C0 )]
	public class DrowChainCoif : BaseArmor, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion

		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 75; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -5; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public DrowChainCoif() : base( 0x13BB )
		{
			Weight = 1.0;
			Name = "Spidermail Coif";
			SkillBonuses.SetValues( 0, SkillName.Stealth, 10.0 );
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public DrowChainCoif( Serial serial ) : base( serial )
		{
		}

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}

	public class DrowWine : BeverageBottle
	{

		[Constructable]
		public DrowWine(): base( BeverageType.Wine)
		{
			Weight = 1;
			Name = "Black Widow Wine";
			Hue = 0x966;
		}

		public DrowWine( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class DrowAle : BeverageBottle
	{
		[Constructable]
		public DrowAle(): base( BeverageType.Ale)
		{
			Weight = 1;
			Name = "Arachnid Ale";
			Hue = 0x96D;
		}

		public DrowAle( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DrowWebPotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.DrowWeb; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 10 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public DrowWebPotion() : base( PotionEffect.DrowWeb )
		{
			Name = "Essence of Spider Web";
			Hue = 656;
		}

		public DrowWebPotion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Drink( Mobile from )
		{
			TeiravonMobile m_Player = (TeiravonMobile) from;

			if (!this.TargetDrink)
			{
				if ( m_Player.CanDrink( PEffect ) )
				{
					m_Player.Target = new WebPotionTarget( m_Player, this);
				}
			}
			else
				base.Drink( from );
		}
	}

	public class DrowPoisonImmunityPotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.DrowPoisonImmune; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 10 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public DrowPoisonImmunityPotion() : base( PotionEffect.DrowPoisonImmune )
		{
			Name = "Potion of Poison Immunity";
			Hue = 51;
		}

		public DrowPoisonImmunityPotion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Drink( Mobile from )
		{
			TeiravonMobile m_Player = (TeiravonMobile) from;

			PoisonImmuneContext context = GetContext( from );

			if ( context != null )
			{
				return;
			}

			from.PlaySound( 0x3B );

			Timer timer = new PoisonImmuneTimer( from );
			timer.Start();

			AddContext( from, new PoisonImmuneContext( timer ) );

			base.Drink( from );
		}
		
		private static Hashtable m_Table = new Hashtable();

		private static void AddContext( Mobile m, PoisonImmuneContext context )
		{
			m_Table[m] = context;
		}

		public static void RemoveContext( Mobile m )
		{
			PoisonImmuneContext context = GetContext( m );

			if ( context != null )
				RemoveContext( m, context );
		}

		private static void RemoveContext( Mobile m, PoisonImmuneContext context )
		{
			m_Table.Remove( m );

			context.Timer.Stop();
		}

		private static PoisonImmuneContext GetContext( Mobile m )
		{
			return ( m_Table[m] as PoisonImmuneContext );
		}

		public static bool UnderEffect( Mobile m )
		{
			return ( GetContext( m ) != null );
		}

		private class PoisonImmuneTimer : Timer
		{
			private Mobile m_Mobile;

			public PoisonImmuneTimer( Mobile from ) : base ( TimeSpan.FromMinutes( 5.0 ) )
			{
				m_Mobile = from;
			}

			protected override void OnTick()
			{
				if ( !m_Mobile.Deleted )
				{
					m_Mobile.SendMessage( "* You feel the effects of your poison resistance wearing off *" );
				}

				RemoveContext( m_Mobile );
			}
		}

		private class PoisonImmuneContext
		{
			private Timer m_Timer;

			public Timer Timer{ get{ return m_Timer; } }

			public PoisonImmuneContext( Timer timer )
			{
				m_Timer = timer;
			}
		}
	}

	[FlipableAttribute( 0x26BD, 0x26C7 )]
	public class DrowBladedStaff : BaseStaff, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion
		
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 16; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 44; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 14; } }
		public override int OldMaxDamage{ get{ return 16; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 21; } }
		public override int InitMaxHits{ get{ return 110; } }

		public override SkillName DefSkill{ get{ return SkillName.Swords; } }

		[Constructable]
		public DrowBladedStaff() : base( 0x26BD )
		{
			Weight = 4.0;
			Name = "Drow Bladed Staff";
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public DrowBladedStaff( Serial serial ) : base( serial )
		{
		}

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_DrowHits = reader.ReadInt();
		}
	}

    [FlipableAttribute(0x26BF, 0x26C9)]
	public class DrowDoubleBladedStaff : BaseSpear, IDrowEquip
	{
		#region Drow Impl
		private int m_DrowHits;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrowHits
		{
			get{ return m_DrowHits; }
			set{ m_DrowHits = value; InvalidateProperties(); Update();}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

				list.Add( 1060741, "{0}", m_DrowHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

				LabelTo( from, 1060741, String.Format( "{0}", m_DrowHits ) );
		}
		
		public void Update()
		{
			if ( DrowHits < 1 )
			{
				if (this.Parent is TeiravonMobile)
				{
					TeiravonMobile mob = (TeiravonMobile)this.Parent;
					mob.SunlightDamage.Stop();
					mob.SunlightDamage = null;
					mob.SendMessage("The sunlight destroys your {0}", Name);
					this.Delete();
				}
			}
		}
		

		#endregion
		
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 55; } }

		public override int OldStrengthReq{ get{ return 50; } }
		public override int OldMinDamage{ get{ return 12; } }
		public override int OldMaxDamage{ get{ return 13; } }
		public override int OldSpeed{ get{ return 49; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
        public DrowDoubleBladedStaff()
            : base(0x26BF)
		{
			Weight = 3.0;
			Name = "Drow Double Bladed Staff";
			DrowHits = Utility.RandomMinMax(200,500);
		}

		public DrowDoubleBladedStaff( Serial serial ) : base( serial )
		{
		}

			public override void OnAdded( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = true;
				}
				base.OnAdded( parent );
			}
			
			public override void OnRemoved( object parent )
			{
				if (parent is TeiravonMobile)
				{
					TeiravonMobile player = (TeiravonMobile)parent;
					player.PoisonShotReady = false;
					foreach (Item item in player.Items)
					{
						if (item.Layer != Layer.Bank && item.Layer != Layer.Backpack)
						{
							if (item is IDrowEquip)
								player.PoisonShotReady = true;
						}
					}
				}
				base.OnRemoved( parent );
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( (int) m_DrowHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if (version < 1 && ItemID == 0x26BD)
                ItemID = 0x26BF;
			m_DrowHits = reader.ReadInt();
		}
	}
	
}

namespace Server.Targets
{
	
	public class InsigTarget : Target
	{
		private TeiravonMobile m_Player;
		private Drowhouseinsig m_dsig1;
		private Drowcloak m_mdcloak;
			
		public InsigTarget(Item m_dsig, TeiravonMobile from): base( -1, false, TargetFlags.None )
		{
			m_Player = (TeiravonMobile)from; 
			m_dsig1 = (Drowhouseinsig) m_dsig;
		}

		protected override void OnTarget( Mobile from, object targ )  
		{  
			if (targ is Drowcloak )  
			{
				m_mdcloak = (Drowcloak)targ;

				if (!(m_mdcloak.Insignia))
				{ 
					m_mdcloak.SkillBonuses.SetValues(1,SkillName.Hiding,25); 
					m_mdcloak.Name = "House Piwafwi";  
					m_mdcloak.Insignia = true; 
					m_dsig1.Delete();
				} 
				else 
					from.SendMessage("This is already a House Piwafwi."); 
			} 
			else 
				from.SendMessage("That can only be used on a Piwafwi."); 
		}  
	}

	public class WebPotionTarget : Target
	{
		private TeiravonMobile m_Player;
		private Map map;
		private DrowWebPotion wpot;
			
		public WebPotionTarget(TeiravonMobile from, DrowWebPotion pot): base( -1, false, TargetFlags.None )
		{
			m_Player = (TeiravonMobile)from; 
			map = m_Player.Map;
			wpot = pot;
		}

		protected override void OnTarget( Mobile from, object targ )  
		{
			IPoint3D p = targ as IPoint3D;

			if ( p != null )
			{
				if ( p is Item )
					p = ((Item)p).GetWorldTop();
				else if ( p is Mobile )
					p = ((Mobile)p).Location;
				else
					SpellHelper.GetSurfaceTop( ref p );
				
				Point3D p3 = new Point3D(p);
	
				foreach(Mobile m in map.GetMobilesInRange(p3, 3))
				{
					int duration = 10 - (int)(m.Str/25);
					if (duration < 1)
						duration = 1;
					m.Paralyze(TimeSpan.FromSeconds(duration));
					m.SendMessage("You are engulfed in spider webs!");
					wpot.TargetDrink = true;
					wpot.Drink(from);
				}
			}
		}
	}

}
