using Mix.DeviceDb;

namespace Mix.SequenceOperations
{
	public class DeviceDbOpenOperation : SequenceOperation
	{
		public DeviceDbOpenOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			AssetCacheDocumentCollectionApi assetCacheDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi;
			KeyValDocumentCollectionApi keyValDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi;
			ChatMetaDataDocumentCollectionApi chatMetaDataDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.chatMetaDataDocumentCollectionApi;
			AvatarSnapshotDocumentCollectionApi avatarSnapshotDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.avatarSnapshotDocumentCollectionApi;
			AvatarTextureDocumentCollectionApi avatarTextureDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.avatarTextureDocumentCollectionApi;
			FakeFirstFriendDocumentCollectionApi fakeFirstFriendDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.fakeFirstFriendDocumentCollectionApi;
			OfflineQueueDocumentCollectionApi offlineQueueDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.offlineQueueDocumentCollectionApi;
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}
	}
}
