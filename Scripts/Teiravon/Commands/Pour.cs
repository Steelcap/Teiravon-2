using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;

namespace Server.Scripts.Commands
{
	public class Pour
	{
		public static void Initialize()
		{
			Server.Commands.Register( "Pour", AccessLevel.Player, new CommandEventHandler( Pour_OnCommand ) );
		}

		[Usage( "Pour" )]
		[Description( "Pours the liquid from a targeted potion." )]
		private static void Pour_OnCommand(CommandEventArgs e)
		{
			e.Mobile.Target = new PourTarget( true );
			e.Mobile.SendMessage( "What do you wish to pour out?");
		}

		private class PourTarget : Target
		{
            public PourTarget(bool isPitcher)
                : base(15, false, TargetFlags.None)
            {
            }

            Type[] typesList = new Type[] { typeof(BasePotion), typeof(BaseBeverage) };

            protected override void OnTarget(Mobile from, object targ)
			{
                if (targ is Item)
                {
                    Item pour = (Item)targ;

                    if ((targ is Item) && pour.IsChildOf(from.Backpack))
                    {
                        if (pour.GetType().IsSubclassOf(typesList[0]))
                        {
                            pour.Consume();
                            Item newItem = new Bottle();
                            from.AddToBackpack(newItem);
                            from.SendMessage("You pour out the contents of the bottle and place the empty bottle in your pack.");
                        }
                        else if (pour.GetType().IsSubclassOf(typesList[1]))
                        {
                            pour.Delete();
                            from.SendMessage("'...and some for my mates.' You pour out the liquid in remembrance of those fallen.");
                        }
                        else
                        {
                            from.SendMessage("That is not a liquid.");
                        }
                    }
                    else
                    {
                        from.SendMessage("That must be in your pack");
                    }
                }
                else
                {
                    from.SendMessage("You must target an item.");
                }
			}
                
		}
	}
}