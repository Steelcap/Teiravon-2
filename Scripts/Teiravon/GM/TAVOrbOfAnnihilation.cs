using System;
using Server;
using Server.Accounting;
using Server.Items;
using Server.Mobiles;
using Server.Scripts.Commands;

	
namespace Server.Items
{
	

	public class OrbofAnnihilation : Item
	{
		private bool used;
		
		[Constructable]
		public OrbofAnnihilation() : base( 0x186E )
		{
			Stackable = false;
			Weight = 5.0;
			Name = "Orb of Annihilation";
			used = false;
		}
	
		public OrbofAnnihilation( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
            if (from.AccessLevel < AccessLevel.Administrator)
            {
                from.SendMessage("This item would have just caused everything in the world to lose it's enchantments, never add this item again.");
                return;
            }
			if (this.used == false)
			{
				this.used = true;
				
				Server.Scripts.Commands.CommandHandlers.BroadcastMessage( AccessLevel.Player, 0x482, String.Format( "Time is halted as the great source is drained!") );
				Server.Scripts.Commands.CommandHandlers.BroadcastMessage( AccessLevel.Player, 0x482, String.Format( "An image of {0} forms in the sky as lightning sparks from all sources of magic!", from.Name ) );

				Timer timer = new OrbTimer( from );
				timer.Start();
				

			}
		}
		
		private class OrbTimer : Timer
		{
			private Mobile m_Mobile;

			public OrbTimer( Mobile from ) : base ( TimeSpan.FromSeconds( 3.0 ) )
			{
				m_Mobile = from;
			}

			protected override void OnTick()
			{
				foreach ( Item item in World.Items.Values )
				{
					if ((item is BaseRunicTool) || (item is BaseArmor) || (item is BaseWeapon) || (item is BaseJewel))
					{
						
					bool parentmob = false;
					bool accesscheck = false;
					
					if (item.RootParent is Mobile)
					{
						parentmob = true;
						Mobile pm = (Mobile)item.RootParent;
						if (pm.AccessLevel < AccessLevel.Counselor)
							accesscheck = true;
						
					}
					
					if (!(parentmob) || accesscheck)
					{
						//if (item.Name == null)
						if ( 0 == 0)
						{
							if (item is BaseRunicTool)
							{
								BaseRunicTool r_tool = (BaseRunicTool)item;
								r_tool.UsesRemaining = 1;
							}
							else if (item is BaseArmor)
							{
								BaseArmor m_this = (BaseArmor)item;

								for ( int i = 0; i < 5; ++i ) //AosSkillBonuses
								{
									m_this.SkillBonuses.SetValues(i,SkillName.Alchemy,0);
								}
								
								//AosArmorAttributes
								m_this.ArmorAttributes.LowerStatReq = 0;
								m_this.ArmorAttributes.SelfRepair = 0;
								m_this.ArmorAttributes.MageArmor = 0;
								m_this.ArmorAttributes.DurabilityBonus = 0;
									
								
								m_this.ColdBonus = 0;     //Cold Bonus
								m_this.EnergyBonus = 0;   //Energy Bonus
								m_this.FireBonus = 0;     //Fire Bonus
								m_this.PhysicalBonus = 0; //Physical Bonus
								m_this.PoisonBonus = 0;   //Poison Bonus
								m_this.StrBonus = 0;      //Str Bonus
								m_this.IntBonus = 0;      //Int Bonus
								m_this.DexBonus = 0;      //Dex Bonus

								// AosAttributes
								m_this.Attributes.RegenHits = 0;
								m_this.Attributes.RegenStam = 0;
								m_this.Attributes.RegenMana = 0;
								m_this.Attributes.DefendChance = 0;
								m_this.Attributes.AttackChance = 0;
								m_this.Attributes.BonusStr = 0;
								m_this.Attributes.BonusDex = 0;
								m_this.Attributes.BonusInt = 0;
								m_this.Attributes.BonusHits = 0;
								m_this.Attributes.BonusStam = 0;
								m_this.Attributes.BonusMana = 0;
								m_this.Attributes.WeaponDamage = 0;
								m_this.Attributes.WeaponSpeed = 0;
								m_this.Attributes.SpellDamage = 0;
								m_this.Attributes.CastRecovery = 0;
								m_this.Attributes.CastSpeed = 0;
								m_this.Attributes.LowerManaCost = 0;
								m_this.Attributes.LowerRegCost = 0;
								m_this.Attributes.ReflectPhysical = 0;
								m_this.Attributes.EnhancePotions = 0;
								m_this.Attributes.Luck = 0;
								m_this.Attributes.SpellChanneling = 0;
								m_this.Attributes.NightSight = 0;
								
							}
							else if (item is BaseWeapon)
							{
								BaseWeapon m_this = (BaseWeapon)item;
								
								for ( int i = 0; i < 5; ++i ) //AosSkillBonuses
								{
									m_this.SkillBonuses.SetValues(i,SkillName.Alchemy,0);
								}
								
								m_this.Slayer = SlayerName.None; //Slayer

								// AosAttributes
								m_this.Attributes.RegenHits = 0;
								m_this.Attributes.RegenStam = 0;
								m_this.Attributes.RegenMana = 0;
								m_this.Attributes.DefendChance = 0;
								m_this.Attributes.AttackChance = 0;
								m_this.Attributes.BonusStr = 0;
								m_this.Attributes.BonusDex = 0;
								m_this.Attributes.BonusInt = 0;
								m_this.Attributes.BonusHits = 0;
								m_this.Attributes.BonusStam = 0;
								m_this.Attributes.BonusMana = 0;
								m_this.Attributes.WeaponDamage = 0;
								m_this.Attributes.WeaponSpeed = 0;
								m_this.Attributes.SpellDamage = 0;
								m_this.Attributes.CastRecovery = 0;
								m_this.Attributes.CastSpeed = 0;
								m_this.Attributes.LowerManaCost = 0;
								m_this.Attributes.LowerRegCost = 0;
								m_this.Attributes.ReflectPhysical = 0;
								m_this.Attributes.EnhancePotions = 0;
								m_this.Attributes.Luck = 0;
								m_this.Attributes.SpellChanneling = 0;
								m_this.Attributes.NightSight = 0;
								
								//AosWeaponAttributes
								m_this.WeaponAttributes.LowerStatReq = 0;
								m_this.WeaponAttributes.SelfRepair = 0;
								m_this.WeaponAttributes.HitLeechHits = 0;
								m_this.WeaponAttributes.HitLeechStam = 0;
								m_this.WeaponAttributes.HitLeechMana = 0;
								m_this.WeaponAttributes.HitLowerAttack = 0;
								m_this.WeaponAttributes.HitLowerDefend = 0;
								m_this.WeaponAttributes.HitMagicArrow = 0;
								m_this.WeaponAttributes.HitHarm = 0;
								m_this.WeaponAttributes.HitFireball = 0;
								m_this.WeaponAttributes.HitLightning = 0;
								m_this.WeaponAttributes.HitDispel = 0;
								m_this.WeaponAttributes.HitColdArea = 0;
								m_this.WeaponAttributes.HitFireArea = 0;
								m_this.WeaponAttributes.HitPoisonArea = 0;
								m_this.WeaponAttributes.HitEnergyArea = 0;
								m_this.WeaponAttributes.HitPhysicalArea = 0;
								m_this.WeaponAttributes.ResistPhysicalBonus = 0;
								m_this.WeaponAttributes.ResistFireBonus = 0;
								m_this.WeaponAttributes.ResistColdBonus = 0;
								m_this.WeaponAttributes.ResistPoisonBonus = 0;
								m_this.WeaponAttributes.ResistEnergyBonus = 0;
								m_this.WeaponAttributes.UseBestSkill = 0;
								m_this.WeaponAttributes.MageWeapon = 0;
								m_this.WeaponAttributes.DurabilityBonus = 0;

							}
							else if (item is BaseJewel)
							{
								BaseJewel m_this = (BaseJewel)item;
								
								for ( int i = 0; i < 5; ++i ) //AosSkillBonuses
								{
									m_this.SkillBonuses.SetValues(i,SkillName.Alchemy,0);
								}

								// AosAttributes
								m_this.Attributes.RegenHits = 0;
								m_this.Attributes.RegenStam = 0;
								m_this.Attributes.RegenMana = 0;
								m_this.Attributes.DefendChance = 0;
								m_this.Attributes.AttackChance = 0;
								m_this.Attributes.BonusStr = 0;
								m_this.Attributes.BonusDex = 0;
								m_this.Attributes.BonusInt = 0;
								m_this.Attributes.BonusHits = 0;
								m_this.Attributes.BonusStam = 0;
								m_this.Attributes.BonusMana = 0;
								m_this.Attributes.WeaponDamage = 0;
								m_this.Attributes.WeaponSpeed = 0;
								m_this.Attributes.SpellDamage = 0;
								m_this.Attributes.CastRecovery = 0;
								m_this.Attributes.CastSpeed = 0;
								m_this.Attributes.LowerManaCost = 0;
								m_this.Attributes.LowerRegCost = 0;
								m_this.Attributes.ReflectPhysical = 0;
								m_this.Attributes.EnhancePotions = 0;
								m_this.Attributes.Luck = 0;
								m_this.Attributes.SpellChanneling = 0;
								m_this.Attributes.NightSight = 0;
								
								for ( int i = 0; i < 5; ++i ) //AosElementAttributes
								{
									m_this.Resistances.SetValue(i,0);
								}
							}
						}
					}
					}
				}
				Server.Scripts.Commands.CommandHandlers.BroadcastMessage( AccessLevel.Player, 0x482, String.Format( "As the image fades away, you notice that item enchantments are now gone.") );
				
			}
			
		}
			
	
	
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( used );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			used = reader.ReadBool();
		}
		
	
	}

}
