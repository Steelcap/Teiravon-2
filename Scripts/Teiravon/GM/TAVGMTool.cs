using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.HuePickers;
using System.Reflection;
using System.Collections;
using Server.Mobiles;
using Server.Items;

namespace Server.Gumps
{
	public class GMToolGump : Gump
	{
		Mobile m_Player;
		
		public GMToolGump( Mobile from ) : base( 0, 0 )
		{
			m_Player = (Mobile)from;
			m_Player.CloseGump( typeof( GMToolGump ) );
			
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			
			AddPage(0);
			
			AddImageTiled(262, 136, 161, 142, 3504);
			AddImageTiled(236, 136, 28, 142, 3503);
			AddImageTiled(262, 111, 161, 25, 3501);
			AddImageTiled(408, 136, 24, 142, 3505);
			AddImageTiled(262, 273, 161, 25, 3507);
			AddImage(408, 273, 3508);
			AddImage(236, 273, 3506);
			AddImage(236, 110, 3500);
			AddImage(408, 111, 3502);
			AddLabel(278, 134, 100, @"Game Master Tool");
			AddLabel(270, 215, 500, @"Hue picker");
			AddLabel(270, 165, 500, @"Add armours");
			AddLabel(270, 190, 500, @"Set resources");
			//AddLabel(270, 240, 500, @"Quick edit stats");
			AddButton(385, 168, 1209, 1210, (int)Buttons.Armor, GumpButtonType.Reply, 0);
			AddButton(385, 194, 1209, 1210, (int)Buttons.Resource, GumpButtonType.Reply, 0);
			AddButton(385, 219, 1209, 1210, (int)Buttons.Hue, GumpButtonType.Reply, 0);
			//AddButton(385, 244, 1209, 1210, (int)Buttons.Stats, GumpButtonType.Reply, 0);
		}
		
		public enum Buttons
		{
			ExitMenu,
			Armor,
			Resource,
			Hue
			//Stats
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			switch ( info.ButtonID )
			{
				case (int)Buttons.Armor:
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Resource:
									
									m_Player.SendGump( new SetResourceGump( m_Player ) );
									break;
				case (int)Buttons.Hue:
									m_Player.Target = new Kullipicker();
									m_Player.SendMessage( "What do you want to color?" );
									m_Player.SendGump( new GMToolGump( m_Player ) );
									break;
				//case (int)Buttons.Stats:
									//m_Player.SendGump( new SetStatsGump( m_Player ) );
									//break;
			}
		}
		
		private class Kullipicker : Target
		{
			public Kullipicker() : base( -1, false, TargetFlags.None )
			{
			}
			
			protected override void OnTarget( Mobile from, object targ )
			{
			
				if ( targ != null )
				{
					PropertyInfo[] props = (targ.GetType()).GetProperties( BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public );
					PropertyInfo m_Kulli = null;
	
					for ( int i = 0; i < props.Length; i++ )
					{
						if ( props[i].Name == "Hue" )
							m_Kulli = props[i];
						
					}
				
					if ( m_Kulli != null )
						from.SendHuePicker( new InternalPicker( m_Kulli, from, targ ) );
					else
						from.SendMessage( "You cannot set a hue on that." );
				}
				else
					from.SendMessage( "Invalid target." );
					
			}
		}
			
		
		private class InternalPicker : HuePicker
		{
			private Mobile m_Mobile;
			private object m_Object;
			private PropertyInfo m_Property;

			public InternalPicker( PropertyInfo prop, Mobile mobile, object o ) : base( ((IHued)o).HuedItemID )
			{
				m_Mobile = mobile;
				m_Object = o;
				m_Property = prop;
			}

			public override void OnResponse( int hue )
			{
				try
				{
					m_Property.SetValue( m_Object, hue, null );
					m_Mobile.SendMessage( "Hue'd!" );
				}
				catch
				{
					m_Mobile.SendMessage( "An error ocurred while setting the hue. The hue may not have changed." );
				}
			}
		}
	}
}

namespace Server.Gumps
{
	public class ArmorAddingGump : Gump
	{
		Mobile m_Player;

		public ArmorAddingGump( Mobile from ): base( 0, 0 )
		{
			m_Player = (Mobile)from;
			m_Player.CloseGump( typeof( ArmorAddingGump ) );
			
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			
			AddPage(0);
			
			AddImageTiled(130, 148, 390, 350, 2624);
			AddImageTiled(130, 122, 390, 27, 3501);
			AddImageTiled(130, 498, 390, 25, 3507);
			AddImageTiled(104, 148, 27, 350, 3503);
			AddImageTiled(520, 148, 25, 350, 3505);
			AddAlphaRegion(130, 148, 390, 350);
			
			AddImage(104, 122, 3500);
			AddImage(520, 122, 3502);
			AddImage(104, 498, 3506);
			AddImage(520, 498, 3508);
			
			AddLabel(175, 165, 100, @"Suits of armor");			
			AddLabel(150, 195, 500, @"Ringmail");
			AddLabel(150, 240, 500, @"Chainmail");
			AddLabel(150, 285, 500, @"Platemail");
			AddLabel(250, 195, 500, @"Leather");
			AddLabel(250, 240, 500, @"Studded");
			AddLabel(250, 285, 500, @"Bone");
			
			AddLabel(364, 165, 100, @"Additional helms");
			AddLabel(351, 330, 500, @"Norse");
			AddLabel(350, 195, 500, @"Close");
			AddLabel(350, 240, 500, @"Bascinet");
			AddLabel(350, 285, 500, @"Helmet");
			AddLabel(450, 195, 500, @"Dragon");
			AddLabel(450, 240, 500, @"Orc");
			AddLabel(450, 285, 500, @"Daemon");

			AddLabel(150, 370, 100, @"Shields");			
			AddLabel(150, 400, 500, @"Bronze");
			AddLabel(360, 400, 500, @"Wooden");
			AddLabel(150, 445, 500, @"Buckler");
			AddLabel(290, 400, 500, @"Heater");
			AddLabel(220, 400, 500, @"Tear Kite");
			AddLabel(440, 400, 500, @"Kite");
			AddLabel(220, 445, 500, @"Metal");
			AddLabel(290, 445, 500, @"Order");
			AddLabel(360, 445, 500, @"Chaos");
			
			AddButton(152, 425, 1209, 1210, (int)Buttons.Bronze, GumpButtonType.Reply, 0);
			AddButton(352, 220, 1209, 1210, (int)Buttons.Close, GumpButtonType.Reply, 0);
			AddButton(252, 220, 1209, 1210, (int)Buttons.Leather, GumpButtonType.Reply, 0);
			AddButton(252, 265, 1209, 1210, (int)Buttons.Studded, GumpButtonType.Reply, 0);
			AddButton(252, 310, 1209, 1210, (int)Buttons.Bone, GumpButtonType.Reply, 0);
			AddButton(152, 310, 1209, 1210, (int)Buttons.Platemail, GumpButtonType.Reply, 0);
			AddButton(152, 265, 1209, 1210, (int)Buttons.Chainmail, GumpButtonType.Reply, 0);
			AddButton(152, 220, 1209, 1210, (int)Buttons.Ringmail, GumpButtonType.Reply, 0);

			AddButton(352, 265, 1209, 1210, (int)Buttons.Bascinet, GumpButtonType.Reply, 0);
			AddButton(352, 310, 1209, 1210, (int)Buttons.Helmet, GumpButtonType.Reply, 0);
			AddButton(352, 355, 1209, 1210, (int)Buttons.Norse, GumpButtonType.Reply, 0);
			AddButton(452, 220, 1209, 1210, (int)Buttons.Dragon, GumpButtonType.Reply, 0);
			AddButton(452, 265, 1209, 1210, (int)Buttons.Orc, GumpButtonType.Reply, 0);
			AddButton(452, 310, 1209, 1210, (int)Buttons.Daemon, GumpButtonType.Reply, 0);
			AddButton(362, 425, 1209, 1210, (int)Buttons.Wooden, GumpButtonType.Reply, 0);
			AddButton(152, 470, 1209, 1210, (int)Buttons.Buckler, GumpButtonType.Reply, 0);
			AddButton(292, 425, 1209, 1210, (int)Buttons.Heater, GumpButtonType.Reply, 0);
			AddButton(222, 425, 1209, 1210, (int)Buttons.TearKite, GumpButtonType.Reply, 0);
			AddButton(442, 425, 1209, 1210, (int)Buttons.Kite, GumpButtonType.Reply, 0);
			AddButton(222, 470, 1209, 1210, (int)Buttons.Metal, GumpButtonType.Reply, 0);
			AddButton(292, 470, 1209, 1210, (int)Buttons.Order, GumpButtonType.Reply, 0);
			AddButton(362, 470, 1209, 1210, (int)Buttons.Chaos, GumpButtonType.Reply, 0);
			
			AddButton(490, 148, 4018, 4017, (int)Buttons.ExitMenu, GumpButtonType.Reply, 0);
		}
		
		public enum Buttons
		{
			ExitMenu,
			Bronze,
			Close,
			Leather,
			Studded,
			Bone,
			Platemail,
			Chainmail,
			Ringmail,
			Bascinet,
			Helmet,
			Norse,
			Dragon,
			Orc,
			Daemon,
			Wooden,
			Buckler,
			Heater,
			TearKite,
			Kite,
			Metal,
			Order,
			Chaos
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Container pack = m_Player.Backpack;

			if (pack == null)
			{
				m_Player.SendMessage( "You don't have a backpack." );
				return;
			}



			switch ( info.ButtonID )
			{

				case (int)Buttons.ExitMenu:
									m_Player.CloseGump( typeof( ArmorAddingGump ) );
									m_Player.SendGump( new GMToolGump( m_Player ) );
									break;
					default:
									break;
				
				case (int)Buttons.Ringmail:

									m_Player.AddToBackpack( new BagOfRingmail() );
									m_Player.SendMessage( "The bag with armor is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Chainmail:

									m_Player.AddToBackpack( new BagOfChainmail() );
									m_Player.SendMessage( "The bag with armor is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Platemail:

									m_Player.AddToBackpack( new BagOfPlatemail() );
									m_Player.SendMessage( "The bag with armor is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Leather:

									m_Player.AddToBackpack( new BagOfLeatherArmor() );
									m_Player.SendMessage( "The bag with armor is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Studded:

									m_Player.AddToBackpack( new BagOfStuddedArmor() );
									m_Player.SendMessage( "The bag with armor is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Bone:

									m_Player.AddToBackpack( new BagOfBoneArmor() );
									m_Player.SendMessage( "The bag with armor is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Norse:

									m_Player.AddToBackpack( new NorseHelm() );
									m_Player.SendMessage( "The helmet is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Bascinet:

									m_Player.AddToBackpack( new Bascinet() );
									m_Player.SendMessage( "The helmet is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Orc:

									m_Player.AddToBackpack( new OrcHelm() );
									m_Player.SendMessage( "The helmet is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;									
				case (int)Buttons.Helmet:

									m_Player.AddToBackpack( new Helmet() );
									m_Player.SendMessage( "The helmet is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Dragon:

									m_Player.AddToBackpack( new DragonHelm() );
									m_Player.SendMessage( "The helmet is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Close:

									m_Player.AddToBackpack( new CloseHelm() );
									m_Player.SendMessage( "The helmet is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Daemon:

									m_Player.AddToBackpack( new DaemonHelm() );
									m_Player.SendMessage( "The helmet is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;
				case (int)Buttons.Bronze:

									m_Player.AddToBackpack( new BronzeShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;									
				case (int)Buttons.Heater:

									m_Player.AddToBackpack( new HeaterShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;	
				case (int)Buttons.Buckler:

									m_Player.AddToBackpack( new Buckler() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;	
				case (int)Buttons.Kite:

									m_Player.AddToBackpack( new MetalKiteShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;	
				case (int)Buttons.TearKite:

									m_Player.AddToBackpack( new WoodenKiteShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;	
				case (int)Buttons.Metal:

									m_Player.AddToBackpack( new MetalShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;										
				case (int)Buttons.Wooden:

									m_Player.AddToBackpack( new WoodenShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;										
				case (int)Buttons.Order:

									m_Player.AddToBackpack( new OrderShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;										
				case (int)Buttons.Chaos:

									m_Player.AddToBackpack( new ChaosShield() );
									m_Player.SendMessage( "The shield is now in your back pack." );
									m_Player.SendGump( new ArmorAddingGump( m_Player ) );
									break;	
									
			}
		}
	}
}

namespace Server.Gumps
{
	public class SetResourceGump : Gump
	{
		Mobile m_Player;
		
		public SetResourceGump( Mobile from ) : base( 0, 0 )
		{
			m_Player = (Mobile)from;
			m_Player.CloseGump( typeof( SetResourceGump ) );
			
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			
			AddPage(0);
			
			AddImageTiled(108, 30, 272, 235, 2624);
			AddAlphaRegion(109, 28, 270, 235);
			AddImageTiled(87, 30, 26, 230, 3503);
			AddImageTiled(108, 6, 260, 24, 3501);
			AddImageTiled(366, 30, 24, 230, 3505);
			AddImageTiled(108, 260, 260, 24, 3507);
			
			AddImage(366, 260, 3508);
			AddImage(87, 260, 3506);
			AddImage(87, 6, 3500);
			AddImage(366, 6, 3502);

			AddLabel(135, 45, 100, @"Weapons/Armor");
			AddLabel(135, 65, 500, @"Iron");
			AddLabel(290, 65, 500, @"Shadow");
			AddLabel(210, 65, 500, @"Dull Copper");
			AddLabel(295, 145, 500, @"Valorite");
			AddLabel(135, 145, 500, @"Agapite");
			AddLabel(135, 105, 500, @"Copper");
			AddLabel(210, 105, 500, @"Bronze");
			AddLabel(295, 105, 500, @"Gold");
			AddLabel(210, 145, 500, @"Verite");
			
			AddLabel(135, 195, 100, @"Leather/Studded/Bone");
			AddLabel(135, 215, 500, @"Spined");
			AddLabel(199, 215, 500, @"Horned");
			AddLabel(263, 216, 500, @"Barbed");			
			
			AddButton(140, 87, 1209, 1210, (int)Buttons.Iron, GumpButtonType.Reply, 0);
			AddButton(147, 237, 1209, 1210, (int)Buttons.Spined, GumpButtonType.Reply, 0);			
			AddButton(225, 87, 1209, 1210, (int)Buttons.DullCopper, GumpButtonType.Reply, 0);
			AddButton(305, 87, 1209, 1210, (int)Buttons.Shadow, GumpButtonType.Reply, 0);
			AddButton(140, 169, 1209, 1210, (int)Buttons.Agapite, GumpButtonType.Reply, 0);
			AddButton(305, 169, 1209, 1210, (int)Buttons.Valorite, GumpButtonType.Reply, 0);
			AddButton(140, 128, 1209, 1210, (int)Buttons.Copper, GumpButtonType.Reply, 0);
			AddButton(225, 127, 1209, 1210, (int)Buttons.Bronze, GumpButtonType.Reply, 0);
			AddButton(305, 127, 1209, 1210, (int)Buttons.Gold, GumpButtonType.Reply, 0);
			AddButton(225, 169, 1209, 1210, (int)Buttons.Verite, GumpButtonType.Reply, 0);
			AddButton(212, 237, 1209, 1210, (int)Buttons.Horned, GumpButtonType.Reply, 0);
			AddButton(277, 237, 1209, 1210, (int)Buttons.Barbed, GumpButtonType.Reply, 0);
			
			AddButton(336, 30, 4018, 4017, (int)Buttons.ExitMenu, GumpButtonType.Reply, 0);

		}
		
		public enum Buttons
		{
			ExitMenu,
			Iron,
			Spined,
			DullCopper,
			Shadow,
			Copper,
			Bronze,
			Gold,
			Agapite,
			Verite,
			Valorite,
			Horned,
			Barbed,
		}
		
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{

			switch ( info.ButtonID )
			{
				case (int)Buttons.ExitMenu:
								m_Player.CloseGump( typeof( SetResourceGump ) );
								m_Player.SendGump( new GMToolGump( m_Player ) );
								break;
				default:
								break;			

				case (int)Buttons.Iron:
				
								m_Player.Target = new ChangeMaterial( CraftResource.Iron );
								break;

				case (int)Buttons.DullCopper:
							
								m_Player.Target = new ChangeMaterial( CraftResource.DullCopper );
								break;
							
				case (int)Buttons.Shadow:
							
								m_Player.Target = new ChangeMaterial( CraftResource.ShadowIron );							
								break;
							
				case (int)Buttons.Copper:
							
								m_Player.Target = new ChangeMaterial( CraftResource.Copper );						
								break;
							
				case (int)Buttons.Bronze:
				
								m_Player.Target = new ChangeMaterial( CraftResource.Bronze );							
								break;
				
				case (int)Buttons.Gold:
							
								m_Player.Target = new ChangeMaterial( CraftResource.Gold );
								break;
							
				case (int)Buttons.Agapite:
							
								m_Player.Target = new ChangeMaterial( CraftResource.Agapite );
								break;
							
				case (int)Buttons.Verite:
							
								m_Player.Target = new ChangeMaterial( CraftResource.Verite );
								break;	
							
				case (int)Buttons.Valorite:
							
								m_Player.Target = new ChangeMaterial( CraftResource.Valorite );
								break;
							
				case (int)Buttons.Spined:
							
								m_Player.Target = new ChangeMaterial( CraftResource.SpinedLeather );
								break;
							
				case (int)Buttons.Horned:
							
								m_Player.Target = new ChangeMaterial( CraftResource.HornedLeather );
								break;
							
				case (int)Buttons.Barbed:
							
								m_Player.Target = new ChangeMaterial( CraftResource.BarbedLeather );
								break;	
			}
			
			if ( info.ButtonID != (int)Buttons.ExitMenu )
			{
				m_Player.SendMessage( "Select the item(s). Press ESC to abort." );
				m_Player.SendGump( new SetResourceGump( m_Player ) );
			}
		}
		
		public class ChangeMaterial : Target
		{
			CraftResource m_Resource;
			
			public ChangeMaterial( CraftResource resource ) : base( -1, false, TargetFlags.None )
			{
				m_Resource = resource;
			}
			
			protected override void OnTarget( Mobile from, object targ )
			{
				if ( targ is BaseArmor )
					((BaseArmor)targ).Resource = m_Resource;
													
				else if ( targ is BaseWeapon )
					((BaseWeapon)targ).Resource = m_Resource;
					
				else
				{
					from.SendMessage("You cannot do that.");
					from.Target = new ChangeMaterial( m_Resource );
					return;
				}
				
				from.Target = new ChangeMaterial( m_Resource );
				from.SendMessage( "Chang'd!" );
			}
		}		
	}
}

namespace Server.Items
{ 
	public class BagOfRingmail : Bag 
	{ 
		[Constructable] 
		public BagOfRingmail() : this( 1 ) 
		{ 
			Movable = true; 
			Hue = 0x250; 
			Name = "a bag of ringmail";
		}
		[Constructable]
		public BagOfRingmail( int amount )
		{
			DropItem( new RingmailArms() );
			DropItem( new RingmailChest() );
			DropItem( new RingmailGloves() );
			DropItem( new RingmailLegs() );
		}

		public BagOfRingmail( Serial serial ) : base( serial ) 
		{ 
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

	public class BagOfChainmail : Bag 
	{ 
		[Constructable] 
		public BagOfChainmail() : this( 1 ) 
		{ 
			Movable = true; 
			Hue = 100; 
			Name = "a bag of chainmail";
		}
		[Constructable]
		public BagOfChainmail( int amount )
		{
			DropItem( new ChainCoif() );
			DropItem( new ChainChest() );
			DropItem( new ChainLegs() );
		}

		public BagOfChainmail( Serial serial ) : base( serial ) 
		{ 
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

	public class BagOfPlatemail : Bag 
	{ 
		[Constructable] 
		public BagOfPlatemail() : this( 1 ) 
		{ 
			Movable = true; 
			Hue = 150; 
			Name = "a bag of platemail";
		}
		[Constructable]
		public BagOfPlatemail( int amount )
		{
			DropItem( new PlateArms() );
			DropItem( new PlateChest() );
			DropItem( new PlateGloves() );
			DropItem( new PlateLegs() );
			DropItem( new PlateGorget() );
			DropItem( new PlateHelm() );
		}

		public BagOfPlatemail( Serial serial ) : base( serial ) 
		{ 
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

	public class BagOfLeatherArmor : Bag 
	{ 
		[Constructable] 
		public BagOfLeatherArmor() : this( 1 ) 
		{ 
			Movable = true; 
			Hue = 110; 
			Name = "a bag of leather armor";
		}
		[Constructable]
		public BagOfLeatherArmor( int amount )
		{
			DropItem( new LeatherArms() );
			DropItem( new LeatherChest() );
			DropItem( new LeatherGloves() );
			DropItem( new LeatherLegs() );
			DropItem( new LeatherGorget() );
			DropItem( new LeatherCap() );
		}

		public BagOfLeatherArmor( Serial serial ) : base( serial ) 
		{ 
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

	public class BagOfStuddedArmor : Bag 
	{ 
		[Constructable] 
		public BagOfStuddedArmor() : this( 1 ) 
		{ 
			Movable = true; 
			Hue = 130; 
			Name = "a bag of studded armor";
		}
		[Constructable]
		public BagOfStuddedArmor( int amount )
		{
			DropItem( new StuddedArms() );
			DropItem( new StuddedChest() );
			DropItem( new StuddedGloves() );
			DropItem( new StuddedLegs() );
			DropItem( new StuddedGorget() );
		}

		public BagOfStuddedArmor( Serial serial ) : base( serial ) 
		{ 
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

	public class BagOfBoneArmor : Bag 
	{ 
		[Constructable] 
		public BagOfBoneArmor() : this( 1 ) 
		{ 
			Movable = true; 
			Hue = 140; 
			Name = "a bag of bone armor";
		}
		[Constructable]
		public BagOfBoneArmor( int amount )
		{
			DropItem( new BoneArms() );
			DropItem( new BoneChest() );
			DropItem( new BoneGloves() );
			DropItem( new BoneLegs() );
			DropItem( new BoneHelm() );
		}

		public BagOfBoneArmor( Serial serial ) : base( serial ) 
		{ 
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

namespace Server.Scripts.Commands
{
	public class ToolGumpCommands
	{
		#region Command Initializers
		public static void Initialize()
		{
			Server.Commands.Register( "gmt", AccessLevel.GameMaster, new CommandEventHandler( ToolGump_OnCommand ) );
		}
		#endregion


		private static void ToolGump_OnCommand( CommandEventArgs e )
		{
			Mobile m_Player = (Mobile)e.Mobile;

				m_Player.SendGump( new GMToolGump( m_Player ) );

		}
	}
}