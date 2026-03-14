using System;
using System.Collections.Generic;

namespace ZXing.Common
{
	public sealed class DecoderResult
	{
		public byte[] RawBytes { get; private set; }

		public string Text { get; private set; }

		public IList<byte[]> ByteSegments { get; private set; }

		public string ECLevel { get; private set; }

		public bool StructuredAppend
		{
			get
			{
				return StructuredAppendParity >= 0 && StructuredAppendSequenceNumber >= 0;
			}
		}

		public int ErrorsCorrected { get; set; }

		public int StructuredAppendSequenceNumber { get; private set; }

		public int Erasures { get; set; }

		public int StructuredAppendParity { get; private set; }

		public object Other { get; set; }

		public DecoderResult(byte[] rawBytes, string text, IList<byte[]> byteSegments, string ecLevel)
			: this(rawBytes, text, byteSegments, ecLevel, -1, -1)
		{
		}

		public DecoderResult(byte[] rawBytes, string text, IList<byte[]> byteSegments, string ecLevel, int saSequence, int saParity)
		{
			if (rawBytes == null && text == null)
			{
				throw new ArgumentException();
			}
			RawBytes = rawBytes;
			Text = text;
			ByteSegments = byteSegments;
			ECLevel = ecLevel;
			StructuredAppendParity = saParity;
			StructuredAppendSequenceNumber = saSequence;
		}
	}
}
