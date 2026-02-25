using System;

namespace LitJson
{
	public class JsonException : ApplicationException
	{
		internal JsonException(ParserToken token, Exception inner_exception)
			: base(string.Format("Invalid token '{0}' in input string", token), inner_exception)
		{
		}

		internal JsonException(int c)
			: base(string.Format("Invalid character '{0}' in input string", (char)c))
		{
		}

		public JsonException(string message)
			: base(message)
		{
		}
	}
}
