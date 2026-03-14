using Disney.Mix.SDK;

namespace Mix.Session.Local.Thread
{
	public class LocalChatThreadMemberAddedArgs : AbstractChatThreadMemberAddedEventArgs
	{
		public override string MemberId { get; protected set; }

		public LocalChatThreadMemberAddedArgs(string memberId)
		{
			MemberId = memberId;
		}
	}
}
