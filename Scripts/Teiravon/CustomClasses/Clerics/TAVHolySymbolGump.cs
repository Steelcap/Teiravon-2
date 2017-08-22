using System;
using Server;
using System.Collections;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Teiravon;

namespace Server.Gumps
{
	public class HolySymbolGump : Gump
	{

		TeiravonMobile m_Player;
		Pitcher m_Found;
		HolySymbol m_HolySymbol;

		public HolySymbolGump( Mobile from, Item hsymbol ) : base( 0, 0 )
		{

		m_Player = (TeiravonMobile) from;
		m_HolySymbol = (HolySymbol) hsymbol;
		m_Player.CloseGump( typeof( HolySymbolGump ) );

		this.Closable=true;
		this.Disposable=true;
		this.Dragable=true;
		this.Resizable=false;
		this.AddPage(0);
		this.AddImageTiled(91, 100, 232, 145, 3504);
		this.AddAlphaRegion(88, 97, 236, 150);

		this.AddImageTiled(87, 245, 236, 3, 9157);
		this.AddImageTiled(322, 98, 4, 149, 9153);
		this.AddImageTiled(87, 98, 4, 149, 9153);
		this.AddImageTiled(87, 97, 236, 3, 9157);

			if ( m_Player.IsCleric() || m_Player.IsPaladin() )
			{
				int page = 1;
				int number = 0;
				bool feat1 = false;
				bool feat2 = false;
				bool feat3 = false;

				AddPage(page);

				if ( m_Player.HasFeat( TeiravonMobile.Feats.TurnUndead ) )
				{
					ListAbility( number, 1, "Turn Undead", false, page);
					number++;
					feat1 = true;
				}
				if ( m_Player.HasFeat( TeiravonMobile.Feats.BestowBlessing ) )
				{
					ListAbility( number, 2, "Bestow Blessing", true, page);
					number++;
					feat2 = true;
				}
				if ( m_Player.HasFeat( TeiravonMobile.Feats.DivineMight ) )
				{
					ListAbility( number, 3, "Divine Might", false, page);
					number++;
					feat3 = true;
				}
				if (!feat1 && !feat2 && !feat3)
				{
					AddLabel(110, 160, 2930, "The symbol appears to be blank.");
				}
				else
				{
					AddLabel(103, 104, 2930, @"Holy abilities");
					HolySymbols();
				}

				page++;
				AddPage(page);
				AddLabel(103,104, 2930, @"Bestow Blessing");
				HolySymbols();

				ListAbility(0, 4, "Prepare Holy Item", true, page);
				ListAbility(1, 5, "Bless Item", false, page);

				page++;
				AddPage(page);
				HolySymbols();

				AddLabel(103, 104, 2930, @"Prepare Holy Item");
				ListAbility(0, 6, "Holy Water", false, page);

				if (m_Player.PlayerLevel >= 5)
					ListAbility(1, 7, "Holy Robe", false, page);
				else
					ListAbility(1, 9, "Unavailable", false, page);

                if (m_Player.PlayerLevel >= 7 && m_Player.HasFeat(TeiravonMobile.Feats.HealersOath))
                    ListAbility(3, 10, "Holy Staff", false, page);
                else
                    ListAbility(3, 9, "Unavailable", false, page);

				if (m_Player.PlayerLevel >= 12 && !m_Player.HasFeat(TeiravonMobile.Feats.HealersOath))
					ListAbility(2, 8, "Holy Mace", false, page);

				else
					ListAbility(2, 9, "Unavailable", false, page);





			}

			else if ( m_Player.IsDarkCleric() )
			{
				int page = 1;
				int number = 0;

				bool feat1 = false;
				bool feat2 = false;
				bool feat3 = false;

				AddPage(page);

				if ( m_Player.HasFeat( TeiravonMobile.Feats.RebukeUndead ) )
				{
					ListAbility( number, 1, "Rebuke Undead", false, page);
					number++;
					feat1 = true;
				}
				if ( m_Player.HasFeat( TeiravonMobile.Feats.BestowDarkBlessing ) )
				{
					ListAbility( number, 2, "Bestow Dark Blessing", true, page);
					number++;
					feat2 = true;
				}
				if ( m_Player.HasFeat( TeiravonMobile.Feats.UnholyMight ) )
				{
					ListAbility( number, 3, "Unholy Might", false, page);
					number++;
					feat3 = true;
				}

				if (!feat1 && !feat2 && !feat3)
				{
					AddLabel(110, 160, 2930, "The symbol appears to be blank.");
				}
				else
				{
					AddLabel(103, 104, 2930, @"Dark cleric abilities");
					UnholySymbols();
				}



				page++;
				AddPage(page);
				AddLabel(103,104, 2930, @"Bestow Dark Blessing");
				UnholySymbols();

				ListAbility(0, 4, "Prepare Unholy Item", true, page);
				ListAbility(1, 5, "Bless Item", false, page);


				page++;
				AddPage(page);
				UnholySymbols();

				AddLabel(103, 104, 2930, @"Prepare Unholy Item");
				ListAbility(0, 6, "Unholy Water", false, page);

				if (m_Player.PlayerLevel >= 5)
					ListAbility(1, 7, "Unholy Robe", false, page);
				else
					ListAbility(1, 9, "Unavailable", false, page);

                if (m_Player.PlayerLevel >= 12 && !m_Player.HasFeat(TeiravonMobile.Feats.HealersOath))
					ListAbility(2, 8, "Unholy Mace", false, page);

				else
					ListAbility(2, 9, "Unavailable", false, page);

                if (m_Player.PlayerLevel >= 7 && m_Player.HasFeat(TeiravonMobile.Feats.HealersOath))
                    ListAbility(3, 10, "Unholy Staff", false, page);
                else
                    ListAbility(3, 9, "Unavailable", false, page);


			}
			else if ( m_Player.IsNecromancer() )
			{
				int page = 1;
				int number = 0;

				bool feat1 = false;

				AddPage(page);

				if ( m_Player.HasFeat( TeiravonMobile.Feats.RebukeUndead ) )
				{
					ListAbility( number, 1, "Rebuke Undead", false, page);
					number++;
					feat1 = true;
				}

				if (!feat1)
				{
					AddLabel(110, 160, 2930, "The symbol appears to be blank.");
				}
				else
				{
					AddLabel(103, 104, 2930, @"Necromancer abilities");
					UnholySymbols();
				}
			}

			else
			{
				AddLabel(110, 160, 2930, "The symbol appears to be inert.");
			}


		}

		private void HolySymbols()
		{
			AddImage(262, 105, 111);
		}

		private void UnholySymbols()
		{
			AddImage(276, 113, 113);
		}


		private void ListAbility( int index, int type, string label, bool nextpage, int page )
		{

			AddPage(page);
			AddLabel(125, 137+(index*30), 5, label );

			if (!nextpage)
				AddButton(103, 140+(index*30), 1210, 1209, type, GumpButtonType.Reply, 0);
			else
				AddButton(103, 140+(index*30), 1210, 1209, type, GumpButtonType.Page, page + 1);
		}


		private void CreateHolyWater( TeiravonMobile crafter )
		{
			crafter.SendMessage( "You bless some water and pour it into a vial." );

			if (crafter.Backpack != null)
				crafter.AddToBackpack( new HolyWater( crafter ) );

			DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes( 15 );

			m_HolySymbol.SaveCraftTime( NextCraft );
		}

		private void CreateUnholyWater( TeiravonMobile crafter )
		{
			crafter.SendMessage( "You curse some water and pour it into a vial." );

			if (crafter.Backpack != null)
				crafter.AddToBackpack( new UnholyWater( crafter ) );

			DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes( 15 );

			m_HolySymbol.SaveCraftTime( NextCraft );
		}


		private void CreateHolyRobe( TeiravonMobile crafter )
		{

			crafter.SendMessage( "You bless the robe with holy water." );

			if (crafter.Backpack != null)
				crafter.AddToBackpack( new HolyRobe( crafter.PlayerLevel ) );

			DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes( 60 );

			m_HolySymbol.SaveCraftTime( NextCraft );

		}

		private void CreateUnholyRobe( TeiravonMobile crafter )
		{

			crafter.SendMessage( "You taint the robe with unholy water." );

			if (crafter.Backpack != null)
				crafter.AddToBackpack( new UnholyRobe( crafter.PlayerLevel ) );

			DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes( 60 );

			m_HolySymbol.SaveCraftTime( NextCraft );

		}

		private void CreateHolyMace( TeiravonMobile crafter )
		{

			crafter.SendMessage( "You place a powerful blessing on the mace." );

			if (crafter.Backpack != null)
				crafter.AddToBackpack( new HolyMace( crafter.PlayerLevel ) );

			DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes( 180 );

			m_HolySymbol.SaveCraftTime( NextCraft );

		}

		private void CreateUnholyMace( TeiravonMobile crafter )
		{

			crafter.SendMessage( "You place a powerful curse on the mace." );

			if (crafter.Backpack != null)
				crafter.AddToBackpack( new UnholyMace( crafter.PlayerLevel ) );

			DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes( 180 );

			m_HolySymbol.SaveCraftTime( NextCraft );

		}

        private void CreateHolyStaff(TeiravonMobile crafter)
        {

            crafter.SendMessage("You place a powerful blessing on the staff.");

            if (crafter.Backpack != null)
                crafter.AddToBackpack(new HolyStaff(crafter.PlayerLevel));

            DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes(180);

            m_HolySymbol.SaveCraftTime(NextCraft);

        }

        private void CreateUnholyStaff(TeiravonMobile crafter)
        {

            crafter.SendMessage("You place a powerful curse on the staff.");

            if (crafter.Backpack != null)
                crafter.AddToBackpack(new UnholyStaff(crafter.PlayerLevel));

            DateTime NextCraft = DateTime.Now + TimeSpan.FromMinutes(180);

            m_HolySymbol.SaveCraftTime(NextCraft);

        }


		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{

		switch ( info.ButtonID )
					{
						case 0:

							if ( m_Player.IsCleric() || m_Player.IsDarkCleric() )
								m_Player.SendMessage( "You stop channeling your divine powers." );

							break;

						case 1:

							if ( m_Player.IsCleric() || m_Player.IsPaladin() )
							{
								m_Player.SendMessage( "Target a creature." );
								m_Player.Target = new TurnUndeadTarget( m_Player, m_HolySymbol );
							}

							else if ( m_Player.IsDarkCleric() || m_Player.IsNecromancer() )
							{
								m_Player.SendMessage( "Target a creature." );
								m_Player.Target = new ControlUndeadTarget( m_Player, m_HolySymbol );
							}

							break;

						case 2:

							break;

						case 3:

                            if (m_Player.IsCleric() || m_Player.IsPaladin())
							{
								if ( m_Player.GetActiveFeats( TeiravonMobile.Feats.DivineMight) )
								{
									m_Player.SendMessage( "You are already affected by that ability!" );
									break;
								}
								if ( m_Player.Mana < 70 )
								{
									m_Player.SendMessage( "You need at least 70 mana to use that.");
									break;
								}

								m_Player.SendMessage( 5, "The divine might flows through you." );
								m_Player.Mana -= 70;
								m_Player.FixedParticles( 0x373A, 1, 15, 5012, 3, 2, EffectLayer.Waist );

								TimerHelper m_TimerHelper = new TimerHelper( (int)m_Player.Serial );
								m_TimerHelper.DoFeat = true;
								m_TimerHelper.Feat = TeiravonMobile.Feats.DivineMight;
								m_TimerHelper.Duration = TimeSpan.FromSeconds( 60 );
								m_TimerHelper.MessageOn = true;
								m_TimerHelper.Message = "You feel the divine might leaving your body.";
								m_TimerHelper.Start();

								m_Player.SetActiveFeats( TeiravonMobile.Feats.DivineMight, true );
							}


							else if ( m_Player.IsDarkCleric() )
							{

								if ( m_Player.GetActiveFeats( TeiravonMobile.Feats.UnholyMight) )
								{
									m_Player.SendMessage( "You are already affected by that ability!" );
									break;
								}
								if ( m_Player.Mana < 70 )
								{
									m_Player.SendMessage( "You need at least 70 mana to use that.");
									break;
								}

								m_Player.SendMessage( 438, "The unholy might flows through you." );
								m_Player.Mana -= 70;
								m_Player.FixedParticles( 0x375A, 1, 30, 9966, 33, 2, EffectLayer.Head );
								m_Player.FixedParticles( 0x37B9, 1, 30, 9502, 43, 3, EffectLayer.Head );

								TimerHelper m_TimerHelper = new TimerHelper( (int)m_Player.Serial );
								m_TimerHelper.DoFeat = true;
								m_TimerHelper.Feat = TeiravonMobile.Feats.UnholyMight;
								m_TimerHelper.Duration = TimeSpan.FromSeconds( 60 );
								m_TimerHelper.MessageOn = true;
								m_TimerHelper.Message = "You feel the unholy might leaving your body.";
								m_TimerHelper.Start();

								m_Player.SetActiveFeats( TeiravonMobile.Feats.UnholyMight, true );

							}

							break;

						case 5:


							DateTime checkbless = m_HolySymbol.NextBless();

							TimeSpan blesstime = checkbless - DateTime.Now;

							if ( blesstime > TimeSpan.Zero )
							{
								if ( blesstime.TotalSeconds > 60)
									m_Player.SendMessage( "You will have to wait {0} minutes before you can bless another item.", (int)(blesstime.TotalMinutes + 1) );
								else
									m_Player.SendMessage( "You will have to wait {0} seconds before you can bless another item.", (int)(blesstime.TotalSeconds) );

								break;
							}

							HolyWater m_HolyWaterFound = null;
							UnholyWater m_UnholyWaterFound = null;

							if ( m_Player.IsCleric() || m_Player.IsPaladin() )
							{

								if (m_Player.Backpack != null)
								{
									bool quantity = false;

									Item[] m_Water = m_Player.Backpack.FindItemsByType( typeof( HolyWater ), true );

									for (int i=0; i < m_Water.Length; i++)
									{
										HolyWater bottle = m_Water[i] as HolyWater;

										if (bottle == null)
											continue;

										if (bottle.Amount < 5)
											continue;

										if (bottle.Amount >= 5)
										{
											quantity = true;
											m_HolyWaterFound = bottle;
											break;
										}
									}

									if ( quantity && m_HolyWaterFound != null )
									{
										m_Player.SendMessage( "Target an item." );
										m_Player.Target = new BlessItemTarget( m_Player, m_HolySymbol, true, m_HolyWaterFound );
									}
									else
										m_Player.SendMessage( "You need more holy water." );
								}
							}


							else if ( m_Player.IsDarkCleric() )
							{
								if (m_Player.Backpack != null)
								{

									bool quantity = false;

									Item[] m_Water = m_Player.Backpack.FindItemsByType( typeof( UnholyWater ), true );

									for (int i=0; i < m_Water.Length; i++)
									{
										UnholyWater bottle = m_Water[i] as UnholyWater;

										if (bottle == null)
											continue;

										if (bottle.Amount < 5)
											continue;

										if (bottle.Amount >= 5)
										{
											quantity = true;
											m_UnholyWaterFound = bottle;
											break;
										}
									}

									if ( quantity && m_UnholyWaterFound != null )
									{
										m_Player.SendMessage( "Target an item." );
										m_Player.Target = new BlessItemTarget( m_Player, m_HolySymbol, false, m_UnholyWaterFound );
									}
									else
										m_Player.SendMessage( "You need more unholy water." );
								}
							}

							break;

						case 6:


							DateTime checkprepare = m_HolySymbol.NextCraft();

							TimeSpan preparetime = checkprepare - DateTime.Now;

							if ( preparetime > TimeSpan.Zero )
							{
								if ( preparetime.TotalSeconds > 60)
									m_Player.SendMessage( "You will have to wait {0} minutes before you can prepare another item.", (int)(preparetime.TotalMinutes + 1) );

								else
									m_Player.SendMessage( "You will have to wait {0} seconds before you can prepare another item.", (int)(preparetime.TotalSeconds) );

								break;
							}

							if (m_Player.IsCleric() || m_Player.IsDarkCleric() || m_Player.IsPaladin() )
							{
								bool pitcher = false;
								bool pitcherquantity = false;
								bool water = false;

								Map map = m_Player.Map;

								if ( map != null )
								{
									IPooledEnumerable eable = map.GetItemsInRange( m_Player.Location, 3 );

									foreach ( Item item in eable )
									{

										if (item is IWaterSource)
										{
											water = true;
											break;
										}
									}

										eable.Free();
								}


								if (m_Player.Backpack != null && !water)
								{

									Item[] m_Pitcher = (m_Player.Backpack.FindItemsByType( typeof( Pitcher ), true ));

									for (int i=0; i < m_Pitcher.Length; i++ )
									{
										Pitcher holypitcher = m_Pitcher[i] as Pitcher;

										if ( holypitcher == null )
											continue;

										if ( holypitcher.Content == BeverageType.Water )
										{
											pitcher = true;

											if ( holypitcher.Quantity >= 5 )
											{
												holypitcher.Quantity = 0;
												pitcherquantity = true;
												break;
											}
										}
									}
								}


								if (!pitcher && !water)
								{
									m_Player.SendMessage( "You need a fresh water source or a filled pitcher.");
								}
								else if (water)
								{
                                    if (m_Player.IsCleric() || m_Player.IsPaladin())
										CreateHolyWater( m_Player );
									else
										CreateUnholyWater( m_Player );
								}
								else if (pitcher)
								{
									if ( pitcherquantity )
									{
                                        if (m_Player.IsCleric() || m_Player.IsPaladin())
											CreateHolyWater( m_Player );
										else
											CreateUnholyWater( m_Player );
									}
									else
										m_Player.SendMessage( "You need a full pitcher." );
								}
							}


							break;

							case 7:

								DateTime checkrobeprepare = m_HolySymbol.NextCraft();

								TimeSpan robepreparetime = checkrobeprepare - DateTime.Now;

								if ( robepreparetime > TimeSpan.Zero )
								{
									if ( robepreparetime.TotalSeconds > 60)
										m_Player.SendMessage( "You will have to wait {0} minutes before you can prepare another item.", (int)(robepreparetime.TotalMinutes + 1) );

									else
										m_Player.SendMessage( "You will have to wait {0} seconds before you can prepare another item.", (int)(robepreparetime.TotalSeconds) );

									break;
								}

								HolyWater m_HolyWaterFound2 = null;
								UnholyWater m_UnholyWaterFound2 = null;


                                if (m_Player.IsCleric() || m_Player.IsPaladin())
								{
									if (m_Player.Backpack != null)
									{
										bool quantity = false;
										bool quality = false;

										Item[] m_Water = m_Player.Backpack.FindItemsByType( typeof( HolyWater ), true );

										for (int i=0; i < m_Water.Length; i++)
										{
											HolyWater bottle = m_Water[i] as HolyWater;

											if (bottle == null)
												continue;

											if (bottle.Amount < 10)
												continue;

											if (bottle.Amount >= 10)
											{
												quantity = true;

												if ( (int)(bottle.PotionStrength) >= 2 )
												{
													quality = true;
													m_HolyWaterFound2 = bottle;
													break;
												}
												else
													quality = false;
											}
										}


										Item m_FindRobe = m_Player.Backpack.FindItemByType( typeof( Robe ), true );

										if ( ( m_FindRobe != null ) && ( m_HolyWaterFound2 != null ) && ( quantity ) && ( quality ) )
										{
											m_FindRobe.Delete();

											if (m_HolyWaterFound2.Amount == 10)
												m_HolyWaterFound2.Delete();
											else
												m_HolyWaterFound2.Amount -= 10;

											CreateHolyRobe( m_Player );
										}
										else
										{
											if (m_FindRobe == null)
												m_Player.SendMessage( "You need a robe." );

											if ( !quantity && quality )
												m_Player.SendMessage( "You need more holy water." );

											if ( quantity && !quality )
												m_Player.SendMessage( "Your holy water isn't potent enough to create that." );

											if ( !quantity && !quality )
												m_Player.SendMessage( "You need more holy water of blessed or better quality." );

										}
									}
								}


								else if (m_Player.IsDarkCleric())
								{
									if (m_Player.Backpack != null)
									{
										bool quantity = false;
										bool quality = false;

										Item[] m_Water = m_Player.Backpack.FindItemsByType( typeof( UnholyWater ), true );

										for (int i=0; i < m_Water.Length; i++)
										{
											UnholyWater bottle = m_Water[i] as UnholyWater;

											if (bottle == null)
												continue;

											if (bottle.Amount < 10)
												continue;

											if (bottle.Amount >= 10)
											{
												quantity = true;

												if ( (int)(bottle.PotionStrength) >= 2 )
												{
													quality = true;
													m_UnholyWaterFound2 = bottle;
													break;
												}
												else
													quality = false;
											}
										}

										Item m_FindRobe = m_Player.Backpack.FindItemByType( typeof( Robe ), true );

										if ( ( m_FindRobe != null ) && ( m_UnholyWaterFound2 != null ) && ( quantity ) && ( quality ) )
										{
											m_FindRobe.Delete();

											if (m_UnholyWaterFound2.Amount == 10)
												m_UnholyWaterFound2.Delete();
											else
												m_UnholyWaterFound2.Amount -= 10;

											CreateUnholyRobe( m_Player );
										}
										else
										{
											if (m_FindRobe == null)
												m_Player.SendMessage( "You need a robe." );

											if ( !quantity && quality )
												m_Player.SendMessage( "You need more unholy water." );

											if ( quantity && !quality )
												m_Player.SendMessage( "Your unholy water isn't potent enough to create that." );

											if ( !quantity && !quality )
												m_Player.SendMessage( "You need more unholy water of cursed or better quality." );
										}
									}
								}

								break;

							case 8:

								DateTime checkmaceprepare = m_HolySymbol.NextCraft();

								TimeSpan macepreparetime = checkmaceprepare - DateTime.Now;

								if ( macepreparetime > TimeSpan.Zero )
								{
									if ( macepreparetime.TotalSeconds > 60)
										m_Player.SendMessage( "You will have to wait {0} minutes before you can prepare another item.", (int)(macepreparetime.TotalMinutes + 1) );

									else
										m_Player.SendMessage( "You will have to wait {0} seconds before you can prepare another item.", (int)(macepreparetime.TotalSeconds) );

									break;
								}

								HolyWater m_HolyWaterFound3 = null;
								UnholyWater m_UnholyWaterFound3 = null;


                                if (m_Player.IsCleric() || m_Player.IsPaladin())
								{
									if (m_Player.Backpack != null)
									{

										bool quantity = false;

										Item[] m_Water = m_Player.Backpack.FindItemsByType( typeof( HolyWater ), true );

										for (int i=0; i < m_Water.Length; i++)
										{
											HolyWater bottle = m_Water[i] as HolyWater;

											if (bottle == null)
												continue;

											if (bottle.Amount < 15)
												continue;

											if (bottle.Amount >= 15)
											{
												if ( (int)(bottle.PotionStrength) == 4 )
												{
													quantity = true;
													m_HolyWaterFound3 = bottle;
													break;
												}
											}
										}


										Item m_FindMace = m_Player.Backpack.FindItemByType( typeof( Mace ), true );

										if ( ( m_FindMace != null ) && ( m_HolyWaterFound3 != null ) && ( quantity ) )
										{
											m_FindMace.Delete();

											if (m_HolyWaterFound3.Amount == 15)
												m_HolyWaterFound3.Delete();
											else
												m_HolyWaterFound3.Amount -= 15;

											CreateHolyMace( m_Player );
										}
										else
										{
											if (m_FindMace == null)
												m_Player.SendMessage( "You need a mace." );

											if ( !quantity )
												m_Player.SendMessage( "You need more divine holy water." );
										}
									}
								}


								else if (m_Player.IsDarkCleric())
								{
									if (m_Player.Backpack != null)
									{

										bool quantity = false;

										Item[] m_Water = m_Player.Backpack.FindItemsByType( typeof( UnholyWater ), true );

										for (int i=0; i < m_Water.Length; i++)
										{
											UnholyWater bottle = m_Water[i] as UnholyWater;

											if (bottle == null)
												continue;

											if (bottle.Amount < 15)
												continue;

											if (bottle.Amount >= 15)
											{
												if ( (int)(bottle.PotionStrength) == 4 )
												{
													quantity = true;
													m_UnholyWaterFound3 = bottle;
													break;
												}
											}
										}

										Item m_FindMace = m_Player.Backpack.FindItemByType( typeof( Mace ), true );

										if ( ( m_FindMace != null ) && ( m_UnholyWaterFound3 != null ) && ( quantity ) )
										{
											m_FindMace.Delete();

											if (m_UnholyWaterFound3.Amount == 15)
												m_UnholyWaterFound3.Delete();
											else
												m_UnholyWaterFound3.Amount -= 15;

											CreateUnholyMace( m_Player );
										}
										else
										{
											if (m_FindMace == null)
												m_Player.SendMessage( "You need a mace." );

											if ( !quantity )
												m_Player.SendMessage( "You need more tainted unholy water." );
										}
									}
								}

								break;

							case 9:
								m_Player.SendMessage( "You cannot craft that yet.");
								break;

                            case 10:

                                DateTime checkStaffprepare = m_HolySymbol.NextCraft();

                                TimeSpan Staffpreparetime = checkStaffprepare - DateTime.Now;

                                if (Staffpreparetime > TimeSpan.Zero)
                                {
                                    if (Staffpreparetime.TotalSeconds > 60)
                                        m_Player.SendMessage("You will have to wait {0} minutes before you can prepare another item.", (int)(Staffpreparetime.TotalMinutes + 1));

                                    else
                                        m_Player.SendMessage("You will have to wait {0} seconds before you can prepare another item.", (int)(Staffpreparetime.TotalSeconds));

                                    break;
                                }

                                HolyWater m_HolyWaterFound4 = null;
                                UnholyWater m_UnholyWaterFound4 = null;


                                if (m_Player.IsCleric() || m_Player.IsPaladin())
                                {
                                    if (m_Player.Backpack != null)
                                    {

                                        bool quantity = false;
                                        string quality = "";
                                        Item[] m_Water = m_Player.Backpack.FindItemsByType(typeof(HolyWater), true);

                                        for (int i = 0; i < m_Water.Length; i++)
                                        {
                                            HolyWater bottle = m_Water[i] as HolyWater;

                                            if (bottle == null)
                                                continue;

                                            if (bottle.Amount < 15)
                                                continue;

                                            if (bottle.Amount >= 15)
                                            {

                                                switch (m_Player.PlayerLevel)
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                    case 7:
                                                        {
                                                            if ((int)(bottle.PotionStrength) == 1)
                                                            {
                                                                quantity = true;
                                                                quality = " Light";
                                                                m_HolyWaterFound4 = bottle;
                                                                break;
                                                            }
                                                            break;
                                                        }
                                                    case 8:
                                                    case 9:
                                                    case 10:
                                                    case 11:
                                                    case 12:
                                                        {
                                                            if ((int)(bottle.PotionStrength) == 2)
                                                            {
                                                                quantity = true;
                                                                quality = " Blessed";
                                                                m_HolyWaterFound4 = bottle;
                                                                break;
                                                            }
                                                            break;
                                                        }
                                                    case 13:
                                                    case 14:
                                                    case 15:
                                                    case 16:
                                                    case 17:
                                                    case 18:
                                                    case 19:
                                                    case 20:
                                                    case 21:
                                                    case 22:
                                                    case 23:
                                                    case 24:
                                                    case 25:
                                                        {
                                                            if ((int)(bottle.PotionStrength) >= 3)
                                                            {
                                                                quantity = true;
                                                                quality = " Holy";
                                                                m_HolyWaterFound4 = bottle;
                                                                break;
                                                            }
                                                            break;
                                                        }
                                                }
                                                
                                                /*
                                                if ((int)(bottle.PotionStrength) == 4)
                                                {
                                                    quantity = true;
                                                    m_HolyWaterFound4 = bottle;
                                                    break;
                                                }
                                                */
                                            }
                                        }


                                        Item m_FindStaff = m_Player.Backpack.FindItemByType(typeof(BaseStaff), true);

                                        if ((m_FindStaff != null) && (m_HolyWaterFound4 != null) && (quantity))
                                        {
                                            m_FindStaff.Delete();

                                            if (m_HolyWaterFound4.Amount == 15)
                                                m_HolyWaterFound4.Delete();
                                            else
                                                m_HolyWaterFound4.Amount -= 15;

                                            CreateHolyStaff(m_Player);
                                        }
                                        else
                                        {
                                            if (m_FindStaff == null)
                                                m_Player.SendMessage("You need a Staff.");

                                            if (!quantity)
                                                m_Player.SendMessage("You need more{0} holy water.", quality);
                                        }
                                    }
                                }


                                else if (m_Player.IsDarkCleric())
                                {
                                    if (m_Player.Backpack != null)
                                    {

                                        bool quantity = false;
                                        string quality = "";
                                        Item[] m_Water = m_Player.Backpack.FindItemsByType(typeof(UnholyWater), true);

                                        for (int i = 0; i < m_Water.Length; i++)
                                        {
                                            UnholyWater bottle = m_Water[i] as UnholyWater;

                                            if (bottle == null)
                                                continue;

                                            if (bottle.Amount < 15)
                                                continue;

                                            if (bottle.Amount >= 15)
                                            {
                                                switch (m_Player.PlayerLevel)
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                    case 7:
                                                        {
                                                            if ((int)(bottle.PotionStrength) == 1)
                                                            {
                                                                quantity = true;
                                                                quality = " Foul";
                                                                m_UnholyWaterFound4 = bottle;
                                                                break;
                                                            }
                                                            break;
                                                        }
                                                    case 8:
                                                    case 9:
                                                    case 10:
                                                    case 11:
                                                    case 12:
                                                        {
                                                            if ((int)(bottle.PotionStrength) == 2)
                                                            {
                                                                quantity = true;
                                                                quality = " Cursed";
                                                                m_UnholyWaterFound4 = bottle;
                                                                break;
                                                            }
                                                            break;
                                                        }
                                                    case 13:
                                                    case 14:
                                                    case 15:
                                                        {
                                                            if ((int)(bottle.PotionStrength) == 3)
                                                            {
                                                                quantity = true;
                                                                quality = " Unholy";
                                                                m_UnholyWaterFound4 = bottle;
                                                                break;
                                                            }
                                                            break;
                                                        }
                                                    case 16:
                                                    case 17:
                                                    case 18:
                                                    case 19:
                                                    case 20:
                                                    case 21:
                                                    case 22:
                                                    case 23:
                                                    case 24:
                                                    case 25:
                                                        {
                                                            if ((int)(bottle.PotionStrength) == 4)
                                                            {
                                                                quantity = true;
                                                                quality = " Tainted";
                                                                m_UnholyWaterFound4 = bottle;
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                }
                                            }
                                        }

                                        Item m_FindStaff = m_Player.Backpack.FindItemByType(typeof(BaseStaff), true);

                                        if ((m_FindStaff != null) && (m_UnholyWaterFound4 != null) && (quantity))
                                        {
                                            m_FindStaff.Delete();

                                            if (m_UnholyWaterFound4.Amount == 15)
                                                m_UnholyWaterFound4.Delete();
                                            else
                                                m_UnholyWaterFound4.Amount -= 15;

                                            CreateUnholyStaff(m_Player);
                                        }
                                        else
                                        {
                                            if (m_FindStaff == null)
                                                m_Player.SendMessage("You need a Staff.");

                                            if (!quantity)
                                                m_Player.SendMessage("You need more{0} unholy water.", quality);
                                        }
                                    }
                                }

                                break;
					}



		}


	}
}


namespace Server.Targets
{

	public class BlessItemTarget : Target
	{
		private TeiravonMobile m_Player;
		private HolySymbol m_HolySymbol;
		private bool m_Cleric;
		private Item m_HolyWater;

		private BaseWeapon m_WeaponTarget;
		private Item m_ItemTarget;


		private bool cleric;

		public BlessItemTarget( TeiravonMobile from, HolySymbol hsymbol, bool cleric, Item holywater) : base( -1, false, TargetFlags.None )
		{
			m_Player = from;
			m_HolySymbol = hsymbol;
			m_Cleric = cleric;
			m_HolyWater = holywater;

		}

		protected override void OnTarget( Mobile from, object targ )
		{


			if (targ is BaseWeapon )
			{
				m_WeaponTarget = (BaseWeapon)targ;

				if ( m_WeaponTarget.IsChildOf( from.Backpack ) || m_WeaponTarget.Parent == from  )
				{

					if (m_Cleric)
					{
						if ( m_WeaponTarget.Slayer != SlayerName.None )
							m_Player.SendMessage( "The weapon is already blessed/enhanced.");
						else
						{
							DateTime NextBless = DateTime.Now + TimeSpan.FromMinutes( 120 );
							m_HolySymbol.SaveBlessTime( NextBless );

							m_WeaponTarget.Slayer = SlayerName.Silver;
							m_Player.SendMessage( "You have blessed the weapon.");

							//Consumes holy water
							if (m_HolyWater.Amount == 5)
								m_HolyWater.Delete();
							else
								m_HolyWater.Amount -= 5;

							m_HolySymbol.RemoveBless( m_WeaponTarget, true );
						}
					}
					else
					{
						if ( m_WeaponTarget.WeaponAttributes.HitLeechHits >= 10 )
						{
							m_Player.SendMessage( "The weapon already drains lifeforce from opponents!" );
						}
						else
						{
							DateTime NextBless = DateTime.Now + TimeSpan.FromMinutes( 120 );
							m_HolySymbol.SaveBlessTime( NextBless );

							m_WeaponTarget.Attributes.WeaponDamage += 25;
							m_WeaponTarget.WeaponAttributes.HitLeechHits += 10;

							m_Player.SendMessage( "You have placed a dark blessing on the weapon." );

							//Consumes unholy water
							if (m_HolyWater.Amount == 5)
								m_HolyWater.Delete();
							else
								m_HolyWater.Amount -= 5;

							m_HolySymbol.RemoveBless( m_WeaponTarget, false );
						}
					}
				}
				else
					m_Player.SendMessage( "You must have the weapon in your pack." );
			}
			else if (targ is Item)
			{
				m_ItemTarget = (Item)targ;

				if ( m_ItemTarget.IsChildOf( from.Backpack ) || m_ItemTarget.Parent == from  )
				{

					if (m_ItemTarget.LootType != LootType.Regular )
						m_Player.SendMessage( "The item already has a blessing.");
					else
					{
						DateTime NextBless = DateTime.Now + TimeSpan.FromMinutes( 120 );
						m_HolySymbol.SaveBlessTime( NextBless );

						m_ItemTarget.LootType = LootType.Blessed;
						m_Player.SendMessage( "You have blessed the item.");

						//Consumes holy water or unholy water

						if (m_HolyWater.Amount == 5)
							m_HolyWater.Delete();
						else
							m_HolyWater.Amount -= 5;

						m_HolySymbol.RemoveBless( m_ItemTarget, true );
					}
				}
				else
					m_Player.SendMessage( "You must have the item in your pack." );
			}
			else
				m_Player.SendMessage( "That is not an item you can bless." );

		}
	}


	public class TurnUndeadTarget : Target
	{
		private TeiravonMobile m_Player;
		private BaseCreature m_Creature;
		private Type[] m_Undead;
		private bool undead;
		private HolySymbol m_HolySymbol;

		public TurnUndeadTarget( Mobile from, HolySymbol hsymbol ) : base( 5, false, TargetFlags.Harmful )
		{
			m_Player = (TeiravonMobile)from;
			m_HolySymbol = hsymbol;
		}

		protected override void OnTarget( Mobile from, object targ )
		{

			if (targ is TeiravonMobile)
			{
                TeiravonMobile m_TavTarg = targ as TeiravonMobile;

                if (!m_TavTarg.IsUndead())
                    m_Player.SendMessage("You cannot use the skill on mortals.");
                else if (!m_Player.CanBeHarmful(m_TavTarg))
                    m_Player.SendMessage("You cannot use the skill.");
                else if (m_Player.Mana < (0.1 * (m_TavTarg.Int)))
                    m_Player.SendMessage("You need at least {0} mana to attempt that.", (int)(0.1 * (m_TavTarg.Int)));
                else if (m_TavTarg.IsUndead())
                {
                    DateTime check = m_HolySymbol.NextTurn();
                    TimeSpan ts = check - DateTime.Now;

                    if (ts > TimeSpan.Zero)
                    {
                        m_Player.SendMessage("You have not recovered from your previous attempt yet.");
                    }
                    else
                    {
                        DateTime NextTurn = DateTime.Now + TimeSpan.FromSeconds(60);
                        m_HolySymbol.SaveTurnTime(NextTurn);

                        m_Player.Mana -= (int)(0.1 * (m_TavTarg.Int));

                        m_Player.SendMessage("You start channeling your powers at the creature.");
                        m_Player.Paralyze(TimeSpan.FromSeconds(4.5));

                        DateTime m_TurnTime = DateTime.Now + TimeSpan.FromSeconds(5.0);
                        Timer m_Timer = new TurnTimer(m_Player, m_TavTarg, m_TurnTime);
                        m_Timer.Start();

                    }

                }

			}
			else if (targ is BaseCreature)
			{
				m_Creature = (BaseCreature)targ;

				if ( !m_Player.CanSee( m_Creature ) || !m_Player.InLOS( m_Creature ) )
					m_Player.SendLocalizedMessage( 502800 ); // You can't see that.

				else if ( m_Player.Mana < ( 0.1 * m_Creature.Int ) )
					m_Player.SendMessage( "You need at least {0} mana to attempt that.", (int)(0.1 * m_Creature.Int) );

				else if ( m_Creature.IsUndeadNPC() )
				{

					DateTime check = m_HolySymbol.NextTurn();
					TimeSpan ts = check - DateTime.Now;

					if ( ts > TimeSpan.Zero )
					{
						m_Player.SendMessage( "You have not recovered from your previous attempt yet." );
					}
					else
					{
						DateTime NextTurn = DateTime.Now + TimeSpan.FromSeconds( 60 );
						m_HolySymbol.SaveTurnTime( NextTurn );

						m_Player.Mana -= (int)( 0.1 * m_Creature.Int );

						m_Player.SendMessage( "You start channeling your powers at the creature.");
						m_Player.Paralyze( TimeSpan.FromSeconds( 4.5 ) );

						DateTime m_TurnTime = DateTime.Now + TimeSpan.FromSeconds( 5.0 );
						Timer m_Timer = new TurnTimer( m_Player, m_Creature, m_TurnTime );
						m_Timer.Start();

					}

				}
				else
					m_Player.SendMessage( "The skill has no effect on that creature." );
			}

			else
				m_Player.SendMessage( "You cannot use the skill on that." );

		}

		private class TurnTimer : Timer
		{
			private TeiravonMobile m_Turner;
			private Mobile m_Targ;
            private BaseCreature m_Target;
            private TeiravonMobile m_Teiravon;

			public TurnTimer( TeiravonMobile from, Mobile target, DateTime end ) : base( end - DateTime.Now )
			{
				m_Turner = from;
				m_Targ = target;
			}

			protected override void OnTick()
			{
                if (m_Targ is TeiravonMobile)
                {
                     m_Teiravon = m_Targ as TeiravonMobile;

                    int diff = m_Turner.PlayerLevel - m_Teiravon.PlayerLevel;
                    if (diff < -3)
                         diff = -3;

                    m_Teiravon.Damage((int)(m_Teiravon.Hits * (.2 + .05 * diff)), m_Turner);
                    Effects.SendTargetParticles(m_Teiravon, 0x37C3, 20, 20, 2555, 0, 0, EffectLayer.Waist, 1);
                    Effects.SendMovingParticles(m_Turner, m_Teiravon, 0x377A, 3, 10, false, false, 0, 2, 0, 0, -1, 0);
                    Effects.PlaySound(m_Teiravon.Location, m_Teiravon.Map, 0x595);

                }

                else
                {
                m_Target = m_Targ as BaseCreature;
				if ( ( m_Turner.PlayerLevel - m_Target.Level ) <= -5 )
				{
					m_Target.Damage( Utility.RandomMinMax(1, m_Turner.PlayerLevel * 3 ), m_Turner );
					m_Turner.SendMessage( "The creature is mildly damaged by your attempt." );
				}
				else if ( ( m_Turner.PlayerLevel - m_Target.Level ) <= -2 )
				{
					if (Utility.RandomBool())
					{
						m_Target.Damage( Utility.RandomMinMax(4, m_Turner.PlayerLevel * 4 ), m_Turner );
						m_Turner.SendMessage( "The creature is damaged by your attempt." );
					}

					else
					{
						m_Target.BeginFlee( TimeSpan.FromSeconds( 10 ) );
						m_Target.Emote( "*flees*" );
						m_Turner.SendMessage( "The creature begins to flee." );
					}
				}
				else if ( Math.Abs( m_Turner.PlayerLevel - m_Target.Level ) < 2 )
				{
						m_Target.BeginFlee( TimeSpan.FromSeconds( 10 ) );
						m_Target.Emote( "*flees in terror*" );
						m_Target.Damage( Utility.RandomMinMax(4, m_Turner.PlayerLevel * 4), m_Turner );
						m_Turner.SendMessage( "The creature flees in terror." );

				}
				else if ( ( ( m_Turner.PlayerLevel - m_Target.Level ) >= 2 ) && ( ( m_Turner.PlayerLevel - m_Target.Level ) < 5 ) )
				{

					if (Utility.RandomBool())
					{
						m_Target.BeginFlee( TimeSpan.FromSeconds( 10 ) );
						m_Target.Emote( "*flees in terror*" );
						m_Target.Damage( Utility.RandomMinMax(4, m_Turner.PlayerLevel * 4), m_Turner );
						m_Turner.SendMessage( "The creature flees in terror." );
					}
					else
					{
						m_Turner.SendMessage( "You banish the creature." );
						m_Target.Damage( m_Target.Hits + 1, m_Turner );
					}
				}
				else if ( ( m_Turner.PlayerLevel - m_Target.Level ) >= 5 )
				{
					m_Turner.SendMessage( "You banish the creature." );
					m_Target.Damage( m_Target.Hits + 1, m_Turner );
				}

			    }
            }
		}
	}

	public class ControlUndeadTarget : Target
	{
		private TeiravonMobile m_Player;
		private BaseCreature m_Creature;
		private Type[] m_Undead;
		private bool undead;
		private HolySymbol m_HolySymbol;

		public ControlUndeadTarget( Mobile from, HolySymbol hsymbol ) : base( 5, false, TargetFlags.Harmful )
		{
			m_Player = (TeiravonMobile)from;
			m_HolySymbol = hsymbol;
		}

		protected override void OnTarget( Mobile from, object targ )
		{

			if (targ is PlayerMobile)
			{
				m_Player.SendMessage( "You cannot control players." );
			}
			else if (targ is BaseCreature)
			{

				m_Creature = (BaseCreature)targ;
				bool alreadyOwned = m_Creature.Owners.Contains( m_Player );
                
                if (m_Creature.ControlSlots == 1)
                    m_Creature.ControlSlots = TAVUtilities.CalculateLevel(m_Creature) / 15;

				if ( !m_Player.CanSee( m_Creature ) || !m_Player.InLOS( m_Creature ) )
					m_Player.SendLocalizedMessage( 502800 ); // You can't see that.

				else if ( undead && alreadyOwned )
					m_Player.SendMessage( "You already control that creature.");

				else if ( m_Creature.Controled )
					m_Player.SendMessage( "That creature is already being controlled!" );

				else if ( m_Player.Mana < ( 0.1 * m_Creature.Int ) )
					m_Player.SendMessage( "You need at least {0} mana to attempt that.", (int)(0.1 * m_Creature.Int) );

				else if ( m_Creature.IsUndeadNPC() )
				{
					if ( m_Player.Followers + m_Creature.ControlSlots > m_Player.FollowersMax )
					{
						m_Player.SendMessage( "You cannot control more creatures." );
					}

					else
					{

						if ( ( m_Player.PlayerLevel > ( m_Creature.Level + 1 ) ) && ( m_Creature.Level != 0 ) )
						{

							DateTime check = m_HolySymbol.NextControl();
							TimeSpan ts = check - DateTime.Now;

							if ( ts > TimeSpan.Zero )
							{
								m_Player.SendMessage( "You have not recovered from your previous attempt yet." );
							}
							else
							{
								DateTime NextControl = DateTime.Now + TimeSpan.FromSeconds( 10 );
								m_HolySymbol.SaveControlTime( NextControl );

								m_Player.Mana -= (int)( 0.1 * m_Creature.Int );

								m_Player.SendMessage( "You start channeling the power to control the creature." );
								m_Player.Paralyze( TimeSpan.FromSeconds( 4.5 ) );

								DateTime m_ControlTime = DateTime.Now + TimeSpan.FromSeconds( 5.0 );
								Timer m_Timer = new ControlTimer( m_Player, m_Creature, m_ControlTime );
								m_Timer.Start();
							}
						}
						else
							m_Player.SendMessage( "The will of the creature is too strong for you to control.");
					}
				}
				else
					m_Player.SendMessage( "You cannot control that kind of creature.");
			}
			else
				m_Player.SendMessage( "You cannot control that.");

		}

		private class ControlTimer : Timer
		{
			private TeiravonMobile m_Rebuker;
			private BaseCreature m_Target;

			public ControlTimer( TeiravonMobile from, BaseCreature target, DateTime end ) : base( end - DateTime.Now )
			{
				m_Rebuker = from;
				m_Target = target;
			}

			protected override void OnTick()
			{
				bool success = GetChance( m_Rebuker, m_Target );

				if ( success )
				{
					if ( m_Target.Alive )
					{
						m_Target.Owners.Add( m_Rebuker );
						m_Target.SetControlMaster( m_Rebuker );
						m_Target.IsBonded = false;
                        Spells.Necromancy.AnimateDeadSpell.Register(m_Rebuker, m_Target);
						m_Rebuker.SendMessage( "You gain control of the creature. ");
					}
					else
						m_Rebuker.SendMessage( "You fail to gain control of the creature because it has died." );
				}
				else
					m_Rebuker.SendMessage( "Your attempt to control the creature failed." );
			}

            private bool GetChance(TeiravonMobile player, BaseCreature creature)
            {
                if (player.PlayerLevel > TAVUtilities.CalculateLevel(creature))
                    return true;

                if (player.PlayerLevel >= Utility.Random(TAVUtilities.CalculateLevel(creature) + 10))
                    return true;

                return false;
            }
            /*
			private bool GetChance( TeiravonMobile player, BaseCreature creature )
			{
				if ( player.PlayerLevel > ( creature.Level + 6 ) )
					return true;

				if ( player.PlayerLevel > ( creature.Level + 4 ) )
				{
					if ( Utility.RandomMinMax(1, 10) != 10 )
						return true;

					return false;
				}

				if ( player.PlayerLevel > ( creature.Level + 2 ) )
				{
					if ( Utility.RandomMinMax(1, 2) == 2 )
						return true;

					return false;
				}

				if ( Utility.RandomMinMax(1, 4) == 4 )
					return false;

				return true;

			}
            */
		}

	}

}

