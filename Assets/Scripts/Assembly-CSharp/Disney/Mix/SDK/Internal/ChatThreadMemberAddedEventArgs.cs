namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadMemberAddedEventArgs : AbstractChatThreadMemberAddedEventArgs
	{
		public override string MemberId { get; protected set; }

		public ChatThreadMemberAddedEventArgs(string memberId)
		{
			MemberId = memberId;
		}
	}
}
