using Mix.Assets;
using Mix.Games.Tray.Hints;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeScrollHandler : MonoBehaviour, IEventSystemHandler, IBeginDragHandler, IAssetReferencer
	{
		public string TooltipText;

		public GameObject GameTooltipPrefab;

		public Sprite DownArrow;

		public Color tooltipTintColor = Color.white;

		private bool isInitialDrag = true;

		private GenericGameTooltip tooltip;

		void IBeginDragHandler.OnBeginDrag(PointerEventData aEventData)
		{
			if (isInitialDrag)
			{
				isInitialDrag = false;
				tooltip.Hide();
			}
		}

		void IAssetReferencer.CleanupReferences()
		{
			AssetUtils.DestroyGameObject(GameTooltipPrefab);
			AssetUtils.DestroySprite(DownArrow);
		}

		private void Start()
		{
			tooltip = InstantiateTooltip();
			tooltip.Show();
		}

		private GenericGameTooltip InstantiateTooltip()
		{
			GameObject gameObject = Object.Instantiate(GameTooltipPrefab);
			gameObject.transform.SetParent(base.transform, false);
			GenericGameTooltip componentInChildren = gameObject.GetComponentInChildren<GenericGameTooltip>();
			if (!(componentInChildren == null))
			{
				componentInChildren.SetAnchor(AnchorUIElement.AnchorStyle.BOTTOM_CENTER);
				componentInChildren.text = TooltipText;
				componentInChildren.hintText.color = tooltipTintColor;
				componentInChildren.leftIcon.sprite = DownArrow;
				componentInChildren.leftIcon.transform.Rotate(new Vector3(0f, 0f, -90f));
				componentInChildren.leftIcon.gameObject.SetActive(true);
				componentInChildren.leftIcon.color = tooltipTintColor;
				componentInChildren.rightIcon.sprite = DownArrow;
				componentInChildren.rightIcon.transform.Rotate(new Vector3(0f, 0f, 90f));
				componentInChildren.rightIcon.gameObject.SetActive(true);
				componentInChildren.rightIcon.color = tooltipTintColor;
			}
			return componentInChildren;
		}
	}
}
