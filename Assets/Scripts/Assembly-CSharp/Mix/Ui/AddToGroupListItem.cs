using System;
using Disney.Mix.SDK;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AddToGroupListItem : IScrollItem
	{
		private GameObject prefab;

		private IChatThread thread;

		private Action<bool> onFriendsAddedToGroup;

		private bool processed;

		public event OnItemClicked ItemClickedEvent;

		public AddToGroupListItem(GameObject aPrefab, IChatThread aThread, Action<bool> aOnFriendsAddedToGroup)
		{
			prefab = aPrefab;
			thread = aThread;
			onFriendsAddedToGroup = aOnFriendsAddedToGroup;
			prefab.GetComponent<Button>().onClick.AddListener(OnClicked);
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
			gameObject.GetComponent<Button>().onClick.AddListener(OnClicked);
			return gameObject;
		}

		void IScrollItem.Destroy()
		{
		}

		private void OnClicked()
		{
			if (processed)
			{
				return;
			}
			processed = true;
			FriendSelectorPanel friendSelectorPanel = (FriendSelectorPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.FRIEND_SELECTOR);
			friendSelectorPanel.Init(thread, onFriendsAddedToGroup);
			if (this.ItemClickedEvent == null)
			{
				return;
			}
			this.ItemClickedEvent(friendSelectorPanel);
			friendSelectorPanel.PanelClosedEvent += delegate
			{
				if (this != null)
				{
					processed = false;
				}
			};
		}
	}
}
