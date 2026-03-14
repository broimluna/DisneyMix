using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class RemoveFollowshipNotificationEventArgs : AbstractRemoveFollowshipNotificationEventArgs
	{
		public override RemoveFollowshipNotification Notification { get; protected set; }

		public RemoveFollowshipNotificationEventArgs(RemoveFollowshipNotification notification)
		{
			Notification = notification;
		}
	}
}
