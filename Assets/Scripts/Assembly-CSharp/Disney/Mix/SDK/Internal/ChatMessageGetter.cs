using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatMessageGetter
	{
		public static void GetChatMessages(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, IEnumerable<long?> messageIds, long chatThreadId, Action<GetRecentChatThreadMessagesResponse> successCallback, Action<string> failureCallback)
		{
			try
			{
				GetSpecificChatThreadMessagesRequest getSpecificChatThreadMessagesRequest = new GetSpecificChatThreadMessagesRequest();
				getSpecificChatThreadMessagesRequest.MessageIds = messageIds.ToList();
				getSpecificChatThreadMessagesRequest.ChatThreadId = chatThreadId;
				GetSpecificChatThreadMessagesRequest request = getSpecificChatThreadMessagesRequest;
				IWebCall<GetSpecificChatThreadMessagesRequest, GetRecentChatThreadMessagesResponse> webCall = mixWebCallFactory.ChatThreadSpecificMessagesPost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetRecentChatThreadMessagesResponse> e)
				{
					GetRecentChatThreadMessagesResponse response = e.Response;
					if (ValidateResponse(response))
					{
						successCallback(response);
					}
					else
					{
						logger.Critical("Failed to validate get chat messages response: " + JsonParser.ToJson(response));
						failureCallback(response.Status);
					}
				};
				webCall.OnError += delegate(object sender, WebCallErrorEventArgs e)
				{
					failureCallback(e.Status);
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback(null);
			}
		}

		private static bool ValidateResponse(GetRecentChatThreadMessagesResponse response)
		{
			return response.Gag != null && response.GameEvent != null && response.GameState != null && response.MemberListChanged != null && response.Photo != null && response.Sticker != null && response.Text != null && response.Video != null;
		}
	}
}
