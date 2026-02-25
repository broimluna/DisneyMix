using System;
using Mix.Games.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class TimerThumb : MonoBehaviour
	{
		public Text TimerText;

		protected DateTime mCountdownEndTime;

		protected bool mShouldDisplayCountdown;

		public bool IsTimerRunning
		{
			get
			{
				return mShouldDisplayCountdown;
			}
		}

		private void FixedUpdate()
		{
			if (mShouldDisplayCountdown)
			{
				SetTimeRemaining();
			}
		}

		public void Initialize(string id, string durationAmount, IGameStatistics gameStatistics)
		{
			TimeSpan result;
			if (!TimeSpan.TryParse(durationAmount, out result))
			{
				return;
			}
			DateTime lastPlayed = gameStatistics.GetLastPlayed(id);
			if (lastPlayed.CompareTo(DateTime.MinValue) == 0)
			{
				mShouldDisplayCountdown = false;
			}
			else
			{
				DateTime value = lastPlayed.Add(result);
				if (result.Days > 0 && result.Hours == 0 && result.Minutes == 0 && result.Seconds == 0)
				{
					value = new DateTime(value.Year, value.Month, value.Day);
				}
				int num = DateTime.Now.CompareTo(value);
				if (num < 0)
				{
					mShouldDisplayCountdown = true;
					mCountdownEndTime = value;
				}
				else
				{
					mShouldDisplayCountdown = false;
				}
			}
			base.gameObject.SetActive(mShouldDisplayCountdown);
		}

		protected void SetTimeRemaining()
		{
			TimeSpan timeSpan = mCountdownEndTime.Subtract(DateTime.Now);
			if (timeSpan.TotalSeconds < 0.0)
			{
				mShouldDisplayCountdown = false;
				base.gameObject.SetActive(mShouldDisplayCountdown);
			}
			else
			{
				string text = string.Format("{0}:{1}:{2}", timeSpan.TotalHours.ToString("00"), timeSpan.Minutes.ToString("D2"), timeSpan.Seconds.ToString("D2"));
				TimerText.text = text;
			}
		}
	}
}
