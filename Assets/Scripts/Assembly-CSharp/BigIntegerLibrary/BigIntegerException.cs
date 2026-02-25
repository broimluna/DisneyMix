using System;

namespace BigIntegerLibrary
{
	[Serializable]
	public sealed class BigIntegerException : Exception
	{
		public BigIntegerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
