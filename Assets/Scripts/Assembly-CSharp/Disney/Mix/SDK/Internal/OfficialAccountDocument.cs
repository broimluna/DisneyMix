using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	[Serialized(109, new byte[] { })]
	public class OfficialAccountDocument : AbstractDocument
	{
		public static readonly string AccountIdFieldName = FieldNameGetter.Get((OfficialAccountDocument f) => f.AccountId);

		[Indexed]
		[Serialized(0, new byte[] { })]
		public string AccountId;

		[Serialized(1, new byte[] { })]
		public string DisplayName;

		[Serialized(2, new byte[] { })]
		public bool IsFollowing;

		[Serialized(3, new byte[] { })]
		public long LastUpdated;

		[Serialized(4, new byte[] { })]
		public bool IsAvailable;

		[Serialized(5, new byte[] { })]
		public bool CanUnfollow;

		public OfficialAccountDocument()
		{
		}

		public OfficialAccountDocument(string accountId, string displayName, bool isFollowing, bool isAvailable, bool canUnfollow)
		{
			AccountId = accountId;
			DisplayName = displayName;
			IsFollowing = isFollowing;
			IsAvailable = isAvailable;
			CanUnfollow = canUnfollow;
			CanUnfollow = canUnfollow;
		}
	}
}
