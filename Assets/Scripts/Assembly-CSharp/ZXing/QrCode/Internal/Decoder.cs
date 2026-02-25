using System.Collections.Generic;
using ZXing.Common;
using ZXing.Common.ReedSolomon;

namespace ZXing.QrCode.Internal
{
	public sealed class Decoder
	{
		private readonly ReedSolomonDecoder rsDecoder;

		public Decoder()
		{
			rsDecoder = new ReedSolomonDecoder(GenericGF.QR_CODE_FIELD_256);
		}

		public DecoderResult decode(bool[][] image, IDictionary<DecodeHintType, object> hints)
		{
			int num = image.Length;
			BitMatrix bitMatrix = new BitMatrix(num);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					bitMatrix[j, i] = image[i][j];
				}
			}
			return decode(bitMatrix, hints);
		}

		public DecoderResult decode(BitMatrix bits, IDictionary<DecodeHintType, object> hints)
		{
			BitMatrixParser bitMatrixParser = BitMatrixParser.createBitMatrixParser(bits);
			if (bitMatrixParser == null)
			{
				return null;
			}
			DecoderResult decoderResult = decode(bitMatrixParser, hints);
			if (decoderResult == null)
			{
				bitMatrixParser.remask();
				bitMatrixParser.setMirror(true);
				Version version = bitMatrixParser.readVersion();
				if (version == null)
				{
					return null;
				}
				FormatInformation formatInformation = bitMatrixParser.readFormatInformation();
				if (formatInformation == null)
				{
					return null;
				}
				bitMatrixParser.mirror();
				decoderResult = decode(bitMatrixParser, hints);
				if (decoderResult != null)
				{
					decoderResult.Other = new QRCodeDecoderMetaData(true);
				}
			}
			return decoderResult;
		}

		private DecoderResult decode(BitMatrixParser parser, IDictionary<DecodeHintType, object> hints)
		{
			Version version = parser.readVersion();
			if (version == null)
			{
				return null;
			}
			FormatInformation formatInformation = parser.readFormatInformation();
			if (formatInformation == null)
			{
				return null;
			}
			ErrorCorrectionLevel errorCorrectionLevel = formatInformation.ErrorCorrectionLevel;
			byte[] array = parser.readCodewords();
			if (array == null)
			{
				return null;
			}
			DataBlock[] dataBlocks = DataBlock.getDataBlocks(array, version, errorCorrectionLevel);
			int num = 0;
			DataBlock[] array2 = dataBlocks;
			foreach (DataBlock dataBlock in array2)
			{
				num += dataBlock.NumDataCodewords;
			}
			byte[] array3 = new byte[num];
			int num2 = 0;
			DataBlock[] array4 = dataBlocks;
			foreach (DataBlock dataBlock2 in array4)
			{
				byte[] codewords = dataBlock2.Codewords;
				int numDataCodewords = dataBlock2.NumDataCodewords;
				if (!correctErrors(codewords, numDataCodewords))
				{
					return null;
				}
				for (int k = 0; k < numDataCodewords; k++)
				{
					array3[num2++] = codewords[k];
				}
			}
			return DecodedBitStreamParser.decode(array3, version, errorCorrectionLevel, hints);
		}

		private bool correctErrors(byte[] codewordBytes, int numDataCodewords)
		{
			int num = codewordBytes.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = codewordBytes[i] & 0xFF;
			}
			int twoS = codewordBytes.Length - numDataCodewords;
			if (!rsDecoder.decode(array, twoS))
			{
				return false;
			}
			for (int j = 0; j < numDataCodewords; j++)
			{
				codewordBytes[j] = (byte)array[j];
			}
			return true;
		}
	}
}
