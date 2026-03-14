using System;
using UnityEngine;

namespace Mix.Games.Tray.FortuneCookie
{
	public class CookieCollisionEnterEventArgs : EventArgs
	{
		private Collision _collision;

		public Collision Collision
		{
			get
			{
				return _collision;
			}
		}

		public CookieCollisionEnterEventArgs(Collision c)
		{
			_collision = c;
		}
	}
}
