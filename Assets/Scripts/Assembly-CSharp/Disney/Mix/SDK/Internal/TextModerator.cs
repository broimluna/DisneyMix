using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class TextModerator
	{
		public static void ModerateText(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, string text, bool isTrusted, Action<ModerateTextResponse> successCallback, Action failureCallback)
		{
			try
			{
				ModerateTextRequest moderateTextRequest = new ModerateTextRequest();
				moderateTextRequest.Text = text;
				moderateTextRequest.ModerationPolicy = ((!isTrusted) ? "UnTrusted" : "Trusted");
				ModerateTextRequest request = moderateTextRequest;
				IWebCall<ModerateTextRequest, ModerateTextResponse> webCall = mixWebCallFactory.ModerationTextPut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<ModerateTextResponse> e)
				{
					ModerateTextResponse response = e.Response;
					if (ValidateModerateTextResponse(response))
					{
						successCallback(response);
					}
					else
					{
						logger.Critical("Failed to validate moderate text response: " + JsonParser.ToJson(response));
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

		private static bool ValidateModerateTextResponse(ModerateTextResponse response)
		{
			bool? moderated = response.Moderated;
			return moderated.HasValue && response.Text != null;
		}
	}
}
