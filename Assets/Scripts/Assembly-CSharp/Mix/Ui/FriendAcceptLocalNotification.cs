using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using Mix.Games;
using Mix.Localization;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class FriendAcceptLocalNotification : BaseNofitication
	{
		public IFriend Friend;

		public FriendAcceptLocalNotification(IFriend aFriend)
		{
			Friend = aFriend;
		}

		public override GameObject GenerateGameObject()
		{
			GameObject original = Resources.Load<GameObject>("Prefabs/Ui/NotificationPanel");
			GameObject gameObject = Object.Instantiate(original);
			Text component = gameObject.transform.Find("Background/ChatText").GetComponent<Text>();
			GameObject gameObject2 = gameObject.transform.Find("Background/AvatarHead").gameObject;
			Text component2 = gameObject.transform.Find("Background/DisplayNameText").GetComponent<Text>();
			Button gotoFriendsButton = gameObject.transform.Find("Background").GetComponent<Button>();
			string name = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(Friend.Avatar)) ? "ImageTarget" : "ImageTarget_Geo");
			Transform imageTarget = gameObject2.transform.Find(name);
			int size = (int)imageTarget.GetComponent<RectTransform>().rect.height;

			UnityAction listener = null;
			listener = delegate
			{
				gotoFriendsButton.GetComponent<Button>().onClick.RemoveListener(listener);
				NavigationRequest lastProcessedRequest = MonoSingleton<NavigationManager>.Instance.GetLastProcessedRequest();
				if (lastProcessedRequest != null && lastProcessedRequest.PrefabPath != "Prefabs/Screens/Friends/FriendsScreen")
				{
					MonoSingleton<GameManager>.Instance.QuitGameSession();
					lastProcessedRequest = new NavigationRequest("Prefabs/Screens/Friends/FriendsScreen", new TransitionNone());
					MonoSingleton<NavigationManager>.Instance.AddRequest(lastProcessedRequest);
				}
			};
			gotoFriendsButton.GetComponent<Button>().onClick.AddListener(listener);

			component2.text = Friend.NickFirstOrDisplayName();
			component.text = Singleton<Localizer>.Instance.getString("customtokens.friends.notification_isnowfriend");
			if (Friend.IsTrusted)
			{
				component.text = Singleton<Localizer>.Instance.getString("customtokens.friends.notification_isnowtrustedfriend");
			}
			MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(Friend.Avatar, (AvatarFlags)0, size, delegate(bool aSuccess, Sprite aSprite)
			{
				if (aSprite != null && imageTarget != null && imageTarget.GetComponent<Image>() != null)
				{
					imageTarget.GetComponent<Image>().sprite = aSprite;
					imageTarget.gameObject.SetActive(true);
				}
			});
			return gameObject;
		}
	}
}
