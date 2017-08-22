using System;
using Server;
using Server.Gumps;

namespace Knives.Utils
{
	public class ButtonPlus : GumpButton
	{
		private string c_Name;
		private object c_Callback;
		private object c_Param;

		public string Name{ get{ return c_Name; } }

		public ButtonPlus( int x, int y, int normalID, int pressedID, int buttonID, string name, TimerCallback back ) : base( x, y, normalID, pressedID, buttonID, GumpButtonType.Reply, 0 )
		{
			c_Name = name;
			c_Callback = back;
			c_Param = "";
		}

		public ButtonPlus( int x, int y, int normalID, int pressedID, int buttonID, string name, TimerStateCallback back, object param ) : base( x, y, normalID, pressedID, buttonID, GumpButtonType.Reply, 0 )
		{
			c_Name = name;
			c_Callback = back;
			c_Param = param;
		}

		public void Invoke()
		{
			if ( c_Callback is TimerCallback )
				((TimerCallback)c_Callback).Invoke();
			else if ( c_Callback is TimerStateCallback )
				((TimerStateCallback)c_Callback).Invoke( c_Param );
		}
	}
}