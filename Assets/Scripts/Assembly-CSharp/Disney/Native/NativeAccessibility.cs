using System;
using UnityEngine;

namespace Disney.Native
{
	public class NativeAccessibility : MonoBehaviour
	{
		public event EventHandler<ButtonClickedEventArgs> OnButtonClicked = delegate
		{
		};

		public virtual int GetAccessibilityLevel()
		{
			return 0;
		}

		public virtual bool IsSwitchControlEnabled()
		{
			return false;
		}

		public virtual bool IsVoiceOverEnabled()
		{
			return false;
		}

		public virtual void RemoveView(int aId)
		{
		}

		public virtual void RenderText(int aId, Rect aRect, string aLabel)
		{
		}

		public virtual void RenderButton(int aId, Rect aRect, string aLabel)
		{
		}

		public virtual void ClearAllElements()
		{
		}

		public virtual void UpdateView(int aId, Rect aRect, string aLabel)
		{
		}

		public virtual void ButtonClicked(string aId)
		{
			this.OnButtonClicked(this, new ButtonClickedEventArgs(int.Parse(aId)));
		}
	}
}
