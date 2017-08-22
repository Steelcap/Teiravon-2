using System;
using Server;
using Server.Network;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseTool : Item, IUsesRemaining
	{
		private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		public bool ShowUsesRemaining{ get{ return true; } set{} }

		public abstract CraftSystem CraftSystem{ get; }

		public BaseTool( int itemID ) : this( 50, itemID )
		{
		}

		public BaseTool( int uses, int itemID ) : base( itemID )
		{
			m_UsesRemaining = uses;
		}

		public BaseTool( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~
		}

		public virtual void DisplayDurabilityTo( Mobile m )
		{
			LabelToAffix( m, 1017323, AffixType.Append, ": " + m_UsesRemaining.ToString() ); // Durability
		}

		public static bool CheckAccessible( Item tool, Mobile m )
		{
			return ( tool.IsChildOf( m ) || tool.Parent == m );
		}

		public static bool CheckTool( Item tool, Mobile m )
		{
			Item check = m.FindItemOnLayer( Layer.OneHanded );

			if ( check is BaseTool && check != tool && !(check is AncientSmithyHammer) )
				return false;

			check = m.FindItemOnLayer( Layer.TwoHanded );

			if ( check is BaseTool && check != tool && !(check is AncientSmithyHammer) )
				return false;

			return true;
		}

		public override void OnSingleClick( Mobile from )
		{
			DisplayDurabilityTo( from );

			base.OnSingleClick( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) || Parent == from )
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				CraftSystem system = this.CraftSystem;

				if ( !m_Player.IsTinker() )
				{
					if ( system == DefAlchemy.CraftSystem || system == DefGlassblowing.CraftSystem )
					{
						if ( !m_Player.IsAlchemist() && !m_Player.HasFeat( TeiravonMobile.Feats.AlchemyScience ) && !m_Player.HasFeat( TeiravonMobile.Feats.ExoticPoisons) )
						{
							m_Player.SendMessage( "You don't have the skills to use this." );
							return;
						}

						if (!m_Player.HasFeat(TeiravonMobile.Feats.ExoticPoisons))
						{
							if ( m_Player.Backpack.FindItemByType( typeof( AlchemyTome ) ) == null )
							{
								m_Player.SendMessage( "Where's your Alchemy Tome?" );
								return;
							}
						}
					}
					else if ( system == DefBlacksmithy.CraftSystem )
					{
						if ( !m_Player.IsBlacksmith() && !m_Player.IsTinker() && !m_Player.IsMerchant() )
						{
							m_Player.SendMessage( "You don't have the skills to use this." );
							return;
						}
					}
					else if ( system == DefBowFletching.CraftSystem )
					{
                        if (!m_Player.IsWoodworker() && !m_Player.IsRanger() && !m_Player.IsArcher() && !m_Player.IsMageSlayer() && !m_Player.IsMerchant() && !m_Player.IsStrider())
						{
							m_Player.SendMessage( "You don't have the skills to use this." );
							return;
						}
					}
					else if ( system == DefCarpentry.CraftSystem )
					{
                        if (!m_Player.IsWoodworker() && !m_Player.IsMerchant())
						{
							m_Player.SendMessage( "You don't have the skills to use this." );
							return;
						}
					}
					else if ( system == DefInscription.CraftSystem )
					{
                        if (!m_Player.HasFeat(TeiravonMobile.Feats.Inscription) && !m_Player.IsAlchemist() && !m_Player.IsMerchant())
						{
							m_Player.SendMessage( "You're not sure how to do this." );
							return;
						}
					}
					else if ( system == DefTailoring.CraftSystem )
					{
                        if (!m_Player.IsTailor() && !m_Player.IsMerchant())
						{
							m_Player.SendMessage( "You don't have the skills to use this." );
							return;
						}
					}
				}

				int num = system.CanCraft( from, this, null );

				if ( num > 0 )
				{
					from.SendLocalizedMessage( num );
				}
				else
				{
					CraftContext context = system.GetContext( from );

					from.SendGump( new CraftGump( from, system, this, null ) );
				}
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_UsesRemaining = reader.ReadInt();
					break;
				}
			}
		}
	}
}
