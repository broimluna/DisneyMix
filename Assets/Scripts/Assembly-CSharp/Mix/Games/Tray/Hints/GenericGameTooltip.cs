using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Hints
{
	public class GenericGameTooltip : AnchorUIElement
	{
		public Action OnHideComplete = delegate
		{
		};

		public Action OnShowComplete = delegate
		{
		};

		[Header("Show Animation")]
		public float showAnimationTime = 0.3f;

		public Ease showAnimationEaseType = Ease.OutBack;

		[Header("Hide Animation")]
		public float hideAnimationTime = 0.3f;

		public Ease hideAnimationEaseType = Ease.InBack;

		[Header("Update Bounce")]
		[Range(0f, 1f)]
		public float hintUpdateBounceAmount = 0.5f;

		[Range(0f, 2f)]
		public float hintUpdateBounceTime = 1f;

		[Range(1f, 20f)]
		public int hintUpdateBounceVibrato = 10;

		[Range(0f, 2f)]
		public float hintUpdateBounceElasticity = 1f;

		[Header("Internal References")]
		public Image leftIcon;

		public Image rightIcon;

		public Text hintText;

		public RectTransform content;

		public LayoutElement textLayoutElement;

		public bool IsShowing { get; private set; }

		public bool IsTransitionComplete { get; private set; }

		public bool IsFullyHidden
		{
			get
			{
				return !IsShowing && IsTransitionComplete;
			}
		}

		public bool IsFullyShowing
		{
			get
			{
				return IsShowing && IsTransitionComplete;
			}
		}

		public string text
		{
			get
			{
				return hintText.text;
			}
			set
			{
				if (!string.Equals(hintText.text, value))
				{
					hintText.text = value;
				}
			}
		}

		private void Awake()
		{
			Hide(true);
		}

		public void SetTextWithBounce(string text, bool onlyBounceIfUpdated = true)
		{
			bool flag = false;
			if (!string.Equals(hintText.text, text))
			{
				hintText.text = text;
				flag = true;
			}
			if (IsShowing && IsTransitionComplete && (!onlyBounceIfUpdated || flag))
			{
				DoPunchRoutine();
			}
		}

		private void DoPunchRoutine()
		{
			DOTween.Complete(content);
			content.DOPunchScale(Vector3.one * hintUpdateBounceAmount, hintUpdateBounceTime, hintUpdateBounceVibrato, hintUpdateBounceElasticity);
		}

		public void Show(bool instant = false)
		{
			if (instant)
			{
				content.localScale = Vector3.one;
				IsTransitionComplete = true;
				OnShowComplete();
			}
			else
			{
				IsTransitionComplete = false;
				DOTween.Complete(content);
				content.DOScale(Vector3.one, showAnimationTime).SetEase(showAnimationEaseType).OnComplete(delegate
				{
					OnTransitionComplete();
					OnShowComplete();
				});
			}
			IsShowing = true;
		}

		private void OnTransitionComplete()
		{
			IsTransitionComplete = true;
		}

		public void Hide(bool instant = false)
		{
			if (instant)
			{
				content.localScale = Vector3.zero;
				IsTransitionComplete = true;
				OnHideComplete();
			}
			else if (IsShowing)
			{
				IsTransitionComplete = false;
				DOTween.Complete(content);
				content.DOScale(Vector3.zero, hideAnimationTime).SetEase(hideAnimationEaseType).OnComplete(delegate
				{
					OnTransitionComplete();
					OnHideComplete();
				});
			}
			IsShowing = false;
		}
	}
}
