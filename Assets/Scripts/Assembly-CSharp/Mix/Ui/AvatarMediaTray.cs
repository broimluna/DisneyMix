using System.Collections.Generic;
using Mix.Data;
using UnityEngine;

namespace Mix.Ui
{
	public class AvatarMediaTray : BaseMediaTray, ISubSelector<AvatarCategoryTrayInfo>, IBaseThumb
	{
		public Texture2D missingTexture;

		public AvatarCategoryTray categorySelector;

		protected AvatarThumbQueuer thumbInstantiator;

		private int? moveDirection;

		private void Awake()
		{
			thumbInstantiator = new AvatarThumbQueuer(this);
		}

		public override void LoadContent(int aDirection)
		{
			moveDirection = aDirection;
			categorySelector.CurrentContent = categorySelector.GetContent(aDirection);
		}

		public void Populate(Transform aContentHolder, int direction)
		{
			List<BaseContentData> subContent = categorySelector.GetSubContent(direction);
			if (subContent == null)
			{
				return;
			}
			if (aContentHolder.Equals(MiddleContentHolder))
			{
				Clear();
			}
			if (aContentHolder == MiddleContentHolder)
			{
				contentDataMiddle = subContent;
				contentHolderMiddle = aContentHolder;
				stickerIconCountMiddle = 0;
				if (IsInvoking("IconLoader"))
				{
					CancelInvoke("IconLoader");
				}
				InvokeRepeating("IconLoader", 0f, 0.01f);
			}
			else if (aContentHolder == LeftContentHolder)
			{
				contentDataLeft = subContent;
				contentHolderLeft = aContentHolder;
				stickerIconCountLeft = 0;
				if (IsInvoking("IconLoaderLeft"))
				{
					CancelInvoke("IconLoaderLeft");
				}
				InvokeRepeating("IconLoaderLeft", 0f, 0.01f);
			}
			else if (aContentHolder == RightContentHolder)
			{
				contentDataRight = subContent;
				contentHolderRight = aContentHolder;
				stickerIconCountRight = 0;
				if (IsInvoking("IconLoaderRight"))
				{
					CancelInvoke("IconLoaderRight");
				}
				InvokeRepeating("IconLoaderRight", 0f, 0.01f);
			}
		}

		public override void IconLoaderRight()
		{
			if (contentDataRight == null || stickerIconCountRight >= contentDataRight.Count)
			{
				CancelInvoke("IconLoaderRight");
				return;
			}
			thumbItems.Add(new AvatarBaseGroupItem(contentHolderRight, 0, this, contentDataRight[stickerIconCountRight], missingTexture, thumbInstantiator));
			stickerIconCountRight++;
		}

		public override void IconLoaderLeft()
		{
			if (contentDataLeft == null || stickerIconCountLeft >= contentDataLeft.Count)
			{
				CancelInvoke("IconLoaderLeft");
				return;
			}
			thumbItems.Add(new AvatarBaseGroupItem(contentHolderLeft, 0, this, contentDataLeft[stickerIconCountLeft], missingTexture, thumbInstantiator));
			stickerIconCountLeft++;
		}

		public override void IconLoader()
		{
			for (int i = 0; i < 4; i++)
			{
				if (contentDataMiddle == null || stickerIconCountMiddle >= contentDataMiddle.Count)
				{
					CancelInvoke("IconLoader");
					break;
				}
				thumbItems.Add(new AvatarBaseGroupItem(contentHolderMiddle, 0, this, contentDataMiddle[stickerIconCountMiddle], missingTexture, thumbInstantiator));
				stickerIconCountMiddle++;
			}
		}

		public void SetSelected(string entitlementId)
		{
			for (int i = 0; i < thumbItems.Count; i++)
			{
				if (thumbItems[i].entitlement != null)
				{
					AvatarBaseGroupItem avatarBaseGroupItem = thumbItems[i] as AvatarBaseGroupItem;
					if (avatarBaseGroupItem != null)
					{
						avatarBaseGroupItem.SetToggleState(thumbItems[i].entitlement.GetUid() == entitlementId);
					}
				}
			}
		}

		public override void ShowPreview(Vector2 aMousePosition)
		{
		}

		public override void CancelPreview()
		{
		}

		public void OnSubSelected(AvatarCategoryTrayInfo aInfo)
		{
			int? num = moveDirection;
			if (num.HasValue)
			{
				int? num2 = moveDirection;
				if (num2.HasValue && num2.Value < 0)
				{
					MoveContent(MiddleContentHolder, RightContentHolder);
					MoveContent(LeftContentHolder, MiddleContentHolder);
					Populate(LeftContentHolder, -1);
					goto IL_010f;
				}
			}
			int? num3 = moveDirection;
			if (num3.HasValue)
			{
				int? num4 = moveDirection;
				if (num4.HasValue && num4.Value > 0)
				{
					MoveContent(MiddleContentHolder, LeftContentHolder);
					MoveContent(RightContentHolder, MiddleContentHolder);
					Populate(RightContentHolder, 1);
					goto IL_010f;
				}
			}
			categorySelector.CurrentContent = aInfo;
			Populate(MiddleContentHolder, 0);
			Populate(LeftContentHolder, -1);
			Populate(RightContentHolder, 1);
			goto IL_010f;
			IL_010f:
			listener.OnContentHolderChanged();
			moveDirection = null;
		}
	}
}
