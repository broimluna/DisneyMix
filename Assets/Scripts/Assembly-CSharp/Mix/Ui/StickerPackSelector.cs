using System.Collections.Generic;
using Mix.Data;
using Mix.DeviceDb;
using Mix.Entitlements;
using UnityEngine;

namespace Mix.Ui
{
	public class StickerPackSelector : BaseSubSelector<Sticker_Pack, BaseThumb>, IBaseThumb
	{
		private const float SELECTION_WAIT_TIME = 0.1f;

		private float currentWaitTime;

		private bool allowClick = true;

		private Sticker_Pack pendingStickerPack;

		private List<Sticker_Pack> items;

		private int packIconLoadCount;

		void IBaseThumb.OnBaseThumbLoaded(bool aError)
		{
		}

		public override void Setup(ISubSelector<Sticker_Pack> aListener)
		{
			Setup(string.Empty, aListener);
		}

		public void PackIconLoader()
		{
			for (int i = 0; i < 4; i++)
			{
				if (items == null || packIconLoadCount >= items.Count)
				{
					CancelInvoke("PackIconLoader");
					base.CurrentContent = ((base.CurrentContent == null) ? items[0] : base.CurrentContent);
					listener.OnSubSelected(base.CurrentContent);
					break;
				}
				thumbItems.Add(new BaseThumb(ContentHolder, packIconLoadCount, this, items[packIconLoadCount]));
				SetHighlightForPack(thumbItems[thumbItems.Count - 1].thumb, Singleton<EntitlementsManager>.Instance.IsStickerPackNew(items[packIconLoadCount]));
				packIconLoadCount++;
			}
		}

		public override void ClearInvokes()
		{
			if (IsInvoking("PackIconLoader"))
			{
				CancelInvoke("PackIconLoader");
			}
		}

		public void Setup(string aSelection, ISubSelector<Sticker_Pack> aListener)
		{
			listener = aListener;
			base.gameObject.SetActive(aSelection.Equals("Stickers") || aSelection.Equals("StickerPacks"));
			if (aSelection.Equals("StickerPacks"))
			{
				if (IsInvoking("PackIconLoader"))
				{
					CancelInvoke("PackIconLoader");
				}
				items = Singleton<EntitlementsManager>.Instance.GetMyStickerPacks();
				packIconLoadCount = 0;
				Clear();
				InvokeRepeating("PackIconLoader", 0.01f, 0.1f);
			}
		}

		public void Update()
		{
			if (allowClick)
			{
				return;
			}
			currentWaitTime += Time.deltaTime;
			if (currentWaitTime > 0.1f)
			{
				if (pendingStickerPack != null)
				{
					base.CurrentContent = pendingStickerPack;
					listener.OnSubSelected(base.CurrentContent);
					currentWaitTime = 0f;
					pendingStickerPack = null;
				}
				else
				{
					allowClick = true;
				}
			}
		}

		public override void ToggleHighlight(BaseThumb aThumb)
		{
			bool aState = false;
			if (aThumb.entitlement.GetNew() && !Singleton<MixDocumentCollections>.Instance.contentSeenDocumentCollectionApi.IsContentSeen(aThumb.entitlement.GetUid()))
			{
				Singleton<MixDocumentCollections>.Instance.contentSeenDocumentCollectionApi.SetContentSeen(aThumb.entitlement.GetUid());
				Singleton<EntitlementsManager>.Instance.UpdateStickerPacksNewStatus();
				Singleton<EntitlementsManager>.Instance.UpdateStickersNewStatus();
				aState = true;
			}
			SetHighlightForPack(aThumb.thumb, aState);
		}

		public override Sticker_Pack GetContent(int aPackIndex)
		{
			if (this.IsNullOrDisposed() || Singleton<EntitlementsManager>.Instance == null)
			{
				return null;
			}
			if (aPackIndex == 0)
			{
				return base.CurrentContent;
			}
			List<Sticker_Pack> myStickerPacks = Singleton<EntitlementsManager>.Instance.GetMyStickerPacks();
			if (myStickerPacks == null)
			{
				return null;
			}
			for (int i = 0; i < myStickerPacks.Count; i++)
			{
				if (base.CurrentContent != null && base.CurrentContent.Equals(myStickerPacks[i]))
				{
					if (i + aPackIndex >= 0 && i + aPackIndex <= myStickerPacks.Count - 1)
					{
						return myStickerPacks[i + aPackIndex];
					}
					return null;
				}
			}
			return null;
		}

		private void SetHighlightForPack(GameObject aItem, bool aState)
		{
			if (aItem.GetComponent<Animator>() != null)
			{
				aItem.GetComponent<Animator>().enabled = true;
				aItem.GetComponent<Animator>().Play((!aState) ? "idle" : "Highlight");
			}
		}

		public void OnBaseThumbClicked(BaseContentData aEntitlement, object aUserData)
		{
			base.CurrentContent = (Sticker_Pack)aEntitlement;
			if (allowClick)
			{
				listener.OnSubSelected(base.CurrentContent);
				currentWaitTime = 0f;
				allowClick = false;
			}
			else
			{
				pendingStickerPack = (Sticker_Pack)aEntitlement;
			}
		}
	}
}
