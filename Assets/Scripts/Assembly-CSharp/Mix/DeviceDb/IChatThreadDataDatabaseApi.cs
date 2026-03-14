namespace Mix.DeviceDb
{
	public interface IChatThreadDataDatabaseApi
	{
		void AddMostRecentWhenAllDone(string key, long aTime, string aMessageId);

		ChatThreadDocument GetMostRecentWhenAllDone(string key);

		void Clear();
	}
}
