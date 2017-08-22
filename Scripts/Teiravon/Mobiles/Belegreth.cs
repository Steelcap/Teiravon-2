using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    public class Belegreth : BaseCreature
    {
        private DateTime m_ColossalSmash = DateTime.Now + TimeSpan.FromSeconds(15);
        
        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.MortalStrike;
        }

        [Constructable]
        public Belegreth()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.05, 0.1)
        {
            Name = "Belegreth";
            Title = "the Juggernaut";
            Body = 189;
            BaseSoundID = 1160;
            Hue = 2228;

            SetStr(600, 750);
            SetDex(100);
            SetInt(151, 200);

            SetHits(50000);
            SetStam(100);
            SetMana(2000);

            SetDamage(18, 19);

            SetDamageType(ResistanceType.Physical, 30);
            SetDamageType(ResistanceType.Cold, 35);
            SetDamageType(ResistanceType.Poison, 35);

            SetResistance(ResistanceType.Physical, 70, 95);
            SetResistance(ResistanceType.Fire, 50, 65);
            SetResistance(ResistanceType.Cold, 60, 85);
            SetResistance(ResistanceType.Poison, 60, 75);
            SetResistance(ResistanceType.Energy, 60, 75);

            SetSkill(SkillName.Anatomy, 120.0);
            SetSkill(SkillName.MagicResist, 450.0);
            SetSkill(SkillName.Tactics, 130.0);
            SetSkill(SkillName.Wrestling, 120.1, 130.0);

            Fame = 10000;
            Karma = -8000;

            VirtualArmor = 40;
            ControlSlots = 4;
            //PackGold( 150, 200 );
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.SuperBoss);
        }
        public Belegreth(Serial serial)
            : base(serial)
        {
        }

        public override void OnThink()
        {
            if (Alive && !Blessed && Map != Map.Internal && Map != null && Combatant != null)
            {
                if (m_ColossalSmash <= DateTime.Now && Combatant != null && Combatant.InRange(Location,1))
                {
                    this.NonlocalOverheadMessage(Server.Network.MessageType.Emote, 2217,false, "*Rears back for a colossal strike*");
                    Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerStateCallback(DoSmash),new object[]{ Combatant.Location});
                    CantWalk = true;
                    this.NextCombatTime = DateTime.Now + TimeSpan.FromSeconds(1.5);
                    m_ColossalSmash = DateTime.Now + TimeSpan.FromSeconds(Utility.Random(15,5));
                }

            }
        }

        private void DoSmash(object state)
        {
            object[] states = ((object[])state);
            Point3D loc = ((Point3D)states[0]);

            AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1), 0.1, Map, loc, 14000, 13, 1102, 2, 1, 3, false, true, false);
            CantWalk = false;
                try
                {
                    foreach (Mobile m in this.Map.GetMobilesInRange(loc,2))
                    {
                        if (m != null && m.Alive && m != this)
                        {
                            if (m is Gust)
                                continue;
                            m.NonlocalOverheadMessage(Server.Network.MessageType.Emote, 2217, true, "*Is sent flying by the massive strike*");
                            AOS.Damage(m, 75, 100, 0, 0, 0, 0);
                            Gust g = new Gust(this, m, 2.0);
                            g.MoveToWorld(m.Location, m.Map);
                        }
                    }
                }
                catch { }
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
}
