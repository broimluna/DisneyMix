using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ZXing.Common;
using ZXing.PDF417.Internal.EC;

namespace ZXing.PDF417.Internal
{
	public static class PDF417ScanningDecoder
	{
		private const int CODEWORD_SKEW_SIZE = 2;

		private const int MAX_ERRORS = 3;

		private const int MAX_EC_CODEWORDS = 512;

		private static readonly ErrorCorrection errorCorrection = new ErrorCorrection();

		public static DecoderResult decode(BitMatrix image, ResultPoint imageTopLeft, ResultPoint imageBottomLeft, ResultPoint imageTopRight, ResultPoint imageBottomRight, int minCodewordWidth, int maxCodewordWidth)
		{
			BoundingBox boundingBox = BoundingBox.Create(image, imageTopLeft, imageBottomLeft, imageTopRight, imageBottomRight);
			if (boundingBox == null)
			{
				return null;
			}
			DetectionResultRowIndicatorColumn detectionResultRowIndicatorColumn = null;
			DetectionResultRowIndicatorColumn detectionResultRowIndicatorColumn2 = null;
			DetectionResult detectionResult = null;
			for (int i = 0; i < 2; i++)
			{
				if (imageTopLeft != null)
				{
					detectionResultRowIndicatorColumn = getRowIndicatorColumn(image, boundingBox, imageTopLeft, true, minCodewordWidth, maxCodewordWidth);
				}
				if (imageTopRight != null)
				{
					detectionResultRowIndicatorColumn2 = getRowIndicatorColumn(image, boundingBox, imageTopRight, false, minCodewordWidth, maxCodewordWidth);
				}
				detectionResult = merge(detectionResultRowIndicatorColumn, detectionResultRowIndicatorColumn2);
				if (detectionResult == null)
				{
					return null;
				}
				if (i == 0 && detectionResult.Box != null && (detectionResult.Box.MinY < boundingBox.MinY || detectionResult.Box.MaxY > boundingBox.MaxY))
				{
					boundingBox = detectionResult.Box;
					continue;
				}
				detectionResult.Box = boundingBox;
				break;
			}
			int num = detectionResult.ColumnCount + 1;
			detectionResult.DetectionResultColumns[0] = detectionResultRowIndicatorColumn;
			detectionResult.DetectionResultColumns[num] = detectionResultRowIndicatorColumn2;
			bool flag = detectionResultRowIndicatorColumn != null;
			for (int j = 1; j <= num; j++)
			{
				int num2 = ((!flag) ? (num - j) : j);
				if (detectionResult.DetectionResultColumns[num2] != null)
				{
					continue;
				}
				DetectionResultColumn detectionResultColumn = ((num2 != 0 && num2 != num) ? new DetectionResultColumn(boundingBox) : new DetectionResultRowIndicatorColumn(boundingBox, num2 == 0));
				detectionResult.DetectionResultColumns[num2] = detectionResultColumn;
				int num3 = -1;
				int num4 = num3;
				for (int k = boundingBox.MinY; k <= boundingBox.MaxY; k++)
				{
					num3 = getStartColumn(detectionResult, num2, k, flag);
					if (num3 < 0 || num3 > boundingBox.MaxX)
					{
						if (num4 == -1)
						{
							continue;
						}
						num3 = num4;
					}
					Codeword codeword = detectCodeword(image, boundingBox.MinX, boundingBox.MaxX, flag, num3, k, minCodewordWidth, maxCodewordWidth);
					if (codeword != null)
					{
						detectionResultColumn.setCodeword(k, codeword);
						num4 = num3;
						minCodewordWidth = Math.Min(minCodewordWidth, codeword.Width);
						maxCodewordWidth = Math.Max(maxCodewordWidth, codeword.Width);
					}
				}
			}
			return createDecoderResult(detectionResult);
		}

		private static DetectionResult merge(DetectionResultRowIndicatorColumn leftRowIndicatorColumn, DetectionResultRowIndicatorColumn rightRowIndicatorColumn)
		{
			if (leftRowIndicatorColumn == null && rightRowIndicatorColumn == null)
			{
				return null;
			}
			BarcodeMetadata barcodeMetadata = getBarcodeMetadata(leftRowIndicatorColumn, rightRowIndicatorColumn);
			if (barcodeMetadata == null)
			{
				return null;
			}
			BoundingBox box = BoundingBox.merge(adjustBoundingBox(leftRowIndicatorColumn), adjustBoundingBox(rightRowIndicatorColumn));
			return new DetectionResult(barcodeMetadata, box);
		}

		private static BoundingBox adjustBoundingBox(DetectionResultRowIndicatorColumn rowIndicatorColumn)
		{
			if (rowIndicatorColumn == null)
			{
				return null;
			}
			int[] rowHeights = rowIndicatorColumn.getRowHeights();
			if (rowHeights == null)
			{
				return null;
			}
			int max = getMax(rowHeights);
			int num = 0;
			int[] array = rowHeights;
			foreach (int num2 in array)
			{
				num += max - num2;
				if (num2 > 0)
				{
					break;
				}
			}
			Codeword[] codewords = rowIndicatorColumn.Codewords;
			int num3 = 0;
			while (num > 0 && codewords[num3] == null)
			{
				num--;
				num3++;
			}
			int num4 = 0;
			for (int num5 = rowHeights.Length - 1; num5 >= 0; num5--)
			{
				num4 += max - rowHeights[num5];
				if (rowHeights[num5] > 0)
				{
					break;
				}
			}
			int num6 = codewords.Length - 1;
			while (num4 > 0 && codewords[num6] == null)
			{
				num4--;
				num6--;
			}
			return rowIndicatorColumn.Box.addMissingRows(num, num4, rowIndicatorColumn.IsLeft);
		}

		private static int getMax(int[] values)
		{
			int num = -1;
			for (int num2 = values.Length - 1; num2 >= 0; num2--)
			{
				num = Math.Max(num, values[num2]);
			}
			return num;
		}

		private static BarcodeMetadata getBarcodeMetadata(DetectionResultRowIndicatorColumn leftRowIndicatorColumn, DetectionResultRowIndicatorColumn rightRowIndicatorColumn)
		{
			BarcodeMetadata barcodeMetadata;
			if (leftRowIndicatorColumn == null || (barcodeMetadata = leftRowIndicatorColumn.getBarcodeMetadata()) == null)
			{
				return (rightRowIndicatorColumn != null) ? rightRowIndicatorColumn.getBarcodeMetadata() : null;
			}
			BarcodeMetadata barcodeMetadata2;
			if (rightRowIndicatorColumn == null || (barcodeMetadata2 = rightRowIndicatorColumn.getBarcodeMetadata()) == null)
			{
				return barcodeMetadata;
			}
			if (barcodeMetadata.ColumnCount != barcodeMetadata2.ColumnCount && barcodeMetadata.ErrorCorrectionLevel != barcodeMetadata2.ErrorCorrectionLevel && barcodeMetadata.RowCount != barcodeMetadata2.RowCount)
			{
				return null;
			}
			return barcodeMetadata;
		}

		private static DetectionResultRowIndicatorColumn getRowIndicatorColumn(BitMatrix image, BoundingBox boundingBox, ResultPoint startPoint, bool leftToRight, int minCodewordWidth, int maxCodewordWidth)
		{
			DetectionResultRowIndicatorColumn detectionResultRowIndicatorColumn = new DetectionResultRowIndicatorColumn(boundingBox, leftToRight);
			for (int i = 0; i < 2; i++)
			{
				int num = ((i == 0) ? 1 : (-1));
				int startColumn = (int)startPoint.X;
				for (int j = (int)startPoint.Y; j <= boundingBox.MaxY && j >= boundingBox.MinY; j += num)
				{
					Codeword codeword = detectCodeword(image, 0, image.Width, leftToRight, startColumn, j, minCodewordWidth, maxCodewordWidth);
					if (codeword != null)
					{
						detectionResultRowIndicatorColumn.setCodeword(j, codeword);
						startColumn = ((!leftToRight) ? codeword.EndX : codeword.StartX);
					}
				}
			}
			return detectionResultRowIndicatorColumn;
		}

		private static bool adjustCodewordCount(DetectionResult detectionResult, BarcodeValue[][] barcodeMatrix)
		{
			int[] value = barcodeMatrix[0][1].getValue();
			int num = detectionResult.ColumnCount * detectionResult.RowCount - getNumberOfECCodeWords(detectionResult.ErrorCorrectionLevel);
			if (value.Length == 0)
			{
				if (num < 1 || num > PDF417Common.MAX_CODEWORDS_IN_BARCODE)
				{
					return false;
				}
				barcodeMatrix[0][1].setValue(num);
			}
			else if (value[0] != num)
			{
				barcodeMatrix[0][1].setValue(num);
			}
			return true;
		}

		private static DecoderResult createDecoderResult(DetectionResult detectionResult)
		{
			BarcodeValue[][] array = createBarcodeMatrix(detectionResult);
			if (!adjustCodewordCount(detectionResult, array))
			{
				return null;
			}
			List<int> list = new List<int>();
			int[] array2 = new int[detectionResult.RowCount * detectionResult.ColumnCount];
			List<int[]> list2 = new List<int[]>();
			List<int> list3 = new List<int>();
			for (int i = 0; i < detectionResult.RowCount; i++)
			{
				for (int j = 0; j < detectionResult.ColumnCount; j++)
				{
					int[] value = array[i][j + 1].getValue();
					int num = i * detectionResult.ColumnCount + j;
					if (value.Length == 0)
					{
						list.Add(num);
						continue;
					}
					if (value.Length == 1)
					{
						array2[num] = value[0];
						continue;
					}
					list3.Add(num);
					list2.Add(value);
				}
			}
			int[][] array3 = new int[list2.Count][];
			for (int k = 0; k < array3.Length; k++)
			{
				array3[k] = list2[k];
			}
			return createDecoderResultFromAmbiguousValues(detectionResult.ErrorCorrectionLevel, array2, list.ToArray(), list3.ToArray(), array3);
		}

		private static DecoderResult createDecoderResultFromAmbiguousValues(int ecLevel, int[] codewords, int[] erasureArray, int[] ambiguousIndexes, int[][] ambiguousIndexValues)
		{
			int[] array = new int[ambiguousIndexes.Length];
			int num = 100;
			while (num-- > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					codewords[ambiguousIndexes[i]] = ambiguousIndexValues[i][array[i]];
				}
				try
				{
					DecoderResult decoderResult = decodeCodewords(codewords, ecLevel, erasureArray);
					if (decoderResult != null)
					{
						return decoderResult;
					}
				}
				catch (ReaderException)
				{
				}
				if (array.Length == 0)
				{
					return null;
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] < ambiguousIndexValues[j].Length - 1)
					{
						array[j]++;
						break;
					}
					array[j] = 0;
					if (j == array.Length - 1)
					{
						return null;
					}
				}
			}
			return null;
		}

		private static BarcodeValue[][] createBarcodeMatrix(DetectionResult detectionResult)
		{
			BarcodeValue[][] array = new BarcodeValue[detectionResult.RowCount][];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new BarcodeValue[detectionResult.ColumnCount + 2];
				for (int j = 0; j < array[i].Length; j++)
				{
					array[i][j] = new BarcodeValue();
				}
			}
			int num = -1;
			DetectionResultColumn[] detectionResultColumns = detectionResult.getDetectionResultColumns();
			foreach (DetectionResultColumn detectionResultColumn in detectionResultColumns)
			{
				num++;
				if (detectionResultColumn == null)
				{
					continue;
				}
				Codeword[] codewords = detectionResultColumn.Codewords;
				foreach (Codeword codeword in codewords)
				{
					if (codeword != null && codeword.RowNumber != -1)
					{
						array[codeword.RowNumber][num].setValue(codeword.Value);
					}
				}
			}
			return array;
		}

		private static bool isValidBarcodeColumn(DetectionResult detectionResult, int barcodeColumn)
		{
			return barcodeColumn >= 0 && barcodeColumn <= detectionResult.DetectionResultColumns.Length + 1;
		}

		private static int getStartColumn(DetectionResult detectionResult, int barcodeColumn, int imageRow, bool leftToRight)
		{
			int num = (leftToRight ? 1 : (-1));
			Codeword codeword = null;
			if (isValidBarcodeColumn(detectionResult, barcodeColumn - num))
			{
				codeword = detectionResult.DetectionResultColumns[barcodeColumn - num].getCodeword(imageRow);
			}
			if (codeword != null)
			{
				return (!leftToRight) ? codeword.StartX : codeword.EndX;
			}
			codeword = detectionResult.DetectionResultColumns[barcodeColumn].getCodewordNearby(imageRow);
			if (codeword != null)
			{
				return (!leftToRight) ? codeword.EndX : codeword.StartX;
			}
			if (isValidBarcodeColumn(detectionResult, barcodeColumn - num))
			{
				codeword = detectionResult.DetectionResultColumns[barcodeColumn - num].getCodewordNearby(imageRow);
			}
			if (codeword != null)
			{
				return (!leftToRight) ? codeword.StartX : codeword.EndX;
			}
			int num2 = 0;
			while (isValidBarcodeColumn(detectionResult, barcodeColumn - num))
			{
				barcodeColumn -= num;
				Codeword[] codewords = detectionResult.DetectionResultColumns[barcodeColumn].Codewords;
				foreach (Codeword codeword2 in codewords)
				{
					if (codeword2 != null)
					{
						return ((!leftToRight) ? codeword2.StartX : codeword2.EndX) + num * num2 * (codeword2.EndX - codeword2.StartX);
					}
				}
				num2++;
			}
			return (!leftToRight) ? detectionResult.Box.MaxX : detectionResult.Box.MinX;
		}

		private static Codeword detectCodeword(BitMatrix image, int minColumn, int maxColumn, bool leftToRight, int startColumn, int imageRow, int minCodewordWidth, int maxCodewordWidth)
		{
			startColumn = adjustCodewordStartColumn(image, minColumn, maxColumn, leftToRight, startColumn, imageRow);
			int[] moduleBitCount = getModuleBitCount(image, minColumn, maxColumn, leftToRight, startColumn, imageRow);
			if (moduleBitCount == null)
			{
				return null;
			}
			int bitCountSum = PDF417Common.getBitCountSum(moduleBitCount);
			int num;
			if (leftToRight)
			{
				num = startColumn + bitCountSum;
			}
			else
			{
				for (int i = 0; i < moduleBitCount.Length >> 1; i++)
				{
					int num2 = moduleBitCount[i];
					moduleBitCount[i] = moduleBitCount[moduleBitCount.Length - 1 - i];
					moduleBitCount[moduleBitCount.Length - 1 - i] = num2;
				}
				num = startColumn;
				startColumn = num - bitCountSum;
			}
			if (!checkCodewordSkew(bitCountSum, minCodewordWidth, maxCodewordWidth))
			{
				return null;
			}
			int decodedValue = PDF417CodewordDecoder.getDecodedValue(moduleBitCount);
			int codeword = PDF417Common.getCodeword(decodedValue);
			if (codeword == -1)
			{
				return null;
			}
			return new Codeword(startColumn, num, getCodewordBucketNumber(decodedValue), codeword);
		}

		private static int[] getModuleBitCount(BitMatrix image, int minColumn, int maxColumn, bool leftToRight, int startColumn, int imageRow)
		{
			int num = startColumn;
			int[] array = new int[8];
			int num2 = 0;
			int num3 = (leftToRight ? 1 : (-1));
			bool flag = leftToRight;
			while (((leftToRight && num < maxColumn) || (!leftToRight && num >= minColumn)) && num2 < array.Length)
			{
				if (image[num, imageRow] == flag)
				{
					array[num2]++;
					num += num3;
				}
				else
				{
					num2++;
					flag = !flag;
				}
			}
			if (num2 == array.Length || (((leftToRight && num == maxColumn) || (!leftToRight && num == minColumn)) && num2 == array.Length - 1))
			{
				return array;
			}
			return null;
		}

		private static int getNumberOfECCodeWords(int barcodeECLevel)
		{
			return 2 << barcodeECLevel;
		}

		private static int adjustCodewordStartColumn(BitMatrix image, int minColumn, int maxColumn, bool leftToRight, int codewordStartColumn, int imageRow)
		{
			int i = codewordStartColumn;
			int num = ((!leftToRight) ? 1 : (-1));
			for (int j = 0; j < 2; j++)
			{
				for (; ((leftToRight && i >= minColumn) || (!leftToRight && i < maxColumn)) && leftToRight == image[i, imageRow]; i += num)
				{
					if (Math.Abs(codewordStartColumn - i) > 2)
					{
						return codewordStartColumn;
					}
				}
				num = -num;
				leftToRight = !leftToRight;
			}
			return i;
		}

		private static bool checkCodewordSkew(int codewordSize, int minCodewordWidth, int maxCodewordWidth)
		{
			return minCodewordWidth - 2 <= codewordSize && codewordSize <= maxCodewordWidth + 2;
		}

		private static DecoderResult decodeCodewords(int[] codewords, int ecLevel, int[] erasures)
		{
			if (codewords.Length == 0)
			{
				return null;
			}
			int numECCodewords = 1 << ecLevel + 1;
			int num = correctErrors(codewords, erasures, numECCodewords);
			if (num < 0)
			{
				return null;
			}
			if (!verifyCodewordCount(codewords, numECCodewords))
			{
				return null;
			}
			DecoderResult decoderResult = DecodedBitStreamParser.decode(codewords, ecLevel.ToString());
			if (decoderResult != null)
			{
				decoderResult.ErrorsCorrected = num;
				decoderResult.Erasures = erasures.Length;
			}
			return decoderResult;
		}

		private static int correctErrors(int[] codewords, int[] erasures, int numECCodewords)
		{
			if ((erasures != null && erasures.Length > numECCodewords / 2 + 3) || numECCodewords < 0 || numECCodewords > 512)
			{
				return -1;
			}
			int errorLocationsCount;
			if (!errorCorrection.decode(codewords, numECCodewords, erasures, out errorLocationsCount))
			{
				return -1;
			}
			return errorLocationsCount;
		}

		private static bool verifyCodewordCount(int[] codewords, int numECCodewords)
		{
			if (codewords.Length < 4)
			{
				return false;
			}
			int num = codewords[0];
			if (num > codewords.Length)
			{
				return false;
			}
			if (num == 0)
			{
				if (numECCodewords >= codewords.Length)
				{
					return false;
				}
				codewords[0] = codewords.Length - numECCodewords;
			}
			return true;
		}

		private static int[] getBitCountForCodeword(int codeword)
		{
			int[] array = new int[8];
			int num = 0;
			int num2 = array.Length - 1;
			while (true)
			{
				if ((codeword & 1) != num)
				{
					num = codeword & 1;
					num2--;
					if (num2 < 0)
					{
						break;
					}
				}
				array[num2]++;
				codeword >>= 1;
			}
			return array;
		}

		private static int getCodewordBucketNumber(int codeword)
		{
			return getCodewordBucketNumber(getBitCountForCodeword(codeword));
		}

		private static int getCodewordBucketNumber(int[] moduleBitCount)
		{
			return (moduleBitCount[0] - moduleBitCount[2] + moduleBitCount[4] - moduleBitCount[6] + 9) % 9;
		}

		public static string ToString(BarcodeValue[][] barcodeMatrix)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < barcodeMatrix.Length; i++)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Row {0,2}: ", i);
				for (int j = 0; j < barcodeMatrix[i].Length; j++)
				{
					BarcodeValue barcodeValue = barcodeMatrix[i][j];
					int[] value = barcodeValue.getValue();
					if (value.Length == 0)
					{
						stringBuilder.Append("        ");
						continue;
					}
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0,4}({1,2})", value[0], barcodeValue.getConfidence(value[0]));
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}
	}
}
