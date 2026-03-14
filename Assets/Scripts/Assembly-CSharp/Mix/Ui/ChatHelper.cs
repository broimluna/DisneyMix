using System;
using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Data;
using Mix.Entitlements;
using Mix.GagManagement;
using Mix.Games;
using Mix.Session;
using UnityEngine;

namespace Mix.Ui
{
	public class ChatHelper : Singleton<ChatHelper>, IBundleObject
	{
		void IBundleObject.OnBundleAssetObject(UnityEngine.Object aGameObject, object aUserData)
		{
			if (!(aGameObject != null))
			{
				return;
			}
			string aGagURL = string.Empty;
			List<Gag> myBookEnds = Singleton<EntitlementsManager>.Instance.GetMyBookEnds();
			foreach (Gag item in myBookEnds)
			{
				if (item != null && item.GetName().Contains("avatarChange"))
				{
					aGagURL = item.GetHd();
				}
			}
			if (aUserData.Equals("gag"))
			{
				MonoSingleton<GagManager>.Instance.PlayGag((GameObject)aGameObject, aGagURL, GagManager.SenderPosition.LEFT, "sender");
			}
		}

		public static bool ValidateMessage(IChatThread aThread, IChatMessage aMessage)
		{
			if (aThread == null || aMessage == null)
			{
				return false;
			}
			return true;
		}

		public static int CompareScrollItems(IScrollItem aItemOne, IScrollItem aItemTwo)
		{
			return GetItemTimeStamp(aItemOne).ToUniversalTime().CompareTo(GetItemTimeStamp(aItemTwo).ToUniversalTime());
		}

		public static DateTime GetItemTimeStamp(IScrollItem aItem)
		{
			if (aItem is BaseChatItem)
			{
				return (aItem as BaseChatItem).DateTime;
			}
			return DateTime.MinValue;
		}

		public void UpdateGagSkins()
		{
			if (Singleton<EntitlementsManager>.Instance == null || MonoSingleton<AssetManager>.Instance == null)
			{
				return;
			}
			List<Gag> myBookEnds = Singleton<EntitlementsManager>.Instance.GetMyBookEnds();
			if (myBookEnds == null || myBookEnds.Count <= 0)
			{
				return;
			}
			foreach (Gag item in myBookEnds)
			{
				if (item != null && item.GetName().Contains("avatarChange"))
				{
					MonoSingleton<AssetManager>.Instance.LoadABundle(this, item.GetHd(), "gag", string.Empty);
					break;
				}
			}
		}

		public void OnChatNotificationClicked(IChatThread fromThread, IChatThread toThread)
		{
			MonoSingleton<GameManager>.Instance.QuitGameSession();
			BaseNavigationTransition baseNavigationTransition = null;
			baseNavigationTransition = ((!(fromThread is IGroupChatThread) && !(toThread is IGroupChatThread)) ? new TransitionSlideLeft() : ((fromThread is IGroupChatThread && !(toThread is IGroupChatThread)) ? new TransitionSlideLeft() : ((!(fromThread is IGroupChatThread) || !(toThread is IGroupChatThread)) ? new TransitionSlideLeft() : new TransitionSlideLeft())));
			NavigateToChatScreen(toThread, baseNavigationTransition);
		}

		public static NavigationRequest NavigateToChatScreen(IChatThread aThread, BaseNavigationTransition transition, bool fromPushNote = false, bool addRequest = true)
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/ChatMix/ChatMixScreen", transition);
			navigationRequest.AddData("thread:", aThread);
			if (fromPushNote)
			{
				navigationRequest.AddData("fromPushNote", true);
			}
			if (addRequest)
			{
				MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			}
			MonoSingleton<ChatPrimerManager>.Instance.PrimeFirstPageNow(aThread);
			return navigationRequest;
		}
	}
}
