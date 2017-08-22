using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Spells;
using Server.Scripts.Commands;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;

namespace Server.Teiravon
{
    public class CandidateGump : Gump
        {
            private Mobile m_Candidate;
            private ElvenVoteController m_Control;

            public CandidateGump(Mobile owner, ElvenVoteController controller)
                : base (150,50)
            {
                m_Candidate = owner;
                m_Control = controller;
                AddPage(0);

                AddBackground(0, 0, 400, 350, 2600);

                AddHtml(0, 20, 400, 35, "Elven Council Candidacy", false, false);

                AddHtml(50,55,300,140,"Some text here, blah blah, you wanna be a candidate?", true, false);

                AddButton(200, 227, 4005, 4007, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(235, 230, 110, 35, 1011012, false, false); // CANCEL

                AddButton(65, 227, 4005, 4007, 1, GumpButtonType.Reply, 0);
                AddHtmlLocalized(100, 230, 110, 35, 1011011, false, false); // CONTINUE
            }

            public override void OnResponse(NetState state, RelayInfo info)
            {
                Mobile from = state.Mobile;
                from.CloseGump(typeof(CandidateGump));

                if (info.ButtonID == 1 || info.ButtonID == 2)
                {
                    from.PlaySound(0x419);
                    from.FixedEffect(0x376A, 10, 16);

                    m_Control.AssignCandidate(from);
                }
            }
        }


    public class ElvenVoteController : Item
    {
        private List<ElvenVotePedestal> m_Pedestals = new List<ElvenVotePedestal>();
        private ArrayList m_Voters;
        private ControllerStatus m_Status;
        private int m_Total;
        private ArrayList m_Candidates;
        

        [CommandProperty(AccessLevel.GameMaster)]
        public ControllerStatus Status 
        {   get { return m_Status; } 
            set { 
                m_Status = value;
                OnStatusChange(m_Status);
            }
        }

        public ArrayList Voters{get { return m_Voters; } set{ m_Voters = value;} }

        [Constructable]
        public ElvenVoteController()
            : base(0x2D12)
		{
			Name = "An Ornate Wooden Statue";
			Movable = false;
            Status = ControllerStatus.Disabled;
		}
        public ElvenVoteController(Serial serial)
            : base(serial)
		{
		}

        
        public void AssignCandidate(Mobile from)
        {
            for (int i = 0; i < m_Pedestals.Count; ++i)
            {
                if (m_Pedestals[i].Assign(from))
                    break;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            TeiravonMobile tav;
            if (from is TeiravonMobile)
            {
                tav = from as TeiravonMobile;
                if (Status == ControllerStatus.Candidate && !m_Candidates.Contains(from))
                {
                    if (tav.IsHalfElf() || tav.IsElf())
                    {
                        CandidateGump g = new CandidateGump(from, this);
                        m_Candidates.Add(from);
                        tav.SendGump(g);
                    }
                }
            }

            base.OnDoubleClick(from);
        }

        public bool AddPedestal(ElvenVotePedestal pedestal)
        {
            if (m_Pedestals.Contains(pedestal))
                return false;
            else
                m_Pedestals.Add(pedestal);
            return true;
        }
        private void OnStatusChange(ControllerStatus status)
        {
            switch (status)
            {
                case ControllerStatus.Candidate:
                    m_Candidates = new ArrayList();
                    m_Voters = new ArrayList();
                    foreach (ElvenVotePedestal e in m_Pedestals)
                    {
                        e.WipeCandidates();
                    }
                    break;
                case ControllerStatus.Election:
                    foreach (ElvenVotePedestal p in m_Pedestals)
                    {
                        p.MakeOrb();
                    }
                    break;
                case ControllerStatus.Disabled:
                    TallyVotes();
                    break;
                default:
                    break;
            }
        }

        private void TallyVotes()
        {
            m_Total = 0;
            foreach (ElvenVotePedestal p in m_Pedestals)
            {
                m_Total += p.Votes;
            }
            m_Pedestals.Sort();
            int seats = 3;
            if (m_Total > 13)
                seats = 4;
            if (m_Total > 21)
                seats = 5;
            if (m_Total > 34)
                seats = 6;
            if (m_Total > 55)
                seats = 7;

            for (int i = 0; i < seats; ++i)
            {
                m_Pedestals[i].DoWin();
            }

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
            writer.Write((int)m_Pedestals.Count);
            for (int i = 0; i < m_Pedestals.Count; ++i)
            {
                writer.Write(m_Pedestals[i]);
            }
            writer.WriteMobileList(m_Voters);
            writer.Write((int)m_Status);
            writer.Write((int)m_Total);
            writer.WriteMobileList(m_Candidates);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            if (version > 0)
            {
                int x = reader.ReadInt();
                List<ElvenVotePedestal> m_Pedestals = new List<ElvenVotePedestal>();
                for (int i = 0; i < x; ++i)
                {
                    m_Pedestals.Add((ElvenVotePedestal)reader.ReadItem());
                }
                m_Voters = reader.ReadMobileList();
                m_Status = (ControllerStatus)reader.ReadInt();
                m_Total = reader.ReadInt();
                m_Candidates = reader.ReadMobileList();
            }
        }
    }
    public class ElvenVotePedestal : Item
    {
        private Mobile m_Candidate;
        private ElvenVoteGem m_Orb;
        private bool m_filled;
        private ElvenVoteController m_Controller;
        private int m_Votes;

                [Constructable]
        public ElvenVotePedestal()
            : base(0x1223)
		{
			Name = "An Ornate Wooden Statue";
			Movable = false;
		}
                public ElvenVotePedestal(Serial serial)
            : base(serial)
		{
		}
        public int Votes { get { return m_Votes; } set { m_Votes = value; } }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.GameMaster)
            {
                from.Target = new InternalTarget(this);
                from.SendMessage("Please select Vote Controller Statue.");
            }
        }

        private class InternalTarget : Target
        {
            private ElvenVotePedestal m_Pedestal;

            public InternalTarget(ElvenVotePedestal pedestal)
                : base(3, true, TargetFlags.None)
            {
                m_Pedestal = pedestal;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Pedestal.Deleted)
                    return;
                if (targeted is ElvenVoteController)
                    if (((ElvenVoteController)targeted).AddPedestal(m_Pedestal))
                        from.SendMessage("Pedestal Added");
                    else
                        from.SendMessage("Cannot add pedestal");
            }
        }
        public bool DoVote(Mobile from)
        {
            TeiravonMobile tav;
            if (from is TeiravonMobile)
            {
                tav = from as TeiravonMobile;

                if (!tav.IsElf() && !tav.IsHalfElf())
                    return false;
                else
                    if (m_Controller.Voters.Contains(tav))
                        return false;
                    else
                    {
                        m_Votes += 1;
                        m_Controller.Voters.Add(tav);
                        CommandLogging.WriteLine(tav, "Voted for {0}", m_Candidate.Name);
                    }                  
            }

            return true;
        }

        public void DoWin()
        {
            Hue = 2225;
            if (m_Orb != null)
                m_Orb.ItemID = 0x19AB;
        }

        public void WipeCandidates()
        {
            m_Candidate = null;
            if( m_Orb != null)
                m_Orb.Delete();
            m_Votes = 0;
        }
        public bool Assign(Mobile from)
        {
            if (!m_filled)
                return false;
            else
            {
                m_Candidate = from;
                m_filled = true;
                m_Votes = 0;
                this.Hue = 2445;
                return true;
            }
        }

        public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

            if (m_Candidate != null)
                list.Add(m_Candidate.Name);
        }

        public void MakeOrb()
        {
            if( m_Candidate != null)
            {
            m_Orb = new ElvenVoteGem();
            m_Orb.MoveToWorld(this.Location, this.Map);
            m_Orb.Z += 5;
            m_Orb.Name = m_Candidate.Name;
            m_Orb.Hue = Utility.RandomBirdHue();
            m_Orb.Controller = this;
            m_Orb.Candidate = m_Candidate;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
            writer.Write((Mobile)m_Candidate);
            writer.Write((Item)m_Orb);
            writer.Write((bool)m_filled);
            writer.Write((Item)m_Controller);
            writer.Write((int)m_Votes);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            if (version > 0)
            {
                m_Candidate = reader.ReadMobile();
                m_Orb = (ElvenVoteGem)reader.ReadItem();
                m_filled = reader.ReadBool();
                m_Controller = (ElvenVoteController)reader.ReadItem();
                m_Votes = reader.ReadInt();
            }
        }
    }


    public class ElvenVoteGem : Item
    {
        private ElvenVotePedestal m_Controller;
        private Mobile m_Candidate;

                [Constructable]
        public ElvenVoteGem()
            : base(0x3196)
		{
			Name = "an ornate gemstone";
            Movable = true;
		}

                public ElvenVoteGem(Serial serial)
            : base(serial)
		{
		}
        public Mobile Candidate
        {
            get { return m_Candidate; }
            set { m_Candidate = value; }
        }
        public ElvenVotePedestal Controller
        {
            get { return m_Controller; }
            set{ m_Controller = value;}
        }

        public override bool OnDragLift(Mobile from)
        {
            if (Controller.DoVote(from))
                from.SendMessage("The orb glows warmly in your hand, your will is known.");
            return false;
        }
        public override bool OnDecay()
        {
            return false;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_Controller);
            writer.Write(m_Candidate);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Controller = (ElvenVotePedestal)reader.ReadItem();
            m_Candidate = reader.ReadMobile();
        }
    }

    public enum ControllerStatus
    {
        Disabled,
        Candidate,
        Election
    }
}