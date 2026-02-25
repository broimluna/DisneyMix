namespace Mix.DeviceDb
{
	public interface IKeyValDatabaseApi
	{
		void RemoveUserKey(string key);

		void RemoveDeviceKey(string key);

		string LoadUserValue(string key);

		void SaveUserValue(string key, string value);

		bool LoadUserValueAsBool(string key, bool defaultVal = false);

		void SaveUserValueFromBool(string key, bool value);

		float LoadUserValueAsFloat(string key, float defaultVal = 0f);

		void SaveUserValueFromFloat(string key, float value);

		int LoadUserValueAsInt(string key, int defaultVal = 0);

		void SaveUserValueFromInt(string key, int value);

		string LoadDeviceValue(string key);

		void SaveDeviceValue(string key, string value);

		bool LoadDeviceValueAsBool(string key, bool defaultVal);

		void SaveDeviceValueFromBool(string key, bool value);

		float LoadDeviceValueAsFloat(string key, float defaultVal);

		void SaveDeviceValueFromFloat(string key, float value);

		int LoadDeviceValueAsInt(string key, int defaultVal);

		void SaveDeviceValueFromInt(string key, int value);
	}
}
