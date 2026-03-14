using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.PDF417.Internal;

namespace ZXing.PDF417
{
	public sealed class PDF417Writer : Writer
	{
		private const int WHITE_SPACE = 30;

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (format != BarcodeFormat.PDF_417)
			{
				throw new ArgumentException("Can only encode PDF_417, but got " + format);
			}
			ZXing.PDF417.Internal.PDF417 pDF = new ZXing.PDF417.Internal.PDF417();
			int margin = 30;
			int errorCorrectionLevel = 2;
			if (hints != null)
			{
				if (hints.ContainsKey(EncodeHintType.PDF417_COMPACT))
				{
					pDF.setCompact((bool)hints[EncodeHintType.PDF417_COMPACT]);
				}
				if (hints.ContainsKey(EncodeHintType.PDF417_COMPACTION))
				{
					pDF.setCompaction((Compaction)(int)hints[EncodeHintType.PDF417_COMPACTION]);
				}
				if (hints.ContainsKey(EncodeHintType.PDF417_DIMENSIONS))
				{
					Dimensions dimensions = (Dimensions)hints[EncodeHintType.PDF417_DIMENSIONS];
					pDF.setDimensions(dimensions.MaxCols, dimensions.MinCols, dimensions.MaxRows, dimensions.MinRows);
				}
				if (hints.ContainsKey(EncodeHintType.MARGIN))
				{
					margin = (int)hints[EncodeHintType.MARGIN];
				}
				if (hints.ContainsKey(EncodeHintType.ERROR_CORRECTION))
				{
					object obj = hints[EncodeHintType.ERROR_CORRECTION];
					if (obj is PDF417ErrorCorrectionLevel || obj is int)
					{
						errorCorrectionLevel = (int)obj;
					}
				}
				if (hints.ContainsKey(EncodeHintType.CHARACTER_SET))
				{
					string text = (string)hints[EncodeHintType.CHARACTER_SET];
					if (text != null)
					{
						pDF.setEncoding(text);
					}
				}
				if (hints.ContainsKey(EncodeHintType.DISABLE_ECI))
				{
					pDF.setDisableEci((bool)hints[EncodeHintType.DISABLE_ECI]);
				}
			}
			return bitMatrixFromEncoder(pDF, contents, width, height, margin, errorCorrectionLevel);
		}

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height)
		{
			return encode(contents, format, width, height, null);
		}

		private static BitMatrix bitMatrixFromEncoder(ZXing.PDF417.Internal.PDF417 encoder, string contents, int width, int height, int margin, int errorCorrectionLevel)
		{
			encoder.generateBarcodeLogic(contents, errorCorrectionLevel);
			sbyte[][] array = encoder.BarcodeMatrix.getScaledMatrix(2, 8);
			bool flag = false;
			if ((height > width) ^ (array[0].Length < array.Length))
			{
				array = rotateArray(array);
				flag = true;
			}
			int num = width / array[0].Length;
			int num2 = height / array.Length;
			int num3 = ((num >= num2) ? num2 : num);
			if (num3 > 1)
			{
				sbyte[][] array2 = encoder.BarcodeMatrix.getScaledMatrix(num3 * 2, num3 * 4 * 2);
				if (flag)
				{
					array2 = rotateArray(array2);
				}
				return bitMatrixFrombitArray(array2, margin);
			}
			return bitMatrixFrombitArray(array, margin);
		}

		private static BitMatrix bitMatrixFrombitArray(sbyte[][] input, int margin)
		{
			BitMatrix bitMatrix = new BitMatrix(input[0].Length + 2 * margin, input.Length + 2 * margin);
			int num = bitMatrix.Height - margin - 1;
			foreach (sbyte[] array in input)
			{
				int num2 = array.Length;
				for (int j = 0; j < num2; j++)
				{
					if (array[j] == 1)
					{
						bitMatrix[j + margin, num] = true;
					}
				}
				num--;
			}
			return bitMatrix;
		}

		private static sbyte[][] rotateArray(sbyte[][] bitarray)
		{
			sbyte[][] array = new sbyte[bitarray[0].Length][];
			for (int i = 0; i < bitarray[0].Length; i++)
			{
				array[i] = new sbyte[bitarray.Length];
			}
			for (int j = 0; j < bitarray.Length; j++)
			{
				int num = bitarray.Length - j - 1;
				for (int k = 0; k < bitarray[0].Length; k++)
				{
					array[k][num] = bitarray[j][k];
				}
			}
			return array;
		}
	}
}
