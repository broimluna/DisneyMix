namespace Disney.Mix.SDK.Internal
{
	internal class RejectFriendInvitationResult : IRejectFriendInvitationResult
	{
		public bool Success { get; private set; }

		public RejectFriendInvitationResult(bool success)
		{
			Success = success;
		}
	}
}
