using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.Data;

namespace Mix.Entitlements
{
	public class OfficialAccountComparer : IComparer<IOfficialAccount>
	{
		public int Compare(IOfficialAccount x, IOfficialAccount y)
		{
			Official_Account officialAccount = Singleton<EntitlementsManager>.Instance.GetOfficialAccount(x.AccountId);
			Official_Account officialAccount2 = Singleton<EntitlementsManager>.Instance.GetOfficialAccount(y.AccountId);
			if (officialAccount == null || officialAccount2 == null)
			{
				return 0;
			}
			return officialAccount.GetOrder().CompareTo(officialAccount2.GetOrder());
		}
	}
}
