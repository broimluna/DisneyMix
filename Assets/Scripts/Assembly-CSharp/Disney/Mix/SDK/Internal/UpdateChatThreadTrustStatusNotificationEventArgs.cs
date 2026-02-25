using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class UpdateChatThreadTrustStatusNotificationEventArgs : AbstractUpdateChatThreadTrustStatusNotificationEventArgs
	{
		public override UpdateChatThreadTrustStatusNotification Notification { get; protected set; }

		public UpdateChatThreadTrustStatusNotificationEventArgs(UpdateChatThreadTrustStatusNotification notification)
		{
			Notification = notification;
		}
	}
}
