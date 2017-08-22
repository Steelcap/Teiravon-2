using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
    public class DefFrostcraft : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Carpentry; }
        }

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
        public override string  GumpTitleString
        {
            get { return "<basefont color=WHITE><CENTER>Frostcrafting Menu</CENTER>"; }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefFrostcraft();

                return m_CraftSystem;
            }
        }

        private DefFrostcraft()
            : base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x057);
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else
                    return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
            int index = -1;
            // TOOLS
            index = AddCraft(typeof(FrostPick), "Tools", "Frostpick", 0.0, 20.0, typeof(Snow), "Snow", 2);
            AddRes(index, typeof(Frost), "Frost", 1);
            AddCraft(typeof(Frostcarver), "Tools", "Frostcarver", 0, 25.0, typeof(Snow), "Snow", 3);
            AddCraft(typeof(Scissors), "Tools", "Frozen Sheers", 15, 35.0, typeof(Frost), "Frost", 3);

            // ARMOR
            index = AddCraft(typeof(SnowScaleGloves), "SnowScale", "Snowscale Gloves", 12.0, 62.0, typeof(Frost), "Frost", 8);
            AddRes(index, typeof(Snow), "Snow", 3);
            index = AddCraft(typeof(SnowScaleLeggings), "SnowScale", "Snowscale Leggings", 19.4, 69.4, typeof(Frost), "Frost", 12);
            AddRes(index, typeof(Snow), "Snow", 5);
            index = AddCraft(typeof(SnowScaleSleeves), "SnowScale", "Snowscale Sleeves", 16.9, 66.9, typeof(Frost), "Frost", 10);
            AddRes(index, typeof(Snow), "Snow", 4);
            index = AddCraft(typeof(SnowScaleChest), "SnowScale", "Snowscale Tunic", 21.9, 71.9, typeof(Frost), "Frost", 14);
            AddRes(index, typeof(Snow), "Snow", 6);

            index = AddCraft(typeof(ArcticPlateArms), "ArcticPlate", "ArcticPlate Arms", 66.3, 106.3, typeof(Frost), "Frost", 18);
            AddRes(index, typeof(Snow), "Snow", 11);
            index = AddCraft(typeof(ArcticPlateGloves), "ArcticPlate", "ArcticPlate Gloves", 58.9, 108.9, typeof(Frost), "Frost", 12);
            AddRes(index, typeof(Snow), "Snow", 7);
            index = AddCraft(typeof(ArcticPlateGorget), "ArcticPlate", "ArcticPlate Gorget", 56.4, 106.4, typeof(Frost), "Frost", 10);
            AddRes(index, typeof(Snow), "Snow", 6);
            index = AddCraft(typeof(ArcticPlateLegs), "ArcticPlate", "ArcticPlate Legs", 68.8, 108.8, typeof(Frost), "Frost", 20);
            AddRes(index, typeof(Snow), "Snow", 12);
            index = AddCraft(typeof(ArcticPlateChest), "ArcticPlate", "ArcticPlate Chest", 75.0, 109.0, typeof(Frost), "Frost", 25);
            AddRes(index, typeof(Snow), "Snow", 14);
            index = AddCraft(typeof(ArcticPlateHelm), "ArcticPlate", "ArcticPlate Helm", 62.6, 112.6, typeof(Frost), "Frost", 15);
            AddRes(index, typeof(Snow), "Snow", 9);

            //WEAPONS
            index = AddCraft(typeof(FrostAxe), "Weapons", "Frigid Axe", 29.3, 79.3, typeof(Frost), "Frost", 12);
            AddRes(index, typeof(PermaFrost), "Permafrost", 6);

            index = AddCraft(typeof(IceBlade), "Weapons", "Frozen Blade", 28.0, 84.0, typeof(Frost), "Frost", 9);
            AddRes(index, typeof(PermaFrost), "Permafrost", 6);

            index = AddCraft(typeof(GlacialMace), "Weapons", "Polar Mace", 28.0, 78.0, typeof(Frost), "Frost", 12);
            AddRes(index, typeof(PermaFrost), "PermaFrost", 4);

            index = AddCraft(typeof(IcicleLance), "Weapons", "Icicle Lance", 31, 90.0, typeof(Frost), "Frost", 15);
            AddRes(index, typeof(PermaFrost), "PermaFrost", 8);

            // Set the overidable material
            SetSubRes(typeof(Frost), "Frost");

            // Add every material you want the player to be able to chose from
            // This will overide the overidable material
            AddSubRes(typeof(Frost), "Frost", 00.0, "You don't have enough skill/resources for that.");
            AddSubRes(typeof(Ice), "Ice", 40.0, "You don't have enough skill/resources for that.");
            AddSubRes(typeof(GlacialIce), "Glacial", 70.0, "You don't have enough skill/resources for that.");
        }
    }
}