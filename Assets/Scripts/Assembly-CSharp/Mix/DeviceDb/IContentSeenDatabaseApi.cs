namespace Mix.DeviceDb
{
	public interface IContentSeenDatabaseApi
	{
		bool IsContentSeen(string aContentId);

		void SetContentSeen(string aContentId);

		void ClearContentSeen(string aContentId);
	}
}
