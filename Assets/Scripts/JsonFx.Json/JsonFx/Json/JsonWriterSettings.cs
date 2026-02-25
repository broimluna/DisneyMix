using System;

namespace JsonFx.Json
{
	public class JsonWriterSettings
	{
		private WriteDelegate<DateTime> dateTimeSerializer;

		private int maxDepth = 25;

		private string newLine = Environment.NewLine;

		private bool prettyPrint;

		private string tab = "\t";

		private string typeHintName;

		private bool useXmlSerializationAttributes;

		public virtual string TypeHintName
		{
			get
			{
				return typeHintName;
			}
		}

		public virtual bool PrettyPrint
		{
			get
			{
				return prettyPrint;
			}
		}

		public virtual string Tab
		{
			get
			{
				return tab;
			}
		}

		public virtual string NewLine
		{
			get
			{
				return newLine;
			}
		}

		public virtual int MaxDepth
		{
			get
			{
				return maxDepth;
			}
		}

		public virtual bool UseXmlSerializationAttributes
		{
			get
			{
				return useXmlSerializationAttributes;
			}
		}

		public virtual WriteDelegate<DateTime> DateTimeSerializer
		{
			get
			{
				return dateTimeSerializer;
			}
		}
	}
}
