using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveUiBadge : MonoBehaviour
	{
		public float AppearTime = 1f;

		public float AppearScale = 5f;

		public float TextDelay = 0.5f;

		public float TextScale = 10f;

		public float TextAppearTime = 0.5f;

		public Image Badge;

		public Image Text;

		private void OnEnable()
		{
			Text.gameObject.SetActive(false);
			Sequence s = DOTween.Sequence();
			s.Append(Badge.transform.DOScale(AppearScale * Vector3.one, AppearTime).From().SetEase(Ease.OutBounce));
			s.Join(Badge.DOFade(0f, 0.25f * AppearTime).From().SetEase(Ease.InCubic));
			s.InsertCallback(TextDelay, delegate
			{
				Text.gameObject.SetActive(true);
			});
			s.Insert(TextDelay, Text.transform.DOScale(TextScale * Vector3.one, TextAppearTime).From().SetEase(Ease.OutBounce));
			s.Join(Text.DOFade(0f, 0.25f * TextAppearTime).From().SetEase(Ease.InCubic));
		}
	}
}
