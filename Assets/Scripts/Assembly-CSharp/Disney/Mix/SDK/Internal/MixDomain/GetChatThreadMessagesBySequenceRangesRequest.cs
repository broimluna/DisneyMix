using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetChatThreadMessagesBySequenceRangesRequest : BaseUserRequest
	{
		public long? ChatThreadId;

		public List<long?> Ranges;
	}
}
