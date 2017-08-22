using System;
using Server.Network;
using Server.Multis;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Regions;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Spells.Seventh
{
	public class GateTravelSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Gate Travel", "Vas Rel Por",
				SpellCircle.Eighth,
				263,
				9032,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		private RunebookEntry m_Entry;
        private int m_Runes;
        public int Runes { get { return m_Runes; } set { m_Runes = value; } }
        public override int GetMana()
        {
            return 100;
        }
		public GateTravelSpell( Mobile caster, Item scroll ) : this( caster, scroll, null )
		{
		}

		public GateTravelSpell( Mobile caster, Item scroll, RunebookEntry entry ) : base( caster, scroll, m_Info )
		{
			m_Entry = entry;
		}

        public void DoTimer()
        {
            Timer.DelayCall(TimeSpan.FromSeconds(1.5), new TimerStateCallback(MoonGate_Callback), new object[] { this,0, new Point3D(0,0,0) });
        }

		public override void OnCast()
		{
            //AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.2), TimeSpan.FromSeconds(0.4), 0.2, Caster.Map, Caster.Location, 0x377A, 15, 0, 3, 8, 10, false, false, false);
            Caster.SendGump(new GateGump((TeiravonMobile)Caster,0,0,0,0,this));
            /*
			if ( m_Entry == null )
				Caster.Target = new InternalTarget( this );
			else
				Effect( m_Entry.Location, m_Entry.Map, true );*/
		}

        public class GateGump : Gump
        {
            public enum Buttons
            {
                NullButton,
                Rune1,
                Rune2,
                Rune3,
                Rune4,
                Rune5,
                OkCast
            }
            private GateTravelSpell m_GateSpell;
            private TeiravonMobile tm;
            private int rcnt;
            int[] RuneList = new int[5];
            int tmpval;

            public GateGump(TeiravonMobile TMPlayer, int cnt, int s1, int s2, int s3, GateTravelSpell spell)
                : base(50, 50)
            {
                m_GateSpell = spell;
                tm = TMPlayer;
                rcnt = cnt;
                RuneList[1] = s1;
                RuneList[2] = s2;
                RuneList[3] = s3;

                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddImage(0, 0, 0x7724);
                if (RuneList[1] != 0)
                    this.AddItem(165, 220, 3673 + (RuneList[1] * 3));
                if (RuneList[2] != 0)
                    this.AddItem(210, 210, 3673 + (RuneList[2] * 3));
                if (RuneList[3] != 0)
                    this.AddItem(245, 220, 3673 + (RuneList[3] * 3));

                this.AddButton(364, 411, 247, 248, (int)Buttons.OkCast, GumpButtonType.Reply, 0);
                this.AddButton(40, 411, 241, 243, (int)Buttons.NullButton, GumpButtonType.Reply, 0);

                //Misc
                //int laby = 98;
                //int buty = 100;
                int rune = 3673;


                //this.AddItem(40, laby += 25, rune += 3);
                if (rcnt < 3)
                {
                    this.AddButton(220, 65, 9026, 9028, (int)Buttons.Rune1, GumpButtonType.Reply, 0);
                    this.AddItem(210, 45, rune += 3);
                }
                //this.AddItem(40, laby += 25, rune += 3);
                if (rcnt < 3){
                    this.AddButton(115, 160, 9026, 9028, (int)Buttons.Rune2, GumpButtonType.Reply, 0);
                    this.AddItem(85, 140, rune += 3);
                }
                //this.AddItem(40, laby += 25, rune += 3);
                if (rcnt < 3){
                    this.AddButton(320, 160, 9026, 9028, (int)Buttons.Rune3, GumpButtonType.Reply, 0);
                    this.AddItem(330, 140, rune += 3);
                }
                //this.AddItem(40, laby += 25, rune += 3);
                if (rcnt < 3){
                    this.AddButton(90, 290, 9026, 9028, (int)Buttons.Rune4, GumpButtonType.Reply, 0);
                    this.AddItem(60, 310, rune += 3);
                }
                //this.AddItem(40, laby += 25, rune += 3);
                if (rcnt < 3){
                    this.AddButton(350, 290, 9026, 9028, (int)Buttons.Rune5, GumpButtonType.Reply, 0);
                    this.AddItem(360, 310, rune += 3);
                }

            }
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
            try
            {

                switch (info.ButtonID)
                {
                    case (int)Buttons.Rune1:
                        rcnt += 1;
                        RuneList[rcnt] = 1;
                        tm.SendGump(new GateGump(tm, rcnt, RuneList[1], RuneList[2], RuneList[3],m_GateSpell));
                        break;
                    case (int)Buttons.Rune2:
                        rcnt += 1;
                        RuneList[rcnt] = 2;
                        tm.SendGump(new GateGump(tm, rcnt, RuneList[1], RuneList[2], RuneList[3],m_GateSpell));
                        break;
                    case (int)Buttons.Rune3:
                        rcnt += 1;
                        RuneList[rcnt] = 3;
                        tm.SendGump(new GateGump(tm, rcnt, RuneList[1], RuneList[2], RuneList[3],m_GateSpell));
                        break;
                    case (int)Buttons.Rune4:
                        rcnt += 1;
                        RuneList[rcnt] = 4;
                        tm.SendGump(new GateGump(tm, rcnt, RuneList[1], RuneList[2], RuneList[3],m_GateSpell));
                        break;
                    case (int)Buttons.Rune5:
                        rcnt += 1;
                        RuneList[rcnt] = 5;
                        tm.SendGump(new GateGump(tm, rcnt, RuneList[1], RuneList[2], RuneList[3],m_GateSpell));
                        break;
                    case (int)Buttons.OkCast:
                        if (rcnt == 3)
                        {
                            m_GateSpell.Runes = (RuneList[1] * 100) + (RuneList[2] * 10) + RuneList[3];
                            m_GateSpell.DoTimer();
                        }
                        else
                        {
                            tm.SendMessage("You must choose 3 runes. You have only selected {0}", rcnt);
                            tm.SendGump(new GateGump(tm, rcnt, 0, 0, 0,m_GateSpell));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch { }
		}

	}

        public static void MoonGate_Callback(object state)
        {
            object[] states = ((object[])state);
            GateTravelSpell m_Spell = (GateTravelSpell)states[0];
            int m_Step = (int)states[1];
            Point3D m_Loc = (Point3D)states[2];
            Map m_Map = Map.Felucca;

            if (m_Spell.Runes != 0 && m_Spell.Runes != null && m_Loc.X == 0)
            {
                switch (m_Spell.Runes)
                {
                    case 152:
                        m_Loc = new Point3D(3145, 3107, -4);
                        break;
                    case 324:
                        m_Loc = new Point3D(3619, 2224,1);
                        break;
                    case 545:
                        m_Loc = new Point3D(3159, 1498, 31);
                        break;
                    case 135:
                        m_Loc = new Point3D(4097, 1979, 1);
                        break;
                    case 412:
                        m_Loc = new Point3D(3149, 405, 1);
                        break;
                    case 345:
                        m_Loc = new Point3D(1083, 2989, -4);
                        break;
                    case 213:
                        m_Loc = new Point3D(4582, 282, 1);
                        break;
                }
            }

            if (!m_Spell.Caster.Mounted && m_Spell.Caster.Body.IsHuman)
                m_Spell.Caster.Animate(263, 7, 1, true, false, 0);
            if (m_Step < 3)
            {
                m_Step++;
                Timer.DelayCall(TimeSpan.FromSeconds(1.5), new TimerStateCallback(MoonGate_Callback), new object[] { m_Spell, m_Step, m_Loc });
                AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1), 0.1, m_Spell.Caster.Map, (Point3D)m_Spell.Caster.Location, 0x377A, 15, 0, 2, 8, 10, true, false, false);
                if (m_Loc.X != 0)
                    AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1), 0.1, m_Map, new Point3D(m_Loc.X, m_Loc.Y, m_Loc.Z + 5), 0x377A, 4, 0, 2, 8, 10, true, false, false);
            }
            else if (m_Loc.X != 0)
            {
                m_Spell.Effect(m_Loc, m_Map, false);
                m_Spell.FinishSequence();
            }
            else
            {
                m_Spell.Caster.SendMessage("You have failed to open a gate to anywhere");
                m_Spell.DoFizzle();
                m_Spell.FinishSequence();
            }
        }

		public override bool CheckCast()
		{
			if ( Factions.Sigil.ExistsOn( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061632 ); // You can't do that while carrying the sigil.
				return false;
			}
			else if ( Caster.Criminal )
			{
				Caster.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else if ( SpellHelper.CheckCombat( Caster ) )
			{
				Caster.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
				return false;
			}

			return SpellHelper.CheckTravel( Caster, TravelCheckType.GateFrom );
		}

		public void Effect( Point3D loc, Map map, bool checkMulti )
		{/*
            if ( !map.CanSpawnMobile( loc.X, loc.Y, loc.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}*/
			if ( CheckSequence() )
			{
				Caster.SendLocalizedMessage( 501024 ); // You open a magical gate to another location

				Effects.PlaySound( Caster.Location, Caster.Map, 0x20E );

				InternalItem firstGate = new InternalItem( loc, map );
				firstGate.MoveToWorld( Caster.Location, Caster.Map );

				Effects.PlaySound( loc, map, 0x20E );

				InternalItem secondGate = new InternalItem( Caster.Location, Caster.Map );
				secondGate.MoveToWorld( loc, map );
			}

			FinishSequence();
		}

		[DispellableField]
		private class InternalItem : Moongate
		{
			public override bool ShowFeluccaWarning{ get{ return Core.AOS; } }

			public InternalItem( Point3D target, Map map ) : base( target, map )
			{
				Map = map;

				if ( ShowFeluccaWarning && map == Map.Felucca )
					ItemID = 0xDDA;

				Dispellable = true;

				InternalTimer t = new InternalTimer( this );
				t.Start();
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				Delete();
			}

			private class InternalTimer : Timer
			{
				private Item m_Item;

				public InternalTimer( Item item ) : base( TimeSpan.FromSeconds( 30.0 ) )
				{
					Priority = TimerPriority.OneSecond;
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}

		private class InternalTarget : Target
		{
			private GateTravelSpell m_Owner;

			public InternalTarget( GateTravelSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;

				owner.Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501029 ); // Select Marked item.
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					RecallRune rune = (RecallRune)o;

					if ( rune.Marked )
						m_Owner.Effect( rune.Target, rune.TargetMap, true );
					else
						from.SendLocalizedMessage( 501803 ); // That rune is not yet marked.
				}
				else if ( o is Runebook )
				{
					RunebookEntry e = ((Runebook)o).Default;

					if ( e != null )
						m_Owner.Effect( e.Location, e.Map, true );
					else
						from.SendLocalizedMessage( 502354 ); // Target is not marked.
				}
				/*else if ( o is Key && ((Key)o).KeyValue != 0 && ((Key)o).Link is BaseBoat )
				{
					BaseBoat boat = ((Key)o).Link as BaseBoat;

					if ( !boat.Deleted && boat.CheckKey( ((Key)o).KeyValue ) )
						m_Owner.Effect( boat.GetMarkedLocation(), boat.Map, false );
					else
						from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501030, from.Name, "" ) ); // I can not gate travel from that object.
				}*/
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501030, from.Name, "" ) ); // I can not gate travel from that object.
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}