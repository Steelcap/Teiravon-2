using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a krakens corpse" )]
	public class LavaSpitter : BaseCreature
	{
		[Constructable]
		public LavaSpitter() : base( AIType.AI_Archer, FightMode.Closest, 10, 10, 0.2, 0.4 )
		{
			Name = "a lava spitter";
			Body = 77;
			BaseSoundID = 353;
            Hue = 2988;
			Level = 16;
            

			SetStr( 56, 80 );
			SetDex( 226, 245 );
			SetInt( 26, 40 );

			SetHits( 554, 768 );
			SetMana( 500 );

			SetDamage( 9, 13 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Fire, 30 );

			SetResistance( ResistanceType.Physical, -20, 5 );
			SetResistance( ResistanceType.Fire, 100, 100 );
			SetResistance( ResistanceType.Cold, -30, -40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 50;

			PackGold( 30, 90 );
			CanSwim = true;
			CantWalk = true;

            AddItem(new MobRange(4));
		}

        public override bool HasBreath { get { return true; } }
        public override int BreathAngerSound { get { return 0x11f; } }
        public override double BreathDamageScalar{get {return 0.2;}   }
        public override int BreathEffectSound { get { return 0x11c; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

        public LavaSpitter(Serial serial)
            : base(serial)
		{
		}


        private DateTime m_NextBomb;
        private int m_Thrown;

        public override void OnActionCombat()
        {
            Mobile combatant = Combatant;

            if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, 12) || !CanBeHarmful(combatant) )
                return;

            if (DateTime.Now >= m_NextBomb)
            {
                ThrowBomb(combatant);

                m_Thrown++;

                if (0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                    m_NextBomb = DateTime.Now + TimeSpan.FromSeconds(3.0);
                else
                    m_NextBomb = DateTime.Now + TimeSpan.FromSeconds(5.0 + (10.0 * Utility.RandomDouble())); // 5-15 seconds
            }
        }

        public void ThrowBomb(Mobile m)
        {
            DoHarmful(m);

            this.MovingParticles(m, 0x19AB, 2, 12, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);

            new InternalTimer(m, this).Start();
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Mobile, m_From;

            public InternalTimer(Mobile m, Mobile from)
                : base(TimeSpan.FromSeconds(1.0))
            {
                m_Mobile = m;
                m_From = from;
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                m_Mobile.PlaySound(0x11F);
                AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(30, 50), 0, 100, 0, 0, 0);
                LavaPuddle puddle = new LavaPuddle();
                puddle.MoveToWorld(m_Mobile.Location, m_Mobile.Map);
            }
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
