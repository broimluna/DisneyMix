using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetChatThreadMessagesRequest : BaseUserRequest
	{
		public long? ChatThreadId;

		public List<long?> ExcludedChatMessageIds;

		public int? MaximumMessageCount;

		public long? TimestampOffset;

		public string Cursor;
	}
}
