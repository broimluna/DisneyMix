using System;
using System.Collections.Generic;
using Disney.Mix.SDK;
using LitJson;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Avatar;
using Mix.Entitlements;
using Mix.Games;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using Mix.Games.Session;
using Mix.Games.Ui;
using Mix.Session.Extensions;
using UnityEngine;

namespace Mix.Ui
{
	public class MixGamePostItem : BaseChatItem, IGameMessageData, IGameStateMessageData, IBundleObject, IScrollItem, IScrollItemHelper
	{
		private const string POST_OBJECT = "Post";

		protected ChatThreadGameSession mChatThreadGameSession;

		protected MixGameData mGameData;

		private static GameObject prefabObjectLeft;

		private static GameObject prefabObjectRight;

		private IEntitlementGameData mEntitlement;

		private GameObject mGameChatItemChild;

		private SdkEvents eventGenerator = new SdkEvents();

		private Action cancelSnapshot;

		public string SenderId
		{
			get
			{
				return message.SenderId;
			}
		}

		public bool Sent
		{
			get
			{
				return message.Sent;
			}
		}

		public DateTime TimeSent
		{
			get
			{
				return message.TimeSent;
			}
		}

		public string Id
		{
			get
			{
				return message.Id;
			}
		}

		public bool IsMine
		{
			get
			{
				return message.IsMine();
			}
		}

		public object MixMessageType
		{
			get
			{
				return message;
			}
		}

		public string GameName
		{
			get
			{
				IGameStateMessage gameStateMessage = message as IGameStateMessage;
				return gameStateMessage.GameName;
			}
		}

		public Dictionary<string, object> State
		{
			get
			{
				IGameStateMessage gameStateMessage = message as IGameStateMessage;
				return gameStateMessage.State;
			}
		}

		public MixGamePostItem(IChatThread aThread, IChatMessage aChatMessage, ScrollView aScrollView, IGameTray aGameListener)
			: base(aThread, aChatMessage, aScrollView)
		{
			if (prefabObjectLeft == null)
			{
				prefabObjectLeft = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/GamePostItemHolderLeft");
			}
			if (prefabObjectRight == null)
			{
				prefabObjectRight = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/GamePostItemHolderRight");
			}
			GameSessionThreadParameters aThreadParameters = new GameSessionThreadParameters(aThread);
			IGameStateMessage gameStateMessage = aChatMessage as IGameStateMessage;
			mChatThreadGameSession = new ChatThreadGameSession(MonoSingleton<GameManager>.Instance, aGameListener, aThreadParameters, this);
			mGameData = JsonMapper.ToObject<MixGameData>((string)State["GameData"]);
			if (aChatMessage is IGameStateMessage)
			{
				gameStateMessage.OnStateUpdated += eventGenerator.AddEventHandler<AbstractChatThreadGameStateMessageUpdatedEventArgs>(this, OnStateUpdatedEvent);
				thread.OnGameStateMessageAdded += eventGenerator.AddEventHandler<AbstractChatThreadGameStateMessageAddedEventArgs>(this, OnGameStateMessageAddedEvent);
			}
		}

		float IScrollItemHelper.GetGameObjectHeight()
		{
			float num = ((mEntitlement == null) ? (-1f) : ((float)mEntitlement.GetPostHeight()));
			float num2 = ((thread is IOneOnOneChatThread) ? 0f : BaseChatItem.AVATAR_NAME_HEIGHT);
			return (!(num > 0f)) ? (-1f) : (num + num2);
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			base.instance = UnityEngine.Object.Instantiate((!message.IsMine()) ? prefabObjectLeft : prefabObjectRight);
			SetupOffline("Content/Holder/ResendBtn", "Content/Holder/ContextualLoader", "Content/Holder/Error", "Content/Holder/ForceUpdate");
			if (mGameData == null)
			{
				ErrorObject.SetActive(true);
			}
			else
			{
				mEntitlement = Singleton<EntitlementsManager>.Instance.GetGameData(mGameData.Entitlement);
				if (mEntitlement == null)
				{
					if (!aGenerateForHeightOnly)
					{
						OnForceUpdateDetected(mGameData.Entitlement);
					}
				}
				else
				{
					LoadGameObject(aGenerateForHeightOnly);
				}
			}
			SetupHeight();
			return base.instance;
		}

		void IScrollItem.Destroy()
		{
			OnDestroy();
			if (eventGenerator != null && mChatThreadGameSession != null && mChatThreadGameSession.MessageData != null && mChatThreadGameSession.MessageData.MixMessageType is IGameStateMessage)
			{
				(mChatThreadGameSession.MessageData.MixMessageType as IGameStateMessage).OnStateUpdated -= eventGenerator.GetEventHandler<AbstractChatThreadGameStateMessageUpdatedEventArgs>(this, OnStateUpdatedEvent);
			}
			if (MonoSingleton<AssetManager>.Instance != null && mGameChatItemChild != null && mEntitlement != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(mEntitlement.GetPost(), mGameChatItemChild);
			}
			if (cancelSnapshot != null)
			{
				cancelSnapshot();
			}
			mGameChatItemChild = null;
		}

		void IBundleObject.OnBundleAssetObject(UnityEngine.Object aGameObject, object aUserData)
		{
			if (MonoSingleton<AssetManager>.Instance == null || base.instance == null)
			{
				return;
			}
			if (mGameChatItemChild == null && mEntitlement != null)
			{
				mGameChatItemChild = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(mEntitlement.GetPost());
			}
			if (mGameChatItemChild != null && base.instance != null)
			{
				if (aUserData != null && aUserData.ToString() == "Post")
				{
					SetupGameChatItem();
				}
			}
			else if (mGameChatItemChild == null && base.instance != null && message.IsMine() && ErrorObject != null)
			{
				ErrorObject.SetActive(true);
			}
			SetupHeight();
		}

		protected void OnStateUpdatedEvent(object sender, AbstractChatThreadGameStateMessageUpdatedEventArgs eventArgs)
		{
			if (!base.instance.IsNullOrDisposed())
			{
				mChatThreadGameSession.MessageData = this;
				SetupGameChatItem();
			}
		}

		protected void OnGameStateMessageAddedEvent(object sender, AbstractChatThreadGameStateMessageAddedEventArgs eventArgs)
		{
			if (this != null && !base.instance.IsNullOrDisposed() && !(mGameChatItemChild == null))
			{
				BaseGameChatController component = mGameChatItemChild.GetComponent<BaseGameChatController>();
				if (!(component != null))
				{
				}
			}
		}

		protected void LoadGameChatItem(bool aGenerateForHeightOnly)
		{
			if (!aGenerateForHeightOnly && mGameData != null && mEntitlement != null && !string.IsNullOrEmpty(mEntitlement.GetPost().Trim()))
			{
				if (!MonoSingleton<AssetManager>.Instance.WillBundleLoadFromWeb(mEntitlement.GetPost()))
				{
					LoaderObject.SetActive(false);
				}
				MonoSingleton<AssetManager>.Instance.LoadABundle(this, mEntitlement.GetPost(), "Post", string.Empty, false, false, true);
			}
		}

		protected void SetupHeight()
		{
			float num = ((mEntitlement == null) ? (-1f) : ((float)mEntitlement.GetPostHeight()));
			if (num > 0f)
			{
				RectTransform component = base.instance.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(component.sizeDelta.x, num + avatarNameHeight);
			}
		}

		protected override void LoadObject()
		{
			if (mGameData != null && Singleton<EntitlementsManager>.Instance != null)
			{
				mEntitlement = Singleton<EntitlementsManager>.Instance.GetGameData(mGameData.Entitlement);
				LoadGameObject(false);
			}
		}

		private void LoadGameObject(bool aGenerateForHeightOnly)
		{
			if (!aGenerateForHeightOnly)
			{
				if (!(thread is IOneOnOneChatThread))
				{
					SkinAvatar();
				}
				LoadGameChatItem(aGenerateForHeightOnly);
			}
			SetupHeight();
		}

		private void SkinAvatar()
		{
			GameChatItem component = base.instance.GetComponent<GameChatItem>();
			IAvatarHolder avatarHolderFromId = thread.GetAvatarHolderFromId(message.SenderId);
			if (avatarHolderFromId != null && !component.IsNullOrDisposed())
			{
				component.AvatarContainer.SetActive(true);
				MonoSingleton<AvatarManager>.Instance.RenderAvatarSnapshotWithCancel(avatarHolderFromId, component.AvatarContainer, "ImageTarget", cancelSnapshot);
			}
		}

		private void ShowAssetError()
		{
			if (message.IsMine())
			{
				ErrorObject.SetActive(true);
			}
		}

		private void SetupGameChatItem()
		{
			if (base.instance == null)
			{
				ShowAssetError();
				return;
			}
			GameChatItem component = base.instance.GetComponent<GameChatItem>();
			if (component == null)
			{
				ShowAssetError();
			}
			else if (mGameChatItemChild != null)
			{
				component.ContextualLoader.SetActive(false);
				mGameChatItemChild.transform.SetParent(component.ItemTarget, false);
				mGameChatItemChild.transform.SetAsFirstSibling();
				mGameChatItemChild.SetActive(true);
				BaseGameChatController component2 = mGameChatItemChild.GetComponent<BaseGameChatController>();
				if (component2 != null)
				{
					component2.Initialize(mChatThreadGameSession, mEntitlement, component);
				}
			}
		}

		public override void UpdateClientMessage(IChatMessage aMessage)
		{
			message = aMessage;
			if (!base.instance.IsNullOrDisposed())
			{
				if (aMessage.IsMine())
				{
					ResendObject.SetActive(FailedToSend);
				}
				SetupGameChatItem();
			}
		}

		protected override void OnResendClicked()
		{
			ResendObject.SetActive(false);
			ResendFailedMessage(message);
		}

		public string GetJson()
		{
			Dictionary<string, object> state = (message as IGameStateMessage).State;
			return (string)state["GameData"];
		}
	}
}
