using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveGameOverPanel : MonoBehaviour
	{
		public RectTransform Panel;

		public HighFiveScoreboard[] Scoreboards;

		public Button EndButton;

		public void Show(int aPlayerNum, HighFiveStats aStats)
		{
			((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("EndGameBoardSlidesIn");
			base.gameObject.SetActive(true);
			EndButton.gameObject.SetActive(false);
			Panel.DOLocalMoveX(400f, 0.5f).SetEase(Ease.OutBack).From()
				.OnComplete(delegate
				{
					ShowScoreboard(aPlayerNum, aStats);
				});
		}

		private void ShowScoreboard(int aPlayerNum, HighFiveStats aStats)
		{
			Scoreboards[aPlayerNum].DisplayScore(aStats, ShowButton);
		}

		private void ShowButton()
		{
			EndButton.gameObject.SetActive(true);
		}

		public void Hide()
		{
			for (int i = 0; i < Scoreboards.Length; i++)
			{
				Scoreboards[i].Hide();
			}
			base.gameObject.SetActive(false);
		}
	}
}
