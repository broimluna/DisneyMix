using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class ClearUnreadMessageCountNotification : BaseNotification
	{
		public List<long?> ChatThreadIds;
	}
}
