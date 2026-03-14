using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ZXing.Common
{
	[Serializable]
	public class EncodingOptions
	{
		[Browsable(false)]
		public IDictionary<EncodeHintType, object> Hints { get; private set; }

		public int Height
		{
			get
			{
				if (Hints.ContainsKey(EncodeHintType.HEIGHT))
				{
					return (int)Hints[EncodeHintType.HEIGHT];
				}
				return 0;
			}
			set
			{
				Hints[EncodeHintType.HEIGHT] = value;
			}
		}

		public int Width
		{
			get
			{
				if (Hints.ContainsKey(EncodeHintType.WIDTH))
				{
					return (int)Hints[EncodeHintType.WIDTH];
				}
				return 0;
			}
			set
			{
				Hints[EncodeHintType.WIDTH] = value;
			}
		}

		public bool PureBarcode
		{
			get
			{
				if (Hints.ContainsKey(EncodeHintType.PURE_BARCODE))
				{
					return (bool)Hints[EncodeHintType.PURE_BARCODE];
				}
				return false;
			}
			set
			{
				Hints[EncodeHintType.PURE_BARCODE] = value;
			}
		}

		public int Margin
		{
			get
			{
				if (Hints.ContainsKey(EncodeHintType.MARGIN))
				{
					return (int)Hints[EncodeHintType.MARGIN];
				}
				return 0;
			}
			set
			{
				Hints[EncodeHintType.MARGIN] = value;
			}
		}

		public EncodingOptions()
		{
			Hints = new Dictionary<EncodeHintType, object>();
		}
	}
}
