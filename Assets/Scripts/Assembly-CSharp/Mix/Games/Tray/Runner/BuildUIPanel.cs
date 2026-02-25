using DG.Tweening;
using Mix.Games.Tray.Hints;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Runner
{
	public class BuildUIPanel : RunnerUIPanel
	{
		[Header("Internal References")]
		public BuilderDifficultyPanel difficultyPanel;

		public Button doneButton;

		[Header("UI Element Holders")]
		public Transform buildHintHolder;

		public Transform chunkCountHolder;

		[Header("Arrows")]
		public Transform horizontalHintArrows;

		public RectTransform horizontalArrowLeft;

		public RectTransform horizontalArrowRight;

		[Header("Done Button")]
		public float doneButtonScaleTime = 0.2f;

		public float doneButtonScaleDelay = 1f;

		[Space(5f)]
		public Transform verticalHintArrows;

		[Space(10f)]
		public float arrowAnimationTime = 0.3f;

		public float arrowAnimationDelay = 0.75f;

		public Ease arrowAnimationEaseType = Ease.OutBounce;

		public float arrowInitalScale = 1.5f;

		[Header("Messages")]
		public string buildHintMessage;

		private GenericGameTooltip mBuildHint;

		private ChunkController mCurrentChunk;

		private bool mShowingLeftArrow;

		private bool mShowingRightArrow;

		private bool mShowingDoneButton;

		private bool mIsWaitingToShow;

		private void Awake()
		{
			mBuildHint = InstantiateTooltip(buildHintHolder);
			mBuildHint.SetAnchor(AnchorUIElement.AnchorStyle.FILL);
			mBuildHint.text = buildHintMessage;
			verticalHintArrows.transform.localScale = Vector3.one * arrowInitalScale;
			verticalHintArrows.transform.DOScale(Vector3.one, arrowAnimationTime).SetDelay(arrowAnimationDelay).SetEase(arrowAnimationEaseType);
			horizontalHintArrows.transform.localScale = Vector3.one * arrowInitalScale;
			horizontalHintArrows.gameObject.SetActive(false);
			horizontalArrowLeft.transform.localScale = Vector3.zero;
			horizontalArrowRight.transform.localScale = Vector3.zero;
			doneButton.transform.localScale = Vector3.zero;
		}

		private void Update()
		{
			if (mBuildHint.IsShowing && runnerGame.levelBuilder.HasThePlayerSwappedAtLeastOneChunk)
			{
				mBuildHint.Hide();
				horizontalHintArrows.gameObject.SetActive(true);
				horizontalHintArrows.transform.DOScale(Vector3.one, arrowAnimationTime).SetDelay(arrowAnimationDelay).SetEase(arrowAnimationEaseType);
			}
			ChunkController currentLevelChunk = runnerGame.levelBuilder.GetCurrentLevelChunk();
			if (mCurrentChunk != currentLevelChunk)
			{
				if (currentLevelChunk != null)
				{
					UpdateDifficulty(currentLevelChunk.difficulty);
				}
				else
				{
					UpdateDifficulty(1);
				}
			}
			mCurrentChunk = currentLevelChunk;
			if (runnerGame.levelBuilder.IsFirstChunkSelected && mShowingLeftArrow)
			{
				horizontalArrowLeft.transform.DOScale(Vector3.zero, arrowAnimationTime).SetEase(Ease.InBack);
				mShowingLeftArrow = false;
			}
			else if (!runnerGame.levelBuilder.IsFirstChunkSelected && !mShowingLeftArrow)
			{
				horizontalArrowLeft.transform.DOScale(Vector3.one, arrowAnimationTime).SetEase(Ease.OutBack);
				mShowingLeftArrow = true;
			}
			if (runnerGame.levelBuilder.IsLastChunkSelected && mShowingRightArrow)
			{
				horizontalArrowRight.transform.DOScale(Vector3.zero, arrowAnimationTime).SetEase(Ease.InBack);
				mShowingRightArrow = false;
			}
			else if (!runnerGame.levelBuilder.IsLastChunkSelected && !mShowingRightArrow)
			{
				horizontalArrowRight.transform.DOScale(Vector3.one, arrowAnimationTime).SetEase(Ease.OutBack);
				mShowingRightArrow = true;
			}
			if (runnerGame.levelBuilder.AllChunksDefined() && !mShowingDoneButton)
			{
				doneButton.transform.DOScale(Vector3.one, doneButtonScaleTime).SetEase(Ease.OutBack).SetDelay(doneButtonScaleDelay);
				verticalHintArrows.transform.DOScale(Vector3.one * arrowInitalScale, arrowAnimationTime).SetEase(Ease.InQuad).SetDelay(doneButtonScaleDelay);
				mShowingDoneButton = true;
			}
			if (mIsWaitingToShow && mBuildHint.IsFullyShowing)
			{
				mIsWaitingToShow = false;
				OnShowComplete();
			}
		}

		public override void Show()
		{
			OnShow();
			mBuildHint.Show();
			mIsWaitingToShow = true;
		}

		public void UpdateDifficulty(int aDifficulty)
		{
			difficultyPanel.SetDifficulty(aDifficulty);
		}

		public void FinishBuidler()
		{
			doneButton.interactable = false;
			runnerGame.levelBuilder.Finish();
		}

		public override void Hide()
		{
			OnHide();
			OnHideComplete();
		}

		public void SelectLeftChunk()
		{
			runnerGame.levelBuilder.LeftArrowTapped();
		}

		public void SelectRightChunk()
		{
			runnerGame.levelBuilder.RightArrowTapped();
		}

		public void SwapChunkUp()
		{
			runnerGame.levelBuilder.UpArrowTapped();
		}

		public void SwapChunkDown()
		{
			runnerGame.levelBuilder.DownArrowTapped();
		}
	}
}
