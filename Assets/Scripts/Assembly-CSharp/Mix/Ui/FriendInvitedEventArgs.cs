using System;
using Disney.Mix.SDK;

namespace Mix.Ui
{
	public class FriendInvitedEventArgs : EventArgs
	{
		public IOutgoingFriendInvitation OutgoingInvite;

		public FriendInvitedEventArgs(IOutgoingFriendInvitation aOutgoingInvite)
		{
			OutgoingInvite = aOutgoingInvite;
		}
	}
}
