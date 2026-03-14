using Disney.Mix.SDK;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Data;
using Mix.Entitlements;
using Mix.GagManagement;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class MixGagController : MonoBehaviour, IBundleObject
	{
		public MixGagItem chatItem;

		private Gag gag;

		private bool playGag;

		private bool replayGag;

		private bool replaceImage = true;

		private bool checkForCheck = true;

		private Object ActiveObject;

		void IBundleObject.OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			if (this.IsNullOrDisposed() || chatItem == null)
			{
				return;
			}
			if (aGameObject == null)
			{
				chatItem.ErrorObject.SetActive(true);
			}
			else if (aUserData != null)
			{
				GameObject gameObject = Util.FindGameObjectByName(base.gameObject, "GagImage");
				if (!(gameObject != null))
				{
					return;
				}
				gameObject.SetActive(true);
				ActiveObject = MonoSingleton<AssetManager>.Instance.GetBundleInstance(gag.GetThumb());
				if (ActiveObject is Texture2D)
				{
					Texture2D texture2D = (Texture2D)ActiveObject;
					Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
					gameObject.GetComponent<Image>().sprite = sprite;
					return;
				}
				GameObject gameObject2 = (GameObject)ActiveObject;
				if (gameObject2 != null)
				{
					gameObject.GetComponent<Image>().sprite = gameObject2.GetComponent<Image>().sprite;
				}
			}
			else
			{
				GagManager.SenderPosition senderPosition = GagManager.SenderPosition.LEFT;
				if (chatItem.message != null && chatItem.message.IsMine())
				{
					senderPosition = GagManager.SenderPosition.RIGHT;
				}
				else if (!chatItem.hasBeenSeen)
				{
					chatItem.hasBeenSeen = true;
					replaceImage = true;
				}
				if (chatItem.thread is IGroupChatThread)
				{
					IGagMessage gagMessage = chatItem.message as IGagMessage;
					IAvatarHolder avatarHolderFromId = chatItem.thread.GetAvatarHolderFromId(gagMessage.SenderId);
					IAvatarHolder avatarHolderFromId2 = chatItem.thread.GetAvatarHolderFromId(gagMessage.TargetUserId);
					MonoSingleton<GagManager>.Instance.PlayGroupGag((GameObject)aGameObject, gag.GetHd(), senderPosition, avatarHolderFromId.Avatar, avatarHolderFromId2.Avatar, "sender", "receiver", true);
				}
				else
				{
					MonoSingleton<GagManager>.Instance.PlayGag((GameObject)aGameObject, gag.GetHd(), senderPosition, "sender", "receiver", true);
				}
			}
		}

		private void Update()
		{
			if (gag == null && chatItem != null)
			{
				gag = Singleton<EntitlementsManager>.Instance.GetGagData(((IGagMessage)chatItem.message).ContentId);
				if (replayGag)
				{
					Analytics.LogReplayGag(chatItem, gag.GetName());
					replayGag = false;
				}
			}
			if (gag == null)
			{
				return;
			}
			if (replaceImage)
			{
				if ((chatItem.message != null && chatItem.message.IsMine()) || chatItem.hasBeenSeen || gag.IsAutoPlay())
				{
					MonoSingleton<AssetManager>.Instance.LoadABundle(this, gag.GetThumb(), new Object(), string.Empty, false, false, true);
				}
				else
				{
					GameObject gameObject = Util.FindGameObjectByName(base.gameObject, "GagImage");
					if (gameObject != null)
					{
						gameObject.SetActive(true);
					}
				}
				replaceImage = false;
			}
			if (checkForCheck && chatItem.message != null && chatItem.message.IsMine() && chatItem.hasBeenSeen)
			{
				GameObject gameObject2 = Util.FindGameObjectByName(base.gameObject, "CheckIcon");
				if (gameObject2 != null)
				{
					gameObject2.SetActive(true);
				}
				checkForCheck = false;
			}
			if (playGag)
			{
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, gag.GetHd(), null, string.Empty, false, false, true);
				playGag = false;
			}
		}

		private void OnDestroy()
		{
			if (gag != null && ActiveObject != null && MonoSingleton<AssetManager>.Instance != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(gag.GetThumb(), ActiveObject);
			}
			ActiveObject = null;
		}

		public void OnPlayGag()
		{
			if (MonoSingleton<GagManager>.Instance != null && !MonoSingleton<GagManager>.Instance.IsGagPlaying())
			{
				playGag = true;
			}
			if (gag != null)
			{
				Analytics.LogReplayGag(chatItem, gag.GetName());
			}
			else
			{
				replayGag = true;
			}
		}

		public void AutoPlayGag()
		{
			playGag = true;
		}
	}
}
