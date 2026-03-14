using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.DeviceDb;
using Mix.Session.Extensions;

namespace Mix.Ui
{
	public class ChatPrimer
	{
		public enum PrimingState
		{
			not_started = 0,
			priming_first_page = 1,
			done = 2
		}

		private const int PAGES_TO_CACHE = 1;

		public bool IsPriming;

		public bool IsDonePriming;

		public IChatThread Thread;

		public int CurrentPage;

		public int ErrorCount;

		private List<Action> callbacks = new List<Action>();

		private int pagesize;

		private Action callback;

		private IChatMessageRetriever retriever;

		private SdkActions actionGenerator = new SdkActions();

		private bool dbDone;

		public ChatPrimer(int pagesize, IChatThread thread)
		{
			this.pagesize = pagesize;
			Thread = thread;
		}

		public void ContinuePriming(Action callback = null)
		{
			if (IsDonePriming)
			{
				if (callback != null)
				{
					callback();
				}
				return;
			}
			if (retriever == null)
			{
				retriever = Thread.CreateChatMessageRetriever(pagesize);
			}
			AddCallback(callback);
			RetrieveMessages();
		}

		public void AddCallback(Action callback)
		{
			if (callback != null)
			{
				callbacks.Add(callback);
			}
		}

		public void CancelPriming()
		{
			callbacks.Clear();
			IsDonePriming = true;
		}

		private void RetrieveMessages()
		{
			IsPriming = true;
			dbDone = false;
			retriever.RetrieveMessages(actionGenerator.CreateAction<IRetrieveChatThreadMessagesResult>(dbRetrieval), actionGenerator.CreateAction<IRetrieveChatThreadMessagesResult>(serverRetrieval));
		}

		private void dbRetrieval(IRetrieveChatThreadMessagesResult result)
		{
			if (result.Success && result.IsDone)
			{
				dbDone = true;
			}
		}

		private void serverRetrieval(IRetrieveChatThreadMessagesResult result)
		{
			if (result.Success)
			{
				CurrentPage++;
				ErrorCount = 0;
				if (result.IsDone && dbDone)
				{
					retriever = null;
					IsDonePriming = true;
					if (Thread.ChatMessages.Any())
					{
						IChatMessage newest = Thread.GetNewest();
						Singleton<MixDocumentCollections>.Instance.chatThreadDocumentCollectionApi.AddMostRecentWhenAllDone(Thread.Id, newest.TimeSent.Ticks, newest.Id);
					}
				}
				else
				{
					ChatThreadDocument mostRecentWhenAllDone = Singleton<MixDocumentCollections>.Instance.chatThreadDocumentCollectionApi.GetMostRecentWhenAllDone(Thread.Id);
					if (mostRecentWhenAllDone != null)
					{
						IChatMessage newest2 = Thread.GetNewest();
						if (newest2 != null && mostRecentWhenAllDone.mostRecentMessageId == newest2.Id)
						{
							IsDonePriming = true;
						}
						else if (result.ChatMessages.Any() && result.ChatMessages.First().TimeSent.Ticks < mostRecentWhenAllDone.mostRecentWhenAllDone)
						{
							IsDonePriming = true;
						}
					}
				}
			}
			else if (++ErrorCount >= 3)
			{
				IsDonePriming = true;
			}
			OnDonePrimingPage();
		}

		private void OnDonePrimingPage()
		{
			IsPriming = false;
			Action[] array = callbacks.ToArray();
			callbacks.Clear();
			Action[] array2 = array;
			foreach (Action action in array2)
			{
				if (action != null)
				{
					action();
				}
			}
		}
	}
}
