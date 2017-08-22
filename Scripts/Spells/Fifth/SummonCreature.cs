using System;
using Server.Mobiles;
using System.Collections;
using Server.Network;
using Server.Gumps;
using Server.Targeting;

namespace Server.Spells.Fifth
{
	public class SummonCreatureSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Summon Creature", "Kal Xen",
				SpellCircle.Fifth,
				266,
				9040,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public SummonCreatureSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		// TODO: Get real list

        public class SummonCreatureGump : Gump
        {
            private Mobile m_From;

            private static Type[] m_Types = new Type[]
			{
				typeof( PolarBear ),
				typeof( GrizzlyBear ),
				typeof( BlackBear ),
				typeof( BrownBear ),
				typeof( Horse ),
				typeof( Walrus ),
				typeof( GreatHart ),
				typeof( Hind ),
				typeof( Dog ),
				typeof( Boar ),
				typeof( Chicken ),
				typeof( Rabbit )
			};

            public SummonCreatureGump(Mobile from)
                : base(20, 30)
            {
                m_From = from;
                
                AddPage(0);

                AddBackground(0, 0, 440, 155, 5054);
                AddBackground(10, 10, 420, 75, 2620);
                AddBackground(10, 85, 420, 45, 3000);

                AddBackground(0, 155, 440, 155, 5054);
                AddBackground(10, 165, 420, 75, 2620);
                AddBackground(10, 240, 420, 45, 3000);

                AddBackground(0, 310, 440, 155, 5054);
                AddBackground(10, 320, 420, 75, 2620);
                AddBackground(10, 395, 420, 45, 3000);

                for (int i = 0; i < 12; ++i)
                {
                    Mobile m = (Mobile)Activator.CreateInstance(m_Types[i]);

                    AddItem(20 + ((i % 4) * 100), 20 + ((i / 4) * 155), ShrinkTable.Lookup(m));
                    AddButton(20 + ((i % 4) * 100), 130 + ((i / 4) * 155), 4005, 4007, i + 1, GumpButtonType.Reply, 0);

                    if (m.Name != null)
                        AddHtml(20 + ((i % 4) * 100), 90 + ((i / 4) * 155), 90, 40, m.Name, false, false);
                }
            }

            public override void OnResponse(NetState state, RelayInfo info)
            {
                int index = info.ButtonID - 1;

                if (index >= 0)
                {
                    try
                    {
                        BaseCreature creature = (BaseCreature)Activator.CreateInstance(m_Types[index]);

                        creature.ControlSlots = 2;

                        TimeSpan duration;

                        if (Core.AOS)
                            duration = TimeSpan.FromSeconds((2 * m_From.Skills.Magery.Fixed) / 5);
                        else
                            duration = TimeSpan.FromSeconds(4.0 * m_From.Skills[SkillName.Magery].Value);

                        SpellHelper.Summon(creature, m_From, 0x215, duration, false, false);
                    }
                    catch
                    {
                    }
                }
            }
        }
		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
			    Caster.SendGump(new SummonCreatureGump(Caster));
			}

			FinishSequence();
		}

		public override TimeSpan GetCastDelay()
		{
			if ( Core.AOS )
				return TimeSpan.FromTicks( base.GetCastDelay().Ticks * 3 );

			return base.GetCastDelay() + TimeSpan.FromSeconds( 4.0 );
		}
	}
}