namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class DecodedChar : DecodedObject
	{
		private char value;

		internal static char FNC1 = '$';

		internal DecodedChar(int newPosition, char value)
			: base(newPosition)
		{
			this.value = value;
		}

		internal char getValue()
		{
			return value;
		}

		internal bool isFNC1()
		{
			return value == FNC1;
		}
	}
}
