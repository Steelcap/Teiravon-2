using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Engines.Craft;
using Server.Items;
using Server.Spells;
using System.Collections;

namespace Server.Items
{

    public class GoblinSatchel : Item
    {

        public virtual object FindParent(Mobile from)
        {
            Mobile m = this.HeldBy;

            if (m != null && m.Holding == this)
                return m;

            object obj = this.RootParent;

            if (obj != null)
                return obj;

            if (Map == Map.Internal)
                return from;

            return this;
        }

        private bool m_Armed = false;
        private Timer m_Timer;

        [Constructable]
        public GoblinSatchel()
            : base(0xE75)
        {
            Hue = 0x372;
            Weight = 3.0;
            Name = "A Goblin Satchel Charge";
        }

        public GoblinSatchel(Serial serial)
            : base(serial)
		{
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (!m_Armed)
            {
               from.SendMessage("Where will you place the satchel charge?");
               from.BeginTarget(1, true, TargetFlags.None, new TargetCallback(OnTarget));
            }
        }

        private void OnTarget(Mobile from, object obj)
        {
            IPoint3D p = obj as IPoint3D;

            SpellHelper.GetSurfaceTop(ref p);

            from.RevealingAction();

            IEntity to;

            if (p is Mobile)
                to = (Mobile)p;
            else
                to = new Entity(Serial.Zero, new Point3D(p), Map);

            if (Deleted || Map == Map.Internal)
                return;

            if (obj is Mobile)
            {
                Mobile m = obj as Mobile;
                if (m.Backpack !=null)
                    m.Backpack.AddItem(this);
                else
                    MoveToWorld(m.Location);

                Arm(from);
            }
            else
            {
                MoveToWorld(to.Location);
                Arm(from);
            }
        }

        private void Arm(Mobile from)
        {
            m_Armed = true;
            m_Timer = Timer.DelayCall(TimeSpan.FromSeconds(0.75), TimeSpan.FromSeconds(1.0 + Utility.RandomDouble()), 11, new TimerStateCallback(Detonate_OnTick), new object[] { from, 10 });
        }

        private void Detonate_OnTick(object state)
        {
            if (Deleted)
                return;

            object[] states = (object[])state;
            Mobile from = (Mobile)states[0];
            int timer = (int)states[1];
            int tickColor = Utility.RandomList(0x25, 0x22, 0x2B, 0x30);
            object parent = FindParent(from);

            if (timer == 0)
            {
                Point3D loc;
                Map map;

                if (parent is Item)
                {
                    Item item = (Item)parent;

                    loc = item.GetWorldLocation();
                    map = item.Map;
                }
                else if (parent is Mobile)
                {
                    Mobile m = (Mobile)parent;

                    loc = m.Location;
                    map = m.Map;
                }
                else
                {
                    return;
                }

                Explode(from, true, loc, map);
            }
            else
            {
                if (parent is Item)
                    ((Item)parent).PublicOverheadMessage(MessageType.Emote, tickColor, false, "*Tick*");
                else if (parent is Mobile)
                    ((Mobile)parent).PublicOverheadMessage(MessageType.Emote, tickColor, false, "*Tick*");

                states[1] = timer - 1;
            }
        }

        public void Explode(Mobile from, bool direct, Point3D loc, Map map)
        {
            if (Deleted)
                return;

            Delete();

            if (map == null)
                return;

            Effects.PlaySound(loc, map, 0x207);
            Effects.SendLocationEffect(loc, map, 0x36BD, 20);
            AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.2), .05, map, loc, 0x36BD, 10, 0, 0);

            IPooledEnumerable eable = map.GetObjectsInRange(loc, 8);
            ArrayList toExplode = new ArrayList();

            int toDamage = 0;

            foreach (object o in eable)
            {
                if (o is Mobile)
                {
                    toExplode.Add(o);
                    ++toDamage;
                }
                else if (o is BreakableTile)
                {
                    toExplode.Add(o);
                }
            }

            eable.Free();

            for (int i = 0; i < toExplode.Count; ++i)
            {
                object o = toExplode[i];

                if (o is Mobile)
                {
                    Mobile m = (Mobile)o;

                    if (from == null || (SpellHelper.ValidIndirectTarget(from, m) && from.CanBeHarmful(m, false)))
                    {
                        if (from != null)
                            from.DoHarmful(m);
                        double distance = m.GetDistanceToSqrt(loc);
                        double falloff = (100 - (10 * distance)) *.01;
                        int damage = (int)(200 * falloff);
                        AOS.Scale(damage, (100 - (int)(10 * distance)));

                        AOS.Damage(m, from, damage, 0, 100, 0, 0, 0);
                    }
                }
                else if (o is BreakableTile)
                {
                    // *** Modified by Valik for Breakable Wall/Floor tiles
                    BreakableTile bt = (BreakableTile)o;
                    bt.Damage(PotionEffect.ExplosionGreater);
                }
            }
        }
        
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

}