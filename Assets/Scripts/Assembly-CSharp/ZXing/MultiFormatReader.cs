using System.Collections.Generic;
using ZXing.Aztec;
using ZXing.Datamatrix;
using ZXing.Maxicode;
using ZXing.OneD;
using ZXing.PDF417;
using ZXing.QrCode;

namespace ZXing
{
	public sealed class MultiFormatReader : Reader
	{
		private IDictionary<DecodeHintType, object> hints;

		private IList<Reader> readers;

		public IDictionary<DecodeHintType, object> Hints
		{
			set
			{
				hints = value;
				bool flag = value != null && value.ContainsKey(DecodeHintType.TRY_HARDER);
				object obj;
				if (value == null || !value.ContainsKey(DecodeHintType.POSSIBLE_FORMATS))
				{
					IList<BarcodeFormat> list = null;
					obj = list;
				}
				else
				{
					obj = (IList<BarcodeFormat>)value[DecodeHintType.POSSIBLE_FORMATS];
				}
				IList<BarcodeFormat> list2 = (IList<BarcodeFormat>)obj;
				if (list2 != null)
				{
					bool flag2 = list2.Contains(BarcodeFormat.All_1D) || list2.Contains(BarcodeFormat.UPC_A) || list2.Contains(BarcodeFormat.UPC_E) || list2.Contains(BarcodeFormat.EAN_13) || list2.Contains(BarcodeFormat.EAN_8) || list2.Contains(BarcodeFormat.CODABAR) || list2.Contains(BarcodeFormat.CODE_39) || list2.Contains(BarcodeFormat.CODE_93) || list2.Contains(BarcodeFormat.CODE_128) || list2.Contains(BarcodeFormat.ITF) || list2.Contains(BarcodeFormat.RSS_14) || list2.Contains(BarcodeFormat.RSS_EXPANDED);
					readers = new List<Reader>();
					if (flag2 && !flag)
					{
						readers.Add(new MultiFormatOneDReader(value));
					}
					if (list2.Contains(BarcodeFormat.QR_CODE))
					{
						readers.Add(new QRCodeReader());
					}
					if (list2.Contains(BarcodeFormat.DATA_MATRIX))
					{
						readers.Add(new DataMatrixReader());
					}
					if (list2.Contains(BarcodeFormat.AZTEC))
					{
						readers.Add(new AztecReader());
					}
					if (list2.Contains(BarcodeFormat.PDF_417))
					{
						readers.Add(new PDF417Reader());
					}
					if (list2.Contains(BarcodeFormat.MAXICODE))
					{
						readers.Add(new MaxiCodeReader());
					}
					if (flag2 && flag)
					{
						readers.Add(new MultiFormatOneDReader(value));
					}
				}
				if (readers == null || readers.Count == 0)
				{
					readers = readers ?? new List<Reader>();
					if (!flag)
					{
						readers.Add(new MultiFormatOneDReader(value));
					}
					readers.Add(new QRCodeReader());
					readers.Add(new DataMatrixReader());
					readers.Add(new AztecReader());
					readers.Add(new PDF417Reader());
					readers.Add(new MaxiCodeReader());
					if (flag)
					{
						readers.Add(new MultiFormatOneDReader(value));
					}
				}
			}
		}

		public Result decode(BinaryBitmap image)
		{
			Hints = null;
			return decodeInternal(image);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			Hints = hints;
			return decodeInternal(image);
		}

		public Result decodeWithState(BinaryBitmap image)
		{
			if (readers == null)
			{
				Hints = null;
			}
			return decodeInternal(image);
		}

		public void reset()
		{
			if (readers == null)
			{
				return;
			}
			foreach (Reader reader in readers)
			{
				reader.reset();
			}
		}

		private Result decodeInternal(BinaryBitmap image)
		{
			if (readers != null)
			{
				ResultPointCallback resultPointCallback = ((hints == null || !hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? null : ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]));
				for (int i = 0; i < readers.Count; i++)
				{
					Reader reader = readers[i];
					reader.reset();
					Result result = reader.decode(image, hints);
					if (result != null)
					{
						readers.RemoveAt(i);
						readers.Insert(0, reader);
						return result;
					}
					if (resultPointCallback != null)
					{
						resultPointCallback(null);
					}
				}
			}
			return null;
		}
	}
}
