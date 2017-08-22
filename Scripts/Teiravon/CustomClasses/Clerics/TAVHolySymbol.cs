using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Server.Gumps;
using Server.Targeting;
using Server.Targets;


namespace Server.Items
{


	[FlipableAttribute( 0xF5C, 0xF5D )]
	public class UnholyMace : BaseBashing
	{
		public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

		public override int AosMinDamage { get { return 14; } }
		public override int AosMaxDamage { get { return 16; } }
		public override int AosSpeed { get { return 40; } }

		public override int InitMinHits { get { return 150; } }
		public override int InitMaxHits { get { return 150; } }

		private int m_Deity;
		private MaceQuality m_MaceQuality;

		public enum MaceQuality
		{
			None,
			Cursed = 1,
			Unholy = 2,
			Tainted = 3
		}

		[Constructable]
		public UnholyMace()
			: this( 10 )
		{
		}

		[Constructable]
		public UnholyMace( int playerlevel )
			: base( 0xF5C )
		{
			Name = "Unholy Mace";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			Weight = 20.0;
			Hue = 2266;

			if ( playerlevel < 13 )
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 30;
				m_MaceQuality = MaceQuality.Cursed;
                WeaponAttributes.HitLeechHits = 5;
			}
			else if ( playerlevel < 16 )
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 35;
				//WeaponAttributes.HitPoisonArea = 2;
				WeaponAttributes.SelfRepair = 5;
                WeaponAttributes.HitLeechHits = 5;
                WeaponAttributes.HitLeechMana = 10;
				m_MaceQuality = MaceQuality.Unholy;

			}
			else if ( playerlevel <= 25 )
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 45;
				//WeaponAttributes.HitPoisonArea = 4;
				WeaponAttributes.SelfRepair = 8;
                WeaponAttributes.HitLeechHits = 10;
				WeaponAttributes.HitLeechMana = 15;
				m_MaceQuality = MaceQuality.Tainted;
			}
			else
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 35;
				//WeaponAttributes.HitPoisonArea = 2;
				WeaponAttributes.SelfRepair = 5;
                WeaponAttributes.HitLeechHits = 5;
                WeaponAttributes.HitLeechMana = 10;
				m_MaceQuality = MaceQuality.Unholy;
			}
		}


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 40;
			fire = 30;
			pois = 30;
			nrgy = cold = 0;
		}


		public override bool CanEquip( Mobile from )
		{
			if ( from is TeiravonMobile )
			{
				TeiravonMobile m_Player = ( TeiravonMobile )from;

				if ( !m_Player.IsDarkCleric() )
				{
					from.SendMessage( "Only dark clerics can equip that." );
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}({2})", "Quality", m_MaceQuality, (int)m_MaceQuality );

		}


		public UnholyMace( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )1 ); // version

			writer.Write( ( int )m_MaceQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            
            if (version < 1)
			    m_Deity =reader.ReadInt();

			m_MaceQuality = ( MaceQuality )reader.ReadInt();
		}
	}

    [FlipableAttribute(0x0E89, 0x0E8A)]
    public class UnholyStaff : BaseStaff
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

        public override int AosMinDamage { get { return 12; } }
        public override int AosMaxDamage { get { return 14; } }
        public override int AosSpeed { get { return 20; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        private StaffQuality m_StaffQuality;


        public enum StaffQuality
        {
			None,
            Defiled = 1,
			Cursed = 2,
			Unholy = 3,
			Tainted = 4
        }

        [Constructable]
        public UnholyStaff()
            : this(10)
        {
        }

        [Constructable]
        public UnholyStaff(int playerlevel)
            : base(0x0E89)
        {
            Name = "Unholy Staff";
            PlayerConstructed = true;
            Resource = CraftResource.None;
            Weight = 20.0;
            Hue = 2266;


            if (playerlevel < 8)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastSpeed = 1;
                m_StaffQuality = StaffQuality.Defiled;

            }
            if (playerlevel < 13)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastSpeed = 2;
                m_StaffQuality = StaffQuality.Cursed;

            }
            else if (playerlevel < 16)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastSpeed = 3;
                Attributes.BonusMana = 15;
                WeaponAttributes.SelfRepair = 5;
                m_StaffQuality = StaffQuality.Unholy;

            }
            else if (playerlevel <= 25)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastSpeed = 4;
                Attributes.BonusMana = 25;
                WeaponAttributes.SelfRepair = 8;
                Attributes.RegenMana = 1;
                m_StaffQuality = StaffQuality.Tainted;
            }
            else
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastSpeed = 3;
                Attributes.BonusMana = 15;
                WeaponAttributes.SelfRepair = 5;
                m_StaffQuality = StaffQuality.Unholy;
            }
        }


        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy)
        {
            phys = 100;
            cold = nrgy = fire = pois = 0;
        }


        public override bool CanEquip(Mobile from)
        {
            if (from is TeiravonMobile)
            {
                TeiravonMobile m_Player = (TeiravonMobile)from;

                if (!m_Player.IsDarkCleric())
                {
                    from.SendMessage("Only dark clerics can equip that.");
                    return false;
                }

                return true;
            }

            return base.CanEquip(from);
        }

        public override void GetProperties(ObjectPropertyList list)
        {

            base.GetProperties(list);

            list.Add(1060658, "{0}\t{1}({2})", "Quality", m_StaffQuality, (int)m_StaffQuality);

        }


        public UnholyStaff(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_StaffQuality);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_StaffQuality = (StaffQuality)reader.ReadInt();
        }
    }

	[FlipableAttribute( 0xF5C, 0xF5D )]
	public class HolyMace : BaseBashing
	{
		public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

		public override int AosMinDamage { get { return 14; } }
		public override int AosMaxDamage { get { return 16; } }
		public override int AosSpeed { get { return 40; } }

		public override int InitMinHits { get { return 150; } }
		public override int InitMaxHits { get { return 150; } }

		private int m_Deity;
		private MaceQuality m_MaceQuality;

		public enum MaceQuality
		{
			None,
			Blessed = 1,
			Holy = 2,
			Divine = 3
		}

		[Constructable]
		public HolyMace()
			: this( 10 )
		{
		}

		[Constructable]
		public HolyMace( int playerlevel )
			: base( 0xF5C )
		{
			Name = "Holy Mace";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			Weight = 20.0;
			Hue = 2229;

			if ( playerlevel < 13 )
			{
				Attributes.SpellChanneling = 1;
				Attributes.WeaponDamage = 30;
				m_MaceQuality = MaceQuality.Blessed;
                Attributes.RegenHits = 1;

			}
			else if ( playerlevel < 16 )
			{
				Attributes.SpellChanneling = 1;
				//WeaponAttributes.HitEnergyArea = 2;
				Attributes.WeaponDamage = 35;
				WeaponAttributes.SelfRepair = 5;
                Attributes.RegenHits = 2;
				m_MaceQuality = MaceQuality.Holy;

			}
			else if ( playerlevel <= 25 )
			{
				Attributes.SpellChanneling = 1;
				//WeaponAttributes.HitEnergyArea = 4;
				Attributes.WeaponDamage = 45;
				WeaponAttributes.SelfRepair = 8;
				Attributes.RegenMana = 1;
                Attributes.RegenHits = 2;
				m_MaceQuality = MaceQuality.Divine;
			}
			else
			{
				Attributes.SpellChanneling = 1;
				//WeaponAttributes.HitEnergyArea = 2;
				Attributes.WeaponDamage = 35;
				WeaponAttributes.SelfRepair = 5;
                Attributes.RegenHits = 2;
				m_MaceQuality = MaceQuality.Holy;
			}
		}


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			phys = 40;
			nrgy = 30;
			cold = 30;
			fire = pois = 0;
		}


		public override bool CanEquip( Mobile from )
		{
			if ( from is TeiravonMobile )
			{
				TeiravonMobile m_Player = ( TeiravonMobile )from;

				if ( !m_Player.IsCleric() && !m_Player.IsPaladin())
				{
					from.SendMessage( "Only clerics can equip that." );
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}({2})", "Quality", m_MaceQuality, (int)m_MaceQuality );

		}


		public HolyMace( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )1 ); // version

			writer.Write( ( int )m_MaceQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            if (version < 1)
			    m_Deity = reader.ReadInt();

			m_MaceQuality = ( MaceQuality )reader.ReadInt();
		}
	}

    [FlipableAttribute(0x0E89, 0x0E8A)]
    public class HolyStaff : BaseStaff
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

        public override int AosMinDamage { get { return 12; } }
        public override int AosMaxDamage { get { return 14; } }
        public override int AosSpeed { get { return 20; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        private StaffQuality m_StaffQuality;


        public enum StaffQuality
        {
            None,
            Consecrated = 1,
            Blessed = 2,
            Holy = 3,
            Divine = 4
        }

        [Constructable]
        public HolyStaff()
            : this(10)
        {
        }

        [Constructable]
        public HolyStaff(int playerlevel)
            : base(0x0E89)
        {
            Name = "Holy Staff";
            PlayerConstructed = true;
            Resource = CraftResource.None;
            Weight = 20.0;
            Hue = 2229;


            if (playerlevel < 8)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastRecovery = 1;
                m_StaffQuality = StaffQuality.Consecrated;

            }
            else if (playerlevel < 13)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastRecovery = 2;
                m_StaffQuality = StaffQuality.Blessed;

            }
            else if (playerlevel < 16)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastRecovery = 3;
                Attributes.LowerManaCost = 10;
                WeaponAttributes.SelfRepair = 5;
                m_StaffQuality = StaffQuality.Holy;

            }
            else if (playerlevel <= 25)
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastRecovery = 4;
                Attributes.LowerManaCost = 15;
                WeaponAttributes.SelfRepair = 8;
                Attributes.RegenMana = 1;
                m_StaffQuality = StaffQuality.Divine;
            }
            else
            {
                Attributes.SpellChanneling = 1;
                Attributes.CastRecovery = 3;
                Attributes.LowerManaCost = 10;
                WeaponAttributes.SelfRepair = 5;
                m_StaffQuality = StaffQuality.Holy;
            }
        }


        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy)
        {
            phys = 100;
            cold = nrgy = fire = pois = 0;
        }


        public override bool CanEquip(Mobile from)
        {
            if (from is TeiravonMobile)
            {
                TeiravonMobile m_Player = (TeiravonMobile)from;

                if (!m_Player.IsCleric() && !m_Player.IsPaladin())
                {
                    from.SendMessage("Only clerics can equip that.");
                    return false;
                }

                return true;
            }

            return base.CanEquip(from);
        }

        public override void GetProperties(ObjectPropertyList list)
        {

            base.GetProperties(list);

            list.Add(1060658, "{0}\t{1}({2})", "Quality", m_StaffQuality, (int)m_StaffQuality);

        }


        public HolyStaff(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_StaffQuality);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_StaffQuality = (StaffQuality)reader.ReadInt();
        }
    }

	public class UnholyRobe : BaseArmor
	{

		private int m_Deity;
		private RobeQuality m_RobeQuality;

		public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Cloth; } }

		public enum RobeQuality
		{
			None,
			Cursed = 1,
			Unholy = 2,
			Tainted = 3
		}


		[Constructable]
		public UnholyRobe()
			: this( 10 )
		{
		}

		[Constructable]
		public UnholyRobe( int playerlevel )
			: base( 0x26AE )
		{
			Name = "Unholy Robe";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			Hue = 902;

			if ( playerlevel < 10 )
			{
				Attributes.BonusStr = 5;
				Attributes.BonusStam = 5;
				m_RobeQuality = RobeQuality.Cursed;
			}
			else if ( playerlevel < 15 )
			{
				Attributes.AttackChance = 5;
				Attributes.BonusStr = 8;
				Attributes.BonusStam = 8;
				m_RobeQuality = RobeQuality.Unholy;
			}
			else if ( playerlevel <= 25 )
			{
				Attributes.AttackChance = 10;
				Attributes.BonusStr = 10;
				Attributes.BonusStam = 10;
				m_RobeQuality = RobeQuality.Tainted;
			}
			else
			{
				Attributes.AttackChance = 5;
				Attributes.BonusStr = 8;
				Attributes.BonusStam = 8;
				m_RobeQuality = RobeQuality.Unholy;
			}

		}

		public override bool CanEquip( Mobile from )
		{
			if ( from is TeiravonMobile )
			{
				TeiravonMobile m_Player = ( TeiravonMobile )from;

				if ( !m_Player.IsDarkCleric() )
				{
					from.SendMessage( "Only dark clerics can equip that." );
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}({2})", "Quality", m_RobeQuality, (int)m_RobeQuality );

		}


		public UnholyRobe( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )1 ); // version


			writer.Write( ( int )m_RobeQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            
            if (version < 1)
			    m_Deity = reader.ReadInt();

			m_RobeQuality = ( RobeQuality )reader.ReadInt();
		}
	}


	public class HolyRobe : BaseArmor
	{

		private int m_Deity;
		private RobeQuality m_RobeQuality;

		public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Cloth; } }


		public enum RobeQuality
		{
			None,
			Blessed = 1,
			Holy = 2,
			Divine = 3
		}


		[Constructable]
		public HolyRobe()
			: this( 10 )
		{
		}

		[Constructable]
		public HolyRobe( int playerlevel )
			: base( 0x26AE )
		{
			Name = "Holy Robe";
			PlayerConstructed = true;
			Resource = CraftResource.None;
			Hue = 601;

			if ( playerlevel < 10 )
			{
				Attributes.BonusInt = 5;
				Attributes.BonusMana = 5;
				m_RobeQuality = RobeQuality.Blessed;
			}
			else if ( playerlevel < 15 )
			{
				Attributes.RegenMana = 2;
				Attributes.BonusInt = 10;
				Attributes.BonusMana = 10;
				m_RobeQuality = RobeQuality.Holy;
			}
			else if ( playerlevel <= 25 )
			{
				Attributes.RegenMana = 4;
				Attributes.BonusInt = 15;
				Attributes.BonusMana = 15;
				m_RobeQuality = RobeQuality.Divine;
			}
			else
			{
				Attributes.RegenMana = 2;
				Attributes.BonusInt = 10;
				Attributes.BonusMana = 10;
				m_RobeQuality = RobeQuality.Holy;
			}

		}

		public override bool CanEquip( Mobile from )
		{
			if ( from is TeiravonMobile )
			{
				TeiravonMobile m_Player = ( TeiravonMobile )from;

				if ( !m_Player.IsCleric() && !m_Player.IsPaladin() )
				{
					from.SendMessage( "Only clerics can equip that." );
					return false;
				}

				return true;
			}

			return base.CanEquip( from );
		}

		public override void GetProperties( ObjectPropertyList list )
		{

			base.GetProperties( list );

            list.Add(1060658, "{0}\t{1}({2})", "Quality", m_RobeQuality, (int)m_RobeQuality);

		}


		public HolyRobe( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )1 ); // version

			writer.Write( ( int )m_RobeQuality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            
            if( version < 1)
			    m_Deity = reader.ReadInt();

			m_RobeQuality = ( RobeQuality )reader.ReadInt();
		}
	}


	public class HolySymbol : Item
	{

		private DateTime m_NextCraft;
		private DateTime m_NextControl;
		private DateTime m_NextTurn;
		private DateTime m_NextBless;
		private SymbolType m_Symbol;
		private TeiravonMobile m_User;
		private bool m_Locked;

		//Bless item variables
		private bool m_BlessTimer;
		private DateTime m_BlessWearOffTime;
		private Item m_BlessedItem;
		private bool m_ClericBless;

		public enum SymbolType
		{
			Cleric = 1,
			DarkCleric = 2,
			Necromancer = 3,
			Paladin = 4
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SymbolType Symbol
		{
			get { return m_Symbol; }
			set
			{
				m_Symbol = value;
				if ( m_Symbol == SymbolType.Cleric )
				{
					this.ItemID = 0x1225;
					this.Hue = 591;
					this.Name = "Holy symbol";
				}
				else if ( m_Symbol == SymbolType.DarkCleric )
				{
					this.ItemID = 0x1853;
					this.Hue = 902;
					this.Name = "Unholy symbol";
				}
				else if ( m_Symbol == SymbolType.Necromancer )
				{
					this.ItemID = 6921;
					this.Hue = 556;
					this.Name = "Unholy symbol";
				}

			}
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public TeiravonMobile User
		{
			get { return m_User; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsLockedToUser
		{
			get { return m_Locked; }
			set { m_Locked = value; }
		}


		[Constructable]
		public HolySymbol()
			: this( SymbolType.Cleric )
		{
		}

		[Constructable]
		public HolySymbol( SymbolType stype )
			: base( 0x1225 )
		{
			LootType = LootType.Blessed;
			Symbol = stype;

			m_NextCraft = DateTime.Now;
			m_NextControl = DateTime.Now;
			m_NextTurn = DateTime.Now;
			m_NextBless = DateTime.Now;

			m_User = null;
			m_Locked = false;

			m_BlessTimer = false;
			m_BlessWearOffTime = DateTime.Now;
			m_ClericBless = false;

			if ( ( ( int )stype > 3 ) || ( ( int )stype < 1 ) )
			{
				Symbol = SymbolType.Cleric;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{

			TeiravonMobile m_Player = ( TeiravonMobile )from;

			if ( ( m_Player.IsCleric() && m_Symbol == SymbolType.Cleric ) || ( m_Player.IsDarkCleric() && ( m_Symbol == SymbolType.DarkCleric ) ) || ( m_Player.IsPaladin() && m_Symbol == SymbolType.Paladin) || ( m_Player.IsNecromancer() && ( m_Symbol == SymbolType.Necromancer ) ) || ( !m_Player.IsCleric() && !m_Player.IsDarkCleric() && !m_Player.IsNecromancer() && !m_Player.IsPaladin() ) )
			{
				if ( IsChildOf( from.Backpack ) || Parent == from )
				{

					if ( m_Player.IsDarkCleric() || m_Player.IsCleric() || m_Player.IsNecromancer() || m_Player.IsPaladin() )
					{
						if ( ( m_User != null && m_User != m_Player && m_Locked ) || ( m_User == null && m_Locked ) )
						{
							m_Player.SendMessage( "That symbol belongs to someone else!" );
							return;
						}
						else
						{
							m_User = m_Player;
							m_Locked = true;
						}
					}

					from.SendGump( new HolySymbolGump( from, this ) );

				}
				else
					from.SendLocalizedMessage( 1042001 );
			}

			else
				from.SendMessage( "You sense that you shouldn't touch it." );
		}

		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			return DeathMoveResult.MoveToBackpack;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
		}

		public DateTime NextCraft()
		{
			return m_NextCraft;
		}

		public void SaveCraftTime( DateTime crafttime )
		{
			m_NextCraft = ( DateTime )crafttime;
		}

		public DateTime NextControl()
		{
			return m_NextControl;
		}

		public void SaveControlTime( DateTime controltime )
		{
			m_NextControl = ( DateTime )controltime;
		}

		public DateTime NextTurn()
		{
			return m_NextTurn;
		}

		public void SaveTurnTime( DateTime turntime )
		{
			m_NextTurn = ( DateTime )turntime;
		}

		public DateTime NextBless()
		{
			return m_NextBless;
		}

		public void SaveBlessTime( DateTime blesstime )
		{
			m_NextBless = ( DateTime )blesstime;
		}

		public void RemoveBless( Item item, bool clericbless )
		{
			m_BlessTimer = true;
			m_BlessedItem = item;
			m_ClericBless = clericbless;

			m_BlessWearOffTime = DateTime.Now + TimeSpan.FromMinutes( 60.0 );
			Timer m_Timer = new WearOffTimer( m_BlessedItem, m_ClericBless, m_BlessWearOffTime, this );
			m_Timer.Start();

		}

		public void BlessTimer( bool timer )
		{
			m_BlessTimer = timer;
		}

		private class WearOffTimer : Timer
		{
			private Item m_BlessedTarget;
			private bool m_RemoveCleric;
			private BaseWeapon m_WeaponTarg;
			private HolySymbol m_HolySymbol;

			public WearOffTimer( Item item, bool cleric, DateTime end, HolySymbol hsymbol )
				: base( end - DateTime.Now )
			{
				Priority = TimerPriority.OneMinute;
				m_BlessedTarget = item;
				m_RemoveCleric = cleric;
				m_HolySymbol = hsymbol;
			}

			protected override void OnTick()
			{

				if ( m_HolySymbol != null )
					m_HolySymbol.BlessTimer( false );

				if ( ( m_BlessedTarget != null ) && ( m_BlessedTarget is BaseWeapon ) )
				{
					m_WeaponTarg = ( BaseWeapon )m_BlessedTarget;

					if ( m_WeaponTarg != null )
					{
						if ( m_RemoveCleric )
						{
							m_WeaponTarg.Slayer = SlayerName.None;
							m_WeaponTarg.PublicOverheadMessage( Network.MessageType.Regular, 0, false, "The blessing placed on the weapon has faded" );
						}
						else
						{
							m_WeaponTarg.Attributes.WeaponDamage -= 25;
							m_WeaponTarg.WeaponAttributes.HitLeechHits -= 10;
							m_WeaponTarg.PublicOverheadMessage( Network.MessageType.Regular, 0, false, "The dark blessing placed on the weapon has faded" );
						}
					}


				}

				else
				{
					if ( m_BlessedTarget != null )
					{
						m_BlessedTarget.LootType = LootType.Regular;
						m_BlessedTarget.PublicOverheadMessage( Network.MessageType.Regular, 0, false, "The blessing placed on the item has faded" );
					}
				}

			}
		}


		public HolySymbol( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )2 ); // version

			writer.WriteDeltaTime( m_NextCraft );

			writer.WriteDeltaTime( m_NextControl );

			writer.WriteDeltaTime( m_NextTurn );

			writer.Write( ( int )m_Symbol );

			writer.Write( m_User );

			writer.Write( ( bool )m_Locked );

			writer.Write( ( bool )m_BlessTimer );

			if ( m_BlessTimer )
			{
				writer.WriteDeltaTime( m_BlessWearOffTime );
				writer.Write( m_BlessedItem );
				writer.Write( ( bool )m_ClericBless );
			}

		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_NextCraft = reader.ReadDeltaTime();

			m_NextControl = reader.ReadDeltaTime();

			m_NextTurn = reader.ReadDeltaTime();

			m_Symbol = ( SymbolType )reader.ReadInt();

			if ( version >= 1 )
				m_User = ( TeiravonMobile )reader.ReadMobile();

			if ( version >= 2 )
				m_Locked = reader.ReadBool();

			m_BlessTimer = reader.ReadBool();

			if ( m_BlessTimer )
			{
				m_BlessWearOffTime = reader.ReadDeltaTime();
				m_BlessedItem = reader.ReadItem();
				m_ClericBless = reader.ReadBool();

				Timer m_Timer = new WearOffTimer( m_BlessedItem, m_ClericBless, m_BlessWearOffTime, this );
				m_Timer.Start();

			}

		}

	}


	public class HolyWater : Item
	{
		private WaterStrength m_Strength;
		private int s_Factor;

		public enum WaterStrength
		{
			None,
			Light = 1,
			Blessed = 2,
			Holy = 3,
			Divine = 4
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WaterStrength PotionStrength
		{
			get { return m_Strength; }
			set { m_Strength = value; InvalidateProperties(); }
		}

		[Constructable]
		public HolyWater()
			: this( 1, 0 )
		{
		}

		[Constructable]
		public HolyWater( int amount )
			: this( amount, 0 )
		{
		}

		[Constructable]
		public HolyWater( int amount, WaterStrength p_Strength )
			: base( 0xE24 )
		{
			Stackable = true;
			Name = "Holy water";
			Hue = 0x254;
			Amount = amount;

			if ( ( int )p_Strength > 4 )
				m_Strength = WaterStrength.Divine;

			else if ( ( int )p_Strength < 0 )
				m_Strength = WaterStrength.None;

			else
				m_Strength = ( WaterStrength )p_Strength;
		}


		[Constructable]
		public HolyWater( TeiravonMobile m_Player )
			: base( 0xE24 )
		{
			Stackable = true;
			Name = "Holy water";
			Hue = 0x254;
			Amount = ( int )( m_Player.PlayerLevel / 5 ) + 1;

			//FORMULA: Determine potion strength

			s_Factor = ( int )( m_Player.PlayerLevel / 5 );

			if ( Utility.Random( 8 ) == 0 )
				s_Factor += 1;

			else if ( Utility.Random( 4 ) == 0 )
				s_Factor -= 1;

			if ( s_Factor < 0 )
				s_Factor = 0;

			if ( s_Factor > 4 )
				s_Factor = 4;

			m_Strength = ( WaterStrength )s_Factor;
		}


		public override Item Dupe( int amount )
		{
			return base.Dupe( new HolyWater( amount, m_Strength ), amount );
		}


		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.CanBeginAction( typeof( HolyWater ) ) )
			{
				from.SendMessage( "It's too soon to do this again." );
				return;
			}

			TeiravonMobile m_Player = ( TeiravonMobile )from;

			//if ( m_Player.IsCleric() )
			//{

			if ( IsChildOf( from.Backpack ) || Parent == from )
			{
				m_Player.Target = new HolyWaterTarget( m_Player, this );
				m_Player.SendMessage( "On what do you want to use the holy water?" );
			}

			else
				from.SendLocalizedMessage( 1042001 );
			//}

		}


		public override bool StackWith( Mobile from, Item dropped, bool playsound )
		{

			if ( dropped is HolyWater )
			{

				HolyWater item = dropped as HolyWater;

				return ( dropped != null && item.PotionStrength == this.PotionStrength && base.StackWith( from, dropped, true ) );
			}

			else
				return base.StackWith( from, dropped, true );
		}


		public class HolyWaterTarget : Target
		{
			private TeiravonMobile m_Player;
			private TeiravonMobile m_PlayerTarget;
			private Mobile m_Target;
			private HolyWater m_Item;
			private Item m_ItemTarget;

			public HolyWaterTarget( Mobile from, Item item )
				: base( 5, true, TargetFlags.Harmful )
			{
				m_Player = ( TeiravonMobile )from;
				m_Item = item as HolyWater;
			}


			protected override void OnTarget( Mobile from, object targ )
			{

				if ( from.CanSee( targ ) )
				{

					m_Item.Amount -= 1;

					if ( m_Item.Amount == 0 )
						m_Item.Delete();


					if ( targ is TeiravonMobile )
						m_PlayerTarget = ( TeiravonMobile )targ;

					if ( targ is Mobile )
						m_Target = ( Mobile )targ;

					m_Player.BlockAction( typeof( HolyWater ), TimeSpan.FromSeconds( 5.0 ) );
                    bool canhurt = m_Player.CanBeHarmful(m_Target);
                    if (canhurt && targ is TeiravonMobile && (m_PlayerTarget.IsEvil() || m_PlayerTarget.Karma <= -4000 || m_PlayerTarget.IsUndead()))
					{
                        if (m_Player.IsUndead() || m_Player.IsVampire())
                        {
                            m_PlayerTarget.Damage((int)(m_Item.HolyWaterDamage(m_PlayerTarget, m_Item)), (Mobile)from);
                            MortalStrike.BeginWound(m_PlayerTarget, TimeSpan.FromSeconds((int)m_Item.PotionStrength * 5));
                        }
                        else
                            m_PlayerTarget.Damage((int)((m_Item.HolyWaterDamage(m_PlayerTarget, m_Item)) / 2), (Mobile)from);

						m_Player.SendMessage( "You see the holy water burn your target!" );
						m_PlayerTarget.Emote( "*is burned by the holy water of {0}*", m_Player.Name );
					}
					else if (canhurt && targ is Mobile && ( m_Target.Karma < 0 ) )
					{
						m_Target.Damage( m_Item.HolyWaterDamage( m_Target, m_Item ), ( Mobile )from );
						m_Player.SendMessage( "You see the holy water burn your target!" );
						m_Target.Emote( "*is burned by the holy water of {0}*", m_Player.Name );
					}

					else if ( targ is Mobile )
					{
						m_Player.SendMessage( "The water seems to have no effect." );
						m_Target.Emote( "*is drenched in {0}'s holy water*", m_Player.Name );
					}
					else if ( targ is Item )
					{

						m_ItemTarget = ( Item )targ;
						m_Player.SendMessage( "You drench the item in holy water." );

						m_ItemTarget.PublicOverheadMessage( Network.MessageType.Emote, 5, false, "*is drenched in holy water*" );
					}
					else
					{
						if ( targ is IPoint3D )
						{
							IPoint3D p = targ as IPoint3D;
							Point3D loc = new Point3D( p.X, p.Y, p.Z );
							bool blessed = false;

							IPooledEnumerable eable = m_Player.Map.GetItemsInRange( loc, 5 );

							foreach ( Item item in eable )
							{
								if ( item is HolyWaterResidue )
								{
									blessed = true;
									break;
								}
							}

							eable.Free();

							if ( !blessed )
							{
								HolyWaterResidue m_Vial = new HolyWaterResidue( ( int )m_Item.PotionStrength, from );
								m_Vial.MoveToWorld( loc, from.Map );
								m_Player.SendMessage( "You have blessed the area!" );
							}
							else
								m_Player.SendMessage( "The area is already blessed." );
						}

						m_Player.Emote( "*splashes holy water around*" );
					}

				}

				else
					from.SendLocalizedMessage( 500237 ); // Target can not be seen.

			}

			protected override void OnTargetOutOfRange( Mobile from, object targ )
			{
				m_Player.SendMessage( "You must be closer to use your holy water!" );
			}

		}




		public int HolyWaterDamage( Mobile target, HolyWater item )
		{
			int s_Scalar = ( int )( item.PotionStrength ) + 1;

			//FORMULA: Determine holy water damage

			int damage = (int)(s_Scalar * Utility.RandomMinMax( 8, 12 ) );

			return damage;

		}



		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
			list.Add( 1060658, "{0}\t{1}", "Quantity", Amount );
			list.Add( 1060659, "{0}\t{1}", "Strength", m_Strength );
		}



		public HolyWater( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version

			writer.Write( ( int )m_Strength );

		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Strength = ( WaterStrength )reader.ReadInt();


		}



	}


	public class HolyWaterResidue : Item
	{

		public enum WaterResidue
		{
			None,
			Light = 1,
			High = 2,
			Holy = 3,
			Divine = 4
		}


		private WaterResidue m_Strength;
		private Mobile m_Placer;
		private int damagecount;
		private DateTime m_DecayTime;
		private Timer m_Timer;


		[CommandProperty( AccessLevel.GameMaster )]
		public WaterResidue ResidueStrength
		{
			get { return m_Strength; }
			set { m_Strength = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile PlacedBy
		{
			get { return m_Placer; }
		}


		[Constructable]
		public HolyWaterResidue( int p_Strength, Mobile from )
			: base( 0x186A )
		{
			Name = "Holy Water Residue";
			Visible = false;
			Movable = false;
			m_Strength = ( WaterResidue )p_Strength;
			m_Placer = from;
			damagecount = 0;

			m_DecayTime = DateTime.Now + TimeSpan.FromMinutes( 60.0 );
			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		public override bool HandlesOnMovement { get { return true; } }


		public override void OnMovement( Mobile m, Point3D oldLocation )
		{

			if ( m != null && m.Location != oldLocation && m.Alive )
			{

				if ( m.InRange( this.GetWorldLocation(), 3 ) )
				{

					if ( m is BaseCreature && m.Karma <= -300  && m_Placer.CanBeHarmful(m))
					{
                        if (m_Placer != null)
                        {
                            m.Damage(GetResidueDamage(m), m_Placer);
                        }
                        else
                            m.Damage(GetResidueDamage(m));

						damagecount++;
					}

					else if ( m is TeiravonMobile && m_Placer.CanBeHarmful(m) )
					{

						TeiravonMobile m_Target = ( TeiravonMobile )m;

						if ( ( m_Target.IsEvil() || m_Target.Karma <= -4000 ) && ( m_Target.AccessLevel == AccessLevel.Player ) )
						{
							if ( m_Placer != null )
								m_Target.Damage( GetResidueDamage( m_Target ), m_Placer );
							else
								m_Target.Damage( GetResidueDamage( m_Target ) );

							m_Target.SendMessage( "The blessed ground burns you!" );
							damagecount++;
						}
					}

				}
			}

			if ( damagecount >= 10 )
			{
				if ( m_Placer != null )
					this.PublicOverheadMessage( Network.MessageType.Regular, 5, false, "The blessing placed on the ground by " + m_Placer.Name + " is negated by evil." );
				else
					this.PublicOverheadMessage( Network.MessageType.Regular, 5, false, "The blessing placed on the ground is negated by evil." );

				this.Delete();
			}

		}


		public int GetResidueDamage( Mobile target )
		{
			//FORMULA: Determine holy water residue damage

			int damage = ( int )(Math.Abs( target.Karma / 5000 ) + Utility.RandomMinMax( 1, 5 ) * ((int)m_Strength + 1) );
			return damage;
		}



		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}


		private class InternalTimer : Timer
		{
			private Item m_Item;

			public InternalTimer( Item item, DateTime end )
				: base( end - DateTime.Now )
			{
				m_Item = item;
			}

			protected override void OnTick()
			{
				if ( m_Item != null )
				{
					m_Item.PublicOverheadMessage( Network.MessageType.Regular, 5, false, "The blessing placed on the ground seems to fade." );
					m_Item.Delete();
				}
			}
		}


		public HolyWaterResidue( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version

			writer.Write( ( int )m_Strength );

			writer.Write( ( int )damagecount );

			writer.WriteDeltaTime( m_DecayTime );

			writer.Write( m_Placer );

		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Strength = ( WaterResidue )reader.ReadInt();

			damagecount = reader.ReadInt();

			m_DecayTime = reader.ReadDeltaTime();

			m_Timer = new InternalTimer( this, m_DecayTime );

			m_Timer.Start();

			m_Placer = reader.ReadMobile();


		}



	}

	public class UnholyWater : Item
	{
		private UnholyWaterStrength m_Strength;
		private int s_Factor;

		public enum UnholyWaterStrength
		{
			None,
			Foul = 1,
			Cursed = 2,
			Unholy = 3,
			Tainted = 4
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public UnholyWaterStrength PotionStrength
		{
			get { return m_Strength; }
			set { m_Strength = value; InvalidateProperties(); }
		}

		[Constructable]
		public UnholyWater()
			: this( 1, 0 )
		{
		}

		[Constructable]
		public UnholyWater( int amount )
			: this( amount, 0 )
		{
		}

		[Constructable]
		public UnholyWater( int amount, UnholyWaterStrength p_Strength )
			: base( 0xE24 )
		{
			Stackable = true;
			Name = "Unholy water";
			Hue = 134;
			Amount = amount;

			if ( ( int )p_Strength > 4 )
				m_Strength = UnholyWaterStrength.Tainted;

			else if ( ( int )p_Strength < 0 )
				m_Strength = UnholyWaterStrength.None;

			else
				m_Strength = ( UnholyWaterStrength )p_Strength;
		}


		[Constructable]
		public UnholyWater( TeiravonMobile m_Player )
			: base( 0xE24 )
		{
			Stackable = true;
			Name = "Unholy water";
			Hue = 134;
			Amount = ( int )( m_Player.PlayerLevel / 5 ) + 1;

			//FORMULA: Determine potion strength

			s_Factor = ( int )( m_Player.PlayerLevel / 5 );

			if ( Utility.Random( 8 ) == 0 )
				s_Factor += 1;

			else if ( Utility.Random( 4 ) == 0 )
				s_Factor -= 1;

			if ( s_Factor < 0 )
				s_Factor = 0;

			if ( s_Factor > 4 )
				s_Factor = 4;

			m_Strength = ( UnholyWaterStrength )s_Factor;
		}


		public override Item Dupe( int amount )
		{
			return base.Dupe( new UnholyWater( amount, m_Strength ), amount );
		}


		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.CanBeginAction( typeof( UnholyWater ) ) )
			{
				from.SendMessage( "It's too soon to do this again." );
				return;
			}

			TeiravonMobile m_Player = ( TeiravonMobile )from;

			//if ( m_Player.IsDarkCleric() )
			//{

			if ( IsChildOf( from.Backpack ) || Parent == from )
			{
				m_Player.Target = new UnholyWaterTarget( m_Player, this );
				m_Player.SendMessage( "On what do you want to use the unholy water?" );
			}

			else
				from.SendLocalizedMessage( 1042001 );
			//}

		}


		public override bool StackWith( Mobile from, Item dropped, bool playsound )
		{

			if ( dropped is UnholyWater )
			{

				UnholyWater item = dropped as UnholyWater;

				return ( dropped != null && item.PotionStrength == this.PotionStrength && base.StackWith( from, dropped, true ) );
			}

			else
				return base.StackWith( from, dropped, true );
		}


		public class UnholyWaterTarget : Target
		{
			private TeiravonMobile m_Player;
			private TeiravonMobile m_PlayerTarget;
			private Mobile m_Target;
			private UnholyWater m_Item;
			private Item m_ItemTarget;

			public UnholyWaterTarget( Mobile from, Item item )
				: base( 5, true, TargetFlags.Harmful )
			{
				m_Player = ( TeiravonMobile )from;
				m_Item = item as UnholyWater;
			}


			protected override void OnTarget( Mobile from, object targ )
			{

				if ( from.CanSee( targ ) )
				{

					m_Item.Amount -= 1;

					if ( m_Item.Amount == 0 )
						m_Item.Delete();


					if ( targ is TeiravonMobile )
						m_PlayerTarget = ( TeiravonMobile )targ;

					if ( targ is Mobile )
						m_Target = ( Mobile )targ;

					m_Player.BlockAction( typeof( UnholyWater ), TimeSpan.FromSeconds( 5.0 ) );
                    bool canhurt = m_Player.CanBeHarmful(m_Target);
					if (canhurt && targ is TeiravonMobile && ( m_PlayerTarget.IsGood() || m_PlayerTarget.Karma >= 4000 ) )
					{
						m_PlayerTarget.Damage( ( int )( ( m_Item.UnholyWaterDamage( m_PlayerTarget, m_Item ) ) / 2 ), ( Mobile )from );
						m_Player.SendMessage( "You see the unholy water burn your target!" );
						m_PlayerTarget.Emote( "*is burned by the unholy water of {0}*", m_Player.Name );
					}
					else if (canhurt && targ is Mobile && ( m_Target.Karma > 0 ) )
					{
						m_Target.Damage( m_Item.UnholyWaterDamage( m_Target, m_Item ), ( Mobile )from );
						m_Player.SendMessage( "You see the unholy water burn your target!" );
						m_Target.Emote( "*is burned by the unholy water of {0}*", m_Player.Name );
					}

					else if ( targ is Mobile )
					{
						m_Player.SendMessage( "The water seems to have no effect." );
						m_Target.Emote( "*is drenched in {0}'s unholy water*", m_Player.Name );
					}
					else if ( targ is Item )
					{

						m_ItemTarget = ( Item )targ;
						m_Player.SendMessage( "You drench the item in unholy water." );

						m_ItemTarget.PublicOverheadMessage( Network.MessageType.Emote, 438, false, "*is drenched in unholy water*" );
					}
					else
					{
						if ( targ is IPoint3D )
						{
							IPoint3D p = targ as IPoint3D;
							Point3D loc = new Point3D( p.X, p.Y, p.Z );
							bool cursed = false;

							IPooledEnumerable eable = m_Player.Map.GetItemsInRange( loc, 5 );

							foreach ( Item item in eable )
							{
								if ( item is UnholyWaterResidue )
								{
									cursed = true;
									break;
								}
							}

							eable.Free();

							if ( !cursed )
							{
								UnholyWaterResidue m_Vial = new UnholyWaterResidue( ( int )m_Item.PotionStrength, from );
								m_Vial.MoveToWorld( loc, from.Map );
								m_Player.SendMessage( "You have cursed the area!" );
							}
							else
								m_Player.SendMessage( "The area is already cursed." );
						}

						m_Player.Emote( "*splashes unholy water around*" );
					}

				}

				else
					from.SendLocalizedMessage( 500237 ); // Target can not be seen.

			}

			protected override void OnTargetOutOfRange( Mobile from, object targ )
			{
				m_Player.SendMessage( "You must be closer to use your unholy water!" );
				//base.OnTargetOutOfRange( from, targ );
			}

		}




		public int UnholyWaterDamage( Mobile target, UnholyWater item )
		{
			int s_Scalar = ( int )( item.PotionStrength ) + 1;

			//FORMULA: Determine unholy water damage

			int damage = s_Scalar * ( Math.Abs( target.Karma / 4000 ) + Utility.RandomMinMax( 1, 6 ) );

			return damage;

		}



		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
			list.Add( 1060658, "{0}\t{1}", "Quantity", Amount );
			list.Add( 1060659, "{0}\t{1}", "Strength", m_Strength );
		}



		public UnholyWater( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version

			writer.Write( ( int )m_Strength );

		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Strength = ( UnholyWaterStrength )reader.ReadInt();


		}



	}

	public class UnholyWaterResidue : Item
	{

		public enum UnholyResidue
		{
			None,
			Foul = 1,
			Cursed = 2,
			Unholy = 3,
			Tainted = 4
		}


		private UnholyResidue m_Strength;
		private Mobile m_Placer;
		private int damagecount;
		private DateTime m_DecayTime;
		private Timer m_Timer;


		[CommandProperty( AccessLevel.GameMaster )]
		public UnholyResidue ResidueStrength
		{
			get { return m_Strength; }
			set { m_Strength = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile PlacedBy
		{
			get { return m_Placer; }
		}


		[Constructable]
		public UnholyWaterResidue( int p_Strength, Mobile from )
			: base( 0x186E )
		{
			Name = "Unholy Water Residue";
			Visible = false;
			Movable = false;
			m_Strength = ( UnholyResidue )p_Strength;
			m_Placer = from;
			damagecount = 0;

			m_DecayTime = DateTime.Now + TimeSpan.FromMinutes( 60.0 );
			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		public override bool HandlesOnMovement { get { return true; } }


		public override void OnMovement( Mobile m, Point3D oldLocation )
		{

			if ( m != null && m.Location != oldLocation && m.Alive )
			{

				if ( m.InRange( this.GetWorldLocation(), 3 ) )
				{

					if ( m is BaseCreature && m.Karma >= 300 && m_Placer.CanBeHarmful(m))
					{
						if ( m_Placer != null )
							m.Damage( GetResidueDamage( m ), m_Placer );
						else
							m.Damage( GetResidueDamage( m ) );

						damagecount++;
					}

                    else if (m is TeiravonMobile && m_Placer.CanBeHarmful(m))
					{

						TeiravonMobile m_Target = ( TeiravonMobile )m;

						if ( ( m_Target.IsGood() || m_Target.Karma >= 4000 ) && ( m_Target.AccessLevel == AccessLevel.Player ) )
						{
							if ( m_Placer != null )
								m_Target.Damage( GetResidueDamage( m_Target ), m_Placer );
							else
								m_Target.Damage( GetResidueDamage( m_Target ) );

							m_Target.SendMessage( "The cursed ground burns you!" );
							damagecount++;
						}
					}

				}
			}

			if ( damagecount >= 10 )
			{
				if ( m_Placer != null )
					this.PublicOverheadMessage( Network.MessageType.Regular, 438, false, "The curse placed on the ground by " + m_Placer.Name + " is negated by good." );
				else
					this.PublicOverheadMessage( Network.MessageType.Regular, 438, false, "The curse placed on the ground is negated by good." );

				this.Delete();
			}

		}


		public int GetResidueDamage( Mobile target )
		{
			//FORMULA: Determine unholy water residue damage

			int damage = ( int )Math.Abs( target.Karma / 5000 ) + Utility.RandomMinMax( 1, ( int )m_Strength + 1 );
			return damage;
		}



		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}


		private class InternalTimer : Timer
		{
			private Item m_Item;

			public InternalTimer( Item item, DateTime end )
				: base( end - DateTime.Now )
			{
				m_Item = item;
			}

			protected override void OnTick()
			{
				if ( m_Item != null )
				{
					m_Item.PublicOverheadMessage( Network.MessageType.Regular, 438, false, "The curse placed on the ground seems to fade." );
					m_Item.Delete();
				}
			}
		}


		public UnholyWaterResidue( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version

			writer.Write( ( int )m_Strength );

			writer.Write( ( int )damagecount );

			writer.WriteDeltaTime( m_DecayTime );

			writer.Write( m_Placer );

		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Strength = ( UnholyResidue )reader.ReadInt();

			damagecount = reader.ReadInt();

			m_DecayTime = reader.ReadDeltaTime();

			m_Timer = new InternalTimer( this, m_DecayTime );

			m_Timer.Start();

			m_Placer = reader.ReadMobile();


		}



	}


}