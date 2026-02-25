namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadMemberRemovedEventArgs : AbstractChatThreadMemberRemovedEventArgs
	{
		public override string MemberId { get; protected set; }

		public ChatThreadMemberRemovedEventArgs(string memberId)
		{
			MemberId = memberId;
		}
	}
}
