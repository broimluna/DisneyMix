using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public class UsernameRecoverySender : IUsernameRecoverySender
	{
		private readonly AbstractLogger logger;

		private readonly IGuestControllerClient guestControllerClient;

		public UsernameRecoverySender(AbstractLogger logger, IGuestControllerClient guestControllerClient)
		{
			this.logger = logger;
			this.guestControllerClient = guestControllerClient;
		}

		public void Send(string lookupValue, Action<ISendUsernameRecoveryResult> callback)
		{
			try
			{
				RecoverRequest recoverRequest = new RecoverRequest();
				recoverRequest.lookupValue = lookupValue;
				RecoverRequest request = recoverRequest;
				guestControllerClient.RecoverUsername(request, delegate(GuestControllerResult<NotificationResponse> r)
				{
					if (!r.Success)
					{
						callback(new SendUsernameRecoveryResult(false));
					}
					else
					{
						ISendUsernameRecoveryResult recoverUsernameResult = GuestControllerErrorParser.GetRecoverUsernameResult(r.Response.error);
						if (recoverUsernameResult != null)
						{
							callback(recoverUsernameResult);
						}
						else
						{
							callback(new SendUsernameRecoveryResult(r.Response.data != null));
						}
					}
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new SendUsernameRecoveryResult(false));
			}
		}
	}
}
