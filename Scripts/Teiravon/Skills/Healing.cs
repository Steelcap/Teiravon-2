using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;

namespace Server.SkillHandlers
{
	public class Healing
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Healing].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile from )
		{
			return TimeSpan.FromSeconds( 5.0 );
		}
	}
}