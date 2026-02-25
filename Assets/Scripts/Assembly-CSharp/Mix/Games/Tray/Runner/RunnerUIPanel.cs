using System;
using Mix.Games.Tray.Hints;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class RunnerUIPanel : MonoBehaviour
	{
		public enum DrawOrder
		{
			FIRST = 0,
			LAST = 1
		}

		public Action OnHide = delegate
		{
		};

		public Action OnHideComplete = delegate
		{
		};

		public Action OnShow = delegate
		{
		};

		public Action OnShowComplete = delegate
		{
		};

		public DrawOrder drawOrder;

		[HideInInspector]
		public MainRunnerGame runnerGame;

		[Header("Prefab References")]
		public GameObject gameTooltipPrefab;

		public virtual void Show()
		{
			OnShow();
			OnShowComplete();
		}

		public virtual void Hide()
		{
			OnHide();
			OnHideComplete();
		}

		protected GenericGameTooltip InstantiateTooltip(Transform parent)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(gameTooltipPrefab);
			gameObject.transform.SetParent(parent, false);
			GenericGameTooltip componentInChildren = gameObject.GetComponentInChildren<GenericGameTooltip>();
			if (componentInChildren == null)
			{
				Debug.LogError("Prefab does not have a component of the required type.");
			}
			return componentInChildren;
		}
	}
}
