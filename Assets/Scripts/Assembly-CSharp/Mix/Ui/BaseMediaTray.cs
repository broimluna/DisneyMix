using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public abstract class BaseMediaTray : MonoBehaviour, IBaseThumb
	{
		public MediaScroller MediaScroller;

		public Transform LeftContentHolder;

		public Transform MiddleContentHolder;

		public Transform RightContentHolder;

		protected IChatThread thread;

		protected IMediaTray listener;

		protected List<BaseThumb> thumbItems = new List<BaseThumb>();

		protected float sendDelay = 0.4f;

		private float sendDelayTime;

		protected int stickerIconCountMiddle;

		protected Transform contentHolderMiddle;

		protected List<BaseContentData> contentDataMiddle;

		protected int stickerIconCountLeft;

		protected Transform contentHolderLeft;

		protected List<BaseContentData> contentDataLeft;

		protected int stickerIconCountRight;

		protected Transform contentHolderRight;

		protected List<BaseContentData> contentDataRight;

		void IBaseThumb.OnBaseThumbLoaded(bool aError)
		{
		}

		void IBaseThumb.OnBaseThumbClicked(BaseContentData aEntitlement, object aUserData)
		{
			if (aUserData is Dictionary<TimerStatus, string>)
			{
				Dictionary<TimerStatus, string> dictionary = aUserData as Dictionary<TimerStatus, string>;
				string value = string.Empty;
				if (dictionary.TryGetValue(TimerStatus.Running, out value))
				{
					UpdateTooltip(value);
					return;
				}
			}
			if (!MediaScroller.IsAnimating && MediaScroller.CurrentDragState == MediaScroller.DragState.Idle && sendDelayTime > sendDelay)
			{
				listener.OnEntitlementClicked(aEntitlement);
				sendDelayTime = 0f;
			}
		}

		public virtual void IconLoaderRight()
		{
			if (contentDataRight == null || stickerIconCountRight >= contentDataRight.Count)
			{
				CancelInvoke("IconLoaderRight");
				return;
			}
			thumbItems.Add(new BaseThumb(contentHolderRight, stickerIconCountRight, this, contentDataRight[stickerIconCountRight]));
			stickerIconCountRight++;
		}

		public virtual void IconLoaderLeft()
		{
			if (contentDataLeft == null || stickerIconCountLeft >= contentDataLeft.Count)
			{
				CancelInvoke("IconLoaderLeft");
				return;
			}
			thumbItems.Add(new BaseThumb(contentHolderLeft, stickerIconCountLeft, this, contentDataLeft[stickerIconCountLeft]));
			stickerIconCountLeft++;
		}

		public virtual void IconLoader()
		{
			for (int i = 0; i < 4; i++)
			{
				if (contentDataMiddle == null || stickerIconCountMiddle >= contentDataMiddle.Count)
				{
					CancelInvoke("IconLoader");
					break;
				}
				thumbItems.Add(new BaseThumb(contentHolderMiddle, stickerIconCountMiddle, this, contentDataMiddle[stickerIconCountMiddle]));
				stickerIconCountMiddle++;
			}
		}

		public virtual void Init(IMediaTray aListener, IChatThread aThread = null)
		{
			listener = aListener;
			thread = aThread;
			float num = MixConstants.CANVAS_WIDTH / MixConstants.CANVAS_HEIGHT;
			float num2 = 0.1875f;
			float num3 = num - 0.5625f;
			float t = num3 / num2;
			float x = Mathf.Lerp(10f, 50f, t);
			MiddleContentHolder.GetComponent<GridLayoutGroup>().spacing = new Vector2(x, 30f);
			LeftContentHolder.GetComponent<GridLayoutGroup>().spacing = new Vector2(x, 30f);
			RightContentHolder.GetComponent<GridLayoutGroup>().spacing = new Vector2(x, 30f);
		}

		public abstract void ShowPreview(Vector2 aMousePosition);

		public abstract void CancelPreview();

		public abstract void LoadContent(int aDirection);

		protected virtual void UpdateTooltip(string aMessage)
		{
		}

		private void OnDestroy()
		{
			Clear();
		}

		private void Update()
		{
			if (sendDelayTime < sendDelay)
			{
				sendDelayTime += Time.deltaTime;
			}
		}

		protected void Clear()
		{
			ClearContentHolder(LeftContentHolder);
			ClearContentHolder(MiddleContentHolder);
			ClearContentHolder(RightContentHolder);
			if (thumbItems != null)
			{
				thumbItems.Clear();
			}
			ClearInvokes();
		}

		protected void MoveContent(Transform aFromContent, Transform aToContent)
		{
			ClearContentHolder(aToContent);
			foreach (BaseThumb thumbItem in thumbItems)
			{
				if (thumbItem.parent != null && thumbItem.parent.Equals(aFromContent))
				{
					thumbItem.SetThumbParent(aToContent);
				}
			}
		}

		protected void ClearContentHolder(Transform aContentHolder)
		{
			if (thumbItems == null || aContentHolder == null)
			{
				return;
			}
			List<BaseThumb> list = new List<BaseThumb>();
			foreach (BaseThumb thumbItem in thumbItems)
			{
				if (thumbItem != null && thumbItem.parent != null && thumbItem.parent.Equals(aContentHolder))
				{
					thumbItem.Clean();
					list.Add(thumbItem);
				}
			}
			thumbItems.RemoveAll(list.Contains);
			for (int i = 0; i < aContentHolder.childCount; i++)
			{
				Object.Destroy(aContentHolder.GetChild(i).gameObject);
			}
		}

		public void ClearInvokes()
		{
			if (IsInvoking("IconLoader"))
			{
				CancelInvoke("IconLoader");
			}
			if (IsInvoking("IconLoaderLeft"))
			{
				CancelInvoke("IconLoaderLeft");
			}
			if (IsInvoking("IconLoaderRight"))
			{
				CancelInvoke("IconLoaderRight");
			}
		}
	}
}
