using System.Collections.Generic;
using ZXing.Common;
using ZXing.OneD.RSS;
using ZXing.OneD.RSS.Expanded;

namespace ZXing.OneD
{
	public sealed class MultiFormatOneDReader : OneDReader
	{
		private readonly IList<OneDReader> readers;

		public MultiFormatOneDReader(IDictionary<DecodeHintType, object> hints)
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
			readers = new List<OneDReader>();
			if (list2 != null)
			{
				if (list2.Contains(BarcodeFormat.All_1D) || list2.Contains(BarcodeFormat.EAN_13) || list2.Contains(BarcodeFormat.UPC_A) || list2.Contains(BarcodeFormat.EAN_8) || list2.Contains(BarcodeFormat.UPC_E))
				{
					readers.Add(new MultiFormatUPCEANReader(hints));
				}
				if (list2.Contains(BarcodeFormat.MSI))
				{
					bool usingCheckDigit = hints.ContainsKey(DecodeHintType.ASSUME_MSI_CHECK_DIGIT) && (bool)hints[DecodeHintType.ASSUME_MSI_CHECK_DIGIT];
					readers.Add(new MSIReader(usingCheckDigit));
				}
				if (list2.Contains(BarcodeFormat.CODE_39) || list2.Contains(BarcodeFormat.All_1D))
				{
					bool usingCheckDigit2 = hints.ContainsKey(DecodeHintType.ASSUME_CODE_39_CHECK_DIGIT) && (bool)hints[DecodeHintType.ASSUME_CODE_39_CHECK_DIGIT];
					bool extendedMode = hints.ContainsKey(DecodeHintType.USE_CODE_39_EXTENDED_MODE) && (bool)hints[DecodeHintType.USE_CODE_39_EXTENDED_MODE];
					readers.Add(new Code39Reader(usingCheckDigit2, extendedMode));
				}
				if (list2.Contains(BarcodeFormat.CODE_93) || list2.Contains(BarcodeFormat.All_1D))
				{
					readers.Add(new Code93Reader());
				}
				if (list2.Contains(BarcodeFormat.CODE_128) || list2.Contains(BarcodeFormat.All_1D))
				{
					readers.Add(new Code128Reader());
				}
				if (list2.Contains(BarcodeFormat.ITF) || list2.Contains(BarcodeFormat.All_1D))
				{
					readers.Add(new ITFReader());
				}
				if (list2.Contains(BarcodeFormat.CODABAR) || list2.Contains(BarcodeFormat.All_1D))
				{
					readers.Add(new CodaBarReader());
				}
				if (list2.Contains(BarcodeFormat.RSS_14) || list2.Contains(BarcodeFormat.All_1D))
				{
					readers.Add(new RSS14Reader());
				}
				if (list2.Contains(BarcodeFormat.RSS_EXPANDED) || list2.Contains(BarcodeFormat.All_1D))
				{
					readers.Add(new RSSExpandedReader());
				}
			}
			if (readers.Count == 0)
			{
				bool usingCheckDigit3 = hints != null && hints.ContainsKey(DecodeHintType.ASSUME_CODE_39_CHECK_DIGIT) && (bool)hints[DecodeHintType.ASSUME_CODE_39_CHECK_DIGIT];
				bool extendedMode2 = hints != null && hints.ContainsKey(DecodeHintType.USE_CODE_39_EXTENDED_MODE) && (bool)hints[DecodeHintType.USE_CODE_39_EXTENDED_MODE];
				readers.Add(new MultiFormatUPCEANReader(hints));
				readers.Add(new Code39Reader(usingCheckDigit3, extendedMode2));
				readers.Add(new CodaBarReader());
				readers.Add(new Code93Reader());
				readers.Add(new Code128Reader());
				readers.Add(new ITFReader());
				readers.Add(new RSS14Reader());
				readers.Add(new RSSExpandedReader());
			}
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			foreach (OneDReader reader in readers)
			{
				Result result = reader.decodeRow(rowNumber, row, hints);
				if (result != null)
				{
					return result;
				}
			}
			return null;
		}

		public override void reset()
		{
			foreach (OneDReader reader in readers)
			{
				((Reader)reader).reset();
			}
		}
	}
}
