namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadTrustStatusChangedEventArgs : AbstractChatThreadTrustStatusChangedEventArgs
	{
		public override IChatThreadTrustLevel TrustLevel { get; protected set; }

		public ChatThreadTrustStatusChangedEventArgs(IChatThreadTrustLevel trustLevel)
		{
			TrustLevel = trustLevel;
		}
	}
}
