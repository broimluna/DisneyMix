using System;

namespace Disney.Mix.SDK.Internal
{
	public interface IUsernameRecoverySender
	{
		void Send(string lookupValue, Action<ISendUsernameRecoveryResult> callback);
	}
}
