using System;
using DG.Tweening;
using Mix.Games.Tray.Hints;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Runner
{
	public class EndUIPanel : RunnerUIPanel
	{
		public HeartCounter heartCounterPrefab;

		[Header("UI Element Holders")]
		public Transform hintHolder;

		public Transform heartCountHolder;

		[Space(10f)]
		public Button hiddenEndButton;

		[Header("Messages")]
		public string tapToEndHint;

		[Space(10f)]
		public Text message;

		[Space(10f)]
		public string gameOverMessage;

		public string levelCompleteMessage;

		[Header("Coin Count")]
		public GameObject coinCountParent;

		public Text coinCountText;

		[Range(-90f, 90f)]
		public float badgeRotationMin;

		[Range(-90f, 90f)]
		public float badgeRotationMax;

		[Header("Background")]
		public Image background;

		[Header("Show Animation")]
		public float backgroundFadeInTime = 0.5f;

		public float backgroundFadeInDelay;

		public Color backgroundFadeInitialColor;

		public Color backgroudnFadeFinalColor;

		[Space(10f)]
		public float messageScaleInTime = 0.5f;

		public Ease messageScaleInEaseType;

		public float messageRotationRandonmess = 10f;

		[Space(10f)]
		public float finalCountScaleInDelay;

		public float finalCountScaleInTime;

		public Ease finalCountScaleInEaseType = Ease.OutBack;

		[Space(5f)]
		public float finalCountHeartUpdateBounceAmount = 0.25f;

		public float finalCountHeartUpdateBouneTime = 0.3f;

		[Space(5f)]
		public float delayBeforeShowingHint;

		[Space(10f)]
		public float delayBeforeAddingHeartsToScore = 1f;

		public float delayForHeartCounterToAppear = 0.5f;

		public float delayBetweenAddingHeartsToScore = 0.4f;

		private GenericGameTooltip mHint;

		private HeartCounter mHeartCount;

		private void Awake()
		{
			mHint = InstantiateTooltip(hintHolder);
			mHint.SetAnchor(AnchorUIElement.AnchorStyle.BOTTOM_CENTER);
			mHint.text = BaseGameController.Instance.Session.GetLocalizedString(tapToEndHint);
			mHeartCount = UnityEngine.Object.Instantiate(heartCounterPrefab);
			mHeartCount.transform.SetParent(heartCountHolder, false);
			mHeartCount.SetAnchor(AnchorUIElement.AnchorStyle.TOP_RIGHT);
		}

		private void Start()
		{
			mHeartCount.Initialize(runnerGame.maxLives);
			mHeartCount.SetHeartCount(runnerGame.lives);
			mHeartCount.Hide(true);
		}

		public void OnHiddenEndButtonPressed()
		{
			hiddenEndButton.gameObject.SetActive(false);
			runnerGame.OnPostButton();
		}

		public override void Show()
		{
			hiddenEndButton.interactable = false;
			OnShow();
			int num = runnerGame.checkpointsPassed;
			coinCountParent.transform.localScale = Vector3.zero;
			coinCountText.text = num.ToString();
			background.color = backgroundFadeInitialColor;
			message.transform.localScale = Vector3.zero;
			message.transform.localEulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0f - messageRotationRandonmess, messageRotationRandonmess));
			string messageSoundEvent;
			if (runnerGame.lives == 0)
			{
				message.text = BaseGameController.Instance.Session.GetLocalizedString(gameOverMessage);
				messageSoundEvent = "BadBadgeAppearUI";
			}
			else
			{
				message.text = BaseGameController.Instance.Session.GetLocalizedString(levelCompleteMessage);
				messageSoundEvent = "Checkpoint";
			}
			Sequence s = DOTween.Sequence();
			s.Append(message.transform.DOScale(Vector3.one, messageScaleInTime).SetEase(messageScaleInEaseType));
			s.AppendCallback(delegate
			{
				MainRunnerGame.PlaySound(messageSoundEvent, runnerGame.SOUND_PREFIX);
			});
			s.Insert(0f, background.DOColor(backgroudnFadeFinalColor, backgroundFadeInTime).SetDelay(backgroundFadeInDelay));
			s.Insert(0f, coinCountParent.transform.DOScale(Vector3.one, finalCountScaleInTime).SetDelay(finalCountScaleInDelay).SetEase(finalCountScaleInEaseType));
			if (runnerGame.lives > 0)
			{
				s.AppendInterval(delayBetweenAddingHeartsToScore);
				s.AppendCallback(() => mHeartCount.Show());
				s.AppendInterval(delayForHeartCounterToAppear);
				for (int num2 = runnerGame.lives; num2 > 0; num2--)
				{
					num++;
					int hearts = num2 - 1;
					string gemsString = num.ToString();
					s.AppendCallback(delegate
					{
						coinCountText.text = gemsString;
						coinCountParent.transform.DOPunchScale(Vector3.one * finalCountHeartUpdateBounceAmount, finalCountHeartUpdateBouneTime, 2);
						mHeartCount.SetHeartCount(hearts, true);
						runnerGame.PassCheckpoint();
					});
					s.AppendInterval(delayBetweenAddingHeartsToScore);
				}
				s.AppendInterval(delayBeforeShowingHint);
				s.AppendCallback(() => mHeartCount.Hide());
			}
			s.AppendInterval(delayBeforeShowingHint);
			s.AppendCallback(delegate
			{
				hiddenEndButton.interactable = true;
				GenericGameTooltip genericGameTooltip = mHint;
				genericGameTooltip.OnShowComplete = (Action)Delegate.Combine(genericGameTooltip.OnShowComplete, OnShowComplete);
				mHint.Show();
			});
		}

		public override void Hide()
		{
			OnHide();
			OnHideComplete();
		}
	}
}
