using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	
	public interface IMagicDrain
	{
		int DrainIntensity{ get; set; }
	}

	
	[CorpseName( "a gremlin's corpse" )]
	public class Gremlin : BaseCreature, IMagicDrain
	{
		#region Magic Drain
		private int m_DrainIntensity;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrainIntensity
		{
			get{ return m_DrainIntensity; }
			set{ m_DrainIntensity = value;}
		}

		#endregion

		[Constructable]
		public Gremlin() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Gremlin";
			Body = 240;
			BaseSoundID = 437;
			Level = 8;
			DrainIntensity = 10;

			SetStr( 125, 150 );
			SetDex( 81, 100 );
			SetInt( 36, 60 );

			SetHits( 108, 182 );

			SetDamage( 6, 7 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 35.1, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 75.0 );
			SetSkill( SkillName.Wrestling, 50.1, 75.0 );

			Fame = 1750;
			Karma = -1750;

			VirtualArmor = 28;

			PackArmor(1, 2, 0.01);
			if (Utility.RandomMinMax(1,100) < 2)
				PackItem( (Item)Activator.CreateInstance( Server.LootPack.AlchyScroll[Utility.RandomMinMax(1,20)] ) );
		}

		public override void GenerateLoot()
		{
			int loot = Utility.RandomMinMax(1,100);
			if (loot < 61)
				AddLoot( LootPack.Poor );
			else if (loot < 91 && loot > 60)
				AddLoot( LootPack.AosMeager );
			else if (loot > 90 && loot < 97)
				AddLoot( LootPack.AosAverage );
			else
				AddLoot(LootPack.AosRich );
			// TODO: weapon, misc
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		
		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if (Utility.RandomMinMax(1,100) < 11)
				reflect = true; // Every spell is reflected back to the caster
		}
		

		public Gremlin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int)m_DrainIntensity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			int m_DrainIntensity = reader.ReadInt();
		}
	}

	[CorpseName( "a gremlin's corpse" )]
	public class GremlinWarrior : BaseCreature, IMagicDrain
	{
		#region Magic Drain
		private int m_DrainIntensity;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrainIntensity
		{
			get{ return m_DrainIntensity; }
			set{ m_DrainIntensity = value;}
		}

		#endregion

		[Constructable]
		public GremlinWarrior() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Gremlin Warrior";
			Body = 245;
			BaseSoundID = 437;
			Level = 12;
			DrainIntensity = 4;

			SetStr( 175, 250 );
			SetDex( 101, 200 );
			SetInt( 36, 60 );

			SetHits( 120, 250 );

			SetDamage( 14, 18 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 30, 45 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 45 );
			SetResistance( ResistanceType.Energy, 50, 70 );

			SetSkill( SkillName.MagicResist, 55.1, 90.0 );
			SetSkill( SkillName.Tactics, 100.1, 115.0 );
			SetSkill( SkillName.Wrestling, 90.1, 120.0 );

			Fame = 1750;
			Karma = -1750;

			VirtualArmor = 28;
			
			PackArmor(1, 3, 0.01);
			if (Utility.RandomMinMax(1,100) < 2)
				PackItem( (Item)Activator.CreateInstance( Server.LootPack.AlchyScroll[Utility.RandomMinMax(1,10)] ) );

		}

		public override void GenerateLoot()
		{
			int loot = Utility.RandomMinMax(1,100);
			if (loot < 61)
				AddLoot( LootPack.Poor );
			else if (loot < 91 && loot > 60)
				AddLoot( LootPack.AosMeager );
			else 
				AddLoot( LootPack.AosAverage );
			// TODO: weapon, misc
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		
		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if (Utility.RandomMinMax(1,100) < 31)
				reflect = true; // Every spell is reflected back to the caster
		}

		public GremlinWarrior( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int)m_DrainIntensity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			int m_DrainIntensity = reader.ReadInt();
		}
	}

	[CorpseName( "a gremlin's corpse" )]
	public class GremlinPriest : BaseCreature, IMagicDrain
	{
		#region Magic Drain
		private int m_DrainIntensity;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrainIntensity
		{
			get{ return m_DrainIntensity; }
			set{ m_DrainIntensity = value;}
		}

		#endregion

		[Constructable]
		public GremlinPriest() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Gremlin Priest";
			Body = 255;
			BaseSoundID = 437;
			Level = 14;
			DrainIntensity = 6;

			SetStr( 130, 150 );
			SetDex( 80, 120 );
			SetInt( 180, 220 );

			SetHits( 120, 150 );

			SetDamage( 9, 11 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 20, 35 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 35 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.MagicResist, 55.1, 90.0 );
			SetSkill( SkillName.Tactics, 80.1, 95.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 1750;
			Karma = -1750;

			VirtualArmor = 28;
			
			PackArmor(2, 4, 0.01);
			if (Utility.RandomMinMax(1,100) < 2)
				PackItem( (Item)Activator.CreateInstance( Server.LootPack.AlchyScroll[Utility.RandomMinMax(10,25)] ) );

		}

		public override void GenerateLoot()
		{
			int loot = Utility.RandomMinMax(1,100);
			if (loot < 61)
				AddLoot( LootPack.Poor );
			else if (loot < 91 && loot > 60)
				AddLoot( LootPack.AosMeager );
			else 
				AddLoot( LootPack.AosAverage );
			// TODO: weapon, misc
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		
		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if (Utility.RandomMinMax(1,100) < 51)
				reflect = true; // Every spell is reflected back to the caster
		}

		public GremlinPriest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int)m_DrainIntensity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			int m_DrainIntensity = reader.ReadInt();
		}
	}

	[CorpseName( "a gremlin's corpse" )]
	public class GremlinMage : BaseCreature, IMagicDrain
	{
		#region Magic Drain
		private int m_DrainIntensity;

		[CommandProperty( AccessLevel.GameMaster )]
		public int DrainIntensity
		{
			get{ return m_DrainIntensity; }
			set{ m_DrainIntensity = value;}
		}

		#endregion

		[Constructable]
		public GremlinMage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Gremlin Mage";
			Body = 253;
			BaseSoundID = 437;
			Level = 14;
			DrainIntensity = 8;

			SetStr( 100, 110 );
			SetDex( 120, 150 );
			SetInt( 250, 300 );

			SetHits( 100, 130 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 40, 55 );
			SetResistance( ResistanceType.Cold, 40, 55 );
			SetResistance( ResistanceType.Poison, 40, 55 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 100.1, 120.0 );
			SetSkill( SkillName.Magery, 100.1, 120.0 );
			SetSkill( SkillName.MagicResist, 55.1, 90.0 );
			SetSkill( SkillName.Tactics, 50.1, 60.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 1750;
			Karma = -1750;

			VirtualArmor = 28;
			
			PackArmor(1, 3, 0.01);
			if (Utility.RandomMinMax(1,100) < 2)
				PackItem( (Item)Activator.CreateInstance( Server.LootPack.AlchyScroll[Utility.RandomMinMax(10,32)] ) );

		}

		public override void GenerateLoot()
		{
			int loot = Utility.RandomMinMax(1,100);
			if (loot < 61)
				AddLoot( LootPack.Poor );
			else if (loot < 91 && loot > 60)
				AddLoot( LootPack.AosMeager );
			else 
				AddLoot( LootPack.AosAverage );
			// TODO: weapon, misc
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		
		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if (Utility.RandomMinMax(1,100) < 51)
				reflect = true; // Every spell is reflected back to the caster
		}

		public GremlinMage( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int)m_DrainIntensity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			int m_DrainIntensity = reader.ReadInt();
		}
	}

}
