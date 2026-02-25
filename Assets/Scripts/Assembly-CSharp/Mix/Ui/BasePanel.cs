using UnityEngine;

namespace Mix.Ui
{
	public class BasePanel : MonoBehaviour
	{
		public bool isPanelCloseClicked;

		private bool hiding;

		public int panelId = -1;

		public event OnPanelOpened PanelOpenedEvent;

		public event OnPanelHidden PanelHiddenEvent;

		public event OnPanelClosing PanelClosingEvent;

		public event OnPanelClosed PanelClosedEvent;

		public event OnPanelCloseClicked PanelCloseClickedEvent;

		public virtual void Setup()
		{
		}

		public virtual bool OnAndroidBackButton()
		{
			PanelCloseClicked();
			return true;
		}

		public virtual void PanelCloseClicked()
		{
			isPanelCloseClicked = true;
			ClosePanel();
		}

		public virtual void ClosePanel()
		{
			if (!this.IsNullOrDisposed() && !(base.gameObject == null))
			{
				if (this.PanelClosingEvent != null)
				{
					this.PanelClosingEvent(this);
				}
				Animator component = base.gameObject.GetComponent<Animator>();
				if (component != null)
				{
					component.Play("Panel_SlideOut");
				}
			}
		}

		public virtual void HidePanel()
		{
			hiding = true;
			Animator component = GetComponent<Animator>();
			if (component != null)
			{
				component.Play("Panel_SlideOut");
			}
		}

		public virtual void ShowPanel()
		{
			Animator component = GetComponent<Animator>();
			if (component != null)
			{
				component.Play("Panel_SlideIn");
			}
		}

		public void PanelOpened()
		{
			if (this.PanelOpenedEvent != null)
			{
				this.PanelOpenedEvent(this);
			}
		}

		public void PanelClosed()
		{
			if (hiding)
			{
				hiding = false;
				if (this.PanelHiddenEvent != null)
				{
					this.PanelHiddenEvent(this);
				}
				return;
			}
			if (this.PanelClosedEvent != null)
			{
				this.PanelClosedEvent(this);
			}
			if (isPanelCloseClicked && this.PanelCloseClickedEvent != null)
			{
				this.PanelCloseClickedEvent(this);
				isPanelCloseClicked = false;
			}
		}
	}
}
