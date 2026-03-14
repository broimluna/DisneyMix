namespace Disney.Mix.SDK.Internal
{
	public class GameEventMessageAddedPushNotification : AbstractChatMessageAddedPushNotification, IChatMessageAddedPushNotification, IGameEventMessageAddedPushNotification, IPushNotification
	{
		public GameEventMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable, chatThreadId, messageId)
		{
		}
	}
}
