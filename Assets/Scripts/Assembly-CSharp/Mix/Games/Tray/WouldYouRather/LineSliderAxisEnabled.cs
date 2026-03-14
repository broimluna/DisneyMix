using System;
using UnityEngine;

namespace Mix.Games.Tray.WouldYouRather
{
	[Serializable]
	public class LineSliderAxisEnabled
	{
		public bool x;

		public bool y;

		public bool z;

		public Vector3 mask
		{
			get
			{
				Vector3 one = Vector3.one;
				if (!x)
				{
					one.x = 0f;
				}
				if (!y)
				{
					one.y = 0f;
				}
				if (!z)
				{
					one.z = 0f;
				}
				return one;
			}
		}

		public LineSliderAxisEnabled()
		{
			x = true;
			y = true;
			z = true;
		}
	}
}
