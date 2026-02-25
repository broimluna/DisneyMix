using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class MultiFormatUPCEANReader : OneDReader
	{
		private readonly UPCEANReader[] readers;

		public MultiFormatUPCEANReader(IDictionary<DecodeHintType, object> hints)
		{
			object obj;
			if (hints == null || !hints.ContainsKey(DecodeHintType.POSSIBLE_FORMATS))
			{
				IList<BarcodeFormat> list = null;
				obj = list;
			}
			else
			{
				obj = (IList<BarcodeFormat>)hints[DecodeHintType.POSSIBLE_FORMATS];
			}
			IList<BarcodeFormat> list2 = (IList<BarcodeFormat>)obj;
			List<UPCEANReader> list3 = new List<UPCEANReader>();
			if (list2 != null)
			{
				if (list2.Contains(BarcodeFormat.EAN_13) || list2.Contains(BarcodeFormat.All_1D))
				{
					list3.Add(new EAN13Reader());
				}
				else if (list2.Contains(BarcodeFormat.UPC_A) || list2.Contains(BarcodeFormat.All_1D))
				{
					list3.Add(new UPCAReader());
				}
				if (list2.Contains(BarcodeFormat.EAN_8) || list2.Contains(BarcodeFormat.All_1D))
				{
					list3.Add(new EAN8Reader());
				}
				if (list2.Contains(BarcodeFormat.UPC_E) || list2.Contains(BarcodeFormat.All_1D))
				{
					list3.Add(new UPCEReader());
				}
			}
			if (list3.Count == 0)
			{
				list3.Add(new EAN13Reader());
				list3.Add(new EAN8Reader());
				list3.Add(new UPCEReader());
			}
			readers = list3.ToArray();
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			int[] array = UPCEANReader.findStartGuardPattern(row);
			if (array == null)
			{
				return null;
			}
			UPCEANReader[] array2 = readers;
			foreach (UPCEANReader uPCEANReader in array2)
			{
				Result result = uPCEANReader.decodeRow(rowNumber, row, array, hints);
				if (result != null)
				{
					bool flag = result.BarcodeFormat == BarcodeFormat.EAN_13 && result.Text[0] == '0';
					object obj;
					if (hints == null || !hints.ContainsKey(DecodeHintType.POSSIBLE_FORMATS))
					{
						IList<BarcodeFormat> list = null;
						obj = list;
					}
					else
					{
						obj = (IList<BarcodeFormat>)hints[DecodeHintType.POSSIBLE_FORMATS];
					}
					IList<BarcodeFormat> list2 = (IList<BarcodeFormat>)obj;
					bool flag2 = list2 == null || list2.Contains(BarcodeFormat.UPC_A) || list2.Contains(BarcodeFormat.All_1D);
					if (flag && flag2)
					{
						Result result2 = new Result(result.Text.Substring(1), result.RawBytes, result.ResultPoints, BarcodeFormat.UPC_A);
						result2.putAllMetadata(result.ResultMetadata);
						return result2;
					}
					return result;
				}
			}
			return null;
		}

		public override void reset()
		{
			UPCEANReader[] array = readers;
			foreach (Reader reader in array)
			{
				reader.reset();
			}
		}
	}
}
