using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Localization
{
	public class LocalizedText : MonoBehaviour
	{
		public bool doNotLocalize;

		public string token = string.Empty;

		private void Start()
		{
			if (!doNotLocalize && !(GetComponent<Text>() != null))
			{
			}
		}
	}
}
