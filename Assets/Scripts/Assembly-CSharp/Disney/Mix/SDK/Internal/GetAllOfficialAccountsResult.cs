using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class GetAllOfficialAccountsResult : IGetAllOfficialAccountsResult
	{
		public bool Success { get; private set; }

		public IEnumerable<IOfficialAccount> OfficialAccounts { get; private set; }

		public GetAllOfficialAccountsResult(bool success, IEnumerable<IOfficialAccount> officialAccounts)
		{
			Success = success;
			OfficialAccounts = officialAccounts;
		}
	}
}
