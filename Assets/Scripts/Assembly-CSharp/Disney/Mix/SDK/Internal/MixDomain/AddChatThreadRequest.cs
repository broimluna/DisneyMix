using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class AddChatThreadRequest : BaseUserRequest
	{
		public string ChatThreadType;

		public List<string> MemberUserIds;
	}
}
