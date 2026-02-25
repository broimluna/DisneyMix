using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.Multi;
using ZXing.Multi.QrCode;

namespace ZXing
{
	public class BarcodeReaderGeneric<T> : IBarcodeReaderGeneric<T>, IMultipleBarcodeReaderGeneric<T>
	{
		private static readonly Func<LuminanceSource, Binarizer> defaultCreateBinarizer = (LuminanceSource luminanceSource) => new HybridBinarizer(luminanceSource);

		protected static readonly Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> defaultCreateRGBLuminanceSource = (byte[] rawBytes, int width, int height, RGBLuminanceSource.BitmapFormat format) => new RGBLuminanceSource(rawBytes, width, height, format);

		private Reader reader;

		private readonly Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> createRGBLuminanceSource;

		private readonly Func<T, int, int, LuminanceSource> createLuminanceSource;

		private readonly Func<LuminanceSource, Binarizer> createBinarizer;

		private bool usePreviousState;

		private DecodingOptions options;

		public DecodingOptions Options
		{
			get
			{
				return options ?? (options = new DecodingOptions());
			}
			set
			{
				options = value;
			}
		}

		protected Reader Reader
		{
			get
			{
				return reader ?? (reader = new MultiFormatReader());
			}
		}

		[Obsolete("Please use the Options.TryHarder property instead.")]
		public bool TryHarder
		{
			get
			{
				return Options.TryHarder;
			}
			set
			{
				Options.TryHarder = value;
			}
		}

		[Obsolete("Please use the Options.PureBarcode property instead.")]
		public bool PureBarcode
		{
			get
			{
				return Options.PureBarcode;
			}
			set
			{
				Options.PureBarcode = value;
			}
		}

		[Obsolete("Please use the Options.CharacterSet property instead.")]
		public string CharacterSet
		{
			get
			{
				return Options.CharacterSet;
			}
			set
			{
				Options.CharacterSet = value;
			}
		}

		[Obsolete("Please use the Options.PossibleFormats property instead.")]
		public IList<BarcodeFormat> PossibleFormats
		{
			get
			{
				return Options.PossibleFormats;
			}
			set
			{
				Options.PossibleFormats = value;
			}
		}

		public bool AutoRotate { get; set; }

		public bool TryInverted { get; set; }

		protected Func<T, int, int, LuminanceSource> CreateLuminanceSource
		{
			get
			{
				return createLuminanceSource;
			}
		}

		protected Func<LuminanceSource, Binarizer> CreateBinarizer
		{
			get
			{
				return createBinarizer ?? defaultCreateBinarizer;
			}
		}

		public event Action<ResultPoint> ResultPointFound
		{
			add
			{
				if (!Options.Hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK))
				{
					ResultPointCallback value2 = OnResultPointFound;
					Options.Hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK] = value2;
				}
				this.explicitResultPointFound = (Action<ResultPoint>)Delegate.Combine(this.explicitResultPointFound, value);
				usePreviousState = false;
			}
			remove
			{
				this.explicitResultPointFound = (Action<ResultPoint>)Delegate.Remove(this.explicitResultPointFound, value);
				if (this.explicitResultPointFound == null)
				{
					Options.Hints.Remove(DecodeHintType.NEED_RESULT_POINT_CALLBACK);
				}
				usePreviousState = false;
			}
		}

		private event Action<ResultPoint> explicitResultPointFound;

		public event Action<Result> ResultFound;

		public BarcodeReaderGeneric()
			: this((Reader)new MultiFormatReader(), (Func<T, int, int, LuminanceSource>)null, defaultCreateBinarizer)
		{
		}

		public BarcodeReaderGeneric(Reader reader, Func<T, int, int, LuminanceSource> createLuminanceSource, Func<LuminanceSource, Binarizer> createBinarizer)
			: this(reader, createLuminanceSource, createBinarizer, (Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource>)null)
		{
		}

		public BarcodeReaderGeneric(Reader reader, Func<T, int, int, LuminanceSource> createLuminanceSource, Func<LuminanceSource, Binarizer> createBinarizer, Func<byte[], int, int, RGBLuminanceSource.BitmapFormat, LuminanceSource> createRGBLuminanceSource)
		{
			this.reader = reader ?? new MultiFormatReader();
			this.createLuminanceSource = createLuminanceSource;
			this.createBinarizer = createBinarizer ?? defaultCreateBinarizer;
			this.createRGBLuminanceSource = createRGBLuminanceSource ?? defaultCreateRGBLuminanceSource;
			Options.ValueChanged += delegate
			{
				usePreviousState = false;
			};
			usePreviousState = false;
		}

		public Result Decode(T rawRGB, int width, int height)
		{
			if (CreateLuminanceSource == null)
			{
				throw new InvalidOperationException("You have to declare a luminance source delegate.");
			}
			if (rawRGB == null)
			{
				throw new ArgumentNullException("rawRGB");
			}
			LuminanceSource luminanceSource = CreateLuminanceSource(rawRGB, width, height);
			return Decode(luminanceSource);
		}

		public virtual Result Decode(LuminanceSource luminanceSource)
		{
			Result result = null;
			Binarizer binarizer = CreateBinarizer(luminanceSource);
			BinaryBitmap image = new BinaryBitmap(binarizer);
			MultiFormatReader multiFormatReader = Reader as MultiFormatReader;
			int i = 0;
			int num = 1;
			if (AutoRotate)
			{
				Options.Hints[DecodeHintType.TRY_HARDER_WITHOUT_ROTATION] = true;
				num = 4;
			}
			else if (Options.Hints.ContainsKey(DecodeHintType.TRY_HARDER_WITHOUT_ROTATION))
			{
				Options.Hints.Remove(DecodeHintType.TRY_HARDER_WITHOUT_ROTATION);
			}
			for (; i < num; i++)
			{
				if (usePreviousState && multiFormatReader != null)
				{
					result = multiFormatReader.decodeWithState(image);
				}
				else
				{
					result = Reader.decode(image, Options.Hints);
					usePreviousState = true;
				}
				if (result == null && TryInverted && luminanceSource.InversionSupported)
				{
					image = new BinaryBitmap(CreateBinarizer(luminanceSource.invert()));
					if (usePreviousState && multiFormatReader != null)
					{
						result = multiFormatReader.decodeWithState(image);
					}
					else
					{
						result = Reader.decode(image, Options.Hints);
						usePreviousState = true;
					}
				}
				if (result != null || !luminanceSource.RotateSupported || !AutoRotate)
				{
					break;
				}
				image = new BinaryBitmap(CreateBinarizer(luminanceSource.rotateCounterClockwise()));
			}
			if (result != null)
			{
				if (result.ResultMetadata == null)
				{
					result.putMetadata(ResultMetadataType.ORIENTATION, i * 90);
				}
				else if (!result.ResultMetadata.ContainsKey(ResultMetadataType.ORIENTATION))
				{
					result.ResultMetadata[ResultMetadataType.ORIENTATION] = i * 90;
				}
				else
				{
					result.ResultMetadata[ResultMetadataType.ORIENTATION] = ((int)result.ResultMetadata[ResultMetadataType.ORIENTATION] + i * 90) % 360;
				}
				OnResultFound(result);
			}
			return result;
		}

		public Result[] DecodeMultiple(T rawRGB, int width, int height)
		{
			if (CreateLuminanceSource == null)
			{
				throw new InvalidOperationException("You have to declare a luminance source delegate.");
			}
			if (rawRGB == null)
			{
				throw new ArgumentNullException("rawRGB");
			}
			LuminanceSource luminanceSource = CreateLuminanceSource(rawRGB, width, height);
			return DecodeMultiple(luminanceSource);
		}

		public virtual Result[] DecodeMultiple(LuminanceSource luminanceSource)
		{
			Result[] array = null;
			Binarizer binarizer = CreateBinarizer(luminanceSource);
			BinaryBitmap image = new BinaryBitmap(binarizer);
			int i = 0;
			int num = 1;
			MultipleBarcodeReader multipleBarcodeReader = null;
			if (AutoRotate)
			{
				Options.Hints[DecodeHintType.TRY_HARDER_WITHOUT_ROTATION] = true;
				num = 4;
			}
			IList<BarcodeFormat> possibleFormats = Options.PossibleFormats;
			multipleBarcodeReader = ((possibleFormats == null || possibleFormats.Count != 1 || !possibleFormats.Contains(BarcodeFormat.QR_CODE)) ? ((MultipleBarcodeReader)new GenericMultipleBarcodeReader(Reader)) : ((MultipleBarcodeReader)new QRCodeMultiReader()));
			for (; i < num; i++)
			{
				array = multipleBarcodeReader.decodeMultiple(image, Options.Hints);
				if (array == null && TryInverted && luminanceSource.InversionSupported)
				{
					image = new BinaryBitmap(CreateBinarizer(luminanceSource.invert()));
					array = multipleBarcodeReader.decodeMultiple(image, Options.Hints);
				}
				if (array != null || !luminanceSource.RotateSupported || !AutoRotate)
				{
					break;
				}
				image = new BinaryBitmap(CreateBinarizer(luminanceSource.rotateCounterClockwise()));
			}
			if (array != null)
			{
				Result[] array2 = array;
				foreach (Result result in array2)
				{
					if (result.ResultMetadata == null)
					{
						result.putMetadata(ResultMetadataType.ORIENTATION, i * 90);
					}
					else if (!result.ResultMetadata.ContainsKey(ResultMetadataType.ORIENTATION))
					{
						result.ResultMetadata[ResultMetadataType.ORIENTATION] = i * 90;
					}
					else
					{
						result.ResultMetadata[ResultMetadataType.ORIENTATION] = ((int)result.ResultMetadata[ResultMetadataType.ORIENTATION] + i * 90) % 360;
					}
				}
				OnResultsFound(array);
			}
			return array;
		}

		protected void OnResultsFound(IEnumerable<Result> results)
		{
			if (this.ResultFound == null)
			{
				return;
			}
			foreach (Result result in results)
			{
				this.ResultFound(result);
			}
		}

		protected void OnResultFound(Result result)
		{
			if (this.ResultFound != null)
			{
				this.ResultFound(result);
			}
		}

		protected void OnResultPointFound(ResultPoint resultPoint)
		{
			if (this.explicitResultPointFound != null)
			{
				this.explicitResultPointFound(resultPoint);
			}
		}

		public Result Decode(byte[] rawRGB, int width, int height, RGBLuminanceSource.BitmapFormat format)
		{
			if (rawRGB == null)
			{
				throw new ArgumentNullException("rawRGB");
			}
			LuminanceSource luminanceSource = createRGBLuminanceSource(rawRGB, width, height, format);
			return Decode(luminanceSource);
		}

		public Result[] DecodeMultiple(byte[] rawRGB, int width, int height, RGBLuminanceSource.BitmapFormat format)
		{
			if (rawRGB == null)
			{
				throw new ArgumentNullException("rawRGB");
			}
			LuminanceSource luminanceSource = createRGBLuminanceSource(rawRGB, width, height, format);
			return DecodeMultiple(luminanceSource);
		}
	}
}
