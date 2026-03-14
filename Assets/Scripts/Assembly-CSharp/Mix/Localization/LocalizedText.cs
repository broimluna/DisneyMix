using UnityEngine;
using UnityEngine.UI;

namespace Mix.Localization
{
	public class LocalizedText : MonoBehaviour
	{
		public bool doNotLocalize;

		public string token = string.Empty;

		private void Start()
		{
			if (!doNotLocalize && GetComponent<Text>() != null)
			{
				GetComponent<Text>().text = Singleton<Localizer>.Instance.getString(token);
			}
		}
	}
}
