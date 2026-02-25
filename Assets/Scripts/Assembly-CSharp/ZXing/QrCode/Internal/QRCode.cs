using System.Text;

namespace ZXing.QrCode.Internal
{
	public sealed class QRCode
	{
		public static int NUM_MASK_PATTERNS = 8;

		public Mode Mode { get; set; }

		public ErrorCorrectionLevel ECLevel { get; set; }

		public Version Version { get; set; }

		public int MaskPattern { get; set; }

		public ByteMatrix Matrix { get; set; }

		public QRCode()
		{
			MaskPattern = -1;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("<<\n");
			stringBuilder.Append(" mode: ");
			stringBuilder.Append(Mode);
			stringBuilder.Append("\n ecLevel: ");
			stringBuilder.Append(ECLevel);
			stringBuilder.Append("\n version: ");
			if (Version == null)
			{
				stringBuilder.Append("null");
			}
			else
			{
				stringBuilder.Append(Version);
			}
			stringBuilder.Append("\n maskPattern: ");
			stringBuilder.Append(MaskPattern);
			if (Matrix == null)
			{
				stringBuilder.Append("\n matrix: null\n");
			}
			else
			{
				stringBuilder.Append("\n matrix:\n");
				stringBuilder.Append(Matrix.ToString());
			}
			stringBuilder.Append(">>\n");
			return stringBuilder.ToString();
		}

		public static bool isValidMaskPattern(int maskPattern)
		{
			return maskPattern >= 0 && maskPattern < NUM_MASK_PATTERNS;
		}
	}
}
