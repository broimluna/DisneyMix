using System;

namespace Disney.Mix.SDK.Internal
{
	public interface INrtUpgradeSender
	{
		void Send(string lookupValue, Action<ISendNonRegisteredTransactorUpgradeResult> callback);
	}
}
