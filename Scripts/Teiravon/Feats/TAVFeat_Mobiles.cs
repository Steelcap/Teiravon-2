using System;
using System.Collections;
using Server;
using Server.Items;
using Server.ContextMenus;
using Server.Gumps;
using Server.Network;
using Server.Teiravon;
using Server.Spells;

namespace Server.Mobiles
{
	#region BaseCompanion
	public abstract class BaseCompanion : BaseCreature
	{
		public BaseCompanion() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
		}

		public override bool BardImmune{ get{ return true; } }
		public override bool Commandable{ get{ return false; } }

		private bool m_LastHidden;

		public override void OnThink()
		{
			base.OnThink();

			Mobile master = ControlMaster;


			if ( master == null )
				return;

			if ( master.Deleted || master.Map != this.Map || !InRange( master.Location, 35 ) )
			{
				DropPackContents();
				EndRelease( null );
				return;
			}

			if ( m_LastHidden != master.Hidden )
				Hidden = m_LastHidden = master.Hidden;

			Mobile toAttack = null;

			if ( !Hidden )
			{
				toAttack = master.Combatant;

				if ( toAttack == this )
					toAttack = master;
				else if ( toAttack == null )
					toAttack = this.Combatant;
			}

			if ( Combatant != toAttack )
				Combatant = null;

			if ( toAttack == null )
			{
				if ( ControlTarget != master || ControlOrder != OrderType.Follow )
				{
					ControlTarget = master;
					ControlOrder = OrderType.Follow;
				}
			}
			else if ( ControlTarget != toAttack || ControlOrder != OrderType.Attack )
			{
				ControlTarget = toAttack;
				ControlOrder = OrderType.Attack;
			}
		}

		public override void GetContextMenuEntries( Mobile from, ArrayList list )
		{
			base.GetContextMenuEntries( from, list );

            if (from.Alive && Controled && from == ControlMaster && from.InRange(this, 14))
            {
                list.Add(new ReleaseEntry(from, this));
                list.Add(new StandDownEntry(from, this));
            }
		}

		public virtual void BeginRelease( Mobile from )
		{
			if ( !Deleted && Controled && from == ControlMaster && from.CheckAlive() )
				EndRelease( from );
		}

		public virtual void EndRelease( Mobile from )
		{
			if ( from == null || (!Deleted && Controled && from == ControlMaster && from.CheckAlive()) )
			{
				Delete();
			}
		}

		public virtual void DropPackContents()
		{
			Map map = this.Map;
			Container pack = this.Backpack;

			if ( map != null && map != Map.Internal && pack != null )
			{
				ArrayList list = new ArrayList( pack.Items );

				for ( int i = 0; i < list.Count; ++i )
					((Item)list[i]).MoveToWorld( Location, map );
			}
		}

		public BaseCompanion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			DropPackContents();
			Delete();
		}

		private class ReleaseEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private BaseCompanion m_Companion;

			public ReleaseEntry( Mobile from, BaseCompanion Companion ) : base( 6118, 14 )
			{
				m_From = from;
				m_Companion = Companion;
			}

			public override void OnClick()
			{
				if ( !m_Companion.Deleted && m_Companion.Controled && m_From == m_Companion.ControlMaster && m_From.CheckAlive() )
					m_Companion.BeginRelease( m_From );
			}
		}

        private class StandDownEntry : ContextMenuEntry
        {
            private Mobile m_From;
            private BaseCompanion m_Companion;

            public StandDownEntry(Mobile from, BaseCompanion Companion)
                : base(6112, 14)
            {
                m_From = from;
                m_Companion = Companion;
            }

            public override void OnClick()
            {
                if (!m_Companion.Deleted && m_Companion.Controled && m_From == m_Companion.ControlMaster && m_From.CheckAlive())
                    m_Companion.Combatant = null;
            }
        }
	}
	#endregion

    #region MountainGoatCompanion
    [CorpseName("a mountain goat corpse")]
    [TypeAlias("Server.Mobiles.MountainGoatCompanion")]
    public class MountainGoatCompanion : BaseCompanion
    {
        [Constructable]
        public MountainGoatCompanion()
        {
            Name = "a mountain goat";
            Body = 88;
            BaseSoundID = 0x99;
            Level = 7;
            Hue = 2304;

            SetStr(126, 155);
            SetDex(81, 105);
            SetInt(16, 40);

            SetHits(76, 93);
            SetMana(0);

            SetDamage(8, 13);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 70.1, 100.0);
            SetSkill(SkillName.Wrestling, 45.1, 70.0);

            Fame = 1000;
            Karma = 0;

            VirtualArmor = 30;

            Tamable = false;
            ControlSlots = 1;
        }

        public override int Meat { get { return 2; } }
        public override int Hides { get { return 16; } }
        public override FoodType FavoriteFood { get { return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Bear; } }

        public MountainGoatCompanion(Serial serial)
            : base(serial)
        {
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
    #endregion

	#region BearCompanion
	[CorpseName( "a bear corpse" )]
	[TypeAlias( "Server.Mobiles.Bearcompanion" )]
	public class BearCompanion : BaseCompanion
	{
		[Constructable]
		public BearCompanion()
		{
			Name = "a bear";
			Body = 211;
			BaseSoundID = 0xA3;
			Level = 7;

			SetStr( 126, 155 );
			SetDex( 81, 105 );
			SetInt( 16, 40 );

			SetHits( 76, 93 );
			SetMana( 0 );

			SetDamage( 8, 13 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 1000;
			Karma = 0;

			VirtualArmor = 30;

			Tamable = false;
			ControlSlots = 1;
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }

		public BearCompanion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	#endregion

	#region EagleCompanion

	[CorpseName( "an eagle corpse" )]
	public class EagleCompanion : BaseCompanion
	{
		[Constructable]
		public EagleCompanion()
		{
			Name = "an eagle";
			Body = 5;
			BaseSoundID = 0x2EE;
			Level = 2;

			SetStr( 31, 47 );
			SetDex( 36, 60 );
			SetInt( 8, 20 );

			SetHits( 20, 27 );
			SetMana( 0 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 15.3, 30.0 );
			SetSkill( SkillName.Tactics, 18.1, 37.0 );
			SetSkill( SkillName.Wrestling, 20.1, 30.0 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 30;

			Tamable = false;
			ControlSlots = 1;
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 36; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public EagleCompanion(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	#endregion

	#region WolfCompanion

	[CorpseName( "a wolf corpse" )]
	[TypeAlias( "Server.Mobiles.Wolfcompanion" )]
	public class WolfCompanion : BaseCompanion
	{
		[Constructable]
		public WolfCompanion()
		{
			Name = "a wolf";
			Body = 225;
			BaseSoundID = 0xE5;
			Level = 5;

			SetStr( 56, 80 );
			SetDex( 56, 75 );
			SetInt( 11, 25 );

			SetHits( 34, 48 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 27.6, 45.0 );
			SetSkill( SkillName.Tactics, 30.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 30;

			Tamable = false;
			ControlSlots = 1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public WolfCompanion(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	#endregion

	#region PantherCompanion

	[CorpseName( "a panther corpse" )]
	public class PantherCompanion : BaseCompanion
	{
		[Constructable]
		public PantherCompanion()
		{
			Name = "a panther";
			Body = 214;
			Hue = 2306;
			BaseSoundID = 0x462;
			Level = 5;

			SetStr( 61, 85 );
			SetDex( 86, 105 );
			SetInt( 26, 50 );

			SetHits( 37, 51 );
			SetMana( 0 );

			SetDamage( 4, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 15.1, 30.0 );
			SetSkill( SkillName.Tactics, 50.1, 65.0 );
			SetSkill( SkillName.Wrestling, 50.1, 65.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 30;

			Tamable = false;
			ControlSlots = 1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public PantherCompanion(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	#endregion

	#region GreatHartCompanion

	[CorpseName( "a deer corpse" )]
	[TypeAlias( "Server.Mobiles.Greathart" )]
	public class GreatHartCompanion : BaseCompanion
	{
		[Constructable]
		public GreatHartCompanion()
		{
			Name = "a great hart";
			Body = 0xEA;
			Level = 4;

			SetStr( 41, 71 );
			SetDex( 47, 77 );
			SetInt( 27, 57 );

			SetHits( 27, 41 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Cold, 5, 10 );

			SetSkill( SkillName.MagicResist, 26.8, 44.5 );
			SetSkill( SkillName.Tactics, 29.8, 47.5 );
			SetSkill( SkillName.Wrestling, 29.8, 47.5 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 30;

			Tamable = false;
			ControlSlots = 1;
		}

		public override int Meat{ get{ return 6; } }
		public override int Hides{ get{ return 15; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public GreatHartCompanion(Serial serial) : base(serial)
		{
		}

		public override int GetAttackSound()
		{
			return 0x82;
		}

		public override int GetHurtSound()
		{
			return 0x83;
		}

		public override int GetDeathSound()
		{
			return 0x84;
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
		}
	}
	#endregion

    #region SpiderCompanion
    [CorpseName("a giant spider corpse")]
    [TypeAlias("Server.Mobiles.SpiderCompanion")]
    public class SpiderCompanion : BaseCompanion
    {
        [Constructable]
        public SpiderCompanion()
        {
            Name = "a giant spider";
            Body = 0x9D;
            BaseSoundID = 0x388;
            Level = 7;

            SetStr(126, 155);
            SetDex(81, 105);
            SetInt(16, 40);

            SetHits(76, 93);
            SetMana(0);

            SetDamage(8, 13);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Cold, 15, 25);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.MagicResist, 25.1, 40.0);
            SetSkill(SkillName.Tactics, 70.1, 100.0);
            SetSkill(SkillName.Wrestling, 45.1, 70.0);

            Fame = 1000;
            Karma = 0;

            VirtualArmor = 30;

            Tamable = false;
            ControlSlots = 1;
        }

        public override int Meat { get { return 2; } }
        public override int Hides { get { return 16; } }
        public override FoodType FavoriteFood { get { return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Bear; } }

        public SpiderCompanion(Serial serial)
            : base(serial)
        {
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
    #endregion

	#region WarMount
	public class WarMount : Horse
	{
		public override void OnDeath( Container c )
		{
			if ( ControlMaster != null )
			{
				TeiravonMobile m_Player = (TeiravonMobile)ControlMaster;

				m_Player.WarMountDeaths += 1;
				ControlMaster.SendMessage( "Your War Mount has died.", m_Player.WarMountDeaths );
			}

			base.OnDeath( c );
		}
		[Constructable]
		public WarMount()
		{
			Name = "a war mount";
			Level = 0;
			BodyValue = 284;
			ItemID = 16018;

			SetStr( 164, 197 );
			SetDex( 81, 105 );
			SetInt( 16, 40 );

			SetHits( 176, 264 );
			SetMana( 0 );

			SetDamage( 12, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 80.0 );

			Fame = 1000;
			Karma = 0;

			VirtualArmor = 30;

			Tamable = false;
			ControlSlots = 1;
		}

		public override int Meat{ get{ return 0; } }
		public override int Hides{ get{ return 0; } }

		public WarMount( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	#endregion

    #region MirrorImage
    public class Clone : BaseCreature
        {
            private Mobile m_Caster;

            public Clone(Mobile caster)
                : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.2, 0.4)
            {
                m_Caster = caster;

                Body = caster.Body;

                Hue = caster.Hue;
                Female = caster.Female;

                Name = caster.Name;
                NameHue = caster.NameHue;

                Fame = caster.Fame;
                Kills = caster.Kills;
                


                for (int i = 0; i < caster.Skills.Length; ++i)
                {
                    Skills[i].Base = caster.Skills[i].Base;
                    Skills[i].Cap = caster.Skills[i].Cap;
                }

                for (int i = 0; i < caster.Items.Count; i++)
                {
                    AddItem(CloneItem((Item)caster.Items[i]));
                }
                
                AccessLevel = AccessLevel.Seer;

                Warmode = caster.Warmode;

                Summoned = true;
                SummonMaster = this;

                ControlOrder = OrderType.Follow;
                ControlTarget = caster;
                ControlSlots = 1;

                TimeSpan duration = TimeSpan.FromSeconds(45);

                new UnsummonTimer(caster, this, duration).Start();
                SummonEnd = DateTime.Now + duration;

                MirrorImage.AddClone(m_Caster);
            }
            protected override BaseAI ForcedAI { get { return new CloneAI(this); } }
            public override bool IsHumanInTown() { return false; }

            private Item CloneItem(Item item)
            {
                Item newItem = new Item(item.ItemID);
                newItem.Hue = item.Hue;
                newItem.Layer = item.Layer;

                return newItem;
            }

            public override void OnDamage(int amount, Mobile from, bool willKill)
            {
                Delete();
            }

            public override bool DeleteCorpseOnDeath { get { return true; } }

            public override void OnDelete()
            {
                Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 15, 5042);

                base.OnDelete();
            }

            public override bool OnMoveOver(Mobile m)
            {
                if (m == SummonMaster)
                    return true;
                else
                    return base.OnMoveOver(m);
            }

            public override void OnAfterDelete()
            {
                MirrorImage.RemoveClone(m_Caster);
                base.OnAfterDelete();
            }

            public override bool Commandable { get { return false; } }

            public Clone(Serial serial)
                : base(serial)
            {
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
    #endregion

    #region gust

    public class Gust : BaseCreature
    {
        private Mobile m_Caster;
        public Mobile shove;
        public Gust(TeiravonMobile caster)
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.02, 0.04)
        {
            m_Caster = caster;

            Body = 58;
            AccessLevel = AccessLevel.Seer;
            Hidden = true;
            Hue = 0;
         
            Name = "Gust of Wind";

            Summoned = false;
            SummonMaster = caster;

            TimeSpan duration = TimeSpan.FromSeconds(0.5);

            new UnsummonTimer(caster, this, duration).Start();
            SummonEnd = DateTime.Now + duration;

        }

        public Gust(Mobile caster, Mobile m)
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.02, 0.04)
        {
            m_Caster = caster;
            ControlTarget = m;

            Body = 58;
            AccessLevel = AccessLevel.Seer;
            Hidden = true;
            Hue = 0;

            Name = "Gust of Wind";
            Summoned = true;
            SummonMaster = caster;
            TimeSpan duration = TimeSpan.FromSeconds(0.5);

            new UnsummonTimer(caster, this, duration).Start();
            SummonEnd = DateTime.Now + duration;

        }
        public Gust(Mobile caster, Mobile m, double time)
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.02, 0.04)
        {
            m_Caster = caster;
            ControlTarget = m;

            Body = 58;
            AccessLevel = AccessLevel.Seer;
            Hidden = true;
            Hue = 0;

            Name = "Gust of Wind";
            Summoned = true;
            SummonMaster = caster;
            TimeSpan duration = TimeSpan.FromSeconds(time);

            new UnsummonTimer(caster, this, duration).Start();
            SummonEnd = DateTime.Now + duration;

        }

        [Constructable]
        public Gust()
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.02, 0.04)
        {
            Body = 58;
            AccessLevel = AccessLevel.Seer;
            Hidden = true;
            Hue = 0;

            Name = "Gust of Wind";
            Summoned = true;
            TimeSpan duration = TimeSpan.FromSeconds(0.5);
            new UnsummonTimer(this, this, duration).Start();
            SummonEnd = DateTime.Now + duration;

        }

        protected override BaseAI ForcedAI { get { return new GustAI(this); } }
        public override bool IsHumanInTown() { return false; }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            Delete();
        }

        public override void OnThink()
        {
            if (ControlTarget != null)
            {
                ControlTarget.Location = Location;
                ControlTarget.Freeze(TimeSpan.FromSeconds(.5));
            }
           base.OnThink();
        }
        public override bool DeleteCorpseOnDeath { get { return true; } }

        public override bool Commandable { get { return false; } }

        public Gust(Serial serial)
            : base(serial)
        {
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

    public class GustToTarget : BaseCreature
    {
        private Mobile m_Caster;
        public Mobile shove;
        public GustToTarget(TeiravonMobile caster)
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.02, 0.04)
        {
            m_Caster = caster;

            Body = 58;
            AccessLevel = AccessLevel.Seer;
            Hidden = true;
            Hue = 0;

            Name = "Gust of Wind";

            Summoned = false;
            SummonMaster = caster;

            TimeSpan duration = TimeSpan.FromSeconds(0.5);

            new UnsummonTimer(caster, this, duration).Start();
            SummonEnd = DateTime.Now + duration;

        }

        public GustToTarget(TeiravonMobile caster, Mobile m, IPoint3D targ)
            : base(AIType.AI_Melee, FightMode.None, 10, 1, 0.02, 0.04)
        {
            m_Caster = caster;
            ControlTarget = m;

            Body = 58;
            AccessLevel = AccessLevel.Seer;
            Hidden = true;
            Hue = 0;

            Name = "Gust of Wind";
            Summoned = true;
            SummonMaster = caster;
            TimeSpan duration = TimeSpan.FromSeconds(1.5);

            new UnsummonTimer(caster, this, duration).Start();
            SummonEnd = DateTime.Now + duration;
            Home = (Point3D)targ;
        }

        public override bool IsHumanInTown() { return false; }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            Delete();
        }

        public override void OnThink()
        {
            if (ControlTarget != null)
            {
                ControlTarget.Location = Location;
                ControlTarget.Freeze(TimeSpan.FromSeconds(.5));
            }
            base.OnThink();
        }
        public override bool DeleteCorpseOnDeath { get { return true; } }

        public override bool Commandable { get { return false; } }

        public GustToTarget(Serial serial)
            : base(serial)
        {
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
    #endregion

    #region AI
    public class CloneAI : BaseAI
    {
        public CloneAI(BaseCreature m)
            : base(m)
        {
            m.CurrentSpeed = m.ActiveSpeed;
        }

        public override bool Think()
        {
            // Clones only follow their owners
            Mobile master = m_Mobile.SummonMaster;

            if (master != null && master.Map == m_Mobile.Map && master.InRange(m_Mobile, m_Mobile.RangePerception))
            {
                int iCurrDist = (int)m_Mobile.GetDistanceToSqrt(master);
                bool bRun = (iCurrDist > 5);

                WalkMobileRange(master, 2, bRun, 0, 1);
            }
            else
                WalkRandom(2, 2, 1);

            return true;
        }
    }

    public class GustAI : BaseAI
    {
        public GustAI(Gust m)
            : base(m)
        {
            m.CurrentSpeed = m.ActiveSpeed;
        }

        public override bool Think()
        {
            // Gusts run away from their masters
            Mobile master = m_Mobile.SummonMaster;

            if (master != null && master.Map == m_Mobile.Map && master.InRange(m_Mobile, m_Mobile.RangePerception))
            {
                m_Mobile.FocusMob = master;
                DoActionFlee();
            }
            else
                WalkRandom(0, 0, 1);
            if (m_Mobile is Gust)
            {
                Gust g = m_Mobile as Gust;
                if (g.shove != null)
                {
                    g.shove.Location = g.Location;
                    g.shove = null;
                }
            }

            return true;
        }
    }
    #endregion

}
