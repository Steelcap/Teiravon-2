using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps;
using Server.Teiravon;
using Server.Factions;
using Server.ContextMenus;

namespace Server.Items
{
    public class WorkSite : Container
    {
        private static Hashtable m_Workers = new Hashtable();

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Completed
        {
            get { return (m_ProgressPoints >= m_TotalPointsRequired); }
        }

        private int m_Sound;
        [CommandProperty(AccessLevel.GameMaster)]
        public int SoundID
        {
            get { return m_Sound; }
            set { m_Sound = value; }
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public int TotalWorkers
        {
            get { return m_Workers.Count; }
        }
        private int m_ProgressPoints;
        [CommandProperty(AccessLevel.GameMaster)]
        public int ProgressPoints
        {
            get { return m_ProgressPoints; }
            set{ m_ProgressPoints = value;}
        }

        private SkillName m_RequiredSkill;
        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName RequiredSkill
        {
            get { return m_RequiredSkill; }
            set { m_RequiredSkill = value; }
        }

        private Double m_SkillLevel;
        [CommandProperty(AccessLevel.GameMaster)]
        public Double SkillLevel
        {
            get { return m_SkillLevel; }
            set { m_SkillLevel = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int PercentComplete
        {
            get
            {
                double quotient = TAVUtilities.DoubleDivide(ProgressPoints, TotalPointsRequired);
                return TAVUtilities.DoubleToPercentage(quotient);
            }
        }

        private int m_TotalPointsRequired;
        [CommandProperty(AccessLevel.GameMaster)]
        public int TotalPointsRequired
        {
            get { return m_TotalPointsRequired; }
            set { m_TotalPointsRequired = value; }
        }

        private int m_Consumed;
        [CommandProperty(AccessLevel.GameMaster)]
        public int ResourcesRequired
        {
            get { return m_Consumed; }
            set { m_Consumed = value; }
        }

        private Type m_Resource;
        [CommandProperty(AccessLevel.GameMaster)]
        public Type ResourceType
        {
            get { return m_Resource; }
            set { m_Resource = value; }
        }
        /*
        [CommandProperty(AccessLevel.GameMaster)]
        public int ResourcePercentage
        {
            get
            {
                Item[] items = FindItemsByType(ResourceType);
                int curramt = 0;
                foreach (Item i in items)
                {
                    curramt += i.Amount;
                }
                double quotient = TAVUtilities.DoubleDivide(curramt, (ResourcesRequired*(m_TotalPointsRequired - m_ProgressPoints)));
                return TAVUtilities.DoubleToPercentage(quotient);
            }
        }
        */
        [Constructable]
        public WorkSite()
            : base(0xE77) //wooden barrel in appearance
        {
            Movable = false;
            Name = "Work Site Chest";
            ResourcesRequired = 1;
            ResourceType = typeof(Gold);
            RequiredSkill = SkillName.Alchemy;
            SkillLevel = 0.0;
            SoundID = 0;
            TotalPointsRequired = 10;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (Completed)
                list.Add("Completed Work Site");
  
        }

        public int GetResourceAmount(Type t)
        {
            Item[] items = FindItemsByType(t);
            int amount = 0;
            foreach (Item i in items)
            {
                amount += i.Amount;
            }
            return amount;
        }

        public void DelayWork(Mobile m)
        {
            m.Animate(9, 5, 1, true, false, 0);
            Timer.DelayCall(TimeSpan.FromSeconds(2),new TimerStateCallback(DoWork), m);
        }
        public void DoWork(object state)
        {
            Mobile m = state as Mobile;
            TeiravonMobile tav = m as TeiravonMobile;

            m.EndAction(typeof(WorkSite));
            if (tav.Skills[m_RequiredSkill].Value < m_SkillLevel)
                tav.SendMessage("You aren't skilled enough to work on this project.");

            else if (GetResourceAmount(ResourceType) < m_Consumed)
                tav.SendMessage("There isn't enough materials to continue work.");

            else if (m_ProgressPoints >= m_TotalPointsRequired)
                tav.SendMessage("This project has already been completed!");

            else
            {
                if (m.CheckSkill(m_RequiredSkill, m_SkillLevel - 20, m_SkillLevel + 20))
                {
                    ConsumeTotal(m_Resource, m_Consumed);
                    ProgressPoints++;
                    tav.SendMessage("You work diligently for a brief time.");
                    
                    if (SoundID != 0)
                        tav.PlaySound(SoundID);

                    if (m_Workers.ContainsKey(m))
                    {
                        m_Workers[m] = (int)m_Workers[m] + 1;
                    }
                    else
                        m_Workers.Add(m, 1);

                    if (ProgressPoints == TotalPointsRequired)
                        DoComplete();
                }
                else
                {
                    tav.SendMessage("You work for some time but are unable to contribute meaningfully to the project.");
                    ConsumeTotal(m_Resource, m_Consumed / 2);
                }

            }
            tav.SendGump(new WorkProgressGump(this));
        }

        public void PayDay(int work, Mobile m)
        {
            int totalpay = GetResourceAmount(typeof(Gold));
            int Gamount = 0;
            int Samount = 0;
            int expTotal = 0;
            double dif = (double)(work / TotalPointsRequired);
            double payout = dif * totalpay;

            if (totalpay >= 1)
            {
                Gamount = totalpay / 1;
                Gold g = new Gold(Gamount);
                m.AddToBackpack(g);
                totalpay -= Gamount;
                totalpay *= 10;
                ConsumeTotal(typeof(Gold), Gamount);

                if (totalpay >= 1)
                {
                    Samount = totalpay / 1;
                    Silver s = new Silver(Samount);
                    m.AddToBackpack(g);
                    ConsumeTotal(typeof(Gold), 1);
                }
                
                m.SendMessage("You recieve {0} gold and {1} silver for your work.", Gamount, Samount);
            }

            expTotal = (int)(Gamount * (SkillLevel / 10) * (ResourcesRequired / 4));
            if (expTotal > 1)
            {
                TeiravonMobile tav = m as TeiravonMobile;
                if (Misc.Titles.AwardExp(tav, expTotal))
                    tav.SendMessage("You have gained {0} experience.", expTotal);
            }
           
        }
        
        public void DoComplete()
        {
            this.PublicOverheadMessage(Server.Network.MessageType.Yell, 168, true, "*The Project is Complete*");

            foreach (Mobile m in m_Workers.Keys)
            {
                int work = (int)m_Workers[m];
                PayDay(work, m);
            }

            if (ItemID == 0xE77)
                ItemID = 0x0FAE;
            
        }

        public override void OnDoubleClick(Mobile from)
        {
            //if(from.AccessLevel >= AccessLevel.Player) //only reveal contents to GMs
            //base.OnDoubleClick(from);
            if (from.BeginAction(typeof(WorkSite)))
            {
                if (ProgressPoints < TotalPointsRequired)
                {
                    if (from.HasGump(typeof(WorkProgressGump)))
                        from.CloseGump(typeof(WorkProgressGump));
                    from.SendGump(new WorkProgressGump(this));
                }
                else
                    base.OnDoubleClick(from);
            }
            else
            {
                Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerStateCallback(from.EndAction), typeof(WorkSite));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            int version = 1;
            writer.Write(version);

            writer.Write(m_Consumed);
            writer.Write(m_Sound);
            writer.Write(m_Resource.ToString());
            writer.Write(m_ProgressPoints);
            writer.Write(m_TotalPointsRequired);
            writer.Write(SkillInfo.Table[(int)m_RequiredSkill].SkillID);
            writer.Write(m_SkillLevel);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();


                    m_Consumed = reader.ReadInt();
                    m_Sound = reader.ReadInt();
                    m_Resource = ScriptCompiler.FindTypeByFullName(reader.ReadString());
                    m_ProgressPoints = reader.ReadInt();
                    m_TotalPointsRequired = reader.ReadInt();
                    m_RequiredSkill = (SkillName)reader.ReadInt();
                    if (version > 0)
                        m_SkillLevel = reader.ReadDouble();

        }

        public WorkSite(Serial s)
            : base(s)
        {

        }
    }

}
