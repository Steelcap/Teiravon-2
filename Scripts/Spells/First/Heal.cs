using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Spells.First
{
	public class HealSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Heal", "In Mani",
				SpellCircle.First,
				224,
				9061,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.SpidersSilk
			);
        private static SlayerEntry m_Slayer = SlayerGroup.GetEntryByName( SlayerName.Silver );

		public HealSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( m.IsDeadBondedPet || m_Slayer.Slays( m ) )
			{
				Caster.SendLocalizedMessage( 1060177 ); // You cannot heal a creature that is already dead!
			}
			else if ( m is BaseCreature && ((BaseCreature)m).IsAnimatedDead )
			{
				Caster.SendLocalizedMessage( 1061654 ); // You cannot heal that which is not alive.
			}
			else if ( m is Golem )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 500951 ); // You cannot heal that.
			}
			else if ( m.Poisoned || Server.Items.MortalStrike.IsWounded( m ) )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x22, (Caster == m) ? 1005000 : 1010398 );
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				int toHeal;

				if ( Core.AOS )
				{
					// TODO: / 100 or / 120 ? 1, 3 or 1, 4 ?
					toHeal = Caster.Skills.Magery.Fixed / 100;
					toHeal += Utility.RandomMinMax( 1, 3 );

                    if (Caster is TeiravonMobile)
                    {
                        TeiravonMobile TAV = (TeiravonMobile)Caster;
                        
                        if (TAV.HasFeat(TeiravonMobile.Feats.HealersOath))
                            toHeal *=2;
                    }
				}
				else
				{
					toHeal = (int)(Caster.Skills[SkillName.Magery].Value * 0.1);
					toHeal += Utility.Random( 1, 5 );
				}

				if (Caster is TeiravonMobile && Caster !=m)
                {
                    TeiravonMobile TAV = (TeiravonMobile)Caster;
                    
                    if (TAV.HasFeat(TeiravonMobile.Feats.HealersOath))
                        toHeal *= 2;

                    if ((m.Hits + toHeal) > m.HitsMax)
                    {
                        toHeal = m.HitsMax - m.Hits;
                    }
                    int exp = toHeal *(1 + (TAV.PlayerLevel / 2));
                    if (TAV.HasFeat(TeiravonMobile.Feats.HealersOath))
                        exp *=2;

                    if (exp > 0 && Misc.Titles.AwardExp(TAV, exp))
                        TAV.SendMessage("You have gained {0} exp", exp);


                }
				m.Heal( toHeal );

				m.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
				m.PlaySound( 0x1F2 );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private HealSpell m_Owner;

			public InternalTarget( HealSpell owner ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}