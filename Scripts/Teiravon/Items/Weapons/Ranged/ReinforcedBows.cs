using System;
using Server;

namespace Server.Items
{
	public class ReinforcedBowFrame : Bow
	{
		[Constructable]
		public ReinforcedBowFrame()
		{
			Name = "a reinforced shortbow frame";
			Layer = Layer.Invalid;
		}
		
		public ReinforcedBowFrame( Serial serial ) : base( serial )
		{
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);
		}
	}

	public class ReinforcedLongbowFrame : Longbow
	{
		[Constructable]
		public ReinforcedLongbowFrame()
		{
			Name = "a reinforced longbow frame";
			Layer = Layer.Invalid;
		}
		
		public ReinforcedLongbowFrame( Serial serial ) : base( serial )
		{
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);
		}
	}
	
	public class ReinforcedHandXBowFrame : HandCrossbow
	{
		[Constructable]
		public ReinforcedHandXBowFrame()
		{
			Name = "a reinforced hand crossbow frame";
			Layer = Layer.Invalid;
		}
		
		public ReinforcedHandXBowFrame( Serial serial ) : base( serial )
		{
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);
		}
	}
}