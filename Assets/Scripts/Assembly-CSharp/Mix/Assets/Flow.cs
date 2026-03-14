using Mix.Connectivity;

namespace Mix.Assets
{
	public class Flow
	{
		public const string CHECK_HEADERS = "CheckHeaders";

		public const string LOAD_FROM_WEB = "LoadFromWeb";

		public const string LOAD_FROM_DB = "LoadFromDB";

		public const string LOAD_FROM_BUNDLE = "LoadFromBundle";

		public const string DESTROY = "Destroy";

		public const string ERROR = "ErrorHandler";

		public const string DEFAULT_CASE = "defaultCase";

		public IAssetManager assetManager;

		protected IFlow control;

		protected LoadParams loadParams;

		private bool WasInternetConnectivitySet;

		private bool _IsInternetConnected = true;

		public int HttpStatus { get; set; }

		public string MethodName { get; set; }

		public bool IsBundled { get; set; }

		public bool IsCached { get; set; }

		public bool IsHeaderStale { get; set; }

		public bool IsCallerCalled { get; set; }

		public bool IsBackgroundLoad { get; private set; }

		public bool IsAlreadyLoadFromDB { get; set; }

		public bool IsAlreadyLoadFromBundle { get; set; }

		public bool IsAlreadyLoadFromWeb { get; set; }

		public bool IsInternetConnected
		{
			get
			{
				if (WasInternetConnectivitySet)
				{
					return _IsInternetConnected;
				}
				return MonoSingleton<ConnectionManager>.Instance.IsConnected;
			}
			set
			{
				WasInternetConnectivitySet = true;
				_IsInternetConnected = value;
			}
		}

		public Flow(IFlow aControl, LoadParams aLoadParams)
		{
			HttpStatus = -1;
			MethodName = string.Empty;
			IsBundled = false;
			IsCached = false;
			IsHeaderStale = true;
			IsCallerCalled = false;
			IsBackgroundLoad = false;
			IsAlreadyLoadFromDB = false;
			IsAlreadyLoadFromBundle = false;
			IsAlreadyLoadFromWeb = false;
			control = aControl;
			loadParams = aLoadParams;
		}

		public void Init()
		{
			switch (loadParams.CachePolicy)
			{
			case CachePolicy.DefaultCacheControlProtocol:
			case CachePolicy.CacheThenBundleThenDownload:
			case CachePolicy.CacheThenBundle:
				MethodName = "CheckHeaders";
				break;
			default:
				MethodName = "ErrorHandler";
				if (!AssetManager.IS_DEBUG)
				{
				}
				break;
			}
			control.ParseNextFlowState();
		}

		public void UpdateFlow()
		{
			if (AssetManager.IsRunLocal || (AssetManager.IS_DEMO && control.IsPathLocalForIsDemoMode(loadParams.Url)))
			{
				if (IsCached && !IsAlreadyLoadFromDB && !IsCallerCalled)
				{
					MethodName = "LoadFromDB";
				}
				else if (!IsAlreadyLoadFromBundle && !IsCallerCalled)
				{
					MethodName = "LoadFromBundle";
				}
				else
				{
					MethodName = "Destroy";
				}
				control.ParseNextFlowState();
				return;
			}
			if (AssetManager.IS_DEBUG)
			{
				Print();
			}
			switch (MethodName)
			{
			case "LoadFromWeb":
			case "CheckHeaders":
			case "LoadFromBundle":
			case "LoadFromDB":
			case "defaultCase":
				if (loadParams.CachePolicy == CachePolicy.DefaultCacheControlProtocol)
				{
					if (IsCached && (!IsInternetConnected || !IsHeaderStale || (HttpStatus != -1 && HttpStatus != 200)) && !IsAlreadyLoadFromDB && !IsCallerCalled)
					{
						MethodName = "LoadFromDB";
					}
					else if (!IsAlreadyLoadFromWeb && !IsCallerCalled)
					{
						MethodName = "LoadFromWeb";
					}
					else if (!IsAlreadyLoadFromBundle && !IsCallerCalled)
					{
						MethodName = "LoadFromBundle";
					}
					else
					{
						MethodName = "Destroy";
					}
				}
				else if (loadParams.CachePolicy == CachePolicy.CacheThenBundleThenDownload)
				{
					if (IsCached && !IsAlreadyLoadFromDB && !IsCallerCalled)
					{
						MethodName = "LoadFromDB";
					}
					else if (IsBundled && !IsAlreadyLoadFromBundle && !IsCallerCalled)
					{
						MethodName = "LoadFromBundle";
					}
					else if (!IsAlreadyLoadFromWeb)
					{
						if (IsCallerCalled)
						{
							IsBackgroundLoad = true;
						}
						MethodName = "LoadFromWeb";
					}
					else
					{
						MethodName = "Destroy";
					}
				}
				else if (loadParams.CachePolicy == CachePolicy.CacheThenBundle)
				{
					if (IsCached && !IsAlreadyLoadFromDB && !IsCallerCalled)
					{
						MethodName = "LoadFromDB";
					}
					else if (IsBundled && !IsAlreadyLoadFromBundle && !IsCallerCalled)
					{
						MethodName = "LoadFromBundle";
					}
					else
					{
						MethodName = "Destroy";
					}
				}
				else
				{
					MethodName = "Destroy";
				}
				break;
			}
			control.ParseNextFlowState();
		}

		public void Destroy()
		{
			control = null;
			loadParams = null;
			assetManager = null;
		}

		public void Print()
		{
			loadParams.Print();
		}
	}
}
