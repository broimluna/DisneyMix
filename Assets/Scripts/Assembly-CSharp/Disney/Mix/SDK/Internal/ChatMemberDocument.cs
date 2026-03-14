using System;
using System.Linq.Expressions;
using System.Reflection;
using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	[Serialized(106, new byte[] { })]
	public class ChatMemberDocument : AbstractDocument
	{
		public static readonly string SwidFieldName;

		public static readonly string ChatThreadIdFieldName;

		[Serialized(0, new byte[] { })]
		[Indexed]
		public string Swid;

		[Serialized(1, new byte[] { })]
		[Indexed]
		public long ChatThreadId;

		static ChatMemberDocument()
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(ChatMemberDocument), "m");
			SwidFieldName = FieldNameGetter.Get((ChatMemberDocument m) => m.Swid);
			ChatThreadIdFieldName = FieldNameGetter.Get((ChatMemberDocument m) => m.ChatThreadId);
		}
	}
}
