using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class DisplayNameUpdater
	{
		public static void UpdateDisplayName(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, string displayName, Action<IUpdateDisplayNameResult> callback)
		{
			try
			{
				SetDisplayNameRequest setDisplayNameRequest = new SetDisplayNameRequest();
				setDisplayNameRequest.DisplayName = displayName;
				SetDisplayNameRequest request = setDisplayNameRequest;
				IWebCall<SetDisplayNameRequest, SetDisplayNameResponse> webCall = mixWebCallFactory.DisplaynamePut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<SetDisplayNameResponse> e)
				{
					SetDisplayNameResponse response = e.Response;
					if (ValidateSetDisplayNameResponse(response))
					{
						callback(new UpdateDisplayNameResult(true));
					}
					else
					{
						logger.Critical("Failed to validate update display name response!");
						callback(new UpdateDisplayNameResult(false));
					}
				};
				webCall.OnError += delegate(object sender, WebCallErrorEventArgs e)
				{
					switch (e.Status)
					{
					case "DISPLAYNAME_MODERATION_FAILED":
						callback(new UpdateDisplayNameFailedModerationResult(false));
						break;
					case "DISPLAYNAME_ASSIGNMENT_FAILED":
						callback(new UpdateDisplayNameExistsResult(false));
						break;
					default:
						callback(new UpdateDisplayNameResult(false));
						break;
					}
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new UpdateDisplayNameResult(false));
			}
		}

		private static bool ValidateSetDisplayNameResponse(SetDisplayNameResponse response)
		{
			return response.DisplayName != null;
		}
	}
}
