using System;
using ZXing.Common;
using ZXing.QrCode.Internal;

namespace ZXing.QrCode
{
	[Serializable]
	public class QrCodeEncodingOptions : EncodingOptions
	{
		public ErrorCorrectionLevel ErrorCorrection
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.ERROR_CORRECTION))
				{
					return (ErrorCorrectionLevel)base.Hints[EncodeHintType.ERROR_CORRECTION];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					if (base.Hints.ContainsKey(EncodeHintType.ERROR_CORRECTION))
					{
						base.Hints.Remove(EncodeHintType.ERROR_CORRECTION);
					}
				}
				else
				{
					base.Hints[EncodeHintType.ERROR_CORRECTION] = value;
				}
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
