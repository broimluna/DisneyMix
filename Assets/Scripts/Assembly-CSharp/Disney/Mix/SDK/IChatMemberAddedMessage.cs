using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IChatMemberAddedMessage : IChatMessage
	{
		IEnumerable<string> MemberIds { get; }
	}
}
