using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetChatThreadsRecentMessagesRequest : BaseUserRequest
	{
		public List<long?> LowerBoundSequenceNumbers;

		public int? MaxMessagesPerChatThread;
	}
}
