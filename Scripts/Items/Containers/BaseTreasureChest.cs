using Server;
using Server.Items;
using Server.Network;
using System;
using System.Collections;

namespace Server.Items
{
	public abstract class BaseTreasureChest : LockableContainer
	{
		private TreasureLevel m_TreasureLevel;
		private short m_MaxSpawnTime = 60;
		private short m_MinSpawnTime = 10;
		private TreasureResetTimer m_ResetTimer;

		private static Type[] m_AlchyScroll = new Type[]
		{
			typeof ( NightSightFormula ),	typeof ( LesserCureFormula ),
			typeof ( CureFormula ),			typeof ( AgilityFormula ),
			typeof ( StrengthFormula ),		typeof ( LesserPoisonFormula ),
			typeof ( PoisonFormula ),		typeof ( RefreshFormula ),
			typeof ( LesserHealFormula ),	typeof ( HealFormula ),
			typeof ( LesserExplosionFormula ), typeof ( ExplosionFormula ),
			typeof ( GreaterAgilityFormula ),	typeof ( GreaterStrengthFormula ),
			typeof ( GreaterPoisonFormula ),	typeof ( ManaRefreshFormula ),
			typeof ( LesserFloatFormula ),	typeof ( FloatFormula ),
			typeof ( SustenanceFormula ),	typeof ( ChameleonFormula ),
			typeof ( GreaterCureFormula ),	typeof ( DeadlyPoisonFormula ),
			typeof ( GreaterRefreshFormula ),	typeof ( GreaterHealFormula ),
			typeof ( GreaterExplosionFormula ),	typeof ( GreaterSustenanceFormula ),
			typeof ( GenderSwapFormula ),	typeof ( GreaterFloatFormula ),
			typeof ( InvisibilityFormula ),	typeof ( TotalManaRefreshFormula ),
			typeof ( MagicResistFormula ),	typeof ( InvulnerabilityFormula )
		};
		
		private static Type[] m_ChestStuff = new Type[]
		{
			typeof ( CandleSkull), 			typeof ( Rope ),
			typeof ( IronWire ),			typeof ( SilverWire ),
			typeof ( GoldWire ), 			typeof ( CopperWire ),
			typeof ( Whip ),				typeof ( PaintsAndBrush ),
			typeof ( MalletAndChisel ),		typeof ( HoodedShroudOfShadows ),
			typeof ( DecorativeShield1 ),	typeof ( DecorativeShield2 ),
			typeof ( DecorativeShield3 ),	typeof ( DecorativeShield4 ),
			typeof ( DecorativeShield5 ),	typeof ( DecorativeShield6 ),
			typeof ( DecorativeShield7 ),	typeof ( DecorativeShield8 ),
			typeof ( DecorativeShield9 ),	typeof ( DecorativeShield10 ),
			typeof ( DecorativeShield11 ),	typeof ( DecorativeShieldSword1North ),
			typeof ( DecorativeShieldSword1West ),	typeof ( DecorativeShieldSword2North ),
			typeof ( DecorativeShieldSword2West ),	typeof ( DecorativeBowWest ),
			typeof ( DecorativeBowNorth ),	typeof ( DecorativeAxeNorth ),
			typeof ( DecorativeAxeWest ),	typeof ( DecorativeSwordNorth ),
			typeof ( DecorativeSwordWest ),	typeof ( DecorativeDAxeNorth ),
			typeof ( DecorativeDAxeWest ),	typeof ( LargePainting ),
			typeof ( WomanPortrait1 ),		typeof ( WomanPortrait2 ),
			typeof ( ManPortrait1 ),		typeof ( ManPortrait2 ),
			typeof ( LadyPortrait1 ),		typeof ( LadyPortrait2 ),
			typeof ( WoodDebris ),			typeof ( Tapestry1N ),
			typeof ( Tapestry2N ),			typeof ( Tapestry2W ),
			typeof ( Tapestry3N ),			typeof ( Tapestry3W ),
			typeof ( Tapestry4N ),			typeof ( Tapestry4W ),
			typeof ( Tapestry5N ),			typeof ( Tapestry5W ),
			typeof ( Tapestry6N ),			typeof ( Tapestry6W ),
			typeof ( RuinedFallenChairA ),	typeof ( RuinedArmoire ),
			typeof ( RuinedBookcase ),		typeof ( RuinedBooks ),
			typeof ( CoveredChair ),		typeof ( RuinedFallenChairB ),
			typeof ( RuinedChair ),			typeof ( RuinedClock ),
			typeof ( RuinedDrawers ),		typeof ( RuinedPainting )
		};
		
		[CommandProperty( AccessLevel.GameMaster )]
		public TreasureLevel Level
		{
			get
			{
				return m_TreasureLevel;
			}
			set
			{
				m_TreasureLevel = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public short MaxSpawnTime
		{
			get
			{
				return m_MaxSpawnTime;
			}
			set
			{
				m_MaxSpawnTime = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public short MinSpawnTime
		{
			get
			{
				return m_MinSpawnTime;
			}
			set
			{
				m_MinSpawnTime = value;
			}
		}

		public override bool CanStore( Mobile m )
		{
			return true; 
		}
		
		public BaseTreasureChest( int itemID ) : base ( itemID )
		{
			m_TreasureLevel = TreasureLevel.Level2;
			Locked = true;
			Movable = false;
			SetLockedName();

			Key key = (Key)FindItemByType( typeof(Key) );

			if( key != null )
				key.Delete();
			
			if (this is WoodenTreasureChest || this is MetalTreasureChest || this is MetalGoldenTreasureChest)
			{
				SetLockLevel();
				GenerateTreasure();
			}
		}

		public BaseTreasureChest( Serial serial ) : base( serial )
		{
		}

		protected virtual void SetLockedName()
		{
			Name = "a locked treasure chest";
		}

		protected virtual void SetUnlockedName()
		{
			Name = "a treasure chest";
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (byte) m_TreasureLevel );
			writer.Write( m_MinSpawnTime );
			writer.Write( m_MaxSpawnTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_TreasureLevel = (TreasureLevel)reader.ReadByte();
			m_MinSpawnTime = reader.ReadShort();
			m_MaxSpawnTime = reader.ReadShort();

			if( !Locked )
				StartResetTimer();
		}

		protected virtual void SetLockLevel()
		{
			switch( m_TreasureLevel )
			{
				case TreasureLevel.Level1:
					this.RequiredSkill = this.LockLevel = this.TrapPower = Utility.RandomMinMax(1,10);
					break;

				case TreasureLevel.Level2:
					this.RequiredSkill = this.LockLevel  = this.TrapPower = Utility.RandomMinMax(10,40);
					break;

				case TreasureLevel.Level3:
					this.RequiredSkill = this.LockLevel  = this.TrapPower = Utility.RandomMinMax(40,60);
					break;

				case TreasureLevel.Level4:
					this.RequiredSkill = this.LockLevel  = this.TrapPower = Utility.RandomMinMax(60,80);
					break;

				case TreasureLevel.Level5:
					this.RequiredSkill = this.LockLevel  = this.TrapPower = Utility.RandomMinMax(80,99);
					break;
				case TreasureLevel.Level6:
					this.RequiredSkill = this.LockLevel  = this.TrapPower = 100;
					break;
			}
			this.TrapType = TrapType.ExplosionTrap;
		}

		private void StartResetTimer()
		{
			if( m_ResetTimer == null )
				m_ResetTimer = new TreasureResetTimer( this );
			else
				m_ResetTimer.Delay = TimeSpan.FromMinutes( Utility.Random( m_MinSpawnTime, m_MaxSpawnTime ));
				
			m_ResetTimer.Start();
		}

		protected virtual void GenerateTreasure()
		{
			int MinGold = 1;
			int MaxGold = 2;
			int TLvl = 0;

			switch( m_TreasureLevel )
			{
				case TreasureLevel.Level1:
					MinGold = 100;
					MaxGold = 300;
					TLvl = 1;
					break;

				case TreasureLevel.Level2:
					MinGold = 300;
					MaxGold = 600;
					TLvl = 2;
					break;

				case TreasureLevel.Level3:
					MinGold = 600;
					MaxGold = 900;
					TLvl = 3;
					break;

				case TreasureLevel.Level4:
					MinGold = 900;
					MaxGold = 1200;
					TLvl = 4;
					break;

				case TreasureLevel.Level5:
					MinGold = 1200;
					MaxGold = 1500;
					TLvl = 5;
					break;
					
				case TreasureLevel.Level6:
					MinGold = 2000;
					MaxGold = 2500;
					TLvl = 6;
					break;
			}
			DropItem( new Gold( MinGold, MaxGold ) );

			//Scrolls
			int numscroll = Utility.RandomMinMax(TLvl, TLvl * 2);
			for ( int i = 0; i < numscroll; ++i )
				DropItem( Loot.RandomScroll( 0, 63, SpellbookType.Regular ) );
			//Gems
			for ( int i = 0; i < TLvl * 5; i++ )
			{
				Item item = Loot.RandomGem();
				DropItem( item );
			}
			
			//Alchemy Scrolls
			int rndm = Utility.Random(100);
			int rndmlvl;
			
			if (TLvl > 3 && rndm < 25)
			{
				if (TLvl == 6 )
					rndmlvl = Utility.RandomMinMax(12,32);
				else if (TLvl == 5)
					rndmlvl = Utility.RandomMinMax(1,32);
				else 
					rndmlvl = Utility.RandomMinMax(1,11);

				DropItem( (Item)Activator.CreateInstance( m_AlchyScroll[rndmlvl] ) );
			}

			//Stuff
			for ( int i = 0; i < (int)(TLvl/2) +1;  i++ )
			{
				int rndmstuff = Utility.Random(m_ChestStuff.Length);
				Item stuffitem = (Item)Activator.CreateInstance( m_ChestStuff[rndmstuff] );
				stuffitem.Movable = true;
				DropItem (stuffitem);
//				DropItem( (Item)Activator.CreateInstance( m_ChestStuff[rndmstuff] ) );
			}
			
			//Magic items
			int TMrandom = Utility.Random(100);
			if ((TLvl > 4 && TMrandom < 5) || (TLvl > 5 && TMrandom < 25))
			{
				Item item;
				item = Loot.RandomArmorOrShieldOrWeaponOrJewelry();

				if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;

						int attributeCount = Utility.RandomMinMax(1,3);
						int min = Utility.RandomMinMax(5,15);
						int max = Utility.RandomMinMax(20,40);
						if (TLvl > 5 && TMrandom < 5)
							{
								attributeCount += 1;
								min += 20;
								max += 30;
							}
						BaseRunicTool.ApplyAttributesTo( weapon, attributeCount, min, max );

						DropItem( item );
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;

						int attributeCount = Utility.RandomMinMax(1,3);
						int min = Utility.RandomMinMax(5,15);
						int max = Utility.RandomMinMax(20,40);
						if (TLvl > 5 && TMrandom < 5)
						{
							attributeCount += 1;
							min += 20;
							max += 30;
						}
						BaseRunicTool.ApplyAttributesTo( armor, attributeCount, min, max );

						DropItem( item );
					}
					else if ( item is BaseJewel )
					{
						int attributeCount = Utility.RandomMinMax(1,3);
						int min = Utility.RandomMinMax(5,15);
						int max = Utility.RandomMinMax(20,40);
						if (TLvl > 5 && TMrandom < 5)
						{
							attributeCount += 1;
							min += 20;
							max += 30;
						}
						BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, attributeCount, min, max );

						DropItem( item );
					}
			}
			
			
		}

		public override void LockPick( Mobile from )
		{
			base.LockPick( from );

			SetUnlockedName();
			StartResetTimer();
		}

		public void ClearContents()
		{
			for ( int i = Items.Count - 1; i >= 0; --i )
				if ( i < Items.Count )
					((Item)Items[i]).Delete();
		}

		public void Reset()
		{
			if( m_ResetTimer != null )
			{
				if( m_ResetTimer.Running )
					m_ResetTimer.Stop();
			}

			Locked = true;
			SetLockedName();
			ClearContents();
			GenerateTreasure();
		}

		public enum TreasureLevel
		{
			Level1, 
			Level2, 
			Level3, 
			Level4, 
			Level5,
			Level6,
		}; 

		private class TreasureResetTimer : Timer
		{
			private BaseTreasureChest m_Chest;
			
			public TreasureResetTimer( BaseTreasureChest chest ) : base ( TimeSpan.FromMinutes( Utility.Random( chest.MinSpawnTime, chest.MaxSpawnTime ) ) )
			{
				m_Chest = chest;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m_Chest.Reset();
			}
		}
	}
}
