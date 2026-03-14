using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class UpdateChatThreadGameStateMessageResponse : BaseResponse
	{
		public UpdateChatThreadGameStateMessageNotification UpdateGameStateNotification;

		public List<AddChatThreadGameEventMessageNotification> GameEventNotifications;
	}
}
