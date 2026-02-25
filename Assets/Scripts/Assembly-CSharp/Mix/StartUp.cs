using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix
{
	public class StartUp : MonoBehaviour
	{
		private void Start()
		{
			Screen.fullScreen = false;
			SetCanvasSize();
			EventSystem eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
			eventSystem.pixelDragThreshold = ((Screen.width <= Screen.height) ? (Screen.width / 20) : (Screen.height / 20));
			eventSystem.pixelDragThreshold = ((eventSystem.pixelDragThreshold >= 10) ? eventSystem.pixelDragThreshold : 10);
			TouchScreenKeyboard.hideInput = true;
			Invoke("SetCanvasSize", 1f);
		}

		public void SetCanvasSize()
		{
			if (!this.IsNullOrDisposed())
			{
				RectTransform component = GameObject.Find("UI_Scrim_Layer").GetComponent<RectTransform>();
				MixConstants.CANVAS_HEIGHT = component.sizeDelta.y;
				MixConstants.CANVAS_WIDTH = component.sizeDelta.x;
			}
		}

		public static void BeginStartSequence()
		{
			MonoSingleton<StartUpSequence>.Instance.Init();
		}

		private void SetLauncherParams()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			string[] array = commandLineArgs;
			foreach (string text in array)
			{
			}
		}
	}
}
