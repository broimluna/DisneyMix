using System.Collections;
using System.Collections.Generic;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class UpdateAsset : MonoBehaviour, IOperationCompleteHandler
	{
		private bool IsUpdateReady;

		private OperationStack operationStack;

		void IOperationCompleteHandler.OnOperationComplete(SequenceOperation aOperation)
		{
		}

		private void Start()
		{
			TestingUtils.ClearAllCache();
			TestingUtils.SetClientVersion("2.0.2-182508+CI");
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
			UpdateAssetHelper updateAssetHelper = new UpdateAssetHelper(operationStack, this);
			Dictionary<SequenceOperation, List<SequenceOperation>> operationDependencyChart = new Dictionary<SequenceOperation, List<SequenceOperation>>();
			operationDependencyChart[assetManagerStartSequence] = new List<SequenceOperation> { preloadDataStartSequence, externalLibrayStartSequence, configStartSequence };
			operationDependencyChart[cpipeUpdateStartSequence] = new List<SequenceOperation> { assetManagerStartSequence };
			operationDependencyChart[updateAssetHelper] = new List<SequenceOperation> { configStartSequence, preloadDataStartSequence, externalLibrayStartSequence, cpipeUpdateStartSequence, assetManagerStartSequence };
			operationStack.OperationDependencyChart = operationDependencyChart;
			operationStack.Add(externalLibrayStartSequence);
			operationStack.Add(preloadDataStartSequence);
			operationStack.Add(assetManagerStartSequence);
			operationStack.Add(configStartSequence);
			operationStack.Add(cpipeUpdateStartSequence);
			operationStack.Add(updateAssetHelper);
			operationStack.StartNextOperation();
			IsUpdateReady = true;
		}
	}
}
