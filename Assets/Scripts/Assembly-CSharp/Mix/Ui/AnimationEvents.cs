using UnityEngine;

namespace Mix.Ui
{
	public class AnimationEvents : MonoBehaviour
	{
		public event OnAnimationStartEvent OnAnimationStart;

		public event OnAnimationEndEvent OnAnimationEnd;

		public event OnAnimationEvent OnAnimEvent;

		public event OnDestroyedEvent OnDestroyed;

		public void OnAnimationStartCallback()
		{
			if (this.OnAnimationStart != null)
			{
				this.OnAnimationStart();
			}
		}

		public void OnAnimationEndCallback()
		{
			if (this.OnAnimationEnd != null)
			{
				this.OnAnimationEnd();
			}
		}

		public void OnAnimationEventCallback()
		{
			if (this.OnAnimEvent != null)
			{
				this.OnAnimEvent();
			}
		}

		private void OnDestroy()
		{
			if (!this.IsNullOrDisposed() && this.OnDestroyed != null)
			{
				this.OnDestroyed();
			}
		}

		public void DisableGameObject()
		{
			base.gameObject.SetActive(false);
		}

		public void DisableGameObjectAndParent()
		{
			Transform parent = base.transform.parent;
			if (parent != null)
			{
				parent.gameObject.SetActive(false);
			}
			DisableGameObject();
		}

		public void RemoveGameObject()
		{
			Object.Destroy(base.gameObject);
		}

		public void DisableAnimator()
		{
			base.gameObject.GetComponent<Animator>().enabled = false;
		}
	}
}
