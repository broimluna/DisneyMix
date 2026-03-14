using System.Collections.Generic;

namespace ZXing.Multi
{
	public sealed class ByQuadrantReader : Reader
	{
		private readonly Reader @delegate;

		public ByQuadrantReader(Reader @delegate)
		{
			this.@delegate = @delegate;
		}

		public Result decode(BinaryBitmap image)
		{
			return decode(image, null);
		}

		public Result decode(BinaryBitmap image, IDictionary<DecodeHintType, object> hints)
		{
			int width = image.Width;
			int height = image.Height;
			int num = width / 2;
			int num2 = height / 2;
			BinaryBitmap image2 = image.crop(0, 0, num, num2);
			Result result = @delegate.decode(image2, hints);
			if (result != null)
			{
				return result;
			}
			BinaryBitmap image3 = image.crop(num, 0, num, num2);
			result = @delegate.decode(image3, hints);
			if (result != null)
			{
				return result;
			}
			BinaryBitmap image4 = image.crop(0, num2, num, num2);
			result = @delegate.decode(image4, hints);
			if (result != null)
			{
				return result;
			}
			BinaryBitmap image5 = image.crop(num, num2, num, num2);
			result = @delegate.decode(image5, hints);
			if (result != null)
			{
				return result;
			}
			int left = num / 2;
			int top = num2 / 2;
			BinaryBitmap image6 = image.crop(left, top, num, num2);
			return @delegate.decode(image6, hints);
		}

		public void reset()
		{
			@delegate.reset();
		}
	}
}
