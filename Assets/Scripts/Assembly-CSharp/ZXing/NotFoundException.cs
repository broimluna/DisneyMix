using System;

namespace ZXing
{
	[Obsolete("Isn't used anymore, will be removed with next version")]
	public sealed class NotFoundException : ReaderException
	{
		private static readonly NotFoundException instance = new NotFoundException();

		public new static NotFoundException Instance
		{
			get
			{
				return instance;
			}
		}

		private NotFoundException()
		{
		}
	}
}
