using Mix.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class NotificationBar : MonoBehaviour
	{
		public Text NotificationText;

		public void ShowFromToken(string aToken)
		{
			NotificationText.text = Singleton<Localizer>.Instance.getString(aToken);
			base.gameObject.SetActive(true);
		}

		public void ShowFromString(string aString)
		{
			NotificationText.text = aString;
			base.gameObject.SetActive(true);
		}
	}
}
