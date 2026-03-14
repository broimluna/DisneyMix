using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IRetrieveChatThreadMessagesResult
	{
		bool Success { get; }

		IEnumerable<IChatMessage> ChatMessages { get; }

		bool IsDone { get; }
	}
}
