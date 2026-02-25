using Disney.Mix.SDK;

namespace Avatar.DataTypes
{
	public class ClientAvatarProperty : IAvatarProperty
	{
		public string SelectionKey { get; set; }

		public int TintIndex { get; set; }

		public double XOffset { get; set; }

		public double YOffset { get; set; }

		public ClientAvatarProperty()
		{
		}

		public ClientAvatarProperty(string SelectionKey, int TintIndex, double XOffset, double YOffset)
		{
			this.SelectionKey = SelectionKey;
			this.TintIndex = TintIndex;
			this.XOffset = XOffset;
			this.YOffset = YOffset;
		}
	}
}
