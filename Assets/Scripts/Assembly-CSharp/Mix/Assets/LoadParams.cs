using System.Collections.Generic;
using System.Threading;

namespace Mix.Assets
{
	public class LoadParams
	{
		public const string IC = "5k=";

		public int CpipeManifestVersion;

		public string Sha { get; private set; }

		public string Url { get; private set; }

		public CachePolicy CachePolicy { get; private set; }

		public ThreadPriority ThreadPriority { get; set; }

		public string CellophaneHeader { get; private set; }

		public Dictionary<string, string> Headers { get; private set; }

		public LoadParams(string aSha, string aUrl, CachePolicy aCachePolicy = CachePolicy.DefaultCacheControlProtocol, ThreadPriority aThreadPriority = ThreadPriority.Normal, Dictionary<string, string> aHeaders = null)
		{
			Sha = aSha;
			Url = aUrl;
			CachePolicy = aCachePolicy;
			ThreadPriority = aThreadPriority;
			Headers = aHeaders;
		}

		public void Print()
		{
		}
	}
}
