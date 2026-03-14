namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class DecodedInformation : DecodedObject
	{
		private string newString;

		private int remainingValue;

		private bool remaining;

		internal DecodedInformation(int newPosition, string newString)
			: base(newPosition)
		{
			this.newString = newString;
			remaining = false;
			remainingValue = 0;
		}

		internal DecodedInformation(int newPosition, string newString, int remainingValue)
			: base(newPosition)
		{
			remaining = true;
			this.remainingValue = remainingValue;
			this.newString = newString;
		}

		internal string getNewString()
		{
			return newString;
		}

		internal bool isRemaining()
		{
			return remaining;
		}

		internal int getRemainingValue()
		{
			return remainingValue;
		}
	}
}
