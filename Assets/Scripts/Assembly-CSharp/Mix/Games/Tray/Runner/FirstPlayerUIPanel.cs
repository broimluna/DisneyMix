using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Runner
{
	public class FirstPlayerUIPanel : RunnerUIPanel
	{
		[Header("Offscreen Object Positions")]
		public Vector3 randomButtonOffscreenOffset;

		public Vector3 createButtonOffscreenOffset;

		public Button randomButton;

		public Button createButton;

		public float buttonTransitionInTime = 0.5f;

		public Ease buttonTransitionInEaseType;

		public float buttonTransitionOutTime = 0.5f;

		public Ease buttonTransitionOutEaseType;

		public float selectedButtonTransitionDelay = 0.5f;

		private Vector3 randomButtonInPosition;

		private Vector3 createButtonInPosition;

		private Vector3 randomButtonOutPosition;

		private Vector3 createButtonOutPosition;

		private float mScreenHeight;

		private Button mSelectedButton;

		private RectTransform mRandomButtonRectTransform;

		private RectTransform mCreateButtonRectTransform;

		private void Awake()
		{
			mRandomButtonRectTransform = randomButton.gameObject.GetComponent<RectTransform>();
			randomButtonInPosition = mRandomButtonRectTransform.anchoredPosition;
			randomButtonOutPosition = randomButtonInPosition + randomButtonOffscreenOffset;
			mRandomButtonRectTransform.anchoredPosition = randomButtonOutPosition;
			mCreateButtonRectTransform = createButton.gameObject.GetComponent<RectTransform>();
			createButtonInPosition = mCreateButtonRectTransform.anchoredPosition;
			createButtonOutPosition = createButtonInPosition + createButtonOffscreenOffset;
			mCreateButtonRectTransform.anchoredPosition = createButtonOutPosition;
		}

		public override void Show()
		{
			Sequence s = DOTween.Sequence();
			OnShow();
			s.Append(mRandomButtonRectTransform.DOAnchorPos(randomButtonInPosition, buttonTransitionInTime).SetEase(buttonTransitionInEaseType));
			s.Join(mCreateButtonRectTransform.DOAnchorPos(createButtonInPosition, buttonTransitionInTime).SetEase(buttonTransitionInEaseType));
			s.AppendCallback(delegate
			{
				OnShowComplete();
			});
		}

		public override void Hide()
		{
			float delay = 0f;
			float delay2 = 0f;
			if (mSelectedButton == randomButton)
			{
				delay = selectedButtonTransitionDelay;
			}
			else if (mSelectedButton == createButton)
			{
				delay2 = selectedButtonTransitionDelay;
			}
			Sequence s = DOTween.Sequence();
			s.Append(mRandomButtonRectTransform.DOAnchorPos(randomButtonOutPosition, buttonTransitionOutTime).SetEase(buttonTransitionOutEaseType).SetDelay(delay));
			s.Join(mCreateButtonRectTransform.DOAnchorPos(createButtonOutPosition, buttonTransitionOutTime).SetEase(buttonTransitionOutEaseType).SetDelay(delay2));
			s.InsertCallback(buttonTransitionOutTime + selectedButtonTransitionDelay, delegate
			{
				OnHideComplete();
			});
		}

		public void OnCreateButtonPressed()
		{
			mSelectedButton = createButton;
			runnerGame.EnterBuildMode();
			createButton.interactable = false;
			randomButton.interactable = false;
		}

		public void OnRandomButtonPressed()
		{
			mSelectedButton = randomButton;
			runnerGame.OnRandomBuildButton();
			createButton.interactable = false;
			randomButton.interactable = false;
		}
	}
}
