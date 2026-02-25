using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	[RequireComponent(typeof(Button))]
	public class ButtonHelper : MonoBehaviour
	{
		public bool OneTimeUse;

		public float ReActivateDelay;

		private Button button;

		private void Start()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(OnClicked);
		}

		private void OnClicked()
		{
			button.interactable = false;
			if (!OneTimeUse)
			{
				Invoke("ReActivate", ReActivateDelay);
			}
		}

		private void ReActivate()
		{
			if (!this.IsNullOrDisposed() && !button.IsNullOrDisposed())
			{
				button.interactable = true;
			}
		}
	}
}
