using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xe80, 0x9a8 )]
	public class GraniteBox : Item
	{
		
		private int iron_amt;
		private int dull_amt;
		private int shadow_amt;
		private int copper_amt;
		private int bronze_amt;
		private int gold_amt;
		private int agapite_amt;
		private int verite_amt;
		private int valorite_amt;
		private int mithril_amt;

		[CommandProperty( AccessLevel.GameMaster )]
		public int IronAmt
		{
			get{ return iron_amt; }
			set{ iron_amt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DullAmt
		{
			get{ return dull_amt; }
			set{ dull_amt = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int ShadowAmt
		{
			get{ return shadow_amt; }
			set{ shadow_amt = value;}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int CopperAmt
		{
			get{ return copper_amt; }
			set{ copper_amt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int BronzeAmt
		{
			get{ return bronze_amt; }
			set{ bronze_amt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int GoldAmt
		{
			get{ return gold_amt; }
			set{ gold_amt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int AgapiteAmt
		{
			get{ return agapite_amt; }
			set{ agapite_amt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int VeriteAmt
		{
			get{ return verite_amt; }
			set{ verite_amt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ValoriteAmt
		{
			get{ return valorite_amt; }
			set{ valorite_amt = value;}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MithrilAmt
		{
			get{ return mithril_amt; }
			set{ mithril_amt = value;}
		}

		[Constructable]
		public GraniteBox() : this( 1 )
		{
		}

		[Constructable]
		public GraniteBox( int amount ) : base( 0xe80 )
		{
			Name = "Granite Storage Box";
			Weight = 100.0;
			Hue = 938;
			IronAmt = 0;
			DullAmt = 0;
			ShadowAmt = 0;
			CopperAmt = 0;
			BronzeAmt = 0;
			GoldAmt = 0;
			AgapiteAmt = 0;
			VeriteAmt = 0;
			ValoriteAmt = 0;
			MithrilAmt = 0;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile player = (TeiravonMobile)from;
				from.CloseGump(typeof(GraniteGump));
				from.SendGump( new GraniteGump(player, this, IronAmt, DullAmt, ShadowAmt, CopperAmt, BronzeAmt, GoldAmt, AgapiteAmt, VeriteAmt, ValoriteAmt, MithrilAmt ));
			}
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			TeiravonMobile m_player = (TeiravonMobile)from;
			
			if ( item is BaseGranite )
			{
				BaseGranite m_granite = (BaseGranite)item;
				switch(m_granite.Resource)
				{
					case CraftResource.Iron:
						iron_amt += item.Amount; break;
					case CraftResource.DullCopper:
						dull_amt += item.Amount; break;
					case CraftResource.ShadowIron:
						shadow_amt += item.Amount; break;
					case CraftResource.Copper:
						copper_amt += item.Amount; break;
					case CraftResource.Bronze:
						bronze_amt += item.Amount; break;
					case CraftResource.Gold:
						gold_amt += item.Amount; break;
					case CraftResource.Agapite:
						agapite_amt += item.Amount; break;
					case CraftResource.Verite:
						verite_amt += item.Amount; break;
					case CraftResource.Valorite:
						valorite_amt += item.Amount; break;
					case CraftResource.Mithril:
						mithril_amt += item.Amount; break;
				}
				
				m_player.SendMessage("You put the granite into the box");
				m_player.CloseGump(typeof(GraniteGump));
				m_player.SendGump( new GraniteGump(m_player, this, this.IronAmt, this.DullAmt, this.ShadowAmt, this.CopperAmt, this.BronzeAmt, this.GoldAmt, this.AgapiteAmt, this.VeriteAmt, this.ValoriteAmt, this.MithrilAmt));
				m_granite.Delete();
				return true;
			}
		
			from.SendMessage( "That box is for granite only." );
			return false;
		}

		public GraniteBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) iron_amt);
			writer.Write( (int) dull_amt);
			writer.Write( (int) shadow_amt);
			writer.Write( (int) copper_amt);
			writer.Write( (int) bronze_amt);
			writer.Write( (int) gold_amt);
			writer.Write( (int) agapite_amt);
			writer.Write( (int) verite_amt);
			writer.Write( (int) valorite_amt);
			writer.Write( (int) mithril_amt);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			iron_amt = reader.ReadInt();
			dull_amt = reader.ReadInt();
			shadow_amt = reader.ReadInt();
			copper_amt = reader.ReadInt();
			bronze_amt = reader.ReadInt();
			gold_amt = reader.ReadInt();
			agapite_amt = reader.ReadInt();
			verite_amt = reader.ReadInt();
			valorite_amt = reader.ReadInt();
			mithril_amt = reader.ReadInt();
		}
	}
}

namespace Server.Gumps
{
	public class GraniteGump : Gump
	{
		TeiravonMobile m_player;
		GraniteBox m_box;
		int m_iron;
		int m_dull;
		int m_shadow;
		int m_copper;
		int m_bronze;
		int m_gold;
		int m_agapite;
		int m_verite;
		int m_valorite;
		int m_mithril;
		
		public enum Buttons
		{
			ZeroButton,
			IronButton,
			DCButton,
			ShadowButton,
			CopperButton,
			BronzeButton,
			GoldButton,
			AgapiteButton,
			VeriteButton,
			ValoriteButton,
			MithrilButton,
		}

		public GraniteGump(TeiravonMobile player, GraniteBox gbox, int amt_iron, int amt_dull, int amt_shadow, int amt_copper, int amt_bronze, int amt_gold, int amt_agapite, int amt_verite, int amt_valorite, int amt_mithril ): base( 0, 0 )
		{
			m_player = player;
			m_box = gbox;
			m_iron = amt_iron;
			m_dull = amt_dull;
			m_shadow = amt_shadow;
			m_copper = amt_copper;
			m_bronze = amt_bronze;
			m_gold = amt_gold;
			m_agapite = amt_agapite;
			m_verite = amt_verite;
			m_valorite = amt_valorite;
			m_mithril = amt_mithril;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(103, 82, 341, 360, 2600);
			this.AddLabel(215, 123, 150, @"Granite Inventory");
			this.AddHtml( 195, 175, 100, 18, @"Iron", (bool)false, (bool)false);
			this.AddHtml( 195, 193, 100, 18, @"Dull Copper", (bool)false, (bool)false);
			this.AddHtml( 195, 211, 100, 18, @"Shadow Iron", (bool)false, (bool)false);
			this.AddHtml( 195, 229, 100, 18, @"Copper", (bool)false, (bool)false);
			this.AddHtml( 195, 247, 100, 18, @"Bronze", (bool)false, (bool)false);
			this.AddHtml( 195, 265, 100, 18, @"Gold", (bool)false, (bool)false);
			this.AddHtml( 195, 283, 100, 18, @"Agapite", (bool)false, (bool)false);
			this.AddHtml( 195, 301, 100, 18, @"Verite", (bool)false, (bool)false);
			this.AddHtml( 195, 319, 100, 18, @"Valorite", (bool)false, (bool)false);
			this.AddHtml( 195, 337, 100, 18, @"Mithril", (bool)false, (bool)false);
			if (amt_iron > 0)
				this.AddButton(179, 179, 216, 216, (int)Buttons.IronButton, GumpButtonType.Reply, 0);
			if (amt_dull > 0)
				this.AddButton(179, 197, 216, 6501, (int)Buttons.DCButton, GumpButtonType.Reply, 0);
			if (amt_shadow > 0)
				this.AddButton(179, 215, 216, 6501, (int)Buttons.ShadowButton, GumpButtonType.Reply, 0);
			if (amt_copper > 0)
				this.AddButton(179, 233, 216, 6501, (int)Buttons.CopperButton, GumpButtonType.Reply, 0);
			if (amt_bronze > 0)
				this.AddButton(179, 251, 216, 6501, (int)Buttons.BronzeButton, GumpButtonType.Reply, 0);
			if (amt_gold > 0)
				this.AddButton(179, 269, 216, 6501, (int)Buttons.GoldButton, GumpButtonType.Reply, 0);
			if (amt_agapite > 0)
				this.AddButton(179, 287, 216, 6501, (int)Buttons.AgapiteButton, GumpButtonType.Reply, 0);
			if (amt_verite > 0)
				this.AddButton(179, 305, 216, 6501, (int)Buttons.VeriteButton, GumpButtonType.Reply, 0);
			if (amt_valorite > 0)
				this.AddButton(179, 323, 216, 6501, (int)Buttons.ValoriteButton, GumpButtonType.Reply, 0);
			if (amt_mithril > 0)
				this.AddButton(179, 341, 216, 6501, (int)Buttons.MithrilButton, GumpButtonType.Reply, 0);
			this.AddHtml(300, 175, 50, 18, amt_iron.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 193, 50, 18, amt_dull.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 211, 50, 18, amt_shadow.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 229, 50, 18, amt_copper.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 247, 50, 18, amt_bronze.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 265, 50, 18, amt_gold.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 283, 50, 18, amt_agapite.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 301, 50, 18, amt_verite.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 319, 50, 18, amt_valorite.ToString(), (bool)false, (bool)false);
			this.AddHtml(300, 337, 50, 18, amt_mithril.ToString(), (bool)false, (bool)false);
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			switch ( info.ButtonID )
			{
				case 0:
				{
					break;
				}
					
				case (int)Buttons.IronButton:
				{
					m_player.AddToBackpack(new Granite());
					m_box.IronAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}
				
				case (int)Buttons.DCButton:
				{
					m_player.AddToBackpack(new DullCopperGranite());
					m_box.DullAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}
			
				case (int)Buttons.ShadowButton:
				{
					m_player.AddToBackpack(new ShadowIronGranite());
					m_box.ShadowAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}

				case (int)Buttons.CopperButton:
				{
					m_player.AddToBackpack(new CopperGranite());
					m_box.CopperAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}

				case (int)Buttons.BronzeButton:
				{
					m_player.AddToBackpack(new BronzeGranite());
					m_box.BronzeAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}

				case (int)Buttons.GoldButton:
				{
					m_player.AddToBackpack(new GoldGranite());
					m_box.GoldAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}
			
				case (int)Buttons.AgapiteButton:
				{
					m_player.AddToBackpack(new AgapiteGranite());
					m_box.AgapiteAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}

				case (int)Buttons.VeriteButton:
				{
					m_player.AddToBackpack(new VeriteGranite());
					m_box.VeriteAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}

				case (int)Buttons.ValoriteButton:
				{
					m_player.AddToBackpack(new ValoriteGranite());
					m_box.ValoriteAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}
			
				case (int)Buttons.MithrilButton:
				{
					m_player.AddToBackpack(new MithrilGranite());
					m_box.MithrilAmt -= 1;
					m_player.SendGump( new GraniteGump(m_player, m_box, m_box.IronAmt, m_box.DullAmt, m_box.ShadowAmt, m_box.CopperAmt, m_box.BronzeAmt, m_box.GoldAmt, m_box.AgapiteAmt, m_box.VeriteAmt, m_box.ValoriteAmt, m_box.MithrilAmt));
					break;
				}
			}
		}
		

	}
}
