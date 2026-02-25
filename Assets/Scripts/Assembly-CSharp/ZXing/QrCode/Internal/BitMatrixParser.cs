using ZXing.Common;

namespace ZXing.QrCode.Internal
{
	internal sealed class BitMatrixParser
	{
		private readonly BitMatrix bitMatrix;

		private Version parsedVersion;

		private FormatInformation parsedFormatInfo;

		private bool mirrored;

		private BitMatrixParser(BitMatrix bitMatrix)
		{
			this.bitMatrix = bitMatrix;
		}

		internal static BitMatrixParser createBitMatrixParser(BitMatrix bitMatrix)
		{
			int height = bitMatrix.Height;
			if (height < 21 || (height & 3) != 1)
			{
				return null;
			}
			return new BitMatrixParser(bitMatrix);
		}

		internal FormatInformation readFormatInformation()
		{
			if (parsedFormatInfo != null)
			{
				return parsedFormatInfo;
			}
			int versionBits = 0;
			for (int i = 0; i < 6; i++)
			{
				versionBits = copyBit(i, 8, versionBits);
			}
			versionBits = copyBit(7, 8, versionBits);
			versionBits = copyBit(8, 8, versionBits);
			versionBits = copyBit(8, 7, versionBits);
			for (int num = 5; num >= 0; num--)
			{
				versionBits = copyBit(8, num, versionBits);
			}
			int height = bitMatrix.Height;
			int num2 = 0;
			int num3 = height - 7;
			for (int num4 = height - 1; num4 >= num3; num4--)
			{
				num2 = copyBit(8, num4, num2);
			}
			for (int j = height - 8; j < height; j++)
			{
				num2 = copyBit(j, 8, num2);
			}
			parsedFormatInfo = FormatInformation.decodeFormatInformation(versionBits, num2);
			if (parsedFormatInfo != null)
			{
				return parsedFormatInfo;
			}
			return null;
		}

		internal Version readVersion()
		{
			if (parsedVersion != null)
			{
				return parsedVersion;
			}
			int height = bitMatrix.Height;
			int num = height - 17 >> 2;
			if (num <= 6)
			{
				return Version.getVersionForNumber(num);
			}
			int versionBits = 0;
			int num2 = height - 11;
			for (int num3 = 5; num3 >= 0; num3--)
			{
				for (int num4 = height - 9; num4 >= num2; num4--)
				{
					versionBits = copyBit(num4, num3, versionBits);
				}
			}
			parsedVersion = Version.decodeVersionInformation(versionBits);
			if (parsedVersion != null && parsedVersion.DimensionForVersion == height)
			{
				return parsedVersion;
			}
			versionBits = 0;
			for (int num5 = 5; num5 >= 0; num5--)
			{
				for (int num6 = height - 9; num6 >= num2; num6--)
				{
					versionBits = copyBit(num5, num6, versionBits);
				}
			}
			parsedVersion = Version.decodeVersionInformation(versionBits);
			if (parsedVersion != null && parsedVersion.DimensionForVersion == height)
			{
				return parsedVersion;
			}
			return null;
		}

		private int copyBit(int i, int j, int versionBits)
		{
			return (!((!mirrored) ? bitMatrix[i, j] : bitMatrix[j, i])) ? (versionBits << 1) : ((versionBits << 1) | 1);
		}

		internal byte[] readCodewords()
		{
			FormatInformation formatInformation = readFormatInformation();
			if (formatInformation == null)
			{
				return null;
			}
			Version version = readVersion();
			if (version == null)
			{
				return null;
			}
			DataMask dataMask = DataMask.forReference(formatInformation.DataMask);
			int height = this.bitMatrix.Height;
			dataMask.unmaskBitMatrix(this.bitMatrix, height);
			BitMatrix bitMatrix = version.buildFunctionPattern();
			bool flag = true;
			byte[] array = new byte[version.TotalCodewords];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int num4 = height - 1; num4 > 0; num4 -= 2)
			{
				if (num4 == 6)
				{
					num4--;
				}
				for (int i = 0; i < height; i++)
				{
					int y = ((!flag) ? i : (height - 1 - i));
					for (int j = 0; j < 2; j++)
					{
						if (!bitMatrix[num4 - j, y])
						{
							num3++;
							num2 <<= 1;
							if (this.bitMatrix[num4 - j, y])
							{
								num2 |= 1;
							}
							if (num3 == 8)
							{
								array[num++] = (byte)num2;
								num3 = 0;
								num2 = 0;
							}
						}
					}
				}
				flag = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
			}
			if (num != version.TotalCodewords)
			{
				return null;
			}
			return array;
		}

		internal void remask()
		{
			if (parsedFormatInfo != null)
			{
				DataMask dataMask = DataMask.forReference(parsedFormatInfo.DataMask);
				int height = bitMatrix.Height;
				dataMask.unmaskBitMatrix(bitMatrix, height);
			}
		}

		internal void setMirror(bool mirror)
		{
			parsedVersion = null;
			parsedFormatInfo = null;
			mirrored = mirror;
		}

		internal void mirror()
		{
			for (int i = 0; i < bitMatrix.Width; i++)
			{
				for (int j = i + 1; j < bitMatrix.Height; j++)
				{
					if (bitMatrix[i, j] != bitMatrix[j, i])
					{
						bitMatrix.flip(j, i);
						bitMatrix.flip(i, j);
					}
				}
			}
		}
	}
}
