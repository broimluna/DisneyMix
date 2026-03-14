using Mix.Games.Tray.Hints;
using Mix.Localization;
using UnityEngine;

namespace Mix.Games.Tray.FortuneCookie
{
	public class ToolTips : MonoBehaviour
	{
		public GameObject ToolTipPrefab;

		public string TutorialToken;

		public string ComeBackLaterToken;

		[HideInInspector]
		public GenericGameTooltip TutorialToolTip;

		[HideInInspector]
		public GenericGameTooltip ComeBackLaterToolTip;

		private void Awake()
		{
			TutorialToolTip = InstantiateTooltip();
			TutorialToolTip.SetAnchor(AnchorUIElement.AnchorStyle.BOTTOM_CENTER);
			TutorialToolTip.text = Singleton<Localizer>.Instance.getString(TutorialToken);
			TutorialToolTip.Hide(true);
			ComeBackLaterToolTip = InstantiateTooltip();
			ComeBackLaterToolTip.SetAnchor(AnchorUIElement.AnchorStyle.TOP_CENTER);
			ComeBackLaterToolTip.text = Singleton<Localizer>.Instance.getString(ComeBackLaterToken);
			ComeBackLaterToolTip.Hide(true);
		}

		private GenericGameTooltip InstantiateTooltip()
		{
			GameObject gameObject = Object.Instantiate(ToolTipPrefab);
			gameObject.transform.SetParent(base.transform, false);
			GenericGameTooltip component = gameObject.GetComponent<GenericGameTooltip>();
			if (component == null)
			{
				Debug.LogError("Prefab does not have a component of the required type.");
			}
			return component;
		}
	}
}
