using System;
using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AvatarIconizer : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
	{
		public AvatarDragger avatarDragger;

		public GameObject avatarToHide;

		public float yThreshold = 96f;

		public Animator AvatarAnimationController;

		public AvatarSpinner AvatarSpinner;

		public Image AvatarGeoImage;

		private bool pendingAnim;

		private AnimationEvents avatarAnimationEvents;

		private bool avatarDragging;

		private bool forceCleanup;

		private PointerEventData lastData;

		private IAvatar dna;

		private Sprite avatarSprite;

		private Action cancel;

		private void Start()
		{
			avatarAnimationEvents = AvatarAnimationController.GetComponent<AnimationEvents>();
		}

		private void Update()
		{
		}

		private void OnDestroy()
		{
			if (cancel != null)
			{
				cancel();
			}
		}

		public void SetCurrentDna(IAvatar aDna)
		{
			dna = aDna;
			if (avatarDragger.IsNullOrDisposed())
			{
				return;
			}
			Image image = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(aDna)) ? avatarDragger.GetComponent<Image>() : AvatarGeoImage);
			int size = ((!image.IsNullOrDisposed()) ? ((int)image.GetComponent<RectTransform>().rect.height) : 0);
			SnapshotCallback snapshotCallback = delegate(bool success, Sprite sprite)
			{
				cancel = null;
				if (success)
				{
					avatarSprite = sprite;
					avatarDragger.UpdateDragIcon(avatarSprite);
				}
			};
			cancel = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(dna, (AvatarFlags)0, size, snapshotCallback);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			lastData = eventData;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			lastData = null;
			if (forceCleanup)
			{
				CleanupDragging(eventData);
			}
		}

		public void CleanupDragging(PointerEventData eventData)
		{
			if (!pendingAnim || eventData == null)
			{
				avatarDragger.OnEndDrag(-1, eventData);
				avatarToHide.SetActive(true);
				AvatarSpinner.AvatarFaceForward(true);
				avatarDragging = false;
				pendingAnim = false;
				AvatarAnimationController.Play("EditOutfit_GrowHead");
			}
		}

		private void BeginDragging(PointerEventData eventData)
		{
			if (avatarDragger.IsBusy())
			{
				return;
			}
			pendingAnim = true;
			forceCleanup = true;
			OnAnimationEndEvent endCallback = null;
			endCallback = delegate
			{
				if (pendingAnim)
				{
					pendingAnim = false;
					avatarAnimationEvents.OnAnimationEnd -= endCallback;
					if (lastData != null)
					{
						avatarToHide.SetActive(false);
						avatarDragger.OnBeginDrag(-1, dna, avatarSprite, lastData);
						avatarDragging = true;
					}
					else
					{
						CleanupDragging(eventData);
					}
				}
			};
			avatarAnimationEvents.OnAnimationEnd += endCallback;
			AvatarAnimationController.Play("EditOutfit_ShrinkHead");
		}

		public void OnDrag(PointerEventData eventData)
		{
			float num = 640f / (float)Screen.height;
			float num2 = yThreshold / num;
			if (!pendingAnim && !avatarDragging && Math.Abs(eventData.position.y - eventData.pressPosition.y) >= num2)
			{
				BeginDragging(eventData);
			}
			if (pendingAnim)
			{
				lastData = eventData;
			}
			else if (avatarDragging && avatarSprite != null)
			{
				avatarDragger.OnDrag(-1, eventData);
				forceCleanup = false;
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (avatarDragging)
			{
				CleanupDragging(eventData);
			}
		}
	}
}
