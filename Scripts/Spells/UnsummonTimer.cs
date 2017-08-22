using System;
using Server.Mobiles;

namespace Server.Spells
{
	class UnsummonTimer : Timer
	{
		private BaseCreature m_Creature;
		private Mobile m_Caster;

		public UnsummonTimer( Mobile caster, BaseCreature creature, TimeSpan delay ) : base( delay )
		{
			m_Caster = caster;
			m_Creature = creature;
			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick()
		{
			if ( !m_Creature.Deleted )
			{
				if (m_Creature is BaseMount)
				{
					BaseMount bm = (BaseMount)m_Creature;
					if (bm.Rider != null)
					{
						Mobile mob = (Mobile)bm.Rider;
						if (mob is TeiravonMobile)
						{
							TeiravonMobile tm = (TeiravonMobile)mob;
							tm.Dismounted();
						}
					}
				}
				m_Creature.Delete();
			}
		}
	}
}
