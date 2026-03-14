using Mix.Assets;

namespace Mix.SequenceOperations
{
	public class CpipeUpdateOperation : SequenceOperation
	{
		public CpipeUpdateOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			MonoSingleton<AssetManager>.Instance.cpipeManager.OnCpipeLoaded += CpipeStartupEventCallback;
			MonoSingleton<AssetManager>.Instance.cpipeManager.LoadCpipe(CachePolicy.DefaultCacheControlProtocol);
		}

		public void HandleOnExternalizedConstantsRefreshed(object sender, ExternalizedConstantsEventArgs args)
		{
			CpipeUpdateOperationComplete();
		}

		public void CpipeUpdateOperationComplete()
		{
			ExternalizedConstants.OnExternalizedConstantsRefreshed -= HandleOnExternalizedConstantsRefreshed;
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}

		public void CpipeStartupEventCallback(CpipeEvent aCpipeEvent)
		{
			MonoSingleton<AssetManager>.Instance.cpipeManager.OnCpipeLoaded -= CpipeStartupEventCallback;
			switch (aCpipeEvent)
			{
			case CpipeEvent.AuthToken:
				break;
			case CpipeEvent.AuthTokenFailed:
				CpipeUpdateOperationComplete();
				break;
			case CpipeEvent.CpipeData:
				if (MonoSingleton<AssetManager>.Instance.LatestManifestVersionNewerThanCachedVersion(ConfigurationManager.RelativePathToConfigFile))
				{
					ExternalizedConstants.OnExternalizedConstantsRefreshed += HandleOnExternalizedConstantsRefreshed;
				}
				else
				{
					CpipeUpdateOperationComplete();
				}
				break;
			case CpipeEvent.CpipeDataFailed:
				CpipeUpdateOperationComplete();
				break;
			case CpipeEvent.CpipeUnchanged:
				CpipeUpdateOperationComplete();
				break;
			}
		}
	}
}
