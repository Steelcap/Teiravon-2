using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a warg corpse" )]
	public class Warg : BaseMount
	{

		[Constructable]
		public Warg() : this( "a Warg" )
		{
		}

		[Constructable]
		public Warg( string name ) : base( name, 277, 16017, AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
			Body = 277;
			ItemID = 16017;
			Hue = 2312;
			BaseSoundID = 0xE5;
			Level = 8;

			SetStr( 121, 140 );
			SetDex( 87, 123 );
			SetInt( 16, 40 );

			SetHits( 140, 160 );
			SetMana( 0 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 5, 15 );

			SetSkill( SkillName.MagicResist, 57.6, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

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
						from.SendMessage( "Only an orc can subdue a Warg." );
						return;
					}
				}
				else
				{
					if ( ((TeiravonMobile)from).IsOrc() )
						base.OnDoubleClick( from );
					else
						from.SendMessage( "Only an orc can ride a Warg." );


					return;
				}
			}

			base.OnDoubleClick( from );
		}

		public Warg( Serial serial ) : base( serial )
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
