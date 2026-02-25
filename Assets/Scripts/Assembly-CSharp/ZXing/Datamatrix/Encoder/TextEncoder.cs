using System.Text;

namespace ZXing.Datamatrix.Encoder
{
	internal sealed class TextEncoder : C40Encoder
	{
		public override int EncodingMode
		{
			get
			{
				return 2;
			}
		}

		protected override int encodeChar(char c, StringBuilder sb)
		{
			switch (c)
			{
			case ' ':
				sb.Append('\u0003');
				return 1;
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				sb.Append((char)(c - 48 + 4));
				return 1;
			default:
				if (c >= 'a' && c <= 'z')
				{
					sb.Append((char)(c - 97 + 14));
					return 1;
				}
				if (c >= '\0' && c <= '\u001f')
				{
					sb.Append('\0');
					sb.Append(c);
					return 2;
				}
				if (c >= '!' && c <= '/')
				{
					sb.Append('\u0001');
					sb.Append((char)(c - 33));
					return 2;
				}
				if (c >= ':' && c <= '@')
				{
					sb.Append('\u0001');
					sb.Append((char)(c - 58 + 15));
					return 2;
				}
				if (c >= '[' && c <= '_')
				{
					sb.Append('\u0001');
					sb.Append((char)(c - 91 + 22));
					return 2;
				}
				switch (c)
				{
				case '`':
					sb.Append('\u0002');
					sb.Append((char)(c - 96));
					return 2;
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
					sb.Append('\u0002');
					sb.Append((char)(c - 65 + 1));
					return 2;
				default:
					if (c >= '{' && c <= '\u007f')
					{
						sb.Append('\u0002');
						sb.Append((char)(c - 123 + 27));
						return 2;
					}
					if (c >= '\u0080')
					{
						sb.Append("\u0001\u001e");
						int num = 2;
						return num + encodeChar((char)(c - 128), sb);
					}
					HighLevelEncoder.illegalCharacter(c);
					return -1;
				}
			}
		}
	}
}
