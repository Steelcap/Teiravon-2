using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Engines.Craft;
using Server.Items;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{

	[Flipable( 0x230A, 0x2309 )]
	public class HumanFurCloak : BaseCloak
	{
		[Constructable]
		public HumanFurCloak() : base( 0x230A)
		{
			Name = "Fur Cloak";
			Weight = 5.0;

			Resistances.Physical = 5;
			Resistances.Cold = 5;
			Resistances.Fire = -5;
		}

		public HumanFurCloak( Serial serial ) : base( serial )
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

	public class HumanStamBoots : Boots
	{
	
		[Constructable]
		public HumanStamBoots() : base( 0x170B )
		{
			Name = "Boots of Running";
			Attributes.RegenStam = 2;
		}
	
		public HumanStamBoots( Serial serial ) : base( serial )
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

	[FlipableAttribute( 11024, 11025 )]
	public class HumanFieldPlateHelm : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 75; } }
		public override int InitMaxHits{ get{ return 85; } }

		public override int AosStrReq{ get{ return 80; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HumanFieldPlateHelm() : base( 11024 )
		{
			Name = "Field Plate Helm";
			Weight = 7.0;
			DexBonus = -5;
            Attributes.DefendChance = 5;
		}

		public HumanFieldPlateHelm( Serial serial ) : base( serial )
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
				Weight = 5.0;
			if (ItemID == 5128)
				ItemID = 11024;
		}
	}

	[FlipableAttribute( 11020, 11021 )]
	public class HumanFieldPlateGloves : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 65; } }
		public override int InitMaxHits{ get{ return 75; } }

		public override int AosStrReq{ get{ return 70; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int OldDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HumanFieldPlateGloves() : base( 11020 )
		{
			Name = "Field Plate Gloves";
			Weight = 3.0;
			DexBonus = -3;
            Attributes.DefendChance = 3;
		}

		public HumanFieldPlateGloves( Serial serial ) : base( serial )
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
			if (ItemID == 5140)
				ItemID = 11020;
		}
	}

	[FlipableAttribute( 11022, 11023 )]
	public class HumanFieldPlateGorget : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override int AosStrReq{ get{ return 70; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HumanFieldPlateGorget() : base( 11022 )
		{
			Name = "Field Plate Gorget";
			Weight = 3.0;
			DexBonus = -3;
            Attributes.DefendChance = 3;
		}

		public HumanFieldPlateGorget( Serial serial ) : base( serial )
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
			
			if (ItemID == 5139)
				ItemID = 11022;
		}
	}

	[FlipableAttribute( 11018, 11019 )]
	public class HumanFieldPlateArms : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override int AosStrReq{ get{ return 90; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HumanFieldPlateArms() : base( 11018 )
		{
			Name = "Field Plate Arms";
			Weight = 7.0;
			DexBonus = -7;
            Attributes.DefendChance = 7;
		}

		public HumanFieldPlateArms( Serial serial ) : base( serial )
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
				Weight = 5.0;
			if (ItemID == 5136)
				ItemID = 11018;
		}
	}

	[FlipableAttribute( 11014, 11015 )]
	public class HumanFieldPlateLegs : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 70; } }
		public override int InitMaxHits{ get{ return 80; } }

		public override int AosStrReq{ get{ return 90; } }

		public override int OldStrReq{ get{ return 60; } }
		public override int OldDexBonus{ get{ return -6; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HumanFieldPlateLegs() : base( 11014 )
		{
			Name = "Field Plate Legs";
			Weight = 10.0;
			DexBonus = -7;
            Attributes.DefendChance = 7;
		}

		public HumanFieldPlateLegs( Serial serial ) : base( serial )
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
			
			if (ItemID == 5137)
				ItemID = 11014;
		}
	}

	[FlipableAttribute( 11016, 11017 )]
	public class HumanFieldPlateChest : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override int AosStrReq{ get{ return 100; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int OldDexBonus{ get{ return -8; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HumanFieldPlateChest() : base( 11016 )
		{
			Name = "Field Plate Chest";
			Weight = 15.0;
			DexBonus = -10;
            Attributes.DefendChance = 10;
		}

		public HumanFieldPlateChest( Serial serial ) : base( serial )
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
				Weight = 10.0;
			if ( ItemID == 5141 )
				ItemID = 11016;
		}
	}

	[FlipableAttribute( 11026, 11027 )]
	public class HumanFieldPlateBoots : BaseArmor
	{

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override int AosStrReq{ get{ return 100; } }
		public override int OldStrReq{ get{ return 60; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HumanFieldPlateBoots() : base( 11026 )
		{
			Name = "Field Plate Boots";
			Weight = 5.0;
			Attributes.RegenStam = 1;
			Attributes.WeaponDamage = 5;
            Attributes.DefendChance = 5;
		}

		public HumanFieldPlateBoots( Serial serial ) : base( serial )
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
		}
	}

	public class HumanBattleShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 1; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 1; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 125; } }

		public override int AosStrReq{ get{ return 80; } }

		public override int ArmorBase{ get{ return 30; } }

		[Constructable]
		public HumanBattleShield() : base( 11009 )
		{
			Name = "Battle Shield";
			Weight = 9.0;
			Attributes.DefendChance += 10;
		}

		public HumanBattleShield( Serial serial ) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			if ( ItemID == 7108 )
				ItemID = 11009;
		}

	}

	[FlipableAttribute( 0x26C0, 0x26CA )]
	public class HumanBandedLance : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Dismount; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }

		public override int AosStrengthReq{ get{ return 95; } }
		public override int AosMinDamage{ get{ return 19; } }
		public override int AosMaxDamage{ get{ return 20; } }
		public override int AosSpeed{ get{ return 20; } }

		public override int OldStrengthReq{ get{ return 95; } }
		public override int OldMinDamage{ get{ return 17; } }
		public override int OldMaxDamage{ get{ return 18; } }
		public override int OldSpeed{ get{ return 24; } }

		public override int DefMaxRange{ get{ return 2; } }

		public override int DefHitSound{ get{ return 0x23C; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 51; } }
		public override int InitMaxHits{ get{ return 110; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public HumanBandedLance() : base( 0x26C0 )
		{
			Name = "Banded Lance";
			Weight = 15.0;
			SkillBonuses.SetValues( 0, SkillName.Tactics, 10.0 );
		}

		public HumanBandedLance( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xF50, 0xF4F )]
	public class HumanCrossbow : BaseRanged
	{
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( HumanAPBolt ); } }
		public override Item Ammo{ get{ return new HumanAPBolt(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 95; } }
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
		public HumanCrossbow() : base( 0xF50 )
		{
			Weight = 8.0;
			Layer = Layer.TwoHanded;
			Name = "Armor Piercing Crossbow";
			Consecrated = true;
		}

		public HumanCrossbow( Serial serial ) : base( serial )
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

	public class HumanAPBolt : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} iron bolt" : "{0} armor piercing bolts", Amount );
			}
		}

		[Constructable]
		public HumanAPBolt() : this( 1 )
		{
		}

		[Constructable]
		public HumanAPBolt( int amount ) : base( 0x1BFB )
		{
			Stackable = true;
			Weight = 0.2;
			Amount = amount;
			Name = "Armor Piercing Bolt";
		}

		public HumanAPBolt( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new HumanAPBolt( amount ), amount );
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

	public class HumanWine : BeverageBottle
	{

		[Constructable]
		public HumanWine(): base( BeverageType.Wine)
		{
			Weight = 1;
			Name = "Mead";
		}

		public HumanWine( Serial serial ) : base( serial )
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
	
	public class HumanAle : BeverageBottle
	{
		[Constructable]
		public HumanAle(): base( BeverageType.Ale)
		{
			Weight = 1;
			Name = "Royal Ale";
		}

		public HumanAle( Serial serial ) : base( serial )
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

	public class HumanBrandy : BeverageBottle
	{
		[Constructable]
		public HumanBrandy(): base( BeverageType.Liquor)
		{
			Weight = 1;
			Name = "Peach Brandy";
		}

		public HumanBrandy( Serial serial ) : base( serial )
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

	public class HumanEnhancePotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.HumanEnhance; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 30 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public HumanEnhancePotion() : base( PotionEffect.HumanEnhance )
		{
			Name = "Potion Enhancer";
			Hue = 3;
		}

		public HumanEnhancePotion( Serial serial ) : base( serial )
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
				m_Player.SendMessage( "You feel a tingling sensation." );
				
			}

			base.Drink( from );
		}
	}

	public class HumanPetHealPotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.PetHeal; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 3 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public HumanPetHealPotion() : base( PotionEffect.PetHeal )
		{
			Name = "Pet Healing Potion";
			Hue = 347;
		}

		public HumanPetHealPotion( Serial serial ) : base( serial )
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
            if (from is TeiravonMobile)
            {
                TeiravonMobile m_Player = (TeiravonMobile)from;

                if (!this.TargetDrink)
                {
                    if (m_Player.CanDrink(PEffect))
                    {
                        from.SendMessage("Target the pet to heal.");
                        from.Target = new PetHealTarget((TeiravonMobile)from, this);
                    }
                }
                else
                    base.Drink(from);
            }
            else
                from.Hits += Utility.RandomMinMax(25, 50);
		}
	}

	
	public class HumanPortableForge : Item
	{

		[Constructable]
		public HumanPortableForge() : base( 0xFB1 )
		{
			Weight = 50;
			Name = "Portable Forge";
		}

		public HumanPortableForge( Serial serial ) : base( serial )
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

	public class HumanPortableAnvil : Item
	{

		[Constructable]
		public HumanPortableAnvil() : base( 0xFAF )
		{
			Weight = 50;
			Name = "Portable Anvil";
		}

		public HumanPortableAnvil( Serial serial ) : base( serial )
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

	public class HumanSaddlebags : Item
	{

		[Constructable]
		public HumanSaddlebags() : base( 0xE75 )
		{
			Weight = 5;
			Name = "Saddle bag";
			Hue = 1001;
		}

		public HumanSaddlebags( Serial serial ) : base( serial )
		{
			
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendMessage("Target the horse to equip with this.");
				from.Target = new SaddlebagsTarget( this, (TeiravonMobile)from);
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in yourpack for you to use it.
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

}

namespace Server.Targets
{
	public class SaddlebagsTarget : Target
	{
		private TeiravonMobile m_Player;
		private HumanSaddlebags bags;
		private BaseMount horsie;
			
		public SaddlebagsTarget(Item sbags, TeiravonMobile from): base( -1, false, TargetFlags.None )
		{
			m_Player = (TeiravonMobile)from; 
			bags = (HumanSaddlebags)sbags;
		}

		protected override void OnTarget( Mobile from, object targ )  
		{  
			if (!(targ is Mobile))
				from.SendMessage("You can only target mounts without bags with this.");
			else
			{
                if (!(targ is BaseMount) || targ is PackAnimal)
                    from.SendMessage("you can't put a saddlebag on that.");
                else if (XmlAttach.FindAttachmentOnMobile(horsie, typeof(XmlData), "saddlebag") != null)
                    from.SendMessage("That already has a bag on it.");
                else
                {
                    horsie = (BaseMount)targ;
                    XmlAttach.AttachTo(horsie, new XmlData("saddlebag"));

                    Container pack = horsie.Backpack;

                    if (pack != null)
                        pack.Delete();

                    pack = new StrongBackpack();
                    pack.Movable = false;

                    horsie.AddItem(pack);


                    bags.Delete();
                    from.SendMessage("You put the saddlebags on the {0}",horsie.ToString());
                }
			}
		
		}
	}
	
	public class PetHealTarget : Target
	{
		private TeiravonMobile m_Player;
		private BaseCreature pet;
		private HumanPetHealPotion phpot;
			
		public PetHealTarget(TeiravonMobile from, HumanPetHealPotion pot): base( -1, false, TargetFlags.None )
		{
			m_Player = (TeiravonMobile)from; 
			phpot = pot;
		}

		protected override void OnTarget( Mobile from, object targ )  
		{  
			if (!(targ is Mobile))
				from.SendMessage("You can't heal that!");
			else if (targ is BaseCreature)
			{
				pet = (BaseCreature)targ;
				if (!pet.Controled)
					from.SendMessage("You can only heal tamed creatures");
				else if (!(m_Player.InRange( pet.Location, 2 )))
					m_Player.SendMessage("You are too far from your pet");
				else
				{
					pet.Hits += Utility.RandomMinMax(50, 150);
					phpot.TargetDrink = true;
					from.SendMessage("You heal your pet!");
					phpot.Drink(from);
				}
			}
			else
				from.SendMessage("You can only heal creatures");
		}
	}
	
}
