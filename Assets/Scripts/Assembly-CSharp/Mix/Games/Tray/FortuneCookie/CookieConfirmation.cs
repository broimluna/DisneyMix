using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.FortuneCookie
{
	public class CookieConfirmation : MonoBehaviour
	{
		public Button confirmButton;

		public Button cancelButton;

		public void HideAllButtons()
		{
			HideConfirmButton();
			HideCancelButton();
		}

		public void ShowAllButtons()
		{
			ShowConfirmButton();
			ShowCancelButton();
		}

		public void ShowConfirmButton()
		{
			confirmButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
			confirmButton.interactable = true;
		}

		public void HideConfirmButton()
		{
			confirmButton.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
			confirmButton.interactable = false;
		}

		public void ShowCancelButton()
		{
			cancelButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
			cancelButton.interactable = true;
		}

		public void HideCancelButton()
		{
			cancelButton.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
			cancelButton.interactable = false;
		}

		private void Start()
		{
			confirmButton.transform.localScale = Vector3.zero;
			cancelButton.transform.localScale = Vector3.zero;
			confirmButton.interactable = false;
			cancelButton.interactable = false;
		}
	}
}
