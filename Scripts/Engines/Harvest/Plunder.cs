using System;
using Server;
using Server.Items;
using Server.Factions;
using System.Collections;

namespace Server.Engines.Harvest
{
    public class Plundering : HarvestSystem
    {
        private static Plundering m_System;

        public static Plundering System
        {
            get
            {
                if (m_System == null)
                    m_System = new Plundering();

                return m_System;
            }
        }

        private HarvestDefinition m_Definition;

        public HarvestDefinition Definition
        {
            get { return m_Definition; }
        }

        private Plundering()
        {
            HarvestResource[] res;
            HarvestVein[] veins;

            #region Plundering
            HarvestDefinition plunder = new HarvestDefinition();

            // Resource banks are every 4x3 tiles
            plunder.BankWidth = 4;
            plunder.BankHeight = 3;

            // Every bank holds from 5 to 10 things
            plunder.MinTotal = 5;
            plunder.MaxTotal = 10;

            // A resource bank will respawn its content every 30 to 45 minutes
            plunder.MinRespawn = TimeSpan.FromMinutes(30.0);
            plunder.MaxRespawn = TimeSpan.FromMinutes(45.0);

            // Skill checking is done on the Plundering skill
            plunder.Skill = SkillName.Stealing;

            // Set the list of harvestable tiles
            plunder.Tiles = m_PlunderTiles;

            // Players must be within 2 tiles to harvest
            plunder.MaxRange = 1;

            // One item per harvest action
            plunder.ConsumedPerHarvest = 1;
            plunder.ConsumedPerFeluccaHarvest = 1;

            // The chopping effect
            plunder.EffectActions = new int[] { 34 };
            plunder.EffectSounds = new int[] { 0x241 };
            plunder.EffectCounts = new int[] { 5, 8 };
            plunder.EffectDelay = TimeSpan.FromSeconds(0.6);
            plunder.EffectSoundDelay = TimeSpan.FromSeconds(0.2);

            plunder.NoResourcesMessage = "There's nothing left here to plunder."; // There's nothing left here to plunder.
            plunder.FailMessage = "You rummage around for a bit with no luck."; // There's nothing left here to plunder.
            plunder.OutOfRangeMessage = 500446; // That is too far away.
            plunder.PackFullMessage = "There's no room in your pack for that."; // You can't place any wood into your backpack!
            plunder.ToolBrokeMessage = "You've snapped your pick."; // You broke your axe.

            res = new HarvestResource[]
				{
					new HarvestResource( 0.0, 0.0, 100.0, "You find some silver and put it in your backpack.", typeof( Silver ) ),
					new HarvestResource( 50.0, 50.0, 100.0, "You find some gold and put it in your backpack.", typeof( Gold ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 95.0, 0.0, res[0], null ),
					new HarvestVein( 5.0, 0.5, res[1], res[0] ),
				};

            plunder.Resources = res;
            plunder.Veins = veins;

            m_Definition = plunder;
            Definitions.Add(plunder);
            #endregion
        }

        public override void OnConcurrentHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            from.SendMessage("You're already plundering");
        }

        public override bool CheckHarvest(Mobile from, Item tool)
        {
            if (!base.CheckHarvest(from, tool))
                return false;

            return true;
        }

        public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            if (!base.CheckHarvest(from, tool, def, toHarvest))
                return false;

            return true;
        }

        public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
        {
            from.SendMessage("You can't plunder that."); // You can't use an axe on that.
        }
        
        public static void Initialize()
        {
            Array.Sort(m_PlunderTiles);

             for (int i = 0; i < m_PlunderTiles.Length; i++)
             {
                 PlunderID.Add(i,null);
             }
        }

        public static Hashtable PlunderID = new Hashtable();
        
        #region Tile lists
        private static int[] m_PlunderTiles = new int[]
			{
				0x49A7, 0x49A8, 0x49A9, 0x49AA, 0x4CD0, 0x4CD3, 0x4CD6, 0x4CD8,
				0x4CDA, 0x4CDD, 0x4CE0, 0x4CE3, 0x4CE6, 0x4CF8, 0x4CFB, 0x4CFE,
				0x4D01, 0x4D41, 0x4D42, 0x4D43, 0x4D44, 0x4D57, 0x4D58, 0x4D59,
				0x4D5A, 0x4D5B, 0x4D6E, 0x4D6F, 0x4D70, 0x4D71, 0x4D72, 0x4D84,
				0x4D85, 0x4D86, 0x52B5, 0x52B6, 0x52B7, 0x52B8, 0x52B9, 0x52BA,
				0x52BB, 0x52BC, 0x52BD,

				0x4CCE, 0x4CCF, 0x4CD1, 0x4CD2, 0x4CD4, 0x4CD5, 0x4CD7, 0x4CD9,
				0x4CDB, 0x4CDC, 0x4CDE, 0x4CDF, 0x4CE1, 0x4CE2, 0x4CE4, 0x4CE5,
				0x4CE7, 0x4CE8, 0x4CF9, 0x4CFA, 0x4CFC, 0x4CFD, 0x4CFF, 0x4D00,
				0x4D02, 0x4D03, 0x4D45, 0x4D46, 0x4D47, 0x4D48, 0x4D49, 0x4D4A,
				0x4D4B, 0x4D4C, 0x4D4D, 0x4D4E, 0x4D4F, 0x4D50, 0x4D51, 0x4D52,
				0x4D53, 0x4D5C, 0x4D5D, 0x4D5E, 0x4D5F, 0x4D60, 0x4D61, 0x4D62,
				0x4D63, 0x4D64, 0x4D65, 0x4D66, 0x4D67, 0x4D68, 0x4D69, 0x4D73,
				0x4D74, 0x4D75, 0x4D76, 0x4D77, 0x4D78, 0x4D79, 0x4D7A, 0x4D7B,
				0x4D7C, 0x4D7D, 0x4D7E, 0x4D7F, 0x4D87, 0x4D88, 0x4D89, 0x4D8A,
				0x4D8B, 0x4D8C, 0x4D8D, 0x4D8E, 0x4D8F, 0x4D90, 0x4D95, 0x4D96,
				0x4D97, 0x4D99, 0x4D9A, 0x4D9B, 0x4D9D, 0x4D9E, 0x4D9F, 0x4DA1,
				0x4DA2, 0x4DA3, 0x4DA5, 0x4DA6, 0x4DA7, 0x4DA9, 0x4DAA, 0x4DAB,
				0x52BE, 0x52BF, 0x52C0, 0x52C1, 0x52C2, 0x52C3, 0x52C4, 0x52C5,
				0x52C6, 0x52C7
			};
        #endregion
    }
}