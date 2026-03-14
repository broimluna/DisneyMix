using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddFollowshipNotificationEventArgs : AbstractAddFollowshipNotificationEventArgs
	{
		public override AddFollowshipNotification Notification { get; protected set; }

		public AddFollowshipNotificationEventArgs(AddFollowshipNotification notification)
		{
			Notification = notification;
		}
	}
}
