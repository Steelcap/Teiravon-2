using System; 
using System.Collections; 
using Server.ContextMenus; 
using Server.Mobiles; 
using Server.Targeting;
using Server.Items;

namespace Server.Items 
{ 
   public class TailorRepair : Item 
   { 
      public double d_SkillLevel;
      public string s_Maker;
      public int m_uses;
        
      [CommandProperty( AccessLevel.GameMaster )] 
      public Double SkillLevel 
      { 
         get { return d_SkillLevel; } 
         set { d_SkillLevel = value; InvalidateProperties(); } 
      } 

      [CommandProperty( AccessLevel.GameMaster )] 
      public string Maker 
      { 
         get { return s_Maker; } 
         set { s_Maker = value; InvalidateProperties(); } 
      } 
        
      [CommandProperty( AccessLevel.GameMaster )]
      public int Uses 
      { 
         get { return m_uses; } 
         set { m_uses = value; InvalidateProperties(); Update();} 
      } 
        
      public void Update()
      {
      	
      }
      
      [Constructable] 
      public  TailorRepair() : base( 0x14F0 ) 
      { 
         //Hue = Utility.RandomMetalHue(); 
         Weight = 0.1;
	 Hue = 2129;
	 LootType = LootType.Blessed;
	 Name = "Tailor"; //1047006 - 1047011  // 1050043 Crafted by name
      } 

      public  TailorRepair( Serial serial ) : base( serial ) 
      { 
      } 

	public override void AddNameProperty( ObjectPropertyList list ) 
	{
	//base.AddNameProperty( list );
	
		if ( SkillLevel >= 100.0)
			list.Add( 1061133, String.Format("{0}\t{1}", "A Grandmaster", "Tailor") ); 
		else if ( SkillLevel >= 90.0)
			list.Add( 1061133, String.Format("{0}\t{1}", "A Master", "Tailor") ); 
		else if ( SkillLevel >= 80.0)
			list.Add( 1061133, String.Format("{0}\t{1}", "An Adept", "Tailor") ); 
		else if ( SkillLevel >= 70.0)
			list.Add( 1061133, String.Format("{0}\t{1}", "An Expert", "Tailor") ); 
		else if ( SkillLevel >= 60.0)
			list.Add( 1061133, String.Format("{0}\t{1}", "A Journeyman", "Tailor") ); 
		else 
			list.Add( 1061133, String.Format("{0}\t{1}", "A Apperntice", "Tailor") ); 


	}
        
	public override void GetProperties( ObjectPropertyList list ) 
	{ 
	base.GetProperties( list );
	
		if ( SkillLevel >= 100.0)
			list.Add( 1050043, String.Format("{0}", Maker) );
		list.Add(1060584, Uses.ToString()); // Uses remaining
	}


      public override void OnDoubleClick( Mobile from ) 
      { 

	 if ( IsChildOf( from.Backpack ) ) 
         { 

		bool IsFound = false;
		Map map = from.Map;

		IPooledEnumerable eable = map.GetMobilesInRange( from.Location, 5 );
		foreach (Mobile p in eable )
		{
			if ( p is Tailor || p is Weaver ) // Check for a NPC within 10 Tiles
			{
			IsFound = true;
			break;
			}
			
		}

		if ( IsFound == false)
		from.SendLocalizedMessage( 1061132 ); // you must be in correct shop
		else
		from.Target = new InternalTarget( this ); //from.SendMessage("NPC Found Repair can Continue");
         } 
         else 
         { 
            from.SendLocalizedMessage( 1047012 ); // in bag please. 
         } 
      } 

      private class InternalTarget : Target 
      { 
         private TailorRepair m_repair;

         public InternalTarget( TailorRepair contract ) :  base ( 1, false, TargetFlags.None ) 
         { 
            m_repair = contract; 
         } 
          
         protected override void OnTarget( Mobile from, object targeted ) 
         { 

		if ( targeted is BaseArmor )
			{
				BaseArmor armor = targeted as BaseArmor;
				int toWeaken = 0;
				int number = 0;
				CraftResource armortype = armor.Resource;

				if ( m_repair.SkillLevel >= 90.0 )
					toWeaken = 1;
				else if ( m_repair.SkillLevel >= 70.0 )
					toWeaken = 2;
				else
					toWeaken = 3;
				
				if ( armortype >= CraftResource.Iron && armortype <= CraftResource.Valorite )
				{
					number = 1044277; // That item cannot be repaired.
				}
				else if ( armortype >= CraftResource.RedScales && armortype <= CraftResource.BlueScales )
				{
					number = 1044277; // That item cannot be repaired.
				}
				else if ( !armor.IsChildOf( from.Backpack ) )
				{
					number = 1044275; // The item must be in your backpack to repair it.
				}
				else if ( armor.MaxHitPoints <= 0 || armor.HitPoints == armor.MaxHitPoints )
				{
					number = 1044281; // That item is in full repair
				}
				else if ( armor.MaxHitPoints <= toWeaken )
				{
					number = 500424; // You destroyed the item.
					armor.Delete();
				}
				else
				{
					number = 1044279; // You repair the item.
					armor.MaxHitPoints -= toWeaken;
					armor.HitPoints = armor.MaxHitPoints;
					if (m_repair.Uses > 1)
						m_repair.Uses -= 1;
					else
						m_repair.Delete();
				}
				
				from.SendLocalizedMessage( number );

 				if ( armor.MaxHitPoints <= toWeaken )
				from.SendLocalizedMessage( 1044278 ); // That item has been repaired many times, and will break if repairs are attempted again.

			}
			else
			from.SendLocalizedMessage(1044277); // That item cannot be repaired.
       
    	  } 
    }

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 1 ); // version 
         writer.Write( (double) d_SkillLevel );
	 writer.Write( (string) s_Maker );
	 writer.Write( (int) m_uses );
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 

         int version = reader.ReadInt(); 
         d_SkillLevel = (double)reader.ReadDouble(); 
	 s_Maker = (string)reader.ReadString(); 
	 if (version == 0)
	 	m_uses = 8;
	 else
	 	m_uses = reader.ReadInt();
	 
      }
   } 
} 
