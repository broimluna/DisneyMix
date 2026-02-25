using System;
using ZXing.Common;
using ZXing.Rendering;

namespace ZXing
{
	public class BarcodeWriterGeneric<TOutput> : IBarcodeWriterGeneric<TOutput>
	{
		private EncodingOptions options;

		public BarcodeFormat Format { get; set; }

		public EncodingOptions Options
		{
			get
			{
				return options ?? (options = new EncodingOptions
				{
					Height = 100,
					Width = 100
				});
			}
			set
			{
				options = value;
			}
		}

		public Writer Encoder { get; set; }

		public IBarcodeRenderer<TOutput> Renderer { get; set; }

		public BitMatrix Encode(string contents)
		{
			Writer writer = Encoder ?? new MultiFormatWriter();
			EncodingOptions encodingOptions = Options;
			return writer.encode(contents, Format, encodingOptions.Width, encodingOptions.Height, encodingOptions.Hints);
		}

		public TOutput Write(string contents)
		{
			if (Renderer == null)
			{
				throw new InvalidOperationException("You have to set a renderer instance.");
			}
			BitMatrix matrix = Encode(contents);
			return Renderer.Render(matrix, Format, contents, Options);
		}

		public TOutput Write(BitMatrix matrix)
		{
			if (Renderer == null)
			{
				throw new InvalidOperationException("You have to set a renderer instance.");
			}
			return Renderer.Render(matrix, Format, null, Options);
		}
	}
}
