using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Common
{
	public class TrayGamePhysics
	{
		public static void ResetAllGameLayers()
		{
			List<int> list = new List<int>();
			list.Add(LayerMask.NameToLayer("Game3D"));
			list.Add(LayerMask.NameToLayer("Game3D A"));
			list.Add(LayerMask.NameToLayer("Game3D B"));
			list.Add(LayerMask.NameToLayer("Game3D C"));
			list.Add(LayerMask.NameToLayer("Game3D UI"));
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = i; j < list.Count; j++)
				{
					Physics.IgnoreLayerCollision(list[i], list[j], false);
					Physics2D.IgnoreLayerCollision(list[i], list[j], false);
				}
			}
		}
	}
}
