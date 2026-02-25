namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractChatMessageAddedPushNotification : AbstractPushNotification, IChatMessageAddedPushNotification, IPushNotification
	{
		public string ChatThreadId { get; private set; }

		public string MessageId { get; private set; }

		protected AbstractChatMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable)
		{
			ChatThreadId = chatThreadId;
			MessageId = messageId;
		}
	}
}
