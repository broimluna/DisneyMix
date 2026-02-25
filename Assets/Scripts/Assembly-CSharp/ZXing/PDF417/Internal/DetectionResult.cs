using System.Globalization;
using System.Text;

namespace ZXing.PDF417.Internal
{
	public class DetectionResult
	{
		private const int ADJUST_ROW_NUMBER_SKIP = 2;

		public BarcodeMetadata Metadata { get; private set; }

		public DetectionResultColumn[] DetectionResultColumns { get; set; }

		public BoundingBox Box { get; set; }

		public int ColumnCount { get; private set; }

		public int RowCount
		{
			get
			{
				return Metadata.RowCount;
			}
		}

		public int ErrorCorrectionLevel
		{
			get
			{
				return Metadata.ErrorCorrectionLevel;
			}
		}

		public DetectionResult(BarcodeMetadata metadata, BoundingBox box)
		{
			Metadata = metadata;
			Box = box;
			ColumnCount = metadata.ColumnCount;
			DetectionResultColumns = new DetectionResultColumn[ColumnCount + 2];
		}

		public DetectionResultColumn[] getDetectionResultColumns()
		{
			adjustIndicatorColumnRowNumbers(DetectionResultColumns[0]);
			adjustIndicatorColumnRowNumbers(DetectionResultColumns[ColumnCount + 1]);
			int num = PDF417Common.MAX_CODEWORDS_IN_BARCODE;
			int num2;
			do
			{
				num2 = num;
				num = adjustRowNumbers();
			}
			while (num > 0 && num < num2);
			return DetectionResultColumns;
		}

		private void adjustIndicatorColumnRowNumbers(DetectionResultColumn detectionResultColumn)
		{
			if (detectionResultColumn != null)
			{
				((DetectionResultRowIndicatorColumn)detectionResultColumn).adjustCompleteIndicatorColumnRowNumbers(Metadata);
			}
		}

		private int adjustRowNumbers()
		{
			int num = adjustRowNumbersByRow();
			if (num == 0)
			{
				return 0;
			}
			for (int i = 1; i < ColumnCount + 1; i++)
			{
				Codeword[] codewords = DetectionResultColumns[i].Codewords;
				for (int j = 0; j < codewords.Length; j++)
				{
					if (codewords[j] != null && !codewords[j].HasValidRowNumber)
					{
						adjustRowNumbers(i, j, codewords);
					}
				}
			}
			return num;
		}

		private int adjustRowNumbersByRow()
		{
			adjustRowNumbersFromBothRI();
			int num = adjustRowNumbersFromLRI();
			return num + adjustRowNumbersFromRRI();
		}

		private void adjustRowNumbersFromBothRI()
		{
			if (DetectionResultColumns[0] == null || DetectionResultColumns[ColumnCount + 1] == null)
			{
				return;
			}
			Codeword[] codewords = DetectionResultColumns[0].Codewords;
			Codeword[] codewords2 = DetectionResultColumns[ColumnCount + 1].Codewords;
			for (int i = 0; i < codewords.Length; i++)
			{
				if (codewords[i] == null || codewords2[i] == null || codewords[i].RowNumber != codewords2[i].RowNumber)
				{
					continue;
				}
				for (int j = 1; j <= ColumnCount; j++)
				{
					Codeword codeword = DetectionResultColumns[j].Codewords[i];
					if (codeword != null)
					{
						codeword.RowNumber = codewords[i].RowNumber;
						if (!codeword.HasValidRowNumber)
						{
							DetectionResultColumns[j].Codewords[i] = null;
						}
					}
				}
			}
		}

		private int adjustRowNumbersFromRRI()
		{
			if (DetectionResultColumns[ColumnCount + 1] == null)
			{
				return 0;
			}
			int num = 0;
			Codeword[] codewords = DetectionResultColumns[ColumnCount + 1].Codewords;
			for (int i = 0; i < codewords.Length; i++)
			{
				if (codewords[i] == null)
				{
					continue;
				}
				int rowNumber = codewords[i].RowNumber;
				int num2 = 0;
				int num3 = ColumnCount + 1;
				while (num3 > 0 && num2 < 2)
				{
					Codeword codeword = DetectionResultColumns[num3].Codewords[i];
					if (codeword != null)
					{
						num2 = adjustRowNumberIfValid(rowNumber, num2, codeword);
						if (!codeword.HasValidRowNumber)
						{
							num++;
						}
					}
					num3--;
				}
			}
			return num;
		}

		private int adjustRowNumbersFromLRI()
		{
			if (DetectionResultColumns[0] == null)
			{
				return 0;
			}
			int num = 0;
			Codeword[] codewords = DetectionResultColumns[0].Codewords;
			for (int i = 0; i < codewords.Length; i++)
			{
				if (codewords[i] == null)
				{
					continue;
				}
				int rowNumber = codewords[i].RowNumber;
				int num2 = 0;
				for (int j = 1; j < ColumnCount + 1; j++)
				{
					if (num2 >= 2)
					{
						break;
					}
					Codeword codeword = DetectionResultColumns[j].Codewords[i];
					if (codeword != null)
					{
						num2 = adjustRowNumberIfValid(rowNumber, num2, codeword);
						if (!codeword.HasValidRowNumber)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		private static int adjustRowNumberIfValid(int rowIndicatorRowNumber, int invalidRowCounts, Codeword codeword)
		{
			if (codeword == null)
			{
				return invalidRowCounts;
			}
			if (!codeword.HasValidRowNumber)
			{
				if (codeword.IsValidRowNumber(rowIndicatorRowNumber))
				{
					codeword.RowNumber = rowIndicatorRowNumber;
					invalidRowCounts = 0;
				}
				else
				{
					invalidRowCounts++;
				}
			}
			return invalidRowCounts;
		}

		private void adjustRowNumbers(int barcodeColumn, int codewordsRow, Codeword[] codewords)
		{
			Codeword codeword = codewords[codewordsRow];
			Codeword[] codewords2 = DetectionResultColumns[barcodeColumn - 1].Codewords;
			Codeword[] array = codewords2;
			if (DetectionResultColumns[barcodeColumn + 1] != null)
			{
				array = DetectionResultColumns[barcodeColumn + 1].Codewords;
			}
			Codeword[] array2 = new Codeword[14]
			{
				null,
				null,
				codewords2[codewordsRow],
				array[codewordsRow],
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null
			};
			if (codewordsRow > 0)
			{
				array2[0] = codewords[codewordsRow - 1];
				array2[4] = codewords2[codewordsRow - 1];
				array2[5] = array[codewordsRow - 1];
			}
			if (codewordsRow > 1)
			{
				array2[8] = codewords[codewordsRow - 2];
				array2[10] = codewords2[codewordsRow - 2];
				array2[11] = array[codewordsRow - 2];
			}
			if (codewordsRow < codewords.Length - 1)
			{
				array2[1] = codewords[codewordsRow + 1];
				array2[6] = codewords2[codewordsRow + 1];
				array2[7] = array[codewordsRow + 1];
			}
			if (codewordsRow < codewords.Length - 2)
			{
				array2[9] = codewords[codewordsRow + 2];
				array2[12] = codewords2[codewordsRow + 2];
				array2[13] = array[codewordsRow + 2];
			}
			Codeword[] array3 = array2;
			foreach (Codeword otherCodeword in array3)
			{
				if (adjustRowNumber(codeword, otherCodeword))
				{
					break;
				}
			}
		}

		private static bool adjustRowNumber(Codeword codeword, Codeword otherCodeword)
		{
			if (otherCodeword == null)
			{
				return false;
			}
			if (otherCodeword.HasValidRowNumber && otherCodeword.Bucket == codeword.Bucket)
			{
				codeword.RowNumber = otherCodeword.RowNumber;
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DetectionResultColumn detectionResultColumn = DetectionResultColumns[0];
			if (detectionResultColumn == null)
			{
				detectionResultColumn = DetectionResultColumns[ColumnCount + 1];
			}
			for (int i = 0; i < detectionResultColumn.Codewords.Length; i++)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "CW {0,3}:", i);
				for (int j = 0; j < ColumnCount + 2; j++)
				{
					if (DetectionResultColumns[j] == null)
					{
						stringBuilder.Append("    |   ");
						continue;
					}
					Codeword codeword = DetectionResultColumns[j].Codewords[i];
					if (codeword == null)
					{
						stringBuilder.Append("    |   ");
						continue;
					}
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0,3}|{1,3}", codeword.RowNumber, codeword.Value);
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}
	}
}
