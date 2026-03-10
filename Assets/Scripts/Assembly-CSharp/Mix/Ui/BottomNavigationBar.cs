using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
    public class BottomNavigationBar : MonoBehaviour
    {
        [Header("UI References")]
        public Button ProfileButton;
        public Button ChatsButton;
        public Button FriendsButton;

        private Text chatCounter;
        private Text friendCounter;

        // Cache specialized button types
        private PressureSensitiveButton chatPressureBtn;
        private PressureSensitiveButton friendPressureBtn;

        private void Start()
        {
            if (ChatsButton != null)
            {
                // Better to assign these in the Inspector if possible!
                chatCounter = ChatsButton.transform.Find("NotificationIcon/NotificationCounterText")?.GetComponent<Text>();
                chatPressureBtn = ChatsButton.GetComponent<PressureSensitiveButton>();
            }

            if (FriendsButton != null)
            {
                friendCounter = FriendsButton.transform.Find("NotificationIcon/NotificationCounterText")?.GetComponent<Text>();
                friendPressureBtn = FriendsButton.GetComponent<PressureSensitiveButton>();
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
            // 'onClick' is a UnityEvent, not a boolean. 
            // Assuming the intention was to check if the button is interactable or just always execute logic since this IS the click handler.
            // If you intended to check a state, you might need a different property, but for now removing the invalid comparison.
            if (ProfileButton.GetComponent<Button>().interactable)
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
            var navigationRequest = new NavigationRequest("Prefabs/Screens/Conversations/ConversationsScreen", new TransitionNone());

            // Check if user pressed hard (3D Touch style)
            if (chatPressureBtn != null && chatPressureBtn.HardPress)
            {
                navigationRequest.AddData("startConversation", true);
            }

            MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
        }

        public void OnFriendsButtonClicked()
        {
            Analytics.LogNavigationAction("history", "friend_list");
            var navigationRequest = new NavigationRequest("Prefabs/Screens/Friends/FriendsScreen", new TransitionNone());

            if (friendPressureBtn != null && friendPressureBtn.HardPress)
            {
                navigationRequest.AddData("qrCode", true);
            }

            MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
        }

        public void UpdateBadges()
        {
            // Update Chat Badges
            if (chatCounter != null)
            {
                uint unread = MixChat.GetTotalDisplayableUnreadMessageCount();
                chatCounter.transform.parent.gameObject.SetActive(unread > 0);
                if (unread > 0) chatCounter.text = unread.ToString();
            }

            // Update Friend Badges
            if (friendCounter != null)
            {
                int invites = MixFriends.GetMaxDisplayableFriendInvites();
                friendCounter.transform.parent.gameObject.SetActive(invites > 0);
                if (invites > 0) friendCounter.text = invites.ToString();
            }
        }
    }
}