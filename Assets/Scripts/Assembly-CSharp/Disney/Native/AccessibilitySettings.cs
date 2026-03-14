using Mix;
using UnityEngine;

namespace Disney.Native
{
	public class AccessibilitySettings : MonoBehaviour
	{
		public bool VisibleOnlyForSwitchControl;

		public bool Priority;

		public bool IgnoreText;

		public bool VoiceOnly;

		public string CustomToken = string.Empty;

		public GameObject ReferenceToken;

		public bool DontRender;

		[HideInInspector]
		public string DynamicText;

		private void Start()
		{
			Setup();
		}

		public virtual void Setup()
		{
			if (VisibleOnlyForSwitchControl)
			{
				MonoSingleton<NativeAccessiblityManager>.Instance.OnToggleAccessibilities += ToggleAccessibilities;
			}
			if (!MonoSingleton<NativeAccessiblityManager>.Instance.IsEnabled && VisibleOnlyForSwitchControl)
			{
				base.gameObject.SetActive(false);
			}
		}

		public void OnEnable()
		{
			if (Priority)
			{
				MonoSingleton<NativeAccessiblityManager>.Instance.AddPriorityGameObject(base.gameObject);
			}
			if (this is ScrollContentAccessibilitySettings)
			{
				MonoSingleton<NativeAccessiblityManager>.Instance.ScrollContentComponents.Add(base.gameObject);
			}
		}

		public void OnDisable()
		{
			if (MonoSingleton<NativeAccessiblityManager>.Instance != null && !Equals(null))
			{
				if (Priority)
				{
					MonoSingleton<NativeAccessiblityManager>.Instance.RemovePriorityGameObject(base.gameObject);
				}
				if (this is ScrollContentAccessibilitySettings)
				{
					MonoSingleton<NativeAccessiblityManager>.Instance.ScrollContentComponents.Remove(base.gameObject);
				}
			}
		}

		public void Destroy()
		{
			if (MonoSingleton<NativeAccessiblityManager>.Instance != null)
			{
				if (Priority)
				{
					MonoSingleton<NativeAccessiblityManager>.Instance.RemovePriorityGameObject(base.gameObject);
				}
				if (VisibleOnlyForSwitchControl)
				{
					MonoSingleton<NativeAccessiblityManager>.Instance.OnToggleAccessibilities -= ToggleAccessibilities;
				}
				if (this is ScrollContentAccessibilitySettings)
				{
					MonoSingleton<NativeAccessiblityManager>.Instance.ScrollContentComponents.Remove(base.gameObject);
				}
			}
		}

		public void ToggleAccessibilities(object sender, ToggleAccessibilitiesEventArgs args)
		{
			if (!Equals(null) && base.gameObject != null)
			{
				base.gameObject.SetActive(args.IsOn);
			}
		}
	}
}
