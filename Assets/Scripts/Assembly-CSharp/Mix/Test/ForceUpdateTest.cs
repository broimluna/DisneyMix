using System.Collections;
using System.Collections.Generic;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class ForceUpdateTest : MonoBehaviour, IOperationCompleteHandler
	{
		private bool IsUpdateReady;

		private OperationStack operationStack;

		private int numConfigUpdates;

		void IOperationCompleteHandler.OnOperationComplete(SequenceOperation aOperation)
		{
		}

		private void Start()
		{
			TestingUtils.ClearAllCache();
			TestingUtils.SetClientVersion("2.1.0-184336+CI");
			StartCoroutine(Waiter());
			ExternalizedConstants.OnExternalizedConstantsRefreshed += ConfigUpdated;
		}

		public void ConfigUpdated(object sender, ExternalizedConstantsEventArgs args)
		{
			numConfigUpdates++;
			if (numConfigUpdates >= 2)
			{
				if (ExternalizedConstants.ForceUpdateVersion != null && ExternalizedConstants.ForceUpdateVersion != "9999.0.0")
				{
					IntegrationTest.Fail("ForceUpdateVersion value is not correct. Expected 2.2.0, got " + ExternalizedConstants.ForceUpdateVersion);
				}
				StartCoroutine(WaitForForceUpdate());
			}
		}

		private void Update()
		{
			if (IsUpdateReady)
			{
				operationStack.StartNextOperation();
			}
		}

		private IEnumerator Waiter()
		{
			yield return new WaitForSeconds(1f);
			operationStack = new OperationStack(this, this);
			ForceUpdateTestHelper forceUpdateTestHelper = new ForceUpdateTestHelper(operationStack, this);
			PreloadDataStartOperation preloadDataStartOperation = new PreloadDataStartOperation(operationStack);
			ExternalLibraryStartOperation externaLibraryStartOperation = new ExternalLibraryStartOperation(operationStack);
			KeyChainManagerStartOperation keyChainManagerStartOperation = new KeyChainManagerStartOperation(operationStack);
			DeviceDbOpenOperation deviceDbOpenOperation = new DeviceDbOpenOperation(operationStack);
			AssetManagerStartOperation assetManagerStartOperation = new AssetManagerStartOperation(operationStack);
			LocalizationStartOperation localizationStartOperation = new LocalizationStartOperation(operationStack, this);
			ForceUpdateStartOperation forceUpdateStartOperation = new ForceUpdateStartOperation(operationStack);
			ExternalConfigurationStartOperation externalConfigurationStartOperation = new ExternalConfigurationStartOperation(operationStack);
			CpipeUpdateOperation cpipeUpdateOperation = new CpipeUpdateOperation(operationStack);
			Dictionary<SequenceOperation, List<SequenceOperation>> operationDependencyChart = new Dictionary<SequenceOperation, List<SequenceOperation>>();
			operationDependencyChart = new Dictionary<SequenceOperation, List<SequenceOperation>>();
			operationDependencyChart[deviceDbOpenOperation] = new List<SequenceOperation> { keyChainManagerStartOperation };
			operationDependencyChart[assetManagerStartOperation] = new List<SequenceOperation> { externaLibraryStartOperation, deviceDbOpenOperation };
			operationDependencyChart[localizationStartOperation] = new List<SequenceOperation> { assetManagerStartOperation };
			operationDependencyChart[externalConfigurationStartOperation] = new List<SequenceOperation> { assetManagerStartOperation };
			operationDependencyChart[cpipeUpdateOperation] = new List<SequenceOperation> { assetManagerStartOperation, externalConfigurationStartOperation };
			operationDependencyChart[forceUpdateStartOperation] = new List<SequenceOperation> { cpipeUpdateOperation, externaLibraryStartOperation, externalConfigurationStartOperation, localizationStartOperation, assetManagerStartOperation };
			operationDependencyChart[forceUpdateTestHelper] = new List<SequenceOperation> { forceUpdateStartOperation, localizationStartOperation, externalConfigurationStartOperation, cpipeUpdateOperation };
			operationStack.OperationDependencyChart = operationDependencyChart;
			operationStack.Add(preloadDataStartOperation);
			operationStack.Add(keyChainManagerStartOperation);
			operationStack.Add(deviceDbOpenOperation);
			operationStack.Add(externaLibraryStartOperation);
			operationStack.Add(assetManagerStartOperation);
			operationStack.Add(localizationStartOperation);
			operationStack.Add(forceUpdateStartOperation);
			operationStack.Add(externalConfigurationStartOperation);
			operationStack.Add(cpipeUpdateOperation);
			operationStack.Add(forceUpdateTestHelper);
			IsUpdateReady = true;
		}

		private IEnumerator WaitForForceUpdate()
		{
			yield return new WaitForSeconds(10f);
			if (ExternalizedConstants.ForceUpdateVersion.Equals("9999.0.0"))
			{
				IntegrationTest.Pass();
			}
			else
			{
				IntegrationTest.Fail("ForceUpdateVersion value is not correct. Expected 2.2.0, got " + ExternalizedConstants.ForceUpdateVersion);
			}
		}
	}
}
