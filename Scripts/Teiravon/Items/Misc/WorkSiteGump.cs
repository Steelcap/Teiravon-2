using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{
    public class WorkProgressGump : Gump
    {
        WorkSite WorkSite;
        public WorkProgressGump(WorkSite wpoint)
            : base(50, 50)
        {
            WorkSite = wpoint;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 348, 256, 9270);
            this.AddBackground(21, 20, 306, 214, 9200);
            this.AddLabel(28, 29, 52, String.Format("Work Site Progress")); //title of gump
            this.AddLabel(31, 59, 0, String.Format("Labour Complete: {0}%", WorkSite.PercentComplete)); //labour complete %
            this.AddLabel(31, 80, 0, String.Format("{0} Collected: {1} of {2} Required",WorkSite.ResourceType.ToString(), WorkSite.GetAmount(WorkSite.ResourceType, true), (WorkSite.ResourcesRequired * (WorkSite.TotalPointsRequired - WorkSite.ProgressPoints)) )); //# collected %
            this.AddLabel(31, 99, 0, String.Format("Required Skill: {0} {1}", WorkSite.SkillLevel, WorkSite.RequiredSkill)); //Skills needed
            this.AddLabel(31, 139, 0, String.Format("Total Workers: {0}", WorkSite.TotalWorkers)); //hides collected %
            this.AddImage(299, 208, 5560);
            if (WorkSite.PercentComplete < 100 && WorkSite.TotalPointsRequired > 0) //if complete, do not allow to apply for work!
            {
                this.AddLabel(36, 179, 0, @"Begin Working");
                this.AddButton(134, 178, 247, 248, (int)Buttons.Button1, GumpButtonType.Reply, 0);
            }

        }

        public enum Buttons
        {
            Button1 = 1,
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            base.OnResponse(sender, info);
            if (info.ButtonID == (int)Buttons.Button1) //wants to begin work!
            {
                    WorkSite.DelayWork(sender.Mobile);
                    ((TeiravonMobile)sender.Mobile).BlockAction(typeof(WorkSite), TimeSpan.FromSeconds(2.0));
            }
        }
}
    /*
    public class WorkProgressGoldGump : Gump
    {
        WorkSiteGold WorkSite;
        public WorkProgressGoldGump(WorkSiteGold wpoint)
            : base(50, 50)
        {
            WorkSite = wpoint;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 348, 256, 9270);
            this.AddBackground(21, 20, 306, 214, 9200);
            this.AddLabel(28, 29, 52, String.Format("Work Point Progress")); //title of gump
            this.AddLabel(31, 59, 0, String.Format("Gold Collected: {0}% ({1} Total)", WorkSite.GoldPercentage, WorkSite.GoldRequired)); //gold collected %
            this.AddLabel(31, 80, 0, String.Format("Logs Collected: {0}% ({1} Total)", WorkSite.LogsPercentage, WorkSite.LogsRequired)); //logs collected %
            this.AddLabel(31, 99, 0, String.Format("Ingots Collected: {0}% ({1} Total)", WorkSite.IngotPercentage, WorkSite.IngotsRequired)); //ingots collected %
            this.AddLabel(31, 119, 0, String.Format("Cloth Collected: {0}% ({1} Total)", WorkSite.ClothPercentage, WorkSite.ClothRequired)); //cloth collected %
            this.AddLabel(31, 139, 0, String.Format("Hides Collected: {0}% ({1} Total)", WorkSite.HidePercentage, WorkSite.HidesRequired)); //hides collected %
            this.AddImage(299, 208, 5560);
        }

        //public override void OnResponse(Server.Network.NetState sender, RelayInfo info)	{}
    }*/
}