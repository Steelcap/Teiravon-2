using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Mobiles;


namespace Server.Gumps
{
	public class AlloyGump : Gump
	{
		private TeiravonMobile m_player;
		private int m_amt;
		private int m_itemnum;
		private Item[] m_step = new Item[7];
//		private	ArrayList m_step = new ArrayList();
		
		public enum Buttons
		{
			Close,
			Iron,
			DullCopper,
			ShadowIron,
			Copper,
			Bronze,
			Gold,
            Silver,
			Agapite,
			Verite,
			Valorite,
			Oak,
			Pine,
			Redwood,
			WhitePine,
			Ashwood,
			SilverBirch,
			Yew,
			BlackOak,
			SulfurousAsh,
			ZoogiFungus,
			FertileDirt,
			PigIron,
			NoxCrystal,
			Wool,
			Bone,
			Citrine,
			BlackPearl,
            Diamond,
			Decrease,
			Increase,
			Cancel,
			OK
		}
		
		public AlloyGump(TeiravonMobile player, int amt, int cnt, Item step1, Item step2, Item step3, Item step4, Item step5, Item step6): base( 0, 0 )
		{
			m_player = player;
			m_amt = amt;
			m_itemnum = cnt;
			
/*			m_step.Add(null);
			m_step.Add(step1);
			m_step.Add(step2);
			m_step.Add(step3);
			m_step.Add(step4);
			m_step.Add(step5);
			m_step.Add(step6);
*/

			m_step[1] = step1;
			m_step[2] = step2;
			m_step[3] = step3;
			m_step[4] = step4;
			m_step[5] = step5;
			m_step[6] = step6;

			string string1;
			string string2;
			string string3;
			string string4;
			string string5;
			string string6;
			
			if (m_step[1] == null)
				string1 = "Step 1";
			else 
			{
				Item tempitem = (Item)m_step[1];
				string1 = tempitem.GetType().Name;
			}
			
			if (m_step[2] == null)
				string2 = "Step 2";
			else 
			{
				Item tempitem = (Item)m_step[2];
				string2 = tempitem.GetType().Name;
			}

			if (m_step[3] == null)
				string3 = "Step 3";
			else 
			{
				Item tempitem = (Item)m_step[3];
				string3 = tempitem.GetType().Name;
			}

			if (m_step[4] == null)
				string4 = "Step 4";
			else 
			{
				Item tempitem = (Item)m_step[4];
				string4 = tempitem.GetType().Name;
			}

			if (m_step[5] == null)
				string5 = "Step 5";
			else 
			{
				Item tempitem = (Item)m_step[5];
				string5 = tempitem.GetType().Name;
			}

			if (m_step[6] == null)
				string6 = "Step 6";
			else 
			{
				Item tempitem = (Item)m_step[6];
				string6 = tempitem.GetType().Name;
			}


			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(16, 35, 500, 400, 2600);

			this.AddLabel(202, 55, 150, @"Alloy Creation Menu");
			this.AddLabel(66, 90, 150, @"Metals");
			this.AddLabel(220, 90, 150, @"Woods");
			this.AddLabel(370, 90, 150, @"Catalysts");

			this.AddHtml( 66, 109, 110, 18, @"Iron", (bool)false, (bool)false);
			this.AddHtml( 66, 127, 110, 18, @"Dull Copper", (bool)false, (bool)false);
			this.AddHtml( 66, 145, 110, 18, @"Shadow Iron", (bool)false, (bool)false);
			this.AddHtml( 66, 163, 110, 18, @"Copper", (bool)false, (bool)false);
			this.AddHtml( 66, 181, 110, 18, @"Bronze", (bool)false, (bool)false);
			this.AddHtml( 66, 199, 110, 18, @"Gold", (bool)false, (bool)false);
            this.AddHtml( 66, 217, 110, 18, @"Silver", (bool)false, (bool)false);
            this.AddHtml( 66, 235, 110, 18, @"Agapite", (bool)false, (bool)false);
            this.AddHtml( 66, 253, 110, 18, @"Verite", (bool)false, (bool)false);
			this.AddHtml( 66, 270, 110, 18, @"Valorite", (bool)false, (bool)false);

			this.AddHtml( 216, 109, 110, 18, @"Oak", (bool)false, (bool)false);
			this.AddHtml( 216, 127, 110, 18, @"Pine", (bool)false, (bool)false);
			this.AddHtml( 216, 145, 110, 18, @"Redwood", (bool)false, (bool)false);
			this.AddHtml( 216, 163, 110, 18, @"White Pine", (bool)false, (bool)false);
			this.AddHtml( 216, 181, 110, 18, @"Ashwood", (bool)false, (bool)false);
			this.AddHtml( 216, 199, 110, 18, @"Silver Birch", (bool)false, (bool)false);
			this.AddHtml( 216, 217, 110, 18, @"Yew", (bool)false, (bool)false);
			this.AddHtml( 216, 235, 110, 18, @"Black Oak", (bool)false, (bool)false);

			this.AddHtml( 366, 109, 110, 18, @"Sulfurous Ash", (bool)false, (bool)false);
			this.AddHtml( 366, 127, 110, 18, @"Zoogi Fungus", (bool)false, (bool)false);
			this.AddHtml( 366, 145, 110, 18, @"Fertile Dirt", (bool)false, (bool)false);
			this.AddHtml( 366, 163, 110, 18, @"Pig Iron", (bool)false, (bool)false);
			this.AddHtml( 366, 181, 110, 18, @"Nox Crystal", (bool)false, (bool)false);
			this.AddHtml( 366, 199, 110, 18, @"Wool", (bool)false, (bool)false);
			this.AddHtml( 366, 217, 110, 18, @"Bone", (bool)false, (bool)false);
			this.AddHtml( 366, 235, 110, 18, @"Citrine", (bool)false, (bool)false);
			this.AddHtml( 366, 253, 110, 18, @"Black Pearl", (bool)false, (bool)false);
            this.AddHtml( 366, 270, 110, 18, @"Diamond", (bool)false, (bool)false);
			
			if (m_itemnum < 7)
			{
				this.AddButton(50, 113, 216, 216, (int)Buttons.Iron, GumpButtonType.Reply, 0);
				this.AddButton(50, 131, 216, 6501, (int)Buttons.DullCopper, GumpButtonType.Reply, 0);
				this.AddButton(50, 149, 216, 6501, (int)Buttons.ShadowIron, GumpButtonType.Reply, 0);
				this.AddButton(50, 167, 216, 6501, (int)Buttons.Copper, GumpButtonType.Reply, 0);
				this.AddButton(50, 185, 216, 6501, (int)Buttons.Bronze, GumpButtonType.Reply, 0);
				this.AddButton(50, 203, 216, 6501, (int)Buttons.Gold, GumpButtonType.Reply, 0);
                this.AddButton(50, 221, 216, 6501, (int)Buttons.Silver, GumpButtonType.Reply, 0);
				this.AddButton(50, 239, 216, 6501, (int)Buttons.Agapite, GumpButtonType.Reply, 0);
				this.AddButton(50, 257, 216, 6501, (int)Buttons.Verite, GumpButtonType.Reply, 0);
				this.AddButton(50, 274, 216, 6501, (int)Buttons.Valorite, GumpButtonType.Reply, 0);

				this.AddButton(200, 113, 216, 216, (int)Buttons.Oak, GumpButtonType.Reply, 0);
				this.AddButton(200, 131, 216, 216, (int)Buttons.Pine, GumpButtonType.Reply, 0);
				this.AddButton(200, 149, 216, 216, (int)Buttons.Redwood, GumpButtonType.Reply, 0);
				this.AddButton(200, 167, 216, 216, (int)Buttons.WhitePine, GumpButtonType.Reply, 0);
				this.AddButton(200, 185, 216, 216, (int)Buttons.Ashwood, GumpButtonType.Reply, 0);
				this.AddButton(200, 203, 216, 216, (int)Buttons.SilverBirch, GumpButtonType.Reply, 0);
				this.AddButton(200, 221, 216, 216, (int)Buttons.Yew, GumpButtonType.Reply, 0);
				this.AddButton(200, 239, 216, 216, (int)Buttons.BlackOak, GumpButtonType.Reply, 0);
			
				this.AddButton(350, 113, 216, 216, (int)Buttons.SulfurousAsh, GumpButtonType.Reply, 0);
				this.AddButton(350, 131, 216, 216, (int)Buttons.ZoogiFungus, GumpButtonType.Reply, 0);
				this.AddButton(350, 149, 216, 216, (int)Buttons.FertileDirt, GumpButtonType.Reply, 0);
				this.AddButton(350, 167, 216, 216, (int)Buttons.PigIron, GumpButtonType.Reply, 0);
				this.AddButton(350, 185, 216, 216, (int)Buttons.NoxCrystal, GumpButtonType.Reply, 0);
				this.AddButton(350, 203, 216, 216, (int)Buttons.Wool, GumpButtonType.Reply, 0);
				this.AddButton(350, 221, 216, 216, (int)Buttons.Bone, GumpButtonType.Reply, 0);
				this.AddButton(350, 239, 216, 216, (int)Buttons.Citrine, GumpButtonType.Reply, 0);
				this.AddButton(350, 257, 216, 216, (int)Buttons.BlackPearl, GumpButtonType.Reply, 0);
                this.AddButton(350, 274, 216, 216, (int)Buttons.Diamond, GumpButtonType.Reply, 0);
			}
			
			this.AddHtml( 50, 290, 126, 20, string1, (bool)false, (bool)false);
			this.AddHtml( 200, 290, 126, 20, string2, (bool)false, (bool)false);
			this.AddHtml( 350, 290, 126, 20, string3, (bool)false, (bool)false);
			this.AddHtml( 50, 315, 126, 20, string4, (bool)false, (bool)false);
			this.AddHtml( 200, 315, 126, 20, string5, (bool)false, (bool)false);
			this.AddHtml( 350, 315, 126, 20, string6, (bool)false, (bool)false);
			
			this.AddLabel(241, 355, 0, @"Amount");
			this.AddHtml( 241, 375, 48, 20, m_amt.ToString(), (bool)false, (bool)false);

			this.AddButton(207, 375, 5223, 5230, (int)Buttons.Decrease, GumpButtonType.Reply, 0);
			this.AddButton(301, 375, 5224, 5230, (int)Buttons.Increase, GumpButtonType.Reply, 0);
			this.AddButton(383, 375, 247, 248, (int)Buttons.OK, GumpButtonType.Reply, 0);
			this.AddButton(79, 375, 241, 242, (int)Buttons.Cancel, GumpButtonType.Reply, 0);

		}
		

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			switch ( info.ButtonID )
			{
				case 0:
				{
					break;
				}
					
				case (int)Buttons.Iron:
				{
					m_step[m_itemnum] = new IronIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.DullCopper:
				{
					m_step[m_itemnum] = new DullCopperIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.ShadowIron:
				{
					m_step[m_itemnum] = new ShadowIronIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Copper:
				{
					m_step[m_itemnum] = new CopperIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Bronze:
				{
					m_step[m_itemnum] = new BronzeIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Gold:
				{
					m_step[m_itemnum] = new GoldIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Agapite:
				{
					m_step[m_itemnum] = new AgapiteIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Verite:
				{
					m_step[m_itemnum] = new VeriteIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Valorite:
				{
					m_step[m_itemnum] = new ValoriteIngot();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Oak:
				{
					m_step[m_itemnum] = new Log();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Pine:
				{
					m_step[m_itemnum] = new PineLog();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Redwood:
				{
					m_step[m_itemnum] = new RedwoodLog();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.WhitePine:
				{
					m_step[m_itemnum] = new WhitePineLog();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Ashwood:
				{
					m_step[m_itemnum] = new AshwoodLog();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.SilverBirch:
				{
					m_step[m_itemnum] = new SilverBirchLog();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

                case (int)Buttons.Silver:
                {
                    m_step[m_itemnum] = new SilverIngot();
                    m_itemnum += 1;
                    m_player.SendGump(new AlloyGump(m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6]));
                    break;
                }

				case (int)Buttons.Yew:
				{
					m_step[m_itemnum] = new YewLog();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.BlackOak:
				{
					m_step[m_itemnum] = new BlackOakLog();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.SulfurousAsh:
				{
					m_step[m_itemnum] = new SulfurousAsh();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.ZoogiFungus:
				{
					m_step[m_itemnum] = new ZoogiFungus();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.FertileDirt:
				{
					m_step[m_itemnum] = new FertileDirt();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.PigIron:
				{
					m_step[m_itemnum] = new PigIron();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.NoxCrystal:
				{
					m_step[m_itemnum] = new NoxCrystal();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

                case (int)Buttons.Diamond:
                {
                    m_step[m_itemnum] = new Diamond();
                    m_itemnum += 1;
                    m_player.SendGump(new AlloyGump(m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6]));
                    break;
                }

				case (int)Buttons.Wool:
				{
					m_step[m_itemnum] = new Wool();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Bone:
				{
					m_step[m_itemnum] = new Bone();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Citrine:
				{
					m_step[m_itemnum] = new Citrine();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.BlackPearl:
				{
					m_step[m_itemnum] = new BlackPearl();
					m_itemnum += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}

				case (int)Buttons.Decrease:
				{
					if (m_amt > 1)
						m_amt -= 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}
				
				case (int)Buttons.Increase:
				{
					m_amt += 1;
					m_player.SendGump( new AlloyGump( m_player, m_amt, m_itemnum, m_step[1], m_step[2], m_step[3], m_step[4], m_step[5], m_step[6] ) );
					break;
				}
				
				case (int)Buttons.Cancel:
				{
					m_player.SendGump( new AlloyGump( m_player, 1, 1, null, null, null, null, null, null ) );
					break;
				}
					
				case (int)Buttons.OK:
				{
						//check for and consume ingredients here
						Container pack = m_player.Backpack;
						
						if (m_step[6] == null)
						{
							m_player.SendMessage("You must pick 6 steps");
							m_player.SendGump( new AlloyGump( m_player, 1, 1, null, null, null, null, null, null ) );
							break;
						}

						bool allres = true;

						for (int i = 1; i < 7; i++)
						{
							Item itm = (Item)m_step[i];
							int packamt = m_player.Backpack.GetAmount( itm.GetType());
							if ( packamt < m_amt)
							{
								m_player.SendMessage("You do not have enough {0} to attempt this", itm.GetType().Name);
								allres = false;
							}
						}
						
						if (allres)
						{
							for (int i = 1; i < 7; i++)
							{
								Item itm = (Item)m_step[i];
								m_player.Backpack.ConsumeTotal( m_step[i].GetType(), m_amt );
								m_player.SendMessage("Step {0} consumes {1} {2}", i.ToString(), m_amt.ToString(), itm.GetType().Name);
							}
						}
						else
						{
							m_player.SendMessage("Please try again when you have all of the needed resources");
							return;
						}
						
						
						if (m_step[1] is AgapiteIngot && m_step[2] is BlackOakLog && m_step[3] is AgapiteIngot && m_step[4] is ShadowIronIngot && m_step[5] is Bone && m_step[6] is AgapiteIngot)
						{
							if (m_player.Backpack != null)
								m_player.AddToBackpack(new BloodrockIngot(m_amt));
							break;
						}

						if (m_step[1] is ValoriteIngot && m_step[2] is WhitePineLog && m_step[3] is ValoriteIngot && m_step[4] is CopperIngot && m_step[5] is FertileDirt && m_step[6] is SilverBirchLog)
						{
							if (m_player.Backpack != null)
								m_player.AddToBackpack(new SteelIngot(m_amt));
							break;
						}
						
						if (m_step[1] is ShadowIronIngot && m_step[2] is AshwoodLog && m_step[3] is VeriteIngot && m_step[4] is ShadowIronIngot && m_step[5] is NoxCrystal && m_step[6] is ShadowIronIngot)
						{
							if (m_player.Backpack != null)
								m_player.AddToBackpack(new AdamantiteIngot(m_amt));
							break;
						}

						if (m_step[1] is IronIngot && m_step[2] is SilverBirchLog && m_step[3] is ValoriteIngot && m_step[4] is IronIngot && m_step[5] is Citrine && m_step[6] is SilverBirchLog)
						{
							if (m_player.Backpack != null)
								m_player.AddToBackpack(new IthilmarIngot(m_amt));
							break;
						}

                        if (m_step[1] is SilverIngot && m_step[2] is GoldIngot && m_step[3] is Diamond && m_step[4] is SilverIngot && m_step[5] is GoldIngot && m_step[6] is SilverIngot)
                        {
                            if (m_player.Backpack != null)
                                m_player.AddToBackpack(new ElectrumIngot(m_amt));
                            break;
                        }

                        if (m_step[1] is PigIron && m_player.IsGoblin())
                        {
                            if (m_player.Backpack != null)
								m_player.AddToBackpack(new SkazzIngot(m_amt));
							break;
                        }

						m_player.SendMessage("You fail to create a new alloy and your materials are lost");
						m_player.SendGump( new AlloyGump( m_player, 1, 1, null, null, null, null, null, null ) );
						break;
				}
					
			}
		}
		
	}
	
}
