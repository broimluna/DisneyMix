using System;
using System.Collections.Generic;
using Mix;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Disney.Native
{
	public class NativeAccessiblityManager : MonoSingleton<NativeAccessiblityManager>
	{
		private List<GameObject> PriorityGameObjects = new List<GameObject>();

		public List<GameObject> ScrollContentComponents = new List<GameObject>();

		public List<AccessibilitySettings> HiddenScrollItemIds = new List<AccessibilitySettings>();

		private ParseButton buttonParser;

		private ParseInput inputParser;

		private ParseToggle toggleParser;

		private ParseText textParser;

		private NativeAccessibility native;

		public bool IsEnabled { get; private set; }

		public NativeAccessibilityLevel AccessibilityLevel { get; private set; }

		public NativeAccessibility Native
		{
			get
			{
				return native;
			}
			private set
			{
				native = value;
			}
		}

		public event EventHandler<ToggleAccessibilitiesEventArgs> OnToggleAccessibilities = delegate
		{
		};

		public void AddPriorityGameObject(GameObject aGameObject)
		{
			PriorityGameObjects.Remove(aGameObject);
			PriorityGameObjects.Add(aGameObject);
		}

		public void RemovePriorityGameObject(GameObject aGameObject)
		{
			PriorityGameObjects.Remove(aGameObject);
		}

		protected void Awake()
		{
			AccessibilityLevel = NativeAccessibilityLevel.NONE;
			native = base.gameObject.AddComponent<NativeAndroidAccessibility>();
		}

		private void OnApplicationPause(bool aIsGoingToBackground)
		{
		}

		public void Init(IAccessibilityLocalization aLocalization)
		{
			buttonParser = new ParseButton(aLocalization);
			inputParser = new ParseInput(aLocalization);
			toggleParser = new ParseToggle(aLocalization);
			textParser = new ParseText(aLocalization);
			base.gameObject.name = "AccessibilityManager";
			InvokeRepeating("CheckSwitchControlEnabled", 0.1f, 1f);
		}

		public void CheckSwitchControlEnabled()
		{
			AccessibilityLevel = (NativeAccessibilityLevel)MonoSingleton<NativeAccessiblityManager>.Instance.Native.GetAccessibilityLevel();
			if (AccessibilityLevel != NativeAccessibilityLevel.NONE)
			{
				MonoSingleton<NativeAccessiblityManager>.Instance.Enable();
			}
			else
			{
				MonoSingleton<NativeAccessiblityManager>.Instance.Disable();
			}
		}

		public void Enable()
		{
			if (!IsEnabled)
			{
				IsEnabled = true;
				Clear();
				Native.OnButtonClicked += HandleOnButtonClicked;
				this.OnToggleAccessibilities(this, new ToggleAccessibilitiesEventArgs(true));
				if (!IsInvoking("ParseScene"))
				{
					InvokeRepeating("ParseScene", 0.1f, 1f);
				}
			}
		}

		public void Disable()
		{
			if (IsEnabled)
			{
				IsEnabled = false;
				Clear();
				Native.OnButtonClicked -= HandleOnButtonClicked;
				this.OnToggleAccessibilities(this, new ToggleAccessibilitiesEventArgs(false));
				if (IsInvoking("ParseScene"))
				{
					CancelInvoke("ParseScene");
				}
			}
		}

		public void Clear()
		{
			buttonParser.Clear();
			inputParser.Clear();
			toggleParser.Clear();
			textParser.Clear();
			Native.ClearAllElements();
		}

		public void ParseHiddenScrollItems()
		{
			HiddenScrollItemIds = new List<AccessibilitySettings>();
			foreach (GameObject scrollContentComponent in MonoSingleton<NativeAccessiblityManager>.Instance.ScrollContentComponents)
			{
				if (!(scrollContentComponent != null))
				{
					continue;
				}
				ScrollContentAccessibilitySettings component = scrollContentComponent.GetComponent<ScrollContentAccessibilitySettings>();
				if (!(component != null))
				{
					continue;
				}
				ScrollRect referenceScrollRect = component.ReferenceScrollRect;
				if (!(referenceScrollRect != null) || !(referenceScrollRect.viewport != null))
				{
					continue;
				}
				Rect rectInScreenSpace = Util.GetRectInScreenSpace(referenceScrollRect.viewport);
				foreach (Transform item in component.gameObject.transform)
				{
					RectTransform component2 = item.gameObject.GetComponent<RectTransform>();
					if (component2 != null)
					{
						Rect rectInScreenSpace2 = Util.GetRectInScreenSpace(component2);
						if (!rectInScreenSpace.Contains(new Vector2(rectInScreenSpace2.x + rectInScreenSpace2.width / 2f, rectInScreenSpace2.y + rectInScreenSpace2.height / 2f)))
						{
							HiddenScrollItemIds.Add(item.GetComponent<AccessibilitySettings>());
						}
					}
				}
			}
		}

		private void ParseScene()
		{
			if (!this.IsNullOrDisposed() && !(base.gameObject == null))
			{
				GameObject[] gameObjects = ((PriorityGameObjects.Count <= 0) ? SceneManager.GetActiveScene().GetRootGameObjects() : new GameObject[1] { PriorityGameObjects[PriorityGameObjects.Count - 1] });
				ParseHiddenScrollItems();
				if (AccessibilityLevel == NativeAccessibilityLevel.VOICE)
				{
					textParser.Parse(gameObjects);
				}
				buttonParser.Parse(gameObjects);
				inputParser.Parse(gameObjects);
				toggleParser.Parse(gameObjects);
			}
		}

		private void HandleOnButtonClicked(object sender, ButtonClickedEventArgs args)
		{
			buttonParser.Click(args.Id);
			inputParser.Click(args.Id);
			toggleParser.Click(args.Id);
			textParser.Click(args.Id);
		}
	}
}
