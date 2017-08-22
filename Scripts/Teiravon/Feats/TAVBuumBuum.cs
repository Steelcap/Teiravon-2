using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{
	public class BuumBuumGump : Gump
	{
		TeiravonMobile m_Player;
		int m_LastItem;

		public BuumBuumGump( Mobile from, int last ): base( 0, 0 )
		{
			m_Player = ( TeiravonMobile ) from;
			m_Player.CloseGump( typeof( BuumBuumGump ) );
			m_LastItem = last;


			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);
			AddLayout();

			if (m_LastItem > 0)
			{
				AddLabel(145, 363, 1000, @"Create last");
				AddButton(223, 364, 2714, 2715, m_LastItem, GumpButtonType.Reply, 0);
			}

			AddPage(2);

			AddHtml( 272, 160, 219, 187, @"This particular explosive formula is relatively easy for any orc to learn. It is a mixture of black pearls and sulfurous ash that causes a small, almost nonexistant explosion when it reacts with air. The noise of burning sulfurous ash is much akin to a yelp, thusly the mixture has been dubbed golog's yelp, the yelp of an elf.", true, true);
			AddButton(454, 364, 2714, 2715, (int)Buttons.CGolog, GumpButtonType.Reply, 0);
			AddLabel(404, 363, 1000, @"Create");

			AddPage(3);

			AddHtml( 272, 160, 219, 187, @"This formula consists of fertile dirt, mixed with sulfurous ash and crushed black pearls. The dirt adds considerably to the explosive properties of the two latter ingredients. When the mixture explodes, it causes a noise that sounds like an audible discharge of gas. The orcs have promptly named it the shara's fart, the flatulence of a human.", true, true);
			AddButton(454, 364, 2714, 2715, (int)Buttons.CShara, GumpButtonType.Reply, 0);
			AddLabel(404, 363, 1000, @"Create");

			AddPage(4);

			AddHtml( 272, 160, 219, 187, @"The Gazat's Roar is an explosive of a medium potency. The blast radius is somewhat wider than usual, due to the highly compressed combination of sulfurous ash and fertile dirt. The audible discharge is extremely deep, low sound. Therefore it has been named the Gazat's Roar, the roar of a dwarf.", true, true);
			AddButton(454, 364, 2714, 2715, (int)Buttons.CGazat, GumpButtonType.Reply, 0);
			AddLabel(404, 363, 1000, @"Create");

			AddPage(5);

			AddHtml( 272, 160, 219, 187, @"The Stone Smasher explosive has previously been used by orcs to clear small caverns of rubble. Upon detonation, it emits a stone-shattering pressure wave, and has promptly been dubbed Stone Smasher.", true, true);
			AddButton(454, 364, 2714, 2715, (int)Buttons.CStone, GumpButtonType.Reply, 0);
			AddLabel(404, 363, 1000, @"Create");

			AddPage(6);

			AddHtml( 272, 160, 219, 187, @"The Mountain Crusher is a more potent version of the Stone Smasher formula. The pressure wave is far greater in both strength and range. It has been used to create makeshift caves for traveling warbands.", true, true);
			AddButton(454, 364, 2714, 2715, (int)Buttons.CMountain, GumpButtonType.Reply, 0);
			AddLabel(404, 363, 1000, @"Create");

			AddPage(7);

			AddHtml( 272, 160, 219, 187, @"The Might of the Uruk is the most potent formula known to orcs. It has been the end of many an orc makur, being very volatile in nature and prone to spontaneous explosions. The formula was originally devised by the legendary Krugthor Spinebreaker, it has since been upgraded with a new reagent and made a lot safer by the promiscuous orc smith, Imrik. The new formula has been passed on as the orc legacy.", true, true);
			AddButton(454, 364, 2714, 2715, (int)Buttons.CMight, GumpButtonType.Reply, 0);
			AddLabel(404, 363, 1000, @"Create");

		}

		public void AddLayout()
		{

			AddImageTiled(120, 120, 375, 275, 5124);
			AddImageTiled(112, 120, 11, 275, 5123);
			AddImageTiled(495, 120, 11, 275, 5125);
			AddImageTiled(121, 110, 375, 11, 5121);
			AddImageTiled(123, 394, 375, 11, 5127);
			AddImage(113, 395, 5126);
			AddImage(111, 111, 5120);
			AddImage(496, 111, 5122);
			AddImage(496, 395, 5128);

			AddAlphaRegion(124, 121, 370, 272);

			AddImageTiled(122, 153, 372, 6, 5138);
			AddImageTiled(264, 159, 7, 190, 5134);
			AddImageTiled(122, 349, 372, 6, 5138);

			AddImageTiled(499, 123, 8, 268, 5135);
			AddImageTiled(121, 110, 374, 5, 5132);
			AddImageTiled(126, 401, 374, 6, 5138);
			AddImageTiled(110, 124, 6, 268, 5134);

			AddLabel(274, 128, 1000, @"Buum Buum");
			AddLabel(130, 170, 1000, @"Golog's Yelp");
			AddLabel(130, 200, 1000, @"Shara's Fart");
			AddLabel(130, 230, 1000, @"Gazat's Roar");
			AddLabel(130, 260, 1000, @"Stone Smasher");
			AddLabel(130, 290, 1000, @"Mountain Crusher");
			AddLabel(130, 320, 1000, @"Might of the Uruk");

			AddButton(245, 173, 9702, 9703, (int)Buttons.Golog, GumpButtonType.Page, 2);
			AddButton(245, 203, 9702, 9703, (int)Buttons.Shara, GumpButtonType.Page, 3);
			AddButton(245, 233, 9702, 9703, (int)Buttons.Gazat, GumpButtonType.Page, 4);
			AddButton(245, 263, 9702, 9703, (int)Buttons.Stone, GumpButtonType.Page, 5);
			AddButton(245, 293, 9702, 9703, (int)Buttons.Mountain, GumpButtonType.Page, 6);
			AddButton(245, 323, 9702, 9703, (int)Buttons.Might, GumpButtonType.Page, 7);

		}


		public enum Buttons
		{
			ExitMenu,
			Golog,
			Gazat,
			Shara,
			Stone,
			Mountain,
			Might,
			CGolog,
			CGazat,
			CShara,
			CStone,
			CMountain,
			CMight,
			Create,

		}

		public SkillName MainSkill( TeiravonMobile player )
		{
			if ( player.IsBlacksmith() )
				return SkillName.Blacksmith;

			else if ( player.IsAlchemist() )
				return SkillName.Alchemy;

			else if ( player.IsTailor() )
				return SkillName.Tailoring;

		//	else if ( player.IsWoodworker() )
		//		return SkillName.Fletching;

			else if ( player.IsCook() )
				return SkillName.Cooking;

			else if ( player.IsWoodworker() )
				return SkillName.Carpentry;

			else if ( player.IsTinker() )
				return SkillName.Tinkering;

			else
				return SkillName.Camping;
		}

		public bool playerlevel( TeiravonMobile player, int level )
		{
			if ( player.PlayerLevel < level )
			{
				player.SendMessage( "You need to be at least level {0} to do that.", level );
				return true;
			}

			return false;
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{

			Container pack = m_Player.Backpack;

			if (pack == null)
			{
				m_Player.SendMessage( "You don't have a backpack." );
				return;
			}

			bool mandrakeroot = false;
			bool sulfurousash = false;
			bool blackpearl = false;
			bool fertiledirt = false;
			bool bottle = false;
			bool daemonblood = false;
			bool success = false;
			bool nightshade = false;

			int m_Bottle;
			int m_MandrakeRoot;
			int m_SulfurousAsh;
			int m_BlackPearl;
			int m_FertileDirt;
			int m_DaemonBlood;
			int m_NightShade;

			switch( info.ButtonID )
			{

				default:
								break;

				case (int)Buttons.CGolog:
								if ( playerlevel( m_Player, 3 ) )
									break;

								m_BlackPearl = pack.GetAmount( typeof( BlackPearl ) );

								if ( m_BlackPearl >= 3 )
									blackpearl = true;

								m_SulfurousAsh = pack.GetAmount( typeof( SulfurousAsh ) );

								if ( m_SulfurousAsh >= 3 )
									sulfurousash = true;

								m_Bottle = pack.GetAmount( typeof( Bottle) );

								if ( m_Bottle >= 1 )
									bottle = true;


								if ( blackpearl && sulfurousash && bottle )
								{
									pack.ConsumeTotal( typeof( BlackPearl ), 3 );
									pack.ConsumeTotal( typeof( SulfurousAsh ), 3 );
									pack.ConsumeTotal( typeof( Bottle ), 1 );

									double chance = ( m_Player.Skills[ MainSkill( m_Player ) ].Value / 100.0 ) + 0.1 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 0.95)
										chance = 0.95;

									if ( Utility.RandomDouble() <= chance )
										success = true;

									if ( success )
									{
										m_Player.AddToBackpack( new GologsYelp() );
										m_Player.SendMessage( "You bottle the explosive liquid and place it in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the explosive and lose your ingredients." );


									m_Player.SendGump( new BuumBuumGump( m_Player, (int)Buttons.CGolog ) );

									break;
								}

								if ( !blackpearl )
									m_Player.SendMessage( "You need more black pearls." );

								if ( !sulfurousash )
									m_Player.SendMessage( "You need more sulfurous ash." );

								if ( !bottle )
									m_Player.SendMessage( "You need a bottle." );

								break;

				case (int)Buttons.CShara:
								if ( playerlevel( m_Player, 6 ) )
									break;

								m_BlackPearl = pack.GetAmount( typeof( BlackPearl ) );

								if ( m_BlackPearl >= 5 )
									blackpearl = true;

								m_SulfurousAsh = pack.GetAmount( typeof( SulfurousAsh ) );

								if ( m_SulfurousAsh >= 5 )
									sulfurousash = true;

								m_FertileDirt = pack.GetAmount( typeof( FertileDirt ) );

								if ( m_FertileDirt >= 5 )
									fertiledirt = true;

								m_Bottle = pack.GetAmount( typeof( Bottle) );

								if ( m_Bottle >= 1 )
									bottle = true;


								if ( blackpearl && sulfurousash && fertiledirt && bottle )
								{
									pack.ConsumeTotal( typeof( BlackPearl ), 5 );
									pack.ConsumeTotal( typeof( SulfurousAsh ), 5 );
									pack.ConsumeTotal( typeof( FertileDirt ), 5 );
									pack.ConsumeTotal( typeof( Bottle ), 1 );

									double chance = ( m_Player.Skills[MainSkill( m_Player )].Value / 100.0 ) + 0.1 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 0.85)
										chance = 0.85;

									if ( Utility.RandomDouble() <= chance )
										success = true;

									if ( success )
									{
										m_Player.AddToBackpack( new SharasFart() );
										m_Player.SendMessage( "You bottle the explosive liquid and place it in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the explosive and lose your ingredients." );


									m_Player.SendGump( new BuumBuumGump( m_Player, (int)Buttons.CShara ) );

									break;
								}

								if ( !blackpearl )
									m_Player.SendMessage( "You need more black pearls." );

								if ( !sulfurousash )
									m_Player.SendMessage( "You need more sulfurous ash." );

								if ( !fertiledirt )
									m_Player.SendMessage( "You need more fertile dirt." );

								if ( !bottle )
									m_Player.SendMessage( "You need a bottle." );

								break;

				case (int)Buttons.CGazat:
								if ( playerlevel( m_Player, 9 ) )
									break;

								m_BlackPearl = pack.GetAmount( typeof( BlackPearl ) );

								if ( m_BlackPearl >= 7 )
									blackpearl = true;

								m_SulfurousAsh = pack.GetAmount( typeof( SulfurousAsh ) );

								if ( m_SulfurousAsh >= 7 )
									sulfurousash = true;

								m_FertileDirt = pack.GetAmount( typeof( FertileDirt ) );

								if ( m_FertileDirt >= 7 )
									fertiledirt = true;

								m_Bottle = pack.GetAmount( typeof( Bottle) );

								if ( m_Bottle >= 1 )
									bottle = true;


								if ( blackpearl && sulfurousash && fertiledirt && bottle )
								{
									pack.ConsumeTotal( typeof( BlackPearl ), 7 );
									pack.ConsumeTotal( typeof( SulfurousAsh ), 7 );
									pack.ConsumeTotal( typeof( FertileDirt ), 7 );
									pack.ConsumeTotal( typeof( Bottle ), 1 );

									double chance = ( m_Player.Skills[MainSkill( m_Player )].Value / 100.0 ) + 0.1 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 0.75)
										chance = 0.75;

									if ( Utility.RandomDouble() <= chance )
										success = true;

									if ( success )
									{
										m_Player.AddToBackpack( new GazatsRoar() );
										m_Player.SendMessage( "You bottle the explosive liquid and place it in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the explosive and lose your ingredients." );

									m_Player.SendGump( new BuumBuumGump( m_Player, (int)Buttons.CGazat ) );

									break;
								}

								if ( !blackpearl )
									m_Player.SendMessage( "You need more black pearls." );

								if ( !sulfurousash )
									m_Player.SendMessage( "You need more sulfurous ash." );

								if ( !fertiledirt )
									m_Player.SendMessage( "You need more fertile dirt." );

								if ( !bottle )
									m_Player.SendMessage( "You need a bottle." );

								break;

				case (int)Buttons.CStone:
								if ( playerlevel( m_Player, 12 ) )
									break;

								m_BlackPearl = pack.GetAmount( typeof( BlackPearl ) );

								if ( m_BlackPearl >= 10 )
									blackpearl = true;

								m_SulfurousAsh = pack.GetAmount( typeof( SulfurousAsh ) );

								if ( m_SulfurousAsh >= 10 )
									sulfurousash = true;

								m_FertileDirt = pack.GetAmount( typeof( FertileDirt ) );

								if ( m_FertileDirt >= 10 )
									fertiledirt = true;

								m_MandrakeRoot = pack.GetAmount( typeof( MandrakeRoot ) );

								if ( m_MandrakeRoot >= 10 )
									mandrakeroot = true;

								m_Bottle = pack.GetAmount( typeof( Bottle) );

								if ( m_Bottle >= 1 )
									bottle = true;


								if ( blackpearl && sulfurousash && fertiledirt && mandrakeroot && bottle )
								{
									pack.ConsumeTotal( typeof( BlackPearl ), 10 );
									pack.ConsumeTotal( typeof( SulfurousAsh ), 10 );
									pack.ConsumeTotal( typeof( FertileDirt ), 10 );
									pack.ConsumeTotal( typeof( MandrakeRoot ), 10 );
									pack.ConsumeTotal( typeof( Bottle ), 1 );

									double chance = ( m_Player.Skills[MainSkill( m_Player )].Value / 100.0 ) + 0.1 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 0.65)
										chance = 0.65;

									if ( Utility.RandomDouble() <= chance )
										success = true;

									if ( success )
									{
										m_Player.AddToBackpack( new StoneSmasher() );
										m_Player.SendMessage( "You bottle the explosive liquid and place it in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the explosive and lose your ingredients." );


									m_Player.SendGump( new BuumBuumGump( m_Player, (int)Buttons.CStone ) );

									break;
								}

								if ( !blackpearl )
									m_Player.SendMessage( "You need more black pearls." );

								if ( !sulfurousash )
									m_Player.SendMessage( "You need more sulfurous ash." );

								if ( !fertiledirt )
									m_Player.SendMessage( "You need more fertile dirt." );

								if ( !mandrakeroot )
									m_Player.SendMessage( "You need more mandrake root." );

								if ( !bottle )
									m_Player.SendMessage( "You need a bottle." );

								break;

				case (int)Buttons.CMountain:
								if ( playerlevel( m_Player, 15 ) )
									break;

								m_BlackPearl = pack.GetAmount( typeof( BlackPearl ) );

								if ( m_BlackPearl >= 15 )
									blackpearl = true;

								m_SulfurousAsh = pack.GetAmount( typeof( SulfurousAsh ) );

								if ( m_SulfurousAsh >= 15 )
									sulfurousash = true;

								m_FertileDirt = pack.GetAmount( typeof( FertileDirt ) );

								if ( m_FertileDirt >= 15 )
									fertiledirt = true;

								m_MandrakeRoot = pack.GetAmount( typeof( MandrakeRoot ) );

								if ( m_MandrakeRoot >= 15 )
									mandrakeroot = true;

								m_Bottle = pack.GetAmount( typeof( Bottle) );

								if ( m_Bottle >= 1 )
									bottle = true;


								if ( blackpearl && sulfurousash && fertiledirt && mandrakeroot && bottle )
								{
									pack.ConsumeTotal( typeof( BlackPearl ), 15 );
									pack.ConsumeTotal( typeof( SulfurousAsh ), 15 );
									pack.ConsumeTotal( typeof( FertileDirt ), 15 );
									pack.ConsumeTotal( typeof( MandrakeRoot ), 15 );
									pack.ConsumeTotal( typeof( Bottle ), 1 );

									double chance = ( m_Player.Skills[MainSkill( m_Player )].Value / 100.0 ) + 0.1 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 0.55)
										chance = 0.55;

									if ( Utility.RandomDouble() <= chance )
										success = true;

									if ( success )
									{
										m_Player.AddToBackpack( new MountainCrusher() );
										m_Player.SendMessage( "You bottle the explosive liquid and place it in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the explosive and lose your ingredients." );

									m_Player.SendGump( new BuumBuumGump( m_Player, (int)Buttons.CMountain ) );

									break;
								}

								if ( !blackpearl )
									m_Player.SendMessage( "You need more black pearls." );

								if ( !sulfurousash )
									m_Player.SendMessage( "You need more sulfurous ash." );

								if ( !fertiledirt )
									m_Player.SendMessage( "You need more fertile dirt." );

								if ( !mandrakeroot )
									m_Player.SendMessage( "You need more mandrake root." );

								if ( !bottle )
									m_Player.SendMessage( "You need a bottle." );

								break;

				case (int)Buttons.CMight:
								if ( playerlevel( m_Player, 18 ) )
									break;

								m_BlackPearl = pack.GetAmount( typeof( BlackPearl ) );

								if ( m_BlackPearl >= 20 )
									blackpearl = true;

								m_SulfurousAsh = pack.GetAmount( typeof( SulfurousAsh ) );

								if ( m_SulfurousAsh >= 20 )
									sulfurousash = true;

								m_FertileDirt = pack.GetAmount( typeof( FertileDirt ) );

								if ( m_FertileDirt >= 20 )
									fertiledirt = true;

								m_DaemonBlood = pack.GetAmount( typeof( DaemonBlood ) );

								if ( m_DaemonBlood >= 20 )
									daemonblood = true;

								m_Bottle = pack.GetAmount( typeof( Bottle) );

								if ( m_Bottle >= 1 )
									bottle = true;

								m_NightShade = pack.GetAmount( typeof( Nightshade ) );

								if ( m_NightShade >= 3 )
									nightshade = true;


								if ( blackpearl && sulfurousash && fertiledirt && daemonblood && bottle && nightshade )
								{
									pack.ConsumeTotal( typeof( BlackPearl ), 20 );
									pack.ConsumeTotal( typeof( SulfurousAsh ), 20 );
									pack.ConsumeTotal( typeof( FertileDirt ), 20 );
									pack.ConsumeTotal( typeof( DaemonBlood ), 20 );
									pack.ConsumeTotal( typeof( Bottle ), 1 );
									pack.ConsumeTotal( typeof( Nightshade ), 3 );

									double chance = ( m_Player.Skills[MainSkill( m_Player )].Value / 100.0 ) + 0.1 + ( (double)Utility.Random( 20 ) / 100.0 );

									if (chance > 0.45)
										chance = 0.45;

									if ( Utility.RandomDouble() <= chance )
										success = true;

									if ( success )
									{
										m_Player.AddToBackpack( new MightOfTheUruk() );
										m_Player.SendMessage( "You bottle the explosive liquid and place it in your pack." );
									}
									else
										m_Player.SendMessage( "You fail to create the explosive and lose your ingredients." );

									m_Player.SendGump( new BuumBuumGump( m_Player, (int)Buttons.CMight ) );

									break;
								}

								if ( !blackpearl )
									m_Player.SendMessage( "You need more black pearls." );

								if ( !sulfurousash )
									m_Player.SendMessage( "You need more sulfurous ash." );

								if ( !fertiledirt )
									m_Player.SendMessage( "You need more fertile dirt." );

								if ( !daemonblood )
									m_Player.SendMessage( "You need more daemon blood." );

								if ( !nightshade )
									m_Player.SendMessage( "You need more nightshade." );

								if ( !bottle )
									m_Player.SendMessage( "You need a bottle." );

								break;
			}
		}
	}
}


namespace Server.Scripts.Commands
{
	public class OrcMortarCommands
	{
		#region Command Initializers
		public static void Initialize()
		{
			Server.Commands.Register( "Buumbuum", AccessLevel.Player, new CommandEventHandler( BuumBuum_OnCommand ) );
		}
		#endregion


		private static void BuumBuum_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			if ( m_Player.HasFeat( TeiravonMobile.Feats.BuumBuum ) )
				m_Player.SendGump( new BuumBuumGump( m_Player, 0 ) );
			else
				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility );
		}
	}
}


namespace Server.Items
{
	public class GologsYelp : BaseExplosionPotion
	{
		public override int MinDamage { get { return 5; } }
		public override int MaxDamage { get { return 10; } }

		[Constructable]
		public GologsYelp() : base( PotionEffect.ExplosionLesser )
		{
			Name = "Golog's Yelp";
			Hue = 2554;
			Weight = 1.0;
		}

		public GologsYelp( Serial serial ) : base( serial )
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

	public class SharasFart : BaseExplosionPotion
	{
		public override int MinDamage { get { return 7; } }
		public override int MaxDamage { get { return 13; } }

		[Constructable]
		public SharasFart() : base( PotionEffect.ExplosionLesser )
		{
			Name = "Shara's Fart";
			Hue = 2560;
			Weight = 1.0;
		}

		public SharasFart( Serial serial ) : base( serial )
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

	public class GazatsRoar : BaseExplosionPotion
	{
		public override int MinDamage { get { return 10; } }
		public override int MaxDamage { get { return 15; } }
		public override int ExplosionRange{ get{ return 4; } }

		[Constructable]
		public GazatsRoar() : base( PotionEffect.Explosion )
		{
			Name = "Gazat's Roar";
			Hue = 2558;
			Weight = 1.0;
		}

		public GazatsRoar( Serial serial ) : base( serial )
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

	public class StoneSmasher : BaseExplosionPotion
	{
		public override int MinDamage { get { return 15; } }
		public override int MaxDamage { get { return 19; } }
		public override int ExplosionRange{ get{ return 5; } }

		[Constructable]
		public StoneSmasher() : base( PotionEffect.Explosion )
		{
			Name = "Stone Smasher";
			Hue = 2547;
			Weight = 1.0;
			ItemID = 0x1AE4;
		}

		public StoneSmasher( Serial serial ) : base( serial )
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
	public class MountainCrusher : BaseExplosionPotion
	{
		public override int MinDamage { get { return 20; } }
		public override int MaxDamage { get { return 38; } }
		public override int ExplosionRange{ get{ return 6; } }

		[Constructable]
		public MountainCrusher() : base( PotionEffect.ExplosionGreater )
		{
			Name = "Mountain Crusher";
			Hue = 2562;
			Weight = 1.0;
			ItemID = 0x1AE2;
		}

		public MountainCrusher( Serial serial ) : base( serial )
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

	public class MightOfTheUruk : BaseExplosionPotion
	{
		public override int MinDamage { get { return 35; } }
		public override int MaxDamage { get { return 60; } }
		public override int ExplosionRange{ get{ return 8; } }

		[Constructable]
		public MightOfTheUruk() : base( PotionEffect.ExplosionGreater )
		{
			Name = "Might of the Uruk";
			Hue = 2545;
			Weight = 1.0;
			ItemID = 0x1F0C;
		}

		public MightOfTheUruk( Serial serial ) : base( serial )
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