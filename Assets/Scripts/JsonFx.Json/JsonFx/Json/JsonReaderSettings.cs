using System;

namespace JsonFx.Json
{
	public class JsonReaderSettings
	{
		public TypeCoercionUtility Coercion = new TypeCoercionUtility();

		private bool allowUnquotedObjectKeys;

		private string typeHintName;

		public bool AllowUnquotedObjectKeys
		{
			get
			{
				return allowUnquotedObjectKeys;
			}
		}

		public bool IsTypeHintName(string name)
		{
			return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(typeHintName) && StringComparer.Ordinal.Equals(typeHintName, name);
		}
	}
}
