using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IChatMessageSender
	{
		void SendTextMessage(IMixWebCallFactory mixWebCallFactory, IInternalTextMessage textMessage, long chatThreadId, Action<AddChatThreadTextMessageNotification> successCallback, Action<string> failureCallback);

		void ModerateTextMessage(IMixWebCallFactory mixWebCallFactory, string text, bool isTrusted, long chatThreadId, Action<ModerateTextResponse> successCallback, Action<string> failureCallback);

		void SendStickerMessage(IMixWebCallFactory mixWebCallFactory, string contentId, long chatThreadId, long? localMessageId, Action<AddChatThreadStickerMessageNotification> successCallback, Action<string> failureCallback);

		void SendGagMessage(IMixWebCallFactory mixWebCallFactory, string contentId, string targetUserId, long chatThreadId, long? localMessageId, Action<AddChatThreadGagMessageNotification> successCallback, Action<string> failureCallback);

		void SendPhotoMessage(IMixWebCallFactory mixWebCallFactory, string ugcMediaId, long chatThreadId, long? localMessageId, Action<AddChatThreadPhotoMessageNotification> successCallback, Action<string> failureCallback);

		void SendVideoMessage(IMixWebCallFactory mixWebCallFactory, string ugcMediaId, long chatThreadId, long? localMessageId, Action<AddChatThreadVideoMessageNotification> successCallback, Action<string> failureCallback);

		void SendGameStateMessage(IMixWebCallFactory mixWebCallFactory, string gameName, Dictionary<string, object> payload, long chatThreadId, long? localMessageId, Action<AddChatThreadGameStateMessageNotification> successCallback, Action<string> failureCallback);

		void UpdateGameStateMessage(IMixWebCallFactory mixWebCallFactory, long gameMessageId, Dictionary<string, object> payload, long chatThreadId, Action<UpdateChatThreadGameStateMessageResponse> successCallback, Action<string> failureCallback);
	}
}
