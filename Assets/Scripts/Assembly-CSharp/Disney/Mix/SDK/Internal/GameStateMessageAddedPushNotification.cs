namespace Disney.Mix.SDK.Internal
{
	public class GameStateMessageAddedPushNotification : AbstractChatMessageAddedPushNotification, IChatMessageAddedPushNotification, IGameStateMessageAddedPushNotification, IPushNotification
	{
		public GameStateMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable, chatThreadId, messageId)
		{
		}
	}
}
