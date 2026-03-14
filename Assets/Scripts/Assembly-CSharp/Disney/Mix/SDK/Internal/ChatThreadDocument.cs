using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	[Serialized(105, new byte[] { 111 })]
	public class ChatThreadDocument : AbstractDocument
	{
		public static readonly string ChatThreadIdFieldName = FieldNameGetter.Get((ChatThreadDocument c) => c.ChatThreadId);

		[Serialized(0, new byte[] { })]
		[Indexed]
		public long ChatThreadId;

		[Serialized(1, new byte[] { })]
		public byte ChatThreadType;

		[Serialized(2, new byte[] { })]
		public bool IsTrusted;

		[Serialized(3, new byte[] { })]
		public string Nickname;

		[Serialized(4, new byte[] { })]
		public uint UnreadMessageCount;

		[Serialized(5, new byte[] { })]
		public bool AreSequenceNumbersIndexed;

		[Serialized(6, new byte[] { })]
		public long LatestSequenceNumber;

		[Serialized(50, new byte[] { })]
		public string OfficialAccountId;
	}
}
