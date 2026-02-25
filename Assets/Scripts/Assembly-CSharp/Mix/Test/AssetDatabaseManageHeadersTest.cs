using System.Collections.Generic;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class AssetDatabaseManageHeadersTest : MonoBehaviour, IOperationCompleteHandler
	{
		private OperationStack operationStack;

		void IOperationCompleteHandler.OnOperationComplete(SequenceOperation aOperation)
		{
		}

		private void Start()
		{
			TestingUtils.ClearAllCache();
			operationStack = new OperationStack(this, this);
			CpipeUpdateOperation cpipeUpdateOperation = new CpipeUpdateOperation(operationStack);
			ExternalLibraryStartOperation item = new ExternalLibraryStartOperation(operationStack);
			PreloadDataStartOperation item2 = new PreloadDataStartOperation(operationStack);
			ExternalConfigurationStartOperation item3 = new ExternalConfigurationStartOperation(operationStack);
			AssetManagerStartOperation assetManagerStartOperation = new AssetManagerStartOperation(operationStack);
			AssetDatabaseManageHeadersTestHelper assetDatabaseManageHeadersTestHelper = new AssetDatabaseManageHeadersTestHelper(operationStack, this);
			Dictionary<SequenceOperation, List<SequenceOperation>> dictionary = new Dictionary<SequenceOperation, List<SequenceOperation>>();
			dictionary[assetManagerStartOperation] = new List<SequenceOperation> { item2, item, item3 };
			dictionary[cpipeUpdateOperation] = new List<SequenceOperation> { assetManagerStartOperation };
			dictionary[assetDatabaseManageHeadersTestHelper] = new List<SequenceOperation> { item3, item2, item, cpipeUpdateOperation, assetManagerStartOperation };
			operationStack.OperationDependencyChart = dictionary;
			operationStack.Add(item);
			operationStack.Add(item2);
			operationStack.Add(assetManagerStartOperation);
			operationStack.Add(item3);
			operationStack.Add(cpipeUpdateOperation);
			operationStack.Add(assetDatabaseManageHeadersTestHelper);
			operationStack.StartNextOperation();
		}

		private void Update()
		{
			operationStack.StartNextOperation();
		}
	}
}
