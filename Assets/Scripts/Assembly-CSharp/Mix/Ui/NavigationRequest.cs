using System.Collections.Generic;

namespace Mix.Ui
{
	public class NavigationRequest : IScreenHolder
	{
		public enum State
		{
			pending = 0,
			loading = 1,
			processing = 2,
			done = 3
		}

		private List<string> persistentDataMessages;

		public State CurrentState { get; set; }

		public bool IsOverlay { get; private set; }

		public bool IsGoingBack { get; set; }

		public bool Remove { get; set; }

		public bool PopLastRequest { get; set; }

		public string PrefabPath { get; private set; }

		public INavigationManagerListener Caller { private get; set; }

		public ScreenHolder ScreenHolder { get; private set; }

		public BaseNavigationTransition Transition { get; set; }

		public Dictionary<string, object> DataMessages { get; private set; }

		public NavigationRequest(string aPrefabPath, BaseNavigationTransition aTransition, bool aIsOverlay = false)
		{
			PrefabPath = aPrefabPath;
			Transition = aTransition;
			IsOverlay = aIsOverlay;
			PopLastRequest = false;
			DataMessages = new Dictionary<string, object>();
			persistentDataMessages = new List<string>();
		}

		void IScreenHolder.OnScreenHolderReady()
		{
			CurrentState = State.processing;
			ProcessDataMessages();
			Transition.Start(ScreenHolder);
			SendEvent("OnUILoaded");
			UpdateRequest();
			SendEvent("OnUITransitionStart");
		}

		public void Reset()
		{
			CurrentState = State.pending;
			PopLastRequest = false;
			Remove = false;
			ClearDataMessages();
		}

		public void AddData(string aToken, object aData, bool aPersist = false)
		{
			DataMessages[aToken] = aData;
			if (aPersist && !persistentDataMessages.Contains(aToken))
			{
				persistentDataMessages.Add(aToken);
			}
			if (!aPersist && persistentDataMessages.Contains(aToken))
			{
				persistentDataMessages.Remove(aToken);
			}
		}

		public void StartRequest()
		{
			CurrentState = State.loading;
			if (ScreenHolder == null)
			{
				ScreenHolder = new ScreenHolder(this, PrefabPath, Caller.GetCachedScreen(PrefabPath));
			}
			Transition.Setup(Caller.GetLastScreenHolder());
			ScreenHolder.Load();
		}

		public void UpdateRequest()
		{
			if (Transition.Update())
			{
				Caller.OnRequestComplete(this);
			}
		}

		public void ProcessDataMessages()
		{
			if (!ScreenHolder.IsActive())
			{
				return;
			}
			foreach (KeyValuePair<string, object> dataMessage in DataMessages)
			{
				ScreenHolder.SendDataEvent(dataMessage.Key, dataMessage.Value);
			}
			ClearDataMessages();
		}

		public void SendEvent(string aEventName)
		{
			if (ScreenHolder.IsActive())
			{
				ScreenHolder.SendEvent(aEventName, this);
			}
		}

		private void ClearDataMessages()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(DataMessages);
			foreach (KeyValuePair<string, object> item in dictionary)
			{
				if (!persistentDataMessages.Contains(item.Key))
				{
					DataMessages.Remove(item.Key);
				}
			}
		}
	}
}
