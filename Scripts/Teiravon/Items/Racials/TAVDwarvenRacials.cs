using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Engines.Craft;
using Server.Items;
using Server.Engines.Harvest;

namespace Server.Items
{

	[FlipableAttribute( 11560, 11572 )]
	public class DwarvenAxe : BaseAxe
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 37; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 35; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 61; } }
		public override int InitMaxHits{ get{ return 110; } }

		[Constructable]
		public DwarvenAxe() : base( 11560 )
		{
			Weight = 8.0;
			Name = "Dwarven Axe";
			Layer = Layer.OneHanded;
		}

		public DwarvenAxe( Serial serial ) : base( serial )
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
			
			if (ItemID == 3915)
				ItemID = 11560;
		}
	}

	public class DwarvenShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 2; } }
		public override int BaseColdResistance{ get{ return 2; } }
		public override int BasePoisonResistance{ get{ return 2; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 70; } }
		public override int InitMaxHits{ get{ return 95; } }

		public override int AosStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 23; } }

		[Constructable]
		public DwarvenShield() : base( 0x1BC3 )
		{
			Weight = 8.0;
			Name = "Dwarven Shield";
			Attributes.ReflectPhysical = 20;
		}

		public DwarvenShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
	
	public class DwarvenArmorPlate : BaseReagent, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( "{0} Dwarven Armor Plate", Amount );
			}
		}

		[Constructable]
		public DwarvenArmorPlate() : this( 1 )
		{
		}

		[Constructable]
		public DwarvenArmorPlate( int amount ) : base( 0x26B4, amount )
		{
			Weight = 1.0;
			Name = "Armor Plate";
			Hue = 2225;
		}

		public DwarvenArmorPlate( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new DwarvenArmorPlate( amount ), amount );
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
	
	[Flipable( 0x13C6, 0x13CE )]
	public class DwarvenGloves : BaseGloves
	{
		
		[Constructable]
		public DwarvenGloves() : this( 0 )
		{
		}

		[Constructable]
		public DwarvenGloves( int hue ) : base( 0x13C6 )
		{
			Name = "Dwarven Gloves";
			Weight = 1.0;
			base.Hue = 0x388;
			SkillBonuses.SetValues(0,SkillName.Mining,10);
		}

		public DwarvenGloves( Serial serial ) : base( serial )
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

	public class DwarvenLeatherChest : BaseArmor, IDyable
	{
        public bool Dye(Mobile from, DyeTub sender) { if (Deleted) return false; Hue = sender.DyedHue; return true; }
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 55; } }

		public override int AosStrReq{ get{ return 50; } }
		public override int OldStrReq{ get{ return 15; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dwarven; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public DwarvenLeatherChest() : base( 11124 )
		{
			Weight = 8.0;
			Name = "Dwarven Armor Tunic";
		}

		public DwarvenLeatherChest( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 6.0;
			if (ItemID == 5068)
				ItemID = 11124;
		}
	}
	
	public class DwarvenLeatherLegs : BaseArmor, IDyable
	{
        public bool Dye(Mobile from, DyeTub sender) { if (Deleted) return false; Hue = sender.DyedHue; return true; }
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 55; } }

		public override int AosStrReq{ get{ return 50; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dwarven; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public DwarvenLeatherLegs() : base( 11128 )
		{
			Weight = 5.0;
			Name = "Dwarven Armor Legs";
		}

		public DwarvenLeatherLegs( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			if (ItemID == 5067)
				ItemID = 11128;
		}
	}
	
	public class DwarvenLeatherArms : BaseArmor, IDyable
	{
        public bool Dye(Mobile from, DyeTub sender) { if (Deleted) return false; Hue = sender.DyedHue; return true; }
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 55; } }

		public override int AosStrReq{ get{ return 50; } }
		public override int OldStrReq{ get{ return 15; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dwarven; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public DwarvenLeatherArms() : base( 11127 )
		{
			Weight = 3.0;
			Name = "Dwarven Armor Arms";
		}

		public DwarvenLeatherArms( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 2.0;
			if (ItemID == 5069)
				ItemID = 11127;
		}
	}

	public class DwarvenLeatherCap : BaseArmor, IDyable
	{
        public bool Dye(Mobile from, DyeTub sender) { if (Deleted) return false; Hue = sender.DyedHue; return true; }
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 55; } }

		public override int AosStrReq{ get{ return 50; } }
		public override int OldStrReq{ get{ return 15; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dwarven; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public DwarvenLeatherCap() : base( 0x1DB9 )
		{
			Weight = 3.0;
			Name = "Dwarven Armor Cap";
		}

		public DwarvenLeatherCap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}

	public class DwarvenLeatherGloves : BaseArmor, IDyable
	{
        public bool Dye(Mobile from, DyeTub sender) { if (Deleted) return false; Hue = sender.DyedHue; return true; }
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 55; } }

		public override int AosStrReq{ get{ return 50; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dwarven; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public DwarvenLeatherGloves() : base( 11125 )
		{
			Weight = 2.0;
			Name = "Dwarven Armor Gloves";
		}

		public DwarvenLeatherGloves( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			if (ItemID == 5062)
				ItemID = 11125;

		}
	}

	public class DwarvenLeatherGorget : BaseArmor, IDyable
	{
        public bool Dye(Mobile from, DyeTub sender) { if (Deleted) return false; Hue = sender.DyedHue; return true; }
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 45; } }
		public override int InitMaxHits{ get{ return 55; } }

		public override int AosStrReq{ get{ return 50; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dwarven; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public DwarvenLeatherGorget() : base( 11126 )
		{
			Weight = 1.0;
		}

		public DwarvenLeatherGorget( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			if (ItemID == 5063)
				ItemID = 11126;
		}
	}

	public class DwarvenPowderOfTemperament : Item, IUsesRemaining
	{
		private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		public bool ShowUsesRemaining{ get{ return true; } set{} }

		public override int LabelNumber{ get{ return 1049082; } } // powder of temperament

		[Constructable]
		public DwarvenPowderOfTemperament() : this( 3 )
		{
		}

		[Constructable]
		public DwarvenPowderOfTemperament( int charges ) : base( 4102 )
		{
			Weight = 1.0;
			Hue = 2419;
			UsesRemaining = charges;
			Name = "Strengthening Compound";
		}

		public DwarvenPowderOfTemperament( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_UsesRemaining );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_UsesRemaining = reader.ReadInt();
					break;
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~
		}

		public virtual void DisplayDurabilityTo( Mobile m )
		{
			LabelToAffix( m, 1017323, AffixType.Append, ": " + m_UsesRemaining.ToString() ); // Durability
		}

		public override void OnSingleClick( Mobile from )
		{
			DisplayDurabilityTo( from );

			base.OnSingleClick( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.Target = new InternalTarget( this );
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}

		private class InternalTarget : Target
		{
			private DwarvenPowderOfTemperament m_Powder;

			public InternalTarget( DwarvenPowderOfTemperament powder ) : base( 2, false, TargetFlags.None )
			{
				m_Powder = powder;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Powder.Deleted || m_Powder.UsesRemaining <= 0 )
				{
					from.SendLocalizedMessage( 1049086 ); // You have used up your powder of temperament.
					return;
				}

				if ( targeted is BaseArmor /*&& (DefBlacksmithy.CraftSystem.CraftItems.SearchForSubclass( targeted.GetType() ) != null)*/ )
				{
					BaseArmor ar = (BaseArmor)targeted;

					if ( ar.IsChildOf( from.Backpack ) && m_Powder.IsChildOf( from.Backpack ) )
					{
						int origMaxHP = ar.MaxHitPoints;
						int origCurHP = ar.HitPoints;

						int initMaxHP = Core.AOS ? 255 : ar.InitMaxHits;

						ar.UnscaleDurability();

						if ( ar.MaxHitPoints < initMaxHP )
						{
							int bonus = initMaxHP - ar.MaxHitPoints;

							if ( bonus > 15 )
								bonus = 15;

							ar.MaxHitPoints += bonus;
							ar.HitPoints += bonus;

							ar.ScaleDurability();

							if ( ar.MaxHitPoints > 255 ) ar.MaxHitPoints = 255;
							if ( ar.HitPoints > 255 ) ar.HitPoints = 255;

							if ( ar.MaxHitPoints > origMaxHP )
							{
								from.SendLocalizedMessage( 1049084 ); // You successfully use the powder on the item.

								--m_Powder.UsesRemaining;

								if ( m_Powder.UsesRemaining <= 0 )
								{
									from.SendLocalizedMessage( 1049086 ); // You have used up your powder of temperament.
									m_Powder.Delete();
								}
							}
							else
							{
								ar.MaxHitPoints = origMaxHP;
								ar.HitPoints = origCurHP;
								from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							}
						}
						else
						{
							from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							ar.ScaleDurability();
						}
					}
					else
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
				}
				else if ( targeted is BaseWeapon /*&& (DefBlacksmithy.CraftSystem.CraftItems.SearchForSubclass( targeted.GetType() ) != null)*/ )
				{
					BaseWeapon wep = (BaseWeapon)targeted;

					if ( wep.IsChildOf( from.Backpack ) && m_Powder.IsChildOf( from.Backpack ) )
					{
						int origMaxHP = wep.MaxHits;
						int origCurHP = wep.Hits;

						int initMaxHP = Core.AOS ? 255 : wep.InitMaxHits;

						wep.UnscaleDurability();

						if ( wep.MaxHits < initMaxHP )
						{
							int bonus = initMaxHP - wep.MaxHits;

							if ( bonus > 15 )
								bonus = 15;

							wep.MaxHits += bonus;
							wep.Hits += bonus;

							wep.ScaleDurability();

							if ( wep.MaxHits > 255 ) wep.MaxHits = 255;
							if ( wep.Hits > 255 ) wep.Hits = 255;

							if ( wep.MaxHits > origMaxHP )
							{
								from.SendLocalizedMessage( 1049084 ); // You successfully use the powder on the item.

								--m_Powder.UsesRemaining;

								if ( m_Powder.UsesRemaining <= 0 )
								{
									from.SendLocalizedMessage( 1049086 ); // You have used up your powder of temperament.
									m_Powder.Delete();
								}
							}
							else
							{
								wep.MaxHits = origMaxHP;
								wep.Hits = origCurHP;
								from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							}
						}
						else
						{
							from.SendLocalizedMessage( 1049085 ); // The item cannot be improved any further.
							wep.ScaleDurability();
						}
					}
					else
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
				}
				else
				{
					from.SendLocalizedMessage( 1049083 ); // You cannot use the powder on that item.
				}
			}
		}
	}
	
	[FlipableAttribute( 0xE86, 0xE85 )]
	public class DwarvenPickaxe : BaseAxe
	{
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 15; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 39; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public DwarvenPickaxe() : base( 0xE86 )
		{
			Weight = 11.0;
			UsesRemaining = 70;
			ShowUsesRemaining = true;
			Name = "Dwarven Pickaxe";
		}

		public DwarvenPickaxe( Serial serial ) : base( serial )
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
			ShowUsesRemaining = true;
		}
	}

	public class DwarvenAle : BeverageBottle
	{

		[Constructable]
		public DwarvenAle(): base( BeverageType.Wine)
		{
			Weight = 1;
			Name = "Troll Liver Ale";
			Hue = 148;
		}

		public DwarvenAle( Serial serial ) : base( serial )
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

	public class TrollLiver : Item
	{

		[Constructable]
		public TrollLiver() : this( 1 )
		{
		}

		[Constructable]
		public TrollLiver( int amount ) : base( 0x1CEE )
		{
			Stackable = true;
			Weight = 0.5;
			Amount = amount;
			Name = "Troll Liver";
		}

		public TrollLiver( Serial serial ) : base( serial )
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
    
    [Server.Engines.Craft.Forge]
    public class DwarvenForgeAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new DwarvenForgeDeed(); } }

        [Constructable]
        public DwarvenForgeAddon()
        {
            AddComponent(new DwarvenForge(), 0, 0, 0);
        }

        public DwarvenForgeAddon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class DwarvenForgeDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new DwarvenForgeAddon(); } }

        [Constructable]
        public DwarvenForgeDeed()
        {
            Name = "Dwarven Forge Deed";
        }

        public DwarvenForgeDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [Server.Engines.Craft.Forge]
    public class DwarvenForge : AddonComponent
    {
        [Constructable]
		public DwarvenForge() : base( 4017 )
		{
            Name = "dwarven forge";
            Hue = 2419;
			Movable = false;
		}

        public DwarvenForge( Serial serial ) : base( serial )
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

	public class DwarvenBeer : BeverageBottle
	{
		[Constructable]
		public DwarvenBeer(): base( BeverageType.Ale)
		{
			Weight = 1;
			Name = "Granitebreaker Beer";
			Hue = 749;
		}

		public DwarvenBeer( Serial serial ) : base( serial )
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

	public class DwarvenRagePotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.DwarvenRage; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 10 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public DwarvenRagePotion() : base( PotionEffect.DwarvenRage )
		{
			Name = "Potion of Dwarven Rage";
			Hue = 432;
		}

		public DwarvenRagePotion( Serial serial ) : base( serial )
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

			if ( m_Player.CanDrink( PEffect ) )
			{
				StatMod DwarvenRageSTRMod = new StatMod( StatType.Str, (int)m_Player.Serial + " Rage Str", 50, TimeSpan.FromSeconds( 30 ) );
				m_Player.AddStatMod( DwarvenRageSTRMod );

				StatMod DwarvenRageDEXMod = new StatMod( StatType.Dex, (int)m_Player.Serial + " Rage Dex", 50, TimeSpan.FromSeconds( 30 ) );
				m_Player.AddStatMod( DwarvenRageDEXMod );

				StatMod DwarvenRageINTMod = new StatMod( StatType.Int, (int)m_Player.Serial + " Rage Int", 50, TimeSpan.FromSeconds( 30 ) );
				m_Player.AddStatMod( DwarvenRageINTMod );
			}

			base.Drink( from );
		}
	}

	public class DwarvenOrePotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.OreRefinement; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromSeconds( 30 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public DwarvenOrePotion() : base( PotionEffect.OreRefinement )
		{
			Name = "Ore Refinement Potion";
			Hue = 637;
		}

		public DwarvenOrePotion( Serial serial ) : base( serial )
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
					m_Player.Target = new OreTarget( m_Player, this );
				}
			}
			else
				base.Drink( from );
		}
	}

	public class DwarvenBolt : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} iron bolt" : "{0} iron bolts", Amount );
			}
		}

		[Constructable]
		public DwarvenBolt() : this( 1 )
		{
		}

		[Constructable]
		public DwarvenBolt( int amount ) : base( 0x1BFB )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Name = "Iron Bolt";
			Hue = 2419;
		}

		public DwarvenBolt( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new DwarvenBolt( amount ), amount );
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

	public class DwarvenBallistaBolt : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Ballista bolt" : "{0} Ballista bolts", Amount );
			}
		}

		[Constructable]
		public DwarvenBallistaBolt() : this( 1 )
		{
		}

		[Constructable]
		public DwarvenBallistaBolt( int amount ) : base( 0x1BFB )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Name = "Ballista Bolt";
			Hue = 2418;
		}

		public DwarvenBallistaBolt( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new DwarvenBallistaBolt( amount ), amount );
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

	[FlipableAttribute( 0xF50, 0xF4F )]
	public class DwarvenCrossbow : BaseRanged
	{
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( DwarvenBolt ); } }
		public override Item Ammo{ get{ return new DwarvenBolt(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 65; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 20; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 5; } }

		public override int InitMinHits{ get{ return 61; } }
		public override int InitMaxHits{ get{ return 100; } }

		[Constructable]
		public DwarvenCrossbow() : base( 0xF50 )
		{
			Weight = 9.0;
			Layer = Layer.TwoHanded;
			Name = "Dwarven Crossbow";
		}

		public DwarvenCrossbow( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x13FD, 0x13FC )]
	public class DwarvenBallista : BaseRanged
	{
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( DwarvenBallistaBolt ); } }
		public override Item Ammo{ get{ return new DwarvenBallistaBolt(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 25; } }
		public override int AosSpeed{ get{ return 10; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 11; } }
		public override int OldMaxDamage{ get{ return 56; } }
		public override int OldSpeed{ get{ return 10; } }

		public override int DefMaxRange{ get{ return 12; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }
		
		private BaseAddon m_Ballista;

		[CommandProperty( AccessLevel.GameMaster )]
		public BaseAddon Ballista
		{
			get{ return m_Ballista; }
			set{ m_Ballista = value; InvalidateProperties(); }
		}
		

		[Constructable]
		public DwarvenBallista() : base( 0x13FD )
		{
			Weight = 9.0;
			Layer = Layer.TwoHanded;
			Name = "Ballista Controller";
		}

		public DwarvenBallista( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (BaseAddon)m_Ballista);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Ballista = (BaseAddon)reader.ReadItem();
		}
	}

}

namespace Server.Targets
{

	public class OreTarget : Target
	{
		private TeiravonMobile m_Player;
		private BaseOre ore;
		private DwarvenOrePotion opot;
        private TAVOre m_Ore;
		
		public OreTarget( TeiravonMobile from, DwarvenOrePotion pot) : base( -1, false, TargetFlags.None )
		{
			m_Player = from;
			opot = pot;
		}

		protected override void OnTarget( Mobile from, object targ )
		{
			if (!(targ is BaseOre))
				from.SendMessage("This can only be used on ore.");
            else if (targ is TAVOre)
            {
                m_Ore = targ as TAVOre ;

                if (!from.InRange(m_Ore.GetWorldLocation(), 2))
                {
                    from.SendLocalizedMessage(501976); // The ore is too far away.
                    return;
                }

                int toConsume = m_Ore.Amount;
                int Volume = 0;
                int count = 0;
                double Success = 2;
                //from.Say("success value : {0}%", Success* 100);   -- Debug Text for Smelting

                for (int i = 0; i < TAVOre.GetLength(m_Ore); ++i)
                {
                    Volume = (int)(toConsume * Success * TAVOre.GetCon(i, m_Ore));
                    if (Volume > 0)
                    {
                        Item ingot = TAVOre.GetMetals(i, m_Ore);
                        ingot.Amount = Volume;
                        from.AddToBackpack(ingot);

                        count++;
                    }
                }

                m_Ore.Consume(toConsume);
                opot.TargetDrink = true;
                opot.Drink(from);
            }
            else
			{
				ore = (BaseOre)targ;

				if ( !from.InRange( ore.GetWorldLocation(), 2 ) )
				{
					from.SendLocalizedMessage( 501976 ); // The ore is too far away.
					return;
				}
		
				int toConsume = ore.Amount;
				
				if (toConsume > 50)
					toConsume = 50;
						
				BaseIngot ingot = ore.GetIngot();
				ingot.Amount = toConsume * 2;

				ore.Consume( toConsume );
				from.AddToBackpack( ingot );
				opot.TargetDrink = true;
				opot.Drink(from);
			}
		}
	}
}
