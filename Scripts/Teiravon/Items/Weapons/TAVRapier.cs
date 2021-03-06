using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

	public class Rapier : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.DoubleStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 12; } }
		public override int AosSpeed{ get{ return 50; } }

		public override int DefHitSound{ get{ return 0x23C; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public Rapier() : base( 0x13B8 )
		{
			Weight = 2.0;
			Name = "Cavalry Blade";
		}
/*
		public override bool CanEquip( Mobile from )
		{
			if (from is TeiravonMobile)
			{
				TeiravonMobile m_Player = (TeiravonMobile)from;

				if ( !m_Player.IsCavalier() )
				{
					from.SendMessage( "Only cavaliers can use that.");
					return false;
				}
			}

			return base.CanEquip( from );
		}
*/
		public Rapier( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 2.0;
			if ( Name == "Rapier")
				Name = "Cavalry Blade";
		}
	}
}
