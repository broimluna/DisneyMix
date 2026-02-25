using System;

namespace Disney.Mix.SDK.Internal
{
	public interface IMaseResolutionSender
	{
		void Send(string lookupValue, Action<ISendMultipleAccountsResolutionResult> callback);
	}
}
