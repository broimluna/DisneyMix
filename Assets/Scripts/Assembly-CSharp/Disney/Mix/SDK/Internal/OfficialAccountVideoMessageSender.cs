using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountVideoMessageSender
	{
		public static void Publish(AbstractLogger logger, string officialAccountId, string caption, IMixWebCallFactory mixWebCallFactory, Action successCallback, Action failureCallback)
		{
			try
			{
				OfficialAccountPublishVideoTestRequest officialAccountPublishVideoTestRequest = new OfficialAccountPublishVideoTestRequest();
				officialAccountPublishVideoTestRequest.AccountId = officialAccountId;
				officialAccountPublishVideoTestRequest.Caption = caption;
				OfficialAccountPublishVideoTestRequest request = officialAccountPublishVideoTestRequest;
				IWebCall<OfficialAccountPublishVideoTestRequest, BaseResponse> webCall = mixWebCallFactory.IntegrationTestSupportOfficialAccountPublishVideoPost(request);
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
