using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public class NrtUpgradeSender : INrtUpgradeSender
	{
		private readonly AbstractLogger logger;

		private readonly IGuestControllerClient guestControllerClient;

		public NrtUpgradeSender(AbstractLogger logger, IGuestControllerClient guestControllerClient)
		{
			this.logger = logger;
			this.guestControllerClient = guestControllerClient;
		}

		public void Send(string lookupValue, Action<ISendNonRegisteredTransactorUpgradeResult> callback)
		{
			try
			{
				guestControllerClient.UpgradeNrt(new RecoverRequest
				{
					lookupValue = lookupValue
				}, delegate(GuestControllerResult<NotificationResponse> r)
				{
					if (!r.Success)
					{
						callback(new SendNonRegisteredTransactorUpgradeResult(false));
					}
					else
					{
						ISendNonRegisteredTransactorUpgradeResult upgradeNrtResult = GuestControllerErrorParser.GetUpgradeNrtResult(r.Response.error);
						if (upgradeNrtResult != null)
						{
							callback(upgradeNrtResult);
						}
						else
						{
							callback(new SendNonRegisteredTransactorUpgradeResult(r.Response.data != null));
						}
					}
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new SendNonRegisteredTransactorUpgradeResult(false));
			}
		}
	}
}
