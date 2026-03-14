using Mix.Games.Tray.Hints;
using UnityEngine;

namespace Mix.Games.Tray.WouldYouRather
{
	public class Hints : MonoBehaviour
	{
		public GameObject tooltipHintPrefab;

		[HideInInspector]
		public GenericGameTooltip selectCardsHint;

		[HideInInspector]
		public GenericGameTooltip storyHint;

		private void Awake()
		{
			selectCardsHint = InstantiateTooltip();
			selectCardsHint.SetAnchor(AnchorUIElement.AnchorStyle.BOTTOM_CENTER);
			selectCardsHint.text = string.Empty;
			selectCardsHint.Hide(true);
			storyHint = InstantiateTooltip();
			storyHint.SetAnchor(AnchorUIElement.AnchorStyle.TOP_CENTER);
			storyHint.text = string.Empty;
			storyHint.Hide(true);
		}

		private void Start()
		{
		}

		private GenericGameTooltip InstantiateTooltip()
		{
			GameObject gameObject = Object.Instantiate(tooltipHintPrefab);
			gameObject.transform.SetParent(base.transform, false);
			GenericGameTooltip componentInChildren = gameObject.GetComponentInChildren<GenericGameTooltip>();
			if (componentInChildren == null)
			{
				Debug.LogError("Prefab does not have a component of the required type.");
			}
			return componentInChildren;
		}
	}
}
