using ZXing.Common;

namespace ZXing.Rendering
{
	public class RawRenderer : IBarcodeRenderer<byte[]>
	{
		public struct Color
		{
			public static Color Black = new Color(0);

			public static Color White = new Color(16777215);

			public byte A;

			public byte R;

			public byte G;

			public byte B;

			public Color(int color)
			{
				A = (byte)((color & 0xFF000000u) >> 24);
				R = (byte)((color & 0xFF0000) >> 16);
				G = (byte)((color & 0xFF00) >> 8);
				B = (byte)(color & 0xFF);
			}
		}

		public Color Foreground { get; set; }

		public Color Background { get; set; }

		public RawRenderer()
		{
			Foreground = Color.Black;
			Background = Color.White;
		}

		public byte[] Render(BitMatrix matrix, BarcodeFormat format, string content)
		{
			return Render(matrix, format, content, null);
		}

		public virtual byte[] Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
		{
			int width = matrix.Width;
			int height = matrix.Height;
			int num = (((options == null || !options.PureBarcode) && !string.IsNullOrEmpty(content) && (format == BarcodeFormat.CODE_39 || format == BarcodeFormat.CODE_128 || format == BarcodeFormat.EAN_13 || format == BarcodeFormat.EAN_8 || format == BarcodeFormat.CODABAR || format == BarcodeFormat.ITF || format == BarcodeFormat.UPC_A || format == BarcodeFormat.MSI || format == BarcodeFormat.PLESSEY)) ? 16 : 0);
			int num2 = 1;
			if (options != null)
			{
				if (options.Width > width)
				{
					width = options.Width;
				}
				if (options.Height > height)
				{
					height = options.Height;
				}
				num2 = width / matrix.Width;
				if (num2 > height / matrix.Height)
				{
					num2 = height / matrix.Height;
				}
			}
			byte[] array = new byte[width * height * 4];
			int num3 = 0;
			for (int i = 0; i < matrix.Height - num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < matrix.Width; k++)
					{
						Color color = ((!matrix[k, i]) ? Background : Foreground);
						for (int l = 0; l < num2; l++)
						{
							array[num3++] = color.A;
							array[num3++] = color.R;
							array[num3++] = color.G;
							array[num3++] = color.B;
						}
					}
					for (int m = num2 * matrix.Width; m < width; m++)
					{
						array[num3++] = Background.A;
						array[num3++] = Background.R;
						array[num3++] = Background.G;
						array[num3++] = Background.B;
					}
				}
			}
			for (int n = matrix.Height * num2 - num; n < height; n++)
			{
				for (int num4 = 0; num4 < width; num4++)
				{
					array[num3++] = Background.A;
					array[num3++] = Background.R;
					array[num3++] = Background.G;
					array[num3++] = Background.B;
				}
			}
			return array;
		}
	}
}
