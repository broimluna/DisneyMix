using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class ClearMemberChatHistoryNotification : BaseNotification
	{
		public List<long?> ChatThreadIds;
	}
}
