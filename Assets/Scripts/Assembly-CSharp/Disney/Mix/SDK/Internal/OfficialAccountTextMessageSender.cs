using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountTextMessageSender
	{
		public static void Publish(AbstractLogger logger, string officialAccountId, string text, IMixWebCallFactory mixWebCallFactory, Action successCallback, Action failureCallback)
		{
			try
			{
				OfficialAccountPublishTextTestRequest officialAccountPublishTextTestRequest = new OfficialAccountPublishTextTestRequest();
				officialAccountPublishTextTestRequest.AccountId = officialAccountId;
				officialAccountPublishTextTestRequest.Text = text;
				OfficialAccountPublishTextTestRequest request = officialAccountPublishTextTestRequest;
				IWebCall<OfficialAccountPublishTextTestRequest, BaseResponse> webCall = mixWebCallFactory.IntegrationTestSupportOfficialAccountPublishTextPost(request);
				webCall.OnResponse += delegate
				{
					successCallback();
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
	}
}
