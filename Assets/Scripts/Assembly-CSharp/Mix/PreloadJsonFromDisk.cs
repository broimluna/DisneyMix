using System;
using Mix.Data;

namespace Mix
{
	public class PreloadJsonFromDisk : IJsonSerializer
	{
		private Action<bool, object> callback;

		public PreloadJsonFromDisk(string aFileContents, bool aReadFromFile, Func<string, object> aFunc, Action<bool, object> aCallback)
		{
			callback = aCallback;
			new JsonSerializer(this, aFileContents, aReadFromFile, aFunc);
		}

		void IJsonSerializer.OnJsonSerializer(object data)
		{
			callback(data != null, data);
		}
	}
}
