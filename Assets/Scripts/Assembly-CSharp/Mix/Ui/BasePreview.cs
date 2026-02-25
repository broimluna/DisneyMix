using System;
using System.Collections.Generic;
using System.Linq;
using Avatar;
using Disney.Mix.SDK;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Avatar;
using Mix.Data;
using Mix.GagManagement;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class BasePreview : IBundleObject
	{
		private Transform holder;

		private BaseContentData entitlement;

		private GameObject loader;

		private bool removed;

		private GameObject gagObject;

		private GameObject avatarLeft;

		private GameObject avatarRight;

		private GameObject placeholderLeft;

		private GameObject placeholderRight;

		private int numAvatarsSkinned;

		private Vector3 avatarLeftPosition;

		private Vector3 avatarRightPosition;

		private float placeholderLeftPosition;

		private float placeholderRightPosition;

		private Vector3 offscreenPosition;

		private bool inGroupThread;

		private bool isGagLoaded;

		private bool isGagPlaying;

		public GameObject ActiveGameObject;

		public BasePreview(Transform aHolder, BaseContentData aEntitlement, IChatThread aChatThread)
		{
			holder = aHolder;
			entitlement = aEntitlement;
			string aUrl = aEntitlement.GetThumb();
			if (entitlement is Sticker)
			{
				aUrl = entitlement.GetHd();
			}
			else if (entitlement is Gag)
			{
				aUrl = ((Gag)entitlement).GetHd();
				gagObject = null;
				numAvatarsSkinned = 0;
				MonoSingleton<GagManager>.Instance.PreventGags();
				avatarLeft = GameObject.Find("AvatarHeadLeft");
				avatarRight = GameObject.Find("AvatarHeadRight");
				if (aChatThread == null)
				{
					return;
				}
				inGroupThread = aChatThread is IGroupChatThread;
				if (!inGroupThread && (avatarLeft == null || avatarRight == null || holder.parent == null))
				{
					Log.Exception((!(holder.parent == null)) ? "Missing avatars" : "Missing parent");
					return;
				}
				placeholderLeft = holder.parent.Find("AvatarHeadsPlacement/Holder/PlaceholderAvatarLeft").gameObject;
				placeholderRight = holder.parent.Find("AvatarHeadsPlacement/Holder/PlaceholderAvatarRight").gameObject;
				offscreenPosition = new Vector3(1000f, 1000f, 1000f);
				if (inGroupThread)
				{
					avatarLeftPosition = placeholderLeft.transform.position;
					avatarRightPosition = placeholderRight.transform.position;
					avatarLeftPosition.y = holder.position.y * 0.2f;
					avatarRightPosition.y = avatarLeftPosition.y;
				}
				else
				{
					avatarLeftPosition = avatarLeft.transform.position;
					avatarRightPosition = avatarRight.transform.position;
					avatarLeft.transform.position = offscreenPosition;
					avatarRight.transform.position = offscreenPosition;
				}
				placeholderLeftPosition = placeholderLeft.transform.position.z;
				placeholderRightPosition = placeholderRight.transform.position.z;
				Transform transform = placeholderLeft.transform.Find("AvatarHeadLeft/cube_rig");
				Transform transform2 = placeholderRight.transform.Find("AvatarHeadRight/cube_rig");
				if (!placeholderLeft.activeSelf && transform != null)
				{
					if (inGroupThread)
					{
						int num = aChatThread.RemoteMembers.Count();
						IAvatarHolder avatarHolder = new AvatarHolder(MixSession.User);
						if (num > 0)
						{
							IEnumerable<IRemoteChatMember> remoteMembers = aChatThread.RemoteMembers;
							System.Random random = new System.Random();
							avatarHolder = new AvatarHolder(remoteMembers.ElementAt(random.Next(num)));
						}
						MonoSingleton<AvatarManager>.Instance.SkinAvatar(transform.gameObject, avatarHolder.Avatar, (AvatarFlags)0, SkinningCallback);
					}
					else if (aChatThread is IOneOnOneChatThread)
					{
						IOneOnOneChatThread thread = aChatThread as IOneOnOneChatThread;
						MonoSingleton<AvatarManager>.Instance.SkinAvatar(transform.gameObject, thread.GetOtherAvatarHolder().Avatar, (AvatarFlags)0, SkinningCallback);
					}
				}
				else
				{
					numAvatarsSkinned++;
				}
				if (!placeholderRight.activeSelf && transform2 != null)
				{
					MonoSingleton<AvatarManager>.Instance.SkinAvatar(transform2.gameObject, MixSession.User.Avatar, (AvatarFlags)0, SkinningCallback);
				}
				else
				{
					numAvatarsSkinned++;
				}
			}
			GameObject original = Resources.Load<GameObject>("Prefabs/Ui/loaders/ContextualLoader");
			loader = UnityEngine.Object.Instantiate(original);
			loader.transform.SetParent(holder, false);
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, aUrl, null, string.Empty);
		}

		void IBundleObject.OnBundleAssetObject(UnityEngine.Object aGameObject, object aUserData)
		{
			UnityEngine.Object.Destroy(loader);
			if (removed || !(aGameObject is GameObject))
			{
				return;
			}
			if (entitlement is Sticker)
			{
				ActiveGameObject = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(entitlement.GetHd());
				UpdateLayers(ActiveGameObject);
				Rect rect = ActiveGameObject.GetComponent<RectTransform>().rect;
				Rect rect2 = holder.GetComponent<RectTransform>().rect;
				float num = ((!(rect2.width - rect.width < rect2.height - rect.height)) ? (rect2.height / rect.height) : (rect2.width / rect.width));
				if (num < 1f)
				{
					ActiveGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width * num, rect.height * num);
				}
				ActiveGameObject.transform.SetParent(holder, false);
				if (ActiveGameObject.GetComponent<Button>() != null)
				{
					ActiveGameObject.GetComponent<Button>().onClick.Invoke();
				}
			}
			else
			{
				MonoSingleton<AssetManager>.Instance.IncrementBundleReferenceCount(entitlement.GetHd());
				gagObject = aGameObject as GameObject;
				isGagLoaded = true;
				UpdateLayers(gagObject);
				PlayGag();
			}
		}

		public void Remove()
		{
			removed = true;
			if (MonoSingleton<AssetManager>.Instance.IsNullOrDisposed())
			{
				return;
			}
			MonoSingleton<AssetManager>.Instance.CancelBundles(this);
			if (entitlement is Gag)
			{
				if (!placeholderLeft.IsNullOrDisposed() && !placeholderRight.IsNullOrDisposed())
				{
					placeholderLeft.transform.position = new Vector3(placeholderLeft.transform.position.x, offscreenPosition.y, placeholderLeftPosition);
					placeholderRight.transform.position = new Vector3(placeholderRight.transform.position.x, offscreenPosition.y, placeholderRightPosition);
					placeholderLeft.SetActive(false);
					placeholderRight.SetActive(false);
				}
				if (!inGroupThread)
				{
					if (!avatarLeft.IsNullOrDisposed())
					{
						avatarLeft.transform.position = avatarLeftPosition;
					}
					if (!avatarRight.IsNullOrDisposed())
					{
						avatarRight.transform.position = avatarRightPosition;
					}
				}
				if (isGagLoaded && !isGagPlaying)
				{
					MonoSingleton<AssetManager>.Instance.DecrementBundleReferenceCount(entitlement.GetHd());
				}
				MonoSingleton<GagManager>.Instance.ClearGags();
			}
			else if (entitlement is Sticker && !ActiveGameObject.IsNullOrDisposed() && entitlement != null)
			{
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(entitlement.GetHd(), ActiveGameObject);
			}
		}

		private void UpdateLayers(GameObject aItem)
		{
			aItem.layer = LayerMask.NameToLayer("UI Foreground");
			if (aItem.transform.childCount > 0)
			{
				for (int i = 0; i < aItem.transform.childCount; i++)
				{
					UpdateLayers(aItem.transform.GetChild(i).gameObject);
				}
			}
		}

		public void SkinningCallback(bool success, string dnaSha)
		{
			if (!holder.IsNullOrDisposed() && !(MonoSingleton<GagManager>.Instance == null) && !(MonoSingleton<AssetManager>.Instance == null))
			{
				numAvatarsSkinned++;
				PlayGag();
			}
		}

		public void PlayGag()
		{
			if (numAvatarsSkinned >= 2 && gagObject != null)
			{
				if (placeholderLeft != null && !placeholderLeft.Equals(null) && placeholderRight != null && !placeholderRight.Equals(null))
				{
					isGagPlaying = true;
					placeholderLeft.SetActive(true);
					placeholderRight.SetActive(true);
					placeholderLeft.transform.position = new Vector3(avatarLeftPosition.x, avatarLeftPosition.y, placeholderLeftPosition);
					placeholderRight.transform.position = new Vector3(avatarRightPosition.x, avatarRightPosition.y, placeholderRightPosition);
					MonoSingleton<GagManager>.Instance.FlipAvatarGeo(placeholderRight.transform.Find("AvatarHeadRight").gameObject);
					MonoSingleton<GagManager>.Instance.PlayGag(gagObject, entitlement.GetHd(), GagManager.SenderPosition.RIGHT, "sender", "receiver", true, placeholderLeft, placeholderRight, holder.parent.gameObject);
				}
				MonoSingleton<AssetManager>.Instance.DecrementBundleReferenceCount(entitlement.GetHd());
			}
		}
	}
}
