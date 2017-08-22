using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public enum FinishResult
	{
		NotInBackpack,
		BadItem,
		AlreadyFinishd,
		FinishNext,
		WrongOne,
        BadContext
	}

	public class Finish
	{
		public static FinishResult Invoke( Mobile from, CraftSystem craftSystem, BaseTool tool, Item item, ref object resMessage, bool polish )
		{
            TeiravonMobile tav = (TeiravonMobile)from;
            CraftContext context = craftSystem.GetContext( from );
            if (context == null)
                return FinishResult.BadItem;

            CraftItem Craft_item = context.LastMade;
            if (Craft_item == null)
                return FinishResult.BadItem;

			if ( item == null )
				return FinishResult.BadItem;

			if ( !item.IsChildOf( from.Backpack ) )
				return FinishResult.NotInBackpack;

            if (!(item == tav.LastCrafted) || !(item.GetType() == Craft_item.ItemType))
                return FinishResult.BadItem;

			CraftItem craftItem = craftSystem.CraftItems.SearchFor( item.GetType() );

			if ( craftItem == null || craftItem.Ressources.Count == 0 )
				return FinishResult.BadItem;

            if (tav.NeedPolish != polish)
                return FinishResult.WrongOne;

            if (tav.LastCraftTime < DateTime.Now - TimeSpan.FromMinutes(2.0))
                return FinishResult.AlreadyFinishd;

            return FinishResult.FinishNext;

		}

		public static void BeginTarget( Mobile from, CraftSystem craftSystem, BaseTool tool, bool polish )
		{
			CraftContext context = craftSystem.GetContext( from );
            
			if ( context == null )
				return;
            
            from.Target = new InternalTarget( craftSystem, tool, polish );
			from.SendMessage( "Target an item you wish to refine your work on" );

		}

		private class InternalTarget : Target
		{
			private CraftSystem m_CraftSystem;
			private BaseTool m_Tool;
			private Type m_ResourceType;
			private CraftResource m_Resource;
            private bool m_Polish;

			public InternalTarget( CraftSystem craftSystem, BaseTool tool, bool polish ) :  base ( 2, false, TargetFlags.None )
			{
                m_Polish = polish;
				m_CraftSystem = craftSystem;
				m_Tool = tool;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					object message = null;
					FinishResult res = Finish.Invoke( from, m_CraftSystem, m_Tool, (Item)targeted, ref message, m_Polish );
                    bool bad = true;
					switch ( res )
					{
						case FinishResult.NotInBackpack: message = "The item must be in your backpack to finish it."; break; // The item must be in your backpack to Finish it.
                        case FinishResult.BadContext: message = "Context not found."; break; // The item must be in your backpack to Finish it.
                        case FinishResult.AlreadyFinishd: message = "You've done as much finishing work as you can on this item."; break; // This item is already Finishd with the properties of a special material.
                        case FinishResult.BadItem: message = "You may not do any finishing work on that."; break; // You cannot Finish this type of item with the properties of the selected special material.
                        case FinishResult.WrongOne: message = "No, that's not right at all, you've ruined any hope of perfecting it."; ((TeiravonMobile)from).LastCrafted = null; break;
                        case FinishResult.FinishNext:
                            {
                                bad = false;
                                from.BeginAction(typeof(CraftSystem));
                                new InternalTimer(from,m_CraftSystem,m_Tool,Utility.RandomMinMax(4,10)).Start();
                                break;
                            }
					}
                    if (bad)
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, message));
				}
			}
		}

        private class InternalTimer : Timer
        {
            private Mobile m_From;
            private int m_iCount;
            private int m_iCountMax;
            private CraftItem m_CraftItem;
            private CraftSystem m_CraftSystem;
            private Type m_TypeRes;
            private BaseTool m_Tool;

            public InternalTimer(Mobile from, CraftSystem craftSystem, BaseTool tool, int iCountMax)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(craftSystem.Delay * 2.0), iCountMax)
            {
                CraftContext context = craftSystem.GetContext(from);
                m_CraftItem = context.LastMade;
                m_From = from;
                m_iCount = 0;
                m_iCountMax = iCountMax;
                m_CraftSystem = craftSystem;
                m_TypeRes = m_CraftItem.GetType();
                m_Tool = tool;
            }

            protected override void OnTick()
            {
                m_iCount++;

                m_From.DisruptiveAction();

                if (m_iCount < m_iCountMax)
                {
                    m_CraftSystem.PlayCraftEffect(m_From);
                    if (m_From.Body.Type == BodyType.Human && !m_From.Mounted)
                        m_From.Animate(9, 5, 1, true, false, 0);
                }
                else
                {

                    m_From.EndAction(typeof(CraftSystem));
                    
                    CheckGain();
                    if (m_From is TeiravonMobile)
                    {
                        TeiravonMobile tav = m_From as TeiravonMobile;

                        bool polish = Utility.RandomBool();
                        tav.NeedPolish = polish;
                        string message = polish ? ("It could use more Polishing") : ("It could use more Finishing");
                        tav.SendGump(new CraftGump(tav, m_CraftSystem, m_Tool, message));
                    }
                }
            }

            private void CheckGain()
            {
                int quality = 0;
                bool allRequiredSkills = true;

                double minMainSkill = 0.0;
                double maxMainSkill = 0.0;
                double valMainSkill = 0.0;

                m_CraftItem.CheckSkills(m_From, m_TypeRes, m_CraftSystem, ref quality, ref allRequiredSkills);
                CraftSkillCol m_arCraftSkill = m_CraftItem.Skills;

                for (int i = 0; i < m_arCraftSkill.Count; i++)
                {
                    CraftSkill craftSkill = m_arCraftSkill.GetAt(i);

                    double minSkill = craftSkill.MinSkill;
                    double maxSkill = craftSkill.MaxSkill;
                    double valSkill = m_From.Skills[craftSkill.SkillToMake].Value;

                    if (valSkill < minSkill)
                        allRequiredSkills = false;

                    if (craftSkill.SkillToMake == m_CraftSystem.MainSkill)
                    {
                        minMainSkill = minSkill;
                        maxMainSkill = maxSkill;
                        valMainSkill = valSkill;
                    }
                    Skill skill = m_From.Skills[craftSkill.SkillToMake];

                    double chance = (valSkill - minSkill) / (maxSkill - minSkill);
                    bool success = (chance >= Utility.RandomDouble());
                    if (success)
                    {
                        double ComputedSkillMod = (((1.0 - chance) / 2) * 1.0);

                        if (ComputedSkillMod >= Utility.RandomDouble())
                            Server.Misc.SkillCheck.Gain(m_From, skill);
                    }
                }
            }
        }
	}
}