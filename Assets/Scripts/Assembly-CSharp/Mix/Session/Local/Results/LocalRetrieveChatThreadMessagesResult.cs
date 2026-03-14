using System.Collections.Generic;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Results
{
	public class LocalRetrieveChatThreadMessagesResult : IRetrieveChatThreadMessagesResult
	{
		private IEnumerable<IChatMessage> messages;

		public bool Success
		{
			get
			{
				return true;
			}
		}

		public IEnumerable<IChatMessage> ChatMessages
		{
			get
			{
				return messages;
			}
		}

		public bool IsDone
		{
			get
			{
				return true;
			}
		}

		public LocalRetrieveChatThreadMessagesResult(IEnumerable<IChatMessage> messages)
		{
			this.messages = messages;
		}
	}
}
