namespace Disney.Mix.SDK.Internal
{
	public class AvatarProperty : IInternalAvatarProperty, IAvatarProperty
	{
		public string SelectionKey { get; set; }

		public int TintIndex { get; set; }

		public double XOffset { get; set; }

		public double YOffset { get; set; }

		public AvatarProperty()
		{
		}

		public AvatarProperty(string selectionKey, int tintIndex, double xOffset, double yOffset)
		{
			SelectionKey = selectionKey;
			TintIndex = tintIndex;
			XOffset = xOffset;
			YOffset = yOffset;
		}
	}
}
