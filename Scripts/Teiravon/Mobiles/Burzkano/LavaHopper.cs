using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a lava hopper corpse")]
    public class LavaHopper : BaseCreature
    {
        [Constructable]
        public LavaHopper()
            : base(AIType.AI_Melee, FightMode.Agressor, 10, 1, 0.1, 0.2)
        {
            Name = "a lavahopper";
            Body = 302;
            BaseSoundID = 959;
            Hue = 2988;
            Level = 3;

            SetStr(41, 65);
            SetDex(291, 315);
            SetInt(26, 50);

            SetHits(221, 345);

            SetDamage(13, 15);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Fire, 50);

            SetResistance(ResistanceType.Physical, 5, 10);
            SetResistance(ResistanceType.Cold, 10, 20);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 30.1, 45.0);
            SetSkill(SkillName.Tactics, 45.1, 70.0);
            SetSkill(SkillName.Wrestling, 40.1, 60.0);

            Fame = 300;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = -12.9;

            VirtualArmor = 12;

            //PackGold( 10, 50 );
        }

        //public override int TreasureMapLevel{ get{ return 1; } }

        public LavaHopper(Serial serial)
            : base(serial)
        {

        }
        public override void OnCombatantChange()
        {
            if (Combatant != null)
            {
                this.MoveToWorld(Combatant.Location, Combatant.Map);
                this.PlaySound(0x239);
                 string name = Combatant.Name;
                if (name != null)
                    this.Emote("*Leaps at {0}*",name);
                else
                    this.Emote("*Leaps*");
            }
        }
        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (Utility.RandomBool() || willKill)
            {
                LavaPuddle puddle = new LavaPuddle();
                puddle.MoveToWorld(Location,this.Map);
            }

            base.OnDamage(amount, from, willKill);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class LavaPuddle : Item
    {
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public LavaPuddle()
            : base(0x122A)
        {
            ItemID = Utility.RandomMinMax(0x122A, 0x122E);
            Hue = 2598;
            Name = "puddle of lava";
            Visible = true;
            Movable = false;

            m_DecayTime = DateTime.Now + TimeSpan.FromMinutes(1.0);
            m_Timer = new InternalTimer(this, m_DecayTime);
            m_Timer.Start();
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override bool OnMoveOver(Mobile m)
        {
            if (m.CanBeDamaged())
            {
                AOS.Damage(m, Utility.RandomMinMax(20,50), 0, 100, 0, 0, 0);
                Effects.SendLocationEffect(this.Location, this.Map, 0x1A75, 7);
                m.PlaySound(0x227);
            }

            return base.OnMoveOver(m);
        }
        
        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }


        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                if (m_Item != null)
                {
                    m_Item.PublicOverheadMessage(Network.MessageType.Regular, 5, false, "The blessing placed on the ground seems to fade.");
                    m_Item.Delete();
                }
            }
        }


        public LavaPuddle(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.WriteDeltaTime(m_DecayTime);

        }


        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_DecayTime = reader.ReadDeltaTime();

            m_Timer = new InternalTimer(this, m_DecayTime);

            m_Timer.Start();


        }



    }
}