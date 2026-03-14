using Mix.Assets;
using Mix.DeviceDb;

namespace Mix.SequenceOperations
{
	public class AssetManagerStartOperation : SequenceOperation, ICpipeReady
	{
		public AssetManagerStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			IAssetDatabaseApi assetCacheDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi;
			MonoSingleton<AssetManager>.Instance.AssetDatabaseApi = assetCacheDocumentCollectionApi;
			assetCacheDocumentCollectionApi.CreateAssetsTable();
			assetCacheDocumentCollectionApi.CreateAssetsUniqueIndex();
			MonoSingleton<AssetManager>.Instance.Init(this);
		}

		public void OnCpipeReady(CpipeEvent aCpipeEvent)
		{
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}

		public void OnCpipeFail(CpipeEvent aCpipeEvent)
		{
			finish(OperationStatus.STATUS_SUCCESSFUL_STILL_FINALIZING);
		}
	}
}
