namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadTrustLevel : IChatThreadTrustLevel
	{
		public bool AllMembersTrustEachOther { get; private set; }

		public ChatThreadTrustLevel(bool allMembersTrustEachOther)
		{
			AllMembersTrustEachOther = allMembersTrustEachOther;
		}
	}
}
