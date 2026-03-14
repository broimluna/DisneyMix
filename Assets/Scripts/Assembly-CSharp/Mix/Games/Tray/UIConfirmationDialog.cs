using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray
{
	public class UIConfirmationDialog : MonoBehaviour
	{
		protected const float SHOW_TIME = 0.5f;

		protected const float HIDE_TIME = 0.5f;

		public Button ConfirmButton;

		public Button CancelButton;

		public void Init(Action aConfirmationDelegate, Action aCancelDelegate)
		{
			ConfirmButton.onClick.AddListener(delegate
			{
				OnClickSequence(ConfirmButton, CancelButton, aConfirmationDelegate);
			});
			CancelButton.onClick.AddListener(delegate
			{
				OnClickSequence(CancelButton, ConfirmButton, aCancelDelegate);
			});
		}

		public void HideAllButtons()
		{
			HideButton(ConfirmButton);
			HideButton(CancelButton);
		}

		public void ShowAllButtons()
		{
			ShowButton(ConfirmButton);
			ShowButton(CancelButton);
		}

		public void ShowConfirmButton()
		{
			ShowButton(ConfirmButton);
		}

		public void HideConfirmButton()
		{
			HideButton(ConfirmButton);
		}

		public void ShowCancelButton()
		{
			ShowButton(CancelButton);
		}

		public void HideCancelButton()
		{
			HideButton(CancelButton);
		}

		protected void Start()
		{
			SetupButton(ConfirmButton);
			SetupButton(CancelButton);
		}

		protected void SetupButton(Button aButton)
		{
			aButton.transform.localScale = Vector3.zero;
			aButton.interactable = false;
			aButton.onClick.RemoveAllListeners();
		}

		protected Tween ShowButton(Button aButton)
		{
			aButton.interactable = true;
			return aButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
		}

		protected Tween HideButton(Button aButton)
		{
			aButton.interactable = false;
			return aButton.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
		}

		protected void OnClickSequence(Button aClicked, Button aOther, Action aDelegate)
		{
			aDelegate();
			HideAllButtons();
		}
	}
}
