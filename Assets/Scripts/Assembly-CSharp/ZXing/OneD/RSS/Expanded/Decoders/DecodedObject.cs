namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal abstract class DecodedObject
	{
		internal int NewPosition { get; private set; }

		internal DecodedObject(int newPosition)
		{
			NewPosition = newPosition;
		}
	}
}
