using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Common;
using ZXing.Common.ReedSolomon;

namespace ZXing.QrCode.Internal
{
	public static class Encoder
	{
		private static readonly int[] ALPHANUMERIC_TABLE = new int[96]
		{
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, 36, -1, -1, -1, 37, 38, -1, -1,
			-1, -1, 39, 40, -1, 41, 42, 43, 0, 1,
			2, 3, 4, 5, 6, 7, 8, 9, 44, -1,
			-1, -1, -1, -1, -1, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
			25, 26, 27, 28, 29, 30, 31, 32, 33, 34,
			35, -1, -1, -1, -1, -1
		};

		internal static string DEFAULT_BYTE_MODE_ENCODING = "ISO-8859-1";

		private static int calculateMaskPenalty(ByteMatrix matrix)
		{
			return MaskUtil.applyMaskPenaltyRule1(matrix) + MaskUtil.applyMaskPenaltyRule2(matrix) + MaskUtil.applyMaskPenaltyRule3(matrix) + MaskUtil.applyMaskPenaltyRule4(matrix);
		}

		public static QRCode encode(string content, ErrorCorrectionLevel ecLevel)
		{
			return encode(content, ecLevel, null);
		}

		public static QRCode encode(string content, ErrorCorrectionLevel ecLevel, IDictionary<EncodeHintType, object> hints)
		{
			string text = ((hints != null && hints.ContainsKey(EncodeHintType.CHARACTER_SET)) ? ((string)hints[EncodeHintType.CHARACTER_SET]) : null);
			if (text == null)
			{
				text = DEFAULT_BYTE_MODE_ENCODING;
			}
			bool flag = !DEFAULT_BYTE_MODE_ENCODING.Equals(text);
			Mode mode = chooseMode(content, text);
			BitArray bitArray = new BitArray();
			if (mode == Mode.BYTE && flag)
			{
				CharacterSetECI characterSetECIByName = CharacterSetECI.getCharacterSetECIByName(text);
				if (characterSetECIByName != null && (hints == null || !hints.ContainsKey(EncodeHintType.DISABLE_ECI) || !(bool)hints[EncodeHintType.DISABLE_ECI]))
				{
					appendECI(characterSetECIByName, bitArray);
				}
			}
			appendModeInfo(mode, bitArray);
			BitArray bitArray2 = new BitArray();
			appendBytes(content, mode, bitArray2, text);
			int numInputBits = bitArray.Size + mode.getCharacterCountBits(Version.getVersionForNumber(1)) + bitArray2.Size;
			Version version = chooseVersion(numInputBits, ecLevel);
			int numInputBits2 = bitArray.Size + mode.getCharacterCountBits(version) + bitArray2.Size;
			Version version2 = chooseVersion(numInputBits2, ecLevel);
			BitArray bitArray3 = new BitArray();
			bitArray3.appendBitArray(bitArray);
			int numLetters = ((mode != Mode.BYTE) ? content.Length : bitArray2.SizeInBytes);
			appendLengthInfo(numLetters, version2, mode, bitArray3);
			bitArray3.appendBitArray(bitArray2);
			Version.ECBlocks eCBlocksForLevel = version2.getECBlocksForLevel(ecLevel);
			int numDataBytes = version2.TotalCodewords - eCBlocksForLevel.TotalECCodewords;
			terminateBits(numDataBytes, bitArray3);
			BitArray bitArray4 = interleaveWithECBytes(bitArray3, version2.TotalCodewords, numDataBytes, eCBlocksForLevel.NumBlocks);
			QRCode qRCode = new QRCode();
			qRCode.ECLevel = ecLevel;
			qRCode.Mode = mode;
			qRCode.Version = version2;
			QRCode qRCode2 = qRCode;
			int dimensionForVersion = version2.DimensionForVersion;
			ByteMatrix matrix = new ByteMatrix(dimensionForVersion, dimensionForVersion);
			int maskPattern = (qRCode2.MaskPattern = chooseMaskPattern(bitArray4, ecLevel, version2, matrix));
			MatrixUtil.buildMatrix(bitArray4, ecLevel, version2, maskPattern, matrix);
			qRCode2.Matrix = matrix;
			return qRCode2;
		}

		internal static int getAlphanumericCode(int code)
		{
			if (code < ALPHANUMERIC_TABLE.Length)
			{
				return ALPHANUMERIC_TABLE[code];
			}
			return -1;
		}

		public static Mode chooseMode(string content)
		{
			return chooseMode(content, null);
		}

		private static Mode chooseMode(string content, string encoding)
		{
			if ("Shift_JIS".Equals(encoding))
			{
				return (!isOnlyDoubleByteKanji(content)) ? Mode.BYTE : Mode.KANJI;
			}
			bool flag = false;
			bool flag2 = false;
			foreach (char c in content)
			{
				if (c >= '0' && c <= '9')
				{
					flag = true;
					continue;
				}
				if (getAlphanumericCode(c) != -1)
				{
					flag2 = true;
					continue;
				}
				return Mode.BYTE;
			}
			if (flag2)
			{
				return Mode.ALPHANUMERIC;
			}
			if (flag)
			{
				return Mode.NUMERIC;
			}
			return Mode.BYTE;
		}

		private static bool isOnlyDoubleByteKanji(string content)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(content);
			}
			catch (Exception)
			{
				return false;
			}
			int num = bytes.Length;
			if (num % 2 != 0)
			{
				return false;
			}
			for (int i = 0; i < num; i += 2)
			{
				int num2 = bytes[i] & 0xFF;
				if ((num2 < 129 || num2 > 159) && (num2 < 224 || num2 > 235))
				{
					return false;
				}
			}
			return true;
		}

		private static int chooseMaskPattern(BitArray bits, ErrorCorrectionLevel ecLevel, Version version, ByteMatrix matrix)
		{
			int num = int.MaxValue;
			int result = -1;
			for (int i = 0; i < QRCode.NUM_MASK_PATTERNS; i++)
			{
				MatrixUtil.buildMatrix(bits, ecLevel, version, i, matrix);
				int num2 = calculateMaskPenalty(matrix);
				if (num2 < num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		private static Version chooseVersion(int numInputBits, ErrorCorrectionLevel ecLevel)
		{
			for (int i = 1; i <= 40; i++)
			{
				Version versionForNumber = Version.getVersionForNumber(i);
				int totalCodewords = versionForNumber.TotalCodewords;
				Version.ECBlocks eCBlocksForLevel = versionForNumber.getECBlocksForLevel(ecLevel);
				int totalECCodewords = eCBlocksForLevel.TotalECCodewords;
				int num = totalCodewords - totalECCodewords;
				int num2 = (numInputBits + 7) / 8;
				if (num >= num2)
				{
					return versionForNumber;
				}
			}
			throw new WriterException("Data too big");
		}

		internal static void terminateBits(int numDataBytes, BitArray bits)
		{
			int num = numDataBytes << 3;
			if (bits.Size > num)
			{
				throw new WriterException("data bits cannot fit in the QR Code" + bits.Size + " > " + num);
			}
			for (int i = 0; i < 4; i++)
			{
				if (bits.Size >= num)
				{
					break;
				}
				bits.appendBit(false);
			}
			int num2 = bits.Size & 7;
			if (num2 > 0)
			{
				for (int j = num2; j < 8; j++)
				{
					bits.appendBit(false);
				}
			}
			int num3 = numDataBytes - bits.SizeInBytes;
			for (int k = 0; k < num3; k++)
			{
				bits.appendBits(((k & 1) != 0) ? 17 : 236, 8);
			}
			if (bits.Size != num)
			{
				throw new WriterException("Bits size does not equal capacity");
			}
		}

		internal static void getNumDataBytesAndNumECBytesForBlockID(int numTotalBytes, int numDataBytes, int numRSBlocks, int blockID, int[] numDataBytesInBlock, int[] numECBytesInBlock)
		{
			if (blockID >= numRSBlocks)
			{
				throw new WriterException("Block ID too large");
			}
			int num = numTotalBytes % numRSBlocks;
			int num2 = numRSBlocks - num;
			int num3 = numTotalBytes / numRSBlocks;
			int num4 = num3 + 1;
			int num5 = numDataBytes / numRSBlocks;
			int num6 = num5 + 1;
			int num7 = num3 - num5;
			int num8 = num4 - num6;
			if (num7 != num8)
			{
				throw new WriterException("EC bytes mismatch");
			}
			if (numRSBlocks != num2 + num)
			{
				throw new WriterException("RS blocks mismatch");
			}
			if (numTotalBytes != (num5 + num7) * num2 + (num6 + num8) * num)
			{
				throw new WriterException("Total bytes mismatch");
			}
			if (blockID < num2)
			{
				numDataBytesInBlock[0] = num5;
				numECBytesInBlock[0] = num7;
			}
			else
			{
				numDataBytesInBlock[0] = num6;
				numECBytesInBlock[0] = num8;
			}
		}

		internal static BitArray interleaveWithECBytes(BitArray bits, int numTotalBytes, int numDataBytes, int numRSBlocks)
		{
			if (bits.SizeInBytes != numDataBytes)
			{
				throw new WriterException("Number of bits and data bytes does not match");
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			List<BlockPair> list = new List<BlockPair>(numRSBlocks);
			for (int i = 0; i < numRSBlocks; i++)
			{
				int[] array = new int[1];
				int[] array2 = new int[1];
				getNumDataBytesAndNumECBytesForBlockID(numTotalBytes, numDataBytes, numRSBlocks, i, array, array2);
				int num4 = array[0];
				byte[] array3 = new byte[num4];
				bits.toBytes(8 * num, array3, 0, num4);
				byte[] array4 = generateECBytes(array3, array2[0]);
				list.Add(new BlockPair(array3, array4));
				num2 = Math.Max(num2, num4);
				num3 = Math.Max(num3, array4.Length);
				num += array[0];
			}
			if (numDataBytes != num)
			{
				throw new WriterException("Data bytes does not match offset");
			}
			BitArray bitArray = new BitArray();
			for (int j = 0; j < num2; j++)
			{
				foreach (BlockPair item in list)
				{
					byte[] dataBytes = item.DataBytes;
					if (j < dataBytes.Length)
					{
						bitArray.appendBits(dataBytes[j], 8);
					}
				}
			}
			for (int k = 0; k < num3; k++)
			{
				foreach (BlockPair item2 in list)
				{
					byte[] errorCorrectionBytes = item2.ErrorCorrectionBytes;
					if (k < errorCorrectionBytes.Length)
					{
						bitArray.appendBits(errorCorrectionBytes[k], 8);
					}
				}
			}
			if (numTotalBytes != bitArray.SizeInBytes)
			{
				throw new WriterException("Interleaving error: " + numTotalBytes + " and " + bitArray.SizeInBytes + " differ.");
			}
			return bitArray;
		}

		internal static byte[] generateECBytes(byte[] dataBytes, int numEcBytesInBlock)
		{
			int num = dataBytes.Length;
			int[] array = new int[num + numEcBytesInBlock];
			for (int i = 0; i < num; i++)
			{
				array[i] = dataBytes[i] & 0xFF;
			}
			new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256).encode(array, numEcBytesInBlock);
			byte[] array2 = new byte[numEcBytesInBlock];
			for (int j = 0; j < numEcBytesInBlock; j++)
			{
				array2[j] = (byte)array[num + j];
			}
			return array2;
		}

		internal static void appendModeInfo(Mode mode, BitArray bits)
		{
			bits.appendBits(mode.Bits, 4);
		}

		internal static void appendLengthInfo(int numLetters, Version version, Mode mode, BitArray bits)
		{
			int characterCountBits = mode.getCharacterCountBits(version);
			if (numLetters >= 1 << characterCountBits)
			{
				throw new WriterException(numLetters + " is bigger than " + ((1 << characterCountBits) - 1));
			}
			bits.appendBits(numLetters, characterCountBits);
		}

		internal static void appendBytes(string content, Mode mode, BitArray bits, string encoding)
		{
			if (mode.Equals(Mode.NUMERIC))
			{
				appendNumericBytes(content, bits);
				return;
			}
			if (mode.Equals(Mode.ALPHANUMERIC))
			{
				appendAlphanumericBytes(content, bits);
				return;
			}
			if (mode.Equals(Mode.BYTE))
			{
				append8BitBytes(content, bits, encoding);
				return;
			}
			if (mode.Equals(Mode.KANJI))
			{
				appendKanjiBytes(content, bits);
				return;
			}
			throw new WriterException("Invalid mode: " + mode);
		}

		internal static void appendNumericBytes(string content, BitArray bits)
		{
			int length = content.Length;
			int num = 0;
			while (num < length)
			{
				int num2 = content[num] - 48;
				if (num + 2 < length)
				{
					int num3 = content[num + 1] - 48;
					int num4 = content[num + 2] - 48;
					bits.appendBits(num2 * 100 + num3 * 10 + num4, 10);
					num += 3;
				}
				else if (num + 1 < length)
				{
					int num5 = content[num + 1] - 48;
					bits.appendBits(num2 * 10 + num5, 7);
					num += 2;
				}
				else
				{
					bits.appendBits(num2, 4);
					num++;
				}
			}
		}

		internal static void appendAlphanumericBytes(string content, BitArray bits)
		{
			int length = content.Length;
			int num = 0;
			while (num < length)
			{
				int alphanumericCode = getAlphanumericCode(content[num]);
				if (alphanumericCode == -1)
				{
					throw new WriterException();
				}
				if (num + 1 < length)
				{
					int alphanumericCode2 = getAlphanumericCode(content[num + 1]);
					if (alphanumericCode2 == -1)
					{
						throw new WriterException();
					}
					bits.appendBits(alphanumericCode * 45 + alphanumericCode2, 11);
					num += 2;
				}
				else
				{
					bits.appendBits(alphanumericCode, 6);
					num++;
				}
			}
		}

		internal static void append8BitBytes(string content, BitArray bits, string encoding)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding(encoding).GetBytes(content);
			}
			catch (Exception ex)
			{
				throw new WriterException(ex.Message, ex);
			}
			byte[] array = bytes;
			foreach (byte value in array)
			{
				bits.appendBits(value, 8);
			}
		}

		internal static void appendKanjiBytes(string content, BitArray bits)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(content);
			}
			catch (Exception ex)
			{
				throw new WriterException(ex.Message, ex);
			}
			int num = bytes.Length;
			for (int i = 0; i < num; i += 2)
			{
				int num2 = bytes[i] & 0xFF;
				int num3 = bytes[i + 1] & 0xFF;
				int num4 = (num2 << 8) | num3;
				int num5 = -1;
				if (num4 >= 33088 && num4 <= 40956)
				{
					num5 = num4 - 33088;
				}
				else if (num4 >= 57408 && num4 <= 60351)
				{
					num5 = num4 - 49472;
				}
				if (num5 == -1)
				{
					throw new WriterException("Invalid byte sequence");
				}
				int value = (num5 >> 8) * 192 + (num5 & 0xFF);
				bits.appendBits(value, 13);
			}
		}

		private static void appendECI(CharacterSetECI eci, BitArray bits)
		{
			bits.appendBits(Mode.ECI.Bits, 4);
			bits.appendBits(eci.Value, 8);
		}
	}
}
