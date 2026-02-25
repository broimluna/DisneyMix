using LitJson;

namespace Mix.GagManagement
{
	public class GroupGagInfo
	{
		protected string mEntitlementId;

		protected string mReceiverSwid;

		public string EntitlementId
		{
			get
			{
				return mEntitlementId;
			}
		}

		public string ReceiverSwid
		{
			get
			{
				return mReceiverSwid;
			}
		}

		public GroupGagInfo(string aEntitlementId, string aReceiverSwid)
		{
			mEntitlementId = aEntitlementId;
			mReceiverSwid = aReceiverSwid;
		}

		public GroupGagInfo(string theJson)
		{
			JsonData jsonData = JsonMapper.ToObject(theJson);
			mEntitlementId = (string)jsonData["EntitlementId"];
			mReceiverSwid = (string)jsonData["ReceiverSwid"];
		}
	}
}
