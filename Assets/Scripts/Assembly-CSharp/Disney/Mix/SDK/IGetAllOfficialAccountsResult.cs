using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IGetAllOfficialAccountsResult
	{
		bool Success { get; }

		IEnumerable<IOfficialAccount> OfficialAccounts { get; }
	}
}
