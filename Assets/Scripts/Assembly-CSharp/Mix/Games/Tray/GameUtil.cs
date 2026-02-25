using System.Globalization;
using UnityEngine;

namespace Mix.Games.Tray
{
	public class GameUtil
	{
		public const int Layer_Game3D = 12;

		public const int Layer_Game3D_UI = 13;

		public const int Layer_Game3D_A = 14;

		public const int Layer_Game3D_B = 15;

		public const int Layer_Game3D_C = 16;

		public static int GetLayerMask(int aLayer)
		{
			return 1 << aLayer;
		}

		public static void SetLayerRecursively(GameObject aObj, int aLayer)
		{
			aObj.layer = aLayer;
			Transform[] componentsInChildren = aObj.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = aLayer;
			}
		}

		public static Color HexToColor(string hex)
		{
			if (!string.IsNullOrEmpty(hex))
			{
				if (hex.StartsWith("0x"))
				{
					hex = hex.Substring(2);
				}
				byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
				byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
				byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
				byte a = byte.MaxValue;
				if (hex.Length >= 8)
				{
					a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
				}
				return new Color32(r, g, b, a);
			}
			return Color.white;
		}
	}
}
