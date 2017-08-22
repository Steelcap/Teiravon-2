using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Multis;
using Server.Regions;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.PartySystem;
using Server.Misc;
using Server.Spells;
using Server.Engines.XmlSpawner2;
using Server.Network;
using Server.Spells.Necromancy;
using Teiravon.Magecraft;

namespace Server
{
	public class DefensiveSpell
	{
		public static void Nullify( Mobile from )
		{
			if ( !from.CanBeginAction( typeof( DefensiveSpell ) ) )
				new InternalTimer( from ).Start();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;

			public InternalTimer( Mobile m ) : base( TimeSpan.FromMinutes( 1.0 ) )
			{
				m_Mobile = m;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Mobile.EndAction( typeof( DefensiveSpell ) );
			}
		}
	}
}

namespace Server.Spells
{
	public enum TravelCheckType
	{
		RecallFrom,
		RecallTo,
		GateFrom,
		GateTo,
		Mark,
		TeleportFrom,
		TeleportTo
	}

	public class SpellHelper
	{
		private static TimeSpan AosDamageDelay = TimeSpan.FromSeconds( 1.0 );
		private static TimeSpan OldDamageDelay = TimeSpan.FromSeconds( 0.5 );

		public static TimeSpan GetDamageDelayForSpell( Spell sp )
		{
			if ( !sp.DelayedDamage )
				return TimeSpan.Zero;

			return ( Core.AOS ? AosDamageDelay : OldDamageDelay );
		}

		public static bool CheckMulti( Point3D p, Map map )
		{
			return CheckMulti( p, map, true );
		}

		public static bool CheckMulti( Point3D p, Map map, bool houses )
		{
			if ( map == null || map == Map.Internal )
				return false;

			Sector sector = map.GetSector( p.X, p.Y );

			for ( int i = 0; i < sector.Multis.Count; ++i )
			{
				BaseMulti multi = (BaseMulti)sector.Multis[i];

				if ( multi is BaseHouse )
				{
					if ( houses && ((BaseHouse)multi).IsInside( p, 16 ) )
						return true;
				}
				else if ( multi.Contains( p ) )
				{
					return true;
				}
			}

			return false;
		}

		public static void Turn( Mobile from, object to )
		{
			IPoint3D target = to as IPoint3D;

			if ( target == null )
				return;

			if ( target is Item )
			{
				Item item = (Item)target;

				if ( item.RootParent != from )
					from.Direction = from.GetDirectionTo( item.GetWorldLocation() );
			}
			else if ( from != target )
			{
				from.Direction = from.GetDirectionTo( target );
			}
		}

		private static TimeSpan CombatHeatDelay = TimeSpan.FromSeconds( 30.0 );
		private static bool RestrictTravelCombat = true;

		public static bool CheckCombat( Mobile m )
		{
			if ( !RestrictTravelCombat )
				return false;

			for ( int i = 0; i < m.Aggressed.Count; ++i )
			{
				AggressorInfo info = (AggressorInfo)m.Aggressed[i];

				if ( info.Defender.Player && (DateTime.Now - info.LastCombatTime) < CombatHeatDelay )
					return true;
			}


			if( Core.AOS )
			{
				for ( int i = 0; i < m.Aggressors.Count; ++i )
				{
					AggressorInfo info = (AggressorInfo)m.Aggressors[i];

					if ( info.Attacker.Player && (DateTime.Now - info.LastCombatTime) < CombatHeatDelay )
						return true;
				}
			}

			return false;
		}

		public static bool AdjustField( ref Point3D p, Map map, int height, bool mobsBlock )
		{
			if ( map == null )
				return false;

			for ( int offset = 0; offset < 10; ++offset )
			{
				Point3D loc = new Point3D( p.X, p.Y, p.Z - offset );

				if ( map.CanFit( loc, height, true, mobsBlock ) )
				{
					p = loc;
					return true;
				}
			}

			return false;
		}

		public static void GetSurfaceTop( ref IPoint3D p )
		{
			if ( p is Item )
			{
				p = ((Item)p).GetSurfaceTop();
			}
			else if ( p is StaticTarget )
			{
				StaticTarget t = (StaticTarget)p;
				int z = t.Z;

				if ( (t.Flags & TileFlag.Surface) == 0 )
					z -= TileData.ItemTable[t.ItemID & 0x3FFF].CalcHeight;

				p = new Point3D( t.X, t.Y, z );
			}
		}

		public static bool AddStatOffset( Mobile m, StatType type, int offset, TimeSpan duration )
		{
			if ( offset > 0 )
				return AddStatBonus( m, m, type, offset, duration );
			else if ( offset < 0 )
				return AddStatCurse( m, m, type, -offset, duration );

			return true;
		}

		public static bool AddStatBonus( Mobile caster, Mobile target, StatType type )
		{
			return AddStatBonus( caster, target, type, GetOffset( caster, target, type, false ), GetDuration( caster, target ) );
		}

		public static bool AddStatBonus( Mobile caster, Mobile target, StatType type, int bonus, TimeSpan duration )
		{
			int offset = bonus;
			string name = String.Format( "[Magic] {0} Offset", type );

			StatMod mod = target.GetStatMod( name );

			if ( mod != null && mod.Offset < 0 )
			{
				target.AddStatMod( new StatMod( type, name, mod.Offset + offset, duration ) );
				return true;
			}
			else if ( mod == null || mod.Offset < offset )
			{
				target.AddStatMod( new StatMod( type, name, offset, duration ) );
				return true;
			}

			return false;
		}

		public static bool AddStatCurse( Mobile caster, Mobile target, StatType type )
		{
			return AddStatCurse( caster, target, type, GetOffset( caster, target, type, true ), GetDuration( caster, target ) );
		}

		public static bool AddStatCurse( Mobile caster, Mobile target, StatType type, int curse, TimeSpan duration )
		{
			int offset = -curse;
			string name = String.Format( "[Magic] {0} Offset", type );

			StatMod mod = target.GetStatMod( name );

			if ( mod != null && mod.Offset > 0 )
			{
				target.AddStatMod( new StatMod( type, name, mod.Offset + offset, duration ) );
				return true;
			}
			else if ( mod == null || mod.Offset > offset )
			{
				target.AddStatMod( new StatMod( type, name, offset, duration ) );
				return true;
			}

			return false;
		}

		public static TimeSpan GetDuration( Mobile caster, Mobile target )
		{
			if ( Core.AOS )
			{
				if ( caster is TeiravonMobile )
				{
					TeiravonMobile m_Player = (TeiravonMobile)caster;

					if ( m_Player.IsCleric() || m_Player.IsDarkCleric() )
					{
						double temp = ( ( Math.Abs( (double)m_Player.Karma / (double)300 ) * (double)m_Player.PlayerLevel ) );

						if (temp < 10)
							temp = (double)10;

						if (temp > 600)
							temp = (double)600;

						return TimeSpan.FromSeconds( temp );
					}
                    else
                        return TimeSpan.FromSeconds(((10 * m_Player.PlayerLevel)) + 15);

				}

				return TimeSpan.FromSeconds( ((6 * caster.Skills.EvalInt.Fixed) / 50) + 1 );
			}

			return TimeSpan.FromSeconds( caster.Skills[SkillName.Magery].Value * 1.2 );
		}

		private static bool m_DisableSkillCheck;

		public static bool DisableSkillCheck
		{
			get{ return m_DisableSkillCheck; }
			set{ m_DisableSkillCheck = value; }
		}

		public static int GetOffset( Mobile caster, Mobile target, StatType type, bool curse )
		{
			if ( Core.AOS )
			{
				int adjtotal = 0;
				
				if ( !m_DisableSkillCheck )
				{
					caster.CheckSkill( SkillName.EvalInt, 0.0, 120.0 );

					if ( curse )
						target.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );
				}

				double percent = (double)0;
				bool cleric = false;

				if ( caster is TeiravonMobile )
				{
					TeiravonMobile m_Player = (TeiravonMobile)caster;
                    int stat = 0;
                    switch ( type )
				    {
					case StatType.Str: stat = (int)(target.RawStr);
                            break;
					case StatType.Dex: stat = (int)(target.RawDex);
                            break;
					case StatType.Int: stat = (int)(target.RawInt);
                            break;
                    }

					if ( m_Player.IsCleric() || m_Player.IsDarkCleric() )
					{
						cleric = true;
						
						adjtotal += (int)(m_Player.PlayerLevel + (stat * .25));

						if ( curse )
							percent = ( - (double)m_Player.Karma / (double)25000 );
						else
							percent = ( (double)m_Player.Karma / (double)25000 );

						percent += ( (double)m_Player.PlayerLevel / (double)100 );

						if ( percent < 0.05 )
							percent = 0.05;

						if ( percent > 0.75 )
							percent = 0.75;

                        AOS.Scale(adjtotal, (int)percent * 100);
					}
				}

				if ( !cleric )
				{
                    if (curse)
                    {
                        adjtotal += (int)(10 + (caster.Skills.EvalInt.Fixed / 100) - (target.Skills.MagicResist.Fixed / 100));
                        percent = 8 + (caster.Skills.EvalInt.Fixed / 100) - (target.Skills.MagicResist.Fixed / 100);
                    }
                    else
                    {
                        adjtotal += (int)(10 + (caster.Skills.EvalInt.Fixed / 100));
                        percent = 1 + (caster.Skills.EvalInt.Fixed / 100);
                    }
					percent *= 0.01;

					if ( percent < 0 )
						percent = 0;

                    AOS.Scale(adjtotal, (int)percent * 100);
				}

				if ( target is TeiravonMobile && curse )
				{
					TeiravonMobile m_Target = (TeiravonMobile)target;

					if ( m_Target.m_CloakOfDarkness )
					{
						m_Target.SendMessage( "The darkness has protected you." );
						return 0;
					}
				}

				switch ( type )
				{
					//case StatType.Str: return (int)(target.RawStr * percent);
					//case StatType.Dex: return (int)(target.RawDex * percent);
					//case StatType.Int: return (int)(target.RawInt * percent);
					case StatType.Str: return (int)(adjtotal);
					case StatType.Dex: return (int)(adjtotal);
					case StatType.Int: return (int)(adjtotal);
				}
			}

			return 1 + (int)(caster.Skills[SkillName.Magery].Value * 0.1);
		}

		public static Guild GetGuildFor( Mobile m )
		{
			Guild g = m.Guild as Guild;

			if ( g == null && m is BaseCreature )
			{
				BaseCreature c = (BaseCreature)m;
				m = c.ControlMaster;

				if ( m != null )
					g = m.Guild as Guild;

				if ( g == null )
				{
					m = c.SummonMaster;

					if ( m != null )
						g = m.Guild as Guild;
				}
			}

			return g;
		}

		public static bool ValidIndirectTarget( Mobile from, Mobile to )
		{
			if ( from == to )
				return true;

			if ( to.Hidden && to.AccessLevel > from.AccessLevel )
				return false;

			Guild fromGuild = GetGuildFor( from );
			Guild toGuild = GetGuildFor( to );

			//if ( !from.Aggressors.Contains(to) &&  !to.Aggressors.Contains(from) && fromGuild != null && toGuild != null && (fromGuild == toGuild || fromGuild.IsAlly( toGuild )) )
			//	return false;

			Party p = Party.Get( from );

			if ( p != null && p.Contains( to ) )
				return false;

			if ( to is BaseCreature )
			{
				BaseCreature c = (BaseCreature)to;

				if ( c.Controled || c.Summoned )
				{
					if ( c.ControlMaster == from || c.SummonMaster == from )
						return false;

					if ( p != null && (p.Contains( c.ControlMaster ) || p.Contains( c.SummonMaster )) )
						return false;
				}
			}
			if ( from is BaseCreature )
			{
				BaseCreature c = (BaseCreature)from;

				if ( c.Controled || c.Summoned )
				{
					if ( c.ControlMaster == to || c.SummonMaster == to )
						return false;

					p = Party.Get( to );

					if ( p != null && (p.Contains( c.ControlMaster ) || p.Contains( c.SummonMaster )) )
						return false;
				}
			}

			if ( to is BaseCreature && !((BaseCreature)to).Controled && ((BaseCreature)to).InitialInnocent )
				return true;

			//int noto = Notoriety.Compute( from, to );
			//return ( noto != Notoriety.Innocent || from.Kills >= 5 );

			return true;
		}

		private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};

		public static void Summon( BaseCreature creature, Mobile caster, int sound, TimeSpan duration, bool scaleDuration, bool scaleStats )
		{
			Map map = caster.Map;

			if ( map == null )
				return;

			double scale = 1.0 + ((caster.Skills[SkillName.Magery].Value - 100.0) / 200.0);
            scale += ((caster.Int - 100.0) / 500.0);

			if ( scaleDuration )
				duration = TimeSpan.FromSeconds( duration.TotalSeconds * scale );

			if ( scaleStats )
			{
				creature.RawStr = (int)(creature.RawStr * scale);
                creature.Hits = (int)(creature.HitsMax * scale);

				creature.RawDex = (int)(creature.RawDex * scale);
				creature.Stam = creature.StamMax;

				creature.RawInt = (int)(creature.RawInt * scale);
				creature.Mana = creature.ManaMax;
			}

			int offset = Utility.Random( 8 ) * 2;

			for ( int i = 0; i < m_Offsets.Length; i += 2 )
			{
				int x = caster.X + m_Offsets[(offset + i) % m_Offsets.Length];
				int y = caster.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

				if ( map.CanSpawnMobile( x, y, caster.Z ) )
				{
					BaseCreature.Summon( creature, caster, new Point3D( x, y, caster.Z ), sound, duration );
					return;
				}
				else
				{
					int z = map.GetAverageZ( x, y );

					if ( map.CanSpawnMobile( x, y, z ) )
					{
						BaseCreature.Summon( creature, caster, new Point3D( x, y, z ), sound, duration );
						return;
					}
				}
			}

			creature.Delete();
			caster.SendLocalizedMessage( 501942 ); // That location is blocked.
		}

		private delegate bool TravelValidator( Map map, Point3D loc );

		private static TravelValidator[] m_Validators = new TravelValidator[]
			{
				new TravelValidator( IsFeluccaT2A ),
				new TravelValidator( IsIlshenar ),
				new TravelValidator( IsTrammelWind ),
				new TravelValidator( IsFeluccaWind ),
				new TravelValidator( IsFeluccaDungeon ),
				new TravelValidator( IsTrammelSolenHive ),
				new TravelValidator( IsFeluccaSolenHive ),
				new TravelValidator( IsCrystalCave ),
				new TravelValidator( IsDoomGauntlet ),
				new TravelValidator( IsDoomFerry ),
				new TravelValidator( IsFactionStronghold )
			};

		private static bool[,] m_Rules = new bool[,]
			{
			/*T2A(Fel)		Ilshenar		Wind(Tram),	Wind(Fel),	Dungeons(Fel),	Solen(Tram),	Solen(Fel), CrystalCave(Malas),	Gauntlet(Malas),	Gauntlet(Ferry),	Stronghold */
/* Recall From */	{ false,		true,			true,		false,		false,			true,			false,		false,				false,				false,				true },
/* Recall To */		{ false,		true,			false,		false,		false,			false,			false,		false,				false,				false,				false },
/* Gate From */		{ false,		true,			false,		false,		false,			false,			false,		false,				false,				false,				false },
/* Gate To */		{ false,		true,			false,		false,		false,			false,			false,		false,				false,				false,				false },
/* Mark In */		{ false,		true,			false,		false,		false,			false,			false,		false,				false,				false,				false },
/* Tele From */		{ true,			true,			true,		true,		true,			true,			true,		false,				true,				true,				false },
/* Tele To */		{ true,			true,			true,		true,		true,			true,			true,		false,				true,				false,				false },
			};

		public static bool CheckTravel( Mobile caster, TravelCheckType type )
		{
			if ( CheckTravel( caster, caster.Map, caster.Location, type ) )
				return true;

			SendInvalidMessage( caster, type );
			return false;
		}

		public static void SendInvalidMessage( Mobile caster, TravelCheckType type )
		{
			if ( type == TravelCheckType.RecallTo || type == TravelCheckType.GateTo )
				caster.SendLocalizedMessage( 1019004 ); // You are not allowed to travel there.
			else if ( type == TravelCheckType.TeleportTo )
				caster.SendLocalizedMessage( 501035 ); // You cannot teleport from here to the destination.
			else
				caster.SendLocalizedMessage( 501802 ); // Thy spell doth not appear to work...
		}

		public static bool CheckTravel( Map map, Point3D loc, TravelCheckType type )
		{
			return CheckTravel( null, map, loc, type );
		}

		private static Mobile m_TravelCaster;
		private static TravelCheckType m_TravelType;

		public static bool CheckTravel( Mobile caster, Map map, Point3D loc, TravelCheckType type )
		{
			if ( IsInvalid( map, loc ) ) // null, internal, out of bounds
			{
				if ( caster != null )
					SendInvalidMessage( caster, type );

				return false;
			}

			m_TravelCaster = caster;
			m_TravelType = type;

			int v = (int)type;
			bool isValid = true;

			for ( int i = 0; isValid && i < m_Validators.Length; ++i )
				isValid = ( m_Rules[v, i] || !m_Validators[i]( map, loc ) );

			if ( !isValid && caster != null )
				SendInvalidMessage( caster, type );

			return isValid;
		}

		public static bool IsWindLoc( Point3D loc )
		{
			int x = loc.X, y = loc.Y;

			return ( x >= 5120 && y >= 0 && x < 5376 && y < 256 );
		}

		public static bool IsFeluccaWind( Map map, Point3D loc )
		{
			return ( map == Map.Felucca && IsWindLoc( loc ) );
		}

		public static bool IsTrammelWind( Map map, Point3D loc )
		{
			return ( map == Map.Trammel && IsWindLoc( loc ) );
		}

		public static bool IsIlshenar( Map map, Point3D loc )
		{
			return ( map == Map.Ilshenar );
		}

		public static bool IsSolenHiveLoc( Point3D loc )
		{
			int x = loc.X, y = loc.Y;

			return ( x >= 5640 && y >= 1776 && x < 5935 && y < 2039 );
		}

		public static bool IsTrammelSolenHive( Map map, Point3D loc )
		{
			return ( map == Map.Trammel && IsSolenHiveLoc( loc ) );
		}

		public static bool IsFeluccaSolenHive( Map map, Point3D loc )
		{
			return ( map == Map.Felucca && IsSolenHiveLoc( loc ) );
		}

		public static bool IsFeluccaT2A( Map map, Point3D loc )
		{
			int x = loc.X, y = loc.Y;

			return ( map == Map.Felucca && x >= 5120 && y >= 2304 && x < 6144 && y < 4096 );
		}

		public static bool IsFeluccaDungeon( Map map, Point3D loc )
		{
			return ( Region.Find( loc, map ) is FeluccaDungeon );
		}

		public static bool IsCrystalCave( Map map, Point3D loc )
		{
			if ( map != Map.Malas )
				return false;

			int x = loc.X, y = loc.Y, z = loc.Z;

			bool r1 = ( x >= 1182 && y >= 437 && x < 1211 && y < 470 );
			bool r2 = ( x >= 1156 && y >= 470 && x < 1211 && y < 503 );
			bool r3 = ( x >= 1176 && y >= 503 && x < 1208 && y < 509 );
			bool r4 = ( x >= 1188 && y >= 509 && x < 1201 && y < 513 );

			return ( z < -80 && ( r1 || r2 || r3 || r4 ) );
		}

		public static bool IsFactionStronghold( Map map, Point3D loc )
		{
			/*// Teleporting is allowed, but only for faction members
			if ( !Core.AOS && m_TravelCaster != null && (m_TravelType == TravelCheckType.TeleportTo || m_TravelType == TravelCheckType.TeleportFrom) )
			{
				if ( Factions.Faction.Find( m_TravelCaster, true, true ) != null )
					return false;
			}*/

			return ( Region.Find( loc, map ) is Factions.StrongholdRegion );
		}

		public static bool IsDoomFerry( Map map, Point3D loc )
		{
			if ( map != Map.Malas )
				return false;

			int x = loc.X, y = loc.Y;

			if ( x >= 426 && y >= 314 && x <= 430 && y <= 331 )
				return true;

			if ( x >= 406 && y >= 247 && x <= 410 && y <= 264 )
				return true;

			return false;
		}

		public static bool IsDoomGauntlet( Map map, Point3D loc )
		{
			if ( map != Map.Malas )
				return false;

			int x = loc.X - 256, y = loc.Y - 304;

			return ( x >= 0 && y >= 0 && x < 256 && y < 256 );
		}

		public static bool IsInvalid( Map map, Point3D loc )
		{
			if ( map == null || map == Map.Internal )
				return true;

			int x = loc.X, y = loc.Y;

			return ( x < 0 || y < 0 || x >= map.Width || y >= map.Height );
		}

		//towns
		public static bool IsTown( IPoint3D loc, Mobile caster )
		{
			if ( loc is Item )
				loc = ((Item)loc).GetWorldLocation();

			return IsTown( new Point3D( loc ), caster );
		}

		public static bool IsTown( Point3D loc, Mobile caster )
		{
			Map map = caster.Map;

			if ( map == null )
				return false;

			GuardedRegion reg = Region.Find( loc, map ) as GuardedRegion;

			return ( reg != null && !reg.IsDisabled() );
		}

		public static bool CheckTown( IPoint3D loc, Mobile caster )
		{
			if ( loc is Item )
				loc = ((Item)loc).GetWorldLocation();

			return CheckTown( new Point3D( loc ), caster );
		}

		public static bool CheckTown( Point3D loc, Mobile caster )
		{
			if ( IsTown( loc, caster ) )
			{
				caster.SendLocalizedMessage( 500946 ); // You cannot cast this in town!
				return false;
			}

			return true;
		}

        

		//magic reflection
		public static void CheckReflect( int circle, Mobile caster, ref Mobile target )
		{
			CheckReflect( circle, ref caster, ref target );
		}
        
		public static void CheckReflect( int circle, ref Mobile caster, ref Mobile target )
		{
			if ( target.FindItemOnLayer( Layer.OuterTorso ) is RobeOfReflection && ( caster != target ) && target.CheckSkill( SkillName.Magery, caster.Skills.Magery.Value * 0.5, caster.Skills.Magery.Value * 1.5 ) )
			{
				RobeOfReflection ror = (RobeOfReflection)target.FindItemOnLayer( Layer.OuterTorso );
				
				if ( ror.IsArcane && ror.CurArcaneCharges >= 0 )
				{
					target.FixedEffect( 0x37B9, 10, 5 );

					Mobile temp = caster;
					caster = target;
					target = temp;
					
					ror.CurArcaneCharges--;
					
					return;
				}
			}
			
			if ( target.MagicDamageAbsorb > 0 )
			{
				++circle;

				target.MagicDamageAbsorb -= circle;

				// This order isn't very intuitive, but you have to nullify reflect before target gets switched

				bool reflect = ( target.MagicDamageAbsorb >= 0 );
						
				if ( target is BaseCreature )
					((BaseCreature)target).CheckReflect( caster, ref reflect );

				if ( target.MagicDamageAbsorb <= 0 )
				{
					target.MagicDamageAbsorb = 0;
					DefensiveSpell.Nullify( target );
				}

				if ( reflect )
				{
					target.FixedEffect( 0x37B9, 10, 5 );

					Mobile temp = caster;
					caster = target;
					target = temp;
				}
			}
			else if ( target is BaseCreature )
			{
				bool reflect = false;

				((BaseCreature)target).CheckReflect( caster, ref reflect );

				if ( reflect )
				{
					target.FixedEffect( 0x37B9, 10, 5 );

					Mobile temp = caster;
					caster = target;
					target = temp;
				}
			}
		}

		private static double CloakOfDarknessCheck( Spell spell, Mobile target, double dam )
		{
			if ( !(target is TeiravonMobile) )
				return dam;

			if ( spell.GetType() == typeof( Second.HarmSpell ) || spell.GetType() == typeof( Fifth.MindBlastSpell ) )
			{
				TeiravonMobile m_Target = (TeiravonMobile)target;

				if ( m_Target.m_CloakOfDarkness )
				{
					m_Target.SendMessage( "The darkness has protected you." );
					return 0.0;
				}
			}

			return dam;
		}

		public static void Damage( Spell spell, Mobile target, double damage )
		{
			TimeSpan ts = GetDamageDelayForSpell( spell );

			Damage( ts, target, spell.Caster, damage );
		}

		public static void Damage( TimeSpan delay, Mobile target, double damage )
		{
			Damage( delay, target, null, damage );
		}

		public static void Damage( TimeSpan delay, Mobile target, Mobile from, double damage )
		{
			int iDamage = (int) damage;

			if ( delay == TimeSpan.Zero )
			{
				if ( from is BaseCreature )
					((BaseCreature)from).AlterSpellDamageTo( target, ref iDamage );

				if ( target is BaseCreature )
					((BaseCreature)target).AlterSpellDamageFrom( from, ref iDamage );

				target.Damage( iDamage, from );
			}
			else
			{
				new SpellDamageTimer( target, from, iDamage, delay ).Start();
			}

			if ( target is BaseCreature && from != null && delay == TimeSpan.Zero )
				((BaseCreature)target).OnDamagedBySpell( from );
		}

        public static void GeoDamage(Spell spell, Mobile target, double damage)
        {
            TimeSpan ts = GetDamageDelayForSpell(spell);
            Damage(ts, target, spell.Caster, damage, 100, 0, 0, 0, 0);
        }

		public static void Damage( Spell spell, Mobile target, double damage, int phys, int fire, int cold, int pois, int nrgy )
		{

			//Most (All) Damage spells start here
            if (spell.CheckResisted(target))
            {
                damage *= 0.75;

                target.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
            }


			//KENSAI AVOIDANCE
			bool KensaiAvoided = false;
			bool KensaiAvoidable = true;

			Type[] m_NotAvoidable = new Type[]
			{
				//typeof( Fifth.MindBlastSpell ),
				typeof( Seventh.MeteorSwarmSpell ),
				typeof( Eighth.EarthquakeSpell ),
				typeof( Seventh.ChainLightningSpell ),
				typeof( Fourth.FireFieldSpell )
			};

			for ( int i=0; i < m_NotAvoidable.Length; i++ )
			{
				if ( m_NotAvoidable[i] == spell.GetType() )
				{
					KensaiAvoidable = false;
					break;
				}
			}

			if ( target is TeiravonMobile )
			{
				TeiravonMobile m_Player = (TeiravonMobile)target;


                if (Spells.Third.BlessSpell.gruumsh.Contains(m_Player) || Spells.Third.BlessSpell.gruumsh.Contains(target))
                    damage *= 1.30;

                if (m_Player.GetActiveFeats(TeiravonMobile.Feats.DisruptingPresence) && m_Player.HasFeat(TeiravonMobile.Feats.DisruptingPresence))
                {
                    damage *= 0.75;
                    m_Player.SendMessage("Your disrupting presense dulls the potency of the spell");
                }

                if (m_Player.HasFeat(TeiravonMobile.Feats.MagicResistance))
                {
                    damage *= 0.75;
                    target.SendLocalizedMessage(501783); 
                }

				int cntmith = 0;
				ArrayList mithitems = new ArrayList();
				BaseArmor tmpitem;
				
				tmpitem = m_Player.FindItemOnLayer(Layer.InnerTorso) as BaseArmor;
				if (tmpitem != null)
				{
					if (tmpitem.Resource == CraftResource.Mithril)
					{
						mithitems.Add(tmpitem);
						cntmith++;
					}
				}
				tmpitem = m_Player.FindItemOnLayer(Layer.Pants) as BaseArmor;
				if (tmpitem != null)
				{
					if (tmpitem.Resource == CraftResource.Mithril)
					{
						mithitems.Add(tmpitem);
						cntmith++;
					}
				}
				tmpitem = m_Player.FindItemOnLayer(Layer.Arms) as BaseArmor;
				if (tmpitem != null)
				{
					if (tmpitem.Resource == CraftResource.Mithril)
					{
						mithitems.Add(tmpitem);
						cntmith++;
					}
				}
				tmpitem = m_Player.FindItemOnLayer(Layer.Gloves) as BaseArmor;
				if (tmpitem != null)
				{
					if (tmpitem.Resource == CraftResource.Mithril)
					{
						mithitems.Add(tmpitem);
						cntmith++;
					}
				}
				tmpitem = m_Player.FindItemOnLayer(Layer.Helm) as BaseArmor;
				if (tmpitem != null)
				{
					if (tmpitem.Resource == CraftResource.Mithril)
					{
						mithitems.Add(tmpitem);
						cntmith++;
					}
				}
				tmpitem = m_Player.FindItemOnLayer(Layer.Neck) as BaseArmor;
				if (tmpitem != null)
				{
					if (tmpitem.Resource == CraftResource.Mithril)
					{
						mithitems.Add(tmpitem);
						cntmith++;
					}
				}
				tmpitem = m_Player.FindItemOnLayer(Layer.TwoHanded) as BaseArmor;
				if (tmpitem != null)
				{
					if (tmpitem.Resource == CraftResource.Mithril)
					{
						mithitems.Add(tmpitem);
						cntmith++;
					}
				}

                if (target.VirtualArmorMod >= 1)
                {
                    if (Utility.Random(3) > 1)
                    {
                        double amount = damage - target.VirtualArmorMod;

                        target.VirtualArmorMod -= (int)damage;
                        if (amount < 0)
                            amount = 0;

                        damage = amount;

                        if (target.VirtualArmorMod < 1)
                        {
                            target.VirtualArmorMod = 0;
                            target.FixedParticles(14276, 10, 20, 14276, EffectLayer.Waist);
                            target.PlaySound(0x38F);
                        }
                        else
                        {
                            string shield = target.VirtualArmorMod.ToString();
                            target.LocalOverheadMessage(MessageType.Emote, 90, true, shield);
                            target.FixedParticles(14265, 10, 20, 14276, EffectLayer.Waist);
                            target.PlaySound(0x525);
                        }
                    }
                }

				if (Utility.RandomMinMax(1,100) < cntmith)
				{
					m_Player.SendMessage("Your mithril arbsorbs the spell damage!");
					int armordam = (int)((damage / 2)/cntmith);
					int tmphits;
					damage = 0;
					
					for ( int i = 0; i < mithitems.Count; ++i )
					{
						BaseArmor tmparm = (BaseArmor)mithitems[i];
						if (tmparm.HitPoints > armordam)
							tmparm.HitPoints -= armordam;
						else
						{
							tmphits = tmparm.HitPoints;
							tmparm.HitPoints = 0;
							tmparm.MaxHitPoints = tmparm.MaxHitPoints - (armordam - tmphits);
							if (tmparm.MaxHitPoints < 1)
							{
								tmparm.Delete();
								m_Player.SendMessage("Your mithril armor has been destroyed!");
							}
							else
								m_Player.SendMessage("Your mithril armor has been severely damaged!");
						}
					}
				}
				
			}

			if (!KensaiAvoided)
				damage = CloakOfDarknessCheck( spell, target, damage );
			//

			TimeSpan ts = GetDamageDelayForSpell( spell );

			Damage( ts, target, spell.Caster, damage, phys, fire, cold, pois, nrgy );
		}

		public static void Damage( Spell spell, Mobile target, double damage, int phys, int fire, int cold, int pois, int nrgy, DFAlgorithm dfa )
		{
			TimeSpan ts = GetDamageDelayForSpell( spell );

			Damage( ts, target, spell.Caster, damage, phys, fire, cold, pois, nrgy, dfa );
		}

		public static void Damage( TimeSpan delay, Mobile target, double damage, int phys, int fire, int cold, int pois, int nrgy )
		{
			Damage( delay, target, null, damage, phys, fire, cold, pois, nrgy );
		}

		public static void Damage( TimeSpan delay, Mobile target, Mobile from, double damage, int phys, int fire, int cold, int pois, int nrgy )
		{
			Damage( delay, target, from, damage, phys, fire, cold, pois, nrgy, DFAlgorithm.Standard );
		}


		public static void Damage( TimeSpan delay, Mobile target, Mobile from, double damage, int phys, int fire, int cold, int pois, int nrgy, DFAlgorithm dfa )
		{
			int iDamage = (int) damage;

			if ( delay == TimeSpan.Zero )
			{
                if (from is BaseCreature)
                {
                    ((BaseCreature)from).AlterSpellDamageTo(target, ref iDamage);
                    AOS.Scale(iDamage, (100 - (int)(target.Skills.MagicResist.Value * .4)));
                }

				if ( target is BaseCreature )
					((BaseCreature)target).AlterSpellDamageFrom( from, ref iDamage );

				WeightOverloading.DFA = dfa;
				AOS.Damage( target, from, iDamage, phys, fire, cold, pois, nrgy );
				
				if ( from != null ) // sanity check
				{
					DoLeech( iDamage, from, target );
				}

				WeightOverloading.DFA = DFAlgorithm.Standard;
			}
			else
			{
				new SpellDamageTimerAOS( target, from, iDamage, phys, fire, cold, pois, nrgy, delay, dfa ).Start();
			}

			if ( target is BaseCreature && from != null && delay == TimeSpan.Zero )
				((BaseCreature)target).OnDamagedBySpell( from );
		}

		public static void DoLeech( int damageGiven, Mobile from, Mobile target )
		{
            TransformContext context = TransformationSpell.GetContext(from);

			if ( context != null ) /* cleanup */
			{
				if ( context.Type == typeof( WraithFormSpell ) )
				{
					int wraithLeech = ( 5 + (int)( ( 15 * from.Skills.SpiritSpeak.Value ) / 100 ) ); // Wraith form gives 5-20% mana leech
					int manaLeech = AOS.Scale( damageGiven, wraithLeech );
					if ( manaLeech != 0 )
					{
						from.Mana += manaLeech;
						from.PlaySound( 0x44D );
					}
				}
                else if (context.Type == typeof(VampiricEmbraceSpell))
				{
					from.Hits += AOS.Scale( damageGiven, 20 );
					from.PlaySound( 0x44D );
				}
			}
		}

		private class SpellDamageTimer : Timer
		{
			private Mobile m_Target, m_From;
			private int m_Damage;

			public SpellDamageTimer( Mobile target, Mobile from, int damage, TimeSpan delay ) : base( delay )
			{
				m_Target = target;
				m_From = from;
				m_Damage = damage;

				Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				if ( m_From is BaseCreature )
					((BaseCreature)m_From).AlterSpellDamageTo( m_Target, ref m_Damage );

				if ( m_Target is BaseCreature )
					((BaseCreature)m_Target).AlterSpellDamageFrom( m_From, ref m_Damage );

				m_Target.Damage( m_Damage );
			}
		}

		private class SpellDamageTimerAOS : Timer
		{
			private Mobile m_Target, m_From;
			private int m_Damage;
			private int m_Phys, m_Fire, m_Cold, m_Pois, m_Nrgy;
			private DFAlgorithm m_DFA;

			public SpellDamageTimerAOS( Mobile target, Mobile from, int damage, int phys, int fire, int cold, int pois, int nrgy, TimeSpan delay, DFAlgorithm dfa ) : base( delay )
			{
				m_Target = target;
				m_From = from;
				m_Damage = damage;
				m_Phys = phys;
				m_Fire = fire;
				m_Cold = cold;
				m_Pois = pois;
				m_Nrgy = nrgy;
				m_DFA = dfa;

				Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				if ( m_From is BaseCreature && m_Target != null )
					((BaseCreature)m_From).AlterSpellDamageTo( m_Target, ref m_Damage );

				if ( m_Target is BaseCreature && m_From != null )
					((BaseCreature)m_Target).AlterSpellDamageFrom( m_From, ref m_Damage );


				if ( m_From != null ) // sanity check
				{
					DoLeech( m_Damage, m_From, m_Target );
				}

				WeightOverloading.DFA = m_DFA;
				AOS.Damage( m_Target, m_From, m_Damage, m_Phys, m_Fire, m_Cold, m_Pois, m_Nrgy );
				WeightOverloading.DFA = DFAlgorithm.Standard;

				if ( m_Target is BaseCreature && m_From != null )
					((BaseCreature)m_Target).OnDamagedBySpell( m_From );
			}
		}
	}
}
