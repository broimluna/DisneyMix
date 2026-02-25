using Disney.MobileNetwork;

namespace Mix
{
	public class ReferralStore : Singleton<ReferralStore>
	{
		public void ShowReferrals()
		{
			Service.Get<ReferralStoreManager>().Show();
		}
	}
}
