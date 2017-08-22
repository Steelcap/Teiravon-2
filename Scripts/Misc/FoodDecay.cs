using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	public class FoodDecayTimer : Timer
	{
		public static void Initialize()
		{
			new FoodDecayTimer().Start();
		}

		public FoodDecayTimer() : base( TimeSpan.FromMinutes( 15 ), TimeSpan.FromMinutes( 15 ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			FoodDecay();			
		}

		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HungerDecay( state.Mobile );
				ThirstDecay( state.Mobile );
			}
		}

		public static void HungerDecay( Mobile m )
		{
			if ( m != null && m.Hunger >= 1 )
				m.Hunger -= 1;

            if (m is TeiravonMobile)
            {
                TeiravonMobile tav = m as TeiravonMobile;
                if (tav.HasFeat(TeiravonMobile.Feats.BodyoftheGrave))
                    tav.Hunger = 20;
            }
		}

		public static void ThirstDecay( Mobile m )
		{
			if ( m != null && m.Thirst >= 1 )
				m.Thirst -= 1;
		}
	}
}