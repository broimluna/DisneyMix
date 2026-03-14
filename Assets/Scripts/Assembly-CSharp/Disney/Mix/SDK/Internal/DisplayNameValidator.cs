using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.GuestControllerDomain;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class DisplayNameValidator
	{
		public static void ValidateDisplayName(AbstractLogger logger, IGuestControllerClient guestControllerClient, IMixWebCallFactory mixWebCallFactory, string displayName, Action<IValidateDisplayNameResult> callback)
		{
			try
			{
				ModerateTextRequest moderateTextRequest = new ModerateTextRequest();
				moderateTextRequest.ModerationPolicy = "DisplayName";
				moderateTextRequest.Text = displayName;
				ModerateTextRequest request = moderateTextRequest;
				IWebCall<ModerateTextRequest, ModerateTextResponse> webCall = mixWebCallFactory.ModerationTextPut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<ModerateTextResponse> e)
				{
					ModerateTextResponse response = e.Response;
					if (ValidateModerateTextResponse(response))
					{
						if (response.Moderated.Value)
						{
							callback(new ValidateDisplayNameFailedModerationResult(false));
						}
						else
						{
							CheckForValidation(guestControllerClient, displayName, callback);
						}
					}
					else
					{
						logger.Critical("Failed to validate moderate display name response!");
						callback(new ValidateDisplayNameResult(false));
					}
				};
				webCall.OnError += delegate
				{
					callback(new ValidateDisplayNameResult(false));
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new ValidateDisplayNameExistsResult(false));
			}
		}

		public static void ValidateDisplayNames(AbstractLogger logger, IMixWebCallFactory mixWebCallFactory, IEnumerable<string> displayNames, Action<IValidateDisplayNamesResult> callback)
		{
			try
			{
				List<string> list = new List<string>();
				list.AddRange(displayNames);
				ValidateDisplayNamesRequest validateRequest = new ValidateDisplayNamesRequest
				{
					DisplayNames = list
				};
				IWebCall<ValidateDisplayNamesRequest, ValidateDisplayNamesResponse> webCall = mixWebCallFactory.DisplaynameValidatePost(validateRequest);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<ValidateDisplayNamesResponse> e)
				{
					ValidateDisplayNamesResponse response = e.Response;
					if (response.DisplayNames != null)
					{
						callback(new ValidateDisplayNamesResult(true, response.DisplayNames));
					}
					else
					{
						string text = string.Join(",", validateRequest.DisplayNames.ToArray());
						logger.Critical("Failed to validate display names " + text);
						callback(new ValidateDisplayNamesResult(false, Enumerable.Empty<string>()));
					}
				};
				webCall.OnError += delegate
				{
					callback(new ValidateDisplayNamesResult(false, Enumerable.Empty<string>()));
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new ValidateDisplayNamesResult(false, Enumerable.Empty<string>()));
			}
		}

		private static bool ValidateModerateTextResponse(ModerateTextResponse response)
		{
			bool? moderated = response.Moderated;
			return moderated.HasValue && response.Text != null;
		}

		private static void CheckForValidation(IGuestControllerClient guestControllerClient, string displayName, Action<IValidateDisplayNameResult> callback)
		{
			ValidateRequest validateRequest = new ValidateRequest();
			validateRequest.displayName = displayName;
			ValidateRequest request = validateRequest;
			guestControllerClient.Validate(request, delegate(GuestControllerResult<ValidateResponse> r)
			{
				if (!r.Success)
				{
					callback(new ValidateDisplayNameResult(false));
				}
				else
				{
					ValidateResponse response = r.Response;
					if (response.error == null)
					{
						callback(new ValidateDisplayNameResult(true));
					}
					else
					{
						callback(new ValidateDisplayNameExistsResult(false));
					}
				}
			});
		}
	}
}
