using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Session.Local.Results;

namespace Mix.Session.Local.Thread
{
	public class LocalChatMessageRetriever : IChatMessageRetriever
	{
		private IEnumerable<IChatMessage> messages;

		private int chunkSize;

		private int currentIndex;

		public LocalChatMessageRetriever(int chunkSize, IEnumerable<IChatMessage> messages)
		{
			this.chunkSize = chunkSize;
			this.messages = messages;
			currentIndex = 0;
		}

		public void RetrieveMessages(Action<IRetrieveChatThreadMessagesResult> callback)
		{
			int num = chunkSize;
			List<IChatMessage> list = new List<IChatMessage>();
			while (num > 0)
			{
				if (messages.Count() - (num + currentIndex) >= 0)
				{
					list.Add(messages.ElementAt(messages.Count() - (num + currentIndex)));
				}
				num--;
			}
			currentIndex += chunkSize;
			callback(new LocalRetrieveChatThreadMessagesResult(messages));
		}

		public void RetrieveMessages(Action<IRetrieveChatThreadMessagesResult> localCallback, Action<IRetrieveChatThreadMessagesResult> remoteCallback)
		{
			int num = chunkSize;
			List<IChatMessage> list = new List<IChatMessage>();
			while (num > 0)
			{
				if (messages.Count() - (num + currentIndex) >= 0)
				{
					list.Add(messages.ElementAt(messages.Count() - (num + currentIndex)));
				}
				num--;
			}
			currentIndex += chunkSize;
			localCallback(new LocalRetrieveChatThreadMessagesResult(list));
		}
	}
}
