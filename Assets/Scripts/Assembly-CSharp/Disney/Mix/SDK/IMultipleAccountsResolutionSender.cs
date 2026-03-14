using System;

namespace Disney.Mix.SDK
{
	public interface IMultipleAccountsResolutionSender
	{
		void Send(string lookupValue, Action<ISendMultipleAccountsResolutionResult> callback);
	}
}
