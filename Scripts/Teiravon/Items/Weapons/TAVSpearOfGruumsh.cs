using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF62, 0xF63 )]
	public class SpearOfGruumsh : BaseSpear
	{
	
		public bool SpearOfGruumshHit;
	
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 120; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 30; } }
		public override int AosSpeed{ get{ return 32; } }

		public override int OldStrengthReq{ get{ return 120; } }
		public override int OldMinDamage{ get{ return 20; } }
		public override int OldMaxDamage{ get{ return 30; } }
		public override int OldSpeed{ get{ return 32; } }

		public override int InitMinHits{ get{ return 300; } }
		public override int InitMaxHits{ get{ return 400; } }


		[Constructable]
		public SpearOfGruumsh() : base( 0xF62 )
		{
			Name = "Spear of Gruumsh";
			Weight = 50.0;
			Hue = 0x09BA;
			Attributes.WeaponDamage = 35;
			WeaponAttributes.HitLeechHits = 10;
			WeaponAttributes.ResistColdBonus = 15;
			WeaponAttributes.ResistEnergyBonus = 15;
			WeaponAttributes.ResistFireBonus = 15;
			WeaponAttributes.ResistPhysicalBonus = 15;
			WeaponAttributes.ResistPoisonBonus = 15;
			WeaponAttributes.SelfRepair = 5;			
		}



		public override void OnHit( Mobile attacker, Mobile defender )
		{
			SpearOfGruumshHit = false;
		
		if (!defender.Paralyzed) { 
			base.OnHit( attacker, defender );
			}
		
		if (Utility.RandomDouble() < .5 && !defender.Paralyzed)
			{
			attacker.SendMessage ( "You have paralyzed " + defender.Name );
			defender.Paralyze( TimeSpan.FromSeconds( 10.0 ) );
			defender.SendMessage( "The Spear of Gruumsh has paralyzed you!" );
			defender.FixedEffect( 0x376A, 5, 10 );
			SpearOfGruumshHit = true;
			}
			
		if (defender.Paralyzed && !SpearOfGruumshHit) { 
			base.OnHit( attacker, defender ); 
			}	
			
		}
		

		public SpearOfGruumsh( Serial serial ) : base( serial )
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