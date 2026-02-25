using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class ClearMemberChatHistoryRequest : BaseUserRequest
	{
		public List<long?> ChatThreadIds;
	}
}
