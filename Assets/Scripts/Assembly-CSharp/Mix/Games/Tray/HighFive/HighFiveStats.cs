using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	[Serializable]
	public class HighFiveStats
	{
		private const int MULTIPLIER_MAX = 4;

		private const int MULTIPLIER_INCREMENT_STEPS = 8;

		private const float MULTIPLIER_PROGRESS_PER_STEP = 0.125f;

		private const int NUM_SAMPLES_MOVING_AVERAGE = 8;

		public Action<float> OnMultiplierProgressChanged;

		public Action<int> OnMultiplierChanged;

		private int[] mNumHits = new int[Enum.GetNames(typeof(HitType)).Length];

		public static readonly int[] PointVals = new int[5] { 0, 25, 25, 50, 100 };

		private int mTotalHits;

		private int mCurStreak;

		private int mMaxStreak;

		private int mCurScore;

		private int mMultiplierIncrement;

		private int mMultiplier = 1;

		private int mScoreAccumulator;

		private Queue<int> mScoreQueue = new Queue<int>(8);

		private int mMultiplierAccumulator;

		public int TotalHits
		{
			get
			{
				return mTotalHits;
			}
		}

		public int TotalBeats
		{
			get
			{
				return mTotalHits + GetNumHitsOfType(HitType.Miss);
			}
		}

		public int CurStreak
		{
			get
			{
				return mCurStreak;
			}
			set
			{
				mCurStreak = value;
				if (mCurStreak > mMaxStreak)
				{
					mMaxStreak = mCurStreak;
				}
			}
		}

		public int MaxStreak
		{
			get
			{
				return mMaxStreak;
			}
		}

		public int CurScore
		{
			get
			{
				return mCurScore;
			}
		}

		public int Multiplier
		{
			get
			{
				return mMultiplier;
			}
		}

		public int BestScore { get; set; }

		public void Reset()
		{
			mCurStreak = 0;
			mMaxStreak = 0;
			mCurScore = 0;
			mMultiplierIncrement = 0;
			mMultiplier = 1;
			for (int i = 0; i < mNumHits.Length; i++)
			{
				mNumHits[i] = 0;
			}
			int num = PointVals[2];
			mScoreAccumulator = 8 * num;
			for (int j = 0; j < 8; j++)
			{
				mScoreQueue.Enqueue(num);
			}
			mMultiplierAccumulator = 0;
		}

		public int RecordOneHit(HitType aHitType)
		{
			mNumHits[(int)aHitType]++;
			int num = 0;
			int num2 = PointVals[(int)aHitType];
			if (aHitType != HitType.Miss)
			{
				CurStreak++;
				mTotalHits++;
				if (mMultiplierIncrement < 8)
				{
					mMultiplierIncrement++;
					OnMultiplierProgressChanged(0.125f * (float)mMultiplierIncrement);
					if (mMultiplierIncrement == 8)
					{
						UpdateMultiplier(mMultiplier + 1);
					}
				}
				num = mMultiplier * num2;
				mCurScore += num;
			}
			else
			{
				CurStreak = 0;
				((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("Error");
				UpdateMultiplier(mMultiplier - 1);
			}
			int num3 = mScoreQueue.Dequeue();
			mScoreQueue.Enqueue(num2);
			mScoreAccumulator = mScoreAccumulator - num3 + num2;
			mMultiplierAccumulator += mMultiplier;
			return num;
		}

		public int GetNumHitsOfType(HitType aHitType)
		{
			return mNumHits[(int)aHitType];
		}

		public float GetCurrentRunningAverage()
		{
			return (float)mScoreAccumulator / 8f;
		}

		public float GetAverageMultiplier()
		{
			return (float)mMultiplierAccumulator / (float)TotalBeats;
		}

		public int GetRating()
		{
			float averageMultiplier = GetAverageMultiplier();
			int result = 0;
			int numHitsOfType = GetNumHitsOfType(HitType.Miss);
			int totalBeats = TotalBeats;
			if (averageMultiplier >= 3.4f && numHitsOfType == 0)
			{
				result = 4;
			}
			else if (averageMultiplier >= 2.9f && numHitsOfType < totalBeats / 10)
			{
				result = 3;
			}
			else if (averageMultiplier >= 2.4f)
			{
				result = 2;
			}
			else if (averageMultiplier > 1.9f)
			{
				result = 1;
			}
			return result;
		}

		private void UpdateMultiplier(int newVal)
		{
			int num = Mathf.Clamp(newVal, 1, 4);
			if (num > mMultiplier)
			{
				mMultiplier = num;
				OnMultiplierChanged(mMultiplier);
				if (mMultiplier < 4)
				{
					mMultiplierIncrement = 0;
					DOVirtual.DelayedCall(0.25f, delegate
					{
						OnMultiplierProgressChanged(0f);
					});
					DOVirtual.DelayedCall(0.3f, delegate
					{
						((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("LeftSideMultiplierUp");
					});
				}
			}
			else
			{
				if (num < mMultiplier)
				{
					mMultiplier = num;
					OnMultiplierChanged(mMultiplier);
				}
				if (mMultiplierIncrement > 0)
				{
					mMultiplierIncrement = 0;
					OnMultiplierProgressChanged(0f);
				}
			}
		}
	}
}
