using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ettins corpse" )]
	public class Ettin : BaseCreature
	{
		DateTime m_NextRock;
		static int m_RockDamage;
		int m_Thrown;

		public void ThrowRock( Mobile m )
		{
			DoHarmful( m );
			Item m_Rock = this.Backpack.FindItemByType( typeof( EttinRock ) );

			if ( m_Rock == null )
				return;

			if ( m_Rock.ItemID == 4963 )
				m_RockDamage = 40;
			else if ( m_Rock.ItemID == 4964 || m_Rock.ItemID == 4965 )
				m_RockDamage = 25;
			else if ( m_Rock.ItemID == 4966 )
				m_RockDamage = 20;

			this.MovingParticles( m, m_Rock.ItemID, 5, 0, false, false, 0, 0, 9502, 6014, 0x120, EffectLayer.Waist, 0 );

			new InternalTimer( m, this ).Start();
			m_Rock.MoveToWorld( m.Location, m.Map );
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile, m_From;

			public InternalTimer( Mobile m, Mobile from ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = m;
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_Mobile.PlaySound( 0x120 );

				if ( m_Mobile.Dex / 1.5 > Utility.RandomMinMax( 1, m_Mobile.Dex ) )
					return;

				AOS.Damage( m_Mobile, m_From, m_RockDamage, 100, 0, 0, 0, 0 );
			}
		}

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if ( DateTime.Now >= m_NextRock )
			{
				ThrowRock( combatant );

				m_Thrown++;

				if ( 0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1 ) // 75% chance to quickly throw another bomb
					m_NextRock = DateTime.Now + TimeSpan.FromSeconds( 3.0 );
				else
					m_NextRock = DateTime.Now + TimeSpan.FromSeconds( 5.0 + (10.0 * Utility.RandomDouble()) ); // 5-15 seconds
			}
		}

		[Constructable]
		public Ettin() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ettin";
			Body = 18;
			BaseSoundID = 367;
			Level = 7;

			SetStr( 136, 165 );
			SetDex( 56, 75 );
			SetInt( 31, 55 );

			SetHits( 82, 99 );

			SetDamage( 7, 17 );

			RangeFight = 1;

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 40.1, 55.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 38;
			AddItem(new MobRange(2));
			PackItem( new EttinRock() );
			PackItem( new EttinRock() );
			PackItem( new EttinRock() );
			//PackGold( 30, 35 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			//AddLoot( LootPack.Average );
			AddLoot( LootPack.Potions );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 4; } }

		public Ettin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
