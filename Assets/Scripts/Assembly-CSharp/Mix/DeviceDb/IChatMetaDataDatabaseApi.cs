namespace Mix.DeviceDb
{
	public interface IChatMetaDataDatabaseApi
	{
		void AddHeight(string key, float height);

		float GetHeight(string key);

		void Clear();
	}
}
