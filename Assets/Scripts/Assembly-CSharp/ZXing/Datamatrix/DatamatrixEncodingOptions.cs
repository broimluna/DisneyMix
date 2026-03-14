using System;
using ZXing.Common;
using ZXing.Datamatrix.Encoder;

namespace ZXing.Datamatrix
{
	[Serializable]
	public class DatamatrixEncodingOptions : EncodingOptions
	{
		public SymbolShapeHint? SymbolShape
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.DATA_MATRIX_SHAPE))
				{
					return (SymbolShapeHint)(int)base.Hints[EncodeHintType.DATA_MATRIX_SHAPE];
				}
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					if (base.Hints.ContainsKey(EncodeHintType.DATA_MATRIX_SHAPE))
					{
						base.Hints.Remove(EncodeHintType.DATA_MATRIX_SHAPE);
					}
				}
				else
				{
					base.Hints[EncodeHintType.DATA_MATRIX_SHAPE] = value;
				}
			}
		}

		public Dimension MinSize
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.MIN_SIZE))
				{
					return (Dimension)base.Hints[EncodeHintType.MIN_SIZE];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					if (base.Hints.ContainsKey(EncodeHintType.MIN_SIZE))
					{
						base.Hints.Remove(EncodeHintType.MIN_SIZE);
					}
				}
				else
				{
					base.Hints[EncodeHintType.MIN_SIZE] = value;
				}
			}
		}

		public Dimension MaxSize
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.MAX_SIZE))
				{
					return (Dimension)base.Hints[EncodeHintType.MAX_SIZE];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					if (base.Hints.ContainsKey(EncodeHintType.MAX_SIZE))
					{
						base.Hints.Remove(EncodeHintType.MAX_SIZE);
					}
				}
				else
				{
					base.Hints[EncodeHintType.MAX_SIZE] = value;
				}
			}
		}

		public int? DefaultEncodation
		{
			get
			{
				if (base.Hints.ContainsKey(EncodeHintType.DATA_MATRIX_DEFAULT_ENCODATION))
				{
					return (int)base.Hints[EncodeHintType.DATA_MATRIX_DEFAULT_ENCODATION];
				}
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					if (base.Hints.ContainsKey(EncodeHintType.DATA_MATRIX_DEFAULT_ENCODATION))
					{
						base.Hints.Remove(EncodeHintType.DATA_MATRIX_DEFAULT_ENCODATION);
					}
				}
				else
				{
					base.Hints[EncodeHintType.DATA_MATRIX_DEFAULT_ENCODATION] = value;
				}
			}
		}
	}
}
