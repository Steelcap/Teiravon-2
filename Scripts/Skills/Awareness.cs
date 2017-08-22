using System;
using Server.Multis;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Regions;
using Server.Factions;
using System.Collections;
using Server.Engines.PartySystem;

namespace Server.SkillHandlers
{
    public class Awareness
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.DetectHidden].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile src)
        {
            src.SendMessage("You look around intent to find anything hidden.");
            if (!src.Hidden)
                src.Emote("*Glances around.*");
            src.Freeze(TimeSpan.FromSeconds(0.5));
            if (CheckSurroundings(src)) { src.SendMessage("You've exposed something hidden."); }
            return TimeSpan.FromSeconds(5.0);
        }
        private class InternalTarget : Target
        {
            public InternalTarget()
                : base(12, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile searcher, object hidden)
            {
                IPoint2D point = (IPoint2D)hidden;
                Direction SerDirection = searcher.Direction;
                Direction SertoObj = searcher.GetDirectionTo(point.X, point.Y);
                searcher.SendMessage("Facing :" + IsFacing((int)SerDirection, (int)SertoObj).ToString());
                   
            }
        }

        public static bool CheckSurroundings(Mobile searcher, Mobile hidden)
        {
            double Value = 20 + searcher.Skills.DetectHidden.Value / 5.0;
            bool found = false;
            IPoint2D point = (IPoint2D)hidden;
            Direction SerDirection = searcher.Direction;
            Direction SertoObj = searcher.GetDirectionTo(point.X, point.Y);
            
            double Dist = searcher.GetDistanceToSqrt(point) + ((Mobile)hidden).Skills.Stealth.Value * .005;
            Dist = Dist / (int)1;
            if (searcher is TeiravonMobile && ((TeiravonMobile)searcher).IsDrow())
                Dist -= 1.0;

            if (IsFacing((int)SerDirection, (int)SertoObj))
                Value *= 2.0;

            if (searcher is TeiravonMobile && ((TeiravonMobile)searcher).IsDrow())
                Dist -= 1.0;

            if (Dist == 0)
                Dist = .01;

            Value = Value / Dist;

            if (searcher.CheckSkill(SkillName.DetectHidden, Value * .01))
                    found = true;

            //searcher.SendMessage("Awareness Value : " + Value + "Dist : " + Dist);
            return found;
        }

        public static bool CheckSurroundings(Mobile searcher)
        {
            bool found = false;
            bool active = true;

            double Value = 20 + searcher.Skills.DetectHidden.Value / 5.0;
            ArrayList targets = new ArrayList();
            IPooledEnumerable eable = searcher.GetObjectsInRange(8);
            Party p = Party.Get(searcher);
            foreach (object o in eable)
            {
                if (o == searcher)
                    continue;
                
                if (o is Mobile)
                {
                    if (p != null && p.Contains((Mobile)o))
                        continue;

                    if (!searcher.InLOS(o))
                        continue;

                    if (((Mobile)o).Hidden && ((Mobile)o).AccessLevel == AccessLevel.Player)
                        targets.Add(o);
                }
                if (o is BaseTrap)
                    targets.Add(o);
            }
            eable.Free();

            if (targets.Count < 1)
                return false;

            for (int i = 0; i < targets.Count; i++)
            {
                object hidden = targets[i] as object;
                IPoint2D point = (IPoint2D)hidden;
                Direction SerDirection = searcher.Direction;
                Direction SertoObj = searcher.GetDirectionTo(point.X, point.Y);
                double Dist = searcher.GetDistanceToSqrt(point);
                Dist = Dist / (int)1;
                if (hidden is Mobile)
                {
                    Dist += ((Mobile)hidden).Skills.Stealth.Value * .005;
                }
                if (searcher is TeiravonMobile && ((TeiravonMobile)searcher).IsDrow())
                    Dist -= 1.0;

                if (IsFacing((int)SerDirection, (int)SertoObj))
                    Value *= 2.0;

                if (active)
                    Value *= 2.0;

                if (Dist == 0)
                    Dist = 0.01;

                Value = (double)(Value / Dist);
                //searcher.SendMessage("Awareness Value : " + Value + "Dist : " + Dist);
                if (searcher.CheckSkill(SkillName.DetectHidden, Value*.01))
                {
                    found = true;
                    if (hidden is Mobile)
                    {
                        ((Mobile)hidden).RevealingAction();
                    }
                }
            }
            return found;
        }

        public static bool IsFacing(int facing, int dirTo)
        {
            if (facing == 0)
            {
                return (dirTo > 6 || dirTo < 2);
            }
            if (facing == 7)
            {
                return (dirTo > 5 || dirTo < 1);
            }
            return (dirTo >= facing - 1 && dirTo <= facing + 1);
        }
    }
}
