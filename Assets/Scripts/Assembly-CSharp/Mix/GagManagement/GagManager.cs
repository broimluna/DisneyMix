using System;
using System.Collections.Generic;
using Avatar;
using Disney.Mix.SDK;
using Mix.Assets;
using Mix.Avatar;
using Mix.Session;
using UnityEngine;

namespace Mix.GagManagement
{
	public class GagManager : MonoSingleton<GagManager>
	{
		public enum SenderPosition
		{
			LEFT = 0,
			RIGHT = 1
		}

		public class GagQueueItem
		{
			public GameObject gagObject;

			public string gagURL;

			public SenderPosition senderPosition;

			public string senderAnimName;

			public string receiverAnimName;

			public bool flipReceiver;

			public bool isGroupGag;

			public IAvatar senderAvatarInfo;

			public IAvatar receiverAvatarInfo;
		}

		public const string CHAT_SCREEN_LEFT_AVATAR_NAME = "PlaceholderAvatarLeft";

		public const string CHAT_SCREEN_RIGHT_AVATAR_NAME = "PlaceholderAvatarRight";

		public const string LEFT_AVATAR_NAME = "AvatarHeadLeft";

		public const string RIGHT_AVATAR_NAME = "AvatarHeadRight";

		public const string BG_HOLDER = "3D_BG_Holder";

		public const string BG_CHAT_SCREEN = "AvatarHeads";

		private const float ANIM_OFFSET = 3f;

		private const float AVATAR_SCALE = 0.01f;

		public const string FG_GROUP_CHAT_SCREEN = "UI_FG_ChatScreen";

		public const string BG_GROUP_CHAT_SCREEN = "UI_BG_ChatScreen";

		public const string GAG_STAGE = "GagStage";

		public const string GAG_CHAT_MASK = "ChatMask";

		public const string MEDIA_INPUT = "MediaInput";

		public const string GROUP_CHAT_BEVEL = "GroupChatBevel";

		public const string AVATAR_HEAD = "cube_rig";

		private List<GagQueueItem> gagQueue = new List<GagQueueItem>();

		private GameObject LeftAvatar;

		private GameObject RightAvatar;

		private GameObject ActiveGag;

		private string ActiveGagURL = string.Empty;

		private GagContainer ActiveGagContainer;

		public SenderPosition ActiveSenderPosition;

		private bool preventGags;

		private Transform gagStage;

		private GameObject groupChatBevel;

		private int numAvatarsSkinned;

		private GagQueueItem pendingGag;

		public float GetAvatarScale()
		{
			GameObject gameObject = GameObject.Find("3D_BG_Holder");
			return gameObject.transform.localScale.x / 0.01f;
		}

		public float GetAvatarOffset()
		{
			float num = 1.5f * GetAvatarScale();
			if (ActiveSenderPosition == SenderPosition.RIGHT)
			{
				num = 0f - num;
			}
			return num;
		}

		public float GetObjectOffset()
		{
			float avatarOffset = GetAvatarOffset();
			float num = RightAvatar.transform.position.x - LeftAvatar.transform.position.x - Mathf.Abs(avatarOffset) * 2f;
			if (ActiveSenderPosition == SenderPosition.RIGHT)
			{
				num = 0f - num;
			}
			return num;
		}

		private void SetLeftAndRightAvatars(GameObject leftAvatar, GameObject rightAvatar, Transform chatScreen)
		{
			Transform transform = null;
			Transform transform2 = null;
			if (leftAvatar != null)
			{
				transform = leftAvatar.transform.Find("AvatarHeadLeft");
			}
			else if (chatScreen != null)
			{
				transform = chatScreen.Find("AvatarHeadLeft");
			}
			if (transform != null)
			{
				LeftAvatar = transform.gameObject;
			}
			if (rightAvatar != null)
			{
				transform2 = rightAvatar.transform.Find("AvatarHeadRight");
			}
			else if (chatScreen != null)
			{
				transform2 = chatScreen.Find("AvatarHeadRight");
			}
			if (transform2 != null)
			{
				RightAvatar = transform2.gameObject;
			}
		}

		public bool IsGagPlaying()
		{
			return ActiveGag != null;
		}

		public void PlayGag(GameObject aGagObject, string aGagURL, SenderPosition aSenderPosition, string aSenderAnimName, string aReceiverAnimName = null, bool aFlipReceiver = false, GameObject aLeftAvatar = null, GameObject aRightAvatar = null, GameObject aGagParent = null, bool aUseGameObject = false)
		{
			if ((aGagParent == null && preventGags) || MonoSingleton<AssetManager>.Instance == null || (aLeftAvatar == null && GameObject.Find("AvatarHeads") == null))
			{
				return;
			}
			Transform transform = null;
			if (aLeftAvatar == null)
			{
				transform = GameObject.Find("AvatarHeads").transform;
			}
			SetLeftAndRightAvatars(aLeftAvatar, aRightAvatar, transform);
			if (ActiveGag == null)
			{
				if (aUseGameObject)
				{
					ActiveGag = UnityEngine.Object.Instantiate(aGagObject);
				}
				else
				{
					ActiveGag = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(aGagURL);
				}
				ActiveGagURL = aGagURL;
				if (aGagParent == null)
				{
					ActiveGag.transform.parent = transform.transform;
					Util.SetLayerRecursively(ActiveGag, LayerMask.NameToLayer("3D"));
				}
				else
				{
					ActiveGag.transform.parent = aGagParent.transform;
					Util.SetLayerRecursively(ActiveGag, LayerMask.NameToLayer("3D Foreground"));
				}
				ActiveGagContainer = ActiveGag.GetComponent<GagContainer>();
				ActiveSenderPosition = aSenderPosition;
				if (ActiveGagContainer == null)
				{
					DestroyGag();
				}
				else if (aSenderPosition == SenderPosition.LEFT)
				{
					ActiveGagContainer.Play(LeftAvatar, RightAvatar, aSenderAnimName, aReceiverAnimName, aFlipReceiver);
				}
				else
				{
					ActiveGagContainer.Play(RightAvatar, LeftAvatar, aSenderAnimName, aReceiverAnimName, aFlipReceiver);
				}
			}
			else if (MonoSingleton<AssetManager>.Instance != null && gagQueue != null)
			{
				MonoSingleton<AssetManager>.Instance.IncrementBundleReferenceCount(aGagURL);
				GagQueueItem gagQueueItem = new GagQueueItem();
				gagQueueItem.isGroupGag = false;
				gagQueueItem.gagObject = aGagObject;
				gagQueueItem.gagURL = aGagURL;
				gagQueueItem.senderPosition = aSenderPosition;
				gagQueueItem.senderAnimName = aSenderAnimName;
				gagQueueItem.receiverAnimName = aReceiverAnimName;
				gagQueueItem.flipReceiver = aFlipReceiver;
				gagQueue.Add(gagQueueItem);
			}
		}

		public void PlayGroupGag(GameObject aGagObject, string aGagURL, SenderPosition aDirection, IAvatar aSenderAvatarInfo, IAvatar aReceiverAvatarInfo, string aSenderAnimName, string aReceiverAnimName = null, bool aFlipReceiver = false)
		{
			if (preventGags)
			{
				return;
			}
			gagStage = GameObject.Find("UI_FG_ChatScreen").transform.Find("GagStage");
			groupChatBevel = GameObject.Find("UI_FG_ChatScreen").transform.Find("MediaInput").Find("GroupChatBevel").gameObject;
			Transform transform = gagStage.Find("PlaceholderAvatarLeft").transform;
			Transform transform2 = gagStage.Find("PlaceholderAvatarRight").transform;
			SetLeftAndRightAvatars(transform.gameObject, transform2.gameObject, gagStage);
			if (ActiveGag == null && numAvatarsSkinned == 2)
			{
				ActiveGag = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(aGagURL);
				pendingGag = null;
				Util.SetLayerRecursively(ActiveGag, LayerMask.NameToLayer("3D"));
				gagStage.gameObject.SetActive(true);
				groupChatBevel.SetActive(false);
				ActiveGag.transform.parent = gagStage.transform;
				ActiveGagContainer = ActiveGag.GetComponent<GagContainer>();
				ActiveSenderPosition = aDirection;
				ActiveGagURL = aGagURL;
				if (ActiveGagContainer == null)
				{
					DestroyGag();
				}
				else if (aDirection == SenderPosition.LEFT)
				{
					ActiveGagContainer.Play(LeftAvatar, RightAvatar, aSenderAnimName, aReceiverAnimName, aFlipReceiver);
				}
				else
				{
					ActiveGagContainer.Play(RightAvatar, LeftAvatar, aSenderAnimName, aReceiverAnimName, aFlipReceiver);
				}
				return;
			}
			MonoSingleton<AssetManager>.Instance.IncrementBundleReferenceCount(aGagURL);
			GagQueueItem gagQueueItem = new GagQueueItem();
			gagQueueItem.isGroupGag = true;
			gagQueueItem.senderAvatarInfo = aSenderAvatarInfo;
			gagQueueItem.receiverAvatarInfo = aReceiverAvatarInfo;
			gagQueueItem.gagObject = aGagObject;
			gagQueueItem.gagURL = aGagURL;
			gagQueueItem.senderPosition = aDirection;
			gagQueueItem.senderAnimName = aSenderAnimName;
			gagQueueItem.receiverAnimName = aReceiverAnimName;
			gagQueueItem.flipReceiver = aFlipReceiver;
			if (ActiveGag != null || pendingGag != null)
			{
				gagQueue.Add(gagQueueItem);
				return;
			}
			pendingGag = gagQueueItem;
			Transform transform3 = LeftAvatar.transform.Find("cube_rig");
			Transform transform4 = RightAvatar.transform.Find("cube_rig");
			MonoSingleton<AvatarManager>.Instance.ResetAvatarHead(transform3.gameObject);
			MonoSingleton<AvatarManager>.Instance.ResetAvatarHead(transform4.gameObject);
			if (aDirection == SenderPosition.LEFT)
			{
				MonoSingleton<AvatarManager>.Instance.SkinAvatar(transform3.gameObject, aSenderAvatarInfo, (AvatarFlags)0, ObjectSkinned);
				MonoSingleton<AvatarManager>.Instance.SkinAvatar(transform4.gameObject, aReceiverAvatarInfo, (AvatarFlags)0, ObjectSkinned);
			}
			else
			{
				MonoSingleton<AvatarManager>.Instance.SkinAvatar(transform4.gameObject, aSenderAvatarInfo, (AvatarFlags)0, ObjectSkinned);
				MonoSingleton<AvatarManager>.Instance.SkinAvatar(transform3.gameObject, aReceiverAvatarInfo, (AvatarFlags)0, ObjectSkinned);
			}
		}

		public void ObjectSkinned(bool success, string dnaSha)
		{
			numAvatarsSkinned++;
			if (numAvatarsSkinned == 2)
			{
				FlipAvatarGeo(RightAvatar);
				string gagURL = pendingGag.gagURL;
				PlayGroupGag(pendingGag.gagObject, pendingGag.gagURL, pendingGag.senderPosition, pendingGag.senderAvatarInfo, pendingGag.receiverAvatarInfo, pendingGag.senderAnimName, pendingGag.receiverAnimName, pendingGag.flipReceiver);
				MonoSingleton<AssetManager>.Instance.DecrementBundleReferenceCount(gagURL);
			}
		}

		public void DestroyGag()
		{
			numAvatarsSkinned = 0;
			if (ActiveGag != null)
			{
				if (MonoSingleton<AssetManager>.Instance != null && !string.IsNullOrEmpty(ActiveGagURL))
				{
					MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(ActiveGagURL, ActiveGag);
				}
				ActiveGag = null;
			}
			if (ActiveGagContainer != null)
			{
				ActiveGagContainer.Destroy();
				ActiveGagContainer = null;
			}
			if (gagStage != null)
			{
				gagStage.gameObject.SetActive(false);
			}
			if (groupChatBevel != null)
			{
				groupChatBevel.SetActive(true);
			}
			if (gagQueue.Count > 0)
			{
				GagQueueItem gagQueueItem = gagQueue[0];
				if (gagQueueItem.isGroupGag)
				{
					PlayGroupGag(gagQueueItem.gagObject, gagQueueItem.gagURL, gagQueueItem.senderPosition, gagQueueItem.senderAvatarInfo, gagQueueItem.receiverAvatarInfo, gagQueueItem.senderAnimName, gagQueueItem.receiverAnimName, gagQueueItem.flipReceiver);
				}
				else
				{
					PlayGag(gagQueueItem.gagObject, gagQueueItem.gagURL, gagQueueItem.senderPosition, gagQueueItem.senderAnimName, gagQueueItem.receiverAnimName, gagQueueItem.flipReceiver);
				}
				MonoSingleton<AssetManager>.Instance.DecrementBundleReferenceCount(gagQueueItem.gagURL);
				gagQueue.RemoveAt(0);
			}
		}

		public void PreventGags(bool aClear = true)
		{
			if (aClear)
			{
				ClearGags();
			}
			preventGags = true;
		}

		public void ClearGags()
		{
			foreach (GagQueueItem item in gagQueue)
			{
				MonoSingleton<AssetManager>.Instance.DecrementBundleReferenceCount(item.gagURL);
			}
			gagQueue.Clear();
			DestroyGag();
			preventGags = false;
		}

		public void FlipAvatarGeo(GameObject go)
		{
			if (go.IsNullOrDisposed())
			{
				return;
			}
			Transform transform = go.transform.Find("cube_rig");
			if (!(transform != null))
			{
				return;
			}
			GameObject gameObject = transform.gameObject;
			Transform transform2 = gameObject.transform.Find("grp_offset/def_front");
			if (transform2 != null && transform2.childCount > 0)
			{
				Transform child = transform2.GetChild(0);
				if (child != null)
				{
					Vector3 localScale = child.localScale;
					if (localScale.x >= 0f)
					{
						child.localScale = new Vector3(localScale.x * -1f, 1f, 1f);
					}
				}
			}
			Transform transform3 = gameObject.transform.Find("grp_offset/def_hatBase");
			if (transform3 != null && transform3.childCount > 0)
			{
				Transform child2 = transform3.GetChild(0);
				if (child2 != null)
				{
					Vector3 localScale2 = child2.localScale;
					if (localScale2.x >= 0f)
					{
						child2.localScale = new Vector3(localScale2.x * -1f, 1f, 1f);
					}
				}
			}
			Transform transform4 = gameObject.transform.Find("grp_offset/face_mouth");
			if (!(transform4 != null))
			{
				return;
			}
			GameObject gameObject2 = transform4.gameObject;
			if (gameObject2.IsNullOrDisposed())
			{
				return;
			}
			SkinnedMeshRenderer component = gameObject2.GetComponent<SkinnedMeshRenderer>();
			if (!component.IsNullOrDisposed())
			{
				float blendShapeWeight = component.GetBlendShapeWeight(0);
				if (Math.Abs(blendShapeWeight - ((float)MixSession.User.Avatar.Mouth.XOffset + 50f)) < 0.001f)
				{
					component.SetBlendShapeWeight(0, 100f - blendShapeWeight);
				}
			}
		}
	}
}
