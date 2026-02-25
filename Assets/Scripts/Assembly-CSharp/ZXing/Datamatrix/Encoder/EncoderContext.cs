using System;
using System.Text;

namespace ZXing.Datamatrix.Encoder
{
	internal sealed class EncoderContext
	{
		private readonly string msg;

		private SymbolShapeHint shape;

		private Dimension minSize;

		private Dimension maxSize;

		private readonly StringBuilder codewords;

		private int pos;

		private int newEncoding;

		private SymbolInfo symbolInfo;

		private int skipAtEnd;

		private static readonly Encoding encoding;

		public char CurrentChar
		{
			get
			{
				return msg[pos];
			}
		}

		public char Current
		{
			get
			{
				return msg[pos];
			}
		}

		public int CodewordCount
		{
			get
			{
				return codewords.Length;
			}
		}

		public bool HasMoreCharacters
		{
			get
			{
				return pos < TotalMessageCharCount;
			}
		}

		private int TotalMessageCharCount
		{
			get
			{
				return msg.Length - skipAtEnd;
			}
		}

		public int RemainingCharacters
		{
			get
			{
				return TotalMessageCharCount - pos;
			}
		}

		public int Pos
		{
			get
			{
				return pos;
			}
			set
			{
				pos = value;
			}
		}

		public StringBuilder Codewords
		{
			get
			{
				return codewords;
			}
		}

		public SymbolInfo SymbolInfo
		{
			get
			{
				return symbolInfo;
			}
		}

		public int NewEncoding
		{
			get
			{
				return newEncoding;
			}
		}

		public string Message
		{
			get
			{
				return msg;
			}
		}

		public EncoderContext(string msg)
		{
			byte[] bytes = encoding.GetBytes(msg);
			StringBuilder stringBuilder = new StringBuilder(bytes.Length);
			int num = bytes.Length;
			for (int i = 0; i < num; i++)
			{
				char c = (char)(bytes[i] & 0xFF);
				if (c == '?' && msg[i] != '?')
				{
					throw new ArgumentException("Message contains characters outside " + encoding.WebName + " encoding.");
				}
				stringBuilder.Append(c);
			}
			this.msg = stringBuilder.ToString();
			shape = SymbolShapeHint.FORCE_NONE;
			codewords = new StringBuilder(msg.Length);
			newEncoding = -1;
		}

		static EncoderContext()
		{
			encoding = Encoding.GetEncoding("ISO-8859-1");
		}

		public void setSymbolShape(SymbolShapeHint shape)
		{
			this.shape = shape;
		}

		public void setSizeConstraints(Dimension minSize, Dimension maxSize)
		{
			this.minSize = minSize;
			this.maxSize = maxSize;
		}

		public void setSkipAtEnd(int count)
		{
			skipAtEnd = count;
		}

		public void writeCodewords(string codewords)
		{
			this.codewords.Append(codewords);
		}

		public void writeCodeword(char codeword)
		{
			codewords.Append(codeword);
		}

		public void signalEncoderChange(int encoding)
		{
			newEncoding = encoding;
		}

		public void resetEncoderSignal()
		{
			newEncoding = -1;
		}

		public void updateSymbolInfo()
		{
			updateSymbolInfo(CodewordCount);
		}

		public void updateSymbolInfo(int len)
		{
			if (symbolInfo == null || len > symbolInfo.dataCapacity)
			{
				symbolInfo = SymbolInfo.lookup(len, shape, minSize, maxSize, true);
			}
		}

		public void resetSymbolInfo()
		{
			symbolInfo = null;
		}
	}
}
