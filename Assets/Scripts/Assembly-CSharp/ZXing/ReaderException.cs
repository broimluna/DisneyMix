using System;

namespace ZXing
{
	[Serializable]
	public class ReaderException : Exception
	{
		private static readonly ReaderException instance = new ReaderException();

		public static ReaderException Instance
		{
			get
			{
				return instance;
			}
		}

		protected ReaderException()
		{
		}
	}
}
