using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	internal class RetrieveChatThreadMessagesResult : IRetrieveChatThreadMessagesResult
	{
		public bool Success { get; private set; }

		public IEnumerable<IChatMessage> ChatMessages { get; private set; }

		public bool IsDone { get; private set; }

		public RetrieveChatThreadMessagesResult(bool success, IEnumerable<IChatMessage> chatMessages, bool isDone)
		{
			Success = success;
			ChatMessages = chatMessages;
			IsDone = isDone;
		}
	}
}
