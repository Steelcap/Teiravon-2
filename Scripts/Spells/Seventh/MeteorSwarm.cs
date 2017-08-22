using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;

namespace Server.Spells.Seventh
{
	public class MeteorSwarmSpell : Spell
	{
        public enum Element
        {
            Air,
            Water,
            Fire,
            Earth
        }
        public Element Elemental;

		private static SpellInfo m_Info = new SpellInfo(
				"Meteor Swarm", "Flam Kal Des Ylem",
				SpellCircle.Seventh,
				233,
				9042,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh,
				Reagent.SpidersSilk
			);

		public MeteorSwarmSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
            XmlData x = (XmlData)XmlAttach.FindAttachmentOnMobile(Caster, typeof(XmlData), "Cryomancer");
            if (x != null && CheckSequence())
            {
                if (SpellHelper.CheckTown(Caster.Location, Caster) && CheckSequence())
                {
                    DoBlizzard(Caster);
                    Effects.PlaySound(Caster.Location, Caster.Map, 0x016);
                    return;
                }
            }
            else 
			Caster.Target = new InternalTarget( this );
		}

        public override int CastRecoveryBase
        {get{return 10;}}

        public void DoBlizzard(Mobile caster)
        {
            int Bliz_damage = GetNewAosDamage(15, 1, 5, false);
            
            for (int i = 0; i < 140; ++i)
            {
                int x = caster.X + Utility.Random(25) - 12;
                int y = caster.Y + Utility.Random(25) - 12;
                int z = caster.Map.GetAverageZ(x, y);

                Point3D loc = new Point3D(x, y, z);

                if (caster.Map.CanFit(loc, 0, true))
                {
                    double delay = 5 * Utility.RandomDouble();
                    Timer.DelayCall(TimeSpan.FromSeconds(delay), new TimerStateCallback(BlizzardEffect_Callback), new object[] { caster, loc });
                    Timer.DelayCall(TimeSpan.FromSeconds(delay + 2.5), new TimerStateCallback(BlizzardDamage_Callback), new object[] { caster, loc, Bliz_damage });
                }
                if (i == 139)
                    FinishSequence();
            }
            

        }

        public static void BlizzardEffect_Callback(object state)
        {
            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
            {
                return;
            }

            IEntity to = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z), ag.Map);
            IEntity from = new Entity(Serial.Zero, new Point3D(loc.X - 1 + Utility.Random(2), loc.Y - 1 + Utility.Random(2), loc.Z + 50), ag.Map);
            Effects.SendMovingEffect(from, to, 14052, 2, 16, false, true, 2491, 3);
            Effects.PlaySound(loc, ag.Map, 0x1E5);
            //AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(0.5), 0.5, to.Map, to.Location, 14120, 15, 2746, 4, 2, 0, true, false, false);
        }

        public static void BlizzardDamage_Callback(object state)
        {
            //Point3D loc = (Point3D)state;

            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);
            int damage = ((int)states[2]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;
            
            ArrayList targets = new ArrayList();

            IPooledEnumerable eable = ag.Map.GetMobilesInRange(loc, 1);
            foreach (Mobile m in eable)
            {
                if ( m.AccessLevel > ag.AccessLevel || m.Blessed || m == null || m.Map == Map.Internal || m.Map == null || !m.Alive || m == ag || !ag.CanBeHarmful(m, false) || !SpellHelper.ValidIndirectTarget(ag, m))
                    return;

                targets.Add(m);
            }
            eable.Free();

            for (int i = 0; i < targets.Count; ++i)
            {
                Mobile from = (Mobile)targets[i];
                AOS.Damage(from, ag, damage, 0, 0, 100, 0, 0);
            }
        }


        public int GetDamage(Element e, bool playerVsPlayer)
        {
            switch (e)
            {
                case Element.Air:
                    return GetNewAosDamage(2, 2, 5, playerVsPlayer);
                case Element.Earth:
                    return GetNewAosDamage(2, 1, 4, playerVsPlayer);
                case Element.Fire:
                    return GetNewAosDamage(12, 1, 5, playerVsPlayer);
                case Element.Water:
                    return GetNewAosDamage(1, 1, 6, playerVsPlayer);
            }
            return 10;
        }
		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				if ( p is Item )
					p = ((Item)p).GetWorldLocation();

				ArrayList targets = new ArrayList();
                double damage;
				Map map = Caster.Map;
				bool playerVsPlayer = false;
                if (Caster is TeiravonMobile)
                {
                    TeiravonMobile tav = Caster as TeiravonMobile;

                    if (tav.IsAeromancer())
                    {
                        Elemental = Element.Air;
                        damage = GetNewAosDamage(2, 2, 10, playerVsPlayer);
                        DoMeteorFall(Caster, p, Elemental, damage);
                        FinishSequence();
                        return;
                    }
                    else if (tav.IsAquamancer())
                    {
                        Elemental = Element.Water;
                        damage = GetNewAosDamage(1, 1, 6, playerVsPlayer);
                        DoMeteorFall(Caster, p, Elemental, damage);
                        FinishSequence();
                        return;
                    }
                    else if (tav.IsGeomancer())
                    {
                        Elemental = Element.Earth;
                        damage = GetNewAosDamage(2, 1, 4, playerVsPlayer);
                        DoMeteorFall(Caster, p, Elemental, damage);
                        FinishSequence();
                        return;
                    }
                    else if (tav.IsPyromancer())
                    {
                        Elemental = Element.Fire;

                        damage = GetNewAosDamage(12, 1, 5, playerVsPlayer);
                        DoMeteorFall(Caster, p, Elemental, damage);
                        FinishSequence();
                        return;
                    }
                }

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 2 );

					foreach ( Mobile m in eable )
					{
						if ( Caster != m && SpellHelper.ValidIndirectTarget( Caster, m ) && Caster.CanBeHarmful( m, false ) )
						{
							targets.Add( m );

							if ( m.Player )
								playerVsPlayer = true;
						}
					}

					eable.Free();
				}

				

				if ( Core.AOS )
					damage = GetNewAosDamage( 48, 1, 5, Caster.Player && playerVsPlayer );
				else
					damage = Utility.Random( 27, 22 );

				if ( targets.Count > 0 )
				{
					Effects.PlaySound( p, Caster.Map, 0x160 );

					if ( Core.AOS && targets.Count > 1 )
						damage = (damage * 2) / targets.Count;
					else if ( !Core.AOS )
						damage /= targets.Count;

					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = (Mobile)targets[i];

						double toDeal = damage;

						if ( !Core.AOS && CheckResisted( m ) )
						{
							toDeal *= 0.5;

							m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
						}

						Caster.DoHarmful( m );
						SpellHelper.Damage( this, m, toDeal, 0, 100, 0, 0, 0 );

						Caster.MovingParticles( m, 0x36D4, 7, 0, false, true, 9501, 1, 0, 0x100 );
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MeteorSwarmSpell m_Owner;

			public InternalTarget( MeteorSwarmSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}


        public void DoMeteorFall(Mobile caster, IPoint3D p, Element e, double damage)
        {
            int num = 40;
            int radius = 5;
            int delaycoef = 5;

                switch (e)
                {
                    case Element.Air:
                        {
                            num = 80;
                            radius = 7;
                            delaycoef = 6;
                        }
                        break;
                    case Element.Earth:
                        {
                            num = 80;
                            radius = 4;
                            delaycoef = 1;
                        }
                        break;
                    case Element.Fire:
                        {
                            num = 15;
                            radius = 6;
                        }
                        break;
                    case Element.Water:
                        {
                            num = 140;
                            radius = 12;
                            delaycoef = 2;
                        }
                        break;
                }
            for (int i = 0; i < num; ++i)
            {
                int x = p.X + Utility.Random(2*radius + 1) - radius;
                int y = p.Y + Utility.Random(2*radius + 1) - radius;
                int z = caster.Map.GetAverageZ(x, y);

                Point3D loc = new Point3D(x, y, z);

                if (!Caster.CanSee(loc))
                {
                    continue;
                }

                if (caster.Map.CanFit(loc, 0, true))
                {
                    double delay = delaycoef * Utility.RandomDouble();
                    Timer.DelayCall(TimeSpan.FromSeconds(delay), new TimerStateCallback(MeteorFallEffect_Callback), new object[] { caster, loc, e });
                    Timer.DelayCall(TimeSpan.FromSeconds(delay + 0.5), new TimerStateCallback(MeteorFallDamage_Callback), new object[] { caster, loc, (int)damage, e, this });
                }
            }


        }

        public static void MeteorFallEffect_Callback(object state)
        {
            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);
            Element e = ((Element)states[2]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
            {
                return;
            }
            switch (e)
            {
                case Element.Air:
                    {
                        int height = 10+ (int)(15* Utility.RandomDouble());
                        //int shape = Utility.RandomList(14026, 14000, 14013);
                        //int shape = 2331;
                        int shape = Utility.RandomList(0x0CDE, 0x0CD1, 0x0CDB);
                        IEntity to = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z), ag.Map);
                        IEntity from = new Entity(Serial.Zero, new Point3D(loc.X - 3 + Utility.Random(4), loc.Y - 3 + Utility.Random(4), loc.Z + Utility.Random(4)), ag.Map);
                        //Effects.SendMovingEffect(to, from, 14200, 2, 16, false, false, 2740, 3);
                        AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.4), TimeSpan.FromSeconds(0.8), 0.0, to.Map, to.Location, shape, 32, 2988, 5, 3, height, true, true, false);
                        Effects.PlaySound(loc, ag.Map, 0x5C6);
                    }
                    break;
                case Element.Water:
                    {
                        IEntity to = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z), ag.Map);
                        IEntity from = new Entity(Serial.Zero, new Point3D(loc.X - 1 + Utility.Random(2), loc.Y - 1 + Utility.Random(2), loc.Z + 50), ag.Map);
                        Effects.SendMovingEffect(from, to, 14200, 1, 120, true, false);
                        Effects.SendLocationEffect(to.Location, to.Map, 10980, 26, 2643, 3);
                        Effects.PlaySound(loc, ag.Map, 0x011);
                    }
                    break;
                case Element.Earth:
                    {
                        int shape = Utility.RandomMinMax(0x0923, 0x092A);
                        
                        IEntity to = new Entity(Serial.Zero, new Point3D(loc.X + 1 + Utility.Random(2), loc.Y - 14, loc.Z + 3), ag.Map);
                        IEntity from = new Entity(Serial.Zero, new Point3D(loc.X - 1 + Utility.Random(2), loc.Y + 14, loc.Z+6), ag.Map);
                        Effects.SendMovingEffect(from, to, shape, 10, 16, true, false);
                        //Effects.SendLocationEffect(to.Location, to.Map, 0x2AE4, 16, 1051, 3);
                        Effects.PlaySound(loc, ag.Map, 0x015);
                    }
                    break;
                case Element.Fire:
                    {
                        int sound = Utility.RandomMinMax(0x11B, 0x120);
                        IEntity to = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z), ag.Map);
                        IEntity from = new Entity(Serial.Zero, new Point3D(loc.X - 1 + Utility.Random(2), loc.Y - 1 + Utility.Random(2), loc.Z + 50), ag.Map);
                        Effects.SendMovingEffect(from, to, 0x36D4, 2, 26, false, false);
                        AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.8), TimeSpan.FromSeconds(0.8), 0.4, to.Map, to.Location, 0x36CB, 16, 0, 0, 2, 0, true, false, false);
                        Effects.PlaySound(loc, ag.Map, sound);
                    }
                    break;
                    
            }
        }

        public static void MeteorFallDamage_Callback(object state)
        {
            //Point3D loc = (Point3D)state;

            object[] states = ((object[])state);
            TeiravonMobile ag = states[0] as TeiravonMobile;
            Point3D loc = ((Point3D)states[1]);
            int damage = ((int)states[2]);
            Element e = ((Element)states[3]);
            MeteorSwarmSpell s = ((MeteorSwarmSpell)states[4]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;

            ArrayList targets = new ArrayList();
            int x = 2;
            
            if (e == Element.Fire)
            {
                x = 3;
            }
            else if (e == Element.Air)
            {
                x = 3;
            }

            IPooledEnumerable eable = ag.Map.GetMobilesInRange(loc, x);
            foreach (Mobile m in eable)
            {
                if (m.AccessLevel > ag.AccessLevel || m.Blessed || m == null || m.Map == Map.Internal || m.Map == null || !m.Alive || m == ag || !ag.CanBeHarmful(m, false) || !SpellHelper.ValidIndirectTarget(ag, m))
                    return;

                targets.Add(m);
            }
            eable.Free();

            switch (e)
            {
                case Element.Air:
                    {
                        for (int i = 0; i < targets.Count; ++i)
                        {
                            Mobile from = (Mobile)targets[i];
                            AOS.Damage(from, ag, s.GetDamage(e,from.Player), 0, 0, 50, 0, 50);
                        }
                    }
                    break;
                case Element.Earth:
                    {
                        for (int i = 0; i < targets.Count; ++i)
                        {
                            Mobile from = (Mobile)targets[i];
                            AOS.Damage(from, ag, s.GetDamage(e, from.Player), 100, 0, 0, 0, 0);
                        }
                    }
                    break;
                case Element.Fire:
                    {
                        for (int i = 0; i < targets.Count; ++i)
                        {
                            Mobile from = (Mobile)targets[i];
                            AOS.Damage(from, ag, s.GetDamage(e, from.Player), 0, 100, 0, 0, 0);
                        }
                    }
                    break;
                case Element.Water:
                    {
                        for (int i = 0; i < targets.Count; ++i)
                        {
                            Mobile from = (Mobile)targets[i];
                            if (.1 > Utility.RandomDouble())
                            {
                                AOS.Damage(from, ag, (int)(s.GetDamage(e, from.Player) * 4.5), 0, 0, 0, 0, 100);
                                Effects.SendBoltEffect(from);
                            }
                            else
                            {
                                AOS.Damage(from, ag, s.GetDamage(e, from.Player), 0, 0, 100, 0, 0);
                                Effects.SendLocationEffect(from.Location, from.Map, 2323,10,1,3);
                            }
                        }
                    }
                    break;
            }  
        }
	}
}