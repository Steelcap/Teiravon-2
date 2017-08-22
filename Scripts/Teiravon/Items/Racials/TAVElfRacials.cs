using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Engines.Craft;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Items
{

	public class ElvenCloak : BaseCloak
	{
		private SkillMod m_Skilmod;
		
		[Constructable]
		public ElvenCloak() : this( 0 )
		{
		}

		[Constructable]
		public ElvenCloak( int hue ) : base( 0x1515, hue )
		{
			Weight = 5.0;
			Name = "Elven Cloak";
		}

		public ElvenCloak( Serial serial ) : base( serial )
		{
		}
		
		public override void OnAdded (object player)
		{
			if (player is TeiravonMobile)
			{
				TeiravonMobile m_player = (TeiravonMobile)player;
				
				if ( m_Skilmod != null )
					m_Skilmod.Remove();

				m_Skilmod = new DefaultSkillMod( SkillName.Stealth, true, 25.0 );
				m_player.AddSkillMod( m_Skilmod );
			}
		}

		public override void OnRemoved (object player)
		{
			if (player is TeiravonMobile)
			{
				TeiravonMobile m_player = (TeiravonMobile)player;
		
				if ( m_Skilmod != null )
					m_Skilmod.Remove();

				m_Skilmod = null;
			}
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

			if (Parent is TeiravonMobile)
			{
				m_Skilmod = new DefaultSkillMod( SkillName.Hiding, true, 25.0 );
				((Mobile)Parent).AddSkillMod( m_Skilmod );
			}
		}
	}
	
	[FlipableAttribute( 0x170b, 0x170c )]
	public class ElvenBoots : BaseShoes
	{
		private SkillMod m_Skilmod;

		[Constructable]
		public ElvenBoots() : this( 0 )
		{
		}

		[Constructable]
		public ElvenBoots( int hue ) : base( 0x170B, hue )
		{
			Weight = 3.0;
			Name = "Elven Boots";
		}
		
		public override void OnAdded (object player)
		{
			if (player is TeiravonMobile)
			{
				TeiravonMobile m_player = (TeiravonMobile)player;
				
				if ( m_Skilmod != null )
					m_Skilmod.Remove();

				m_Skilmod = new DefaultSkillMod( SkillName.Stealth, true, 25.0 );
				m_player.AddSkillMod( m_Skilmod );
			}
		}

		public override void OnRemoved (object player)
		{
			if (player is TeiravonMobile)
			{
				TeiravonMobile m_player = (TeiravonMobile)player;
		
				if ( m_Skilmod != null )
					m_Skilmod.Remove();

				m_Skilmod = null;
			}
		}

		public ElvenBoots( Serial serial ) : base( serial )
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

			if (Parent is TeiravonMobile)
			{
				m_Skilmod = new DefaultSkillMod( SkillName.Stealth, true, 25.0 );
				((Mobile)Parent).AddSkillMod( m_Skilmod );
			}
		}
	}

	[FlipableAttribute( 11550, 11562 )]
	public class Elvenbow : BaseRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }


		public override int AosStrengthReq{ get{ return 0; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 37; } }
		public override int DefMaxRange{ get{ return 20; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		public override int InitMinHits{ get{ return 70; } }
		public override int InitMaxHits{ get{ return 90; } }

		[Constructable]
		public Elvenbow() : base( 11550 )
		{
			Name = "Elven Bow";
			Weight = 4.0;
			Layer = Layer.TwoHanded;
			Attributes.DefendChance += 10;
		}

		public Elvenbow( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			if (ItemID == 9922)
				ItemID = 11550;
		}
	}
	
	[Flipable( 11010, 11011 )]
	public class ElvenQuiver : BodySash
	{
		[Constructable]
		public ElvenQuiver() : this( 0 )
		{
		}

		[Constructable]
		public ElvenQuiver( int hue ) : base( 11010 )
		{
			Weight = 1.0;
			Name = "Elven Quiver";
		}

		public ElvenQuiver( Serial serial ) : base( serial )
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
			
			if (ItemID == 5441)
				ItemID = 11010;
		}
	}

	[FlipableAttribute( 0x13bf, 0x13c4 )]
	public class ElvenChainChest : BaseArmor
	{
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
		public ElvenChainChest() : base( 0x13BF )
		{
			Weight = 5.0;
			Name = "Elven Chainmail Tunic";
			ArmorAttributes.MageArmor = 1;
		}

		public ElvenChainChest( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x13be, 0x13c3 )]
	public class ElvenChainLegs : BaseArmor
	{
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
		public ElvenChainLegs() : base( 0x13BE )
		{
			Weight = 5.0;
			Name = "Elven Chainmail Legs";
			ArmorAttributes.MageArmor = 1;
		}

		public ElvenChainLegs( Serial serial ) : base( serial )
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
	
	[FlipableAttribute( 0x13BB, 0x13C0 )]
	public class ElvenChainCoif : BaseArmor
	{
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
		public ElvenChainCoif() : base( 0x13BB )
		{
			Weight = 1.0;
			Name = "Elven Chainmail Coif";
			ArmorAttributes.MageArmor = 1;
		}

		public ElvenChainCoif( Serial serial ) : base( serial )
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
	
	[FlipableAttribute( 11561, 11573 )]
	public class ElvenLongsword : BaseSword
	{
		public override int AosStrengthReq{ get{ return 15; } }
		public override int AosMinDamage{ get{ return 14; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 40; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 61; } }
		public override int InitMaxHits{ get{ return 90; } }

		[Constructable]
		public ElvenLongsword() : base( 11561 )
		{
			Weight = 1.0;
			Name = "Elven Longsword";
			Attributes.SpellChanneling = 1;
			WeaponAttributes.MageWeapon = 20;
		}

		public ElvenLongsword( Serial serial ) : base( serial )
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
			
			if (ItemID == 5048)
				ItemID = 11561;

            if (version < 1)
            {
                MinDamage = 14;
                MaxDamage = 15;
                Speed = 40;
                StrRequirement = 15;
            }
		}
	}
	
	public class ElvenFaeWine : BeverageBottle
	{

		[Constructable]
		public ElvenFaeWine(): base( BeverageType.Wine)
		{
			Weight = 1;
			Name = "Fae Wine";
			Hue = 2271;
		}

		public ElvenFaeWine( Serial serial ) : base( serial )
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
	
	public class ElvenAle : BeverageBottle
	{
		[Constructable]
		public ElvenAle(): base( BeverageType.Ale)
		{
			Weight = 1;
			Name = "Winterfrost Ale";
			Hue = 2375;
		}

		public ElvenAle( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x27A8, 0x27F3 )]
	public class ElvenPracticeSword : BaseSword
	{

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 1; } }
		public override int AosMaxDamage{ get{ return 2; } }
		public override int AosSpeed{ get{ return 55; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 11; } }
		public override int OldSpeed{ get{ return 53; } }

		public override int DefHitSound{ get{ return 0x536; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 50; } }

		[Constructable]
		public ElvenPracticeSword() : base( 0x27A8 )
		{
			Weight = 3.0;
			Name = "Elven Practice Sword";
			Hue = 342;
		}

		public ElvenPracticeSword( Serial serial ) : base( serial )
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
	
		public class ElvenShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 2; } }
		public override int BaseColdResistance{ get{ return 2; } }
		public override int BasePoisonResistance{ get{ return 2; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 25; } }

		public override int AosStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 8; } }

		[Constructable]
		public ElvenShield() : base( 0x1B7A )
		{
			Name = "Elven Shield";
			Weight = 5.0;
			Attributes.SpellChanneling = 1;
			ArmorAttributes.MageArmor = 1;
		}

		public ElvenShield( Serial serial ) : base(serial)
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
	
	[FlipableAttribute( 0xF43, 0xF44 )]
	public class ElvenHatchet : BaseAxe
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 14; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 45; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldMinDamage{ get{ return 2; } }
		public override int OldMaxDamage{ get{ return 17; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		[Constructable]
		public ElvenHatchet() : base( 0xF43 )
		{
			Weight = 4.0;
			Name = "Elven Hatchet";
		}

		public ElvenHatchet( Serial serial ) : base( serial )
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
	
	public class ElvenAmulet : BaseNecklace
	{
		[Constructable]
		public ElvenAmulet() : base( 0x1089 )
		{
			Weight = 0.1;
			Name = "Amulet of Protection";
			SkillBonuses.SetValues(4,SkillName.MagicResist,20);
		}

		public ElvenAmulet( Serial serial ) : base( serial )
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
	
	public class ElvenHealPotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.ElvenElixer; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 10 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public ElvenHealPotion() : base( PotionEffect.ElvenElixer )
		{
			Name = "Elixer of Life";
			Hue = 53;
		}

		public ElvenHealPotion( Serial serial ) : base( serial )
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
				m_Player.SendMessage( "You feel a warmth spread throughout your body." );

				m_Player.Hits = m_Player.MaxHits;
			}

			base.Drink( from );
		}
	}

	public class ElvenPlantPotion : TeiravonPotion
	{
		public override PotionEffect PEffect{ get { return PotionEffect.PlantGrowth; } }
		public override TimeSpan Duration{ get { return TimeSpan.FromMinutes( 1 ); } }
		public override bool Racial{ get { return true; } }

		[Constructable]
		public ElvenPlantPotion() : base( PotionEffect.PlantGrowth )
		{
			Name = "A Plant Growth Potion";
			Hue = 2478;
		}

		public ElvenPlantPotion( Serial serial ) : base( serial )
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
					m_Player.Target = new PlantTarget( m_Player, this );
				}
			}
			else
				base.Drink( from );
		}
	}
	
}

namespace Server.Targets
{

	public class PlantTarget : Target
	{
		private TeiravonMobile m_Player;
		private PlantItem plant;
		private ElvenPlantPotion pgpot;
		
		public PlantTarget( TeiravonMobile from, ElvenPlantPotion pot) : base( -1, false, TargetFlags.None )
		{
			m_Player = from;
			pgpot = pot;
		}

		protected override void OnTarget( Mobile from, object targ )
		{
			if (!(targ is PlantItem))
			{
				from.SendMessage( "That is not a plant!");
			}
			else
			{
				plant = (PlantItem)targ;
				if (plant.PlantStatus > PlantStatus.Stage6)
					from.SendMessage("That plant is fully grown!");
				else
				{
					switch (plant.PlantStatus)
					{
						case PlantStatus.BowlOfDirt:
							plant.PlantStatus = PlantStatus.Stage1; break;
						case PlantStatus.Stage1:
							plant.PlantStatus = PlantStatus.Stage2; break;
						case PlantStatus.Stage2:
							plant.PlantStatus = PlantStatus.Stage3; break;
						case PlantStatus.Stage3:
							plant.PlantStatus = PlantStatus.Stage4; break;
						case PlantStatus.Stage4:
							plant.PlantStatus = PlantStatus.Stage5; break;
						case PlantStatus.Stage5:
							plant.PlantStatus = PlantStatus.Stage6; break;
						case PlantStatus.Stage6:
							plant.PlantStatus = PlantStatus.Stage7; break;
						default:
							break;
					}
					pgpot.TargetDrink = true;
					pgpot.Drink(from);
				}
			}
		}
	}
}
