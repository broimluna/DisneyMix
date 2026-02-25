using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class AddChatThreadMembershipNotification : BaseNotification
	{
		public long? ChatThreadId;

		public List<User> Members;

		public string WelcomingUserId;
	}
}
