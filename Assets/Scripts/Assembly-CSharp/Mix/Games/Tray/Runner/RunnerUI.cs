using System;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class RunnerUI : MonoBehaviour
	{
		public MainRunnerGame runnerGame;

		[Header("UI Panel Prefabs")]
		public GameObject BuildUIPanelPrefab;

		public GameObject FirstPlayerUIPanelPrefab;

		public GameObject PlayUIPanelPrefab;

		public GameObject EndUIPanelPrefab;

		public BuildUIPanel BuildUI { get; private set; }

		public RunnerUIPanel CurrentUIPanel { get; private set; }

		public void UnloadCurrentUI()
		{
			if (CurrentUIPanel != null)
			{
				RunnerUIPanel currentUIPanel = CurrentUIPanel;
				currentUIPanel.OnHideComplete = (Action)Delegate.Combine(currentUIPanel.OnHideComplete, (Action)delegate
				{
					UnityEngine.Object.Destroy(CurrentUIPanel.gameObject);
				});
				CurrentUIPanel.Hide();
				CurrentUIPanel = null;
			}
		}

		public void LoadBuildUI(Action OnHideCompleteAction = null, Action OnShowCompleteAction = null)
		{
			SwapPanel(BuildUIPanelPrefab, OnHideCompleteAction, OnShowCompleteAction);
		}

		public void LoadFirstPlayerUI(Action OnHideCompleteAction = null, Action OnShowCompleteAction = null)
		{
			SwapPanel(FirstPlayerUIPanelPrefab, OnHideCompleteAction, OnShowCompleteAction);
		}

		public void LoadPlayUI(Action OnHideCompleteAction = null, Action OnShowCompleteAction = null)
		{
			SwapPanel(PlayUIPanelPrefab, OnHideCompleteAction, OnShowCompleteAction);
		}

		public void LoadEndUI(Action OnHideCompleteAction = null, Action OnShowCompleteAction = null)
		{
			SwapPanel(EndUIPanelPrefab, OnHideCompleteAction, OnShowCompleteAction);
		}

		private void SwapPanel(GameObject nextPanelPrefab, Action onHideCompleteAction, Action onShowCompleteAction)
		{
			if (CurrentUIPanel != null)
			{
				RunnerUIPanel oldPanel = CurrentUIPanel;
				CurrentUIPanel = null;
				RunnerUIPanel runnerUIPanel = oldPanel;
				runnerUIPanel.OnHideComplete = (Action)Delegate.Combine(runnerUIPanel.OnHideComplete, (Action)delegate
				{
					if (onHideCompleteAction != null)
					{
						onHideCompleteAction();
					}
					LoadPanelPrefab(nextPanelPrefab, onShowCompleteAction);
					UnityEngine.Object.Destroy(oldPanel.gameObject);
				});
				oldPanel.Hide();
			}
			else
			{
				if (onHideCompleteAction != null)
				{
					onHideCompleteAction();
				}
				LoadPanelPrefab(nextPanelPrefab, onShowCompleteAction);
			}
		}

		private void LoadPanelPrefab(GameObject panelPrefab, Action onShowCompleteAction)
		{
			if (panelPrefab != null)
			{
				CurrentUIPanel = InstantiateRunnerUIPanel(panelPrefab);
				RunnerUIPanel currentUIPanel = CurrentUIPanel;
				currentUIPanel.OnShowComplete = (Action)Delegate.Combine(currentUIPanel.OnShowComplete, (Action)delegate
				{
					if (onShowCompleteAction != null)
					{
						onShowCompleteAction();
					}
				});
				CurrentUIPanel.Show();
			}
			else
			{
				CurrentUIPanel = null;
				if (onShowCompleteAction != null)
				{
					onShowCompleteAction();
				}
			}
		}

		private RunnerUIPanel InstantiateRunnerUIPanel(GameObject uiPrefab)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(uiPrefab);
			gameObject.transform.SetParent(base.transform, false);
			RunnerUIPanel component = gameObject.GetComponent<RunnerUIPanel>();
			if (component.drawOrder == RunnerUIPanel.DrawOrder.LAST)
			{
				component.transform.SetAsLastSibling();
			}
			else
			{
				component.transform.SetAsFirstSibling();
			}
			component.runnerGame = runnerGame;
			return component;
		}
	}
}
