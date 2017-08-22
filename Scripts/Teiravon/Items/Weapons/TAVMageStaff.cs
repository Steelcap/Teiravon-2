using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;

namespace Server.Items
{
	[FlipableAttribute( 0xE89, 0xE8a )]
	public class MageStaff : BaseStaff
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 48; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 28; } }
		public override int OldSpeed{ get{ return 48; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }
		
		private int m_GemsUsed;
		private int m_MageType;

		[Constructable]
		public MageStaff() : base( 0xE89 )
		{
			Weight = 4.0;
			this.Attributes.SpellChanneling = 1;
			this.WeaponAttributes.SelfRepair = 5;
			this.Name = "Mage Staff";
			this.MaxHits = 50;
			this.Hits = 50;
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int GemsUsed
		{
			get{ return m_GemsUsed; }
			set{ m_GemsUsed = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int MageType
		{
			get{ return m_MageType; }
			set{ m_MageType = value; }
		}

		public override bool CanEquip( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( !m_Player.IsMage() )
				{
					from.SendMessage( "Only mages can use that.");
					return false;
				}
			}

			return base.CanEquip( from );
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{

				if (this.MageType == 1)
				{
					cold = ((int)this.GemsUsed) * 25;
					phys = 100 - (((int)this.GemsUsed) * 25);
					pois = fire = nrgy = 0;
				}
				else if (this.MageType == 2)
				{
					nrgy = ((int)this.GemsUsed) * 25;
					phys = 100 - (((int)this.GemsUsed) * 25);
					pois = fire = cold = 0;
				}
				else if (this.MageType == 3)
				{
					fire = ((int)this.GemsUsed) * 25;
					phys = 100 - (((int)this.GemsUsed) * 25);
					pois = cold = nrgy = 0;
				}
				else if (this.MageType == 4)
				{
					pois = ((int)this.GemsUsed) * 25;
					phys = 100 - (((int)this.GemsUsed) * 25);
					fire = cold = nrgy = 0;
				}
				else
				{
					phys = 100;
					cold = fire = nrgy = pois = 0;
				}
		}
		
		public MageStaff( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_GemsUsed);
			writer.Write( (int) m_MageType);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_GemsUsed = reader.ReadInt();
			m_MageType = reader.ReadInt();
		}
	}
	
	
	
	public class MageStaffGem : Item
	{
		[Constructable]
		public MageStaffGem() : base( 0x186E )
		{
			Weight = 1.0;
			this.Name = "Mage Staff Gem";
			this.Hue = 2213;
		}

		public MageStaffGem( Serial serial ) : base( serial )
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
		
		public override void OnDoubleClick( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( !m_Player.IsMage() )
				{
					from.SendMessage( "Only mages can use that.");
				}
				else
				{
					from.Target = new MageGemTarget( this, m_Player );					
				}
			}
		}
	}
}

namespace Server.Targets
{

	public class MageGemTarget : Target
	{
		private TeiravonMobile m_Player;
		private MageStaffGem m_MGem;
		private int MageClass;
		private MageStaff m_MStaff;
		
		public MageGemTarget( Item mgem , TeiravonMobile from) : base( -1, false, TargetFlags.None )
		{
			m_Player = from;
			m_MGem = (MageStaffGem)mgem;
			if (from.IsAquamancer())
				MageClass = 1;
			else if (from.IsAeromancer())
				MageClass = 2;
			else if (from.IsPyromancer())
				MageClass = 3;
			else if (from.IsNecromancer())
				MageClass = 4;
			else if (from.IsGeomancer())
				MageClass = 5;
		}

		protected override void OnTarget( Mobile from, object targ )
		{
			if (targ is MageStaff)
			{
				m_MStaff = (MageStaff)targ;
				if (m_MStaff.GemsUsed > 3)
				{
					from.SendMessage( "This Mage Staff is fully advanced.");
				}
				else
				{
					m_MStaff.GemsUsed = m_MStaff.GemsUsed + 1;
				
					if (m_MStaff.GemsUsed > 0)
					{
						m_MStaff.Name = "Mage Staff of " + from.Name ;
						m_MStaff.MageType = MageClass;
					}
				
					switch (MageClass)
					{
						case 1: // Aquamancer
							if (m_MStaff.GemsUsed == 4)
								m_MStaff.Hue = 98;
							break;
						case 2: // Aeromancer
							if (m_MStaff.GemsUsed == 4)
								m_MStaff.Hue = 18;
							break;
						case 3: // Pyromancer
							if (m_MStaff.GemsUsed == 4)
								m_MStaff.Hue = 43;
							break;
						case 4: // Necromancer
							if (m_MStaff.GemsUsed == 4)
								m_MStaff.Hue = 477;
							break;
						case 5: // Geomancer
							m_MStaff.Attributes.WeaponDamage = m_MStaff.Attributes.WeaponDamage + 20;
							if (m_MStaff.GemsUsed == 4)
								m_MStaff.Hue = 954;
							break;
					}

					if (m_MStaff.GemsUsed < 4)
						m_MStaff.Hue = 0;
					
					from.SendGump( new MSGemGump( from, m_MGem, m_MStaff ) );
					
					m_MGem.Delete();
					
				}
			}
			else
			{
				from.SendMessage( "This can only be used on a Mage Staff.");
			}
		}
		
		
		
	}
}

namespace Server.Gumps
{
	public class MSGemGump : Gump
	{

		TeiravonMobile m_Player;
		MageStaffGem gem;
		MageStaff staff;

		public MSGemGump( Mobile from, Item msgem, MageStaff mstaff ) : base( 0, 0 )
		{
		m_Player = (TeiravonMobile) from;
		gem  = (MageStaffGem) msgem;
		staff = (MageStaff) mstaff;
		
		m_Player.CloseGump( typeof( MSGemGump ) );

		int x = 140;
		int y = 170;
		int x2 = 116;
		int y2 = 173;
		int i = 1;

		this.Closable=false;
		this.Disposable=false;
		this.Dragable=true;
		this.Resizable=false;

		AddPage( 0 );
		AddBackground( 70, 80, 350, 325, 3600 );
		AddBackground( 95, 125, 300, 255, 9350 );

		AddHtml( 200, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Mage Staff Gem</basefont>", false, false );
		AddLabel( 155, 135, 150, "Choose your Mage Staff Power:" );

		AddLabel( x, y+(20*i), 150, "Mana Reneration" );
		AddButton( x2, y2+(20*i), 2224, 2224, 1, GumpButtonType.Reply, 0 );
		i ++;
		AddLabel( x, y+(20*i), 150, "Increased Spell Damage" );
		AddButton( x2, y2+(20*i), 2224, 2224, 2, GumpButtonType.Reply, 0 );
		i ++;
		if (staff.WeaponAttributes.MageWeapon < 30)
		{
			AddLabel( x, y+(20*i), 150, "Mage Weapon" );
			AddButton( x2, y2+(20*i), 2224, 2224, 3, GumpButtonType.Reply, 0 );
			i ++;
		}
		AddLabel( x, y+(20*i), 150, "Faster Casting" );
		AddButton( x2, y2+(20*i), 2224, 2224, 4, GumpButtonType.Reply, 0 );
		i ++;
		AddLabel( x, y+(20*i), 150, "Faster Cast Recovery" );
		AddButton( x2, y2+(20*i), 2224, 2224, 5, GumpButtonType.Reply, 0 );
		i ++;
		if (m_Player.IsAquamancer())
		{
			AddLabel( x, y+(20*i), 150, "Hit Harm" );
			AddButton( x2, y2+(20*i), 2224, 2224, 6, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Hit Cold Area" );
			AddButton( x2, y2+(20*i), 2224, 2224, 7, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Cold Resist" );
			AddButton( x2, y2+(20*i), 2224, 2224, 8, GumpButtonType.Reply, 0 );
			i ++;
		}
		else if (m_Player.IsAeromancer())
		{
			AddLabel( x, y+(20*i), 150, "Hit Lightning" );
			AddButton( x2, y2+(20*i), 2224, 2224, 9, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Hit Energy Area" );
			AddButton( x2, y2+(20*i), 2224, 2224, 10, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Energy Resist" );
			AddButton( x2, y2+(20*i), 2224, 2224, 11, GumpButtonType.Reply, 0 );
			i ++;
		}
		else if (m_Player.IsPyromancer())
		{
			AddLabel( x, y+(20*i), 150, "Hit Fireball" );
			AddButton( x2, y2+(20*i), 2224, 2224, 12, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Hit Fire Area" );
			AddButton( x2, y2+(20*i), 2224, 2224, 13, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Fire Resist" );
			AddButton( x2, y2+(20*i), 2224, 2224, 14, GumpButtonType.Reply, 0 );
			i ++;
		}
		else if (m_Player.IsNecromancer())
		{
			AddLabel( x, y+(20*i), 150, "Hit Dispel" );
			AddButton( x2, y2+(20*i), 2224, 2224, 15, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Hit Poison Area" );
			AddButton( x2, y2+(20*i), 2224, 2224, 16, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Poison Resist" );
			AddButton( x2, y2+(20*i), 2224, 2224, 17, GumpButtonType.Reply, 0 );
			i ++;
		}
		else if (m_Player.IsGeomancer())
		{
			AddLabel( x, y+(20*i), 150, "Hit Magic Arrow" );
			AddButton( x2, y2+(20*i), 2224, 2224, 18, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Hit Physical Area" );
			AddButton( x2, y2+(20*i), 2224, 2224, 19, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Physical Resist" );
			AddButton( x2, y2+(20*i), 2224, 2224, 20, GumpButtonType.Reply, 0 );
			i ++;
		}
		
	}
	
	public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
	{
		if ( info.ButtonID >= 1 )
		{
			switch(info.ButtonID)
			{
				case 1:
					staff.Attributes.RegenMana += 1;
					break;
				case 2:
					staff.Attributes.SpellDamage += 5;
					break;
				case 3:
					staff.WeaponAttributes.MageWeapon += 10;
					break;
				case 4:
					staff.Attributes.CastSpeed += 1;
					break;
				case 5:
					staff.Attributes.CastRecovery += 1;
					break;
				case 6:
					staff.WeaponAttributes.HitHarm += 15;
					break;
				case 7:
					staff.WeaponAttributes.HitColdArea += 10;
					break;
				case 8:
					staff.WeaponAttributes.ResistColdBonus += 10;
					break;
				case 9:
					staff.WeaponAttributes.HitLightning += 15;
					break;
				case 10:
					staff.WeaponAttributes.HitEnergyArea += 10;
					break;
				case 11:
					staff.WeaponAttributes.ResistEnergyBonus += 10;
					break;
				case 12:
					staff.WeaponAttributes.HitFireball += 15;
					break;
				case 13:
					staff.WeaponAttributes.HitFireArea += 10;
					break;
				case 14:
					staff.WeaponAttributes.ResistFireBonus += 10;
					break;
				case 15:
					staff.WeaponAttributes.HitDispel += 15;
					break;
				case 16:
					staff.WeaponAttributes.HitPoisonArea += 10;
					break;
				case 17:
					staff.WeaponAttributes.ResistPoisonBonus += 10;
					break;
				case 18:
					staff.WeaponAttributes.HitMagicArrow += 15;
					break;
				case 19:
					staff.WeaponAttributes.HitPhysicalArea += 10;
					break;
					case 20:
					staff.WeaponAttributes.ResistPhysicalBonus += 10;
					break;
					
			}
		}
		
	}
	}
	
}
