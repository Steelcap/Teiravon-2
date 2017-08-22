using System;
using Server.Mobiles;
using Server.Engines.PartySystem;
using Server.Items;

namespace Server.SkillHandlers
{
	public class Stealth
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Stealth].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
            if (m is TeiravonMobile && ((TeiravonMobile)m).m_Revealed + TimeSpan.FromSeconds(3.0) > DateTime.Now)
            {
                m.SendMessage("You cannot find any cover so quickly after being revealed.");
                return TimeSpan.FromSeconds(1.0);
            }

			if ( !m.Hidden )
			{
                Hiding.OnUse(m);
			}
			/*
			else if ( m.Skills[SkillName.Hiding].Base < ((Core.SE) ? 50.0 : 80.0) )
			{
				m.SendLocalizedMessage( 502726 ); // You are not hidden well enough.  Become better at hiding.
			}
			*/
			/*else if ( CheckNear( m ) )
			{
				m.SendLocalizedMessage( 502731 ); // You fail in your attempt to move unnoticed.
				m.RevealingAction();
			}*/
			else
			{
//				int armorRating = (int) m.ArmorRating;

//				if ( armorRating > 25 )
//				{
//					m.SendLocalizedMessage( 502727 ); // You could not hope to move quietly wearing this much armor.
//					m.RevealingAction();
//				}
//				else if ( m.CheckSkill( SkillName.Stealth, -20.0 + (armorRating * 2), 80.0 + (armorRating * 2) ) )
				
				int armorRating = 0;
				BaseArmor bam;
				Item item;
				
				item = m.FindItemOnLayer(Layer.TwoHanded);
				if (item is BaseShield)
				{
					if (item is Buckler)
						armorRating++;
					else if (item is HeaterShield)
						armorRating+=5;
					else
						armorRating+=3;
				}
				
				item = m.FindItemOnLayer(Layer.InnerTorso);
				if (item is BaseArmor)
				{
					bam = (BaseArmor)item;
					if (bam is DrowChainChest || bam is ElvenChainChest)
						armorRating +=(((int)bam.MaterialType -4) *4);
					else
						armorRating += ((int)bam.MaterialType * 4);
				}
				
				item = m.FindItemOnLayer(Layer.Pants);
				if (item is BaseArmor)
				{
					bam = (BaseArmor)item;
					if (bam is DrowChainLegs || bam is ElvenChainLegs)
						armorRating +=(((int)bam.MaterialType -4) *4);
					else
					armorRating += ((int)bam.MaterialType * 3);
				}

				item = m.FindItemOnLayer(Layer.Arms);
				if (item is BaseArmor)
				{
					bam = (BaseArmor)item;
					armorRating += ((int)bam.MaterialType * 2);
				}

				item = m.FindItemOnLayer(Layer.Helm);
				if (item is BaseArmor)
				{
					bam = (BaseArmor)item;
					if (bam is DrowChainCoif || bam is ElvenChainCoif)
						armorRating +=(((int)bam.MaterialType -4) *4);
					else
					armorRating += ((int)bam.MaterialType * 2);
				}
				
				item = m.FindItemOnLayer(Layer.Gloves);
				if (item is BaseArmor)
				{
					bam = (BaseArmor)item;
					armorRating += ((int)bam.MaterialType * 1);
				}

				item = m.FindItemOnLayer(Layer.Neck);
				if (item is BaseArmor)
				{
					bam = (BaseArmor)item;
					armorRating += ((int)bam.MaterialType * 1);
				}

//				m.SendMessage("{0} to {1}", -20.0 + armorRating, 80.0 + armorRating);
				
				if ( m.CheckSkill( SkillName.Stealth, -30.0 + (armorRating), 80.0 + (armorRating) ) )
				{
					int steps = (int)(m.Skills[SkillName.Stealth].Value / (Core.AOS ? 5.0 : 10.0));

					if ( steps < 1 )
						steps = 1;

					if ( m is TeiravonMobile )
					{
						TeiravonMobile m_Player = (TeiravonMobile)m;

						if ( m_Player.HasFeat( TeiravonMobile.Feats.AdvancedStealth ) )
							steps += 15;
					}

					m.AllowedStealthSteps = steps;

					m.SendLocalizedMessage( 502730 ); // You begin to move quietly.

					return TimeSpan.FromSeconds( 3.0 );
				}
				else
				{
					m.SendLocalizedMessage( 502731 ); // You fail in your attempt to move unnoticed.
					m.RevealingAction();
				}
			}

			return TimeSpan.FromSeconds( 3.0 );
		}
       
		public static bool CheckNear( Mobile m )
		{
			bool ok = false;
			Party p = Party.Get( m );

			foreach ( Mobile blah in m.GetMobilesInRange( 2 ) )
			{
				
				if ( blah == m)
					continue;

				if ( blah != null && blah != m)
					if ( !m.InLOS( blah ) || ( p != null && p.Contains( blah ) ) )
						ok = true;
				
				double stealthskill = m.Skills.Stealth.Value;
				
				if ( blah.LightLevel < 10 )
					stealthskill -= ( Math.Abs( blah.LightLevel - 10 ) ) * 2.5;
				else if ( blah.LightLevel > 10 )
					stealthskill += ( blah.LightLevel - 10 ) * 2.5;

				if ( stealthskill < 0.0 )
					stealthskill = 0.0;
				
				if ( m is TeiravonMobile && ((TeiravonMobile)m).HasFeat( TeiravonMobile.Feats.AdvancedStealth ) )
					stealthskill *= 1.3;
				
				if( !blah.CheckSkill( SkillName.DetectHidden, 30.0, stealthskill ) )
					ok = true;
			}

			return ok;
		}
	}
}
