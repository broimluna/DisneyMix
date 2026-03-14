using System;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountGetter
	{
		public static void GetAllOfficialAccounts(AbstractLogger logger, IUserDatabase userDatabase, IMixWebCallFactory mixWebCallFactory, Action<OfficialAccountsResponse> successCallback, Action failureCallback)
		{
			try
			{
				BaseUserRequest request = new BaseUserRequest();
				IWebCall<BaseUserRequest, OfficialAccountsResponse> webCall = mixWebCallFactory.OfficialAccountAllPost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<OfficialAccountsResponse> e)
				{
					OfficialAccountsResponse response = e.Response;
					if (ValidateResponse(response))
					{
						userDatabase.InsertOrUpdateOfficialAccounts(response.OfficialAccountIds);
						successCallback(e.Response);
					}
					else
					{
						logger.Critical("Failed to validate add followship response: " + JsonParser.ToJson(response));
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

		private static bool ValidateResponse(OfficialAccountsResponse response)
		{
			return response.OfficialAccountIds != null && !response.OfficialAccountIds.Any(delegate(GuestOfficialAccount oa)
			{
				int result;
				if (oa != null && oa.OaId != null && oa.OaName != null)
				{
					bool? isAvailable = oa.IsAvailable;
					if (isAvailable.HasValue)
					{
						bool? canUnfollow = oa.CanUnfollow;
						result = ((!canUnfollow.HasValue) ? 1 : 0);
						goto IL_0046;
					}
				}
				result = 1;
				goto IL_0046;
				IL_0046:
				return (byte)result != 0;
			});
		}
	}
}
