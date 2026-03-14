using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class AddChatThreadMembershipRequest : BaseUserRequest
	{
		public long? ChatThreadId;

		public List<string> AddedUserIds;
	}
}
