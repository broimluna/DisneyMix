using UnityEngine;

namespace ZXing
{
	public class Color32LuminanceSource : BaseLuminanceSource
	{
		public Color32LuminanceSource(int width, int height)
			: base(width, height)
		{
		}

		public Color32LuminanceSource(Color32[] color32s, int width, int height)
			: base(width, height)
		{
			SetPixels(color32s);
		}

		public void SetPixels(Color32[] color32s)
		{
			int num = 0;
			for (int num2 = Height - 1; num2 >= 0; num2--)
			{
				for (int i = 0; i < Width; i++)
				{
					Color32 color = color32s[num2 * Width + i];
					luminances[num++] = (byte)(color.r + color.g + color.g + color.b >> 2);
				}
			}
		}

		protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
		{
			Color32LuminanceSource color32LuminanceSource = new Color32LuminanceSource(width, height);
			color32LuminanceSource.luminances = newLuminances;
			return color32LuminanceSource;
		}
	}
}
