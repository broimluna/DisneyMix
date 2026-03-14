using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.Datamatrix.Encoder;
using ZXing.QrCode.Internal;

namespace ZXing.Datamatrix
{
	public sealed class DataMatrixWriter : Writer
	{
		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height)
		{
			return encode(contents, format, width, height, null);
		}

		public BitMatrix encode(string contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (string.IsNullOrEmpty(contents))
			{
				throw new ArgumentException("Found empty contents", contents);
			}
			if (format != BarcodeFormat.DATA_MATRIX)
			{
				throw new ArgumentException("Can only encode DATA_MATRIX, but got " + format);
			}
			if (width < 0 || height < 0)
			{
				throw new ArgumentException("Requested dimensions are too small: " + width + 'x' + height);
			}
			SymbolShapeHint shape = SymbolShapeHint.FORCE_NONE;
			int defaultEncodation = 0;
			Dimension minSize = null;
			Dimension maxSize = null;
			if (hints != null)
			{
				SymbolShapeHint? symbolShapeHint = ((!hints.ContainsKey(EncodeHintType.DATA_MATRIX_SHAPE)) ? ((SymbolShapeHint?)null) : ((SymbolShapeHint?)hints[EncodeHintType.DATA_MATRIX_SHAPE]));
				if (symbolShapeHint.HasValue)
				{
					shape = symbolShapeHint.Value;
				}
				Dimension dimension = ((!hints.ContainsKey(EncodeHintType.MIN_SIZE)) ? null : ((Dimension)hints[EncodeHintType.MIN_SIZE]));
				if (dimension != null)
				{
					minSize = dimension;
				}
				Dimension dimension2 = ((!hints.ContainsKey(EncodeHintType.MAX_SIZE)) ? null : ((Dimension)hints[EncodeHintType.MAX_SIZE]));
				if (dimension2 != null)
				{
					maxSize = dimension2;
				}
				int? num = ((!hints.ContainsKey(EncodeHintType.DATA_MATRIX_DEFAULT_ENCODATION)) ? ((int?)null) : ((int?)hints[EncodeHintType.DATA_MATRIX_DEFAULT_ENCODATION]));
				if (num.HasValue)
				{
					defaultEncodation = num.Value;
				}
			}
			string text = HighLevelEncoder.encodeHighLevel(contents, shape, minSize, maxSize, defaultEncodation);
			SymbolInfo symbolInfo = SymbolInfo.lookup(text.Length, shape, minSize, maxSize, true);
			string codewords = ErrorCorrection.encodeECC200(text, symbolInfo);
			DefaultPlacement defaultPlacement = new DefaultPlacement(codewords, symbolInfo.getSymbolDataWidth(), symbolInfo.getSymbolDataHeight());
			defaultPlacement.place();
			return encodeLowLevel(defaultPlacement, symbolInfo);
		}

		private static BitMatrix encodeLowLevel(DefaultPlacement placement, SymbolInfo symbolInfo)
		{
			int symbolDataWidth = symbolInfo.getSymbolDataWidth();
			int symbolDataHeight = symbolInfo.getSymbolDataHeight();
			ByteMatrix byteMatrix = new ByteMatrix(symbolInfo.getSymbolWidth(), symbolInfo.getSymbolHeight());
			int num = 0;
			for (int i = 0; i < symbolDataHeight; i++)
			{
				int num2;
				if (i % symbolInfo.matrixHeight == 0)
				{
					num2 = 0;
					for (int j = 0; j < symbolInfo.getSymbolWidth(); j++)
					{
						byteMatrix.set(num2, num, j % 2 == 0);
						num2++;
					}
					num++;
				}
				num2 = 0;
				for (int k = 0; k < symbolDataWidth; k++)
				{
					if (k % symbolInfo.matrixWidth == 0)
					{
						byteMatrix.set(num2, num, true);
						num2++;
					}
					byteMatrix.set(num2, num, placement.getBit(k, i));
					num2++;
					if (k % symbolInfo.matrixWidth == symbolInfo.matrixWidth - 1)
					{
						byteMatrix.set(num2, num, i % 2 == 0);
						num2++;
					}
				}
				num++;
				if (i % symbolInfo.matrixHeight == symbolInfo.matrixHeight - 1)
				{
					num2 = 0;
					for (int l = 0; l < symbolInfo.getSymbolWidth(); l++)
					{
						byteMatrix.set(num2, num, true);
						num2++;
					}
					num++;
				}
			}
			return convertByteMatrixToBitMatrix(byteMatrix);
		}

		private static BitMatrix convertByteMatrixToBitMatrix(ByteMatrix matrix)
		{
			int width = matrix.Width;
			int height = matrix.Height;
			BitMatrix bitMatrix = new BitMatrix(width, height);
			bitMatrix.clear();
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if (matrix[i, j] == 1)
					{
						bitMatrix[i, j] = true;
					}
				}
			}
			return bitMatrix;
		}
	}
}
