using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LitJson
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class JsonIgnore : Attribute
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private JsonIgnoreWhen _003CUsage_003Ek__BackingField;

		public JsonIgnoreWhen Usage
		{
			[CompilerGenerated]
			get
			{
				return _003CUsage_003Ek__BackingField;
			}
		}
	}
}
