using System;
using System.Collections.Generic;
using UnityEngine;
using ZXing.Common;

namespace ZXing
{
	public interface IBarcodeReader
	{
		[Obsolete("Please use the Options.TryHarder property instead.")]
		bool TryHarder { get; set; }

		[Obsolete("Please use the Options.PureBarcode property instead.")]
		bool PureBarcode { get; set; }

		[Obsolete("Please use the Options.CharacterSet property instead.")]
		string CharacterSet { get; set; }

		[Obsolete("Please use the Options.PossibleFormats property instead.")]
		IList<BarcodeFormat> PossibleFormats { get; set; }

		DecodingOptions Options { get; set; }

		event Action<ResultPoint> ResultPointFound;

		event Action<Result> ResultFound;

		Result Decode(byte[] rawRGB, int width, int height, RGBLuminanceSource.BitmapFormat format);

		Result Decode(LuminanceSource luminanceSource);

		Result Decode(Color32[] rawColor32, int width, int height);
	}
}
