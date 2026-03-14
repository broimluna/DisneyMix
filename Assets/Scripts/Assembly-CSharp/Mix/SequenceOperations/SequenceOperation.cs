using UnityEngine;

namespace Mix.SequenceOperations
{
	public class SequenceOperation
	{
		private IOperationCompleteHandler mCaller;

		public OperationStatus status;

		private int startFrame;

		private float startTime;

		public string Name
		{
			get
			{
				return GetType().Name;
			}
		}

		public SequenceOperation(IOperationCompleteHandler aCaller)
		{
			mCaller = aCaller;
		}

		public virtual void StartOperation()
		{
			BaseStartOperation();
		}

		public void BaseStartOperation()
		{
			status = OperationStatus.STATUS_STARTING;
			startFrame = Time.frameCount;
			startTime = Time.realtimeSinceStartup;
		}

		protected void finish(OperationStatus aStatus)
		{
			status = aStatus;
			if (mCaller != null)
			{
				mCaller.OnOperationComplete(this);
			}
		}
	}
}
