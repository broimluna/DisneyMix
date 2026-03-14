using System.Collections.Generic;
using System.Text;
using ZXing.Common;

namespace ZXing.OneD
{
	public sealed class UPCAReader : UPCEANReader
	{
		private readonly UPCEANReader ean13Reader = new EAN13Reader();

		internal override BarcodeFormat BarcodeFormat
		{
			get
			{
				return BarcodeFormat.UPC_A;
			}
		}

		public override Result decodeRow(int rowNumber, BitArray row, int[] startGuardRange, IDictionary<DecodeHintType, object> hints)
		{
			return maybeReturnResult(ean13Reader.decodeRow(rowNumber, row, startGuardRange, hints));
		}

		public override Result decodeRow(int rowNumber, BitArray row, IDictionary<DecodeHintType, object> hints)
		{
			return maybeReturnResult(ean13Reader.decodeRow(rowNumber, row, hints));
		}

		public override Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			return maybeReturnResult(ean13Reader.decode(image, hints));
		}

		protected internal override int decodeMiddle(BitArray row, int[] startRange, StringBuilder resultString)
		{
			return ean13Reader.decodeMiddle(row, startRange, resultString);
		}

		private static Result maybeReturnResult(Result result)
		{
			if (result == null)
			{
				return null;
			}
			string text = result.Text;
			if (text[0] == '0')
			{
				return new Result(text.Substring(1), null, result.ResultPoints, BarcodeFormat.UPC_A);
			}
			return null;
		}
	}
}
