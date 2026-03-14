using System;

namespace ZXing.PDF417.Internal
{
	public sealed class DetectionResultRowIndicatorColumn : DetectionResultColumn
	{
		public bool IsLeft { get; set; }

		public DetectionResultRowIndicatorColumn(BoundingBox box, bool isLeft)
			: base(box)
		{
			IsLeft = isLeft;
		}

		public void setRowNumbers()
		{
			Codeword[] codewords = base.Codewords;
			foreach (Codeword codeword in codewords)
			{
				if (codeword != null)
				{
					codeword.setRowNumberAsRowIndicatorColumn();
				}
			}
		}

		public int adjustCompleteIndicatorColumnRowNumbers(BarcodeMetadata metadata)
		{
			Codeword[] codewords = base.Codewords;
			setRowNumbers();
			removeIncorrectCodewords(codewords, metadata);
			ResultPoint resultPoint = ((!IsLeft) ? base.Box.TopRight : base.Box.TopLeft);
			ResultPoint resultPoint2 = ((!IsLeft) ? base.Box.BottomRight : base.Box.BottomLeft);
			int num = imageRowToCodewordIndex((int)resultPoint.Y);
			int num2 = imageRowToCodewordIndex((int)resultPoint2.Y);
			float num3 = (float)(num2 - num) / (float)metadata.RowCount;
			int num4 = -1;
			int num5 = 1;
			int num6 = 0;
			for (int i = num; i < num2; i++)
			{
				Codeword codeword = codewords[i];
				if (codeword == null)
				{
					continue;
				}
				int num7 = codeword.RowNumber - num4;
				if (num7 == 0)
				{
					num6++;
					continue;
				}
				if (num7 == 1)
				{
					num5 = Math.Max(num5, num6);
					num6 = 1;
					num4 = codeword.RowNumber;
					continue;
				}
				if (num7 < 0 || codeword.RowNumber >= metadata.RowCount || num7 > i)
				{
					codewords[i] = null;
					continue;
				}
				int num8 = ((num5 <= 2) ? num7 : ((num5 - 2) * num7));
				bool flag = num8 > i;
				for (int j = 1; j <= num8; j++)
				{
					if (flag)
					{
						break;
					}
					flag = codewords[i - j] != null;
				}
				if (flag)
				{
					codewords[i] = null;
					continue;
				}
				num4 = codeword.RowNumber;
				num6 = 1;
			}
			return (int)((double)num3 + 0.5);
		}

		public int[] getRowHeights()
		{
			BarcodeMetadata barcodeMetadata = getBarcodeMetadata();
			if (barcodeMetadata == null)
			{
				return null;
			}
			adjustIncompleteIndicatorColumnRowNumbers(barcodeMetadata);
			int[] array = new int[barcodeMetadata.RowCount];
			Codeword[] codewords = base.Codewords;
			foreach (Codeword codeword in codewords)
			{
				if (codeword != null)
				{
					int rowNumber = codeword.RowNumber;
					if (rowNumber >= array.Length)
					{
						return null;
					}
					array[rowNumber]++;
				}
			}
			return array;
		}

		public int adjustIncompleteIndicatorColumnRowNumbers(BarcodeMetadata metadata)
		{
			ResultPoint resultPoint = ((!IsLeft) ? base.Box.TopRight : base.Box.TopLeft);
			ResultPoint resultPoint2 = ((!IsLeft) ? base.Box.BottomRight : base.Box.BottomLeft);
			int num = imageRowToCodewordIndex((int)resultPoint.Y);
			int num2 = imageRowToCodewordIndex((int)resultPoint2.Y);
			float num3 = (float)(num2 - num) / (float)metadata.RowCount;
			Codeword[] codewords = base.Codewords;
			int num4 = -1;
			int val = 1;
			int num5 = 0;
			for (int i = num; i < num2; i++)
			{
				Codeword codeword = codewords[i];
				if (codeword == null)
				{
					continue;
				}
				codeword.setRowNumberAsRowIndicatorColumn();
				switch (codeword.RowNumber - num4)
				{
				case 0:
					num5++;
					continue;
				case 1:
					val = Math.Max(val, num5);
					num5 = 1;
					num4 = codeword.RowNumber;
					continue;
				}
				if (codeword.RowNumber > metadata.RowCount)
				{
					base.Codewords[i] = null;
					continue;
				}
				num4 = codeword.RowNumber;
				num5 = 1;
			}
			return (int)((double)num3 + 0.5);
		}

		public BarcodeMetadata getBarcodeMetadata()
		{
			Codeword[] codewords = base.Codewords;
			BarcodeValue barcodeValue = new BarcodeValue();
			BarcodeValue barcodeValue2 = new BarcodeValue();
			BarcodeValue barcodeValue3 = new BarcodeValue();
			BarcodeValue barcodeValue4 = new BarcodeValue();
			Codeword[] array = codewords;
			foreach (Codeword codeword in array)
			{
				if (codeword != null)
				{
					codeword.setRowNumberAsRowIndicatorColumn();
					int num = codeword.Value % 30;
					int num2 = codeword.RowNumber;
					if (!IsLeft)
					{
						num2 += 2;
					}
					switch (num2 % 3)
					{
					case 0:
						barcodeValue2.setValue(num * 3 + 1);
						break;
					case 1:
						barcodeValue4.setValue(num / 3);
						barcodeValue3.setValue(num % 3);
						break;
					case 2:
						barcodeValue.setValue(num + 1);
						break;
					}
				}
			}
			int[] value = barcodeValue.getValue();
			int[] value2 = barcodeValue2.getValue();
			int[] value3 = barcodeValue3.getValue();
			int[] value4 = barcodeValue4.getValue();
			if (value.Length == 0 || value2.Length == 0 || value3.Length == 0 || value4.Length == 0 || value[0] < 1 || value2[0] + value3[0] < PDF417Common.MIN_ROWS_IN_BARCODE || value2[0] + value3[0] > PDF417Common.MAX_ROWS_IN_BARCODE)
			{
				return null;
			}
			BarcodeMetadata barcodeMetadata = new BarcodeMetadata(value[0], value2[0], value3[0], value4[0]);
			removeIncorrectCodewords(codewords, barcodeMetadata);
			return barcodeMetadata;
		}

		private void removeIncorrectCodewords(Codeword[] codewords, BarcodeMetadata metadata)
		{
			for (int i = 0; i < codewords.Length; i++)
			{
				Codeword codeword = codewords[i];
				if (codeword == null)
				{
					continue;
				}
				int num = codeword.Value % 30;
				int num2 = codeword.RowNumber;
				if (num2 >= metadata.RowCount)
				{
					codewords[i] = null;
					continue;
				}
				if (!IsLeft)
				{
					num2 += 2;
				}
				switch (num2 % 3)
				{
				default:
					if (num * 3 + 1 != metadata.RowCountUpper)
					{
						codewords[i] = null;
					}
					break;
				case 1:
					if (num % 3 != metadata.RowCountLower || num / 3 != metadata.ErrorCorrectionLevel)
					{
						codewords[i] = null;
					}
					break;
				case 2:
					if (num + 1 != metadata.ColumnCount)
					{
						codewords[i] = null;
					}
					break;
				}
			}
		}

		public override string ToString()
		{
			return "Is Left: " + IsLeft + " \n" + base.ToString();
		}
	}
}
