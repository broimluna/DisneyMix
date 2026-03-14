using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class MemberListChangedMessageDocumentPayload
	{
		public List<string> MemberUserIds;

		public bool? IsAdded;
	}
}
