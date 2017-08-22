using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Teiravon;
using Server.Engines.XmlSpawner2;
using System.Collections;
using Server.Gumps;
using Server.Regions;

namespace Server.Regions
{
    public class FogRegion : Region
    {
        public static readonly int HousePriority = Region.HousePriority;

        private Mobile m_Caster;

        public FogRegion(Mobile Caster, int Radius)
            : base("","",Caster.Map)
        {
            m_Caster = Caster;

            this.GoLocation = new Point3D(Caster.X - Radius, Caster.Y - Radius, Caster.Z);
        }

        private static Rectangle3D[] GetArea(Point3D p, int Radius)
        {
            Rectangle3D[] area = new Rectangle3D[1];
            area[0] = new Rectangle3D(p.X, p.Y, p.Z, Radius * 2, Radius * 2, 16);
            return area;
        }



        private bool m_Recursion;

        // Use OnLocationChanged instead of OnEnter because it can be that we enter a house region even though we're not actually inside the house
        public override void OnLocationChanged(Mobile m, Point3D oldLocation)
        {

            if (m_Recursion)
                return;

            bool hidden = false;
            hidden = m.Hidden;
            m.Hidden = true;

            base.OnLocationChanged(m, oldLocation);
            m_Recursion = true;
            ArrayList doods = this.Mobiles;

            if (doods.Contains(m))
            {
                if (m is TeiravonMobile)
                    ((TeiravonMobile)m).Obscured = true;

                if (m is BaseCreature)
                    ((BaseCreature)m).Obscured = true;
            }
            else
            {
                if (m is TeiravonMobile)
                    ((TeiravonMobile)m).Obscured = false;

                if (m is BaseCreature)
                    ((BaseCreature)m).Obscured = false;
            }

            m.Hidden = hidden;

            m.ProcessDelta();
            m_Recursion = false;
        }

        public override void OnEnter(Mobile m)
        {
            bool hidden = false;
            hidden = m.Hidden;
            m.Hidden = true;
            if (m is TeiravonMobile)
                ((TeiravonMobile)m).Obscured = true;

            if (m is BaseCreature)
                ((BaseCreature)m).Obscured = true;

            m.Hidden = hidden;

            base.OnEnter(m);
        }
        public override void Unregister()
        {
            ArrayList doods = this.Mobiles;
            foreach (Mobile m in doods)
            {
                bool hidden = false;
                hidden = m.Hidden;
                m.Hidden = true;
                if (m is TeiravonMobile)
                    ((TeiravonMobile)m).Obscured = false;

                if (m is BaseCreature)
                    ((BaseCreature)m).Obscured = false;

                m.Hidden = hidden;
            }
            base.Unregister();
        }

        public override void Register()
        {
            ArrayList doods = this.Mobiles;
            foreach (Mobile m in doods)
            {
                bool hidden = false;
                hidden = m.Hidden;
                m.Hidden = true;
                if (m is TeiravonMobile)
                    ((TeiravonMobile)m).Obscured = false;

                if (m is BaseCreature)
                    ((BaseCreature)m).Obscured = false;

                m.Hidden = hidden;
            }
            base.Register();
        }
        public override void OnExit(Mobile m)
        {
            bool hidden = false;
            hidden = m.Hidden;
            m.Hidden = true;
            if (m is TeiravonMobile)
                ((TeiravonMobile)m).Obscured = false;

            if (m is BaseCreature)
                ((BaseCreature)m).Obscured = false;

            m.Hidden = hidden;

            base.OnExit(m);
        }
    }
}
namespace Server.Items
{
    public class BlindGump : Gump
        {
            public BlindGump()
                : base(0, 0)
            {
                Closable = false;
                Disposable = false;
                Dragable = false;
                Resizable = false;
                AddPage(0);
                AddImageTiled(0, 0, 1280, 1024, 2702);
            }
        }

    public class MolotovResidue : Item
    {
        private int m_Strength;
        private Mobile m_Placer;
        private int damagecount;
        private DateTime m_DecayTime;
        private Timer m_Timer;


        [CommandProperty(AccessLevel.GameMaster)]
        public int ResidueStrength
        {
            get { return m_Strength; }
            set { m_Strength = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile PlacedBy
        {
            get { return m_Placer; }
        }

        public int DamageCount { get { return damagecount; } set { damagecount = value; } }

        [Constructable]
        public MolotovResidue(int p_Strength, Mobile from)
            : base(0x186A)
        {
            Name = "Molotov Residue";
            Visible = false;
            Movable = false;
            m_Strength = p_Strength;
            m_Placer = from;
            damagecount = 0;

            m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(10.0);
            m_Timer = new InternalTimer(this, m_DecayTime);
            m_Timer.Start();
        }

        public override bool HandlesOnMovement { get { return true; } }


        public override void OnMovement(Mobile m, Point3D oldLocation)
        {

            if (m != null && m.Location != oldLocation && m.Alive)
            {

                if (m.InRange(this.GetWorldLocation(), 3))
                {

                    if (m is BaseCreature && m.AccessLevel == AccessLevel.Player && m_Placer.CanBeHarmful(m))
                    {
                        if (m_Placer != null)
                        {
                            AOS.Damage(m,PlacedBy,GetResidueDamage(),0,100,0,0,0);
                        }
                        else
                            AOS.Damage(m, GetResidueDamage(), 0, 100, 0, 0, 0);

                        damagecount++;
                    }
                }
            }

            if (damagecount >= 10)
            {
                this.Delete();
            }

        }


        public int GetResidueDamage()
        {
            //FORMULA: Determine holy water residue damage

            int damage = (int)(20 + Utility.RandomMinMax(1, 5) * ((int)m_Strength + 1));
            return damage;
        }



        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }


        private class InternalTimer : Timer
        {
            private MolotovResidue m_Item;
            private DateTime m_End;

            public InternalTimer(MolotovResidue item, DateTime end)
                : base(TimeSpan.FromSeconds(.5))
            {
                m_End = end;
                m_Item = item;

            }

            protected override void OnTick()
            {
                if (m_Item == null)
                    return;

                if (m_End < DateTime.Now)
                    m_Item.Delete();
                else
                {
                    //Effects.SendLocationEffect(m_Item, m_Item.Map, 0x36B0, 13 );
                    for (int i = 0; i < 15; i++)
                    {
                        IPoint2D lo = new Point2D(TAVUtilities.RandomNormalInt(m_Item.X, 5), TAVUtilities.RandomNormalInt(m_Item.Y, 5));
                        IEntity from = new Entity(Serial.Zero, new Point3D(lo,m_Item.Z+5), m_Item.Map);
                        //m_Item.PlacedBy.SendMessage("{0}, {1}, {2}",from.X,from.Y,from.Z);
                        Effects.SendLocationEffect(from, m_Item.Map, Utility.RandomList(0x398C, 0x3996), 0);
                        //Effects.SendMovingEffect(from, too, 0x19AB, 0, 0, true, false);
                    }

                    if (m_Item == null)
                        return;

                    if (m_Item.Map == null)
                        return;

                    IPooledEnumerable eable = m_Item.Map.GetMobilesInRange(m_Item.Location, 3);
                    
                    if (eable == null)
                        return;

                    ArrayList toBurn = new ArrayList();

                    foreach(Mobile m in eable)
                    {
                        if (m.Deleted || m == null)
                            continue;

                        if (m.AccessLevel == AccessLevel.Player && m_Item.PlacedBy.CanBeHarmful(m) && Spells.SpellHelper.ValidIndirectTarget(m_Item.PlacedBy,m))
                        {
                            toBurn.Add(m);
                        }
                    }

                    eable.Free();

                    for (int i = 0; i < toBurn.Count; i++)
                    {
                        Mobile m = toBurn[i] as Mobile;
                        
                        if (m == null || m.Deleted)
                            continue;

                        if (m_Item.PlacedBy != null)
                        {
                            AOS.Damage(m, m_Item.PlacedBy, m_Item.GetResidueDamage(), 0, 100, 0, 0, 0);
                        }
                        else
                            AOS.Damage(m, m_Item.GetResidueDamage(), 0, 100, 0, 0, 0);
                    }

                    m_Item.DamageCount++;
                    if (m_Item.DamageCount > 10)
                    {
                        m_Item.Delete();
                        this.Stop();
                        return;
                    }
                    Timer m_Timer = new InternalTimer(m_Item, m_End);
                    m_Timer.Start();
                }
            }
        }


        public MolotovResidue(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_Strength);

            writer.Write((int)damagecount);

            writer.WriteDeltaTime(m_DecayTime);

            writer.Write(m_Placer);

        }


        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Strength = reader.ReadInt();

            damagecount = reader.ReadInt();

            m_DecayTime = reader.ReadDeltaTime();

            m_Timer = new InternalTimer(this, m_DecayTime);

            m_Timer.Start();

            m_Placer = reader.ReadMobile();
        }



    }

    public class Fog
    {
        public static void FillFog(object state)
        {
            object[] states = ((object[])state);
            Mobile caster = states[0] as Mobile;
            IPoint3D p = ((IPoint3D)states[1]);
            int radius = ((int)states[2]);
            Map CastMap = ((Map)states[3]);

            int num = (int)(15 + Math.Pow(5, radius));

            for (int i = 0; i < num; ++i)
            {
                int randX, randY;

                randX = Utility.Random(2 * radius + 1);
                randY = Utility.Random(2 * radius + 1);

                if ((Math.Abs(randX - radius) + Math.Abs(randY - radius)) > radius + 1)
                {
                    i--;
                    continue;
                }
                int x = p.X + randX - radius;
                int y = p.Y + randY - radius;
                int z = CastMap.GetAverageZ(x, y);

                Point3D loc = new Point3D(x, y, z);

                if (CastMap.CanFit(loc, 0, true, false))
                {
                    double delay = .5 + (2 * Utility.RandomDouble());
                    Timer.DelayCall(TimeSpan.FromSeconds(delay), new TimerStateCallback(FogFallEffect_Callback), new object[] { new Point3D(loc.X, loc.Y, loc.Z + 5), CastMap });
                }
            }
        }

        public static void FogFallEffect_Callback(object state)
        {
            object[] states = ((object[])state);
            Point3D p = ((Point3D)states[0]);
            Map map = ((Map)states[1]);
            //p.Z += Utility.RandomMinMax(0, 2);
            Entity to = new Entity(Serial.Zero, p, map);
            //Effects.SendLocationParticles(to, 0x3789, 1, 42, 1891, 2, 0, 0);
            //Effects.SendLocationParticles(to, Utility.RandomList(0x0CDB, 0x0CDB, 0x0CE1, 0x0CE4), 1, 42, 1891, 2, 0, 0);
            Effects.SendLocationParticles(to, Utility.RandomList(0x372A, 0x372F, 0x3733), 1, 42, 1891, 2, 0, 0);

        }

        public static void RemoveZone(object state)
        {
            object[] states = state as Object[];
            FogRegion fog = states[0] as FogRegion;
            if (fog != null)
                Region.RemoveRegion(fog);
        }
    }
}