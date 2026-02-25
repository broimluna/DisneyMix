using System;
using Disney.Mix.SDK;

namespace Mix.Ui
{
	public class InviteAcceptedEventArgs : EventArgs
	{
		public IFriend Friend;

		public bool WithTrust;

		public InviteAcceptedEventArgs(IFriend aFriend, bool aWithTrust)
		{
			Friend = aFriend;
			WithTrust = aWithTrust;
		}
	}
}
