using System;

namespace JsonFx.Json
{
	public class JsonSerializationException : InvalidOperationException
	{
		public JsonSerializationException(string message)
			: base(message)
		{
		}
	}
}
