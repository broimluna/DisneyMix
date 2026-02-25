using Mix.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class Tooltip : MonoBehaviour
	{
		protected string mTooltipText = string.Empty;

		protected bool mTooltipUpdated;

		public void SetTooltip(string text)
		{
			mTooltipText = text;
		}

		private void Update()
		{
			if (!string.IsNullOrEmpty(mTooltipText) && !mTooltipUpdated)
			{
				Text componentInChildren = GetComponentInChildren<Text>();
				componentInChildren.text = mTooltipText;
				mTooltipUpdated = true;
			}
		}

		private void OnDisable()
		{
			mTooltipText = string.Empty;
			mTooltipUpdated = false;
			LocalizedText componentInChildren = GetComponentInChildren<LocalizedText>();
			if (componentInChildren != null && !componentInChildren.doNotLocalize)
			{
				Text componentInChildren2 = GetComponentInChildren<Text>();
				componentInChildren2.text = Singleton<Localizer>.Instance.getString(componentInChildren.token);
			}
		}
	}
}
