using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.ResultsCanvas
{
	public class PlayerResultsData : MonoBehaviour
	{
		private const float FADE_DURATION = 0.5f;

		[SerializeField]
		private Image ResultsImage;

		[SerializeField]
		private Text ResultsEntryMessage;

		private Color FADED_COLOR = new Color(1f, 1f, 1f, 0f);

		public void SetResultsImage(Sprite aSprite)
		{
			ResultsImage.sprite = aSprite;
			ResultsImage.enabled = false;
		}

		public void SetResultsMessage(string aMessage)
		{
			ResultsEntryMessage.text = aMessage;
		}

		public void FadeInImage()
		{
			ResultsImage.enabled = true;
			ResultsImage.color = FADED_COLOR;
			ResultsImage.DOFade(1f, 0.5f);
		}
	}
}
