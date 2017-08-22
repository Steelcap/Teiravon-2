using System;
using Server;
using Server.Items;
using Server.Teiravon;

namespace Server.Mobiles
{
	public class GrizzlyFamiliar : BaseCreature
	{
		[Constructable]
		public GrizzlyFamiliar() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "Grizzly Bear";
			Body = Teiravon.Mobiles.GrizzlyBear;

			SetStr( Utility.RandomMinMax( 50, 70 ) );
			SetDex( Utility.RandomMinMax( 40, 50 ) );
			SetInt( Utility.RandomMinMax( 30, 40 ) );

			SetDamage( 8, 12 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Energy, 0 );
			SetDamageType( ResistanceType.Poison, 0 );

			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 50 );
			SetResistance( ResistanceType.Poison, 0 );
			SetResistance( ResistanceType.Energy, 0 );

			SetSkill( SkillName.Tactics, 40.0, 50.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			VirtualArmor = 20;
		}

		public GrizzlyFamiliar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}