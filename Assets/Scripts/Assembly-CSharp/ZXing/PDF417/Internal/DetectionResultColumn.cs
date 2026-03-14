using System.Globalization;
using System.Text;

namespace ZXing.PDF417.Internal
{
	public class DetectionResultColumn
	{
		private const int MAX_NEARBY_DISTANCE = 5;

		public BoundingBox Box { get; private set; }

		public Codeword[] Codewords { get; set; }

		public DetectionResultColumn(BoundingBox box)
		{
			Box = BoundingBox.Create(box);
			Codewords = new Codeword[Box.MaxY - Box.MinY + 1];
		}

		public int IndexForRow(int imageRow)
		{
			return imageRow - Box.MinY;
		}

		public int RowForIndex(int codewordIndex)
		{
			return Box.MinY + codewordIndex;
		}

		public Codeword getCodeword(int imageRow)
		{
			return Codewords[imageRowToCodewordIndex(imageRow)];
		}

		public Codeword getCodewordNearby(int imageRow)
		{
			Codeword codeword = getCodeword(imageRow);
			if (codeword != null)
			{
				return codeword;
			}
			for (int i = 1; i < 5; i++)
			{
				int num = imageRowToCodewordIndex(imageRow) - i;
				if (num >= 0)
				{
					codeword = Codewords[num];
					if (codeword != null)
					{
						return codeword;
					}
				}
				num = imageRowToCodewordIndex(imageRow) + i;
				if (num < Codewords.Length)
				{
					codeword = Codewords[num];
					if (codeword != null)
					{
						return codeword;
					}
				}
			}
			return null;
		}

		internal int imageRowToCodewordIndex(int imageRow)
		{
			return imageRow - Box.MinY;
		}

		public void setCodeword(int imageRow, Codeword codeword)
		{
			Codewords[IndexForRow(imageRow)] = codeword;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			Codeword[] codewords = Codewords;
			foreach (Codeword codeword in codewords)
			{
				if (codeword == null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0,3}:    |   \n", num++);
					continue;
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0,3}: {1,3}|{2,3}\n", num++, codeword.RowNumber, codeword.Value);
			}
			return stringBuilder.ToString();
		}
	}
}
