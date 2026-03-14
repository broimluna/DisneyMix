using UnityEngine;
using ZXing.Common;
using ZXing.Rendering;

namespace ZXing
{
	public class Color32Renderer : IBarcodeRenderer<Color32[]>
	{
		public Color32[] Render(BitMatrix matrix, BarcodeFormat format, string content)
		{
			return Render(matrix, format, content, null);
		}

		public Color32[] Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
		{
			Color32[] array = new Color32[matrix.Width * matrix.Height];
			int num = matrix.Height - 1;
			for (int i = 0; i < matrix.Height; i++)
			{
				BitArray row = matrix.getRow(num - i, null);
				int[] array2 = row.Array;
				for (int j = 0; j < array2.Length; j++)
				{
					for (int k = 0; k < 32; k++)
					{
						int num2 = (array2[j] >> k) & 1;
						if (num2 == 1)
						{
							array[256 * i + j * 32 + k] = new Color32(0, 0, 0, byte.MaxValue);
						}
						else
						{
							array[256 * i + j * 32 + k] = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
						}
					}
				}
			}
			return array;
		}
	}
}
