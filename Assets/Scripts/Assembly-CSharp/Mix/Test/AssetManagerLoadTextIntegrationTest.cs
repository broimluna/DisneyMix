using System.Collections;
using System.Collections.Generic;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class AssetManagerLoadTextIntegrationTest : MonoBehaviour, IOperationCompleteHandler
	{
		private OperationStack operationStack;

		private bool IsUpdateReady;

		void IOperationCompleteHandler.OnOperationComplete(SequenceOperation aOperation)
		{
		}

		private void Start()
		{
			Application.TestingDirectory = string.Empty;
			TestingUtils.ClearAllCache();
			StartCoroutine(waiter());
		}

		private void Update()
		{
			if (IsUpdateReady)
			{
				operationStack.StartNextOperation();
			}
		}

		private IEnumerator waiter()
		{
			yield return new WaitForSeconds(1f);
			operationStack = new OperationStack(this, this);
			CpipeUpdateOperation cpipeUpdateStartSequence = new CpipeUpdateOperation(operationStack);
			ExternalLibraryStartOperation externalLibrayStartSequence = new ExternalLibraryStartOperation(operationStack);
			PreloadDataStartOperation preloadDataStartSequence = new PreloadDataStartOperation(operationStack);
			ExternalConfigurationStartOperation configStartSequence = new ExternalConfigurationStartOperation(operationStack);
			AssetManagerStartOperation assetManagerStartSequence = new AssetManagerStartOperation(operationStack);
			AssetManagerLoadTextIntegrationTestHelper assetManagerLoadTextIntegrationTestHelper = new AssetManagerLoadTextIntegrationTestHelper(operationStack, this);
			Dictionary<SequenceOperation, List<SequenceOperation>> operationDependencyChart = new Dictionary<SequenceOperation, List<SequenceOperation>>();
			operationDependencyChart[assetManagerStartSequence] = new List<SequenceOperation> { preloadDataStartSequence, externalLibrayStartSequence, configStartSequence };
			operationDependencyChart[cpipeUpdateStartSequence] = new List<SequenceOperation> { assetManagerStartSequence };
			operationDependencyChart[assetManagerLoadTextIntegrationTestHelper] = new List<SequenceOperation> { configStartSequence, preloadDataStartSequence, externalLibrayStartSequence, cpipeUpdateStartSequence, assetManagerStartSequence };
			operationStack.OperationDependencyChart = operationDependencyChart;
			operationStack.Add(externalLibrayStartSequence);
			operationStack.Add(preloadDataStartSequence);
			operationStack.Add(assetManagerStartSequence);
			operationStack.Add(configStartSequence);
			operationStack.Add(cpipeUpdateStartSequence);
			operationStack.Add(assetManagerLoadTextIntegrationTestHelper);
			operationStack.StartNextOperation();
			IsUpdateReady = true;
		}
	}
}
