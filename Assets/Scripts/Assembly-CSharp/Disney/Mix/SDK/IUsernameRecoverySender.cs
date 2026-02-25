using System;

namespace Disney.Mix.SDK
{
	public interface IUsernameRecoverySender
	{
		void Send(string lookupValue, Action<ISendUsernameRecoveryResult> callback);
	}
}
