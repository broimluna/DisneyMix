using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class MemberListChangedChatMessage : BaseChatMessage
	{
		public List<string> MemberUserIds;

		public bool? IsAdded;
	}
}
