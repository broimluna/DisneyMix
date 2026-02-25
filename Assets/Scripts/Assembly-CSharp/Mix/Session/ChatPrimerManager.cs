using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Connectivity;
using Mix.DeviceDb;
using Mix.Session.Extensions;
using Mix.Ui;

namespace Mix.Session
{
	public class ChatPrimerManager : MonoSingleton<ChatPrimerManager>
	{
		private const string ClearedPrimingForVersionKey = "clearedPrimingForVersion";

		private static Version ClearPrimingForVersionsLessThan = new Version("2.2.1");

		private Dictionary<IChatThread, ChatPrimer> chatPrimers = new Dictionary<IChatThread, ChatPrimer>();

		private int currPage;

		private int skipThreadWithThisManyErrors = 1;

		private bool allDone;

		private IChatThread gettingFirstPageNowOnThread;

		private bool sessionPaused;

		private bool waitingForRecentMessages = true;

		private int maxConcurrentPrimers = 1;

		private int concurrentPrimers;

		private int frameDelay;

		public ChatPrimerManager()
		{
			MixSession.OnConnectionChanged += delegate(MixSession.ConnectionState newState, MixSession.ConnectionState oldState)
			{
				switch (newState)
				{
				case MixSession.ConnectionState.RESUMING:
					OnSessionPaused();
					break;
				case MixSession.ConnectionState.ONLINE:
					OnSessionUnPaused();
					break;
				case MixSession.ConnectionState.NOT_STARTED:
					allDone = true;
					waitingForRecentMessages = true;
					chatPrimers.Clear();
					break;
				}
			};
			MixSession.OnGotNewMessages += OnGotRecentMessages;
		}

		public void ResetPriming()
		{
			string text = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.LoadUserValue("clearedPrimingForVersion");
			Version version = new Version((text == null) ? "0.0.0" : text);
			if (version < ClearPrimingForVersionsLessThan)
			{
				ClearAllMostRecentWhenAllDone();
				Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.SaveUserValue("clearedPrimingForVersion", ClearPrimingForVersionsLessThan.ToString());
			}
			currPage = 0;
			skipThreadWithThisManyErrors = 1;
			allDone = false;
			waitingForRecentMessages = true;
			foreach (ChatPrimer value2 in chatPrimers.Values)
			{
				value2.CancelPriming();
			}
			chatPrimers.Clear();
			foreach (IChatThread item in MixSession.User.ChatThreadsFromUsers())
			{
				if (!item.IsFakeOrLocal())
				{
					ChatPrimer value = new ChatPrimer(50, item);
					chatPrimers[item] = value;
					if (gettingFirstPageNowOnThread == item)
					{
						PrimeFirstPageNow(item);
					}
				}
			}
		}

		public void ClearAllMostRecentWhenAllDone()
		{
			foreach (IChatThread item in MixSession.User.ChatThreadsFromUsers())
			{
				if (!item.IsFakeOrLocal())
				{
					Singleton<MixDocumentCollections>.Instance.chatThreadDocumentCollectionApi.AddMostRecentWhenAllDone(item.Id, 0L, "0");
				}
			}
		}

		public void OnGotRecentMessages(bool success, bool newMessages)
		{
			if (success)
			{
				foreach (ChatPrimer value in chatPrimers.Values)
				{
					IChatThread thread = value.Thread;
					if (thread.ChatMessages.Any())
					{
						ChatThreadDocument mostRecentWhenAllDone = Singleton<MixDocumentCollections>.Instance.chatThreadDocumentCollectionApi.GetMostRecentWhenAllDone(thread.Id);
						if (mostRecentWhenAllDone != null)
						{
							IChatMessage newest = thread.GetNewest();
							if (newest != null && newest.Id == mostRecentWhenAllDone.mostRecentMessageId)
							{
								value.IsDonePriming = true;
							}
						}
					}
					else
					{
						value.IsDonePriming = true;
					}
				}
			}
			waitingForRecentMessages = false;
		}

		public void OnSessionUnPaused()
		{
			foreach (IChatThread item in MixSession.User.ChatThreadsFromUsers())
			{
				if (!item.IsFakeOrLocal() && !chatPrimers.ContainsKey(item))
				{
					ChatPrimer value = new ChatPrimer(50, item);
					chatPrimers[item] = value;
					allDone = false;
				}
			}
			sessionPaused = false;
		}

		public void OnSessionPaused()
		{
			foreach (ChatPrimer value in chatPrimers.Values)
			{
				IChatThread thread = value.Thread;
				if (thread.ChatMessages.Any() && value.IsDonePriming)
				{
					IChatMessage newest = thread.GetNewest();
					Singleton<MixDocumentCollections>.Instance.chatThreadDocumentCollectionApi.AddMostRecentWhenAllDone(thread.Id, newest.TimeSent.Ticks, newest.Id);
				}
			}
			sessionPaused = true;
		}

		public void OnThreadDeleted(IChatThread aChatThread)
		{
			if (chatPrimers.ContainsKey(aChatThread))
			{
				chatPrimers[aChatThread].CancelPriming();
				chatPrimers.Remove(aChatThread);
			}
		}

		private void Update()
		{
			if (frameDelay-- <= 0 && !allDone && !sessionPaused && !waitingForRecentMessages && gettingFirstPageNowOnThread == null && MonoSingleton<ConnectionManager>.Instance.IsConnected && concurrentPrimers < maxConcurrentPrimers)
			{
				findNextThreadToPrime();
				frameDelay = 10;
			}
		}

		private void findNextThreadToPrime()
		{
			int num = 0;
			int num2 = 0;
			foreach (ChatPrimer item in chatPrimers.Values.OrderByDescending((ChatPrimer x) => (!x.Thread.ChatMessages.Any()) ? DateTime.MinValue : x.Thread.ChatMessages.Last().TimeSent))
			{
				if (item.IsDonePriming)
				{
					continue;
				}
				num++;
				if (item.ErrorCount >= skipThreadWithThisManyErrors)
				{
					num2++;
				}
				else if (item.CurrentPage <= currPage && !item.IsPriming)
				{
					concurrentPrimers++;
					item.ContinuePriming(delegate
					{
						concurrentPrimers--;
					});
					return;
				}
			}
			if (num == 0)
			{
				allDone = true;
				return;
			}
			if (num2 > 0)
			{
				skipThreadWithThisManyErrors++;
			}
			currPage++;
		}

		public void PrimeFirstPageNow(IChatThread thread)
		{
			if (thread.IsFakeOrLocal() || !MonoSingleton<ConnectionManager>.Instance.IsConnected)
			{
				return;
			}
			ChatPrimer chatPrimer;
			if (!chatPrimers.ContainsKey(thread))
			{
				chatPrimer = new ChatPrimer(50, thread);
				chatPrimers[thread] = chatPrimer;
				allDone = false;
			}
			else
			{
				chatPrimer = chatPrimers[thread];
			}
			if (chatPrimer.CurrentPage == 0 && !chatPrimer.IsPriming && !chatPrimer.IsDonePriming)
			{
				gettingFirstPageNowOnThread = thread;
				chatPrimer.ContinuePriming(delegate
				{
					gettingFirstPageNowOnThread = null;
				});
			}
		}

		public void ContinuePriming(IChatThread thread)
		{
			if (chatPrimers.ContainsKey(thread))
			{
				chatPrimers[thread].ContinuePriming();
			}
		}

		public void OnFirstPageReady(IChatThread thread, Action callback)
		{
			if (chatPrimers.ContainsKey(thread))
			{
				ChatPrimer chatPrimer = chatPrimers[thread];
				if (chatPrimer.CurrentPage == 0)
				{
					if (chatPrimer.IsPriming)
					{
						chatPrimer.AddCallback(callback);
						return;
					}
					if (!chatPrimer.IsDonePriming && chatPrimer.ErrorCount == 0 && MonoSingleton<ConnectionManager>.Instance.IsConnected)
					{
						Log.Exception("PRIMING NOT STARTED on " + thread.GetThreadTitle());
					}
				}
			}
			callback();
		}

		public bool IsDonePriming(IChatThread chatThread)
		{
			if (chatPrimers.ContainsKey(chatThread))
			{
				return chatPrimers[chatThread].IsDonePriming;
			}
			return true;
		}
	}
}
