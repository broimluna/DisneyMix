using System.Collections.Generic;
using Disney.Manimal.Async.Unity;
using Mix.SequenceOperations;
using UnityEngine;

public class OperationStack : List<SequenceOperation>, IOperationCompleteHandler
{
	private sealed class OnOperationComplete_003Ec__AnonStorey28E
	{
		internal SequenceOperation aOperation;

		internal OperationStack _003C_003Ef__this;

		internal void _003C_003Em__563(object state)
		{
			_003C_003Ef__this.handleSequenceOperation(aOperation);
		}
	}

	private IOperationCompleteHandler mOptionalCaller;

	private UnitySynchronizationContext syncContext;

	public Dictionary<SequenceOperation, List<SequenceOperation>> OperationDependencyChart { get; set; }

	public OperationStack(IOperationCompleteHandler aOptionalCaller = null, MonoBehaviour monoEngine = null)
	{
		mOptionalCaller = aOptionalCaller;
		if (monoEngine != null)
		{
			syncContext = new UnitySynchronizationContext(monoEngine);
		}
	}

	void IOperationCompleteHandler.OnOperationComplete(SequenceOperation aOperation)
	{
		OnOperationComplete_003Ec__AnonStorey28E CS_0024_003C_003E8__locals5 = new OnOperationComplete_003Ec__AnonStorey28E();
		CS_0024_003C_003E8__locals5.aOperation = aOperation;
		CS_0024_003C_003E8__locals5._003C_003Ef__this = this;
		if (syncContext != null)
		{
			syncContext.Post(delegate
			{
				CS_0024_003C_003E8__locals5._003C_003Ef__this.handleSequenceOperation(CS_0024_003C_003E8__locals5.aOperation);
			}, null);
		}
		else
		{
			handleSequenceOperation(CS_0024_003C_003E8__locals5.aOperation);
		}
	}

	public void StartNextOperation()
	{
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SequenceOperation current = enumerator.Current;
				if (current.status != OperationStatus.STATUS_NOT_STARTED && current.status != OperationStatus.STATUS_FAILED)
				{
					continue;
				}
				List<SequenceOperation> value = null;
				bool flag = false;
				if (OperationDependencyChart != null && OperationDependencyChart.TryGetValue(current, out value))
				{
					foreach (SequenceOperation item in value)
					{
						if (item.status != OperationStatus.STATUS_SUCCESSFUL && item.status != OperationStatus.STATUS_SUCCESSFUL_STILL_FINALIZING)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					continue;
				}
				current.StartOperation();
				break;
			}
		}
	}

	private void handleSequenceOperation(SequenceOperation aOperation)
	{
		if (aOperation.status != OperationStatus.STATUS_SUCCESSFUL && aOperation.status != OperationStatus.STATUS_SUCCESSFUL_STILL_FINALIZING)
		{
			Application.Quit();
		}
		if (mOptionalCaller != null)
		{
			mOptionalCaller.OnOperationComplete(aOperation);
		}
	}
}
