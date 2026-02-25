using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordSuccessPanel : MonoBehaviour
	{
		[Header("Internal References")]
		public Image background;

		public Text message;

		[Header("Show Animation")]
		public float messageScaleInTime = 0.5f;

		public float messageScaleInDelay = 0.3f;

		public Ease messageScaleInEaseType;

		public float messageRotationRandonmess = 10f;

		public float backgroundFadeInTime = 0.5f;

		public float backgroundFadeInDelay;

		public Color backgroundFadeInitialColor;

		public Color backgroudnFadeFinalColor;

		[ContextMenu("Show")]
		public void Show()
		{
			base.gameObject.SetActive(true);
			message.transform.localScale = Vector3.zero;
			background.color = backgroundFadeInitialColor;
			background.DOColor(backgroudnFadeFinalColor, backgroundFadeInTime).SetDelay(backgroundFadeInDelay);
			message.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f - messageRotationRandonmess, messageRotationRandonmess));
			Sequence s = DOTween.Sequence();
			s.AppendInterval(messageScaleInDelay);
			s.Append(message.transform.DOScale(Vector3.one, messageScaleInTime).SetEase(messageScaleInEaseType));
		}

		[ContextMenu("Hide")]
		public void Hide()
		{
			background.DOColor(backgroundFadeInitialColor, backgroundFadeInTime);
			message.transform.DOScale(Vector3.zero, messageScaleInTime).SetEase(messageScaleInEaseType);
		}
	}
}
