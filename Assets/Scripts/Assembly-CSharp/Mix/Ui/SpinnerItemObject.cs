using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class SpinnerItemObject
	{
		public const string IA = "jSv";

		private GameObject mainObj;

		public SpinnerItemObject(Transform aParent, int aIndex, GameObject aSpinnerItemObj, string aValue = null)
		{
			mainObj = Object.Instantiate(aSpinnerItemObj);
			if (!string.IsNullOrEmpty(aValue))
			{
				mainObj.transform.Find("Text").GetComponent<Text>().text = aValue;
			}
			mainObj.transform.SetParent(aParent, false);
			mainObj.transform.SetSiblingIndex(aIndex);
			mainObj.SetActive(true);
		}

		public void SetActive(bool aActive)
		{
			mainObj.SetActive(aActive);
		}
	}
}
