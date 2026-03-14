using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class ChatThread
	{
		public long? ChatThreadId;

		public string ChatThreadType;

		public bool? IsTrusted;

		public List<User> Members;

		public string OfficialAccountId;
	}
}
