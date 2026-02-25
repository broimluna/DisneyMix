using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetSpecificChatThreadMessagesRequest : BaseUserRequest
	{
		public long? ChatThreadId;

		public List<long?> MessageIds;
	}
}
