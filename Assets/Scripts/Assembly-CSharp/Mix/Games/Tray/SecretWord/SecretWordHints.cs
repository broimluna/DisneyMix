using Mix.Games.Tray.Hints;
using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordHints : MonoBehaviour
	{
		public GameObject tooltipHintPrefab;

		[Header("Hint Messages")]
		public string hintText = string.Empty;

		public string hintTextGroup = string.Empty;

		public AnchorUIElement.AnchorStyle hintAnchor;

		[HideInInspector]
		public GenericGameTooltip secretWordHint;

		public string HintText
		{
			get
			{
				return hintText;
			}
			set
			{
				hintText = value;
				secretWordHint.text = hintText;
			}
		}

		private void Awake()
		{
			secretWordHint = InstantiateTooltip();
			secretWordHint.SetAnchor(hintAnchor);
			secretWordHint.text = hintText;
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
