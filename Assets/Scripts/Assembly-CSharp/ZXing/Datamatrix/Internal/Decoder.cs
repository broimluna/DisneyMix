using ZXing.Common;
using ZXing.Common.ReedSolomon;

namespace ZXing.Datamatrix.Internal
{
	public sealed class Decoder
	{
		private readonly ReedSolomonDecoder rsDecoder;

		public Decoder()
		{
			rsDecoder = new ReedSolomonDecoder(GenericGF.DATA_MATRIX_FIELD_256);
		}

		public DecoderResult decode(bool[][] image)
		{
			int num = image.Length;
			BitMatrix bitMatrix = new BitMatrix(num);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if (image[i][j])
					{
						bitMatrix[j, i] = true;
					}
				}
			}
			return decode(bitMatrix);
		}

		public DecoderResult decode(BitMatrix bits)
		{
			BitMatrixParser bitMatrixParser = new BitMatrixParser(bits);
			if (bitMatrixParser.Version == null)
			{
				return null;
			}
			byte[] array = bitMatrixParser.readCodewords();
			if (array == null)
			{
				return null;
			}
			DataBlock[] dataBlocks = DataBlock.getDataBlocks(array, bitMatrixParser.Version);
			int num = dataBlocks.Length;
			int num2 = 0;
			DataBlock[] array2 = dataBlocks;
			foreach (DataBlock dataBlock in array2)
			{
				num2 += dataBlock.NumDataCodewords;
			}
			byte[] array3 = new byte[num2];
			for (int j = 0; j < num; j++)
			{
				DataBlock dataBlock2 = dataBlocks[j];
				byte[] codewords = dataBlock2.Codewords;
				int numDataCodewords = dataBlock2.NumDataCodewords;
				if (!correctErrors(codewords, numDataCodewords))
				{
					return null;
				}
				for (int k = 0; k < numDataCodewords; k++)
				{
					array3[k * num + j] = codewords[k];
				}
			}
			return DecodedBitStreamParser.decode(array3);
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
