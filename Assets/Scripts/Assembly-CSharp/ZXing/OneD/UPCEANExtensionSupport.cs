using ZXing.Common;

namespace ZXing.OneD
{
	internal sealed class UPCEANExtensionSupport
	{
		private static readonly int[] EXTENSION_START_PATTERN = new int[3] { 1, 1, 2 };

		private readonly UPCEANExtension2Support twoSupport = new UPCEANExtension2Support();

		private readonly UPCEANExtension5Support fiveSupport = new UPCEANExtension5Support();

		internal Result decodeRow(int rowNumber, BitArray row, int rowOffset)
		{
			int[] array = UPCEANReader.findGuardPattern(row, rowOffset, false, EXTENSION_START_PATTERN);
			if (array == null)
			{
				return null;
			}
			Result result = fiveSupport.decodeRow(rowNumber, row, array);
			if (result == null)
			{
				result = twoSupport.decodeRow(rowNumber, row, array);
			}
			return result;
		}
	}
}
