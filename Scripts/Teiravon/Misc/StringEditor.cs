using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Scripts.Commands;
using Server.Targeting;

namespace Server.Gumps
{
    public class StringEditorGump : Gump
    {
        #region Commands
        public static void Initialize()
        {
            Commands.Register( "EditString", AccessLevel.Counselor, new CommandEventHandler( EditString_OnCommand ) );
        }

        public static void EditString_OnCommand( CommandEventArgs args )
        {
            Mobile from = args.Mobile;

            if ( args.ArgString == string.Empty )
            {
                from.SendMessage( "Usage: EditString <propertyName>" );
                return;
            }
            else
            {
                from.Target = new InternalTarget( args.ArgString );
            }
        }

        private class InternalTarget : Target
        {
            string m_Name;

            public InternalTarget( string name )
                : base( -1, false, TargetFlags.None )
            {
                m_Name = name;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                from.SendGump( new StringEditorGump( from, m_Name, targeted ) );
            }
        }
        #endregion

        private string m_Name = "";
        private object m_Target;

        public enum Buttons
        {
            LineOne = 1001,
            LineTwo,
            LineThree,
            LineFour,

            Okay = 2001,
            Cancel,
        }

        public StringEditorGump( Mobile from, string name, object target )
            : base( 100, 100 )
        {
            Closable = true;
            Dragable = true;

            m_Target = target;
            m_Name = name;

            AddPage( 0 );
            AddBackground( 14, 13, 600, 352, 9270 );

            AddLabel( 271, 35, 0xA00, @"String Editor" );

            AddBackground( 38, 217, 550, 20, 9200 );
            AddTextEntry( 38, 217, 549, 20, 0, ( int )Buttons.LineOne, @"" );

            AddBackground( 38, 241, 550, 20, 9200 );
            AddTextEntry( 38, 241, 549, 20, 0, ( int )Buttons.LineTwo, @"" );

            AddBackground( 38, 264, 550, 20, 9200 );
            AddTextEntry( 38, 265, 549, 20, 0, ( int )Buttons.LineThree, @"" );

            AddBackground( 38, 288, 550, 20, 9200 );
            AddTextEntry( 38, 289, 549, 20, 0, ( int )Buttons.LineFour, @"" );

            AddHtml( 38, 80, 549, 100, Properties.GetValue( from, target, name ), ( bool )true, ( bool )true );

            AddButton( 237, 319, 247, 248, ( int )Buttons.Okay, GumpButtonType.Reply, 0 );
            AddButton( 332, 319, 241, 242, ( int )Buttons.Cancel, GumpButtonType.Reply, 0 );
        }

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile from = sender.Mobile;

            if ( info.ButtonID < 1000 || info.ButtonID == ( int )Buttons.Cancel )
                return;

            switch ( ( Buttons )info.ButtonID )
            {
                case Buttons.Okay:

                    from.SendMessage( Properties.SetValue(
                        from, m_Target, m_Name,
                        String.Format( "{0} {1} {2} {3}",
                            info.TextEntries[0].Text,
                            info.TextEntries[1].Text,
                            info.TextEntries[2].Text,
                            info.TextEntries[3].Text ).Trim()
                    ) );

                    break;
            }
        }
    }
}