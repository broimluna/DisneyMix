using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class RecentChatThreadMessageRetriever
	{
		public static void RetrieveMessages(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, IEnumerable<long?> lowerBoundSequenceNumbers, Action<GetChatThreadMessagesResponse> successCallback, Action failureCallback)
		{
			try
			{
				GetChatThreadsRecentMessagesRequest getChatThreadsRecentMessagesRequest = new GetChatThreadsRecentMessagesRequest();
				getChatThreadsRecentMessagesRequest.LowerBoundSequenceNumbers = lowerBoundSequenceNumbers.ToList();
				getChatThreadsRecentMessagesRequest.MaxMessagesPerChatThread = 1;
				GetChatThreadsRecentMessagesRequest request = getChatThreadsRecentMessagesRequest;
				IWebCall<GetChatThreadsRecentMessagesRequest, GetChatThreadMessagesResponse> webCall = mixWebCallFactory.ChatThreadMessagesRecentPost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetChatThreadMessagesResponse> e)
				{
					GetChatThreadMessagesResponse response = e.Response;
					if (ValidateResponse(response))
					{
						successCallback(response);
					}
					else
					{
						logger.Critical("Failed to validate get recent chat messages response: " + JsonParser.ToJson(response));
						failureCallback();
					}
				};
				webCall.OnError += delegate
				{
					failureCallback();
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback();
			}
		}

		private static bool ValidateResponse(GetChatThreadMessagesResponse response)
		{
			return response.Gag != null && response.GameEvent != null && response.GameState != null && response.MemberListChanged != null && response.Photo != null && response.Sticker != null && response.Text != null && response.Video != null;
		}
	}
}
