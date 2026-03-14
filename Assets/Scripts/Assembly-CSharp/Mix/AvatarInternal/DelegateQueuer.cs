using System;

namespace Mix.AvatarInternal
{
	public class DelegateQueuer
	{
		private int count;

		private Action callback;

		public DelegateQueuer(Action aCallback)
		{
			count = 1;
			callback = aCallback;
		}

		public Action EnqueueAction()
		{
			count++;
			return delegate
			{
				DecrementCount();
			};
		}

		private void DecrementCount()
		{
			count--;
			if (count <= 0 && callback != null)
			{
				callback();
				callback = null;
			}
		}

		public void FinishedEnqueuing()
		{
			DecrementCount();
		}
	}
}
