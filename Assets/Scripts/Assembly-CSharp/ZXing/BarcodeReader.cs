using System;
using UnityEngine;

namespace ZXing
{
	public class BarcodeReader : BarcodeReaderGeneric<Color32[]>, IBarcodeReader, IMultipleBarcodeReader
	{
		private static readonly Func<Color32[], int, int, LuminanceSource> defaultCreateLuminanceSource = (Color32[] rawColor32, int width, int height) => new Color32LuminanceSource(rawColor32, width, height);

		public BarcodeReader()
			: this(new MultiFormatReader(), defaultCreateLuminanceSource, null)
		{
		}

		public BarcodeReader(Reader reader, Func<Color32[], int, int, LuminanceSource> createLuminanceSource, Func<LuminanceSource, Binarizer> createBinarizer)
			: base(reader, createLuminanceSource ?? defaultCreateLuminanceSource, createBinarizer)
		{
		}

		public BarcodeReader(Reader reader, Func<Color32[], int, int, LuminanceSource> createLuminanceSource, Func<LuminanceSource, Binarizer> createBinarizer, Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> createRGBLuminanceSource)
			: base(reader, createLuminanceSource ?? defaultCreateLuminanceSource, createBinarizer, createRGBLuminanceSource)
		{
		}
	}
}
