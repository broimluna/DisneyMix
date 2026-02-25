using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveScoreboard : MonoBehaviour
	{
		private const float STAT_LINE_TIME = 0.25f;

		public GameObject ScoreContainer;

		public UICounter ScoreCounter;

		public UICounter[] StatsCounters;

		public GameObject[] ScoreLines;

		public HighFiveUiBadge[] Badges;

		private HighFiveStats mStats;

		private TweenCallback mOnComplete;

		public void Hide()
		{
			ScoreContainer.SetActive(false);
			for (int i = 0; i < ScoreLines.Length; i++)
			{
				ScoreLines[i].SetActive(false);
			}
		}

		public void DisplayScore(HighFiveStats aStats, TweenCallback aCallback)
		{
			ScoreContainer.SetActive(true);
			mStats = aStats;
			ScoreCounter.OnCountFinished += DisplayRating;
			((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("MainScoreTally");
			ScoreCounter.CountToTargetValue(aStats.CurScore, 1.5f);
			mOnComplete = aCallback;
		}

		protected void DisplayRating(int aCounterVal)
		{
			int rating = mStats.GetRating();
			if (rating > 0)
			{
				Badges[rating - 1].gameObject.SetActive(true);
				DOVirtual.DelayedCall(0.75f, DisplayStats);
				((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("EndGameStamp");
			}
			else
			{
				DisplayStats();
			}
		}

		private void DisplayStats()
		{
			Sequence s = DOTween.Sequence();
			int totalBeats = mStats.TotalBeats;
			float num = (float)mStats.TotalHits / (float)totalBeats;
			int accuracyPercent = Mathf.RoundToInt(100f * num);
			s.InsertCallback(0.25f, delegate
			{
				DisplayOneScoreLine(0, accuracyPercent);
			});
			s.InsertCallback(0.375f, delegate
			{
				((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("PercentageTallyScore");
			});
			s.InsertCallback(0.5f, delegate
			{
				DisplayOneScoreLine(1, mStats.MaxStreak);
			});
			s.InsertCallback(0.75f, delegate
			{
				((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("EndGameCheck");
			});
			if (mOnComplete != null)
			{
				s.InsertCallback(0.75f, mOnComplete);
			}
		}

		private void DisplayOneScoreLine(int aIndex, int aValue)
		{
			ScoreLines[aIndex].SetActive(true);
			StatsCounters[aIndex].CountToTargetValue(aValue, 0.25f);
		}
	}
}
