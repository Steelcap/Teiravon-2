using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public abstract class TeiravonPotion : BasePotion
    {
        public abstract PotionEffect PEffect { get; }
        public abstract TimeSpan Duration { get; }
        public abstract bool Racial { get; }
        public bool TargetDrink = false;

        public TeiravonPotion(PotionEffect effect, int amount)
            : base(0xF08, effect, amount)
        {
        }

        public TeiravonPotion(PotionEffect effect)
            : base(0xF08, effect, 1)
        {
        }

        public TeiravonPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                BasePotion.PlayDrinkEffect(from);
                this.Consume();

                m_Player.SendMessage("You take a sip of the potion...");

                TimerHelper m_TimerHelper = new TimerHelper((int)m_Player.Serial);

                m_TimerHelper.Potion = PEffect;
                m_TimerHelper.Duration = Scale(from, Duration);
                m_TimerHelper.Start();

                m_Player.SetActivePotions(PEffect, true);
            }
            else
            {
                m_Player.SendMessage("You can't drink this now.");
            }
        }
    }

    public class InvulnerabilityPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.Invulnerability; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(15); } }
        public override bool Racial { get { return false; } }


        [Constructable]
        public InvulnerabilityPotion(int amount)
            : base(PotionEffect.Invulnerability)
        {
            Name = "Invulnerability Potion";
            Hue = Utility.RandomMetalHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public InvulnerabilityPotion()
            : base(PotionEffect.Invulnerability)
        {
            Name = "Invulnerability Potion";
            Hue = Utility.RandomMetalHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new InvulnerabilityPotion(amount), amount);
        }

        public InvulnerabilityPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.SendMessage("Your skin begins to tingle...");
                m_Player.Blessed = true;
            }

            base.Drink(from);
        }
    }

    public class MagicResistPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.MagicResist; } }
        public override TimeSpan Duration { get { return TimeSpan.FromMinutes(5); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public MagicResistPotion(int amount)
            : base(PotionEffect.MagicResist)
        {
            Name = "Magic Resist Potion";
            Hue = Utility.RandomRedHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public MagicResistPotion()
            : base(PotionEffect.MagicResist)
        {
            Name = "Magic Resist Potion";
            Hue = Utility.RandomRedHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new MagicResistPotion(amount), amount);
        }

        public MagicResistPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.SendMessage("Your skin begins to tingle...");
                m_Player.AddSkillMod(new TimedSkillMod(Server.SkillName.MagicResist, true, 30.0, TimeSpan.FromMinutes(3)));
            }

            base.Drink(from);
        }
    }

    public class TotalManaRefreshPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.TotalManaRefresh; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(30); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public TotalManaRefreshPotion(int amount)
            : base(PotionEffect.TotalManaRefresh)
        {
            Name = "Total Mana Refresh Potion";
            Hue = 0xA01;
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public TotalManaRefreshPotion()
            : base(PotionEffect.TotalManaRefresh)
        {
            Name = "Total Mana Refresh Potion";
            Hue = 0xA01;
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new TotalManaRefreshPotion(amount), amount);
        }

        public TotalManaRefreshPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.SendMessage("Your concentration has been completely restored.");

                m_Player.Mana = m_Player.ManaMax;
            }

            base.Drink(from);
        }
    }

    public class ManaRefreshPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.ManaRefresh; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(30); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public ManaRefreshPotion(int amount)
            : base(PotionEffect.ManaRefresh)
        {
            Name = "Mana Refresh Potion";
            Hue = 0xA00;
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public ManaRefreshPotion()
            : base(PotionEffect.ManaRefresh)
        {
            Name = "Mana Refresh Potion";
            Hue = 0xA00;
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new ManaRefreshPotion(amount), amount);
        }

        public ManaRefreshPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.SendMessage("You find it easier to concentrate...");

                m_Player.Mana += 25;

                if (m_Player.Mana > m_Player.ManaMax)
                    m_Player.Mana = m_Player.ManaMax;
            }

            base.Drink(from);
        }
    }

    public class InvisibilityPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.Invisibility; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(10); } }
        public override bool Racial { get { return false; } }
        private Timer m_Timer;

        [Constructable]
        public InvisibilityPotion(int amount)
            : base(PotionEffect.Invisibility)
        {
            Name = "Invisibility Potion";
            Hue = Utility.RandomPinkHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public InvisibilityPotion()
            : base(PotionEffect.Invisibility)
        {
            Name = "Invisibility Potion";
            Hue = Utility.RandomPinkHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new InvisibilityPotion(amount), amount);
        }

        public InvisibilityPotion(Serial serial)
            : base(serial)
        {
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
        
        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.Emote("*begins to fade from view...*");
                m_Timer = Timer.DelayCall(TimeSpan.FromSeconds(0.75), new TimerStateCallback(DoHide), new object[] { from});
            }

            base.Drink(from);
        }
        public void DoHide( object state)
        {
            object[] states = (object[])state;
            Mobile from = (Mobile)states[0];
            from.Hidden = true;
        }
    }

    public class GenderSwapPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.GenderSwap; } }
        public override TimeSpan Duration { get { return TimeSpan.FromMinutes(3); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public GenderSwapPotion(int amount)
            : base(PotionEffect.GenderSwap)
        {
            Name = "Gender Swap Potion";
            Hue = Utility.RandomNeutralHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public GenderSwapPotion()
            : base(PotionEffect.GenderSwap)
        {
            Name = "Gender Swap Potion";
            Hue = Utility.RandomNeutralHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GenderSwapPotion(amount), amount);
        }

        public GenderSwapPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect) && (m_Player.Body == 0x190 || m_Player.Body == 0x191))
            {
                m_Player.SendMessage("You feel a little strange...");

                if (m_Player.Body == 0x190)
                    m_Player.BodyMod = 0x191;
                else
                    m_Player.BodyMod = 0x190;
            }
            else
            {
                m_Player.SendMessage("It has no effect.");

                m_Player.RevealingAction();

                m_Player.PlaySound(0x2D6);
                m_Player.AddToBackpack(new Bottle());

                this.Consume();

                return;
            }

            base.Drink(from);
        }
    }

    public class SustenancePotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.Sustenance; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(30); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public SustenancePotion(int amount)
            : base(PotionEffect.Sustenance)
        {
            Name = "Sustenance Potion";
            Hue = Utility.RandomNeutralHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public SustenancePotion()
            : base(PotionEffect.Sustenance)
        {
            Name = "Sustenance Potion";
            Hue = Utility.RandomNeutralHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new SustenancePotion(amount), amount);
        }

        public SustenancePotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.Hunger += 3;
                m_Player.SendMessage("You feel a little less hungry.");
            }

            base.Drink(from);
        }
    }

    public class GreaterSustenancePotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.GreaterSustenance; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(30); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public GreaterSustenancePotion(int amount)
            : base(PotionEffect.GreaterSustenance)
        {
            Name = "Greater Sustenance Potion";
            Hue = Utility.RandomNeutralHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public GreaterSustenancePotion()
            : base(PotionEffect.GreaterSustenance)
        {
            Name = "Greater Sustenance Potion";
            Hue = Utility.RandomNeutralHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GreaterSustenancePotion(amount), amount);
        }

        public GreaterSustenancePotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.Hunger = 20;
                m_Player.SendMessage("You feel completely full.");
            }

            base.Drink(from);
        }
    }

    public class GreaterFloatPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.GreaterFloat; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(20); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public GreaterFloatPotion(int amount)
            : base(PotionEffect.GreaterFloat)
        {
            Name = "Greater Float Potion";
            Hue = Utility.RandomBlueHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public GreaterFloatPotion()
            : base(1)
        {
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new GreaterFloatPotion(amount), amount);
        }

        public GreaterFloatPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect) && m_Player.CanDrink(PotionEffect.LesserFloat) && m_Player.CanDrink(PotionEffect.Float) && m_Player.InLOS(new Point3D(m_Player.X, m_Player.Y, m_Player.Z + 30)))
            {
                m_Player.Z += 15;
                m_Player.CantWalk = true;
                m_Player.Emote("*begin to hover off the ground!*");
            }

            base.Drink(from);
        }
    }

    public class FloatPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.Float; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(10); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public FloatPotion(int amount)
            : base(PotionEffect.Float)
        {
            Name = "Float Potion";
            Hue = Utility.RandomBlueHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public FloatPotion()
            : base(PotionEffect.Float)
        {
            Name = "Float Potion";
            Hue = Utility.RandomBlueHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new FloatPotion(amount), amount);
        }

        public FloatPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect) && m_Player.CanDrink(PotionEffect.LesserFloat) && m_Player.CanDrink(PotionEffect.Float) && m_Player.InLOS(new Point3D(m_Player.X, m_Player.Y, m_Player.Z + 30)))
            {
                m_Player.Z += 10;
                m_Player.CantWalk = true;
                m_Player.Emote("*begin to hover off the ground!*");
            }

            base.Drink(from);
        }
    }

    public class LesserFloatPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.LesserFloat; } }
        public override TimeSpan Duration { get { return TimeSpan.FromSeconds(5); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public LesserFloatPotion(int amount)
            : base(PotionEffect.LesserFloat)
        {
            Name = "Lesser Float Potion";
            Hue = Utility.RandomBlueHue();
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public LesserFloatPotion()
            : base(PotionEffect.LesserFloat)
        {
            Name = "Lesser Float Potion";
            Hue = Utility.RandomBlueHue();
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new LesserFloatPotion(amount), amount);
        }

        public LesserFloatPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect) && m_Player.CanDrink(PotionEffect.LesserFloat) && m_Player.CanDrink(PotionEffect.Float) && m_Player.InLOS(new Point3D(m_Player.X, m_Player.Y, m_Player.Z + 30)))
            {
                m_Player.Z += 5;
                m_Player.CantWalk = true;
                m_Player.Emote("*begin to hover off the ground!*");
            }

            base.Drink(from);
        }
    }

    public class ChameleonPotion : TeiravonPotion
    {
        public override PotionEffect PEffect { get { return PotionEffect.Chameleon; } }
        public override TimeSpan Duration { get { return TimeSpan.FromMinutes(3); } }
        public override bool Racial { get { return false; } }

        [Constructable]
        public ChameleonPotion(int amount)
            : base(PotionEffect.Chameleon)
        {
            Name = "Chameleon Potion";
            Hue = 0x84C;
            Stackable = true;
            Amount = amount;
        }

        [Constructable]
        public ChameleonPotion()
            : base(PotionEffect.Chameleon)
        {
            Name = "Chameleon Potion";
            Hue = 0x84C;
            Stackable = true;
            Amount = 1;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new ChameleonPotion(amount), amount);
        }

        public ChameleonPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            TeiravonMobile m_Player = (TeiravonMobile)from;

            if (m_Player.CanDrink(PEffect))
            {
                m_Player.SolidHueOverride = 0x7fff;
            }

            base.Drink(from);
        }
    }
}
