using System;
using Server;
using System.Collections;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Spells;
using Server.Items;
using Server.Gumps;
using Server.Scripts.Commands;

namespace Server.Scripts.Commands
{
	public class TAVMageSpellCommands
	{
        public static Hashtable MGSPA = new Hashtable();

        public static void AddMGSPA(TeiravonMobile from)
        {
            MGSPA.Add(from, from);
            Timer.DelayCall(TimeSpan.FromSeconds(2), new TimerStateCallback(EndMGSPA_Callback),
            new object[] { from });
        }

        public static void EndMGSPA_Callback(object state)
        {
            object[] args = (object[])state;

            TeiravonMobile from = (TeiravonMobile)args[0];

            if (MGSPA.Contains(from))
                MGSPA.Remove(from);
        }

		#region Command Initializers
		public static void Initialize()
		{
			Server.Commands.Register( "MgSpA", AccessLevel.Player, new CommandEventHandler( MageSpell1_OnCommand ) );
			Server.Commands.Register( "MgSpB", AccessLevel.Player, new CommandEventHandler( MageSpell2_OnCommand ) );
			Server.Commands.Register( "MgSpC", AccessLevel.Player, new CommandEventHandler( MageSpell3_OnCommand ) );
			//Server.Commands.Register( "MgSpD", AccessLevel.Player, new CommandEventHandler( MageSpell4_OnCommand ) );
		}
		#endregion

        

		[Usage( "MgSpA" )]
		[Description( "Custom Mage Spell Offense Melee Range" )]
		private static void MageSpell1_OnCommand( CommandEventArgs e )
		{
				TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
                if (!MGSPA.Contains(m_Player))
                {
                    if (m_Player.IsMage() && !m_Player.IsNecromancer())
                    {
                        if (m_Player.Paralyzed || m_Player.Frozen)
                            m_Player.SendMessage("You cannot cast a spell while frozen.");
                        else
                        {
                            if (m_Player.Mana >= 30)
                            {
                                m_Player.Target = new MageSpell1Target(m_Player);

                            }
                            else
                                m_Player.SendMessage("You must have 30 mana to cast this!");
                        }
                    }
                    else
                    {
                        m_Player.SendMessage("Only Elemental Mages can use this command");
                    }
                }
                else
                {
                    m_Player.SendMessage("You must wait before using this abilitiy again.");
                }
		}

		[Usage( "MgSpB" )]
		[Description( "Custom Mage Spell Offense Ranged Targeted" )]
		private static void MageSpell2_OnCommand( CommandEventArgs e )
		{
				TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
				if (m_Player.IsMage() && !m_Player.IsNecromancer())
				{
					if (m_Player.Paralyzed || m_Player.Frozen)
						m_Player.SendMessage("You cannot cast a spell while frozen.");
					else
					{
						if (m_Player.Mana >= 5 + (5 * (int)(m_Player.PlayerLevel/3)))
							m_Player.Target = new MageSpell2Target( m_Player );
						else
							m_Player.SendMessage("You must have {0} mana to cast this!", 5 + (5 * (int)(m_Player.PlayerLevel/3)));
					}
				}
				else
				{
					m_Player.SendMessage("Only Elemental Mages can use this command");
				}
		}
		[Usage( "MgSpC" )]
		[Description( "Custom Mage Spell Defense/Transform" )]
		private static void MageSpell3_OnCommand( CommandEventArgs e )
		{
				TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
				if (m_Player.IsMage() && !m_Player.IsNecromancer())
				{
					if (m_Player.Paralyzed || m_Player.Frozen)
						m_Player.SendMessage("You cannot cast a spell while frozen.");
					else
					{
						if (m_Player.Mana >= 10 * (int)(m_Player.PlayerLevel/2))
						{	if (m_Player.OBody == 0)
								if (!m_Player.Mounted)
									EleTransform(m_Player);
								else
									m_Player.SendMessage("You cannot cast this while mounted.");
							else
								m_Player.SendMessage("You are already transformed!");
						}
						else
							m_Player.SendMessage("You must have {0} mana to cast this!", 10 * (int)(m_Player.PlayerLevel/2));
					}
				}
				else
				{
					m_Player.SendMessage("Only Elemental Mages can use this command");
				}
		}

		public static void EleTransform( TeiravonMobile from )
		{
			int EleBody;
			int StrBon;
			int DexBon;
			int PhysAdj;
			int FireAdj;
			int ColdAdj;
			int PoisAdj;
			int EnerAdj;
			
			if (from.IsGeomancer())
			{
				EleBody = 14;
				StrBon = 30;
				DexBon = 10;
				PhysAdj = 30 + (from.PlayerLevel * 2);
				FireAdj = from.PlayerLevel;
				ColdAdj = from.PlayerLevel;
				PoisAdj = from.PlayerLevel;
				EnerAdj = -10 - ((int)(from.PlayerLevel * 1.5));
			}
			else if (from.IsPyromancer())
			{
				EleBody = 15;
				StrBon = 15;
				DexBon = 25;
				FireAdj = 30 + (from.PlayerLevel * 2);
				PhysAdj = from.PlayerLevel;
				EnerAdj = from.PlayerLevel;
				PoisAdj = from.PlayerLevel;
				ColdAdj = -10 - ((int)(from.PlayerLevel * 1.5));
			}
			else if (from.IsAeromancer())
			{
				EleBody = 13;
				StrBon = 10;
				DexBon = 30;
				EnerAdj = 30 + (from.PlayerLevel * 2);
				FireAdj = from.PlayerLevel;
				ColdAdj = from.PlayerLevel;
				PoisAdj = from.PlayerLevel;
				PhysAdj = -10 - ((int)(from.PlayerLevel * 1.5));
			}
			else  //Aquamancer
			{
				EleBody = 16;
				StrBon = 25;
				DexBon = 15;
				ColdAdj = 30 + (from.PlayerLevel * 2);
				EnerAdj = from.PlayerLevel;
				PhysAdj = from.PlayerLevel;
				PoisAdj = from.PlayerLevel;
				FireAdj = -10 - ((int)(from.PlayerLevel * 1.5));
			}

			from.SendMessage("You call upon the elemental forces and Transform yourself!");
			
			TimeSpan StatDuration = TimeSpan.FromMinutes(1 + ((int)(from.PlayerLevel/2)));
			DateTime Duration = DateTime.Now + TimeSpan.FromMinutes(2 + ((int)(from.PlayerLevel / 2)));
			from.Mana -= 10 * ((int)(from.PlayerLevel/2));
			
			Timer m_Timer = new EleTransformTimer( from, EleBody, StrBon, DexBon, PhysAdj, FireAdj, ColdAdj, PoisAdj, EnerAdj, StatDuration, Duration);
			m_Timer.Start();
		}
		
		private class EleTransformTimer : Timer
		{
			private TeiravonMobile m_player;
			private int m_EleBody;
			private int m_StrBon;
			private int m_DexBon;
			private int m_PhysAdj;
			private int m_FireAdj;
			private int m_ColdAdj;
			private int m_PoisAdj;
			private int m_EnerAdj;
			private TimeSpan m_StatDur;
			private ArrayList mods = new ArrayList();

			public EleTransformTimer( TeiravonMobile from, int body, int strb, int dexb, int ph, int fi, int co, int po, int en, TimeSpan statdur, DateTime end ) : base( end - DateTime.Now )
			{
				m_player = from;
				m_EleBody = body;
				m_StrBon = strb;
				m_DexBon = dexb;
				m_PhysAdj = ph;
				m_FireAdj = fi;
				m_ColdAdj = co;
				m_PoisAdj = po;
				m_EnerAdj = en;
				m_StatDur = statdur;
				
				m_player.OBody = m_EleBody;
				SpellHelper.AddStatBonus( m_player, m_player, StatType.Str, m_StrBon, m_StatDur );
				SpellHelper.AddStatBonus( m_player, m_player, StatType.Dex, m_DexBon, m_StatDur );

				mods.Add( new ResistanceMod( ResistanceType.Physical, m_PhysAdj ) );
				mods.Add( new ResistanceMod( ResistanceType.Fire, m_FireAdj ) );
				mods.Add( new ResistanceMod( ResistanceType.Cold, m_ColdAdj ) );
				mods.Add( new ResistanceMod( ResistanceType.Poison, m_PoisAdj ) );
				mods.Add( new ResistanceMod( ResistanceType.Energy, m_EnerAdj ) );

				for ( int i = 0; i < mods.Count; ++i )
					m_player.AddResistanceMod( (ResistanceMod)mods[i] );
			}

			protected override void OnTick()
			{
				for ( int i = 0; i < mods.Count; ++i )
					m_player.RemoveResistanceMod( (ResistanceMod)mods[i] );
				m_player.OBody = 0;
				
				m_player.SendMessage("You lose control of the elemental forces and transform back to normal.");
				Stop();
			}
		}

		[Usage( "MgSpD" )]
		[Description( "Custom Mage Spell Offense AOE" )]
		private static void MageSpell4_OnCommand( CommandEventArgs e )
		{
				TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;
				if (m_Player.IsMage() && !m_Player.IsNecromancer())
				{
					if (m_Player.Paralyzed || m_Player.Frozen)
						m_Player.SendMessage("You cannot cast a spell while frozen.");
					else
					{
						if (m_Player.Mana >= 125)
							MageAOEEffect(m_Player);
						else
							m_Player.SendMessage("You must have 125 mana to cast this!");
					}
				}
				else
				{
					m_Player.SendMessage("Only Elemental Mages can use this command");
				}
		}

		public static void MageAOEEffect( TeiravonMobile from )
		{
			TeiravonMobile player = from;
			int EffectID;
			int PhysDam;
			int FireDam;
			int ColdDam;
			int PoisDam;
			int EnerDam;
			int Dmg;
			int MageClass;
			ArrayList TargList = new ArrayList();
			
			player.Mana -= 125;

			if (from.IsGeomancer())
			{
				MageClass = 1;
				EffectID = 0x37FA;
				PhysDam = 100;
				FireDam = ColdDam = PoisDam = EnerDam = 0;
			}
			else if (from.IsPyromancer())
			{
				MageClass = 2;
				EffectID = 0x3709;
				FireDam = 100;
				PhysDam = ColdDam = PoisDam = EnerDam = 0;
			}
			else if (from.IsAeromancer())
			{
				MageClass = 3;
				EffectID = 0x375A;
				EnerDam = 100;
				FireDam = ColdDam = PoisDam = PhysDam = 0;
			}
			else // Aquamancer
			{
				MageClass = 4;
				EffectID = 0x372B;
				ColdDam = 100;
				FireDam = PhysDam = PoisDam = EnerDam = 0;
			}
			
			for ( int xx = -5; xx <= 5; ++xx )
			{
				for ( int yy = -5; yy <= 5; ++yy )
				{
					if (!((xx == 0) && (yy == 0)))
						Effects.SendLocationEffect( new Point3D( player.X + xx, player.Y + yy, player.Z ), player.Map, EffectID, 13 );
				}
			}

			Dmg = 10 * player.PlayerLevel;
			
			foreach( Mobile m in player.GetMobilesInRange( 5 ) )
			{
				if (m != player)
					TargList.Add(m);
			}
			
			if (TargList.Count != 0)
			{
				for ( int i = 0; i < TargList.Count; ++i )
				{
					Mobile m = (Mobile)TargList[i];
					AOS.Damage( m, player, Dmg, PhysDam, FireDam, ColdDam, PoisDam, EnerDam );
					
					switch (MageClass)
					{
						case 1:
							BleedAttack.BeginBleed(m, player);
							m.SendMessage( "The piercing spikes cause you to begin bleeding" );
							break;
						case 2:
							m.Stam = (int)(m.Stam/10);
							m.SendMessage("The scorching heats drains you of your stamina");
							break;
						case 3:
							m.Mana = (int)(m.Mana/10);
							m.SendMessage("The electrifying experience robs your of your ability to concentrate mana");
							break;
						case 4:
							m.Paralyze( m.Player ? TimeSpan.FromSeconds(6.0) : TimeSpan.FromSeconds(6.0) );
							m.SendMessage("The intence cold paralyzes you");
							break;
					}
				}
			}
		}
		
	}
}
	
namespace Server.Targets
{

	public class MageSpell1Target : Target
	{
		private TeiravonMobile m_Player;
		private int MageClass;
		private int phys;
		private int fire;
		private int nrgy;
		private int cold;
		private int pois;
		private int damage;
		private Timer m_timer;
		private bool timecheck = true;

		public MageSpell1Target(TeiravonMobile from) : base( -1, false, TargetFlags.Harmful )
		{
				if (from.IsGeomancer())
					MageClass = 1;
				else if (from.IsPyromancer())
					MageClass = 2;
				else if (from.IsAeromancer())
					MageClass = 3;
				else if (from.IsAquamancer())
					MageClass = 4;
				//else if (from.IsNecromancer())
				//	MageClass = 5;

				switch(MageClass)
				{
					case 1:
						phys = 100; fire = cold = pois = nrgy = 0;
						break;
					case 2:
						fire = 100; phys = cold = pois = nrgy = 0;
						break;
					case 3:
						nrgy = 100; phys = cold = pois = fire = 0;
						break;
					case 4:
						cold = 100; phys = fire = pois = nrgy = 0;
						break;
					//case 5:
					//	pois = 100; phys = fire = cold = nrgy = 0;
					//	break;
				}
		}
	
	protected override void OnTarget( Mobile from, object targ )
		{
			TeiravonMobile m_player = (TeiravonMobile)from;
			
			if (targ is Mobile)
			{
				Mobile m_targ = (Mobile)targ;
				if ( m_targ.InRange( m_player.Location, 1 ) )
				{
					damage = Utility.RandomMinMax(1,10) + ((int)(m_player.PlayerLevel) * 2);
					m_targ.FixedEffect(0x37B9, 244, 25);
					//AOS.Damage(m_targ, m_player, damage, phys, fire, cold, pois, nrgy);
                    SpellHelper.Damage(TimeSpan.Zero, m_targ, m_player, damage, phys, fire, cold, pois, nrgy);
					m_player.Mana -= 30;
                    			TAVMageSpellCommands.AddMGSPA(m_player);
				}
				else
				{
					m_player.SendMessage("That is too far away");
				}
			}
			else
			{
				from.SendMessage("Only Mobiles may be targeted");
			}
		}
	}

	public class MageSpell2Target : Target
	{
		private TeiravonMobile m_Player;
		private int MageClass;
		private int phys;
		private int fire;
		private int nrgy;
		private int cold;
		private int pois;
		private int damage;
		private Timer m_timer;
		private bool timecheck = true;
		private int effecthue;
		
		public MageSpell2Target(TeiravonMobile from) : base( -1, false, TargetFlags.Harmful )
		{
				if (from.IsGeomancer())
					MageClass = 1;
				else if (from.IsPyromancer())
					MageClass = 2;
				else if (from.IsAeromancer())
					MageClass = 3;
				else if (from.IsAquamancer())
					MageClass = 4;
				//else if (from.IsNecromancer())
				//	MageClass = 5;

				switch(MageClass)
				{
					case 1:
						phys = 100; fire = cold = pois = nrgy = 0;
						break;
					case 2:
						fire = 100; phys = cold = pois = nrgy = 0;
						break;
					case 3:
						nrgy = 100; phys = cold = pois = fire = 0;
						break;
					case 4:
						cold = 100; phys = fire = pois = nrgy = 0;
						break;
					//case 5:
					//	pois = 100; phys = fire = cold = nrgy = 0;
					//  effecthue = 2883;
					//	break;
				}
		}
	
	protected override void OnTarget( Mobile from, object targ )
		{
			TeiravonMobile m_player = (TeiravonMobile)from;
			
			if (targ is Mobile)
			{
				if (!(targ is TeiravonMobile))
				{
					Mobile m_targ = (Mobile)targ;
					Timer m_EffectTimer = new EffectTimer( m_player, m_targ, phys, fire, nrgy, cold, pois, 0, 1 + (int)(m_player.PlayerLevel/3), DateTime.Now + TimeSpan.FromSeconds( 0.25 ) );
					m_EffectTimer.Start();
					m_player.Mana -= 5 + (5 * (int)(m_player.PlayerLevel/3));
				}
				else
				{
					TeiravonMobile pctarg = (TeiravonMobile)targ;
					
					if (pctarg.ShadowShotReady)
						m_player.SendMessage("This spell is already active on that target!");
					else
					{
						Timer m_EffectTimer = new EffectTimer( m_player, pctarg, phys, fire, nrgy, cold, pois, 0, 1 + (int)(m_player.PlayerLevel/3), DateTime.Now + TimeSpan.FromSeconds( 0.25 ) );
						m_EffectTimer.Start();
						m_player.Mana -= 5 + (5 * (int)(m_player.PlayerLevel/3));
						pctarg.ShadowShotReady = true; //using shadowshotready to show spell active on target
					}
				}
				
			}
			else
			{
				from.SendMessage("Only Mobiles may be targeted");
			}
		}

		private class EffectTimer : Timer
		{
			private TeiravonMobile m_from;
			private Mobile m_targ;
			private int MaxCount;
			private int RunCount;
			private int d_ph;
			private int d_fi;
			private int d_co;
			private int d_en;
			private int d_po;

			public EffectTimer( TeiravonMobile from, Mobile targ, int ph, int fi, int en, int co, int po, int runcount, int maxcount, DateTime end ) : base( end - DateTime.Now )
			{
				m_from = from;
				m_targ = targ;
				Priority = TimerPriority.OneSecond;
				RunCount = runcount;
				MaxCount = maxcount;
				d_ph = ph;
				d_fi = fi;
				d_en = en;
				d_co = co;
				d_po = po;
			}

			protected override void OnTick()
			{

				int damage = Utility.RandomMinMax(10,15 + (int)(m_from.PlayerLevel/4));
				m_from.MovingParticles( m_targ, 0x36E4, 5, 0, false, false, 3006, 4006, 0);
				m_from.PlaySound( 0x1E5 );
				SpellHelper.Damage(TimeSpan.Zero,m_targ, m_from, damage, d_ph, d_fi, d_co, d_po, d_en);

                if (!m_from.CanSee(m_targ) || !m_from.InLOS(m_targ))
                    RunCount = MaxCount;

				if (!m_targ.Alive)
					RunCount = MaxCount;
				
				if ( RunCount == MaxCount)
				{
					if (m_targ is TeiravonMobile)
					{
						TeiravonMobile pctarg = (TeiravonMobile)m_targ;
						pctarg.ShadowShotReady = false;
					}
				}
				if ( RunCount < MaxCount && m_from.Alive)
				{
					RunCount++;
					Timer m_EffectTimer = new EffectTimer( m_from, m_targ, d_ph, d_fi, d_en, d_co, d_po, RunCount, MaxCount, DateTime.Now + TimeSpan.FromSeconds( 0.25 ) );
					m_EffectTimer.Start();
				}
			}
		}
	}	
}

namespace Server.Items
{
	public class BookofElements : Item
	{
		[Constructable]
		public BookofElements() : base( 0x2259 )
		{
			Weight = 5.0;
			Name = "Book of Elements";
			Hue = 2229;
		}

		public BookofElements( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile player = (TeiravonMobile)from;

				if (player.IsMage() && !player.IsNecromancer())
				{
					player.SendGump( new BOEGump( player ) );
				}
				else
				{
					player.SendMessage("Only Elemental Mages can use this.");
				}
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}	

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}	
	}
}

namespace Server.Gumps
{
	public class BOEGump : Gump
	{
		TeiravonMobile mage;
		
		public BOEGump( TeiravonMobile from ) : base( 0, 0 )
		{
			mage = from;
			
			mage.CloseGump( typeof( MSGemGump ) );
	
			int page = 0;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			AddPage(0);
			AddImage(72, 91, 2200);

			AddPage(++page);
			
			AddHtml( 145, 135, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Book</basefont>", false, false );
			AddHtml( 155, 180, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Of</basefont>", false, false );
			AddHtml( 135, 225, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Elements</basefont>", false, false );

			AddHtml( 260, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Greetings to you</basefont>", false, false );
			AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">new member of the</basefont>", false, false );
			AddHtml( 260, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">learned. </basefont>", false, false );
			AddHtml( 260, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">With the lore</basefont>", false, false );
			AddHtml( 260, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">found in this tome, </basefont>", false, false );
			AddHtml( 260, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">your abilities in</basefont>", false, false );
			AddHtml( 260, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">binding the forces</basefont>", false, false );
			AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">of the elements will</basefont>", false, false );
			AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">be increased tenfold.</basefont>", false, false );

			AddButton(366, 95, 2206, 2206, 0, GumpButtonType.Page, page + 1);

			AddPage(++page);

			if (mage.IsGeomancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Stone Punch</basefont>", false, false );
			else if (mage.IsPyromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Burning Hands</basefont>", false, false );
			else if (mage.IsAeromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Shocking Grasp</basefont>", false, false );
			else 
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Icy Blow</basefont>", false, false );
			AddHtml( 105, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Usage: ]mgspa</basefont>", false, false );
			AddHtml( 105, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Description: </basefont>", false, false );
			AddHtml( 105, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\"></basefont>", false, false );
			AddHtml( 105, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">You must be</basefont>", false, false );
			AddHtml( 105, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">able to touch your</basefont>", false, false );
			AddHtml( 105, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">your target. The</basefont>", false, false );
			AddHtml( 105, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">lightest touch will </basefont>", false, false );
			AddHtml( 105, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">suffice.  Release</basefont>", false, false );

			AddHtml( 260, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">your arcane power,</basefont>", false, false );
			AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">and select your.</basefont>", false, false );
			AddHtml( 260, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">target. The</basefont>", false, false );
			AddHtml( 260, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">elemental force</basefont>", false, false );
			AddHtml( 260, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">you unleash will</basefont>", false, false );
			AddHtml( 260, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">strike your opponent</basefont>", false, false );
			AddHtml( 260, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">for a moderate</basefont>", false, false );
			AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">amount of damage.</basefont>", false, false );
			AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\"></basefont>", false, false );

			AddButton(366, 95, 2206, 2206, 0, GumpButtonType.Page, page + 1);
			AddButton(94, 99, 2205, 2205, 0, GumpButtonType.Page, page - 1);
			
			AddPage(++page);

			if (mage.IsGeomancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Sling Stones</basefont>", false, false );
			else if (mage.IsPyromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Firebolts</basefont>", false, false );
			else if (mage.IsAeromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Magic Arrows</basefont>", false, false );
			else 
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Hail Stones</basefont>", false, false );
			AddHtml( 105, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Usage: ]mgspb</basefont>", false, false );
			AddHtml( 105, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Description: </basefont>", false, false );
			AddHtml( 105, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\"></basefont>", false, false );
			AddHtml( 105, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">You call upon the</basefont>", false, false );
			AddHtml( 105, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">elemental power</basefont>", false, false );
			AddHtml( 105, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">to form tangible</basefont>", false, false );
			AddHtml( 105, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">missles to strike</basefont>", false, false );
			AddHtml( 105, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">your opponent from</basefont>", false, false );

			AddHtml( 260, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">a distance. Call</basefont>", false, false );
			AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">the power and target</basefont>", false, false );
			AddHtml( 260, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">your enemy. As you </basefont>", false, false );
			AddHtml( 260, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">grow in power, the</basefont>", false, false );
			AddHtml( 260, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">more missles you</basefont>", false, false );
			AddHtml( 260, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">will call into</basefont>", false, false );
			AddHtml( 260, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">being.</basefont>", false, false );
			AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\"></basefont>", false, false );
			AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\"></basefont>", false, false );

			AddButton(366, 95, 2206, 2206, 0, GumpButtonType.Page, page + 1);
			AddButton(94, 99, 2205, 2205, 0, GumpButtonType.Page, page - 1);
		
			AddPage(++page);

			if (mage.IsGeomancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Earth</basefont>", false, false );
			else if (mage.IsPyromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Fire</basefont>", false, false );
			else if (mage.IsAeromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Air</basefont>", false, false );
			else 
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Water</basefont>", false, false );
			AddHtml( 105, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Transformation</basefont>", false, false );
			AddHtml( 105, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Usage: ]mgspc</basefont>", false, false );
			AddHtml( 105, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Description: </basefont>", false, false );
			AddHtml( 105, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\"></basefont>", false, false );
			AddHtml( 105, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">You control and</basefont>", false, false );
			AddHtml( 105, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">surrender yourself</basefont>", false, false );
			AddHtml( 105, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">to the power of</basefont>", false, false );
			if (mage.IsGeomancer())
				AddHtml( 105, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Earth. </basefont>", false, false );
			else if (mage.IsPyromancer())
				AddHtml( 105, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Fire. </basefont>", false, false );
			else if (mage.IsAeromancer())
				AddHtml( 105, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Air. </basefont>", false, false );
			else 
				AddHtml( 105, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Water. </basefont>", false, false );

			AddHtml( 260, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">As you progress</basefont>", false, false );
			AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">in your art, you</basefont>", false, false );
			AddHtml( 260, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">may spend a </basefont>", false, false );
			AddHtml( 260, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">longer time in</basefont>", false, false );
			AddHtml( 260, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">this form.  You</basefont>", false, false );
			AddHtml( 260, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">will feel a</basefont>", false, false );
			AddHtml( 260, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">weakening shortly</basefont>", false, false );
			AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">before you lose </basefont>", false, false );
			AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">control.</basefont>", false, false );

			AddButton(366, 95, 2206, 2206, 0, GumpButtonType.Page, page + 1);
			AddButton(94, 99, 2205, 2205, 0, GumpButtonType.Page, page - 1);
			
			AddPage(++page);

			if (mage.IsGeomancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Earth Spikes</basefont>", false, false );
			else if (mage.IsPyromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">FireStorm</basefont>", false, false );
			else if (mage.IsAeromancer())
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Lightning Storm</basefont>", false, false );
			else 
				AddHtml( 105, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Hail Storm</basefont>", false, false );
			AddHtml( 105, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Usage: ]mgspd</basefont>", false, false );
			AddHtml( 105, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Description: </basefont>", false, false );
			AddHtml( 105, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\"></basefont>", false, false );
			AddHtml( 105, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Your harness</basefont>", false, false );
			AddHtml( 105, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">elemental power</basefont>", false, false );
			AddHtml( 105, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">to form an area</basefont>", false, false );
			AddHtml( 105, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">surronding you</basefont>", false, false );
			AddHtml( 105, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">with the deadly</basefont>", false, false );
			AddHtml( 260, 135, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">force of your</basefont>", false, false );
			if (mage.IsGeomancer())
				AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Earth powers.</basefont>", false, false );
			else if (mage.IsPyromancer())
				AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Fire powers.</basefont>", false, false );
			else if (mage.IsAeromancer())
				AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Air powers.</basefont>", false, false );
			else 
				AddHtml( 260, 150, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Water powers.</basefont>", false, false );
			AddHtml( 260, 165, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">Additionaly,</basefont>", false, false );
			AddHtml( 260, 180, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">creatures caught</basefont>", false, false );
			AddHtml( 260, 195, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">in the elemental</basefont>", false, false );
			AddHtml( 260, 210, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">effect will be</basefont>", false, false );
			AddHtml( 260, 225, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">victim of </basefont>", false, false );
			if (mage.IsGeomancer())
			{
				AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">a vicious </basefont>", false, false );
				AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">bleed attack.</basefont>", false, false );
			}
			else if (mage.IsPyromancer())
			{
				AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">a severe </basefont>", false, false );
				AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">stamina drain.</basefont>", false, false );
			}
			else if (mage.IsAeromancer())
			{
				AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">a massive</basefont>", false, false );
				AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">mana loss.</basefont>", false, false );
			}
			else 
			{
				AddHtml( 260, 240, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">a freezing</basefont>", false, false );
				AddHtml( 260, 255, 600, 20, "<basefont size=\"6\" color=\"#ffffff\">paralyzation.</basefont>", false, false );
			}

			AddButton(94, 99, 2205, 2205, 0, GumpButtonType.Page, page - 1);
		}
		
	}
}
