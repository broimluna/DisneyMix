using System;
using ZXing.Common;
using ZXing.PDF417.Internal;

namespace ZXing.PDF417
{
	[Serializable]
	public class PDF417EncodingOptions : EncodingOptions
	{
		public bool Compact
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.PDF417_COMPACT))
				{
					return (bool)base.Hints[EncodeHintType.PDF417_COMPACT];
				}
				return false;
			}
			set
			{
				base.Hints[EncodeHintType.PDF417_COMPACT] = value;
			}
		}

		public Compaction Compaction
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.PDF417_COMPACTION))
				{
					return (Compaction)(int)base.Hints[EncodeHintType.PDF417_COMPACTION];
				}
				return Compaction.AUTO;
			}
			set
			{
				base.Hints[EncodeHintType.PDF417_COMPACTION] = value;
			}
		}

		public Dimensions Dimensions
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.PDF417_DIMENSIONS))
				{
					return (Dimensions)base.Hints[EncodeHintType.PDF417_DIMENSIONS];
				}
				return null;
			}
			set
			{
				base.Hints[EncodeHintType.PDF417_DIMENSIONS] = value;
			}
		}

		public PDF417ErrorCorrectionLevel ErrorCorrection
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.ERROR_CORRECTION))
				{
					object obj = base.Hints[EncodeHintType.ERROR_CORRECTION];
					if (obj is PDF417ErrorCorrectionLevel)
					{
						return (PDF417ErrorCorrectionLevel)(int)obj;
					}
					if (obj is int)
					{
						return (PDF417ErrorCorrectionLevel)(int)Enum.Parse(typeof(PDF417ErrorCorrectionLevel), obj.ToString(), true);
					}
				}
				return PDF417ErrorCorrectionLevel.L2;
			}
			set
			{
				base.Hints[EncodeHintType.ERROR_CORRECTION] = value;
			}
		}

		public string CharacterSet
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.CHARACTER_SET))
				{
					return (string)base.Hints[EncodeHintType.CHARACTER_SET];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					if (base.Hints.ContainsKey(EncodeHintType.CHARACTER_SET))
					{
						base.Hints.Remove(EncodeHintType.CHARACTER_SET);
					}
				}
				else
				{
					base.Hints[EncodeHintType.CHARACTER_SET] = value;
				}
			}
		}

		public bool DisableECI
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.DISABLE_ECI))
				{
					return (bool)base.Hints[EncodeHintType.DISABLE_ECI];
				}
				return false;
			}
			set
			{
				base.Hints[EncodeHintType.DISABLE_ECI] = value;
			}
		}
	}
}
