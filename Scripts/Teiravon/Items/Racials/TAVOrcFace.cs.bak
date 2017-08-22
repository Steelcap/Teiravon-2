using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class OrcFace : BaseArmor
	{
	
		private Rank m_Rank;
		private TeiravonMobile m_Player;
	
		//public override int PhysicalResistance{ get{ return 10; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Cloth; }  }
		public override bool DisplayLootType{ get{ return false; } }

		public enum Rank
		{
			None,
			Makur = 1,
			Grunt = 2,
			Sergeant = 3,
			Captain = 4,
			Shaman = 5,
			HighShaman = 6,
			Warboss = 7
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Rank OrcRank
		{
			get { return m_Rank; }
			set 
			{ 
				Rank m_Previous = m_Rank;
				
				m_Rank = value;
				
				bool equipped = false;
				
				Hue = 2414;
				Attributes.RegenHits = 0;
				Attributes.RegenMana = 0;
				Attributes.RegenStam = 0;
				Attributes.CastSpeed = 0;
				Attributes.LowerManaCost = 0;
				Attributes.WeaponDamage = 0;
				
				if ( this.RootParent != null && this.RootParent is TeiravonMobile )
				{
					m_Player = (TeiravonMobile)this.RootParent;
					equipped = true;
				}
				
				if ( m_Rank == Rank.None )
				{
					if (equipped)
					{
						this.Hue = 2414;
						m_Player.Hue = 2414;
						m_Player.Title = m_Player.IsOrc() ? "the Orc" : "the Goblin";
						
						if ((int)m_Previous > (int)m_Rank)
							m_Player.SendMessage( "You have been demoted to Pug!" );							
					}
					
				}
				else if ( m_Rank == Rank.Makur )
				{
					if (equipped)
					{
						this.Hue = 2418;
						m_Player.Hue = 2418;
                        m_Player.Title = m_Player.IsOrc() ? "the Orc Makur" : "the Goblin Makur";
						
						if ((int)m_Previous < (int)m_Rank)
							m_Player.SendMessage( "You have been promoted to Makur!" );
						else if ((int)m_Previous != (int)m_Rank)
							m_Player.SendMessage( "You have been demoted to Makur!" );
					}
					
				}
				else if ( m_Rank == Rank.Grunt )
				{
					if (equipped)
					{
						this.Hue = 1446;
						m_Player.Hue = 1446;
                        m_Player.Title = m_Player.IsOrc() ? "the Orc Grunt" : "the Goblin Grunt";
						
						if ((int)m_Previous < (int)m_Rank)
							m_Player.SendMessage( "You have been promoted to Grunt!" );
						else if ((int)m_Previous != (int)m_Rank)
							m_Player.SendMessage( "You have been demoted to Grunt!" );						
					}
				
				}
				else if (m_Rank == Rank.Sergeant)
				{
					if (equipped)
					{
						this.Hue = 2210;
					//	Attributes.RegenHits = 1;
						
						m_Player.Hue = 2210;
                        m_Player.Title = m_Player.IsOrc() ? "the Orc Sergeant" : "the Goblin Sergeant";

						if ((int)m_Previous < (int)m_Rank)
							m_Player.SendMessage( "You have been promoted to Sergeant!" );
						else if ((int)m_Previous != (int)m_Rank)
							m_Player.SendMessage( "You have been demoted to Sergeant!" );						
					}
				}
				else if (m_Rank == Rank.Captain)
				{
					if (equipped)
					{
						this.Hue = 2130;
					//	Attributes.RegenHits = 2;
					//	Attributes.WeaponDamage = 10;
						
						m_Player.Hue = 2130;
                        m_Player.Title = m_Player.IsOrc() ? "the Orc Captain" : "the Goblin Captain";
						
						if ((int)m_Previous < (int)m_Rank)
							m_Player.SendMessage( "You have been promoted to Captain!" );
						else if ((int)m_Previous != (int)m_Rank)
							m_Player.SendMessage( "You have been demoted to Captain!" );						
						 
					}
				}
				else if (m_Rank == Rank.Shaman)
				{
					if (equipped)
					{
						this.Hue = 2114;
					//	Attributes.RegenMana = 1;
					//	Attributes.LowerManaCost = 5;
						
						m_Player.Hue = 2114;

                        m_Player.Title = m_Player.IsOrc() ? "the Orc Shaman" : "the Goblin Shaman";
						
						if ((int)m_Previous < (int)m_Rank)
							m_Player.SendMessage( "You have been promoted to Shaman!" );
						else if ((int)m_Previous != (int)m_Rank)
							m_Player.SendMessage( "You have been demoted to Shaman!" );						
						 
					}
				}
				else if (m_Rank == Rank.HighShaman)
				{
					if (equipped)
					{
						this.Hue = 2117;
					//	Attributes.RegenMana = 1;
					//	Attributes.CastSpeed = 1;
					//	Attributes.LowerManaCost = 10;
						
						m_Player.Hue = 2117;
                        m_Player.Title = m_Player.IsOrc() ? "the Orc High Shaman" : "the Goblin High Shaman";
						
						if ((int)m_Previous < (int)m_Rank)
							m_Player.SendMessage( "You have been promoted to High Shaman!" );
						else if ((int)m_Previous != (int)m_Rank)
							m_Player.SendMessage( "You have been demoted to High Shaman!" );						
						 
					}
				}
				else if (m_Rank == Rank.Warboss)
				{
					if (equipped)
					{
					
						this.Hue = 2406;
					//	Attributes.RegenStam = 3;
					//	Attributes.RegenHits = 3;
					//	Attributes.WeaponDamage = 20;
						
						m_Player.Hue = 2406;
                        m_Player.Title = m_Player.IsOrc() ? "the Orc Warboss" : "the Goblin Warboss";
						
						if ((int)m_Previous < (int)m_Rank)
							m_Player.SendMessage( "You have been promoted to Warboss!" );	
					}
				}	
			}
		}
			
		

		[Constructable]
		public OrcFace() : this( null )
		{
		}
	
		[Constructable]
		public OrcFace( Mobile player ) : base( 0x141B )
		{
			Weight = 0.0;
			Hue = 2414;
			Resource = CraftResource.None;
			Layer = Layer.FacialHair;
			LootType = LootType.Blessed;
			m_Rank = Rank.None;
			OrcRank = Rank.None;
			
			if (player == null)
				Name = "Orc Face";
			else
				Name = player.Name + "'s face";	
		}

		public override bool CanEquip( Mobile m )
		{
			if ( m is TeiravonMobile )
			{
				TeiravonMobile m_Player = (TeiravonMobile)m;
				
				if ( (m_Player.IsOrc() || m_Player.IsGoblin()))
				{
					if ( m.Hair != null )
						(m.Hair).Delete();
							
					if ( m.Beard != null )
						(m.Beard).Delete();
						
					return true;
				}
			}

			if ( !base.CanEquip( m ) )
				return false;

			return false;
		}

		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			return DeathMoveResult.RemainEquiped;
		}

		public OrcFace( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			
			writer.Write( (int) m_Rank );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Rank = (Rank)reader.ReadInt();

            Attributes.RegenHits = 0;
            Attributes.RegenMana = 0;
            Attributes.RegenStam = 0;
            Attributes.CastSpeed = 0;
            Attributes.LowerManaCost = 0;
			/*
			if (m_Rank == Rank.Captain)
				Attributes.WeaponDamage = 10;
			if (m_Rank == Rank.Warboss)
				Attributes.WeaponDamage = 20;
			if (m_Rank == Rank.Shaman)
			{
				Attributes.RegenMana = 1;
				Attributes.LowerManaCost = 5;
			}
			if (m_Rank == Rank.HighShaman)
			{
				Attributes.RegenMana = 1;
				Attributes.CastSpeed = 1;
				Attributes.LowerManaCost = 10;
			}
            */
		}
	}
}
