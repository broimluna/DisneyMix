using System;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class OffscreenScoreBanners : MonoBehaviour
	{
		public UICounter ScoreCounter;

		public UICounter BestScoreCounter;

		public Camera ScoreCamera;

		public Camera BestScoreCamera;

		private DropGame dropGame;

		private void OnEnable()
		{
			dropGame = DropGame.Instance;
			DropGame obj = dropGame;
			obj.OnScoreUpdated = (Action<int>)Delegate.Combine(obj.OnScoreUpdated, new Action<int>(UpdateScore));
			DropGame obj2 = dropGame;
			obj2.OnBestScoreUpdated = (Action<int>)Delegate.Combine(obj2.OnBestScoreUpdated, new Action<int>(UpdateBestScore));
			ScoreCounter.OnAnimationStarted += EnableScoreCamera;
			ScoreCounter.OnAnimationFinished += DisableScoreCamera;
			BestScoreCounter.OnAnimationStarted += EnableBestScoreCamera;
			BestScoreCounter.OnAnimationFinished += DisableBestScoreCamera;
		}

		private void OnDisable()
		{
			DropGame obj = dropGame;
			obj.OnScoreUpdated = (Action<int>)Delegate.Remove(obj.OnScoreUpdated, new Action<int>(UpdateScore));
			DropGame obj2 = dropGame;
			obj2.OnBestScoreUpdated = (Action<int>)Delegate.Remove(obj2.OnBestScoreUpdated, new Action<int>(UpdateBestScore));
			ScoreCounter.OnAnimationStarted -= EnableScoreCamera;
			ScoreCounter.OnAnimationFinished -= DisableScoreCamera;
			BestScoreCounter.OnAnimationStarted -= EnableBestScoreCamera;
			BestScoreCounter.OnAnimationFinished -= DisableBestScoreCamera;
		}

		private void Start()
		{
			BestScoreCounter.CountToTargetValue(dropGame.BestScore, 0f);
		}

		private void UpdateScore(int score)
		{
			ScoreCounter.CountToTargetValue(score, 0f);
		}

		private void UpdateBestScore(int score)
		{
			BestScoreCounter.CountToTargetValue(score, 0f);
		}

		private void EnableScoreCamera()
		{
			ScoreCamera.enabled = true;
		}

		private void DisableScoreCamera()
		{
			ScoreCamera.enabled = false;
		}

		private void EnableBestScoreCamera()
		{
			BestScoreCamera.enabled = true;
		}

		private void DisableBestScoreCamera()
		{
			BestScoreCamera.enabled = false;
		}
	}
}
