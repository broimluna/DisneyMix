using System;
using System.Linq.Expressions;
using System.Reflection;
using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	[Serialized(107, new byte[] { })]
	public class ChatMessageDocument : AbstractDocument
	{
		public static readonly string ChatMessageIdFieldName;

		public static readonly string CreatedFieldName;

		public static readonly string SequenceNumberFieldName;

		[Indexed]
		[Serialized(0, new byte[] { })]
		public long ChatMessageId;

		[Serialized(1, new byte[] { })]
		public string SenderId;

		[Indexed]
		[Serialized(2, new byte[] { })]
		public long Created;

		[Serialized(3, new byte[] { })]
		public string ChatMessageType;

		[Serialized(4, new byte[] { })]
		public string Payload;

		[Serialized(5, new byte[] { })]
		public bool IsSent;

		[Serialized(6, new byte[] { })]
		public long LocalChatMessageId;

		[Serialized(7, new byte[] { })]
		[Indexed]
		public long SequenceNumber;

		static ChatMessageDocument()
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(ChatMessageDocument), "m");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(ChatMessageDocument), "m");
			ChatMessageIdFieldName = FieldNameGetter.Get((ChatMessageDocument m) => m.ChatMessageId);
			CreatedFieldName = FieldNameGetter.Get((ChatMessageDocument m) => m.Created);
			SequenceNumberFieldName = FieldNameGetter.Get((ChatMessageDocument m) => m.SequenceNumber);
		}
	}
}
