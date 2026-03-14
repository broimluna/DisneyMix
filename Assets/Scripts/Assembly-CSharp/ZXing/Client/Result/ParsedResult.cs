using System.Text;

namespace ZXing.Client.Result
{
	public abstract class ParsedResult
	{
		protected string displayResultValue;

		public virtual ParsedResultType Type { get; private set; }

		public virtual string DisplayResult
		{
			get
			{
				return displayResultValue;
			}
		}

		protected ParsedResult(ParsedResultType type)
		{
			Type = type;
		}

		public override string ToString()
		{
			return DisplayResult;
		}

		public override bool Equals(object obj)
		{
			ParsedResult parsedResult = obj as ParsedResult;
			if (parsedResult == null)
			{
				return false;
			}
			return parsedResult.Type.Equals(Type) && parsedResult.DisplayResult.Equals(DisplayResult);
		}

		public override int GetHashCode()
		{
			return Type.GetHashCode() + DisplayResult.GetHashCode();
		}

		public static void maybeAppend(string value, StringBuilder result)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (result.Length > 0)
				{
					result.Append('\n');
				}
				result.Append(value);
			}
		}

		public static void maybeAppend(string[] values, StringBuilder result)
		{
			if (values != null)
			{
				foreach (string value in values)
				{
					maybeAppend(value, result);
				}
			}
		}
	}
}
