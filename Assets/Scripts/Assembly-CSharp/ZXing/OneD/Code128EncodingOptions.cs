using System;
using ZXing.Common;

namespace ZXing.OneD
{
	[Serializable]
	public class Code128EncodingOptions : EncodingOptions
	{
		public bool ForceCodesetB
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.CODE128_FORCE_CODESET_B))
				{
					return (bool)base.Hints[EncodeHintType.CODE128_FORCE_CODESET_B];
				}
				return false;
			}
			set
			{
				base.Hints[EncodeHintType.CODE128_FORCE_CODESET_B] = value;
			}
		}
	}
}
