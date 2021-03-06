using System;
using Server;
using Server.Accounting;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.Engines.XmlSpawner2;

using TAVCoolDown = Server.Teiravon.AbilityCoolDown;

namespace Server.Teiravon.Feats
{
    public enum Crafts
    {
        Blacksmith,
        Tailor,
        Carpenter,
        Fletcher,
        Cook,
        Tinker
    }

	public class Functions
	{
        public static void MasterCraftsman(TeiravonMobile m_Player)
        {
            if (m_Player.HasFeat(TeiravonMobile.Feats.BlacksmithTraining))
                m_Player.Skills.Blacksmith.Cap = 120;

            if (m_Player.HasFeat(TeiravonMobile.Feats.TinkerTraining))
                m_Player.Skills.Tinkering.Cap = 120;

            if (m_Player.HasFeat(TeiravonMobile.Feats.CarpenterTraining))
                m_Player.Skills.Carpentry.Cap = 120;
            
            if (m_Player.HasFeat(TeiravonMobile.Feats.CookTraining))
                m_Player.Skills.Cooking.Cap = 120;
    
            if (m_Player.HasFeat(TeiravonMobile.Feats.FletcherTraining))
                m_Player.Skills.Fletching.Cap = 120;

            if (m_Player.HasFeat(TeiravonMobile.Feats.TailorTraining))
                m_Player.Skills.Tailoring.Cap = 120;

        }
        
        public static void MerchantCraft(TeiravonMobile m_Player, Crafts craft)
        {
            int i = 0;
            switch (craft)
            {
                case Crafts.Blacksmith:
                    {
                        i = 7;
                        m_Player.SendMessage("You've set out to learn Blacksmithing.");
                        m_Player.AddToBackpack(new ArmorEnamelKit());
                        break;
                    }
                case Crafts.Fletcher:
                    {
                        i = 8;
                        m_Player.SendMessage("You've set out to learn Bowcraft and Fletching.");
                        break;
                    }
                case Crafts.Carpenter:
                    {
                        i = 11;
                        m_Player.SendMessage("You've set out to learn Carpentry.");
                        m_Player.AddToBackpack(new FurnitureDyeTub());
                        break;
                    }
                case Crafts.Cook:
                    {
                        i = 13;
                        m_Player.SendMessage("You've set out to learn Cooking.");
                        break;
                    }
                case Crafts.Tailor:
                    {
                        i = 34;
                        m_Player.SendMessage("You've set out to learn Tailoring.");
                        m_Player.AddToBackpack(new LeatherDyeTub());
                        break;
                    }
                case Crafts.Tinker:
                    {
                        i =37;
                        m_Player.SendMessage("You've set out to learn Tinkering.");
                        break;
                    }
            }

            m_Player.Skills[i].Cap = 100;
        }
        public static void Camouflage(TeiravonMobile m_Player)
        {
            m_Player.SendMessage("You learn to conceal your position.");


            if (m_Player.Skills.Stealth.Cap < 80)
                m_Player.Skills.Stealth.Cap = 80;

            if (m_Player.Skills.Stealth.Base < 30)
                m_Player.Skills.Stealth.Base = 30;

            CheckSkills(m_Player);

        }

        public static void CombatTraining(TeiravonMobile m_Player)
        {
            m_Player.SendMessage("You learn to defend yourself.");


            if (m_Player.Skills.Swords.Cap < 80)
                m_Player.Skills.Swords.Cap = 80;
            else
                m_Player.Skills.Swords.Cap += 10;

            if (m_Player.Skills.Macing.Cap < 80)
                m_Player.Skills.Macing.Cap = 80;
            else
                m_Player.Skills.Macing.Cap += 10;

            if (m_Player.Skills.Fencing.Cap < 80)
                m_Player.Skills.Fencing.Cap = 80;
            else
                m_Player.Skills.Fencing.Cap += 10;

            if (m_Player.Skills.Archery.Cap < 40)
                m_Player.Skills.Archery.Cap = 40;
            else
                m_Player.Skills.Archery.Cap += 10;

        }

        public static void NaturesDefender(TeiravonMobile m_Player)
        {
            m_Player.SendMessage("You are a sworn Defender of the Wilds.");

            m_Player.Skills.Macing.Cap = 100;
            m_Player.Skills.Parry.Cap = 100;
            m_Player.Skills.Tactics.Cap = 100;
            if (m_Player.Skills.Focus.Cap < 60) 
                m_Player.Skills.Focus.Cap = 60;
            
            m_Player.Skills.Macing.Base += 10;
            m_Player.Skills.Parry.Base += 10;
            m_Player.Skills.Tactics.Base += 10;
            CheckSkills(m_Player);

        }

        public static void TempestsWrath(TeiravonMobile m_Player)
        {
            m_Player.SendMessage("You posses all the wrath of the tempest's fury.");

            m_Player.Skills.EvalInt.Cap = 100;
            m_Player.Skills.EvalInt.Base += 10;
            
            m_Player.Skills.Meditation.Cap = 80.0;
            if (m_Player.Skills.Meditation.Base < 30.0)
                m_Player.Skills.Meditation.Base = 30.0;

            CheckSkills(m_Player);
        }

        public static void HealersOath(TeiravonMobile m_player)
        {
            m_player.SendMessage("You have taken up the oath of the healer.");
            m_player.Skills.Macing.Cap = 60.0;
            m_player.Skills.Tactics.Cap = 0.0;
            m_player.Skills.Healing.Cap = 120.0;
            m_player.Skills.Anatomy.Cap = 120.0;
            m_player.AddToBackpack(new HealScroll());
            m_player.AddToBackpack(new GreaterHealScroll());
            CheckSkills(m_player);
        }

        public static void Pounce(TeiravonMobile m_Player)
        {
            m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You have gained the Pounce Feat.");
        }

        public static void JackOfAll(TeiravonMobile m_Player)
        {
            m_Player.Skills.Forensics.Cap = 100.0;
            m_Player.Skills.Cartography.Cap = 100.0;
            m_Player.Skills.Tracking.Cap = 100.0;
            m_Player.Skills.Lockpicking.Cap = 100.0;
            m_Player.Skills.RemoveTrap.Cap = 100.0;

            m_Player.Skills.Forensics.Base += 35.0;
            m_Player.Skills.Cartography.Base += 35.0;
            m_Player.Skills.Tracking.Base += 35.0;
            m_Player.Skills.Lockpicking.Base += 35.0;
            m_Player.Skills.RemoveTrap.Base += 35.0;

            if (m_Player.IsHuman())
                m_Player.SkillsCap = 13000;
            else
                m_Player.SkillsCap = 12000;

            CheckSkills(m_Player);

            m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You have gained the Jack Of All Trades Feat.");
        }

		public static void CheckSkills( TeiravonMobile m_Player )
		{
			for ( int i = 0; i < 51; i++ )
				if ( m_Player.Skills[i].Base > m_Player.Skills[i].Cap )
					m_Player.Skills[i].Base = m_Player.Skills[i].Cap;
		}

        // Enchanting Melody
        public static void EnchantingMelody(TeiravonMobile m_Player)
        {
            m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You have gained the Enchanting Melody Feat.");
        }

		// Rigorous Training
		public static void RigorousTraining( TeiravonMobile m_Player )
		{
			m_Player.Skills.Magery.Base += 10.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Rigorous Training Feat." );
		}
        // Mind, Body, and Soul
        public static void MindBodySoul( TeiravonMobile m_Player )
        {
            m_Player.Skills.Focus.Cap = 100.0;
            m_Player.Skills.Meditation.Cap = 100.0;
            m_Player.Skills.MagicResist.Cap = 100.0;

            m_Player.Skills.Focus.Base += 45.0;
            m_Player.Skills.Meditation.Base += 45.0;
            m_Player.Skills.MagicResist.Base += 45.0;

            if (m_Player.Skills.MagicResist.Base >= 100.0)
                m_Player.Skills.MagicResist.Base = 100.0;
            
            if (m_Player.Skills.Focus.Base >= 100.0)
                m_Player.Skills.Focus.Base = 100.0;

            if (m_Player.Skills.Meditation.Base >= 100.0)
                m_Player.Skills.Meditation.Base = 100.0;

            m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You have gained the Mind Body and Soul Feat.");
        }

		// Meditative Concentration
		public static void MeditativeConcentration( TeiravonMobile m_Player )
		{
			m_Player.Skills.Meditation.Cap = 100.0;
            if( m_Player.Skills.Meditation.Base < 30.0)
            m_Player.Skills.Meditation.Base = 30.0;

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Meditative Concentration Feat." );
		}

        // Flurry of Blows
        public static void Flurry(TeiravonMobile m_Player)
        {
            m_Player.SendMessage(Teiravon.Colors.FeatMessageColor, "You have gained the Flurry of Blows Feat.");
        }
		// Inscription
		public static void Inscription( TeiravonMobile m_Player )
		{
			ScribesPen m_ScribePen = new ScribesPen();
			BlankScroll m_BlankScroll = new BlankScroll( 10 );
			Container m_Backpack = m_Player.Backpack;

			m_Backpack.AddItem( m_ScribePen );
			m_Backpack.AddItem( m_BlankScroll );

			m_Player.Skills.Inscribe.Base += 30.0;
            if( m_Player.Skills.Inscribe.Cap < 100.0)
                m_Player.Skills.Inscribe.Cap = 100.0;
			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Inscription Feat." );
		}

		// Advanced Stealth
		public static void AdvancedStealth( TeiravonMobile m_Player )
		{
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Advanced Stealth Feat." );
		}

		// Skilled gatherer
		public static void SkilledGatherer( TeiravonMobile m_Player )
		{
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Skilled Gatherer Feat." );
		}

		// Advanced Dying
		public static void AdvancedDying( TeiravonMobile m_Player )
		{
			Container m_Backpack = m_Player.Backpack;
			DyeTub m_dtub = new SpecialDyeTub();
			m_Backpack.AddItem( m_dtub);
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Advanced Dying Feat." );
		}

		// Leather Dying
		public static void LeatherDying( TeiravonMobile m_Player )
		{
			Container m_Backpack = m_Player.Backpack;
			DyeTub m_dtub = new LeatherDyeTub();
			m_Backpack.AddItem( m_dtub);
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Leather Dying Feat." );
		}

		// Furniture Staining
		public static void FurnitureStaining( TeiravonMobile m_Player )
		{
			Container m_Backpack = m_Player.Backpack;
			DyeTub m_dtub = new FurnitureDyeTub();
			m_Backpack.AddItem( m_dtub);
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Furniture Staining Feat." );
		}

		// ArmorEnameling
		public static void ArmorEnameling( TeiravonMobile m_Player )
		{
			Container m_Backpack = m_Player.Backpack;
			ArmorEnamelKit m_dkit = new ArmorEnamelKit();
			m_Backpack.AddItem( m_dkit);
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Armor Enameling Feat." );
		}

		// Wilderness Lore
		public static void WildernessLore( TeiravonMobile m_Player )
		{
			m_Player.Skills.Camping.Base += 30.0;
			m_Player.Skills.Cooking.Base += 30.0;
			m_Player.Skills.Fishing.Base += 30.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Wilderness Lore Feat." );
		}

		// Beast Lore
		public static void BeastLore( TeiravonMobile m_Player )
		{
			m_Player.Skills.Veterinary.Base += 45.0;
			m_Player.Skills.AnimalTaming.Base += 40.0;
			m_Player.Skills.Herding.Base += 30.0;
			m_Player.Skills.AnimalLore.Base += 45.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Beast Lore Feat." );
		}

		// Forensics
		public static void Forensics( TeiravonMobile m_Player )
		{
			m_Player.Skills.Tracking.Base += 30.0;
			m_Player.Skills.Forensics.Base += 30.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Forensics Feat." );
		}

		// Orientation
		public static void Orientation( TeiravonMobile m_Player )
		{
			m_Player.Skills.Cartography.Base += 30.0;

            if (m_Player.Skills.Cartography.Cap < 60)
                m_Player.Skills.Cartography.Cap = 60;

            if (m_Player.Backpack != null)
            {
                m_Player.AddToBackpack(new MapmakersPen());
                m_Player.AddToBackpack(new BlankScroll(50));
            }


			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Cartography Feat." );
		}

		// Leadership
		public static void Leadership( TeiravonMobile m_Player )
		{
			m_Player.FollowersMax += 3;
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Leadership Feat." );
		}

		// Locksmithing
		public static void Locksmithing( TeiravonMobile m_Player )
		{
			Lockpick m_Lockpick = new Lockpick();
			m_Lockpick.Amount = 5;
			Container m_Backpack = m_Player.Backpack;

			m_Backpack.AddItem( m_Lockpick );
			m_Player.Skills.Lockpicking.Base += 30.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Locksmithing Feat." );
		}

		// Alchemy Science / Apothecary
		public static void AlchemyScience( TeiravonMobile m_Player )
		{
			Bag bag = new Bag();

			bag.DropItem( new MortarPestle() );
			bag.DropItem( new Bottle( 20 ) );
			bag.DropItem( new BagOfReagents( 25 ) );
			bag.DropItem( new AlchemyTome() );
			bag.DropItem( new AgilityFormula() );
			bag.DropItem( new NightSightFormula() );
			bag.DropItem( new LesserHealFormula() );
			bag.DropItem( new StrengthFormula() );
			bag.DropItem( new LesserPoisonFormula() );
			bag.DropItem( new LesserCureFormula() );
			bag.DropItem( new LesserExplosionFormula() );

			m_Player.Backpack.DropItem( bag );

			m_Player.Skills.Alchemy.Base += 30.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Alchemy Science/Apothecary Feat." );
		}

		// Expert Mining
		public static void ExpertMining( TeiravonMobile m_Player )
		{
			m_Player.Skills.Mining.Base += 20.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Expert Mining Feat." );
		}

		// Marksmanship
		public static void Marksmanship( TeiravonMobile m_Player )
		{
			m_Player.Skills.Archery.Base += 20.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Marksmanship Feat." );
		}

		// Blademaster
		public static void Blademaster( TeiravonMobile m_Player )
		{
			m_Player.Skills.Swords.Base += 20.0;

			CheckSkills( m_Player );

			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You have gained the Blademaster Feat." );
		}

		// Magic Resistance
		public static void MagicResistance( TeiravonMobile m_Player )
		{
			m_Player.Skills.MagicResist.Base += 30.0;

			CheckSkills( m_Player );

			m_Player.SendMessage ( Teiravon.Colors.FeatMessageColor, "You have gained the Magic Resistance Feat." );
		}
        
		// Wealthy Lineage
		public static void WealthyLineage( TeiravonMobile m_Player )
		{
			BankBox m_Bank = m_Player.BankBox;

			Container m_Cont = new WoodenBox();
			m_Cont.AddItem ( new BankCheck( 10000 ) );
			m_Bank.AddItem ( m_Cont );
            
            Account acct = m_Player.Account as Account;
            acct.Comments.Add( new AccountComment( "System", String.Format( "Wealthy Lineage : {0}",m_Player.Name ) ) );

			m_Player.SendMessage ( Teiravon.Colors.FeatMessageColor, "You have recieved a family inheritence upon reaching adulthood." );
			m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "The inheritance was placed in your bank account." );
		}

		// Warfare Training
		public static void WarfareTraining( TeiravonMobile m_Player )
		{

            m_Player.SendMessage("Are you really going to take a feat who's sole purpose is to give you mediocre items? Really? A whole feat? That's a really bad choice.");
            m_Player.SendMessage("I'm not going to let you waste your feat point on this.. go back and pick something else, something useful this time.");
            /*
			Container m_Backpack = m_Player.Backpack;
			Backpack m_Bag = new Backpack();

			bool armor = true;

			switch ( m_Player.PlayerClass )
			{
				case TeiravonMobile.Class.Fighter:
				case TeiravonMobile.Class.Cavalier:
				case TeiravonMobile.Class.Berserker:
					Longsword ls = new Longsword();
					ls.Resource = CraftResource.DullCopper;

					m_Bag.AddItem( ls );

					break;

				case TeiravonMobile.Class.Kensai:
					Katana kat = new Katana();
					kat.Resource = CraftResource.DullCopper;

					m_Bag.AddItem( kat );

					break;

				case TeiravonMobile.Class.Dragoon:
					Bardiche bd = new Bardiche();
					bd.Resource = CraftResource.DullCopper;

					m_Bag.AddItem( bd );

					break;

				case TeiravonMobile.Class.Cleric:
				case TeiravonMobile.Class.DarkCleric:
					Mace m = new Mace();
					m.Resource = CraftResource.DullCopper;

					m_Bag.AddItem( m );
					break;

				case TeiravonMobile.Class.Archer:
					CompositeBow bow = new CompositeBow();
					bow.Resource = CraftResource.Pine;

					m_Bag.AddItem( bow );
					armor = false;

					break;

				case TeiravonMobile.Class.Ranger:
					Longbow bow2 = new Longbow();
					bow2.Resource = CraftResource.Pine;

					m_Bag.AddItem( bow2 );
					armor = true;

					break;

				case TeiravonMobile.Class.Thief:
				case TeiravonMobile.Class.Assassin:
				case TeiravonMobile.Class.MageSlayer:
					Kryss krys = new Kryss();
					krys.Resource = CraftResource.DullCopper;

					m_Bag.AddItem( krys );

					armor = false;

					break;

				case TeiravonMobile.Class.Aeromancer:
				case TeiravonMobile.Class.Aquamancer:
				case TeiravonMobile.Class.Geomancer:
				case TeiravonMobile.Class.Pyromancer:
				case TeiravonMobile.Class.Necromancer:
					m_Bag.AddItem( new BlackStaff() );
					armor = false;

					break;

				case TeiravonMobile.Class.Forester:
				case TeiravonMobile.Class.Shapeshifter:
					m_Bag.AddItem( new GnarledStaff() );
					armor = false;

					break;





			}

			if ( armor )
				CreateArmor( m_Bag );

			m_Backpack.AddItem( m_Bag ); */
		}

		private static void CreateArmor( Container m_Cont )
		{
			RingmailArms arms = new RingmailArms();
			arms.Resource = CraftResource.Copper;

			RingmailChest chest = new RingmailChest();
			chest.Resource = CraftResource.Copper;

			RingmailGloves gloves = new RingmailGloves();
			gloves.Resource = CraftResource.Copper;

			RingmailLegs legs = new RingmailLegs();
			legs.Resource = CraftResource.Copper;

			m_Cont.AddItem( arms );
			m_Cont.AddItem( chest );
			m_Cont.AddItem( gloves );
			m_Cont.AddItem( legs );
		}

		public void AdvanceFeat( TeiravonMobile m_Player, TeiravonMobile.Feats newfeat )
		{
			TeiravonMobile.Feats[] m_Feats = m_Player.GetFeats();

			switch ( newfeat )
			{
				case TeiravonMobile.Feats.Marksmanship:
					m_Player.Skills.Archery.Base += 20.0;
					break;

				case TeiravonMobile.Feats.Blademaster:
					m_Player.Skills.Swords.Base += 20.0;
					break;

				case TeiravonMobile.Feats.MagicResistance:
					m_Player.Skills.MagicResist.Base += 30.0;
					break;

				case TeiravonMobile.Feats.ExpertMining:
					m_Player.Skills.Mining.Base += 20.0;
					break;
			}
		}
	}

	// Called Shot
/*	public class CalledShotGump : Gump
	{
		public CalledShotGump( TeiravonMobile m_Player ) : base( 175, 92 )
		{
			int x = 160;
			int y = 170;
			int x2 = 136;
			int y2 = 173;

			AddPage( 0 );
			AddBackground( 70, 80, 280, 325, 3600 );
			AddBackground( 95, 125, 230, 255, 9350 );

			AddHtml( 160, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Called Shot</basefont>", false, false );
			AddLabel( 110, 135, 150, "Choose your attack:" );

			AddLabel( x, y, 150, "Disarm Shot" );
			AddButton( x2, y2, 2224, 2224, 1, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Stun Shot" );
			AddButton( x2, y2+=30, 2224, 2224, 2, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Poison Shot" );
			AddButton( x2, y2+=30, 2224, 2224, 3, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Shadow Shot" );
			AddButton( x2, y2+=30, 2224, 2224, 4, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Crippling Shot" );
			AddButton( x2, y2+=30, 2224, 2224, 5, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Fatal Shot" );
			AddButton( x2, y2+=30, 2224, 2224, 6, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			TeiravonMobile m_Player = (TeiravonMobile) sender.Mobile;
			bool dofeat = false;

			if ( !( m_Player.Weapon is BaseRanged ) )
			{
				m_Player.SendMessage( "You must have a ranged weapon equipped." );
				return;
			} else if ( m_Player.GetActiveFeats( TeiravonMobile.Feats.CalledShot ) )
			{
				m_Player.SendMessage( "It's too soon to do this again." );
				return;
			}

			switch ( info.ButtonID )
			{
				case 1:
					m_Player.DisarmShotReady = true;
					dofeat = true;

					break;

				case 2:
					if ( m_Player.Skills.Archery.Base < 50 )
						m_Player.SendMessage( "You're not skilled enough to do that..." );
					else
					{
						m_Player.StunShotReady = true;
						dofeat = true;
					}

					break;

				case 3:
					Container pack = m_Player.Backpack;

					int Lpoison = pack.GetAmount( typeof( LesserPoisonPotion ) );
					int poison = pack.GetAmount( typeof( PoisonPotion ) );
					int Gpoison = pack.GetAmount( typeof( GreaterPoisonPotion ) );
					int Dpoison = pack.GetAmount( typeof( DeadlyPoisonPotion ) );

					if ( m_Player.Skills.Archery.Base < 60 || m_Player.Skills.Tactics.Base < 40 )
						m_Player.SendMessage( "You're not skilled enough to do that..." );
					else if ( ( Lpoison + poison + Gpoison + Dpoison ) == 0 )
						m_Player.SendMessage( "You must have at least one bottle of poison to do this." );
					else
					{
						m_Player.PoisonShotReady = true;
						dofeat = true;
					}

					break;

				case 4:
					if ( m_Player.Skills.Archery.Base < 80 || m_Player.Skills.Tactics.Base < 60 )
						m_Player.SendMessage( "You're not skilled enough to do that..." );
					else
					{
						m_Player.ShadowShotReady = true;
						dofeat = true;
					}

					break;

				case 5:
					if ( m_Player.Skills.Archery.Base < 90 || m_Player.Skills.Tactics.Base < 80 )
						m_Player.SendMessage( "You're not skilled enough to do that..." );
					else
					{
						m_Player.CripplingShotReady = true;
						dofeat = true;
					}

					break;

				case 6:
					if ( m_Player.Skills.Archery.Base < 100 || m_Player.Skills.Tactics.Base < 100 )
						m_Player.SendMessage( "You're not skilled enough to do that..." );
					else
					{
						m_Player.FatalShotReady = true;
						dofeat = true;
					}

					break;
			}

			if ( dofeat )
			{
				m_Player.SetActiveFeats( TeiravonMobile.Feats.CalledShot, true );

				// Reuse timer
				TimerHelper m_TimerHelper = new TimerHelper( (int)m_Player.Serial );
				m_TimerHelper.DoFeat = true;
				m_TimerHelper.Feat = TeiravonMobile.Feats.CalledShot;
				m_TimerHelper.Duration = TAVCoolDown.CalledShot;
				m_TimerHelper.Start();
			}
		}
	} */

	// Apprenticeship
	public class ApprenticeshipGump : Gump
	{
		public ApprenticeshipGump( TeiravonMobile m_Player ) : base( 175, 92 )
		{
			m_Player.CloseGump( typeof(ApprenticeshipGump) );

			Closable = false;
			Disposable = false;
			Resizable = false;

			int x = 160;
			int y = 170;
			int x2 = 136;
			int y2 = 173;

			AddPage( 0 );
			AddBackground( 70, 80, 280, 325, 3600 );
			AddBackground( 95, 125, 230, 255, 9350 );

			AddHtml( 160, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Apprenticeship</basefont>", false, false );
			AddLabel( 110, 135, 150, "Choose your Apprenticeship path:" );

			AddLabel( x, y, 150, "Camping" );
			AddButton( x2, y2, 2224, 2224, 1, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Cooking" );
			AddButton( x2, y2+=30, 2224, 2224, 2, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Healing" );
			AddButton( x2, y2+=30, 2224, 2224, 3, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Fishing" );
			AddButton( x2, y2+=30, 2224, 2224, 4, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Lumberjacking" );
			AddButton( x2, y2+=30, 2224, 2224, 5, GumpButtonType.Reply, 0 );

			AddLabel( x, y+=30, 150, "Mining" );
			AddButton( x2, y2+=30, 2224, 2224, 6, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			TeiravonMobile m_Player = (TeiravonMobile) sender.Mobile;
			bool dofeat = true;

			switch ( info.ButtonID)
			{
				case 1:
					m_Player.Skills.Camping.Base += 30.0;
					m_Player.ApprenticeSkill = "Camping";
					break;

				case 2:
					m_Player.Skills.Cooking.Base += 30.0;
					m_Player.ApprenticeSkill = "Cooking";
					break;

				case 3:
					m_Player.Skills.Healing.Base += 30.0;
					m_Player.ApprenticeSkill = "Healing";
					break;

				case 4:
					m_Player.Skills.Fishing.Base += 30.0;
					m_Player.ApprenticeSkill = "Fishing";
					break;

				case 5:
					if ( m_Player.IsAeromancer() || m_Player.IsAquamancer() || m_Player.IsGeomancer() || m_Player.IsPyromancer() || m_Player.IsNecromancer() )
						dofeat = false;
					else
					{
						m_Player.Skills.Lumberjacking.Base += 30.0;
						m_Player.ApprenticeSkill = "Lumberjacking";
					}

					break;

				case 6:
					if ( m_Player.IsAeromancer() || m_Player.IsAquamancer() || m_Player.IsGeomancer() || m_Player.IsPyromancer() || m_Player.IsNecromancer() )
						dofeat = false;
					else
					{
						m_Player.Skills.Mining.Base += 30.0;
						m_Player.ApprenticeSkill = "Mining";
					}

					break;
			}

			if ( dofeat )
			{
				Teiravon.Feats.Functions.CheckSkills( m_Player );

				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "As a youth, you picked up a useful tradeskill." );

				m_Player.AddFeat( TeiravonMobile.Feats.Apprenticeship );
				m_Player.RemainingFeats -= 1;
			} else {
				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You cannot learn this tradeskill." );
			}
		}
	}

	// Research
	public class ResearchGump : Gump
	{

		TeiravonMobile m_Player;

		private static Type[] m_AlchyScroll = new Type[]
		{
			typeof ( HealFormula ),
			typeof ( PoisonFormula ),
			typeof ( CureFormula ),
			typeof ( GreaterAgilityFormula ),
			typeof ( ExplosionFormula ),
			typeof ( GreaterStrengthFormula ),
			typeof ( LesserFloatFormula ),
			typeof ( SustenanceFormula ),
			typeof ( GenderSwapFormula ),
			typeof ( FloatFormula ),
			typeof ( GreaterSustenanceFormula ),
			typeof ( ChameleonFormula ),
			typeof ( ManaRefreshFormula ),
			typeof ( GreaterPoisonFormula ),
			typeof ( GreaterCureFormula ),
			typeof ( GreaterFloatFormula ),
			typeof ( GreaterRefreshFormula ),
			typeof ( GreaterHealFormula ),
			typeof ( InvisibilityFormula ),
			typeof ( MagicResistFormula ),
			typeof ( GreaterExplosionFormula ),
			typeof ( DeadlyPoisonFormula ),
			typeof ( TotalManaRefreshFormula ),
			typeof ( InvulnerabilityFormula )
		};

		private static string[] FormulaName = new string[]
		{
			"Heal",
			"Poison",
			"Cure",
			"Greater Agility",
			"Explosion",
			"Greater Strength",
			"Lesser Float",
			"Sustenance",
			"Gender Swap",
			"Float",
			"Greater Sustenance",
			"Chameleon",
			"Mana Refresh",
			"Greater Poison",
			"Greater Cure",
			"Greater Float",
			"Greater Refresh",
			"Greater Heal",
			"Invisibility",
			"Magic Resist",
			"Greater Explosion",
			"Deadly Poison",
			"Total Mana Refresh",
			"Invulnerability"
		};


		public ResearchGump( TeiravonMobile player ) : base( 175, 92 )
		{
			m_Player = player;
			m_Player.CloseGump( typeof(ResearchGump) );

			Closable = false;
			Disposable = false;
			Resizable = false;

			int x = 140;
			int y = 170;
			int x2 = 116;
			int y2 = 173;

			int m_list = m_Player.PlayerLevel + (int)(m_Player.PlayerLevel / 5);
			int m_l;

			if (m_list < 12)
				m_l = 12;
			else
				m_l = m_list;

			int addedy = ((int)((m_l - 12)/2)) * 30;

			AddPage( 0 );
			AddBackground( 70, 80, 460, 325 + addedy, 3600 );
			AddBackground( 95, 125, 410, 255 + addedy, 9350 );

			AddHtml( 200, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Alchemical Research</basefont>", false, false );
			AddLabel( 155, 135, 150, "Choose your primary researched formula:" );

			for ( int i = 0; i < m_list; i+=2 )
			{
				AddLabel( x, y+(15*i), 150, FormulaName[i] );
				AddButton( x2, y2+(15*i), 2224, 2224, i+1, GumpButtonType.Reply, 0 );

				AddLabel( x+200, y+(15*i), 150, FormulaName[i+1] );
				AddButton( x2+200, y2+(15*i), 2224, 2224, i+2, GumpButtonType.Reply, 0 );
			}
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{

			Container m_Backpack = m_Player.Backpack;

			if ( m_Backpack == null )
			{
				m_Player.SendMessage( "You have no backpack" );
				return;
			}

			if ( info.ButtonID >= 1 )
			{

				m_Backpack.DropItem( (Item)Activator.CreateInstance( m_AlchyScroll[info.ButtonID - 1] ) );
				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You find the formula you were researching." );
				m_Player.AddFeat( TeiravonMobile.Feats.Research );
				m_Player.RemainingFeats -= 1;
				m_Player.SendGump( new Teiravon.Feats.ResearchGump2( m_Player ) );
			}
		}
	}

	// Research Second Formula
	public class ResearchGump2 : Gump
	{
		TeiravonMobile m_Player;

		private static Type[] m_AlchyScroll = new Type[]
		{
			typeof ( HealFormula ),
			typeof ( PoisonFormula ),
			typeof ( CureFormula ),
			typeof ( GreaterAgilityFormula ),
			typeof ( ExplosionFormula ),
			typeof ( GreaterStrengthFormula ),
			typeof ( LesserFloatFormula ),
			typeof ( SustenanceFormula ),
			typeof ( GenderSwapFormula ),
			typeof ( FloatFormula ),
			typeof ( GreaterSustenanceFormula ),
			typeof ( ChameleonFormula ),
			typeof ( ManaRefreshFormula ),
			typeof ( GreaterPoisonFormula ),
			typeof ( GreaterCureFormula ),
			typeof ( GreaterFloatFormula ),
			typeof ( GreaterRefreshFormula ),
			typeof ( GreaterHealFormula ),
			typeof ( InvisibilityFormula ),
			typeof ( MagicResistFormula ),
			typeof ( GreaterExplosionFormula ),
			typeof ( DeadlyPoisonFormula ),
			typeof ( TotalManaRefreshFormula ),
			typeof ( InvulnerabilityFormula )
		};

		private static string[] FormulaName = new string[]
		{
			"Heal",
			"Poison",
			"Cure",
			"Greater Agility",
			"Explosion",
			"Greater Strength",
			"Lesser Float",
			"Sustenance",
			"Gender Swap",
			"Float",
			"Greater Sustenance",
			"Chameleon",
			"Mana Refresh",
			"Greater Poison",
			"Greater Cure",
			"Greater Float",
			"Greater Refresh",
			"Greater Heal",
			"Invisibility",
			"Magic Resist",
			"Greater Explosion",
			"Deadly Poison",
			"Total Mana Refresh",
			"Invulnerability"
		};


		public ResearchGump2( TeiravonMobile player ) : base( 175, 92 )
		{
			m_Player = player;

			Closable = false;
			Disposable = false;
			Resizable = false;

			int x = 140;
			int y = 170;
			int x2 = 116;
			int y2 = 173;

			int m_list = m_Player.PlayerLevel + (int)(m_Player.PlayerLevel / 5);
			int m_l;

			if (m_list < 12)
				m_l = 12;
			else
				m_l = m_list;

			int addedy = ((int)((m_l - 12)/2)) * 30;

			AddPage( 0 );
			AddBackground( 70, 80, 460, 325 + addedy, 3600 );
			AddBackground( 95, 125, 410, 255 + addedy, 9350 );

			AddHtml( 200, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Alchemical Research</basefont>", false, false );
			AddLabel( 155, 135, 150, "Choose your secondary researched formula:" );

			for ( int i = 0; i < m_list; i+=2 )
			{
				AddLabel( x, y+(15*i), 150, FormulaName[i] );
				AddButton( x2, y2+(15*i), 2224, 2224, i+1, GumpButtonType.Reply, 0 );

				AddLabel( x+200, y+(15*i), 150, FormulaName[i+1] );
				AddButton( x2+200, y2+(15*i), 2224, 2224, i+2, GumpButtonType.Reply, 0 );
			}
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Container m_Backpack = m_Player.Backpack;

			if ( m_Backpack == null )
			{
				m_Player.SendMessage( "You have no backpack and thus you lost your second formula. Page a GM." );
				return;
			}

			if ( info.ButtonID >= 1 )
			{
				m_Backpack.DropItem( (Item)Activator.CreateInstance( m_AlchyScroll[info.ButtonID - 1] ) );
				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You find the formula you were researching." );
			}
		}
	}


	// Weapon Specialization
	public class WeaponSpecGump : Gump
	{
		public WeaponSpecGump( TeiravonMobile m_Player ) : base( 175, 92 )
		{
			Closable = false;
			Disposable = false;
			Resizable = false;

			AddPage( 0 );
			AddBackground( 70, 80, 280, 245, 3600 );
			AddBackground( 95, 125, 230, 175, 9350 );

			AddHtml( 125, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">" + m_Player.PlayerRace.ToString() + " Weapon Specialization</basefont>", false, false );
			AddLabel( 110, 135, 150, "Choose your weapon training: " );

			AddLabel( 160, 170, 150, "Bladed Weapons" );
			AddButton( 136, 173, 2224, 2224, 1, GumpButtonType.Reply, 0 );

			AddLabel( 160, 200, 150, "Blunted Weapons" );
			AddButton( 136, 203, 2224, 2224, 2, GumpButtonType.Reply, 0 );

			AddLabel( 160, 230, 150, "Piercing Weapons" );
			AddButton( 136, 233, 2224, 2224, 3, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			TeiravonMobile m_Player = (TeiravonMobile) sender.Mobile;
			string GainedFeat = "You have gained the Weapon Specialization feat.";
			string ExceedFeat = "You have exceeded the skill limit for this feat.";

			switch ( info.ButtonID )
			{
				case 1:

					if ( m_Player.Skills.Swords.Base == m_Player.Skills.Swords.Cap )
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, ExceedFeat );
						break;
					}


					if ( m_Player.Skills.Swords.Base + 20.0 >= m_Player.Skills.Swords.Cap )
						m_Player.Skills.Swords.Base = m_Player.Skills.Swords.Cap;
					else
						m_Player.Skills.Swords.Base += 20;

					m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, GainedFeat );

					m_Player.AddFeat( TeiravonMobile.Feats.WeaponSpecialization );
					m_Player.RemainingFeats -= 1;

					break;


				case 2:

					if ( m_Player.Skills.Macing.Base == m_Player.Skills.Macing.Cap )
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, ExceedFeat );
						break;
					}


					if ( m_Player.Skills.Macing.Base + 20 >= m_Player.Skills.Macing.Cap )
						m_Player.Skills.Macing.Base = m_Player.Skills.Macing.Cap;
					else
						m_Player.Skills.Macing.Base += 20.0;

					m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, GainedFeat );

					m_Player.AddFeat( TeiravonMobile.Feats.WeaponSpecialization );
					m_Player.RemainingFeats -= 1;


					break;


				case 3:

					if ( m_Player.IsKensai() )
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You cannot specialize in this weapon." );
						break;
					}

/*					if ( m_Player.IsCavalier() )
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, "You cannot specialize in this weapon." );
						break;
					}
*/
					if ( m_Player.Skills.Fencing.Base == m_Player.Skills.Fencing.Cap )
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, ExceedFeat );
						break;
					}


					if ( m_Player.Skills.Fencing.Base + 20 >= m_Player.Skills.Fencing.Cap )
						m_Player.Skills.Fencing.Base = m_Player.Skills.Fencing.Cap;
					else
						m_Player.Skills.Fencing.Base += 20.0;



					m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, GainedFeat );

					m_Player.AddFeat( TeiravonMobile.Feats.WeaponSpecialization );
					m_Player.RemainingFeats -= 1;

					break;

			}
		}
	}

	// Weapon Mastery
	public class WeaponMasteryGump : Gump
	{
		public WeaponMasteryGump( TeiravonMobile m_Player ) : base( 175, 92 )
		{
			Closable = false;
			Disposable = false;
			Resizable = false;

			AddPage( 0 );
			AddBackground( 70, 80, 280, 245, 3600 );
			AddBackground( 95, 125, 230, 175, 9350 );

			AddHtml( 125, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">" + m_Player.PlayerRace.ToString() + " Weapon Specialization</basefont>", false, false );
			AddLabel( 110, 135, 150, "Choose your weapon training: " );

			AddLabel( 160, 170, 150, "Bladed Weapons" );
			AddButton( 136, 173, 2224, 2224, 1, GumpButtonType.Reply, 0 );

			AddLabel( 160, 200, 150, "Blunted Weapons" );
            AddButton( 136, 203, 2224, 2224, 2, GumpButtonType.Reply, 0 );

			AddLabel( 160, 230, 150, "Piercing Weapons" );
			AddButton( 136, 233, 2224, 2224, 3, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			TeiravonMobile m_Player = (TeiravonMobile) sender.Mobile;
			string GainedFeat = "You have gained the Weapon Mastery feat.";
			string ExceedFeat = "You have exceeded the skill limit for this feat.";

			bool dofeat = true;

			switch ( info.ButtonID )
			{
				case 1:
					if ( m_Player.Skills.Swords.Base < 81.0 )
						m_Player.Skills.Swords.Base += 20.0;
					else
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, ExceedFeat );
						dofeat = false;
					}

					break;

				case 2:
					if ( m_Player.Skills.Macing.Base < 81.0 )
						m_Player.Skills.Macing.Base += 20.0;
					else
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, ExceedFeat );
						dofeat = false;
					}

					break;

				case 3:
					if ( m_Player.Skills.Fencing.Base < 81.0 )
						m_Player.Skills.Fencing.Base += 20.0;
					else
					{
						m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, ExceedFeat );
						dofeat = false;
					}

					break;
			}

			if ( dofeat )
			{
				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, GainedFeat );

				m_Player.AddFeat( TeiravonMobile.Feats.WeaponMastery );
				m_Player.RemainingFeats -= 1;
			}
		}
	}
	
	public class WpnMasterGump : Gump
	{

		TeiravonMobile m_Player;

		public WpnMasterGump( Mobile from ) : base( 0, 0 )
		{
			m_Player = (TeiravonMobile) from;
		
			//m_Player.CloseGump( typeof( WpnMasterGump ) );

			int x = 140;
			int y = 140;
			int x2 = 116;
			int y2 = 143;
			int i = 1;

			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			AddPage( 0 );
			AddBackground( 70, 80, 350, 325, 3600 );
			AddBackground( 95, 125, 300, 255, 9350 );

			AddHtml( 200, 100, 600, 20, "<basefont size=\"8\" color=\"#ffffff\">Weapon Master</basefont>", false, false );
			AddLabel( 155, 135, 150, "Choose your style to master:" );

			AddLabel( x, y+(20*i), 150, "Axes" );
			AddButton( x2, y2+(20*i), 2224, 2224, 1, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Slashing" );
			AddButton( x2, y2+(20*i), 2224, 2224, 2, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Staves" );
			AddButton( x2, y2+(20*i), 2224, 2224, 3, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Bashing" );

            if (!m_Player.IsRavager())
			    AddButton( x2, y2+(20*i), 2224, 2224, 4, GumpButtonType.Reply, 0 );

			i ++;
			AddLabel( x, y+(20*i), 150, "Piercing" );
			AddButton( x2, y2+(20*i), 2224, 2224, 5, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Polearms" );
			AddButton( x2, y2+(20*i), 2224, 2224, 6, GumpButtonType.Reply, 0 );
			i ++;
			AddLabel( x, y+(20*i), 150, "Bows" );

            if (!m_Player.IsRavager())
			    AddButton( x2, y2+(20*i), 2224, 2224, 7, GumpButtonType.Reply, 0 );

			i ++;
			AddLabel( x, y+(20*i), 150, "Thrown" );
            if (!m_Player.IsRavager())
			    AddButton( x2, y2+(20*i), 2224, 2224, 8, GumpButtonType.Reply, 0 );
			
		}
		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			if ( info.ButtonID >= 1 )
			{
				switch(info.ButtonID)
				{
					case 1:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Axe;
						break;
					case 2:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Slashing;
						break;
					case 3:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Staff;
						break;
					case 4:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Bashing;
						break;
					case 5:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Piercing;
						break;
					case 6:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Polearm;
						break;
					case 7:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Ranged;
						break;
					case 8:
						m_Player.MasterWeapon = TeiravonMobile.MasterWeapons.Thrown;
						break;
				}

				m_Player.RemainingFeats -= 1;
			}
		}
	}
}
