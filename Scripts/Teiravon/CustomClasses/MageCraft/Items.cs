using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Eighth;
using Server.Network;

namespace Teiravon.Magecraft
{
	public class CreationOrb : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefMagecraft.CraftSystem; } }

		public override void OnDoubleClick( Mobile from )
		{
			TeiravonMobile m_Player = (TeiravonMobile)from;
			
			if ( m_Player.HasFeat( TeiravonMobile.Feats.ArcaneEnchantment ) )
				base.OnDoubleClick( from );
			else
				from.SendMessage( "You can't use this." );
		}

		[Constructable]
		public CreationOrb() : base( 30, 3630 )
		{
			Name = "Creation Orb";
		}
		
		public CreationOrb( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	public class ChargedExpGem : Item
	{
		[Constructable]
		public ChargedExpGem() : base( 6252 )
		{
			Name = "Charged Experience Gem";
		}
		
		public ChargedExpGem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	public class ExpGem : Item
	{
		public override void OnDoubleClick( Mobile from )
		{
			TeiravonMobile m_Player = (TeiravonMobile)from;
			
			if ( m_Player.PlayerExp >= 500 )
			{
				ChargedExpGem ceg = new ChargedExpGem();
				m_Player.Backpack.AddItem( ceg );
				
				m_Player.PlayerExp -= 500;
				m_Player.SendMessage( "You charge the gem with your experience." );
				
				this.Delete();
			} else
				m_Player.SendMessage( "You don't have enough experience to do this." );
		}

		[Constructable]
		public ExpGem() : base( 6251 )
		{
			Name = "Experience Gem";
		}
		
		public ExpGem( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}

	public class ChargedSkillGem : Item
	{
		[Constructable]
		public ChargedSkillGem() : base( 6250 )
		{
			Name = "Charged Skill Gem";
		}
		
		public ChargedSkillGem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	public class SkillGem : Item
	{
		public override void OnDoubleClick( Mobile from )
		{
			TeiravonMobile m_Player = (TeiravonMobile)from;
			
			if ( m_Player.Skills.Magery.Base >= 2.5 )
			{
				ChargedSkillGem csg = new ChargedSkillGem();
				m_Player.Backpack.AddItem( csg );
				
				m_Player.Skills.Magery.Base -= 2.5;
				m_Player.SendMessage( "You charge the gem with your skill." );
				
				this.Delete();
			}
			else 
				from.SendMessage( "You don't have enough skill to do this." );
		}

		[Constructable]
		public SkillGem() : base( 6249 )
		{
			Name = "Skill Gem";
		}
		
		public SkillGem( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	
	public class LesserManaBracer : SilverBracelet
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
				
			return false;
		}

		[Constructable]
		public LesserManaBracer()
		{
			Name = "Lesser Mana Bracer";
			
			this.Attributes.BonusMana = 10;
			this.Attributes.RegenMana = 1;
		}
		
		public LesserManaBracer( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class ManaBracer : SilverBracelet
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
				
			return false;
		}

		[Constructable]
		public ManaBracer()
		{
			Name = "Mana Bracer";
			
			this.Attributes.BonusMana = 15;
			this.Attributes.RegenMana = 3;
		}
		
		public ManaBracer( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class GreaterManaBracer : SilverBracelet
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
				
			return false;
		}

		[Constructable]
		public GreaterManaBracer()
		{
			Name = "Greater Mana Bracer";
			
			this.Attributes.BonusMana = 20;
			this.Attributes.RegenMana = 5;
		}
		
		public GreaterManaBracer( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	public class LegendaryManaBracer : SilverBracelet
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ( ( TeiravonMobile )from ).IsMage() )
				return true;

			return false;
		}

		[Constructable]
		public LegendaryManaBracer()
		{
			Name = "Legendary Mana Bracer";

			this.Attributes.BonusMana = 20;
			this.SkillBonuses.Skill_1_Name = SkillName.Meditation;
			this.SkillBonuses.Skill_1_Value = 50.0;
		}

		public LegendaryManaBracer( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			this.SkillBonuses.Skill_1_Value = 50.0;
		}
	}
	
	public class LesserRingOfElementalPower : SilverRing
	{
		DefaultSkillMod sm = new DefaultSkillMod( SkillName.MagicResist, true, -10.0 );
		
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
			{
				from.AddSkillMod( sm );
				return true;
			}
			
			return false;
		}
		
		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				
				from.RemoveSkillMod( sm );
			}
			
			base.OnRemoved( parent );
		}
		
		[Constructable]
		public LesserRingOfElementalPower()
		{
			Name = "Lesser Ring Of Elemental Power";
			
			this.SkillBonuses.Skill_1_Name = SkillName.Magery;
			this.SkillBonuses.Skill_1_Value = 5.0;
			
			this.SkillBonuses.Skill_2_Name = SkillName.EvalInt;
			this.SkillBonuses.Skill_2_Value = 5.0;
		}
		
		public LesserRingOfElementalPower( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class RingOfElementalPower : SilverRing
	{
		DefaultSkillMod sm = new DefaultSkillMod( SkillName.MagicResist, true, -10.0 );
		
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
			{
				from.AddSkillMod( sm );
				return true;
			}
			
			return false;
		}
		
		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				
				from.RemoveSkillMod( sm );
			}
			
			base.OnRemoved( parent );
		}
			
		[Constructable]
		public RingOfElementalPower()
		{
			Name = "Ring Of Elemental Power";
			
			this.SkillBonuses.Skill_1_Name = SkillName.Magery;
			this.SkillBonuses.Skill_1_Value = 10.0;
			
			this.SkillBonuses.Skill_2_Name = SkillName.EvalInt;
			this.SkillBonuses.Skill_2_Value = 10.0;
		}
		
		public RingOfElementalPower( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class GreaterRingOfElementalPower : SilverRing
	{
		DefaultSkillMod sm = new DefaultSkillMod( SkillName.MagicResist, true, -10.0 );
		
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
			{
				from.AddSkillMod( sm );
				return true;
			}
			
			return false;
		}
		
		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				
				from.RemoveSkillMod( sm );
			}
			
			base.OnRemoved( parent );
		}

		[Constructable]
		public GreaterRingOfElementalPower()
		{
			Name = "Greater Ring Of Elemental Power";
			
			this.SkillBonuses.Skill_1_Name = SkillName.Magery;
			this.SkillBonuses.Skill_1_Value = 15.0;
			
			this.SkillBonuses.Skill_2_Name = SkillName.EvalInt;
			this.SkillBonuses.Skill_2_Value = 15.0;
		}
		
		public GreaterRingOfElementalPower( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	

	public class WardingNecklace : SilverNecklace
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
			
			return false;
		}
		
		[Constructable]
		public WardingNecklace()
		{
			Name = "Warding Necklace";
			
			this.SkillBonuses.Skill_1_Name = SkillName.MagicResist;
			this.SkillBonuses.Skill_1_Value = 10.0;
		}
		
		public WardingNecklace( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	
	public class EnchantedWizardsHat : WizardsHat
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
			
			return false;
		}
		
		[Constructable]
		public EnchantedWizardsHat()
		{
			Name = "Enchanted Wizards Hat";
			
			this.SkillBonuses.Skill_1_Name = SkillName.Magery;
			this.SkillBonuses.Skill_1_Value = 5.0;
			
			this.SkillBonuses.Skill_2_Name = SkillName.EvalInt;
			this.SkillBonuses.Skill_2_Value = 5.0;
		}
		
		public EnchantedWizardsHat( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	
	public class MasteringTheArcaneI : BaseClothing
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
			
			return false;
		}
		
		[Constructable]
		public MasteringTheArcaneI() : base( 3834, Layer.OneHanded )
		{
			Name = "Mastering the Arcane: Volume I";
			
			this.Attributes.CastSpeed = 1;
			this.Attributes.SpellDamage = 5;
			this.Attributes.SpellChanneling = 1;
		}
		
		public MasteringTheArcaneI( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	public class MasteringTheArcaneII : BaseClothing
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
			
			return false;
		}
		
		[Constructable]
		public MasteringTheArcaneII() : base( 3834, Layer.OneHanded )
		{
			Name = "Mastering the Arcane: Volume II";
			
			this.Attributes.CastSpeed = 3;
			this.Attributes.SpellDamage = 10;
			this.Attributes.SpellChanneling = 1;
		}
		
		public MasteringTheArcaneII( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class MasteringTheArcaneIII : BaseClothing
	{
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
				return true;
			
			return false;
		}
		
		[Constructable]
		public MasteringTheArcaneIII() : base( 3834, Layer.OneHanded )
		{
			Name = "Mastering the Arcane: Volume III";
			
			this.Attributes.CastSpeed = 5;
			this.Attributes.SpellDamage = 15;
			this.Attributes.SpellChanneling = 1;
		}
		
		public MasteringTheArcaneIII( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}

	public class MasteringTheArcaneIV : BaseArmor
	{
		public override ArmorMaterialType MaterialType
		{
			get { return ArmorMaterialType.Cloth; }
		}

		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ( ( TeiravonMobile )from ).IsMage() )
				return true;

			return false;
		}

		[Constructable]
		public MasteringTheArcaneIV()
			: base( 3834 )
		{
			Name = "Mastering the Arcane: Volume IV";

			this.Layer = Layer.OneHanded;
			this.Attributes.CastSpeed = 5;
			this.Attributes.SpellDamage = 15;
			this.Attributes.SpellChanneling = 1;
			this.SkillBonuses.Skill_1_Name = SkillName.Meditation;
			this.SkillBonuses.Skill_1_Value = 50.0;
		}

		public MasteringTheArcaneIV( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			this.SkillBonuses.Skill_1_Value = 50.0;
		}
	}

	public class RobeOfReflection : Robe
	{
		DefaultSkillMod sm = new DefaultSkillMod( SkillName.MagicResist, true, -10.0 );
		
		public override bool OnEquip( Mobile from )
		{
			if ( from is TeiravonMobile && ((TeiravonMobile)from).IsMage() )
			{
				from.AddSkillMod( sm );
				return true;
			}
			
			return false;
		}
		
		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				
				from.RemoveSkillMod( sm );
			}
			
			base.OnRemoved( parent );
		}
		
		[Constructable]
		public RobeOfReflection() : base( 3834 )
		{
			Name = "Robe of Reflection";
			
			this.MaxArcaneCharges = 30;
			this.CurArcaneCharges = 10;
			this.Quality = ClothingQuality.Exceptional;
		}
			
		public RobeOfReflection( Serial serial ) : base( serial )
		{
		}
				
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			this.Quality = ClothingQuality.Exceptional;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	
	
	public class EmptyWand : BaseWand
	{
		private string m_Spell = null;
		private double m_Strength = 6.0;
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public string Spell { get { return m_Spell; } set { m_Spell = value; Name = value; } }
		
		[CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
		public double Strength { get { return m_Strength; } set { m_Strength = value; } }

        public override TimeSpan GetUseDelay
        {
            get
            {
                return TimeSpan.FromSeconds(Strength);
            }
        }
		
		[Constructable]
		public EmptyWand() : base( WandEffect.None, 5, 10 )
		{
			Name = "Empty Wand";
		}
		
		public EmptyWand( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( m_Spell );
			writer.Write( m_Strength );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			m_Spell = reader.ReadString();
			m_Strength = reader.ReadDouble();
		}

		public override void OnWandUse( Mobile from )
		{
            if (!from.CanBeginAction(typeof(BaseWand)))
                return;
            ConsumeCharge(from);
			#region Cast Spell
			switch ( m_Spell )
			{
				case "Agility":
					Cast( new AgilitySpell( from, this ) );
					break;
					
				case "Air Elemental":
					Cast( new AirElementalSpell( from, this ) );
					break;
					
				case "Arch Cure":
					Cast( new ArchCureSpell( from, this ) );
					break;
					
				case "Arch Protection":
					Cast( new ArchProtectionSpell( from, this ) );
					break;
					
				case "Bless":
					Cast( new BlessSpell( from, this ) );
					break;
					
				case "Chain Lightning":
					Cast( new ChainLightningSpell( from, this ) );
					break;
					
				case "Clumsy":
					Cast( new ClumsySpell( from, this ) );
					break;
					
				case "Create Food":
					Cast( new CreateFoodSpell( from, this ) );
					break;
					
				case "Cunning":
					Cast( new CunningSpell( from, this ) );
					break;
					
				case "Cure":
					Cast( new CureSpell( from, this ) );
					break;
					
				case "Curse":
					Cast( new CurseSpell( from, this ) );
					break;
					
				case "Dispel":
					Cast( new DispelSpell( from, this ) );
					break;
					
				case "Dispel Field":
					Cast( new DispelFieldSpell( from, this ) );
					break;
					
				case "Earth Elemental":
					Cast( new EarthElementalSpell( from, this ) );
					break;
					
				case "Earthquake":
					Cast( new EarthquakeSpell( from, this ) );
					break;
					
				case "Energy Bolt":
					Cast( new EnergyBoltSpell( from, this ) );
					break;
					
				case "Energy Field":
					Cast( new EnergyFieldSpell( from, this ) );
					break;
					
				case "Energy Vortex":
					Cast( new EnergyVortexSpell( from, this ) );
					break;
					
				case "Explosion":
					Cast( new ExplosionSpell( from, this ) );
					break;
					
				case "Feeblemind":
					Cast( new FeeblemindSpell( from, this ) );
					break;
					
				case "Fireball":
					Cast( new FireballSpell( from, this ) );
					break;
					
				case "Fire Elemental":
					Cast( new FireElementalSpell( from, this ) );
					break;
					
				case "Fire Field":
					Cast( new FireFieldSpell( from, this ) );
					break;
					
				case "Flame Strike":
					Cast( new FlameStrikeSpell( from, this ) );
					break;
					
				case "Gate Travel":
					Cast( new GateTravelSpell( from, this ) );
					break;
					
				case "Greater Heal":
					Cast( new GreaterHealSpell( from, this ) );
					break;
					
				case "Harm":
					Cast( new HarmSpell( from, this ) );
					break;
					
				case "Heal":
					Cast( new HealSpell( from, this ) );
					break;
					
				case "Incognito":
					Cast( new IncognitoSpell( from, this ) );
					break;
					
				case "Invisibility":
					Cast( new InvisibilitySpell( from, this ) );
					break;
					
				case "Lightning":
					Cast( new LightningSpell( from, this ) );
					break;
					
				case "Magic Arrow":
					Cast( new MagicArrowSpell( from, this ) );
					break;
					
				case "Magic Lock":
					Cast( new MagicLockSpell( from, this ) );
					break;
					
				case "Magic Reflection":
					Cast( new MagicReflectSpell( from, this ) );
					break;
					
				case "Magic Trap":
					Cast( new MagicTrapSpell( from, this ) );
					break;
					
				case "Mana Drain":
					Cast( new ManaDrainSpell( from, this ) );
					break;
					
				case "Mana Vampire":
					Cast( new ManaVampireSpell( from, this ) );
					break;
					
				case "Mark":
					Cast( new MarkSpell( from, this ) );
					break;
					
				case "Mass Curse":
					Cast( new MassCurseSpell( from, this ) );
					break;
					
				case "Mass Dispel":
					Cast( new MassDispelSpell( from, this ) );
					break;
					
				case "Meteor Swarm":
					Cast( new MeteorSwarmSpell( from, this ) );
					break;
					
				case "Mind Blast":
					Cast( new MindBlastSpell( from, this ) );
					break;
					
				case "Night Sight":
					Cast( new NightSightSpell( from, this ) );
					break;
					
				case "Paralyze Field":
					Cast( new ParalyzeFieldSpell( from, this ) );
					break;
					
				case "Paralyze":
					Cast( new ParalyzeSpell( from, this ) );
					break;
					
				case "Poison Field":
					Cast( new PoisonFieldSpell( from, this ) );
					break;
					
				case "Poison":
					Cast( new PoisonSpell( from, this ) );
					break;
					
				case "Polymorph":
					Cast( new PolymorphSpell( from, this ) );
					break;
					
				case "Protection":
					Cast( new ProtectionSpell( from, this ) );
					break;
					
				case "Reactive Armor":
					Cast( new ReactiveArmorSpell( from, this ) );
					break;
					
				case "Recall":
					Cast( new RecallSpell( from, this ) );
					break;
					
				case "Remove Trap":
					Cast( new RemoveTrapSpell( from, this ) );
					break;
					
				case "Resurrection":
					Cast( new ResurrectionSpell( from, this ) );
					break;
					
				case "Reveal":
					Cast( new RevealSpell( from, this ) );
					break;
					
				case "Strength":
					Cast( new StrengthSpell( from, this ) );
					break;
					
				case "Summon Creature":
					Cast( new SummonCreatureSpell( from, this ) );
					break;
					
				case "Summon Daemon":
					Cast( new SummonDaemonSpell( from, this ) );
					break;
					
				case "Telekinesis":
					Cast( new TelekinesisSpell( from, this ) );
					break;
					
				case "Teleport":
					Cast( new TeleportSpell( from, this ) );
					break;
					
				case "Unlock Spell":
					Cast( new UnlockSpell( from, this ) );
					break;
					
				case "Wall of Stone":
					Cast( new WallOfStoneSpell( from, this ) );
					break;
					
				case "Water Elemental":
					Cast( new WaterElementalSpell( from, this ) );
					break;
					
				case "Weaken":
					Cast( new WeakenSpell( from, this ) );
					break;
			}
			#endregion
		}
	}
	
	
	public class RuneTravelGem : Item
	{
		[Constructable]
		public RuneTravelGem() : base( 7956 )
		{
			Name = "Rune Travel Gem";
		}
		
		public RuneTravelGem( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}

	}
	
	/*
	Lesser Items ( 0 - 40% )
	-Lesser Mana Bracer - (Mage Only) Converts 10 max stamina to max mana.
	-Lesser Ring of Elemental Power - (Mage Only) Magery +5%, Eval Int +5%, Magic Resist -10%.
	
	Regular Items ( 40.1 - 70% )
	-Mana Bracer - (Mage Only) Converts 15 max stamina to max mana.
	-Ring of Elemental Power - (Mage Only) Magery +10%, Eval Int +10%, Magic Resist -10%.
	-Warding Necklace - Increases Magic Resist 10%.
	-Enchanted Wizards Hat - (Wizard Hat) (Mage Only) Eval Int +5%.
	-Mastering the Arcane: Volume I - (Mage Only) SDI +5%, FC +1.
	Wand - (Mage/Use Magic Item Feat Only) Charges are 1/2 of the first casters Magery. Strength is 75% of the first casters Magery. Wands break easy. Limited durability.

	Greater Items ( 70.1 - 120% )
	-Greater Mana Bracer - (Mage Only) Converts 20 max stamina to max mana.
	-Greater Ring of Elemental Power - (Mage Only) Magery +15%, Eval Int +15%, Magic Resist -10%.
	-Mastering the Arcane: Volume II - (Mage Only) SDI +10%, FC +2.
	-Mastering the Arcane: Volume III - (Mage Only) SDI +15%, FC +5.
	-Robe of Reflection - Reflects a limited amount of magical attacks, -50% Magic Resist.
	Mana Crystal - (Mage Only) Stores mana equal to the damage of the spell being cast on it. Max mana is 1.5x first chargers Magery skill. Releases 2-3 mana at a time. User is frozen until done.

	Legendary Items ( 100.0+ )
	Master's Staff - (Mage Only) Echantable Staff. Used with Enchantable Runes.
	Enchantable Rune - (Mage Only) Used with the Master's Staff.
	
	Rune Gate Items
	Lesser Travel Rune - (Mage Only) Short Distance Gate. Unlocked piece-by-piece via static quest.
	Travel Rune - (Mage Only) Medium Distance Gate.
	Greater Travel Rune - (Mage Only) Long Distance Gate.
	*/
}