using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Misc
{
	public class SkillCheck
	{
		private const bool AntiMacroCode = false;		//Change this to false to disable anti-macro code

		public static TimeSpan AntiMacroExpire = TimeSpan.FromMinutes( 5.0 ); //How long do we remember targets/locations?
		public const int Allowance = 3;	//How many times may we use the same location/target for gain
		private const int LocationSize = 5; //The size of eeach location, make this smaller so players dont have to move as far
		private static bool[] UseAntiMacro = new bool[]
		{
			// true if this skill uses the anti-macro code, false if it does not
			false,// Alchemy = 0,
			false,// Anatomy = 1,
			false,// AnimalLore = 2,
			false,// ItemID = 3,
			false,// ArmsLore = 4,
			false,// Parry = 5,
			false,// Begging = 6,
			false,// Blacksmith = 7,
			false,// Fletching = 8,
			false,// Peacemaking = 9,
			false,// Camping = 10,
			false,// Carpentry = 11,
			false,// Cartography = 12,
			false,// Cooking = 13,
			false,// DetectHidden = 14,
			false,// Discordance = 15,
			false,// EvalInt = 16,
			false,// Healing = 17,
			false,// Fishing = 18,
			false,// Forensics = 19,
			false,// Herding = 20,
			false,// Hiding = 21,
			false,// Provocation = 22,
			false,// Inscribe = 23,
			false,// Lockpicking = 24,
			false,// Magery = 25,
			false,// MagicResist = 26,
			false,// Tactics = 27,
			false,// Snooping = 28,
			false,// Musicianship = 29,
			false,// Poisoning = 30,
			false,// Archery = 31,
			false,// SpiritSpeak = 32,
			false,// Stealing = 33,
			false,// Tailoring = 34,
			false,// AnimalTaming = 35,
			false,// TasteID = 36,
			false,// Tinkering = 37,
			false,// Tracking = 38,
			false,// Veterinary = 39,
			false,// Swords = 40,
			false,// Macing = 41,
			false,// Fencing = 42,
			false,// Wrestling = 43,
			false,// Lumberjacking = 44,
			false,// Mining = 45,
			false,// Meditation = 46,
			false,// Stealth = 47,
			false,// RemoveTrap = 48,
			false,// Necromancy = 49,
			false,// Focus = 50,
			false,// Chivalry = 51
			false,// Bushido = 52
			false,//Ninjitsu = 53
		};

		public static void Initialize()
		{
// Begin mod to enable XmlSpawner skill triggering
Mobile.SkillCheckLocationHandler = new SkillCheckLocationHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckLocation );
Mobile.SkillCheckDirectLocationHandler = new SkillCheckDirectLocationHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckDirectLocation );

Mobile.SkillCheckTargetHandler = new SkillCheckTargetHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckTarget );
Mobile.SkillCheckDirectTargetHandler = new SkillCheckDirectTargetHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckDirectTarget );
// End mod to enable XmlSpawner skill triggering

			//*********EDIT THESE LINES TO SET THE BASE PERCENT CHANCE TO GAIN FOR EACH INDIVIDUAL SKILL***********************//
			//This Table used to set the base multiplier to gain skill
			SkillInfo.Table[0].GainFactor = 1.5;// Alchemy = 0,
			SkillInfo.Table[1].GainFactor = 0.8;// Anatomy = 1,
			SkillInfo.Table[2].GainFactor = 1.0;// AnimalLore = 2,
			SkillInfo.Table[3].GainFactor = 1.0;// ItemID = 3,
			SkillInfo.Table[4].GainFactor = 1.0;// ArmsLore = 4,
			SkillInfo.Table[5].GainFactor = 1.0;// Parry = 5,
			SkillInfo.Table[6].GainFactor = 1.0;// Begging = 6,
			SkillInfo.Table[7].GainFactor = 1.0;// Blacksmith = 7,
			SkillInfo.Table[8].GainFactor = 1.0;// Fletching = 8,
			SkillInfo.Table[9].GainFactor = 1.0;// Peacemaking = 9,
			SkillInfo.Table[10].GainFactor = 1.0;// Camping = 10,
			SkillInfo.Table[11].GainFactor = 1.0;// Carpentry = 11,
			SkillInfo.Table[12].GainFactor = 1.0;// Cartography = 12,
			SkillInfo.Table[13].GainFactor = 2.5;// Cooking = 13,
			SkillInfo.Table[14].GainFactor = 1.0;// DetectHidden = 14,
			SkillInfo.Table[15].GainFactor = 1.6;// Discordance = 15,
			SkillInfo.Table[16].GainFactor = 1.0;// EvalInt = 16,
			SkillInfo.Table[17].GainFactor = 1.0;// Healing = 17,
			SkillInfo.Table[18].GainFactor = 2.0;// Fishing = 18,
			SkillInfo.Table[19].GainFactor = 1.0;// Forensics = 19,
			SkillInfo.Table[20].GainFactor = 1.0;// Herding = 20,
			SkillInfo.Table[21].GainFactor = 1.1;// Hiding = 21,
			SkillInfo.Table[22].GainFactor = 1.6;// Provocation = 22,
			SkillInfo.Table[23].GainFactor = 1.0;// Inscribe = 23,
			SkillInfo.Table[24].GainFactor = 2.0;// Lockpicking = 24,
			SkillInfo.Table[25].GainFactor = 1.0;// Magery = 25,
			SkillInfo.Table[26].GainFactor = 1.5;// MagicResist = 26,
			SkillInfo.Table[27].GainFactor = 0.8;// Tactics = 27,
			SkillInfo.Table[28].GainFactor = 1.5;// Snooping = 28,
			SkillInfo.Table[29].GainFactor = 1.0;// Musicianship = 29,
			SkillInfo.Table[30].GainFactor = 1.0;// Poisoning = 30
			SkillInfo.Table[31].GainFactor = 0.9;// Archery = 31
			SkillInfo.Table[32].GainFactor = 1.0;// SpiritSpeak = 32
			SkillInfo.Table[33].GainFactor = 1.5;// Stealing = 33
			SkillInfo.Table[34].GainFactor = 1.0;// Tailoring = 34
			SkillInfo.Table[35].GainFactor = 2.5;// AnimalTaming = 35
			SkillInfo.Table[36].GainFactor = 1.0;// TasteID = 36
			SkillInfo.Table[37].GainFactor = 1.0;// Tinkering = 37
			SkillInfo.Table[38].GainFactor = 1.0;// Tracking = 38
			SkillInfo.Table[39].GainFactor = 1.0;// Veterinary = 39
			SkillInfo.Table[40].GainFactor = 0.6;// Swords = 40
			SkillInfo.Table[41].GainFactor = 0.6;// Macing = 41
			SkillInfo.Table[42].GainFactor = 0.6;// Fencing = 42
			SkillInfo.Table[43].GainFactor = 0.8;// Wrestling = 43
			SkillInfo.Table[44].GainFactor = 1.0;// Lumberjacking = 44
			SkillInfo.Table[45].GainFactor = 1.0;// Mining = 45
			SkillInfo.Table[46].GainFactor = 1.0;// Meditation = 46
			SkillInfo.Table[47].GainFactor = 1.1;// Stealth = 47
			SkillInfo.Table[48].GainFactor = 1.0;// RemoveTrap = 48
			SkillInfo.Table[49].GainFactor = 1.0;// Necromancy = 49
			SkillInfo.Table[50].GainFactor = 1.0;// Focus = 50
			SkillInfo.Table[51].GainFactor = 1.0;// Chivalry = 51
			SkillInfo.Table[52].GainFactor = 1.0;// Bushido = 52
			SkillInfo.Table[53].GainFactor = 1.0;// Ninjitsu = 53
		}

		public static bool Mobile_SkillCheckLocation( Mobile from, SkillName skillName, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			double value = skill.Value;

			if ( value < minSkill )
				return false; // Too difficult
			else if ( value >= maxSkill )
				return true; // No challenge

			double chance = ( value - minSkill ) / ( maxSkill - minSkill );

			//rw3: Temp fix
			//if ( skillName == SkillName.Fletching || skillName == SkillName.Carpentry || skillName == SkillName.Tailoring )
			//	chance = 0.5 + ( value - minSkill ) / ( 2.0 * ( maxSkill - minSkill ) );

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

		public static bool Mobile_SkillCheckDirectLocation( Mobile from, SkillName skillName, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

		public static bool CheckSkill( Mobile from, Skill skill, object amObj, double chance )
		{
			if ( from.Skills.Cap == 0 )
				return false;

			bool success = ( chance >= Utility.RandomDouble() );

			//**********EDIT THESE LINES FOR MODIFICATIONS TO BASE PERCENTAGE AT DIFFERENT SKILL LEVELS************************//
			double LowSkillMod = 1.3; // multiplier to skill gain for skill between 0 - 40
			double BaseSkillGain = skill.Info.GainFactor * (1 + (double)((double)from.Int / 200)); // base percent to gain at skill between 40 - 70 set in table above.
			double HighSkillMod = .4; // multiplier to base gain for skill between 70 - 90
			double VeryHighSkillMod = .15; // multiplier to base gain for skill between 90-100
			double AboveGmSkillMod = .1; // mulitplier base gain for skill over 100

			//************************************************** ************************************************** *************//

            double ComputedSkillMod = ComputedSkillMod = (((1.0 - chance) / 2) * BaseSkillGain); // holds the percent chance to gain after checking players skill
            /*
			if ( skill.Base < 40.1 )
				ComputedSkillMod = ( ( ( 1.0 - chance ) / 2 ) * BaseSkillGain ) * LowSkillMod;
			else if ( skill.Base < 70.1 )
				ComputedSkillMod = ( ( ( 1.0 - chance ) / 2 ) * BaseSkillGain );
			else if ( skill.Base < 90.1 )
				ComputedSkillMod = ( ( ( 1.0 - chance ) / 2 ) * BaseSkillGain ) * HighSkillMod;
			else if ( skill.Base < 100.1 )
				ComputedSkillMod = ( ( ( 1.0 - chance ) / 2 ) * BaseSkillGain ) * VeryHighSkillMod;
			else if ( skill.Base > 100.0 )
				ComputedSkillMod = ( ( ( 1.0 - chance ) / 2 ) * BaseSkillGain ) * AboveGmSkillMod;
            */
			if ( from is BaseCreature && ( ( BaseCreature )from ).Controled )
				ComputedSkillMod *= 2;

			if ( !success && skill.Base > 40.1)
				ComputedSkillMod *= 0.2;

			if ( ComputedSkillMod < 0.005 )
				ComputedSkillMod = 0.005;

            //Mercy Rule for learning new skills.
            if (skill.Base < 20.0 && ComputedSkillMod < .2)
                ComputedSkillMod = .2;

			//following line used to see chance to gain ingame
			//from.SendMessage( "Chance is: {0} and Success was: {1}", chance, success );
			//from.SendMessage( "Your chance to gain {0} is {1} %",skill.Name, (double)(ComputedSkillMod) * 100 );

            if (skill.SkillName == SkillName.Carpentry || skill.SkillName == SkillName.Blacksmith || skill.SkillName == SkillName.Tailoring || skill.SkillName == SkillName.Fletching || skill.SkillName == SkillName.Tinkering)
            {
                if (from is TeiravonMobile)
                {
                    TeiravonMobile tav = from as TeiravonMobile;
                    int featstotal = 0;
                    if (tav.HasFeat(TeiravonMobile.Feats.BlacksmithTraining))
                        featstotal++;
                    if (tav.HasFeat(TeiravonMobile.Feats.CarpenterTraining))
                        featstotal++;
                    if (tav.HasFeat(TeiravonMobile.Feats.TailorTraining))
                        featstotal++;
                    if (tav.HasFeat(TeiravonMobile.Feats.TinkerTraining))
                        featstotal++;
                    if (tav.HasFeat(TeiravonMobile.Feats.CookTraining))
                        featstotal++;
                    if (tav.HasFeat(TeiravonMobile.Feats.FletcherTraining))
                        featstotal++;

                    if (featstotal > 0)
                        ComputedSkillMod *=(  1.15 - (double)(((featstotal*15.0))/100.0));
                    
                    if (tav.HasFeat(TeiravonMobile.Feats.ShoddyCrafts))
                    {
                        ComputedSkillMod *= 3;
                    }
                }
            }

			bool jailed = false;

			if ( from.Backpack != null )
			{
				Item m_JailedMarker = from.Backpack.FindItemByType( typeof( Items.JailedMarker ) );

				if ( m_JailedMarker != null )
					jailed = true;
			}

			if ( from.Alive && ( ( ComputedSkillMod >= Utility.RandomDouble() && AllowGain( from, skill, amObj ) ) || skill.Base < 10.0 ) )
			{
				if ( !jailed )
					Gain( from, skill );

				if ( jailed )
					from.SendMessage( "You cannot gain any skill since you are jailed!" );
			}

			return success;
		}

		public static bool Mobile_SkillCheckTarget( Mobile from, SkillName skillName, object target, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			double value = skill.Value;

			if ( value < minSkill )
				return false; // Too difficult
			else if ( value >= maxSkill )
				return true; // No challenge

			double chance = ( value - minSkill ) / ( maxSkill - minSkill );

			return CheckSkill( from, skill, target, chance );
		}

		public static bool Mobile_SkillCheckDirectTarget( Mobile from, SkillName skillName, object target, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			return CheckSkill( from, skill, target, chance );
		}

		private static bool AllowGain( Mobile from, Skill skill, object obj )
		{

			TeiravonMobile m_Player = from as TeiravonMobile;

			if ( m_Player == null )
				return true;

			/*if ( m_Player.AntiMacroCheckTime == null )
			{
				for ( int i = 0; i < 52; i++ )
					m_Player.AntiMacroCheckTime[i] = DateTime.Now;

				for ( int i = 0; i < 52; i++ )
					m_Player.AntiMacroCheckCount[i] = 0;
			}

			bool goahead = true;

			switch ( skill.SkillID )
			{
				case (int)SkillName.Blacksmith:
				case (int)SkillName.Mining:
				case (int)SkillName.Carpentry:
				case (int)SkillName.Lumberjacking:
				case (int)SkillName.Tailoring:
				case (int)SkillName.Fletching:
				case (int)SkillName.Alchemy:
				case (int)SkillName.Tinkering:
				case (int)SkillName.AnimalTaming:
				case (int)SkillName.AnimalLore:
				case (int)SkillName.Tactics:
				case (int)SkillName.Swords:
				case (int)SkillName.Macing:
				case (int)SkillName.Archery:
				case (int)SkillName.Fencing:
				case (int)SkillName.Parry:
				case (int)SkillName.Wrestling:
				case (int)SkillName.Fishing:
				case (int)SkillName.Cooking:
				case (int)SkillName.Healing:
				case (int)SkillName.Magery:
					goahead = false;
					break;
			}

			if ( goahead )
			{
				DateTime check = m_Player.AntiMacroCheckTime[ skill.SkillID ];
				TimeSpan ts = check - DateTime.Now;

				if ( ts > TimeSpan.Zero )
					return false;
				else
				{
					m_Player.AntiMacroCheckCount[ skill.SkillID ] += 1;

					if ( m_Player.AntiMacroCheckCount[ skill.SkillID ] > 30 )
					{
						m_Player.AntiMacroCheckCount[ skill.SkillID ] = 0;
						m_Player.AntiMacroCheckTime[ skill.SkillID ] = DateTime.Now + TimeSpan.FromHours( 1 );

						Console.WriteLine( "Starting up anti-macro code for {3} on {2}... Next skill gain allowed at {0}. Current time: {1}.", m_Player.AntiMacroCheckTime[ skill.SkillID ].ToString(), DateTime.Now.ToString(), m_Player.Name, skill.Name );
					}

					if ( from is PlayerMobile && AntiMacroCode && UseAntiMacro[skill.Info.SkillID] )
						return ((PlayerMobile)from).AntiMacroCheck( skill, obj );
				}
			}*/

			if ( from is PlayerMobile && AntiMacroCode && UseAntiMacro[skill.Info.SkillID] )
				return ( ( PlayerMobile )from ).AntiMacroCheck( skill, obj );
			else
				return true;
		}

		public enum Stat { Str, Dex, Int }

		public static void Gain( Mobile from, Skill skill )
		{
			if ( from.Region is Regions.Jail )
				return;

			if ( from is BaseCreature && ( ( BaseCreature )from ).IsDeadPet )
				return;

			if ( skill.SkillName == SkillName.Focus && from is BaseCreature )
				return;

			if ( skill.Base < skill.Cap && skill.Lock == SkillLock.Up )
			{
				int toGain = 1;

				if ( skill.Base <= 10.0 )
					toGain = Utility.Random( 4 ) + 1;

				Skills skills = from.Skills;

				if ( ( skills.Total / skills.Cap ) >= Utility.RandomDouble() )//( skills.Total >= skills.Cap )
				{
					for ( int i = 0; i < skills.Length; ++i )
					{
						Skill toLower = skills[i];

						if ( toLower != skill && toLower.Lock == SkillLock.Down && toLower.BaseFixedPoint >= toGain )
						{
							toLower.BaseFixedPoint -= toGain;
							break;
						}
					}
				}

				if ( ( skills.Total + toGain ) <= skills.Cap )
					skill.BaseFixedPoint += toGain;
			}
		}

		public static bool CanLower( Mobile from, Stat stat )
		{
			/*
			switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Down && from.RawStr > 10 );
				case Stat.Dex: return ( from.DexLock == StatLockType.Down && from.RawDex > 10 );
				case Stat.Int: return ( from.IntLock == StatLockType.Down && from.RawInt > 10 );
			}
			*/
			return false;
		}

		public static bool CanRaise( Mobile from, Stat stat )
		{
			/*
			if ( !(from is BaseCreature && ((BaseCreature)from).Controled) )
			{
				if ( from.RawStatTotal >= from.StatCap )
					return false;
			}

			switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Up && from.RawStr < 125 );
				case Stat.Dex: return ( from.DexLock == StatLockType.Up && from.RawDex < 125 );
				case Stat.Int: return ( from.IntLock == StatLockType.Up && from.RawInt < 125 );
			}
			*/

			return false;
		}

		public static void IncreaseStat( Mobile from, Stat stat, bool atrophy )
		{
			atrophy = atrophy || ( from.RawStatTotal >= from.StatCap );

			switch ( stat )
			{
				case Stat.Str:
					{
						if ( atrophy )
						{
							if ( CanLower( from, Stat.Dex ) && ( from.RawDex < from.RawInt || !CanLower( from, Stat.Int ) ) )
								--from.RawDex;
							else if ( CanLower( from, Stat.Int ) )
								--from.RawInt;
						}

						if ( CanRaise( from, Stat.Str ) )
							++from.RawStr;

						break;
					}
				case Stat.Dex:
					{
						if ( atrophy )
						{
							if ( CanLower( from, Stat.Str ) && ( from.RawStr < from.RawInt || !CanLower( from, Stat.Int ) ) )
								--from.RawStr;
							else if ( CanLower( from, Stat.Int ) )
								--from.RawInt;
						}

						if ( CanRaise( from, Stat.Dex ) )
							++from.RawDex;

						break;
					}
				case Stat.Int:
					{
						if ( atrophy )
						{
							if ( CanLower( from, Stat.Str ) && ( from.RawStr < from.RawDex || !CanLower( from, Stat.Dex ) ) )
								--from.RawStr;
							else if ( CanLower( from, Stat.Dex ) )
								--from.RawDex;
						}

						if ( CanRaise( from, Stat.Int ) )
							++from.RawInt;

						break;
					}
			}
		}

		private static TimeSpan m_StatGainDelay = TimeSpan.FromMinutes( 15.0 );

		public static void GainStat( Mobile from, Stat stat )
		{
			if ( ( from.LastStatGain + m_StatGainDelay ) >= DateTime.Now )
				return;

			from.LastStatGain = DateTime.Now;

			bool atrophy = ( ( from.RawStatTotal / ( double )from.StatCap ) >= Utility.RandomDouble() );

			IncreaseStat( from, stat, atrophy );
		}
	}
}