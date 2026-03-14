using System;
using ZXing.Common;

namespace ZXing.Aztec
{
	[Serializable]
	public class AztecEncodingOptions : EncodingOptions
	{
		public int? ErrorCorrection
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.ERROR_CORRECTION))
				{
					return (int)base.Hints[EncodeHintType.ERROR_CORRECTION];
				}
				return null;
			}
			set
			{
				if (!value.HasValue)
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

		public int? Layers
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.AZTEC_LAYERS))
				{
					return (int)base.Hints[EncodeHintType.AZTEC_LAYERS];
				}
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					if (base.Hints.ContainsKey(EncodeHintType.AZTEC_LAYERS))
					{
						base.Hints.Remove(EncodeHintType.AZTEC_LAYERS);
					}
				}
				else
				{
					base.Hints[EncodeHintType.AZTEC_LAYERS] = value;
				}
			}
		}
	}
}
