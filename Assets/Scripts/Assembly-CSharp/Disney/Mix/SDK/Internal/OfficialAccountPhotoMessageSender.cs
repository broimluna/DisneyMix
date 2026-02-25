using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountPhotoMessageSender
	{
		public static void Publish(AbstractLogger logger, string officialAccountId, string caption, IMixWebCallFactory mixWebCallFactory, Action successCallback, Action failureCallback)
		{
			try
			{
				OfficialAccountPublishPhotoTestRequest officialAccountPublishPhotoTestRequest = new OfficialAccountPublishPhotoTestRequest();
				officialAccountPublishPhotoTestRequest.AccountId = officialAccountId;
				officialAccountPublishPhotoTestRequest.Caption = caption;
				OfficialAccountPublishPhotoTestRequest request = officialAccountPublishPhotoTestRequest;
				IWebCall<OfficialAccountPublishPhotoTestRequest, BaseResponse> webCall = mixWebCallFactory.IntegrationTestSupportOfficialAccountPublishPhotoPost(request);
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
