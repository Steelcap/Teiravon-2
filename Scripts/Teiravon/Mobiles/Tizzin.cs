using System;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	[CorpseName( "a tizzin corpse" )]
	public class Tizzin : BaseMount
	{
		public override int Meat{ get{ return 3; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Ostard; } }
		public override bool InitialInnocent{ get{ return true; } }
		
		public override void GetContextMenuEntries( Mobile from, ArrayList list )
		{
			if ( !( from is TeiravonMobile ) )
				return;
			
			if ( ((TeiravonMobile)from).IsDrow() || from.AccessLevel > AccessLevel.Player )
				base.GetContextMenuEntries( from, list );
		}
		
		public override double GetControlChance( Mobile m )
		{
			if ( !( m is TeiravonMobile ) )
				return base.GetControlChance( m );
			
			if ( ((TeiravonMobile)m).IsDrow() || m.AccessLevel > AccessLevel.Player )
				return 100.0;
			else
			{
				m.SendMessage( "You're unable to control this creature." );
				return 0.0;
			}
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( from is TeiravonMobile )
			{
				if (!this.Controled )
				{
					if ( ((TeiravonMobile)from).IsDrow() )
					{
						if ( !(from.Followers + this.ControlSlots > from.FollowersMax) )
						{
							this.ControlMaster = from;
							this.Controled = true;
							this.ControlTarget = from;
							this.ControlOrder = OrderType.Follow;
						}
						else
						{
							from.SendMessage( "You cannot control any more creatures." );
							return;
						}
					}
					else
					{
						from.SendMessage( "Only an Drow can subdue this lizzard." );
						return;
					}
				}
				else
				{
					if ( ((TeiravonMobile)from).IsDrow() )
						base.OnDoubleClick( from );
					else
						from.SendMessage( "Only a Drow can ride this lizzard." );


					return;
				}
			}

			base.OnDoubleClick( from );
		}
		
		[Constructable]
		public Tizzin() : this( "a tizzin" )
		{
		}

		[Constructable]
		public Tizzin( string name ) : base( name, 0xDA, 0x3EA4, AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0x275;
			Hue = Utility.RandomMinMax( 0x450, 0x455 );
			Level = 3;

			SetStr( 22, 98 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 28, 45 );
			SetMana( 0 );

			SetDamage( 3, 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );

			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 300;
			Karma = 300;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 75.0;
		}

		public Tizzin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
}

