using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ButtonHandler : MonoBehaviour
	{
		public Button MyButton;

		private void Start()
		{
			MyButton.onClick.AddListener(OnClickFour);
		}

		public void OneClickOne()
		{
			Debug.Log("== On Click One");
		}

		public void OneClickTwo()
		{
			Debug.Log("== On Click Two");
		}

		public void OnClickFour()
		{
			Debug.Log("== On Click Four");
		}
	}
}
