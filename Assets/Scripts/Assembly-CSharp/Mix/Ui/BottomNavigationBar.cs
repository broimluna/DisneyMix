using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class BottomNavigationBar : MonoBehaviour
	{
		public Button ProfileButton;

		public Button ChatsButton;

		public Button FriendsButton;

		private Text chatCounter;

		private Text friendCounter;

		private void Start()
		{
			if (ChatsButton != null)
			{
				chatCounter = ChatsButton.transform.Find("NotificationIcon/NotificationCounterText").GetComponent<Text>();
			}
			if (FriendsButton != null)
			{
				friendCounter = FriendsButton.transform.Find("NotificationIcon/NotificationCounterText").GetComponent<Text>();
			}
			MixFriends.OnBadgesChanged += UpdateBadges;
			UpdateBadges();
		}

		private void OnDestroy()
		{
			MixFriends.OnBadgesChanged -= UpdateBadges;
		}

		public void OnProfileButtonClicked()
		{
			NavigationRequest navigationRequest = null;
			if (ProfileButton.GetComponent<PressureSensitiveButton>().HardPress)
			{
				navigationRequest = new NavigationRequest("Prefabs/Screens/AvatarEditor/AvatarEditorScreen", new TransitionAnimations());
				navigationRequest.AddData("mode", AvatarEditorController.EDITOR_MODES.EDITOR);
			}
			else
			{
				navigationRequest = new NavigationRequest("Prefabs/Screens/Profile/ProfileScreen", new TransitionNone());
			}
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		public void OnChatsButtonClicked()
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Conversations/ConversationsScreen", new TransitionNone());
			if (ChatsButton.GetComponent<PressureSensitiveButton>().HardPress)
			{
				navigationRequest.AddData("startConversation", true);
			}
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		public void OnFriendsButtonClicked()
		{
			if (MixSession.ParentalConsentRequired)
			{
				ParentalConsent.ShowGateDialog();
				return;
			}
			Analytics.LogNavigationAction("history", "friend_list");
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Friends/FriendsScreen", new TransitionNone());
			if (FriendsButton.GetComponent<PressureSensitiveButton>().HardPress)
			{
				navigationRequest.AddData("qrCode", true);
			}
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		public void UpdateBadges()
		{
			if (ChatsButton != null)
			{
				uint totalDisplayableUnreadMessageCount = MixChat.GetTotalDisplayableUnreadMessageCount();
				if (totalDisplayableUnreadMessageCount != 0)
				{
					chatCounter.transform.parent.gameObject.SetActive(true);
					chatCounter.text = totalDisplayableUnreadMessageCount.ToString();
				}
				else
				{
					chatCounter.transform.parent.gameObject.SetActive(false);
				}
			}
			if (FriendsButton != null)
			{
				int maxDisplayableFriendInvites = MixFriends.GetMaxDisplayableFriendInvites();
				if (maxDisplayableFriendInvites > 0)
				{
					friendCounter.transform.parent.gameObject.SetActive(true);
					friendCounter.text = maxDisplayableFriendInvites.ToString();
				}
				else
				{
					friendCounter.transform.parent.gameObject.SetActive(false);
				}
			}
		}
	}
}
