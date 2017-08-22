using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Items;
using Server.Teiravon;
using Server.Engines.XmlSpawner2;

namespace Server.Gumps
{

    public class NewShapeshiftGump : Gump
    {
        private int max;
        private TeiravonMobile m_Player;

        public NewShapeshiftGump(TeiravonMobile from)
            : base(0, 0)
        {
            m_Player = from;
            max = 1 + m_Player.PlayerLevel / (m_Player.IsShapeshifter()? 4 : 7 ) ;
            if (max > 7)
                max = 7;

            Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			m_Player = (TeiravonMobile) from;
			m_Player.CloseGump( typeof( NewShapeshiftGump ) );
            this.AddPage(0);
            AddBackground(312, 10, 30 + (max * 98), 140, 9260);
            //this.AddImage(328, -5, 30061);
            //this.AddImage(328, 55 + (max * 82), 30077);

            //AddLabel(355,  115, 2930, @"Evaluate creature");
            AddButton(330, 120, 22153, 22155, (int)Buttons.Information, GumpButtonType.Reply, 0);

            if (m_Player.IsShifted() && m_Player.Shapeshifted)
            {
                //AddLabel(401, 487, 2930, @"Undo transformation");
                AddButton(276 + ((30+ (max * 98))), 112, 5052, 5053, (int)Buttons.Cancel, GumpButtonType.Reply, 0);
            }

            bool DeleteTime = false;

            if (m_Player.ShapeshiftSlotDelete <= DateTime.Now)
                DeleteTime = true;

            switch (max)
            {
                case 1: 
                    if (FilledSlot(1, m_Player))
                    {
                        FullSlot((int)Buttons.CTransform1, 1, m_Player);

                        if (DeleteTime)
                            DeleteButton((int)Buttons.CDelete1, 1);
                    }
                    else
                        EmptySlot((int)Buttons.CAdd1, 1);

                    break;

                case 2:

                    if (FilledSlot(2, m_Player))
                    {
                        FullSlot((int)Buttons.CTransform2, 2, m_Player);

                        if (DeleteTime)
                            DeleteButton((int)Buttons.CDelete2, 2);
                    }
                    else
                        EmptySlot((int)Buttons.CAdd2, 2);

                    goto case 1;

                case 3:
                    if (FilledSlot(3, m_Player))
                    {
                        FullSlot((int)Buttons.CTransform3, 3, m_Player);

                        if (DeleteTime)
                            DeleteButton((int)Buttons.CDelete3, 3);
                    }
                    else
                        EmptySlot((int)Buttons.CAdd3, 3);

                    goto case 2;

                case 4:
                    if (FilledSlot(4, m_Player))
                    {
                        FullSlot((int)Buttons.CTransform4, 4, m_Player);

                        if (DeleteTime)
                            DeleteButton((int)Buttons.CDelete4, 4);
                    }
                    else
                        EmptySlot((int)Buttons.CAdd4, 4);

                    goto case 3;

                case 5:

                    if (FilledSlot(5, m_Player))
                    {
                        FullSlot((int)Buttons.CTransform5, 5, m_Player);

                        if (DeleteTime)
                            DeleteButton((int)Buttons.CDelete5, 5);
                    }
                    else
                        EmptySlot((int)Buttons.CAdd5, 5);

                    goto case 4;

                case 6:

                    if (FilledSlot(6, m_Player))
                    {
                        FullSlot((int)Buttons.CTransform6, 6, m_Player);

                        if (DeleteTime)
                            DeleteButton((int)Buttons.CDelete6, 6);
                    }
                    else
                        EmptySlot((int)Buttons.CAdd6, 6);

                    goto case 5;

                case 7:

                    if (FilledSlot(7, m_Player))
                    {
                        FullSlot((int)Buttons.CTransform7, 7, m_Player);

                        if (DeleteTime)
                            DeleteButton((int)Buttons.CDelete7, 7);
                    }
                    else
                        EmptySlot((int)Buttons.CAdd7, 7);

                    goto case 6;

                default: break;
            }
        }

        private bool FilledSlot(int slot, TeiravonMobile player)
        {
            int slotnumber = slot - 1;

            if ((player.ShapeshiftSlotName[slotnumber] != null) && player.ShapeshiftSlot[slotnumber] != 0)
                return true;

            return false;
        }

        private void DeleteButton(int button, int number)
        {
            AddButton(295 + (number * 98), 20, 4017, 4019, button, GumpButtonType.Reply, 0);
        }

        private void CustomNoSlot(int number)
        {
            AddLabel(380, 80 + (number * 63), 2930, @"Not available");
        }

        private void EmptySlot(int button, int number)
        {
            CustomTransformButton(true, button, number, null, 0);
        }

        private void FullSlot(int button, int number, TeiravonMobile player)
        {
            int slotnumber = number - 1;

            string name = player.ShapeshiftSlotName[slotnumber];
            int bodyvalue = player.ShapeshiftSlot[slotnumber];

            CustomTransformButton(false, button, number, name, bodyvalue);
        }

        private void CustomTransformButton(bool empty, int button, int number, string name, int bodyvalue)
        {
            AddAlphaRegion(244 + (number * 98), 24, 50, 50);

            if (!empty)
            {
                AddLabel(236 + (number * 98), 72, 2930, name);
                AddItem(245 + (number * 98), 30, ShrinkTable.Lookup(bodyvalue));
                AddButton(246 + (number * 98), 94, 2074, 2075, button, GumpButtonType.Reply, 0);
            }
            else
            {
                AddLabel(248 + (number * 98), 72, 2930, "Empty");
                AddButton(239 + (number * 98), 94, 5533, 5535, button, GumpButtonType.Reply, 0);
            }
        }

        private void AddTransformation(int level)
        {
            m_Player.SendMessage("From to add to that slot.");
            m_Player.Target = new AddTransformationTarget(m_Player, level);

        }

        private void DeleteSlot(int slot, TeiravonMobile player)
        {
            int slotnumber = slot - 1;

            player.ShapeshiftSlotName[slotnumber] = null;
            player.ShapeshiftSlot[slotnumber] = 0;
            player.ShapeshiftSlotLevel[slotnumber] = 0;
            player.ShapeshiftSlotHue[slotnumber] = 0;

            player.ShapeshiftSlotDelete = DateTime.Now + TimeSpan.FromHours(12.0);

            player.SendMessage("The transformation in slot {0} has been removed", slot);
            player.SendMessage("You will be able to delete again in twelve hours");

        }

        public static void SaveTransformation(TeiravonMobile from, TransformGroup type, int slot, BaseCreature target)
        {
            TeiravonMobile m_Shapeshifter = from;
            BaseCreature m_Creature = (BaseCreature)Activator.CreateInstance(target.GetType());
            int BodyValue = m_Creature.BodyValue;
            int ProperHue = m_Creature.Hue;
            string CreatureName = (m_Creature.Name).ToLower();
            TransformGroup ShiftType = type;
            //			string TransformationName = "";

            /*			char[] separate = new char[]{' '};
                        string[] separated = CreatureName.Split( separate, 3 );

                        for( int i=0; i < separated.Length; i++ )
                        {
                            string ss = separated[i];

                            if ( i == 0 )
                            {
                                if ( ss == "an" || ss == "a" || ss == "the" )
                                    continue;
                            }

                            TransformationName += char.ToUpper(ss[0]);

                            if ( i < separated.Length - 1 )
                                TransformationName += (ss.Substring(1, ss.Length - 1) + ' ');
                            else
                                TransformationName += (ss.Substring(1, ss.Length - 1));


                        }
            */
            m_Shapeshifter.ShapeshiftSlot[slot] = BodyValue;
            //			m_Shapeshifter.ShapeshiftSlotName[level] = TransformationName;
            m_Shapeshifter.ShapeshiftSlotLevel[slot] = (int)ShiftType;
            m_Shapeshifter.ShapeshiftSlotName[slot] = CreatureName;
            m_Shapeshifter.ShapeshiftSlotHue[slot] = ProperHue;

        }

        public static void Infravision(TeiravonMobile player)
        {
            int hour, min;

            Clock.GetTime(player.Map, player.X, player.Y, out hour, out min);

            if (hour > 23 || hour < 5)
                player.SendMessage("Your eyes begin to adjust to the darkness around you.");
        }

        public static void ShapeshiftFunctions(bool shift, int slot, TeiravonMobile player)
        {
            int slotnumber = slot - 1;
            int difficulty = player.ShapeshiftSlotLevel[slotnumber];
            int formhue = player.ShapeshiftSlotHue[slotnumber];
            string formname = player.ShapeshiftSlotName[slotnumber];

            DateTime ShapeshiftTime;

            if (shift)
            {
                player.DruidForm = player.ShapeshiftSlot[slotnumber];
                player.DruidFormGroup = player.ShapeshiftSlotLevel[slotnumber];
                if (!player.Shapeshifted)
                    player.ShapeshiftHue = player.Hue;
                if (player.DruidFormGroup == 5)
                    player.Send(Server.Network.SpeedMode.Run);

                player.FixedParticles(0x377A, 1, 50, 9949, 1153, 0, EffectLayer.Head);
                player.SendMessage("You begin to transform into an animal.");

                player.Paralyze(TimeSpan.FromSeconds(3.0));

                ShapeshiftTime = DateTime.Now + TimeSpan.FromSeconds(3.0);

                player.NameMod = player.ShapeshiftSlotName[slotnumber];


            }

            else
            {
                if (player.DruidFormGroup == 5)
                    player.Send(Server.Network.SpeedMode.Disabled);

                player.DruidForm = 0;
                player.DruidFormGroup = -1;
                player.SendMessage("You begin transforming back to normal.");

                player.Paralyze(TimeSpan.FromSeconds(2.0));
                player.NameMod = null;

                ShapeshiftTime = DateTime.Now + TimeSpan.FromSeconds(2.0);

            }


            Timer m_Timer = new CustomTransformTimer(player, shift, slotnumber, difficulty, formhue, formname, ShapeshiftTime);
            m_Timer.Start();
        }

        private class CustomTransformTimer : Timer
        {
            private TeiravonMobile m_Shapeshifter;
            private int formgroup;
            private int formhue;
            private int slotnumber;
            private bool shapeshift;
            private string formname;

            public CustomTransformTimer(TeiravonMobile from, bool shift, int slot, int type, int hue, string name, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Shapeshifter = from;
                shapeshift = shift;
                formgroup = type;
                formname = name;
                formhue = hue;
                slotnumber = slot;
            }

            protected override void OnTick()
            {

                if (shapeshift)
                {
                    Container pack = m_Shapeshifter.Backpack;

                    Item animalskin = m_Shapeshifter.FindItemOnLayer(Layer.InnerTorso);

                    if (animalskin != null && animalskin is CustomShapeshifterArmor)
                        animalskin.Delete();

                    Item animalweapon = m_Shapeshifter.FindItemOnLayer(Layer.TwoHanded);

                    if (animalweapon != null && animalweapon is CustomShapeshifterWeapon)
                        animalweapon.Delete();

                    for (int i = 1; i < 30; i++)
                    {
                        Item item = m_Shapeshifter.FindItemOnLayer((Layer)i);

                        if ((item != null) && (pack != null) && (i != 9 && i != 11 && i != 15 && i != 16 && i != 21 && i < 24))
                            pack.DropItem(item);
                    }

                    m_Shapeshifter.Shapeshifted = true;

                    //if (formhue != 0)
                    m_Shapeshifter.Hue = formhue;

                    m_Shapeshifter.EquipItem(new CustomShapeshifterArmor(formgroup, formname, formhue, m_Shapeshifter));
                    m_Shapeshifter.EquipItem(new CustomShapeshifterWeapon(formgroup, formname, formhue, m_Shapeshifter));

                    m_Shapeshifter.SendMessage("You speak now an animal language.");
                    m_Shapeshifter.CurrentLanguage = TeiravonMobile.LLupine;

                    Infravision(m_Shapeshifter);
                    if( m_Shapeshifter.CantWalk == true)
                        m_Shapeshifter.CantWalk = false;
                    m_Shapeshifter.Shapeshift(true, m_Shapeshifter.ShapeshiftSlot[slotnumber]);

                }

                else
                {
                    m_Shapeshifter.Shapeshift(false, 0);
                    m_Shapeshifter.Shapeshifted = false;
                    m_Shapeshifter.ShapeshiftNext = DateTime.Now + TimeSpan.FromSeconds(3.0);

                    Item animalskin = m_Shapeshifter.FindItemOnLayer(Layer.InnerTorso);

                    if (animalskin != null && animalskin is CustomShapeshifterArmor)
                        animalskin.Delete();

                    Item animalweapon = m_Shapeshifter.FindItemOnLayer(Layer.TwoHanded);

                    if (animalweapon != null && animalweapon is CustomShapeshifterWeapon)
                        animalweapon.Delete();

                    m_Shapeshifter.Hue = m_Shapeshifter.ShapeshiftHue;
                    m_Shapeshifter.SendMessage("Your language is now Common.");
                    m_Shapeshifter.CurrentLanguage = TeiravonMobile.LCommon;
                    m_Shapeshifter.NameMod = null;

                }
            }
        }

        private void NoTransform(TeiravonMobile player, int slot)
        {
            
            if (player.DruidFormGroup == 5)
                player.Send(Server.Network.SpeedMode.Disabled);

            player.DruidForm = 0;
            player.DruidFormGroup = -1;

            player.Paralyze(TimeSpan.FromSeconds(2.0));
            player.NameMod = null;

            ShapeshiftFunctions(true, slot, player);
        }

        private bool NextTransform(TeiravonMobile player)
        {
            if (player.ShapeshiftNext > DateTime.Now)
            {
                player.SendMessage("You haven't recovered from your previous shapeshift.");
                return true;
            }

            return false;
        }

        public enum Buttons
        {
            ExitMenu,
            Cancel,
            Information,
            UndoTransformation,
            CTransform1,
            CTransform2,
            CTransform3,
            CTransform4,
            CTransform5,
            CTransform6,
            CTransform7,
            CAdd1,
            CAdd2,
            CAdd3,
            CAdd4,
            CAdd5,
            CAdd6,
            CAdd7,
            CDelete1,
            CDelete2,
            CDelete3,
            CDelete4,
            CDelete5,
            CDelete6,
            CDelete7
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {

            switch (info.ButtonID)
            {

                case (int)Buttons.CAdd1: AddTransformation(1);
                    break;

                case (int)Buttons.CAdd2: AddTransformation(2);
                    break;

                case (int)Buttons.CAdd3: AddTransformation(3);
                    break;

                case (int)Buttons.CAdd4: AddTransformation(4);
                    break;

                case (int)Buttons.CAdd5: AddTransformation(5);
                    break;

                case (int)Buttons.CAdd6: AddTransformation(6);
                    break;

                case (int)Buttons.CAdd7: AddTransformation(7);
                    break;

                case (int)Buttons.CDelete1: DeleteSlot(1, m_Player);
                    break;

                case (int)Buttons.CDelete2: DeleteSlot(2, m_Player);
                    break;

                case (int)Buttons.CDelete3: DeleteSlot(3, m_Player);
                    break;

                case (int)Buttons.CDelete4: DeleteSlot(4, m_Player);
                    break;

                case (int)Buttons.CDelete5: DeleteSlot(5, m_Player);
                    break;

                case (int)Buttons.CDelete6: DeleteSlot(6, m_Player);
                    break;

                case (int)Buttons.CDelete7: DeleteSlot(7, m_Player);
                    break;

                case (int)Buttons.Information:

                    m_Player.SendMessage("Target a creature.");
                    m_Player.Target = new TransformationInformation(m_Player);
                    break;

                case (int)Buttons.CTransform1:
                    if (m_Player.IsShifted() || m_Player.Shapeshifted)
                        NoTransform(m_Player, 1);
                    else if (NextTransform(m_Player))
                        break;
                    else
                        ShapeshiftFunctions(true, 1, m_Player);

                    break;
                case (int)Buttons.CTransform2:

                    if (m_Player.IsShifted() || m_Player.Shapeshifted)
                        NoTransform(m_Player, 2);
                    else if (NextTransform(m_Player))
                        break;
                    else
                        ShapeshiftFunctions(true, 2, m_Player);

                    break;
                case (int)Buttons.CTransform3:

                    if (m_Player.IsShifted() || m_Player.Shapeshifted)
                        NoTransform(m_Player, 3);
                    else if (NextTransform(m_Player))
                        break;
                    else
                        ShapeshiftFunctions(true, 3, m_Player);

                    break;
                case (int)Buttons.CTransform4:

                    if (m_Player.IsShifted() || m_Player.Shapeshifted)
                        NoTransform(m_Player,4);
                    else if (NextTransform(m_Player))
                        break;
                    else
                        ShapeshiftFunctions(true, 4, m_Player);

                    break;
                case (int)Buttons.CTransform5:

                    if (m_Player.IsShifted() || m_Player.Shapeshifted)
                        NoTransform(m_Player,5);
                    else if (NextTransform(m_Player))
                        break;
                    else
                        ShapeshiftFunctions(true, 5, m_Player);

                    break;
                case (int)Buttons.CTransform6:

                    if (m_Player.IsShifted() || m_Player.Shapeshifted)
                        NoTransform(m_Player,6);
                    else if (NextTransform(m_Player))
                        break;
                    else
                        ShapeshiftFunctions(true, 6, m_Player);

                    break;
                case (int)Buttons.CTransform7:

                    if (m_Player.IsShifted() || m_Player.Shapeshifted)
                        NoTransform(m_Player,7);
                    else if (NextTransform(m_Player))
                        break;
                    else
                        ShapeshiftFunctions(true, 7, m_Player);

                    break;

                case (int)Buttons.Cancel:
                        ShapeshiftFunctions(false, 1, m_Player);
                        break;

                default:
                    break;
            }
        }

    }
   
}


namespace Server.Items
{
	public class CustomShapeshifterWeapon : BaseWeapon
	{
		public override bool DisplayLootType{ get{ return false; } }

        public override WeaponAbility PrimaryAbility { get { return Primary; } }
        public override WeaponAbility SecondaryAbility { get { return Secondary; } }

		public override SkillName DefSkill{ get{ return SkillName.Wrestling; } }
		public override WeaponType DefType{ get{ return WeaponType.Bashing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Wrestle; } }

		public override int AosMinDamage{ get{ return 8; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 25; } }
        public override int DefHitSound { get { return -1; } }
        public override int DefMissSound { get { return -1; } }

		public int WeaponLevel = 1;
        private WeaponAbility Primary;
        private WeaponAbility Secondary;
		//[Constructable]
		public CustomShapeshifterWeapon( int type, string name, int hue, TeiravonMobile Shifter ) : base( 0x2644 )
		{
            int index = type;

            int ShifterLevel = Shifter.PlayerLevel;

            if (!Shifter.IsShapeshifter())
                ShifterLevel = (ShifterLevel / 2);

			Weight = 0.0;
			Movable = false;

			if (name != null)
				Name = name + "'s Limb";
			else
				Name = "Animal Limb";

			Resource = CraftResource.None;
			Hue = 1;
			LootType = LootType.Newbied;
			Layer = Layer.TwoHanded;
            ItemID = (int)(TAVShiftUtilities.ShiftInfo.Table[index].WeapID);
            Primary = (WeaponAbility)(TAVShiftUtilities.ShiftInfo.Table[index].PrimaryAbility);
            Secondary = (WeaponAbility)(TAVShiftUtilities.ShiftInfo.Table[index].PrimaryAbility);

            PoisonCharges = 9999;
            Poison = Poison.GetPoison(ShifterLevel / 5);

            Attributes.WeaponSpeed = (int)(TAVShiftUtilities.ShiftInfo.Table[index].SpeedScale * (ShifterLevel * 2));
            MinDamage = 8  + (int)(TAVShiftUtilities.ShiftInfo.Table[index].DamageScale * ShifterLevel);
            MaxDamage = 11 + (int)(TAVShiftUtilities.ShiftInfo.Table[index].DamageScale * ShifterLevel);
            
            if (index == 1)
            {
                SkillBonuses.Skill_1_Name = SkillName.Tracking;
                SkillBonuses.Skill_1_Value = 5 * ShifterLevel;
            }
            if (index == 7)
            {
                SkillBonuses.Skill_1_Name = SkillName.Stealth;
                SkillBonuses.Skill_1_Value = 5 * ShifterLevel;
            }
            if (index == 8)
            {
                SkillBonuses.Skill_1_Name = SkillName.Poisoning;
                SkillBonuses.Skill_1_Value = 5 * ShifterLevel;
            }
            
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			switch ( WeaponLevel )
			{

				case 1:
					phys = 100;
					fire = pois = nrgy = cold = 0;
					break;

				case 2:
					phys = 60;
					pois = 20;
					cold = 20;
					nrgy = fire = 0;
					break;

				case 3:
					phys = 50;
					pois = 20;
					cold = 10;
					nrgy = 10;
					fire = 10;
					break;

				case 4:
					phys = 25;
					pois = 20;
					cold = 20;
					fire = 15;
					nrgy = 20;
					break;

				case 5:
					phys = 10;
					fire = 25;
					pois = 20;
					nrgy = 25;
					cold = 20;
					break;

				default:
					phys = 100;
					fire = pois = nrgy = cold = 0;
					break;
			}

		}

		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			return DeathMoveResult.RemainEquiped;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
		}


		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent);

			if ( this != null )
				this.Delete();
		}


		public CustomShapeshifterWeapon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x13db, 0x13e2 )]
	public class CustomShapeshifterArmor : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override bool DisplayLootType{ get{ return false; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }


		//[Constructable]
		public CustomShapeshifterArmor( int type, string name, int hue, TeiravonMobile Shifter ) : base( 0x13DB )
		{
            int index = type;
            int ShifterLevel = Shifter.PlayerLevel;

            if (!Shifter.IsShapeshifter())
                ShifterLevel = (ShifterLevel / 2);

			Weight = 0.0;
			Movable = false;
			Hue = hue;
			LootType = LootType.Newbied;

			if (name != null)
				Name = name + "'s Hide";
			else
				Name = "Animal Hide";


					PhysicalBonus = (int)(Utility.RandomMinMax( 18, 21 ) + TAVShiftUtilities.ShiftInfo.Table[index].ArmorScale) + (int)(1.5 * ShifterLevel);
                    ColdBonus = (int)(Utility.RandomMinMax(18, 21) + TAVShiftUtilities.ShiftInfo.Table[index].ArmorScale) + (int)(1.5 * ShifterLevel);
                    EnergyBonus = (int)(Utility.RandomMinMax(18, 21) + TAVShiftUtilities.ShiftInfo.Table[index].ArmorScale) + (int)(1.5 * ShifterLevel);
                    PoisonBonus = (int)(Utility.RandomMinMax(18, 21) + TAVShiftUtilities.ShiftInfo.Table[index].ArmorScale) + (int)(1.5 * ShifterLevel);
                    FireBonus = (int)(Utility.RandomMinMax(18, 21) + TAVShiftUtilities.ShiftInfo.Table[index].ArmorScale) + (int)(1.5 * ShifterLevel);

                    Attributes.BonusStr = (int)(TAVShiftUtilities.ShiftInfo.Table[index].StrScale * ShifterLevel);
                    Attributes.BonusDex = (int)(TAVShiftUtilities.ShiftInfo.Table[index].DexScale * ShifterLevel);
                    Attributes.BonusInt = (int)(TAVShiftUtilities.ShiftInfo.Table[index].IntScale * ShifterLevel);

                    Attributes.BonusHits = (int)(TAVShiftUtilities.ShiftInfo.Table[index].HpScale * ShifterLevel);
                    Attributes.BonusStam = (int)(TAVShiftUtilities.ShiftInfo.Table[index].StamScale * ShifterLevel);
                    Attributes.BonusMana = (int)(TAVShiftUtilities.ShiftInfo.Table[index].ManaScale * ShifterLevel);

                    Attributes.RegenHits = (int)(TAVShiftUtilities.ShiftInfo.Table[index].HpRegen * ShifterLevel);
                    Attributes.RegenStam = (int)(TAVShiftUtilities.ShiftInfo.Table[index].StamRegen * ShifterLevel);
                    Attributes.RegenMana = (int)(TAVShiftUtilities.ShiftInfo.Table[index].ManaRegen * ShifterLevel);

		}


		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			return DeathMoveResult.RemainEquiped;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent);

			if ( this != null )
				this.Delete();
		}

		public CustomShapeshifterArmor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

		}
	}

	public class ShapeshifterWeapon : BaseWeapon
	{
		public override bool DisplayLootType{ get{ return false; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override SkillName DefSkill{ get{ return SkillName.Wrestling; } }
		public override WeaponType DefType{ get{ return WeaponType.Bashing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Wrestle; } }

		public override int AosMinDamage{ get{ return 8; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 35; } }

		public override int DefHitSound { get { return -1; } }
       	public override int DefMissSound { get { return -1; } }

		public int WeaponLevel = 1;

		//[Constructable]
		public ShapeshifterWeapon( int slotlevel ) : base( 0x27F6 )
		{
			Weight = 0.0;
			Movable = false;
			LootType = LootType.Newbied;
			Layer = Layer.TwoHanded;
			WeaponLevel = slotlevel;
			Resource = CraftResource.None;


			switch ( slotlevel )
			{
				case 1:
					Name = "Eagle's Talons";
					Hue = 1880;
					ItemID = 0x27FA;

					Attributes.WeaponDamage = Utility.RandomMinMax( 5, 15 );

					break;

				case 2:
					Name = "Wolf's Paw";
					Hue = 2229;

					MinDamage = Utility.RandomMinMax( 9, 12 );
					MaxDamage = Utility.RandomMinMax( 12, 15 );
					Speed = Utility.RandomMinMax( 32, 42 );

//					Attributes.AttackChance = Utility.RandomMinMax( 5, 15 );
					Attributes.WeaponDamage = Utility.RandomMinMax( 10, 25 );

					break;

				case 3:
					Name = "Panther's Claw";
					Hue = 2306;

					MinDamage = Utility.RandomMinMax( 12, 15);
					MaxDamage = Utility.RandomMinMax( 15, 17);
					Speed = Utility.RandomMinMax( 38, 46);

//					Attributes.AttackChance = Utility.RandomMinMax( 15, 20 );
					Attributes.WeaponDamage = Utility.RandomMinMax( 15, 35 );

					break;

				case 4:
					Name = "Bear's Paw";
					Hue = 1841;

					MinDamage = Utility.RandomMinMax( 13, 15 );
					MaxDamage = Utility.RandomMinMax( 17, 19 );
					Speed = Utility.RandomMinMax( 32, 38);

//					Attributes.AttackChance = Utility.RandomMinMax( 15, 30 );
					Attributes.WeaponDamage = Utility.RandomMinMax( 20, 45 );



					break;

				case 5:
					Name = "Spirit Appendage";
					Hue = 2479;
					ItemID = 0x27F8;

					MinDamage = Utility.RandomMinMax( 15, 18 );
					MaxDamage = Utility.RandomMinMax( 18, 20 );
					Speed = Utility.RandomMinMax( 34, 38 );

//					Attributes.AttackChance = Utility.RandomMinMax( 30, 45 );
					Attributes.WeaponDamage = Utility.RandomMinMax( 35, 60 );

					break;

				default: break;
			}

		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy )
		{
			switch ( WeaponLevel )
			{

				case 1:
					phys = 100;
					fire = pois = nrgy = cold = 0;
					break;

				case 2:
					phys = 90;
					pois = 10;
					cold = nrgy = fire = 0;
					break;

				case 3:
					phys = 80;
					pois = 10;
					cold = 10;
					nrgy = fire = 0;
					break;

				case 4:
					phys = 70;
					pois = 10;
					cold = 10;
					fire = 10;
					nrgy = 0;
					break;

				case 5:
					phys = 60;
					pois = 10;
					cold = 10;
					fire = 10;
					nrgy = 10;
					break;

				default:
					phys = 100;
					fire = pois = nrgy = cold = 0;
					break;
			}

		}

		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			return DeathMoveResult.RemainEquiped;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
		}


		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent);

			if ( this != null )
				this.Delete();
		}


		public ShapeshifterWeapon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

            this.Delete();
		}
	}

	[FlipableAttribute( 0x13cc, 0x13d3 )]
	public class ShapeshifterArmor : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		//public override int InitMinHits{ get{ return 30; } }
		//public override int InitMaxHits{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.None; } }
		public override bool DisplayLootType{ get{ return false; } }

		//[Constructable]
		public ShapeshifterArmor( int slotlevel ) : base( 0x13CC )
		{
			Weight = 0.0;
			Movable = false;
			LootType = LootType.Newbied;

			switch ( slotlevel )
			{
				case 1:
					Name = "Eagle's feathers";
					Hue = 1880;

					Attributes.RegenHits = 4;
					Attributes.BonusDex = Utility.RandomMinMax( 15, 25 );
					Attributes.BonusStam = Utility.RandomMinMax( 5, 15 );
					Attributes.BonusHits = Utility.RandomMinMax( 25, 45 );

					PhysicalBonus = Utility.RandomMinMax( 12, 30 );
					ColdBonus = Utility.RandomMinMax( 20, 35 );
					EnergyBonus = Utility.RandomMinMax( 20, 35 );
					PoisonBonus = Utility.RandomMinMax( 20, 35 );
					FireBonus = Utility.RandomMinMax( 20, 35 );

					break;

				case 2:
					Name = "Wolf's Fur";
					Hue = 2229;

					Attributes.RegenHits = 4;
					Attributes.BonusStr = Utility.RandomMinMax( 5, 15 );
					Attributes.BonusDex = Utility.RandomMinMax( 15, 25 );
					Attributes.BonusStam = Utility.RandomMinMax( 25, 35 );
					Attributes.BonusHits = Utility.RandomMinMax( 25, 60 );

					Attributes.DefendChance = Utility.RandomMinMax( 1, 5);

					PhysicalBonus = Utility.RandomMinMax( 20, 30 );
					ColdBonus = Utility.RandomMinMax( 20, 40 );
					EnergyBonus = Utility.RandomMinMax( 20, 40 );
					PoisonBonus = Utility.RandomMinMax( 20, 40 );
					FireBonus = Utility.RandomMinMax( 20, 40 );

					break;

				case 3:
					Name = "Panther's Fur";
					Hue = 2306;

					Attributes.RegenHits = 4;
					Attributes.BonusStr = Utility.RandomMinMax( 20, 30 );
					Attributes.BonusDex = Utility.RandomMinMax( 25, 35 );
					Attributes.BonusStam = Utility.RandomMinMax( 30, 50 );
					Attributes.BonusHits = Utility.RandomMinMax( 45, 70 );

					Attributes.DefendChance = Utility.RandomMinMax( 1, 10);

					SkillBonuses.SetValues( 0, SkillName.Wrestling, 5.0 );

					PhysicalBonus = Utility.RandomMinMax( 25, 40 );
					ColdBonus = Utility.RandomMinMax( 35, 50 );
					EnergyBonus = Utility.RandomMinMax( 35, 50 );
					PoisonBonus = Utility.RandomMinMax( 35, 50 );
					FireBonus = Utility.RandomMinMax( 35, 50 );

					break;

				case 4:
					Name = "Bear's Fur";
					Hue = 1841;

					Attributes.RegenHits = 4;
					Attributes.BonusStr = Utility.RandomMinMax( 40, 60 );
					Attributes.BonusDex = Utility.RandomMinMax( 40, 50 );
					Attributes.BonusStam = Utility.RandomMinMax( 30, 60 );
					Attributes.BonusHits = Utility.RandomMinMax( 70, 120 );

					Attributes.DefendChance = Utility.RandomMinMax( 5, 15 );

					SkillBonuses.SetValues( 0, SkillName.Wrestling, 10.0 );

					PhysicalBonus = Utility.RandomMinMax( 35, 45 );
					ColdBonus = Utility.RandomMinMax( 40, 55 );
					EnergyBonus = Utility.RandomMinMax( 40, 55 );
					PoisonBonus = Utility.RandomMinMax( 40, 55 );
					FireBonus = Utility.RandomMinMax( 40, 55 );

					break;

				case 5:
					Name = "Animal Spirit's Shroud";
					Hue = 2479;
					ItemID = 0x27CD;

					Attributes.RegenHits = 5;
					Attributes.BonusDex = Utility.RandomMinMax( 50, 70 );
					Attributes.BonusStr = Utility.RandomMinMax( 60, 80 );
					Attributes.BonusHits = Utility.RandomMinMax( 100, 155 );
					Attributes.BonusStam = Utility.RandomMinMax( 65, 85 );
					Attributes.BonusInt = Utility.RandomMinMax( 40, 50 );
					Attributes.BonusMana = Utility.RandomMinMax( 40, 50 );

					Attributes.DefendChance = Utility.RandomMinMax( 10, 20 );

					SkillBonuses.SetValues( 0, SkillName.Wrestling, 20.0 );

					PhysicalBonus = Utility.RandomMinMax( 40, 50 );
					ColdBonus = Utility.RandomMinMax( 45, 60);
					EnergyBonus = Utility.RandomMinMax( 45, 60);
					PoisonBonus = Utility.RandomMinMax( 45, 60);
					FireBonus = Utility.RandomMinMax( 45, 60);

					break;

				default: break;
			}

		}

		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			return DeathMoveResult.RemainEquiped;
		}


		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( Name );
		}


		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent);

			if ( this != null )
				this.Delete();
		}

		public ShapeshifterArmor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

            this.Delete();
		}
	}
}


namespace Server.Targets
{

	public class AddTransformationTarget : Target
	{
		TeiravonMobile m_Player;
        Corpse m_Corpse;
		int Slot;
		TransformGroup TransformType;

		public AddTransformationTarget( TeiravonMobile from, int level ) : base( -1, false, TargetFlags.None )
		{
			m_Player = from;
			Slot = level -1;
		}

		protected override void OnTarget( Mobile from, object targ )
		{

			if ( targ is Corpse )
			{
                m_Corpse = (Corpse)targ;

                TransformType =  (TransformGroup)TAVShiftUtilities.GetTransformGroup(m_Corpse.Owner);

                if (TransformType != TransformGroup.Impossible)
				{
						m_Player.SendMessage( "The creature has been added to your transformation list" );
                        NewShapeshiftGump.SaveTransformation(m_Player, TransformType, Slot, (BaseCreature)m_Corpse.Owner);
                        m_Player.SendMessage("This creature is {0}", TransformType);
				}
				else
					m_Player.SendMessage( "That creature cannot be added to your transformation list. " );
			}
			else
				m_Player.SendMessage( "You must drink the hearts blood of a slain creature to learn its form." );
		}
	}

	public class TransformationInformation : Target
	{
		TeiravonMobile m_Player;
        BaseCreature m_Creature;

		public TransformationInformation( TeiravonMobile from ) : base( -1, false, TargetFlags.None )
		{
			m_Player = from;
		}

		protected override void OnTarget( Mobile from, object targ )
		{
			if (targ is BaseCreature )
			{
                m_Creature = (BaseCreature)targ;

                TransformGroup TransformType = (TransformGroup)TAVShiftUtilities.GetTransformGroup(m_Creature);

				if ( TransformType !=  TransformGroup.Impossible )
				{
					m_Player.SendMessage( "That is a {0} creature.", TransformType.ToString() );
				}
				else
					m_Player.SendMessage( "You cannot shapeshift into that creature.");
			}
			else
				m_Player.SendMessage( "That is not a creature." );
		}
	}

}


namespace Server.Scripts.Commands
{
	public class DruidCommands
	{
		#region Command Initializers
		public static void Initialize()
		{
			Server.Commands.Register( "Shapeshift", AccessLevel.Player, new CommandEventHandler( Shapeshift_OnCommand ) );
		}
		#endregion

		[Usage( "Shapeshift" )]
		[Description( "Allows shapeshifters to take a form of another creature" )]
		private static void Shapeshift_OnCommand( CommandEventArgs e )
		{
			TeiravonMobile m_Player = (TeiravonMobile)e.Mobile;

			if ( !m_Player.HasFeat( TeiravonMobile.Feats.WildShape ) )
				m_Player.SendMessage( Teiravon.Colors.FeatMessageColor, Teiravon.Messages.NoAbility );

			else if ( m_Player.Mounted )
				m_Player.SendMessage( "You can't do this while mounted." );

			else if ( m_Player.IsShapeshifter() || m_Player.IsForester() )
				m_Player.SendGump( new NewShapeshiftGump( m_Player ) );
			else
				m_Player.SendMessage( "Only shapeshifters/foresters can do that. ");
		}
	}
}
