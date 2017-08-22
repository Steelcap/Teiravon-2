using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a horngoth corpse" )]
	public class Horngoth : BaseMount
	{

		[Constructable]
		public Horngoth() : this( "a horngoth" )
		{
		}

		[Constructable]
		public Horngoth( string name ) : base( name, 793, 0x3EBB, AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Body = 0x319;
			ItemID = 0x3EBB;
			Hue = 0xA25;
			BaseSoundID = 0xA8;

			SetStr( 142, 186 );
			SetDex( 87, 123 );
			SetInt( 30, 50 );

			SetHits( 200, 230 );
			SetMana( 0 );

			SetDamage( 3, 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 60 );

			SetSkill( SkillName.MagicResist, 40.1, 50.0 );
			SetSkill( SkillName.Tactics, 60.3, 70.0 );
			SetSkill( SkillName.Wrestling, 50.3, 70.0 );

			Fame = 3000;
			Karma = 3000;

			Tamable = false;
			ControlSlots = 2;
			MinTameSkill = 120.1;
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is TeiravonMobile )
			{
				if (!this.Controled )
				{
					if ( ((TeiravonMobile)from).IsOrc() )
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
						from.SendMessage( "Only an orc can subdue a horngoth." );
						return;
					}
				}
				else
				{
					if ( ((TeiravonMobile)from).IsOrc() )
						base.OnDoubleClick( from );
					else
						from.SendMessage( "Only an orc can ride a horngoth." );


					return;
				}
			}

			base.OnDoubleClick( from );
		}

		public Horngoth( Serial serial ) : base( serial )
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