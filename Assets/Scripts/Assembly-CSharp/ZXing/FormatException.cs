namespace ZXing
{
	public sealed class FormatException : ReaderException
	{
		private static readonly FormatException instance = new FormatException();

		public new static FormatException Instance
		{
			get
			{
				return instance;
			}
		}

		private FormatException()
		{
		}
	}
}
