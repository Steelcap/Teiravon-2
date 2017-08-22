using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a juka corpse" )] // Why is this 'juka' and warriors 'jukan' ? :-(
	public class JukaLord : BaseCreature
	{
        DateTime m_NextHealTime;
		[Constructable]
		public JukaLord() : base( AIType.AI_Archer, FightMode.Closest, 10, 3, 0.15, 0.4 )
		{
			Name = "a juka lord";
			Body = 766;
			Level = 14;

			SetStr( 501, 600 );
			SetDex( 121, 140 );
			SetInt( 151, 200 );

			SetHits( 441, 500 );

			SetDamage( 15, 22 );
            m_NextHealTime = DateTime.Now;
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 45, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 55 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 90.1, 100.0 );
			SetSkill( SkillName.Archery, 95.1, 100.0 );
			SetSkill( SkillName.Healing, 80.1, 100.0 );
			SetSkill( SkillName.MagicResist, 120.1, 130.0 );
			SetSkill( SkillName.Swords, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 95.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 28;

			Container pack = new Backpack();
            AddToBackpack(new Longsword());
			pack.DropItem( new Arrow( Utility.RandomMinMax( 25, 35 ) ) );
			pack.DropItem( new Arrow( Utility.RandomMinMax( 25, 35 ) ) );
			pack.DropItem( new Bandage( Utility.RandomMinMax( 5, 15 ) ) );
			pack.DropItem( new Bandage( Utility.RandomMinMax( 5, 15 ) ) );
			pack.DropItem( Loot.RandomGem() );
			pack.DropItem( new ArcaneGem() );

			PackItem( pack );

			AddItem( new JukaBow() );

			// TODO: Bandage self
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && !willKill && amount > 5 && from.Player && 5 > Utility.Random( 100 ) )
			{
				string[] toSay = new string[]
					{
						"{0}!!  You will have to do better than that!",
						"{0}!!  Prepare to meet your doom!",
						"{0}!!  My armies will crush you!",
						"{0}!!  You will pay for that!"
					};

				this.Say( true, String.Format( toSay[Utility.Random( toSay.Length )], from.Name ) );
			}

			base.OnDamage( amount, from, willKill );
		}

		public override int GetIdleSound()
		{
			return 0x262;
		}

		public override int GetAngerSound()
		{
			return 0x263;
		}

		public override int GetHurtSound()
		{
			return 0x1D0;
		}

		public override int GetDeathSound()
		{
			return 0x28D;
		}

        public override void OnActionCombat()
        {
            

            if (Combatant == null || Combatant.Deleted)
                return;

            /* Weapon Switching -
             * Range 0 - 3: BaseMeleeWeapon
             * Range 4 - 10: Crossbow
             * Range 11 - 20: Longbow
             */

            int distance = (int)GetDistanceToSqrt(Combatant);
            //Say ("distance : {0}",distance);
            if (distance <= 3 && !(Weapon is Longsword) && !(Weapon is Fists))
            {
                Item weapon = Backpack.FindItemByType(typeof(BaseMeleeWeapon));
                Item shield = Backpack.FindItemByType(typeof(BaseShield));
                //Say("Switching to Melee");
                if (weapon != null)
                {
                    AddToBackpack(Weapon as Item);
                    EquipItem(weapon);

                    if (weapon.Layer != Layer.TwoHanded && shield != null)
                        EquipItem(shield);

                    RangeFight = 0;
                }
            }
            else if (distance >= 4 && !(Weapon is BaseRanged) && !(Weapon is Fists))
            {
                Item weapon = Backpack.FindItemByType(typeof(BaseRanged));

                if (weapon != null)
                {
                    AddToBackpack(Weapon as Item);

                    if (ShieldArmor != null)
                        AddToBackpack(ShieldArmor);

                    EquipItem(weapon);

                    RangeFight = 6;
                }
            }

            // Healing
            if (m_NextHealTime < DateTime.Now && Hits <= (int)(HitsMax / 4) && Backpack.FindItemByType(typeof(Bandage)) != null)
            {
                Hits += Utility.RandomMinMax((int)(Skills.Healing.Value * .2), (int)(Skills.Healing.Value * .5));
                Emote("*You see {0} heal {1}*", Name, Female ? "herself" : "himself");

                Backpack.ConsumeTotal(typeof(Bandage), 1);
                m_NextHealTime = DateTime.Now + TimeSpan.FromSeconds(30.0);
            }

            base.OnActionCombat();
        }

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public JukaLord( Serial serial ) : base( serial )
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
