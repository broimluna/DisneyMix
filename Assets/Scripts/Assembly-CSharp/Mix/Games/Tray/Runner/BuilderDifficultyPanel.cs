using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class BuilderDifficultyPanel : MonoBehaviour
	{
		public List<GameObject> heads = new List<GameObject>();

		public float mPunchDuration;

		public Vector3 mPunchAmt;

		private int mCurrentDifficulty = -1;

		public void SetDifficulty(int difficulty)
		{
			if (mCurrentDifficulty == difficulty)
			{
				return;
			}
			for (int i = 0; i < heads.Count; i++)
			{
				if (i < difficulty)
				{
					DOTween.Kill(heads[i].transform, true);
					heads[i].SetActive(true);
					heads[i].transform.DOPunchScale(mPunchAmt, mPunchDuration);
				}
				else
				{
					heads[i].SetActive(false);
				}
			}
			mCurrentDifficulty = difficulty;
		}
	}
}
